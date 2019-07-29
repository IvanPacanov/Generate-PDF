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
    class CreateSubSection : IChapterAndSectionMethod
    {
        public Section subSection;
        public CreateSubSection(Section section, Paragraph title, int numberDepth)
        {
            title.Font.Size = 13;
            title.SetLeading(2.0f, 2.0f);
            title.SpacingAfter = 20f;
            subSection = section.AddSection(20f, title, numberDepth);
        }
        public void AddImage(iTextSharp.text.Image image)
        {
            image.Alignment = Element.ALIGN_LEFT;
      //      image.SpacingBefore = 50;
            subSection.Add(image);
        }
        public void AddText(Paragraph text)
        {
             //   paragraph.Alignment = Element.ALIGN_CENTER;
            subSection.Add(text);
        }
   
        public void AddTitleCHart(Paragraph paragraph, float imageWidth)
        {
     
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.SetLeading(1.0f, 2.0f);
            
            paragraph.IndentationLeft = ((540 / imageWidth) * 80)*2;

            subSection.Add(paragraph);
        }
        public void AddTable(PdfPTable table)
        {
            table.SpacingBefore = 10f;
            table.SpacingAfter = 12.5f;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph paragraph = new Paragraph();
        //    paragraph.SetLeading(1.0f, 3.0f);
  //          paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Add(table);
        //    paragraph.SetLeading(1.0f, 3.0f);
            subSection.Add(paragraph);
        }
        public Section GetSection()
        {
            return subSection;
        }
    }
}
