using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

