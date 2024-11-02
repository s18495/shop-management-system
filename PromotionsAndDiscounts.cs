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
    public partial class PromotionsAndDiscounts : UserControl
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public int PromotionID { get; private set; }

        public PromotionsAndDiscounts()
        {
            InitializeComponent();
            LoadPromotions();

           


        }

        // Method to refresh DataGridView
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            LoadPromotions();
        }

        private void LoadPromotions()
        {
            try
            {
                connect.Open();
                string query = "SELECT * FROM promotions";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvPromotions.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading promotions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }



        // Check if any required field is empty
        private bool EmptyFields()
        {
            return string.IsNullOrWhiteSpace(txtPromotionName.Text) ||
                   string.IsNullOrWhiteSpace(txtDiscountDetails.Text) ||
                   string.IsNullOrWhiteSpace(txtDiscountPercentage.Text);
        }

        private void ClearFields()
        {
            txtPromotionName.Clear();
            txtDiscountDetails.Clear();
            txtDiscountPercentage.Clear();
            dtpStartDate.Value = DateTime.Today;
            //dtpEndDate.Value = DateTime.Today;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();
                    string query = @"INSERT INTO promotions (PromotionName, Description, DiscountPercentage, StartDate) 
                             VALUES (@PromotionName, @Description, @DiscountPercentage, @StartDate)";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@PromotionName", txtPromotionName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", txtDiscountDetails.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiscountPercentage", Convert.ToDecimal(txtDiscountPercentage.Text.Trim()));
                        cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                        //cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Promotion created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadPromotions();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating promotion: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Repeat similar changes in btnUpdate_Click, btnDelete_Click, and other methods.


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();
                    string query = @"UPDATE promotions SET PromotionName = @PromotionName, Description = @Description, 
                             DiscountPercentage = @DiscountPercentage, StartDate = @StartDate 
                             WHERE PromotionID = @PromotionID";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@PromotionName", txtPromotionName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", txtDiscountDetails.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiscountPercentage", Convert.ToDecimal(txtDiscountPercentage.Text.Trim()));
                        cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                        //cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
                        cmd.Parameters.AddWithValue("@PromotionID", GetSelectedPromotionId());

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Promotion updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadPromotions();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating promotion: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadPromotions();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (EmptyFields())
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int promotionIdToDelete = GetSelectedPromotionId();
            if (promotionIdToDelete == 0)
            {
                MessageBox.Show("Please select a promotion to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this promotion?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        connect.Open();
                        string query = "DELETE FROM promotions WHERE PromotionID = @PromotionID";
                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@PromotionID", promotionIdToDelete);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Promotion deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadPromotions();
                                ClearFields();
                            }
                            else
                            {
                                MessageBox.Show("No rows deleted. Promotion may not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting promotion: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public DataTable GetPromotions()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connect.Open();
                    string query = "SELECT PromotionID, PromotionName, DiscountPercentage FROM promotions";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading promotions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }





        private void dgvPromotions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPromotions.Rows[e.RowIndex];

                // Handle null values or invalid conversions gracefully
                try
                {
                    PromotionID = Convert.ToInt32(row.Cells["PromotionID"].Value);
                    txtPromotionName.Text = row.Cells["PromotionName"].Value.ToString();
                    txtDiscountDetails.Text = row.Cells["Description"].Value.ToString();
                    //txtDiscountPercentage.Text = row.Cells["DiscountPercentage"].Value.ToString();

                    // Format DiscountPercentage as a percentage without ".00"
                    if (decimal.TryParse(row.Cells["DiscountPercentage"].Value.ToString(), out decimal discountPercentage))
                    {
                        txtDiscountPercentage.Text = discountPercentage.ToString("0.##"); // Display without ".00"
                    }
                    else
                    {
                        txtDiscountPercentage.Text = ""; // Handle if conversion fails
                    }
                    
                    // Convert and set DateTimePicker values
                    if (DateTime.TryParse(row.Cells["StartDate"].Value.ToString(), out DateTime startDate))
                    {
                        dtpStartDate.Value = startDate;
                    }
                    else
                    {
                        dtpStartDate.Value = DateTime.Today;
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading row data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private int GetSelectedPromotionId()
        {
            if (dgvPromotions.SelectedRows.Count > 0)
            {
                // Assuming "PromotionID" is the name of the column in your DataGridView
                object promotionIdObj = dgvPromotions.SelectedRows[0].Cells["PromotionID"].Value;

                // Check for null or DBNull before conversion
                if (promotionIdObj != null && promotionIdObj != DBNull.Value)
                {
                    return Convert.ToInt32(promotionIdObj);
                }
            }
            return 0; // Return 0 if no row is selected or if PromotionID is null/DBNull
        }



    }
}
