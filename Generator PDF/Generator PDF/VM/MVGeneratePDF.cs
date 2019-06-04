﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Generator_PDF.MySqlClass;
using Prism.Commands;

namespace Generator_PDF.VM
{
    class MVGeneratePDF : BaseViewClass
    {
      static  ConnectionMySql connectionSql;

        NumberOfCarPark numberOfCarPark;
        public Func<Operation> SelectedOperationListBox { get; set;}
        public Action HideWindow { get; set; }

        public static Action ShowWindow { get; set; }

        public Func<Operation> RemoveOperationListBox { get; set; }
        public ObservableCollection<Operation> operationsListBox { get;  set; }

        public static ObservableCollection<IdParking> CarParks { get; set; }

        public ObservableCollection<Operation> possiblyOperationsListBox { get; set; }

        private Nullable<DateTime> availableFrom;
        public Nullable<DateTime> AvailableFrom
        {
            get { return availableFrom; }
            set
            {
                availableFrom = value;
                this.NotifyPropertyChanged("AvailableFrom");

            }
        }


        private Nullable<DateTime> availableTo;
        public Nullable<DateTime> AvailableTo
        {
            get { return availableTo; }
            set
            {
                availableTo = value;
                this.NotifyPropertyChanged("AvailableTo");

            }
        }

        public static void OnAddCurrentCarParks(ConnectionMySql connectionMySql, ConnectionMySqlArgs carParksArgs)
        {
            connectionSql = connectionMySql;
        }






        public ICommand GeneratePdfButtonCommand { get; private set; }
        public ICommand RemoveButtonCommand { get; private set; }
        public ICommand AddOperationButtonCommand { get; private set; }
      
        public MVGeneratePDF()
        {
         

            possiblyOperationsListBox = GeneratePossiblyOperation.CreatePossiblyOperation();
            operationsListBox = new ObservableCollection<Operation>();
            CloseButtonCommand = new DelegateCommand(CloseApp);
            AddOperationButtonCommand = new DelegateCommand(AddOperation);
            RemoveButtonCommand = new DelegateCommand(RemoveOperation);
            GeneratePdfButtonCommand = new DelegateCommand(PdfGenerate);
                                                  
        }
        private void PdfGenerate()
        {
            throw new NotImplementedException();
        }

        public static void OnAddCurrentDevice(ObservableCollection<IdParking> CarParkss, CarParksArgs carParksArgs)
        {
            CarParks = CarParkss;
            ShowWindow();
            
        }
        public void GetIdParking()
        {
            numberOfCarPark = new NumberOfCarPark();
            numberOfCarPark.Show();
         
        }

   

        private void RemoveOperation()
        {
       

            if (RemoveOperationListBox() != null)
                operationsListBox.Remove(RemoveOperationListBox());
        }

        private void AddOperation()
        {
            if(SelectedOperationListBox()!=null)
            operationsListBox.Add(SelectedOperationListBox());

            }
       

      
    }
}
