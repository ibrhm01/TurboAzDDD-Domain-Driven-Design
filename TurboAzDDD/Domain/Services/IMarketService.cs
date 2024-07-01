using System;
using Domain.DTOs.Market;

namespace Domain.Services
{
	public interface IMarketService
	{
        Task<bool> CreateAsync(CreateMarketDto createMarketDto);
        Task<bool> UpdateAsync(int id, UpdateMarketDto updateMarketDto);
        Task<List<GetMarketDto>> GetAllAsync();
        Task<GetMarketDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

