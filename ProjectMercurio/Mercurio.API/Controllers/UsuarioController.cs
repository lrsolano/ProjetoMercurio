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
        [ProducesResponseType(typeof(ErrorClass), 500)]
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
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }

        }

        [HttpPost("create")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult CreateUser([FromBody] CreateUsuarioClass usuarioClass)
        {
            try
            {
                if(usuarioClass.Grupos.Count == 0 || usuarioClass.Idade == 0 || usuarioClass.Nome == "" || usuarioClass.Senha == "")
                {
                    return StatusCode(400, new ErrorClass(400, "Insira todas Informações", DateTime.Now));
                }
                Usuario u = new Usuario(usuarioClass.Nome, usuarioClass.Idade);
                u.AddSenha(usuarioClass.Senha);
                foreach(GrupoV g in usuarioClass.Grupos)
                {
                    if(g.Nome != "")
                    {
                        Grupo grupo = Grupo.FindByName(g.Nome);
                        if(grupo == null)
                        {
                            return StatusCode(404, new ErrorClass(404, "Grupo não encontrado", DateTime.Now));
                        }
                        u.AddGrupo(grupo);
                    }
                    else
                    {
                        Grupo grupo = Grupo.FindById((int)g.Id);
                        if (grupo == null)
                        {
                            return StatusCode(404, new ErrorClass(404, "Grupo não encontrado", DateTime.Now));
                        }
                        u.AddGrupo(grupo);
                    }
                }
                u.CreateUsuario();
                return Ok(_converter.Parser(u));
            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }

        }


        [HttpDelete("{idUsuario}")]
        [Authorize(Roles = "SuperUser")]
        [ProducesResponseType(typeof(UsuarioV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult DeleteUser(long idUsuario)
        {
            try
            {
                Usuario u = Usuario.FindById(idUsuario);
                if(u == null)
                {
                    return StatusCode(400, new ErrorClass(400, "Usuario não Encontrado", DateTime.Now));
                }
                u.DeleteUsuario();
                return NoContent();
            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }

        }


    }
}
