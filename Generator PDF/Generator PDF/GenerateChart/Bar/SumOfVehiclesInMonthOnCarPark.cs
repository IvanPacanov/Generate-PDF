using Generator_PDF.VM;
using iTextSharp.text.pdf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator_PDF.GenerateChart
{
    class SumOfVehiclesInMonthOnCarPark : AbstractChart, ITable
    {
        List<IdParking> listCarParks;
        public List<PdfPTable> pdfTablelist;
        PdfPTable pdfTable;
        public SumOfVehiclesInMonthOnCarPark(List<IdParking> listCarParks, int key)
        {
            pdfTablelist = new List<PdfPTable>();                  
            chart = new CartesianChart();
            chart.Tag = key.ToString();       
            this.listCarParks = listCarParks.OrderBy(x => x.year).ThenBy(x => x.month).ToList();
            FillMissingMonth();
        
        }

        public override SeriesCollection GeneerateSeries()
        {
            SeriesCollection seriesCollection = new SeriesCollection();
            ChartValues<double> ts = new ChartValues<double>();         
            var aaa = listCarParks.OrderBy(x => x.year).ThenBy(y => y.month);
            listCarParks = aaa.ToList();
            GenerateTable();
            foreach (var item in listCarParks)
            {
                ts = new ChartValues<double>();
                ts.Add(item.count);
                seriesCollection.Add(new ColumnSeries
                {
                    Title = Enum.GetName(typeof(EnumMonth), item.month),
                    Values = ts
                });
            }         
           
            return seriesCollection;
        }
        public void GenerateTable()
        {
            double sum = listCarParks.Sum(x => x.count);
            pdfTablelist = TableCreate.PdfTableByNameOrMonth(listCarParks, FirstRow.Month);
        }

        private void FillMissingMonth()
        {
            int counter = 0;

            //uwzglednić lata !!!, 
            while (listCarParks.Count != numberOfMonths + 1)
            {
         

                if (MVGeneratePDF.availableFrom.Value.Month != listCarParks[0].month || counter == 0 && MVGeneratePDF.availableFrom.Value.Year != listCarParks[0].year)
                {
                    listCarParks.Insert(counter, new IdParking() { month = MVGeneratePDF.availableFrom.Value.Month, count = 0, year = MVGeneratePDF.availableFrom.Value.Year, name= listCarParks[0].name });

                }
                else if (listCarParks[counter].month != listCarParks[counter + 1].month - 1 && listCarParks[counter].month != 12)
                {
                    listCarParks.Insert(counter + 1, new IdParking() { month = listCarParks[counter].month + 1, count = 0, year = listCarParks[counter].year, name = listCarParks[0].name });
                    counter++;
                }
                else if (listCarParks[counter].month - 11 != listCarParks[counter + 1].month && listCarParks[counter].month == 12)
                {
                    listCarParks.Insert(counter + 1, new IdParking() { month = 1, count = 0, year = listCarParks[counter].year + 1, name = listCarParks[0].name });
                    counter++;
                }
                else
                {
                    counter++;
                }


            }
        }
    }
}