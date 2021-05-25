using System;
using System.Collections.Generic;
using System.Text;

namespace Mercurio.Core
{
    public class MercurioCoreException : Exception
    {
        public MercurioCoreException()
        {

        }

        public MercurioCoreException(string message)
        : base(String.Format("Mercurio Core Error: {0}", message))
        {
            LogActivity log = new LogActivity(LogNivel.Erro, "MercurioCoreException");
            log.Write(LogNivel.Erro, message);
        }
    }
}
