using Biblioteca.Application.DTOs;
using FluentValidation;

namespace Biblioteca.Application.Validators
{
    internal class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookRequestValidator()
        {
            RuleFor(x => x.Titulo)
           .NotEmpty().WithMessage("Título é obrigatório.")
           .MaximumLength(200);

            RuleFor(x => x.Autor)
                .NotEmpty().WithMessage("Autor é obrigatório.")
                .MaximumLength(150);

            RuleFor(x => x.AnoPublicacao)
                .GreaterThan(0).WithMessage("Ano de publicação inválido.");

            RuleFor(x => x.QuantidadeDisponivel)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantidade disponível não pode ser negativa.");
        }
    }
}
