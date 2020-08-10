using Elogroup.Lead.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Services.Contract
{
    public interface IOpportunityService
    {
        void CreateOpportunity(CreateOpportunityDTO opportunityDTO);
        void EditOpportunity(EditOpportunityDTO opportunityDTO);
        void DeleteOpportunity(int opportunityId);
    }
}
