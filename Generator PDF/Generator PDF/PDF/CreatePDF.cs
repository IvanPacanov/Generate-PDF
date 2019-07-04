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
        public static void GeneratePDF(Dictionary<object,System.Drawing.Image> images, Dictionary<object, List<PdfPTable>> pdfPTables) //, List<IdParking> listcarParks, List<List<IdParking>> listListCarParks, List<List<IdParking>> listListCarParksPercent)
        {
            CreateChapter chapter2 = null ;
            CreateChapter chapter = null;
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            List<System.Drawing.Image> vs = new List<System.Drawing.Image>();

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
            PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));




            document.Open();

            images = images.OrderBy(x => x.Value.Tag).ToDictionary(x=>x.Key, x=>x.Value);
//pdfPTables= pdfPTables.OrderBy(x => x.Value.Tag).ToDictionary(x => x.Key, x => x.Value);




            foreach (var image in images)
                {

                    if (!vs.Contains(image.Value))
                    {
                        if (int.Parse(image.Value.Tag.ToString()) == 5)
                        {
                            iTextSharp.text.Image pivc = iTextSharp.text.Image.GetInstance(image.Value, System.Drawing.Imaging.ImageFormat.Jpeg);

                            if (pivc.Height > pivc.Width)
                            {
                                //Maximum height is 800 pixels.
                                float percentage = 0.0f;
                                percentage = 700 / pivc.Height;
                                pivc.ScalePercent(percentage * 100);
                            }
                            else
                            {
                                //Maximum width is 600 pixels.
                                float percentage = 0.0f;
                                percentage = 540 / pivc.Width;
                                pivc.ScalePercent(percentage * 100);
                            }

                            //   pic.Border = iTextSharp.text.Rectangle.BOX;
                            //  pic.BorderColor = iTextSharp.text.BaseColor.BLACK;
                            //     pic.BorderWidth = 3f;
                            Paragraph paragrapha = new Paragraph("Suma pojazdó z całego okresu", FontFactory.GetFont("Arial", 15f, new BaseColor(Color.Blue)));
                            paragrapha.SetLeading(1.0f, 3.0f);
                            //          chapter2.Add(paragrapha);



                            //       chapter2.Add(pivc);
                            vs.Add(image.Value);
                            paragrapha = new Paragraph();
                            paragrapha.SetLeading(1.0f, 3.0f);
                            break;
                        }
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
                        if  (image.Key is SumOfParkedByMonthPercent)
                        {
                        
                            chapter2.AddSection("Ilosc wzbudzeń w podziale na poszczególne miesiące.");
                            chapter2.createSection.AddTitleCHart("Wartosc Procentowa");
                            chapter2.createSection.AddImage(pic);
                            vs.Add(image.Value);
                            Set(image, chapter2.createSection, pdfPTables);
                            document.Add(chapter2.GetSection());

                        }
                     
                    }



























                    //pic.Alignment = Element.ALIGN_CENTER;

                    //Paragraph paragraphaz = new Paragraph("Suma pojazdów z całego okresu", FontFactory.GetFont("Arial", 10f, new BaseColor(Color.Blue))) { Alignment = Element.ALIGN_CENTER };
                    //paragraphaz.SetLeading(1.0f, 3.0f);
                    //chapter2.Add(paragraphaz);
                    //chapter2.Add(pic);
                    //vs.Add(image);
                    //Paragraph paragraph = new Paragraph();
                    //paragraph.SetLeading(1.0f, 3.0f);

                    //chapter2.Add(paragraph);
                    //paragraph = new Paragraph();
                    //paragraph.SetLeading(1.0f, 3.0f);




                
              


                document.NewPage();


            }



            //Paragraph title2 = new Paragraph() { "This is Chapter 2" };
            ////Chapter chapter2 = new Chapter(title2, 2);
            //chapter2.SetChapterNumber(0);
            //Paragraph someText = new Paragraph("This is some text");
            //chapter2.Add(someText);
            //Paragraph title21 = new Paragraph(){"This is Section 1 in Chapter 2"};
            //Section section1 = chapter2.AddSection(title21);
            //Paragraph someSectionText = new Paragraph("This is some silly paragraph in a chapter and/or section. It contains some text to test the functionality of Chapters and Section.");
            //section1.Add(someSectionText);
            try
            {
                document.Close();
            }
            catch
            {

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
                            chapter.AddText(MVGeneratePDF.idParkings[parkingNumber].name);
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
                            section.AddText(MVGeneratePDF.idParkings[parkingNumber].name);
                        }
                        section.AddTable(tab);
                        parkingNumber++;
                    }
                }
            }



            //double b = double.Parse(image.Tag.ToString());
            //int j = 0;
            //while (j != 100)
            //{
            //    if (b == item.Key)
            //    {
            //        break;
            //    }
            //    j++;
            //    b = b + b;
            //}
            //// if (b == item.Key)
            //for (int i = 0; i < item.Value.Count; i++)
            //{

            //    chapter.AddText("Dokładne Dane:");
            //    chapter.AddTable(pdfPTables[item.Key][i]);



            //}
        }
    }
}



