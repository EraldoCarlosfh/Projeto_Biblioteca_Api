using Biblioteca.Application.DTOs;
using FluentValidation;
using System;

namespace Biblioteca.Application.Validators
{
    internal class BookLoanRequestValidator : AbstractValidator<BookLoanRequest>
    {
        public BookLoanRequestValidator()
        {
            RuleFor(x => x.LivroId)
                .NotEqual(Guid.Empty)
                .WithMessage("LivroId é obrigatório.");
        }
    }
}
