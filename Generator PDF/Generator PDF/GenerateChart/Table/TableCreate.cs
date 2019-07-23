using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Generator_PDF.VM;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Generator_PDF.GenerateChart
{
    static class TableCreate
    {

        static public List<PdfPTable> PdfTableByNameOrMonth(List<IdParking> idParkings, FirstRow firstRow)
        {
            List<PdfPTable> pdfTablelist = new List<PdfPTable>();
            var help = idParkings.OrderBy(x => x.year).ThenBy(x => x.month);
            int from = 0;
            if (idParkings.Count > 6)
            {
                from = idParkings.Count - idParkings.Count / 2;
                pdfTablelist.Add(new PdfPTable(from));
                pdfTablelist.Add(new PdfPTable(idParkings.Count / 2));
          
            }
            else
            {
                pdfTablelist.Add(new PdfPTable(idParkings.Count));
                from = idParkings.Count;
            }
            int counter = 0;
            switch (firstRow)
            {
                case FirstRow.Name:
                    {

                        counter = 0;
                        foreach (var carPark in help)
                        {
                            if (counter < from)
                            {
                                pdfTablelist[0].AddCell(CreatePDF.FontPolish(carPark.name));
                                counter++;
                            }
                            else
                            {
                                pdfTablelist[1].AddCell(CreatePDF.FontPolish(carPark.name));
                            }
                        }
                        counter = 0;
                        foreach (var carPark in help)
                        {
                            if (counter < from)
                            {
                                pdfTablelist[0].AddCell(CreatePDF.FontPolish(carPark.count.ToString()));
                                counter++;
                            }
                            else
                            {
                                pdfTablelist[1].AddCell(CreatePDF.FontPolish(carPark.count.ToString()));
                            }
                        }

                        return pdfTablelist;

                    }
                case FirstRow.Month:
                    {
                        counter = 0;
                        var month = idParkings.OrderBy(x => x.month);
                        foreach (var carPark in help)
                        {

                            if (counter < from)
                            {
                                pdfTablelist[0].AddCell(CreatePDF.FontPolish(Enum.GetName(typeof(EnumMonth), carPark.month)));
                                counter++;
                            }
                            else
                            {
                                pdfTablelist[1].AddCell(CreatePDF.FontPolish(Enum.GetName(typeof(EnumMonth), carPark.month)));
                            }
                        }
                        counter = 0;
                        foreach (var carPark in help)
                        {
                            if (counter < from)
                            {
                                pdfTablelist[0].AddCell(CreatePDF.FontPolish(carPark.count.ToString()));
                                counter++;
                            }
                            else
                            {
                                pdfTablelist[1].AddCell(CreatePDF.FontPolish(carPark.count.ToString()));
                            }
                        }

                        return pdfTablelist;
                    }
                default:
                    {
                        return null;
                    }
            }

        }


        static public List<PdfPTable> PdfTableByNameOrMonth(List<IdParking> idParkings, double sum, FirstRow firstRow)
        {
            List<PdfPTable> pdfTablelist = new List<PdfPTable>();
            var help = idParkings.OrderBy(x => x.year).ThenBy(x => x.month);
            int from = 0;
            if (idParkings.Count > 6)
            {
                from = idParkings.Count - idParkings.Count / 2;
                pdfTablelist.Add(new PdfPTable(from));
                pdfTablelist.Add(new PdfPTable(idParkings.Count / 2));
              
            }
            else
            {
                pdfTablelist.Add(new PdfPTable(idParkings.Count));
                from = idParkings.Count;
            }
            int counter = 0;

            switch (firstRow)
            {
                case FirstRow.Name:
                    {

                        counter = 0;
                        foreach (var carPark in help)
                        {
                            if (counter < from)
                            {
                                pdfTablelist[0].AddCell(CreatePDF.FontPolish(carPark.name));
                                counter++;
                            }
                            else
                            {
                                pdfTablelist[1].AddCell(CreatePDF.FontPolish(carPark.name));
                            }
                        }
                        counter = 0;
                        foreach (var carPark in help)
                        {
                            if (counter < from)
                            {
                                pdfTablelist[0].AddCell(CreatePDF.FontPolish(SetPercent(carPark.count, sum).ToString()));
                                counter++;
                            }
                            else
                            {
                                pdfTablelist[1].AddCell(CreatePDF.FontPolish(SetPercent(carPark.count, sum).ToString()));
                            }
                        }

                        return pdfTablelist;

                    }
                case FirstRow.Month:
                    {
                        counter = 0;               
                        foreach (var carPark in help)
                        {

                            if (counter < from)
                            {
                                pdfTablelist[0].AddCell(CreatePDF.FontPolish(Enum.GetName(typeof(EnumMonth), carPark.month)));
                                counter++;
                            }
                            else
                            {
                                pdfTablelist[1].AddCell(CreatePDF.FontPolish(Enum.GetName(typeof(EnumMonth), carPark.month)));
                            }
                        }
                        counter = 0;
                        foreach (var carPark in help)
                        {
                            if (counter < from)
                            {
                                pdfTablelist[0].AddCell(CreatePDF.FontPolish(SetPercent(carPark.count, sum).ToString()));
                                counter++;
                            }
                            else
                            {
                                pdfTablelist[1].AddCell(CreatePDF.FontPolish(SetPercent(carPark.count, sum).ToString()));
                            }
                        }

                        return pdfTablelist;
                    }
                default:
                    {
                        return null;
                    }
            }



           

        }


        public static List<PdfPTable> PdfTableByHours(List<IdParking> z, double sum)
        {
            List<PdfPTable> pdfTablelist = new List<PdfPTable>();
            pdfTablelist.Add(new PdfPTable(12));
            pdfTablelist.Add(new PdfPTable(12));
            pdfTablelist.Add(new PdfPTable(12));
            pdfTablelist.Add(new PdfPTable(12));

            for (int i = 0; i < 12; i++)
            {
                pdfTablelist[0].AddCell(CreatePDF.FontPolish(i.ToString()));
                pdfTablelist[1].AddCell(CreatePDF.FontPolish((i + 12).ToString()));
                pdfTablelist[2].AddCell(CreatePDF.FontPolish(i.ToString()));
                pdfTablelist[3].AddCell(CreatePDF.FontPolish((i + 12).ToString()));
            }
            for (int i = 0; i < 12; i++)
            {
                pdfTablelist[0].AddCell(CreatePDF.FontPolish(z[i].count.ToString()));
                pdfTablelist[1].AddCell(CreatePDF.FontPolish(z[i + 12].count.ToString()));
                pdfTablelist[2].AddCell(CreatePDF.FontPolish(SetPercent(z[i].count, sum).ToString()));
                pdfTablelist[3].AddCell(CreatePDF.FontPolish(SetPercent(z[i + 12].count, sum).ToString()));

            }
            return pdfTablelist;
        }

        public static double SetPercent(double x1, double sum)
        {
            double percent = Math.Round(((x1 / sum) * 100), 2);
            return percent;
        }
        internal static PdfPTable SumOfVehiclesInMonth(List<IdParking> list)
        {
            throw new NotImplementedException();
        }
    }
}
