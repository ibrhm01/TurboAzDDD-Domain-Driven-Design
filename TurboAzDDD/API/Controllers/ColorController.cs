using Domain.DTOs.Color;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;
        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateColor(int id, [FromForm] UpdateColorDto updateColorDto)
        {
                if(await _colorService.UpdateAsync(id, updateColorDto)) return Ok();
                else return BadRequest();
            
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateColor([FromForm] CreateColorDto createColorDto)
        {
                if(await _colorService.CreateAsync(createColorDto)) return Ok();
                else return BadRequest();
            
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllColors()
        {
            return StatusCode(200, await _colorService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneColor(int id)
        {
                GetColorDto getColorDto = await _colorService.GetOneAsync(id);
                return StatusCode(200, getColorDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            if(await _colorService.DeleteAsync(id)) return Ok();
            else return BadRequest();
        }
    }
}

