using AutoMapper;
using Hospital.Dtos;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public StaffController(
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{DepartmentId}")]
        public IActionResult GetStafffs([FromRoute] string departmentId) // 路由都是string
        {
            int Id = Convert.ToInt32(departmentId);
            var staffsFromRepo = _userRepository.GetStaffs(Id);
            if (staffsFromRepo == null || staffsFromRepo.Count() <= 0)
            {
                return NotFound("找不到任何医生");
            }
            var staffDto = _mapper.Map<IEnumerable<StaffDto>>(staffsFromRepo);
            return Ok(staffDto);
        }
    }
}
