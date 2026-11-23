using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmThuTraNo : Form
    {
        private bool dangTaoMoi = false;
        private bool dangLoadDuLieu = false;
        private DataTable dtKhachHang;
        private DataTable dtNhaCungCap;
        private decimal noHienTai = 0;

        public frmThuTraNo()
        {
            InitializeComponent();
            GanSuKien();
        }

        private void GanSuKien()
        {
            this.Load += frmThuTraNo_Load;

            radThuNoKhach.CheckedChanged += LoaiPhieu_CheckedChanged;
            radTraNoNCC.CheckedChanged += LoaiPhieu_CheckedChanged;
            cboKhachHang.SelectedIndexChanged += DoiTuong_SelectedIndexChanged;
            cboNhaCungCap.SelectedIndexChanged += DoiTuong_SelectedIndexChanged;
            txtSoTien.TextChanged += txtSoTien_TextChanged;

            btnThem.Click += btnThem_Click;
            btnLuu.Click += btnLuu_Click;
            btnXoa.Click += btnXoa_Click;
            btnHuy.Click += btnHuy_Click;
            btnThoat.Click += (s, e) => this.Close();

            cboLocLoaiPhieu.SelectedIndexChanged += (s, e) =>
            {
                if (!dangLoadDuLieu) LoadDanhSachPhieu();
            };
            btnTimKiem.Click += (s, e) => LoadDanhSachPhieu();
            btnLamMoi.Click += btnLamMoi_Click;
            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LoadDanhSachPhieu();
                    e.SuppressKeyPress = true;
                }
            };

            dgvPhieuThuTraNo.CellClick += dgvPhieuThuTraNo_CellClick;
        }

        private void frmThuTraNo_Load(object sender, EventArgs e)
        {
            dangLoadDuLieu = true;

            dgvPhieuThuTraNo.AutoGenerateColumns = false;
            colNgayLap.DefaultCellStyle.Format = "dd/MM/yyyy";
            colSoTien.DefaultCellStyle.Format = "N0";
            colSoTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            cboLocLoaiPhieu.SelectedIndex = 0; // "Tất cả"
            LoadKhachHang();
            LoadNhaCungCap();

            dangLoadDuLieu = false;

            SetTrangThai(false);
            LoadDanhSachPhieu();
        }

        private void SetTrangThai(bool taoMoi)
        {
            dangTaoMoi = taoMoi;

            txtMaPhieu.Enabled = taoMoi;
            dtpNgayLap.Enabled = taoMoi;
            radThuNoKhach.Enabled = taoMoi;
            radTraNoNCC.Enabled = taoMoi;
            cboKhachHang.Enabled = taoMoi && radThuNoKhach.Checked;
            cboNhaCungCap.Enabled = taoMoi && radTraNoNCC.Checked;
            txtSoTien.Enabled = taoMoi;
            txtGhiChu.Enabled = taoMoi;

            btnThem.Enabled = !taoMoi;
            btnLuu.Enabled = taoMoi;
            btnXoa.Enabled = !taoMoi;
            btnHuy.Enabled = taoMoi;

            dgvPhieuThuTraNo.Enabled = !taoMoi;
        }

        private void LoadKhachHang()
        {
            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);
                    string sql = "SELECT makh, tenkh, sonohientai FROM khachhang ORDER BY tenkh";
                    SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                    dtKhachHang = new DataTable();
                    da.Fill(dtKhachHang);

                    cboKhachHang.DataSource = dtKhachHang;
                    cboKhachHang.DisplayMember = "tenkh";
                    cboKhachHang.ValueMember = "makh";
                    cboKhachHang.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhaCungCap()
        {
            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);
                    string sql = "SELECT mancc, tenncc, congnoncc FROM nhacungcap ORDER BY tenncc";
                    SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                    dtNhaCungCap = new DataTable();
                    da.Fill(dtNhaCungCap);

                    cboNhaCungCap.DataSource = dtNhaCungCap;
                    cboNhaCungCap.DisplayMember = "tenncc";
                    cboNhaCungCap.ValueMember = "mancc";
                    cboNhaCungCap.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachPhieu()
        {
            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);
                    string sql = @"SELECT p.maphieu, p.ngaylap, p.loaiphieu,
                                          CASE WHEN p.loaiphieu = N'Thu nợ khách'
                                               THEN ISNULL(kh.tenkh, p.makh)
                                               ELSE ISNULL(ncc.tenncc, p.mancc)
                                          END AS doituong,
                                          p.sotien, p.ghichu, p.makh, p.mancc
                                   FROM phieuthutrano p
                                   LEFT JOIN khachhang kh ON p.makh = kh.makh
                                   LEFT JOIN nhacungcap ncc ON p.mancc = ncc.mancc
                                   WHERE 1 = 1";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    string loaiLoc = cboLocLoaiPhieu.Text;
                    if (loaiLoc == "Thu nợ khách" || loaiLoc == "Trả nợ NCC")
                    {
                        sql += " AND p.loaiphieu = @loaiphieu";
                        cmd.Parameters.AddWithValue("@loaiphieu", loaiLoc);
                    }

                    string kw = txtTimKiem.Text.Trim();
                    if (!string.IsNullOrEmpty(kw))
                    {
                        sql += " AND (p.maphieu LIKE @kw OR kh.tenkh LIKE @kw OR ncc.tenncc LIKE @kw OR p.ghichu LIKE @kw)";
                        cmd.Parameters.AddWithValue("@kw", "%" + kw + "%");
                    }

                    sql += " ORDER BY p.ngaylap DESC, p.maphieu DESC";
                    cmd.CommandText = sql;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPhieuThuTraNo.DataSource = dt;

                    if (dgvPhieuThuTraNo.Rows.Count > 0)
                        HienThiDongDangChon(0);
                    else
                        XoaTrangInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaTrangInput()
        {
            txtMaPhieu.Clear();
            dtpNgayLap.Value = DateTime.Today;
            cboKhachHang.SelectedIndex = -1;
            cboNhaCungCap.SelectedIndex = -1;
            noHienTai = 0;
            txtNoHienTai.Text = "0";
            txtSoTien.Text = "0";
            txtNoConLai.Text = "0";
            txtGhiChu.Clear();
        }

        private void HienThiDongDangChon(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvPhieuThuTraNo.Rows.Count) return;

            dangLoadDuLieu = true;
            DataRowView drv = dgvPhieuThuTraNo.Rows[rowIndex].DataBoundItem as DataRowView;
            if (drv != null)
            {
                txtMaPhieu.Text = Convert.ToString(drv["maphieu"]);
                if (drv["ngaylap"] != DBNull.Value)
                    dtpNgayLap.Value = Convert.ToDateTime(drv["ngaylap"]);

                string loai = Convert.ToString(drv["loaiphieu"]);
                if (loai == "Thu nợ khách")
                {
                    radThuNoKhach.Checked = true;
                    cboNhaCungCap.SelectedIndex = -1;
                    if (drv["makh"] != DBNull.Value)
                        cboKhachHang.SelectedValue = drv["makh"].ToString();
                }
                else
                {
                    radTraNoNCC.Checked = true;
                    cboKhachHang.SelectedIndex = -1;
                    if (drv["mancc"] != DBNull.Value)
                        cboNhaCungCap.SelectedValue = drv["mancc"].ToString();
                }

                decimal soTien = drv["sotien"] != DBNull.Value ? Convert.ToDecimal(drv["sotien"]) : 0;
                txtSoTien.Text = soTien.ToString("N0");
                txtGhiChu.Text = Convert.ToString(drv["ghichu"]);
            }
            dangLoadDuLieu = false;
            CapNhatNoHienTai();
            txtNoConLai.Text = noHienTai.ToString("N0");
        }

        private void dgvPhieuThuTraNo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dangTaoMoi)
                HienThiDongDangChon(e.RowIndex);
        }

        private void LoaiPhieu_CheckedChanged(object sender, EventArgs e)
        {
            if (dangLoadDuLieu) return;

            if (dangTaoMoi)
            {
                cboKhachHang.Enabled = radThuNoKhach.Checked;
                cboNhaCungCap.Enabled = radTraNoNCC.Checked;

                if (radThuNoKhach.Checked)
                {
                    cboNhaCungCap.SelectedIndex = -1;
                    txtMaPhieu.Text = TaoMaPhieuMoi("PT");
                }
                else
                {
                    cboKhachHang.SelectedIndex = -1;
                    txtMaPhieu.Text = TaoMaPhieuMoi("PC");
                }

                CapNhatNoHienTai();
                TinhNoConLai();
            }
        }

        private void DoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangLoadDuLieu) return;
            CapNhatNoHienTai();
            if (dangTaoMoi) TinhNoConLai();
        }

        private void CapNhatNoHienTai()
        {
            noHienTai = 0;

            if (radThuNoKhach.Checked && cboKhachHang.SelectedItem is DataRowView drvKH)
            {
                noHienTai = drvKH["sonohientai"] != DBNull.Value ? Convert.ToDecimal(drvKH["sonohientai"]) : 0;
            }
            else if (radTraNoNCC.Checked && cboNhaCungCap.SelectedItem is DataRowView drvNCC)
            {
                noHienTai = drvNCC["congnoncc"] != DBNull.Value ? Convert.ToDecimal(drvNCC["congnoncc"]) : 0;
            }

            txtNoHienTai.Text = noHienTai.ToString("N0");
        }

        private void txtSoTien_TextChanged(object sender, EventArgs e)
        {
            if (dangTaoMoi)
                TinhNoConLai();
        }

        private void TinhNoConLai()
        {
            decimal soTien = ParseSoTien(txtSoTien.Text);
            decimal conLai = noHienTai - soTien;
            txtNoConLai.Text = conLai.ToString("N0");
        }

        private decimal ParseSoTien(string text)
        {
            string clean = text.Trim().Replace(",", "").Replace(".", "");
            if (decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal kq))
                return kq;
            return 0;
        }

        private string TaoMaPhieuMoi(string tienTo)
        {
            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);
                    string sql = "SELECT TOP 1 maphieu FROM phieuthutrano WHERE maphieu LIKE @prefix ORDER BY LEN(maphieu) DESC, maphieu DESC";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@prefix", tienTo + "%");
                    object kq = cmd.ExecuteScalar();

                    if (kq != null && kq != DBNull.Value)
                    {
                        string maCu = kq.ToString().Trim();
                        string phanSo = maCu.Substring(tienTo.Length);
                        if (int.TryParse(phanSo, out int so))
                            return tienTo + (so + 1).ToString("D3");
                    }
                }
            }
            catch { }
            return tienTo + "001";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            dangLoadDuLieu = true;
            XoaTrangInput();
            radThuNoKhach.Checked = true;
            dangLoadDuLieu = false;

            SetTrangThai(true);
            txtMaPhieu.Text = TaoMaPhieuMoi("PT");
            cboKhachHang.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maPhieu = txtMaPhieu.Text.Trim();
            string loaiPhieu = radThuNoKhach.Checked ? "Thu nợ khách" : "Trả nợ NCC";
            decimal soTien = ParseSoTien(txtSoTien.Text);
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Mã phiếu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            string maKH = null;
            string maNCC = null;

            if (radThuNoKhach.Checked)
            {
                if (cboKhachHang.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKhachHang.Focus();
                    return;
                }
                maKH = cboKhachHang.SelectedValue.ToString();
            }
            else
            {
                if (cboNhaCungCap.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNhaCungCap.Focus();
                    return;
                }
                maNCC = cboNhaCungCap.SelectedValue.ToString();
            }

            if (soTien <= 0)
            {
                MessageBox.Show("Số tiền thu/trả phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTien.Focus();
                return;
            }

            if (soTien > noHienTai)
            {
                MessageBox.Show($"Số tiền thu/trả ({soTien:N0} đ) không được vượt quá nợ hiện tại ({noHienTai:N0} đ)!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTien.Focus();
                return;
            }

            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);

                    // Kiểm tra trùng mã phiếu
                    SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM phieuthutrano WHERE maphieu = @maphieu", cn);
                    cmdCheck.Parameters.AddWithValue("@maphieu", maPhieu);
                    if ((int)cmdCheck.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Mã phiếu này đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaPhieu.Focus();
                        return;
                    }

                    using (SqlTransaction tran = cn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Lưu phiếu thu/trả nợ
                            string sqlInsert = @"INSERT INTO phieuthutrano (maphieu, ngaylap, loaiphieu, makh, mancc, sotien, ghichu)
                                                 VALUES (@maphieu, @ngaylap, @loaiphieu, @makh, @mancc, @sotien, @ghichu)";
                            SqlCommand cmdIns = new SqlCommand(sqlInsert, cn, tran);
                            cmdIns.Parameters.AddWithValue("@maphieu", maPhieu);
                            cmdIns.Parameters.AddWithValue("@ngaylap", dtpNgayLap.Value.Date);
                            cmdIns.Parameters.AddWithValue("@loaiphieu", loaiPhieu);
                            cmdIns.Parameters.AddWithValue("@makh", (object)maKH ?? DBNull.Value);
                            cmdIns.Parameters.AddWithValue("@mancc", (object)maNCC ?? DBNull.Value);
                            cmdIns.Parameters.AddWithValue("@sotien", soTien);
                            cmdIns.Parameters.AddWithValue("@ghichu", ghiChu);
                            cmdIns.ExecuteNonQuery();

                            // 2. Giảm công nợ tương ứng
                            if (radThuNoKhach.Checked)
                            {
                                SqlCommand cmdUpd = new SqlCommand(
                                    "UPDATE khachhang SET sonohientai = sonohientai - @sotien WHERE makh = @makh", cn, tran);
                                cmdUpd.Parameters.AddWithValue("@sotien", soTien);
                                cmdUpd.Parameters.AddWithValue("@makh", maKH);
                                cmdUpd.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmdUpd = new SqlCommand(
                                    "UPDATE nhacungcap SET congnoncc = congnoncc - @sotien WHERE mancc = @mancc", cn, tran);
                                cmdUpd.Parameters.AddWithValue("@sotien", soTien);
                                cmdUpd.Parameters.AddWithValue("@mancc", maNCC);
                                cmdUpd.ExecuteNonQuery();
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("Lưu phiếu và cập nhật công nợ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dangLoadDuLieu = true;
                LoadKhachHang();
                LoadNhaCungCap();
                dangLoadDuLieu = false;

                SetTrangThai(false);
                LoadDanhSachPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhieuThuTraNo.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = dgvPhieuThuTraNo.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            string maPhieu = Convert.ToString(drv["maphieu"]);
            string loaiPhieu = Convert.ToString(drv["loaiphieu"]);
            string maKH = drv["makh"] != DBNull.Value ? Convert.ToString(drv["makh"]) : null;
            string maNCC = drv["mancc"] != DBNull.Value ? Convert.ToString(drv["mancc"]) : null;
            decimal soTien = drv["sotien"] != DBNull.Value ? Convert.ToDecimal(drv["sotien"]) : 0;

            if (MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa phiếu [{maPhieu}]?\nSố tiền {soTien:N0} đ sẽ được cộng hoàn lại vào công nợ.",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);
                    using (SqlTransaction tran = cn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Xóa phiếu
                            SqlCommand cmdDel = new SqlCommand("DELETE FROM phieuthutrano WHERE maphieu = @maphieu", cn, tran);
                            cmdDel.Parameters.AddWithValue("@maphieu", maPhieu);
                            cmdDel.ExecuteNonQuery();

                            // 2. Hoàn lại công nợ tương ứng
                            if (loaiPhieu == "Thu nợ khách" && !string.IsNullOrEmpty(maKH))
                            {
                                SqlCommand cmdUpd = new SqlCommand(
                                    "UPDATE khachhang SET sonohientai = sonohientai + @sotien WHERE makh = @makh", cn, tran);
                                cmdUpd.Parameters.AddWithValue("@sotien", soTien);
                                cmdUpd.Parameters.AddWithValue("@makh", maKH);
                                cmdUpd.ExecuteNonQuery();
                            }
                            else if (loaiPhieu == "Trả nợ NCC" && !string.IsNullOrEmpty(maNCC))
                            {
                                SqlCommand cmdUpd = new SqlCommand(
                                    "UPDATE nhacungcap SET congnoncc = congnoncc + @sotien WHERE mancc = @mancc", cn, tran);
                                cmdUpd.Parameters.AddWithValue("@sotien", soTien);
                                cmdUpd.Parameters.AddWithValue("@mancc", maNCC);
                                cmdUpd.ExecuteNonQuery();
                            }

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("Xóa phiếu và hoàn lại công nợ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dangLoadDuLieu = true;
                LoadKhachHang();
                LoadNhaCungCap();
                dangLoadDuLieu = false;

                LoadDanhSachPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetTrangThai(false);
            if (dgvPhieuThuTraNo.CurrentRow != null)
                HienThiDongDangChon(dgvPhieuThuTraNo.CurrentRow.Index);
            else
                XoaTrangInput();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dangLoadDuLieu = true;
            txtTimKiem.Clear();
            cboLocLoaiPhieu.SelectedIndex = 0;
            LoadKhachHang();
            LoadNhaCungCap();
            dangLoadDuLieu = false;

            LoadDanhSachPhieu();
        }
    }
}
