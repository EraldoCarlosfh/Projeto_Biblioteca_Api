using Biblioteca.Application.DependencyInjection;
using Biblioteca.Infrastructure.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Biblioteca.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddApplication();
            services.AddInfrastructure(configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Livros",
                    Version = "v1"
                });
            });

            return services;
        }
    }
}
