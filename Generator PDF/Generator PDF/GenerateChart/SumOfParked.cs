using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
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
            chart = new CartesianChart() ;
            chart.Tag = "1";
            chart.Background = Brushes.Black ;
    
            
            this.listcarParks = listcarParks[0];
        }

        public override Axis SetAxisX()
        {
            return new Axis()
            {
                Foreground = Brushes.Black,
                FontSize = 20,
                Separator = new Separator() { Stroke = Brushes.Black },
              ShowLabels = false,
                Title = "Ilość Pojazdów"
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
