using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 有关病人的操作
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

        /*[HttpPost("guahao")]
        public IActionResult Guahao([FromBody] GuahaoDto guahaoDto)
        {
            // 这里只是简单地将挂号信息加入到数据库中，并没有进行其他检查
            // 后面还要加入细节
            var guahao = _mapper.Map<Registration>(guahaoDto);
            _userRepository.AddRegistration(guahao);
            return Ok();
        }*/

        [HttpGet("{patientId}")]
        [Authorize]
        public async Task<IActionResult> GetPatientByPatientId([FromRoute] int patientId)
        {
            // 这里最好能加一个病史的功能，但是现在方便起见，先不加入
            var patient = await _userRepository.GetPatientByPatientIdAsync(patientId);

            return Ok(_mapper.Map<PatientDto>(patient));
        }
    }
}
