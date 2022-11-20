using MediatR;
using FluentValidation;
using System.Reflection;
using Domain.ConfigurationSettings;
using Domain.Common.PipelineBehaviours;
using Microsoft.Extensions.Configuration;
using Domain.Common.FileHalper.Abstraction;
using Domain.Common.FileHalper.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        services.AddMediatR(assembly);
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<IFileProvider, FileProvider>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.Configure<CsvReaderOptions>(configuration.GetSection(nameof(CsvReaderOptions)));
        services.Configure<ElectricityUrlOptions>(configuration.GetSection(nameof(ElectricityUrlOptions)));

        return services;
    }
}