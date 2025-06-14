using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;
using System.Windows.Forms;
using QuestPDF.Drawing;

public class CustomerInvoiceGenerator
{
    public static void GenerateInvoice(string customerName, string contact, DateTime saleDate, int billId, decimal weight, decimal totalAmount, decimal paid, decimal due)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        using (SaveFileDialog saveDialog = new SaveFileDialog())
        {
            saveDialog.Title = "Save Invoice As";
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveDialog.FileName = $"Invoice_Bill_{billId}.pdf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveDialog.FileName;

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(12));

                        page.Header()
                            .Text("Yasir Poultry Farm - Customer Invoice")
                            .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                        page.Content().PaddingVertical(10).Column(col =>
                        {
                            // Customer info
                            col.Item().Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text($"Customer: {customerName}");
                                    c.Item().Text($"Contact: {contact}");
                                    c.Item().Text($"Sale Date: {saleDate:dd MMM yyyy}");
                                    c.Item().Text($"Bill ID: {billId}");
                                });
                            });

                            col.Item().Element(x => x.PaddingVertical(10)).LineHorizontal(1).LineColor(Colors.Grey.Medium);

                            // Sale details table
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                AddRow(table, "Weight (kg)", weight.ToString("F2"));
                                AddRow(table, "Total Amount", "Rs " + totalAmount.ToString("N2"));
                                AddRow(table, "Amount Paid", "Rs " + paid.ToString("N2"));
                                AddRow(table, "Remaining Due", "Rs " + due.ToString("N2"));
                            });

                            col.Item().PaddingTop(25).Text("Thank you for your business!")
                                .Italic().FontSize(14).FontColor(Colors.Grey.Darken2);
                        });

                        page.Footer().AlignCenter().Text(x =>
                        {
                            x.Span("Generated on ");
                            x.Span(DateTime.Now.ToString("f")).SemiBold();
                        });
                    });
                })
                .GeneratePdf(filePath);

                MessageBox.Show($"Invoice saved successfully to:\n{filePath}", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    // Helper to style each cell
    private static IContainer CellStyle(IContainer container)
    {
        return container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
    }

    // Helper to add rows to the table
    private static void AddRow(TableDescriptor table, string label, string value)
    {
        table.Cell().Element(CellStyle).Text(label).SemiBold();
        table.Cell().Element(CellStyle).Text(value);
    }
}
