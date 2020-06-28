using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class AuthenticationResult
    {
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string token { get; set; }
    }
}
