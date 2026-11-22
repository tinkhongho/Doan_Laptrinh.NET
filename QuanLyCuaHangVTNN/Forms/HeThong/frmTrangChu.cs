using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmTrangChu : Form
    {
        private Form formHienTai = null;
        private Button nutDangChon = null;

        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            string hoTen = string.IsNullOrEmpty(KetNoiDB.HoTen) ? "Quản trị viên" : KetNoiDB.HoTen;
            string quyen = string.IsNullOrEmpty(KetNoiDB.QuyenHan) ? "Quản lý" : KetNoiDB.QuyenHan;

            lblUserSidebar.Text = hoTen + " (" + quyen + ")";
            lblTrangThaiUser.Text = "Người dùng: " + hoTen + " | Quyền hạn: " + quyen + " | Máy chủ: .\\SQLEXPRESS";

            // Phân quyền: Nhân viên không được xem Thống kê Doanh thu & Lợi nhuận
            if (KetNoiDB.QuyenHan == "Nhân viên")
            {
                mnuThongKeDoanhThu.Enabled = false;
                btnNavDoanhThu.Enabled = false;
            }
        }

        /// <summary>
        /// Đổi màu nút đang được chọn trên thanh Sidebar bên trái
        /// </summary>
        private void DanhDauNutSidebar(Button btnMoi)
        {
            if (nutDangChon != null)
            {
                nutDangChon.BackColor = Color.FromArgb(27, 94, 32);
                nutDangChon.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
            }

            nutDangChon = btnMoi;
            if (nutDangChon != null)
            {
                nutDangChon.BackColor = Color.FromArgb(56, 142, 60);
                nutDangChon.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            }
        }

        /// <summary>
        /// Nhúng Form con của các thành viên hiển thị gọn ngay trong khung pnlContent hiện đại
        /// </summary>
        private void MoFormCon(Form fCon, string tieuDe, Button btnSidebar)
        {
            if (formHienTai != null)
            {
                formHienTai.Close();
            }

            DanhDauNutSidebar(btnSidebar);
            formHienTai = fCon;
            lblHeaderTitle.Text = tieuDe.ToUpper();
            pnlHomeOverview.Visible = false;
            btnVeTrangChu.Visible = true;

            fCon.TopLevel = false;
            fCon.FormBorderStyle = FormBorderStyle.None;
            fCon.Dock = DockStyle.Fill;
            fCon.FormClosed += (s, args) =>
            {
                if (formHienTai == fCon)
                {
                    formHienTai = null;
                    HienThiTrangChu();
                }
            };

            pnlContent.Controls.Add(fCon);
            pnlContent.Tag = fCon;
            fCon.BringToFront();
            fCon.Show();
        }

        private void HienThiTrangChu()
        {
            if (formHienTai != null)
            {
                Form tmp = formHienTai;
                formHienTai = null;
                tmp.Close();
            }

            DanhDauNutSidebar(null);
            lblHeaderTitle.Text = "TỔNG QUAN HỆ THỐNG CỬA HÀNG VẬT TƯ NÔNG NGHIỆP";
            btnVeTrangChu.Visible = false;
            pnlHomeOverview.Visible = true;
            pnlHomeOverview.BringToFront();
        }

        private void btnVeTrangChu_Click(object sender, EventArgs e)
        {
            HienThiTrangChu();
        }

        // --- 1. NHÓM QUẢN LÝ HÀNG HÓA (TÍN) ---
        private void mnuLoaiHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmLoaiHang(), "1.1. Danh mục Loại vật tư nông nghiệp", btnNavLoaiHang);
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmHangHoa(), "1.2. Quản lý Hàng hóa & Tồn kho", btnNavHangHoa);
        }

        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmNhapHang(), "1.3. Lập phiếu Nhập hàng từ Nhà cung cấp", btnNavNhapHang);
        }

        // --- 2. NHÓM BÁN HÀNG & DOANH THU (THIỆN) ---
        private void mnuBanHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmBanHang(), "2.1. Lập Hóa đơn Bán hàng (Thanh toán / Ghi nợ mùa vụ)", btnNavBanHang);
        }

        private void mnuDanhSachHoaDon_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmDanhSachHoaDon(), "2.2. Tra cứu Danh sách Hóa đơn đã bán", btnNavDSHoaDon);
        }

        private void mnuThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmThongKeDoanhThu(), "2.3. Thống kê Doanh thu - Thực thu - Lợi nhuận", btnNavDoanhThu);
        }

        // --- 3. NHÓM QUẢN LÝ CÔNG NỢ (TÂN) ---
        private void mnuKhachHangCongNo_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmKhachHang_CongNo(), "3.1. Quản lý Khách hàng & Công nợ phải thu", btnNavKhachHang);
        }

        private void mnuNhaCungCapCongNo_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmNhaCungCap_CongNo(), "3.2. Quản lý Nhà cung cấp & Công nợ phải trả", btnNavNhaCungCap);
        }

        private void mnuThuTraNo_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmThuTraNo(), "3.3. Lập phiếu Thu nợ khách hàng / Trả nợ NCC", btnNavThuTraNo);
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn đăng xuất khỏi phiên làm việc không?",
                "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
