using Cafetown.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL.StockDL
{
    public class StockDL : BaseDL<Stock>, IStockDL
    {
        private readonly IConnectionDL _connectionDL;

        public StockDL(IConnectionDL connectionDL) : base(connectionDL)
        {
            _connectionDL = connectionDL;
        }
    }
}
