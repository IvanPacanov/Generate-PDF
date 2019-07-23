using Generator_PDF.VM;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Separator = LiveCharts.Wpf.Separator;


namespace Generator_PDF.GenerateChart
{
    abstract class AbstractChart
    {
        public CartesianChart chart;

        protected int numberOfMonths = ((MVGeneratePDF.availableTo.Value.Year - MVGeneratePDF.availableFrom.Value.Year) * 12) + MVGeneratePDF.availableTo.Value.Month - MVGeneratePDF.availableFrom.Value.Month;

        System.Drawing.Bitmap imageChart = null;

        public abstract SeriesCollection GeneerateSeries();





        public virtual void GeneerateChart()
        {
            SetChartParameters();
            chart.AxisX.Add(SetAxisX(Format.Normal));
            chart.AxisY.Add(SetAxisY(Format.Normal));
            TakeTheChart();
        }



        public virtual Axis SetAxisX(Format format)
        {
            switch (format)
            {
                case Format.Normal:
                    {
                        return new Axis()
                        {

                            Foreground = System.Windows.Media.Brushes.Black,
                            FontSize = 15,
                            IsMerged = false,
                            Separator = new Separator
                            {
                                Step = 1,
                                IsEnabled = false,
                                StrokeThickness = 0,
                                StrokeDashArray = new System.Windows.Media.DoubleCollection(0),
                                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                            }
                        };
                    }
                case Format.Percent:
                    {
                        return new Axis()
                        {

                            LabelFormatter = value => value.ToString("N"),
                            Foreground = System.Windows.Media.Brushes.Black,
                            FontSize = 15,
                            IsMerged = false,
                            Separator = new Separator
                            {
                                IsEnabled = false,
                                StrokeThickness = 0,
                                StrokeDashArray = new System.Windows.Media.DoubleCollection(0),
                                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                            }
                        };
                    }
                default:
                    {
                        return null;
                    }
            }
        }


        public virtual Axis SetAxisY(Format format)
        {
            switch (format)
            {
                case Format.Normal:
                    {
                        return new Axis
                        {

                         //   LabelFormatter = value => string.Format("{0:0.00}%", value),
                            Foreground = Brushes.Black,
                            FontSize = 15,
                            MinValue = 0,
                            IsMerged = false,
                            Separator = new Separator
                            {
      //                          Step = 1.5,
                                StrokeThickness = 1.5,
                                StrokeDashArray = new System.Windows.Media.DoubleCollection(4),
                                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                            }

                        };
                    }
                case Format.Percent:
                    {
                        return new Axis
                        {

                            LabelFormatter = value => string.Format("{0:0.00}%", value),
                            Foreground = Brushes.Black,
                            FontSize = 15,
                            IsMerged = false,

                            Separator = new Separator
                            {

                         //       StrokeThickness = 1.5,
                                StrokeDashArray = new System.Windows.Media.DoubleCollection(4),
                                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                            }


                        };
                    }
                default:
                    {
                        return null;
                    }

            }
        }
        public void SetChartParameters()
        {
            chart.DisableAnimations = true;
            chart.Width = 720;
            chart.Height = 360;
            chart.Series = GeneerateSeries();

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
            chart.Update(true, true);
            viewbox.UpdateLayout();
            SaveToPng(chart, chart.Tag.ToString());

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
            OnAddCurrentCarParks(imageChart, this);


        }

        public void FillMissingMonths(List<IdParking> months)
        {
            int numberOfMonths = ((MVGeneratePDF.availableTo.Value.Year - MVGeneratePDF.availableFrom.Value.Year) * 12) + MVGeneratePDF.availableTo.Value.Month - MVGeneratePDF.availableFrom.Value.Month;
            for (int i = 0; i < numberOfMonths; i++)
            {
                if (MVGeneratePDF.availableFrom.Value.Month != months[0].month && MVGeneratePDF.availableFrom.Value.Year != months[0].year)
                {
                    months.Insert(0, new IdParking() { month = MVGeneratePDF.availableFrom.Value.Month, count = 0, year = MVGeneratePDF.availableFrom.Value.Year });
                }
                else if (months[i - 1].month != months[i].month - 1 && months[i].month != 12)
                {
                    months.Insert(i, new IdParking() { month = months[i - 1].month + 1, count = 0, year = months[i - 1].year });
                }
                else if (months[i - 1].month != months[i].month - 1 && months[i - 1].month == 12)
                {
                    months.Insert(i, new IdParking() { month = 1, count = 0, year = months[i - 1].year + 1 });
                }
            }
        }
        public List<IdParking> SortByCount(List<IdParking> idParkings)
        {
            for (int i = 0; i <= 23; i++)
            {
                if (!idParkings.Exists(x => x.hours == i))
                {
                    idParkings.Add(new IdParking() { hours = i, count = 0 });
                }
            }
            int counter = 1;
            while (idParkings.Count != 48)
            {
                if (counter < 24)
                {
                    if (idParkings[counter].count == idParkings[counter - 1].count)
                    {
                        var help = idParkings[counter - 1];
                        idParkings[counter - 1] = idParkings[counter];
                        idParkings[counter] = help;
                    }
                }
                else
                {
                    for (int i = 23; i >= 0; i--)
                    {
                        idParkings.Add(idParkings.ElementAt(i));
                    }
                }
                counter++;
            }
            return idParkings;
        }





        public delegate void GetImageHandler(System.Drawing.Bitmap image, AbstractChart chart, ImageArgs imageArgs);
        public event GetImageHandler AddCurrentImage;

        protected virtual void OnAddCurrentCarParks(System.Drawing.Bitmap imagechar, AbstractChart Chart)
        {
            if (AddCurrentImage != null)
            {
                AddCurrentImage(imageChart, this, new ImageArgs() { image = imagechar });
            }
        }

    }

}