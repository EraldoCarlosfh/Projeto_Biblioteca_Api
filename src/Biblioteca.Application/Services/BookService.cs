using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(
            IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDto> CreateAsync(
        CreateBookRequest request,
        CancellationToken cancellationToken)
        {
            var book = new Book(
                request.Titulo,
                request.Autor,
                request.AnoPublicacao,
                request.QuantidadeDisponivel);

            await _bookRepository.CreateAsync(book, cancellationToken);
            await _bookRepository.SaveChangeAsync(cancellationToken);

            return MapToDto(book);
        }

        public async Task<BookDto> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);

            if (book is null)
                throw new NotFoundException("Livro não encontrado.");

            return MapToDto(book);
        }

        public async Task<IReadOnlyList<BookDto>> ListAsync(
            CancellationToken cancellationToken)
        {
            var book = await _bookRepository.ListAsync(cancellationToken);

            return book
                .Select(MapToDto)
                .ToList();
        }

        private static BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Titulo = book.Titulo,
                Autor = book.Autor,
                AnoPublicacao = book.AnoPublicacao,
                QuantidadeDisponivel = book.QuantidadeDisponivel
            };
        }
    }
}
