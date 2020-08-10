using Elogroup.Lead.Api.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerDTO>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.LeadId).NotNull().GreaterThan(0).WithMessage("LeadId inválido.");
        }
    }
}
