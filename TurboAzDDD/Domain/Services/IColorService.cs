using System;
using Domain.DTOs.Brand;
using Domain.DTOs.Color;

namespace Domain.Services
{
	public interface IColorService
	{
        Task<bool> CreateAsync(CreateColorDto createColorDto);
        Task<bool> UpdateAsync(int id, UpdateColorDto updateColorDto);
        Task<List<GetColorDto>> GetAllAsync();
        Task<GetColorDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

