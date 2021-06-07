using Mercurio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class RotaConverter : IParser<RotaV, Rota>, IParser<Rota, RotaV>
    {
        public RotaV Parser(Rota origin)
        {
            SensorConverter sensorConverter = new SensorConverter();
            if (origin == null) return null;
            RotaV rota = new RotaV
            {
                SensorInicial = sensorConverter.Parser(origin.SensorInicial),
                SensorFinal = sensorConverter.Parser(origin.SensorFinal),
                Tracado = origin.Tracado,
                Id = origin.Id
            };
            return rota;
        }
        public Rota Parser(RotaV origin)
        {
            if (origin == null) return null;
            Rota rota = Rota.FindById(origin.Id);
            return rota;
        }

        public List<RotaV> Parser(List<Rota> origin)
        {
            if (origin.Count == 0) return null;
            List<RotaV> rotas = new List<RotaV>();
            foreach (Rota u in origin)
            {
                rotas.Add(Parser(u));
            }
            return rotas;
        }

        public List<Rota> Parser(List<RotaV> origin)
        {
            if (origin.Count == 0) return null;
            List<Rota> rotas = new List<Rota>();
            foreach (RotaV u in origin)
            {
                rotas.Add(Parser(u));
            }
            return rotas;
        }
    }
}
