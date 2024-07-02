using Domain.DTOs.Vehicle;
using Domain.Entities;

namespace Domain.Services
{
	public interface IVehicleService
	{

        Task<bool> CreateAsync(CreateVehicleDto createVehicleDto);
        Task<bool> UpdateAsync(int id, UpdateVehicleDto updateVehicleDto);
        Task<List<GetVehicleDto>> GetAllAsync();
        List<GetVehicleDto> GetAllFilteredAsync(GetAllFilteredVehiclesDto getAllFilteredVehiclesDto);
        Task<GetVehicleDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

