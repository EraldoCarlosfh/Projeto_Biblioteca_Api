using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace Biblioteca.UnitTests.Domain
{
    public class BookLoanTests
    {
        [Fact]
        public void Should_Create_BookLoan_With_Active_Status()
        {
            var bookId = Guid.NewGuid();

            var bookLoan = new BookLoan(bookId);

            bookLoan.Id.Should().NotBeEmpty();
            bookLoan.LivroId.Should().Be(bookId);
            bookLoan.Status.Should().Be(StatusBookEnum.Ativo);
            bookLoan.DataEmprestimo.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            bookLoan.DataDevolucao.Should().BeNull();
        }

        [Fact]
        public void Should_Throw_When_BookId_Is_Empty()
        {
            Action act = () => new BookLoan(Guid.Empty);

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Id do Livro é obrigatório.");
        }

        [Fact]
        public void Should_Deliver_BookLoan()
        {
            var bookLoan = new BookLoan(Guid.NewGuid());

            bookLoan.Return();

            bookLoan.Status.Should().Be(StatusBookEnum.Devolvido);
            bookLoan.DataDevolucao.Should().NotBeNull();
            bookLoan.DataDevolucao.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void Should_Throw_When_Delivering_Already_Delivered_BookLoan()
        {
            var bookLoan = new BookLoan(Guid.NewGuid());
            bookLoan.Return();

            Action act = () => bookLoan.Return();

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Não é possível devolver um empréstimo já devolvido.");
        }
    }
}
