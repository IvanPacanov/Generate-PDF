using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            selectList.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickAdd);
            mainList.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickRemove);

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

        private void DoubleClickRemove(object sender, MouseButtonEventArgs e)
        {
            mVGeneratePDF.RemoveOperation();
        }

        private void DoubleClickAdd(object sender, EventArgs e)
        {
            mVGeneratePDF.AddOperation();
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
