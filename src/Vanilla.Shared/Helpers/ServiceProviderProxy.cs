using Vanilla.Shared.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Vanilla.Shared.Helpers;

public class ServiceProviderProxy : IContainer
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ServiceProviderProxy(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public T GetService<T>(Type type)
        => (T)_httpContextAccessor.HttpContext.RequestServices.GetService(type);

}