//   foreach (var image in images)
//                {

//                    if (!vs.Contains(image))
//                    {
//                        if (int.Parse(image.Tag.ToString()) == 5)
//                        {
//                            iTextSharp.text.Image pivc = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);

//                            if (pivc.Height > pivc.Width)
//                            {
//                                //Maximum height is 800 pixels.
//                                float percentage = 0.0f;
//percentage = 700 / pivc.Height;
//                                pivc.ScalePercent(percentage* 100);
//                            }
//                            else
//                            {
//                                //Maximum width is 600 pixels.
//                                float percentage = 0.0f;
//percentage = 540 / pivc.Width;
//                                pivc.ScalePercent(percentage* 100);
//                            }

//                            //   pic.Border = iTextSharp.text.Rectangle.BOX;
//                            //  pic.BorderColor = iTextSharp.text.BaseColor.BLACK;
//                            //     pic.BorderWidth = 3f;
//                            Paragraph paragrapha = new Paragraph("Suma pojazdó z całego okresu", FontFactory.GetFont("Arial", 15f, new BaseColor(Color.Blue)));
//paragrapha.SetLeading(1.0f, 3.0f);
//                            chapter2.Add(paragrapha);



//                            chapter2.Add(pivc);
//                            vs.Add(image);
//                            paragrapha = new Paragraph();
//paragrapha.SetLeading(1.0f, 3.0f);
//                            break;
//                        }


//                        iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);

//                        if (pic.Height > pic.Width)
//                        {
//                            //Maximum height is 800 pixels.
//                            float percentage = 0.0f;
//percentage = 700 / pic.Height;
//                            pic.ScalePercent(percentage* 100);
//                        }
//                        else
//                        {
//                            //Maximum width is 600 pixels.
//                            float percentage = 0.0f;
//percentage = 540 / pic.Width;
//                            pic.ScalePercent(percentage* 100);
//                        }
//                        pic.Alignment = Element.ALIGN_CENTER;

//                        Paragraph paragraphaz = new Paragraph("Suma pojazdów z całego okresu", FontFactory.GetFont("Arial", 10f, new BaseColor(Color.Blue))) { Alignment = Element.ALIGN_CENTER };
//paragraphaz.SetLeading(1.0f, 3.0f);
//                        chapter2.Add(paragraphaz);
//                        chapter2.Add(pic);
//                        vs.Add(image);
//                        Paragraph paragraph = new Paragraph();
//paragraph.SetLeading(1.0f, 3.0f);

//                        chapter2.Add(paragraph);
//                        paragraph = new Paragraph();
//paragraph.SetLeading(1.0f, 3.0f);


//                        int j = 0;
//double b = double.Parse(image.Tag.ToString());
//                        while (j != 100)
//                        {
//                            if (b == item.Key)
//                            {
//                                break;
//                            }
//                            j++;
//                            b = b + b;
//                        }
//                        if (b == item.Key)
//                            for (int i = 0; i<item.Value.Count; i++)
//                            {
//                                paragraph = new Paragraph();
//paragraph.Add("Dokładne Dane:");
//                                paragraph.SetLeading(1.0f, 3.0f);
//                                paragraph.Add(pdfPTables[item.Key][i]);


//                                chapter2.Add(paragraph);
//                            }
//                        break;
//                    }
//                }
//                document.Add(chapter2);


//                document.NewPage();


//            }



//            //Paragraph title2 = new Paragraph() { "This is Chapter 2" };
//            ////Chapter chapter2 = new Chapter(title2, 2);
//            //chapter2.SetChapterNumber(0);
//            //Paragraph someText = new Paragraph("This is some text");
//            //chapter2.Add(someText);
//            //Paragraph title21 = new Paragraph(){"This is Section 1 in Chapter 2"};
//            //Section section1 = chapter2.AddSection(title21);
//            //Paragraph someSectionText = new Paragraph("This is some silly paragraph in a chapter and/or section. It contains some text to test the functionality of Chapters and Section.");
//            //section1.Add(someSectionText);
//            try
//            {
//                document.Close();
//            }
//            catch
//            {

//            }
//            MessageBox.Show("PDF Wygenerowany");
//        }

//    }
//}
