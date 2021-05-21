using ProjetoMercurioCore.db.DataManipulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurioCore.Model
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
        public static Direcao FindByName(string nome)
        {
            DirecaoManipulation<Direcao> item = new DirecaoManipulation<Direcao>();
            Direcao i = item.FindByName(nome);
            return i;
        }
        public static List<Direcao> FindAll()
        {
            DirecaoManipulation<Direcao> item = new DirecaoManipulation<Direcao>();
            List<Direcao> i = item.FindAll();
            return i;
        }
        public override bool Equals(object obj)
        {
            var item = obj as Direcao;

            if (item == null)
            {
                return false;
            }

            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
