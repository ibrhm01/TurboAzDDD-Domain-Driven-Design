using Domain.DTOs.Vehicle;
using Domain.Entities;

namespace Domain.Services
{
	public interface IVehicleService
	{

        Task<int> CreateAsync(CreateVehicleDto createVehicleDto, string webRootPath);
        Task<int> UpdateAsync(int id, UpdateVehicleDto updateVehicleDto, string webRootPath);
        Task<IEnumerable<GetVehicleDto>> GetAllAsync();
        IEnumerable<GetVehicleDto> GetAllFilteredAsync(GetAllFilteredVehiclesDto getAllFilteredVehiclesDto);
        Task<GetVehicleDto> GetOneAsync(int id);
        Task<int> DeleteAsync(int id, string WebRootPath);
    }
}

