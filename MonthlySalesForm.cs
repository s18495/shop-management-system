﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class MonthlySalesForm : UserControl
    {

        private DateTimePicker MonthPicker;
        private PrintDocument printDocument1;
        private PrintPreviewDialog printPreviewDialog1;
        private int rowIndex;
        private TextBox txtSearchBar;

        private float totalSales;
        //private ComboBox categoryFilterComboBox; // Reference to existing ComboBox for filtering by category


        public MonthlySalesForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            displayMonthlySalesData();
            //txtSearchBar.TextChanged += txtSearchBar_TextChanged;

        }

        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Selected Month and Year: " + datePicker.Value.ToString("MMMM yyyy"));
            displayMonthlySalesData();
        }


        private void InitializeCustomComponents()
        {
            // Initialize MonthPicker
            MonthPicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MMMM yyyy",
                ShowUpDown = true,
                Location = new Point(10, 10) // Adjust the location as needed
            };
            MonthPicker.Value = DateTime.Today;
            MonthPicker.ValueChanged += new EventHandler(datePicker_ValueChanged);
            this.Controls.Add(MonthPicker);

            // Initialize PrintDocument and PrintPreviewDialog
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);

            printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = printDocument1;

            // Initialize ComboBox for category filtering

            // Assuming you have a ComboBox named 'categoryFilterComboBox' in your form's designer
            //categoryFilterComboBox.SelectedIndexChanged += new EventHandler(categoryFilterComboBox_SelectedIndexChanged);

            

            txtSearchBar.TextChanged += txtSearchBar_TextChanged;




        }


        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchBar.Text.Trim().ToLower();

            DateTime selectedMonth = datePicker.Value;
            MonthlySalesData msData = new MonthlySalesData();
            List<MonthlySalesData> listData = msData.GetMonthlySalesDataByDate(selectedMonth);

            // Filter listData based on searchText
            listData = listData.Where(row =>
                row.Cat_Name.ToLower().Contains(searchText)
            // Add additional criteria as needed
            ).ToList();

            // Rebind the filtered data to salesGrid
            salesGrid.DataSource = null; // Clear the current data binding
            salesGrid.DataSource = listData; // Bind the filtered data

            // Optionally, refresh the DataGridView to reflect changes
            salesGrid.Refresh();
        }









        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayMonthlySalesData();
        }


        /*
         * private void displayMonthlySalesData()
        {
            DateTime selectedMonth = MonthPicker.Value;
            MonthlySalesData msData = new MonthlySalesData();
            List<MonthlySalesData> listData = msData.GetMonthlySalesDataByDate(selectedMonth);

            // Optionally, clear existing columns and reset DataGridView
            salesGrid.Columns.Clear();
            
            
            // Bind data to DataGridView
            salesGrid.AutoGenerateColumns = false; // Ensure columns are not autogenerated
            salesGrid.DataSource = listData;

            // Optionally, manually add columns if not already defined in designer
            if (salesGrid.Columns.Count == 0)
            {
                salesGrid.Columns.Add("ID", "ID");
                salesGrid.Columns.Add("CID", "Customer ID");
                salesGrid.Columns.Add("ProdID", "Product ID");
                salesGrid.Columns.Add("ProdName", "Product Name");
                salesGrid.Columns.Add("Qty", "Quantity");
                salesGrid.Columns.Add("Price", "Price");
                salesGrid.Columns.Add("CategoryName", "Category Name");

            }

            // Optional: Format price column as currency
            salesGrid.Columns["Price"].DefaultCellStyle.Format = "C2";

           
            // Optionally, refresh the DataGridView to reflect changes
            salesGrid.Refresh();

            // Debugging: Log number of rows retrieved
            Console.WriteLine($"Retrieved {listData.Count} rows for {selectedMonth:yyyy-MM}");

        }
        */







        
         private void displayMonthlySalesData()
       {
           DateTime selectedMonth = datePicker.Value;
           MonthlySalesData msData = new MonthlySalesData();
           List<MonthlySalesData> listData = msData.GetMonthlySalesDataByDate(selectedMonth);
           salesGrid.DataSource = listData;

           // Populate the category filter ComboBox with unique category names
           /*
           categoryFilterComboBox.Items.Clear();
           var uniqueCategories = listData.Select(x => x.CategoryName).Distinct().ToList();
           uniqueCategories.Insert(0, "All"); // Add "All" option to view all categories
           categoryFilterComboBox.Items.AddRange(uniqueCategories.ToArray());
           categoryFilterComboBox.SelectedIndex = 0; // Default to "All"
           */


       }
           





        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayMonthlySalesData();
        }

        private void cashierOrderForm_receiptBtn_Click(object sender, EventArgs e)
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

            Font font = new Font("Arial", 10);
            Font bold = new Font("Arial", 10, FontStyle.Bold);
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font labelFont = new Font("Arial", 12, FontStyle.Bold);

            float margin = e.MarginBounds.Top;

            StringFormat alignCenter = new StringFormat();
            alignCenter.Alignment = StringAlignment.Center;
            alignCenter.LineAlignment = StringAlignment.Center;

            string headerText = "Monthly Sales Report\n\n";
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
            e.Graphics.DrawString("Date: " + selectedDate.ToString("MM/yyyy"), labelFont, Brushes.Black, e.MarginBounds.Left, y);
        }

        private void salesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }



}
