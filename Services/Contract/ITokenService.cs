using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Services.Contract
{
    public interface ITokenService
    {
        string GenerateToken(string userid, string username);
    }
}
