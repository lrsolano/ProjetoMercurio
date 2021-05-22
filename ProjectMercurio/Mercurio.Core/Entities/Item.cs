using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Item : CommonColumns, IColumnAtivo
    {
        internal int IdPedidoxItem { get; set; }
        public string Nome { get; private set; }
        internal int Quantidade { get; set; }
        public bool Ativo { get; set ; }
        public Item() : base("item", "IdItem")
        {

        }
    }
}
