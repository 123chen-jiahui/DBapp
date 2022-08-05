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
using System.Security.Claims;
using System.Threading.Tasks;

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
        public OrdersController(
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            IResourceRepository resourceRepository,
            IMapper mapper
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        // 查看历史订单
        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetOrders()
        {
            // 1. 获得当前病人
            // 从http上下文中获得，需要注入http上下文关系对象服务
            // 此处有一个惊天大坑，需要在Setup.cs中加入http服务依赖
            // 详见https://stackoverflow.com/questions/70981655/unable-to-resolve-service-for-type-microsoft-aspnetcore-http-ihttpcontextaccess
            var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // Console.WriteLine("patientId is {0}", patientId);

            // 2. 使用用户Id来获取订单历史记录
            var orders = await _userRepository.GetOrdersByPatientIdAsync(Convert.ToInt32(patientId));

            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        } 

        // 查看订单详情
        [HttpGet("{orderId}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetOrderByOrderId([FromRoute] Guid orderId)
        {
            // 1. 获得当前病人
            var patientId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var order = await _resourceRepository.GetOrderByOrderIdAsync(orderId);

            return Ok(_mapper.Map<OrderDto>(order));
        }
    }
}