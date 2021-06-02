using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    class GrupoManipulation
    {
        private DBConnection connection;
        public GrupoManipulation()
        {
            connection = new DBConnection();
        }
        public List<Grupo> FindAll()
        {
            List<Grupo> items = new List<Grupo>();
            string sql = string.Format("SELECT IdGrupo, Nome FROM  projetomercurio.grupo");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Grupo item = new Grupo((int)result["IdGrupo"], result["Nome"].ToString());
                    items.Add(item);
                }


            }
            result.Close();
            return items;

        }

        public List<Grupo> FindAll(int idUsuario)
        {
            List<Grupo> items = new List<Grupo>();
            string sql = string.Format(@"SELECT a.IdGrupo, b.Nome FROM projetomercurio.GrupoXUsuario a
                                        INNER JOIN projetomercurio.grupo b on a.IdGrupo = b.IdGrupo
                                        WHERE a.IdUsuario = {0};",idUsuario);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Grupo item = new Grupo((int)result["IdGrupo"], result["Nome"].ToString());
                    items.Add(item);
                }


            }
            result.Close();
            return items;

        }

        public Grupo FindByID(long id)
        {
            Grupo item = null;
            string sql = string.Format("SELECT IdGrupo, Nome FROM  projetomercurio.grupo WHERE IdGrupo={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                item = new Grupo((int)result["IdGrupo"], result["Nome"].ToString());

            }
            result.Close();
            return item;
        }

        public Grupo FindByName(string nome)
        {
            Grupo item = null;
            string sql = string.Format("SELECT IdGrupo, Nome FROM  projetomercurio.grupo WHERE Nome='{0}' ", nome);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                item = new Grupo((int)result["IdGrupo"], result["Nome"].ToString());


            }
            result.Close();
            return item;
        }

        public void RemoveGrupo(int idUsuario, int idGrupo)
        {
            string sql = string.Format("DELETE FROM projetomercurio.GrupoXUsuario WHERE IdUsuario={0} and IdGrupo = {1}", idUsuario, idGrupo);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao remover Grupo no banco.");
            }
        }

        public void AddGrupo(int idUsuario, int idGrupo)
        {
            string sql = string.Format("INSERT INTO projetomercurio.GrupoXUsuario (IdUsuario, IdGrupo) VALUES ({0}, {1})", idUsuario,idGrupo);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao GrupoxUsuario item no banco.");
            }
        }
    }
}
