using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.DTO
{
    public class LeadDTO
    {
        public int Id { get; set; }
        public StatusLeadDTO Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime Date { get; set; }
        public CustomerDTO Customer { get; set; }
        public OpportunityDTO Opportunity { get; set; }
    }
}
