using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.RequestModels;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserPersistence userPersistence;

        public AuthController(IConfiguration configuration, UserPersistence userPersistence)
        {
            this.configuration = configuration;
            this.userPersistence = userPersistence;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody]LoginRequest loginRequest)
        {
            var userInfo = userPersistence.Login(loginRequest.Username, loginRequest.Password);
            if (userInfo.Id == default(Guid))
            {
                return BadRequest();
            }

            var token = new JwtSecurityToken(
                   issuer: configuration["Jwt:Issuer"],
                   audience: configuration["Jwt:Audience"],
                   claims: new[]
                    {
                        new Claim(ClaimTypes.Name, userInfo.FirstName),
                        new Claim(ClaimTypes.NameIdentifier, userInfo.Username),
                        new Claim("UserId", userInfo.Id.ToString())
                    },
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecurityKey"])), SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
