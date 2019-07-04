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
    class CreateSection : IChapterAndSectionMethod
    {
     public Section section;
        public CreateSection(Chapter chapter, string sectionTitle)
        {
            section = chapter.AddSection(new Paragraph(sectionTitle, FontFactory.GetFont("Arial", 15f, new BaseColor(Color.Blue))));
        }
        public void AddImage(iTextSharp.text.Image image)
        {
            image.Alignment = Element.ALIGN_CENTER;
            section.Add(image);
        }
        public void AddText(string text)
        {
            Paragraph paragraph = new Paragraph(text, FontFactory.GetFont("Arial", 15f, new BaseColor(Color.Blue)));
         //   paragraph.Alignment = Element.ALIGN_CENTER;
            section.Add(paragraph);
        }
        public void AddTitleCHart(string text)
        {
            Paragraph paragraph = new Paragraph(text, FontFactory.GetFont("Arial", 15f, new BaseColor(Color.Blue)));
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.SetLeading(1.0f, 4.0f);
            section.Add(paragraph);
        }
        public void AddTable(PdfPTable table)
        {
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            Paragraph paragraph = new Paragraph();
            paragraph.SetLeading(1.0f, 3.0f);
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Add(table);
            paragraph.SetLeading(1.0f, 3.0f);
            section.Add(paragraph);
        }
        public Section GetSection()
        {
            return section;
        }
    }
}
