using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class TransmissionRepository : RepositoryBase<Transmission>, ITransmissionRepository
    {
        public readonly ApplicationDbContext _appDbContext;
        public TransmissionRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public override async Task<IEnumerable<Transmission>> GetAllAsync()
        {
            return await _appDbContext.Set<Transmission>().Include(b => b.Vehicles).ToListAsync();
        }
        public override async Task<Transmission?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<Transmission>().Include(b => b.Vehicles).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

