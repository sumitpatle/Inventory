using Application.Booking;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : BaseApiController<BookingController>
    {
        [HttpPost("book")]
        public async Task<IActionResult> BookItem([FromBody] BookItemCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] CancelBookingCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
