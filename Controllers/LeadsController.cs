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
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService leadService;

        public LeadsController(ILeadService leadService)
        {
            this.leadService = leadService;
        }

        [HttpGet]
        public ApiResponse<IEnumerable<LeadDTO>> Get([FromQuery] LeadFilterDTO dto)
        {
            var leads = leadService.GetLeads(dto);

            return new ApiResponse<IEnumerable<LeadDTO>>
            {
                Action = "GetLeads",
                Data = leads
            };
        }

        [HttpPost]
        public ApiResponse<string> Post([FromBody] CreateLeadDTO dto)
        {
            leadService.CreateLead(dto);

            return new ApiResponse<string>
            {
                Action = "CreateLead",
                Data = "Lead cadastrado com sucesso."
            };
        }

        [HttpPut]
        public ApiResponse<string> Put([FromBody] EditLeadDTO dto)
        {
            leadService.EditLead(dto);

            return new ApiResponse<string>
            {
                Action = "EditLead",
                Data = "Lead alterado com sucesso."
            };
        }

        [HttpDelete]
        public ApiResponse<string> Delete([FromQuery] int id)
        {
            leadService.DeleteLead(id);

            return new ApiResponse<string>
            {
                Action = "DeleteLead",
                Data = "Lead removido com sucesso."
            };
        }

        [HttpPost, Route("[action]")]
        public ApiResponse<string> Discard([FromBody] DiscardLeadDTO dto)
        {
            leadService.DiscardLead(dto);

            return new ApiResponse<string>
            {
                Action = "DiscardLead",
                Data = "Lead descartado com sucesso."
            };
        }

        [HttpPost, Route("[action]")]
        public ApiResponse<string> StartQualification([FromBody] StartLeadQualificationDTO dto)
        {
            leadService.StartQualification(dto);

            return new ApiResponse<string>
            {
                Action = "StartQualification",
                Data = "Qualificação iniciada com succeso."
            };
        }
    }
}