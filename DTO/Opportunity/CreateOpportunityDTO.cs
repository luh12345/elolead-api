using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.DTO
{
    public class CreateOpportunityDTO
    {
        public int LeadId { get; set; }
        public string Description { get; set; }
    }
}
