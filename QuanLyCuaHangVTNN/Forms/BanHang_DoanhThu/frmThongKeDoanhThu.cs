using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
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

        /// <summary>
        /// Xác định tên cột ngày lập trên bảng phiếu thu trả nợ.
        /// </summary>
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

        /// <summary>
        /// Tổng hợp doanh thu, thực thu, lợi nhuận gộp và công nợ theo kỳ.
        /// </summary>
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

                string sqlTheoNgay = @"SELECT CAST(h.ngayban AS DATE) AS ngay,
                                              COUNT(h.mahd) AS sohd,
                                              ISNULL(SUM(h.tongtien), 0) AS doanhthu,
                                              ISNULL(SUM(h.dathanhtoan), 0) AS thucthu,
                                              ISNULL(SUM(h.conno), 0) AS conno,
                                              ISNULL(SUM(ln.loinhuan_hd), 0) AS loinhuan
                                       FROM hoadon h
                                       LEFT JOIN (
                                           SELECT mahd, SUM((dongiaban - giavon) * soluong) AS loinhuan_hd
                                           FROM ct_hoadon
                                           GROUP BY mahd
                                       ) ln ON h.mahd = ln.mahd " + dkNgayHD + @"
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

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "Xuất báo cáo Thống kê Doanh thu";
            saveDlg.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            saveDlg.FileName = "BaoCaoDoanhThu_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".txt";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveDlg.FileName, false, Encoding.UTF8))
                    {
                        writer.WriteLine("======================================================================");
                        writer.WriteLine("       BÁO CÁO THỐNG KÊ DOANH THU - THỰC THU - LỢI NHUẬN GỘP");
                        writer.WriteLine("               CỬA HÀNG VẬT TƯ NÔNG NGHIỆP");
                        writer.WriteLine("======================================================================");
                        writer.WriteLine("Thời điểm xuất : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        writer.WriteLine("Người lập      : " + (string.IsNullOrEmpty(KetNoiDB.HoTen) ? "Quản lý" : KetNoiDB.HoTen));
                        writer.WriteLine(lblKyThongKe.Text);
                        writer.WriteLine("----------------------------------------------------------------------");
                        writer.WriteLine("1. TỔNG HỢP CHỈ SỐ TÀI CHÍNH:");
                        writer.WriteLine("   - Tổng doanh thu bán hàng : " + lblSoDoanhThu.Text + " (" + lblGhiChuDoanhThu.Text + ")");
                        writer.WriteLine("   - Tổng tiền thực thu      : " + lblSoThucThu.Text + " (" + lblGhiChuThucThu.Text + ")");
                        writer.WriteLine("   - Lợi nhuận gộp           : " + lblSoLoiNhuan.Text + " (" + lblGhiChuLoiNhuan.Text + ")");
                        writer.WriteLine("   - Công nợ phát sinh       : " + lblSoCongNo.Text);
                        writer.WriteLine("----------------------------------------------------------------------");
                        writer.WriteLine("2. CHI TIẾT DOANH THU THEO NGÀY:");
                        writer.WriteLine(string.Format("{0,-14}{1,8}{2,16}{3,16}{4,16}", "Ngày bán", "Số HĐ", "Doanh thu", "Thực thu", "Lợi nhuận"));
                        foreach (DataGridViewRow r in dgvThongKeTheoNgay.Rows)
                        {
                            if (r.IsNewRow) continue;
                            string ngay = Convert.ToDateTime(r.Cells["ngay"].Value).ToString("dd/MM/yyyy");
                            string sohd = Convert.ToInt32(r.Cells["sohd"].Value).ToString();
                            string dt = Convert.ToDecimal(r.Cells["doanhthu"].Value).ToString("N0");
                            string tt = Convert.ToDecimal(r.Cells["thucthu"].Value).ToString("N0");
                            string ln = Convert.ToDecimal(r.Cells["loinhuan"].Value).ToString("N0");
                            writer.WriteLine(string.Format("{0,-14}{1,8}{2,16}{3,16}{4,16}", ngay, sohd, dt, tt, ln));
                        }
                        writer.WriteLine("----------------------------------------------------------------------");
                        writer.WriteLine("3. THỐNG KÊ THEO MẶT HÀNG VẬT TƯ:");
                        writer.WriteLine(string.Format("{0,-8}{1,-32}{2,8}{3,15}{4,15}", "Mã", "Tên vật tư", "SL bán", "Doanh thu", "Lợi nhuận"));
                        foreach (DataGridViewRow r in dgvTopSanPham.Rows)
                        {
                            if (r.IsNewRow) continue;
                            string ma = Convert.ToString(r.Cells["mahh"].Value);
                            string ten = Convert.ToString(r.Cells["tenhh"].Value);
                            if (ten.Length > 30) ten = ten.Substring(0, 27) + "...";
                            string sl = Convert.ToInt32(r.Cells["tongsl"].Value).ToString("N0");
                            string dt = Convert.ToDecimal(r.Cells["doanhthu"].Value).ToString("N0");
                            string ln = Convert.ToDecimal(r.Cells["loinhuan"].Value).ToString("N0");
                            writer.WriteLine(string.Format("{0,-8}{1,-32}{2,8}{3,15}{4,15}", ma, ten, sl, dt, ln));
                        }
                        writer.WriteLine("======================================================================");
                    }

                    MessageBox.Show("Đã xuất báo cáo thống kê thành công ra file:\n" + saveDlg.FileName,
                        "Xuất báo cáo thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi ghi file báo cáo: " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}