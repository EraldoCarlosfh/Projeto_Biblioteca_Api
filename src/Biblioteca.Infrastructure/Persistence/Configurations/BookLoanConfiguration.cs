using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Persistence.Configurations
{
    public class BookLoanConfiguration : IEntityTypeConfiguration<BookLoan>
    {
        public void Configure(EntityTypeBuilder<BookLoan> builder)
        {
            builder.ToTable("emprestimos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.LivroId)
                .HasColumnName("livro_id")
                .IsRequired();

            builder.Property(x => x.DataEmprestimo)
                .HasColumnName("data_emprestimo")
                .IsRequired();

            builder.Property(x => x.DataDevolucao)
                .HasColumnName("data_devolucao");

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne<Book>()
                .WithMany()
                .HasForeignKey(x => x.LivroId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
