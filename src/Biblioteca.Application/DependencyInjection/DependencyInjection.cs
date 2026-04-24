using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBookLoanService, BookLoanService>();
            services.AddScoped<IBookService, BookService>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
