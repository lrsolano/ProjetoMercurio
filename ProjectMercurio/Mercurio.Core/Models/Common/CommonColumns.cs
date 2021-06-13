using MySqlConnector;
using System;

namespace Mercurio.Core
{
    public abstract class CommonColumns
    {
        private readonly string _tabela;
        private readonly string _colunaId;
        private DBConnection _connection;

        public long Id { get; internal set; }
        public DateTime DataCriacao { get; internal set; }

        public CommonColumns(string tabela, string colunaId, int id, DateTime dataCriacao)
        {
            _tabela = tabela;
            _colunaId = colunaId;
            _connection = new DBConnection();
            Id = id;
            DataCriacao = dataCriacao;
            
        }

        public CommonColumns(string tabela, string colunaId)
        {
            _tabela = tabela;
            _colunaId = colunaId;
            _connection = new DBConnection();
        }

        public bool Exists(long id)
        {
            string sql = string.Format("SELECT {0} FROM  projetomercurio.{1} WHERE {0}={2} ", _colunaId, _tabela, id);
            //Console.WriteLine(string.Format("SELECT EXIST => {0}",sql));
            MySqlDataReader result = _connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Close();
                return true;
            }
            else
            {
                result.Close();
                return false;
            }
        }

    }
}
