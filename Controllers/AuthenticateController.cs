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

// 该类用于登录注册
namespace Hospital.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        // 通过构造函数注入配置文件服务依赖
        private readonly IConfiguration _configuration;
        // 通过构造函数注入数据仓库服务依赖
        private IUserRepository _userRepository; // 注意这里要注入接口而不是类！ 
        // 通过构造函数注入automapper服务
        private readonly IMapper _mapper;

        public AuthenticateController(
            IConfiguration configuration,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // 为登录用户创建token
        public string CreateJwtToken(string userId, string roleName)
        {
            // header：定义了token编码的算法，我们使用最通用的HmacSha256编码
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;

            // payload：属于自定义的内容，根据项目需求进行填写，可能会用到用户的Id，用户名，email等
            // 需要使用Claim进行处理
            var claims = new List<Claim> // 声明一个claim数组
            {
                // 在jwt中，Id有一个专有名词叫sub
                new Claim(JwtRegisteredClaimNames.Sub, userId), // 第二个参数是用户Id的准确信息
                new Claim(ClaimTypes.Role, roleName) // 添加身份信息
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
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        [AllowAnonymous]
        [HttpPost("login_patient")]
        public async Task<IActionResult> LoginForPatient([FromBody] LoginForPatientDto loginForPatientDto)
        {
            // 1. 验证用户名和密码，方便起见密码明文验证
            // 用户是否存在
            if (!(await _userRepository.PatientExistsByPatientIdAsync(loginForPatientDto.Id)))
            {
                return BadRequest("用户名或密码错误");
            }

            // 取得病人model
            var patient = await _userRepository.GetPatientByPatientIdAsync(loginForPatientDto.Id);
            if (patient.Password != loginForPatientDto.Password)
            {
                return BadRequest("用户名或密码错误");
            }

            return Ok(CreateJwtToken(patient.Id.ToString(), "Patient"));
        }

        [AllowAnonymous]
        [HttpPost("login_staff")]
        public async Task<IActionResult> LoginForStaff([FromBody] LoginForStaffDto loginForStaffDto)
        {
            var staff = await _userRepository.GetStaffByStaffIdAsync(loginForStaffDto.Id);
            if (staff == null || staff.Password != loginForStaffDto.Password)
            {
                return BadRequest("用户名或密码错误");
            }

            return Ok(CreateJwtToken(staff.Id.ToString(), staff.Role.ToString()));
        }


        [AllowAnonymous]
        [HttpPost("register_patient")]
        public async Task<IActionResult> RegisterForPatientAsync([FromBody] RegisterForPatientDto registerForPatientDto)
        {
            // 检查病人存不存在与数据库，如果存在，返回错误
            if(await _userRepository.PatientExistsByGlobalIdAsync(registerForPatientDto.GlobalId))
            {
                return BadRequest("病人已存在，请勿重复注册");
            }
            // 将Dto映射为model
            var patient = _mapper.Map<Patient>(registerForPatientDto);
            // 将model加入数据库，病人Id自动生成
            _userRepository.AddPatient(patient);
            await _userRepository.SaveAsync();

            // 初始化病人的购物车
            var shoppingCart = new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                PatientId = patient.Id
            };
            // 调用数据仓库，将购物车添加进去
            _userRepository.CreateShoppingCart(shoppingCart);
            await _userRepository.SaveAsync();

            // 将创建的Id返回
            return Ok(patient.Id);
        }


        // 请注意，staff的注册不应通过网页，而是通过管理员操作页面
        // 所以staff注册的权限应该是管理员Admin，其他人不能访问
        // 这里为了调试起见，将访问权限设置为了所有人
        [AllowAnonymous]
        [HttpPost("register_staff")]
        public async Task<IActionResult> RegisterForStaff([FromBody] RegisterForStaffDto registerForStaffDto)
        {
            // 用户名和身份证号都不能相同
            if (await _userRepository.StaffExistsByGlobalIdAsync(registerForStaffDto.GlobalId))
            {
                return BadRequest("身份证号已存在");
            }
            /*else if (_userRepository.StaffExistsByStaffId(registerForStaffDto.Id))
            {
                return BadRequest("用户名已存在");
            }*/
            var staff = _mapper.Map<Staff>(registerForStaffDto);
            _userRepository.AddStaff(staff);
            await _userRepository.SaveAsync();

            return Ok(staff.Id);
        }
    }
}
