namespace Vanilla.Shared.Service
{
    public interface IBaseService<TId, TEntity, TResponse>
        where TId : struct
        where TEntity : class
        where TResponse : class
    {
        Task<List<TResponse>> GetAsync();
        Task<TResponse> GetAsync(TId id);
        Task<TResponse> AddAsync(TEntity entityDto);
        Task<TResponse> UpdateAsync(TId id, TEntity entityDto);
        Task<bool> DeleteAsync(TId id);
    }
}
