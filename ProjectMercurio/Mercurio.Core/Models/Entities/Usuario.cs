﻿using System;
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
        internal string Senha { get; private set; }
        internal Usuario(int id, string nome, DateTime dataCriacao, int idade, string senha) : base("usuario", "IdUsuario")
        {
            base.Id = id;
            base.DataCriacao = dataCriacao;
            Nome = nome;
            DataCriacao = dataCriacao;
            Idade = idade;
            Senha = senha;

        }
        public Usuario(string nome, int idade) : base("usuario", "IdUsuario")
        {
            Nome = nome;
            Idade = idade;
            Senha = string.Empty;
        }
        internal Usuario(int id) : base("usuario", "IdUsuario")
        {
            if (base.Exists(id))
            {
                UsuarioManipulation item = new UsuarioManipulation();
                Usuario i = item.FindByID(id);
                base.Id = id;
                Nome = i.Nome;
                base.DataCriacao = i.DataCriacao;
                Idade = i.Idade;
                Senha = i.Senha;
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
        public static Usuario FindById(long id)
        {
            UsuarioManipulation item = new UsuarioManipulation();
            Usuario i = item.FindByID(id);
            return i;
        }
        public void CreateUsuario()
        {
            if (string.IsNullOrEmpty(Senha))
            {
                throw new MercurioCoreException("Defina a Senha");
            }
            if (base.Id != 0)
            {
                throw new MercurioCoreException("Usuario já criado no Banco de Dados");
            }
            UsuarioManipulation item = new UsuarioManipulation();
            if (item.FindByName(Nome) != null)
            {
                throw new MercurioCoreException("Usuario já criado no Banco de Dados");
            }
            Usuario novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateUsuario()
        {
            if (string.IsNullOrEmpty(Senha))
            {
                throw new MercurioCoreException("Defina a Senha");
            }
            UsuarioManipulation item = new UsuarioManipulation();

            if (item.FindByName(Nome) == null)
            {
                throw new MercurioCoreException("Sensor já criado no Banco de Dados");
            }
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
        public void AddSenha(string senha)
        {
            string password = Password.ComputeHash(senha, "SHA256");
            Senha = password;
        }
        public void ChangePassword(PasswordChange passwordChange) 
        {
            string oldPassword = Password.ComputeHash(passwordChange.SenhaAntiga, "SHA256");
            if(oldPassword != Senha)
            {
                throw new MercurioCoreException("Senha antiga inválida");
            }
            AddSenha(passwordChange.NovaSenha);
            UpdateUsuario();
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
