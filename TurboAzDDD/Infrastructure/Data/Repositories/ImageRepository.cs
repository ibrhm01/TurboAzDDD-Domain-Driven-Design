using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        public readonly ApplicationDbContext _appDbContext;

        public ImageRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //public override async Task<List<Image>> GetAllAsync()
        //{
        //    return await _appDbContext.Set<Image>().Where(b => !b.IsDeleted).ToListAsync();
        //}
        //public override async Task<Image?> GetByIdAsync(int id)
        //{
        //    return await _appDbContext.Set<Image>().Where(b => !b.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);
        //}
    }
}

