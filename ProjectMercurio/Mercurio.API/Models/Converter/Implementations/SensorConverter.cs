using Mercurio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class SensorConverter : IParser<SensorV, Sensor>, IParser<Sensor, SensorV>
    {
        public SensorV Parser(Sensor origin)
        {
            DirecaoConverter direcaoConverter = new DirecaoConverter();
            if (origin == null) return null;
            SensorV sensor = new SensorV(origin.Id,origin.Nome ,origin.Inicial, origin.SensorAnterior, direcaoConverter.Parser(origin.Direcao), origin.DirecaoRota);
            return sensor;
        }
        public Sensor Parser(SensorV origin)
        {
            if (origin == null) return null;
            Sensor sensor = Sensor.FindById(origin.Id);
            if (sensor == null) return null;
            return sensor;
        }

        public List<SensorV> Parser(List<Sensor> origin)
        {
            if (origin.Count == 0) return null;
            List<SensorV> sensores = new List<SensorV>();
            foreach (Sensor u in origin)
            {
                sensores.Add(Parser(u));
            }
            return sensores;
        }

        public List<Sensor> Parser(List<SensorV> origin)
        {
            if (origin.Count == 0) return null;
            List<Sensor> sensores = new List<Sensor>();
            foreach (SensorV u in origin)
            {
                sensores.Add(Parser(u));
            }
            return sensores;
        }
    }
}
