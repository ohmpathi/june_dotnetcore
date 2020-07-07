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
using WebApi.WebApiDatabase;

namespace WebApi.Services
{
    public class UserService
    {
        public IConfiguration Configuration { get; }
        private ApiDatabase database;

        public UserService(IConfiguration config, ApiDatabase database)
        {
            Configuration = config;
            this.database = database;
        }

        public AuthenticationResult Authenticate(AuthenticationInput input)
        {
            var user = database.Users.SingleOrDefault(x => x.Email == input.Username && x.Password == input.Password);

            if (user == null)
            {
                return null;
            }
            else
            {
                var keyAsBytes = Encoding.ASCII.GetBytes(Configuration.GetSection("AuthKey").Value);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("City", user.City)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(50),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyAsBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new AuthenticationResult
                {
                    Username = user.Email,
                    token = tokenHandler.WriteToken(token),
                    Exipres = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt")
                };
            }
        }
    }
}