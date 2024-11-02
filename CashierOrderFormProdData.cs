using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CafeShopManagementSystem
{
    class CashierOrderFormProdData
    {
        public int ID { set; get; } // 0
        public string ProductID { set; get; } // 1
        public string ProductName { set; get; } // 2
       // public string Type { set; get; } // 3
        public string Stock { set; get; } // 4
        public string Price { set; get; } // 5
        public string Status { set; get; } // 6

        public string CategoryName { get; set; } // New property for category name
        public int CategoryID { get; set; } // Add CategoryID

        public int shop_id { get; set; }


        
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public List<CashierOrderFormProdData> availableProductsData()
        {
            List<CashierOrderFormProdData> listData = new List<CashierOrderFormProdData>();

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = @"
                    SELECT p.product_id, p.prod_id, p.prod_name,  p.prod_stock, p.prod_price, p.prod_status, p.date_insert, p.date_update,p.shop_id, c.CategoryID, c.CategoryName
                    FROM products p
                    JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE p.date_delete IS NULL AND p.prod_stock > 0  AND p.prod_status = @Status";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@Status", "Available");

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CashierOrderFormProdData apd = new CashierOrderFormProdData();

                            apd.ID = (int)reader["product_id"];
                            apd.ProductID = reader["prod_id"].ToString();
                            apd.ProductName = reader["prod_name"].ToString();
                            //apd.Type = reader["prod_type"].ToString();
                            apd.Stock = reader["prod_stock"].ToString();
                            apd.Price = reader["prod_price"].ToString();
                            apd.Status = reader["prod_status"].ToString();
                            apd.CategoryName = reader["CategoryName"].ToString(); // Set category name
                            apd.CategoryID = (int)reader["CategoryID"]; // Ensure CategoryID is correctly cast
                            apd.shop_id = (int)reader["shop_id"];


                            listData.Add(apd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed Connection: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }



        public int UpdateStock(string productId, int newStock)
        {
            int updatedStock = 0;
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string updateQuery = "UPDATE products SET prod_stock = prod_stock-@newStock WHERE prod_id = @productId";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@newStock", newStock);
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.ExecuteNonQuery();
                    }

                    // Fetch the updated stock value
                    string selectQuery = "SELECT prod_stock FROM products WHERE prod_id = @productId";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@productId", productId);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            updatedStock = Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to update stock: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                }
            }
            return updatedStock;
        }
    }
}
