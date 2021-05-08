using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.Model.Exceptions
{
    public class MercurioCoreException : Exception
    {
        public MercurioCoreException()
        {

        }

        public MercurioCoreException(string message)
        : base(String.Format("Mercurio Core Error: {0}", message))
        {

        }
    }
}
