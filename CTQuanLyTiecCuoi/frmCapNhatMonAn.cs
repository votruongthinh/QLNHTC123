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
    public partial class frmCapNhatMonAn : Form
    {
        public frmCapNhatMonAn()
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
        private void frmCapNhatMonAn_Load(object sender, EventArgs e)
        {
            OpenConnection();
            NapLoaiMonAn();
            NapMonAn();
        }
        private void LamTrong()
        {
            txtDonGia.Text = "";
            txtGhiChu.Text = "";
            txtTenMonAn.Text = "";
            cbbLoaiMonAn.Text = "";
        }
        private void NapLoaiMonAn()
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from LOAIMONAN";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            cbbLoaiMonAn.Items.Clear();
            while (reader.Read())
            {
                cbbLoaiMonAn.Items.Add(reader.GetString(0));
            }
            reader.Close();
        }

        private void NapMonAn()
        {
            dgvMonAn.Columns.Clear();
            string query = "select * from THUCDON";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dgvMonAn.DataSource = ds.Tables[0];
            dgvMonAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgvMonAn.Rows[rowIndex];
                txtTenMonAn.Text = dgvMonAn.Rows[rowIndex].Cells[1].Value.ToString();
                txtDonGia.Text = dgvMonAn.Rows[rowIndex].Cells[2].Value.ToString();
                cbbLoaiMonAn.Text = dgvMonAn.Rows[rowIndex].Cells[3].Value.ToString();
                try { txtGhiChu.Text = dgvMonAn.Rows[rowIndex].Cells[4].Value.ToString(); } catch { }
            }
            catch { return; }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenMonAn.Text == "" || txtDonGia.Text == "" || cbbLoaiMonAn.Text=="")
            {
                MessageBox.Show("Thông tin trống!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from THUCDON";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ten = reader.GetString(1);
                if (ten == txtTenMonAn.Text)
                {
                    MessageBox.Show("Tên món ăn đã tồn tại!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;

                }
            }
            reader.Close();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ThemMonAn";
            command.Connection = connection;
            command.Parameters.Add("@TenMonAn", SqlDbType.NVarChar).Value = txtTenMonAn.Text;
            command.Parameters.Add("@DonGia", SqlDbType.Int).Value = int.Parse(txtDonGia.Text);
            command.Parameters.Add("@LoaiMonAn", SqlDbType.NChar).Value = cbbLoaiMonAn.Text;
            try { command.Parameters.Add("@GhiChu", SqlDbType.NChar).Value = txtGhiChu.Text; } catch { }
            command.ExecuteNonQuery();
            MessageBox.Show("Thêm thành công!");
            LamTrong();
            NapMonAn();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvMonAn.SelectedRows.Count == 1)
            {
                if (txtTenMonAn.Text == "" || txtDonGia.Text == "" || cbbLoaiMonAn.Text == "")
                {
                    MessageBox.Show("Thông tin trống!", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SuaMonAn";
                command.Connection = connection;
                command.Parameters.Add("@IDMonAN", SqlDbType.Int).Value = dgvMonAn.SelectedRows[0].Cells[0].Value;
                command.Parameters.Add("@TenMonAn", SqlDbType.NVarChar).Value = txtTenMonAn.Text;
                command.Parameters.Add("@DonGia", SqlDbType.Int).Value = int.Parse(txtDonGia.Text);
                command.Parameters.Add("@LoaiMonAn", SqlDbType.NChar).Value = cbbLoaiMonAn.Text;
                try { command.Parameters.Add("@GhiChu", SqlDbType.NChar).Value = txtGhiChu.Text; } catch { }
                command.ExecuteNonQuery();
                MessageBox.Show("Sửa thành công!");
                LamTrong();
                NapMonAn();
            }
            else
            {
                MessageBox.Show("Mời chọn món ăn!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvMonAn.SelectedRows.Count == 1)
            {

                DialogResult result = MessageBox.Show("Bạn muốn xóa?",
                    "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
                try
                {
                    string query = "delete from THUCDON where IDMonAn=" + dgvMonAn.SelectedRows[0].Cells[0].Value;
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Connection = connection;
                    int kq = command.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa!");
                    LamTrong();
                    NapMonAn();
                }
                catch
                {
                    MessageBox.Show("Món ăn đã được đặt, không thể xóa!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LamTrong();
                }
            }
            else
            {
                MessageBox.Show("Mời chọn một món ăn!");
            }
        }

        private void frmCapNhatMonAn_FormClosing(object sender, FormClosingEventArgs e)
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
