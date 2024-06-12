using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class BodyTypeRepository : RepositoryBase<BodyType>, IBodyTypeRepository
    {
        public readonly ApplicationDbContext _appDbContext;
        public BodyTypeRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<IEnumerable<BodyType>> GetAllAsync()
        {
            return await _appDbContext.Set<BodyType>().Include(b => b.Vehicles).ToListAsync();
        }
        public override async Task<BodyType?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<BodyType>().Include(b => b.Vehicles).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

