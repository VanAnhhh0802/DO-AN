using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common.Entities
{
    public class Vendor  : BaseEntity
    {
        public Guid VendorID { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string TaxCode { get; set; }
        public string Email { get; set; }
        public string Inactive { get; set; }
    }
}
