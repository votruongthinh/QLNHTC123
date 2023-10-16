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
    public partial class frmDanhSachTiec : Form
    {
        static public bool IsUpdate = false;
        static public string TenKhUd;
        static public int SdtUd;
        static public DateTime NgayDatUd;
        static public string TenCoDauUd;
        static public string TenChuReUd;
        static public int SoLuongBanUd;
        static public string SanhUd;
        static public string CaUd;
        static public DateTime NgayToChucUd;
        static public int TienDatCocUd;
        static public int IDKHUd;
        static public int IDDatTiec;
        static public List<MonAn> MonAnsCheckedUd;
        static public List<DichVu> DichVusCheckedUd;
        public frmDanhSachTiec()
        {
            InitializeComponent();
        }
        string strcnn = System.Configuration.ConfigurationManager.ConnectionStrings["qlnhtc"].ConnectionString;
        SqlConnection connection = null;
        public void OpenConnection()
        {
            if (connection == null)
                connection = new SqlConnection(strcnn);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        string queryfull = "select * from KHACHHANG kh,THONGTINDATTIEC t, SANH s,ThongTinCa c where kh.IDKH = t.IDKH and t.IDSanh = s.IDSanh and t.MaCa = c.MaCa";

        private void frmDanhSachTiec_Load(object sender, EventArgs e)
        {
            OpenConnection();
            NapDsTiecCuoi(queryfull);
        }

        private void NapDsTiecCuoi(string query)
        {
            dgvDsTiecCuoi.Columns.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataGridViewColumn col = new DataGridViewColumn();
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            col.HeaderText = "STT";
            col.Visible = true;
            col.Width = 40;
            col.CellTemplate = cell;
            dgvDsTiecCuoi.Columns.Add(col);
            dgvDsTiecCuoi.DataSource = ds.Tables[0];
            for (int i = 0; i < dgvDsTiecCuoi.Rows.Count - 1; i++)
            {
                dgvDsTiecCuoi.Rows[i].Cells[0].Value = (i + 1);
                dgvDsTiecCuoi.Rows[i].Cells[8].Value = dgvDsTiecCuoi.Rows[i].Cells[8].Value.ToString().Split(' ')[0];
                dgvDsTiecCuoi.Rows[i].Cells[9].Value = dgvDsTiecCuoi.Rows[i].Cells[9].Value.ToString().Split(' ')[0];
            }
            dgvDsTiecCuoi.Columns[1].Visible = false;
            dgvDsTiecCuoi.Columns[6].Visible = false;
            dgvDsTiecCuoi.Columns[7].Visible = false;
            dgvDsTiecCuoi.Columns[8].Visible = false;
            dgvDsTiecCuoi.Columns[10].Visible = false;
            dgvDsTiecCuoi.Columns[11].Visible = false;
            dgvDsTiecCuoi.Columns[13].Visible = false;
            dgvDsTiecCuoi.Columns[14].Visible = false;
            dgvDsTiecCuoi.Columns[16].Visible = false;
            dgvDsTiecCuoi.Columns[17].Visible = false;
            dgvDsTiecCuoi.Columns[18].Visible = false;
            dgvDsTiecCuoi.Columns[19].Visible = false;
        }

        private void txtTimSdt_TextChanged(object sender, EventArgs e)
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
            string query = "select * from KHACHHANG kh,THONGTINDATTIEC t, SANH s,ThongTinCa c where kh.IDKH = t.IDKH and t.IDSanh = s.IDSanh and t.MaCa = c.MaCa and kh.DienThoai=" + txtTimSdt.Text;
            if (txtTimSdt.Text != "")
                NapDsTiecCuoi(query);
            else
                NapDsTiecCuoi(queryfull);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvDsTiecCuoi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSuaTiec_Click(object sender, EventArgs e)
        {
            if(dgvDsTiecCuoi.SelectedRows.Count==1)
            {

                IsUpdate = true;
                try { TenKhUd = dgvDsTiecCuoi.SelectedRows[0].Cells[2].Value.ToString(); } catch { return; } ;
                TaoGiaTriSelect();
                frmDatTiec frm = new frmDatTiec();
                if (frm.ShowDialog()==DialogResult.OK)
                {
                    NapDsTiecCuoi(queryfull);
                }
            }
            
        }

        private void TaoGiaTriSelect()
        {
            if (dgvDsTiecCuoi.SelectedRows.Count == 1)
            {
                try { TenKhUd = dgvDsTiecCuoi.SelectedRows[0].Cells[2].Value.ToString(); } catch { return; };
                SdtUd = int.Parse(dgvDsTiecCuoi.SelectedRows[0].Cells[5].Value.ToString());
                NgayDatUd = DateTime.Parse(dgvDsTiecCuoi.SelectedRows[0].Cells[8].Value.ToString());
                TenChuReUd = dgvDsTiecCuoi.SelectedRows[0].Cells[3].Value.ToString();
                TenCoDauUd = dgvDsTiecCuoi.SelectedRows[0].Cells[4].Value.ToString();
                SoLuongBanUd = int.Parse(dgvDsTiecCuoi.SelectedRows[0].Cells[13].Value.ToString());
                SanhUd = dgvDsTiecCuoi.SelectedRows[0].Cells[15].Value.ToString() + '-' + dgvDsTiecCuoi.SelectedRows[0].Cells[16].Value.ToString();
                CaUd = dgvDsTiecCuoi.SelectedRows[0].Cells[20].Value.ToString();
                NgayToChucUd = DateTime.Parse(dgvDsTiecCuoi.SelectedRows[0].Cells[9].Value.ToString());
                TienDatCocUd = int.Parse(dgvDsTiecCuoi.SelectedRows[0].Cells[12].Value.ToString());
                IDDatTiec = int.Parse(dgvDsTiecCuoi.SelectedRows[0].Cells[6].Value.ToString());
                IDKHUd = int.Parse(dgvDsTiecCuoi.SelectedRows[0].Cells[1].Value.ToString()); NapMonAnDaChecked(IDDatTiec);
                NapDichVuDaChecked(IDDatTiec);
            }
        }

        private void NapDichVuDaChecked(int iDDatTiec)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select distinct dv.* from CTDATTIEC ct,DICHVU dv where ct.IDDichVu = dv.IDDichVu and ct.IDDatTiec ="+iDDatTiec;
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            DichVusCheckedUd = new List<DichVu>();
            while (reader.Read())
            {
                DichVu dv = new DichVu();
                dv.IdDichVu = reader.GetInt32(0);
                dv.TenDichVu = reader.GetString(1);
                dv.DonGia = reader.GetInt32(2);
                DichVusCheckedUd.Add(dv);
            }
            reader.Close();
        }

        private void NapMonAnDaChecked(int IDDatTiec)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select distinct td.* from CTDATTIEC ct,THUCDON td where ct.IDMonAn = td.IDMonAn and ct.IDDatTiec ="+IDDatTiec;
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            MonAnsCheckedUd = new List<MonAn>();
            while (reader.Read())
            {
                MonAn ma = new MonAn();
                ma.IdMonAn = reader.GetInt32(0);
                ma.TenMonAn = reader.GetString(1);
                ma.DonGia = reader.GetInt32(2);
                ma.loaimonan = reader.GetString(3);
                try { ma.GhiChu = reader.GetString(4); } catch { };
                MonAnsCheckedUd.Add(ma);
            }
            reader.Close();
        }

        private void btnXoaTiec_Click(object sender, EventArgs e)
        {
            if(dgvDsTiecCuoi.SelectedRows.Count == 1)
            {
                DialogResult result = MessageBox.Show("Bạn muốn xóa?",
                    "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
                try { TenKhUd = dgvDsTiecCuoi.SelectedRows[0].Cells[2].Value.ToString(); } catch { return; };
                TaoGiaTriSelect();
                frmDatTiec.XoaCTDatTiec(connection);
                frmDatTiec.XoaTiec(connection);
                frmDatTiec.XoaHoaDon(connection);
                frmDatTiec.XoaKhachHang(connection);
                NapDsTiecCuoi(queryfull);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if(dgvDsTiecCuoi.SelectedRows.Count==1)
            {
                try { TenKhUd = dgvDsTiecCuoi.SelectedRows[0].Cells[2].Value.ToString(); } catch { return; };
                TaoGiaTriSelect();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select IDKH from HOADON";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int IDKH = reader.GetInt32(0);
                    if (IDKH == IDKHUd)
                    {
                        MessageBox.Show("Khách hàng này đã thanh toán!");
                        reader.Close();
                        return;
                    }
                }
                reader.Close();
                frmHoaDon frm = new frmHoaDon();
                if(frm.ShowDialog()==DialogResult.OK)
                {

                }
              
            }
        }

        private void frmDanhSachTiec_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát?",
                "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                IsUpdate = false;
                e.Cancel = false;
            }
            else if (result == DialogResult.No)
                e.Cancel = true;
        }
    }
}
