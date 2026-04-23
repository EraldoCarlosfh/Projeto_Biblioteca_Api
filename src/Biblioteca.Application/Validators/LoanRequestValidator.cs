using Biblioteca.Application.DTOs;
using FluentValidation;
using System;

namespace Biblioteca.Application.Validators
{
    internal class LoanRequestValidator : AbstractValidator<LoanRequest>
    {
        public LoanRequestValidator()
        {
            RuleFor(x => x.LivroId)
                .NotEqual(Guid.Empty);
        }
    }
}
