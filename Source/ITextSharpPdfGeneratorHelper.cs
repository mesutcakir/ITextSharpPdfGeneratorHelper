using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Berga.Argelogic.Database.Test.ITextSharp
{
    public static class ITextSharpPdfGeneratorHelper
    {
        public static Font GetTahoma(int fontSize = 12)
        {
            var fontName = "Tahoma";
            if (!FontFactory.IsRegistered(fontName))
            {
                var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\tahoma.ttf";
                FontFactory.Register(fontPath);
            }
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, fontSize);
        }

        public static string Author = "Mesut ÇAKIR";
        public static string DocumentCreator = "Mesut ÇAKIR";
        public static string DocumentSubject = "Doküman Konusu";
        public static string DocumentTitle = "Doküman Baþlýðý";
        public static string DocumentKeywords = "";

        public static void CreateDocument(Stream stream, Action<Document> action)
        {
            using (var document = new Document(PageSize.A4))
            {
                var writer = PdfWriter.GetInstance(document, stream);
                document.Open();
                document.AddAuthor(Author);
                document.AddCreator(DocumentCreator);
                document.AddKeywords(DocumentKeywords);
                document.AddSubject(DocumentSubject);
                document.AddTitle(DocumentTitle);

                action.Invoke(document);

                document.Close();
            }
        }

        public static void AddText(Document document, string str, PdfGeneratorHeaderLevel fontSize = PdfGeneratorHeaderLevel.Default)
        {
            var parag = new Paragraph(str, GetTahoma((int)fontSize));
            parag.Alignment = Image.ALIGN_JUSTIFIED;
            document.Add(parag);
        }

        public static IElement AddImage(string filePath)
        {
            var image = Image.GetInstance(filePath);
            var imgRaw = new ImgRaw(image);
            imgRaw.Alignment = Image.ALIGN_CENTER;
            imgRaw.ScaleToFit(PageSize.A4.Width - (PageSize.A4.Width * 0.15f), PageSize.A4.Height);
            return imgRaw;
        }
    }
}