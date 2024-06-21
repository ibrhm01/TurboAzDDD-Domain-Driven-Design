using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {

        public readonly ApplicationDbContext _appDbContext;
        public TagRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<List<Tag>> GetAllAsync()
        {
            return await _appDbContext.Set<Tag>().Where(b => !b.IsDeleted).Include(t=>t.TagVehicles).ThenInclude(t=>t.Vehicle).ToListAsync();
        }
        public override async Task<Tag?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<Tag>().Where(b => !b.IsDeleted).Include(t => t.TagVehicles).ThenInclude(t => t.Vehicle).FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}

