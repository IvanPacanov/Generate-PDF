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
    class SumOfParkedInEachMonthPercent : AbstractChart, ITable
    {
        List<IdParking> listCarParks;
        public List<PdfPTable> pdfTablelist;
   
        public SumOfParkedInEachMonthPercent(List<IdParking> listCarParks, int key)
        {
            pdfTablelist = new List<PdfPTable>();
            chart = new CartesianChart();
            chart.Tag = $"{key}, Wartość procentowa,{listCarParks[0].name}";
            chart.HideLegend();
            this.listCarParks = listCarParks;
             FillMissingMonth();

        }

        public override Axis SetAxisX(Format format)
        {
            Axis axisX = base.SetAxisX(Format.Normal);
            axisX.Labels = new List<string>();
            foreach (var item in listCarParks.OrderBy(x => x.month))
            {
                axisX.Labels.Add(Enum.GetName(typeof(EnumMonth), item.month));
            }
            axisX.LabelsRotation = 40;
            return axisX;
        }
        public override Axis SetAxisY(Format format)
        {
            Axis axisY = base.SetAxisY(Format.Percent);
            return axisY;
        }

        public override SeriesCollection GeneerateSeries()
        {
            //      ChartSelect.ChangeToPercents(listCarParks);
            SeriesCollection seriesCollection = new SeriesCollection();
            ChartValues<double> ts = new ChartValues<double>();
            var aaa = listCarParks.OrderBy(x => x.year).ThenBy(y => y.month);
            listCarParks = aaa.ToList();


            List<IdParking> hlistCarParks = new List<IdParking>();
            hlistCarParks = ChartSelect.ChangeToPercents(listCarParks);


            foreach (var item in hlistCarParks)
            {
                ts.Add(item.count);

            }
            seriesCollection.Add(new ColumnSeries
            {
                Title = listCarParks[0].name,
                Values = ts
            });
            GenerateTable();
            return seriesCollection;
        }
        public void GenerateTable()
        {
            double sum = listCarParks.Sum(x => x.count);
            pdfTablelist = TableCreate.PdfTableByNameOrMonth(listCarParks, sum, FirstRow.Month);
        }

        private void FillMissingMonth()
        {
            int counter = 0;
            while (listCarParks.Count != numberOfMonths + 1)
            {

                if (MVGeneratePDF.availableFrom.Value.Month != listCarParks[0].month || counter == 0 && MVGeneratePDF.availableFrom.Value.Year != listCarParks[0].year)
                {
                    listCarParks.Insert(counter, new IdParking() { month = MVGeneratePDF.availableFrom.Value.Month, count = 0, name = listCarParks[0].name, year = MVGeneratePDF.availableFrom.Value.Year });

                }
                else if (listCarParks[counter].month != listCarParks[counter + 1].month - 1 && listCarParks[counter].month != 12)
                {
                    listCarParks.Insert(counter + 1, new IdParking() { month = listCarParks[counter].month + 1, count = 0, name = listCarParks[0].name, year = listCarParks[counter].year });
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