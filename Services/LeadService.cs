using Elogroup.Api.Repository;
using Elogroup.Lead.Api.DTO;
using Elogroup.Lead.Api.Services.Contract;
using Elogroup.Lead.Api.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elogroup.Lead.Api.Services
{
    public class LeadService : ILeadService
    {
        private readonly Context context;

        public LeadService(Context context)
        {
            this.context = context;
        }

        public void CreateLead(CreateLeadDTO leadDTO)
        {
            var validator = new CreateLeadValidator();
            validator.ValidateAndThrow(leadDTO);

            context.Leads.Add(new Repository.Entities.Lead
            {
                CustomerEmail = leadDTO.CustomerEmail,
                CustomerName = leadDTO.CustomerName,
                CustomerPhone = leadDTO.CustomerPhone,
                Date = DateTime.Now,
                StatusId = (int)Common.Enums.StatusLead.EM_QUALIFICACAO
            });

            context.SaveChanges();
        }

        public void DeleteLead(int id)
        {
            var lead = context.Leads.SingleOrDefault(x => x.Id == id);
            if (lead == null)
                throw new Exception("Lead não encontrado.");

            context.Leads.Remove(lead);
            context.SaveChanges();
        }

        public void DiscardLead(DiscardLeadDTO leadDTO)
        {
            var validator = new DiscardLeadValidator();
            validator.ValidateAndThrow(leadDTO);

            var lead = context.Leads.SingleOrDefault(x => x.Id == leadDTO.Id);
            if (lead == null)
                throw new Exception("Lead não encontrado.");

            lead.StatusId = (int)Common.Enums.StatusLead.DESCARTADO;

            var opportunities = context.Opportunities.Where(x => x.LeadId == lead.Id);
            context.Opportunities.RemoveRange(opportunities);

            context.SaveChanges();
        }

        public void EditLead(EditLeadDTO leadDTO)
        {
            var validator = new EditLeadValidator();
            validator.ValidateAndThrow(leadDTO);

            var lead = context.Leads.SingleOrDefault(x => x.Id == leadDTO.Id);
            if (lead == null)
                throw new Exception("Lead não encontrado.");

            lead.CustomerEmail = leadDTO.CustomerEmail;
            lead.CustomerPhone = leadDTO.CustomerPhone;
            lead.CustomerName = leadDTO.CustomerName;

            if (lead.StatusId == (int)Common.Enums.StatusLead.QUALIFICADO && leadDTO.Opportunity != null)
            {
                var opportunity = context.Opportunities.SingleOrDefault(x => x.Id == leadDTO.Opportunity.Id && x.LeadId == lead.Id);

                if (opportunity == null)
                    throw new Exception("Não foi possível atualizar pois essa Opportunity não pertence a este Lead.");

                opportunity.Description = leadDTO.Opportunity.Description;
            }

            context.SaveChanges();
        }

        public IEnumerable<LeadDTO> GetLeads(LeadFilterDTO leadDTO)
        {
            var leads = context.Leads.Include(x => x.Status)
                .Where(x => string.IsNullOrEmpty(leadDTO.CustomerEmail) || x.CustomerEmail.ToLower().Contains(leadDTO.CustomerEmail.ToLower()))
                .Where(x => string.IsNullOrEmpty(leadDTO.CustomerName) || x.CustomerName.ToLower().Contains(leadDTO.CustomerName.ToLower()))
                .Where(x => !leadDTO.StatusId.HasValue || x.StatusId == leadDTO.StatusId.Value)
                .ToList();

            foreach (var lead in leads)
            {
                CustomerDTO customerDTO = null;
                OpportunityDTO opportunityDTO = null;

                if (context.Customers.Any(x => x.LeadId == lead.Id))
                {
                    var customer = context.Customers.Single(x => x.LeadId == lead.Id);
                    customerDTO = new CustomerDTO
                    {
                        Id = customer.Id
                    };
                }

                if (context.Opportunities.Any(x => x.LeadId == lead.Id))
                {
                    var opportunity = context.Opportunities.Single(x => x.LeadId == lead.Id);
                    opportunityDTO = new OpportunityDTO
                    {
                        Id = opportunity.Id,
                        Description = opportunity.Description
                    };
                }

                yield return new LeadDTO
                {
                    Id = lead.Id,
                    CustomerEmail = lead.CustomerEmail,
                    CustomerName = lead.CustomerName,
                    CustomerPhone = lead.CustomerPhone,
                    Customer = customerDTO,
                    Date = lead.Date,
                    Opportunity = opportunityDTO,
                    Status = new StatusLeadDTO
                    {
                        Id = lead.Status.Id,
                        Description = lead.Status.Description
                    }
                };
            }
        }

        public void StartQualification(StartLeadQualificationDTO leadDTO)
        {
            var validator = new StartLeadQualificationValidator();
            validator.ValidateAndThrow(leadDTO);

            var lead = context.Leads.SingleOrDefault(x => x.Id == leadDTO.Id);
            if (lead == null)
                throw new Exception("Lead não encontrado.");

            lead.StatusId = (int)Common.Enums.StatusLead.EM_QUALIFICACAO;

            context.SaveChanges();
        }
    }
}
