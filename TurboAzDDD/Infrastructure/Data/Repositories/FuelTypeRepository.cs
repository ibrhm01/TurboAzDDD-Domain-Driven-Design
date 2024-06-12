using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class FuelTypeRepository : RepositoryBase<FuelType>, IFuelTypeRepository
    {
        public readonly ApplicationDbContext _appDbContext;
        public FuelTypeRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<IEnumerable<FuelType>> GetAllAsync()
        {
            return await _appDbContext.Set<FuelType>().Include(b => b.Vehicles).ToListAsync();
        }
        public override async Task<FuelType?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<FuelType>().Include(b => b.Vehicles).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

