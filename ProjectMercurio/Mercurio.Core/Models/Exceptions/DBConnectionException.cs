using System;
using System.Collections.Generic;
using System.Text;

namespace Mercurio.Core
{
    public class DBConnectionException : Exception
    {
        public DBConnectionException()
        {

        }

        public DBConnectionException(string message)
        : base(String.Format("DataBase Error: {0}", message))
        {
            LogActivity log = new LogActivity(LogNivel.Erro, "DBConnectionException");
            log.Write(LogNivel.Erro, message);
        }
    }
}
