using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class BodyTypeRepository : RepositoryBase<BodyType>, IBodyTypeRepository
    {
        public BodyTypeRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

