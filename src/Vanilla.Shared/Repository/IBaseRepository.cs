using System.Linq.Expressions;

namespace Vanilla.Shared.Repository
{
    public interface IBaseRepository<TId, TEntity>
    {
        Task<TEntity> AddAsync(TEntity obj);
        void Update(TEntity obj);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> ToListAsync();
        Task<IEnumerable<TEntity>> ToListAsyncAsNoTracking();
        Task<TEntity> GetByIdAsync(TId id);
        void Remove(TEntity obj);
        void RemoveRange(IEnumerable<TEntity> list);
    }
}
