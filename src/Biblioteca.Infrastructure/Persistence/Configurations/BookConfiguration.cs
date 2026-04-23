using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("livros");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Titulo)
                .HasColumnName("titulo")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Autor)
                .HasColumnName("autor")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.AnoPublicacao)
                .HasColumnName("ano_publicacao")
                .IsRequired();

            builder.Property(x => x.QuantidadeDisponivel)
                .HasColumnName("quantidade_disponivel")
                .IsRequired();
        }
    }
}
