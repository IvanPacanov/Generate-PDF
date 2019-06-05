using System;
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
using LiveCharts;
using LiveCharts.Wpf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Media;
using Image = iTextSharp.text.Image;
using Separator = LiveCharts.Wpf.Separator;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace Generator_PDF.VM
{
    class MVGeneratePDF : BaseViewClass
    {
      static  ConnectionMySql connectionSql;
        CartesianChart MyTestChart;
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

        int[] ilosc;
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
            ilosc = new int[CarParks.Count];
            foreach (var item in operationsListBox)
            {
                if(item.operation== "Ilość Pojazdów")
                {
                    Task.Run(async () =>
                    {
                        for (int i = 0; i < CarParks.Count; i++)
                        {
                            ilosc[i]= await connectionSql.numberVehicle(CarParks[i].idParking);
                        }
                    }).Wait();
                }
            }
            int z = 1;
            pdf();
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
       
        private void pdf()
        {
            SeriesCollection MySeriesCollection = new SeriesCollection();


             MyTestChart = new CartesianChart
            {
                DisableAnimations = true,
                Width = 800,
                Height = 350,
                Name = "Wykres",
                Tag = "EEEEEE",

                Series = MySeriesCollection

            };

            MyTestChart.LegendLocation = LegendLocation.Right;

            for (int i = 0; i < CarParks.Count; i++)
            {


                MyTestChart.Series.Add(new ColumnSeries
                {
                    Title = CarParks[i].idParking.ToString(),
                    FontSize = 20,
                    Width = 5,
                    Values = new ChartValues<double> { ilosc[i] }


                });
            }
       

            MyTestChart.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            MyTestChart.FontSize = 20;

            MyTestChart.FontFamily = new FontFamily("Segoe UI Black");
            MyTestChart.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));


            MyTestChart.AxisY.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.DodgerBlue,
                FontSize = 20,
                Title = "Blue Axis"
            });
            MyTestChart.AxisX.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.IndianRed,
                Title = "Red Axis",
                FontSize = 20,

            });
            TakeTheChart();
        }

        public void TakeTheChart()
        {
            var viewbox = new Viewbox();
            viewbox.Child = MyTestChart;
            viewbox.Measure(MyTestChart.RenderSize);
            viewbox.Arrange(new Rect(new Point(0, 0), MyTestChart.RenderSize));
            MyTestChart.Update(true, true); //force chart redraw
            viewbox.UpdateLayout();

            SaveToPng(MyTestChart, "Chart.png");
            //png file was created at the root directory.
        }

        public void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            System.Drawing.Bitmap cos;
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                cos = new System.Drawing.Bitmap(stream);
            }
            System.Drawing.Image i = (System.Drawing.Image)cos;
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(document, new FileStream("Chart.pdf", FileMode.Create));
            document.Open();
            document.Add(new iTextSharp.text.Paragraph("????"));
            var iamge = Image.GetInstance(i, BaseColor.BLACK);
            iamge.ScalePercent(55f);
            iamge.SetAbsolutePosition((PageSize.A4.Width - iamge.ScaledWidth) / 2, (PageSize.A4.Height - iamge.ScaledHeight) / 2);

            document.Add(iamge);
            document.Close();


        }


    }
}
