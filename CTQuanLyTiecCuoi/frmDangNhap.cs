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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        //Data Source=DESKTOP-4N48QA4\SQLEXPRESS;Initial Catalog=QUANLYTIECCUOI;Integrated Security=True;
        //string strcnn = "Data Source=DESKTOP-90K2CL6\\THANHHUNG;Initial Catalog=QUANLYTIECCUOI;Integrated Security=True";
        //tạo kết nối cục bộ với cơ sở dữ liệu
        string strcnn = System.Configuration.ConfigurationManager.ConnectionStrings["qlnhtc"].ConnectionString;
        public static int IDTaiKhoan;

       
        private void btnDangNhap_Click(object sender, EventArgs e)//thực hiện button nhập dữ liệu từ sql vào form đăng nhập
        {   
            
            SqlConnection conn = new SqlConnection(strcnn);
            string sqlSelect = "Select * from DANGNHAP where TaiKhoan = '" +
            txtTaiKhoan.Text + "' and MatKhau = '" + txtMatKhau.Text + "'";
            conn.Open(); // mở kết nối đến cơ sở dữ liệu
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);
            SqlDataReader reader = cmd.ExecuteReader(); //thực thi câu lệnh trong sql
            // reader là lớp đối tượng của lớp sqlReader
            if (reader.Read() == true)
            {
                IDTaiKhoan = reader.GetInt32(0); // lấy giá trị một cột kiểu int. Lấy giá trị của cột `ID` trong bản ghi đầu tiên int id = reader.GetInt32(0)
                int id = reader.GetInt32(0);
                this.Hide(); // ẩn form login sau khi đã đăng nhập
                frmMain main = new frmMain();
                if (main.ShowDialog() == DialogResult.OK) // main tham chiếu đối tượng của frmMain và DlResult giá trị trả về 
                {
                    this.Visible = true;
                    txtTaiKhoan.Text = "";
                    txtMatKhau.Text = "";
                    txtTaiKhoan.Focus(); // ô tiêm điểm để người dùng có thể nhập vào
                }
                else
                    this.Close();
 
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công!", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTaiKhoan.Text = "";
                txtMatKhau.Text = "";
                txtTaiKhoan.Focus();
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
  
            this.Close();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void txtTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
