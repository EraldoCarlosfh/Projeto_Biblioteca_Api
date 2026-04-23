using Biblioteca.Application.DTOs;
using FluentValidation;

namespace Biblioteca.Application.Validators
{
    internal class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookRequestValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Autor)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.AnoPublicacao)
                .GreaterThan(0);

            RuleFor(x => x.QuantidadeDisponivel)
                .GreaterThanOrEqualTo(0);
        }
    }
}
