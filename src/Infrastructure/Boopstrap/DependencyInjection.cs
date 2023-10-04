using Andreani.Data.CQRS.Extension;
using Andreani.SolucionesTYD.Tools.Configuration;
using ExampleApi.Application.Common.Interfaces;
using ExampleApi.Infrastructure.Persistence;
using ExampleApi.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleApi.Infrastructure;

public static class DependencyInjection
{
      public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
      {
            services.AddCQRS<ApplicationDbContext>(configuration);

            services.AddScoped<ApplicationDbContext>();

            services.AddDBConnection(configuration);

            services.AddScoped<IExampleService, ExampleService>();

            return services;
      }
}