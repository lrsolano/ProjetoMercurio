using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Mercurio.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mercurio.API.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioConverter _converter;
        private readonly IConfiguration _config;

        public UsuarioController(IConfiguration configuration)
        {
            _converter = new UsuarioConverter();
            _config = configuration;
        }


        [HttpGet]
        [Authorize(Roles = "SuperUser")]
        [ProducesResponseType(typeof(List<UsuarioV>), 200)]
        public IActionResult GetAll()
        {
            
            return Ok(_converter.Parser(Usuario.FindAll()));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(UsuarioV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        public IActionResult Get(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400,new ErrorClass(400, "Codigo Invalido", DateTime.Now));
                }
                var usuario = _converter.Parser(Usuario.FindById(id));
                if (usuario == null)
                {
                    return StatusCode(404,new ErrorClass(404, "Usuario não encontrado", DateTime.Now));
                }
                return StatusCode(200,usuario);
            }
            catch(MercurioCoreException ex)
            {
                return StatusCode(400,new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(400, ex.Message, DateTime.Now));
            }

        }

        


    }
}
