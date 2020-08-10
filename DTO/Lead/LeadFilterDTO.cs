using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.DTO
{
    public class LeadFilterDTO
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int? StatusId { get; set; }
    }
}
