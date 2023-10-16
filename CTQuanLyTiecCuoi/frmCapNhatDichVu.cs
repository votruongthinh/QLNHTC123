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
    public partial class frmCapNhatDichVu : Form
    {
        public frmCapNhatDichVu()
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
        private void frmCapNhatDichVu_Load(object sender, EventArgs e)
        {
            OpenConnection();
            NapDichVu();
        }
        private void LamTrong()
        {
            txtDonGia.Text = "";
            txtTenDichVu.Text = "";
        }
        private void NapDichVu()
        {

            dgvDichVu.Columns.Clear();
            string query = "select * from DICHVU";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dgvDichVu.DataSource = ds.Tables[0];
            dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void txtTenMonAn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgvDichVu.Rows[rowIndex];
                txtTenDichVu.Text = dgvDichVu.Rows[rowIndex].Cells[1].Value.ToString();
                txtDonGia.Text = dgvDichVu.Rows[rowIndex].Cells[2].Value.ToString();
            }
            catch { return; }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenDichVu.Text == "" || txtDonGia.Text == "")
            {
                MessageBox.Show("Thông tin trống!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from DICHVU";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ten = reader.GetString(1);
                if (ten == txtTenDichVu.Text)
                {
                    MessageBox.Show("Tên dịch vụ đã tồn tại!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;

                }
            }
            reader.Close();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ThemDichVu";
            command.Connection = connection;
            command.Parameters.Add("@TenDichVu", SqlDbType.NVarChar).Value = txtTenDichVu.Text;
            command.Parameters.Add("@DonGia", SqlDbType.Int).Value = int.Parse(txtDonGia.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Thêm thành công!");
            LamTrong();
            NapDichVu();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDichVu.SelectedRows.Count == 1)
            {
                if (txtTenDichVu.Text == "" || txtDonGia.Text == "")
                {
                    MessageBox.Show("Thông tin trống!", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SuaDichVu";
                command.Connection = connection;
                command.Parameters.Add("@IDDichVu", SqlDbType.Int).Value = dgvDichVu.SelectedRows[0].Cells[0].Value;
                command.Parameters.Add("@TenDichVu", SqlDbType.NVarChar).Value = txtTenDichVu.Text;
                command.Parameters.Add("@DonGia", SqlDbType.Int).Value = int.Parse(txtDonGia.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Sửa thành công!");
                LamTrong();
                NapDichVu();
            }
            else
            {
                MessageBox.Show("Mời chọn một dịch vụ!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDichVu.SelectedRows.Count == 1)
            {

                DialogResult result = MessageBox.Show("Bạn muốn xóa?",
                    "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
                try
                {
                    string query = "delete from DICHVU where IDDichVu=" + dgvDichVu.SelectedRows[0].Cells[0].Value;
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Connection = connection;
                    int kq = command.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa!");
                    LamTrong();
                    NapDichVu();
                }
                catch
                {
                    MessageBox.Show("Dịch vụ đã được đặt, không thể xóa!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LamTrong();
                }
            }
            else
            {
                MessageBox.Show("Mời chọn một dich vụ!");
            }
        }

        private void frmCapNhatDichVu_FormClosing(object sender, FormClosingEventArgs e)
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
