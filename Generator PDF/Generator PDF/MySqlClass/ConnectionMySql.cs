using Generator_PDF.GenerateChart;
using Generator_PDF.MySqlClass;
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

        MySqlConnectionStringBuilder conn_string;
        MySqlConnection mySqlConnection;
        MySqlCommand mySqlCommand;
        MySqlDataReader mySqlDataReader;
        private ConnectionMySql() {

        }

        public void SetLoginParameters()
        {

            conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = $"{server}";
            conn_string.Port = 3306;
            conn_string.Database = $"{database}";
            conn_string.UserID = $"{user}";
            conn_string.Password = $"{password}";
            conn_string.DefaultCommandTimeout = 3000;
            mySqlConnection = new MySqlConnection(conn_string.ToString());
        }
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
            mySqlConnection.Open();
            mySqlConnection.Close();
        }


        public ObservableCollection<IdParking> GetCarParks()
        {
            ObservableCollection<IdParking> idParkings = new ObservableCollection<IdParking>();

            mySqlConnection.Open();
            cmdText = $"SELECT parkingID, parkingName FROM Parkingi";
            mySqlCommand = new MySqlCommand(cmdText, mySqlConnection);
            mySqlDataReader = mySqlCommand.ExecuteReader();

            while (mySqlDataReader.Read())
            {
                idParkings.Add(new IdParking { idParking = int.Parse(mySqlDataReader.GetString(0)), name = mySqlDataReader.GetString(1) });

            }

            mySqlConnection.Close();

            return idParkings;


        }


        public async Task numberVehicle(List<IdParking> idParkings, int key)
        {
            foreach (var item in idParkings)
            {
                List<StruckTest> test = new List<StruckTest>();

                mySqlConnection.Open();
                string cmd = $"SELECT *  FROM History WHERE Address IN(SELECT detectorID FROM Czujniki WHERE parkingID = {item.idParking})AND DateTime BETWEEN {fromTime} and {toTime} ORDER BY DateTime ASC ";
                mySqlCommand = new MySqlCommand(cmd, mySqlConnection);

                try
                {
                    mySqlDataReader = mySqlCommand.ExecuteReader();

                    while (mySqlDataReader.Read())
                    {
                        test.Add(new StruckTest()
                        {
                            isBusy = int.Parse(mySqlDataReader.GetString(4)),
                            Address = (int.Parse(mySqlDataReader.GetString(2)))
                        });
                    
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                mySqlConnection.Close();
                var help = test.GroupBy(x => x.Address);
                IEnumerable<StruckTest> smths = help.SelectMany(group => group);
                List<StruckTest> newList = smths.ToList();
                List<StruckTest> newList1 = new List<StruckTest>();
                try
                {
                    foreach (var itemm in help)
                    {
                        newList1.AddRange(GetTrigger(newList.Where(x => x.Address == itemm.Key).ToList()));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                item.count += newList1.Count;
            }
            await Task.Delay(1);

            OnAddListParkings(key, new List<List<IdParking>>() { idParkings });

        }


        public async Task numberVehicleGroupBy(List<IdParking> idParkings, GroupBy groupBy, int key)
        {

            List<List<IdParking>> parkings = new List<List<IdParking>>();

            foreach (var item in idParkings)
            {
                List<StruckTest> test = new List<StruckTest>();
                List<IdParking> idParkingss = new List<IdParking>();

                mySqlConnection.Open();
                cmdText = $"SELECT Address,IsBusy,{GroupBy.YEAR.ToString()}(FROM_UNIXTIME(DateTime/1000)),{groupBy.ToString()}(FROM_UNIXTIME(DateTime/1000)) FROM History WHERE Address IN(SELECT detectorID FROM Czujniki WHERE parkingID= {item.idParking}) AND DateTime BETWEEN {fromTime} and {toTime} ORDER BY `DateTime` ASC";
                mySqlCommand = new MySqlCommand(cmdText, mySqlConnection);
                try
                {
                    mySqlDataReader = mySqlCommand.ExecuteReader();

                    while (mySqlDataReader.Read())
                    {
                        if (groupBy == GroupBy.MONTH)
                        {
                            test.Add(new StruckTest()
                            {
                                isBusy = int.Parse(mySqlDataReader.GetString(1)),
                                Address = (int.Parse(mySqlDataReader.GetString(0))),
                                YEAR = (int.Parse(mySqlDataReader.GetString(2))),
                                Month = (int.Parse(mySqlDataReader.GetString(3)))
                            });
                        }
                        else if (groupBy == GroupBy.HOUR)
                        {
                            test.Add(new StruckTest()
                            {
                                isBusy = int.Parse(mySqlDataReader.GetString(1)),
                                Address = (int.Parse(mySqlDataReader.GetString(0))),
                                Hour = (int.Parse(mySqlDataReader.GetString(3)))
                            });
                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                mySqlConnection.Close();
                var help = test.GroupBy(x => x.Address);
                IEnumerable<StruckTest> smths = help.SelectMany(group => group);
                List<StruckTest> newList = smths.ToList();
                List<StruckTest> newList1 = new List<StruckTest>();
                foreach (var itemm in help)
                {
                    newList1.AddRange(GetTrigger(newList.Where(x => x.Address == itemm.Key).ToList()));
                }

                if (groupBy == GroupBy.MONTH)
                {
                    var all = newList1.GroupBy(x => new { x.YEAR, x.Month });
                    foreach (var itemq in all.ToList())
                    {
                        idParkingss.Add(new IdParking { year = itemq.Min(x => x.YEAR), month = itemq.Min(x => x.Month), count = itemq.Sum(x => x.isBusy+1), idParking = item.idParking, name = item.name });
                    }
                }
                else if (groupBy == GroupBy.HOUR)
                {

                    var all = newList1.GroupBy(x => x.Hour);

                    foreach (var itemq in all.ToList())
                    {
                        idParkingss.Add(new IdParking { hours = itemq.Min(x => x.Hour), count = itemq.Sum(x => x.isBusy+1), idParking = item.idParking, name = item.name });
                    }

                }

                parkings.Add(idParkingss);
            }
            await Task.Delay(1);

            OnAddListParkings(key, parkings);

        }
        public List<StruckTest> GetTrigger(List<StruckTest> struckTests)
        {
            List<StruckTest> Help = new List<StruckTest>();
            for (int i = 0; i < struckTests.Count; i++)
            {
                try
                {
                    if (struckTests.ElementAt(i).isBusy != struckTests.ElementAt(i + 1).isBusy && struckTests.ElementAt(i).isBusy == 0)
                    {
                        Help.Add(struckTests.ElementAt(i));
                    }


                }
                catch
                {
                    //Taki myczek, jak wyjdzie poza indeks tablicy,
                }

            }
            return Help;

        }

        public async Task numberVehicleGroupByInEachHoursOfMonth(List<IdParking> idParkings, int key)
        {

            List<IdParking> idParkingss;
            List<List<IdParking>> parkings = new List<List<IdParking>>();
            List<IdParking> moth = new List<IdParking>();
            List<StruckTest> test = new List<StruckTest>();
            foreach (var item in idParkings)
            {
                idParkingss = new List<IdParking>();
                //  percent = new List<IdParking>();
                mySqlConnection.Open();
                cmdText = $"SELECT Address,IsBusy,{GroupBy.YEAR.ToString()}(FROM_UNIXTIME(DateTime/1000)) ,{GroupBy.MONTH.ToString()}(FROM_UNIXTIME(DateTime/1000)),{GroupBy.HOUR.ToString()}(FROM_UNIXTIME(DateTime/1000)) FROM History WHERE Address IN(SELECT detectorID FROM Czujniki WHERE parkingID= {item.idParking}) AND DateTime BETWEEN {fromTime} and {toTime} ORDER BY `DateTime` ASC";
                mySqlCommand = new MySqlCommand(cmdText, mySqlConnection);
                try
                {
                    mySqlDataReader = mySqlCommand.ExecuteReader();

                    while (mySqlDataReader.Read())
                    {

                        test.Add(new StruckTest()
                        {
                            isBusy = int.Parse(mySqlDataReader.GetString(1)),
                            Address = (int.Parse(mySqlDataReader.GetString(0))),
                            YEAR = (int.Parse(mySqlDataReader.GetString(2))),
                            Month = (int.Parse(mySqlDataReader.GetString(3))),
                            Hour = (int.Parse(mySqlDataReader.GetString(4))),

                        });


                    }

                    mySqlConnection.Close();


                }
                catch (System.IO.IOException e)
                {
                    MessageBox.Show(e.ToString());
                }


                var help = test.GroupBy(x => x.Address);
                IEnumerable<StruckTest> smths = help.SelectMany(group => group);
                List<StruckTest> newList = smths.ToList();
                List<StruckTest> newList1 = new List<StruckTest>();
                foreach (var itemm in help)
                {
                    newList1.AddRange(GetTrigger(newList.Where(x => x.Address == itemm.Key).ToList()));
                }
                var all = newList1.GroupBy(x => new { x.YEAR, x.Month, x.Hour });

                foreach (var itemq in all.ToList())
                {
                    idParkingss.Add(new IdParking { year = itemq.Min(x => x.YEAR), month = itemq.Min(x => x.Month), hours = itemq.Min(x => x.Hour), count = itemq.Sum(x => x.isBusy+1), idParking = item.idParking, name = item.name });
                }
                parkings.Add(idParkingss);
            }
            await Task.Delay(1);
            OnAddListParkings(key, parkings);

        }


        public delegate void GetListHandler(int key, List<List<IdParking>> idParkings, ListIdParking imageArgs);
        static public event GetListHandler AddListParkings;

        protected virtual void OnAddListParkings(int key, List<List<IdParking>> idParkings)
        {
            if (AddListParkings != null)
            {
                AddListParkings(key, idParkings, new ListIdParking() { key = key, value = idParkings });
            }
        }

    }
}





