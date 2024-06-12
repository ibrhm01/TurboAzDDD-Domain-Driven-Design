using System;
using Domain.DTOs.Salon;

namespace Domain.Services
{
	public interface ISalonService
	{
        Task<int> CreateAsync(CreateSalonDto createSalonDto);
        Task<int> UpdateAsync(int id, UpdateSalonDto updateSalonDto);
        Task<IEnumerable<GetSalonDto>> GetAllAsync();
        Task<GetSalonDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}

