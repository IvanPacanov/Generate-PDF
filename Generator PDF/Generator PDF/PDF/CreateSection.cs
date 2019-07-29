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
        public CreateSection(Chapter chapter, Paragraph title, int numberDepth)
        {
            title.Font.Size = 13;
            title.SetLeading(2.0f, 2.0f);
            section = chapter.AddSection(20f,title, numberDepth);
            section.IndentationLeft = 20f;
            
        }
        public void AddImage(iTextSharp.text.Image image)
        {
            image.Alignment = Element.ALIGN_LEFT;
            image.SpacingBefore = 50;
            section.Add(image);
        }
        public void AddText(Paragraph text)
        {
               //   paragraph.Alignment = Element.ALIGN_CENTER;
            section.Add(text);
        }
        public void AddTitleCHart(Paragraph text, float imageWidth)
        {
             text.Alignment = Element.ALIGN_LEFT;
            text.SetLeading(1.0f, 4.0f);
            text.IndentationLeft = ((540 / imageWidth) * 80)*2;
            section.Add(text);
        }
        public void AddTable(PdfPTable table)
        {
            table.SpacingBefore = 10f;
            table.SpacingAfter = 12.5f;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph paragraph = new Paragraph();
       //     paragraph.SetLeading(1.0f, 3.0f);
       //     paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Add(table);
      //      paragraph.SetLeading(1.0f, 3.0f);
            section.Add(paragraph);
        }
        public Section GetSection()
        {
            return section;
        }
    }
}
