using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public VehicleRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}