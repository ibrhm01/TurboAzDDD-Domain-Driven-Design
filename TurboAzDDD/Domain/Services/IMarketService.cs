using System;
using Domain.DTOs.Market;

namespace Domain.Services
{
	public interface IMarketService
	{
        Task<int> CreateAsync(CreateMarketDto createMarketDto);
        Task<int> UpdateAsync(int id, UpdateMarketDto updateMarketDto);
        Task<IEnumerable<GetMarketDto>> GetAllAsync();
        Task<GetMarketDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

