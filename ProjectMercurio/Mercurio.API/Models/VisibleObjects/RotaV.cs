using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class RotaV
    {
        public long Id { get; set; }
        public SensorV SensorInicial { get; set; }
        public SensorV SensorFinal { get; set; }
        public string Tracado { get; set; }
    }
}
