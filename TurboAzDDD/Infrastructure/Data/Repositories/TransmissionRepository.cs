using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class TransmissionRepository : RepositoryBase<Transmission>, ITransmissionRepository
    {
        public TransmissionRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

