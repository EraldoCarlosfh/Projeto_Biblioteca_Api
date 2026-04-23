namespace Biblioteca.Application.DTOs
{
    public class CreateBookRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public int QuantidadeDisponivel { get; set; }
    }
}
