using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultConext = await next();

            if (!resultConext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultConext.HttpContext.User.GetUserId();

            var uow = resultConext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

            var user = await uow.UserRepository.GetUserByIdAsync(userId);

            user.LastActive = DateTime.UtcNow;

            await uow.Complete();
        }
    }
}