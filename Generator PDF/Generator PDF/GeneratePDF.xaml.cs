using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Generator_PDF.VM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LiveCharts;
using LiveCharts.Wpf;
using Image = iTextSharp.text.Image;
using Separator = LiveCharts.Wpf.Separator;

namespace Generator_PDF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GeneratePDF : Window
    {
        MVGeneratePDF mVGeneratePDF;
        ListBox itemToAdd;
        ListBox itemToRemove;

        public CartesianChart MyTestChart;
       
        public SeriesCollection MySeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public string AxisTitle { get; set; }
        public Func<double, string> YFormatter { get; set; }
        //  public Axis Axis1, Axis2, Axis3, Axis4, Axis5, Axis6, Axis7, Axis8, Axis9, Axis10, AxisXChart;

        public GeneratePDF()
        {
            InitializeComponent();
        



     



            mVGeneratePDF = new MVGeneratePDF();
            this.DataContext = mVGeneratePDF;
            if (mVGeneratePDF.SelectedOperationListBox == null)
            {
                mVGeneratePDF.SelectedOperationListBox = new Func<Operation>(GetSecelectedItemToAdd);
            }
            if (mVGeneratePDF.RemoveOperationListBox == null)
            {
                mVGeneratePDF.RemoveOperationListBox = new Func<Operation>(GetSecelectedItemToRemove);
            }
            if (mVGeneratePDF.HideWindow == null)
            {
                mVGeneratePDF.HideWindow = new Action(this.Hide);
            }
            if (MVGeneratePDF.ShowWindow == null)
            {
                MVGeneratePDF.ShowWindow = new Action(this.Show);
            }

            mVGeneratePDF.GetIdParking();
        }
        private void lstBox_SelectionChanget(object sender, SelectionChangedEventArgs eventArgs)
        {
            itemToAdd = (ListBox)sender;
           
        }
        private void lstBox_SelectionChangetRemove(object sender, SelectionChangedEventArgs eventArgs)
        {
            itemToRemove = (ListBox)sender;

        }
        
        private Operation GetSecelectedItemToAdd()
        {
            try
            {
                return ((Operation)itemToAdd.SelectedItem);
            }
            catch
            {
                MessageBox.Show("Proszę zaznaczyć element z listy");
                return null;
            }
        }
        private Operation GetSecelectedItemToRemove()
        {
            try
            {
                return ((Operation)itemToRemove.SelectedItem);
            }
            catch
            {
                MessageBox.Show("Proszę zaznaczyć element z listy");
                return null;
            }
        }


    
       

     
    }
}
