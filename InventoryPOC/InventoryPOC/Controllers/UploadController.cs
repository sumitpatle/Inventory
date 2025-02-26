using Application.Upload;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventoryPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        protected readonly ILogger<UploadController> _logger;
        protected readonly IMediator _mediator;

        public UploadController(ILogger<UploadController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost()]

        public async Task<IActionResult> Upload([FromForm] UploadCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!string.IsNullOrEmpty(result))
                    return Ok(new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "File Uploaded successfully"
                    });
                else
                    return BadRequest(new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Error on file upload"
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading the file.");

                return BadRequest(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Error on file upload"
                });
            }
        }
    }
}
