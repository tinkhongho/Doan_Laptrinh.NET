using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmHangHoa : Form
    {
        private DataTable dtHangHoa = new DataTable();
        private bool isThem = false;

        public frmHangHoa()
        {
            InitializeComponent();
            txtMaHH.CharacterCasing = CharacterCasing.Upper;
            dgvHangHoa.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Bật tắt các điều khiển nhập liệu và nút chức năng.
        /// </summary>
        private void SetTrangThai(bool dangThaoTac)
        {
            txtMaHH.Enabled = dangThaoTac && isThem;
            txtTenHH.Enabled = dangThaoTac;
            cboLoaiHang.Enabled = dangThaoTac;
            txtDVT.Enabled = dangThaoTac;
            txtHoatChat.Enabled = dangThaoTac;
            dtpHanSuDung.Enabled = dangThaoTac;
            txtGiaVon.Enabled = dangThaoTac;
            txtGiaBan.Enabled = dangThaoTac;
            txtSoLuongTon.Enabled = dangThaoTac;

            bool coDuLieu = dgvHangHoa.Rows.Count > 0;
            btnThem.Enabled = !dangThaoTac;
            btnSua.Enabled = !dangThaoTac && coDuLieu;
            btnXoa.Enabled = !dangThaoTac && coDuLieu;

            btnLuu.Enabled = dangThaoTac;
            btnHuy.Enabled = dangThaoTac;

            dgvHangHoa.Enabled = !dangThaoTac;
            txtTimKiem.Enabled = !dangThaoTac;
            cboLocTrangThai.Enabled = !dangThaoTac;
            btnTimKiem.Enabled = !dangThaoTac;
            btnLamMoi.Enabled = !dangThaoTac;
        }

        /// <summary>
        /// Làm trống các ô nhập thông tin hàng hóa.
        /// </summary>
        private void XoaTrangTextBox()
        {
            txtMaHH.Clear();
            txtTenHH.Clear();
            cboLoaiHang.SelectedIndex = -1;
            txtDVT.Clear();
            txtHoatChat.Clear();
            dtpHanSuDung.Value = DateTime.Today;
            txtGiaVon.Clear();
            txtGiaBan.Clear();
            txtSoLuongTon.Clear();
        }

        /// <summary>
        /// Hiển thị thông tin mặt hàng đang chọn trên lưới lên vùng nhập liệu.
        /// </summary>
        private void HienThiDongHienTai()
        {
            if (dgvHangHoa.CurrentRow == null || !(dgvHangHoa.CurrentRow.DataBoundItem is DataRowView drv))
            {
                XoaTrangTextBox();
                return;
            }

            txtMaHH.Text = drv["mahh"].ToString();
            txtTenHH.Text = drv["tenhh"].ToString();
            cboLoaiHang.SelectedValue = drv["maloai"] != DBNull.Value ? drv["maloai"] : -1;
            txtDVT.Text = drv["dvt"].ToString();
            txtHoatChat.Text = drv["hoatchat"].ToString();
            dtpHanSuDung.Value = drv["hansudung"] != DBNull.Value
                ? Convert.ToDateTime(drv["hansudung"])
                : DateTime.Today;
            txtGiaVon.Text = drv["giavon"].ToString();
            txtGiaBan.Text = drv["giaban"].ToString();
            txtSoLuongTon.Text = drv["soluongton"].ToString();
        }

        /// <summary>
        /// Nạp danh mục loại hàng vào ComboBox và tải danh sách hàng hóa lên lưới.
        /// </summary>
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = KetNoiDB.TaoKetNoi())
                {
                    SqlDataAdapter daLoai = new SqlDataAdapter("SELECT maloai, tenloai FROM loaihang ORDER BY maloai", conn);
                    DataTable dtLoai = new DataTable();
                    daLoai.Fill(dtLoai);
                    cboLoaiHang.DataSource = dtLoai;
                    cboLoaiHang.DisplayMember = "tenloai";
                    cboLoaiHang.ValueMember = "maloai";

                    string sqlHang = @"SELECT h.mahh, h.tenhh, h.maloai, l.tenloai, h.dvt,
                                              h.hoatchat, h.giavon, h.giaban, h.soluongton, h.hansudung
                                       FROM hanghoa h
                                       LEFT JOIN loaihang l ON h.maloai = l.maloai
                                       ORDER BY h.mahh";
                    SqlDataAdapter daHang = new SqlDataAdapter(sqlHang, conn);
                    dtHangHoa = new DataTable();
                    daHang.Fill(dtHangHoa);

                    colGiaVon.DefaultCellStyle.Format = "N0";
                    colGiaBan.DefaultCellStyle.Format = "N0";
                    colHanSuDung.DefaultCellStyle.Format = "dd/MM/yyyy";

                    dgvHangHoa.DataSource = dtHangHoa;
                    HienThiDongHienTai();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu hàng hóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lọc danh sách hàng hóa theo từ khóa, tồn kho thấp hoặc sắp hết hạn dùng.
        /// </summary>
        private void LocDuLieu()
        {
            if (dtHangHoa == null || dtHangHoa.Columns.Count == 0) return;

            string tuKhoa = txtTimKiem.Text.Trim().Replace("'", "''");
            string dieuKien = "";

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                dieuKien = string.Format("(mahh LIKE '%{0}%' OR tenhh LIKE '%{0}%' OR hoatchat LIKE '%{0}%')", tuKhoa);
            }

            if (cboLocTrangThai.SelectedIndex == 1)
            {
                string dkTon = "soluongton <= 10";
                dieuKien = string.IsNullOrEmpty(dieuKien) ? dkTon : dieuKien + " AND " + dkTon;
            }
            else if (cboLocTrangThai.SelectedIndex == 2)
            {
                string ngayGioiHan = DateTime.Today.AddDays(30).ToString("MM/dd/yyyy");
                string dkHan = "hansudung IS NOT NULL AND hansudung <= #" + ngayGioiHan + "#";
                dieuKien = string.IsNullOrEmpty(dieuKien) ? dkHan : dieuKien + " AND " + dkHan;
            }

            dtHangHoa.DefaultView.RowFilter = dieuKien;
            HienThiDongHienTai();
            SetTrangThai(false);
        }

        private void frm_Load(object sender, EventArgs e)
        {
            LoadData();
            cboLocTrangThai.SelectedIndex = 0;
            SetTrangThai(false);
        }

        private void dgvHangHoa_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHangHoa.Enabled)
            {
                HienThiDongHienTai();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            XoaTrangTextBox();
            SetTrangThai(true);
            txtMaHH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHangHoa.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn vật tư cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isThem = false;
            SetTrangThai(true);
            txtTenHH.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHangHoa.CurrentRow == null || !(dgvHangHoa.CurrentRow.DataBoundItem is DataRowView drv))
            {
                MessageBox.Show("Vui lòng chọn vật tư cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHH = drv["mahh"].ToString();
            string tenHH = drv["tenhh"].ToString();

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa vật tư \"" + tenHH + "\" (" + maHH + ") không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = KetNoiDB.TaoKetNoi())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM hanghoa WHERE mahh = @mahh", conn);
                    cmd.Parameters.AddWithValue("@mahh", maHH);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đã xóa vật tư thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SetTrangThai(false);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể xóa vật tư này vì đã phát sinh dữ liệu trong hóa đơn hoặc phiếu nhập!",
                    "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maHH = txtMaHH.Text.Trim();
            string tenHH = txtTenHH.Text.Trim();
            string dvt = txtDVT.Text.Trim();

            if (string.IsNullOrEmpty(maHH))
            {
                MessageBox.Show("Vui lòng nhập Mã hàng hóa!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHH.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tenHH))
            {
                MessageBox.Show("Vui lòng nhập Tên hàng hóa!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenHH.Focus();
                return;
            }
            if (cboLoaiHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Loại hàng!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiHang.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dvt))
            {
                MessageBox.Show("Vui lòng nhập Đơn vị tính (Bao, Chai, Gói...)!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDVT.Focus();
                return;
            }

            decimal giaVon, giaBan;
            int soLuongTon;

            if (!decimal.TryParse(txtGiaVon.Text.Trim(), out giaVon) || giaVon < 0)
            {
                MessageBox.Show("Giá vốn phải là số lớn hơn hoặc bằng 0!", "Sai định dạng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaVon.Focus();
                return;
            }
            if (!decimal.TryParse(txtGiaBan.Text.Trim(), out giaBan) || giaBan < 0)
            {
                MessageBox.Show("Giá bán phải là số lớn hơn hoặc bằng 0!", "Sai định dạng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }
            if (giaBan < giaVon)
            {
                DialogResult hoiLo = MessageBox.Show(
                    "Giá bán hiện đang thấp hơn giá vốn. Bạn có chắc chắn muốn lưu không?",
                    "Cảnh báo giá bán", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (hoiLo != DialogResult.Yes)
                {
                    txtGiaBan.Focus();
                    return;
                }
            }
            if (!int.TryParse(txtSoLuongTon.Text.Trim(), out soLuongTon) || soLuongTon < 0)
            {
                MessageBox.Show("Số lượng tồn phải là số nguyên lớn hơn hoặc bằng 0!", "Sai định dạng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongTon.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = KetNoiDB.TaoKetNoi())
                {
                    conn.Open();

                    if (isThem)
                    {
                        SqlCommand cmdKiemTra = new SqlCommand(
                            "SELECT COUNT(*) FROM hanghoa WHERE mahh = @mahh", conn);
                        cmdKiemTra.Parameters.AddWithValue("@mahh", maHH);
                        int tonTai = Convert.ToInt32(cmdKiemTra.ExecuteScalar());

                        if (tonTai > 0)
                        {
                            MessageBox.Show("Mã hàng hóa \"" + maHH + "\" đã tồn tại!", "Trùng khóa chính",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaHH.Focus();
                            return;
                        }

                        string sqlInsert = @"INSERT INTO hanghoa (mahh, tenhh, maloai, dvt, hoatchat, giavon, giaban, soluongton, hansudung)
                                             VALUES (@mahh, @tenhh, @maloai, @dvt, @hoatchat, @giavon, @giaban, @soluongton, @hansudung)";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn);
                        cmdInsert.Parameters.AddWithValue("@mahh", maHH);
                        cmdInsert.Parameters.AddWithValue("@tenhh", tenHH);
                        cmdInsert.Parameters.AddWithValue("@maloai", cboLoaiHang.SelectedValue.ToString());
                        cmdInsert.Parameters.AddWithValue("@dvt", dvt);
                        cmdInsert.Parameters.AddWithValue("@hoatchat", txtHoatChat.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@giavon", giaVon);
                        cmdInsert.Parameters.AddWithValue("@giaban", giaBan);
                        cmdInsert.Parameters.AddWithValue("@soluongton", soLuongTon);
                        cmdInsert.Parameters.AddWithValue("@hansudung", dtpHanSuDung.Value.Date);
                        cmdInsert.ExecuteNonQuery();

                        MessageBox.Show("Thêm vật tư mới thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sqlUpdate = @"UPDATE hanghoa
                                             SET tenhh = @tenhh, maloai = @maloai, dvt = @dvt, hoatchat = @hoatchat,
                                                 giavon = @giavon, giaban = @giaban, soluongton = @soluongton, hansudung = @hansudung
                                             WHERE mahh = @mahh";
                        SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
                        cmdUpdate.Parameters.AddWithValue("@mahh", maHH);
                        cmdUpdate.Parameters.AddWithValue("@tenhh", tenHH);
                        cmdUpdate.Parameters.AddWithValue("@maloai", cboLoaiHang.SelectedValue.ToString());
                        cmdUpdate.Parameters.AddWithValue("@dvt", dvt);
                        cmdUpdate.Parameters.AddWithValue("@hoatchat", txtHoatChat.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@giavon", giaVon);
                        cmdUpdate.Parameters.AddWithValue("@giaban", giaBan);
                        cmdUpdate.Parameters.AddWithValue("@soluongton", soLuongTon);
                        cmdUpdate.Parameters.AddWithValue("@hansudung", dtpHanSuDung.Value.Date);
                        cmdUpdate.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật thông tin vật tư thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadData();
                SetTrangThai(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetTrangThai(false);
            HienThiDongHienTai();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LocDuLieu();
        }

        private void cboLocTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocDuLieu();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
            cboLocTrangThai.SelectedIndex = 0;
            SetTrangThai(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChiNhapSo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
