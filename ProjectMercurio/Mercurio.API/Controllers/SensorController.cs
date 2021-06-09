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
        [Authorize]
        [ProducesResponseType(typeof(List<SensorV>), 200)]
        public IActionResult GetAll()
        {

            return Ok(_converter.Parser(Sensor.FindAll()));
        }

        [HttpGet("{id}")]
        [Authorize]
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
                var sensor = _converter.Parser(Sensor.FindById(id));
                if (sensor == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                return StatusCode(200, sensor);
            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }

        }
        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(SensorV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        public IActionResult CreateSensor([FromBody] SensorV sensorV)
        {
            try
            {
                Sensor sensor = _converter.Parser(sensorV);
                if (sensor == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                sensor.CreateSensor();

                return StatusCode(200, _converter.Parser(sensor));
            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }

        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        public IActionResult DeleteSensor(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, new ErrorClass(400, "Codigo Invalido", DateTime.Now));
                }
                var sensor = Sensor.FindById(id);
                if (sensor == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                sensor.DeleteSensor();
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

        }

        [HttpPut("updateInfos/{idSensor}")]
        [Authorize]
        [ProducesResponseType(typeof(SensorV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        public IActionResult UpdateInfos([FromBody] SensorV sensorV, long idSensor)
        {
            try
            {
                Sensor sensor = Sensor.FindById(idSensor);
                if (sensor == null || sensor.Id == 0)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                bool needUpdate = false;
                if(sensorV.Nome != sensor.Nome  & !string.IsNullOrEmpty(sensorV.Nome))
                {
                    sensor.ChangeName(sensorV.Nome);
                    needUpdate = true;
                }
                DirecaoConverter direcaoConverter = new DirecaoConverter();
                if (!sensor.Direcao.Equals(direcaoConverter.Parser(sensorV.Direcao)))
                {
                    sensor.ChangeDirecao(direcaoConverter.Parser(sensorV.Direcao));
                    needUpdate = true;
                }


                if (needUpdate)
                {
                    sensor.UpdateSensor();
                }
                else
                {
                    return StatusCode(400, new ErrorClass(400, "Nenhuma mudança encontrada", DateTime.Now));
                }
                

                return StatusCode(200, _converter.Parser(sensor));
            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }

        }
    }
}
