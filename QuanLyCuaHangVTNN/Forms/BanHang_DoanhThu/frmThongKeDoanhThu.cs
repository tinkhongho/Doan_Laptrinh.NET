using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmThongKeDoanhThu : Form
    {
        private SqlConnection cn;
        private string cotNgayPhieuThu = "";

        public frmThongKeDoanhThu()
        {
            InitializeComponent();
        }

        private void frmThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            cn = KetNoiDB.TaoKetNoi();
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;
            KiemTraCotNgayPhieuThu();
            NapThongKe(false);
        }

        private void KiemTraCotNgayPhieuThu()
        {
            try
            {
                KetNoiDB.MoKetNoi(cn);
                DataTable schema = new DataTable();
                new SqlDataAdapter("SELECT TOP 0 * FROM phieuthutrano", cn).FillSchema(schema, SchemaType.Source);
                if (schema.Columns.Contains("ngaylap")) cotNgayPhieuThu = "ngaylap";
                else if (schema.Columns.Contains("ngaythu")) cotNgayPhieuThu = "ngaythu";
                else if (schema.Columns.Contains("ngayphieu")) cotNgayPhieuThu = "ngayphieu";
            }
            catch
            {
                cotNgayPhieuThu = "";
            }
        }

        private void NapThongKe(bool locTheoNgay)
        {
            try
            {
                KetNoiDB.MoKetNoi(cn);

                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;
                string dkNgayHD = locTheoNgay ? " WHERE CAST(h.ngayban AS DATE) BETWEEN @tu AND @den " : "";

                lblKyThongKe.Text = locTheoNgay
                    ? string.Format("Kỳ: {0:dd/MM/yyyy} - {1:dd/MM/yyyy}", tuNgay, denNgay)
                    : "Kỳ: Toàn bộ dữ liệu";

                // 1. Doanh thu, Thực thu lúc bán, Công nợ phát sinh, Số hóa đơn
                string sqlHD = @"SELECT COUNT(*),
                                        ISNULL(SUM(h.tongtien), 0),
                                        ISNULL(SUM(h.dathanhtoan), 0),
                                        ISNULL(SUM(h.conno), 0)
                                 FROM hoadon h " + dkNgayHD;
                SqlCommand cmdHD = new SqlCommand(sqlHD, cn);
                if (locTheoNgay)
                {
                    cmdHD.Parameters.AddWithValue("@tu", tuNgay);
                    cmdHD.Parameters.AddWithValue("@den", denNgay);
                }

                int soHD = 0;
                decimal tongDoanhThu = 0, thucThuBan = 0, congNoPhatSinh = 0;
                SqlDataReader drHD = cmdHD.ExecuteReader();
                if (drHD.Read())
                {
                    soHD = Convert.ToInt32(drHD[0]);
                    tongDoanhThu = Convert.ToDecimal(drHD[1]);
                    thucThuBan = Convert.ToDecimal(drHD[2]);
                    congNoPhatSinh = Convert.ToDecimal(drHD[3]);
                }
                drHD.Close();

                // 2. Tiền thu nợ khách hàng từ bảng phieuthutrano
                decimal tienThuNo = 0;
                try
                {
                    string sqlThuNo = "SELECT ISNULL(SUM(sotien), 0) FROM phieuthutrano WHERE loaiphieu = N'Thu nợ khách'";
                    if (locTheoNgay && !string.IsNullOrEmpty(cotNgayPhieuThu))
                    {
                        sqlThuNo += string.Format(" AND CAST({0} AS DATE) BETWEEN @tu AND @den", cotNgayPhieuThu);
                    }
                    SqlCommand cmdThuNo = new SqlCommand(sqlThuNo, cn);
                    if (locTheoNgay && !string.IsNullOrEmpty(cotNgayPhieuThu))
                    {
                        cmdThuNo.Parameters.AddWithValue("@tu", tuNgay);
                        cmdThuNo.Parameters.AddWithValue("@den", denNgay);
                    }
                    tienThuNo = Convert.ToDecimal(cmdThuNo.ExecuteScalar());
                }
                catch
                {
                    tienThuNo = 0;
                }

                // 3. Lợi nhuận gộp từ chi tiết hóa đơn
                string sqlLN = @"SELECT ISNULL(SUM((ct.dongiaban - ct.giavon) * ct.soluong), 0)
                                 FROM ct_hoadon ct
                                 INNER JOIN hoadon h ON ct.mahd = h.mahd " + dkNgayHD;
                SqlCommand cmdLN = new SqlCommand(sqlLN, cn);
                if (locTheoNgay)
                {
                    cmdLN.Parameters.AddWithValue("@tu", tuNgay);
                    cmdLN.Parameters.AddWithValue("@den", denNgay);
                }
                decimal loiNhuanGop = Convert.ToDecimal(cmdLN.ExecuteScalar());

                // Hiển thị lên 4 thẻ KPI
                decimal tongThucThu = thucThuBan + tienThuNo;
                decimal tySuatLN = tongDoanhThu > 0 ? (loiNhuanGop / tongDoanhThu) * 100m : 0m;

                lblSoDoanhThu.Text = string.Format("{0:N0} đ", tongDoanhThu);
                lblGhiChuDoanhThu.Text = string.Format("Tổng cộng {0} hóa đơn đã lập", soHD);

                lblSoThucThu.Text = string.Format("{0:N0} đ", tongThucThu);
                lblGhiChuThucThu.Text = string.Format("Bán: {0:N0} đ | Thu nợ: {1:N0} đ", thucThuBan, tienThuNo);

                lblSoLoiNhuan.Text = string.Format("{0:N0} đ", loiNhuanGop);
                lblGhiChuLoiNhuan.Text = string.Format("Tỷ suất lợi nhuận gộp: {0:N1}%", tySuatLN);

                lblSoCongNo.Text = string.Format("{0:N0} đ", congNoPhatSinh);
                lblGhiChuCongNo.Text = "Khách ghi nợ mùa vụ trên hóa đơn";

                // 4. Bảng trái: Thống kê Doanh thu & Lợi nhuận theo từng ngày
                string sqlTheoNgay = @"SELECT CAST(h.ngayban AS DATE) AS ngay,
                                              COUNT(DISTINCT h.mahd) AS sohd,
                                              ISNULL(SUM(h.tongtien), 0) AS doanhthu,
                                              ISNULL(SUM(h.dathanhtoan), 0) AS thucthu,
                                              ISNULL(SUM(h.conno), 0) AS conno,
                                              ISNULL((SELECT SUM((ct.dongiaban - ct.giavon) * ct.soluong)
                                                      FROM ct_hoadon ct
                                                      INNER JOIN hoadon h2 ON ct.mahd = h2.mahd
                                                      WHERE CAST(h2.ngayban AS DATE) = CAST(h.ngayban AS DATE)), 0) AS loinhuan
                                       FROM hoadon h " + dkNgayHD + @"
                                       GROUP BY CAST(h.ngayban AS DATE)
                                       ORDER BY CAST(h.ngayban AS DATE) DESC";

                SqlCommand cmdNgay = new SqlCommand(sqlTheoNgay, cn);
                if (locTheoNgay)
                {
                    cmdNgay.Parameters.AddWithValue("@tu", tuNgay);
                    cmdNgay.Parameters.AddWithValue("@den", denNgay);
                }
                DataTable dtNgay = new DataTable();
                new SqlDataAdapter(cmdNgay).Fill(dtNgay);
                dgvThongKeTheoNgay.DataSource = dtNgay;

                if (dgvThongKeTheoNgay.Columns.Count > 0)
                {
                    dgvThongKeTheoNgay.Columns["ngay"].HeaderText = "Ngày bán";
                    dgvThongKeTheoNgay.Columns["ngay"].FillWeight = 85;
                    dgvThongKeTheoNgay.Columns["ngay"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    dgvThongKeTheoNgay.Columns["sohd"].HeaderText = "Số HĐ";
                    dgvThongKeTheoNgay.Columns["sohd"].FillWeight = 55;

                    dgvThongKeTheoNgay.Columns["doanhthu"].HeaderText = "Doanh thu";
                    dgvThongKeTheoNgay.Columns["doanhthu"].FillWeight = 95;
                    dgvThongKeTheoNgay.Columns["doanhthu"].DefaultCellStyle.Format = "N0";

                    dgvThongKeTheoNgay.Columns["thucthu"].HeaderText = "Thực thu";
                    dgvThongKeTheoNgay.Columns["thucthu"].FillWeight = 90;
                    dgvThongKeTheoNgay.Columns["thucthu"].DefaultCellStyle.Format = "N0";

                    dgvThongKeTheoNgay.Columns["conno"].HeaderText = "Còn nợ";
                    dgvThongKeTheoNgay.Columns["conno"].FillWeight = 85;
                    dgvThongKeTheoNgay.Columns["conno"].DefaultCellStyle.Format = "N0";

                    dgvThongKeTheoNgay.Columns["loinhuan"].HeaderText = "Lợi nhuận";
                    dgvThongKeTheoNgay.Columns["loinhuan"].FillWeight = 90;
                    dgvThongKeTheoNgay.Columns["loinhuan"].DefaultCellStyle.Format = "N0";
                }

                // 5. Bảng phải: Thống kê Doanh thu & Lợi nhuận theo mặt hàng
                string sqlMatHang = @"SELECT ct.mahh,
                                             ISNULL(hh.tenhh, ct.mahh) AS tenhh,
                                             ISNULL(SUM(ct.soluong), 0) AS tongsl,
                                             ISNULL(SUM(ct.soluong * ct.dongiaban), 0) AS doanhthu,
                                             ISNULL(SUM((ct.dongiaban - ct.giavon) * ct.soluong), 0) AS loinhuan
                                      FROM ct_hoadon ct
                                      INNER JOIN hoadon h ON ct.mahd = h.mahd
                                      LEFT JOIN hanghoa hh ON ct.mahh = hh.mahh " + dkNgayHD + @"
                                      GROUP BY ct.mahh, hh.tenhh
                                      ORDER BY doanhthu DESC";

                SqlCommand cmdMH = new SqlCommand(sqlMatHang, cn);
                if (locTheoNgay)
                {
                    cmdMH.Parameters.AddWithValue("@tu", tuNgay);
                    cmdMH.Parameters.AddWithValue("@den", denNgay);
                }
                DataTable dtMH = new DataTable();
                new SqlDataAdapter(cmdMH).Fill(dtMH);
                dgvTopSanPham.DataSource = dtMH;

                if (dgvTopSanPham.Columns.Count > 0)
                {
                    dgvTopSanPham.Columns["mahh"].HeaderText = "Mã";
                    dgvTopSanPham.Columns["mahh"].FillWeight = 55;

                    dgvTopSanPham.Columns["tenhh"].HeaderText = "Tên vật tư";
                    dgvTopSanPham.Columns["tenhh"].FillWeight = 145;

                    dgvTopSanPham.Columns["tongsl"].HeaderText = "SL bán";
                    dgvTopSanPham.Columns["tongsl"].FillWeight = 60;
                    dgvTopSanPham.Columns["tongsl"].DefaultCellStyle.Format = "N0";

                    dgvTopSanPham.Columns["doanhthu"].HeaderText = "Doanh thu";
                    dgvTopSanPham.Columns["doanhthu"].FillWeight = 95;
                    dgvTopSanPham.Columns["doanhthu"].DefaultCellStyle.Format = "N0";

                    dgvTopSanPham.Columns["loinhuan"].HeaderText = "Lợi nhuận";
                    dgvTopSanPham.Columns["loinhuan"].FillWeight = 90;
                    dgvTopSanPham.Columns["loinhuan"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê doanh thu: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NapThongKe(true);
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            NapThongKe(false);
        }
    }
}