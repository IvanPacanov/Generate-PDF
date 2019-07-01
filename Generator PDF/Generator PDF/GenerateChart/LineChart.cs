using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator_PDF.VM;
using iTextSharp.text.pdf;
using LiveCharts;
using LiveCharts.Wpf;

namespace Generator_PDF.GenerateChart
{
    class LineChart : AbstractChart
    {
        List<List<IdParking>> listListCarParks;
        public List<PdfPTable> pdfTablelist;
      
        public LineChart(List<List<IdParking>> listListCarParks, string tag)
        {
            pdfTablelist = new List<PdfPTable>();
            pdfTablelist.Add(new PdfPTable(12));
            pdfTablelist.Add(new PdfPTable(12));
            pdfTablelist.Add(new PdfPTable(12));
            pdfTablelist.Add(new PdfPTable(12));

            chart = new CartesianChart();
            chart.Tag = tag;
            this.listListCarParks = listListCarParks;
        }

        public override Axis SetAxisX()
        {
            Axis axisX = new Axis()
            {
                Separator = new Separator() { Step = 1, IsEnabled = false },
                Foreground = System.Windows.Media.Brushes.IndianRed,
                FontSize = 20
            };
            axisX.Labels = new List<string>();
            for (int i = 0; i <= 23; i++)
            {
                try
                {
                    axisX.Labels.Add(i.ToString());
                    if (i < 12)
                    {
                        pdfTablelist[0].AddCell(i.ToString());
                        pdfTablelist[2].AddCell(i.ToString());
                    }
                    else
                    {
                        pdfTablelist[1].AddCell(i.ToString());
                        pdfTablelist[3].AddCell(i.ToString());
                    }
                  
                }
                catch
                {

                }
            }
            return axisX;
        }

        public override SeriesCollection GeneerateSeries()
        {
            SeriesCollection seriesCollection = new SeriesCollection();
            ColumnSeries columnSeries = new ColumnSeries();
    //        chart.AxisX.Add(SetAxisX());

            foreach (var carPark in listListCarParks)
            {
                double total = carPark.Sum(x => x.count);
                ChartValues<double> ts = new ChartValues<double>();
                for (int i = 0; i <= 23; i++)
                {

                    if (!carPark.Exists(x => x.GrupuByTime == i))
                    {                                        
                        carPark.Add(new IdParking() { GrupuByTime = i, name = carPark[0].name, count = 0 });
                        ts.Add(carPark[i].count);
                        if (i < 12)
                        {

                            pdfTablelist[0].AddCell(carPark[i].count.ToString());
                            pdfTablelist[2].AddCell((GetPow(carPark[i].count, total)).ToString());

                        }
                        else
                        {
                            pdfTablelist[1].AddCell(carPark[i].count.ToString());
                            pdfTablelist[3].AddCell((GetPow(carPark[i].count, total)).ToString());
                        }
                    }
                    else
                    {
                        ts.Add((carPark[i].count / total) * 100);
                        if (i < 12)
                        {

                            pdfTablelist[0].AddCell((carPark[i].count).ToString());
                            pdfTablelist[2].AddCell((GetPow(carPark[i].count,total)).ToString());

                        }
                        else
                        {
                            pdfTablelist[1].AddCell(carPark[i].count.ToString());
                            pdfTablelist[3].AddCell((GetPow(carPark[i].count, total)).ToString());
                        }

                    }
                }
                seriesCollection.Add(new LineSeries
                {
                    Title = carPark[0].name,
                    Values = ts,
                    PointGeometry = null

                });

            }

            return seriesCollection;
        }
      public double GetPow(double aa, double total)
        {
            double a = Math.Round(((aa / total) * 100), 2);
                return a;
        }
    }
}
