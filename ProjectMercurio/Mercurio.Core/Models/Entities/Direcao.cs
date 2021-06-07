using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Direcao : CommonColumns
    {
        public string Movimento { get; set; }
        internal Direcao(int id) : base("direcao", "IdDirecao")
        {
            if (base.Exists(id))
            {
                DirecaoManipulation item = new DirecaoManipulation();

                Direcao i = item.FindByID(id);
                Id = id;
                Movimento = i.Movimento;
            }

        }
        internal Direcao(int idDirecao, string movimento) : base("direcao", "IdDirecao")
        {
            Id = idDirecao;
            Movimento = movimento;
        }

        public void ChangeItem(int id)
        {
            DirecaoManipulation item = new DirecaoManipulation();
            Direcao i = item.FindByID(id);
            Id = id;
            Movimento = i.Movimento;
        }
        public static Direcao FindByName(string nome)
        {
            DirecaoManipulation item = new DirecaoManipulation();
            Direcao i = item.FindByName(nome);
            return i;
        }
        public static List<Direcao> FindAll()
        {
            DirecaoManipulation item = new DirecaoManipulation();
            List<Direcao> i = item.FindAll();
            return i;
        }
        public static Direcao FindById(long id)
        {
            DirecaoManipulation item = new DirecaoManipulation();
            Direcao i = item.FindByID(id);
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
