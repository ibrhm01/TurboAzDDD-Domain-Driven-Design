using System;
using Domain.DTOs.Image;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Extensions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IImageService imageService, IWebHostEnvironment webHostEnvironment)
        {
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateImage(int id, [FromForm] UpdateImageDto updateImageDto)
        {
            if (await _imageService.UpdateAsync(id, updateImageDto, _webHostEnvironment.WebRootPath) > 0) return Ok();
            else return BadRequest();
            
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateImage([FromForm] CreateImageDto createImageDto)
        {
            if (await _imageService.CreateAsync(createImageDto, _webHostEnvironment.WebRootPath) > 0) return Ok();
            else return BadRequest();
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllImages()
        {
            return StatusCode(200, await _imageService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneImage(int id)
        {
            GetImageDto getImageDto = await _imageService.GetOneAsync(id);
            return StatusCode(200, getImageDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {

            if (await _imageService.DeleteAsync(id, _webHostEnvironment.WebRootPath) > 0) return Ok();
            else return BadRequest();
        }
    }
}

