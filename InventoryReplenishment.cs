using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class InventoryReplenishment : UserControl
    {

        private PrintDocument printDocument1;
        private PrintPreviewDialog printPreviewDialog1;
        private int rowIndex;

        public InventoryReplenishment()
        {
            InitializeComponent();
            LoadInventoryData();
            //LoadReplenishmentSettings();
            LoadSupplierInformation();
            InitializePrintComponents(); // Ensure this is called


        }


        private void InitializePrintComponents()
        {
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = printDocument1;
        }


        private void dataGridViewInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadInventoryData()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        prod_id,
                        prod_name,
                        prod_stock,
                        shop_id
                        
                    FROM 
                        products";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridViewInventory.DataSource = dataTable;
            }
        }



        private void LoadSupplierInformation()
        {
            // Load supplier information from the database
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Suppliers"; // Replace with your actual query
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridViewSuppliers.DataSource = dataTable;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadInventoryData();
            LoadSupplierInformation();
        }







        private int selectedSupplierID; // Store selected SupplierID temporarily

        private void dataGridViewSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewSuppliers.Rows[e.RowIndex];
                txtSupplierName.Text = row.Cells["SupplierName"].Value.ToString();
                txtSupplierContact.Text = row.Cells["SupplierContact"].Value.ToString();

                // Store the SupplierID
                if (int.TryParse(row.Cells["SupplierID"].Value.ToString(), out int supplierID))
                {
                    selectedSupplierID = supplierID;
                }
            }
        }



        private void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            if (selectedSupplierID != 0) // Ensure a valid SupplierID is selected
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30";
                string query = "UPDATE Suppliers SET SupplierName = @SupplierName, SupplierContact = @SupplierContact WHERE SupplierID = @SupplierID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                    command.Parameters.AddWithValue("@SupplierContact", txtSupplierContact.Text);
                    command.Parameters.AddWithValue("@SupplierID", selectedSupplierID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                // Refresh the data grid view after updating
                LoadSupplierInformation();
                txtSupplierName.Text = "";
                txtSupplierContact.Text = "";
            }
            else
            {
                MessageBox.Show("Please select a supplier from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateReports_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();

        }


        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 0;
            int count = 0;
            int tableMargin = 20;
            int colMargin = 100; // Margin between columns

            Font font = new Font("Arial", 12);
            Font bold = new Font("Arial", 12, FontStyle.Bold);
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font labelFont = new Font("Arial", 14, FontStyle.Bold);

            float margin = e.MarginBounds.Top;

            StringFormat alignCenter = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            string headerText = "Inventory Report\n\n";
            y = margin + count * headerFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(headerText, headerFont, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width / 2), y, alignCenter);

            count++;
            y += tableMargin;

            // Calculate column widths dynamically
            int[] colWidths = new int[dataGridViewInventory.Columns.Count];
            int totalWidth = dataGridViewInventory.Columns.Cast<DataGridViewColumn>().Sum(col => col.Width);
            for (int i = 0; i < dataGridViewInventory.Columns.Count; i++)
            {
                colWidths[i] = (int)((float)dataGridViewInventory.Columns[i].Width / totalWidth * (e.MarginBounds.Width - (colMargin * (dataGridViewInventory.Columns.Count - 1))));
            }

            // Print column headers
            float currentX = e.MarginBounds.Left;
            for (int i = 0; i < dataGridViewInventory.Columns.Count; i++)
            {
                e.Graphics.DrawString(dataGridViewInventory.Columns[i].HeaderText, bold, Brushes.Black, currentX, y, alignCenter);
                currentX += colWidths[i] + colMargin;
            }
            count++;
            y += tableMargin;

            // Print rows
            while (rowIndex < dataGridViewInventory.Rows.Count)
            {
                DataGridViewRow row = dataGridViewInventory.Rows[rowIndex];
                currentX = e.MarginBounds.Left;

                for (int i = 0; i < dataGridViewInventory.Columns.Count; i++)
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

            // Print footer
            float labelX = e.MarginBounds.Right - e.Graphics.MeasureString("------------------------------------------------", labelFont).Width;
            float yFooter = e.MarginBounds.Bottom - labelFont.GetHeight(e.Graphics);
            e.Graphics.DrawString("Report generated on: " + DateTime.Now.ToString("dd/MM/yyyy"), labelFont, Brushes.Black, e.MarginBounds.Left, yFooter);
        }
    }


}

