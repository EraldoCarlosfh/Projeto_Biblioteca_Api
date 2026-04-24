using Biblioteca.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> CreateAsync(
         CreateBookRequest request,
         CancellationToken cancellationToken);

        Task<BookDto> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<BookDto>> ListAsync(
            CancellationToken cancellationToken);
    }
}
