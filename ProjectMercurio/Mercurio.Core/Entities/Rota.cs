using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Rota : CommonColumns
    {
        public Sensor SensorInicial { get; private set; }
        public Sensor SensorFinal { get; set; }
        public string Tracado { get; set; }
        public Rota() : base("rota", "IdRota")
        {

        }
    }
}
