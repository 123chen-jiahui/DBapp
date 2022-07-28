using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PatientsController(
            IConfiguration configuration,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("guahao")]
        public IActionResult Guahao([FromBody] GuahaoDto guahaoDto)
        {
            // 这里只是简单地将挂号信息加入到数据库中，并没有进行其他检查
            // 后面还要加入细节
            var guahao = _mapper.Map<Registration>(guahaoDto);
            _userRepository.AddRegistration(guahao);
            return Ok();
        }
    }
}
