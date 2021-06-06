using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class ErrorClass
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }

        public ErrorClass(int code, string message, DateTime time)
        {
            Code = code;
            Message = message;
            Time = time;
        }
    }
}
