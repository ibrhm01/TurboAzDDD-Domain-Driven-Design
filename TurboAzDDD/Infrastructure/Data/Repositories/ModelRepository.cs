using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ModelRepository : RepositoryBase<Model>, IModelRepository
    {

        public readonly ApplicationDbContext _appDbContext;
        public ModelRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<List<Model>> GetAllAsync()
        {
            return await _appDbContext.Set<Model>().Where(b => !b.IsDeleted).Include(b => b.Vehicles).ToListAsync();
        }
        public override async Task<Model?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<Model>().Where(b => !b.IsDeleted).Include(b => b.Vehicles).FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}

