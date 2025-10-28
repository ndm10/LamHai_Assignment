using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<List<T>> GenericGetPaginationAsync(int pageIndex, int pageSize, Expression<Func<T, bool>>? predicate = null, string[]? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
        public Task AddAsync(T entity);
        public void Update(T entity);
        public Task<T?> GetByIdAsync(Guid id, string[]? includes = null, Expression<Func<T, bool>>? predicate = null);
        public Task<T?> GetEntityByPredicateAsync(Expression<Func<T, bool>> predicate, string[]? includes = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string[]? include = null);
        Task<int> CountEntitiesAsync(Expression<Func<T, bool>>? predicate = null);
    }
}
