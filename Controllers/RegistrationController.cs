using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("checkout")]
        [Authorize]
        public async Task<IActionResult> Checkout([FromBody] RegistrationForCreationDto registrationForCreationDto)
        {
            var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            int staffId = registrationForCreationDto.StaffId;
            int day = registrationForCreationDto.Day;

            // 前端保证输入的day与今天的差值小于等于3

            // 找到该医生该天的schedul
            var schedule = await _affairsRepository.GetScheduleOfOneDay(staffId, day);
            int timeSlotId = schedule.TimeSlotId;
            int remaining = schedule.Capacity;

            // 获取timeslot以获取总容量
            var timeSlot = await _affairsRepository.GetTimeSlotAsync(timeSlotId);
            int total = 6 * (timeSlot.EndTime - timeSlot.StartTime);

            var registration = new Registration()
            {
                Id = Guid.NewGuid(),
                PatientId = Convert.ToInt32(patientId),
                StaffId = staffId,
                fee = 15,
                Day = day,
                Order = total - remaining + 1,
                Time = new DateTime(
                    DateTime.Today.Year,
                    DateTime.Today.Month,
                    DateTime.Today.Day,
                    timeSlot.StartTime + (total - remaining) * 10 / 60,
                    (total - remaining) * 10 % 60,
                    0
                ),
                RoomId = schedule.RoomId,
                State = OrderStateEnum.Pending,
                CreateDateUTC = DateTime.UtcNow,
                TransactionMetadata = "null"
            };

            // 使容量-1
            schedule.Capacity -= 1;

            // 保存数据
            await _affairsRepository.AddRegistrationAsync(registration);
            await _affairsRepository.SaveAsync();
            return Ok(_mapper.Map<RegistrationDto>(registration));
        }
    }
}
