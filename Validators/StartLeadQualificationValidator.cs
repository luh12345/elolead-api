using Elogroup.Lead.Api.DTO;
using FluentValidation;

namespace Elogroup.Lead.Api.Validators
{
    public class StartLeadQualificationValidator : AbstractValidator<StartLeadQualificationDTO>
    {
        public StartLeadQualificationValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage("LeadId inválido.");
        }
    }
}
