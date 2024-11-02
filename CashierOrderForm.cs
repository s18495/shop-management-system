using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using System.Xml.Linq;


namespace CafeShopManagementSystem
{
    public partial class CashierOrderForm : UserControl

    {



        public static int getCustID;

        //static string conn = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;
        //SqlConnection connect = new SqlConnection(conn);
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        //private TextBox cashierOrderForm_prodName_txt;

        private List<CashierOrderFormProdData> listData; // Initialize the listData
                                                         //private TextBox cashierOrderForm_prodName_txt;

        

        public CashierOrderForm()
        {
            InitializeComponent();

            //cashierOrderForm_menuTable.RowHeaderMouseClick += cashierOrderForm_menuTable_CellClick;
            //LoadPromotionNames();

            displayAvailableProds();
            displayAllOrders();
            LoadCategories();
            //UpdateCategories();
            //InitializeCustomComponents();

            displayTotalPrice();
            // Call the LoadAvailableProducts method when the form is loaded
            displayAvailableProds(); // Optionally, you can load all orders as
                                     // 

            InitializeCustomComponents();

            cashierOrderForm_price.Enabled = false;
            cashierOrderForm_productID_txt.Enabled = false;
            LoadPromotions();
            comboBoxPromotions.SelectedIndexChanged += comboBoxPromotions_SelectedIndexChanged;
            cashierOrderForm_discount.TextChanged += cashierOrderForm_discount_TextChanged;
            cashierOrderForm_discount.Enabled = false;
            //InitializeOrderTable();
            cashierOrderForm_menuTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;






        }








        private void cashierOrderForm_discount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Prompt the user for a password
                string enteredPassword = PromptForPassword();

                // Check if the entered password matches the expected one (e.g., "admin123")
                if (enteredPassword == "admin123") // Replace with your actual password
                {
                    float getDiscount;

                    // Check if the input is a valid float
                    if (float.TryParse(cashierOrderForm_discount.Text, out getDiscount))
                    {
                        // Calculate the price after discount
                        float getPriceAfterDiscount = (totalPrice - (totalPrice * (getDiscount / 100)));

                        if (getPriceAfterDiscount < 0)
                        {
                            MessageBox.Show("Invalid discount value. Discounted price cannot be negative.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            // Clear the text boxes if the discount is invalid
                            cashierOrderForm_discount.Text = "";
                            cashierOrderForm_price_after_discount.Text = "";
                        }
                        else
                        {
                            // Display the price after discount
                            cashierOrderForm_price_after_discount.Text = getPriceAfterDiscount.ToString();
                        }
                    }
                    else
                    {
                        // Handle the case where input is not a valid float
                        cashierOrderForm_price_after_discount.Text = "";
                    }
                }
                else
                {
                    // If the password is incorrect, show an error message
                    MessageBox.Show("Incorrect password. You cannot apply the discount.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cashierOrderForm_discount.Text = ""; // Reset the discount field if password is wrong
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating discount: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cashierOrderForm_discount.Text = "";
                cashierOrderForm_price_after_discount.Text = "";
            }
        }





        private void comboBoxPromotions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPromotions.SelectedItem != null)
            {
                string selectedPromotion = comboBoxPromotions.SelectedValue.ToString();
                LoadDiscountPercentage(selectedPromotion);
            }
        }


