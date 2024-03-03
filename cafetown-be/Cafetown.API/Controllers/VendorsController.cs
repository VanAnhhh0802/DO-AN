using Cafetown.BL;
using Cafetown.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VendorsController : BasesController<Vendor>
    {
         private readonly IVendorBL _vendorBL;

        public VendorsController(IVendorBL vendorBL) : base(vendorBL)
        {
            _vendorBL = vendorBL;
        }
    }
}
