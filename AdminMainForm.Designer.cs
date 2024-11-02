
namespace CafeShopManagementSystem
{
    partial class AdminMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnShop = new System.Windows.Forms.Button();
            this.btnGRN = new System.Windows.Forms.Button();
            this.btnDiscounts = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.order_btn = new System.Windows.Forms.Button();
            this.logout_btn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.adminDashboardForm1 = new CafeShopManagementSystem.AdminDashboardForm();
            this.adminAddUsers1 = new CafeShopManagementSystem.AdminAddUsers();
            this.adminAddProducts1 = new CafeShopManagementSystem.AdminAddProducts();
            this.addProductCategories2 = new CafeShopManagementSystem.AddProductCategories();
            this.cashierCustomersForm1 = new CafeShopManagementSystem.CashierCustomersForm();
            this.cashierOrderForm1 = new CafeShopManagementSystem.CashierOrderForm();
            this.dailySalesForm1 = new CafeShopManagementSystem.DailySalesForm();
            this.monthlySalesForm1 = new CafeShopManagementSystem.MonthlySalesForm();
            this.inventoryReplenishment1 = new CafeShopManagementSystem.InventoryReplenishment();
            this.refundsAndExchanges2 = new CafeShopManagementSystem.RefundsAndExchanges();
            this.promotionsAndDiscounts2 = new CafeShopManagementSystem.PromotionsAndDiscounts();
            this.grn1 = new CafeShopManagementSystem.GRN();
            this.shop1 = new CafeShopManagementSystem.Shop();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1500, 45);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Shop Management System";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // close
            // 
            this.close.AutoSize = true;
            this.close.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Location = new System.Drawing.Point(1474, 10);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(18, 18);
            this.close.TabIndex = 2;
            this.close.Text = "X";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.panel2.Controls.Add(this.btnShop);
            this.panel2.Controls.Add(this.btnGRN);
            this.panel2.Controls.Add(this.btnDiscounts);
            this.panel2.Controls.Add(this.button9);
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.order_btn);
            this.panel2.Controls.Add(this.logout_btn);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 745);
            this.panel2.TabIndex = 1;
            // 
            // btnShop
            // 
            this.btnShop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.btnShop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShop.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShop.ForeColor = System.Drawing.Color.White;
            this.btnShop.Location = new System.Drawing.Point(13, 658);
            this.btnShop.Name = "btnShop";
            this.btnShop.Size = new System.Drawing.Size(224, 39);
            this.btnShop.TabIndex = 26;
            this.btnShop.Text = "Shop";
            this.btnShop.UseVisualStyleBackColor = false;
            this.btnShop.Click += new System.EventHandler(this.btnShop_Click);
            // 
            // btnGRN
            // 
            this.btnGRN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.btnGRN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGRN.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGRN.ForeColor = System.Drawing.Color.White;
            this.btnGRN.Location = new System.Drawing.Point(13, 613);
            this.btnGRN.Name = "btnGRN";
            this.btnGRN.Size = new System.Drawing.Size(224, 39);
            this.btnGRN.TabIndex = 25;
            this.btnGRN.Text = "GRN";
            this.btnGRN.UseVisualStyleBackColor = false;
            this.btnGRN.Click += new System.EventHandler(this.btnGRN_Click);
            // 
            // btnDiscounts
            // 
            this.btnDiscounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.btnDiscounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiscounts.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiscounts.ForeColor = System.Drawing.Color.White;
            this.btnDiscounts.Location = new System.Drawing.Point(13, 568);
            this.btnDiscounts.Name = "btnDiscounts";
            this.btnDiscounts.Size = new System.Drawing.Size(224, 39);
            this.btnDiscounts.TabIndex = 24;
            this.btnDiscounts.Text = "Discounts";
            this.btnDiscounts.UseVisualStyleBackColor = false;
            this.btnDiscounts.Click += new System.EventHandler(this.btnDiscounts_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(13, 433);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(224, 39);
            this.button9.TabIndex = 23;
            this.button9.Text = "Monthly Sales";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(13, 253);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(224, 39);
            this.button8.TabIndex = 22;
            this.button8.Text = "Add Category";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(13, 523);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(224, 39);
            this.button7.TabIndex = 21;
            this.button7.Text = "Exchanges";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(13, 478);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(224, 39);
            this.button6.TabIndex = 20;
            this.button6.Text = "Inventory";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(13, 388);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(224, 39);
            this.button5.TabIndex = 19;
            this.button5.Text = "Daily Sales";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // order_btn
            // 
            this.order_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.order_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.order_btn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.order_btn.ForeColor = System.Drawing.Color.White;
            this.order_btn.Location = new System.Drawing.Point(13, 343);
            this.order_btn.Name = "order_btn";
            this.order_btn.Size = new System.Drawing.Size(224, 39);
            this.order_btn.TabIndex = 18;
            this.order_btn.Text = "Sales";
            this.order_btn.UseVisualStyleBackColor = false;
            this.order_btn.Click += new System.EventHandler(this.order_btn_Click);
            // 
            // logout_btn
            // 
            this.logout_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.logout_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logout_btn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout_btn.ForeColor = System.Drawing.Color.White;
            this.logout_btn.Location = new System.Drawing.Point(12, 703);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(224, 39);
            this.logout_btn.TabIndex = 17;
            this.logout_btn.Text = "Logout";
            this.logout_btn.UseVisualStyleBackColor = false;
            this.logout_btn.Click += new System.EventHandler(this.logout_btn_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(13, 298);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(224, 39);
            this.button3.TabIndex = 16;
            this.button3.Text = "Customers";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(13, 208);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(224, 39);
            this.button4.TabIndex = 15;
            this.button4.Text = "Add Products";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(13, 163);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(224, 39);
            this.button2.TabIndex = 14;
            this.button2.Text = "Add User";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(13, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 39);
            this.button1.TabIndex = 13;
            this.button1.Text = "Dashboard";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(94, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Admin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(50, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Admin\'s Portal";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.adminDashboardForm1);
            this.panel3.Controls.Add(this.adminAddUsers1);
            this.panel3.Controls.Add(this.adminAddProducts1);
            this.panel3.Controls.Add(this.addProductCategories2);
            this.panel3.Controls.Add(this.cashierCustomersForm1);
            this.panel3.Controls.Add(this.cashierOrderForm1);
            this.panel3.Controls.Add(this.dailySalesForm1);
            this.panel3.Controls.Add(this.monthlySalesForm1);
            this.panel3.Controls.Add(this.inventoryReplenishment1);
            this.panel3.Controls.Add(this.refundsAndExchanges2);
            this.panel3.Controls.Add(this.promotionsAndDiscounts2);
            this.panel3.Controls.Add(this.grn1);
            this.panel3.Controls.Add(this.shop1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(249, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1251, 745);
            this.panel3.TabIndex = 2;
            // 
            // adminDashboardForm1
            // 
            this.adminDashboardForm1.Location = new System.Drawing.Point(0, 0);
            this.adminDashboardForm1.Name = "adminDashboardForm1";
            this.adminDashboardForm1.Size = new System.Drawing.Size(1251, 745);
            this.adminDashboardForm1.TabIndex = 12;
            // 
            // adminAddUsers1
            // 
            this.adminAddUsers1.Location = new System.Drawing.Point(0, 0);
            this.adminAddUsers1.Name = "adminAddUsers1";
            this.adminAddUsers1.Size = new System.Drawing.Size(1251, 745);
            this.adminAddUsers1.TabIndex = 11;
            // 
            // adminAddProducts1
            // 
            this.adminAddProducts1.Location = new System.Drawing.Point(0, 0);
            this.adminAddProducts1.Name = "adminAddProducts1";
            this.adminAddProducts1.Size = new System.Drawing.Size(1251, 745);
            this.adminAddProducts1.TabIndex = 10;
            // 
            // addProductCategories2
            // 
            this.addProductCategories2.Location = new System.Drawing.Point(0, 0);
            this.addProductCategories2.Name = "addProductCategories2";
            this.addProductCategories2.Size = new System.Drawing.Size(1251, 745);
            this.addProductCategories2.TabIndex = 9;
            // 
            // cashierCustomersForm1
            // 
            this.cashierCustomersForm1.Location = new System.Drawing.Point(0, 0);
            this.cashierCustomersForm1.Name = "cashierCustomersForm1";
            this.cashierCustomersForm1.Size = new System.Drawing.Size(1251, 745);
            this.cashierCustomersForm1.TabIndex = 8;
            // 
            // cashierOrderForm1
            // 
            this.cashierOrderForm1.Location = new System.Drawing.Point(0, 0);
            this.cashierOrderForm1.Name = "cashierOrderForm1";
            this.cashierOrderForm1.Size = new System.Drawing.Size(1251, 745);
            this.cashierOrderForm1.TabIndex = 7;
            // 
            // dailySalesForm1
            // 
            this.dailySalesForm1.Location = new System.Drawing.Point(0, 0);
            this.dailySalesForm1.Name = "dailySalesForm1";
            this.dailySalesForm1.Size = new System.Drawing.Size(1251, 745);
            this.dailySalesForm1.TabIndex = 6;
            // 
            // monthlySalesForm1
            // 
            this.monthlySalesForm1.Location = new System.Drawing.Point(0, 0);
            this.monthlySalesForm1.Name = "monthlySalesForm1";
            this.monthlySalesForm1.Size = new System.Drawing.Size(1235, 729);
            this.monthlySalesForm1.TabIndex = 5;
            // 
            // inventoryReplenishment1
            // 
            this.inventoryReplenishment1.Location = new System.Drawing.Point(0, 0);
            this.inventoryReplenishment1.Name = "inventoryReplenishment1";
            this.inventoryReplenishment1.Size = new System.Drawing.Size(1251, 745);
            this.inventoryReplenishment1.TabIndex = 4;
            // 
            // refundsAndExchanges2
            // 
            this.refundsAndExchanges2.Location = new System.Drawing.Point(0, 0);
            this.refundsAndExchanges2.Name = "refundsAndExchanges2";
            this.refundsAndExchanges2.Size = new System.Drawing.Size(1251, 745);
            this.refundsAndExchanges2.TabIndex = 3;
            // 
            // promotionsAndDiscounts2
            // 
            this.promotionsAndDiscounts2.Location = new System.Drawing.Point(0, 0);
            this.promotionsAndDiscounts2.Name = "promotionsAndDiscounts2";
            this.promotionsAndDiscounts2.Size = new System.Drawing.Size(1251, 745);
            this.promotionsAndDiscounts2.TabIndex = 2;
            // 
            // grn1
            // 
            this.grn1.Location = new System.Drawing.Point(0, 0);
            this.grn1.Name = "grn1";
            this.grn1.Size = new System.Drawing.Size(1251, 745);
            this.grn1.TabIndex = 1;
            // 
            // shop1
            // 
            this.shop1.Location = new System.Drawing.Point(0, 0);
            this.shop1.Name = "shop1";
            this.shop1.Size = new System.Drawing.Size(1251, 745);
            this.shop1.TabIndex = 0;
            // 
            // AdminMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 790);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminMainForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button logout_btn;
        private System.Windows.Forms.Button order_btn;
        //private CashierOrderForm cashierOrderForm1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button btnDiscounts;
        private System.Windows.Forms.Panel panel3;
        private AddProductCategories AddProductCategories1;
        private PromotionsAndDiscounts PromotionsAndDiscounts1;
        private RefundsAndExchanges RefundsAndExchanges1;
        private System.Windows.Forms.Button btnGRN;
        private System.Windows.Forms.Button btnShop;
        private AdminAddUsers adminAddUsers1;
        private AdminAddProducts adminAddProducts1;
        private AddProductCategories addProductCategories2;
        private CashierCustomersForm cashierCustomersForm1;
        private CashierOrderForm cashierOrderForm1;
        private DailySalesForm dailySalesForm1;
        private MonthlySalesForm monthlySalesForm1;
        private InventoryReplenishment inventoryReplenishment1;
        private RefundsAndExchanges refundsAndExchanges2;
        private PromotionsAndDiscounts promotionsAndDiscounts2;
        private GRN grn1;
        private Shop shop1;
        private AdminDashboardForm adminDashboardForm1;
    }
}