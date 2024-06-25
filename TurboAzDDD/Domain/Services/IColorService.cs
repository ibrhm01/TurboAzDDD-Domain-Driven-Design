using System;
using Domain.DTOs.Brand;
using Domain.DTOs.Color;

namespace Domain.Services
{
	public interface IColorService
	{
        Task<int> CreateAsync(CreateColorDto createColorDto);
        Task<int> UpdateAsync(int id, UpdateColorDto updateColorDto);
        Task<List<GetColorDto>> GetAllAsync();
        Task<GetColorDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

