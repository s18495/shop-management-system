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

namespace CafeShopManagementSystem
{
    public partial class Shop : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");


        public Shop()
        {
            InitializeComponent();
            DisplayShops();


            dataGridViewShops.CellClick += dataGridViewShops_CellClick;
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            DisplayShops();
        }

        private void DisplayShops()
        {
            try
            {
                string query = "SELECT shop_id, shop_name, address, phone FROM shops";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridViewShops.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        

        private void dataGridViewCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        

        private void btnAddShop_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("All fields are required to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string shopName = txtShopName.Text.Trim();
                string address = txtAddress.Text.Trim();
                string phone = txtPhone.Text.Trim();

                if (ShopExists(shopName))
                {
                    MessageBox.Show(shopName + " already exists.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    AddShop(shopName, address, phone);
                }
            }
        }

        private bool EmptyFields()
        {
            return string.IsNullOrEmpty(txtShopName.Text.Trim());
            return string.IsNullOrEmpty(txtAddress.Text.Trim());

            return string.IsNullOrEmpty(txtPhone.Text.Trim());

        }


        private bool ShopExists(string shop_name)
        {
            bool exists = false;
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "SELECT COUNT(*) FROM shops WHERE shop_name = @shop_name";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@shop_name", shop_name);

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


        private void AddShop(string shopName, string address, string phone)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "INSERT INTO shops (shop_name, address, phone) VALUES (@shop_name, @address, @phone)";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@shop_name", shopName);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Shop added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the list of shops to reflect the changes
                DisplayShops();
                txtShopName.Text = "";
                txtAddress.Text = "";  // Assuming you have a TextBox for Address
                txtPhone.Text = "";    // Assuming you have a TextBox for Phone
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding shop: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }

        private int selectedShopId = 0;

        private void dataGridViewShops_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is not a header cell
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewShops.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dataGridViewShops.Rows[e.RowIndex];

                    // Get values from the clicked row
                    selectedShopId = Convert.ToInt32(row.Cells["shop_id"].Value); // Ensure this column name matches
                    txtShopName.Text = row.Cells["shop_name"].Value.ToString(); // Ensure this column name matches
                    txtAddress.Text = row.Cells["address"].Value.ToString();   // Ensure this column name matches
                    txtPhone.Text = row.Cells["phone"].Value.ToString();       // Ensure this column name matches

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
                string newShopName = txtShopName.Text.Trim();
                string newAddress = txtAddress.Text.Trim();
                string newPhone = txtPhone.Text.Trim();
                DialogResult result = MessageBox.Show("Are you sure you want to update Shop ID: " + selectedShopId + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    UpdateShop(selectedShopId, newShopName, newAddress, newPhone);
                }
            }
        }

        private void UpdateShop(int shop_id, string shop_name, string address, string phone)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                string query = "UPDATE Shops SET shop_name = @shop_name, address = @address, phone = @phone WHERE shop_id = @shop_id";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@shop_id", shop_id);
                cmd.Parameters.AddWithValue("@shop_name", shop_name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Shop updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DisplayShops();
                txtShopName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating shop: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DialogResult result = MessageBox.Show("Are you sure you want to delete Shop ID: " + selectedShopId + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteShop(selectedShopId);
                }
            }
        }

        private void DeleteShop(int shop_id)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();

                // Delete shop
                string query = "DELETE FROM shops WHERE shop_id = @shop_id";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@shop_id", shop_id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Shop deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Shop deletion failed. The shop might not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Refresh display and clear form
                DisplayShops();
                txtShopName.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting shop: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
