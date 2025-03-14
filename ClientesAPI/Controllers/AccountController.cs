using ClientesAPI.Services;
using ClientesAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClientesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _authentication;

        public AccountController(IConfiguration configuration, IAuthenticate authentication)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "As senhas nao conferem");
                return BadRequest(ModelState);
            }

            if (await _authentication.RegisterUser(model.Email, model.Password))
            {
                return Ok($"Usuario {model.Email} criado com sucesso");
            }
            else
            {
                ModelState.AddModelError("CreateUser", "Registro invalido");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> LoginUser([FromBody] LoginModel userInfo)
        {
            if (await _authentication.Authenticate(userInfo.Email, userInfo.Password))
            {
                var token = GenerateToken(userInfo);
                return Ok(token);
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Login invalido");
                return BadRequest(ModelState);
            }
        }

        private object GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuToken", "tokenFritz"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(20);

            JwtSecurityToken token = new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            claims: claims,
                            expires: expiration,
                            signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }
}
