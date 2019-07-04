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
    class SumOfParkedInEachMonthPercent : AbstractChart
    {
        List<IdParking> listListCarPark;
        public List<PdfPTable> pdfTablelist;
        PdfPTable pdfTable;
        int max=0;
        int min = 99;
        int month = 0;
        public SumOfParkedInEachMonthPercent(List<IdParking> listListCarParks, int key)
        {
            listListCarPark = new List<IdParking>();
            pdfTablelist = new List<PdfPTable>();
            chart = new CartesianChart();
            chart.Tag = key.ToString();
            chart.HideLegend();
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
            for (int i = min; i <= month+1; i++)
            {
                try
                {
                    axisX.Labels.Add(Enum.GetName(typeof(EnumMonth), i));
                }
                catch
                {

                }
            }
            axisX.LabelsRotation = 40;
            return axisX;
        }

        public override SeriesCollection GeneerateSeries()
        {
            SeriesCollection seriesCollection = new SeriesCollection();
            ChartValues<double> ts = new ChartValues<double>();
             min = MVGeneratePDF.availableFrom.Value.Month;
             max = MVGeneratePDF.availableTo.Value.Month;
         
             month = MVGeneratePDF.availableTo.Value.Month - MVGeneratePDF.availableFrom.Value.Month;
            pdfTable = new PdfPTable(month + 1);
            for (int i = min; i <= max; i++)
            {
                pdfTable.AddCell(Enum.GetName(typeof(EnumMonth), i));
            }


            for (int i = min; i <= max; i++)
            {
               
                if (listListCarPark.Exists(x => x.GrupuByTime == i))
                {
                    pdfTable.AddCell(listListCarPark[i - 1].count.ToString());
                    ts.Add(listListCarPark[i - 1].count);

                }
                else
                {
                    pdfTable.AddCell("0");
                    ts.Add(0);
                }
           
            }
            seriesCollection.Add(new ColumnSeries
            {

                Values = ts

            });
            pdfTablelist.Add(pdfTable);
            return seriesCollection;
        }
    }
}