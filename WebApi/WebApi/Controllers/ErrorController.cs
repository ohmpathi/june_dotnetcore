using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [AllowAnonymous]
        public IActionResult Get()
        {
            // Error message
            // StackTrace
            // current user (if logged in)
            // request url
            // Query string parameters (if any)
            // Headers, Cookies (if any),
            // Post Body (if any)

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context?.Error != null)
            {
                var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                //reExecute.OriginalPath;


                var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionHandlerPathFeature != null)
                {
                    var path = exceptionHandlerPathFeature.Path;
                }

                var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                if (statusCodeReExecuteFeature != null)
                {
                    var path =
                        statusCodeReExecuteFeature.OriginalPathBase
                        + statusCodeReExecuteFeature.OriginalPath
                        + statusCodeReExecuteFeature.OriginalQueryString;
                }


                var result = new
                {
                    statusCode = 500,
                    //Url = HttpContext.Request.GetEncodedUrl(),
                    HttpContext.User.Identity.Name,
                    context.Error.Message,
                    context.Error.StackTrace,
                };

                return StatusCode((int)HttpStatusCode.InternalServerError, result);
            }
            else
            {
                return null;
            }
        }
    }
}
