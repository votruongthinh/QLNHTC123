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
    public partial class frmLapBaoCao : Form
    {
        public frmLapBaoCao()
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
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtThang.Text=="" || txtSlTiec.Text=="" || txtDoanhThu.Text=="" || dtpNgaylap.Text=="")
            {
                MessageBox.Show("Thông tin trống!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(int.Parse(txtThang.Text)>12 || int.Parse(txtThang.Text)<1 )
            {
                MessageBox.Show("Tháng không hợp lệ!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ThemBaoCao";
            command.Connection = connection;
            command.Parameters.Add("@NgayLap", SqlDbType.SmallDateTime).Value = dtpNgaylap.Value;
            command.Parameters.Add("@Thang", SqlDbType.Int).Value = int.Parse(txtThang.Text);
            command.Parameters.Add("@SoLuongTiec", SqlDbType.Int).Value = int.Parse(txtSlTiec.Text);
            command.Parameters.Add("@DoanhThu", SqlDbType.BigInt).Value = long.Parse(txtDoanhThu.Text);
            command.ExecuteNonQuery();
            NapBaoCao();
            MessageBox.Show("Đã lưu!");
            LamTrong();
        }

        private void LamTrong()
        {
            dtpNgaylap.Text = "";
            txtDoanhThu.Text = "";
            txtSlTiec.Text = "";
            txtThang.Text = "";
        }

        private void txtSlTiec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDoanhThu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtThang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmLapBaoCao_Load(object sender, EventArgs e)
        {
            OpenConnection();
            NapBaoCao();
        }

        private void NapBaoCao()
        {
            dgvDsBaoCao.Columns.Clear();
            string query = "select * from LAPBAOCAO";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dgvDsBaoCao.DataSource = ds.Tables[0];
            dgvDsBaoCao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnXoaBaoCao_Click(object sender, EventArgs e)
        {
            if (dgvDsBaoCao.SelectedRows.Count == 1)
            {
                DialogResult result = MessageBox.Show("Bạn muốn xóa?",
                    "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from LAPBAOCAO where IDBaoCao=" + dgvDsBaoCao.SelectedRows[0].Cells[0].Value;
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            NapBaoCao();
        }

        private void frmLapBaoCao_FormClosing(object sender, FormClosingEventArgs e)
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
