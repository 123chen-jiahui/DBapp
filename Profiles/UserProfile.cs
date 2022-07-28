using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Profiles
{
    public class UserProfile : Profile
    {
        public int GetAge(DateTime birthday)
        {
            DateTime n = DateTime.Now; // To avoid a race condition around midnight
            int age = n.Year - birthday.Year;

            if (n.Month < birthday.Month || (n.Month == birthday.Month && n.Day < birthday.Day))
                age--;

            return age;
        }

        public UserProfile()
        {
            CreateMap<RegisterForPatientDto, Patient>();
            CreateMap<RegisterForStaffDto, Staff>();
            CreateMap<Staff, StaffDto>();
            CreateMap<Staff, StaffDto>()
                .ForMember(
                    dest => dest.age,
                    opt => opt.MapFrom(src => GetAge(src.Birthday))
            );
            CreateMap<GuahaoDto, Registration>();
        }
    }
}
