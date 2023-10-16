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
    public partial class frmDanhSachHoaDon : Form
    {
        public frmDanhSachHoaDon()
        {
            InitializeComponent();
        }
        string strcnn = System.Configuration.ConfigurationManager.ConnectionStrings["qlnhtc"].ConnectionString;
        SqlConnection connection = null;
        string queryfull = "select hd.IDHD,kh.TenKH,kh.DienThoai,hd.NgayThanhToan,hd.TongTienHoaDon,hd.TienCoc,hd.TienPhat,hd.TienConLai from HOADON hd,KHACHHANG kh where hd.IDKH=kh.IDKH";

        public void OpenConnection()
        {
            if (connection == null)
                connection = new SqlConnection(strcnn);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        private void frmDanhSachHoaDon_Load(object sender, EventArgs e)
        {
            OpenConnection();
            NapHoaDon(queryfull);
        }

        private void NapHoaDon(string query)
        {
            dgvDanhSachHoaDon.Columns.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dgvDanhSachHoaDon.DataSource = ds.Tables[0];
            dgvDanhSachHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dgvDanhSachHoaDon.Rows.Count - 1; i++)
            {
                dgvDanhSachHoaDon.Rows[i].Cells[2].Value = dgvDanhSachHoaDon.Rows[i].Cells[2].Value.ToString().Split(' ')[0];
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTimSdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string query= queryfull+" and kh.DienThoai=" + txtTimSdt.Text;
            if (txtTimSdt.Text != "")
                NapHoaDon(query);
            else
                NapHoaDon(queryfull);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachHoaDon.SelectedRows.Count == 1)
            {

                DialogResult result = MessageBox.Show("Bạn muốn xóa?",
                    "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from HOADON where IDHD=" + dgvDanhSachHoaDon.SelectedRows[0].Cells[0].Value;
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            NapHoaDon(queryfull);
        }

        private void frmDanhSachHoaDon_FormClosing(object sender, FormClosingEventArgs e)
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
