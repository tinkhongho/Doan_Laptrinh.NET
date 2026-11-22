using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmDangNhap : Form
    {
        SqlConnection cn;

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            cn = KetNoiDB.TaoKetNoi();
            txtTenDangNhap.Text = "admin";
            txtMatKhau.Text = "123";
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text.Trim() == "" || txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                KetNoiDB.MoKetNoi(cn);
                string sql = "SELECT * FROM taikhoan WHERE tendangnhap = @user AND matkhau = @pass";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@user", txtTenDangNhap.Text.Trim());
                cmd.Parameters.AddWithValue("@pass", txtMatKhau.Text.Trim());

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    KetNoiDB.TenDangNhap = dr["tendangnhap"].ToString();
                    KetNoiDB.HoTen = dr["hoten"].ToString();
                    KetNoiDB.QuyenHan = dr["quyenhan"].ToString();
                    dr.Close();

                    frmTrangChu f = new frmTrangChu();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                    txtMatKhau.Clear();
                    txtTenDangNhap.Focus();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa kết nối được CSDL QLVTNN (Hãy chạy file Database/QLVTNN.sql trước).\nChi tiết: " + ex.Message,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn thoát chương trình không?", "Xác nhận thoát",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
