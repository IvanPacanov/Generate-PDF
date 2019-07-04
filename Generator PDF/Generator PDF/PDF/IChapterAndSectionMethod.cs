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

         void AddText(string text);

         void AddTitleCHart(string text);

         void AddTable(PdfPTable table);
      
    }
}
