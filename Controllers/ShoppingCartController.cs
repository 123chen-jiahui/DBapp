using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
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
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        public ShoppingCartController(
            IUserRepository userRepository,
            IResourceRepository resourceRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }
        // 与电商网站不同的是，购物车只能由医生访问（添加，删除，批量删除等）
        // 获取购物车信息
        [HttpGet("{PatientId}")]
        [Authorize(Roles = "Doctor")] // 属于私有资源，只有医生才能访问
        public async Task<IActionResult> GetShoppingCart([FromRoute] int patientId)
        {
            // 获得购物车
            var shoppingCart = await _userRepository.GetShoppingCartByPatientId(patientId);
            // 病人的购物车从来没有被初始化过，需要在用户注册时初始化

            // 输出ShoppingCart的具体信息，所以需要ShoppingCartDto
            return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart));
        }

        [HttpPost("items/{PatientId}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddShoppingCartItem(
            [FromRoute] int patientId,
            [FromBody] AddShoppingCartItemDto addShoppingCartItemDto
        )
        {
            // 获得购物车
            var shoppingCart = await _userRepository.GetShoppingCartByPatientId(patientId);

            // 创建LineItem
            var medicine = _resourceRepository
                .GetMedicine(addShoppingCartItemDto.Id);
            if (medicine == null)
            {
                return NotFound("药品不存在");
            }
            var lineItem = new LineItem()
            {
                MedicineId = addShoppingCartItemDto.Id,
                ShoppingCartId = shoppingCart.Id,
                Price = medicine.Price
            };

            // 添加lineItem，并保存数据库
            _resourceRepository.AddShoppingCartItem(lineItem);
            _userRepository.Save();

            // 输出购物车新数据
            return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart));

        }
    }
}
