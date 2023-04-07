﻿using Microsoft.AspNetCore.Diagnostics;
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

            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured"),
            };

            return Problem(statusCode: statusCode, title: message);
        }
    }
}
