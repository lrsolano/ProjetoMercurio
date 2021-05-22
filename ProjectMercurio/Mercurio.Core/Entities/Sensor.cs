using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Sensor : CommonColumns, IColumnAtivo
    {
        public string Nome { get; private set; }
        public Boolean Inicial { get; private set; }
        public Sensor SensorAnterior { get; set; }
        public Direcao Direcao { get; set; }
        public DirecaoRota DirecaoRota { get; private set; }
        public bool Ativo { get; set; }
        public Sensor() : base("sensor", "IdSensor")
        {

        }
    }
}
