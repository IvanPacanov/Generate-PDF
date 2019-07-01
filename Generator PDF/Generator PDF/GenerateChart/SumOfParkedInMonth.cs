using Generator_PDF.VM;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Generator_PDF.GenerateChart
{
    class SumOfParkedInMonth : AbstractChart
    {
        List<List<IdParking>> listListCarParks;
        Dictionary<int, double> result;
        int min = 99;
        int months = 0;
        public SumOfParkedInMonth(List<List<IdParking>> listListCarParks)
        {
            chart = new CartesianChart();
            chart.Tag = "3";
            this.listListCarParks = listListCarParks;
            SumByMonth();
        }

        private void SumByMonth()
        {
            result = new Dictionary<int, double>();
       
            foreach (var item in listListCarParks)
            {
                var total = item.GroupBy(x => x).ToDictionary(group => group.Key.GrupuByTime, group => group.Sum(y=>y.count));


                result = (from e in result.Concat(total)
                                                  group e by e.Key into g
                                                  select new { Name = g.Key, Count = g.Sum(kvp => kvp.Value) })
               .ToDictionary(itemm => itemm.Name, itemm => itemm.Count);
            }
               
          
            
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
            for (int i = min; i <= months; i++)
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
            ColumnSeries columnSeries = new ColumnSeries();

            for (int i = 0; i < listListCarParks.Count; i++)
            {

                try
                {
                    if (months < listListCarParks[i].Select(x => x.GrupuByTime).Max())
                    {
                        months = listListCarParks[i].Select(x => x.GrupuByTime).Max();

                    }
                    if (min > listListCarParks[i].Select(x => x.GrupuByTime).Min())
                    {
                        min = listListCarParks[i].Select(x => x.GrupuByTime).Min();
                    }
                }
                catch
                {

                }
            }

           
            foreach (var item in result)
                {
                ChartValues<double> ts = new ChartValues<double>() { item.Value};
            
                
                seriesCollection.Add(new ColumnSeries
                {
                    Title = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Key),
                Values = ts

                });

            }

            return seriesCollection;
        }
    }
}
  
