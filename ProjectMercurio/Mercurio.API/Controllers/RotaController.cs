﻿using Mercurio.Core;
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
    }
}
