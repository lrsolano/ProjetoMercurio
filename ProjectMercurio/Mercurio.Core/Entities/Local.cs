using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Local : CommonColumns, IColumnAtivo
    {
        public bool Ativo { get; set; }
        public string Nome { get; private set; }
        public Sensor Sensor { get; private set; }
        public Local() : base("local", "IdLocal")
        {

        }
    }
}
