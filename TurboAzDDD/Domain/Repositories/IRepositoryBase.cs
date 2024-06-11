using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRepositoryBase <T> where T: class
	{
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression); //
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}