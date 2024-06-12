using Domain.DTOs.BodyType;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BodyTypeController : Controller
    {
        private readonly IBodyTypeService _bodyTypeService;
        public BodyTypeController(IBodyTypeService bodyTypeService)
        {
            _bodyTypeService = bodyTypeService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBodyType(int id, [FromForm] UpdateBodyTypeDto updateBodyTypeDto)
        {
            if (await _bodyTypeService.UpdateAsync(id, updateBodyTypeDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateBodyType([FromForm] CreateBodyTypeDto createBodyTypeDto)
        {
            if (await _bodyTypeService.CreateAsync(createBodyTypeDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllBodyTypes()
        {
            return StatusCode(200, await _bodyTypeService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneBodyType(int id)
        {
            GetBodyTypeDto getBodyTypeDto = await _bodyTypeService.GetOneAsync(id);
            return StatusCode(200, getBodyTypeDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBodyType(int id)
        {
            if (await _bodyTypeService.DeleteAsync(id) > 0) return Ok();
            else return BadRequest();
        }

    }
}

