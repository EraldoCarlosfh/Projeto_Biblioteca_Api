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
    public class BookLoanService : IBookLoanService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookLoanRepository _bookLoanRepository;

        public BookLoanService(
            IBookRepository bookRepository,
            IBookLoanRepository bookLoanRepository)
        {
            _bookRepository = bookRepository;
            _bookLoanRepository = bookLoanRepository;
        }

        public async Task<BookLoanDto> PickUpAsync(
            BookLoanRequest request,
            CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(
                request.LivroId,
                cancellationToken);

            if (book is null)
                throw new NotFoundException("Livro não encontrado.");

            book.PickBook();

            var bookLoan = new BookLoan(book.Id);

            await _bookLoanRepository.CreateAsync(
                bookLoan,
                cancellationToken);

            await _bookLoanRepository.SaveChangeAsync(cancellationToken);

            return MapToDto(bookLoan);
        }

        public async Task<BookLoanDto> GetByIdAsync(
            Guid bookLoanId,
            CancellationToken cancellationToken)
        {
            var bookLoan = await _bookLoanRepository.GetByIdAsync(
                bookLoanId,
                cancellationToken);

            if (bookLoan is null)
                throw new NotFoundException("Empréstimo não encontrado.");

            return MapToDto(bookLoan);
        }

        public async Task<BookLoanDto> DeliverAsync(
            Guid bookLoanId,
            CancellationToken cancellationToken)
        {
            var bookLoan = await _bookLoanRepository.GetByIdAsync(
                bookLoanId,
                cancellationToken);

            if (bookLoan is null)
                throw new NotFoundException("Empréstimo não encontrado.");

            var book = await _bookRepository.GetByIdAsync(
                bookLoan.LivroId,
                cancellationToken);

            if (book is null)
                throw new NotFoundException("Livro vinculado ao empréstimo não encontrado.");

            bookLoan.Deliver();
            book.DeliverBook();

            await _bookLoanRepository.SaveChangeAsync(cancellationToken);

            return MapToDto(bookLoan);
        }

        public async Task<PagedResult<BookLoanDto>> ListAsync(
            PagedRequest request,
            CancellationToken cancellationToken)
        {
            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            if (pageSize > 100)
                pageSize = 100;

            var totalItems = await _bookLoanRepository.CountAsync(cancellationToken);

            var bookLoans = await _bookLoanRepository.ListAsync(
                page,
                pageSize,
                cancellationToken);

            return new PagedResult<BookLoanDto>
            {
                Items = bookLoans.Select(MapToDto).ToList(),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }        

        private static BookLoanDto MapToDto(BookLoan bookLoan)
        {
            return new BookLoanDto
            {
                Id = bookLoan.Id,
                LivroId = bookLoan.LivroId,
                DataEmprestimo = bookLoan.DataEmprestimo,
                DataDevolucao = bookLoan.DataDevolucao,
                Status = bookLoan.Status.ToString()
            };
        }
    }
}
