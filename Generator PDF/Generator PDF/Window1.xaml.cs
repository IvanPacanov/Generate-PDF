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
    public partial class Window1 : Window
    {
        public CartesianChart MyTestChart;
       
        public SeriesCollection MySeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public string AxisTitle { get; set; }
        public Func<double, string> YFormatter { get; set; }
      //  public Axis Axis1, Axis2, Axis3, Axis4, Axis5, Axis6, Axis7, Axis8, Axis9, Axis10, AxisXChart;

        public Window1()
        {
            InitializeComponent();
            LoadBarChartData();
            MySeriesCollection = new SeriesCollection();
            

            MyTestChart = new CartesianChart
            {
                DisableAnimations = true,
                Width = 800,
                Height = 350,
                              Series = MySeriesCollection
                
            };

            MyTestChart.LegendLocation = LegendLocation.Right;
           
            MyTestChart.Series.Add(new ColumnSeries
            {
                Title = "2016",
                FontSize = 20,
                Width = 5,
                Values = new ChartValues<double> { 11, 56 }
                

            });
            MyTestChart.Series.Add(new ColumnSeries
            {
                Title = "2015",
              FontSize = 20,
              Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 250, 140)),
            Values = new ChartValues<double> { 10, 50 }
            });

            MyTestChart.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            MyTestChart.FontSize = 20;
           
            MyTestChart.FontFamily= new FontFamily("Segoe UI Black");
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
        private void LoadBarChartData()
        {
            /*       ((BarSeries)mcChart.Series[0]).ItemsSource =
                       new KeyValuePair<string, int>[]{
                   new KeyValuePair<string, int>("Project Manager", 12),
                   new KeyValuePair<string, int>("CEO", 25),
                   new KeyValuePair<string, int>("Software Engg.", 5),
                   new KeyValuePair<string, int>("Team Leader", 6),
                   new KeyValuePair<string, int>("Project Leader", 10),
                   new KeyValuePair<string, int>("Developer", 4) };
           */
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
