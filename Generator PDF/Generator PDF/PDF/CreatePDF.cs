using Generator_PDF.GenerateChart;
using Generator_PDF.PDF;
using Generator_PDF.VM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Spire.Pdf;
using Spire.Pdf.AutomaticFields;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Image = iTextSharp.text.Image;

namespace Generator_PDF
{
    static class CreatePDF
    {
        static List<Image> vs = new List<Image>();
      

        public static Paragraph FontPolish(string stringToPdf)
        {
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true);
            iTextSharp.text.Font times = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.ITALIC);
            return new Paragraph(stringToPdf, times);
        }
       
        private static CreateChapter AddSubSection(CreateChapter chapter, string topicSubSection, string titleChart, Image pic, KeyValuePair<object, System.Drawing.Image> image, Dictionary<object, List<PdfPTable>> pdfPTables)
        {
            Paragraph elements = FontPolish(topicSubSection);
            elements.IndentationLeft = 40f;
            chapter.AddSubSection(elements, 3);
            chapter.createSubSection.AddTitleCHart(FontTitleChart(titleChart),pic.Width);
            chapter.createSubSection.AddImage(pic);
            vs.Add(ConvertImage(image.Value));
            Set(image, chapter.createSubSection, pdfPTables);
            return chapter;
        }

        private static Image ConvertImage(System.Drawing.Image image)
        {
            return Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        public static void GeneratePDF(Dictionary<object, System.Drawing.Image> images, Dictionary<object, List<PdfPTable>> pdfPTables) //, List<IdParking> listcarParks, List<List<IdParking>> listListCarParks, List<List<IdParking>> listListCarParksPercent)
        {                      
           

            using (Document document = new Document(PageSize.A4, 30f, 20f, 55f, 80f))
            {

                CreateChapter chapter = null;
                Dictionary<object, System.Drawing.Image> imagesbyMonth = images;              
                PdfWriter pdfWriter = CreateOrChooseFile(document);
                pdfWriter.PageEvent = new PdfPageEvent();
                document.Open();               
                bool sumOfParkedByMonth = false;
                bool lineChart = false;
                string name = null;
             
               CreateTableOfContents.AddTableOfContent(document, images, pdfWriter);
                string nameParking = String.Empty;
                string[] chwilowy = null;
                int chapterNumber = 1;
                foreach (var image in images)
                {
                   
                    try
                    {
                        Image pic = Image.GetInstance(image.Value, System.Drawing.Imaging.ImageFormat.Jpeg);


                        if (pic.Height > pic.Width)
                        {
                           
                            float percentage = 0.0f;
                            percentage = 700 / pic.Height;
                            pic.ScalePercent(percentage * 80);
                        }
                        else
                        {
                           
                            float percentage = 0.0f;
                            percentage = 540 / pic.Width;
                            pic.ScalePercent(percentage * 80);
                        }

                        if (image.Key is SumOfParked)
                        {
                            chwilowy = image.Value.Tag.ToString().Split(',');
                            chapter = AddChapter(chwilowy[1], chapterNumber, "Suma pojazdów z całego okresu", pic, image, pdfPTables);
                            document.Add(chapter.GetChapter());
                            chapter = null;
                            chapterNumber++;
                        }


                        if (image.Key is SumOfParkedByMonth)
                        {
                            sumOfParkedByMonth = true;
                            nameParking = null;
                            chwilowy = image.Value.Tag.ToString().Split(',');
                            for (int i = 2; i < chwilowy.Length; i++)
                            {
                                nameParking += $"{chwilowy[i]},";
                            }
                            chapter = AddChapterSectionAndSubSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.", $"Porównanie ilości wzbudzeń na Parkingu {nameParking} w danych miesiącach.", chwilowy[1], chapterNumber, "Porównanie parkingów", pic, image, pdfPTables);
                            document.Add(chapter.GetChapter());
                            chapterNumber++;

                            //      document.NewPage();
                        }
                        if (image.Key is SumOfParkedByMonthPercent)
                        {

                            chwilowy = image.Value.Tag.ToString().Split(',');

                            if (sumOfParkedByMonth == false)
                            {
                                nameParking = null;
                                sumOfParkedByMonth = true;
                                for (int i = 2; i < chwilowy.Length; i++)
                                {
                                    nameParking += $"{chwilowy[i]},";
                                }
                                chapter = AddChapterSectionAndSubSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.", $"Porównanie ilości wzbudzeń na Parkingu {nameParking} w danych miesiącach.", chwilowy[1], chapterNumber, "Porównanie parkingów", pic, image, pdfPTables);
                                document.Add(chapter.GetChapter());
                                chapterNumber++;
                            }
                            else
                            {
                                document.NewPage();
                                document.Add(AddSubSection(chapter, chwilowy[1], "Procentowe porównanie parkingów", pic, image, pdfPTables).GetSubSection());
                 
                            }




                        }

                        if (image.Key is SumOfVehiclesInMonthOnCarPark)
                        {
                            if (sumOfParkedByMonth == false)
                            {
                               
                                chwilowy = image.Value.Tag.ToString().Split(',');
                                nameParking = null;
                                for (int i = 2; i < chwilowy.Length; i++)
                                {
                                    nameParking += $"{chwilowy[i]},";
                                }
                                chapter = AddChapterSectionAndSubSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.", $"Ilość wzbudzeń na Parkingu {chwilowy[2]} w danych miesiacach", chwilowy[1] ,chapterNumber, chwilowy[2], pic, image, pdfPTables);
                                document.Add(chapter.GetChapter());
                                chapterNumber++;

                            }
                            else
                            {
                        
                                document.NewPage();
                                chwilowy = image.Value.Tag.ToString().Split(',');
                                document.Add(AddSectionAndSubSection(chapter, $"Ilość wzbudzeń na Parkingu {chwilowy[2]} w danych miesiacach", chwilowy[1], chwilowy[2], pic, image, pdfPTables).GetSection());
                            }




                        }


                        if (image.Key is SumOfParkedInEachMonthPercent)
                        {
                            chwilowy = image.Value.Tag.ToString().Split(',');                          
                           
                                document.NewPage();
                                document.Add(AddSubSection(chapter, chwilowy[1], $"{chwilowy[2]}-%", pic, image, pdfPTables).GetSubSection());
                             


                        }


                        if (image.Key is LineChart)
                        {
                            chapter = null;
                            nameParking = null;
                            lineChart = true;
                            chwilowy = image.Value.Tag.ToString().Split(',');
                            for (int i = 1; i < chwilowy.Length; i++)
                            {
                                nameParking += $"{chwilowy[i]},";
                            }
                            chapter = new CreateChapter(new Paragraph(FontPolish("Zestawienie godzinowe ilość wzbudzeń w całym okresie raportu - wartości procentowe.")), chapterNumber);
                                 chapter.AddSection(FontPolish($"Porównanie ilości wzbudzeń na parkingach {nameParking}"), 2);
                                                  chapter.AddTitleCHartToLineChart(new Paragraph(FontPolish("Porównanie %")),image.Value.Width);
                            chapter.AddImageToLineChart(pic);                            
                            vs.Add(ConvertImage(image.Value));                     
                            document.Add(chapter.GetChapter());
                            chapterNumber++;

                        }


                        if (image.Key is LineSumByHour)
                        {
                            if (lineChart == false)
                            {
                                chapter = new CreateChapter(new Paragraph(FontPolish("Zestawienie godzinowe ilość wzbudzeń w całym okresie raportu - wartości procentowe.")), chapterNumber);

                            }
                            document.NewPage();
                            chwilowy = image.Value.Tag.ToString().Split(',');                      
                            document.Add(AddSectionBySetLine(chapter, chwilowy[1], $"{chwilowy[1]}-%", pic, image, pdfPTables).GetSection());
                        }

                        if (image.Key is LineSumByHourEachMonth)
                        {
                            chwilowy = image.Value.Tag.ToString().Split(',');
                            if (name == null)
                            {
                                chapter = AddChapterSectionAndSubSectionBySetLine("Zestawienie godzinowe ilości wzbudzeń w podziale na poszczególne miesiące.", $"{chwilowy[1]}", $"{Enum.GetName(typeof(EnumMonth), int.Parse(chwilowy[2]))}", chapterNumber, $"{chwilowy[1]} - %", pic, image, pdfPTables);
                                document.Add(chapter.GetChapter());
                                name = chwilowy[1];
                                chapterNumber++;

                            }
                            else if (name == chwilowy[1])
                            {
                                document.NewPage();                                
                                document.Add(AddSubSectionBySetLine(chapter, $"{Enum.GetName(typeof(EnumMonth), int.Parse(chwilowy[2]))}", $"{chwilowy[1]} - %", pic, image, pdfPTables).GetSubSection());
                                 name = chwilowy[1];

                            }
                            else if (name != chwilowy[1])
                            {
                                document.NewPage();
                                document.Add(AddSectionAndSubSectionBySetLine(chapter, chwilowy[1], $"{Enum.GetName(typeof(EnumMonth), int.Parse(chwilowy[2]))}", $"{chwilowy[1]} - %", pic, image, pdfPTables).GetSection());
                                name = chwilowy[1];

                            }



                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }

                   
                }
                try
                {
                    document.Close();
                    MessageBox.Show("PDF Wygenerowany");
                }
                catch
                {
                    MessageBox.Show("Wystąpił problem z zamknięciem pliku PDF");
                }
            }

        }
        public static void Set(KeyValuePair<object, System.Drawing.Image> item, CreateChapter chapter, Dictionary<object, List<PdfPTable>> pdfPTables)
        {

            int parkingNumber = 0;
            chapter.AddText(FontPolish("Dokładne Dane:"));
            foreach (var table in pdfPTables)
            {
                if (table.Key == item.Key)
                {
                    foreach (var tab in table.Value)
                    {
                       
                        chapter.AddTable(tab);
                        parkingNumber++;
                    }
                }
            }
        }
        public static void Set(KeyValuePair<object, System.Drawing.Image> item, CreateSubSection chapter, Dictionary<object, List<PdfPTable>> pdfPTables)
        {

            int parkingNumber = 0;
            chapter.AddText(FontPolish("Dokładne Dane:"));
            foreach (var table in pdfPTables)
            {
                if (table.Key == item.Key)
                {
                    foreach (var tab in table.Value)
                    {
                      
                        chapter.AddTable(tab);
                        parkingNumber++;
                    }
                }
            }
        }
        public static void Set(KeyValuePair<object, System.Drawing.Image> item, CreateSection section, Dictionary<object, List<PdfPTable>> pdfPTables)
        {

            int parkingNumber = 0;
            section.AddText(FontPolish("Dokładne Dane:"));
            foreach (var table in pdfPTables)
            {
                if (table.Key == item.Key)
                {
                    foreach (var tab in table.Value)
                    {                       
                        section.AddTable(tab);
                        parkingNumber++;
                    }
                }
            }
        }
        public static void SetLine(KeyValuePair<object, System.Drawing.Image> item, CreateChapter section, Dictionary<object, List<PdfPTable>> pdfPTables)
        {

            int parkingNumber = 0;
            section.AddText(FontPolish("Dokładne Dane:"));
            foreach (var table in pdfPTables)
            {
                if (table.Key == item.Key)
                {
                    foreach (var tab in table.Value)
                    {
                        if (parkingNumber == 0)
                        {
                            section.AddText(FontPolish("Godziny przed południem:"));
                        }

                        if (parkingNumber == 1)
                        {
                            section.AddText(FontPolish("Godziny po południu:"));
                        }

                        if (parkingNumber == 2)
                        {
                            section.AddText(FontPolish("Godziny przed południem:"));
                        }

                        if (parkingNumber == 3)
                        {
                            section.AddText(FontPolish("Godziny po południu:"));
                        }
                        section.AddTable(tab);
                        parkingNumber++;
                    }
                }
            }
        }
        public static void SetLine(KeyValuePair<object, System.Drawing.Image> item, CreateSection section, Dictionary<object, List<PdfPTable>> pdfPTables)
        {
            Paragraph elements = new Paragraph();
            int parkingNumber = 0;
            section.AddText(FontPolish("Dokładne Dane:"));      
                foreach (var table in pdfPTables)
                {
                    if (table.Key == item.Key)
                    {
                        foreach (var tab in table.Value)
                        {
                            if (parkingNumber == 0)
                            {
                                elements = FontPolish("Wartość liczbowa");
                                elements.IndentationLeft = 10f;
                                section.AddText(elements);
                                elements = FontPolish("Godziny przed południem:");
                                elements.IndentationLeft = 20f;
                                section.AddText(elements);
                            }

                            if (parkingNumber == 1)
                            {
                                elements = FontPolish("Godziny po południu:");
                                elements.IndentationLeft = 20f;
                                section.AddText(elements);
                            }

                            if (parkingNumber == 2)
                            {
                                elements = FontPolish("Wartość procentowa");
                                elements.IndentationLeft = 10f;
                                section.AddText(elements);
                                elements = FontPolish("Godziny przed południem:");
                                elements.IndentationLeft = 20f;
                                section.AddText(elements);
                            }

                            if (parkingNumber == 3)
                            {
                                elements = FontPolish("Godziny po południu:");
                                elements.IndentationLeft = 20f;
                                section.AddText(elements);
                            }
                            section.AddTable(tab);
                            parkingNumber++;
                        }
                    }
                }
        }
        public static void SetLine(KeyValuePair<object, System.Drawing.Image> item, CreateSubSection section, Dictionary<object, List<PdfPTable>> pdfPTables)
        {
            Paragraph elements = new Paragraph();
            int parkingNumber = 0;
            section.AddText(FontPolish("Dokładne Dane:"));
            foreach (var table in pdfPTables)
            {
                if (table.Key == item.Key)
                {
                    foreach (var tab in table.Value)
                    {
                        if (parkingNumber == 0)
                        {
                            elements = FontPolish("Wartość liczbowa");
                            elements.IndentationLeft = 10f;
                            section.AddText(elements);
                            elements = FontPolish("Godziny przed południem:");
                            elements.IndentationLeft = 20f;
                            section.AddText(elements);
                        }

                        if (parkingNumber == 1)
                        {
                            elements = FontPolish("Godziny po południu:");
                            elements.IndentationLeft = 20f;
                            section.AddText(elements);
                        }

                        if (parkingNumber == 2)
                        {                           
                            elements = FontPolish("Wartość procentowa");
                            elements.IndentationLeft = 10f;
                            section.AddText(elements);
                            elements = FontPolish("Godziny przed południem:");
                            elements.IndentationLeft = 20f;
                            section.AddText(elements);
                        }

                        if (parkingNumber == 3)
                        {
                            elements = FontPolish("Godziny po południu:");
                            elements.IndentationLeft = 20f;
                            section.AddText(elements);
                        }
                        section.AddTable(tab);
                        parkingNumber++;
                    }
                }
            }
        }


        private static PdfWriter CreateOrChooseFile(Document document)
        {
            PdfWriter pdfWriter = null;
            Stream stream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveFileDialog.OpenFile()) != null)
                {
                    stream.Close();
                }
            }
            try
            {
                pdfWriter = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
            }
            catch
            {
                MessageBox.Show("Wystąpił problem z otworzeniem pliku PDF");
            }
            return pdfWriter;
        }

        public static Paragraph FontTitleChart(string stringTitleChart)
        {
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true);
            iTextSharp.text.Font times = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            return new Paragraph(stringTitleChart, times);
        }

        private static CreateChapter AddChapter(string topic, int chapterNumber, string titleChart, Image pic, KeyValuePair<object, System.Drawing.Image> image, Dictionary<object, List<PdfPTable>> pdfPTables)
        {
            CreateChapter chapter = new CreateChapter(FontPolish(topic), chapterNumber);
            chapter.AddTitleCHart(FontTitleChart(titleChart), pic.Width);
            chapter.AddImage(pic);
            Set(image, chapter, pdfPTables);
            vs.Add(ConvertImage(image.Value));
            return chapter;
        }
        private static CreateChapter AddChapterSectionAndSubSection(string topic, string topicSection, string topisSubSection, int chapterNumber, string titleChart, Image pic, KeyValuePair<object, System.Drawing.Image> image, Dictionary<object, List<PdfPTable>> pdfPTables)
        {
            CreateChapter chapter = new CreateChapter(FontPolish(topic), chapterNumber);
            chapter.AddSection(FontPolish(topicSection), 2);
            chapter.AddSubSection(FontPolish(topisSubSection), 3);
            chapter.createSubSection.AddTitleCHart(FontTitleChart(titleChart),pic.Width);
            chapter.createSection.AddImage(pic);
            vs.Add(ConvertImage(image.Value));
            Set(image, chapter.createSection, pdfPTables);
            return chapter;
        }
        private static CreateChapter AddChapterSectionAndSubSectionBySetLine(string topic, string topicSection, string topisSubSection, int chapterNumber, string titleChart, Image pic, KeyValuePair<object, System.Drawing.Image> image, Dictionary<object, List<PdfPTable>> pdfPTables)
        {
            CreateChapter chapter = new CreateChapter(FontPolish(topic), chapterNumber);
            chapter.AddSection(FontPolish(topicSection), 2);
            chapter.AddSubSection(FontPolish(topisSubSection), 3);
            chapter.createSubSection.AddTitleCHart(FontTitleChart(titleChart), pic.Width);
            chapter.createSection.AddImage(pic);
            vs.Add(ConvertImage(image.Value));
            SetLine(image, chapter.createSection, pdfPTables);
            return chapter;
        }
        private static CreateChapter AddSectionAndSubSection(CreateChapter chapter, string topicSection, string topisSubSection, string titleChart, Image pic, KeyValuePair<object, System.Drawing.Image> image, Dictionary<object, List<PdfPTable>> pdfPTables)
        {

            chapter.AddSection(FontPolish(topicSection), 2);
            chapter.AddSubSection(FontPolish(topisSubSection), 3);
            chapter.createSubSection.AddTitleCHart(FontTitleChart(titleChart),pic.Width);
            chapter.createSection.AddImage(pic);
            vs.Add(ConvertImage(image.Value));
            Set(image, chapter.createSection, pdfPTables);
            return chapter;
        }
        private static CreateChapter AddSectionAndSubSectionBySetLine(CreateChapter chapter, string topicSection,string topicSubSection, string titleChart, Image pic, KeyValuePair<object, System.Drawing.Image> image, Dictionary<object, List<PdfPTable>> pdfPTables)
        {

            chapter.AddSection(FontPolish(topicSection), 2);
            chapter.AddSubSection(FontPolish(topicSubSection), 3);
            chapter.createSubSection.AddTitleCHart(FontTitleChart(titleChart), pic.Width);
            chapter.createSubSection.AddImage(pic);
            vs.Add(ConvertImage(image.Value));
            SetLine(image, chapter.createSection, pdfPTables);
            return chapter;


                 }
        private static CreateChapter AddSectionBySetLine(CreateChapter chapter, string topicSection, string titleChart, Image pic, KeyValuePair<object, System.Drawing.Image> image, Dictionary<object, List<PdfPTable>> pdfPTables)
        {

            chapter.AddSection(FontPolish(topicSection), 2);
            chapter.createSection.AddTitleCHart(FontTitleChart(titleChart), pic.Width);
            chapter.createSection.AddImage(pic);
            vs.Add(ConvertImage(image.Value));
            SetLine(image, chapter.createSection, pdfPTables);
            return chapter;
        }
        private static CreateChapter AddSubSectionBySetLine(CreateChapter chapter, string topicSubSection, string titleChart, Image pic, KeyValuePair<object, System.Drawing.Image> image, Dictionary<object, List<PdfPTable>> pdfPTables)
        {
            Paragraph elements = FontPolish(topicSubSection);
            elements.IndentationLeft = 40f;
            chapter.AddSubSection(elements, 3);
            chapter.createSubSection.AddTitleCHart(FontTitleChart(titleChart), pic.Width);
            chapter.createSubSection.AddImage(pic);
                vs.Add(ConvertImage(image.Value));
            SetLine(image, chapter.createSubSection, pdfPTables);
            return chapter;


          
        }
      

    }



}