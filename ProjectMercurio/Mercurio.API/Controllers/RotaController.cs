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
    public class RotaController : Controller
    {
        private readonly RotaConverter _converter;
        private readonly IConfiguration _config;

        public RotaController(IConfiguration configuration)
        {
            _converter = new RotaConverter();
            _config = configuration;
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<RotaV>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_converter.Parser(Rota.FindAll()));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(RotaV), 200)]
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
                var rota = _converter.Parser(Rota.FindById(id));
                if (rota == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                return StatusCode(200, rota);
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

        [HttpPost("{idCarrinho}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UltimoSensor), 200)]
        public IActionResult GetNextSensor([FromBody] UltimoSensor ultimoSensor, long idCarrinho)
        {
            try
            {
                Carrinho c = Carrinho.FindById(idCarrinho);
                if (c.Pedido == null)
                {
                    return StatusCode(400, new ErrorClass(400, "Carrinho não está em rota no momento", DateTime.Now));
                }
                Rota rota = c.Pedido.Rota;
                Sensor sensor = null;
                if (string.IsNullOrWhiteSpace(ultimoSensor.SensorAtual))
                {
                    sensor = Sensor.FindById(1);
                }
                else
                {
                    sensor = Sensor.FindByHash(ultimoSensor.SensorAtual);
                }
                if(sensor == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                bool proximo = false;
                string proximoSensorHash = string.Empty;
                string movimento = string.Empty;

                foreach(string s in rota.Tracado.Split(';'))
                {
                    long idSensor = ConvertToLong(s.Split('-')[0]);
                    if(idSensor == 0)
                    {
                        return StatusCode(404, new ErrorClass(404, "Sensor não encontrado na rota", DateTime.Now));
                    }
                    if (proximo)
                    {
                        proximoSensorHash = Sensor.FindById(idSensor).Hash;
                        
                        break;
                    }
                    
                    if(idSensor == sensor.Id)
                    {
                        movimento = s.Split('-')[1];
                        if(movimento == "Final")
                        {
                            proximoSensorHash = "FIM DA ROTA";
                            break;
                        }
                        proximo = true;
                    }
                }
                c.ChangeUltimoSensor(sensor);
                c.UpdateCarrinho();
                UltimoSensor u = new UltimoSensor() { Movimento = movimento, ProximoSensor = proximoSensorHash };
                return Ok(u);
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

        private long ConvertToLong(string strNumber)
        {
            long longValue;
            if (long.TryParse(strNumber, out longValue))
            {
                return longValue;
            }

            return 0;
        }
    }
}
