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
        public string Password;

        private string textLogt;
        public string TextLog
        {
            get { return this.textLogt; }
            set
            {
                this.textLogt = value;
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
            ConnectionButtonCommand = new DelegateCommand(Connection);
            CloseButtonCommand = new DelegateCommand(CloseApp);
        }

        private void Connection()
        {
             
            connectionMy = new ConnectionMySql(textIp, textDataBase, textLogt, GetPassowrd());
            connectionMy.StartConnect();
            SetConnection();




        }
        private void ToDo()
        {
           

        }

        static void generate()
        {
            Chart charaa = new Chart();
            Series seria = new Series("Cos");
            seria.Points.DataBindXY(new[] { "cos", "Co", "Działa", "EEEEE" }, new[] { 1, 2, 3, -20 });
            
            charaa.Series.Add(seria);

        }

      

       
    }




}

