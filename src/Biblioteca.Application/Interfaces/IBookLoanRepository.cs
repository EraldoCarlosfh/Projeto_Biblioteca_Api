using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface IBookLoanRepository
    {
        Task AdicionarAsync(BookLoan bookLoan, CancellationToken cancellationToken);
        Task<BookLoan?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<BookLoan>> ListarAsync(CancellationToken cancellationToken);
        Task SalvarAlteracoesAsync(CancellationToken cancellationToken);
    }
}
