using MercurioCore.db;
using MercurioCore.Model.Exceptions;
using MercurioCore.Model.Interfaces;
using MySqlConnector;
using ProjetoMercurioCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurioCore.db.DataManipulation
{
    class DirecaoManipulation<T> : IRepository<T> where T : Direcao
    {
        private DBConnection connection;
        public DirecaoManipulation()
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
            string sql = string.Format("SELECT IdDirecao FROM  projetomercurio.direcao WHERE IdDirecao={0} ", id);
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
            string sql = string.Format("SELECT IdDirecao, Movimento FROM  projetomercurio.direcao");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Direcao item = new Direcao((int)result["IdDirecao"], result["Movimento"].ToString());
                    items.Add((T)item);
                }
                result.Close();

            }
            return items;

        }
        public T FindByID(long id)
        {
            Direcao item = null;
            string sql = string.Format("SELECT IdDirecao, Movimento FROM  projetomercurio.direcao WHERE IdDirecao={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                item = new Direcao((int)result["IdDirecao"], result["Movimento"].ToString());
                result.Close();
            }
            return (T)item;
        }
        public T FindLastId()
        {
            throw new NotImplementedException();
        }
        public T Update(T item)
        {
            throw new NotImplementedException();
        }
        public T FindByName(string movimento)
        {
            Direcao item = null;
            string sql = string.Format("SELECT IdDirecao, Movimento FROM  projetomercurio.direcao WHERE Movimento='{0}' ", movimento);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                item = new Direcao((int)result["IdDirecao"], result["Movimento"].ToString());
                result.Close();
                
            }
            return (T)item;
        }
    }
}
