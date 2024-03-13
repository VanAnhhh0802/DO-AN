using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common.Entities.DTO
{
    public class TableStatusRequest
    {
        public Guid TableManagerID { get; set; }
        public string Status { get; set; }

    }
}
