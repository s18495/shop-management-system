using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace CafeShopManagementSystem
{
    public partial class GRN : UserControl
    {

        //private bool eventsSubscribed = false;

        private List<string> existingProductNames;

        public GRN()
        {
            InitializeComponent();
            txtTotalQuantity.Enabled = false;
            txtTotalPrice.Enabled = false;
            LoadExistingProductNames();
            btnSave.Enabled = false; // Disable Save button initially
            ValidateForm(); // Call validation on form load

            // Hook up event handlers for DataGridView events
            dgvItemList.RowsAdded += (sender, e) => ValidateForm();
            dgvItemList.RowsRemoved += (sender, e) => ValidateForm();



        }


        // Method to validate form fields and enable/disable Save button
        private void ValidateForm()
        {
            bool isValid = true;

            // Check Supplier Name
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
                isValid = false;

            // Check Supplier Contact
            if (string.IsNullOrWhiteSpace(txtSpplierContact.Text))
                isValid = false;

            // Check GRN Number
            if (string.IsNullOrWhiteSpace(txtGRNNumber.Text))
                isValid = false;

            // Check if DataGridView has at least one row
            if (dgvItemList.Rows.Count == 0)
                isValid = false;

            // Check Shop ID
            if (string.IsNullOrWhiteSpace(txtShopID.Text) || !int.TryParse(txtShopID.Text, out _))
                isValid = false;

            // Enable or disable Save button based on validation
            btnSave.Enabled = isValid;
        }

        // Event handlers for text changed events to validate form on input change
        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void txtSpplierContact_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void txtGRNNumber_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void txtShopID_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }





        private void LoadExistingProductNames()
        {
            existingProductNames = new List<string>();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT prod_name FROM Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    existingProductNames.Add(reader["prod_name"].ToString());
                }
            }

            comboBoxProductNames.DataSource = existingProductNames;
        }



        private void ClearForm()
        {
            // Clear text boxes
            txtSupplierName.Text = "";
            txtSpplierContact.Text = "";
            txtGRNNumber.Text = "";
            txtItemName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
            txtTotalQuantity.Text = "0";
            txtTotalPrice.Text = "0.00";
            txtShopID.Text = "";


            // Clear ComboBox selection
            comboBoxProductNames.SelectedIndex = -1;

            // Clear DataGridView
            dgvItemList.Rows.Clear();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CalculateTotals();
        }


        private void DgvItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateTotals();
        }


        private void DgvItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateTotals();
        }


        private void CalculateTotals()
        {
            int totalQuantity = 0;
            decimal totalPrice = 0;

            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                if (row.Cells["Quantity"].Value != null && row.Cells["Price"].Value != null)
                {
                    string quantityText = row.Cells["Quantity"].Value.ToString();
                    string priceText = row.Cells["Price"].Value.ToString();

                    // MessageBox.Show($"Quantity: {quantityText}, Price: {priceText}");

                    int quantity;
                    decimal price;

                    if (int.TryParse(quantityText, out quantity) && decimal.TryParse(priceText, out price))
                    {
                        totalQuantity += quantity;
                        totalPrice += price * quantity;
                    }
                    else
                    {
                        MessageBox.Show($"Invalid value found. Quantity: {quantityText}, Price: {priceText}");
                    }
                }
            }

            txtTotalQuantity.Text = totalQuantity.ToString();
            txtTotalPrice.Text = totalPrice.ToString("0.00");
        }











        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Validate and gather data from UI controls
            string supplierName = txtSupplierName.Text;
            string supplierContact = txtSpplierContact.Text;
            string grnNumber = txtGRNNumber.Text;
            DateTime date = dateTimePicker.Value;
            int totalQuantity = int.Parse(txtTotalQuantity.Text);
            int shop_id = int.Parse(txtShopID.Text);
            decimal totalPrice = decimal.Parse(txtTotalPrice.Text);

            // Insert into database
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Example: Insert into Suppliers and retrieve SupplierID
                int supplierID = InsertOrUpdateSupplier(connection, supplierName, supplierContact);

                // Example: Insert into GRNs and retrieve GRNID
                //int grnID = InsertIntoGRNs(connection, supplierID, grnNumber, date, totalQuantity, totalPrice);

                SaveGRNToDatabase(supplierName, supplierContact, grnNumber, date, shop_id,  dgvItemList.Rows);

                /*
                // Insert into GRNItems
                foreach (DataGridViewRow row in dgvItemList.Rows)
                {
                    string productName = row.Cells["ProductName"].Value.ToString(); // Assuming "ProductName" is the column name in dgvItemList
                    string itemName = row.Cells["ItemName"].Value.ToString();
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                    // Resolve product name to product ID or insert if not exists
                    int product_id = ResolveProductName(connection, productName);

                    // Example: Insert into GRNItems
                    InsertIntoGRNItems(connection, grnID, itemName, quantity, price, product_id);
                }
                */
            }

            //MessageBox.Show("GRN saved successfully!");

            // Clear form inputs after saving
            ClearForm();
        }


        private void SaveGRNToDatabase(string supplierName, string supplierContact, string grnNumber, DateTime date, int shopID, DataGridViewRowCollection rows)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if shop_id exists
                if (!ShopIdExists(connection, shopID))
                {
                    MessageBox.Show("The specified Shop ID does not exist.");
                    return;
                }

                // Insert into Suppliers and retrieve SupplierID
                int supplierID = InsertOrUpdateSupplier(connection, supplierName, supplierContact);

                // Insert into GRNs and retrieve GRNID
                int grnID = InsertIntoGRNs(connection, supplierID, grnNumber, date, CalculateTotalQuantity(rows), CalculateTotalPrice(rows));

                // Update or insert product stock and cost in Products table
                foreach (DataGridViewRow row in rows)
                {
                    if (row.Cells["ProductName"].Value != null)
                    {
                        string productName = row.Cells["ProductName"].Value.ToString();
                        string itemName = row.Cells["ItemName"].Value.ToString();
                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                        decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                        // Resolve product name to product ID
                        int productID = ResolveProductName(connection, productName);

                        // Update or insert product stock and cost
                        UpdateProduct(connection, productID, quantity, price, shopID, productName);

                        // Insert into GRNItems
                        InsertIntoGRNItems(connection, grnID, itemName, quantity, price, productID);
                    }
                }

                MessageBox.Show("GRN saved successfully!");
            }

            // Clear form inputs after saving
            ClearForm();
        }





        private bool ShopIdExists(SqlConnection connection, int shop_id)
        {
            string shopCheckQuery = "SELECT COUNT(1) FROM Shops WHERE shop_id = @shop_id";
            SqlCommand shopCheckCmd = new SqlCommand(shopCheckQuery, connection);
            shopCheckCmd.Parameters.AddWithValue("@shop_id", shop_id);

            int shopCount = Convert.ToInt32(shopCheckCmd.ExecuteScalar());
            return shopCount > 0;
        }


        private int CalculateTotalQuantity(DataGridViewRowCollection rows)
        {
            int totalQuantity = 0;
            foreach (DataGridViewRow row in rows)
            {
                if (row.Cells["Quantity"].Value != null)
                {
                    totalQuantity += Convert.ToInt32(row.Cells["Quantity"].Value);
                }
            }
            return totalQuantity;
        }

        private decimal CalculateTotalPrice(DataGridViewRowCollection rows)
        {
            decimal totalPrice = 0;
            foreach (DataGridViewRow row in rows)
            {
                if (row.Cells["Quantity"].Value != null && row.Cells["Price"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                    totalPrice += quantity * price;
                }
            }
            return totalPrice;
        }

        private void UpdateProduct(SqlConnection connection, int productID, int quantity, decimal price, int shopID, string prod_name)
        {
            // Check if the product exists in the specified shop
            string checkProductQuery = @"
IF EXISTS (SELECT 1 FROM Products WHERE product_id = @ProductID AND shop_id = @ShopID)
BEGIN
    -- Update the existing product
    UPDATE Products
    SET prod_stock = ISNULL(prod_stock, 0) + @Quantity,
        cost = @Price,
        date_insert = GETDATE()
    WHERE product_id = @ProductID AND shop_id = @ShopID;
END
ELSE
BEGIN
    -- Insert a new product
    INSERT INTO Products (prod_name, prod_stock, cost, shop_id, date_insert)
    VALUES (@prod_name, @Quantity, @Price, @ShopID, GETDATE());
END";

            using (SqlCommand cmd = new SqlCommand(checkProductQuery, connection))
            {
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@ShopID", shopID);
                cmd.Parameters.AddWithValue("@prod_name", prod_name);

                cmd.ExecuteNonQuery();
            }
        }















        private int InsertOrUpdateSupplier(SqlConnection connection, string supplierName, string supplierContact)
        {
            // Example logic to insert or update supplier and return SupplierID
            
            string supplierQuery = @"
                IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE SupplierName = @SupplierName)
                BEGIN
                    INSERT INTO Suppliers (SupplierName, SupplierContact)
                    VALUES (@SupplierName, @SupplierContact);
                END
                ELSE
                BEGIN
                    UPDATE Suppliers
                    SET SupplierContact = @SupplierContact
                    WHERE SupplierName = @SupplierName;
                END
                SELECT SupplierID FROM Suppliers WHERE SupplierName = @SupplierName;";
            
            /*
            string supplierQuery = @"
            INSERT INTO Suppliers (SupplierName, SupplierContact)
            VALUES (@SupplierName, @SupplierContact);
            SELECT SCOPE_IDENTITY();";

            */


            SqlCommand supplierCmd = new SqlCommand(supplierQuery, connection);
            supplierCmd.Parameters.AddWithValue("@SupplierName", supplierName);
            supplierCmd.Parameters.AddWithValue("@SupplierContact", supplierContact);

            int supplierID = Convert.ToInt32(supplierCmd.ExecuteScalar());
            return supplierID;
        }









        private int InsertIntoGRNs(SqlConnection connection, int supplierID, string grnNumber, DateTime date, int totalQuantity, decimal totalPrice)
        {
            // Example logic to insert into GRNs and return GRNID
            string grnQuery = "INSERT INTO GRNs (SupplierID, GRNNumber, GRNDate, TotalQuantity, TotalPrice) " +
                              "VALUES (@SupplierID, @GRNNumber, @GRNDate, @TotalQuantity, @TotalPrice); " +
                              "SELECT SCOPE_IDENTITY();";
            SqlCommand grnCmd = new SqlCommand(grnQuery, connection);
            grnCmd.Parameters.AddWithValue("@SupplierID", supplierID);
            grnCmd.Parameters.AddWithValue("@GRNNumber", grnNumber);
            grnCmd.Parameters.AddWithValue("@GRNDate", date);
            //grnCmd.Parameters.AddWithValue("@ShopID", shop_id);

            grnCmd.Parameters.AddWithValue("@TotalQuantity", totalQuantity);
            grnCmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
            int grnID = Convert.ToInt32(grnCmd.ExecuteScalar());
            return grnID;
        }



        private int ResolveProductName(SqlConnection connection, string productName)
        {
            // Check if product name exists in Products table, insert if not
            string checkQuery = @"
        IF NOT EXISTS (SELECT 1 FROM products WHERE prod_name = @ProductName)
        BEGIN
            INSERT INTO products (prod_name) VALUES (@ProductName);
        END
        SELECT product_id FROM products WHERE prod_name = @ProductName;";

            SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@ProductName", productName);

            object result = checkCmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result); // Convert to int safely
            }
            else
            {
                throw new InvalidOperationException("Failed to resolve product ID.");
            }
        }







        private void InsertIntoGRNItems(SqlConnection connection, int grnID, string itemName, int quantity, decimal price, int product_id)
        {
            // Example logic to insert into GRNItems
            string itemQuery = "INSERT INTO GRNItems (GRNID, product_id, ItemName, Quantity, Price) " +
                               "VALUES (@GRNID, @product_id, @ItemName, @Quantity, @Price);";
            SqlCommand itemCmd = new SqlCommand(itemQuery, connection);
            itemCmd.Parameters.AddWithValue("@GRNID", grnID);
            itemCmd.Parameters.AddWithValue("@product_id", product_id);
            itemCmd.Parameters.AddWithValue("@ItemName", itemName);
            itemCmd.Parameters.AddWithValue("@Quantity", quantity);
            itemCmd.Parameters.AddWithValue("@Price", price);
            itemCmd.ExecuteNonQuery();
        }






        private void btnCancel_Click_1(object sender, EventArgs e)
        {

        }

       

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            // Get values from text boxes
            string productName = comboBoxProductNames.Text.Trim(); // Get selected or entered product name
            string itemName = txtItemName.Text;
            int quantity = int.Parse(txtQuantity.Text);
            decimal price = decimal.Parse(txtPrice.Text);
            //int shop_id = int.Parse(txtShopID.Text);


            // Validate input
            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Please select or enter a product name.");
                return;
            }
            if (string.IsNullOrWhiteSpace(itemName))
            {
                MessageBox.Show("Item name cannot be empty.");
                return;
            }
            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be a positive integer.");
                return;
            }

            

            if (price <= 0)
            {
                MessageBox.Show("Price must be a positive decimal number.");
                return;
            }

            // Add values to DataGridView
            dgvItemList.Rows.Add(itemName, quantity, price, productName);

            // Clear input fields
            comboBoxProductNames.SelectedIndex = -1; // Reset selection in ComboBox
            txtItemName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
            //txtShopID.Text = "";


            // Show success message
            MessageBox.Show("Item added successfully!");
        }



        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is not a header cell
            if (e.RowIndex >= 0)
            {
                // Prompt user with confirmation message box
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Delete Row", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // If user clicks 'Yes', delete the row
                if (result == DialogResult.Yes)
                {
                    dgvItemList.Rows.RemoveAt(e.RowIndex);
                    CalculateTotals(); // Recalculate totals after deletion
                }
            }
        }

        private void dgvItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadExistingProductNames();
        }
    }
}
