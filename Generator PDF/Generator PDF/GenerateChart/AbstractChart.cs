using Generator_PDF.VM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = iTextSharp.text.Image;
using Separator = LiveCharts.Wpf.Separator;

namespace Generator_PDF.GenerateChart
{
    abstract class AbstractChart
    {
        public CartesianChart chart;


        System.Drawing.Bitmap imageChart = null;

        public abstract SeriesCollection GeneerateSeries();

        public virtual Axis SetAxisX()
        {
            return new Axis()
            {
                ShowLabels = false,
                Foreground = System.Windows.Media.Brushes.IndianRed,
                FontSize = 20
            };
        }


        public virtual void GeneerateChart()
        {
            SetChartParameters();
            chart.AxisX.Add(SetAxisX());


            TakeTheChart();
        }



        public void SetChartParameters()
        {
            chart.DisableAnimations = true;

            chart.Width = 600;
            chart.Height = 300;
            chart.Series = GeneerateSeries();
          

            if (this is LineChart)
            {
                chart.AxisY.Add(new Axis
                {

                    LabelFormatter = value => string.Format("{0:0.00}%", value),
                    Foreground = Brushes.Black,
                    FontSize = 15,
                    Separator = new Separator() { Stroke = Brushes.Black },
                    Title = "% Pojazdów"

                });
            }
            else
            {

                chart.AxisY.Add(new Axis
                {


                    Foreground = Brushes.Black,
                    FontSize = 15,
                    Separator = new Separator() { Stroke = Brushes.Black },


                });
            }
            if (this is SumOfParkedInEachMonthPercent)
            {

            }
            else
            {
                chart.LegendLocation = LegendLocation.Right;
            }
            chart.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            chart.FontSize = 20;
            chart.ChartLegend.FontSize = 15;

            chart.FontFamily = new FontFamily("Segoe UI Black");
            chart.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));

        }

        public void TakeTheChart()
        {
            var viewbox = new Viewbox();
            viewbox.Child = chart;
            viewbox.Measure(chart.RenderSize);
            viewbox.Arrange(new Rect(new Point(0, 0), chart.RenderSize));
            chart.Update(true, true); //force chart redraw
            viewbox.UpdateLayout();
            //      GenerateTAble(chart);
            SaveToPng(chart, chart.Tag.ToString());
            //png file was created at the root directory.
        }

        public void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            AddCurrentImage += MVGeneratePDF.OnAddCurrentImage;
            System.Drawing.Bitmap variableOfImage;
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                variableOfImage = new System.Drawing.Bitmap(stream);
                variableOfImage.Tag = $"{fileName}";
            }
            imageChart = variableOfImage;
            OnAddCurrentCarParks(imageChart,this);
       

        }


        public double[] SortByCount(List<IdParking> idParkings)
        {
            double[] sorted = new double[48];
            int count = idParkings.Count;
            for (int i = 0; i <= 23; i++)
            {
                if (count != 24)
                {
                    sorted[i] = 0;
                    count++;
                }
                else
                {
                    sorted[i] = idParkings[i - (count - idParkings.Count)].count;
                }
            }
            int counter = 23;
            for (int i = 0; i < sorted.Length; i++)
            {
                if (i > 0)
                {
                    if (sorted[i] == sorted[i - 1] && i < idParkings.Count)
                    {
                        double help = sorted[i];
                        sorted[i] = sorted[i + 1];
                        sorted[i + 1] = help;
                    }
                    else if (i >= sorted.Length / 2)
                    {
                        sorted[i] = sorted[counter--];
                    }
                }
            }
            return sorted;
        }



        public delegate void GetImageHandler(System.Drawing.Bitmap image, AbstractChart chart, ImageArgs imageArgs);
        public event GetImageHandler AddCurrentImage;

        protected virtual void OnAddCurrentCarParks(System.Drawing.Bitmap imagechar, AbstractChart Chart)
        {
            if (AddCurrentImage != null)
            {
                AddCurrentImage(imageChart,this, new ImageArgs() { image = imagechar });
            }
        }

    }
   
}