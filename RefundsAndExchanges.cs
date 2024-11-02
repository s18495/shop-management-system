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
    public partial class RefundsAndExchanges : UserControl
    {

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30";


        public RefundsAndExchanges()
        {
            InitializeComponent();
            LoadReturnTrends();
            txtOrderID.Enabled = false;
            dgvReturnTrends.ReadOnly = true;


        }

        private void RefundsAndExchanges_Load(object sender, EventArgs e)
        {

        }


        private void ClearTextboxes()
        {
            txtOrderID.Clear();
            txtReasonForReturn.Clear();
            txtConditionOfItems.Clear();
            txtQuantity.Clear();
        }




        private void btnProcessRefund_Click(object sender, EventArgs e)
        {
            // Check if OrderID is already present in RefundsAndExchanges table
            if (IsOrderIDExists(txtOrderID.Text))
            {
                MessageBox.Show("Refund for this OrderID already exists. Cannot insert duplicate refund.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Validate input
                    if (string.IsNullOrWhiteSpace(txtOrderID.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text))
                    {
                        MessageBox.Show("OrderID and Quantity cannot be empty.");
                        return;
                    }

                    int refundQuantity;
                    if (!int.TryParse(txtQuantity.Text, out refundQuantity) || refundQuantity <= 0)
                    {
                        MessageBox.Show("Quantity must be a positive integer.");
                        return;
                    }

                    // Get ProductID and Order Details from the orders table
                    string getOrderDetailsQuery = "SELECT prod_id, qty FROM orders WHERE id = @OrderID";
                    string productId = null;
                    int originalQuantity = 0;

                    using (SqlCommand getOrderDetailsCmd = new SqlCommand(getOrderDetailsQuery, conn))
                    {
                        getOrderDetailsCmd.Parameters.AddWithValue("@OrderID", txtOrderID.Text);

                        using (SqlDataReader reader = getOrderDetailsCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                productId = reader["prod_id"].ToString();
                                originalQuantity = Convert.ToInt32(reader["qty"]);
                            }
                        }
                    }

                    // Check if refund quantity is valid
                    if (refundQuantity > originalQuantity)
                    {
                        MessageBox.Show("Refund quantity cannot exceed the original order quantity.");
                        return;
                    }

                    // Insert refund details
                    string insertQuery = "INSERT INTO RefundsAndExchanges (OrderID, RefundReason, ConditionOfItems, RefundDate, Quantity) " +
                                         "VALUES (@OrderID, @RefundReason, @ConditionOfItems, @RefundDate, @Quantity)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@OrderID", txtOrderID.Text);
                        insertCmd.Parameters.AddWithValue("@RefundReason", txtReasonForReturn.Text);
                        insertCmd.Parameters.AddWithValue("@ConditionOfItems", txtConditionOfItems.Text);
                        insertCmd.Parameters.AddWithValue("@RefundDate", DateTime.Now);
                        insertCmd.Parameters.AddWithValue("@Quantity", refundQuantity);

                        insertCmd.ExecuteNonQuery();
                    }

                    // Update product quantity
                    string updateProductQuery = "UPDATE products SET prod_stock = prod_stock + @Quantity WHERE prod_id = @ProductID";
                    using (SqlCommand updateProductCmd = new SqlCommand(updateProductQuery, conn))
                    {
                        updateProductCmd.Parameters.AddWithValue("@Quantity", refundQuantity);
                        updateProductCmd.Parameters.AddWithValue("@ProductID", productId);
                        updateProductCmd.ExecuteNonQuery();
                    }

                    // Update the orders table with the new quantity
                    string updateOrderQuery = "UPDATE orders SET qty = qty - @RefundQuantity WHERE id = @OrderID";
                    using (SqlCommand updateOrderCmd = new SqlCommand(updateOrderQuery, conn))
                    {
                        updateOrderCmd.Parameters.AddWithValue("@RefundQuantity", refundQuantity);
                        updateOrderCmd.Parameters.AddWithValue("@OrderID", txtOrderID.Text);
                        updateOrderCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Refund processed successfully.");
                    LoadReturnTrends(); // Refresh the DataGridView after inserting a new record
                    ClearTextboxes(); // Clear the textboxes after processing the refund
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }






        // Method to check if OrderID already exists in RefundsAndExchanges table
        private bool IsOrderIDExists(string orderId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM RefundsAndExchanges WHERE OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", orderId);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // If count > 0, OrderID exists; otherwise, it doesn't
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return false; // Return false in case of exception
                }
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check if OrderID is null or empty
            if (string.IsNullOrEmpty(txtOrderID.Text))
            {
                MessageBox.Show("OrderID cannot be null or empty for update operation.");
                return;
            }





            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Validate input
                    if (string.IsNullOrWhiteSpace(txtOrderID.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text))
                    {
                        MessageBox.Show("OrderID and Quantity cannot be empty.");
                        return;
                    }

                    int refundQuantity;
                    if (!int.TryParse(txtQuantity.Text, out refundQuantity) || refundQuantity <= 0)
                    {
                        MessageBox.Show("Quantity must be a positive integer.");
                        return;
                    }

                    // Get ProductID and Order Details from the orders table
                    string getOrderDetailsQuery = "SELECT prod_id, qty FROM orders WHERE id = @OrderID";
                    string productId = null;
                    int originalQuantity = 0;

                    using (SqlCommand getOrderDetailsCmd = new SqlCommand(getOrderDetailsQuery, conn))
                    {
                        getOrderDetailsCmd.Parameters.AddWithValue("@OrderID", txtOrderID.Text);

                        using (SqlDataReader reader = getOrderDetailsCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                productId = reader["prod_id"].ToString();
                                originalQuantity = Convert.ToInt32(reader["qty"]);
                            }
                        }
                    }

                    // Check if refund quantity is valid
                    if (refundQuantity > originalQuantity)
                    {
                        MessageBox.Show("Refund quantity cannot exceed the original order quantity.");
                        return;
                    }

                    // Insert refund details
                    string updateQuery = "UPDATE RefundsAndExchanges SET RefundReason = @RefundReason, ConditionOfItems = @ConditionOfItems, Quantity = Quantity + @Quantity " +
                     "WHERE OrderID = @OrderID";

                    using (SqlCommand insertCmd = new SqlCommand(updateQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@OrderID", txtOrderID.Text);
                        insertCmd.Parameters.AddWithValue("@RefundReason", txtReasonForReturn.Text);
                        insertCmd.Parameters.AddWithValue("@ConditionOfItems", txtConditionOfItems.Text);
                        insertCmd.Parameters.AddWithValue("@RefundDate", DateTime.Now);
                        insertCmd.Parameters.AddWithValue("@Quantity", refundQuantity);

                        insertCmd.ExecuteNonQuery();
                    }

                    // Update product quantity
                    string updateProductQuery = "UPDATE products SET prod_stock = prod_stock + @Quantity WHERE prod_id = @ProductID";
                    using (SqlCommand updateProductCmd = new SqlCommand(updateProductQuery, conn))
                    {
                        updateProductCmd.Parameters.AddWithValue("@Quantity", refundQuantity);
                        updateProductCmd.Parameters.AddWithValue("@ProductID", productId);
                        updateProductCmd.ExecuteNonQuery();
                    }

                    // Update the orders table with the new quantity
                    string updateOrderQuery = "UPDATE orders SET qty = qty - @RefundQuantity WHERE id = @OrderID";
                    using (SqlCommand updateOrderCmd = new SqlCommand(updateOrderQuery, conn))
                    {
                        updateOrderCmd.Parameters.AddWithValue("@RefundQuantity", refundQuantity);
                        updateOrderCmd.Parameters.AddWithValue("@OrderID", txtOrderID.Text);
                        updateOrderCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Refund processed successfully.");
                    LoadReturnTrends(); // Refresh the DataGridView after inserting a new record
                    ClearTextboxes(); // Clear the textboxes after processing the refund
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }






        }



        private void LoadReturnTrends()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            o.id AS OrderID,
                            o.customer_id AS CustomerID,
                            o.prod_name AS ProductName,
                            o.price_after_discount AS Price,
                            o.qty AS Quantity,
                            o.order_date AS OrderDate,
                            o.shop_id AS shop_id,
                            re.RefundID,
                            re.RefundReason,
                            re.ConditionOfItems,
                            re.RefundDate,
                            re.Quantity AS RefundQuantity
                        FROM 
                            orders o
                        LEFT JOIN 
                            RefundsAndExchanges re ON o.id = re.OrderID";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReturnTrends.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }




        private void dgvReturnTrends_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Check both row and column index are valid
                {
                    DataGridViewRow row = dgvReturnTrends.Rows[e.RowIndex];

                    // Assuming the "OrderID" column is present and accessible
                    if (row.Cells["OrderID"].Value != null)
                    {
                        int orderId = Convert.ToInt32(row.Cells["OrderID"].Value); // Convert to the appropriate data type
                        MessageBox.Show($"Clicked on OrderID: {orderId}");
                    }
                    else
                    {
                        MessageBox.Show("OrderID is null or empty.");
                    }

                    // Retrieve and display other fields
                    txtOrderID.Text = row.Cells["OrderID"].Value?.ToString() ?? string.Empty;
                    txtReasonForReturn.Text = row.Cells["RefundReason"].Value?.ToString() ?? string.Empty;
                    txtConditionOfItems.Text = row.Cells["ConditionOfItems"].Value?.ToString() ?? string.Empty;
                    txtQuantity.Text = row.Cells["Quantity"].Value?.ToString() ?? string.Empty;

                    // Check if both reason for return and condition of items are null or empty
                    if (string.IsNullOrEmpty(txtReasonForReturn.Text) && string.IsNullOrEmpty(txtConditionOfItems.Text))
                    {
                        btnUpdate.Enabled = false; // Disable the update button
                    }
                    else
                    {
                        btnUpdate.Enabled = true; // Enable the update button
                    }
                }
                else
                {
                    MessageBox.Show("Invalid row or column index.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in cell click event: {ex.Message}");
            }
        }






        

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadReturnTrends();

        }
    }
}
