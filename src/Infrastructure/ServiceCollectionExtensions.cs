using Infrastructure.Database;
using Microsoft.Extensions.Options;
using Domain.ConfigurationSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Common.Database.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        void ConfigureDbOptions(DbContextOptionsBuilder options) =>
                options.UseSqlServer(configuration.GetConnectionString("ElectricityConnectionString"),
                    b => b.MigrationsAssembly(typeof(ElectricityDbContext).Assembly.FullName));

        services.AddHttpClient("Electricity", (provider, client) =>
        {
            var electricityUrlOptions = provider.GetRequiredService<IOptions<ElectricityUrlOptions>>().Value;

            client.BaseAddress = new Uri(electricityUrlOptions.BaseAddress + electricityUrlOptions.EndPoint);
        });

        services.AddDbContextFactory<ElectricityDbContext>(ConfigureDbOptions, ServiceLifetime.Scoped);

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        return services;
    }
}