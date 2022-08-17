using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.ResourceParameter;
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

// 有关订单的信息
namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        public OrdersController(
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            IResourceRepository resourceRepository,
            IMapper mapper,
            IHttpClientFactory httpClientFactory
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        // 查看历史订单
        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetOrders(
            [FromQuery] PageResourceParameter parameter
        )
        {
            // 1. 获得当前病人
            // 从http上下文中获得，需要注入http上下文关系对象服务
            // 此处有一个惊天大坑，需要在Setup.cs中加入http服务依赖
            // 详见https://stackoverflow.com/questions/70981655/unable-to-resolve-service-for-type-microsoft-aspnetcore-http-ihttpcontextaccess
            var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // Console.WriteLine("patientId is {0}", patientId);

            // 2. 使用用户Id来获取订单历史记录
            var orders = await _userRepository.GetOrdersByPatientIdAsync(Convert.ToInt32(patientId), parameter.PageNumber, parameter.PageSize);

            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        } 

        // 查看订单详情
        [HttpGet("{orderId}")]
        [Authorize/*(Roles = "Patient")*/]
        public async Task<IActionResult> GetOrderByOrderId([FromRoute] Guid orderId)
        {
            // 1. 获得当前病人
            /*var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;*/

            var order = await _resourceRepository.GetOrderByOrderIdAsync(orderId);

            return Ok(_mapper.Map<OrderDto>(order));
        }

        // 支付的过程
        // 发起支付请求、处理支付请求、通知支付结果
        [HttpPost("{orderId}/placeOrder")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> PlaceOrder([FromRoute] Guid orderId)
        {
            // 1. 获得当前用户信息
            var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // 2. 开始处理支付
            var order = await _resourceRepository.GetOrderByOrderIdAsync(orderId);
            order.PaymentProcessing(); // 处理订单，pending->processing
            // 但是目前订单状态的改变是内存中的改变，需要在数据库中持久化
            await _userRepository.SaveAsync();

            // 3. 向第三方提交支付请求，等待第三方相应
            // 需要添加http请求的服务依赖（见Startup.cs以及本文件最上面）
            var httpClient = _httpClientFactory.CreateClient();
            string url = @"http://123.56.149.216/api/FakePaymentProcess?icode={0}&orderNumber={1}&returnFault={2}";
            var response = await httpClient.PostAsync(
                string.Format(url, "417291C5E5EADD9A", order.Id, false), // 第一个参数，请求地址
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
                order.PaymentApprove();
            } 
            else
            {
                order.PaymentReject();
            }
            order.TransactionMetadata = transactionMetadate;
            await _userRepository.SaveAsync();

            return Ok(_mapper.Map<OrderDto>(order));
        }
    }
}