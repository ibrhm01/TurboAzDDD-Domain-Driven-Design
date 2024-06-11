using Domain.DTOs.Transmission;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransmissionController : Controller
    {
        private readonly ITransmissionService _transmissionService;
        public TransmissionController(ITransmissionService transmissionService)
        {
            _transmissionService = transmissionService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateTransmission(int id, [FromForm] UpdateTransmissionDto updateTransmissionDto)
        {
            if (await _transmissionService.UpdateAsync(id, updateTransmissionDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTransmission([FromForm] CreateTransmissionDto createTransmissionDto)
        {
            if (await _transmissionService.CreateAsync(createTransmissionDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllTransmissions()
        {
            return StatusCode(200, await _transmissionService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneTransmission(int id)
        {
            GetTransmissionDto getTransmissionDto = await _transmissionService.GetOneAsync(id);
            return StatusCode(200, getTransmissionDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTransmission(int id)
        {
            if (await _transmissionService.DeleteAsync(id) > 0) return Ok();
            else return BadRequest();
        }
    }
}

