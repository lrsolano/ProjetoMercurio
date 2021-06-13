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
            if (item.SensorAnterior == 0)
            {
                sql = string.Format("INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota, HashNum) VALUES ('{0}',now(),{1},{2},'{3}',{4});", item.Nome, item.Inicial, item.Direcao.Id, direcaoRota,  item.Hash);
            }
            else
            {
                sql = string.Format("INSERT INTO projetomercurio.sensor (Nome, DataCriacao, Inicial, IdDirecao, DirecaoRota, IdSensorAnterior, HashNum) VALUES ('{0}',now(),{1},{2},'{3}',{4},{5});", item.Nome, item.Inicial, item.Direcao.Id, direcaoRota, item.SensorAnterior, item.Hash);
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
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota, HashNum FROM projetomercurio.sensor");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    int sensor;
                    int idAnterior = 0;
                    if (!Int32.TryParse(result["IdSensorAnterior"].ToString(), out idAnterior))
                    {
                        sensor = 0;
                    }
                    else
                    {
                        sensor = (int)result["IdSensorAnterior"];
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

                    Sensor item = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"], sensor, direcao, rota, result["HashNum"].ToString());
                    items.Add(item);
                }
                

               
            }
            result.Close();
            return (items);
        }
        public Sensor FindByID(long id)
        {
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota, HashNum FROM  projetomercurio.sensor WHERE IdSensor={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            Sensor item = null;
            if (result.HasRows)
            {
                result.Read();
                int sensor;
                int idAnterior = 0;
                if (!Int32.TryParse(result["IdSensorAnterior"].ToString(), out idAnterior))
                {
                    sensor = 0;
                }
                else
                {
                    sensor = ((int)result["IdSensorAnterior"]);
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

                item = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"], sensor, direcao, rota, result["HashNum"].ToString());
                
                
            }
            result.Close();
            return item;
        }
        public Sensor FindLastId()
        {
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota, HashNum FROM  projetomercurio.sensor order by IdSensor desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            Sensor item = null;
            if (result.HasRows)
            {
                result.Read();
                int sensor;
                int idAnterior = 0;
                if (!Int32.TryParse(result["IdSensorAnterior"].ToString(), out idAnterior))
                {
                    sensor = 0;
                }
                else
                {
                    sensor =(int)result["IdSensorAnterior"];
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

                item = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"], sensor, direcao, rota, result["HashNum"].ToString());
                
                
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

            string sql = string.Format("UPDATE projetomercurio.sensor SET  Nome = '{0}', Inicial = {1}, IdDirecao = {2}, DirecaoRota = '{3}'WHERE IdSensor = {4};", item.Nome, item.Inicial, item.Direcao.Id, direcaoRota,item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar Sensor no banco.");
            }

            return FindByID(item.Id);
        }
        public Sensor FindByName(string nome)
        {
            Sensor sensor = null;
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota, HashNum FROM  projetomercurio.sensor WHERE Nome = '{0}'", nome);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    int sensor1;
                    int idAnterior = 0;
                    if (!Int32.TryParse(result["IdSensorAnterior"].ToString(), out idAnterior))
                    {
                        sensor1 = 0;
                    }
                    else
                    {
                        sensor1 = ((int)result["IdSensorAnterior"]);
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

                    sensor = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"], sensor1, direcao, rota, result["HashNum"].ToString());
                }
                

                
            }
            result.Close();
            return sensor;
        }
        public Sensor FindByHash(string hash)
        {
            Sensor sensor = null;
            string sql = string.Format("SELECT IdSensor, Nome, DataCriacao, Inicial, IdSensorAnterior, IdDirecao, DirecaoRota, HashNum FROM  projetomercurio.sensor WHERE HashNum = '{0}'", hash);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    int sensor1;
                    int idAnterior = 0;
                    if (!Int32.TryParse(result["IdSensorAnterior"].ToString(), out idAnterior))
                    {
                        sensor1 = 0;
                    }
                    else
                    {
                        sensor1 = ((int)result["IdSensorAnterior"]);
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

                    sensor = new Sensor((int)result["IdSensor"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], (bool)result["Inicial"], sensor1, direcao, rota, result["HashNum"].ToString());
                }



            }
            result.Close();
            return sensor;
        }

        public bool CanDelete(long id)
        {
            bool resultado;

            string sql = string.Format(@"SELECT a.IdSensorAnterior FROM  projetomercurio.sensor as a
                                        left join projetomercurio.local as b on a.IdSensorAnterior = b.IdSensor
                                        left join projetomercurio.rota as c on c.IdSensorInicial = a.IdSensorAnterior 
                                        left join projetomercurio.rota as d on d.IdSensorFinal = a.IdSensorAnterior 
                                        WHERE IdSensorAnterior={0} ", id);
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
