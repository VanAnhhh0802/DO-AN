using Cafetown.Common.Entities;
using Cafetown.DL;
using Cafetown.DL.StockDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL.StockBL
{
    public class StockBL : BaseBL<Stock>, IStockBL
    {
        private readonly IStockDL _IStockDL;

        public StockBL(IStockDL stockDL) : base(stockDL)
        {
            _IStockDL = stockDL;
        }


    }
}
