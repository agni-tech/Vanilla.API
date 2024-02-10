using System.Security.Cryptography;

namespace Vanilla.Shared.Service;

public class BaseService<TId, TEntity, TResponse>
{
    public Task<TResponse> Add(TEntity entityDto)
    {
        throw new NotImplementedException();
    }

    public Task Delete(TId id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TResponse>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<TResponse> Get(TId id)
    {
        throw new NotImplementedException();
    }

    public Task<TResponse> Update(TId id, TEntity entityDto)
    {
        throw new NotImplementedException();
    }
}
