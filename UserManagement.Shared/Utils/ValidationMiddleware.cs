using FluentValidation;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Text.Json;

namespace UserManagement.Shared.Utils;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;
    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == "POST" || context.Request.Method == "PUT")
        {
            try
            {
                await _next(context);
                return;
            }
            catch (ValidationException ex)
            {
                Log.Error(ex, ex.Message);
                await HandleValidationExceptionAsync(context, ex);
                return;
            }
        }

        await _next(context);
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new { message = exception.Message, errorType = exception.GetType().Name };
        var result = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(result);
    }
}