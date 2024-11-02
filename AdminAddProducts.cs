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
using System.IO;
using System.Configuration;

namespace CafeShopManagementSystem
{
    public partial class AdminAddProducts : UserControl
    {
        //static string conn = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;
        //SqlConnection connect = new SqlConnection(conn);
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        private string selectedProductId;


        public AdminAddProducts()
        {
            InitializeComponent();
            LoadCategories();
            displayData();
            LoadProductsIntoDataGridView();

            adminAddProducts_name.Enabled = false;
            adminAddProducts_stock.Enabled = false;




        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayData();
        }

        private bool emptyFields()
        {
            if (string.IsNullOrEmpty(adminAddProducts_id.Text) || string.IsNullOrEmpty(adminAddProducts_name.Text)
                || string.IsNullOrEmpty(adminAddProducts_stock.Text)
                || string.IsNullOrEmpty(adminAddProducts_price.Text) || adminAddProducts_status.SelectedIndex == -1
                || adminAddProducts_category.SelectedIndex == -1)
            {
                return true;
            }
            return false;
        }



        private void AdminAddProducts_Load(object sender, EventArgs e)
        {
            LoadCategories(); // Load categories into dropdown when the form loads
            displayData(); // Display data when the form loads
        }

        private void LoadCategories()
        {
            UpdateCategories();
            adminAddProducts_category.Items.Clear(); // Clear existing items first

            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlCommand cmd = new SqlCommand(query, connect);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int categoryId = Convert.ToInt32(reader["CategoryID"]);
                    string categoryName = reader["CategoryName"].ToString();
                    Category category = new Category(categoryId, categoryName);
                    adminAddProducts_category.Items.Add(category);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }







        public void displayData()
        {
            AdminAddProductsData prodData = new AdminAddProductsData();
            List<AdminAddProductsData> listData = prodData.productsListData();

            DataGridView1.DataSource = listData;

            // Hide the CategoryID column
            if (DataGridView1.Columns["CategoryID"] != null)
            {
                DataGridView1.Columns["CategoryID"].Visible = false;
            }


            // Ensure the columns are properly displayed
            DataGridView1.Columns["ProductID"].HeaderText = "Product ID";
            DataGridView1.Columns["ProductName"].HeaderText = "Product Name";
            //DataGridView1.Columns["Type"].HeaderText = "Product Type";
            DataGridView1.Columns["Stock"].HeaderText = "Stock";
            DataGridView1.Columns["Price"].HeaderText = "Price";
            DataGridView1.Columns["Cost"].HeaderText = "Cost";
            DataGridView1.Columns["Status"].HeaderText = "Status";
            DataGridView1.Columns["CategoryName"].HeaderText = "Category Name";
            DataGridView1.Columns["DateInsert"].HeaderText = "Date Inserted";
            DataGridView1.Columns["shop_id"].HeaderText = "shop_id";

        }





        /*
        private void adminAddProducts_addBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connect.Open();

                // Check if the product ID already exists
                string selectProdID = "SELECT * FROM products WHERE prod_id = @prodID";
                using (SqlCommand selectPID = new SqlCommand(selectProdID, connect))
                {
                    selectPID.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                    SqlDataReader reader = selectPID.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Product ID: " + adminAddProducts_id.Text.Trim() + " is already taken.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        reader.Close();
                        return;
                    }
                    reader.Close();
                }

                // Get selected category
                Category selectedCategory = (Category)adminAddProducts_category.SelectedItem;
                int categoryId = selectedCategory.CategoryID;

                // Insert new product into the database
                string insertData = "INSERT INTO products (prod_id, prod_name,  prod_stock, prod_price, prod_status, CategoryID, date_insert) " +
                                    "VALUES(@prodID, @prodName, @prodStock, @prodPrice, @prodStatus, @categoryID, @dateInsert)";

                DateTime today = DateTime.Today;

                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                {
                    cmd.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                    cmd.Parameters.AddWithValue("@prodName", adminAddProducts_name.Text.Trim());
                    //cmd.Parameters.AddWithValue("@prodType", adminAddProducts_type.Text.Trim());
                    cmd.Parameters.AddWithValue("@prodStock", adminAddProducts_stock.Text.Trim());
                    cmd.Parameters.AddWithValue("@prodPrice", adminAddProducts_price.Text.Trim());
                    cmd.Parameters.AddWithValue("@prodStatus", adminAddProducts_status.Text.Trim());
                    cmd.Parameters.AddWithValue("@categoryID", categoryId);
                    cmd.Parameters.AddWithValue("@dateInsert", today);

                    cmd.ExecuteNonQuery();
                    clearFields();

                    MessageBox.Show("Product added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayData();
                    LoadProductsIntoDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add product: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }
        */





        public void clearFields()
        {
            adminAddProducts_id.Text = "";
            adminAddProducts_name.Text = "";
            //adminAddProducts_type.SelectedIndex = -1;
            adminAddProducts_stock.Text = "";
            adminAddProducts_price.Text = "";
            adminAddProducts_status.SelectedIndex = -1;
            adminAddProducts_category.SelectedIndex = -1;
        }


