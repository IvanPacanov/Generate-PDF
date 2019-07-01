using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator_PDF.VM;
using iTextSharp.text.pdf;

namespace Generator_PDF.GenerateChart
{
    static class TableCreate
    {

        static int min;
        static int max;
        static public PdfPTable SumOfVehicles(List<IdParking> idParkings)
        {

            PdfPTable pdfPTable = new PdfPTable(idParkings.Count);


            foreach (var carPark in idParkings)
            {
                pdfPTable.AddCell(carPark.name);
            }
            foreach (var carPark in idParkings)
            {
                pdfPTable.AddCell(carPark.count.ToString());
            }

            return pdfPTable;
        }

        internal static PdfPTable SumOfVehiclesInMonth(List<IdParking> list)
        {
            throw new NotImplementedException();
        }
    }
}

    //static public PdfPTable CreateTableByMonth(List<List<IdParking>> idParkings)
    //{

    //    PdfPTable pdfPTable = new PdfPTable(setMinAndMaxMonth(idParkings));
    //    for (int i = min; i < max; i++)
    //    {
    //        pdfPTable.AddCell((Enum.GetName(typeof(EnumMonth), i)));
    //    }
    //    foreach (var carPark in idParkings)
    //    {
    //        if (carPark)
    //            for (int i = 0; i <= 23; i++)
    //            {

    //                if (!carPark.Exists(x => x.GrupuByTime == i))
    //                {
    //                    carPark.Add(new IdParking() { GrupuByTime = i, name = carPark[0].name, count = 0 });
    //                    ts.Add(carPark[i].count);
    //                }
    //                else
    //                {
    //                    ts.Add(carPark[i].count);
    //                }
    //            }
    //        seriesCollection.Add(new LineSeries
    //        {
    //            Title = carPark[0].name,
    //            Values = ts,
    //            PointGeometry = null

    //        });

    //    }

    //}

    //private static int setMinAndMaxMonth(List<List<IdParking>> listListCarParks)
    //{
    //    for (int i = 0; i < listListCarParks.Count; i++)
    //    {

    //        try
    //        {
    //            if (max < listListCarParks[i].Select(x => x.GrupuByTime).Max())
    //            {
    //                max = listListCarParks[i].Select(x => x.GrupuByTime).Max();

    //            }
    //            if (min > listListCarParks[i].Select(x => x.GrupuByTime).Min())
    //            {
    //                min = listListCarParks[i].Select(x => x.GrupuByTime).Min();
    //            }
    //        }
    //        catch
    //        {

    //        }
    //    }
    //    return max - min;
    //}
//}
//}
