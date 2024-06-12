using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{

    public class MarketRepository : RepositoryBase<Market>, IMarketRepository
    {
        public readonly ApplicationDbContext _appDbContext;
        public MarketRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<IEnumerable<Market>> GetAllAsync()
        {
            return await _appDbContext.Set<Market>().Include(b => b.Vehicles).ToListAsync();
        }
        public override async Task<Market?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<Market>().Include(b => b.Vehicles).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

