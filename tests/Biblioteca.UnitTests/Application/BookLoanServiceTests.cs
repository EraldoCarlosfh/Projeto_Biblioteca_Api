using Biblioteca.Application.DTOs;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Biblioteca.UnitTests.Application
{
    public class BookLoanServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IBookLoanRepository> _bookLoanRepositoryMock;
        private readonly BookLoanService _service;

        public BookLoanServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookLoanRepositoryMock = new Mock<IBookLoanRepository>();

            _service = new BookLoanService(
                _bookRepositoryMock.Object,
                _bookLoanRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_Create_BookLoan_When_Book_Has_Available_Quantity()
        {
            var book = new Book("Clean Code", "Robert C. Martin", 2008, 2);

            var request = new BookLoanRequest
            {
                LivroId = book.Id
            };

            _bookRepositoryMock
                .Setup(x => x.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            var result = await _service.PickUpAsync(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.LivroId.Should().Be(book.Id);
            book.QuantidadeDisponivel.Should().Be(1);

            _bookLoanRepositoryMock.Verify(x =>
                x.CreateAsync(It.IsAny<BookLoan>(), It.IsAny<CancellationToken>()),
                Times.Once);

            _bookLoanRepositoryMock.Verify(x =>
                x.SaveChangeAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Book_Not_Found_On_PickUp()
        {
            var request = new BookLoanRequest
            {
                LivroId = Guid.NewGuid()
            };

            _bookRepositoryMock
                .Setup(x => x.GetByIdAsync((Guid)request.LivroId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Book)null);

            Func<Task> act = async () => await _service.PickUpAsync(request, CancellationToken.None);

            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Livro não encontrado.");
        }

        [Fact]
        public async Task Should_Throw_When_Book_Has_No_Available_Quantity()
        {
            var book = new Book("Clean Code", "Robert C. Martin", 2008, 0);

            var request = new BookLoanRequest
            {
                LivroId = book.Id
            };

            _bookRepositoryMock
                .Setup(x => x.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            Func<Task> act = async () => await _service.PickUpAsync(request, CancellationToken.None);

            await act.Should()
                .ThrowAsync<DomainException>()
                .WithMessage("Não há exemplares disponíveis para empréstimo.");
        }

        [Fact]
        public async Task Should_Deliver_BookLoan()
        {
            var book = new Book("Clean Code", "Robert C. Martin", 2008, 1);
            var bookLoan = new BookLoan(book.Id);

            _bookLoanRepositoryMock
                .Setup(x => x.GetByIdAsync(bookLoan.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookLoan);

            _bookRepositoryMock
                .Setup(x => x.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            var result = await _service.DeliverAsync(bookLoan.Id, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(bookLoan.Id);
            result.Status.Should().Be("Devolvido");
            book.QuantidadeDisponivel.Should().Be(2);

            _bookLoanRepositoryMock.Verify(x =>
                x.SaveChangeAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_BookLoan_Not_Found_On_Deliver()
        {
            var loanId = Guid.NewGuid();

            _bookLoanRepositoryMock
                .Setup(x => x.GetByIdAsync(loanId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((BookLoan)null);

            Func<Task> act = async () => await _service.DeliverAsync(loanId, CancellationToken.None);

            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Empréstimo não encontrado.");
        }
    }
}
