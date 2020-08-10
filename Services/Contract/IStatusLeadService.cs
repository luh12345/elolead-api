using Elogroup.Lead.Api.DTO;
using System.Collections.Generic;

namespace Elogroup.Lead.Api.Services.Contract
{
    public interface IStatusLeadService
    {
        IEnumerable<StatusLeadDTO> GetStatusLead();
    }
}
