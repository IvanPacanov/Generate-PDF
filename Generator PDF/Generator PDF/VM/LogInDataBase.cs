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
    public class LogInDataBase:AbstractViewClass
    {
        #region BlindingButton
        public ICommand ConnectionButtonCommand { get; private set; }
        public ICommand CloseButtonCommand { get; private set; }

        #endregion

        #region BlindingTextBlock
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

        private string texTextLogt;
        public string TextLog
        {
            get { return this.texTextLogt; }
            set
            {
                this.texTextLogt = value;
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



        public Action GetPassowrd { get; set; }


                     


        public LogInDataBase()
        {
            ConnectionButtonCommand = new DelegateCommand(Connection);
            CloseButtonCommand = new DelegateCommand(CloseApp);
        }

        private void Connection()
        {
            //  GetPassowrd();
            //  ToDo();
            Window1 window1 = new Window1();
            window1.Show();
            //Form1 form = new Form1();
            //string a = TextIp;
            //form.Show();
        }
        private void ToDo()
        {
           

        }

        static void generate()
        {
            Chart charaa = new Chart();
            Series seria = new Series("Cos");
            seria.Points.DataBindXY(new[] { "cos", "Co", "Działa" }, new[] { 1, 2, 3 });
            charaa.Series.Add(seria);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }




}