        private void adminAddProducts_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DataGridView1.Rows.Count)
            {
                DataGridViewRow row = DataGridView1.Rows[e.RowIndex];

                // Clear previous selections
                clearFields();


                adminAddProducts_id.Text = row.Cells["ProductID"].Value.ToString();
                adminAddProducts_name.Text = row.Cells["ProductName"].Value.ToString();
                //adminAddProducts_type.Text = row.Cells["Type"].Value.ToString();
                adminAddProducts_stock.Text = row.Cells["Stock"].Value.ToString();
                adminAddProducts_price.Text = row.Cells["Price"].Value.ToString();
                adminAddProducts_status.Text = row.Cells["Status"].Value.ToString();

                // Store the Product ID
                selectedProductId = row.Cells["ID"].Value.ToString();


                try
                {
                    int categoryId = Convert.ToInt32(row.Cells["CategoryID"].Value);
                    string categoryName = row.Cells["CategoryName"].Value.ToString();

                    // Populate category dropdown or textbox with retrieved values
                    foreach (Category category in adminAddProducts_category.Items)
                    {
                        if (category.CategoryID == categoryId && category.CategoryName == categoryName)
                        {
                            adminAddProducts_category.SelectedItem = category;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching category name: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void adminAddProducts_updateBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult check = MessageBox.Show("Are you sure you want to update Product ID: " + adminAddProducts_id.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        // Check for uniqueness of prod_id
                        string checkUniqueIdQuery = "SELECT COUNT(*) FROM products WHERE prod_id = @ProductID AND product_id != @CurrentID";
                        using (SqlCommand checkCmd = new SqlCommand(checkUniqueIdQuery, connect))
                        {
                            checkCmd.Parameters.AddWithValue("@ProductID", adminAddProducts_id.Text.Trim());
                            checkCmd.Parameters.AddWithValue("@CurrentID", selectedProductId); // Exclude current product ID

                            int count = (int)checkCmd.ExecuteScalar();
                            if (count > 0)
                            {
                                MessageBox.Show("The Product ID already exists. Please use a different Product ID.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Proceed with the update
                        string updateData = @"
                UPDATE products 
                SET prod_name = @prodName, 
                    prod_id = @ProductID,
                    prod_stock = @prodStock, 
                    prod_price = @prodPrice, 
                    prod_status = @prodStatus, 
                    CategoryID = @categoryID, 
                    date_update = @dateUpdate 
                WHERE product_id = @ID";

                        DateTime today = DateTime.Today;

                        Category selectedCategory = (Category)adminAddProducts_category.SelectedItem;
                        int categoryId = selectedCategory.CategoryID;

                        using (SqlCommand cmd = new SqlCommand(updateData, connect))
                        {
                            cmd.Parameters.AddWithValue("@ID", selectedProductId); // Use selectedProductId here
                            cmd.Parameters.AddWithValue("@ProductID", adminAddProducts_id.Text.Trim());
                            cmd.Parameters.AddWithValue("@prodName", adminAddProducts_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@prodStock", adminAddProducts_stock.Text.Trim());
                            cmd.Parameters.AddWithValue("@prodPrice", adminAddProducts_price.Text.Trim());
                            cmd.Parameters.AddWithValue("@prodStatus", adminAddProducts_status.Text.Trim());
                            cmd.Parameters.AddWithValue("@categoryID", categoryId); // Ensure CategoryID is updated
                            cmd.Parameters.AddWithValue("@dateUpdate", today);

                            cmd.ExecuteNonQuery();
                            clearFields();

                            MessageBox.Show("Product updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            displayData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to update product: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }









        /*
        private void adminAddProducts_deleteBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult check = MessageBox.Show("Are you sure you want to delete Product ID: " + adminAddProducts_id.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                try
                {
                    connect.Open();

                    string deleteData = "UPDATE products SET date_delete = @dateDelete WHERE prod_id = @prodID";
                    DateTime today = DateTime.Today;

                    using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                    {
                        cmd.Parameters.AddWithValue("@dateDelete", today);
                        cmd.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());

                        cmd.ExecuteNonQuery();
                        clearFields();

                        MessageBox.Show("Removed successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        displayData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete product: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }
        */

        

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategories();
        }

        public void UpdateCategories()
        {
            adminAddProducts_category.Items.Clear(); // Clear existing items first

            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlCommand cmd = new SqlCommand(query, connect);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int categoryId = Convert.ToInt32(reader["CategoryID"]);
                    string categoryName = reader["CategoryName"].ToString();
                    Category category = new Category(categoryId, categoryName);
                    adminAddProducts_category.Items.Add(category);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }



        private void LoadProductsIntoDataGridView()
        {
            /*
            try
            {
               

                // Query to retrieve products data including category name
                string query = @"
            SELECT p.id, p.prod_id, p.prod_name, p.prod_type, p.prod_stock, p.prod_price, p.prod_status, p.date_insert, p.date_update, c.CategoryName
            FROM products p
            JOIN Categories c ON p.CategoryID = c.CategoryID
            WHERE p.date_delete IS NULL";

                SqlCommand cmd = new SqlCommand(query, connect);
                SqlDataReader reader = cmd.ExecuteReader();

                // Populate DataGridView with retrieved data
                while (reader.Read())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(DataGridView1);

                    // Set values for each cell in the row
                    row.Cells[0].Value = reader["id"].ToString();
                    row.Cells[1].Value = reader["prod_id"].ToString();
                    row.Cells[2].Value = reader["prod_name"].ToString();
                    row.Cells[3].Value = reader["prod_type"].ToString();
                    row.Cells[4].Value = reader["prod_stock"].ToString();
                    row.Cells[5].Value = reader["prod_price"].ToString();
                    row.Cells[6].Value = reader["prod_status"].ToString();
                    row.Cells[7].Value = reader["CategoryName"].ToString();
                    row.Cells[8].Value = reader["date_insert"].ToString();
                    row.Cells[9].Value = reader["date_update"].ToString();

                    DataGridView1.Rows.Add(row);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {

            LoadCategories();
            displayData();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void adminAddProducts_category_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
