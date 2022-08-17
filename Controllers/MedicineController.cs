using AutoMapper;
using Hospital.Dtos;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 有关药品的信息
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

        // 医生根据关键词查找药品
        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetMedicines(
            [FromQuery] string keyWord
        )
        {
            var medicine = await _resourceRepository.GetMedicinesAsync(keyWord);

            return Ok(_mapper.Map<IEnumerable<MedicineDto>>(medicine));
        }

        [HttpPost("output")]
        [Authorize] // 这里的权限验证是为了方便起见
        public async Task<IActionResult> OutPut([FromBody] MedicineForDeletionDto medicineForDeletionDto)
        {
            int idLen = medicineForDeletionDto.Id.Length;
            int numberLen = medicineForDeletionDto.Number.Length;
            
            if (idLen != numberLen)
            {
                return BadRequest("药品和数量无法一一对应");
            }

            for (int i = 0; i < idLen; i ++)
            {
                var medicine = await _resourceRepository.GetMedicineAsync(medicineForDeletionDto.Id[i]);
                medicine.Inventory -= medicineForDeletionDto.Number[i];
            }
            await _resourceRepository.SaveAsync();
            return NoContent();
        }

    }
}
