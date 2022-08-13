using AutoMapper;
using Hospital.Dtos;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    // 该类用于医生门诊操作

    [Route("waitline")]
    [ApiController]
    public class WaitLineController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAffairsRepository _affairsRepository;
        private IMapper _mapper;

        public WaitLineController(
            IHttpContextAccessor httpContextAccessor,
            IAffairsRepository affairsRepository,
            IMapper mapper
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _affairsRepository = affairsRepository;
            _mapper = mapper;
        }

        // 获取病人队列
        [HttpGet("{day}")]
        [Authorize]
        public async Task<IActionResult> GetWaitLine([FromRoute] int day)
        {
            var staffId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Console.WriteLine("staffId is {0}", staffId);
            var waitLines = await _affairsRepository.GetWaitLinesAsync(Convert.ToInt32(staffId), day);
            // Console.WriteLine("waitLines has {0} items", waitLines.Count());
            if (waitLines == null || waitLines.Count() == 0)
            {
                return BadRequest("waitline不存在");
            }
            /*var waitLines_Ordered = waitLines.OrderBy(wl => wl.Order);
            waitLines = waitLines_Ordered;*/

            return Ok(_mapper.Map<IEnumerable<WaitLineDto>>(waitLines));
        }
    }
}
