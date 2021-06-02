using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Grupo
    {
        public bool Salvo { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }

        internal Grupo(int id, string nome) 
        {
            Nome = nome;
            Id = id;
            Salvo = false;
        }


        public static Grupo FindByName(string nome)
        {
            GrupoManipulation item = new GrupoManipulation();
            Grupo i = item.FindByName(nome);
            return i;
        }
        public static List<Grupo> FindAll()
        {
            GrupoManipulation item = new GrupoManipulation();
            var i = item.FindAll();
            return i;
        }
        public static List<Grupo> FindAll(int idUsuario)
        {
            GrupoManipulation item = new GrupoManipulation();
            var i = item.FindAll(idUsuario);
            return i;
        }
        public static Grupo FindById(int id)
        {
            GrupoManipulation item = new GrupoManipulation();
            Grupo i = item.FindByID(id);
            return i;
        }
        internal void RemoveGrupo(int idUsuario)
        {
            GrupoManipulation item = new GrupoManipulation();
            item.RemoveGrupo(idUsuario,Id);
        }
        internal void AddGrupo(int idUsuario)
        {
            GrupoManipulation item = new GrupoManipulation();
            item.AddGrupo(idUsuario, Id);
        }
        public override bool Equals(object obj)
        {
            var item = obj as Grupo;

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
