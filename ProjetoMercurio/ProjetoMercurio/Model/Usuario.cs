using MercurioCore.Model.Exceptions;
using ProjetoMercurioCore.db.DataManipulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurioCore.Model
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public int Idade { get; private set; }
        public Usuario(int id, string nome, DateTime dataCriacao, int idade)
        {
            Id = id;
            Nome = nome;
            DataCriacao = dataCriacao;
            Idade = idade;
        }
        public Usuario(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }
        public Usuario(int id)
        {
            Id = id;
        }

        public void CreateUsuario()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Objeto já criado no Banco de Dados");
            }
            UsuarioManipulation<Usuario> item = new UsuarioManipulation<Usuario>();

            Usuario novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateUsuario()
        {
            UsuarioManipulation<Usuario> item = new UsuarioManipulation<Usuario>();

            item.Update(this);

        }
        public void DeleteUsuario()
        {
            UsuarioManipulation<Usuario> item = new UsuarioManipulation<Usuario>();

            item.Delete(this.Id);
        }
        public void ChangeUsuario(int id)
        {
            UsuarioManipulation<Usuario> item = new UsuarioManipulation<Usuario>();
            Usuario i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            DataCriacao = i.DataCriacao;
            Idade = i.Idade;
        }
    }
}
