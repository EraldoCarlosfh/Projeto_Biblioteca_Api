using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Repositories;
using Biblioteca.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BibliotecaContext _context;

        public BookRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
        Book book,
        CancellationToken cancellationToken)
        {
            await _context.Livros.AddAsync(book, cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Livros
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Livros.CountAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Book>> ListAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            return await _context.Livros
                .AsNoTracking()
                .OrderBy(x => x.Titulo)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangeAsync(
            CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
