using Biblioteca.Application.DTOs;
using FluentValidation;
using System;

namespace Biblioteca.Application.Validators
{
    public class BookLoanRequestValidator : AbstractValidator<BookLoanRequest>
    {
        public BookLoanRequestValidator()
        {
            RuleFor(x => x.LivroId)
                .NotNull().WithMessage("LivroId é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("LivroId é obrigatório.");
        }
    }
}
