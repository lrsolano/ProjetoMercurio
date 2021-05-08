using MercurioCore.Model;
using MercurioCore.Model.Exceptions;
using MercurioCore.Model.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.db.DataManipulation
{
    public class PedinteManipulation<T> : IRepository<T> where T : Pedinte
    {
        private DBConnection connection;

        public PedinteManipulation()
        {
            connection = new DBConnection();
        }

        public T Create(T item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.pedinte (Nome, Idade, DataEntrada) VALUES ('{0}', {1}, NOW())", item.Nome, item.Idade);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir pedinte no banco.");
            }

            return FindLastId();


        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.pedinte WHERE IdPedinte={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir pedinte no banco.");
            }
        }

        public bool Exists(long id)
        {
            string sql = string.Format("SELECT IdPedinte FROM  projetomercurio.pedinte WHERE IdPedinte={0} ", id);
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
            string sql = string.Format("SELECT IdPedinte, Nome, Idade, DataEntrada FROM projetomercurio.pedinte");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Pedinte item = new Pedinte((int)result["IdPedinte"], (int)result["Idade"],result["Nome"].ToString(),(DateTime)result["DataEntrada"]);
                    items.Add((T)item);
                }
                result.Close();

                return (items);
            }
            else
            {
                throw new DBConnectionException("Nenhum registro encontrado");
            }



        }

        public T FindByID(long id)
        {
            string sql = string.Format("SELECT IdPedinte, Idade, Nome, DataEntrada FROM  projetomercurio.pedinte WHERE IdPedinte={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Pedinte item = new Pedinte((int)result["IdPedinte"], (int)result["Idade"], result["Nome"].ToString(), (DateTime)result["DataEntrada"]);
                result.Close();
                return (T)item;
            }
            else
            {
                throw new DBConnectionException("Nenhum registro encontrado");
            }
        }

        public T Update(T item)
        {
            string sql = string.Format("UPDATE projetomercurio.pedinte SET Nome = '{0}', Idade = {1}, DataEntrada = '{2}' WHERE IdItem = {3}", item.Nome, item.Idade, item.DataEntrada.ToString("yyyy-MM-dd HH:mm:ss"), item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }

            return FindByID(item.Id);
        }

        public T FindLastId()
        {
            string sql = string.Format("SELECT IdPedinte, Idade, Nome, DataEntrada FROM projetomercurio.pedinte order by IdPedinte desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Pedinte item = new Pedinte((int)result["IdPedinte"], (int)result["Idade"], result["Nome"].ToString(), (DateTime)result["DataEntrada"]);
                result.Close();
                return (T)item;
            }
            else
            {
                throw new DBConnectionException("Nenhum registro encontrado");
            }
        }
    }
}
