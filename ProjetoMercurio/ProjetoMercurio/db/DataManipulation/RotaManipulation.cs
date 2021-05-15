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
    class RotaManipulation<T> : IRepository<T> where T : Rota
    {
        private DBConnection connection;

        public RotaManipulation()
        {
            connection = new DBConnection();
        }
        public T Create(T item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.rota (IdSensorInicial, IdSensorFinal, Rota) VALUES ({0}, {1}, '{2}')", item.SensorInicial.Id, item.SensorFinal.Id, item.Tracado);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }

            return FindLastId();
        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.rota WHERE IdRota={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }
        }

        public bool Exists(long id)
        {
            string sql = string.Format("SELECT IdItem FROM  projetomercurio.rota WHERE IdRota={0} ", id);
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
            string sql = string.Format("SELECT IdRota FROM projetomercurio.rota");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Rota rota = new Rota((int)result["IdRota"]);
                    items.Add((T)rota);
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
            string sql = string.Format("SELECT IdRota, IdSensorInicial, IdSensorFinal, Rota FROM projetomercurio.rota");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Sensor inicial = new Sensor((int)result["IdSensorInicial"]);
                Sensor final = new Sensor((int)result["IdSensorFinal"]);
                string strRota = result["Rota"].ToString();
                Rota rota = new Rota((int)result["IdRota"],inicial,final,strRota);
                result.Close();
                return (T)rota;
            }
            else
            {
                throw new DBConnectionException("Nenhum registro encontrado");
            }
        }

        public T FindLastId()
        {
            string sql = string.Format("SELECT IdRota FROM  projetomercurio.rota order by IdRota desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Rota item = new Rota((int)result["IdRota"]);
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
            string sql = string.Format("UPDATE projetomercurio.rota SET  IdSensorInicial = {0}, IdSensorFinal = {1}, Rota = '{3}' WHERE IdRota = {4}", item.SensorInicial.Id, item.SensorFinal.Id, item.Tracado);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }

            return FindByID(item.Id);
        }

        public int RotaExist(T item)
        {
            string sql = string.Format("SELECT IdRota FROM  projetomercurio.rota WHERE IdSensorInicial={0} and  IdSensorFinal={1}",item.SensorInicial.Id, item.SensorFinal.Id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                int id = (int)result["IdRota"];
                result.Close();
                return id;
            }
            else
            {
                return 0;
            }
        }
    }
}
