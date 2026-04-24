using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repositories
{
    public interface IBookLoanRepository
    {
        Task CreateAsync(BookLoan bookLoan, CancellationToken cancellationToken);
        Task<BookLoan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<BookLoan>> ListAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task SaveChangeAsync(CancellationToken cancellationToken);
    }
}
