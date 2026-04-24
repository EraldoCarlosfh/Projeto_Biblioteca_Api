using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace Biblioteca.UnitTests.Domain
{
    public class BookTests
    {
        [Fact]
        public void Should_Create_Book_When_Data_Is_Valid()
        {
            var book = new Book("Clean Code", "Robert C. Martin", 2008, 3);

            book.Id.Should().NotBeEmpty();
            book.Titulo.Should().Be("Clean Code");
            book.Autor.Should().Be("Robert C. Martin");
            book.AnoPublicacao.Should().Be(2008);
            book.QuantidadeDisponivel.Should().Be(3);
        }

        [Fact]
        public void Should_Throw_When_Title_Is_Empty()
        {
            Action act = () => new Book("", "Autor", 2000, 1);

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Título é obrigatório.");
        }

        [Fact]
        public void Should_Throw_When_Author_Is_Empty()
        {
            Action act = () => new Book("Livro", "", 2000, 1);

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Autor é obrigatório.");
        }

        [Fact]
        public void Should_Throw_When_Quantity_Is_Negative()
        {
            Action act = () => new Book("Livro", "Autor", 2000, -1);

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Quantidade disponível não pode ser negativa.");
        }

        [Fact]
        public void Should_Decrease_Quantity_When_Book_Is_Picked()
        {
            var book = new Book("Livro", "Autor", 2000, 2);

            book.BorrowBook();

            book.QuantidadeDisponivel.Should().Be(1);
        }

        [Fact]
        public void Should_Throw_When_Picking_Book_Without_Available_Quantity()
        {
            var book = new Book("Livro", "Autor", 2000, 0);

            Action act = () => book.BorrowBook();

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Não há exemplares disponíveis para empréstimo.");
        }

        [Fact]
        public void Should_Increase_Quantity_When_Book_Is_Delivered()
        {
            var book = new Book("Livro", "Autor", 2000, 1);

            book.ReturnBook();

            book.QuantidadeDisponivel.Should().Be(2);
        }
    }
}
