using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

public static class PoultryPdfPrinter
{
    public static void PrintInvoiceDirectly(string customerName, string contact, DateTime saleDate, int billId, decimal weight, decimal totalAmount, decimal paid, decimal due)
    {
        if (PrinterSettings.InstalledPrinters.Count == 0)
        {
            MessageBox.Show("No printers are installed on this system.", "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        PrinterSettings settings = new PrinterSettings();
        if (string.IsNullOrEmpty(settings.PrinterName) || !settings.IsValid)
        {
            MessageBox.Show("No valid default printer found.", "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (sender, e) =>
            {
                DrawInvoice(e, customerName, contact, saleDate, billId, weight, totalAmount, paid, due);
            };

            PrintDialog dialog = new PrintDialog
            {
                Document = printDoc,
                AllowSomePages = true,
                UseEXDialog = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
                MessageBox.Show("Invoice sent to printer successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Printing failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void DrawInvoice(PrintPageEventArgs e, string customerName, string contact, DateTime saleDate, int billId, decimal weight, decimal totalAmount, decimal paid, decimal due)
    {
        Font titleFont = new Font("Arial", 18, FontStyle.Bold);
        Font subFont = new Font("Arial", 12, FontStyle.Bold);
        Font normalFont = new Font("Arial", 11);
        Brush brush = Brushes.Black;
        float topMargin = 80f;
        float leftMargin = 80f;
        float rightMargin = 80f;

        float pageWidth = e.PageBounds.Width - leftMargin - rightMargin;
        float x = leftMargin;
        float y = topMargin;

        e.Graphics.DrawString("Yasir Poultry Farm - Invoice", titleFont, brush, new RectangleF(x, y, pageWidth, 30), new StringFormat { Alignment = StringAlignment.Center });
        y += 50;

        e.Graphics.DrawString($"Customer Name: {customerName}", normalFont, brush, x, y); y += 25;
        e.Graphics.DrawString($"Contact: {contact}", normalFont, brush, x, y); y += 25;
        e.Graphics.DrawString($"Date: {saleDate:dd MMM yyyy HH:mm}", normalFont, brush, x, y); y += 25;
        e.Graphics.DrawString($"Bill ID: {billId}", normalFont, brush, x, y); y += 40;

        // Invoice Table
        float labelWidth = 200;
        float valueX = x + labelWidth + 10;

        DrawLine(e.Graphics, x, y, x + pageWidth, y);
        y += 10;

        DrawLabelValue(e.Graphics, "Weight (kg):", weight.ToString("F2"), x, valueX, y, subFont, normalFont); y += 25;
        DrawLabelValue(e.Graphics, "Total Amount:", "Rs " + totalAmount.ToString("N2"), x, valueX, y, subFont, normalFont); y += 25;
        DrawLabelValue(e.Graphics, "Paid:", "Rs " + paid.ToString("N2"), x, valueX, y, subFont, normalFont); y += 25;
        DrawLabelValue(e.Graphics, "Due:", "Rs " + due.ToString("N2"), x, valueX, y, subFont, normalFont); y += 40;

        DrawLine(e.Graphics, x, y, x + pageWidth, y);
        y += 30;

        e.Graphics.DrawString("Thank you for your business!", normalFont, brush, x, y);
        y += 25;
        e.Graphics.DrawString("Generated on: " + DateTime.Now.ToString("f"), new Font("Arial", 9, FontStyle.Italic), Brushes.DarkGray, x, y);
    }

    private static void DrawLabelValue(Graphics g, string label, string value, float xLabel, float xValue, float y, Font labelFont, Font valueFont)
    {
        g.DrawString(label, labelFont, Brushes.Black, xLabel, y);
        g.DrawString(value, valueFont, Brushes.Black, xValue, y);
    }

    private static void DrawLine(Graphics g, float x1, float y, float x2, float y2)
    {
        using (Pen pen = new Pen(Color.Gray, 0.5f))
        {
            g.DrawLine(pen, x1, y, x2, y2);
        }
    }
}
