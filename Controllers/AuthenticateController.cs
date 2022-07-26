using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        // 通过构造函数注入配置文件服务依赖
        private readonly IConfiguration _configuration;
        // 通过构造函数注入数据仓库服务依赖
        private IPatientRepository _patientRepository; // 注意这里要注入接口而不是类！ 
        // 通过构造函数注入automapper服务
        private readonly IMapper _mapper;

        public AuthenticateController(
            IConfiguration configuration,
            IPatientRepository patientRepository,
            IMapper mapper
        )
        {
            _configuration = configuration;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginForPatient([FromBody] LoginForPatientDto loginForPatientDto)
        {
            // 1. 验证用户名和密码，方便起见密码明文验证
            // 用户是否存在
            if (!_patientRepository.PatientExistsByPatientId(loginForPatientDto.Id))
            {
                return BadRequest("用户名或密码错误");
            }

            // 取得病人model
            var patient = _patientRepository.GetPatientByPatientId(loginForPatientDto.Id);
            if (patient.Password != loginForPatientDto.Password)
            {
                return BadRequest("用户名或密码错误");
            }
            
            // 2. 创建jwt token
            // header：定义了token编码的算法，我们使用最通用的HmacSha256编码
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;

            // payload：属于自定义的内容，根据项目需求进行填写，可能会用到用户的Id，用户名，email等
            // 需要使用Claim进行处理
            var claims = new[] // 声明一个claim数组
            {
                // 在jwt中，Id有一个专有名词叫sub
                new Claim(JwtRegisteredClaimNames.Sub, patient.Id.ToString()), // 第二个参数是用户Id的准确信息
                new Claim(ClaimTypes.Role, "Patient") // 添加身份认证角色信息
            };

            // signiture：数字签名。签名需要私钥，可以把私钥放入配置文件appsetting.json
            // 然后通过IOC容器将配置文件服务的依赖通过构建函数注入进来，就像TouristRouteRepository.cs中的_context一样
            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]); // 将私钥（字符串）通过UTF8编码转换为字节
            var signingKey = new SymmetricSecurityKey(secretByte); // 使用非对称算法对私钥进行加密
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm); // 使用HmacSha256验证加密后的私钥

            // 使用这些数据创建jwttoken
            var token = new JwtSecurityToken(
                // issuer: "fakexiecheng.com", // 谁发布了token
                // audience: "fakexiecheng.com", // 该token将会发布给谁，就是项目前端，使用统一域名
                // 将以上两条放到appsetting中
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims,
                notBefore: DateTime.UtcNow, // 发布时间
                expires: DateTime.UtcNow.AddDays(1), // 有效期，一天
                signingCredentials // 数字签名
            );

            // 以字符串形式输出token
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            // 3. 在响应主体中添加jwttoken，并return 200OK
            return Ok(tokenStr);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterForPatient([FromBody] RegisterForPatientDto registerForPatientDto)
        {
            // 1. 创建病人对象，病人Id自动生成，

            // 将Dto映射为model
            var patient = _mapper.Map<Patient>(registerForPatientDto);
            // 将model加入数据库
            _patientRepository.AddPatient(patient);
            _patientRepository.Save();

            // 将创建的Id返回
            return Ok(patient.Id);
        }
    }
}
