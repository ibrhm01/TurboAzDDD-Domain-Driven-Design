using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class SalonRepository : RepositoryBase<Salon>, ISalonRepository
    {

        public readonly ApplicationDbContext _appDbContext;
        public SalonRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<IEnumerable<Salon>> GetAllAsync()
        {
            return await _appDbContext.Set<Salon>().Include(b => b.Vehicles).ToListAsync();
        }
        public override async Task<Salon?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<Salon>().Include(b => b.Vehicles).FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}

