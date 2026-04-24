using Biblioteca.Application.DTOs;
using System;
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

        Task<PagedResult<BookDto>> ListAsync(
            PagedRequest request,
            CancellationToken cancellationToken);
    }
}
