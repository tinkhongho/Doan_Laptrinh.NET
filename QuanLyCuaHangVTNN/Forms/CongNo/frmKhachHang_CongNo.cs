using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmKhachHang_CongNo : Form
    {
        private string trangThai = "";

        public frmKhachHang_CongNo()
        {
            InitializeComponent();
        }

        private void frmKhachHang_CongNo_Load(object sender, EventArgs e)
        {
            dgvKhachHang.AutoGenerateColumns = false;
            colHanMucNo.DefaultCellStyle.Format = "N0";
            colHanMucNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoNoHienTai.DefaultCellStyle.Format = "N0";
            colSoNoHienTai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            SetTrangThai(false);
            LoadDanhSach();
        }

        /// <summary>
        /// Thiết lập trạng thái đóng mở của các ô nhập liệu và nút lệnh.
        /// </summary>
        private void SetTrangThai(bool dangNhapLieu)
        {
            txtMaKH.Enabled = (trangThai == "THEM");
            txtTenKH.Enabled = dangNhapLieu;
            txtSDT.Enabled = dangNhapLieu;
            txtDiaChi.Enabled = dangNhapLieu;
            txtHanMucNo.Enabled = dangNhapLieu;

            btnThem.Enabled = !dangNhapLieu;
            btnSua.Enabled = !dangNhapLieu;
            btnXoa.Enabled = !dangNhapLieu;
            btnLuu.Enabled = dangNhapLieu;
            btnHuy.Enabled = dangNhapLieu;

            dgvKhachHang.Enabled = !dangNhapLieu;
        }

        /// <summary>
        /// Xóa trắng thông tin trên vùng nhập liệu khách hàng.
        /// </summary>
        private void XoaTrangInput()
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtHanMucNo.Text = "20000000";
            txtSoNoHienTai.Text = "0";
        }

        /// <summary>
        /// Tải danh sách khách hàng và tính tổng công nợ phải thu.
        /// </summary>
        private void LoadDanhSach()
        {
            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);
                    string sql = @"SELECT makh, tenkh, sdt, diachi, hanmucno, sonohientai
                                   FROM khachhang
                                   WHERE 1 = 1";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    if (chkChiHienThiDangNo.Checked)
                    {
                        sql += " AND sonohientai > 0";
                    }

                    string kw = txtTimKiem.Text.Trim();
                    if (!string.IsNullOrEmpty(kw))
                    {
                        sql += " AND (makh LIKE @kw OR tenkh LIKE @kw OR sdt LIKE @kw OR diachi LIKE @kw)";
                        cmd.Parameters.AddWithValue("@kw", "%" + kw + "%");
                    }

                    sql += " ORDER BY makh";
                    cmd.CommandText = sql;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvKhachHang.DataSource = dt;

                    decimal tongNo = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["sonohientai"] != DBNull.Value)
                            tongNo += Convert.ToDecimal(row["sonohientai"]);
                    }
                    lblTongCongNo.Text = string.Format("Tổng công nợ phải thu: {0:N0} đ", tongNo);

                    if (dgvKhachHang.Rows.Count > 0)
                        HienThiDongDangChon(0);
                    else
                        XoaTrangInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị thông tin dòng khách hàng đang chọn lên các ô nhập.
        /// </summary>
        private void HienThiDongDangChon(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvKhachHang.Rows.Count) return;

            DataGridViewRow r = dgvKhachHang.Rows[rowIndex];
            txtMaKH.Text = Convert.ToString(r.Cells["colMaKH"].Value);
            txtTenKH.Text = Convert.ToString(r.Cells["colTenKH"].Value);
            txtSDT.Text = Convert.ToString(r.Cells["colSDT"].Value);
            txtDiaChi.Text = Convert.ToString(r.Cells["colDiaChi"].Value);

            decimal hanMuc = r.Cells["colHanMucNo"].Value != DBNull.Value ? Convert.ToDecimal(r.Cells["colHanMucNo"].Value) : 0;
            decimal soNo = r.Cells["colSoNoHienTai"].Value != DBNull.Value ? Convert.ToDecimal(r.Cells["colSoNoHienTai"].Value) : 0;

            txtHanMucNo.Text = hanMuc.ToString("0");
            txtSoNoHienTai.Text = soNo.ToString("N0");
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                HienThiDongDangChon(e.RowIndex);
        }

        /// <summary>
        /// Sinh tự động mã khách hàng kế tiếp.
        /// </summary>
        private string TaoMaKHMoi()
        {
            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);
                    string sql = "SELECT TOP 1 makh FROM khachhang WHERE makh LIKE 'KH%' ORDER BY LEN(makh) DESC, makh DESC";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    object kq = cmd.ExecuteScalar();

                    if (kq != null && kq != DBNull.Value)
                    {
                        string maCu = kq.ToString().Trim();
                        string phanSo = maCu.Substring(2);
                        if (int.TryParse(phanSo, out int so))
                            return "KH" + (so + 1).ToString("D2");
                    }
                }
            }
            catch { }
            return "KH01";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            trangThai = "THEM";
            XoaTrangInput();
            txtMaKH.Text = TaoMaKHMoi();
            SetTrangThai(true);
            txtTenKH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            trangThai = "SUA";
            SetTrangThai(true);
            txtTenKH.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string tenKH = txtTenKH.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Mã khách hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tenKH))
            {
                MessageBox.Show("Tên khách hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }

            string strHanMuc = txtHanMucNo.Text.Trim().Replace(",", "").Replace(".", "");
            if (!decimal.TryParse(strHanMuc, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal hanMuc) || hanMuc < 0)
            {
                MessageBox.Show("Hạn mức nợ phải là số không âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHanMucNo.Focus();
                return;
            }

            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);

                    if (trangThai == "THEM")
                    {
                        SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM khachhang WHERE makh = @makh", cn);
                        cmdCheck.Parameters.AddWithValue("@makh", maKH);
                        if ((int)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Mã khách hàng này đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaKH.Focus();
                            return;
                        }

                        string sqlInsert = @"INSERT INTO khachhang (makh, tenkh, sdt, diachi, hanmucno, sonohientai)
                                             VALUES (@makh, @tenkh, @sdt, @diachi, @hanmucno, 0)";
                        SqlCommand cmd = new SqlCommand(sqlInsert, cn);
                        cmd.Parameters.AddWithValue("@makh", maKH);
                        cmd.Parameters.AddWithValue("@tenkh", tenKH);
                        cmd.Parameters.AddWithValue("@sdt", sdt);
                        cmd.Parameters.AddWithValue("@diachi", diaChi);
                        cmd.Parameters.AddWithValue("@hanmucno", hanMuc);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (trangThai == "SUA")
                    {
                        string sqlUpdate = @"UPDATE khachhang
                                             SET tenkh = @tenkh, sdt = @sdt, diachi = @diachi, hanmucno = @hanmucno
                                             WHERE makh = @makh";
                        SqlCommand cmd = new SqlCommand(sqlUpdate, cn);
                        cmd.Parameters.AddWithValue("@makh", maKH);
                        cmd.Parameters.AddWithValue("@tenkh", tenKH);
                        cmd.Parameters.AddWithValue("@sdt", sdt);
                        cmd.Parameters.AddWithValue("@diachi", diaChi);
                        cmd.Parameters.AddWithValue("@hanmucno", hanMuc);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                trangThai = "";
                SetTrangThai(false);
                LoadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng [{maKH} - {txtTenKH.Text}]?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);

                    SqlCommand cmdNo = new SqlCommand("SELECT sonohientai FROM khachhang WHERE makh = @makh", cn);
                    cmdNo.Parameters.AddWithValue("@makh", maKH);
                    decimal soNo = Convert.ToDecimal(cmdNo.ExecuteScalar() ?? 0);
                    if (soNo > 0)
                    {
                        MessageBox.Show("Khách hàng đang còn công nợ, không thể xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SqlCommand cmdRef = new SqlCommand(
                        "SELECT (SELECT COUNT(*) FROM hoadon WHERE makh = @makh) + (SELECT COUNT(*) FROM phieuthutrano WHERE makh = @makh)", cn);
                    cmdRef.Parameters.AddWithValue("@makh", maKH);
                    if ((int)cmdRef.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Khách hàng đã phát sinh hóa đơn hoặc phiếu thu nợ, không thể xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SqlCommand cmdDel = new SqlCommand("DELETE FROM khachhang WHERE makh = @makh", cn);
                    cmdDel.Parameters.AddWithValue("@makh", maKH);
                    cmdDel.ExecuteNonQuery();

                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            trangThai = "";
            SetTrangThai(false);
            if (dgvKhachHang.CurrentRow != null)
                HienThiDongDangChon(dgvKhachHang.CurrentRow.Index);
            else
                XoaTrangInput();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void chkChiHienThiDangNo_CheckedChanged(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            chkChiHienThiDangNo.Checked = false;
            LoadDanhSach();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDanhSach();
                e.SuppressKeyPress = true;
            }
        }
    }
}