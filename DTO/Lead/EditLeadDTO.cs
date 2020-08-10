using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.DTO
{
    public class EditLeadDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public OpportunityDTO Opportunity { get; set; }
    }
}
