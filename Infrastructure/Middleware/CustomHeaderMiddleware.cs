using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middleware;

public class CustomHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public CustomHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response!.Headers!.Append("X-Custom-Header", "This is my custom header.");
        await _next(context)!;
    }
}