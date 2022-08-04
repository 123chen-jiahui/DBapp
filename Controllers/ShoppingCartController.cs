using AutoMapper;
using Hospital.Dtos;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/shoppingCart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ShoppingCartController(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        // 与电商网站不同的是，购物车只能由医生访问（添加，删除，批量删除等）
        // 获取购物车信息
        [HttpGet("{PatientId}")]
        [Authorize(Roles = "MedicineToken")] // 属于私有资源，只有医生才能访问
        public async Task<IActionResult> GetShoppingCart([FromRoute] int patientId)
        {
            // 获得购物车
            var shoppingCart = await _userRepository.GetShoppingCartByPatientId(patientId);
            // 病人的购物车从来没有被初始化过，需要在用户注册时初始化

            // 输出ShoppingCart的具体信息，所以需要ShoppingCartDto
            return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart));
        }
    }
}
