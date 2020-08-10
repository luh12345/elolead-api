using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Lead.Api.Common
{
    public class ApiResponse<T>
    {
        public string Action { get; set; }
        public bool IsSuccess { get; set; } = true;
        public T Data { get; set; }
    }
}
