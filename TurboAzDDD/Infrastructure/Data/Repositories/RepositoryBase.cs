using System.Linq.Expressions;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _appDbContext;
        

        public RepositoryBase(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
             entity.IsDeleted = true;
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _appDbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);

        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await _appDbContext.Set<T>().AnyAsync(expression);
        }

        public async Task UpdateAsync(T entity)
        {
             _appDbContext.Set<T>().Update(entity);
        }
    }
}

