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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpPost]
        public ApiResponse<string> Post([FromBody] CreateCustomerDTO dto)
        {
            customerService.CreateCustomer(dto);
            return new ApiResponse<string>
            {
                Action = "CreateCustomer",
                Data = "Customer criado com sucesso."
            };
        }
    }
}