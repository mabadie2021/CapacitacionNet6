using Andreani.ARQ.Core.Pipeline.Extension;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExampleApi.Application;

public static class DependencyInjection
{
      public static IServiceCollection AddApplication(this IServiceCollection services)
      {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAndreaniPipeline(Verbose: true);

            return services;
      }
}