using Biblioteca.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface IBookLoanService
    {
        Task<BookLoanDto> PickUpAsync(
        BookLoanRequest request,
        CancellationToken cancellationToken);

        Task<BookLoanDto> DeliverAsync(
            Guid loanId,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<BookLoanDto>> ListAsync(
            CancellationToken cancellationToken);
    }
}
