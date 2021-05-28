using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    class RotaManipulation : IRepository<Rota>
    {
        private DBConnection connection;
        public RotaManipulation()
        {
            connection = new DBConnection();
        }

        public Rota Create(Rota item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.rota (IdSensorInicial, IdSensorFinal, Rota, DataCriacao) VALUES ({0}, {1}, '{2}', NOW())", item.SensorInicial.Id, item.SensorFinal.Id, item.Tracado);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir Rota no banco.");
            }

            return FindLastId();
        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.rota WHERE IdRota={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao remover Rota no banco.");
            }
        }

        public List<Rota> FindAll()
        {
            List<Rota> items = new List<Rota>();
            string sql = string.Format("SELECT IdRota FROM projetomercurio.rota");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Sensor inicial = new Sensor((int)result["IdSensorInicial"]);
                    Sensor final = new Sensor((int)result["IdSensorFinal"]);
                    string strRota = result["Rota"].ToString();
                    Rota rota = new Rota((int)result["IdRota"], inicial, final, strRota);
                    items.Add(rota);
                }
                

                
            }
            result.Close();
            return items;

        }

        public Rota FindByID(long id)
        {
            string sql = string.Format("SELECT IdRota, IdSensorInicial, IdSensorFinal, Rota FROM projetomercurio.rota");
            MySqlDataReader result = connection.SendQuery(sql);
            Rota rota = null;
            if (result.HasRows)
            {
                result.Read();
                Sensor inicial = new Sensor((int)result["IdSensorInicial"]);
                Sensor final = new Sensor((int)result["IdSensorFinal"]);
                string strRota = result["Rota"].ToString();
                rota = new Rota((int)result["IdRota"], inicial, final, strRota);
                
                
            }
            result.Close();
            return rota;
        }

        public Rota FindLastId()
        {
            string sql = string.Format("SELECT IdRota, IdSensorInicial, IdSensorFinal, Rota FROM  projetomercurio.rota order by IdRota desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            Rota item = null;
            if (result.HasRows)
            {
                result.Read();
                Sensor inicial = new Sensor((int)result["IdSensorInicial"]);
                Sensor final = new Sensor((int)result["IdSensorFinal"]);
                string strRota = result["Rota"].ToString();
                item = new Rota((int)result["IdRota"], inicial, final, strRota);
                
                
            }
            result.Close();
            return item;
        }

        public Rota Update(Rota item)
        {
            string sql = string.Format("UPDATE projetomercurio.rota SET  IdSensorInicial = {0}, IdSensorFinal = {1}, Rota = '{3}' WHERE IdRota = {4}", item.SensorInicial.Id, item.SensorFinal.Id, item.Tracado);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar Rota no banco.");
            }

            return FindByID(item.Id);
        }
        public int RotaExist(Rota item)
        {
            int idRetorno = 0;
            string sql = string.Format("SELECT IdRota FROM  projetomercurio.rota WHERE IdSensorInicial={0} and  IdSensorFinal={1}", item.SensorInicial.Id, item.SensorFinal.Id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                idRetorno = (int)result["IdRota"];
                

            }
            result.Close();
            return idRetorno;

        }
        public bool CanDelete(long id)
        {
            bool resultado;

            string sql = string.Format(@"SELECT IdRota FROM projetomercurio.pedido
                                            WHERE IdRota = {0};", id);
            MySqlDataReader result = connection.SendQuery(sql);

            if (result.HasRows)
            {
                result.Close();
                resultado = false;
            }
            else
            {
                result.Close();
                resultado = true;
            }

            return resultado;
        }
    }
}
