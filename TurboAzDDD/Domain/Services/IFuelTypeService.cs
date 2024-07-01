using System;
using Domain.DTOs.FuelType;

namespace Domain.Services
{
	public interface IFuelTypeService
	{
        Task<bool> CreateAsync(CreateFuelTypeDto createFuelTypeDto);
        Task<bool> UpdateAsync(int id, UpdateFuelTypeDto updateFuelTypeDto);
        Task<List<GetFuelTypeDto>> GetAllAsync();
        Task<GetFuelTypeDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

