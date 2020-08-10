using Elogroup.Lead.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Services.Contract
{
    public interface ILeadService
    {
        IEnumerable<LeadDTO> GetLeads(LeadFilterDTO leadDTO);
        void CreateLead(CreateLeadDTO leadDTO);
        void EditLead(EditLeadDTO leadDTO);
        void DeleteLead(int id);
        void DiscardLead(DiscardLeadDTO leadDTO);
        void StartQualification(StartLeadQualificationDTO leadDTO);
    }
}
