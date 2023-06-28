using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Common.Errors;

namespace PMS.Api.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [NonAction]
        [Route("/error")]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            return Problem();
        }
    }
}
