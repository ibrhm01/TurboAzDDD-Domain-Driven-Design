using Domain.DTOs.Vehicle;
using Domain.Entities;

namespace Domain.Repositories
{
	public interface IVehicleRepository : IRepositoryBase<Vehicle>
	{
        public IQueryable<Vehicle> GetAllQueryable();
    }
}

