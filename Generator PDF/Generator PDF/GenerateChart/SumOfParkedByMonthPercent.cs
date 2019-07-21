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
    class SumOfParkedByMonthPercent : AbstractChart
    {
        List<List<IdParking>> listListCarPark;
        public List<PdfPTable> pdfTablelist;
        PdfPTable pdfTable;

        int min = 99;
        int months = 0;
        public SumOfParkedByMonthPercent(List<List<IdParking>> listListCarParks, int key)
        {
            listListCarPark = new List<List<IdParking>>();
            pdfTablelist = new List<PdfPTable>();
            chart = new CartesianChart();
            chart.Tag = key.ToString();
            listListCarPark = listListCarParks;

        }



        public override Axis SetAxisX()
        {
            Axis axisX = new Axis()
            {
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 20,
                Separator = new Separator() { StrokeThickness = 0, Step = 1 }
            };
            axisX.Labels = new List<string>();
            for (int i = min; i <= months + 1; i++)
            {
                try
                {
                    axisX.Labels.Add(Enum.GetName(typeof(EnumMonth), i));
                }
                catch
                {

                }
            }
            return axisX;
        }

        public override SeriesCollection GeneerateSeries()
        {
            min = 99;
            SeriesCollection seriesCollection = new SeriesCollection();





            for (int i = 0; i < listListCarPark.Count; i++)
            {

                try
                {
                    if (months < listListCarPark[i].Select(x => x.GrupuByTime).Max())
                    {
                        months = listListCarPark[i].Select(x => x.GrupuByTime).Max();

                    }
                    if (min > listListCarPark[i].Select(x => x.GrupuByTime).Min())
                    {
                        min = listListCarPark[i].Select(x => x.GrupuByTime).Min();
                    }
                }
                catch
                {

                }
            }

            foreach (var carPark in listListCarPark)
            {
                pdfTable = new PdfPTable(months);
                int licznik = 0;
                int mont = 0;
                ChartValues<double> ts = new ChartValues<double>();
                for (int i = min; i <= months; i++)
                {
                    pdfTable.AddCell(Enum.GetName(typeof(EnumMonth), i));
                }
                for (int i = min; i <= months; i++)
                {

                    if (!carPark.Exists(x => x.GrupuByTime == i))
                    {

                        if (i < months)
                        {
                            carPark.Insert(mont, new IdParking() { GrupuByTime = i, name = carPark[0].name, count = 0 });
                            mont++;
                        }
                        else
                            carPark.Add(new IdParking() { GrupuByTime = i, name = carPark[0].name, count = 0 });
                        pdfTable.AddCell("0");

                        ts.Add(carPark[licznik].count);
                        licznik++;
                    }
                    else
                    {
                        pdfTable.AddCell(carPark[licznik].count.ToString());
                        ts.Add(carPark[licznik].count);
                        licznik++;
                    }




                }
                seriesCollection.Add(new ColumnSeries
                {

                    Title = carPark[0].name,
                    Values = ts

                });
                pdfTablelist.Add(pdfTable);
            }



            return seriesCollection;
        }
    }
}