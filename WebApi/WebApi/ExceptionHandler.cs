using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace WebApi
{
    public static class ExceptionHandler
    {
        internal static Func<HttpContext, Func<Task>, Task> ReqBodyRewind()
        {
            return async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            };
        }

        // Handling exceptions through middleware
        public static void WebApiExceptionHandler(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Run(async context =>
            {
                // Error message
                // StackTrace
                // current user (if logged in)
                // request url
                // Query string parameters (if any)
                // Headers, Cookies (if any),
                // Post Body (if any)

                context.Response.StatusCode = 500;

                var ehf = context.Features.Get<IExceptionHandlerFeature>();

                //ehf.Error.Message;
                //ehf.Error.StackTrace;

                //context.User.Identity.Name
                //context.Request.Path.Value
                //context.Request.QueryString.Value
                //context.Request.Headers["Authorization"].ToString()

                //string body = new StreamReader(context.Request.Body).ReadToEnd();

                //context.Request.Body.Position = 0;

                

                //context.Response.ContentType = "text/html";

                //await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                //await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                var x = 9;

                // Use exceptionHandlerPathFeature to process the exception (for example, 
                // logging), but do NOT expose sensitive error information directly to 
                // the client.

                //if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                //{
                //    await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
                //}

                //await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                //await context.Response.WriteAsync("</body></html>\r\n");
                await context.Response.WriteAsync(new string(' ', 512)); // IE padding
            });
        }
    }
}
