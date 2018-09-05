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
                .Select(u => new { u.Email, u.Password, u.Salt })
                .FirstOrDefaultAsync();

            if (user == null || !signInModel.Password.IsValid(user.Password, user.Salt))
                return NotFound();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                configuration["Tokens:Issuer"],
                configuration["Tokens:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}