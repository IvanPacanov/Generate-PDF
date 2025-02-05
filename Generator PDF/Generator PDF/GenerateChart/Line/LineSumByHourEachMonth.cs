﻿using Generator_PDF.VM;
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
    class LineSumByHourEachMonth : AbstractChart, ITable
    {
        List<IdParking> listCarParks;
        public List<PdfPTable> pdfTablelist;
        List<IdParking> sortedByHours;


        public LineSumByHourEachMonth(List<IdParking> listCarParks, string key)
        {
            sortedByHours = new List<IdParking>();
            chart = new CartesianChart();
            chart.Tag = $"{key}, {listCarParks[0].name}, {listCarParks[0].month}";         
            this.listCarParks = FillMissHours(listCarParks);
            GenerateTable();
        }

        public override Axis SetAxisX(Format format)
        {
            Axis axisX = base.SetAxisX(Format.Percent);
            axisX.Separator.Step = 2;
            axisX.Labels = new List<string>();
            foreach (var item in listCarParks)
            {
                axisX.Labels.Add(item.hours.ToString());
            }
            return axisX;
        }

        public override SeriesCollection GeneerateSeries()
        {
            SeriesCollection seriesCollection = new SeriesCollection();
            ChartValues<double> chartValues = new ChartValues<double>();


            var test = ChartSelect.ChangeToPercents(listCarParks);
            listCarParks = SortByCount(listCarParks.OrderBy(x => x.count).ToList());
            test = SortByCount(test.OrderBy(x => x.count).ToList());

            foreach (var item in test)
            {
                chartValues.Add(item.count);
            }

            seriesCollection.Add(new LineSeries
            {
                Title = listCarParks[0].name,
                Values = chartValues,
                StrokeThickness = 3,
                StrokeDashArray = new System.Windows.Media.DoubleCollection(20),
                Stroke = System.Windows.Media.Brushes.Blue,
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null

            });
            return seriesCollection;
        }

        public void GenerateTable()
        {
            double sum = listCarParks.Sum(x => x.count);
            sortedByHours = listCarParks.OrderBy(x => x.hours).ToList();
            pdfTablelist = TableCreate.PdfTableByHours(sortedByHours, sum);
        }
    }
}
