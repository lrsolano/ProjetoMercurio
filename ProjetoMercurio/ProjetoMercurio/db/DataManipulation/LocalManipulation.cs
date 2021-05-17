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
    class LocalManipulation<T> : IRepository<T> where T : Local
    {
        private DBConnection connection;

        public LocalManipulation()
        {
            connection = new DBConnection();
        }
        public T Create(T item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.local (Nome, DataCriacao, IdSensor) VALUES ('{0}', NOW(),{1})", item.Nome,item.Sensor.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }

            return FindLastId();
        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.local WHERE IdLocal={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }
        }

        public bool Exists(long id)
        {
            string sql = string.Format("SELECT IdLocal FROM  projetomercurio.local WHERE IdLocal={0} ", id);
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
            string sql = string.Format("SELECT IdLocal, Nome, DataCriacao, IdSensor FROM projetomercurio.local");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Sensor sensor = new Sensor((int)result["IdSensor"]);
                    Local item = new Local((int)result["IdLocal"], result["Nome"].ToString(), (DateTime)result["DataCriacao"],sensor);
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
            string sql = string.Format("SELECT IdLocal, Nome, DataCriacao, IdSensor FROM projetomercurio.local WHERE IdLocal={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Sensor sensor = new Sensor((int)result["IdSensor"]);
                Local item = new Local((int)result["IdLocal"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], sensor);
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
            string sql = string.Format("SELECT IdLocal, Nome, DataCriacao, IdSensor FROM projetomercurio.local order by IdLocal desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Local item = new Local((int)result["IdLocal"]);
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
            string sql = string.Format("UPDATE projetomercurio.Local SET  Nome = '{0}', IdSensor = {1} WHERE IdSensor = {2}", item.Nome, item.Sensor.Id, item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }

            return FindByID(item.Id);
        }
    }
}
