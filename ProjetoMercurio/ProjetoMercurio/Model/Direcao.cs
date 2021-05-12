using ProjetoMercurio.db.DataManipulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurio.Model
{
    public class Direcao
    {
        public int Id { get; private set; }
        public string Movimento { get; private set; }

        public Direcao(int idDirecao, string movimento)
        {
            Id= idDirecao;
            Movimento = movimento;
        }
        public Direcao(int id)
        {
            DirecaoManipulation<Direcao> item = new DirecaoManipulation<Direcao>();
            Direcao i = item.FindByID(id);
            Id = id;
            Movimento = i.Movimento;
        }

        public void ChangeItem(int id)
        {
            DirecaoManipulation<Direcao> item = new DirecaoManipulation<Direcao>();
            Direcao i = item.FindByID(id);
            Id = id;
            Movimento = i.Movimento;
        }
    }
}
