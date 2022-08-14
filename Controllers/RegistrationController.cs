using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IAffairsRepository _affairsRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RegistrationController(
            IAffairsRepository affairsRepository,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _affairsRepository = affairsRepository;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public int WeekOfDayToInt(DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday)
            {
                return 0;
            }
            else if (day == DayOfWeek.Monday)
            {
                return 1;
            }
            else if (day == DayOfWeek.Tuesday)
            {
                return 2;
            }
            else if (day == DayOfWeek.Wednesday)
            {
                return 3;
            }
            else if (day == DayOfWeek.Thursday)
            {
                return 4;
            }
            else if (day == DayOfWeek.Friday)
            {
                return 5;
            }
            else
            {
                return 6;
            }

        }

        [HttpPost("checkout")]
        [Authorize]
        public async Task<IActionResult> Checkout([FromBody] RegistrationForCreationDto registrationForCreationDto)
        {
            var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            int staffId = registrationForCreationDto.StaffId;
            int day = registrationForCreationDto.Day;

            // 输入的day与今天的差值小于3
            int today = (int)DateTime.Now.DayOfWeek;
            if ((day + 7 - today) % 7 >= 3)
            {
                return BadRequest("只能预约三天内的挂号");
            }

            // 找到该医生该天的schedul
            var schedule = await _affairsRepository.GetScheduleOfOneDay(staffId, day);
            int timeSlotId = schedule.TimeSlotId;
            int remaining = schedule.Capacity;

            // 获取timeslot以获取总容量
            // 经过改动，现在总容量在total字段中
            var timeSlot = await _affairsRepository.GetTimeSlotAsync(timeSlotId);
            // int total = 6 * (timeSlot.EndTime - timeSlot.StartTime);

            // 预约日期
            DateTime time = DateTime.Now.AddDays((day + 7 - WeekOfDayToInt(DateTime.Now.DayOfWeek)) % 7);

            var registration = new Registration()
            {
                Id = Guid.NewGuid(),
                PatientId = Convert.ToInt32(patientId),
                StaffId = staffId,
                fee = 15,
                Day = day,
                Order = schedule.Total - remaining + 1,
                // Order = total - remaining + 1,
                Time = new DateTime(
                    time.Year,
                    time.Month,
                    time.Day,
                    // DateTime.Now.Year,
                    // DateTime.Today.Year,
                    // DateTime.Now.Month,
                    // DateTime.Today.Month,
                    // DateTime.Now.Day,
                    // DateTime.Today.Day,
                    timeSlot.StartTime + (schedule.Total - remaining) * 10 / 60,
                    (schedule.Total - remaining) * 10 % 60,
                    0
                ),
                RoomId = schedule.RoomId,
                State = OrderStateEnum.Pending,
                CreateDateLocal = DateTime.Now,
                TransactionMetadata = "null"
            };
            
            // 使容量-1
            schedule.Capacity -= 1;

            // 将信息加入到waitline中
            var waitline = new WaitLine()
            {
                Id = Guid.NewGuid(),
                StaffId = registration.StaffId,
                PatientId = registration.PatientId,
                Day = registration.Day,
                Order = registration.Order
            };
            await _affairsRepository.AddWaitLineAsync(waitline);

            // 保存数据
            await _affairsRepository.AddRegistrationAsync(registration);
            await _affairsRepository.SaveAsync();
            return Ok(_mapper.Map<RegistrationDto>(registration));
        }

        // 支付挂号费
        [HttpPost("{registrationId}/placeOrder")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([FromRoute] Guid registrationId)
        {
            // 1. 获得当前用户信息
            var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // 2. 开始处理支付
            var registration = await _affairsRepository.GetRegistrationByRegistrationId(registrationId);
            // var order = await _resourceRepository.GetOrderByOrderIdAsync(orderId);
            registration.PaymentProcessing();
            // order.PaymentProcessing(); // 处理订单，pending->processing
            // 但是目前订单状态的改变是内存中的改变，需要在数据库中持久化
            await _affairsRepository.SaveAsync();
            // await _userRepository.SaveAsync();

            // 3. 向第三方提交支付请求，等待第三方相应
            // 需要添加http请求的服务依赖（见Startup.cs以及本文件最上面）
            var httpClient = _httpClientFactory.CreateClient();
            string url = @"http://123.56.149.216/api/FakePaymentProcess?icode={0}&orderNumber={1}&returnFault={2}";
            var response = await httpClient.PostAsync(
                string.Format(url, "417291C5E5EADD9A", registration.Id, false), // 第一个参数，请求地址
                null // 第二个参数，请求主体
                );

            // Console.WriteLine("response is {0}", response.IsSuccessStatusCode);
            // 4. 从第三方相应中提取支付信息、支付结果
            bool isApprove = false; // 保存支付结果
            string transactionMetadate = ""; // 以字符串形式保存支付信息
            if (response.IsSuccessStatusCode) // 表示返回200 Ok
            {
                transactionMetadate = await response.Content.ReadAsStringAsync();
                // 将相应字符串转换为json对象
                var jsonObject = (JObject)JsonConvert.DeserializeObject(transactionMetadate);
                isApprove = jsonObject["approved"].Value<bool>();
            }

            // 5. 如果第三方支付成功，完成订单
            if (isApprove)
            {
                registration.PaymentApprove();
            }
            else
            {
                registration.PaymentReject();
            }
            registration.TransactionMetadata = transactionMetadate;
            await _affairsRepository.SaveAsync();

            return Ok(_mapper.Map<RegistrationDto>(registration));
        }


        // 查看历史挂号信息
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRegistrations()
        {
            var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var registrations = await _affairsRepository.GetRegistrationsAsync(Convert.ToInt32(patientId));
            return Ok(_mapper.Map<IEnumerable<RegistrationDto>>(registrations));
        }

        // 查看某条挂号详细信息
        [HttpGet("{registrationId}")]
        [Authorize]
        public async Task<IActionResult> GetRegistrationByRegistrationId([FromRoute] Guid registrationId)
        {
            var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var registration = await _affairsRepository.GetRegistrationByRegistrationId(registrationId);
            return Ok(_mapper.Map<RegistrationDto>(registration));
        }
    }
}
