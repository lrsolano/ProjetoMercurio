using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Direcao : CommonColumns
    {
        public string Movimento { get; private set; }
        public Direcao(int id) : base("direcao", "IdDirecao")
        {
            if (!base.Exists(id))
            {
                throw new DBConnectionException(string.Format("ID: {0} não encontrado no Banco.", id));
            }

        }

    }
}
