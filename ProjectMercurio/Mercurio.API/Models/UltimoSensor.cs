using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class UltimoSensor
    {
        public string SensorAtual { get; set; }
        public string ProximoSensor { get; internal set; }
        public string Movimento { get; internal set; }
    }
}
