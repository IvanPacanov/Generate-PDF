using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Generator_PDF.MySqlClass;
using Generator_PDF.DateTimeConvert;
using Prism.Commands;
using iTextSharp.text.pdf;
using System.Windows;
using Generator_PDF.GenerateChart;
using System.Threading;

namespace Generator_PDF.VM
{
    class MVGeneratePDF : BaseViewClass
    {
        #region fields
        public static Dictionary<int, List<List<IdParking>>> parkings;

        public static Dictionary<object, List<PdfPTable>> PDfTableDictionary;

        private static ConnectionMySql connectionSql;

        private NumberOfCarPark numberOfCarPark;

        public static Dictionary<object, System.Drawing.Image> images;

        public Func<Operation> SelectedOperationListBox { get; set; }
        public Func<Operation> RemoveOperationListBox { get; set; }
        public Action HideWindow { get; set; }
        public static Action ShowWindow { get; set; }

        public static List<IdParking> idParkings;
        public ObservableCollection<Operation> operationsListBox { get; set; }
        public ObservableCollection<Operation> possiblyOperationsListBox { get; set; }


        public static Nullable<DateTime> availableFrom;
        public Nullable<DateTime> AvailableFrom
        {
            get { return availableFrom; }
            set
            {
                availableFrom = value;
                this.NotifyPropertyChanged("AvailableFrom");

            }
        }

        public static Nullable<DateTime> availableTo;
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



        static List<Thread> threadsList;

        public static int counteOfChartr;
        public ICommand GeneratePdfButtonCommand { get; private set; }
        public ICommand RemoveButtonCommand { get; private set; }
        public ICommand AddOperationButtonCommand { get; private set; }

        #endregion

        public MVGeneratePDF()
        {


            counteOfChartr = 0;
            idParkings = new List<IdParking>();
            PDfTableDictionary = new Dictionary<object, List<PdfPTable>>();

            possiblyOperationsListBox = GeneratePossiblyOperation.CreatePossiblyOperation();
            operationsListBox = new ObservableCollection<Operation>();
            CloseButtonCommand = new DelegateCommand(CloseApp);
            AddOperationButtonCommand = new DelegateCommand(AddOperation);
            RemoveButtonCommand = new DelegateCommand(RemoveOperation);
            GeneratePdfButtonCommand = new DelegateCommand(PdfGenerate);

        }


        private void PdfGenerate()
        {

            counteOfChartr = 0;
            threadsList = new List<Thread>();
            images = new Dictionary<object, System.Drawing.Image>();
            parkings = new Dictionary<int, List<List<IdParking>>>();
            PDfTableDictionary = new Dictionary<object, List<PdfPTable>>();
            try
            {

                connectionSql.ToTime = availableTo.ConvertToUnixTimestamp();
                connectionSql.FromTime = availableFrom.ConvertToUnixTimestamp();
                if ((((availableTo.Value.Year - availableFrom.Value.Year) * 12) + availableTo.Value.Month - availableFrom.Value.Month) >= 12)
                {
                    throw new Exception("NIe można wygenerować raportu większego niż 1 rok");
                }
            }

            catch
            {
                MessageBox.Show("Nie wybrano daty, lub data jest większa niż 1 rok");
                return;
            }


            Thread thread = new Thread(WaitForAllChart);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();

            MessageBox.Show("Proces może potrwać pare minut!");

            foreach (var item in operationsListBox)
            {
                if (item.operation == "Ilość pojazdów")
                {
                    Task.Run(async () =>
                    {
                        threadsList.Add(new Thread(ChartSelect.TheSumOfVehicles) { Name = "1" });
                        await connectionSql.numberVehicle(idParkings, 1);
                    }).Wait();
                }

                if (item.operation == "Ilość wzbudzeń")
                {
                    Task.Run(async () =>
                    {
                        threadsList.Add(new Thread(ChartSelect.TheSumOfVehiclesInMonth) { Name = "2" });
                        await connectionSql.numberVehicleGroupBy(idParkings, GroupBy.MONTH, 2);
                    }).Wait();
                }



                if (item.operation == "Ilość wzbudzeń procentowe")
                {
                    Task.Run(async () =>
                    {

                        threadsList.Add(new Thread(ChartSelect.TheSumOfVehiclesInMonthPercent) { Name = "3" });
                        await connectionSql.numberVehicleGroupBy(idParkings, GroupBy.MONTH, 3);
                    }).Wait();
                }
                if (item.operation == "Ilośd wzbudzeń na parkingu w danym miesiącu")
                {
                    Task.Run(async () =>
                    {
                        threadsList.Add(new Thread(ChartSelect.TheSumOfVehiclesInMonthPercentOnCarPark) { Name = "4" });
                        await connectionSql.numberVehicleGroupBy(idParkings, GroupBy.MONTH, 4);
                    }).Wait();
                }

                if (item.operation == "Zestawienie godzinowe")
                {
                    Task.Run(async () =>
                    {
                        threadsList.Add(new Thread(ChartSelect.HourlySummary) { Name = "5" });
                        await connectionSql.numberVehicleGroupBy(idParkings, GroupBy.HOUR, 5);
                    }).Wait();

                }
                if (item.operation == "Zestawienie godzinowe dla każdego parkingu")
                {
                    Task.Run(async () =>
                    {
                        threadsList.Add(new Thread(ChartSelect.HourlySummaryForEachParking) { Name = "6" });
                        await connectionSql.numberVehicleGroupBy(idParkings, GroupBy.HOUR, 6);
                    }).Wait();

                }
                if (item.operation == "Zestawienie godzinowe dla każdego parkingu wzgledem miesięcy")
                {
                    Task.Run(async () =>
                    {
                        threadsList.Add(new Thread(ChartSelect.HourlySummaryForEachParkingByMonth) { Name = "7" });
                        await connectionSql.numberVehicleGroupByInEachHoursOfMonth(idParkings, 7);
                    }).Wait();
                }
            }
        }
        public static void StartTask(Thread thread)
        {
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }


        public static void OnAddListParkings(int key, List<List<IdParking>> idParkings, ListIdParking imageArgs)
        {
            parkings.Add(key, idParkings);
            StartTask(threadsList.Find(x => x.Name == $"{key}"));
        }

        public static void OnAddCurrentImage(System.Drawing.Image image, AbstractChart chart, ImageArgs imageArgs)
        {
            images.Add(chart, image);
        }

        public static void OnAddCurrentCarParksObservableCollection(ObservableCollection<IdParking> CarParkss, CarParksArgs carParksArgs)
        {
            idParkings = CarParkss.ToList();
            ShowWindow();
        }
        public void GetIdParking()
        {
            numberOfCarPark = new NumberOfCarPark();
            numberOfCarPark.CarParksChoice.possiblyParkingListBox = new ObservableCollection<IdParking>();
            numberOfCarPark.CarParksChoice.possiblyParkingListBox = connectionSql.GetCarParks();
            numberOfCarPark.Show();
        }



        public void RemoveOperation()
        {
            if (RemoveOperationListBox() != null)
                operationsListBox.Remove(RemoveOperationListBox());
        }

        public void AddOperation()
        {
            if (SelectedOperationListBox() != null)
            {
                if (!operationsListBox.Any(x => x.operation == SelectedOperationListBox().operation))
                    operationsListBox.Add(SelectedOperationListBox());
                else
                    MessageBox.Show("Element widnieje na liście!");
            }
        }

        public void WaitForAllChart()
        {
            while (operationsListBox.Count != counteOfChartr)
            {

            }
            CreatePDF.GeneratePDF(images, PDfTableDictionary);
        }
    }
}