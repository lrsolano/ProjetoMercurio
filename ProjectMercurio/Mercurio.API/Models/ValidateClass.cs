using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class ValidateClass
    {
        public bool Autenticado { get; set; }
        public string Usuario { get; set; }


        public ValidateClass(bool autenticado, string usuario)
        {
            Autenticado = autenticado;
            Usuario = usuario;
        }
    }
}
