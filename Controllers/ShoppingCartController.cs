using AutoMapper;
using FakeXiecheng.API.Helper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 有关购物车的操作
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
            var shoppingCart = await _userRepository.GetShoppingCartByPatientIdAsync(patientId);
            // 病人的购物车从来没有被初始化过，需要在用户注册时初始化

            // 输出ShoppingCart的具体信息，所以需要ShoppingCartDto
            return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart));
        }

        // 向购物车中添加items
        [HttpPost("items/{PatientId}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddShoppingCartItem(
            [FromRoute] int patientId,
            [FromBody] AddShoppingCartItemDto addShoppingCartItemDto
        )
        {
            // 获得购物车
            var shoppingCart = await _userRepository.GetShoppingCartByPatientIdAsync(patientId);

            // 创建LineItem
            var medicine = await _resourceRepository
                .GetMedicineAsync(addShoppingCartItemDto.Id);
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
            await _userRepository.SaveAsync();

            // 输出购物车新数据
            return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart));

        }

        // 从购物车中删除商品
        [HttpDelete("items/{patientId}/{lineItemId}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteShoppingCartItem(
            [FromRoute] int patientId,
            [FromRoute] int lineItemId
        )
        {
            Console.WriteLine("itemId is {0}", lineItemId);
            // patientId和itemId需要对应
            // 比如对某个病人进行操作时，不能删除另一个病人购物车中的item

            // 根据patientId获取ShoppingCart
            var shoppingCart_1 = await _userRepository.GetShoppingCartByPatientIdAsync(patientId);
            // 根据itemId获取整个LineItem
            var lineItem = await _resourceRepository.GetShoppingCartItemByItemIdAsync(lineItemId);
            if (lineItem == null)
            {
                return NotFound("当前处方中无该项");
            }
            // 判断是否属于同一购物车
            if (shoppingCart_1.Id != lineItem.ShoppingCartId)
            {
                return BadRequest("不能删除其他病人的处方");
            }

            _resourceRepository.DeleteShoppingCartItem(lineItem);
            await _userRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("items/{patientId}/({lineItemIds})")]
        [Authorize(Roles = "Doctor")]   
        public async Task<IActionResult> RemoveShoppingCartItems(
            [FromRoute] int patientId,
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            [FromRoute] IEnumerable<int> lineItemIds // 模板，将字符串列表转换为int列表
        )
        {
            // 1. 批量获得购物车商品
            // 2. 批量删除这些商品
            // 所以需要添加两个接口

            var items = await _resourceRepository.GetShoppingCartItemsByItemIdListAsync(lineItemIds);
            _resourceRepository.DeleteShoppingCartItems(items);
            await _userRepository.SaveAsync();

            return NoContent();
        }

        [HttpPost("checkout/{patientId}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Checkout([FromRoute] int patientId)
        {
            // 获取购物车
            var shoppingCart = await _userRepository.GetShoppingCartByPatientIdAsync(patientId);
            // 创建订单
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                PatientId = patientId,
                State = OrderStateEnum.Pending,
                OrderItems = shoppingCart.ShoppingCartItems,
                CreateDateUTC = DateTime.UtcNow,
                TransactionMetadata = "what"
            };

            /*var items = await _resourceRepository.GetShoppingCartItemsByShoppingCartIdAsync(shoppingCart.Id);

            shoppingCart.ShoppingCartItems = null;
            await _userRepository.SaveAsync();

            order.OrderItems = items;
            await _userRepository.AddOrderAsync(order);
            await _userRepository.SaveAsync();*/

            // shoppingCart.ShoppingCartItems.Clear();
            // 清空购物车
            shoppingCart.ShoppingCartItems = null;
            // 保存数据
            await _userRepository.AddOrderAsync(order);

            await _userRepository.SaveAsync();

            return Ok(_mapper.Map<OrderDto>(order));
        }

    }
}
