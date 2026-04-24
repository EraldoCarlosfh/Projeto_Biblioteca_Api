using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Repositories
{
    public interface IBookRepository
    {
        Task CreateAsync(Book book, CancellationToken cancellationToken);
        Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Book>> ListAsync(CancellationToken cancellationToken);
        Task SaveChangeAsync(CancellationToken cancellationToken);
    }
}
