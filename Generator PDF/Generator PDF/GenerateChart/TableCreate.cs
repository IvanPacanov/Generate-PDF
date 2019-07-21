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
