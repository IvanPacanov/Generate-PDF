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
        MVCarParksChoice carParksChoice;
        public NumberOfCarPark()
        {
            InitializeComponent();
            carParksChoice = new MVCarParksChoice();
            this.DataContext = carParksChoice;
            if (carParksChoice.SetCarParks == null)
            {
                carParksChoice.SetCarParks = new Action(SetCarPark);
            }
            AddCurrentCarParks += MVGeneratePDF.OnAddCurrentDevice;
        }
        public void SetCarPark()
        {
            OnAddCurrentCarParks(carParksChoice.carParkingListBox);
            Close();
        }

        public delegate void SetCarParksHandler(ObservableCollection<IdParking> CarParks, CarParksArgs carParksArgs);
        public event SetCarParksHandler AddCurrentCarParks;

        protected virtual void OnAddCurrentCarParks(ObservableCollection<IdParking> CurrentCarParks)
        {
            if (AddCurrentCarParks != null)
            {
                AddCurrentCarParks(carParksChoice.carParkingListBox, new CarParksArgs() { CarParks = CurrentCarParks });
            }
        }
    }

    }

