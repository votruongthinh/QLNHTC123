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
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
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
        int TongTienBan=0;
        int TongTienDichVu = 0;
        int TongTienHoaDon = 0;
        int TienDatCoc = frmDanhSachTiec.TienDatCocUd;
        DateTime NgayToChuc = frmDanhSachTiec.NgayToChucUd;
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void lvMonAn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            lblTenKH.Text = frmDanhSachTiec.TenKhUd;
            lblTenChuRe.Text = frmDanhSachTiec.TenChuReUd;
            lblTenCoDau.Text = frmDanhSachTiec.TenCoDauUd;
            lblSoLuongBan.Text = frmDanhSachTiec.SoLuongBanUd + "";
            dtpNgayThanhToan.Value = frmDanhSachTiec.NgayToChucUd;
            NapMonAnLv();
            NapDichVuLv();
            TongTienHoaDon = TongTienBan*frmDanhSachTiec.SoLuongBanUd + TongTienDichVu;
            lblTongTienHoaDon.Text = TongTienHoaDon+" VNĐ";
            lblTienCoc.Text = TienDatCoc + " VNĐ";
        }

        private void NapDichVuLv()
        {
            lvDichVu.Items.Clear();
            for (int i = 0; i < frmDanhSachTiec.DichVusCheckedUd.Count; i++)
            {
                ListViewItem lvi = new ListViewItem(frmDanhSachTiec.DichVusCheckedUd[i].TenDichVu);
                lvi.SubItems.Add(frmDanhSachTiec.DichVusCheckedUd[i].DonGia + "");
                TongTienDichVu += frmDanhSachTiec.DichVusCheckedUd[i].DonGia;
                lvDichVu.Items.Add(lvi);
            }
            lblTongTienDv.Text = TongTienDichVu + " VNĐ";
        }

        private void NapMonAnLv()
        {
            lvMonAn.Items.Clear();
            for(int i=0;i<frmDanhSachTiec.MonAnsCheckedUd.Count;i++)
            {
                ListViewItem lvi = new ListViewItem(frmDanhSachTiec.MonAnsCheckedUd[i].TenMonAn);
                lvi.SubItems.Add(frmDanhSachTiec.MonAnsCheckedUd[i].DonGia + "");
                TongTienBan += frmDanhSachTiec.MonAnsCheckedUd[i].DonGia;
                lvMonAn.Items.Add(lvi);
            }
            lblTongTienBan.Text = TongTienBan + " VNĐ";
        }
        private void rbPhat_CheckedChanged(object sender, EventArgs e)
        {
            //int SoNgayTre=(dtpNgayThanhToan.Value.Year-NgayToChuc.Year)*365+
            TimeSpan timeSpan = dtpNgayThanhToan.Value.Date - NgayToChuc.Date;
            double ngaytre =timeSpan.TotalDays;
            double TienPhat = ngaytre*TongTienHoaDon*0.01;
            double TienConLai = TongTienHoaDon + TienPhat - TienDatCoc;
            lblTienPhat.Text = TienPhat + " VNĐ";
            lblTienPhatThanhToan.Text = TienConLai + " VNĐ";
        }

        private void rbKhongPhat_CheckedChanged(object sender, EventArgs e)
        {
            lblTienPhat.Text = "0 VNĐ";
            int TienConLai = TongTienHoaDon - TienDatCoc;
            lblTienPhatThanhToan.Text = TienConLai + " VNĐ";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(rbKhongPhat.Checked==false && rbPhat.Checked==false)
            {
                MessageBox.Show("Chưa chọn quy định phạt!", "Error",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ThemHoaDon";
            command.Connection = connection;
            command.Parameters.Add("@IDKH", SqlDbType.Int).Value = frmDanhSachTiec.IDKHUd;
            command.Parameters.Add("@NgayThanhToan", SqlDbType.SmallDateTime).Value = dtpNgayThanhToan.Value;
            command.Parameters.Add("@TongTienBan", SqlDbType.Int).Value =TongTienBan;
            command.Parameters.Add("@TongTienDichVu", SqlDbType.Int).Value = TongTienDichVu;
            command.Parameters.Add("@TongTienHoaDon", SqlDbType.Int).Value = TongTienHoaDon;
            command.Parameters.Add("@TienCoc", SqlDbType.Int).Value = TienDatCoc;
            command.Parameters.Add("@TienPhat", SqlDbType.Float).Value = float.Parse(lblTienPhat.Text.Split(' ')[0]);
            command.Parameters.Add("@TienConLai", SqlDbType.Float).Value = float.Parse(lblTienPhatThanhToan.Text.Split(' ')[0]);
            command.ExecuteNonQuery();
            MessageBox.Show("Lưu thành công!");
            DialogResult = DialogResult.OK;
        }

        private void frmHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void dtpNgayThanhToan_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNgayThanhToan.Value < NgayToChuc)
            {
                MessageBox.Show("Ngày không hợp lệ (Kiểm tra ngày tổ chức)!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpNgayThanhToan.Value = NgayToChuc;
                return;
            }
        }
    }
}
