using Generator_PDF.VM;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.GenerateChart
{
   static class ChartSelect
    {
        


        static public void TheSumOfVehiclesInEachMonthByHours()
        {
          //  ChangeToPercent(MVGeneratePDF.parkings[7]);
            string name = null;
            double key = 7;
            foreach (var parking in MVGeneratePDF.parkings[7])
            {
                if (parking[0].name != name)
                {
                  
                }

                LineChart zz = new LineChart(new List<List<IdParking>>() { parking },"7");
                zz.GeneerateChart("Procentowy udział godzinowy");
                MVGeneratePDF.PDfTableDictionary.Add((int)key, zz.pdfTablelist);
                name = parking[0].name;
                key = key + key;
            }
            MVGeneratePDF.counteOfChartr++;
        }

        static public void TheSumOfVehiclesInHoursEachParking()
        {
            double key = 6;
               
         
            foreach (var parking in MVGeneratePDF.parkings[6])
            {
                LineChart z = new LineChart(new List<List<IdParking>>() { parking },"6");
                z.GeneerateChart("Procentowy udział godzinowy");
                MVGeneratePDF.PDfTableDictionary.Add((int)key, z.pdfTablelist);
                key = key+key;
            }
            MVGeneratePDF.counteOfChartr++;

        }


        static public void TheSumOfVehiclesInHour()
        {
            LineChart z = new LineChart(MVGeneratePDF.parkings[5],"5");
         //   ChangeToPercent(MVGeneratePDF.parkings[5]);
            z.GeneerateChart("Procentowy udział godzinowy");
          
            MVGeneratePDF.counteOfChartr++;
        }
  
        static public void TheSumOfVehiclesInMonthPercent()
        {
            ChangeToPercent(MVGeneratePDF.parkings[3]);           
            SumOfParkedByMonth za = new SumOfParkedByMonth(MVGeneratePDF.parkings[3], 3);
            za.GeneerateChart("Procentowa wartość wzbudzeń");
            MVGeneratePDF.PDfTableDictionary.Add(3, za.pdfTablelist);
            MVGeneratePDF.counteOfChartr++;


        }
        static public void TheSumOfVehiclesInMonth()
        {
            SumOfParkedByMonth z = new SumOfParkedByMonth(MVGeneratePDF.parkings[2],2);
            z.GeneerateChart("Ilość wzbudzeń");
           MVGeneratePDF.PDfTableDictionary.Add(2, z.pdfTablelist);
            MVGeneratePDF.counteOfChartr++;
        }
        static public void TheSumOfVehicles()
        {
            SumOfParked z = new SumOfParked(MVGeneratePDF.parkings[1]);
            z.GeneerateChart("Ilość pojazdów");
            MVGeneratePDF.PDfTableDictionary.Add(1, new List<PdfPTable>() { TableCreate.SumOfVehicles(MVGeneratePDF.parkings[1][0]) });
            MVGeneratePDF.counteOfChartr++;

        }

        private static void ChangeToPercent(List<List<IdParking>> idParkings)
        {
            foreach (var item in idParkings)
            {
                double total = item.Sum(x => x.count);
                foreach (var park in item)
                {
                    park.count = Math.Round(((park.count / total) * 100),2);
                }
            }
        }

        internal static void TheSumOfVehiclesInMonthPercentOnCarPark()
        {
           
            int key = 4;
            foreach (var parking in MVGeneratePDF.parkings[4])
            {
                SumOfVehiclesInMonthOnCarPark sumOfParkedIn = new SumOfVehiclesInMonthOnCarPark( parking , 4);

                sumOfParkedIn.GeneerateChart("Procentowy udział godzinowy");
                MVGeneratePDF.PDfTableDictionary.Add((int)key, sumOfParkedIn.pdfTablelist);
                key = key + key;
            }
            MVGeneratePDF.counteOfChartr++;
        }
    }
}
