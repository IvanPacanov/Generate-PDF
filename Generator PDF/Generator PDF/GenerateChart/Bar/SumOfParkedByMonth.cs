using Generator_PDF.VM;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text.pdf;


namespace Generator_PDF.GenerateChart
{
    class SumOfParkedByMonth : AbstractChart, ITable
    {
        List<List<IdParking>> listListCarPark;
        public List<PdfPTable> pdfTablelist;
        PdfPTable pdfTable;
        List<IdParking> idParkings;
       
        public SumOfParkedByMonth(List<List<IdParking>> listListCarPark, int key)
        {
           
            pdfTablelist = new List<PdfPTable>();
            chart = new CartesianChart();
            chart.Tag = key.ToString();
            this.listListCarPark = listListCarPark;

        }



        public override Axis SetAxisX(Format format)
        {
            Axis axisX = base.SetAxisX(Format.Normal);
            axisX.Labels = new List<string>();
            var maxItem = listListCarPark.OrderBy(x => x.Count).Take(1);
            foreach (var item in maxItem.First().OrderBy(x => x.month))
            {
                axisX.Labels.Add(Enum.GetName(typeof(EnumMonth), item.month));
            }

            return axisX;
        }
        public override Axis SetAxisY(Format format)
        {
            Axis axisY = base.SetAxisY(Format.Normal);
            return axisY;
        }
        public override SeriesCollection GeneerateSeries()
        {

            SeriesCollection seriesCollection = new SeriesCollection();

            foreach (var carPark in listListCarPark)
            {
                ChartValues<double> ts = new ChartValues<double>();
                idParkings = carPark;
                GenerateTable();
                carPark.OrderBy(x => x.month);

                foreach (var item in carPark)
                {
                    ts.Add(item.count);
                }
                seriesCollection.Add(new ColumnSeries
                {

                    Title = carPark[0].name,
                    Values = ts

                });
                GenerateTable();
            }
            return seriesCollection;
        }

        public void GenerateTable()
        {
            pdfTablelist = TableCreate.PdfTableByNameOrMonth(idParkings, FirstRow.Month);
        }
    }
}