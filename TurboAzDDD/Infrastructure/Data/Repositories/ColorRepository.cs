using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

