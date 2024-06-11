using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class FuelTypeRepository : RepositoryBase<FuelType>, IFuelTypeRepository
    {
        public FuelTypeRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

