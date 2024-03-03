using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common.Entities
{
    public class TableManager : BaseEntity
    {
        public Guid TablesID { get; set; }
        public string TablesCode { get; set; }
        public string TablesName { get; set; }

        /// <summary>
        /// Sức chứa của bàn
        /// </summary>
        public int Capacity { get; set; }

        public string Status { get; set; } = "Còn trống";

        public int? NumberUses { get; set; }
    }
}
