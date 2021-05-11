using MercurioCore.db;
using MercurioCore.Model.Exceptions;
using MercurioCore.Model.Interfaces;
using MySqlConnector;
using ProjetoMercurio.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurio.db.DataManipulation
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
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public T FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public T FindLastId()
        {
            string sql = string.Format("SELECT IdItem, Nome, DataCriacao FROM projetomercurio.item order by IdItem desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Item item = new Item((int)result["IdItem"], result["Nome"].ToString(), (DateTime)result["DataCriacao"]);
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
            throw new NotImplementedException();
        }
    }
}
