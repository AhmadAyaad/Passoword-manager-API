using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace PasswordManager.Entities.IRepository
{
    public interface IRepository<T> where T : class
    {
        ValueTask<EntityEntry<T>> AddAsync(T t);
        Task AddRangeAsync(IEnumerable<T> entities);
        ValueTask<T> GetByIdAsync(int id);
        void Remove(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
    }
}
