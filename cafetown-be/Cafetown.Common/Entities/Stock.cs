using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common.Entities
{
    public class Stock : BaseEntity
    {
        public Guid StockID { get; set; }
        public string StockCode { get; set; }
        public string StockName{ get; set; }
        public string Category { get; set; }

        public int Quantites { get; set; }

        public bool Inactive { get; set; }
    }
}
