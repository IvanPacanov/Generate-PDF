using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator_PDF
{
    class ConnectionMySql
    {
        #region fields

        private MySqlConnection mySqlConnection = null;

        private MySqlDataReader mySqlDataReader = null;

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
        public void SendQuestion(string nameColumn, string nameTable)
        {

            string cmdText = $"SELECT {nameColumn} FROM {nameTable}";
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(cmdText);
            mySqlDataReader = mySqlCommand.ExecuteReader();
        }



    }
}




/*

    try
    {




com.Open();
        String cmdText = "SELECT * FROM Algorytm1";

reader = cmd.ExecuteReader();
    //    while (reader.Read())
        {
            //                   MessageBox.Show(reader.GetString(5));
        }
        MessageBox.Show("Podłaczony");
    }
    catch (MySqlException error)
    {
        MessageBox.Show(error.ToString());
    }
    finally
    {
        if (com != null)
        {
            com.Close();
        }
    }
}
}
*/
