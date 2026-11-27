using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmNhapHang : Form
    {
        private DataTable dtHangHoa = new DataTable();
        private DataTable dtChiTiet = new DataTable();
        private decimal tongTienPhieu = 0;

        public frmNhapHang()
        {
            InitializeComponent();
            txtMaPN.CharacterCasing = CharacterCasing.Upper;
            dgvChiTietNhap.AutoGenerateColumns = false;
            KhoiTaoBangChiTiet();
        }

        /// <summary>
        /// Khởi tạo cấu trúc bảng tạm lưu chi tiết các mặt hàng nhập kho.
        /// </summary>
        private void KhoiTaoBangChiTiet()
        {
            dtChiTiet = new DataTable();
            dtChiTiet.Columns.Add("mahh", typeof(string));
            dtChiTiet.Columns.Add("tenhh", typeof(string));
            dtChiTiet.Columns.Add("dvt", typeof(string));
            dtChiTiet.Columns.Add("soluong", typeof(int));
            dtChiTiet.Columns.Add("dongianhap", typeof(decimal));
            dtChiTiet.Columns.Add("thanhtien", typeof(decimal));

            colDonGiaNhap.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Format = "N0";

            dgvChiTietNhap.DataSource = dtChiTiet;
        }

        /// <summary>
        /// Nạp danh sách nhà cung cấp và hàng hóa vào các ComboBox.
        /// </summary>
        private void NapDanhMuc()
        {
            try
            {
                using (SqlConnection conn = KetNoiDB.TaoKetNoi())
                {
                    SqlDataAdapter daNCC = new SqlDataAdapter("SELECT mancc, tenncc FROM nhacungcap ORDER BY mancc", conn);
                    DataTable dtNCC = new DataTable();
                    daNCC.Fill(dtNCC);
                    cboNhaCungCap.DataSource = dtNCC;
                    cboNhaCungCap.DisplayMember = "tenncc";
                    cboNhaCungCap.ValueMember = "mancc";

                    SqlDataAdapter daHH = new SqlDataAdapter("SELECT mahh, tenhh, dvt, giavon FROM hanghoa ORDER BY tenhh", conn);
                    dtHangHoa = new DataTable();
                    daHH.Fill(dtHangHoa);
                    cboHangHoa.DataSource = dtHangHoa;
                    cboHangHoa.DisplayMember = "tenhh";
                    cboHangHoa.ValueMember = "mahh";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khởi tạo mã phiếu nhập mới và đặt lại thông tin ban đầu.
        /// </summary>
        private void TaoPhieuMoi()
        {
            txtMaPN.Text = "PN" + DateTime.Now.ToString("yyMMddHHmmss");
            dtpNgayNhap.Value = DateTime.Today;
            if (cboNhaCungCap.Items.Count > 0) cboNhaCungCap.SelectedIndex = 0;
            if (cboHangHoa.Items.Count > 0) cboHangHoa.SelectedIndex = 0;

            txtSoLuong.Text = "1";
            dtChiTiet.Rows.Clear();
            txtDaThanhToan.Text = "0";
            CapNhatTongTien();
            txtMaPN.Focus();
        }

        /// <summary>
        /// Tính tổng tiền phiếu nhập từ các dòng chi tiết.
        /// </summary>
        private void CapNhatTongTien()
        {
            tongTienPhieu = 0;
            foreach (DataRow row in dtChiTiet.Rows)
            {
                tongTienPhieu += Convert.ToDecimal(row["thanhtien"]);
            }

            lblTongTienNhap.Text = string.Format("Tổng tiền phiếu nhập: {0:N0} đ", tongTienPhieu);
            TinhConNo();
        }

        /// <summary>
        /// Tính số tiền còn nợ nhà cung cấp sau khi trừ khoản thanh toán ngay.
        /// </summary>
        private void TinhConNo()
        {
            decimal daTra = 0;
            decimal.TryParse(txtDaThanhToan.Text.Trim(), out daTra);

            decimal conNo = tongTienPhieu - daTra;
            if (conNo < 0) conNo = 0;

            txtConNoNCC.Text = string.Format("{0:N0}", conNo);
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            NapDanhMuc();
            TaoPhieuMoi();
        }

        private void cboHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedValue == null || dtHangHoa.Rows.Count == 0) return;

            string maHH = cboHangHoa.SelectedValue.ToString();
            foreach (DataRow row in dtHangHoa.Rows)
            {
                if (row["mahh"].ToString() == maHH)
                {
                    txtDonGiaNhap.Text = Convert.ToDecimal(row["giavon"]).ToString("0");
                    break;
                }
            }
        }

        private void btnThemDong_Click(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vật tư cần nhập!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong;
            decimal donGia;

            if (!int.TryParse(txtSoLuong.Text.Trim(), out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng nhập phải là số nguyên lớn hơn 0!", "Sai định dạng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            if (!decimal.TryParse(txtDonGiaNhap.Text.Trim(), out donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá nhập phải là số lớn hơn hoặc bằng 0!", "Sai định dạng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaNhap.Focus();
                return;
            }

            string maHH = cboHangHoa.SelectedValue.ToString();
            string tenHH = cboHangHoa.Text;
            string dvt = "";

            foreach (DataRow r in dtHangHoa.Rows)
            {
                if (r["mahh"].ToString() == maHH)
                {
                    dvt = r["dvt"].ToString();
                    break;
                }
            }

            bool daCo = false;
            foreach (DataRow row in dtChiTiet.Rows)
            {
                if (row["mahh"].ToString() == maHH)
                {
                    int slMoi = Convert.ToInt32(row["soluong"]) + soLuong;
                    row["soluong"] = slMoi;
                    row["dongianhap"] = donGia;
                    row["thanhtien"] = slMoi * donGia;
                    daCo = true;
                    break;
                }
            }

            if (!daCo)
            {
                DataRow newRow = dtChiTiet.NewRow();
                newRow["mahh"] = maHH;
                newRow["tenhh"] = tenHH;
                newRow["dvt"] = dvt;
                newRow["soluong"] = soLuong;
                newRow["dongianhap"] = donGia;
                newRow["thanhtien"] = soLuong * donGia;
                dtChiTiet.Rows.Add(newRow);
            }

            CapNhatTongTien();
            txtSoLuong.Text = "1";
            cboHangHoa.Focus();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvChiTietNhap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng vật tư cần xóa khỏi phiếu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvChiTietNhap.Rows.RemoveAt(dgvChiTietNhap.CurrentRow.Index);
            CapNhatTongTien();
        }

        private void txtDaThanhToan_TextChanged(object sender, EventArgs e)
        {
            TinhConNo();
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            string maPN = txtMaPN.Text.Trim();
            if (string.IsNullOrEmpty(maPN))
            {
                MessageBox.Show("Vui lòng nhập Mã phiếu nhập!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPN.Focus();
                return;
            }
            if (cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }
            if (dtChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập chưa có vật tư nào! Vui lòng thêm ít nhất 1 mặt hàng.",
                    "Thiếu chi tiết", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal daTra;
            if (!decimal.TryParse(txtDaThanhToan.Text.Trim(), out daTra) || daTra < 0)
            {
                MessageBox.Show("Số tiền trả ngay phải lớn hơn hoặc bằng 0!", "Sai định dạng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDaThanhToan.Focus();
                return;
            }
            if (daTra > tongTienPhieu)
            {
                MessageBox.Show("Số tiền trả ngay không được lớn hơn Tổng tiền phiếu nhập!", "Sai số tiền",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDaThanhToan.Focus();
                return;
            }

            decimal conNo = tongTienPhieu - daTra;
            string maNCC = cboNhaCungCap.SelectedValue.ToString();

            try
            {
                using (SqlConnection conn = KetNoiDB.TaoKetNoi())
                {
                    conn.Open();

                    SqlCommand cmdKiemTra = new SqlCommand(
                        "SELECT COUNT(*) FROM phieunhap WHERE mapn = @mapn", conn);
                    cmdKiemTra.Parameters.AddWithValue("@mapn", maPN);
                    if (Convert.ToInt32(cmdKiemTra.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Mã phiếu nhập \"" + maPN + "\" đã tồn tại!", "Trùng mã phiếu",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaPN.Focus();
                        return;
                    }

                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        string sqlPN = @"INSERT INTO phieunhap (mapn, ngaynhap, mancc, tongtien, datrancc, connoncc)
                                         VALUES (@mapn, @ngaynhap, @mancc, @tongtien, @datrancc, @connoncc)";
                        SqlCommand cmdPN = new SqlCommand(sqlPN, conn, tran);
                        cmdPN.Parameters.AddWithValue("@mapn", maPN);
                        cmdPN.Parameters.AddWithValue("@ngaynhap", dtpNgayNhap.Value.Date);
                        cmdPN.Parameters.AddWithValue("@mancc", maNCC);
                        cmdPN.Parameters.AddWithValue("@tongtien", tongTienPhieu);
                        cmdPN.Parameters.AddWithValue("@datrancc", daTra);
                        cmdPN.Parameters.AddWithValue("@connoncc", conNo);
                        cmdPN.ExecuteNonQuery();

                        foreach (DataRow row in dtChiTiet.Rows)
                        {
                            string maHH = row["mahh"].ToString();
                            int soLuong = Convert.ToInt32(row["soluong"]);
                            decimal donGia = Convert.ToDecimal(row["dongianhap"]);
                            decimal thanhTien = Convert.ToDecimal(row["thanhtien"]);

                            string sqlCT = @"INSERT INTO ct_phieunhap (mapn, mahh, soluong, dongianhap, thanhtien)
                                             VALUES (@mapn, @mahh, @soluong, @dongianhap, @thanhtien)";
                            SqlCommand cmdCT = new SqlCommand(sqlCT, conn, tran);
                            cmdCT.Parameters.AddWithValue("@mapn", maPN);
                            cmdCT.Parameters.AddWithValue("@mahh", maHH);
                            cmdCT.Parameters.AddWithValue("@soluong", soLuong);
                            cmdCT.Parameters.AddWithValue("@dongianhap", donGia);
                            cmdCT.Parameters.AddWithValue("@thanhtien", thanhTien);
                            cmdCT.ExecuteNonQuery();

                            string sqlKho = @"UPDATE hanghoa
                                              SET soluongton = soluongton + @soluong, giavon = @dongianhap
                                              WHERE mahh = @mahh";
                            SqlCommand cmdKho = new SqlCommand(sqlKho, conn, tran);
                            cmdKho.Parameters.AddWithValue("@soluong", soLuong);
                            cmdKho.Parameters.AddWithValue("@dongianhap", donGia);
                            cmdKho.Parameters.AddWithValue("@mahh", maHH);
                            cmdKho.ExecuteNonQuery();
                        }

                        if (conNo > 0)
                        {
                            string sqlNCC = "UPDATE nhacungcap SET congnoncc = congnoncc + @conno WHERE mancc = @mancc";
                            SqlCommand cmdNCC = new SqlCommand(sqlNCC, conn, tran);
                            cmdNCC.Parameters.AddWithValue("@conno", conNo);
                            cmdNCC.Parameters.AddWithValue("@mancc", maNCC);
                            cmdNCC.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }

                MessageBox.Show("Lưu phiếu nhập hàng thành công! Đã cập nhật tồn kho và công nợ NCC.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapDanhMuc();
                TaoPhieuMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            NapDanhMuc();
            TaoPhieuMoi();
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
