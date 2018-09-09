using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCVBackend.Domain;
using SCVBackend.Model;
using System.Linq;
using System.Threading.Tasks;
using SCVBackend.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace SCVBackend.Controllers
{
    [Route("api/[controller]")]
    public class SignInController : Controller
    {
        private readonly ScvContext scvContext;

        private readonly IConfiguration configuration;

        public SignInController(ScvContext scvContext, IConfiguration configuration)
        {
            this.scvContext = scvContext;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await scvContext.Users
                .Where(u => u.Email == signInModel.Email)
                .Select(u => new { u.Id, u.Type, u.Name, u.Email, u.Password, u.Salt, u.Photo })
                .FirstOrDefaultAsync();

            if (user == null || !signInModel.Password.IsValid(user.Password, user.Salt))
                return NotFound();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Type.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.WithSecretIfAvailable("Tokens:Key", "SECRET_TOKEN")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                configuration.WithSecretIfAvailable("Tokens:Issuer", "SECRET_ISSUER"),
                configuration.WithSecretIfAvailable("Tokens:Audience", "SECRET_AUDIENCE"),
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new SignInResponseModel(serializedToken, user.Id, user.Type, user.Name, user.Photo.ToBase64()));
        }
    }
}