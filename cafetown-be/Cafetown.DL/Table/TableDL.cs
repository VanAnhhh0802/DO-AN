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

        public async Task<List<TableManager>> GetAllFilter(string textFilter)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            var queryText = "Proc_TableManager_getAllFilter";
            var parameters = new DynamicParameters();
            parameters.Add("$text", textFilter);

            // Khai báo kết quả trả về
            var list = new List<TableManager>();

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                var records = mySqlConnection.QueryMultiple(queryText, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                list = records.Read<TableManager>().ToList();
            }

            return list;
        }

        public async Task<bool> UpdateStatus(Guid tableID, string status)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            var queryText = "Proc_TableManager_UpdateStatus";
            var parameters = new DynamicParameters();
            parameters.Add("$TableManagerID", tableID);
            parameters.Add("$Status", status);


            // Khai báo kết quả trả về;
            var numberOfAffectedRows = 0;
            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                numberOfAffectedRows = _connectionDL.Execute(mySqlConnection, queryText, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }

            if (numberOfAffectedRows > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateNumberUse(Guid tableID)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            var queryText = "Proc_TableManager_UpdateNumber";
            var parameters = new DynamicParameters();
            parameters.Add("$TableManagerID", tableID);

            // Khai báo kết quả trả về
            var numberOfAffectedRows = 0;
            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                numberOfAffectedRows = _connectionDL.Execute(mySqlConnection, queryText, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }

            if (numberOfAffectedRows > 0)
            {
                return true;
            }
            return false;
        }
    }


}
