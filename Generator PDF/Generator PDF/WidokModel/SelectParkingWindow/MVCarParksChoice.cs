using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;

namespace Generator_PDF.VM
{
    class MVCarParksChoice : BaseViewClass
    {

        public Func<IdParking> SelectedCarParksListBox { get; set;}

        public Func<IdParking> RemoveCarParksListBox { get; set; }
        
        public ObservableCollection<IdParking> carParksListBox { get; set; }

        public ObservableCollection<IdParking> possiblyParkingListBox { get; set; }

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
        public ICommand ContinuePdfButtonCommand { get; set; }

        public ICommand RemoveCarParkButtonCommand { get; set; }
        public ICommand AddCarParkButtonCommand { get; set; }
        public MVCarParksChoice()
        {
            ContinuePdfButtonCommand = new DelegateCommand(ButtonCarPark);
            CloseButtonCommand = new DelegateCommand(CloseApp);
            AddCarParkButtonCommand = new DelegateCommand(AddCarPark);
            RemoveCarParkButtonCommand = new DelegateCommand(RemoveCarPark);
            carParksListBox = new ObservableCollection<IdParking>();
       

        }

        public void RemoveCarPark()
        {
            if (RemoveCarParksListBox() != null)
                carParksListBox.Remove(RemoveCarParksListBox());
        }

        public  void AddCarPark()
        {
            if (SelectedCarParksListBox() != null)
            {
                if (!carParksListBox.Any(x => x.name == SelectedCarParksListBox().name))
                    carParksListBox.Add(SelectedCarParksListBox());
                else
                    MessageBox.Show("Elemento widnieje na liście!");
            }

        }

       
        private void ButtonCarPark()
        {
            SetCarParks();
        }
    }
}
