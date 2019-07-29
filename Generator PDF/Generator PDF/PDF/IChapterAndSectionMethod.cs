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
    interface IChapterAndSectionMethod
    {       
         void AddImage(iTextSharp.text.Image image);

         void AddText(Paragraph text);

         void AddTitleCHart(Paragraph text, float widthImage);

         void AddTable(PdfPTable table);
      
    }
}
