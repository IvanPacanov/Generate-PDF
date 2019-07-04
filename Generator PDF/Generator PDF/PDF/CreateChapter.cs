using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.PDF
{
    class CreateChapter : IChapterAndSectionMethod
    {
        public Chapter chapter;
       public CreateSection createSection;
        
        public CreateChapter(string title, int chapterNumber)
        {
            Paragraph paragraph = new Paragraph(title, FontFactory.GetFont("Arial", 15f, new BaseColor(Color.Blue))) { Alignment = Element.ALIGN_CENTER };
            paragraph.SetLeading(4.0f, 4.0f);
            chapter = new Chapter(paragraph, chapterNumber);
        }
        public void AddImage(iTextSharp.text.Image image)
        {
            image.Alignment = Element.ALIGN_CENTER;
            chapter.Add(image);
        }
        public void AddText(string text)
        {
            Paragraph paragraph = new Paragraph(text, FontFactory.GetFont("Arial", 15f, new BaseColor(Color.Blue)));
        //   paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.SetLeading(1.0f, 4.0f);
            chapter.Add(paragraph);
        }
        public void AddTitleCHart(string text)
        {
            Paragraph paragraph = new Paragraph(text, FontFactory.GetFont("Arial", 15f, new BaseColor(Color.Blue)));
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.SetLeading(1.0f, 4.0f);
            chapter.Add(paragraph);
        }
        public void AddTable(PdfPTable table)
        {
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            Paragraph paragraph = new Paragraph();
            paragraph.SetLeading(1.0f, 3.0f);
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Add(table);
            paragraph.SetLeading(1.0f, 3.0f);
            chapter.Add(paragraph);
        }
        public void AddSection(string sectionTitle)
        {
             createSection = new CreateSection(chapter, sectionTitle);
        }
        public Chapter GetChapter()
        {            
            return chapter;
        }
        public Section GetSection()
        {
            return createSection.section;
        }


    }
}
