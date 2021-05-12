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
            throw new NotImplementedException();
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

                return (items);
            }
            else
            {
                throw new DBConnectionException("Nenhum registro encontrado");
            }
        }

        public T FindByID(long id)
        {
            string sql = string.Format("SELECT IdDirecao, Movimento FROM  projetomercurio.direcao WHERE IdItem={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Direcao item = new Direcao((int)result["IdDirecao"], result["Movimento"].ToString());
                result.Close();
                return (T)item;
            }
            else
            {
                throw new DBConnectionException("Nenhum registro encontrado");
            }
        }

        public T FindLastId()
        {
            throw new NotImplementedException();
        }

        public T Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
