using Domain.Common.Error.Handling;

namespace Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ILogger<ErrorHandlerMiddleware> logger)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            var errorMessage = ex.ToProblemDetails();
            await httpContext.Response.WriteAsJsonAsync(errorMessage);
        }
    }
}