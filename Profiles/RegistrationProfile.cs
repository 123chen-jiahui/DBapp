using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Profiles
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<Registration, RegistrationDto>()
                .ForMember(
                    dest => dest.State,
                    opt => opt.MapFrom(src => src.State.ToString())
                );
        }
    }
}
