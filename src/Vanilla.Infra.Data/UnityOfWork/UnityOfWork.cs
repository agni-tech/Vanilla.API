using Microsoft.AspNetCore.Mvc.Filters;
using Vanilla.Infra.Data.Contexts;

namespace Vanilla.Infra.Data.UnityOfWork
{
    public class UnitOfWork : IAsyncActionFilter, IUnityOfWork
    {
        private readonly VanillaContext _context;

        public UnitOfWork(VanillaContext context)
        {
            _context = context;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var result = await next();
                if ((result.Exception == null || result.ExceptionHandled) && _context.ChangeTracker.HasChanges())
                {
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
