using Elogroup.Lead.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Services.Contract
{
    public interface IUserService
    {
        void CreateUser(CreateUserDTO user);
        UserDTO GetUser(UserDTO user);
    }
}
