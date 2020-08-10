using Elogroup.Lead.Api.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Validators
{
    public class EditOpportunityValidator : AbstractValidator<EditOpportunityDTO>
    {
        public EditOpportunityValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage("OpportunityId inválido.");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Preencher campo Description corretamente.");
        }
    }
}
