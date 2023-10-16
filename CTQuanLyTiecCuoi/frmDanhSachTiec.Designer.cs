namespace CTQuanLyTiecCuoi
{
    partial class frmDanhSachTiec
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimSdt = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDsTiecCuoi = new System.Windows.Forms.DataGridView();
            this.btnSuaTiec = new System.Windows.Forms.Button();
            this.btnXoaTiec = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDsTiecCuoi)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTimSdt);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(774, 97);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(239, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(359, 33);
            this.label2.TabIndex = 5;
            this.label2.Text = "DANH SÁCH TIỆC CƯỚI";
            // 
            // txtTimSdt
            // 
            this.txtTimSdt.Location = new System.Drawing.Point(320, 58);
            this.txtTimSdt.Name = "txtTimSdt";
            this.txtTimSdt.Size = new System.Drawing.Size(196, 26);
            this.txtTimSdt.TabIndex = 0;
            this.txtTimSdt.TextChanged += new System.EventHandler(this.txtTimSdt_TextChanged);
            this.txtTimSdt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimSdt_KeyPress);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(597, 53);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(108, 32);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập số điện thoại khách hàng:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(774, 200);
            this.panel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvDsTiecCuoi);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách tiệc cưới";
            // 
            // dgvDsTiecCuoi
            // 
            this.dgvDsTiecCuoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDsTiecCuoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDsTiecCuoi.Location = new System.Drawing.Point(3, 22);
            this.dgvDsTiecCuoi.Name = "dgvDsTiecCuoi";
            this.dgvDsTiecCuoi.ReadOnly = true;
            this.dgvDsTiecCuoi.RowHeadersWidth = 51;
            this.dgvDsTiecCuoi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDsTiecCuoi.Size = new System.Drawing.Size(768, 175);
            this.dgvDsTiecCuoi.TabIndex = 0;
            this.dgvDsTiecCuoi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDsTiecCuoi_CellContentClick);
            // 
            // btnSuaTiec
            // 
            this.btnSuaTiec.Location = new System.Drawing.Point(546, 308);
            this.btnSuaTiec.Name = "btnSuaTiec";
            this.btnSuaTiec.Size = new System.Drawing.Size(79, 33);
            this.btnSuaTiec.TabIndex = 1;
            this.btnSuaTiec.Text = "Sửa";
            this.btnSuaTiec.UseVisualStyleBackColor = true;
            this.btnSuaTiec.Click += new System.EventHandler(this.btnSuaTiec_Click);
            // 
            // btnXoaTiec
            // 
            this.btnXoaTiec.Location = new System.Drawing.Point(665, 308);
            this.btnXoaTiec.Name = "btnXoaTiec";
            this.btnXoaTiec.Size = new System.Drawing.Size(75, 33);
            this.btnXoaTiec.TabIndex = 2;
            this.btnXoaTiec.Text = "Xóa";
            this.btnXoaTiec.UseVisualStyleBackColor = true;
            this.btnXoaTiec.Click += new System.EventHandler(this.btnXoaTiec_Click);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.Red;
            this.btnThanhToan.Location = new System.Drawing.Point(35, 308);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(149, 33);
            this.btnThanhToan.TabIndex = 0;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // frmDanhSachTiec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 350);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.btnXoaTiec);
            this.Controls.Add(this.btnSuaTiec);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmDanhSachTiec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách tiêc cưới";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDanhSachTiec_FormClosing);
            this.Load += new System.EventHandler(this.frmDanhSachTiec_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDsTiecCuoi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtTimSdt;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSuaTiec;
        private System.Windows.Forms.Button btnXoaTiec;
        private System.Windows.Forms.Button btnThanhToan;
        public System.Windows.Forms.DataGridView dgvDsTiecCuoi;
        private System.Windows.Forms.Label label2;
    }
}