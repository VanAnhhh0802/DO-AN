using Cafetown.Common;
using Cafetown.Common.Entities;
using Cafetown.DL.StockDL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public override TableManager GetRecordByID(Guid recordID)
        {
            // Khai báo kết nối tới DB
            var connectionString = DattaContext.ConnectionString;

            // Khai báo tên stored procedure
            var storedProcedureName = string.Format(ProcedureNames.GET_BY_ID, typeof(TableManager).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("TablesID", recordID);

            // Khai báo kết quả trả về
            var record = default(TableManager);

            using (var connection = _connectionDL.InitConnection(connectionString))
            {
                // Thực hiện gọi vào DB để chạy procedure
                record = _connectionDL.QueryFirstOrDefault<TableManager>(connection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return record;
        }
    }
}
