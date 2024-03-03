using Cafetown.Common.Entities;
using Cafetown.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL
{
    public class VendorBL : BaseBL<Vendor>, IVendorBL
    {
        public VendorBL(IBaseDL<Vendor> baseDL) : base(baseDL)
        {
        }
    }
}
