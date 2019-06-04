using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;

namespace Generator_PDF.VM
{
    class MVCarParksChoice : BaseViewClass
    {
        public ObservableCollection<IdParking> carParkingListBox { get; set; }

        private string textIdParking;
        public string TextIdParking
        {
            get { return this.textIdParking; }
            set
            {
                int valu;
                if (int.TryParse(value, out valu))
                    this.textIdParking = valu.ToString();
                else
                    this.textIdParking = null;

                this.NotifyPropertyChanged("TextIdParking");

            }
        }
        public Action SetCarParks { get; set; }
        public ICommand ContinueParkingButtonCommand { get; set; }
        public ICommand AddParkingButtonCommand { get; set; }
        public MVCarParksChoice()
        {
            ContinueParkingButtonCommand = new DelegateCommand(ButtonCarPark);
            CloseButtonCommand = new DelegateCommand(CloseApp);
            AddParkingButtonCommand = new DelegateCommand(AddParking);
            carParkingListBox = new ObservableCollection<IdParking>();
            carParkingListBox.Add(new IdParking {  idParking = 50 });
        }

        private void AddParking()
        {
            if(textIdParking!=null)
            carParkingListBox.Add(new IdParking { idParking = int.Parse(textIdParking) });
        }
        private void ButtonCarPark()
        {
            SetCarParks();
        }
    }
}
