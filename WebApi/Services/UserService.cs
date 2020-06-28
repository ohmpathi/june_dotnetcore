using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

namespace WebApi.Services
{
    public class User
    {
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserService
    {
        public IConfiguration Configuration { get; }

        public UserService(IConfiguration config)
        {
            Configuration = config;
        }

        private List<User> userNames = new List<User> {
            new User{Username="admin",Password="1234", FirstName="Administrator" },
            new User{Username="test",Password="admin", FirstName="Test User" },
        };

        public AuthenticationResult Authenticate(AuthenticationInput input)
        {
            var user = userNames.SingleOrDefault(x => x.Username == input.Username && x.Password == input.Password);

            if (user == null)
            {
                return null;
            }
            else
            {
                var keyAsBytes = Encoding.ASCII.GetBytes(Configuration.GetSection("AuthKey").Value);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username)
                    }),
                    Expires = DateTime.UtcNow.AddSeconds(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyAsBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new AuthenticationResult
                {
                    FirstName = user.FirstName,
                    Username = user.Username,
                    token = tokenHandler.WriteToken(token)
                };
            }
        }
    }
}