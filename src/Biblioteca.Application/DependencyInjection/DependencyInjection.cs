using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBookLoanService, BookLoanService>();

            return services;
        }
    }
}
