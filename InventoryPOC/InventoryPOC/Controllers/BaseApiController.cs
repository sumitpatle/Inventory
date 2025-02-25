using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController<T> : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> Logger => HttpContext.RequestServices.GetService<ILogger<T>>();
    }
}
