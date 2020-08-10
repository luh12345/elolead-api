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
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public ApiResponse<string> Post([FromBody] CreateUserDTO dto)
        {
            userService.CreateUser(dto);
            return new ApiResponse<string>
            {
                Action = "CreateUser",
                Data = "Usuário cadastrado com succeso."
            };
        }
    }
}