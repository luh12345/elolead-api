using Elogroup.Lead.Api.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Validators
{
    public class CreateLeadValidator : AbstractValidator<CreateLeadDTO>
    {
        public CreateLeadValidator()
        {
            RuleFor(x => x.CustomerEmail).NotEmpty().NotNull().WithMessage("Preencher o campo CustomerEmail corretamente.");
            RuleFor(x => x.CustomerEmail).EmailAddress().WithMessage("O campo CustomerEmail deve ser um e-mail válido.");

            RuleFor(x => x.CustomerName).NotEmpty().NotNull().WithMessage("Preencher o campo CustomerName corretamente.");
            RuleFor(x => x.CustomerPhone).NotEmpty().NotNull().WithMessage("Preencher o campo CustomerPhone corretamente.");
        }
    }
}
