using Generator_PDF.GenerateChart;
using Generator_PDF.VM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.PDF
{
   public static class CreateTableOfContents
    {        
        public static void AddTableOfContent(Document document, Dictionary<object, System.Drawing.Image> images, PdfWriter writer)
        {

            Dictionary<object, System.Drawing.Image> imagesbyMonth = images;

       
            bool cos1 = true;
            CreateChapter chapter = new CreateChapter(CreatePDF.FontPolish("Spis Treści"), 0, 3f);
            chapter.chapter.NumberDepth = 0;
            string name = null;
            int page = 3;
            int numberChapert = 1;
            int numberSection = 1;
            int numberSubSection = 1;


            foreach (var image in images)
            {
                string[] chwilowy = image.Value.Tag.ToString().Split(',');
                string nameParking = null;
                for (int i = 2; i < chwilowy.Length; i++)
                {
                    nameParking += $"{chwilowy[i]},";
                }

                if (image.Key is SumOfParked)
                {
                    chapter.AddToTableOfContents(AddHypperLink($"{numberChapert++}.          {chwilowy[1]}", page++, writer));
                  
                }


                if (image.Key is SumOfParkedByMonth)
                {
                    cos1 = false;
                    chapter.AddToTableOfContents(AddHypperLink($"{numberChapert}.          Ilosc wzbudzeń w podziale na poszczególne miesiące.", page, writer));
                    chapter.AddToTableOfContents(AddHypperLink($"{numberChapert}.{numberSection}.          Porównanie ilości wzbudzeń na Parkingu {nameParking} w danych miesiącach.", page, writer));
                    chapter.AddToTableOfContents(AddHypperLink($"          {numberChapert}.{numberSection}.{numberSubSection++}.    Wartość liczbowa.", page++, writer));

                }
                if (image.Key is SumOfParkedByMonthPercent)
                {
                    if (cos1 == true)
                    {
                        chapter.AddToTableOfContents(AddHypperLink($"{numberChapert}          Ilosc wzbudzeń w podziale na poszczególne miesiące.", page, writer));
                        chapter.AddToTableOfContents(AddHypperLink($"{numberChapert}.{numberSection}.          Porównanie ilości wzbudzeń na Parkingu {nameParking} w danych miesiącach.", page, writer));
                        chapter.AddToTableOfContents(AddHypperLink($"          {numberChapert}.{numberSection}.{numberSubSection++}.    Wartość Procentowa.", page++, writer));
                    }
                    else
                    {
                        chapter.AddToTableOfContents(AddHypperLink($"          {numberChapert}.{numberSection++}.{numberSubSection++}.    Wartość Procentowa.", page++, writer));
                    }

                }

                if (image.Key is SumOfVehiclesInMonthOnCarPark)
                {
                    numberSubSection = 1;
                    chapter.AddToTableOfContents(AddHypperLink($"   {numberChapert}.{numberSection}.          Ilość wzbudzeń na Parkingu {chwilowy[2]} w danych miesiacach", page, writer));
                    chapter.AddToTableOfContents(AddHypperLink($"          {numberChapert}.{numberSection}.{numberSubSection++}.    " + chwilowy[1], page++, writer));
                }


                if (image.Key is SumOfParkedInEachMonthPercent)
                {

                        chapter.AddToTableOfContents(AddHypperLink($"          {numberChapert}.{numberSection++}.{numberSubSection++}.    " + chwilowy[1], page++, writer));
                

                }


                if (image.Key is LineChart)
                {
                    numberSection = 1;
                    numberSubSection = 1;
                    chapter.AddToTableOfContents(AddHypperLink($"{++numberChapert}.          Zestawienie godzinowe ilość wzbudzeń w całym okresie raportu - wartości procentowe.", page, writer));
                    chapter.AddToTableOfContents(AddHypperLink($"   {numberChapert}.{numberSection++}.          Porównanie ilości wzbudzeń na parkingach {nameParking}", page++, writer));

                }


                if (image.Key is LineSumByHour)
                {

                    chapter.AddToTableOfContents(AddHypperLink($"   {numberChapert}.{numberSection++}.          {chwilowy[1]}", page++, writer));

                }

                if (image.Key is LineSumByHourEachMonth)
                {
                    if (name == null)
                    {
                        numberSection = 1;
                        numberSubSection = 1;
                        chapter.AddToTableOfContents(AddHypperLink($"{++numberChapert}.         Zestawienie godzinowe ilości wzbudzeń w podziale na poszczególne miesiące.", page, writer));
                        chapter.AddToTableOfContents(AddHypperLink($"   {numberChapert}.{numberSection}.          {chwilowy[1]}", page, writer));
                        chapter.AddToTableOfContents(AddHypperLink($"          {numberChapert}.{numberSection}.{numberSubSection++}.    { Enum.GetName(typeof(EnumMonth), int.Parse(chwilowy[2])) }", page++, writer));
                        name = chwilowy[1];
                    }
                    else if (name == chwilowy[1])
                    {
                        chapter.AddToTableOfContents(AddHypperLink($"          {numberChapert}.{numberSection}.{numberSubSection++}.    { Enum.GetName(typeof(EnumMonth), int.Parse(chwilowy[2])) }", page++, writer));
                        name = chwilowy[1];


                    }
                    else if (name != chwilowy[1])
                    {
                        numberSubSection = 1;
                        chapter.AddToTableOfContents(AddHypperLink($"    {chwilowy[1]}", page, writer));
                        chapter.AddToTableOfContents(AddHypperLink($"          {numberChapert}.{++numberSection}.{numberSubSection++}.    { Enum.GetName(typeof(EnumMonth), int.Parse(chwilowy[2])) }", page++, writer));
                        name = chwilowy[1];
                    }
                }
            }
            document.Add(chapter.GetChapter());
        }
        public static Paragraph AddHypperLink(string name, int page, PdfWriter writer)
        {
            Chunk dottedLine = new Chunk(new DottedLineSeparator());
            Chunk chunk = new Chunk(FontPolishChunk(name));
            Chunk pageNumber = new Chunk($"{page}");
            PdfAction action = PdfAction.GotoLocalPage(page, new PdfDestination(0), writer);
            chunk.SetAction(action);
            dottedLine.SetAction(action);
            pageNumber.SetAction(action);
            Paragraph pas = new Paragraph(chunk);
            pas.Add(dottedLine);
            pas.Add(pageNumber);
            return pas;
        }
        public static Chunk FontPolishChunk(string stringToPdf)
        {
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true);
            iTextSharp.text.Font times = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.ITALIC);
            return new Chunk(stringToPdf, times);
        }
    }
}
