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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<LoaiSanh> loaiSanhs = new List<LoaiSanh>();
        DataSet ds = null;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public SqlConnection connection = null;
        string strcnn = System.Configuration.ConfigurationManager.ConnectionStrings["qlnhtc"].ConnectionString;
        SqlDataAdapter adapter = null;
        private void Form1_Load(object sender, EventArgs e)
        { 
            HienThiDanhSachSanh();
            NapLoaiSanh(cbbSuaLoaiSanh);
        }
        public void OpenConnection()
        {
            if (connection == null)
                connection = new SqlConnection(strcnn);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        public void NapLoaiSanh(ComboBox cbb)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from LOAISANH";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            cbb.Items.Clear();
            loaiSanhs.Clear();
            while(reader.Read())
            {
                LoaiSanh ls = new LoaiSanh();
                ls.MaLoaiSanh = reader.GetString(0);
                ls.DonGiaBanTT = reader.GetInt32(1);
                cbb.Items.Add(ls.MaLoaiSanh);
                loaiSanhs.Add(ls);
            }
            reader.Close();
        }

        private void HienThiDanhSachSanh()
        {
            gvDsSanh.Columns.Clear();
            if (connection == null)
                connection = new SqlConnection(strcnn);
            adapter = new SqlDataAdapter("SELECT IDSANH,TENSANH,S.MALOAISANH,SOBANTD,DONGIABANTT,GHICHU FROM SANH S,LOAISANH LS WHERE S.MALOAISANH = LS.MALOAISANH", connection);
            ds = new DataSet();
            adapter.Fill(ds, "sanh");
            DataGridViewColumn col = new DataGridViewColumn();
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            col.HeaderText = "STT";
            col.Visible = true;
            col.Width = 40;
            col.CellTemplate = cell;
            gvDsSanh.Columns.Add(col);
            gvDsSanh.DataSource = ds.Tables["sanh"];
            gvDsSanh.Columns[1].HeaderText = "ID sảnh";
            gvDsSanh.Columns[2].HeaderText = "Tên sảnh";
            gvDsSanh.Columns[3].HeaderText = "Loại sảnh";
            gvDsSanh.Columns[4].HeaderText = "Số bàn tối đa";
            gvDsSanh.Columns[5].HeaderText = "Đơn giá bàn tối thiểu";
            gvDsSanh.Columns[5].Width = 170;
            gvDsSanh.Columns[4].Width = 160;
            gvDsSanh.Columns[6].HeaderText = "Ghi chú";
            gvDsSanh.Columns[1].Visible = false;
            for (int i = 0; i < gvDsSanh.Rows.Count -1; i++)
            {
                gvDsSanh.Rows[i].Cells[0].Value = (i + 1);
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThemsanh frm = new frmThemsanh();
   
            if(frm.ShowDialog()==DialogResult.OK)
                HienThiDanhSachSanh();
         
        }
        int vt = -1;
        string IDSanh;
        private void gvDsSanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                vt = e.RowIndex;
                if (vt == -1)
                    return;
                DataRow row = ds.Tables["sanh"].Rows[vt];
                IDSanh = row["IDSANH"] + "";
                txtSuaTenSanh.Text = row["TENSANH"] + "";
                txtSuaSoBanTD.Text = row["SOBANTD"] + "";
                txtSuaGhiChu.Text = row["GHICHU"] + "";
                cbbSuaLoaiSanh.Text = row["MALOAISANH"] + "";
            }
            catch { }
        }

        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {

        }
        private void LamTrong()
        {
            txtSuaGhiChu.Text = "";
            txtSuaTenSanh.Text = "";
            txtSuaSoBanTD.Text = "";
            cbbSuaLoaiSanh.Text = "";
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (gvDsSanh.SelectedRows.Count != 1)
                return;
            try
            {
                int er = -1;
                errorProvider1.SetError(txtSuaTenSanh, "");
                errorProvider1.SetError(cbbSuaLoaiSanh, "");
                errorProvider1.SetError(txtSuaSoBanTD, "");
                if (txtSuaTenSanh.Text == "")
                {
                    errorProvider1.SetError(txtSuaTenSanh, "Tên sảnh trống!");
                    er = 1;
                }
                if (cbbSuaLoaiSanh.Text == "")
                {
                    errorProvider1.SetError(cbbSuaLoaiSanh, "Loại sảnh trống!");
                    er = 1;
                }
                if (txtSuaSoBanTD.Text == "")
                {
                    errorProvider1.SetError(txtSuaSoBanTD, "Số bàn tối đa trống!");
                    er = 1;
                }
                OpenConnection();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SuaSanh";
                command.Connection = connection;
                command.Parameters.Add("@TenSanh", SqlDbType.NVarChar).Value = txtSuaTenSanh.Text;
                command.Parameters.Add("@IDSanh", SqlDbType.Int).Value = IDSanh;
                command.Parameters.Add("@SoBanTD", SqlDbType.Int).Value = txtSuaSoBanTD.Text;
                try { command.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = txtSuaGhiChu.Text; } catch { }
                command.Parameters.Add("@MaLoaiSanh", SqlDbType.Char).Value = cbbSuaLoaiSanh.Text;
                int kq = -1;
                if (er == -1)
                {
                    kq = command.ExecuteNonQuery();
                }
                if (kq > 0)
                {
                    HienThiDanhSachSanh();
                    MessageBox.Show("Sửa thành công");
                    LamTrong();
                }
                else
                {
                    LamTrong();
                    MessageBox.Show("Lỗi! Không sửa được");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi! Không sửa được");
                LamTrong();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gvDsSanh.SelectedRows.Count == 1)
            {
                DialogResult result = MessageBox.Show("Bạn muốn xóa?",
                    "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
                try
                {
                    OpenConnection();
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "XoaSanh";
                    command.Connection = connection;
                    command.Parameters.Add("@IDSanh", SqlDbType.Int).Value = IDSanh;
                    int kq = command.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        HienThiDanhSachSanh();
                        MessageBox.Show("Xóa thành công");
                        LamTrong();
                    }
                }
                catch
                {
                    MessageBox.Show("Loại sảnh đã được sử dụng, không thể xóa!", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LamTrong();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
