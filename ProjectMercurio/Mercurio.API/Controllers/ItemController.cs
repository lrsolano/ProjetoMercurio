using Mercurio.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly ItemConverter _converter;
        private readonly IConfiguration _config;

        public ItemController(IConfiguration configuration)
        {
            _converter = new ItemConverter();
            _config = configuration;
        }
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<ItemV>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_converter.Parser(Item.FindAll()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ItemV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        public IActionResult Get(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, new ErrorClass(400, "Codigo Invalido", DateTime.Now));
                }
                var usuario = _converter.Parser(Item.FindById(id));
                if (usuario == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                return StatusCode(200, usuario);
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
    }
}
