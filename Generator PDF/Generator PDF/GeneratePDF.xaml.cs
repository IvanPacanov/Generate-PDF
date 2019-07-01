using System;
using System.Windows;
using System.Windows.Controls;
using Generator_PDF.VM;
using LiveCharts.Wpf;


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
        }

        public void GetIdParking()
        {
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
