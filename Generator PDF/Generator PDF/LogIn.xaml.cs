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
        LogInDataBase logInDataBase;
        public LogIn()
        {
            InitializeComponent();
            logInDataBase= new LogInDataBase();
            this.DataContext = logInDataBase;
            if(logInDataBase.GetPassowrd == null)
            {
                logInDataBase.GetPassowrd = new Action(SendPasswordToClass);
            }
         
        }
        public void SendPasswordToClass()
        {
            logInDataBase.Password = Password.Password;
        }
        

     
    }
}
