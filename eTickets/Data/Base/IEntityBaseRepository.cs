
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includePropperties);

        public Task<T> GetByIdAsync(int Id);

        public Task AddAsync(T entity);

        public Task UpdateAsync(int Id, T entity);

        public Task DeleteAsync(int Id);
    }
}