using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;

namespace Demo1.BasicAuthentication
{
    public class BasicAuthFilter : IAuthorizationFilter
    {
        private readonly string appName;
        public BasicAuthFilter(string appName)
        {
            this.appName = appName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // extract Authentication header
            // Basic base64(username:password)
            string authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (authHeader.StartsWith("Basic "))
            {
                string base64 = authHeader.Substring(6);

                var credentials = decode(base64).Split(':', 2);

                if (!IsAuthorized(context, credentials[0], credentials[1]))
                {
                    UnauthorizedAccessException(context);
                }
            }
            else
            {
                UnauthorizedAccessException(context);
            }
        }

        private bool IsAuthorized(AuthorizationFilterContext context, string username, string password)
        {
            UserService userService = context.HttpContext.RequestServices.GetService(typeof(UserService)) as UserService;

            return userService.IsAuthenticated(username, password);
        }

        private string decode(string base64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64 ?? string.Empty));
        }

        private void UnauthorizedAccessException(AuthorizationFilterContext context)
        {
            // Return 401 and a basic authentication challenge (causes browser to show login dialog)
            context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{appName}\"";
            context.Result = new UnauthorizedResult();
        }
    }
}
