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
    public partial class frmCapNhatLoaiSanh : Form
    {
        public frmCapNhatLoaiSanh()
        {
            InitializeComponent();
        }
        public SqlConnection connection = null;
        string strcnn = System.Configuration.ConfigurationManager.ConnectionStrings["qlnhtc"].ConnectionString;
        public void OpenConnection()
        {
            if (connection == null)
                connection = new SqlConnection(strcnn);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        private void frmCapNhatLoaiSanh_Load(object sender, EventArgs e)
        {
            OpenConnection();
            NapLoaiSanh();
        }
        private void LamTrong()
        {
            txtMaLoai.Text = "";
            txtDonGiaBantt.Text = "";
        }
        private void NapLoaiSanh()
        {
            dgvLoaiSanh.Columns.Clear();
            string query = "select * from LOAISANH";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dgvLoaiSanh.DataSource = ds.Tables[0];
            dgvLoaiSanh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void txtDonGiaBantt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvLoaiSanh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtDonGiaBantt.Text=="" || txtMaLoai.Text=="")
            {
                MessageBox.Show("Thông tin trống!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string query = "insert into LOAISANH values('" + txtMaLoai.Text + "'," + int.Parse(txtDonGiaBantt.Text) + ")";
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.Connection = connection;
                command.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công!");
                LamTrong();
            }
            catch
            {
                MessageBox.Show("Mã loại sảnh đã tồn tại!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            NapLoaiSanh();
        }

        private void dgvLoaiSanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgvLoaiSanh.Rows[rowIndex];
                txtMaLoai.Text = dgvLoaiSanh.Rows[rowIndex].Cells[0].Value.ToString();
                txtDonGiaBantt.Text = dgvLoaiSanh.Rows[rowIndex].Cells[1].Value.ToString();
            }
            catch { return; }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtDonGiaBantt.Text == "" || txtMaLoai.Text == "")
            {
                MessageBox.Show("Thông tin trống!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
                string query = "update LOAISANH set DonGiaBanTT=" + int.Parse(txtDonGiaBantt.Text) + " where MaLoaiSanh='" + txtMaLoai.Text+"'";
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.Connection = connection;
                int kq=command.ExecuteNonQuery();
            if(kq==1)
            {
                MessageBox.Show("Sửa thành công!");
                LamTrong();
            }
            else
            {
                MessageBox.Show("Không sửa được!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                LamTrong();
            }
            NapLoaiSanh();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLoaiSanh.SelectedRows.Count == 1)
            {

                DialogResult result = MessageBox.Show("Bạn muốn xóa?",
                    "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
                try
                {
                    string query = "delete from LOAISANH where MaLoaiSanh='" + dgvLoaiSanh.SelectedRows[0].Cells[0].Value.ToString() + "'";
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Connection = connection;
                    int kq = command.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa!");
                    NapLoaiSanh();
                    LamTrong();
                }
                catch
                {
                    MessageBox.Show("Loại sảnh đã được sử dụng, không thể xóa!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LamTrong();
                }
            }
            else
            {
                MessageBox.Show("Mời chọn một loại sảnh!");
            }
        }

        private void frmCapNhatLoaiSanh_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát?",
                "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else if (result == DialogResult.No)
                e.Cancel = true;
        }
    }
}
