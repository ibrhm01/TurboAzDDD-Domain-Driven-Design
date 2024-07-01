using Domain.DTOs.Image;
using Domain.DTOs.Vehicle;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VehicleController(IVehicleService vehicleService, IWebHostEnvironment webHostEnvironment)
        {
            _vehicleService = vehicleService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromForm] UpdateVehicleDto updateVehicleDto)
        {
            if (await _vehicleService.UpdateAsync(id, updateVehicleDto, _webHostEnvironment.WebRootPath)) return Ok();
            else return BadRequest();

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateVehicle([FromForm] CreateVehicleDto createVehicleDto)
        {

            if (await _vehicleService.CreateAsync(createVehicleDto, _webHostEnvironment.WebRootPath)) return Ok();
            else return BadRequest();

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllVehicles()
        {
            return StatusCode(200, await _vehicleService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneVehicle(int id)
        {
            GetVehicleDto getVehicleDto = await _vehicleService.GetOneAsync(id);
            return StatusCode(200, getVehicleDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (await _vehicleService.DeleteAsync(id, _webHostEnvironment.WebRootPath)) return Ok();
            else return BadRequest();
        }

        [HttpPost]
        [Route("getAllFiltered")]
        public IActionResult GetAllFilteredVehicles([FromForm] GetAllFilteredVehiclesDto getAllFilteredVehiclesDto)
        {
            return StatusCode(200, _vehicleService.GetAllFilteredAsync(getAllFilteredVehiclesDto));
        }

    }
}

