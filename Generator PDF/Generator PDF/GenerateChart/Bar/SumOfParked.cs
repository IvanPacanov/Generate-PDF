using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Generator_PDF.VM;
using iTextSharp.text.pdf;
using LiveCharts;
using LiveCharts.Wpf;

namespace Generator_PDF.GenerateChart
{
    class SumOfParked : AbstractChart, ITable
    {
     
        List<IdParking> listcarParks;
        public List<PdfPTable> pdfTablelist;
     

        public SumOfParked(List<List<IdParking>> listcarParks, int tag)
        {            
            chart = new CartesianChart() ;
            chart.Tag = $"{tag},Suma wzbudzeń parkujących (wzbudzeń) w całym okresie raportu";
            chart.Background = Brushes.Black ;           
            this.listcarParks = listcarParks[0];
        }

        public override Axis SetAxisX(Format format)
        {
            Axis axisX = base.SetAxisX(Format.Normal);         
            axisX.ShowLabels = false;
            axisX.Title = "Ilość Pojazdów";
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
            GenerateTable();
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

        public void GenerateTable()
        {
            pdfTablelist = TableCreate.PdfTableByNameOrMonth(listcarParks, FirstRow.Name);
        }
    }
}
