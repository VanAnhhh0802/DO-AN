using Cafetown.Common;
using Cafetown.Common.Entities;
using Cafetown.DL.StockDL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL.Table
{
    public class TableDL : BaseDL<TableManager>, ITableDL
    {
        private readonly IConnectionDL _connectionDL;

        public TableDL(IConnectionDL connectionDL) : base(connectionDL)
        {
            _connectionDL = connectionDL;
        }
        
        public override void GetValueProperties(TableManager record, PropertyInfo[] properties, DynamicParameters parameters)
        {
            parameters.Add($"TableManagerName", record.TableManagerName);
            parameters.Add($"TableManagerCode", record.TableManagerCode);
            parameters.Add($"Capacity", record.Capacity);
            parameters.Add($"ModifiedDate", record.ModifiedDate);
        }
    }

    
}
