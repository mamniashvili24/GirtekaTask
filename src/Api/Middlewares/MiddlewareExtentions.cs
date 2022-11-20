namespace Api.Middlewares;

public static class MiddlewareExtentions
{
    public static void ErrorExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}