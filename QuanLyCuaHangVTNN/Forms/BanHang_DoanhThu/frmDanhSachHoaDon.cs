using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmDanhSachHoaDon : Form
    {
        private SqlConnection cn;

        public frmDanhSachHoaDon()
        {
            InitializeComponent();
        }

        private void frmDanhSachHoaDon_Load(object sender, EventArgs e)
        {
            cn = KetNoiDB.TaoKetNoi();
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;
            cboTrangThaiNo.SelectedIndex = 0;
            NapDanhSachHoaDon();
        }

        private void NapDanhSachHoaDon()
        {
            try
            {
                KetNoiDB.MoKetNoi(cn);

                string sql = @"SELECT h.mahd, h.ngayban, h.makh,
                                      ISNULL(k.tenkh, N'Khách lẻ') AS tenkh,
                                      ISNULL(k.sdt, '') AS sdt,
                                      h.tongtien, h.dathanhtoan, h.conno
                               FROM hoadon h
                               LEFT JOIN khachhang k ON h.makh = k.makh
                               WHERE 1 = 1 ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                string tuKhoa = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    sql += " AND (CAST(h.mahd AS NVARCHAR(50)) LIKE @kw OR k.tenkh LIKE @kw OR k.sdt LIKE @kw) ";
                    cmd.Parameters.AddWithValue("@kw", "%" + tuKhoa + "%");
                }

                if (chkLocNgay.Checked)
                {
                    sql += " AND CAST(h.ngayban AS DATE) BETWEEN @tu AND @den ";
                    cmd.Parameters.AddWithValue("@tu", dtpTuNgay.Value.Date);
                    cmd.Parameters.AddWithValue("@den", dtpDenNgay.Value.Date);
                }

                if (cboTrangThaiNo.SelectedIndex == 1)
                {
                    sql += " AND ISNULL(h.conno, 0) = 0 ";
                }
                else if (cboTrangThaiNo.SelectedIndex == 2)
                {
                    sql += " AND ISNULL(h.conno, 0) > 0 ";
                }

                sql += " ORDER BY h.ngayban DESC, h.mahd DESC";
                cmd.CommandText = sql;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvHoaDon.DataSource = dt;
                DinhDangLuoiHoaDon();

                // Tính tổng hợp
                decimal tongDT = 0, tongThu = 0, tongNo = 0;
                foreach (DataRow r in dt.Rows)
                {
                    tongDT += r["tongtien"] != DBNull.Value ? Convert.ToDecimal(r["tongtien"]) : 0m;
                    tongThu += r["dathanhtoan"] != DBNull.Value ? Convert.ToDecimal(r["dathanhtoan"]) : 0m;
                    tongNo += r["conno"] != DBNull.Value ? Convert.ToDecimal(r["conno"]) : 0m;
                }

                lblTongSoHD.Text = string.Format("Số hóa đơn: {0}", dt.Rows.Count);
                lblTongDoanhThuLoc.Text = string.Format("Tổng doanh thu: {0:N0} đ", tongDT);
                lblTongDaThuLoc.Text = string.Format("Đã thu: {0:N0} đ", tongThu);
                lblTongNoLoc.Text = string.Format("Còn nợ: {0:N0} đ", tongNo);

                if (dt.Rows.Count > 0)
                {
                    NapChiTietHoaDon(dt.Rows[0]["mahd"].ToString());
                }
                else
                {
                    dgvChiTietHoaDon.DataSource = null;
                    lblTieuDeCT.Text = "CHI TIẾT MẶT HÀNG CỦA HÓA ĐƠN";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hóa đơn: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangLuoiHoaDon()
        {
            if (dgvHoaDon.Columns.Count == 0) return;

            dgvHoaDon.Columns["mahd"].HeaderText = "Mã HĐ";
            dgvHoaDon.Columns["mahd"].FillWeight = 65;

            dgvHoaDon.Columns["ngayban"].HeaderText = "Ngày bán";
            dgvHoaDon.Columns["ngayban"].FillWeight = 85;
            dgvHoaDon.Columns["ngayban"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvHoaDon.Columns["makh"].Visible = false;

            dgvHoaDon.Columns["tenkh"].HeaderText = "Khách hàng";
            dgvHoaDon.Columns["tenkh"].FillWeight = 145;

            dgvHoaDon.Columns["sdt"].HeaderText = "SĐT";
            dgvHoaDon.Columns["sdt"].FillWeight = 90;

            dgvHoaDon.Columns["tongtien"].HeaderText = "Tổng tiền";
            dgvHoaDon.Columns["tongtien"].FillWeight = 95;
            dgvHoaDon.Columns["tongtien"].DefaultCellStyle.Format = "N0";

            dgvHoaDon.Columns["dathanhtoan"].HeaderText = "Đã thanh toán";
            dgvHoaDon.Columns["dathanhtoan"].FillWeight = 95;
            dgvHoaDon.Columns["dathanhtoan"].DefaultCellStyle.Format = "N0";

            dgvHoaDon.Columns["conno"].HeaderText = "Còn nợ";
            dgvHoaDon.Columns["conno"].FillWeight = 95;
            dgvHoaDon.Columns["conno"].DefaultCellStyle.Format = "N0";
        }

        private void NapChiTietHoaDon(string maHD)
        {
            try
            {
                KetNoiDB.MoKetNoi(cn);
                lblTieuDeCT.Text = "CHI TIẾT MẶT HÀNG CỦA HÓA ĐƠN [" + maHD + "]";

                string sql = @"SELECT ct.mahh, ISNULL(hh.tenhh, ct.mahh) AS tenhh,
                                      ct.soluong, ct.dongiaban, ct.giavon,
                                      (ct.soluong * ct.dongiaban) AS thanhtien,
                                      ((ct.dongiaban - ct.giavon) * ct.soluong) AS loinhuan
                               FROM ct_hoadon ct
                               LEFT JOIN hanghoa hh ON ct.mahh = hh.mahh
                               WHERE ct.mahd = @mahd";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@mahd", maHD);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvChiTietHoaDon.DataSource = dt;

                if (dgvChiTietHoaDon.Columns.Count > 0)
                {
                    dgvChiTietHoaDon.Columns["mahh"].HeaderText = "Mã HH";
                    dgvChiTietHoaDon.Columns["mahh"].FillWeight = 65;

                    dgvChiTietHoaDon.Columns["tenhh"].HeaderText = "Tên vật tư nông nghiệp";
                    dgvChiTietHoaDon.Columns["tenhh"].FillWeight = 165;

                    dgvChiTietHoaDon.Columns["soluong"].HeaderText = "Số lượng";
                    dgvChiTietHoaDon.Columns["soluong"].FillWeight = 65;

                    dgvChiTietHoaDon.Columns["dongiaban"].HeaderText = "Đơn giá bán";
                    dgvChiTietHoaDon.Columns["dongiaban"].FillWeight = 85;
                    dgvChiTietHoaDon.Columns["dongiaban"].DefaultCellStyle.Format = "N0";

                    dgvChiTietHoaDon.Columns["giavon"].HeaderText = "Giá vốn";
                    dgvChiTietHoaDon.Columns["giavon"].FillWeight = 80;
                    dgvChiTietHoaDon.Columns["giavon"].DefaultCellStyle.Format = "N0";

                    dgvChiTietHoaDon.Columns["thanhtien"].HeaderText = "Thành tiền";
                    dgvChiTietHoaDon.Columns["thanhtien"].FillWeight = 95;
                    dgvChiTietHoaDon.Columns["thanhtien"].DefaultCellStyle.Format = "N0";

                    dgvChiTietHoaDon.Columns["loinhuan"].HeaderText = "Lợi nhuận";
                    dgvChiTietHoaDon.Columns["loinhuan"].FillWeight = 90;
                    dgvChiTietHoaDon.Columns["loinhuan"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp chi tiết hóa đơn: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHoaDon.Rows[e.RowIndex].Cells["mahd"].Value != null)
            {
                string maHD = dgvHoaDon.Rows[e.RowIndex].Cells["mahd"].Value.ToString();
                NapChiTietHoaDon(maHD);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            NapDanhSachHoaDon();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            chkLocNgay.Checked = false;
            cboTrangThaiNo.SelectedIndex = 0;
            NapDanhSachHoaDon();
        }

        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHD = dgvHoaDon.CurrentRow.Cells["mahd"].Value.ToString();
            object maKH = dgvHoaDon.CurrentRow.Cells["makh"].Value;
            decimal conNo = dgvHoaDon.CurrentRow.Cells["conno"].Value != DBNull.Value
                ? Convert.ToDecimal(dgvHoaDon.CurrentRow.Cells["conno"].Value) : 0m;

            DialogResult kq = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn hủy/xóa hóa đơn [{0}]?\nHệ thống sẽ tự động hoàn trả số lượng tồn kho và trừ lại công nợ của khách hàng (nếu có).", maHD),
                "Xác nhận xóa hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (kq == DialogResult.No) return;

            SqlTransaction tran = null;
            try
            {
                KetNoiDB.MoKetNoi(cn);
                tran = cn.BeginTransaction();

                // 1. Lấy chi tiết hóa đơn để hoàn lại số lượng tồn kho
                SqlCommand cmdGetCT = new SqlCommand("SELECT mahh, soluong FROM ct_hoadon WHERE mahd = @mahd", cn, tran);
                cmdGetCT.Parameters.AddWithValue("@mahd", maHD);
                DataTable dtCT = new DataTable();
                new SqlDataAdapter(cmdGetCT).Fill(dtCT);

                foreach (DataRow r in dtCT.Rows)
                {
                    SqlCommand cmdHoanKho = new SqlCommand(
                        "UPDATE hanghoa SET soluongton = soluongton + @sl WHERE mahh = @mahh", cn, tran);
                    cmdHoanKho.Parameters.AddWithValue("@sl", Convert.ToInt32(r["soluong"]));
                    cmdHoanKho.Parameters.AddWithValue("@mahh", r["mahh"].ToString());
                    cmdHoanKho.ExecuteNonQuery();
                }

                // 2. Giảm công nợ khách hàng nếu hóa đơn có ghi nợ
                if (maKH != null && maKH != DBNull.Value && conNo > 0)
                {
                    SqlCommand cmdGiamNo = new SqlCommand(
                        "UPDATE khachhang SET sonohientai = CASE WHEN sonohientai >= @no THEN sonohientai - @no ELSE 0 END WHERE makh = @makh",
                        cn, tran);
                    cmdGiamNo.Parameters.AddWithValue("@no", conNo);
                    cmdGiamNo.Parameters.AddWithValue("@makh", maKH);
                    cmdGiamNo.ExecuteNonQuery();
                }

                // 3. Xóa chi tiết hóa đơn và hóa đơn chính
                SqlCommand cmdDelCT = new SqlCommand("DELETE FROM ct_hoadon WHERE mahd = @mahd", cn, tran);
                cmdDelCT.Parameters.AddWithValue("@mahd", maHD);
                cmdDelCT.ExecuteNonQuery();

                SqlCommand cmdDelHD = new SqlCommand("DELETE FROM hoadon WHERE mahd = @mahd", cn, tran);
                cmdDelHD.Parameters.AddWithValue("@mahd", maHD);
                cmdDelHD.ExecuteNonQuery();

                tran.Commit();
                MessageBox.Show("Đã xóa hóa đơn [" + maHD + "] và cập nhật lại tồn kho thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapDanhSachHoaDon();
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}