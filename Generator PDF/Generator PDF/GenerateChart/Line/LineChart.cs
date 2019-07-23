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
        private List<List<IdParking>> listListCarParks;
        public List<PdfPTable> pdfTablelist;
        List<IdParking> sortedByHours;
        public LineChart(List<List<IdParking>> listListCarParks, string tag)
        {
            sortedByHours = new List<IdParking>();
            chart = new CartesianChart();
            chart.Tag = tag;           
            this.listListCarParks = listListCarParks;
     
        }

        public override Axis SetAxisX(Format format)
        {
            Axis axisX = base.SetAxisX(Format.Percent);
            axisX.Separator.Step = 2;
            axisX.Labels = new List<string>();
            //foreach (var item in listListCarParks)
            //{
            //    axisX.Labels.Add(item[0].hours.ToString());
            //}
            return axisX;
        }
        public override SeriesCollection GeneerateSeries()
        {
            SeriesCollection seriesCollection = new SeriesCollection();
            ColumnSeries columnSeries = new ColumnSeries();
            //        chart.AxisX.Add(SetAxisX());

            foreach (var item in listListCarParks)
            {
                var ppp = item.OrderBy(x => x.count).ToList();
                var help  = SortByCount(item.OrderBy(x => x.count).ToList());
                ChartValues<double> chartValues = new ChartValues<double>();

                foreach (var hour in help)
                {
                    chartValues.Add(hour.count);
                }               

                seriesCollection.Add(new LineSeries
                {
                    Title = item[0].name,
                    Values = chartValues,
                    StrokeThickness = 3,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(20),
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = null
                });

            }

            return seriesCollection;
        }
      


    }
}
