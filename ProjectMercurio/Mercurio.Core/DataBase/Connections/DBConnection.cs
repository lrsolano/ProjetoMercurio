using MySqlConnector;
using System;

namespace Mercurio.Core
{
    class DBConnection
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
		private LogActivity log;

        public DBConnection()
        {
			log = new LogActivity(LogNivel.Verbose, "DataBase");
            Initialize();
        }

		public DBConnection(string server, string database, string uid, string password)
        {
			string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
			database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

			connection = new MySqlConnection(connectionString);
		}

		public DBConnection(MySqlConnection connect)
        {
			connection = connect;
        }

        private void Initialize()
        {
            server = "192.168.18.43";

            database = "projetomercurio";
            uid = "root";
            password = "1234";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
			//log.Write(LogNivel.Info, string.Format("Connection String: {0}", connectionString));
            connection = new MySqlConnection(connectionString);
        }
		private bool OpenConnection()
		{
			bool result = false;
			try
			{
				connection.Open();
				result = true;
			}
			catch (MySqlException ex)
			{
				log.Write(LogNivel.Erro, ex);
				switch (ex.Number)
				{
					case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
						break;

					case 1045:
						Console.WriteLine("Invalid username/password, please try again");
						break;
					case 1042:
						Console.WriteLine("Connect Timeout Expired");
						break;
				}
			}
			catch(Exception ex)
            {
                Console.WriteLine(ex);
				log.Write(LogNivel.Erro, ex);
				throw new DBConnectionException(ex.Message);
			}
			return result;
		}
		private bool CloseConnection()
		{
			bool result = false;
			try
			{
				connection.Close();
				result = true;
			}
			catch (MySqlException ex)
			{
				log.Write(LogNivel.Erro, ex);
				throw new DBConnectionException(ex.Message);
			}
			return result;
		}

		public bool SendCommand(string sql)
        {
			bool result = false;
			log.Write(LogNivel.Info, string.Format("Command: {0}", sql));
			try
            {
				if (this.OpenConnection() == true)
				{
					MySqlCommand cmd = new MySqlCommand(sql, connection);

					cmd.ExecuteNonQuery();

					this.CloseConnection();

					result = true;
				}
                else
                {
					result = false;
                }
			}
			catch(Exception ex)
            {
				log.Write(LogNivel.Erro, ex);
				throw new DBConnectionException(string.Format("DBConection => {0}", ex));
            }

			return result;



		}

		public MySqlDataReader SendQuery(string query)
        {
			MySqlDataReader dataReader = null;
			log.Write(LogNivel.Info, string.Format("Query: {0}", query));
			try
            {
				if (this.OpenConnection() == true)
				{
					MySqlCommand cmd = new MySqlCommand(query, connection);
					dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				}
            }
            catch (Exception ex)
            {
				log.Write(LogNivel.Erro, ex);
				throw new DBConnectionException(string.Format("DBConection => {0}", ex));
			}
			return dataReader;

		}


	}
}
