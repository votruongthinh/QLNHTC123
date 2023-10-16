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

namespace CTQuanLyTiecCuoi
{
    public partial class frmThemsanh : Form
    {
        public frmThemsanh()
        {
            InitializeComponent();
        }
        Form1 frm = new Form1();

        private void btnThem_Click(object sender, EventArgs e)
        { 
            try
            {
                int er = -1;
                errorProvider1.SetError(txtTenSanh, "");
                errorProvider1.SetError(cbbLoaiSanh, "");
                errorProvider1.SetError(txtSoBanTD, "");
                if (txtTenSanh.Text == "")
                {
                    errorProvider1.SetError(txtTenSanh, "Tên sảnh trống!");
                    er = 1;
                }
                if (cbbLoaiSanh.Text == "")
                {
                    errorProvider1.SetError(cbbLoaiSanh, "Loại sảnh trống!");
                    er = 1;
                }
                if (txtSoBanTD.Text == "")
                {
                    errorProvider1.SetError(txtSoBanTD, "Số bàn tối đa trống!");
                    er = 1;
                }
                frm.OpenConnection();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ThemSanh";
                command.Connection = frm.connection;
                command.Parameters.Add("@TenSanh", SqlDbType.NVarChar).Value = txtTenSanh.Text;
                command.Parameters.Add("@SoBanTD", SqlDbType.Int).Value = txtSoBanTD.Text;
                try { command.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = txtGhiChu.Text; } catch { }
                command.Parameters.Add("@MaLoaiSanh", SqlDbType.Char).Value = cbbLoaiSanh.Text;
                int kq = -1;
                if (er == -1)
                {
                    kq = command.ExecuteNonQuery();
                }
                if (kq > 0)
                {
                    MessageBox.Show("Thêm thành công");
                    DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Lỗi! Không thêm được");
            }
            catch
            {
                MessageBox.Show("Lỗi! Không thêm được");

            }
        }

        private void frmThemsanh_Load(object sender, EventArgs e)
        {
            frm.NapLoaiSanh(cbbLoaiSanh);
        }
    }
}
