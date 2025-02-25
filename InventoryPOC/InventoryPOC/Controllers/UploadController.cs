using Application.Booking;
using Application.Upload;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventoryPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : BaseApiController<UploadController>
    {
        [HttpPost()]

        public async Task<IActionResult> Upload([FromForm] UploadCommand command)
        {
            var result = await Mediator.Send(command);
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
    }
}
