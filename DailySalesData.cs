using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace CafeShopManagementSystem
{
    class DailySalesData
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");

        public int ID { set; get; }
        public int CID { set; get; }
        public string ProdID { set; get; }
        public string ProdName { set; get; }
        //public string ProdType { set; get; }
        public int Qty { set; get; }
        public string Price { set; get; }

        public string Discount { set; get; }

        public string Disc_Price { set; get; }

        public string TotalCost { set; get; }

        public int shop_id { set; get; }



        //public float TotalPrice { get; set; }


        public List<DailySalesData> GetDailySalesDataByDate(DateTime selectedDate)
        {
            List<DailySalesData> listData = new List<DailySalesData>();

            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }

            try
            {
                string selectOrderData = "SELECT * FROM orders WHERE CAST(order_date AS DATE) = @selectedDate AND qty > 0";
                using (SqlCommand getOrderData = new SqlCommand(selectOrderData, connect))
                {
                    getOrderData.Parameters.AddWithValue("@selectedDate", selectedDate.Date);
                    SqlDataReader reader = getOrderData.ExecuteReader();

                    while (reader.Read())
                    {
                        DailySalesData dsData = new DailySalesData
                        {
                            ID = (int)reader["id"],
                            CID = (int)reader["customer_id"],
                            ProdID = reader["prod_id"].ToString(),
                            ProdName = reader["prod_name"].ToString(),
                            Qty = (int)reader["qty"],
                            Price = reader["prod_price"].ToString(),
                            Discount = reader["discount"].ToString(),
                            Disc_Price = reader["price_after_discount"].ToString(),
                            TotalCost = reader["total_cost"].ToString(),
                            shop_id = (int)reader["shop_id"]

                        };

                        

                        listData.Add(dsData);
                    }
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

            return listData;
        }
    }
}