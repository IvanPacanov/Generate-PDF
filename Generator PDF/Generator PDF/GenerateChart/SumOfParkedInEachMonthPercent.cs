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
      public  List<IdParking> listListCarPark;
        public List<PdfPTable> pdfTablelist;
        PdfPTable pdfTable;
        int z;
        public SumOfParkedInEachMonthPercent(List<IdParking> listListCarParks, int key)
        {
            listListCarPark = new List<IdParking>();
            pdfTablelist = new List<PdfPTable>();
            chart = new CartesianChart();
            chart.Tag = key.ToString();
            chart.HideLegend();
            listListCarPark = listListCarParks;
            z = ((MVGeneratePDF.availableTo.Value.Year - MVGeneratePDF.availableFrom.Value.Year) * 12) + MVGeneratePDF.availableTo.Value.Month - MVGeneratePDF.availableFrom.Value.Month;
            int counter = 0;
            var aaa = listListCarPark.OrderBy(x => x.Year).ThenBy(y => y.GrupuByTime);
            listListCarPark = aaa.ToList();
            while (listListCarPark.Count != z + 1)
            {

                if (MVGeneratePDF.availableFrom.Value.Month != listListCarPark[0].GrupuByTime && counter == 0)
                {
                    listListCarPark.Insert(counter, new IdParking() { GrupuByTime = MVGeneratePDF.availableFrom.Value.Month, count = 0, Year = MVGeneratePDF.availableFrom.Value.Year });

                }
                else if (listListCarPark[counter].GrupuByTime != listListCarPark[counter + 1].GrupuByTime - 1 && listListCarPark[counter].GrupuByTime != 12)
                {
                    listListCarPark.Insert(counter + 1, new IdParking() { GrupuByTime = listListCarPark[counter].GrupuByTime + 1, count = 0, Year = listListCarPark[counter].Year });
                    counter++;
                }
                else if (listListCarPark[counter].GrupuByTime - 11 != listListCarPark[counter + 1].GrupuByTime && listListCarPark[counter].GrupuByTime == 12)
                {
                    listListCarPark.Insert(counter + 1, new IdParking() { GrupuByTime = 1, count = 0, Year = listListCarPark[counter].Year + 1 });
                    counter++;
                }
                else
                {
                    counter++;
                }




            }
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
            foreach (var item in listListCarPark)
            {

                axisX.Labels.Add(Enum.GetName(typeof(EnumMonth), item.GrupuByTime));
            }
            axisX.LabelsRotation = 40;
            return axisX;
        }

        public override SeriesCollection GeneerateSeries()
        {
            ChartSelect.ChangeToPercents(listListCarPark);
            SeriesCollection seriesCollection = new SeriesCollection();
            ChartValues<double> ts = new ChartValues<double>();

            pdfTable = new PdfPTable(listListCarPark.Count);

            foreach (var item in listListCarPark)
            {
                pdfTable.AddCell(Enum.GetName(typeof(EnumMonth), item.GrupuByTime));
            }


            for (int i = 0; i < z; i++)
            {

                pdfTable.AddCell(listListCarPark[i].count.ToString());
                ts.Add(listListCarPark[i].count);
               
                  
                
           
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