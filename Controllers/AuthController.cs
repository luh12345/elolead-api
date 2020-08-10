using Elogroup.Lead.Api.Common;
using Elogroup.Lead.Api.DTO;
using Elogroup.Lead.Api.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Elogroup.Lead.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public ApiResponse<TokenDTO> Post([FromBody] UserDTO dto)
        {
            var user = userService.GetUser(dto);
            var token = tokenService.GenerateToken(user.Id.ToString(), user.Username);

            return new ApiResponse<TokenDTO>
            {
                Action = "Auth",
                Data = new TokenDTO
                {
                    Username = user.Username,
                    ExpiresAt = DateTime.Now.AddHours(2),
                    Token = token
                }
            };
        }
    }
}