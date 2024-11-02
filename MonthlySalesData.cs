using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace CafeShopManagementSystem
{
    class MonthlySalesData
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public int ID { get; set; }
        public int CID { get; set; }
        public string ProdID { get; set; }
        public string ProdName { get; set; }
        public int Qty { get; set; }
        public string Price { get; set; }
        public string Cat_Name { get; set; }

        public string Discount { set; get; }

        public string Disc_Price { set; get; }

        public string TotalCost { set; get; }

        public int shop_id { get; set; }



        public List<MonthlySalesData> GetMonthlySalesDataByDate(DateTime selectedMonth)
        {
            List<MonthlySalesData> listData = new List<MonthlySalesData>();
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectOrderData = @"
                        SELECT o.id, o.customer_id, o.prod_id, o.prod_name, o.qty, o.prod_price,o.shop_id,  o.discount, o.price_after_discount, o.total_cost, c.CategoryName
                        FROM orders o
                        INNER JOIN products p ON o.prod_id = p.prod_id
                        INNER JOIN categories c ON p.CategoryID = c.CategoryID
                        WHERE YEAR(o.order_date) = @selectedYear 
                        AND MONTH(o.order_date) = @selectedMonth AND o.qty > 0";

                    using (SqlCommand getOrderData = new SqlCommand(selectOrderData, connect))
                    {
                        getOrderData.Parameters.AddWithValue("@selectedYear", selectedMonth.Year);
                        getOrderData.Parameters.AddWithValue("@selectedMonth", selectedMonth.Month);
                        SqlDataReader reader = getOrderData.ExecuteReader();

                        while (reader.Read())
                        {
                            MonthlySalesData msData = new MonthlySalesData
                            {
                                ID = (int)reader["id"],
                                CID = (int)reader["customer_id"],
                                ProdID = reader["prod_id"].ToString(),
                                ProdName = reader["prod_name"].ToString(),
                                Qty = (int)reader["qty"],
                                Price = reader["prod_price"].ToString(),
                                Cat_Name = reader["CategoryName"].ToString(),
                                Discount = reader["discount"].ToString(),
                                Disc_Price = reader["price_after_discount"].ToString(),
                                TotalCost = reader["total_cost"].ToString(),
                                shop_id = (int)reader["shop_id"]

                            };

                            listData.Add(msData);

                           // Console.WriteLine($"ID: {msData.ID}, Customer ID: {msData.CID}, Product ID: {msData.ProdID}, Product Name: {msData.ProdName}, Quantity: {msData.Qty}, Price: {msData.Price}");

                        }

                        // Debugging: Output retrieved data count
                       // Console.WriteLine($"Retrieved {listData.Count} rows for {selectedMonth:yyyy-MM}");

                        // Output to console for debugging
                        //listData.Add(msData); 

                        //Console.WriteLine($"Number of rows retrieved: {listData.Count}");
                        /*
                         foreach (var item in listData)
                        {
                            Console.WriteLine($"ID: {item.ID}, ProdName: {item.ProdName},  Price: {item.Price}");
                        }
                        */



                       reader.Close(); // Close reader
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed connection or query: " + ex.Message);
                }
                finally
                {
                    connect.Close(); // Close connection
                }
            }
            return listData;
        }
    }
    }
