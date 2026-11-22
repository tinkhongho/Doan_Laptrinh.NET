using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmTrangChu : Form
    {
        private Form formHienTai = null;
        private Button nutDangChon = null;
        private SqlConnection cn;

        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            cn = KetNoiDB.TaoKetNoi();

            string hoTen = string.IsNullOrEmpty(KetNoiDB.HoTen) ? "Quản trị viên" : KetNoiDB.HoTen;
            string quyen = string.IsNullOrEmpty(KetNoiDB.QuyenHan) ? "Quản lý" : KetNoiDB.QuyenHan;

            lblNguoiDungDangNhap.Text = hoTen;
            lblTrangThaiNguoiDung.Text = "Người dùng: " + hoTen + "  |  Quyền hạn: " + quyen + "  |  CSDL: QLVTNN";

            // Phân quyền: Nhân viên không được mở màn hình Thống kê Doanh thu chi tiết
            if (KetNoiDB.QuyenHan == "Nhân viên")
            {
                mnuThongKeDoanhThu.Enabled = false;
                btnMoThongKeDoanhThu.Enabled = false;
            }

            NapThongKeTrangChu();
        }

        /// <summary>
        /// Tải số liệu Thống kê Doanh thu, Lợi nhuận, Công nợ và Tồn kho trực tiếp từ SQL Server lên Trang chủ
        /// </summary>
        private void NapThongKeTrangChu()
        {
            try
            {
                KetNoiDB.MoKetNoi(cn);

                // 1. Thống kê Tổng doanh thu, Thực thu lúc bán và Số hóa đơn
                SqlCommand cmdDoanhThu = new SqlCommand(
                    "SELECT COUNT(*), ISNULL(SUM(tongtien), 0), ISNULL(SUM(dathanhtoan), 0) FROM hoadon", cn);
                SqlDataReader drDT = cmdDoanhThu.ExecuteReader();
                if (drDT.Read())
                {
                    int soHD = Convert.ToInt32(drDT[0]);
                    decimal tongDoanhThu = Convert.ToDecimal(drDT[1]);
                    decimal thucThuBan = Convert.ToDecimal(drDT[2]);

                    lblSoDoanhThu.Text = string.Format("{0:N0} đ", tongDoanhThu);
                    lblGhiChuDoanhThu.Text = string.Format("Từ {0} hóa đơn  |  Thu ngay: {1:N0} đ", soHD, thucThuBan);
                }
                drDT.Close();

                // 2. Thống kê Lợi nhuận gộp = SUM((dongiaban - giavon) * soluong) + Tiền thu nợ
                SqlCommand cmdLoiNhuan = new SqlCommand(
                    "SELECT ISNULL(SUM((dongiaban - giavon) * soluong), 0) FROM ct_hoadon", cn);
                decimal loiNhuanGop = Convert.ToDecimal(cmdLoiNhuan.ExecuteScalar());

                SqlCommand cmdThuNo = new SqlCommand(
                    "SELECT ISNULL(SUM(sotien), 0) FROM phieuthutrano WHERE loaiphieu = N'Thu nợ khách'", cn);
                decimal tienThuNo = Convert.ToDecimal(cmdThuNo.ExecuteScalar());

                lblSoLoiNhuan.Text = string.Format("{0:N0} đ", loiNhuanGop);
                lblGhiChuLoiNhuan.Text = string.Format("Đã thu hồi nợ khách: {0:N0} đ", tienThuNo);

                // 3. Thống kê Tổng Công nợ Khách hàng (Phải thu)
                SqlCommand cmdCongNoKH = new SqlCommand(
                    "SELECT COUNT(*), ISNULL(SUM(sonohientai), 0) FROM khachhang WHERE sonohientai > 0", cn);
                SqlDataReader drKH = cmdCongNoKH.ExecuteReader();
                if (drKH.Read())
                {
                    int soKhachNo = Convert.ToInt32(drKH[0]);
                    decimal tongNoKhach = Convert.ToDecimal(drKH[1]);
                    lblSoCongNoKhach.Text = string.Format("{0:N0} đ", tongNoKhach);
                    lblGhiChuCongNoKhach.Text = string.Format("Hiện có {0} khách hàng đang nợ", soKhachNo);
                }
                drKH.Close();

                // 4. Thống kê Công nợ Nhà cung cấp (Phải trả) & Số mặt hàng tồn kho
                SqlCommand cmdNoNCC = new SqlCommand(
                    "SELECT ISNULL(SUM(congnoncc), 0) FROM nhacungcap", cn);
                decimal tongNoNCC = Convert.ToDecimal(cmdNoNCC.ExecuteScalar());

                SqlCommand cmdHangHoa = new SqlCommand(
                    "SELECT COUNT(*), ISNULL(SUM(soluongton), 0) FROM hanghoa", cn);
                SqlDataReader drHH = cmdHangHoa.ExecuteReader();
                if (drHH.Read())
                {
                    int soMatHang = Convert.ToInt32(drHH[0]);
                    int tongTon = Convert.ToInt32(drHH[1]);
                    lblSoTonKho.Text = string.Format("{0:N0} đ", tongNoNCC);
                    lblGhiChuTonKho.Text = string.Format("Kho: {0} mặt hàng ({1:N0} đơn vị tồn)", soMatHang, tongTon);
                }
                drHH.Close();

                // 5. Bảng bên trái: Danh sách Hóa đơn & Doanh thu gần đây
                string sqlHoaDon = @"SELECT TOP 10 h.mahd, h.ngayban, k.tenkh, h.tongtien, h.dathanhtoan, h.conno
                                     FROM hoadon h
                                     LEFT JOIN khachhang k ON h.makh = k.makh
                                     ORDER BY h.ngayban DESC, h.mahd DESC";
                SqlDataAdapter daHD = new SqlDataAdapter(sqlHoaDon, cn);
                DataTable dtHD = new DataTable();
                daHD.Fill(dtHD);
                dgvHoaDonGanDay.DataSource = dtHD;

                if (dgvHoaDonGanDay.Columns.Count > 0)
                {
                    dgvHoaDonGanDay.Columns["mahd"].HeaderText = "Mã HĐ";
                    dgvHoaDonGanDay.Columns["ngayban"].HeaderText = "Ngày bán";
                    dgvHoaDonGanDay.Columns["tenkh"].HeaderText = "Khách hàng";
                    dgvHoaDonGanDay.Columns["tongtien"].HeaderText = "Tổng tiền";
                    dgvHoaDonGanDay.Columns["dathanhtoan"].HeaderText = "Đã thu";
                    dgvHoaDonGanDay.Columns["conno"].HeaderText = "Còn nợ";

                    dgvHoaDonGanDay.Columns["tongtien"].DefaultCellStyle.Format = "N0";
                    dgvHoaDonGanDay.Columns["dathanhtoan"].DefaultCellStyle.Format = "N0";
                    dgvHoaDonGanDay.Columns["conno"].DefaultCellStyle.Format = "N0";
                }

                // 6. Bảng bên phải: Danh sách Khách hàng đang nợ
                string sqlKhachNo = @"SELECT makh, tenkh, sdt, sonohientai, hanmucno
                                      FROM khachhang
                                      WHERE sonohientai > 0
                                      ORDER BY sonohientai DESC";
                SqlDataAdapter daKH = new SqlDataAdapter(sqlKhachNo, cn);
                DataTable dtKH = new DataTable();
                daKH.Fill(dtKH);
                dgvKhachHangDangNo.DataSource = dtKH;

                if (dgvKhachHangDangNo.Columns.Count > 0)
                {
                    dgvKhachHangDangNo.Columns["makh"].HeaderText = "Mã KH";
                    dgvKhachHangDangNo.Columns["tenkh"].HeaderText = "Tên khách hàng";
                    dgvKhachHangDangNo.Columns["sdt"].HeaderText = "SĐT";
                    dgvKhachHangDangNo.Columns["sonohientai"].HeaderText = "Đang nợ";
                    dgvKhachHangDangNo.Columns["hanmucno"].HeaderText = "Hạn mức nợ";

                    dgvKhachHangDangNo.Columns["sonohientai"].DefaultCellStyle.Format = "N0";
                    dgvKhachHangDangNo.Columns["hanmucno"].DefaultCellStyle.Format = "N0";
                }
            }
            catch
            {
                // Nếu chưa chạy script SQL thì giữ giá trị mặc định trên giao diện
            }
        }

        private void btnLamMoiThongKe_Click(object sender, EventArgs e)
        {
            NapThongKeTrangChu();
        }

        /// <summary>
        /// Đổi màu nút đang được chọn trên thanh Menu bên trái
        /// </summary>
        private void DanhDauNutMenuTrai(Button btnMoi)
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
        /// Nhúng Form con hiển thị gọn ngay trong khung pnlNoiDungChinh
        /// </summary>
        private void MoFormCon(Form fCon, string tieuDe, Button btnMenu)
        {
            if (formHienTai != null)
            {
                formHienTai.Close();
            }

            DanhDauNutMenuTrai(btnMenu);
            formHienTai = fCon;
            lblTieuDeChucNang.Text = tieuDe.ToUpper();
            pnlThongKeTrangChu.Visible = false;
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

            pnlNoiDungChinh.Controls.Add(fCon);
            pnlNoiDungChinh.Tag = fCon;
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

            DanhDauNutMenuTrai(null);
            lblTieuDeChucNang.Text = "TRANG CHỦ - THỐNG KÊ TỔNG QUAN DOANH THU && CÔNG NỢ";
            btnVeTrangChu.Visible = false;
            pnlThongKeTrangChu.Visible = true;
            pnlThongKeTrangChu.BringToFront();
            NapThongKeTrangChu();
        }

        private void btnVeTrangChu_Click(object sender, EventArgs e)
        {
            HienThiTrangChu();
        }

        // --- 1. NHÓM QUẢN LÝ HÀNG HÓA (TÍN) ---
        private void mnuLoaiHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmLoaiHang(), "1.1. Danh mục Loại vật tư nông nghiệp", btnMoLoaiHang);
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmHangHoa(), "1.2. Quản lý Hàng hóa && Tồn kho", btnMoHangHoa);
        }

        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmNhapHang(), "1.3. Lập phiếu Nhập hàng từ Nhà cung cấp", btnMoNhapHang);
        }

        // --- 2. NHÓM BÁN HÀNG & DOANH THU (THIỆN) ---
        private void mnuBanHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmBanHang(), "2.1. Lập Hóa đơn Bán hàng (Thanh toán / Ghi nợ mùa vụ)", btnMoBanHang);
        }

        private void mnuDanhSachHoaDon_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmDanhSachHoaDon(), "2.2. Tra cứu Danh sách Hóa đơn đã bán", btnMoDanhSachHoaDon);
        }

        private void mnuThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmThongKeDoanhThu(), "2.3. Thống kê Doanh thu - Thực thu - Lợi nhuận", btnMoThongKeDoanhThu);
        }

        // --- 3. NHÓM QUẢN LÝ CÔNG NỢ (TÂN) ---
        private void mnuKhachHangCongNo_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmKhachHang_CongNo(), "3.1. Quản lý Khách hàng && Công nợ phải thu", btnMoKhachHangCongNo);
        }

        private void mnuNhaCungCapCongNo_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmNhaCungCap_CongNo(), "3.2. Quản lý Nhà cung cấp && Công nợ phải trả", btnMoNhaCungCapCongNo);
        }

        private void mnuThuTraNo_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmThuTraNo(), "3.3. Lập phiếu Thu nợ khách hàng / Trả nợ NCC", btnMoThuTraNo);
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
