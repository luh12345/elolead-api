using Elogroup.Api.Repository;
using Elogroup.Lead.Api.DTO;
using Elogroup.Lead.Api.Repository.Entities;
using Elogroup.Lead.Api.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Services
{
    public class StatusLeadService : IStatusLeadService
    {
        private readonly Context context;

        public StatusLeadService(Context context)
        {
            this.context = context;
        }

        public IEnumerable<StatusLeadDTO> GetStatusLead()
        {
            return context.StatusLeads.Select(x => new StatusLeadDTO
            {
                Id = x.Id,
                Description = x.Description
            });
        }
    }
}
