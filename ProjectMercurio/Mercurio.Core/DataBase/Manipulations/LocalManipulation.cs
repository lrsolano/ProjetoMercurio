using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    class LocalManipulation : IRepository<Local>
    {
        private DBConnection connection;
        public LocalManipulation()
        {
            connection = new DBConnection();
        }
        public Local Create(Local item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.local (Nome, DataCriacao, IdSensor) VALUES ('{0}', NOW(),{1})", item.Nome, item.Sensor.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir Local no banco.");
            }

            return FindLastId();
        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.local WHERE IdLocal={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao remover Local no banco.");
            }
        }

        public List<Local> FindAll()
        {
            List<Local> items = new List<Local>();
            string sql = string.Format("SELECT IdLocal, Nome, DataCriacao, IdSensor FROM projetomercurio.local");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Sensor sensor = new Sensor((int)result["IdSensor"]);
                    Local item = new Local((int)result["IdLocal"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], sensor);
                    items.Add(item);
                }
                

                
            }
            result.Close();
            return (items);
        }

        public Local FindByID(long id)
        {
            string sql = string.Format("SELECT IdLocal, Nome, DataCriacao, IdSensor FROM projetomercurio.local WHERE IdLocal={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            Local item = null;
            if (result.HasRows)
            {
                result.Read();
                Sensor sensor = new Sensor((int)result["IdSensor"]);
                item = new Local((int)result["IdLocal"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], sensor);
                
                
            }
            result.Close();
            return item;
        }

        public Local FindLastId()
        {
            string sql = string.Format("SELECT IdLocal, Nome, DataCriacao, IdSensor FROM projetomercurio.local order by IdLocal desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            Local item = null;
            if (result.HasRows)
            {
                result.Read();
                Sensor sensor = new Sensor((int)result["IdSensor"]);
                item = new Local((int)result["IdLocal"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], sensor);
                
                
            }
            result.Close();
            return item;
        }

        public Local Update(Local item)
        {
            string sql = string.Format("UPDATE projetomercurio.Local SET  Nome = '{0}', IdSensor = {1} WHERE IdSensor = {2}", item.Nome, item.Sensor.Id, item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar Local no banco.");
            }

            return FindByID(item.Id);
        }
        public Local FindByName(string nome)
        {
            string sql = string.Format("SELECT IdLocal, Nome, DataCriacao, IdSensor FROM projetomercurio.local WHERE Nome='{0}' ", nome);
            MySqlDataReader result = connection.SendQuery(sql);
            Local item = null;
            if (result.HasRows)
            {
                result.Read();
                Sensor sensor = new Sensor((int)result["IdSensor"]);
                item = new Local((int)result["IdLocal"], result["Nome"].ToString(), (DateTime)result["DataCriacao"], sensor);
                
                
            }
            result.Close();
            return item;
        }
    }
}
