using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface IBookRepository
    {
        Task AdicionarAsync(Book book, CancellationToken cancellationToken);
        Task<Book?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Book>> ListarAsync(CancellationToken cancellationToken);
        Task SalvarAlteracoesAsync(CancellationToken cancellationToken);
    }
}
