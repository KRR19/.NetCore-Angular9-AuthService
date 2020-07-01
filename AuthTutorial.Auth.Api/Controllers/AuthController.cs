using AuthTutorial.Auth.Api.Models;
using AuthTutorial.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace AuthTutorial.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> AuthOptions;

        public AuthController(IOptions<AuthOptions> authOptions)
        {
            AuthOptions = authOptions;
        }
        private List<Account> Accounts => new List<Account>
        {
            new Account()
            {
              Id = Guid.Parse("42E4F8AF-FCCD-4BE9-BC12-ADB0F050DBD1"),
              Email = "user@email.com",
              Password = "user",
              Roles = new Role[]{ Role.User}
            },
            new Account()
            {
              Id = Guid.Parse("BB1208BD-912A-408D-BEE9-78C7F4BC7A67"),
              Email = "user2@email.com",
              Password = "user2",
              Roles = new Role[]{ Role.User}
            },
            new Account()
            {
              Id = Guid.Parse("72D352C1-BA61-4D53-B93E-8FD57B5F9483"),
              Email = "admin@email.com",
              Password = "admin",
              Roles = new Role[]{ Role.Admin}
            }
        };

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            var user = AuthenticatUser(login.Email, login.Password);
            if (user != null)
            {
                var token = GenerateJWT(user);
                return Ok(new { access_token = token });
            }
            return Unauthorized();
        }

        [Route("logins")]
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
           
            return Ok();
        }

        private Account AuthenticatUser(string email, string password)
        {
            return Accounts.SingleOrDefault(user => user.Email == email && user.Password == password);
        }

        private string GenerateJWT(Account user)
        {
            var authParams = AuthOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
