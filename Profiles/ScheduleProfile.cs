using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Profiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<ScheduleDto, Schedule>();
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<ScheduleOfOneDayForCreationDto, Schedule>();
            CreateMap<ScheduleOfOneDayForCreationDto, ScheduleDto>();
            CreateMap<ScheduleForUpdationDto, Schedule>();
        }
    }
}
