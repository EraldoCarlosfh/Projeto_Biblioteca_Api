using System;

namespace Biblioteca.Application.DTOs
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public int QuantidadeDisponivel { get; set; }
    }
}
