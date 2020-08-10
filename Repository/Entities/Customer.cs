using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Repository.Entities
{
    public class Customer : BaseEntity
    {
        public int LeadId { get; set; }
        public Lead Lead { get; set; }
    }
}
