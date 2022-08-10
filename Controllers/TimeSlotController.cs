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
    [Route("time_slot")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly IAffairsRepository _affairsRepository;
        private readonly IMapper _mapper;
        public TimeSlotController(
            IAffairsRepository affairsRepository,
            IMapper mapper
        )
        {
            _affairsRepository = affairsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        // 身份验证
        public async Task<IActionResult> GetTimeSlots()
        {
            var timeSlots = await _affairsRepository.GetTimeSlotsAsync();
            return Ok(_mapper.Map<IEnumerable<TimeSlotDto>>(timeSlots));
        }

        [HttpGet("{timeSlotId}", Name = "GetTimeSlot")]
         // [Authorize(Roles = "Admin")] // 为方便起见，这里不加权限认证
        public async Task<IActionResult> GetTimeSlotById([FromRoute] int timeSlotId)
        {
            var timeSlot = await _affairsRepository.GetTimeSlotAsync(timeSlotId);
            if (timeSlot == null)
            {
                return NotFound("找不到id为{timeSlot}的Timeslot");
            }

            return Ok(_mapper.Map<TimeSlotDto>(timeSlot));
        }

        [HttpPost]
         // [Authorize(Roles = "Admin")] // 为调试方便起见，这里不加权限认证
        public async Task<IActionResult> CreateTimeSlot(
            [FromBody] TimeSlotForCreationDto timeSlotForCreationDto
        )
        {
            // Dto->Model
            var timeSlot = _mapper.Map<TimeSlot>(timeSlotForCreationDto);

            // 不能重复加入time_slot
            if (await _affairsRepository.TimeSlotExistsAsync(timeSlot))
            {
                return BadRequest("该Time_Slot已存在，请勿重复创建");
            }

            _affairsRepository.AddTimeSlot(timeSlot);
            await _affairsRepository.SaveAsync();

            var timeSlotToReturn = _mapper.Map<TimeSlotDto>(timeSlot);

            // 创建资源，返回的应该是201Create，而不是200 Ok
            // 可以使用asp的内键函数CreateRoute来完成
            return CreatedAtRoute(
                "GetTimeSlot",
                new { timeSlotId = timeSlotToReturn.Id },
                timeSlotToReturn
            );
        }
    }
}
