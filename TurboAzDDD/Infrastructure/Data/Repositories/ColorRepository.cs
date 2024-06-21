using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public readonly ApplicationDbContext _appDbContext;
        public ColorRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<List<Color>> GetAllAsync()
        {
            return await _appDbContext.Set<Color>().Where(b => !b.IsDeleted).Include(b => b.Vehicles).ToListAsync();
        }
        public override async Task<Color?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<Color>().Where(b => !b.IsDeleted).Include(b => b.Vehicles).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

