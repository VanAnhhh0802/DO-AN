using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common.Entities.DTO
{
    public class LoginResult
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public bool? IsManager { get; set; } 
    }
}
