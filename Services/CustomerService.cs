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
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly Context context;

        public CustomerService(Context context)
        {
            this.context = context;
        }

        public void CreateCustomer(CreateCustomerDTO customerDTO)
        {
            var validator = new CreateCustomerValidator();
            validator.ValidateAndThrow(customerDTO);

            var lead = context.Leads.SingleOrDefault(x => x.Id == customerDTO.LeadId);
            if (lead == null)
                throw new Exception("Lead não encontrado.");

            lead.StatusId = (int)Common.Enums.StatusLead.FINALIZADO;

            context.Customers.Add(new Customer
            {
                LeadId = lead.Id
            });

            context.SaveChanges();
        }
    }
}
