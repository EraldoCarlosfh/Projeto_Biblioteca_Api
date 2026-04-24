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
    public class BookLoanRepository : IBookLoanRepository
    {
        private readonly BibliotecaContext _context;

        public BookLoanRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
        BookLoan bookLoan,
        CancellationToken cancellationToken)
        {
            await _context.Emprestimos.AddAsync(bookLoan, cancellationToken);
        }

        public async Task<BookLoan?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Emprestimos
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<BookLoan>> ListAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Emprestimos
                .AsNoTracking()
                .OrderByDescending(x => x.DataEmprestimo)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangeAsync(
            CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
