using Generator_PDF.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Generator_PDF
{
    /// <summary>
    /// Interaction logic for NumberOfCarPark.xaml
    /// </summary>
    public partial class NumberOfCarPark : Window
    {

        private   MVCarParksChoice carParksChoice;
        internal MVCarParksChoice CarParksChoice { get => carParksChoice; set => carParksChoice = value; }

        ListBox itemToAdd;
        ListBox itemToRemove;
       
        public NumberOfCarPark()
        {
            InitializeComponent();
            carParksChoice = new MVCarParksChoice();
            this.DataContext = CarParksChoice;

            if (carParksChoice.SetCarParks == null)
            {
                carParksChoice.SetCarParks = new Action(SetCarPark);
            }
            if(carParksChoice.SelectedCarParksListBox==null)
            {
                carParksChoice.SelectedCarParksListBox = new Func<IdParking>(GetSecelectedItemToAdd);

            }
            if (carParksChoice.RemoveCarParksListBox == null)
            {
                carParksChoice.RemoveCarParksListBox = new Func<IdParking>(GetSecelectedItemToRemove);

            }

            AddCurrentCarParks += MVGeneratePDF.OnAddCurrentDevice;
        }
        public void SetCarPark()
        {
            OnAddCurrentCarParks(CarParksChoice.carParksListBox);
            Close();
        }
        private void lstBox_SelectionChanget(object sender, SelectionChangedEventArgs eventArgs)
        {
            itemToAdd = (ListBox)sender;

        }
        private void lstBox_SelectionChangetRemove(object sender, SelectionChangedEventArgs eventArgs)
        {
            itemToRemove = (ListBox)sender;

        }
        private IdParking GetSecelectedItemToAdd()
        {
            try
            {
                return ((IdParking)itemToAdd.SelectedItem);
            }
            catch
            {
                MessageBox.Show("Proszę zaznaczyć element z listy");
                return null;
            }
        }
        private IdParking GetSecelectedItemToRemove()
        {
            try
            {
                return ((IdParking)itemToRemove.SelectedItem);
            }
            catch
            {
                MessageBox.Show("Proszę zaznaczyć element z listy");
                return null;
            }
        }



        public delegate void SetCarParksHandler(ObservableCollection<IdParking> CarParks, CarParksArgs carParksArgs);
        public event SetCarParksHandler AddCurrentCarParks;
        protected virtual void OnAddCurrentCarParks(ObservableCollection<IdParking> CurrentCarParks)
        {
            if (AddCurrentCarParks != null)
            {
                AddCurrentCarParks(CarParksChoice.carParksListBox, new CarParksArgs() { CarParks = CurrentCarParks });
            }
        }
    }

    }

