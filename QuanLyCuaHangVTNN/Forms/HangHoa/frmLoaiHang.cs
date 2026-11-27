using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmLoaiHang : Form
    {
        private DataTable dtLoaiHang = new DataTable();
        private bool isThem = false;

        public frmLoaiHang()
        {
            InitializeComponent();
            txtMaLoai.CharacterCasing = CharacterCasing.Upper;
            dgvLoaiHang.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Bật tắt trạng thái các ô nhập liệu và nút lệnh theo thao tác.
        /// </summary>
        private void SetTrangThai(bool dangThaoTac)
        {
            txtMaLoai.Enabled = dangThaoTac && isThem;
            txtTenLoai.Enabled = dangThaoTac;
            txtMoTa.Enabled = dangThaoTac;

            bool coDuLieu = dgvLoaiHang.Rows.Count > 0;
            btnThem.Enabled = !dangThaoTac;
            btnSua.Enabled = !dangThaoTac && coDuLieu;
            btnXoa.Enabled = !dangThaoTac && coDuLieu;

            btnLuu.Enabled = dangThaoTac;
            btnHuy.Enabled = dangThaoTac;

            dgvLoaiHang.Enabled = !dangThaoTac;
            txtTimKiem.Enabled = !dangThaoTac;
            btnTimKiem.Enabled = !dangThaoTac;
            btnLamMoi.Enabled = !dangThaoTac;
        }

        /// <summary>
        /// Xóa trắng các ô nhập thông tin loại hàng.
        /// </summary>
        private void XoaTrang()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtMoTa.Clear();
        }

        /// <summary>
        /// Đổ dữ liệu dòng đang chọn trên lưới lên các ô nhập liệu.
        /// </summary>
        private void HienThiDongHienTai()
        {
            if (dgvLoaiHang.CurrentRow == null || !(dgvLoaiHang.CurrentRow.DataBoundItem is DataRowView drv))
            {
                XoaTrang();
                return;
            }

            txtMaLoai.Text = drv["maloai"].ToString();
            txtTenLoai.Text = drv["tenloai"].ToString();
            txtMoTa.Text = drv["ghichu"].ToString();
        }

        /// <summary>
        /// Tải danh sách loại vật tư từ cơ sở dữ liệu lên lưới.
        /// </summary>
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = KetNoiDB.TaoKetNoi())
                {
                    string sql = "SELECT maloai, tenloai, ghichu FROM loaihang ORDER BY maloai";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    dtLoaiHang = new DataTable();
                    da.Fill(dtLoaiHang);

                    dgvLoaiHang.DataSource = dtLoaiHang;
                    HienThiDongHienTai();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách loại hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLoaiHang_Load(object sender, EventArgs e)
        {
            LoadData();
            SetTrangThai(false);
        }

        private void dgvLoaiHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiHang.Enabled)
            {
                HienThiDongHienTai();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            XoaTrang();
            SetTrangThai(true);
            txtMaLoai.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvLoaiHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn loại vật tư cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isThem = false;
            SetTrangThai(true);
            txtTenLoai.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLoaiHang.CurrentRow == null || !(dgvLoaiHang.CurrentRow.DataBoundItem is DataRowView drv))
            {
                MessageBox.Show("Vui lòng chọn loại vật tư cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLoai = drv["maloai"].ToString();
            string tenLoai = drv["tenloai"].ToString();

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa loại vật tư \"" + tenLoai + "\" (" + maLoai + ") không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (kq != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = KetNoiDB.TaoKetNoi())
                {
                    conn.Open();
                    string sql = "DELETE FROM loaihang WHERE maloai = @maloai";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@maloai", maLoai);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đã xóa loại vật tư thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SetTrangThai(false);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể xóa loại vật tư này vì đang có hàng hóa thuộc loại này!",
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
            string maLoai = txtMaLoai.Text.Trim();
            string tenLoai = txtTenLoai.Text.Trim();
            string ghiChu = txtMoTa.Text.Trim();

            if (string.IsNullOrEmpty(maLoai))
            {
                MessageBox.Show("Vui lòng nhập Mã loại!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoai.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập Tên loại hàng!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
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
                            "SELECT COUNT(*) FROM loaihang WHERE maloai = @maloai", conn);
                        cmdKiemTra.Parameters.AddWithValue("@maloai", maLoai);
                        int tonTai = Convert.ToInt32(cmdKiemTra.ExecuteScalar());

                        if (tonTai > 0)
                        {
                            MessageBox.Show("Mã loại \"" + maLoai + "\" đã tồn tại!", "Trùng khóa chính",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaLoai.Focus();
                            return;
                        }

                        string sqlInsert = "INSERT INTO loaihang (maloai, tenloai, ghichu) VALUES (@maloai, @tenloai, @ghichu)";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn);
                        cmdInsert.Parameters.AddWithValue("@maloai", maLoai);
                        cmdInsert.Parameters.AddWithValue("@tenloai", tenLoai);
                        cmdInsert.Parameters.AddWithValue("@ghichu", ghiChu);
                        cmdInsert.ExecuteNonQuery();

                        MessageBox.Show("Thêm loại vật tư mới thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sqlUpdate = "UPDATE loaihang SET tenloai = @tenloai, ghichu = @ghichu WHERE maloai = @maloai";
                        SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
                        cmdUpdate.Parameters.AddWithValue("@maloai", maLoai);
                        cmdUpdate.Parameters.AddWithValue("@tenloai", tenLoai);
                        cmdUpdate.Parameters.AddWithValue("@ghichu", ghiChu);
                        cmdUpdate.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật loại vật tư thành công!", "Thông báo",
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
            if (dtLoaiHang == null) return;

            string tuKhoa = txtTimKiem.Text.Trim().Replace("'", "''");
            if (string.IsNullOrEmpty(tuKhoa))
            {
                dtLoaiHang.DefaultView.RowFilter = "";
            }
            else
            {
                dtLoaiHang.DefaultView.RowFilter =
                    string.Format("maloai LIKE '%{0}%' OR tenloai LIKE '%{0}%' OR ghichu LIKE '%{0}%'", tuKhoa);
            }

            HienThiDongHienTai();
            SetTrangThai(false);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
            SetTrangThai(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
