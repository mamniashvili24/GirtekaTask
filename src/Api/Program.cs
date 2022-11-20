using Api;
using Application;
using Infrastructure;
using Api.Middlewares;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Application.Commands.AggregatedData;
using Application.Queries.GetAllElectricities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost<AggregatedDataCommand>("AggregatedData")
    .WithName("AggregatedData")
    .WithOpenApi();

app.AsyncEnumerableMapGet<GetAllElectricitiesQuery>("GetAllElectricities")
    .WithName("GetAllElectricities")
    .WithOpenApi();

app.ErrorExceptionMiddleware();

await DbMigrateAsync();

app.Run();

async Task DbMigrateAsync()
{
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    try
    {
        var context = serviceProvider.GetRequiredService<ElectricityDbContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}