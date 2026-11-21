using System;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            lblTrangThaiUser.Text = "Xin chào: " + KetNoiDB.HoTen + " (" + KetNoiDB.QuyenHan + ")";

            // Phân quyền: Nếu là Nhân viên thì không cho xem Thống kê Doanh thu & Lợi nhuận
            if (KetNoiDB.QuyenHan == "Nhân viên")
            {
                mnuThongKeDoanhThu.Enabled = false;
            }
        }

        // --- 1. NHÓM QUẢN LÝ HÀNG HÓA ---
        private void mnuLoaiHang_Click(object sender, EventArgs e)
        {
            new frmLoaiHang().ShowDialog();
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            new frmHangHoa().ShowDialog();
        }

        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            new frmNhapHang().ShowDialog();
        }

        // --- 2. NHÓM BÁN HÀNG & DOANH THU ---
        private void mnuBanHang_Click(object sender, EventArgs e)
        {
            new frmBanHang().ShowDialog();
        }

        private void mnuDanhSachHoaDon_Click(object sender, EventArgs e)
        {
            new frmDanhSachHoaDon().ShowDialog();
        }

        private void mnuThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            new frmThongKeDoanhThu().ShowDialog();
        }

        // --- 3. NHÓM QUẢN LÝ CÔNG NỢ ---
        private void mnuKhachHangCongNo_Click(object sender, EventArgs e)
        {
            new frmKhachHang_CongNo().ShowDialog();
        }

        private void mnuNhaCungCapCongNo_Click(object sender, EventArgs e)
        {
            new frmNhaCungCap_CongNo().ShowDialog();
        }

        private void mnuThuTraNo_Click(object sender, EventArgs e)
        {
            new frmThuTraNo().ShowDialog();
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
