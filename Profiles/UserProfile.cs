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

        public string GetGender(Gender gender)
        {
            if (gender == Gender.female)
            {
                return "女";
            }
            else
            {
                return "男";
            }
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

            CreateMap<Patient, PatientDto>()
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => GetAge(src.Birthday))
                )
                .ForMember(
                    dest => dest.Gender,
                    opt => opt.MapFrom(src => GetGender(src.Gender))
            );
        }
    }
}
