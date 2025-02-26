using Application.Booking;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        protected readonly ILogger<DataController> _logger;
        protected readonly IMediator _mediator;

        public DataController(ILogger<DataController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("member")]
        public async Task<IActionResult> MemberData()
        {
            var result = await _mediator.Send(new MemberQueries { });
            return Ok(result);
        }

        [HttpGet("inventory")]
        public async Task<IActionResult> InventoryData()
        {
            var result = await _mediator.Send(new InventoryQueries { });
            return Ok(result);
        }
    }
}
