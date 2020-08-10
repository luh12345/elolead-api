using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elogroup.Lead.Api.Common;
using Elogroup.Lead.Api.DTO;
using Elogroup.Lead.Api.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elogroup.Lead.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityService opportunityService;

        public OpportunityController(IOpportunityService opportunityService)
        {
            this.opportunityService = opportunityService;
        }

        [HttpPost]
        public ApiResponse<string> Post([FromBody] CreateOpportunityDTO dto)
        {
            opportunityService.CreateOpportunity(dto);
            return new ApiResponse<string>
            {
                Action = "CreateOpportunitie",
                Data = "Opportunity criada com sucesso."
            };
        }

        [HttpPut]
        public ApiResponse<string> Put([FromBody] EditOpportunityDTO dto)
        {
            opportunityService.EditOpportunity(dto);
            return new ApiResponse<string>
            {
                Action = "EditOpportunity",
                Data = "Opportunity editada com sucesso."
            };
        }

        [HttpDelete]
        public ApiResponse<string> Delete([FromQuery] int opportunityId)
        {
            opportunityService.DeleteOpportunity(opportunityId);
            return new ApiResponse<string>
            {
                Action = "DeleteOpportunity",
                Data = "Opportunity removida com sucesso."
            };
        }
    }
}