using Cafetown.BL;
using Cafetown.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InventoryItemsController : BasesController<InventoryItem>
    {
        #region Field
        private readonly IIventoryItemBL _inventoryItem;
        #endregion

        #region Constructor
        public InventoryItemsController(IIventoryItemBL inventoryItemBL) : base(inventoryItemBL)
        {
            _inventoryItem = inventoryItemBL;
        }
        #endregion


    }
}
