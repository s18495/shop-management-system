using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CafeShopManagementSystem
{
    class AdminAddProductsData
    {
        public int ID { set; get; } // 0
        public string ProductID { set; get; } // 1
        public string ProductName { set; get; } // 2
        //public string Type { set; get; } // 3
        public string Stock { set; get; } // 4
        public string Price { set; get; } // 5

        public string Cost { set; get; }
        public string Status { set; get; } // 6
        public string DateInsert { set; get; } // 8
        public string DateUpdate { set; get; } // 9
        public string CategoryName { get; set; } // New property for category name
        public int CategoryID { get; set; } // Add CategoryID

        public int shop_id { get; set; } 



        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public List<AdminAddProductsData> productsListData()
        {
            List<AdminAddProductsData> listData = new List<AdminAddProductsData>();

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = @"
                    SELECT 
                        p.product_id, p.prod_id, p.prod_name, p.prod_stock, p.prod_price, 
                        p.prod_status, p.date_insert, p.date_update, p.cost, p.shop_id, 
                        ISNULL(c.CategoryID, 0) AS CategoryID, 
                        ISNULL(c.CategoryName, '') AS CategoryName
                    FROM 
                        products p
                    LEFT JOIN 
                        Categories c ON p.CategoryID = c.CategoryID
                    WHERE 
                        p.date_delete IS NULL
                    ";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AdminAddProductsData apd = new AdminAddProductsData();

                            // Map data from SqlDataReader to AdminAddProductsData object
                            apd.ID = (int)reader["product_id"];
                            apd.ProductID = reader["prod_id"].ToString();
                            apd.ProductName = reader["prod_name"].ToString();
                            apd.CategoryName = reader["CategoryName"].ToString(); // Set category name

                            apd.Stock = reader["prod_stock"].ToString();
                            apd.Price = reader["prod_price"].ToString();
                            apd.Cost = reader["cost"].ToString();
                            apd.Status = reader["prod_status"].ToString();
                            apd.DateInsert = reader["date_insert"].ToString();
                            apd.DateUpdate = reader["date_update"].ToString();
                            apd.CategoryID = (int)reader["CategoryID"]; // Ensure CategoryID is correctly cast
                            apd.shop_id = (int)reader["shop_id"];

                            listData.Add(apd); // Add AdminAddProductsData object to list
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed connection: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }

            return listData;
        }


        public List<AdminAddProductsData> availableProductsData()
        {
            List<AdminAddProductsData> listData = new List<AdminAddProductsData>();

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM products WHERE status = @stats";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@stats", "Available");

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AdminAddProductsData apd = new AdminAddProductsData();

                            apd.ID = (int)reader["product_id"];
                            apd.ProductID = reader["prod_id"].ToString();
                            apd.ProductName = reader["prod_name"].ToString();
                           // apd.Type = reader["prod_type"].ToString();
                            apd.Stock = reader["prod_stock"].ToString();
                            apd.Price = reader["prod_price"].ToString();
                            apd.Cost = reader["cost"].ToString();
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
    }
}
