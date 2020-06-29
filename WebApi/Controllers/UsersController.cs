using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;
using WebApi.WebApiDatabase;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserService userService;
        private ApiDatabase database;
        public UsersController(UserService userService, ApiDatabase database)
        {
            this.userService = userService;
            this.database = database;
        }

        [HttpPost]
        public IActionResult Post(AuthenticationInput input)
        {
            var result = userService.Authenticate(input);
            if (result == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(result);
        }


        [Route("createuser")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult PostUser(User user)
        {
            user.CreatedBy = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email).Value;

            database.Add(user);
            database.SaveChanges();

            return Ok();
        }

        [Route("VerifyEmail/{email}")]
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmail(string email)
        {
            var user = database.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
                return Ok($"Email {email} is already in use.");
            return Ok(true);
        }


    }
}
