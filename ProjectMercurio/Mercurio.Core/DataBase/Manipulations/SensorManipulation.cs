using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    class SensorManipulation : IRepository<Sensor>
    {
        private DBConnection connection;
        public SensorManipulation()
        {
            connection = new DBConnection();
        }
        public Sensor Create(Sensor item)
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
            if (item.SensorAnterior == null)
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

        public List<Sensor> FindAll()
        {
            List<Sensor> items = new List<Sensor>();
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota FROM projetomercurio.sensor");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Sensor sensor;
                    int idAnterior = 0;
                    if (!Int32.TryParse(result["IdSensorAnterior"].ToString(), out idAnterior))
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

                    Sensor item = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"], sensor, direcao, rota);
                    items.Add(item);
                }
                

               
            }
            result.Close();
            return (items);
        }

        public Sensor FindByID(long id)
        {
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota FROM  projetomercurio.sensor WHERE IdSensor={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            Sensor item = null;
            if (result.HasRows)
            {
                result.Read();
                Sensor sensor;
                int idAnterior = 0;
                if (!Int32.TryParse(result["IdSensorAnterior"].ToString(), out idAnterior))
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

                item = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"], sensor, direcao, rota);
                
                
            }
            result.Close();
            return item;
        }

        public Sensor FindLastId()
        {
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota FROM  projetomercurio.sensor order by IdSensor desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            Sensor item = null;
            if (result.HasRows)
            {
                result.Read();
                Sensor sensor;
                int idAnterior = 0;
                if (!Int32.TryParse(result["IdSensorAnterior"].ToString(), out idAnterior))
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

                item = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"], sensor, direcao, rota);
                
                
            }
            result.Close();
            return item;
        }

        public Sensor Update(Sensor item)
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
        public Sensor FindByName(string nome)
        {
            Sensor sensor = null;
            string sql = string.Format("SELECT IdSensor FROM projetomercurio.sensor WHERE Nome = '{0}'", nome);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Sensor sensor1;
                    int idAnterior = 0;
                    if (!Int32.TryParse(result["IdSensorAnterior"].ToString(), out idAnterior))
                    {
                        sensor1 = null;
                    }
                    else
                    {
                        sensor1 = new Sensor((int)result["IdSensorAnterior"]);
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

                    sensor = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"], sensor1, direcao, rota);
                }
                

                
            }
            result.Close();
            return sensor;
        }
    }
}
