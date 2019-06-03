using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using Generator_PDF.VM;

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
            logInDataBase= new MVLogInDataBase();
            this.DataContext = logInDataBase;
            if(logInDataBase.GetPassowrd == null)
            {
                logInDataBase.GetPassowrd = new Action(SendPasswordToClass);
            }
            if (logInDataBase.CloseWindow == null)
            {
                logInDataBase.CloseWindow = new Action(this.Close);
            }

        }
        public void SendPasswordToClass()
        {
            logInDataBase.Password = Password.Password;
        }
        

     
    }
}
