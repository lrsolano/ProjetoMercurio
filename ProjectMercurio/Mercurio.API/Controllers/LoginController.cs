using Mercurio.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenClass), 200)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult Post([FromBody] LoginClasse login)
        {
            try
            {
                var usuario = Login.Signin(login.Usuario, login.Senha);
                var token = GerarTokenJwt(usuario);
                return Ok(token);

            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(400, ex.Message, DateTime.Now));
            }



        }

        [HttpGet("validate")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ValidateClass), 200)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult Validate()
        {
            try
            {
                bool autenticado = User.Identity.IsAuthenticated;
                string usuario = "";
                if (autenticado)
                {
                    usuario = User.Identity.Name;
                }
                return Ok(new ValidateClass(autenticado, usuario));

            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(400, ex.Message, DateTime.Now));
            }



        }

        [HttpPut("changePassword")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult ChangePassword([FromBody] PasswordChange pw)
        {
            try
            {
                Usuario usuario = Usuario.FindByName(User.Identity.Name);
                if(usuario == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Usuario não encontrado", DateTime.Now));
                }
                usuario.ChangePassword(pw);
                return NoContent();

                

            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(400, ex.Message, DateTime.Now));
            }



        }




        private object GerarTokenJwt(Usuario usuario)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(60);
            var securityKey = new SymmetricSecurityKey
                              (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials
                             (securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            foreach (Grupo g in usuario.Grupos)
            {
                claims.Add(new Claim(ClaimTypes.Role, g.Nome));
            }
            claims.Add(new Claim(ClaimTypes.Name, usuario.Nome));

            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             expires: expiry,
                                             signingCredentials: credentials,
                                             claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            string stringToken = tokenHandler.WriteToken(token);
            TimeSpan time = expiry -DateTime.Now ;
            return new TokenClass(stringToken, time.TotalSeconds);
        }
    }
}
