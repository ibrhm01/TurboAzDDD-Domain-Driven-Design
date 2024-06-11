using Domain.DTOs.Brand;
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
                if(await _brandService.UpdateAsync(id, updateBrandDto)>0) return Ok();
                else return StatusCode(500);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateBrand([FromForm] CreateBrandDto createBrandDto)
        {
            if (await _brandService.CreateAsync(createBrandDto) > 0) return Ok();
            else return StatusCode(500);

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
                GetBrandDto getBrandDto = await _brandService.GetOneAsync(id);
                return StatusCode(200, getBrandDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (await _brandService.DeleteAsync(id) > 0) return Ok();
            else return StatusCode(500);
        }
    }
}

