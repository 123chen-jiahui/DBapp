﻿using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Profiles
{
    public class TimeSlotProfile : Profile
    {
        public TimeSlotProfile()
        {
            CreateMap<TimeSlotForCreationDto, TimeSlot>();
            CreateMap<TimeSlot, TimeSlotDto>();

            CreateMap<ScheduleDto, Staff_TimeSlot>();
            CreateMap<Staff_TimeSlot, ScheduleDto>();
            CreateMap<ScheduleForUpdationDto, Staff_TimeSlot>();
        }
    }
}