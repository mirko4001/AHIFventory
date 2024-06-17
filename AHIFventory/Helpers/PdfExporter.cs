using System.Collections.Generic;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Serilog;

namespace AHIFventory
{
    public class PdfExporter
    {
        public static void ExportToPdf<T>(string title, string filename, List<string> headers, List<T> data)
        {
            Log.Information("Exporitng to pdf");

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pdfPath = Path.Combine(documentsPath, filename);

            using (var fileStream = new FileStream(pdfPath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fileStream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                Document document = new Document(pdfDocument);

                Log.Debug("Add title");
                var titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                document.Add(new Paragraph(title).SetFont(titleFont).SetFontSize(18));
                document.Add(new Paragraph("\n"));

                Log.Debug("Create table with headers");
                float[] columnWidths = Enumerable.Repeat(1f, headers.Count).ToArray();
                Table table = new Table(columnWidths).UseAllAvailableWidth();

                Log.Debug("Add table headers");
                foreach (var header in headers)
                {
                    AddCellToHeader(table, header);
                }

                Log.Debug("Add data rows");
                foreach (var item in data)
                {
                    var properties = item.GetType().GetProperties();
                    foreach (var prop in properties)
                    {
                        if (headers.Contains(prop.Name) || (prop.Name == "ProductName" && headers.Contains("Product")) || (prop.Name == "Price" && headers.Contains("Price per unit")))
                        {
                            AddCellToBody(table, prop.GetValue(item)?.ToString());
                        }
                    }
                }

                document.Add(table);

                document.Close();
            }
        }

        public static void ExportProductsToPdf()
        {
            Log.Information("Exporting products to pdf");

            string title = "Products";
            string filename = "products.pdf";
            List<string> headers = new List<string> { "Name", "Price per unit", "Stock" };
            List<Product> data = ProductViewModel.Products.ToList();

            ExportToPdf(title, filename, headers, data);
        }

        public static void ExportOrdersToPdf()
        {
            Log.Information("Exporting orders to pdf");

            string title = "Orders";
            string filename = "orders.pdf";
            List<string> headers = new List<string> { "Supplier", "Product", "Quantity", "Price" };
            List<Order> data = OrderViewModel.Orders.ToList();

            ExportToPdf(title, filename, headers, data);
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