using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator_PDF
{
    public class ConnectionMySql
    {

        private static ConnectionMySql connectionMySql = null;
        public static ConnectionMySql Instance_connectionMySql
        {
            get
            {
                if (connectionMySql == null)
                {
                    connectionMySql = new ConnectionMySql();
                }
                return connectionMySql;
            }
        }


        #region fields

     

        private string server;
        public string Server { get => server; set => server = value; }

        private string database;
        public string Database { get => database; set => database = value; }

        private string user;
        public string User { get => user; set => user = value; }

        private string password;
        public string Password { get => password; set => password = value; }

        #endregion

        public ConnectionMySql()
        {

        }

        //public ConnectionMySql(string server, string database, string user, string password)
        //{
        //    Server = server;
        //    Database = database;
        //    User = user;
        //    Password = password;
        //}



        public async Task<int> numberVehicle(int parkingID)
        {
            int result = 0;
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = $"{server}";
            conn_string.Port = 3306;
            conn_string.Database = $"{database}";
            conn_string.UserID= $"{user}";
            conn_string.Password = $"{password}";
           // String connectionString = $@"server={Server};database={Database};userid={User};password={Password}";
            MySqlConnection mySqlConnection = new MySqlConnection(conn_string.ToString());

                mySqlConnection.Open();
                string cmdText = $"SELECT COUNT(Address) FROM History WHERE Address IN (SELECT detectorID FROM Czujniki WHERE parkingID = {parkingID})";
                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, mySqlConnection);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    result= int.Parse(mySqlDataReader.GetString(0));
                    await Task.Delay(1);
                }
                mySqlConnection.Close();
                return result;
            
        }
    }
}
        



