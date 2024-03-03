using Cafetown.Common;
using Cafetown.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL
{
    public class InventoryItemBL : BaseBL<InventoryItem>, IIventoryItemBL
    {
        #region Field
        private readonly IInventoryItemDL _inventoryItemDL;
        #endregion

        #region Constructor
        public InventoryItemBL(IInventoryItemDL inventoryItemDL) : base(inventoryItemDL)
        {
            _inventoryItemDL = inventoryItemDL;
        }
        #endregion
    }
}
