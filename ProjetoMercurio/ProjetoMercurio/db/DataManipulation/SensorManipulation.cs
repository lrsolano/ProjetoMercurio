using MercurioCore.db;
using MercurioCore.Model.Exceptions;
using MercurioCore.Model.Interfaces;
using MySqlConnector;
using ProjetoMercurio.Model;
using ProjetoMercurio.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurio.db.DataManipulation
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
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota FROM  projetomercurio.sensor WHERE IdSensor={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                Sensor sensor;
                int idAnterior = 0;
                if(!Int32.TryParse(result["IdSensorAnterior"].ToString(),out idAnterior))
                {
                    sensor = null;
                }
                else
                {
                    sensor = FindByID((idAnterior));
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
                result.Read();
                Sensor item = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"],sensor,direcao,rota);
                result.Close();
                return (T)item;
            }
            else
            {
                throw new DBConnectionException("Nenhum registro encontrado");
            }
            throw new NotImplementedException();
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
