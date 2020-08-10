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
    public class StatusLeadController : ControllerBase
    {
        private readonly IStatusLeadService statusLeadService;

        public StatusLeadController(IStatusLeadService statusLeadService)
        {
            this.statusLeadService = statusLeadService;
        }

        [HttpGet]
        public ApiResponse<IEnumerable<StatusLeadDTO>> Get()
        {
            var data = statusLeadService.GetStatusLead();

            return new ApiResponse<IEnumerable<StatusLeadDTO>>
            {
                Action = "GetStatusLead",
                Data = data
            };
        }
    }
}