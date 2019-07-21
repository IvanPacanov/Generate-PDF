using Generator_PDF.VM;
using iTextSharp.text.pdf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.GenerateChart
{
    class ListSumByHourEachMonth : AbstractChart
    {
     List<IdParking> listListCarParks;
        public List<PdfPTable> pdfTablelist;
        

        public ListSumByHourEachMonth(List<IdParking> listListCarParks, string tag)
        {
            pdfTablelist = new List<PdfPTable>();
            pdfTablelist.Add(new PdfPTable(12));
            pdfTablelist.Add(new PdfPTable(12));
            pdfTablelist.Add(new PdfPTable(12));
            pdfTablelist.Add(new PdfPTable(12));
         
            chart = new CartesianChart();
            chart.Tag = 7; //listListCarParks[0][0].GrupuByTime;
            this.listListCarParks = listListCarParks;
        }

        public override Axis SetAxisX()
        {
            Axis axisX = new Axis()
            {
                Separator = new Separator() { Step = 1, IsEnabled = false },
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 15
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



     
           
                double total = listListCarParks.Sum(x => x.count);
                ChartValues<double> ts = new ChartValues<double>();

                double[] gauss = SortByCount(listListCarParks.OrderBy(x => x.count).ToList());
                for (int i = 0; i <= gauss.Length; i++)
                {
                    try
                    {
                        ts.Add(gauss[i]);
                    }
                    catch
                    {

                    }
                }

          


                seriesCollection.Add(new LineSeries
                {
                    Title = listListCarParks[0].name,
                    Values = ts,
                    PointGeometry = null

                });


            return seriesCollection;
        }
        public double GetPow(double aa, double total)
        {
            double a = Math.Round(((aa / total) * 100), 2);
            return a;
        }
      
    }
}
