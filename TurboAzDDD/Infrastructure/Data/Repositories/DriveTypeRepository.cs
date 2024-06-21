using System;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class DriveTypeRepository : RepositoryBase<Domain.Entities.DriveType>, IDriveTypeRepository
    {
        public readonly ApplicationDbContext _appDbContext;
        public DriveTypeRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<List<Domain.Entities.DriveType>> GetAllAsync()
        {
            return await _appDbContext.Set<Domain.Entities.DriveType>().Where(b => !b.IsDeleted).Include(b => b.Vehicles).ToListAsync();
        }
        public override async Task<Domain.Entities.DriveType?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<Domain.Entities.DriveType>().Where(b => !b.IsDeleted).Include(b => b.Vehicles).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

