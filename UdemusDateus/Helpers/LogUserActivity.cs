using Microsoft.AspNetCore.Mvc.Filters;
using UdemusDateus.Extensions;
using UdemusDateus.Interfaces;

namespace UdemusDateus.Helpers;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.HttpContext.User.Identity is { IsAuthenticated: false }) return;

        var userId = resultContext.HttpContext.User.GetUserId();
        var uow = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
        if (uow != null)
        {
            var user = await uow.UserRepository.GetUserByIdAsync(userId);
            user.LastActive = DateTime.UtcNow;
        }

        if (uow != null) await uow.Complete();
    }
}