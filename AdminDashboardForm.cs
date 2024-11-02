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
using System.Configuration;

namespace CafeShopManagementSystem
{
    public partial class AdminDashboardForm : UserControl
    {
        //static string conn = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;
        //SqlConnection connect = new SqlConnection(conn);
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");


        public AdminDashboardForm()
        {
            InitializeComponent();

            displayTotalCashier();
            displayTotalCustomers();
            displayTotalIncome();
            displayTodaysIncome();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayTotalCashier();
            displayTotalCustomers();
            displayTotalIncome();
            displayTodaysIncome();
        }

        public void displayTotalCashier()
        {
            if(connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT COUNT(id) FROM users WHERE role = @role AND status = @status";

                    using(SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@role", "Cashier");
                        cmd.Parameters.AddWithValue("@status", "Active");

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            dashboard_TC.Text = count.ToString();
                        }

                        reader.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTotalCustomers()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT COUNT(id) FROM customers";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            dashboard_TCust.Text = count.ToString();
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTodaysIncome()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT sum(price_after_discount) FROM customers WHERE DATE = @date";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        DateTime today = DateTime.Today;
                        string getToday = today.ToString("yyyy-MM-dd");

                        cmd.Parameters.AddWithValue("@date", getToday);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            object value = reader[0];

                            if (value != DBNull.Value)
                            {
                                float count = Convert.ToSingle(reader[0]);
                                dashboard_TI.Text = "Rs" + count.ToString("0.00");
                            }
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTotalIncome()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT SUM(price_after_discount) FROM customers";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            float count = Convert.ToSingle(reader[0]);
                            dashboard_TIn.Text = "Rs" + count.ToString("0.00");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dashboard_TIn_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
