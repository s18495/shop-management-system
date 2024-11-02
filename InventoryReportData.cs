using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CafeShopManagementSystem
{
    class InventoryReportData
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public string ProdID { set; get; }
        public string ProdName { set; get; }
        public int Stock { set; get; }

        public List<InventoryReportData> GetInventoryData()
        {
            List<InventoryReportData> listData = new List<InventoryReportData>();

            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }

            try
            {
                string selectInventoryData = "SELECT prod_id, prod_name, prod_stock FROM products";
                using (SqlCommand getInventoryData = new SqlCommand(selectInventoryData, connect))
                {
                    SqlDataReader reader = getInventoryData.ExecuteReader();

                    while (reader.Read())
                    {
                        InventoryReportData invData = new InventoryReportData
                        {
                            ProdID = reader["prod_id"].ToString(),
                            ProdName = reader["prod_name"].ToString(),
                            Stock = (int)reader["prod_stock"]
                        };

                        listData.Add(invData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed connection or data retrieval: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }

            return listData;
        }
    }
}
