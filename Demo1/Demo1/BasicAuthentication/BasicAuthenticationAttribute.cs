using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.BasicAuthentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthenticationAttribute: TypeFilterAttribute
    {
        public BasicAuthenticationAttribute(string appName="Demo APP") : base(typeof(BasicAuthFilter))
        {
            Arguments = new object[] { appName };
        }
    }
}
