using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTQuanLyTiecCuoi
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void đặtTiệcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDatTiec frm = new frmDatTiec();
            frm.ShowDialog();
        }

        private void danhSáchTiệcCướiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDanhSachTiec frm = new frmDanhSachTiec();
            frm.ShowDialog();
        }

        private void danhSáchHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDanhSachHoaDon frm = new frmDanhSachHoaDon();
            frm.ShowDialog();
        }
    
        private void cậpNhậtSảnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
        }

        private void cậpNhậtLoạiSảnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCapNhatLoaiSanh frm = new frmCapNhatLoaiSanh();
            frm.ShowDialog();
        }

        private void cậpNhậtCaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCapNhatCa frm = new frmCapNhatCa();
            frm.ShowDialog();
        }

        private void cậpNhậtMónĂnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCapNhatMonAn frm = new frmCapNhatMonAn();
            frm.ShowDialog();

        }

        private void cậpNhậtDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCapNhatDichVu frm = new frmCapNhatDichVu();
            frm.ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát chương trình?",
                "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void lậpBáoCáoThángToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLapBaoCao frm = new frmLapBaoCao();
            frm.ShowDialog();
        }

        private void frmMain_Load_1(object sender, EventArgs e)
        {
            MaximizeBox = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUser frm = new frmUser();
            frm.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
