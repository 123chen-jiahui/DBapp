using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 有关员工和排班的信息
namespace Hospital.Controllers
{
    [Route("schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IAffairsRepository _affairsRepository;
        private readonly IMapper _mapper;

        public ScheduleController(
            IAffairsRepository affairsRepository,
            IMapper mapper
        )
        {
            _affairsRepository = affairsRepository;
            _mapper = mapper;
        }

        // 获得员工排班信息
        [HttpGet("{staffId}", Name = "GetSchedule")]
        // 身份验证
        public async Task<IActionResult> GetSchedule([FromRoute] int staffId)
        {
            var schedule = await _affairsRepository.GetScheduleAsync(staffId);
            if (schedule == null || schedule.Count() == 0)
            {
                return BadRequest("找不到该医生的排班信息");
            }

            // var schedule_ordered = schedule.OrderBy(s => s.Day);
            return Ok(_mapper.Map<IEnumerable<ScheduleDto>>(schedule));
        }

        // 添加员工排班信息，以周为单位
        [HttpPost]
        // 身份验证
        public async Task<IActionResult> AddSchedule(
            [FromBody] ScheduleForCreationDto scheduleForCreationDto    
        )
        {
            // 查找该医生是否已经创建schedule，如已创建，应报错
            var staffId = scheduleForCreationDto.StaffId; // 这句的作用仅仅是为了减小长度
            if (await _affairsRepository.ScheduleExistsAsync(staffId))
            {
                return BadRequest("该员工排班信息已存在，请勿重新创建");
            }

            // 将7天的排班信息加入到数据库中的Staff_TimeSlot中
            var scheduleToReturn = new List<ScheduleDto>();
            for (int i = 0; i < 7; i ++)
            {
                int day = scheduleForCreationDto.Day[i];
                int timeSlotId = scheduleForCreationDto.TimeSlotId[i];
                ScheduleDto scheduleDto = new ScheduleDto();
                scheduleDto.StaffId = staffId;
                scheduleDto.Day = day;
                scheduleDto.TimeSlotId = timeSlotId;
                scheduleToReturn.Add(scheduleDto);
                _affairsRepository.AddSchedule(_mapper.Map<Staff_TimeSlot>(scheduleDto));
            }
            await _affairsRepository.SaveAsync();
            /*for (int i = 0; i < 7; i ++)
            {
                Console.WriteLine("{0} {1}", scheduleForCreationDto.Day[i], scheduleForCreationDto.TimeSlotId[i]);
            }*/
            var scheduleToReturn_Ordered = scheduleToReturn.OrderBy(s => s.Day);
            return CreatedAtRoute(
                "GetSchedule",
                new { staffId = staffId },
                scheduleToReturn_Ordered
            );
        }
    }
}
