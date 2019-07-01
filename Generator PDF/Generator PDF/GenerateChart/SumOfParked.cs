using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Generator_PDF.VM;
using LiveCharts;
using LiveCharts.Wpf;

namespace Generator_PDF.GenerateChart
{
    class SumOfParked : AbstractChart
    {
     
        List<IdParking> listcarParks;
        public SumOfParked(List<List<IdParking>> listcarParks)
        {
            chart = new CartesianChart();
            chart.Tag = "1";
            this.listcarParks = listcarParks[0];
        }

        public override Axis SetAxisX()
        {
            return new Axis()
            {
                // Foreground = System.Windows.Media.Brushes.DodgerBlue,
                FontSize = 20,
                ShowLabels = false,
                Title = "Ilość wzbudzeń"
            };
        }




        public override SeriesCollection GeneerateSeries()
        {
            SeriesCollection seriesCollection = new SeriesCollection();

            foreach (var carPark in listcarParks)
            {
                seriesCollection.Add(new ColumnSeries
                {
                    Title = carPark.name,
                    Values = new ChartValues<double> { carPark.count}

                });
            }

            return seriesCollection;
        }
    }
}
