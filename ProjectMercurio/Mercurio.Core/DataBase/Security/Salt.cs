using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    class Salt
    {
        internal static byte[] GetSalt()
        {
            string salt = "dEf!!=m:ckE8";
            byte[] bytesSalt = Encoding.ASCII.GetBytes(salt);
            return bytesSalt;
        }

    }
}
