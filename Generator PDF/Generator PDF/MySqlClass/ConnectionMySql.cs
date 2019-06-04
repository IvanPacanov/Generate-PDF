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
        #region fields

        private MySqlConnection mySqlConnection = null;

        private MySqlDataReader mySqlDataReader = null;
        private MySqlCommand mySqlCommand = null;

        private string server;
        public string Server { get => server; set => server = value; }

        private string database;
        public string Database { get => database; set => database = value; }

        private string user;
        public string User { get => user; set => user = value; }

        private string password;
        public string Password { get => password; set => password = value; }

        #endregion

        public ConnectionMySql(string server, string database, string user, string password)
        {
            Server = server;
            Database = database;
            User = user;
            Password = password;
        }
        public void StartConnect()
        {
            String connectionString = $@"server={Server};database={Database};userid={User};password={Password}";
            try
            {
                mySqlConnection = new MySqlConnection(connectionString);

            }
            catch (MySqlException en)
            {
                MessageBox.Show(en.ToString());
            }

        }
        public void SendQuestion(int IdParking)
        {
            string cmdText = $"SELECT COUNT(Address) FROM History WHERE Address IN (SELECT detectorID FROM Czujniki WHERE parkingID = {IdParking})";
            mySqlConnection.Open();
            mySqlCommand = new MySqlCommand(cmdText);
            mySqlDataReader = mySqlCommand.ExecuteReader();
           
        }

        public int ReadSqlMessage()
        {
            mySqlDataReader = mySqlCommand.ExecuteReader();
            while(mySqlDataReader.Read())
            {
               return int.Parse(mySqlDataReader.GetString(0));
            }
            return 0;
        }

    }
}




