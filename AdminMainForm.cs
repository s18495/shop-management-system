using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class AdminMainForm : Form
    {
        private DailySalesForm dailySales1;
        //private PromotionsAndDiscounts promotionsAndDiscounts;
        //private CashierOrderForm cashierOrderForm2;
        //private CashierOrderForm cashierOrderForm2;



        public AdminMainForm()
        {
            InitializeComponent();
            //InitializeControls();
            


        }

        


        public void displayUsername()
        {
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to Sign out?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(check == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();

                this.Hide();
            }
        }

        
        
        private void button1_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = true;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = false;

            shop1.Visible = false;


            AdminDashboardForm adForm = adminDashboardForm1 as AdminDashboardForm;

            if(adForm != null)
            {
                adForm.refreshData();
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = true;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            grn1.Visible = false;
            shop1.Visible = false;

            refundsAndExchanges2.Visible = false;


            AdminAddUsers aaUsers = adminAddUsers1 as AdminAddUsers;

            if (aaUsers != null)
            {
                aaUsers.refreshData();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = true;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            grn1.Visible = false;
            shop1.Visible = false;

            refundsAndExchanges2.Visible = false;


            AdminAddProducts aaProd = adminAddProducts1 as AdminAddProducts;

            if (aaProd != null)
            {
                aaProd.refreshData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = true;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = false;
            shop1.Visible = false;


            CashierCustomersForm ccForm = cashierCustomersForm1 as CashierCustomersForm;

            if (ccForm != null)
            {
                ccForm.refreshData();
            }
        }

        private void order_btn_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = true;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = false;
            shop1.Visible = false;


            CashierOrderForm coForm = cashierOrderForm1 as CashierOrderForm;
            //CashierOrderForm coForm = new CashierOrderForm(promotionsAndDiscounts); // Pass promotionsAndDiscounts here

            if (coForm != null)
            {
                coForm.refreshData();
                coForm.displayAvailableProds();
                coForm.LoadAvailableProducts();

                // Call the ReloadData method to refresh the table data

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void adminDashboardForm1_Load(object sender, EventArgs e)
        {

        }



        private void button5_Click_1(object sender, EventArgs e)
        {
            // Ensure all controls are initialized
            if (adminDashboardForm1 == null || adminAddUsers1 == null || adminAddProducts1 == null || cashierCustomersForm1 == null || dailySalesForm1 == null)
            {
                MessageBox.Show("One or more controls are not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set visibility of controls
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = true;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = false;
            shop1.Visible = false;


            // Safely cast dailySales1 to DailySalesForm
            DailySalesForm dsForm = dailySalesForm1 as DailySalesForm;
            if (dsForm == null)
            {
                MessageBox.Show("dailySales1 is not a DailySalesForm.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Now you can safely use dsForm
            // Example: dsForm.SomeMethod();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = true;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = false;
            shop1.Visible = false;


            MonthlySalesForm msForm = monthlySalesForm1 as MonthlySalesForm;

            if (msForm != null)
            {
                msForm.refreshData();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void adminDashboardForm1_Load_1(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = true;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = false;
            shop1.Visible = false;



            AddProductCategories adForm = AddProductCategories1 as AddProductCategories;

            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = true;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = false;
            shop1.Visible = false;


            InventoryReplenishment adForm = inventoryReplenishment1 as InventoryReplenishment;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = true;
            grn1.Visible = false;
            shop1.Visible = false;


            RefundsAndExchanges adForm = RefundsAndExchanges1 as RefundsAndExchanges;
        }

        private void btnDiscounts_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = true;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = false;
            shop1.Visible = false;


            PromotionsAndDiscounts adForm = PromotionsAndDiscounts1 as PromotionsAndDiscounts;
        }

        private void btnGRN_Click(object sender, EventArgs e)
        {
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = true;
            shop1.Visible = false;


            GRN adForm = grn1 as GRN;
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            shop1.Visible = true;
            addProductCategories2.Visible = false;
            adminDashboardForm1.Visible = false;
            adminAddUsers1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashierOrderForm1.Visible = false;
            dailySalesForm1.Visible = false;
            inventoryReplenishment1.Visible = false;
            monthlySalesForm1.Visible = false;
            promotionsAndDiscounts2.Visible = false;
            refundsAndExchanges2.Visible = false;
            grn1.Visible = false;

            Shop adForm = shop1 as Shop;


        }
    }
}
