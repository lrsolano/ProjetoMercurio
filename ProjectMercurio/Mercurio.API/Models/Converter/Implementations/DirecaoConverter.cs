using Mercurio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class DirecaoConverter : IParser<DirecaoV, Direcao>, IParser<Direcao, DirecaoV>
    {
        public DirecaoV Parser(Direcao origin)
        {
            if (origin == null) return null;
            DirecaoV direcao = new DirecaoV { Movimento = origin.Movimento };
            return direcao;
        }
        public Direcao Parser(DirecaoV origin)
        {
            if (origin == null) return null;
            Direcao direcao = null;
            if (origin.Id != 0)
            {
                direcao = Direcao.FindById(origin.Id);
            }
            else
            {
                direcao = Direcao.FindByName(origin.Movimento);
            }
            
            return direcao;
        }

        public List<DirecaoV> Parser(List<Direcao> origin)
        {
            if (origin.Count == 0) return null;
            List<DirecaoV> direcoes = new List<DirecaoV>();
            foreach (Direcao u in origin)
            {
                direcoes.Add(Parser(u));
            }
            return direcoes;
        }

        public List<Direcao> Parser(List<DirecaoV> origin)
        {
            if (origin.Count == 0) return null;
            List<Direcao> direcoes = new List<Direcao>();
            foreach (DirecaoV u in origin)
            {
                direcoes.Add(Parser(u));
            }
            return direcoes;
        }
    }
}
