using Microsoft.AspNetCore.Builder;
using UserManagement.Shared.Utils;

namespace UserManagement.Shared;

public static class DependencyInjection
{
    public static IApplicationBuilder UseCustomExceptionHandlers(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandleMiddleware>();

        app.UseMiddleware<ValidationMiddleware>();

        return app;
    }
}