using System;
using Domain.DTOs.Tag;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromForm] UpdateTagDto updateTagDto)
        {
            if (await _tagService.UpdateAsync(id, updateTagDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTag([FromForm] CreateTagDto createTagDto)
        {
            if (await _tagService.CreateAsync(createTagDto) > 0) return Ok();
            else return BadRequest();

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllTags()
        {
            return StatusCode(200, await _tagService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneTag(int id)
        {
            GetTagDto getTagDto = await _tagService.GetOneAsync(id);
            return StatusCode(200, getTagDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            if (await _tagService.DeleteAsync(id) > 0) return Ok();
            else return BadRequest();
        }
    }
}

