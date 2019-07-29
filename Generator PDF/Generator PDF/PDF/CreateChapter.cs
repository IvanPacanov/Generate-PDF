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
        public CreateSubSection createSubSection;
      

        public CreateChapter(Paragraph title, int chapterNumber)
        {
            title.Font.Size = 13;
            //       title.Alignment = Element.ALIGN_CENTER ;
            title.SpacingBefore = 4f;
            title.SpacingAfter = 8f;
            chapter = new Chapter(title, chapterNumber);
           
            chapter.IndentationLeft = 20f;
        }
        public CreateChapter(Paragraph title, int chapterNumber, float spacingAfter)
        {
            title.Font.Size = 13;
            //       title.Alignment = Element.ALIGN_CENTER ;
            title.SpacingBefore = 4f;
            title.SpacingAfter = spacingAfter;
            chapter = new Chapter(title, chapterNumber);

            chapter.IndentationLeft = 20f;
        }
            public void AddImage(iTextSharp.text.Image image)
        {
            image.Alignment = Element.ALIGN_LEFT;
            image.SpacingBefore = 50;
            chapter.Add(image);
        }
        public void AddImageToLineChart(iTextSharp.text.Image image)
        {
            image.Alignment = Element.ALIGN_LEFT;
            image.SpacingBefore = 50;
            chapter.Add(image);
        }
        public void AddTitleCHartToLineChart(Paragraph text, float imageWidth)
        {

            text.Alignment = Element.ALIGN_LEFT;
            text.SpacingBefore = 100;
            text.SetLeading(1.0f, 4.0f);
            text.IndentationLeft = ((540 / imageWidth) * 80) * 2;
            chapter.Add(text);
        }
        public void AddToTableOfContents(Paragraph text)
        {
            chapter.Add(text);
        }
        public void AddText(Paragraph text)
        {

            //   paragraph.Alignment = Element.ALIGN_CENTER;
            text.SetLeading(1.0f, 4.0f);
            chapter.Add(text);
        }
        public void AddTitleCHart(Paragraph text, float imageWidth)
        {

            text.Alignment = Element.ALIGN_LEFT;
            text.SetLeading(1.0f, 4.0f);
            text.IndentationLeft = ((540/imageWidth) * 80)*2;
            chapter.Add(text);
        }
        public void AddTable(PdfPTable table)
        {
            table.SpacingBefore = 10f;
            table.SpacingAfter = 12.5f;
            table.HorizontalAlignment = Element.ALIGN_LEFT;

     //          table.WidthPercentage = 40f;
            Paragraph paragraph = new Paragraph();

            //         paragraph.SetLeading(1.0f, 4.0f);
            //     paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Add(table);
    //        paragraph.SpacingBefore=2f;
            chapter.Add(paragraph);
        }
        public void AddSection(Paragraph sectionTitle, int numberDepth)
        {
             createSection = new CreateSection(chapter, sectionTitle, numberDepth);
        }

        public void AddSubSection(Paragraph sectionTitle, int numberDepth)
        {
            createSubSection = new CreateSubSection(createSection.section, sectionTitle, numberDepth);
        }
      

        public Chapter GetChapter()
        {            
            return chapter;
        }
        public Section GetSection()
        {
            return createSection.section;
        }
        public Section GetSubSection()
        {
            return createSubSection.subSection;
        }


    }
}
