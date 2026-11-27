using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmBanHang : Form
    {
        private SqlConnection cn;
        private DataTable dtHangHoa;
        private DataTable dtKhachHang;
        private DataTable dtGioHang;

        private bool laMaHDTuTang = false;
        private bool coCotGhiChuHD = false;
        private string cotNguoiLapHD = "";
        private bool coCotThanhTienCT = false;
        private string cotDVT = "donvitinh";
        private string cotGiaVon = "giavon";
        private string cotGiaBan = "giaban";

        public frmBanHang()
        {
            InitializeComponent();
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            cn = KetNoiDB.TaoKetNoi();
            KhoiTaoBangGioHang();
            KiemTraCauTrucCSDL();
            NapDanhSachKhachHang();
            NapDanhSachHangHoa();
            TaoHoaDonMoi();
        }

        /// <summary>
        /// Khởi tạo bảng tạm chứa danh sách vật tư trong giỏ hàng.
        /// </summary>
        private void KhoiTaoBangGioHang()
        {
            dtGioHang = new DataTable();
            dtGioHang.Columns.Add("mahh", typeof(string));
            dtGioHang.Columns.Add("tenhh", typeof(string));
            dtGioHang.Columns.Add("dvt", typeof(string));
            dtGioHang.Columns.Add("soluong", typeof(int));
            dtGioHang.Columns.Add("dongiaban", typeof(decimal));
            dtGioHang.Columns.Add("giavon", typeof(decimal));
            dtGioHang.Columns.Add("thanhtien", typeof(decimal));

            dgvChiTietHoaDon.DataSource = dtGioHang;

            if (dgvChiTietHoaDon.Columns.Count > 0)
            {
                dgvChiTietHoaDon.Columns["mahh"].HeaderText = "Mã HH";
                dgvChiTietHoaDon.Columns["mahh"].FillWeight = 65;

                dgvChiTietHoaDon.Columns["tenhh"].HeaderText = "Tên vật tư nông nghiệp";
                dgvChiTietHoaDon.Columns["tenhh"].FillWeight = 160;

                dgvChiTietHoaDon.Columns["dvt"].HeaderText = "ĐVT";
                dgvChiTietHoaDon.Columns["dvt"].FillWeight = 55;

                dgvChiTietHoaDon.Columns["soluong"].HeaderText = "SL";
                dgvChiTietHoaDon.Columns["soluong"].FillWeight = 50;

                dgvChiTietHoaDon.Columns["dongiaban"].HeaderText = "Đơn giá bán";
                dgvChiTietHoaDon.Columns["dongiaban"].FillWeight = 85;
                dgvChiTietHoaDon.Columns["dongiaban"].DefaultCellStyle.Format = "N0";

                dgvChiTietHoaDon.Columns["giavon"].Visible = false;

                dgvChiTietHoaDon.Columns["thanhtien"].HeaderText = "Thành tiền";
                dgvChiTietHoaDon.Columns["thanhtien"].FillWeight = 95;
                dgvChiTietHoaDon.Columns["thanhtien"].DefaultCellStyle.Format = "N0";
            }
        }

        /// <summary>
        /// Kiểm tra các cột tùy chọn của bảng hóa đơn trong cơ sở dữ liệu.
        /// </summary>
        private void KiemTraCauTrucCSDL()
        {
            try
            {
                KetNoiDB.MoKetNoi(cn);

                DataTable schemaHD = new DataTable();
                new SqlDataAdapter("SELECT TOP 0 * FROM hoadon", cn).FillSchema(schemaHD, SchemaType.Source);
                if (schemaHD.Columns.Contains("mahd"))
                    laMaHDTuTang = schemaHD.Columns["mahd"].AutoIncrement;
                coCotGhiChuHD = schemaHD.Columns.Contains("ghichu");
                if (schemaHD.Columns.Contains("tendangnhap"))
                    cotNguoiLapHD = "tendangnhap";
                else if (schemaHD.Columns.Contains("nguoilap"))
                    cotNguoiLapHD = "nguoilap";

                DataTable schemaCT = new DataTable();
                new SqlDataAdapter("SELECT TOP 0 * FROM ct_hoadon", cn).FillSchema(schemaCT, SchemaType.Source);
                coCotThanhTienCT = schemaCT.Columns.Contains("thanhtien") && !schemaCT.Columns["thanhtien"].ReadOnly;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối CSDL QLVTNN.\nChi tiết: " + ex.Message,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Tải danh sách khách hàng kèm tùy chọn khách mua lẻ vào ComboBox.
        /// </summary>
        private void NapDanhSachKhachHang()
        {
            try
            {
                KetNoiDB.MoKetNoi(cn);
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM khachhang ORDER BY tenkh", cn);
                DataTable dtGoc = new DataTable();
                da.Fill(dtGoc);

                dtKhachHang = dtGoc.Clone();
                if (dtKhachHang.Columns.Contains("makh"))
                    dtKhachHang.Columns["makh"].AllowDBNull = true;

                DataRow rowKhachLe = dtKhachHang.NewRow();
                rowKhachLe["makh"] = DBNull.Value;
                rowKhachLe["tenkh"] = "-- Khách lẻ (Thanh toán ngay) --";
                if (dtKhachHang.Columns.Contains("sdt")) rowKhachLe["sdt"] = "";
                if (dtKhachHang.Columns.Contains("sonohientai")) rowKhachLe["sonohientai"] = 0m;
                if (dtKhachHang.Columns.Contains("hanmucno")) rowKhachLe["hanmucno"] = 0m;
                dtKhachHang.Rows.Add(rowKhachLe);

                foreach (DataRow r in dtGoc.Rows)
                {
                    dtKhachHang.ImportRow(r);
                }

                cboKhachHang.DataSource = dtKhachHang;
                cboKhachHang.DisplayMember = "tenkh";
                cboKhachHang.ValueMember = "makh";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách khách hàng: " + ex.Message,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh mục vật tư nông nghiệp lên ComboBox chọn hàng.
        /// </summary>
        private void NapDanhSachHangHoa()
        {
            try
            {
                KetNoiDB.MoKetNoi(cn);
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM hanghoa ORDER BY tenhh", cn);
                dtHangHoa = new DataTable();
                da.Fill(dtHangHoa);

                if (dtHangHoa.Columns.Contains("donvitinh")) cotDVT = "donvitinh";
                else if (dtHangHoa.Columns.Contains("dvt")) cotDVT = "dvt";

                if (dtHangHoa.Columns.Contains("giavon")) cotGiaVon = "giavon";
                else if (dtHangHoa.Columns.Contains("gianhap")) cotGiaVon = "gianhap";

                if (dtHangHoa.Columns.Contains("giaban")) cotGiaBan = "giaban";
                else if (dtHangHoa.Columns.Contains("dongiaban")) cotGiaBan = "dongiaban";

                cboHangHoa.DataSource = dtHangHoa;
                cboHangHoa.DisplayMember = "tenhh";
                cboHangHoa.ValueMember = "mahh";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách vật tư: " + ex.Message,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tự động sinh mã hóa đơn bán hàng kế tiếp.
        /// </summary>
        private string SinhMaHoaDonMoi()
        {
            if (laMaHDTuTang) return "(Tự động tăng)";

            try
            {
                KetNoiDB.MoKetNoi(cn);
                SqlCommand cmd = new SqlCommand("SELECT mahd FROM hoadon", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                int maxSo = 0;
                while (dr.Read())
                {
                    string ma = dr[0].ToString().Trim();
                    string phanSo = "";
                    foreach (char c in ma)
                    {
                        if (char.IsDigit(c)) phanSo += c;
                    }
                    int so;
                    if (int.TryParse(phanSo, out so) && so > maxSo)
                        maxSo = so;
                }
                dr.Close();
                return "HD" + (maxSo + 1).ToString("D3");
            }
            catch
            {
                return "HD" + DateTime.Now.ToString("yyMMddHHmm");
            }
        }

        /// <summary>
        /// Làm mới thông tin hóa đơn và xóa trống giỏ hàng để lập đơn mới.
        /// </summary>
        private void TaoHoaDonMoi()
        {
            txtMaHD.Text = SinhMaHoaDonMoi();
            txtMaHD.ReadOnly = laMaHDTuTang;
            dtpNgayBan.Value = DateTime.Now;
            if (cboKhachHang.Items.Count > 0) cboKhachHang.SelectedIndex = 0;
            txtGhiChu.Clear();
            dtGioHang.Rows.Clear();
            numSoLuong.Value = 1;
            chkThanhToanDu.Checked = true;
            CapNhatTongThanhToan();
        }

        private void cboHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedItem is DataRowView drv)
            {
                int ton = drv.Row.Table.Columns.Contains("soluongton") && drv["soluongton"] != DBNull.Value
                    ? Convert.ToInt32(drv["soluongton"]) : 0;
                string dvt = drv.Row.Table.Columns.Contains(cotDVT) && drv[cotDVT] != DBNull.Value
                    ? drv[cotDVT].ToString() : "Cái";
                decimal giaBan = drv.Row.Table.Columns.Contains(cotGiaBan) && drv[cotGiaBan] != DBNull.Value
                    ? Convert.ToDecimal(drv[cotGiaBan]) : 0m;

                lblTonKho.Text = string.Format("Tồn: {0:N0} {1}", ton, dvt);
                lblTonKho.ForeColor = ton > 0 ? Color.FromArgb(46, 125, 50) : Color.FromArgb(198, 40, 40);
                txtDonGiaBan.Text = giaBan.ToString("N0");
                numSoLuong.Value = 1;
            }
        }

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedItem is DataRowView drv)
            {
                bool laKhachLe = (drv["makh"] == DBNull.Value);
                if (laKhachLe)
                {
                    lblThongTinNoKH.Text = "Khách mua lẻ: Thanh toán đủ 100%, không ghi nợ mùa vụ.";
                    lblThongTinNoKH.ForeColor = Color.FromArgb(84, 110, 122);
                    chkThanhToanDu.Checked = true;
                    chkThanhToanDu.Enabled = false;
                }
                else
                {
                    string sdt = drv.Row.Table.Columns.Contains("sdt") ? drv["sdt"].ToString() : "";
                    decimal noCu = drv.Row.Table.Columns.Contains("sonohientai") && drv["sonohientai"] != DBNull.Value
                        ? Convert.ToDecimal(drv["sonohientai"]) : 0m;
                    decimal hanMuc = drv.Row.Table.Columns.Contains("hanmucno") && drv["hanmucno"] != DBNull.Value
                        ? Convert.ToDecimal(drv["hanmucno"]) : 0m;

                    lblThongTinNoKH.Text = string.Format("SĐT: {0} | Nợ cũ: {1:N0} đ\nHạn mức nợ: {2:N0} đ",
                        sdt, noCu, hanMuc);
                    lblThongTinNoKH.ForeColor = noCu > 0 ? Color.FromArgb(230, 81, 0) : Color.FromArgb(27, 94, 32);
                    chkThanhToanDu.Enabled = true;
                }
                CapNhatTongThanhToan();
            }
        }

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            if (!(cboHangHoa.SelectedItem is DataRowView drv))
            {
                MessageBox.Show("Vui lòng chọn vật tư cần bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHH = drv["mahh"].ToString();
            string tenHH = drv["tenhh"].ToString();
            string dvt = drv.Row.Table.Columns.Contains(cotDVT) ? drv[cotDVT].ToString() : "";
            int tonKho = drv.Row.Table.Columns.Contains("soluongton") && drv["soluongton"] != DBNull.Value
                ? Convert.ToInt32(drv["soluongton"]) : 0;
            decimal giaVon = drv.Row.Table.Columns.Contains(cotGiaVon) && drv[cotGiaVon] != DBNull.Value
                ? Convert.ToDecimal(drv[cotGiaVon]) : 0m;

            decimal donGiaBan;
            if (!decimal.TryParse(txtDonGiaBan.Text.Replace(",", "").Replace(".", "").Trim(), out donGiaBan) || donGiaBan < 0)
            {
                MessageBox.Show("Đơn giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaBan.Focus();
                return;
            }

            int soLuongThem = (int)numSoLuong.Value;
            if (soLuongThem <= 0)
            {
                MessageBox.Show("Số lượng bán phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow dongTonTai = null;
            foreach (DataRow r in dtGioHang.Rows)
            {
                if (r["mahh"].ToString() == maHH)
                {
                    dongTonTai = r;
                    break;
                }
            }

            int tongSLDuKien = soLuongThem + (dongTonTai != null ? Convert.ToInt32(dongTonTai["soluong"]) : 0);
            if (tongSLDuKien > tonKho)
            {
                MessageBox.Show(string.Format("Số lượng xuất ({0}) vượt quá tồn kho hiện tại ({1} {2})!",
                    tongSLDuKien, tonKho, dvt), "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dongTonTai != null)
            {
                dongTonTai["soluong"] = tongSLDuKien;
                dongTonTai["dongiaban"] = donGiaBan;
                dongTonTai["thanhtien"] = tongSLDuKien * donGiaBan;
            }
            else
            {
                dtGioHang.Rows.Add(maHH, tenHH, dvt, soLuongThem, donGiaBan, giaVon, soLuongThem * donGiaBan);
            }

            CapNhatTongThanhToan();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng vật tư cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvChiTietHoaDon.Rows.RemoveAt(dgvChiTietHoaDon.CurrentRow.Index);
            CapNhatTongThanhToan();
        }

        /// <summary>
        /// Tính tổng thành tiền của toàn bộ vật tư trong giỏ hàng.
        /// </summary>
        private decimal TinhTongTienHoaDon()
        {
            decimal tong = 0;
            foreach (DataRow r in dtGioHang.Rows)
            {
                tong += Convert.ToDecimal(r["thanhtien"]);
            }
            return tong;
        }

        private void chkThanhToanDu_CheckedChanged(object sender, EventArgs e)
        {
            txtKhachThanhToan.ReadOnly = chkThanhToanDu.Checked;
            CapNhatTongThanhToan();
        }

        private void txtKhachThanhToan_TextChanged(object sender, EventArgs e)
        {
            if (!chkThanhToanDu.Checked)
            {
                CapNhatTongThanhToan();
            }
        }

        /// <summary>
        /// Cập nhật tổng tiền, tiền còn nợ và dư nợ dự kiến sau hóa đơn.
        /// </summary>
        private void CapNhatTongThanhToan()
        {
            decimal tongTien = TinhTongTienHoaDon();
            lblTongTien.Text = string.Format("{0:N0} đ", tongTien);

            decimal daThanhToan = 0;
            if (chkThanhToanDu.Checked)
            {
                daThanhToan = tongTien;
                txtKhachThanhToan.Text = tongTien.ToString("N0");
            }
            else
            {
                decimal.TryParse(txtKhachThanhToan.Text.Replace(",", "").Replace(".", "").Trim(), out daThanhToan);
            }

            decimal conNo = tongTien - daThanhToan;
            if (conNo < 0) conNo = 0;

            lblConNo.Text = string.Format("{0:N0} đ", conNo);
            lblConNo.ForeColor = conNo > 0 ? Color.FromArgb(230, 81, 0) : Color.FromArgb(46, 125, 50);

            if (cboKhachHang.SelectedItem is DataRowView drv && drv["makh"] != DBNull.Value)
            {
                decimal noCu = drv.Row.Table.Columns.Contains("sonohientai") && drv["sonohientai"] != DBNull.Value
                    ? Convert.ToDecimal(drv["sonohientai"]) : 0m;
                decimal hanMuc = drv.Row.Table.Columns.Contains("hanmucno") && drv["hanmucno"] != DBNull.Value
                    ? Convert.ToDecimal(drv["hanmucno"]) : 0m;
                decimal tongNoMoi = noCu + conNo;

                lblDuNoSauBan.Text = string.Format("Tổng nợ sau đơn: {0:N0} đ (Hạn mức: {1:N0} đ)", tongNoMoi, hanMuc);
                lblDuNoSauBan.ForeColor = (hanMuc > 0 && tongNoMoi > hanMuc)
                    ? Color.FromArgb(198, 40, 40)
                    : Color.FromArgb(84, 110, 122);
            }
            else
            {
                lblDuNoSauBan.Text = "Tổng nợ sau đơn: 0 đ (Khách lẻ)";
                lblDuNoSauBan.ForeColor = Color.FromArgb(84, 110, 122);
            }
        }

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            if (dtGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có vật tư nào! Vui lòng thêm hàng hóa trước khi lưu.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!laMaHDTuTang && string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHD.Focus();
                return;
            }

            decimal tongTien = TinhTongTienHoaDon();
            decimal daThanhToan = 0;
            if (!decimal.TryParse(txtKhachThanhToan.Text.Replace(",", "").Replace(".", "").Trim(), out daThanhToan) || daThanhToan < 0)
            {
                MessageBox.Show("Số tiền khách thanh toán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKhachThanhToan.Focus();
                return;
            }

            if (daThanhToan > tongTien)
            {
                MessageBox.Show("Số tiền thanh toán không được lớn hơn tổng tiền hóa đơn!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal conNo = tongTien - daThanhToan;
            DataRowView khachChon = cboKhachHang.SelectedItem as DataRowView;
            object maKH = (khachChon != null) ? khachChon["makh"] : DBNull.Value;

            if (maKH == DBNull.Value && conNo > 0)
            {
                MessageBox.Show("Khách lẻ bắt buộc phải thanh toán đủ 100%!\nNếu ghi nợ mùa vụ, vui lòng chọn khách hàng trong danh bạ.",
                    "Quy định công nợ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (maKH != DBNull.Value && conNo > 0)
            {
                decimal noCu = khachChon.Row.Table.Columns.Contains("sonohientai") && khachChon["sonohientai"] != DBNull.Value
                    ? Convert.ToDecimal(khachChon["sonohientai"]) : 0m;
                decimal hanMuc = khachChon.Row.Table.Columns.Contains("hanmucno") && khachChon["hanmucno"] != DBNull.Value
                    ? Convert.ToDecimal(khachChon["hanmucno"]) : 0m;

                if (hanMuc > 0 && (noCu + conNo) > hanMuc)
                {
                    DialogResult xacNhan = MessageBox.Show(
                        string.Format("Tổng nợ sau hóa đơn ({0:N0} đ) vượt quá hạn mức nợ cho phép ({1:N0} đ)!\nBạn vẫn muốn cho phép ghi nợ hóa đơn này?",
                        noCu + conNo, hanMuc),
                        "Cảnh báo vượt hạn mức nợ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (xacNhan == DialogResult.No) return;
                }
            }

            SqlTransaction tran = null;
            try
            {
                KetNoiDB.MoKetNoi(cn);
                tran = cn.BeginTransaction();

                string maHDThucTe = txtMaHD.Text.Trim();
                if (!laMaHDTuTang)
                {
                    if (string.IsNullOrEmpty(maHDThucTe))
                    {
                        tran.Rollback();
                        tran = null;
                        MessageBox.Show("Vui lòng nhập Mã hóa đơn hoặc bấm 'Làm mới' để sinh mã tự động!",
                            "Thiếu mã hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaHD.Focus();
                        return;
                    }

                    SqlCommand cmdKiemTraMa = new SqlCommand(
                        "SELECT COUNT(*) FROM hoadon WHERE mahd = @mahd", cn, tran);
                    cmdKiemTraMa.Parameters.AddWithValue("@mahd", maHDThucTe);
                    if (Convert.ToInt32(cmdKiemTraMa.ExecuteScalar()) > 0)
                    {
                        tran.Rollback();
                        tran = null;
                        MessageBox.Show("Mã hóa đơn \"" + maHDThucTe + "\" đã tồn tại! Vui lòng nhập mã khác hoặc bấm 'Làm mới'.",
                            "Trùng mã hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaHD.Focus();
                        return;
                    }
                }

                string cotInsert = "ngayban, makh, tongtien, dathanhtoan, conno";
                string valInsert = "@ngayban, @makh, @tongtien, @dathanhtoan, @conno";

                if (!laMaHDTuTang)
                {
                    cotInsert = "mahd, " + cotInsert;
                    valInsert = "@mahd, " + valInsert;
                }
                if (coCotGhiChuHD)
                {
                    cotInsert += ", ghichu";
                    valInsert += ", @ghichu";
                }
                if (!string.IsNullOrEmpty(cotNguoiLapHD))
                {
                    cotInsert += ", " + cotNguoiLapHD;
                    valInsert += ", @nguoilap";
                }

                string sqlHD = laMaHDTuTang
                    ? string.Format("INSERT INTO hoadon ({0}) OUTPUT INSERTED.mahd VALUES ({1})", cotInsert, valInsert)
                    : string.Format("INSERT INTO hoadon ({0}) VALUES ({1})", cotInsert, valInsert);

                SqlCommand cmdHD = new SqlCommand(sqlHD, cn, tran);
                if (!laMaHDTuTang) cmdHD.Parameters.AddWithValue("@mahd", maHDThucTe);
                cmdHD.Parameters.AddWithValue("@ngayban", dtpNgayBan.Value);
                cmdHD.Parameters.AddWithValue("@makh", maKH);
                cmdHD.Parameters.AddWithValue("@tongtien", tongTien);
                cmdHD.Parameters.AddWithValue("@dathanhtoan", daThanhToan);
                cmdHD.Parameters.AddWithValue("@conno", conNo);
                if (coCotGhiChuHD) cmdHD.Parameters.AddWithValue("@ghichu", txtGhiChu.Text.Trim());
                if (!string.IsNullOrEmpty(cotNguoiLapHD))
                    cmdHD.Parameters.AddWithValue("@nguoilap", string.IsNullOrEmpty(KetNoiDB.TenDangNhap) ? "admin" : KetNoiDB.TenDangNhap);

                if (laMaHDTuTang)
                {
                    maHDThucTe = cmdHD.ExecuteScalar().ToString();
                }
                else
                {
                    cmdHD.ExecuteNonQuery();
                }

                foreach (DataRow r in dtGioHang.Rows)
                {
                    string sqlCT = coCotThanhTienCT
                        ? "INSERT INTO ct_hoadon (mahd, mahh, soluong, dongiaban, giavon, thanhtien) VALUES (@mahd, @mahh, @sl, @gia, @von, @tt)"
                        : "INSERT INTO ct_hoadon (mahd, mahh, soluong, dongiaban, giavon) VALUES (@mahd, @mahh, @sl, @gia, @von)";

                    SqlCommand cmdCT = new SqlCommand(sqlCT, cn, tran);
                    cmdCT.Parameters.AddWithValue("@mahd", maHDThucTe);
                    cmdCT.Parameters.AddWithValue("@mahh", r["mahh"].ToString());
                    cmdCT.Parameters.AddWithValue("@sl", Convert.ToInt32(r["soluong"]));
                    cmdCT.Parameters.AddWithValue("@gia", Convert.ToDecimal(r["dongiaban"]));
                    cmdCT.Parameters.AddWithValue("@von", Convert.ToDecimal(r["giavon"]));
                    if (coCotThanhTienCT)
                        cmdCT.Parameters.AddWithValue("@tt", Convert.ToDecimal(r["thanhtien"]));
                    cmdCT.ExecuteNonQuery();

                    SqlCommand cmdKho = new SqlCommand(
                        "UPDATE hanghoa SET soluongton = soluongton - @sl WHERE mahh = @mahh AND ISNULL(soluongton, 0) >= @sl", cn, tran);
                    cmdKho.Parameters.AddWithValue("@sl", Convert.ToInt32(r["soluong"]));
                    cmdKho.Parameters.AddWithValue("@mahh", r["mahh"].ToString());
                    if (cmdKho.ExecuteNonQuery() == 0)
                    {
                        tran.Rollback();
                        tran = null;
                        MessageBox.Show("Vật tư \"" + r["tenhh"].ToString() + "\" không đủ số lượng tồn kho để bán!",
                            "Thiếu tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        NapDanhSachHangHoa();
                        return;
                    }
                }

                if (maKH != DBNull.Value && conNo > 0)
                {
                    SqlCommand cmdNo = new SqlCommand(
                        "UPDATE khachhang SET sonohientai = ISNULL(sonohientai, 0) + @conno WHERE makh = @makh", cn, tran);
                    cmdNo.Parameters.AddWithValue("@conno", conNo);
                    cmdNo.Parameters.AddWithValue("@makh", maKH);
                    cmdNo.ExecuteNonQuery();
                }

                tran.Commit();

                MessageBox.Show(
                    string.Format("Lưu hóa đơn [{0}] thành công!\nTổng tiền: {1:N0} đ | Đã thu: {2:N0} đ | Ghi nợ: {3:N0} đ",
                    maHDThucTe, tongTien, daThanhToan, conNo),
                    "Hoàn tất bán hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                NapDanhSachKhachHang();
                NapDanhSachHangHoa();
                TaoHoaDonMoi();
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message,
                    "Lỗi giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            NapDanhSachKhachHang();
            NapDanhSachHangHoa();
            TaoHoaDonMoi();
        }

        private void mnuXoaMatHangGio_Click(object sender, EventArgs e)
        {
            btnXoaDong_Click(sender, e);
        }

        private void mnuXoaTatCaGio_Click(object sender, EventArgs e)
        {
            if (dtGioHang.Rows.Count == 0) return;

            DialogResult kq = MessageBox.Show("Bạn có muốn xóa toàn bộ vật tư trong giỏ hàng hiện tại không?",
                "Xác nhận làm trống giỏ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                dtGioHang.Rows.Clear();
                CapNhatTongThanhToan();
            }
        }
    }
}