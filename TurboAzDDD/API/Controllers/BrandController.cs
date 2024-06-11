using Application.Exceptions;
using Domain.DTOs.Brand;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromForm] UpdateBrandDto updateBrandDto)
        {
            try
            {
                int d = await _brandService.UpdateAsync(id, updateBrandDto) ;

                if (d > 0) return Ok();
                else return BadRequest();
            }
            catch(EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(DuplicateNameException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateBrand([FromForm] CreateBrandDto createBrandDto)
        {

            try
            {
                int d = await _brandService.CreateAsync(createBrandDto);

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
        public async Task<IActionResult> GetAllBrands()
        {

            return StatusCode(200, await _brandService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneBrand(int id)
        {

            try
            {
                GetBrandDto getBrandDto = await _brandService.GetOneAsync(id);
                return StatusCode(200, getBrandDto);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                int d = await _brandService.DeleteAsync(id);
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

