using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class TokenClass
    {
        public string Token { get; set; }
        public long ExpireIn { get; set; }

        public TokenClass(string token, double expireIn)
        {
            Token = token;
            ExpireIn = Convert.ToInt64(expireIn);
        }
    }
}
