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
                return NotFound("找不到该医生的排班信息");
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
            for (int i = 0; i < 7; i++)
            {
                int day = scheduleForCreationDto.Day[i];
                int timeSlotId = scheduleForCreationDto.TimeSlotId[i];
                string roomId = scheduleForCreationDto.RoomId[i];
                ScheduleOfOneDayForCreationDto scheduleOfOneDayForCreationDto = new ScheduleOfOneDayForCreationDto();
                /*ScheduleDto scheduleDto = new ScheduleDto();
                scheduleDto.StaffId = staffId;
                scheduleDto.Day = day;
                scheduleDto.TimeSlotId = timeSlotId;
                scheduleDto.RoomId = roomId;
                scheduleToReturn.Add(scheduleDto);*/

                scheduleOfOneDayForCreationDto.StaffId = staffId;
                scheduleOfOneDayForCreationDto.Day = day;
                scheduleOfOneDayForCreationDto.TimeSlotId = timeSlotId;
                scheduleOfOneDayForCreationDto.RoomId = roomId;
                // 根据timeSlotId找到StartTime和EndTime
                var timeSlot = await _affairsRepository.GetTimeSlotAsync(timeSlotId);

                scheduleOfOneDayForCreationDto.Capacity = 6 * (timeSlot.EndTime - timeSlot.StartTime);
                scheduleOfOneDayForCreationDto.Total = scheduleOfOneDayForCreationDto.Capacity;
                scheduleToReturn.Add(_mapper.Map<ScheduleDto>(scheduleOfOneDayForCreationDto));
                _affairsRepository.AddSchedule(_mapper.Map<Schedule>(scheduleOfOneDayForCreationDto));
            }
            await _affairsRepository.SaveAsync();
            /*for (int i = 0; i < 7; i++)
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

        // 更新员工排班信息
        [HttpPut("{staffId}")]
        public async Task<IActionResult> UpdateSchedule(
            [FromRoute] int staffId,
            [FromBody] ScheduleForUpdationDto scheduleForUpdationDto
        )
        {
            // 要进行时间检查，近三天的不能更改
            int day = scheduleForUpdationDto.Day;
            int today = (int)DateTime.Now.DayOfWeek;

            Console.WriteLine("today: {0}", today);

            if ((day + 7 - today) % 7 < 3)
            {
                return BadRequest("只能修改两天后的排班");
            }


            // 原来的数据
            var scheduleOfOneDay = await _affairsRepository.GetScheduleOfOneDay(staffId, day);
            if (scheduleOfOneDay == null)
            {
                return NotFound("所要更新的信息不存在");
            }

            // 修改容量
            var timeSlot = await _affairsRepository.GetTimeSlotAsync(scheduleForUpdationDto.TimeSlotId);
            scheduleOfOneDay.Capacity = 6 * (timeSlot.EndTime - timeSlot.StartTime);
            scheduleOfOneDay.Total = scheduleOfOneDay.Capacity;

            // 映射
            _mapper.Map(scheduleForUpdationDto, scheduleOfOneDay);
            // 保存数据
            await _affairsRepository.SaveAsync();

            return NoContent();
        }
    }
}