using Application.Booking;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace InventoryPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        protected readonly ILogger<BookingController> _logger;
        protected readonly IMediator _mediator;

        public BookingController(ILogger<BookingController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost("book")]
        public async Task<IActionResult> BookItem([FromBody] BookItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] CancelBookingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpGet("booking")]
        public async Task<IActionResult> Bookings([FromQuery] BookingQueries query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
