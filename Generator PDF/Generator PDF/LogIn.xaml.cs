using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using Generator_PDF.VM;
using Generator_PDF.MySqlClass;

namespace Generator_PDF
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        MVLogInDataBase logInDataBase;
        public LogIn()
        {
            InitializeComponent();
       
            logInDataBase = new MVLogInDataBase();
            this.DataContext = logInDataBase;
            if(logInDataBase.GetPassowrd == null)
            {
                logInDataBase.GetPassowrd = new Func<string>(SendPasswordToClass);
            }
            if (logInDataBase.CloseWindow == null)
            {
                logInDataBase.CloseWindow = new Action(this.Close);
            }
            if(logInDataBase.SetConnection==null)
            {
                logInDataBase.SetConnection = new Action(SendToMvGenerateConnection);
            }
            AddCurrentConnection += MVGeneratePDF.OnAddCurrentCarParks;

        }

        public void SendToMvGenerateConnection()
        {
            GeneratePDF window1 = new GeneratePDF();
            OnAddCurrentCarParks(logInDataBase.connectionMy);
        }
        public string SendPasswordToClass()
        {
          return Password.Password;
        }
         public delegate void CurrentConnection(ConnectionMySql connectionMySql, ConnectionMySqlArgs carParksArgs);
        public event CurrentConnection AddCurrentConnection;

        protected virtual void OnAddCurrentCarParks(ConnectionMySql ConnectionMySql)
        {
            if (AddCurrentConnection != null)
            {
                AddCurrentConnection(logInDataBase.connectionMy, new ConnectionMySqlArgs() { CurrentconnectionMySql = ConnectionMySql });
            }
        }

     
    }
}
