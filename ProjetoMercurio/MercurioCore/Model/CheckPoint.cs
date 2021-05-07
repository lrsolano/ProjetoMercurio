using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.Model
{
    public class CheckPoint
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public List<Rua> Ruas { get; private set; }
        public List<CheckPoint> NextCheckPoints { get; private set; }

        public CheckPoint(int id, string nome, List<Rua> ruas, List<CheckPoint> nextCheckPoints)
        {
            Id = id;
            Nome = nome;
            Ruas = ruas;
            NextCheckPoints = nextCheckPoints;
        }
    }
}
