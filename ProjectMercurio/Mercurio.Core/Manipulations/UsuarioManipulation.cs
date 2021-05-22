using MySqlConnector;
using System;
using System.Collections.Generic;

namespace Mercurio.Core
{
    class UsuarioManipulation : IRepository<Usuario>
    {
        private DBConnection connection;
        public UsuarioManipulation()
        {
            connection = new DBConnection();
        }
        public Usuario Create(Usuario item)
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

        public List<Usuario> FindAll()
        {
            List<Usuario> items = new List<Usuario>();
            string sql = string.Format("SELECT IdUsuario, Nome, DataCriacao, Idade FROM projetomercurio.usuario");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Usuario item = new Usuario((int)result["IdUsuario"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (int)result["Idade"]);
                    items.Add(item);
                }
                result.Close();

                return (items);
            }
            else
            {
                throw new DBConnectionException("Nenhum Usuario encontrado");
            }
        }

        public Usuario FindByID(long id)
        {
            string sql = string.Format("SELECT IdUsuario FROM projetomercurio.usuario WHERE IdUsuario={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Usuario item = new Usuario((int)result["IdUsuario"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (int)result["Idade"]);
                result.Close();
                return item;
            }
            else
            {
                throw new DBConnectionException("Nenhum Usuario encontrado");
            }
        }

        public Usuario FindLastId()
        {
            string sql = string.Format("SELECT IdUsuario, Nome, DataCriacao, Idade FROM projetomercurio.usuario order by IdUsuario desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Usuario item = new Usuario((int)result["IdUsuario"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (int)result["Idade"]);
                result.Close();
                return item;
            }
            else
            {
                throw new DBConnectionException("Nenhum Usuario encontrado");
            }
        }

        public Usuario Update(Usuario item)
        {
            string sql = string.Format("UPDATE projetomercurio.usuario SET  Nome = '{0}', Idade = {1} WHERE IdUsuario = {2}", item.Nome, item.Idade, item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar Usuario no banco.");
            }

            return FindByID(item.Id);
        }
        public Usuario FindByName(string nome)
        {
            string sql = string.Format("SELECT IdUsuario FROM projetomercurio.usuario WHERE Nome='{0}' ", nome);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Usuario item = new Usuario((int)result["IdUsuario"]);
                result.Close();
                return item;
            }
            else
            {
                throw new DBConnectionException("Nenhum Usuario encontrado");
            }
        }
    }
}
