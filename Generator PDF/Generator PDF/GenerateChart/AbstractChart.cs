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

        //    public SeriesCollection seriesCollection;
        System.Drawing.Bitmap imageChart = null;

        public abstract SeriesCollection GeneerateSeries();

        public virtual Axis SetAxisX()
        {
            return new Axis()
            {
                Separator = new Separator() { Step = 1, IsEnabled = false },
                Foreground = System.Windows.Media.Brushes.IndianRed,
                FontSize = 20
            };
        }


        public virtual void GeneerateChart(string AxisYTitle)
        {
            chart.AxisX.Add(SetAxisX());
            //   chart.AxisX.Add(SetAxisX());           
            SetChartParameters(AxisYTitle);
            TakeTheChart();
        }



        public void SetChartParameters(string AxisYTitle)
        {
            chart.DisableAnimations = true;

            chart.Width = 800;
            chart.Height = 350;
            chart.Series = GeneerateSeries();



            chart.AxisY.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.DodgerBlue,
                FontSize = 20,

                Title = AxisYTitle
            });

            chart.LegendLocation = LegendLocation.Right;

            chart.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            chart.FontSize = 20;

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

        private void GenerateTAble(CartesianChart chart)
        {
            SeriesCollection z = chart.Series;
            IChartValues chartValues = z[0].Values;
            AxesCollection axisX = chart.AxisX;


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
            OnAddCurrentCarParks(imageChart);

        }

        public delegate void GetImageHandler(System.Drawing.Bitmap image, ImageArgs imageArgs);
        public event GetImageHandler AddCurrentImage;

        protected virtual void OnAddCurrentCarParks(System.Drawing.Bitmap imagechar)
        {
            if (AddCurrentImage != null)
            {
                AddCurrentImage(imageChart, new ImageArgs() { image = imagechar });
            }
        }

        public static void GeneratePDF(List<System.Drawing.Image> images, Dictionary<int, List<PdfPTable>> pdfPTables) //, List<IdParking> listcarParks, List<List<IdParking>> listListCarParks, List<List<IdParking>> listListCarParksPercent)
        {
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            List<System.Drawing.Image> vs = new List<System.Drawing.Image>();

        
                try
                {
                    PdfWriter.GetInstance(document, new FileStream("Chap0101.pdf", FileMode.Create));
                }
                catch
                {
                    PdfWriter.GetInstance(document, new FileStream("Chap0101.pdf", FileMode.Create));
                }
          
        
            document.Open();
            foreach (var item in pdfPTables)
            {

                images.OrderBy(x => x.Tag);

                foreach (var image in images)
                {

                    if ( !vs.Contains(image))
                    {
                        if(int.Parse(image.Tag.ToString()) == 5)
                        {
                            iTextSharp.text.Image pivc = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);

                            if (pivc.Height > pivc.Width)
                            {
                                //Maximum height is 800 pixels.
                                float percentage = 0.0f;
                                percentage = 700 / pivc.Height;
                                pivc.ScalePercent(percentage * 100);
                            }
                            else
                            {
                                //Maximum width is 600 pixels.
                                float percentage = 0.0f;
                                percentage = 540 / pivc.Width;
                                pivc.ScalePercent(percentage * 100);
                            }

                            //   pic.Border = iTextSharp.text.Rectangle.BOX;
                            //  pic.BorderColor = iTextSharp.text.BaseColor.BLACK;
                            //     pic.BorderWidth = 3f;

                            document.Add(pivc);
                            vs.Add(image);
                            Paragraph paragrapha = new Paragraph();
                            paragrapha.SetLeading(1.0f, 3.0f);
                            break;
                        }

                        
                            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);

                            if (pic.Height > pic.Width)
                            {
                                //Maximum height is 800 pixels.
                                float percentage = 0.0f;
                                percentage = 700 / pic.Height;
                                pic.ScalePercent(percentage * 100);
                            }
                            else
                            {
                                //Maximum width is 600 pixels.
                                float percentage = 0.0f;
                                percentage = 540 / pic.Width;
                                pic.ScalePercent(percentage * 100);
                            }

                            //   pic.Border = iTextSharp.text.Rectangle.BOX;
                            //  pic.BorderColor = iTextSharp.text.BaseColor.BLACK;
                            //     pic.BorderWidth = 3f;

                            document.Add(pic);
                            vs.Add(image);
                            Paragraph paragraph = new Paragraph();
                            paragraph.SetLeading(1.0f, 3.0f);

                            document.Add(paragraph);
                            paragraph = new Paragraph();
                            paragraph.SetLeading(1.0f, 3.0f);

                        
                        int j = 0;
                        double b = double.Parse(image.Tag.ToString());
                        while (j != 100)
                        {
                            if (b == item.Key)
                            {
                                break;
                            }
                            j++;
                            b = b + b;
                        }
                        if (b == item.Key )
                            for (int i = 0; i < item.Value.Count; i++)
                            {
                                paragraph = new Paragraph();
                                paragraph.Add("Dokładne Dane:");
                                paragraph.SetLeading(1.0f, 3.0f);
                                paragraph.Add(pdfPTables[item.Key][i]);


                                document.Add(paragraph);
                            }
                        break;
                    }
                }



                document.NewPage();

            }
            try
            {
                document.Close();
            }
            catch
            {

            }
            MessageBox.Show("PDF Wygenerowany");
        }

        public PdfPTable Createtable(List<IdParking> listcarParks)
        {

            PdfPTable table = new PdfPTable(listcarParks.Count);

            foreach (var item in listcarParks)
            {
                table.AddCell(item.name);
            }
            foreach (var item in listcarParks)
            {
                table.AddCell(item.count.ToString());
            }
            //  8//      table.SpacingAfter = 30f;
            //       table.SpacingBefore = 30f;


            return table;
        }
        public void Createtable(List<List<IdParking>> listListCarParks)
        {
            foreach (var item in listListCarParks)
            {


                PdfPTable table = new PdfPTable(item.Count);

                foreach (var park in item)
                {
                    table.AddCell(park.GrupuByTime.ToString());
                }
                foreach (var park in item)
                {
                    table.AddCell(park.count.ToString());
                }
                //        table.SpacingAfter =30f;
                //         table.SpacingBefore = 3f;
                table.AddCell(new Phrase("COOOO"));
                //   MVGeneratePDF.pdfPTables.Add(table);
            }
        }


    }
}