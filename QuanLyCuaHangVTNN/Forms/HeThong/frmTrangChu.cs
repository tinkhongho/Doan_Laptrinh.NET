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
            lblTrangThaiThoiGian.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            if (KetNoiDB.QuyenHan == "Nhân viên")
            {
                mnuThongKeDoanhThu.Enabled = false;
                btnMoThongKeDoanhThu.Enabled = false;
            }

            NapThongKeTrangChu();
        }

        /// <summary>
        /// Tổng hợp số liệu doanh thu, lợi nhuận, công nợ và tồn kho hiển thị lên trang chủ.
        /// </summary>
        private void NapThongKeTrangChu()
        {
            try
            {
                KetNoiDB.MoKetNoi(cn);

                SqlCommand cmdDoanhThu = new SqlCommand(
                    "SELECT COUNT(*), ISNULL(SUM(tongtien), 0), ISNULL(SUM(dathanhtoan), 0) FROM hoadon", cn);
                SqlDataReader drDT = cmdDoanhThu.ExecuteReader();
                if (drDT.Read())
                {
                    int soHD = Convert.ToInt32(drDT[0]);
                    decimal tongDoanhThu = Convert.ToDecimal(drDT[1]);
                    decimal thucThuBan = Convert.ToDecimal(drDT[2]);

                    lblSoDoanhThu.Text = string.Format("{0:N0} đ", tongDoanhThu);
                    lblGhiChuDoanhThu.Text = string.Format("{0} hóa đơn | Thu: {1:N0} đ", soHD, thucThuBan);
                }
                drDT.Close();

                SqlCommand cmdLoiNhuan = new SqlCommand(
                    "SELECT ISNULL(SUM((dongiaban - giavon) * soluong), 0) FROM ct_hoadon", cn);
                decimal loiNhuanGop = Convert.ToDecimal(cmdLoiNhuan.ExecuteScalar());

                SqlCommand cmdThuNo = new SqlCommand(
                    "SELECT ISNULL(SUM(sotien), 0) FROM phieuthutrano WHERE loaiphieu = N'Thu nợ khách'", cn);
                decimal tienThuNo = Convert.ToDecimal(cmdThuNo.ExecuteScalar());

                lblSoLoiNhuan.Text = string.Format("{0:N0} đ", loiNhuanGop);
                lblGhiChuLoiNhuan.Text = string.Format("Thu nợ khách: {0:N0} đ", tienThuNo);

                SqlCommand cmdCongNoKH = new SqlCommand(
                    "SELECT COUNT(*), ISNULL(SUM(sonohientai), 0) FROM khachhang WHERE sonohientai > 0", cn);
                SqlDataReader drKH = cmdCongNoKH.ExecuteReader();
                if (drKH.Read())
                {
                    int soKhachNo = Convert.ToInt32(drKH[0]);
                    decimal tongNoKhach = Convert.ToDecimal(drKH[1]);
                    lblSoCongNoKhach.Text = string.Format("{0:N0} đ", tongNoKhach);
                    lblGhiChuCongNoKhach.Text = string.Format("Hiện có {0} khách đang nợ", soKhachNo);
                }
                drKH.Close();

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
                    lblGhiChuTonKho.Text = string.Format("Kho: {0} mặt hàng ({1:N0} SP)", soMatHang, tongTon);
                }
                drHH.Close();

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
                    dgvHoaDonGanDay.Columns["mahd"].FillWeight = 62;

                    dgvHoaDonGanDay.Columns["ngayban"].HeaderText = "Ngày bán";
                    dgvHoaDonGanDay.Columns["ngayban"].FillWeight = 82;
                    dgvHoaDonGanDay.Columns["ngayban"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    dgvHoaDonGanDay.Columns["tenkh"].HeaderText = "Khách hàng";
                    dgvHoaDonGanDay.Columns["tenkh"].FillWeight = 135;

                    dgvHoaDonGanDay.Columns["tongtien"].HeaderText = "Tổng tiền";
                    dgvHoaDonGanDay.Columns["tongtien"].FillWeight = 88;
                    dgvHoaDonGanDay.Columns["tongtien"].DefaultCellStyle.Format = "N0";

                    dgvHoaDonGanDay.Columns["dathanhtoan"].HeaderText = "Đã thu";
                    dgvHoaDonGanDay.Columns["dathanhtoan"].FillWeight = 85;
                    dgvHoaDonGanDay.Columns["dathanhtoan"].DefaultCellStyle.Format = "N0";

                    dgvHoaDonGanDay.Columns["conno"].HeaderText = "Còn nợ";
                    dgvHoaDonGanDay.Columns["conno"].FillWeight = 85;
                    dgvHoaDonGanDay.Columns["conno"].DefaultCellStyle.Format = "N0";
                }

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
                    dgvKhachHangDangNo.Columns["makh"].HeaderText = "Mã";
                    dgvKhachHangDangNo.Columns["makh"].FillWeight = 48;

                    dgvKhachHangDangNo.Columns["tenkh"].HeaderText = "Khách hàng";
                    dgvKhachHangDangNo.Columns["tenkh"].FillWeight = 135;

                    dgvKhachHangDangNo.Columns["sdt"].HeaderText = "SĐT";
                    dgvKhachHangDangNo.Columns["sdt"].FillWeight = 90;

                    dgvKhachHangDangNo.Columns["sonohientai"].HeaderText = "Đang nợ";
                    dgvKhachHangDangNo.Columns["sonohientai"].FillWeight = 88;
                    dgvKhachHangDangNo.Columns["sonohientai"].DefaultCellStyle.Format = "N0";

                    dgvKhachHangDangNo.Columns["hanmucno"].HeaderText = "Hạn mức";
                    dgvKhachHangDangNo.Columns["hanmucno"].FillWeight = 88;
                    dgvKhachHangDangNo.Columns["hanmucno"].DefaultCellStyle.Format = "N0";
                }
            }
            catch
            {
            }
        }

        private void btnLamMoiThongKe_Click(object sender, EventArgs e)
        {
            NapThongKeTrangChu();
        }

        private void btnThayDoiPanel_Click(object sender, EventArgs e)
        {
            bool dangHienHoaDon = pnlKhungHoaDonGanDay.Visible;

            pnlKhungHoaDonGanDay.Visible = !dangHienHoaDon;
            lblTieuDeBangHoaDon.Visible = !dangHienHoaDon;
            pnlKhungKhachHangNo.Visible = dangHienHoaDon;
            lblTieuDeBangCongNo.Visible = dangHienHoaDon;
        }

        /// <summary>
        /// Đổi màu làm nổi bật nút chức năng đang chọn trên thanh menu trái.
        /// </summary>
        private void DanhDauNutMenuTrai(Button btnMoi)
        {
            if (nutDangChon != null)
            {
                nutDangChon.BackColor = Color.Transparent;
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
        /// Nhúng và hiển thị Form con bên trong vùng nội dung chính.
        /// </summary>
        private void MoFormCon(Form fCon, string tieuDe, Button btnMenu)
        {
            Form formCu = formHienTai;
            formHienTai = null;
            if (formCu != null)
            {
                formCu.Close();
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

        /// <summary>
        /// Đóng Form con hiện tại và quay về màn hình thống kê trang chủ.
        /// </summary>
        private void HienThiTrangChu()
        {
            if (formHienTai != null)
            {
                Form tmp = formHienTai;
                formHienTai = null;
                tmp.Close();
            }

            DanhDauNutMenuTrai(null);
            lblTieuDeChucNang.Text = "TRANG CHỦ - THỐNG KÊ TỔNG QUAN DOANH THU VÀ CÔNG NỢ";
            btnVeTrangChu.Visible = false;
            pnlThongKeTrangChu.Visible = true;
            pnlThongKeTrangChu.BringToFront();
            NapThongKeTrangChu();
        }

        private void btnVeTrangChu_Click(object sender, EventArgs e)
        {
            HienThiTrangChu();
        }

        private void mnuLoaiHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmLoaiHang(), "Danh mục Loại vật tư nông nghiệp", btnMoLoaiHang);
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmHangHoa(), "Quản lý Hàng hóa và Tồn kho", btnMoHangHoa);
        }

        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmNhapHang(), "Lập phiếu Nhập hàng từ Nhà cung cấp", btnMoNhapHang);
        }

        private void mnuBanHang_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmBanHang(), "Lập Hóa đơn Bán hàng (Thanh toán / Ghi nợ mùa vụ)", btnMoBanHang);
        }

        private void mnuDanhSachHoaDon_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmDanhSachHoaDon(), "Tra cứu Danh sách Hóa đơn đã bán", btnMoDanhSachHoaDon);
        }

        private void mnuThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmThongKeDoanhThu(), "Thống kê Doanh thu - Thực thu - Lợi nhuận", btnMoThongKeDoanhThu);
        }

        private void mnuKhachHangCongNo_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmKhachHang_CongNo(), "Quản lý Khách hàng và Công nợ phải thu", btnMoKhachHangCongNo);
        }

        private void mnuNhaCungCapCongNo_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmNhaCungCap_CongNo(), "Quản lý Nhà cung cấp và Công nợ phải trả", btnMoNhaCungCapCongNo);
        }

        private void mnuThuTraNo_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmThuTraNo(), "Lập phiếu Thu nợ khách hàng / Trả nợ NCC", btnMoThuTraNo);
        }

        private void tmrDongHo_Tick(object sender, EventArgs e)
        {
            lblTrangThaiThoiGian.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
