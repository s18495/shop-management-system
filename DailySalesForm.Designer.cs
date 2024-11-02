namespace CafeShopManagementSystem
{
    partial class DailySalesForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.DailySalesPrintBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.salesGrid = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.salesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.DailySalesPrintBtn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.datePicker);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.salesGrid);
            this.panel1.Location = new System.Drawing.Point(7, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1235, 729);
            this.panel1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(789, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(192, 49);
            this.button2.TabIndex = 32;
            this.button2.Text = "RELOAD";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DailySalesPrintBtn
            // 
            this.DailySalesPrintBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            this.DailySalesPrintBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DailySalesPrintBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DailySalesPrintBtn.ForeColor = System.Drawing.Color.White;
            this.DailySalesPrintBtn.Location = new System.Drawing.Point(987, 33);
            this.DailySalesPrintBtn.Name = "DailySalesPrintBtn";
            this.DailySalesPrintBtn.Size = new System.Drawing.Size(212, 49);
            this.DailySalesPrintBtn.TabIndex = 31;
            this.DailySalesPrintBtn.Text = "PRINT";
            this.DailySalesPrintBtn.UseVisualStyleBackColor = false;
            this.DailySalesPrintBtn.Click += new System.EventHandler(this.DailySalesPrintBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Date";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(119, 62);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(200, 20);
            this.datePicker.TabIndex = 4;
            this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Daily Sales Report";
            // 
            // salesGrid
            // 
            this.salesGrid.AllowUserToAddRows = false;
            this.salesGrid.AllowUserToDeleteRows = false;
            this.salesGrid.AllowUserToResizeColumns = false;
            this.salesGrid.AllowUserToResizeRows = false;
            this.salesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.salesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(99)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.salesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.salesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.salesGrid.EnableHeadersVisualStyles = false;
            this.salesGrid.Location = new System.Drawing.Point(16, 101);
            this.salesGrid.Name = "salesGrid";
            this.salesGrid.ReadOnly = true;
            this.salesGrid.RowHeadersVisible = false;
            this.salesGrid.Size = new System.Drawing.Size(1183, 542);
            this.salesGrid.TabIndex = 2;
            this.salesGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridview1_CellContentClick);
            // 
            // DailySalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "DailySalesForm";
            this.Size = new System.Drawing.Size(1251, 745);
            this.Load += new System.EventHandler(this.DailySalesForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.salesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView salesGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Button DailySalesPrintBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button exchangeButton;
        private System.Windows.Forms.Button refundButton;
    }
}
