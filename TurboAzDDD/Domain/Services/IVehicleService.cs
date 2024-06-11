using Domain.Entities;

namespace Domain.Services
{
	public interface IVehicleService
	{

		Task<IEnumerable<Vehicle>> GetAllVehicleAsync();
        Task<Vehicle> GetOneVehicleAsync(int id);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int id);
        Task CreateVehicleAsync(Vehicle vehicle);
    }
}

