using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Pedido : CommonColumns, IColumnAtivo
    {
        public Usuario Usuario { get; private set; }
        public Rota Rota { get; private set; }
        public List<Item> Items { get; set; }
        public bool Ativo { get; set; }
        public Pedido() : base("pedido", "IdPedido")
        {

        }
    }
}
