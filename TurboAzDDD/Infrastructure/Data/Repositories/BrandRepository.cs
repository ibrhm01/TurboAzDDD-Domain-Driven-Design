using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public readonly ApplicationDbContext _appDbContext;
        public BrandRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _appDbContext.Set<Brand>().Include(b => b.Models).ToListAsync();
        }
        public override async Task<Brand?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<Brand>().Include(b => b.Models).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

