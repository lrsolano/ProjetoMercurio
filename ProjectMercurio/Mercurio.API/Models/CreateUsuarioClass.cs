using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class CreateUsuarioClass
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Senha { get; set; }
        public List<GrupoV> Grupos { get; set; }
    }
}
