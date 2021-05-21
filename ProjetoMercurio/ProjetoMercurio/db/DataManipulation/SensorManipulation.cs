using MercurioCore.db;
using MercurioCore.Model.Exceptions;
using MercurioCore.Model.Interfaces;
using MySqlConnector;
using ProjetoMercurioCore.Model;
using ProjetoMercurioCore.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurioCore.db.DataManipulation
{
    class SensorManipulation<T> : IRepository<T> where T : Sensor
    {
        private DBConnection connection;
        public SensorManipulation()
        {
            connection = new DBConnection();
        }
        public T Create(T item)
        {
            string direcaoRota = string.Empty;

            switch (item.DirecaoRota)
            {
                case DirecaoRota.Ida:
                    direcaoRota = "Ida";
                    break;
                case DirecaoRota.Volta:
                    direcaoRota = "Volta";
                    break;

            }
            string sql = string.Empty;
            if(item.SensorAnterior == null)
            {
                sql = string.Format("INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota) VALUES ('{0}',now(),{1},{2},'{3}');", item.Nome, item.Inicial, item.Direcao.Id, direcaoRota);
            }
            else
            {
                sql = string.Format("INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota, IdSensorAnterior) VALUES ('{0}',now(),{1},{2},'{3}',{4});", item.Nome, item.Inicial, item.Direcao.Id, direcaoRota, item.SensorAnterior.Id);
            }
            

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir Sensor no banco.");
            }

            return FindLastId();
        }
        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.sensor WHERE IdSensor={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao remover sensor no banco.");
            }
        }
        public bool Exists(long id)
        {
            string sql = string.Format("SELECT IdSensor FROM  projetomercurio.sensor WHERE IdSensor={0} ", id);
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
            string sql = string.Format("SELECT IdSensor FROM projetomercurio.sensor");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Sensor sensor = new Sensor((int)result["IdSensor"]);
                    items.Add((T)sensor);
                }
                result.Close();

                return (items);
            }
            else
            {
                throw new DBConnectionException("Nenhum Sensor encontrado");
            }
        }
        public T FindByID(long id)
        {
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota FROM  projetomercurio.sensor WHERE IdSensor={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Sensor sensor;
                int idAnterior = 0;
                if(!Int32.TryParse(result["IdSensorAnterior"].ToString(),out idAnterior))
                {
                    sensor = null;
                }
                else
                {
                    sensor = new Sensor((int)result["IdSensorAnterior"]);
                }

                Direcao direcao = new Direcao((int)result["IdDirecao"]);
                DirecaoRota rota = DirecaoRota.Ida;
                switch (result["DirecaoRota"].ToString())
                {
                    case "Ida":
                        rota = DirecaoRota.Ida;
                        break;
                    case "Volta":
                        rota = DirecaoRota.Volta;
                        break;

                }
                
                Sensor item = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"],sensor,direcao,rota);
                result.Close();
                return (T)item;
            }
            else
            {
                throw new DBConnectionException("Nenhum Sensor encontrado");
            }
            throw new NotImplementedException();
        }
        public T FindLastId()
        {
            string sql = string.Format("SELECT IdSensor FROM  projetomercurio.sensor order by IdSensor desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Sensor item = new Sensor((int)result["IdSensor"]);
                result.Close();
                return (T)item;
            }
            else
            {
                throw new DBConnectionException("Nenhum Sensor encontrado");
            }
        }
        public T Update(T item)
        {
            string direcaoRota = string.Empty;
            switch (item.DirecaoRota)
            {
                case DirecaoRota.Ida:
                    direcaoRota = "Ida";
                    break;
                case DirecaoRota.Volta:
                    direcaoRota = "Volta";
                    break;

            }

            string sql = string.Format("UPDATE projetomercurio.sensor SET  Nome = '{0}', Inicial = {1}, IdDirecao = {2}, DirecaoRota = '{3}'WHERE IdSensor = {4}", item.Nome, item.Inicial, item.Direcao.Id, direcaoRota);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar Sensor no banco.");
            }

            return FindByID(item.Id);
        }
        public T FindByName(string nome)
        {
            Sensor sensor = null;
            string sql = string.Format("SELECT IdSensor FROM projetomercurio.sensor WHERE Nome = '{0}'", nome);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    sensor = new Sensor((int)result["IdSensor"]);
                }
                result.Close();

                return ((T)sensor);
            }
            else
            {
                throw new DBConnectionException("Nenhum Sensor encontrado");
            }
        }
    }
}
