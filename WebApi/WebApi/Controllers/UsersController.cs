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

        /// <summary>
        /// To authenticate users
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Users
        ///     {
        ///        "username": "admin@webapi",
        ///        "password": "admin_password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Authentication details</response>
        /// <response code="401">Unauthorized</response>
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

        [Route("changerole")]
        [Authorize(Policy = "ChangeRole")]
        [HttpPost]
        public IActionResult ChangeUserRole(User user)
        {
            var u = database.Users.SingleOrDefault(dbUser => dbUser.Email == user.Email);
            u.Role = user.Role;
            database.SaveChanges();
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("cityinfo")]
        [Authorize(Policy = "CityAccess")]
        [HttpGet]
        public IActionResult CityAccess()
        {
            return Ok(User.Claims.Single(c => c.Type == "City").Value);
        }
    }
}
