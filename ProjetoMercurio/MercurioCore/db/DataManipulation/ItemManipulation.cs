using MercurioCore.Model;
using MercurioCore.Model.Exceptions;
using MercurioCore.Model.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.db.DataManipulation
{
    public class ItemManipulation<T> : IRepository<T> where T : Item
    {
        private DBConnection connection;

        public ItemManipulation()
        {
            connection = new DBConnection();
        }

        public T Create(T item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.item (Tipo, Nome, Quantidade) VALUES ('{0}', '{1}', {2})", item.Tipo, item.Nome, item.Quantidade);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }

            return FindLastId();
        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.item WHERE IdItem={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }
        }

        public bool Exists(long id)
        {
            string sql = string.Format("SELECT IdItem FROM  projetomercurio.item WHERE IdItem={0} ", id);
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
            string sql = string.Format("SELECT IdItem, Tipo, Nome, Quantidade FROM projetomercurio.item");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Item item = new Item((int)result["IdItem"],result["Nome"].ToString(),result["Tipo"].ToString(),(int)result["Quantidade"]);
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
            string sql = string.Format("SELECT IdItem, Nome, Tipo, Quantidade FROM  projetomercurio.item WHERE IdItem={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Item item = new Item((int)result["IdItem"], result["Nome"].ToString(), result["Tipo"].ToString(), (int)result["Quantidade"]);
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
            string sql = string.Format("UPDATE projetomercurio.item SET Tipo = '{0}', Nome = '{1}', Quantidade = {2} WHERE IdItem = {3}", item.Tipo, item.Nome, item.Quantidade, item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }

            return FindByID(item.Id);
        }

        public T FindLastId()
        {
            string sql = string.Format("SELECT IdItem, Tipo, Nome, Quantidade FROM projetomercurio.item order by IdItem desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Item item = new Item((int)result["IdItem"], result["Nome"].ToString(), result["Tipo"].ToString(), (int)result["Quantidade"]);
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
