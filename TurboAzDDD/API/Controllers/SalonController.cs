using Domain.DTOs.Salon;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalonController : Controller
    {
        private readonly ISalonService _salonService;
        public SalonController(ISalonService salonService)
        {
            _salonService = salonService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateSalon(int id, [FromForm] UpdateSalonDto updateSalonDto)
        {
            if (await _salonService.UpdateAsync(id, updateSalonDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSalon([FromForm] CreateSalonDto createSalonDto)
        {
            if (await _salonService.CreateAsync(createSalonDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllSalons()
        {
            return StatusCode(200, await _salonService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneSalon(int id)
        {
            GetSalonDto getSalonDto = await _salonService.GetOneAsync(id);
            return StatusCode(200, getSalonDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteSalon(int id)
        {
            if (await _salonService.DeleteAsync(id) > 0) return Ok();
            else return BadRequest();
        }
    }
}

