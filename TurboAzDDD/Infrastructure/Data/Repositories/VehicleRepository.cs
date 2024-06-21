using Domain.DTOs.Vehicle;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public readonly ApplicationDbContext _appDbContext;
        public VehicleRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<List<Vehicle>> GetAllAsync()
        {
            return await _appDbContext.Set<Vehicle>().Where(b => !b.IsDeleted).Include(v => v.TagVehicles).ThenInclude(v => v.Tag).Include(v => v.Images).ToListAsync();
        }

        public IQueryable<Vehicle> GetAllQueryable()
        {
            return _appDbContext.Set<Vehicle>().Where(b => !b.IsDeleted).Include(v => v.TagVehicles).ThenInclude(v => v.Tag).Include(v => v.Images).AsQueryable();
        }

        public override async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<Vehicle>().Where(b => !b.IsDeleted).Include(v => v.TagVehicles).ThenInclude(v => v.Tag).Include(v => v.Images).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}