using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserService userService;
        public UsersController(UserService userService)
        {
            this.userService = userService;
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
    }
}
