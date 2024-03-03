using Cafetown.BL;
using Cafetown.BL.StockBL;
using Cafetown.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StocksController : BasesController<Stock>
    {
        private readonly IStockBL _StockBL;

        public StocksController(IStockBL stockBL) : base(stockBL)
        {
            _StockBL = stockBL;
        }
    }
}
