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
    class SumOfVehiclesInMonthOnCarPark : AbstractChart
    {
        List<IdParking> listListCarPark;
        public List<PdfPTable> pdfTablelist;
        PdfPTable pdfTable;
        public SumOfVehiclesInMonthOnCarPark(List<IdParking> listListCarParks, int key)
        {
            listListCarPark = new List<IdParking>();
            pdfTablelist = new List<PdfPTable>();
            chart = new CartesianChart();
            chart.Tag = key.ToString();
            //  chart.Name = name;
            listListCarPark = listListCarParks;
        }

        public override SeriesCollection GeneerateSeries()
        {
            SeriesCollection seriesCollection = new SeriesCollection();
            ChartValues<double> ts = new ChartValues<double>();
            int min = MVGeneratePDF.availableFrom.Value.Month;
            int max = MVGeneratePDF.availableTo.Value.Month;
            int licznik = 0;
            int month = MVGeneratePDF.availableTo.Value.Month-MVGeneratePDF.availableFrom.Value.Month;
            pdfTable = new PdfPTable(month+1);
            for (int i = min; i <= max; i++)
            {
                pdfTable.AddCell(Enum.GetName(typeof(EnumMonth), i));
            }


            for (int i = min; i <= max; i++)
            {
                ts = new ChartValues<double>();
                if (listListCarPark.Exists(x => x.GrupuByTime == i))
                    {
                        pdfTable.AddCell(listListCarPark[i-1].count.ToString());
                        ts.Add(listListCarPark[i-1].count);
                        
                    }
                    else
                {
                    pdfTable.AddCell("0");
                    ts.Add(0);
                }
                seriesCollection.Add(new ColumnSeries
                {
                    Title = Enum.GetName(typeof(EnumMonth), i),
                    Values = ts

                });


                        
           
            }
            pdfTablelist.Add(pdfTable);
            return seriesCollection;
        }
    }
}