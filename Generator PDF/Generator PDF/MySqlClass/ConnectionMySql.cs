using Generator_PDF.GenerateChart;
using Generator_PDF.VM;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator_PDF
{
    public class ConnectionMySql
    {
        #region Singleton

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
        

        private ConnectionMySql() { }

        #endregion

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

        private long toTime;
        public long ToTime { get => toTime; set => toTime = value; }       
      
        private long fromTime;
        public long FromTime { get => fromTime; set => fromTime = value; }
        private string cmdText;

        //private MySqlConnectionStringBuilder conn_string;
        //private MySqlConnection mySqlConnection;
        public static ConnectionMySql CreateOrGetConnectionClass()
        {
            if (connectionMySql == null)
            {
                connectionMySql = new ConnectionMySql();
                AddListParkings += MVGeneratePDF.OnAddListParkings;
            }
            return connectionMySql;
        }
        private void SetParameters()
        {
            //conn_string = new MySqlConnectionStringBuilder();
            //conn_string.Server = $"{server}";
            //conn_string.Port = 3306;
            //conn_string.Database = $"{database}";
            //conn_string.UserID = $"{user}";
            //conn_string.Password = $"{password}";
            //mySqlConnection = new MySqlConnection(conn_string.ToString());
        }
        //public ConnectionMySql(string server, string database, string user, string password)
        //{
        //    Server = server;
        //    Database = database;
        //    User = user;
        //    Password = password;
        //}

        public void TestConnect()
        {

            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = $"{server}";
            conn_string.Port = 3306;
            conn_string.Database = $"{database}";
            conn_string.UserID = $"{user}";
            conn_string.Password = $"{password}";
            MySqlConnection mySqlConnection = new MySqlConnection(conn_string.ToString());

            mySqlConnection.Open();
            mySqlConnection.Close();


        }

     
         public ObservableCollection<IdParking> GetCarParks()
        {
            ObservableCollection<IdParking> idParkings = new ObservableCollection<IdParking>();
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = $"{server}";
            conn_string.Port = 3306;
            conn_string.Database = $"{database}";
            conn_string.UserID = $"{user}";
            conn_string.Password = $"{password}";
            MySqlConnection mySqlConnection = new MySqlConnection(conn_string.ToString());

            mySqlConnection.Open();
            cmdText = $"SELECT parkingID, parkingName FROM Parkingi";
            MySqlCommand mySqlCommand = new MySqlCommand(cmdText, mySqlConnection);
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                idParkings.Add(new IdParking { idParking = int.Parse(mySqlDataReader.GetString(0)), name = mySqlDataReader.GetString(1) });

            }
            mySqlConnection.Close();
            return idParkings;

        }


        public async Task numberVehicle(List<IdParking> idParkings, int key)
        {
          
            AddListParkings += MVGeneratePDF.OnAddListParkings;
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = $"{server}";
            conn_string.Port = 3306;
            conn_string.Database = $"{database}";
            conn_string.UserID = $"{user}";
            conn_string.Password = $"{password}";
            MySqlConnection mySqlConnection = new MySqlConnection(conn_string.ToString());


            MySqlCommand mySqlCommand;
            MySqlDataReader mySqlDataReader;

            foreach (var item in idParkings)
            {
                mySqlConnection.Open();
                cmdText = $"SELECT COUNT(Address) FROM History WHERE Address IN (SELECT detectorID FROM Czujniki WHERE parkingID = {item.idParking}) AND DateTime BETWEEN {fromTime} and {toTime};";
                mySqlCommand = new MySqlCommand(cmdText, mySqlConnection);
                mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    item.count = int.Parse(mySqlDataReader.GetString(0));
                    await Task.Delay(1);
                }
                mySqlConnection.Close();

            }
           
            OnAddListParkings(key, new List<List<IdParking>>() { idParkings });
           // return true;

        }


        public async Task numberVehicleGroupBy(List<IdParking> idParkings, GroupBy groupBy, int key)
        {

            List<IdParking> idParkingss;
            //   List<IdParking> percent;

            AddListParkings += MVGeneratePDF.OnAddListParkings;


            List<List<IdParking>> parkings = new List<List<IdParking>>();
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = $"{server}";
            conn_string.Port = 3306;
            conn_string.Database = $"{database}";
            conn_string.UserID = $"{user}";
            conn_string.Password = $"{password}";
            MySqlConnection mySqlConnection = new MySqlConnection(conn_string.ToString());


            MySqlCommand mySqlCommand;
            MySqlDataReader mySqlDataReader;
            foreach (var item in idParkings)
            {
                idParkingss = new List<IdParking>();
              //  percent = new List<IdParking>();
                mySqlConnection.Open();                
                cmdText = $"SELECT {groupBy.ToString()}(FROM_UNIXTIME(DateTime/1000)), COUNT(Address) FROM History WHERE Address IN(SELECT detectorID FROM Czujniki WHERE parkingID= {item.idParking}) AND DateTime BETWEEN {fromTime} and {toTime} GROUP BY {groupBy.ToString()}(FROM_UNIXTIME(DateTime/1000))";
                mySqlCommand = new MySqlCommand(cmdText, mySqlConnection);
                mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    idParkingss.Add(new IdParking { GrupuByTime = int.Parse(mySqlDataReader.GetString(0)), count = int.Parse(mySqlDataReader.GetString(1)), idParking = item.idParking, name = item.name });
                 //   percent.Add(new IdParking { GrupuByTime = int.Parse(mySqlDataReader.GetString(0)), count = int.Parse(mySqlDataReader.GetString(1)), idParking = item.idParking, name = item.name });// int.Parse(mySqlDataReader.GetString(0));
                    await Task.Delay(1);
                }
                mySqlConnection.Close();
                parkings.Add(idParkingss);
            }
            

            OnAddListParkings(key, parkings);
      //      return true;
        }


        public async Task numberVehicleGroupByInEachHoursOfMonth(List<IdParking> idParkings, int key)
        {

            List<IdParking> idParkingss;
            //   List<IdParking> percent;
            AddListParkings += MVGeneratePDF.OnAddListParkings;
            List<List<IdParking>> parkings = new List<List<IdParking>>();
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = $"{server}";
            conn_string.Port = 3306;
            conn_string.Database = $"{database}";
            conn_string.UserID = $"{user}";
            conn_string.Password = $"{password}";
            MySqlConnection mySqlConnection = new MySqlConnection(conn_string.ToString());
            List<IdParking> moth = new List<IdParking>();
            int last = -2;
            int counter = 5;
            MySqlCommand mySqlCommand;
            MySqlDataReader mySqlDataReader;
            
            foreach (var item in idParkings)
            {
                idParkingss = new List<IdParking>();
               //  percent = new List<IdParking>();
                mySqlConnection.Open();
                cmdText = $"SELECT Month(FROM_UNIXTIME(DateTime/1000)), Hour(FROM_UNIXTIME(DateTime/1000)), COUNT(Address) FROM History WHERE Address IN( SELECT detectorID FROM Czujniki WHERE parkingID= {item.idParking}) AND DateTime BETWEEN {fromTime} and {toTime} GROUP BY Month(FROM_UNIXTIME(DateTime/1000)), Hour(FROM_UNIXTIME(DateTime/1000))";
                mySqlCommand = new MySqlCommand(cmdText, mySqlConnection);
                mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    int zmienna = int.Parse(mySqlDataReader.GetString(1));
                    try
                    {
                        if (last > zmienna)
                        {
                            
                                while (last < 23)
                                {
                                moth.Add(new IdParking { GrupuByTime = int.Parse(mySqlDataReader.GetString(0))+1, GrubByInEachHour = 0, count = 0, idParking = item.idParking, name = item.name });
                                last++;
                                }
                          

                            counter = 0;
                            parkings.Add(moth);
                            moth = new List<IdParking>();

                            while (int.Parse(mySqlDataReader.GetString(1)) > counter)
                            {
                                moth.Add(new IdParking { GrupuByTime = parkings.Last()[0].GrupuByTime+1, GrubByInEachHour = 0, count = 0, idParking = item.idParking, name = item.name });
                                counter++;
                            }
                        }
                    }
                    catch
                    {
                        moth = new List<IdParking>();
                    }


                   



                        moth.Add(new IdParking { GrupuByTime = int.Parse(mySqlDataReader.GetString(0)), GrubByInEachHour = int.Parse(mySqlDataReader.GetString(1)), count = int.Parse(mySqlDataReader.GetString(2)), idParking = item.idParking, name = item.name });
                    //   percent.Add(new IdParking { GrupuByTime = int.Parse(mySqlDataReader.GetString(0)), count = int.Parse(mySqlDataReader.GetString(1)), idParking = item.idParking, name = item.name });// int.Parse(mySqlDataReader.GetString(0));
                    await Task.Delay(1);
                    last = int.Parse(mySqlDataReader.GetString(1));
                   
                }

                mySqlConnection.Close();
                parkings.Add(moth);
            }
            OnAddListParkings(key, parkings);
            
            //      return true;
        }


        public delegate void GetListHandler(int key, List<List<IdParking>> idParkings, ListIdParking imageArgs);
        static  public event GetListHandler AddListParkings;

        protected virtual void OnAddListParkings(int key, List<List<IdParking>> idParkings )
        {
            if (AddListParkings != null)
            {
                AddListParkings(key, idParkings, new ListIdParking() { key = key, value = idParkings });
            }
        }

    }
}
        



