using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class CarrinhoV
    {
        public long Id { get; internal set; }
        public string Nome { get; set; }
        public long IdPedido { get; set; }
        public bool Disponivel { get; internal set; }
        public long IdUltimoSensor { get; internal set; }
        public string HashRota { get; internal set; }
    }
}
