using MySqlConnector;
using System;
using Mercurio.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Repository
{
    class DBConnection
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnection()
        {
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
				throw new DBConnectionException(ex.Message);
			}
			return result;
		}

		public bool SendCommand(string sql)
        {
			bool result = false;
            try
            {
				if (this.OpenConnection() == true)
				{
					MySqlCommand cmd = new MySqlCommand(sql, connection);

					cmd.ExecuteNonQuery();

					this.CloseConnection();
				}
                else
                {
					result = false;
                }
			}
			catch(Exception ex)
            {
				throw new DBConnectionException(string.Format("DBConection => {0}", ex));
            }

			return result;



		}

		public MySqlDataReader SendQuery(string query)
        {
			MySqlDataReader dataReader = null;
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
				throw new DBConnectionException(string.Format("DBConection => {0}", ex));
			}
			return dataReader;

		}


	}
}
