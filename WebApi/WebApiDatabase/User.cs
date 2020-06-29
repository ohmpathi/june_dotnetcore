using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.WebApiDatabase
{
    public class User
    {
        public int Id { get; set; }

        [MinLength(10)]
        public string Name { get; set; }

        //[EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Users")]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
    }
}
