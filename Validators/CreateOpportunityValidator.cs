using Elogroup.Lead.Api.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Validators
{
    public class CreateOpportunityValidator : AbstractValidator<CreateOpportunityDTO>
    {
        public CreateOpportunityValidator()
        {
            RuleFor(x => x.LeadId).NotNull().GreaterThan(0).WithMessage("LeadId inválido.");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Preencher campo Description corretamente.");
        }
    }
}
