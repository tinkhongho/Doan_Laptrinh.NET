using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmNhaCungCap_CongNo : Form
    {
        private string trangThai = "";

        public frmNhaCungCap_CongNo()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_CongNo_Load(object sender, EventArgs e)
        {
            dgvNhaCungCap.AutoGenerateColumns = false;
            colCongNoNCC.DefaultCellStyle.Format = "N0";
            colCongNoNCC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            SetTrangThai(false);
            LoadDanhSach();
        }

        private void SetTrangThai(bool dangNhapLieu)
        {
            txtMaNCC.Enabled = (trangThai == "THEM");
            txtTenNCC.Enabled = dangNhapLieu;
            txtSDT.Enabled = dangNhapLieu;
            txtDiaChi.Enabled = dangNhapLieu;

            btnThem.Enabled = !dangNhapLieu;
            btnSua.Enabled = !dangNhapLieu;
            btnXoa.Enabled = !dangNhapLieu;
            btnLuu.Enabled = dangNhapLieu;
            btnHuy.Enabled = dangNhapLieu;

            dgvNhaCungCap.Enabled = !dangNhapLieu;
        }

        private void XoaTrangInput()
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtCongNoNCC.Text = "0";
        }

        private void LoadDanhSach()
        {
            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);
                    string sql = @"SELECT mancc, tenncc, sdt, diachi, congnoncc
                                   FROM nhacungcap
                                   WHERE 1 = 1";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    if (chkChiHienThiConNo.Checked)
                    {
                        sql += " AND congnoncc > 0";
                    }

                    string kw = txtTimKiem.Text.Trim();
                    if (!string.IsNullOrEmpty(kw))
                    {
                        sql += " AND (mancc LIKE @kw OR tenncc LIKE @kw OR sdt LIKE @kw OR diachi LIKE @kw)";
                        cmd.Parameters.AddWithValue("@kw", "%" + kw + "%");
                    }

                    sql += " ORDER BY mancc";
                    cmd.CommandText = sql;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvNhaCungCap.DataSource = dt;

                    decimal tongNo = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["congnoncc"] != DBNull.Value)
                            tongNo += Convert.ToDecimal(row["congnoncc"]);
                    }
                    lblTongCongNoNCC.Text = string.Format("Tổng nợ phải trả NCC: {0:N0} đ", tongNo);

                    if (dgvNhaCungCap.Rows.Count > 0)
                        HienThiDongDangChon(0);
                    else
                        XoaTrangInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiDongDangChon(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvNhaCungCap.Rows.Count) return;

            DataGridViewRow r = dgvNhaCungCap.Rows[rowIndex];
            txtMaNCC.Text = Convert.ToString(r.Cells["colMaNCC"].Value);
            txtTenNCC.Text = Convert.ToString(r.Cells["colTenNCC"].Value);
            txtSDT.Text = Convert.ToString(r.Cells["colSDT"].Value);
            txtDiaChi.Text = Convert.ToString(r.Cells["colDiaChi"].Value);

            decimal congNo = r.Cells["colCongNoNCC"].Value != DBNull.Value ? Convert.ToDecimal(r.Cells["colCongNoNCC"].Value) : 0;
            txtCongNoNCC.Text = congNo.ToString("N0");
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                HienThiDongDangChon(e.RowIndex);
        }

        private string TaoMaNCCMoi()
        {
            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);
                    string sql = "SELECT TOP 1 mancc FROM nhacungcap WHERE mancc LIKE 'NCC%' ORDER BY LEN(mancc) DESC, mancc DESC";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    object kq = cmd.ExecuteScalar();

                    if (kq != null && kq != DBNull.Value)
                    {
                        string maCu = kq.ToString().Trim();
                        string phanSo = maCu.Substring(3);
                        if (int.TryParse(phanSo, out int so))
                            return "NCC" + (so + 1).ToString("D2");
                    }
                }
            }
            catch { }
            return "NCC01";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            trangThai = "THEM";
            XoaTrangInput();
            txtMaNCC.Text = TaoMaNCCMoi();
            SetTrangThai(true);
            txtTenNCC.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            trangThai = "SUA";
            SetTrangThai(true);
            txtTenNCC.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNCC.Text.Trim();
            string tenNCC = txtTenNCC.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Mã nhà cung cấp không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNCC.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tenNCC))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);

                    if (trangThai == "THEM")
                    {
                        SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM nhacungcap WHERE mancc = @mancc", cn);
                        cmdCheck.Parameters.AddWithValue("@mancc", maNCC);
                        if ((int)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Mã nhà cung cấp này đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaNCC.Focus();
                            return;
                        }

                        string sqlInsert = @"INSERT INTO nhacungcap (mancc, tenncc, sdt, diachi, congnoncc)
                                             VALUES (@mancc, @tenncc, @sdt, @diachi, 0)";
                        SqlCommand cmd = new SqlCommand(sqlInsert, cn);
                        cmd.Parameters.AddWithValue("@mancc", maNCC);
                        cmd.Parameters.AddWithValue("@tenncc", tenNCC);
                        cmd.Parameters.AddWithValue("@sdt", sdt);
                        cmd.Parameters.AddWithValue("@diachi", diaChi);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (trangThai == "SUA")
                    {
                        string sqlUpdate = @"UPDATE nhacungcap
                                             SET tenncc = @tenncc, sdt = @sdt, diachi = @diachi
                                             WHERE mancc = @mancc";
                        SqlCommand cmd = new SqlCommand(sqlUpdate, cn);
                        cmd.Parameters.AddWithValue("@mancc", maNCC);
                        cmd.Parameters.AddWithValue("@tenncc", tenNCC);
                        cmd.Parameters.AddWithValue("@sdt", sdt);
                        cmd.Parameters.AddWithValue("@diachi", diaChi);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string maNCC = txtMaNCC.Text.Trim();
            if (string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà cung cấp [{maNCC} - {txtTenNCC.Text}]?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection cn = KetNoiDB.TaoKetNoi())
                {
                    KetNoiDB.MoKetNoi(cn);

                    SqlCommand cmdNo = new SqlCommand("SELECT congnoncc FROM nhacungcap WHERE mancc = @mancc", cn);
                    cmdNo.Parameters.AddWithValue("@mancc", maNCC);
                    decimal congNo = Convert.ToDecimal(cmdNo.ExecuteScalar() ?? 0);
                    if (congNo > 0)
                    {
                        MessageBox.Show("Cửa hàng đang còn nợ nhà cung cấp này, không thể xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SqlCommand cmdRef = new SqlCommand(
                        "SELECT (SELECT COUNT(*) FROM phieunhap WHERE mancc = @mancc) + (SELECT COUNT(*) FROM phieuthutrano WHERE mancc = @mancc)", cn);
                    cmdRef.Parameters.AddWithValue("@mancc", maNCC);
                    if ((int)cmdRef.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Nhà cung cấp đã phát sinh phiếu nhập hoặc phiếu trả nợ, không thể xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SqlCommand cmdDel = new SqlCommand("DELETE FROM nhacungcap WHERE mancc = @mancc", cn);
                    cmdDel.Parameters.AddWithValue("@mancc", maNCC);
                    cmdDel.ExecuteNonQuery();

                    MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dgvNhaCungCap.CurrentRow != null)
                HienThiDongDangChon(dgvNhaCungCap.CurrentRow.Index);
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

        private void chkChiHienThiConNo_CheckedChanged(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            chkChiHienThiConNo.Checked = false;
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