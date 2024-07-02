using System;
using Domain.DTOs.DriveType;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriveTypeController : Controller
    {
        private readonly IDriveTypeService _driveTypeService;
        public DriveTypeController(IDriveTypeService driveTypeService)
        {
            _driveTypeService = driveTypeService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateDriveType(int id, [FromForm] UpdateDriveTypeDto updateDriveTypeDto)
        {
            if (await _driveTypeService.UpdateAsync(id, updateDriveTypeDto)) return Ok();
            else return BadRequest();

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateDriveType([FromForm] CreateDriveTypeDto createDriveTypeDto)
        {
            if (await _driveTypeService.CreateAsync(createDriveTypeDto)) return Ok();
            else return BadRequest();

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllDriveTypes()
        {
            return StatusCode(200, await _driveTypeService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneDriveType(int id)
        {
            GetDriveTypeDto getDriveTypeDto = await _driveTypeService.GetOneAsync(id);
            return StatusCode(200, getDriveTypeDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteDriveType(int id)
        {
            if (await _driveTypeService.DeleteAsync(id)) return Ok();
            else return BadRequest();
        }
    }
}

