using Application.Exceptions;
using Application.Services;
using Domain.DTOs.Brand;
using Domain.DTOs.Color;
using Domain.Exceptions;
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
            try
            {
                int d = await _colorService.UpdateAsync(id, updateColorDto);

                if (d > 0) return Ok();
                else return BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateColor([FromForm] CreateColorDto createColorDto)
        {
            try
            {
                int d = await _colorService.CreateAsync(createColorDto);

                if (d > 0) return Ok();
                else return BadRequest();
            }
            catch (DuplicateNameException ex)
            {
                return BadRequest(ex.Message);
            }
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

            try
            {
                GetColorDto getColorDto = await _colorService.GetOneAsync(id);
                return StatusCode(200, getColorDto);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            try
            {
                int d = await _colorService.DeleteAsync(id);
                if (d > 0) return Ok();
                else return BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

