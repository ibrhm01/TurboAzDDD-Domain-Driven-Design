using Domain.DTOs.Vehicle;
using Domain.Entities;

namespace Domain.Services
{
	public interface IVehicleService
	{

        Task<bool> CreateAsync(CreateVehicleDto createVehicleDto, string webRootPath);
        Task<bool> UpdateAsync(int id, UpdateVehicleDto updateVehicleDto, string webRootPath);
        Task<List<GetVehicleDto>> GetAllAsync();
        List<GetVehicleDto> GetAllFilteredAsync(GetAllFilteredVehiclesDto getAllFilteredVehiclesDto);
        Task<GetVehicleDto> GetOneAsync(int id);
        Task<bool> DeleteAsync(int id, string WebRootPath);
    }
}

