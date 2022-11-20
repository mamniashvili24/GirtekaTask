using MediatR;

namespace Api;

public static class BaseMinimalApi
{
    public static RouteHandlerBuilder AsyncEnumerableMapGet<T>(this WebApplication app, string route)
    {
        return app.MapGet(route,
            ([AsParameters] T request, CancellationToken cancellationToken) => 
            app.GetSender().CreateStream(request, cancellationToken));
    }

    public static RouteHandlerBuilder CustomAsyncEnumerableMapPost<T>(this WebApplication app, string route)
    {
        return app.MapPost(route,
            (T request, CancellationToken cancellationToken) =>
            app.GetSender().CreateStream(request, cancellationToken));
    }

    public static RouteHandlerBuilder MapGet<T>(this WebApplication app, string route)
    {
        return app.MapGet(route,
            ([AsParameters] T request, CancellationToken cancellationToken) =>
            app.GetSender().Send(request, cancellationToken));
    }

    public static RouteHandlerBuilder MapPost<T>(this WebApplication app, string route)
    {
        return app.MapPost(route,
            async (T request, CancellationToken cancellationToken) =>
            await app.GetSender().Send(request, cancellationToken));
    }

    private static IMediator GetSender(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var sender = serviceProvider.GetRequiredService<IMediator>();
        
        return sender;
    }
}