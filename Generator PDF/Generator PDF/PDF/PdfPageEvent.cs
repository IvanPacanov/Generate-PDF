using Generator_PDF.VM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Generator_PDF.PDF
{
    public class PdfPageEvent : PdfPageEventHelper
    {

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        iTextSharp.text.pdf.PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;


        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                System.Drawing.Image image= null;
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                ;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = ".jpg|*.jpg| .png|*.png";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    image = System.Drawing.Image.FromFile(openFile.FileName);
                }
                
               Image pic = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true);
                Font times = new Font(baseFont, 25, Font.NORMAL);
                pic.Alignment = Element.ALIGN_CENTER;
                pic.Bottom = 100;
                Paragraph elements = new Paragraph("Raport zajętości czujników parkingowych", times);
                elements.Alignment = Element.ALIGN_CENTER;
                elements.SpacingBefore = 50f;

                BaseColor color = new BaseColor(22, 129, 186);
                document.Add(pic);
                document.Add(elements);
                Paragraph p = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0f, color, Element.ALIGN_LEFT, 3)));
                document.Add(p);

                Font blue = new Font(baseFont, 12, Font.ITALIC, color);
                Chunk blueText = new Chunk("Niniejszy raport poświęcony jest przekazaniu statystyk pozyskanych z czujników parkingowych  ", blue);
                Paragraph elements1 = new Paragraph(blueText);
                elements1.Alignment = Element.ALIGN_CENTER;
                document.Add(elements1);
                var from = MVGeneratePDF.availableFrom.Value;
                var to = MVGeneratePDF.availableTo.Value;

                BaseFont baseFontDate = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true);
                Font timesDate = new Font(baseFontDate, 20, Font.BOLD);


                Paragraph date = new Paragraph($" Raportowany okres: {from.ToShortDateString()} – {to.ToShortDateString()} r.", timesDate);
                date.SetLeading(1.0f, 8.0f);
                date.Alignment = Element.ALIGN_CENTER;
                document.Add(date);




            }
            catch (DocumentException de)
            {
                MessageBox.Show(de.ToString());
            }
         
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
          //  writer.PageEmpty = false;
            base.OnEndPage(writer, document);
            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            Phrase p1Header = new Phrase("KSK Developments", baseFontNormal);

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(1);

            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1

            PdfPCell pdfCell2 = new PdfPCell(p1Header);

            String text = "Page " + writer.PageNumber + " of ";
            
            cb = writer.DirectContent;
            

            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(150), document.PageSize.GetBottom(25));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(150) + len, document.PageSize.GetBottom(25));
            }


            pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;




            pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;






            pdfCell2.Border = 0;



            //add all three cells into PdfTable

            pdfTab.AddCell(pdfCell2);


            pdfTab.TotalWidth = document.PageSize.Width - 40f;
            pdfTab.WidthPercentage = 70;



            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);

            //Move the pointer and draw line to separate header section from rest of page
            cb.MoveTo(40, document.PageSize.Height - 50);
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 50);
            cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(40));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(40));
            cb.Stroke();
        }
        public override void OnStartPage(PdfWriter writer, Document document)
        {
         //   writer.PageEmpty = false;
            base.OnStartPage(writer, document);
            var cbb = writer.DirectContentUnder;
        
            cbb.RoundRectangle(0f, 40f, document.PageSize.Width, document.PageSize.Height - 120f, 1f);
            cbb.SetColorFill(new CMYKColor(0f, 0f, 0f, 0f));
            cbb.Fill();
        }
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
        
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();
  
            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber).ToString());
            footerTemplate.EndText();
           


        }
    }
}
