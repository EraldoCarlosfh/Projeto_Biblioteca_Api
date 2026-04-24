using Biblioteca.Domain.Exceptions;
using System;

namespace Biblioteca.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public int AnoPublicacao { get; private set; }
        public int QuantidadeDisponivel { get; private set; }

        private Book() { }

        public Book(string titulo, string autor, int anoPublicacao, int quantidadeDisponivel)
        {
            Id = Guid.NewGuid();
            SetTitle(titulo);
            SetAuthor(autor);

            if (quantidadeDisponivel < 0)
                throw new DomainException("Quantidade disponível não pode ser negativa.");

            AnoPublicacao = anoPublicacao;
            QuantidadeDisponivel = quantidadeDisponivel;
        }

        public void BorrowBook()
        {
            if (QuantidadeDisponivel <= 0)
                throw new DomainException("Não há exemplares disponíveis para empréstimo.");

            QuantidadeDisponivel--;
        }

        public void ReturnBook()
        {
            QuantidadeDisponivel++;
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Título é obrigatório.");

            Titulo = title.Trim();
        }

        private void SetAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new DomainException("Autor é obrigatório.");

            Autor = author.Trim();
        }
    }
}
