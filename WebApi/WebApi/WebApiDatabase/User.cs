using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.WebApiDatabase
{
    public class User : IValidatableObject
    {
        public int Id { get; set; }

        [MinLength(10)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
        public string City { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService(typeof(ApiDatabase)) as ApiDatabase;

            var exists = dbContext.Users.Any(user => user.Email == Email);

            if (exists)
            {
                yield return new ValidationResult($"Email {Email} is already taken.");
            }
        }
    }
}
