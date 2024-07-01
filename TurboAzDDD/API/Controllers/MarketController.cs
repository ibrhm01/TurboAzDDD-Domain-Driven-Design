using Domain.DTOs.Market;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketController : Controller
    {
        private readonly IMarketService _marketService;
        public MarketController(IMarketService marketService)
        {
            _marketService = marketService;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateMarket(int id, [FromForm] UpdateMarketDto updateMarketDto)
        {
            if (await _marketService.UpdateAsync(id, updateMarketDto)) return Ok();
            else return BadRequest();

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateMarket([FromForm] CreateMarketDto createMarketDto)
        {
            if (await _marketService.CreateAsync(createMarketDto)) return Ok();
            else return BadRequest();

        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllMarkets()
        {
            return StatusCode(200, await _marketService.GetAllAsync());
        }

        [HttpGet]
        [Route("getOne/{id}")]
        public async Task<IActionResult> GetOneMarket(int id)
        {
            GetMarketDto getMarketDto = await _marketService.GetOneAsync(id);
            return StatusCode(200, getMarketDto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteMarket(int id)
        {
            if (await _marketService.DeleteAsync(id)) return Ok();
            else return BadRequest();
        }
    }
}

