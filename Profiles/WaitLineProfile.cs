using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Profiles
{
    public class WaitLineProfile : Profile
    {
        public WaitLineProfile()
        {
            CreateMap<WaitLine, WaitLineDto>();
        }
    }
}
