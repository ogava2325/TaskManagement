using System.Security.Claims;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Api.Middleware;

public class TaskOwnershipMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, ITaskRepository taskRepository)
    {
        if (context.User.Identity is { IsAuthenticated: true })
        {
            var userId = Guid.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var taskId = context.Request.RouteValues["id"]?.ToString();

            if (!string.IsNullOrEmpty(taskId))
            {
                var task = await taskRepository.GetByIdAsync(Guid.Parse(taskId));
                if (task.UserId != userId)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }
        }

        await _next(context);
    }
}