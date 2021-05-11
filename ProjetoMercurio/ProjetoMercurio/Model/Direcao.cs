using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurio.Model
{
    public class Direcao
    {
        public int IdDirecao { get; private set; }
        public string Movimento { get; private set; }

        public Direcao(int idDirecao, string movimento)
        {
            IdDirecao = idDirecao;
            Movimento = movimento;
        }
    }
}
