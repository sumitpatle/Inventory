using Application.Booking;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : BaseApiController<DataController>
    {
        [HttpGet("member")]
        public async Task<IActionResult> MemberData()
        {
            var result = await Mediator.Send(new MemberQueries { });
            return Ok(result);
        }

        [HttpGet("inventory")]
        public async Task<IActionResult> InventoryData()
        {
            var result = await Mediator.Send(new InventoryQueries { });
            return Ok(result);
        }
    }
}
