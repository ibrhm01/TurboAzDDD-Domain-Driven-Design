using System;
using Domain.DTOs.Model;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController : Controller
    {
        private readonly IModelService _modelService;
        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateModel(int id, [FromForm] UpdateModelDto updateModelDto)
        {
            if (await _modelService.UpdateAsync(id, updateModelDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateModel([FromForm] CreateModelDto createModelDto)
        {
            if (await _modelService.CreateAsync(createModelDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllModels()
        {
            return StatusCode(200, await _modelService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneModel(int id)
        {
            GetModelDto getModelDto = await _modelService.GetOneAsync(id);
            return StatusCode(200, getModelDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            if (await _modelService.DeleteAsync(id) > 0) return Ok();
            else return BadRequest();
        }
    }
}

