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
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void txtTenDichVu_TextChanged(object sender, EventArgs e)
        {

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
        private void frmUser_Load(object sender, EventArgs e)
        {
            OpenConnection();
            NapUser();
        }
        private void LamTrong()
        {
            txtMatKhau.Text = "";
            txtTenTaiKhoan.Text = "";
            txtXacNhanMK.Text = "";
        }
        private void NapUser()
        {
            dgvUser.Columns.Clear();
            string query = "select * from DANGNHAP";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dgvUser.DataSource = ds.Tables[0];
            dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUser.Columns[2].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenTaiKhoan.Text == "" || txtMatKhau.Text == "" || txtXacNhanMK.Text=="")
            {
                MessageBox.Show("Thông tin trống!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtMatKhau.Text != txtXacNhanMK.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không trùng khớp!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from DANGNHAP";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                string ten = reader.GetString(1);
                string mk = reader.GetString(2);
                if (ten == txtTenTaiKhoan.Text)
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;

                }
            }
            reader.Close();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ThemTaiKhoan";
            command.Connection = connection;
            command.Parameters.Add("@TaiKhoan", SqlDbType.NVarChar).Value = txtTenTaiKhoan.Text;
            command.Parameters.Add("@MatKhau", SqlDbType.NVarChar).Value = txtMatKhau.Text;
            command.ExecuteNonQuery();
            MessageBox.Show("Thêm thành công!");
            
            LamTrong();
            NapUser();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgvUser.Rows[rowIndex];
                txtTenTaiKhoan.Text = dgvUser.Rows[rowIndex].Cells[1].Value.ToString();
                //txtMatKhau.Text = dgvUser.Rows[rowIndex].Cells[2].Value.ToString();
            }
            catch { return; }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count == 1)
            {
                if(txtMatKhau.Text!=dgvUser.SelectedRows[0].Cells[2].Value.ToString())
                {
                    MessageBox.Show("Nhập đúng mật khẩu trước khi xóa!", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DialogResult result = MessageBox.Show("Bạn muốn xóa?",
                    "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
                if(dgvUser.SelectedRows[0].Cells[0].Value.ToString()!=frmDangNhap.IDTaiKhoan.ToString())
                {
                    string query = "delete from DANGNHAP where IDTaiKhoan=" + dgvUser.SelectedRows[0].Cells[0].Value;
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Connection = connection;
                    int kq = command.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa!");
                    LamTrong();
                    NapUser();
                }
                else
                {
                    MessageBox.Show("Tài khoản đang sử dụng, không thể xóa!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LamTrong();
                }
            }
            else
            {
                MessageBox.Show("Mời chọn một User!");
            }
        }
    }
}
