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
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly BookService _service;

        public BookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _service = new BookService(_bookRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_Create_Book()
        {
            var request = new CreateBookRequest
            {
                Titulo = "Clean Code",
                Autor = "Robert C. Martin",
                AnoPublicacao = 2008,
                QuantidadeDisponivel = 3
            };

            var result = await _service.CreateAsync(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Titulo.Should().Be(request.Titulo);
            result.Autor.Should().Be(request.Autor);
            result.QuantidadeDisponivel.Should().Be(3);

            _bookRepositoryMock.Verify(x =>
                x.CreateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()),
                Times.Once);

            _bookRepositoryMock.Verify(x =>
                x.SaveChangeAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Should_Get_Book_By_Id()
        {
            var book = new Book("Clean Code", "Robert C. Martin", 2008, 3);

            _bookRepositoryMock
                .Setup(x => x.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            var result = await _service.GetByIdAsync(book.Id, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);
            result.Titulo.Should().Be(book.Titulo);
        }

        [Fact]
        public async Task Should_Throw_When_Book_Not_Found()
        {
            var id = Guid.NewGuid();

            _bookRepositoryMock
                .Setup(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Book)null);

            Func<Task> act = async () => await _service.GetByIdAsync(id, CancellationToken.None);

            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Livro não encontrado.");
        }
    }
}
