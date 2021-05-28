using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Usuario : CommonColumns, IColumnAtivo
    {

        public string Nome { get; private set; }
        public int Idade { get; private set; }
        public bool Ativo { get ; set ; }
        public Usuario(int id, string nome, DateTime dataCriacao, int idade) : base("usuario", "IdUsuario")
        {
            base.Id = id;
            base.DataCriacao = dataCriacao;
            Nome = nome;
            DataCriacao = dataCriacao;
            Idade = idade;

        }
        public Usuario(string nome, int idade) : base("usuario", "IdUsuario")
        {
            Nome = nome;
            Idade = idade;
        }
        public Usuario(int id) : base("usuario", "IdUsuario")
        {
            if (base.Exists(id))
            {
                UsuarioManipulation item = new UsuarioManipulation();
                Usuario i = item.FindByID(id);
                base.Id = id;
                Nome = i.Nome;
                base.DataCriacao = i.DataCriacao;
                Idade = i.Idade;
            }
        }
        public static Usuario FindByName(string nome)
        {
            UsuarioManipulation item = new UsuarioManipulation();
            Usuario i = item.FindByName(nome);
            return i;
        }
        public static List<Usuario> FindAll()
        {
            UsuarioManipulation item = new UsuarioManipulation();
            List<Usuario> i = item.FindAll();
            return i;
        }
        public void CreateUsuario()
        {
            if (base.Id != 0)
            {
                throw new MercurioCoreException("Usuario já criado no Banco de Dados");
            }
            UsuarioManipulation item = new UsuarioManipulation();

            Usuario novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateUsuario()
        {
            UsuarioManipulation item = new UsuarioManipulation();

            item.Update(this);

        }
        public void DeleteUsuario()
        {
            UsuarioManipulation item = new UsuarioManipulation();
            if (item.CanDelete(Id))
            {
                item.Delete(this.Id);
            }
            else
            {
                throw new MercurioCoreException("Usuario em uso");
            }
            
        }
        public void ChangeUsuario(int id)
        {
            UsuarioManipulation item = new UsuarioManipulation();
            Usuario i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            DataCriacao = i.DataCriacao;
            Idade = i.Idade;
        }
        public override bool Equals(object obj)
        {
            var item = obj as Usuario;

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
