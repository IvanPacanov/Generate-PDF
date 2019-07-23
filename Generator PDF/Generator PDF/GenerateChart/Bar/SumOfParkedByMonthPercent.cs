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
    class SumOfParkedByMonthPercent : AbstractChart, ITable
    {
        List<List<IdParking>> listListCarPark;
        public List<PdfPTable> pdfTablelist;
        PdfPTable pdfTable;
        List<IdParking> idParkings;
        List<IdParking> sortedByHours;


        public SumOfParkedByMonthPercent(List<List<IdParking>> listListCarPark, int key)
        {
            sortedByHours = new List<IdParking>();
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
            Axis axisY = base.SetAxisY(Format.Percent);
            return axisY;
        }

        public override SeriesCollection GeneerateSeries()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            foreach (var carPark in listListCarPark)
            {
                ChartValues<double> ts = new ChartValues<double>();
                idParkings = carPark;
      var help=          carPark.OrderBy(x => x.month);
                GenerateTable();
                foreach (var item in help)
                {
                    ts.Add(item.count);
                }
                seriesCollection.Add(new ColumnSeries
                {

                    Title = carPark[0].name,
                    Values = ts

                });               
            }
            return seriesCollection;
        }

        public void GenerateTable()
        {
            sortedByHours = idParkings.OrderBy(x => x.month).ToList();
            pdfTablelist = TableCreate.PdfTableByNameOrMonth(sortedByHours, FirstRow.Month);
        }
    }
}
