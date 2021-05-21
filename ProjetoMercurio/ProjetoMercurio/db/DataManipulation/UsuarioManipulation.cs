using MercurioCore.db;
using MercurioCore.Model.Exceptions;
using MercurioCore.Model.Interfaces;
using MySqlConnector;
using ProjetoMercurioCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMercurioCore.db.DataManipulation
{
    class UsuarioManipulation<T> : IRepository<T> where T : Usuario
    {
        private DBConnection connection;
        public UsuarioManipulation()
        {
            connection = new DBConnection();
        }
        public T Create(T item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.usuario (Nome, DataCriacao, Idade) VALUES ('{0}', NOW(),{1})", item.Nome, item.Idade);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao Usuario no banco.");
            }
            return FindLastId();
        }
        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.usuario WHERE IdUsuario={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao remover Usuario no banco.");
            }
        }
        public bool Exists(long id)
        {
            string sql = string.Format("SELECT IdUsuario FROM  projetomercurio.usuario WHERE IdUsuario={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<T> FindAll()
        {
            List<T> items = new List<T>();
            string sql = string.Format("SELECT IdUsuario, Nome, DataCriacao, Idade FROM projetomercurio.usuario");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Usuario item = new Usuario((int)result["IdUsuario"], result["Nome"].ToString(), (DateTime)result["DataCriacao"],(int)result["Idade"]);
                    items.Add((T)item);
                }
                result.Close();

                return (items);
            }
            else
            {
                throw new DBConnectionException("Nenhum Usuario encontrado");
            }
        }
        public T FindByID(long id)
        {
            string sql = string.Format("SELECT IdUsuario FROM projetomercurio.usuario WHERE IdUsuario={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Usuario item = new Usuario((int)result["IdUsuario"]);
                result.Close();
                return (T)item;
            }
            else
            {
                throw new DBConnectionException("Nenhum Usuario encontrado");
            }
        }
        public T FindLastId()
        {
            string sql = string.Format("SELECT IdUsuario, Nome, DataCriacao, Idade FROM projetomercurio.usuario order by IdUsuario desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Usuario item = new Usuario((int)result["IdUsuario"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (int)result["Idade"]);
                result.Close();
                return (T)item;
            }
            else
            {
                throw new DBConnectionException("Nenhum Usuario encontrado");
            }
        }
        public T Update(T item)
        {

            string sql = string.Format("UPDATE projetomercurio.usuario SET  Nome = '{0}', Idade = {1} WHERE IdUsuario = {2}", item.Nome, item.Idade, item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar Usuario no banco.");
            }

            return FindByID(item.Id);
        }
        public T FindByName(string nome)
        {
            string sql = string.Format("SELECT IdUsuario FROM projetomercurio.usuario WHERE Nome='{0}' ", nome);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Usuario item = new Usuario((int)result["IdUsuario"]);
                result.Close();
                return (T)item;
            }
            else
            {
                throw new DBConnectionException("Nenhum Usuario encontrado");
            }
        }
    }
}
