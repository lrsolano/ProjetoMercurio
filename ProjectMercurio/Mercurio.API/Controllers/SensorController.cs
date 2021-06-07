using Mercurio.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : Controller
    {
        private readonly SensorConverter _converter;
        private readonly IConfiguration _config;

        public SensorController(IConfiguration configuration)
        {
            _converter = new SensorConverter();
            _config = configuration;
        }
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<SensorV>), 200)]
        public IActionResult GetAll()
        {

            return Ok(_converter.Parser(Sensor.FindAll()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SensorV), 200)]
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
                var usuario = _converter.Parser(Sensor.FindById(id));
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
