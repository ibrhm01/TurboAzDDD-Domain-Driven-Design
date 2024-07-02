using Domain.DTOs.FuelType;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuelTypeController : Controller
    {
        private readonly IFuelTypeService _fuelTypeService;
        public FuelTypeController(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateFuelType(int id, [FromForm] UpdateFuelTypeDto updateFuelTypeDto)
        {
            if (await _fuelTypeService.UpdateAsync(id, updateFuelTypeDto)) return Ok();
            else return BadRequest();

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateFuelType([FromForm] CreateFuelTypeDto createFuelTypeDto)
        {
            if (await _fuelTypeService.CreateAsync(createFuelTypeDto)) return Ok();
            else return BadRequest();

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllFuelTypes()
        {
            return StatusCode(200, await _fuelTypeService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneFuelType(int id)
        {
            GetFuelTypeDto getFuelTypeDto = await _fuelTypeService.GetOneAsync(id);
            return StatusCode(200, getFuelTypeDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteFuelType(int id)
        {
            if (await _fuelTypeService.DeleteAsync(id)) return Ok();
            else return BadRequest();
        }
    }
}

