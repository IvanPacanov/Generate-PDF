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
        static public void HourlySummaryForEachParkingByMonth()
        {
          
            string name = null;
            double key = 7;

          
       

            foreach (var parking in MVGeneratePDF.parkings[7])
            {

                List<List<IdParking>> months = new List<List<IdParking>>();
                var monthsalla = parking.GroupBy(x => x.year);

                foreach (var item in monthsalla)
                {

                    var monthss = item.GroupBy(g => g.month).OrderBy(g => g.Key).SelectMany(g => g.OrderByDescending(x => x.month)).ToList();

                    var montha = monthss.GroupBy(x => x.month);
             
                    foreach (var itemm in montha)
                    {

                     //   months.Add(itemm.ToList());

                
                LineSumByHourEachMonth zz = new LineSumByHourEachMonth( itemm.ToList() ,"7");
                zz.GeneerateChart();
                MVGeneratePDF.PDfTableDictionary.Add(zz, zz.pdfTablelist);
                name = parking[0].name;
                key = key + key;
                    }
                }
            }
            MVGeneratePDF.counteOfChartr++;

        }

        static public void HourlySummaryForEachParking()
        {
            double key = 6;
               
         
            foreach (var parking in MVGeneratePDF.parkings[6])
            {
                LineSumByHour z = new LineSumByHour(parking, "6");
                z.GeneerateChart();
                MVGeneratePDF.PDfTableDictionary.Add(z, z.pdfTablelist);
                key = key+key;
            }
            MVGeneratePDF.counteOfChartr++;

        }


        static public void HourlySummary()
        {

            LineChart z = new LineChart(MVGeneratePDF.parkings[5],"5");
            ChangeToPercent(MVGeneratePDF.parkings[5]);
            z.GeneerateChart();
            MVGeneratePDF.PDfTableDictionary.Add(z, new List<PdfPTable>());
            MVGeneratePDF.counteOfChartr++;
        }
  
        static public void TheSumOfVehiclesInMonthPercent()
        {
            ChangeToPercent(MVGeneratePDF.parkings[3]);
            SumOfParkedByMonthPercent za = new SumOfParkedByMonthPercent(MVGeneratePDF.parkings[3], 3);
            za.GeneerateChart();
            MVGeneratePDF.PDfTableDictionary.Add(za, za.pdfTablelist);
            MVGeneratePDF.counteOfChartr++;


        }
        static public void TheSumOfVehiclesInMonth()
        {
            SumOfParkedByMonth z = new SumOfParkedByMonth(MVGeneratePDF.parkings[2],2);
            z.GeneerateChart();
            MVGeneratePDF.PDfTableDictionary.Add(z, z.pdfTablelist);
            MVGeneratePDF.counteOfChartr++;
        }
        static public void TheSumOfVehicles()
        {
            SumOfParked z = new SumOfParked(MVGeneratePDF.parkings[1],1);
            z.GeneerateChart();
            MVGeneratePDF.PDfTableDictionary.Add(z, z.pdfTablelist );
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
        public static List<IdParking> ChangeToPercents(List<IdParking> idParkings)
        {
            List<IdParking> percent = new List<IdParking>();
                  double total = idParkings.Sum(x => x.count);
                foreach (var park in idParkings)
                {
                percent.Add(new IdParking() { count = Math.Round(((park.count / total) * 100), 2), name=park.name, hours=park.hours, month=park.hours, year=park.year });
           
                    
                }
            return percent;
        }

  

        internal static void TheSumOfVehiclesInMonthPercentOnCarPark()
        {
           
            int key = 4;
            foreach (var parking in MVGeneratePDF.parkings[4])
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {
                      
                        SumOfParkedInEachMonthPercent sumOfParkedInEachMonthPercent = new SumOfParkedInEachMonthPercent(parking, 4);
                        
                        sumOfParkedInEachMonthPercent.GeneerateChart();
               
                        MVGeneratePDF.PDfTableDictionary.Add(sumOfParkedInEachMonthPercent, sumOfParkedInEachMonthPercent.pdfTablelist);
                        key = key + key;
                    }
                    else
                    {
                        SumOfVehiclesInMonthOnCarPark sumOfParkedIn = new SumOfVehiclesInMonthOnCarPark(parking, 4);

                        sumOfParkedIn.GeneerateChart();
                        MVGeneratePDF.PDfTableDictionary.Add(sumOfParkedIn, sumOfParkedIn.pdfTablelist);
                        key = key + key;
                    }
                    }
               
            }
            MVGeneratePDF.counteOfChartr++;
        }
    }
}
