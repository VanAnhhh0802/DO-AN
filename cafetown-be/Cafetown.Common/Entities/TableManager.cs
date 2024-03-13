using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common.Entities
{
    public class TableManager : BaseEntity
    {
        [PrimaryKey]
        public Guid TableManagerID { get; set; }

        
        [ExcelColumnName("Mã bàn")]
        public string TableManagerCode { get; set; }

        [ExcelColumnName("Tên bàn")]

        public string TableManagerName { get; set; }

        /// <summary>
        /// Sức chứa của bàn
        /// </summary
        [ExcelColumnName("Sức chưa của bàn")]

        public int Capacity { get; set; }

        [ExcelColumnName("Trạng thái của bàn")]

        public string Status { get; set; } = "Còn trống";


        [ExcelColumnName("Số lần bàn được sử dụng")]

        public int NumberUses { get; set; } = 0;
    }
}
