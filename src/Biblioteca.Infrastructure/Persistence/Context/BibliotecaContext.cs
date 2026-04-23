using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Persistence.Context
{
    public class BibliotecaContext : DbContext
    {
        public DbSet<Book> Livros { get; set; }
        public DbSet<BookLoan> Emprestimos { get; set; }

        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BibliotecaContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
