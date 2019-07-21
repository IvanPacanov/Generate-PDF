using Generator_PDF.GenerateChart;
using Generator_PDF.PDF;
using Generator_PDF.VM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator_PDF
{
    static class CreatePDF
    {
       public static int minMont =99;
        internal static int maxMont=0;

        public static void GeneratePDF(Dictionary<object, System.Drawing.Image> images, Dictionary<object, List<PdfPTable>> pdfPTables) //, List<IdParking> listcarParks, List<List<IdParking>> listListCarParks, List<List<IdParking>> listListCarParksPercent)
        {
            int minMontcounter=minMont;
            CreateChapter chapter4 = null;
            CreateChapter chapter3 = null;
            CreateChapter chapter2 = null;
            CreateChapter chapter = null;
          
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            List<System.Drawing.Image> vs = new List<System.Drawing.Image>();
            Dictionary<object, System.Drawing.Image> imagesbyMonth = images;

            CreateOrChooseFile(document);
            document.Open();

            images = images.OrderBy(x => x.Value.Tag).ToDictionary(x => x.Key, x => x.Value);           

            foreach (var image in images)
            {

                if (!vs.Contains(image.Value))
                {
                  
                    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image.Value, System.Drawing.Imaging.ImageFormat.Jpeg);

                    if (pic.Height > pic.Width)
                    {
                        //Maximum height is 800 pixels.
                        float percentage = 0.0f;
                        percentage = 700 / pic.Height;
                        pic.ScalePercent(percentage * 100);
                    }
                    else
                    {
                        //Maximum width is 600 pixels.
                        float percentage = 0.0f;
                        percentage = 540 / pic.Width;
                        pic.ScalePercent(percentage * 100);
                    }

                    if (image.Key is SumOfParked)
                    {
                        chapter = new CreateChapter("Suma wzbudzen parkujących(wzbudzen) w całym okresie raportu", 1);
                        chapter.AddTitleCHart("Suma pojazdów z całego okresu");
                        chapter.AddImage(pic);
                        vs.Add(image.Value);
                        Set(image, chapter, pdfPTables);
                        document.Add(chapter.GetChapter());
                    }


                    if (image.Key is SumOfParkedByMonth)
                    {
                        chapter2 = new CreateChapter("Ilosc wzbudzeń w podziale na poszczególne miesiące.", 2);
                        chapter2.AddSection("Porównanie ilości wzbudzeń w danym miesiącu.");
                        chapter2.AddTitleCHart("Wartosc liczbowa");
                        chapter2.AddImage(pic);
                        vs.Add(image.Value);
                        Set(image, chapter2, pdfPTables);
                        document.Add(chapter2.GetChapter());

                    }
                    if (image.Key is SumOfParkedByMonthPercent)
                    {
                        try
                        {

                            chapter2.AddSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.");
                        }
                        catch
                        {
                            chapter2 = new CreateChapter("Ilosc wzbudzeń w podziale na poszczególne miesiące.", 2);
                            chapter2.AddSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.");

                        }
                        chapter2.createSection.AddTitleCHart("Wartosc Procentowa");
                        chapter2.createSection.AddImage(pic);
                        vs.Add(image.Value);
                        Set(image, chapter2.createSection, pdfPTables);
                        document.Add(chapter2.GetSection());

                    }
                    if (image.Key is SumOfParkedInEachMonthPercent || image.Key is SumOfVehiclesInMonthOnCarPark)
                    {
                        chapter3 = new CreateChapter("Zestawienie godzinowe ilośd wzbudzen w całym okresie raportu - wartości procentowe.", 3);
                        chapter3.AddSection("Porównanie ilości wzbudzeo na parkingach");
                        //  chapter3.AddTitleCHart();
                        chapter3.AddImage(pic);
                        vs.Add(image.Value);
                        Set(image, chapter3, pdfPTables);
                        document.Add(chapter3.GetChapter());
                    }



                    if (image.Key is LineChart)
                    {
                        chapter4 = new CreateChapter("Liniowy Test", 4);
                        chapter4.AddSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.");
                        chapter4.createSection.AddTitleCHart("Wartosc Procentowa");
                        chapter4.createSection.AddImage(pic);
                        vs.Add(image.Value);
                        document.Add(chapter4.GetSection());
                    }
                    if (image.Key is LineSumByHour)
                    {
                        try
                        {
                            chapter4.AddSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.");
                        }
                        catch
                        {
                            chapter4 = new CreateChapter("Liniowy Test", 4);
                            chapter4.AddSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.");
                        }
                            chapter4.createSection.AddTitleCHart("Wartosc Procentowa");
                        chapter4.createSection.AddImage(pic);
                        vs.Add(image.Value);
                        Set(image, chapter4.createSection, pdfPTables);
                        document.Add(chapter4.GetSection());
                    }      
                    
                    if (image.Key is ListSumByHourEachMonth)
                    {
                       if(minMont>maxMont+1)
                        {
                            minMont = minMontcounter;
                        }
                        try
                        {
                            chapter4.AddSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.");
                        }
                        catch
                        {
                            chapter4 = new CreateChapter("Liniowy Test", 4);
                            chapter4.AddSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.");
                        }
                        chapter4.createSection.AddTitleCHart(Enum.GetName(typeof(EnumMonth), minMont));
                        chapter4.createSection.AddImage(pic);
                        vs.Add(image.Value);
                        Set(image, chapter4.createSection, pdfPTables);
                        document.Add(chapter4.GetSection());
                        minMont++;
                    }
                }
                document.NewPage();
            }     
            try
            {
                document.Close();
            }
            catch
            {
                MessageBox.Show("Wystąpił problem z zamknięciem pliku PDF");
            }

            MessageBox.Show("PDF Wygenerowany");
        }
        public static void Set(KeyValuePair<object, System.Drawing.Image> item, CreateChapter chapter, Dictionary<object, List<PdfPTable>> pdfPTables)
        {
           
            int parkingNumber = 0;
            chapter.AddText("Dokładne Dane:");
            foreach (var table in pdfPTables)
            {
                if (table.Key == item.Key)
                {
                    foreach (var tab in table.Value)
                    {
                        if (table.Value.Count > 1)
                        {
                    //        chapter.AddText(MVGeneratePDF.idParkings[parkingNumber].name);
                        }
                        chapter.AddTable(tab);
                        parkingNumber++;
                    }
                }
            }
        }
        public static void Set(KeyValuePair<object, System.Drawing.Image> item, CreateSection section, Dictionary<object, List<PdfPTable>> pdfPTables)
        {
          
            int parkingNumber = 0;
            section.AddText("Dokładne Dane:");
            foreach (var table in pdfPTables)
            {
                if (table.Key == item.Key)
                {
                    foreach (var tab in table.Value)
                    {
                        if (table.Value.Count > 1)
                        {
                      //      section.AddText(MVGeneratePDF.idParkings[parkingNumber].name);
                        }
                        section.AddTable(tab);
                        parkingNumber++;
                    }
                }
            }
        }
        private static void CreateOrChooseFile(Document document)
        {
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
                PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
            }
            catch
            {
                MessageBox.Show("Wystąpił problem z otworzeniem pliku PDF");
            }
        }
    }
}