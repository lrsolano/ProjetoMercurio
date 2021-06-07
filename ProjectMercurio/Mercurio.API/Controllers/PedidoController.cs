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
    public class PedidoController : Controller
    {
        private readonly PedidoConverter _converterPedido;
        private readonly ItemConverter _itemConverter;
        private readonly IConfiguration _config;

        public PedidoController(IConfiguration configuration)
        {
            _converterPedido = new PedidoConverter();
            _itemConverter = new ItemConverter();
            _config = configuration;
        }
        [HttpGet]
        [Authorize(Roles = "SuperUser")]
        [ProducesResponseType(typeof(List<PedidoV>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_converterPedido.Parser(Pedido.FindAll()));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<PedidoV>), 200)]
        public IActionResult GetById(long id)
        {
            return Ok(_converterPedido.Parser(Pedido.FindById(id)));
        }


        [HttpGet("meusPedidos")]
        [Authorize]
        [ProducesResponseType(typeof(List<PedidoV>), 200)]
        public IActionResult GetPedidos()
        {
            var lista = _converterPedido.Parser(Pedido.FindAll());
            lista.RemoveAll(i => i.NomeUsuario != User.Identity.Name);
            return Ok(lista);
        }


        [HttpPost]
        [Route("create")]
        [Authorize]
        [ProducesResponseType(typeof(PedidoV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult CreatePedido([FromBody]PedidoV pedidoV)
        {
            try
            {
                Pedido pedido = _converterPedido.Parser(pedidoV);
                pedido.CreatePedido();
                return Ok(_converterPedido.Parser(pedido));
            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }
            
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult Delte(long id)
        {
            try
            {
                Pedido pedido = Pedido.FindById(id);
                if(pedido == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Pedido não encontrado", DateTime.Now));
                }
                pedido.DeletePedido();
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
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }

        }

        [HttpPut]
        [Route("addItem/{idPedido}")]
        [Authorize]
        [ProducesResponseType(typeof(PedidoV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult AddItem([FromBody] List<ItemV> itemV, long idPedido)
        {
            try
            {
                
                Pedido pedido = Pedido.FindById(idPedido);
                if (pedido == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Pedido não encontrado", DateTime.Now));
                }
                foreach(ItemV i in itemV)
                {
                    Item item = _itemConverter.Parser(i);
                    pedido.AddItem(item, item.Quantidade);
                }
                pedido.UpdatePedido();
                return Ok(_converterPedido.Parser(pedido));

            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }
            

        }
        [HttpDelete]
        [Route("removeItem/{idPedido}")]
        [Authorize]
        [ProducesResponseType(typeof(PedidoV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult RemoveItem([FromBody] List<ItemV> itemV, long idPedido)
        {
            try
            {

                Pedido pedido = Pedido.FindById(idPedido);
                if (pedido == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Pedido não encontrado", DateTime.Now));
                }
                foreach (ItemV i in itemV)
                {
                    Item item = _itemConverter.Parser(i);
                    pedido.RemoveItem(item);
                }
                pedido.UpdatePedido();
                return Ok(_converterPedido.Parser(pedido));

            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }


        }

        [HttpPut]
        [Route("changeItem/{idPedido}")]
        [Authorize]
        [ProducesResponseType(typeof(PedidoV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult ChangeItem([FromBody] List<ItemV> itemV, long idPedido)
        {
            try
            {

                Pedido pedido = Pedido.FindById(idPedido);
                if (pedido == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Pedido não encontrado", DateTime.Now));
                }
                foreach (ItemV i in itemV)
                {
                    Item item = _itemConverter.Parser(i);
                    pedido.ChangeItem(item, item.Quantidade);
                }
                pedido.UpdatePedido();
                return Ok(_converterPedido.Parser(pedido));

            }
            catch (MercurioCoreException ex)
            {
                return StatusCode(400, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (DBConnectionException ex)
            {
                return StatusCode(500, new ErrorClass(400, ex.Message, DateTime.Now));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorClass(500, ex.Message, DateTime.Now));
            }


        }
        [HttpPut]
        [Route("changeRota/{idPedido}")]
        [Authorize]
        [ProducesResponseType(typeof(PedidoV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult ChangeRota([FromBody] ChangeRotaClass rota, long idPedido)
        {
            try
            {

                Pedido pedido = Pedido.FindById(idPedido);
                if (pedido == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Pedido não encontrado", DateTime.Now));
                }
                Sensor si = Sensor.FindById(rota.IdSensorInicial);
                if (si == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor inicial não encontrado", DateTime.Now));
                }
                
                Sensor sf = Sensor.FindById(rota.IdSensorFinal);
                if (sf == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor final não encontrado", DateTime.Now));
                }

                pedido.SetRota(si, sf);
                pedido.UpdatePedido();
                return Ok(_converterPedido.Parser(pedido));


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
