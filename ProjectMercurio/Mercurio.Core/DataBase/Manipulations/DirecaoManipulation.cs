using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    class DirecaoManipulation
    {
        private DBConnection connection;
        public DirecaoManipulation()
        {
            connection = new DBConnection();
        }
        public List<Direcao> FindAll()
        {
            List<Direcao> items = new List<Direcao>();
            string sql = string.Format("SELECT IdDirecao, Movimento FROM  projetomercurio.direcao");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Direcao item = new Direcao((int)result["IdDirecao"], result["Movimento"].ToString());
                    items.Add(item);
                }
                

            }
            result.Close();
            return items;

        }
        public Direcao FindByID(long id)
        {
            Direcao item = null;
            string sql = string.Format("SELECT IdDirecao, Movimento FROM  projetomercurio.direcao WHERE IdDirecao={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                item = new Direcao((int)result["IdDirecao"], result["Movimento"].ToString());
                
            }
            result.Close();
            return item;
        }
        public Direcao FindByName(string movimento)
        {
            Direcao item = null;
            string sql = string.Format("SELECT IdDirecao, Movimento FROM  projetomercurio.direcao WHERE Movimento='{0}' ", movimento);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                item = new Direcao((int)result["IdDirecao"], result["Movimento"].ToString());
                

            }
            result.Close();
            return item;
        }
    }
}
