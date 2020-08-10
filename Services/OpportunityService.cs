using Elogroup.Api.Repository;
using Elogroup.Lead.Api.DTO;
using Elogroup.Lead.Api.Repository.Entities;
using Elogroup.Lead.Api.Services.Contract;
using Elogroup.Lead.Api.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Elogroup.Lead.Api.Services
{
    public class OpportunityService : IOpportunityService
    {
        private readonly Context context;

        public OpportunityService(Context context)
        {
            this.context = context;
        }
        public void CreateOpportunity(CreateOpportunityDTO opportunityDTO)
        {
            var validator = new CreateOpportunityValidator();
            validator.ValidateAndThrow(opportunityDTO);

            var lead = context.Leads.SingleOrDefault(x => x.Id == opportunityDTO.LeadId);

            if (lead == null)
                throw new Exception("Lead não encontrado.");

            lead.StatusId = (int)Common.Enums.StatusLead.QUALIFICADO;

            context.Opportunities.Add(new Opportunity
            {
                LeadId = opportunityDTO.LeadId,
                Description = opportunityDTO.Description
            });

            context.SaveChanges();
        }

        public void EditOpportunity(EditOpportunityDTO opportunityDTO)
        {
            var validator = new EditOpportunityValidator();
            validator.ValidateAndThrow(opportunityDTO);

            var opportunity = context.Opportunities.SingleOrDefault(x => x.Id == opportunityDTO.Id);

            if (opportunity == null)
                throw new Exception("Opportunity não encontrada.");

            opportunity.Description = opportunityDTO.Description;
            context.SaveChanges();
        }

        public void DeleteOpportunity(int opportunityId)
        {
            var opportunity = context.Opportunities.SingleOrDefault(x => x.Id == opportunityId);

            if (opportunity == null)
                throw new Exception("Opportunity não encontrada.");

            var lead = context.Leads.SingleOrDefault(x => x.Id == opportunity.LeadId);
            lead.StatusId = (int)Common.Enums.StatusLead.EM_QUALIFICACAO;

            context.Opportunities.Remove(opportunity);
            context.SaveChanges();
        }
    }
}
