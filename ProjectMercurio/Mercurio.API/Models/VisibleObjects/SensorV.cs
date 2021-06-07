using Mercurio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class SensorV
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public Boolean Inicial { get; set; }
        public int SensorAnterior { get; set; }
        public DirecaoV Direcao { get; set; }
        public DirecaoRota DirecaoRota { get; set; }

        public SensorV(long id, string nome, bool inicial, int sensorAnterior, DirecaoV direcao, DirecaoRota direcaoRota)
        {
            Id = id;
            Nome = nome;
            Inicial = inicial;
            SensorAnterior = sensorAnterior;
            Direcao = direcao;
            DirecaoRota = direcaoRota;
        }
    }
}
