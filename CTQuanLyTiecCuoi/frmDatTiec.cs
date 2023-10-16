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
    public partial class frmDatTiec : Form
    {
        
        List<LoaiMonAn> LoaiMonAns = new List<LoaiMonAn>();
        List<MonAn> MonAns = new List<MonAn>();
        List<MonAn> DsMaChecked;
        List<DichVu> DsDvChecked;
        List<Ca> DsCa = new List<Ca>();
        List<Sanh> DsSanh = new List<Sanh>();
        //List<Tiec> DsTiec = new List<Tiec>();
        public frmDatTiec()
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
        private void frmDatTiec_Load(object sender, EventArgs e) 
        { 
            OpenConnection();
            NapMonAn();
            NapLoaiMonAn(cbbLoaiMonAn);
            NapDichVu();
            NapSanh();
            NapCa();
            if (frmDanhSachTiec.IsUpdate == false)
                button3.Text = "Lưu";
            else
            {
                button3.Text = "Lưu sửa chữa";
                txtTenKH.Text = frmDanhSachTiec.TenKhUd;
                txtSdt.Text = frmDanhSachTiec.SdtUd + "";
                dtpNgayDatTiec.Value = frmDanhSachTiec.NgayDatUd;
                txtTenCoDau.Text = frmDanhSachTiec.TenCoDauUd;
                txtTenChuRe.Text = frmDanhSachTiec.TenChuReUd;
                txtSoLuongBan.Text = frmDanhSachTiec.SoLuongBanUd +"";
                cbbTenSanh.Text = frmDanhSachTiec.SanhUd;
                cbbTenCa.Text = frmDanhSachTiec.CaUd;
                dtpNgayToChuc.Value = frmDanhSachTiec.NgayToChucUd;
                txtTienDatCoc.Text = frmDanhSachTiec.TienDatCocUd+"";
                DsMaChecked = new List<MonAn>();
                foreach (MonAn ma in frmDanhSachTiec.MonAnsCheckedUd)
                    DsMaChecked.Add(ma);
                bool checkDv = true;
                for (int i = 0; i < dgvDichVu.Rows.Count; i++)
                {
                    foreach (DichVu dv in frmDanhSachTiec.DichVusCheckedUd)
                        if (dv.IdDichVu + "" == dgvDichVu.Rows[i].Cells[3].Value.ToString())
                            dgvDichVu.Rows[i].Cells[4].Value = checkDv;
                }
                
            }
        }

        private void NapDichVu()
        { 
            dgvDichVu.Columns.Clear();
            DataGridViewTextBoxColumn dgvcStt = new DataGridViewTextBoxColumn();
            dgvcStt.HeaderText = "STT";
            DataGridViewTextBoxColumn dgvcTenDichVu = new DataGridViewTextBoxColumn();
            dgvcTenDichVu.HeaderText = "Tên dịch vụ";
            DataGridViewTextBoxColumn dgvcDonGia = new DataGridViewTextBoxColumn();
            dgvcDonGia.HeaderText = "Đơn giá";
            DataGridViewTextBoxColumn dgvcIdDichVu = new DataGridViewTextBoxColumn();
            dgvcIdDichVu.HeaderText = "IdMonAn";
            DataGridViewCheckBoxColumn dgvcCheckBox = new DataGridViewCheckBoxColumn();
            dgvcCheckBox.HeaderText = "Chọn";
            dgvDichVu.Columns.Add(dgvcStt);
            dgvDichVu.Columns.Add(dgvcTenDichVu);
            dgvDichVu.Columns.Add(dgvcDonGia);
            dgvDichVu.Columns.Add(dgvcIdDichVu);
            dgvDichVu.Columns.Add(dgvcCheckBox);
            int stt = 1;
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from DICHVU";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DichVu dv = new DichVu();
                dv.IdDichVu = reader.GetInt32(0);
                dv.TenDichVu = reader.GetString(1);
                dv.DonGia = reader.GetInt32(2);
                dgvDichVu.Rows.Add(stt,dv.TenDichVu,dv.DonGia,dv.IdDichVu,false);
                stt++;
            }
            reader.Close();
            dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDichVu.AllowUserToAddRows = false;
            dgvDichVu.Columns[3].Visible = false;
        }

        private void NapCa()
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from ThongTinCa";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            cbbTenCa.Items.Clear();
            while (reader.Read())
            {
                Ca ca = new Ca();
                ca.MaCa = reader.GetInt32(0);
                ca.TenCa = reader.GetString(1);
                DsCa.Add(ca);
                cbbTenCa.Items.Add(ca.TenCa);
            }
            reader.Close();
        }

        private void NapSanh()
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from SANH";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            cbbTenSanh.Items.Clear();
            while (reader.Read())
            {
                Sanh sanh = new Sanh();
                sanh.IdSanh = reader.GetInt32(0);
                sanh.TenSanh = reader.GetString(1);
                sanh.MaLoaiSanh = reader.GetString(2);
                sanh.SoBanTD = reader.GetInt32(3);
                try { sanh.GhiChu = reader.GetString(4); } catch { }
                DsSanh.Add(sanh);
                cbbTenSanh.Items.Add(sanh.TenSanh+"-"+sanh.MaLoaiSanh);
            }
            reader.Close();
        }

        private void NapMonAn()
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from THUCDON";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                MonAn ma = new MonAn();
                ma.IdMonAn = reader.GetInt32(0);
                ma.TenMonAn = reader.GetString(1);
                ma.DonGia = reader.GetInt32(2);
                ma.loaimonan = reader.GetString(3);
                 try { ma.GhiChu = reader.GetString(4); } catch(Exception ex) { ma.GhiChu = " "; }
               

                MonAns.Add(ma);
            }
            reader.Close();
        }

        private void NapLoaiMonAn(ComboBox cbb)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from LOAIMONAN";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            cbb.Items.Clear();
            while(reader.Read())
            {
                LoaiMonAn lma = new LoaiMonAn();
                lma.MaLoaiMA = reader.GetString(0);
                lma.TenLoaiMA = reader.GetString(1);
                foreach (MonAn ma in MonAns)
                    if (ma.loaimonan == lma.MaLoaiMA)
                        lma.dsma.Add(ma);
                LoaiMonAns.Add(lma);
                cbb.Items.Add(lma.TenLoaiMA);
            }
            reader.Close();
        }

        private void cbbLoaiMonAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control control in gropBox1.Controls)
                control.Visible = false;
            foreach (LoaiMonAn lma in LoaiMonAns)
            {
                if (lma.TenLoaiMA == cbbLoaiMonAn.Text && lma.tag != 1)
                {
                    lma.tag = 1;
                    DataGridView dgv = new DataGridView();
                    dgv.Name ="dgv"+ lma.TenLoaiMA;
                    dgv.Dock = DockStyle.Fill;
                    dgv.Visible = true;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    NapMonAnLenDgv(dgv, lma);
                    if (frmDanhSachTiec.IsUpdate == true)
                    {
                        try
                        {
                            foreach (MonAn ma in DsMaChecked)
                                if (ma.loaimonan == lma.MaLoaiMA)
                                    DsMaChecked.Remove(ma);
                        }
                        catch { }
                        bool checkMa = true;
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            foreach (MonAn ma in frmDanhSachTiec.MonAnsCheckedUd)
                                if (ma.IdMonAn + "" == dgv.Rows[i].Cells[5].Value.ToString())
                                    dgv.Rows[i].Cells[6].Value = checkMa;
                        }
                    }
                    gropBox1.Controls.Add(dgv);
                }
                else if (lma.TenLoaiMA == cbbLoaiMonAn.Text && lma.tag == 1)
                    foreach (Control control in gropBox1.Controls)
                        if (control.Name == "dgv" + lma.TenLoaiMA)
                            control.Visible = true;
            }
        }

        private void NapMonAnLenDgv(DataGridView dgv, LoaiMonAn lma)
        {
            dgv.Columns.Clear();
            DataGridViewTextBoxColumn dgvStt = new DataGridViewTextBoxColumn();
            dgvStt.HeaderText = "STT";
            DataGridViewTextBoxColumn dgvTenMonAn = new DataGridViewTextBoxColumn();
            dgvTenMonAn.HeaderText = "Tên món ăn";
            DataGridViewTextBoxColumn dgvDonGia = new DataGridViewTextBoxColumn();
            dgvDonGia.HeaderText = "Đơn giá";
            DataGridViewTextBoxColumn dgvLoaiMonAn = new DataGridViewTextBoxColumn();
            dgvLoaiMonAn.HeaderText = "Loại món ăn";
            DataGridViewTextBoxColumn dgvGhiChu = new DataGridViewTextBoxColumn();
            dgvGhiChu.HeaderText = "Ghi chú";
            DataGridViewTextBoxColumn dgvIdMonAn = new DataGridViewTextBoxColumn();
            dgvIdMonAn.HeaderText = "IdMonAn";
            DataGridViewCheckBoxColumn dgvCheckBox = new DataGridViewCheckBoxColumn();
            dgvCheckBox.HeaderText = "Chọn";
            dgv.Columns.Add(dgvStt);
            dgv.Columns.Add(dgvTenMonAn);
            dgv.Columns.Add(dgvDonGia);
            dgv.Columns.Add(dgvLoaiMonAn);
            dgv.Columns.Add(dgvGhiChu);
            dgv.Columns.Add(dgvIdMonAn);
            dgv.Columns.Add(dgvCheckBox);
            int stt = 1;
            foreach (MonAn ma in lma.dsma)
            {
                dgv.Rows.Add(stt, ma.TenMonAn, ma.DonGia, ma.loaimonan, ma.GhiChu, ma.IdMonAn, false);
                stt++;

            }
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.Columns[5].Visible = false;
            
        }

        private void cbbLoaiMonAn_SelectedValueChanged(object sender, EventArgs e)
        {
        }
        int NewIdKH = 0;
        int NewIdTiec = 0;
        int MaCa = 0;
        int IdSanh = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            DsDvChecked = new List<DichVu>();
            for (int i = 0; i < dgvDichVu.Rows.Count; i++)
            {
                bool checkedCellDv = (bool)dgvDichVu.Rows[i].Cells[4].Value;
                if (checkedCellDv == true)
                {
                    DataGridViewRow row = dgvDichVu.Rows[i];
                    DichVu dv = new DichVu();
                    dv.IdDichVu = int.Parse(row.Cells[3].Value.ToString());
                    dv.TenDichVu = row.Cells[1].Value.ToString();
                    dv.DonGia = int.Parse(row.Cells[2].Value.ToString());
                    DsDvChecked.Add(dv);
                }
            }
            if(frmDanhSachTiec.IsUpdate==false) DsMaChecked = new List<MonAn>();
            foreach (Control control in gropBox1.Controls)
            {
                DataGridView dgv = control as DataGridView;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    bool checkedCellMa = (bool)dgv.Rows[i].Cells[6].Value;
                    if (checkedCellMa == true)
                    {
                        DataGridViewRow row = dgv.Rows[i];
                        MonAn ma = new MonAn();
                        ma.IdMonAn = int.Parse(row.Cells[5].Value.ToString());
                        ma.TenMonAn = row.Cells[1].Value.ToString();
                        ma.DonGia = int.Parse(row.Cells[2].Value.ToString());
                        ma.loaimonan = row.Cells[3].Value.ToString();
                        try { ma.GhiChu = row.Cells[4].Value.ToString(); } catch { }
                        DsMaChecked.Add(ma);
                    }
                }
            }
            foreach (Ca ca in DsCa)
                if (ca.TenCa == cbbTenCa.Text)
                    MaCa = ca.MaCa;
            foreach (Sanh sanh in DsSanh)
                if (sanh.TenSanh == cbbTenSanh.Text.Split('-')[0])
                    IdSanh = sanh.IdSanh;
            int check = KiemTraBietLe();
            if (check == 1)
                return;
           
            ThemKhachHang();
            ThemTiec();
            ThemCtDatTiec();
            if (frmDanhSachTiec.IsUpdate == true)
                MessageBox.Show("Sửa thành công!");
            else
                MessageBox.Show("Đặt thành công!");
            DialogResult = DialogResult.OK;
        }

        public static void XoaCTDatTiec(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from CTDATTIEC where IDDatTiec ="+frmDanhSachTiec.IDDatTiec;
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        public static void XoaTiec(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from THONGTINDATTIEC where IDDatTiec ="+frmDanhSachTiec.IDDatTiec;
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        public static void XoaKhachHang(SqlConnection connection)
        {

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from KHACHHANG where IDKH ="+frmDanhSachTiec.IDKHUd;
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        private void ThemCtDatTiec()
        {
            if(DsDvChecked.Count>0)
                for(int i=0;i<DsMaChecked.Count;i++)
                    for(int j=0;j<DsDvChecked.Count;j++)
                    {
                        SqlCommand command = new SqlCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "ThemCtDatTiec";
                        command.Connection = connection;
                        command.Parameters.Add("@IdDatTiec", SqlDbType.Int).Value = NewIdTiec;
                        command.Parameters.Add("@IdMonAn", SqlDbType.Int).Value = DsMaChecked[i].IdMonAn;
                        command.Parameters.Add("@IdDichVu", SqlDbType.Int).Value = DsDvChecked[j].IdDichVu;
                        command.ExecuteNonQuery();
                    }
            else
            {
                for(int i=0;i<DsMaChecked.Count;i++)
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "ThemCtDatTiec";
                    command.Connection = connection;
                    command.Parameters.Add("@IdDatTiec", SqlDbType.Int).Value = NewIdTiec;
                    command.Parameters.Add("@IdMonAn", SqlDbType.Int).Value = DsMaChecked[i].IdMonAn;
                    command.Parameters.Add("@IdDichVu", SqlDbType.Int).Value = null;
                    command.ExecuteNonQuery();
                }
            }
        }

        string MaLoaiSanh;
        int DonGiaBanTT;
        int SlBanTd;
        private int KiemTraBietLe()
        {
            if (txtTenKH.Text == "" || txtTenCoDau.Text == "" || txtTenChuRe.Text == "" || txtSdt.Text == "" || txtSoLuongBan.Text == "" ||
                txtTienDatCoc.Text == "" || cbbTenSanh.Text == "" || cbbTenCa.Text == "" || dtpNgayDatTiec.Text == "" || dtpNgayDatTiec.Text == "")
            {
                MessageBox.Show("Thông tin trống!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            if (DsMaChecked.Count == 0)
            {
                MessageBox.Show("Chưa chọn món ăn!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            int GiaBan = 0;
            if (DsMaChecked.Count > 0)
                foreach (MonAn ma in DsMaChecked)
                    GiaBan += ma.DonGia;
            if (GiaBan < DonGiaBanTT)
            {
                MessageBox.Show("Chưa đủ giá bàn tương ứng với loại sảnh này!", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            if(int.Parse(txtSoLuongBan.Text)>SlBanTd)
            {
                MessageBox.Show("Vượt quá số lượng bàn!", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
            if (frmDanhSachTiec.IsUpdate == true)
            {
                XoaCTDatTiec(connection);
                XoaTiec(connection);
                XoaHoaDon(connection);
                XoaKhachHang(connection);

            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from THONGTINDATTIEC";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DateTime date = reader.GetDateTime(3);
                int MaCaExist = reader.GetInt32(4);
                int IdSanhExist = reader.GetInt32(5);
                if (MaCaExist == MaCa && date.ToShortDateString() == dtpNgayToChuc.Value.ToShortDateString() && IdSanhExist == IdSanh)
                {
                    MessageBox.Show("Đã có khách hàng đặt!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return 1;
                }
            }
            reader.Close();
            return 0;
        }

        public static void XoaHoaDon(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from HOADON where IDKH =" + frmDanhSachTiec.IDKHUd;
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        private void ThemTiec()
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ThemTiec";
            command.Connection = connection;
            command.Parameters.Add("@IDKH", SqlDbType.Int).Value = NewIdKH;
            command.Parameters.Add("@NgayDatTiec", SqlDbType.SmallDateTime).Value = dtpNgayDatTiec.Value;
            command.Parameters.Add("@NgayToChuc", SqlDbType.SmallDateTime).Value = dtpNgayToChuc.Value;
            command.Parameters.Add("@MaCa", SqlDbType.Int).Value = MaCa;
            command.Parameters.Add("@IDSanh", SqlDbType.Int).Value = IdSanh;
            command.Parameters.Add("@TienDatCoc", SqlDbType.Int).Value = int.Parse(txtTienDatCoc.Text);
            command.Parameters.Add("@SoLuongBan", SqlDbType.Int).Value = int.Parse(txtSoLuongBan.Text);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                NewIdTiec = int.Parse(reader.GetValue(0).ToString());
            reader.Close();
        }

        private void ThemKhachHang()
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ThemKhachHang";
            command.Connection = connection;
            command.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = txtTenKH.Text;
            command.Parameters.Add("@TenChuRe", SqlDbType.NVarChar).Value = txtTenChuRe.Text;
            command.Parameters.Add("@TenCoDau", SqlDbType.NVarChar).Value = txtTenCoDau.Text;
            command.Parameters.Add("@DienThoai", SqlDbType.NVarChar).Value = txtSdt.Text;
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
                NewIdKH=int.Parse(reader.GetValue(0).ToString());
            reader.Close();
        }

        private void txtSdt_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoLuongBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTienDatCoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            DsDvChecked = new List<DichVu>();
            for (int i = 0; i < dgvDichVu.Rows.Count; i++)
            {
                bool checkedCellDv = (bool)dgvDichVu.Rows[i].Cells[4].Value;
                if (checkedCellDv == true)
                {
                    DataGridViewRow row = dgvDichVu.Rows[i];
                    DichVu dv = new DichVu();
                    dv.IdDichVu = int.Parse(row.Cells[3].Value.ToString());
                    dv.TenDichVu = row.Cells[1].Value.ToString();
                    dv.DonGia = int.Parse(row.Cells[2].Value.ToString());
                    DsDvChecked.Add(dv);
                }
            }
            DsMaChecked = new List<MonAn>();
            foreach (Control control in gropBox1.Controls)
            {
                DataGridView dgv = control as DataGridView;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    bool checkedCellMa = (bool)dgv.Rows[i].Cells[6].Value;
                    if (checkedCellMa == true)
                    {
                        DataGridViewRow row = dgv.Rows[i];
                        MonAn ma = new MonAn();
                        ma.IdMonAn = int.Parse(row.Cells[5].Value.ToString());
                        ma.TenMonAn = row.Cells[1].Value.ToString();
                        ma.DonGia = int.Parse(row.Cells[2].Value.ToString());
                        ma.loaimonan = row.Cells[3].Value.ToString();
                        try { ma.GhiChu = row.Cells[4].Value.ToString(); } catch { }
                        DsMaChecked.Add(ma);
                    }
                }
            }
            int sum = 0;
            if(DsMaChecked.Count>0)
                foreach (MonAn ma in DsMaChecked)
                    sum += ma.DonGia;
            if(DsDvChecked.Count>0)
                foreach (DichVu dv in DsDvChecked)
                    sum += dv.DonGia;
            lblTinhTien.Text = sum + "  VNĐ";
        }

        private void cbbTenSanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from LOAISANH";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                MaLoaiSanh = reader.GetString(0);
                DonGiaBanTT = reader.GetInt32(1);
                if (MaLoaiSanh == cbbTenSanh.Text.Split('-')[1])
                    break; 
            }
            reader.Close();
            txtDonGiaBantt.Text = DonGiaBanTT + " VNĐ";
            foreach (Sanh s in DsSanh)
                if (s.TenSanh == cbbTenSanh.Text.Split('-')[0])
                    SlBanTd = s.SoBanTD;
            txtSlBanTd.Text = SlBanTd + "";
        }

        private void txtSoLuongBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn hủy đặt tiệc?",
                "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
            else if (result == DialogResult.No)
                return;
        }

        private void frmDatTiec_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void frmDatTiec_FormClosed(object sender, FormClosedEventArgs e)
        {
                
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpNgayToChuc_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNgayToChuc.Value < dtpNgayDatTiec.Value)
            {
                MessageBox.Show("Ngày không hợp lệ (Kiểm tra ngày đặt tiệc)!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpNgayToChuc.Value = dtpNgayDatTiec.Value;
                return;
            }
        }
    }
}
