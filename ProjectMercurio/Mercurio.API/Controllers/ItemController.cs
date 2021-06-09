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
        [Authorize]
        [ProducesResponseType(typeof(List<ItemV>), 200)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_converter.Parser(Item.FindAll()));
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
        [Authorize]
        [ProducesResponseType(typeof(ItemV), 200)]
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
                var item = _converter.Parser(Item.FindById(id));
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
        [ProducesResponseType(typeof(ItemV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult CreateItem([FromBody] ItemV item)
        {
            try
            {
                Item i = _converter.Parser(item);
                i.CreateItem();
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

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ItemV), 200)]
        [ProducesResponseType(typeof(ErrorClass), 404)]
        [ProducesResponseType(typeof(ErrorClass), 400)]
        [ProducesResponseType(typeof(ErrorClass), 500)]
        public IActionResult AtualizaNome([FromBody] ItemV itemV,long id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, new ErrorClass(400, "Codigo Invalido", DateTime.Now));
                }
                var item = (Item.FindById(id));
                if (item == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Sensor não encontrado", DateTime.Now));
                }
                item.ChangeName(itemV.Nome);
                item.UpdateItem();

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
                var item = (Item.FindById(id));
                if (item == null)
                {
                    return StatusCode(404, new ErrorClass(404, "Item não encontrado", DateTime.Now));
                }
                item.DeleteItem();

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
    }
}
