using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Exceptions;
using Biblioteca.Domain.Repositories;
using System;
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

        public async Task<PagedResult<BookDto>> ListAsync(
            PagedRequest request,
            CancellationToken cancellationToken)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            if (pageSize > 100)
                pageSize = 100;

            var totalItems = await _bookRepository.CountAsync(cancellationToken);

            var books = await _bookRepository.ListAsync(
                page,
                pageSize,
                cancellationToken);

            return new PagedResult<BookDto>
            {
                Items = books.Select(MapToDto).ToList(),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
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
