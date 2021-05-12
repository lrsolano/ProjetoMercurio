using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.Model.Exceptions
{
    public class DBConnectionException : Exception
    {
        public DBConnectionException()
        {

        }

        public DBConnectionException(string message)
        : base(String.Format("DataBase Error: {0}", message))
        {

        }
    }
}
