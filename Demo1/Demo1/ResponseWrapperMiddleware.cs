using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Demo1
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrapperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                //set the current response to the memorystream.
                context.Response.Body = memoryStream;

                await _next(context);

                //reset the body 
                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                string controllerResult = new StreamReader(memoryStream).ReadToEnd();

                string response = "{ \"data\": " + controllerResult + "}";
                await context.Response.WriteAsync(response);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseWrapperMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapperMiddleware>();
        }
    }
}
