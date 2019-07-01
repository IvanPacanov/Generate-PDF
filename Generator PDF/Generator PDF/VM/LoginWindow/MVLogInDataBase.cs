using MySql.Data.MySqlClient;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;

namespace Generator_PDF.VM
{
    public class MVLogInDataBase:BaseViewClass
    {
        #region BlindingButton
        public ICommand ConnectionButtonCommand { get; private set; }
        
        #endregion

        #region BlindingTextBlock

        public ConnectionMySql connectionMy { get; set; }
        public Action SetConnection { get; set; }
        private string textIp;
        public string TextIp
        {
            get { return this.textIp; }
            set
            {
                this.textIp = value;
                this.NotifyPropertyChanged("TextIp");

            }
        }
        private string password;
        public string Password { get { return password; } set { password = value; }}

        private string textLog;
        public string TextLog
        {
            get { return this.textLog; }
            set
            {
                this.textLog = value;
                this.NotifyPropertyChanged("TextLog");

            }
        }

        private string textDataBase;
        public string TextDataBase
        {
            get { return this.textDataBase; }
            set
            {
                this.textDataBase = value;
                this.NotifyPropertyChanged("TextDataBase");

            }
        }

        #endregion



        public Func<string> GetPassowrd { get; set; }
        public Action CloseWindow { get; set; } 



        public MVLogInDataBase()
        {
            textIp = "cloud.jumarpol.pl";
            textLog = "mateusz";
            password = "mateusz_ksk";
            textDataBase = "parkingiBackup";
            ConnectionButtonCommand = new DelegateCommand(Connection);
            CloseButtonCommand = new DelegateCommand(CloseApp);
        }

        private void Connection()
        {
            connectionMy = ConnectionMySql.CreateOrGetConnectionClass();
            connectionMy.Server = textIp;
            connectionMy.Database = textDataBase;
            connectionMy.User = textLog;
            connectionMy.Password = GetPassowrd();
            try
            {
                connectionMy.TestConnect();
                SetConnection();
            }
            catch
            {
                MessageBox.Show("Brak połaczenia");
            }          
        }      
    }
}

