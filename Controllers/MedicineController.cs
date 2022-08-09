using AutoMapper;
using Hospital.Dtos;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("medicine")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        public MedicineController(
            IResourceRepository resourceRepository,
            IMapper mapper
        )
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetMedicines(
            [FromQuery] string keyWord
        )
        {
            var medicine = await _resourceRepository.GetMedicinesAsync(keyWord);

            return Ok(_mapper.Map<IEnumerable<MedicineDto>>(medicine));
        }
    }
}
