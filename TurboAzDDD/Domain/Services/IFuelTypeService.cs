using System;
using Domain.DTOs.FuelType;

namespace Domain.Services
{
	public interface IFuelTypeService
	{
        Task<int> CreateAsync(CreateFuelTypeDto createFuelTypeDto);
        Task<int> UpdateAsync(int id, UpdateFuelTypeDto updateFuelTypeDto);
        Task<IEnumerable<GetFuelTypeDto>> GetAllAsync();
        Task<GetFuelTypeDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

