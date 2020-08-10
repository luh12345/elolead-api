using Elogroup.Lead.Api.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("O campo Username não deve ser vazio");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("O campo Password não deve ser vazia");
        }
    }
}
