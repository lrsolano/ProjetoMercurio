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
    public class CarrinhoController : Controller
    {
        private readonly CarrinhoConverter _converter;
        private readonly IConfiguration _config;

        public CarrinhoController(IConfiguration configuration)
        {
            _converter = new CarrinhoConverter();
            _config = configuration;
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<CarrinhoV>), 200)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_converter.Parser(Carrinho.FindAll()));
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CarrinhoV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult Get(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, new ErrorClass(400, "Codigo Invalido", DateTime.Now));
                }
                var item = _converter.Parser(Carrinho.FindById(id));
                if (item == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                return StatusCode(200, item);
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

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CarrinhoV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult CreateItem([FromBody] CarrinhoV item)
        {
            try
            {
                Carrinho i = _converter.Parser(item);
                i.CreateCarrinho();
                return Ok(_converter.Parser(i));
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
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult DeleteItem(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, new ErrorClass(400, "Codigo Invalido", DateTime.Now));
                }
                var item = (Carrinho.FindById(id));
                if (item == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Item não encontrado", DateTime.Now));
                }
                item.DeleteCarrinho();

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

        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CarrinhoV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult AtualizarCarrinho([FromBody] CarrinhoV carrinhoV, long id)
        {
            try
            {
                PedidoConverter pedidoConverter = new PedidoConverter();
                SensorConverter sensorConverter = new SensorConverter();
                if (id <= 0)
                {
                    return StatusCode(400, new ErrorClass(400, "Codigo Invalido", DateTime.Now));
                }
                var item = (Carrinho.FindById(id));
                if (item == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                if (carrinhoV.Nome != "" || string.IsNullOrWhiteSpace(carrinhoV.Nome))
                {
                    item.ChangeName(carrinhoV.Nome);
                }
                if(carrinhoV.IdPedido != 0)
                {
                    item.ChangePedido(Pedido.FindById(carrinhoV.IdPedido));
                }
                
                if(carrinhoV.IdUltimoSensor != 0)
                {
                    item.ChangeUltimoSensor(Sensor.FindById(carrinhoV.IdUltimoSensor));
                }
                
                item.UpdateCarrinho();

                return StatusCode(200, _converter.Parser(item));
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

        [HttpPut("finalizar/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CarrinhoV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult FinalizaCorrida(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, new ErrorClass(400, "Codigo Invalido", DateTime.Now));
                }
                var item = (Carrinho.FindById(id));
                if (item == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                item.FinalizarCorrida();

                return StatusCode(200, _converter.Parser(Carrinho.FindById(id)));
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
