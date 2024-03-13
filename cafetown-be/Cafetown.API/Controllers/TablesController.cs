using Cafetown.BL;
using Cafetown.BL.Table;
using Cafetown.Common.Entities;
using Cafetown.Common.Entities.DTO;
using Cafetown.DL.Table;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TablesController : BasesController<TableManager>
    {
        private readonly ITableBL _tableBL;
        public TablesController(ITableBL tableBL) : base(tableBL)
        {
            _tableBL = tableBL;
        }

        [HttpGet("getAllFilter")]
        public async Task<IActionResult> GetAllFilterStatus(string text = "Còn trống") 
        {
            var res = await _tableBL.GetAllFilter(text);
            return StatusCode(StatusCodes.Status200OK, res);
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] TableStatusRequest tableStatusRequest)
        {
            var res = await _tableBL.UpdateStatus(tableStatusRequest.TableManagerID, tableStatusRequest.Status);
            return StatusCode(StatusCodes.Status200OK, res);
        }

    }
}