        private void LoadDiscountPercentage(string promotionName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    string query = "SELECT DiscountPercentage FROM Promotions WHERE PromotionName = @PromotionName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PromotionName", promotionName);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != null)
                    {
                        float discountPercentage;
                        if (float.TryParse(result.ToString(), out discountPercentage))
                        {
                            cashierOrderForm_discount.Text = discountPercentage.ToString();
                        }
                        else
                        {
                            cashierOrderForm_discount.Text = "";
                        }
                    }
                    else
                    {
                        cashierOrderForm_discount.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving discount percentage: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cashierOrderForm_discount.Text = "";
            }
        }





            private void LoadPromotions()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    string query = "SELECT PromotionName FROM Promotions"; // Adjust the query based on your actual table structure
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxPromotions.DataSource = dt;
                    comboBoxPromotions.DisplayMember = "PromotionName";
                    comboBoxPromotions.ValueMember = "PromotionName";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading promotions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }










        // Method to reload the data
        public void ReloadData()
        {
            LoadAvailableProducts();
        }


        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayAvailableProds();
            displayAllOrders();

            displayTotalPrice();
        }

        private void InitializeCustomComponents()
        {
            cashierOrderForm_prodName_txt.TextChanged += cashierOrderForm_prodName_txt_TextChanged;
            cashierOrderForm_menuTable.CellClick += cashierOrderForm_menuTable_CellClick;
            cashierOrderForm_orderTable.CellClick += cashierOrderForm_orderTable_CellClick;
            //this.comboBoxPromotions.SelectedIndexChanged += new System.EventHandler(this.comboBoxPromotions_SelectedIndexChanged);

        }



        private void cashierOrderForm_prodName_txt_TextChanged(object sender, EventArgs e)
        {
            string searchText = cashierOrderForm_prodName_txt.Text.Trim().ToLower();

            // Filter listData based on searchText
            var filteredData = listData.Where(row =>
                row.ProductName.ToLower().Contains(searchText)
            ).ToList();

            // Rebind the filtered data to the DataGridView
            cashierOrderForm_menuTable.DataSource = null; // Clear the current data binding
            cashierOrderForm_menuTable.DataSource = filteredData; // Bind the filtered data

            // Optionally, refresh the DataGridView to reflect changes
            cashierOrderForm_menuTable.Refresh();
        }




        public void displayAvailableProds()
        {
            CashierOrderFormProdData allProds = new CashierOrderFormProdData();
            listData = allProds.availableProductsData(); // Assign the result to listData

            cashierOrderForm_menuTable.DataSource = listData;

            // Hide the CategoryID column
            if (cashierOrderForm_menuTable.Columns["CategoryID"] != null)
            {
                cashierOrderForm_menuTable.Columns["CategoryID"].Visible = false;
            }

            // Ensure the columns are properly displayed
            cashierOrderForm_menuTable.Columns["ProductID"].HeaderText = "Product ID";
            cashierOrderForm_menuTable.Columns["ProductName"].HeaderText = "Product Name";
            cashierOrderForm_menuTable.Columns["Stock"].HeaderText = "Stock";
            cashierOrderForm_menuTable.Columns["Price"].HeaderText = "Price";
            cashierOrderForm_menuTable.Columns["Status"].HeaderText = "Status";
            cashierOrderForm_menuTable.Columns["CategoryName"].HeaderText = "Category Name";
            cashierOrderForm_menuTable.Columns["shop_id"].HeaderText = "shop_id";

            
        }




        


        public DataTable availableProductsData()
        {
            string query = "SELECT ProductID, ProductName, Stock, Price, Status, CategoryName " +
                           "FROM products WHERE Status = 'Available' AND Stock > 0";

            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }





        public void displayAllOrders()
        {
            CashierOrdersData allOrders = new CashierOrdersData();

            List<CashierOrdersData> listData = allOrders.ordersListData();

            cashierOrderForm_orderTable.DataSource = listData;
        }
        





        private float totalPrice;

        public void displayTotalPrice()
        {
            IDGenerator();

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT SUM(prod_price) FROM orders WHERE customer_id = @custId";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@custId", idGen);

                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            totalPrice = Convert.ToSingle(result);

                            cashierOrderForm_orderPrice.Text = totalPrice.ToString("0.00");
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }


      


        private void LoadCategories()
        {
            //UpdateCategories();
        }



        
        private void cashierOrderForm_addBtn_Click(object sender, EventArgs e)
        {
            IDGenerator();

            if (string.IsNullOrEmpty(cashierOrderForm_productID_txt.Text)
                || string.IsNullOrEmpty(cashierOrderForm_prodName_txt.Text)
                || cashierOrderForm_quantity.Value == 0
                || string.IsNullOrEmpty(cashierOrderForm_price.Text))
            {
                MessageBox.Show("Please select the product first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ensure a valid row is selected
            if (string.IsNullOrEmpty(cashierOrderForm_productID_txt.Text))
            {
                MessageBox.Show("Please select a row from the menu table.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    float getPrice = 0;
                    int stockQuantity = 0;
                    int stock_quantity = 0;
                    int shopID = 0;

                    // Retrieve shopID from the selected row
                    // You can use a placeholder for shopID if it is set somewhere else
                    if (cashierOrderForm_menuTable.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = cashierOrderForm_menuTable.SelectedRows[0];
                        shopID = Convert.ToInt32(selectedRow.Cells["shop_id"].Value);
                        stock_quantity = Convert.ToInt32(selectedRow.Cells["Stock"].Value);
                        //getPrice = Convert.ToInt32(selectedRow.Cells["Price"].Value);

                    }



                    

                    string selectOrder = "SELECT prod_price FROM products WHERE prod_id = @prodID AND shop_id = @shopID";

                    using (SqlCommand getOrder = new SqlCommand(selectOrder, connect))
                    {
                        getOrder.Parameters.AddWithValue("@prodID", cashierOrderForm_productID_txt.Text.Trim());
                        getOrder.Parameters.AddWithValue("@shopID", shopID);


                        using (SqlDataReader reader = getOrder.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                object rawPrice = reader["prod_price"];
                                //object rawStock = reader["prod_stock"];

                                if (rawPrice != DBNull.Value)
                                {
                                    getPrice = Convert.ToSingle(rawPrice);
                                }

                                
                            }
                        }
                    }



                    // Check stock level
                    int orderedQuantity = (int)cashierOrderForm_quantity.Value;
                    if (stock_quantity < orderedQuantity)
                    {
                        MessageBox.Show($"Insufficient stock. Available quantity: {stock_quantity}. Ordered quantity: {orderedQuantity}. Please adjust the quantity.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Proceed with the order insertion
                    string insertOrder = "INSERT INTO orders (customer_id, prod_id, prod_name, qty, prod_price, order_date, shop_id) " +
                        "VALUES(@customerID, @prodID, @prodName, @qty, @prodPrice, @orderDate, @shopID)";

                    DateTime today = DateTime.Today;

                    using (SqlCommand cmd = new SqlCommand(insertOrder, connect))
                    {
                        cmd.Parameters.AddWithValue("@customerID", idGen);
                        cmd.Parameters.AddWithValue("@prodID", cashierOrderForm_productID_txt.Text.Trim());
                        cmd.Parameters.AddWithValue("@prodName", cashierOrderForm_prodName_txt.Text);

                        float totalPrice = getPrice * orderedQuantity;

                        cmd.Parameters.AddWithValue("@qty", orderedQuantity);
                        cmd.Parameters.AddWithValue("@prodPrice", totalPrice);
                        cmd.Parameters.AddWithValue("@orderDate", today);
                        cmd.Parameters.AddWithValue("@shopID", shopID);


                        cmd.ExecuteNonQuery();
                    }

                    // Update stock in the database after order insertion
                    //string updateStock = "UPDATE products SET prod_stock = prod_stock - @orderedQuantity WHERE prod_id = @prodID AND shop_id = @shopID";
                    /*
                    using (SqlCommand updateCmd = new SqlCommand(updateStock, connect))
                    {
                        updateCmd.Parameters.AddWithValue("@orderedQuantity", orderedQuantity);
                        updateCmd.Parameters.AddWithValue("@prodID", cashierOrderForm_productID_txt.Text.Trim());
                        updateCmd.Parameters.AddWithValue("@shopID", shopID);

                        updateCmd.ExecuteNonQuery();
                    }
                    */

                    displayAllOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
            displayTotalPrice();
        }
        




       


        private int idGen = 0;


        public void IDGenerator()
        {
            using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connect.Open();
                string selectID = "SELECT MAX(customer_id) FROM customers";

                using (SqlCommand cmd = new SqlCommand(selectID, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int temp = Convert.ToInt32(result);

                        if (temp == 0)
                        {
                            idGen = 1;
                        }
                        else
                        {
                            idGen = temp + 1;
                        }
                    }
                    else
                    {
                        idGen = 1;
                    }
                    getCustID = idGen;
                }
            }
        }
    



        private void cashierOrderForm_amount_TextChanged(object sender, EventArgs e)
        {

        }

        private void cashierOrderForm_amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    float getAmount = Convert.ToSingle(cashierOrderForm_amount.Text);
                    float getPriceAfterDiscount = Convert.ToSingle(cashierOrderForm_price_after_discount.Text);
                    


                    float getChange = (getAmount - getPriceAfterDiscount);

                    if (getChange <= -1)
                    {
                        MessageBox.Show("Amount entered is less than the total price after discount.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        cashierOrderForm_amount.Text = "";
                        cashierOrderForm_change.Text = "";
                    }
                    else
                    {
                        cashierOrderForm_change.Text = getChange.ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cashierOrderForm_amount.Text = "";
                    cashierOrderForm_change.Text = "";
                }
            }
        }

        public void LoadAvailableProducts()
        {
            try
            {
                CashierOrderFormProdData prodData = new CashierOrderFormProdData();
                List<CashierOrderFormProdData> productList = prodData.availableProductsData();

                // Set DataSource to null to clear the DataGridView
                cashierOrderForm_orderTable.DataSource = null;

                // Re-populate the DataGridView with the new data
                cashierOrderForm_orderTable.DataSource = productList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void cashierOrderForm_payBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cashierOrderForm_amount.Text) || cashierOrderForm_orderTable.Rows.Count == 0)
            {
                MessageBox.Show("Something went wrong.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure for paying?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();

                        IDGenerator();

                        string insertData = "INSERT INTO customers (customer_id, total_price, amount, change, date, discount, price_after_discount) " +
                                            "VALUES(@custID, @totalprice, @amount, @change, @date, @discount, @priceAfterDiscount)";

                        DateTime today = DateTime.Today;

                        using (SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@custID", idGen);
                            cmd.Parameters.AddWithValue("@totalprice", totalPrice);
                            cmd.Parameters.AddWithValue("@discount", cashierOrderForm_discount.Text);
                            cmd.Parameters.AddWithValue("@priceAfterDiscount", cashierOrderForm_price_after_discount.Text);
                            cmd.Parameters.AddWithValue("@amount", cashierOrderForm_amount.Text);
                            cmd.Parameters.AddWithValue("@change", cashierOrderForm_change.Text);
                            cmd.Parameters.AddWithValue("@date", today);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Paid successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cashierOrderForm_payBtn.Enabled = false; // Disable the button
                        }

                        // Update stock levels
                        using (SqlTransaction transaction = connect.BeginTransaction())
                        {
                            try
                            {
                                foreach (DataGridViewRow row in cashierOrderForm_orderTable.Rows)
                                {
                                    if (row.Cells["ProdID"].Value == null || row.Cells["Qty"].Value == null)
                                    {
                                        continue; // Skip rows with missing data
                                    }

                                    string productId = row.Cells["ProdID"].Value.ToString();
                                    int quantity = int.Parse(row.Cells["Qty"].Value.ToString());

                                    string updateStockQuery = "UPDATE products SET prod_stock = prod_stock - @quantity WHERE prod_id = @productId";
                                    using (SqlCommand updateCmd = new SqlCommand(updateStockQuery, connect, transaction))
                                    {
                                        updateCmd.Parameters.AddWithValue("@productId", productId);
                                        updateCmd.Parameters.AddWithValue("@quantity", quantity);

                                        int rowsAffected = updateCmd.ExecuteNonQuery();
                                        if (rowsAffected == 0)
                                        {
                                            throw new Exception($"Product ID {productId} not found.");
                                        }
                                    }
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Stock update failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // Update orders table with discount
                        UpdateOrdersTable();

                        // Refresh the available products list
                        displayAvailableProds();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
            displayTotalPrice();
            // clearFields1(); // Uncomment if you want to clear fields after payment
        }



        private void UpdateOrdersTable()
        {
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }

            try
            {
                // Retrieve the discount from the textbox
                float discountPercentage = float.Parse(cashierOrderForm_discount.Text);
                float discountFactor = 1 - (discountPercentage / 100);

                //MessageBox.Show($"Discount Factor: {discountFactor}", "Debug Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Retrieve customer ID
                int customerId = idGen; // Assuming idGen holds the customer ID

                using (SqlTransaction transaction = connect.BeginTransaction())
                {
                    try
                    {
                        foreach (DataGridViewRow row in cashierOrderForm_orderTable.Rows)
                        {
                            if (row.Cells["ProdID"].Value == null || row.Cells["Qty"].Value == null || row.Cells["Price"].Value == null)
                            {
                                continue; // Skip rows with missing data
                            }

                            string productId = row.Cells["ProdID"].Value.ToString();
                            int quantity = int.Parse(row.Cells["Qty"].Value.ToString());
                            float price = float.Parse(row.Cells["Price"].Value.ToString());

                            // Retrieve the cost from the products table
                            float costPerUnit = 0;
                            string selectProductCostQuery = "SELECT cost FROM products WHERE prod_id = @productId";
                            using (SqlCommand getCostCmd = new SqlCommand(selectProductCostQuery, connect, transaction))
                            {
                                getCostCmd.Parameters.AddWithValue("@productId", productId);
                                object costObj = getCostCmd.ExecuteScalar();
                                if (costObj != null)
                                {
                                    costPerUnit = float.Parse(costObj.ToString());
                                }
                                else
                                {
                                    throw new Exception($"Product ID {productId} not found in products table.");
                                }
                            }

                            // Calculate the total cost and discounted price for the row
                            float totalCost = costPerUnit * quantity;
                            float originalTotalPrice = price;
                            float discountedPrice = originalTotalPrice * discountFactor;

                            string updateOrderQuery = "UPDATE orders SET discount = @discount, price_after_discount = @priceAfterDiscount, total_cost = @totalCost " +
                                                       "WHERE customer_id = @customerId AND prod_id = @productId AND order_date = @orderDate";

                            using (SqlCommand updateCmd = new SqlCommand(updateOrderQuery, connect, transaction))
                            {
                                updateCmd.Parameters.AddWithValue("@customerId", customerId);
                                updateCmd.Parameters.AddWithValue("@productId", productId);
                                updateCmd.Parameters.AddWithValue("@discount", discountPercentage);
                                updateCmd.Parameters.AddWithValue("@priceAfterDiscount", discountedPrice);
                                updateCmd.Parameters.AddWithValue("@totalCost", totalCost);
                                updateCmd.Parameters.AddWithValue("@orderDate", DateTime.Today); // Assuming orders are identified by date

                                int rowsAffected = updateCmd.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception($"Order for Product ID {productId} and Customer ID {customerId} not found.");
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Orders table update failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }








        private int rowIndex = 0;

        private void cashierOrderForm_receiptBtn_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
            
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rowIndex = 0;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            displayTotalPrice();

            float y = 0;
            int count = 0;
            int colWidth = 120;
            int headerMargin = 10;
            int tableMargin = 20;

            Font font = new Font("Arial", 12);
            Font bold = new Font("Arial", 12, FontStyle.Bold);
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font labelFont = new Font("Arial", 14, FontStyle.Bold);

            float margin = e.MarginBounds.Top;

            StringFormat alignCenter = new StringFormat();
            alignCenter.Alignment = StringAlignment.Center;
            alignCenter.LineAlignment = StringAlignment.Center;

            string headerText = "Shop Name";
            y = (margin + count * headerFont.GetHeight(e.Graphics) + headerMargin);
            e.Graphics.DrawString(headerText, headerFont, Brushes.Black, e.MarginBounds.Left
                + (cashierOrderForm_orderTable.Columns.Count / 2) * colWidth, y, alignCenter);

            count++;
            y += tableMargin;

            string[] header = { "CID", "ProdID", "ProdName",  "Qty", "Price" };

            for (int i = 1; i < header.Length+1; i++)
            {
                y = margin + count * bold.GetHeight(e.Graphics) + tableMargin;
                e.Graphics.DrawString(header[i-1], bold, Brushes.Black, e.MarginBounds.Left + (i-1) * colWidth, y, alignCenter);
            }
            count++;

            float rSpace = e.MarginBounds.Bottom - y;

            while (rowIndex < cashierOrderForm_orderTable.Rows.Count)
            {
                DataGridViewRow row = cashierOrderForm_orderTable.Rows[rowIndex];

                for (int i = 1; i < cashierOrderForm_orderTable.Columns.Count; i++)
                {
                    object cellValue = row.Cells[i].Value;
                    string cell = (cellValue != null) ? cellValue.ToString() : string.Empty;

                    y = margin + count * font.GetHeight(e.Graphics) + tableMargin;
                    e.Graphics.DrawString(cell, font, Brushes.Black, e.MarginBounds.Left + (i-1) * colWidth, y, alignCenter);
                }
                count++;
                rowIndex++;

                if (y + font.GetHeight(e.Graphics) > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            float discount = float.Parse(cashierOrderForm_discount.Text); // Assuming this is a TextBox for entering discount
            float amount = float.Parse(cashierOrderForm_amount.Text); // Assuming this is a TextBox for entering discount
            float priceAfterDiscount = totalPrice - (totalPrice * (discount / 100));
            float change = amount - priceAfterDiscount;


            int labelMargin = (int)Math.Min(rSpace, 200);

            DateTime today = DateTime.Now;

            float labelX = e.MarginBounds.Right - e.Graphics.MeasureString("------------------------------------------------", labelFont).Width;

            y = e.MarginBounds.Bottom - labelMargin - labelFont.GetHeight(e.Graphics);
            e.Graphics.DrawString("Total Price: \t\tRs" + totalPrice
                + "\nDiscount %: \t\t" + discount
                + "\nPrice After Discount: \tRs" + priceAfterDiscount
                + "\nAmount: \t\tRs" + cashierOrderForm_amount.Text 
                + "\n\t\t\t----------------\nChange: \t\tRs" + change, labelFont, Brushes.Black, labelX, y);

            labelMargin = (int)Math.Min(rSpace, -40);

            string labelText = today.ToString();
            y = e.MarginBounds.Bottom - labelMargin - labelFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(labelText, labelFont, Brushes.Black, e.MarginBounds.Right - e.Graphics.MeasureString("----------------------------------", labelFont).Width, y);
        }

        private void cashierOrderForm_removeBtn_Click(object sender, EventArgs e)
        {
            if (getOrderID == 0)
            {
                MessageBox.Show("Select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();

                        string deleteData = "DELETE FROM orders WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                        {
                            cmd.Parameters.AddWithValue("@id", getOrderID);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Removed successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
                cashierOrderForm_discount.Text = "";
            }

            // After deletion, refresh the order table and update UI as needed
            displayAllOrders();
            displayTotalPrice();
        }


        


        private void cashierOrderForm_orderTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private int getOrderID = 0;
        private object discount;
        private object price_after_discount;

        private void cashierOrderForm_orderTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < cashierOrderForm_orderTable.Rows.Count)
            {
                DataGridViewRow row = cashierOrderForm_orderTable.Rows[e.RowIndex];

                // Debugging output to check cell value and row index
                Console.WriteLine($"Clicked row index: {e.RowIndex}");
                Console.WriteLine($"Cell value: {row.Cells["ID"].Value}");

                // Assigning getOrderID variable
                getOrderID = Convert.ToInt32(row.Cells["ID"].Value); // Ensure "ID" matches your column name

                // Optionally, you can also update the UI based on the selected row

                


            }
        }


        






        public void clearFields()
        {
            // Clear ComboBox items and reset selection
            cashierOrderForm_productID_txt.Text = "";
            //cashierOrderForm_productID_txt.SelectedIndex = -1;

            // Clear other form fields
            //cashierOrderForm_type.SelectedIndex = -1;
            cashierOrderForm_prodName_txt.Text = "";
            cashierOrderForm_price.Text = "";
            cashierOrderForm_quantity.Value = 0;
        }



        public void clearFields1()
        {
          

            cashierOrderForm_discount.Text = "";
            cashierOrderForm_amount.Text = "";
            cashierOrderForm_orderPrice.Text = "";
            cashierOrderForm_price_after_discount.Text = "";
            cashierOrderForm_change.Text = "";
        }



        private void cashierOrderForm_clearBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void cashierOrderForm_clearBtn_Click_1(object sender, EventArgs e)
        {
            displayAllOrders();
            displayTotalPrice();

            clearFields();
        }

        private void CashierOrderForm_Load(object sender, EventArgs e)
        {
            LoadAvailableProducts();
            displayAllOrders(); // Optionally, you can load all orders as well


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cashierOrderForm_discount_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    float getDiscount = Convert.ToSingle(cashierOrderForm_discount.Text);

                    float getPriceAfterDiscount = (totalPrice - (totalPrice * (getDiscount / 100)));


                    if (getPriceAfterDiscount < 0)
                    {
                        MessageBox.Show("Invalid discount value. Discounted price cannot be negative.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        cashierOrderForm_discount.Text = "";
                        cashierOrderForm_price_after_discount.Text = "";
                    }
                    else
                    {
                        cashierOrderForm_price_after_discount.Text = getPriceAfterDiscount.ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cashierOrderForm_discount.Text = "";
                    cashierOrderForm_price_after_discount.Text = "";
                }
            }

        }
      

        // Assuming id is declared at the class level of your form or within a relevant scope
        private int id; // Declare id at the class level
        private object textBoxDiscountPercentage;

        // Your event handler
        private void cashierOrderForm_menuTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < cashierOrderForm_menuTable.Rows.Count)
            {
                DataGridViewRow row = cashierOrderForm_menuTable.Rows[e.RowIndex];

                // Assigning id variable
                id = (int)row.Cells[0].Value; // Assuming ID is in the first column

                // Displaying data in TextBoxes or other controls
                cashierOrderForm_productID_txt.Text = row.Cells[1].Value?.ToString() ?? ""; // Assuming ProductID or ID is in the first column
                cashierOrderForm_prodName_txt.Text = row.Cells[2].Value?.ToString() ?? ""; // Assuming ProductName is in the second column
                cashierOrderForm_price.Text = row.Cells[4].Value?.ToString() ?? ""; // Assuming Price is in the fourth column
                                                                                    // Ensure to handle null values to prevent exceptions
                displayAvailableProds(); // Optionally, you can load all orders as well
                LoadCategories();
                //int shop_id = Convert.ToInt32(row.Cells["shop_id"].Value);


            }
        }





        public string PromptForPassword()
        {
            string password = null;
            Form prompt = new Form();
            prompt.Width = 300;
            prompt.Height = 150;
            prompt.Text = "Enter Password";
            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Password:" };
            TextBox passwordBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
            passwordBox.UseSystemPasswordChar = true; // Hide password characters

            Button confirmation = new Button() { Text = "Ok", Left = 160, Width = 100, Top = 80 };
            confirmation.Click += (sender, e) => { password = passwordBox.Text; prompt.Close(); };

            prompt.Controls.Add(passwordBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.StartPosition = FormStartPosition.CenterScreen;

            prompt.ShowDialog();
            return password;
        }






        private void cashierOrderForm_menuTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cashierOrderForm_orderTable.DataSource = null; // This will clear the DataGridView

            clearFields1();
            cashierOrderForm_payBtn.Enabled = true; 


        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayAvailableProds(); // Optionally, you can load all orders as well
            LoadCategories();
            LoadPromotions();
            cashierOrderForm_price_after_discount.Text = "";

            //UpdateCategories();
        }
    }
}
