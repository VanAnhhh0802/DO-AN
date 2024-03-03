using Cafetown.Common.Entities;
using Cafetown.DL.StockDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL.VendorDL
{
    public class VendorDL : BaseDL<Vendor>, IVendorDL
    {
        private readonly IConnectionDL _connectionDL;

        public VendorDL(IConnectionDL connectionDL) : base(connectionDL)
        {
            _connectionDL = connectionDL;
        }
    }
}
