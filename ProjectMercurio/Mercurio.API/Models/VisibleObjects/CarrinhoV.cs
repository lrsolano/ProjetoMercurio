using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class CarrinhoV
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public long IdPedido { get; set; }
        public bool Disponivel { get; set; }
        public long IdUltimoSensor { get; set; }
        public string HashRota { get; set; }
    }
}
