using System;

namespace Elogroup.Lead.Api.Repository.Entities
{
    public class Lead : BaseEntity
    {
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }

        public StatusLead Status { get; set; }
        public int StatusId { get; set; }
    }
}
