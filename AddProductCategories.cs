using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class AddProductCategories : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public AddProductCategories()
        {
            InitializeComponent();
            DisplayCategories();


            dataGridViewCategories.CellClick += dataGridViewCategories_CellClick;

        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            DisplayCategories();
        }



        private void DisplayCategories()
        {
            try
            {
                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridViewCategories.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("All fields are required to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string categoryName = txtCategoryName.Text.Trim();
                if (CategoryExists(categoryName))
                {
                    MessageBox.Show(categoryName + " already exists.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    AddCategory(categoryName);
                }
            }
        }

        private bool EmptyFields()
        {
            return string.IsNullOrEmpty(txtCategoryName.Text.Trim());
        }

        private bool CategoryExists(string categoryName)
        {
            bool exists = false;
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "SELECT COUNT(*) FROM Categories WHERE CategoryName = @CategoryName";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                int count = (int)cmd.ExecuteScalar();
                exists = (count > 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
            return exists;
        }

        private void AddCategory(string categoryName)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DisplayCategories();
                txtCategoryName.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }

        private int selectedCategoryId = 0;

        private void dataGridViewCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is not a header cell
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewCategories.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dataGridViewCategories.Rows[e.RowIndex];

                    // Get values from the clicked row
                    selectedCategoryId = Convert.ToInt32(row.Cells["CategoryID"].Value); // Ensure this column name matches
                    txtCategoryName.Text = row.Cells["CategoryName"].Value.ToString(); // Ensure this column name matches

                    // Enable update and delete buttons
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string newCategoryName = txtCategoryName.Text.Trim();
                DialogResult result = MessageBox.Show("Are you sure you want to update Category ID: " + selectedCategoryId + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    UpdateCategory(selectedCategoryId, newCategoryName);
                }
            }
        }

        private void UpdateCategory(int categoryId, string newCategoryName)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@CategoryName", newCategoryName);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Category updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DisplayCategories();
                txtCategoryName.Text = "";
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete Category ID: " + selectedCategoryId + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteCategory(selectedCategoryId);
                }
            }
        }

        private void DeleteCategory(int categoryId)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                // Create and open connection
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();

                    // Check if category has associated products
                    if (CategoryHasProducts(categoryId, connect))
                    {
                        MessageBox.Show("Cannot delete this category. It has associated products.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Delete category
                    string query = "DELETE FROM Categories WHERE CategoryID = @CategoryID";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Category deletion failed. The category might not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                // Refresh display and clear form
                DisplayCategories();
                txtCategoryName.Text = "";
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CategoryHasProducts(int categoryId, SqlConnection connect)
        {
            bool hasProducts = false;
            try
            {
                string query = "SELECT COUNT(*) FROM Products WHERE CategoryID = @CategoryID";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                    int productCount = (int)cmd.ExecuteScalar();
                    hasProducts = (productCount > 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return hasProducts;
        }

    }
}
