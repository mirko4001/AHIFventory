using System.Collections.Generic;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace AHIFventory
{
    public class PdfExporter
    {
        public static void ExportProductsToPdf()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pdfPath = Path.Combine(documentsPath, "products.pdf");

            using (var fileStream = new FileStream(pdfPath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fileStream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                Document document = new Document(pdfDocument);

                // Add title
                var titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                document.Add(new Paragraph("Prodcuts").SetFont(titleFont).SetFontSize(18));
                document.Add(new Paragraph("\n"));

                // Create table with three columns
                Table table = new Table(new float[] { 1, 1, 1 }).UseAllAvailableWidth();

                // Add table headers
                AddCellToHeader(table, "Name");
                AddCellToHeader(table, "Price per Unit");
                AddCellToHeader(table, "Stock");

                // Add data rows
                foreach (var product in ProductViewModel.Products)
                {
                    AddCellToBody(table, product.Name);
                    AddCellToBody(table, product.Price.ToString());
                    AddCellToBody(table, product.Stock.ToString());
                }

                document.Add(table);

                document.Close();
            }
        }

        public static void ExportOrdersToPdf()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pdfPath = Path.Combine(documentsPath, "orders.pdf");

            using (var fileStream = new FileStream(pdfPath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fileStream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                Document document = new Document(pdfDocument);

                // Add title
                var titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                document.Add(new Paragraph("Orders").SetFont(titleFont).SetFontSize(18));
                document.Add(new Paragraph("\n"));

                // Create table with three columns
                Table table = new Table(new float[] { 1, 1, 1, 1 }).UseAllAvailableWidth();

                // Add table headers
                AddCellToHeader(table, "Supplier");
                AddCellToHeader(table, "Product");
                AddCellToHeader(table, "Quantity");
                AddCellToHeader(table, "Price");

                // Add data rows
                foreach (var order in OrderViewModel.Orders)
                {
                    AddCellToBody(table, order.Supplier);
                    AddCellToBody(table, order.ProductName.ToString());
                    AddCellToBody(table, order.Quantity.ToString());
                    AddCellToBody(table, order.Price.ToString());
                }

                document.Add(table);

                document.Close();
            }
        }

        private static void AddCellToHeader(Table table, string headerText)
        {
            var cell = new Cell().Add(new Paragraph(headerText));
            cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY);
            cell.SetTextAlignment(TextAlignment.CENTER);
            cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            table.AddHeaderCell(cell);
        }

        private static void AddCellToBody(iText.Layout.Element.Table table, string text)
        {
            var cell = new Cell().Add(new Paragraph(text));
            cell.SetTextAlignment(TextAlignment.CENTER);
            cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            table.AddCell(cell);
        }
    }
}