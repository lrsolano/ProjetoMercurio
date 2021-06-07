using Mercurio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class PedidoV
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public List<ItemV> Items { get; set; }
        public long IdSensorInicial { get; set; }
        public long IdSensorFinal { get; set; }
        public long IdRota { get;  set; }
    }
}
