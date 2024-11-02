using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Printing;


namespace CafeShopManagementSystem
{
    public partial class DailySalesForm : UserControl
    {
        private DateTimePicker DatePicker;
        private PrintDocument printDocument1;
        private PrintPreviewDialog printPreviewDialog1;
        private int rowIndex;
        private float totalSales;


        public DailySalesForm()
        {
            InitializeComponent();
            displayDailySalesData();
            InitializeCustomComponents();
        }


        private void InitializeCustomComponents()
        {
            // Initialize DatePicker
            DatePicker = new DateTimePicker();
            DatePicker.Location = new Point(10, 10); // Adjust the location as needed
            DatePicker.ValueChanged += new EventHandler(datePicker_ValueChanged);
            this.Controls.Add(DatePicker);

            // Initialize PrintDocument and PrintPreviewDialog
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);

            printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = printDocument1;
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayDailySalesData();
        }

        private void displayDailySalesData()
        {
            DateTime selectedDate = datePicker.Value;
            DailySalesData dsData = new DailySalesData();
            List<DailySalesData> listData = dsData.GetDailySalesDataByDate(selectedDate);
            salesGrid.DataSource = listData;
        }

        private void CalculateTotalSales(List<DailySalesData> listData)
        {
            //totalSales = listData.Sum(data => data.TotalPrice); // Assuming TotalPrice is a property in DailySalesData
        }


        private void datagridview1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            displayDailySalesData();

        }

        private void DailySalesForm_Load(object sender, EventArgs e)
        {

        }

        private void DailySalesPrintBtn_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            rowIndex = 0;
            totalSales = 0; // Reset total sales when printing begins

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 0;
            int count = 0;
            int tableMargin = 20;
            int colMargin = 100; // Margin between columns

            Font font = new Font("Arial", 9);
            Font bold = new Font("Arial", 7, FontStyle.Bold);
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font labelFont = new Font("Arial", 12, FontStyle.Bold);

            float margin = e.MarginBounds.Top;

            StringFormat alignCenter = new StringFormat();
            alignCenter.Alignment = StringAlignment.Center;
            alignCenter.LineAlignment = StringAlignment.Center;

            string headerText = "Daily Sales Report\n\n";
            y = margin + count * headerFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(headerText, headerFont, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width / 2), y, alignCenter);

            count++;
            y += tableMargin;

            // Calculate column widths dynamically
            int[] colWidths = new int[salesGrid.Columns.Count];
            int totalWidth = salesGrid.Columns.Cast<DataGridViewColumn>().Sum(col => col.Width);
            for (int i = 0; i < salesGrid.Columns.Count; i++)
            {
                colWidths[i] = (int)((float)salesGrid.Columns[i].Width / totalWidth * (e.MarginBounds.Width - (colMargin * (salesGrid.Columns.Count - 1))));
            }

            // Print column headers
            float currentX = e.MarginBounds.Left;
            for (int i = 0; i < salesGrid.Columns.Count; i++)
            {
                e.Graphics.DrawString(salesGrid.Columns[i].HeaderText, bold, Brushes.Black, currentX, y, alignCenter);
                currentX += colWidths[i] + colMargin;
            }
            count++;
            y += tableMargin;

            // Print rows
            while (rowIndex < salesGrid.Rows.Count)
            {
                DataGridViewRow row = salesGrid.Rows[rowIndex];
                currentX = e.MarginBounds.Left;

                for (int i = 0; i < salesGrid.Columns.Count; i++)
                {
                    object cellValue = row.Cells[i].Value;
                    string cell = (cellValue != null) ? cellValue.ToString() : string.Empty;

                    e.Graphics.DrawString(cell, font, Brushes.Black, currentX, y, alignCenter);
                    currentX += colWidths[i] + colMargin;

                    
                }
                count++;
                rowIndex++;
                y += font.GetHeight(e.Graphics) + tableMargin;

                if (y + font.GetHeight(e.Graphics) > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            // Calculate total sales
            float totalSales = 0;
            foreach (DataGridViewRow row in salesGrid.Rows)
            {
                float Disc_Price;
                if (float.TryParse(row.Cells["Disc_Price"].Value.ToString(), out Disc_Price))
                {
                    totalSales += Disc_Price;
                }
            }


            // Calculate total Cost
            float totalCost = 0;
            foreach (DataGridViewRow row in salesGrid.Rows)
            {
                float TotalCost;
                if (float.TryParse(row.Cells["TotalCost"].Value.ToString(), out TotalCost))
                {
                    totalCost += TotalCost;
                }
            }


            float Profit_or_Loss = totalSales - totalCost;

            



            // Print footer
            float labelX = e.MarginBounds.Right - e.Graphics.MeasureString("------------------------------------------------", labelFont).Width;

            // Calculate the positions
            float yTotalSales = e.MarginBounds.Bottom - 3 * labelFont.GetHeight(e.Graphics);
            float yTotalCost = e.MarginBounds.Bottom - 2 * labelFont.GetHeight(e.Graphics);
            float yProfit = e.MarginBounds.Bottom - labelFont.GetHeight(e.Graphics);



            // Draw the strings at the calculated positions
            e.Graphics.DrawString("Total Sales : Rs." + totalSales.ToString(), labelFont, Brushes.Black, e.MarginBounds.Left, yTotalSales);
            e.Graphics.DrawString("Total Cost : Rs." + totalCost.ToString(), labelFont, Brushes.Black, e.MarginBounds.Left, yTotalCost);

            // Determine the text to display based on profit or loss
            string resultText = Profit_or_Loss >= 0
                ? "Profit : Rs. " + Profit_or_Loss.ToString("0.00")
                : "Loss : Rs. " + (-Profit_or_Loss).ToString("0.00");

            // Draw the result text at the calculated position
            e.Graphics.DrawString(resultText, labelFont, Brushes.Black, e.MarginBounds.Left, yProfit);

            y += labelFont.GetHeight(e.Graphics);
            DateTime selectedDate = datePicker.Value;
            e.Graphics.DrawString("Date: " + selectedDate.ToString("dd/MM/yyyy"), labelFont, Brushes.Black, e.MarginBounds.Left, y);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayDailySalesData();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }


}
