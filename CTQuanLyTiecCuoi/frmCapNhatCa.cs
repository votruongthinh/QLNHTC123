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
using System.Configuration;

namespace CTQuanLyTiecCuoi
{
    public partial class frmCapNhatCa : Form
    {
        public frmCapNhatCa()
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
        private void frmCapNhatCa_Load(object sender, EventArgs e)
        {
            OpenConnection();
            NapCa();
        }
        private void LamTrong()
        {
            txtTenCa.Text = "";
        }
        private void NapCa()
        {
            dgvCa.Columns.Clear();
            string query = "select * from ThongTinCa";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dgvCa.DataSource = ds.Tables[0];
            dgvCa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenCa.Text == "")
            {
                MessageBox.Show("Thông tin trống!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string query = "insert into ThongTinCa (TenCa) values (@TenCa)";
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from ThongTinCa";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ten = reader.GetString(1);
                if (ten == txtTenCa.Text)
                {
                    MessageBox.Show("Tên ca đã tồn tại!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;

                }
            }
            reader.Close();
            command.CommandText = query;
            command.Connection = connection;
            command.Parameters.Add("@TenCa", SqlDbType.NVarChar).Value = txtTenCa.Text;
            command.ExecuteNonQuery();
            MessageBox.Show("Thêm thành công!");
            LamTrong();
            NapCa();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvCa.SelectedRows.Count == 1)
            {
                if (txtTenCa.Text == "")
                {
                    MessageBox.Show("Thông tin trống!", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = "update ThongTinCa set TenCa=@TenCa where MaCa=@MaCa";
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.Add("@TenCa", SqlDbType.NVarChar).Value = txtTenCa.Text;
                command.Parameters.Add("@MaCa", SqlDbType.Int).Value = dgvCa.SelectedRows[0].Cells[0].Value;
                int kq = command.ExecuteNonQuery();
                if (kq == 1)
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
                NapCa();
            }
            else
            {
                MessageBox.Show("Mời chọn ca!");
            }
        }

        private void dgvCa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgvCa.Rows[rowIndex];
                txtTenCa.Text = dgvCa.Rows[rowIndex].Cells[1].Value.ToString();
            }
            catch { return; }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (dgvCa.SelectedRows.Count == 1)
            {

                DialogResult result = MessageBox.Show("Bạn muốn xóa?",
                    "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
                try
                {
                    string query = "delete from ThongTinCa where MaCa=" + dgvCa.SelectedRows[0].Cells[0].Value;
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Connection = connection;
                    int kq = command.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa!");
                    LamTrong();
                    NapCa();
                }
                catch
                {
                    MessageBox.Show("Loại ca đã được sử dụng, không thể xóa!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LamTrong();
                }
            }
            else
            {
                MessageBox.Show("Mời chọn một loại sảnh!");
            }
        }

        private void frmCapNhatCa_FormClosing(object sender, FormClosingEventArgs e)
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
