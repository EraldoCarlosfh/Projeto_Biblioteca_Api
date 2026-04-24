using System;

namespace Biblioteca.Application.DTOs
{
    public class BookLoanDto
    {
        public Guid Id { get; set; }
        public Guid LivroId { get; set; }
        public BookDto? Livro { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
