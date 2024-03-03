using Cafetown.BL.StockBL;
using Cafetown.Common.Entities;
using Cafetown.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL.Table
{
    public class TableBL : BaseBL<TableManager>, ITableBL
    {
        public TableBL(IBaseDL<TableManager> baseDL) : base(baseDL)
        {
        }
    }
}
