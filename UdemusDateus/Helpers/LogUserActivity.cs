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
        var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
        if (repo != null)
        {
            var user = await repo.GetUserByIdAsync(userId);
            user.LastActive = DateTime.Now;
        }

        if (repo != null) await repo.SaveAllAsync();
    }
}