using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    public class InventoryItem : BaseEntity
    {
        /// <summary>
        /// ID mặt hàng
        /// </summary>
        [PrimaryKey]
        public Guid? InventoryItemID { get; set; }

        public string InventoryItemCode { get; set; }

        public string  InventoryItemName { get; set; }
        
        /// <summary>
        /// số lượng hàng
        /// </summary>
        public int? QuantitiesAdd { get; set;}

        /// <summary>
        /// số lượng hàng
        /// </summary>
        public int? QuantitiesOut { get; set; }

        public Guid? UseIDAdd { get; set; }
        public string? UserNameAdd { get; set; }
        public DateTime DateAdd { get; set; }
        public Guid? UseIDOut { get; set; }
        public string? UserNameOut { get; set; }
        public DateTime DateOut { get; set; }
    }
}
