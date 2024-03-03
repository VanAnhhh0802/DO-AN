using Cafetown.BL;
using Cafetown.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TablesController : BasesController<TableManager>
    {
        public TablesController(IBaseBL<TableManager> baseBL) : base(baseBL)
        {
        }
    }
}
