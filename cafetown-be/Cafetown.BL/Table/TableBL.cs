using Cafetown.BL.StockBL;
using Cafetown.Common.Entities;
using Cafetown.DL;
using Cafetown.DL.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL.Table
{
    public class TableBL : BaseBL<TableManager>, ITableBL
    {
        private ITableDL _tableDL;
        public TableBL(ITableDL tableDL) : base(tableDL)
        {
            _tableDL = tableDL;
        }

        public async Task<List<TableManager>> GetAllFilter(string textFilter)
        {
            return await _tableDL.GetAllFilter(textFilter);
        }

        public async Task<bool> UpdateStatus(Guid tableID, string status)
        {
            
              var res = await _tableDL.UpdateStatus(tableID, status);
            if (res == false) return false;

            if(status == "Đã sử dụng")
            {
                await _tableDL.UpdateNumberUse(tableID);
            }
            return true;

        }
    }
}
