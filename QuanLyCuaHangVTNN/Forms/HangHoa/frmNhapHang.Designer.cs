namespace QuanLyCuaHangVTNN
{
    partial class frmNhapHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.pnlVienTren = new System.Windows.Forms.Panel();
            this.lblTieuDeThongTin = new System.Windows.Forms.Label();
            this.lblMaPN = new System.Windows.Forms.Label();
            this.txtMaPN = new System.Windows.Forms.TextBox();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.lblHangHoa = new System.Windows.Forms.Label();
            this.cboHangHoa = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblDonGiaNhap = new System.Windows.Forms.Label();
            this.txtDonGiaNhap = new System.Windows.Forms.TextBox();
            this.btnThemDong = new System.Windows.Forms.Button();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.pnlDanhSach = new System.Windows.Forms.Panel();
            this.lblTieuDeDanhSach = new System.Windows.Forms.Label();
            this.lblTongTienNhap = new System.Windows.Forms.Label();
            this.lblDaThanhToan = new System.Windows.Forms.Label();
            this.txtDaThanhToan = new System.Windows.Forms.TextBox();
            this.lblConNoNCC = new System.Windows.Forms.Label();
            this.txtConNoNCC = new System.Windows.Forms.TextBox();
            this.dgvChiTietNhap = new System.Windows.Forms.DataGridView();
            this.colMaHH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDVT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlThongTin.SuspendLayout();
            this.pnlDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlThongTin.BackColor = System.Drawing.Color.White;
            this.pnlThongTin.Controls.Add(this.pnlVienTren);
            this.pnlThongTin.Controls.Add(this.lblTieuDeThongTin);
            this.pnlThongTin.Controls.Add(this.lblMaPN);
            this.pnlThongTin.Controls.Add(this.txtMaPN);
            this.pnlThongTin.Controls.Add(this.lblNgayNhap);
            this.pnlThongTin.Controls.Add(this.dtpNgayNhap);
            this.pnlThongTin.Controls.Add(this.lblNhaCungCap);
            this.pnlThongTin.Controls.Add(this.cboNhaCungCap);
            this.pnlThongTin.Controls.Add(this.lblHangHoa);
            this.pnlThongTin.Controls.Add(this.cboHangHoa);
            this.pnlThongTin.Controls.Add(this.lblSoLuong);
            this.pnlThongTin.Controls.Add(this.txtSoLuong);
            this.pnlThongTin.Controls.Add(this.lblDonGiaNhap);
            this.pnlThongTin.Controls.Add(this.txtDonGiaNhap);
            this.pnlThongTin.Controls.Add(this.btnThemDong);
            this.pnlThongTin.Controls.Add(this.btnXoaDong);
            this.pnlThongTin.Controls.Add(this.btnLuuPhieu);
            this.pnlThongTin.Controls.Add(this.btnLamMoi);
            this.pnlThongTin.Controls.Add(this.btnThoat);
            this.pnlThongTin.Location = new System.Drawing.Point(15, 12);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(900, 178);
            this.pnlThongTin.TabIndex = 0;
            // 
            // pnlVienTren
            // 
            this.pnlVienTren.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.pnlVienTren.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVienTren.Location = new System.Drawing.Point(0, 0);
            this.pnlVienTren.Name = "pnlVienTren";
            this.pnlVienTren.Size = new System.Drawing.Size(900, 4);
            this.pnlVienTren.TabIndex = 0;
            // 
            // lblTieuDeThongTin
            // 
            this.lblTieuDeThongTin.AutoSize = true;
            this.lblTieuDeThongTin.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.lblTieuDeThongTin.Location = new System.Drawing.Point(16, 12);
            this.lblTieuDeThongTin.Name = "lblTieuDeThongTin";
            this.lblTieuDeThongTin.Size = new System.Drawing.Size(344, 19);
            this.lblTieuDeThongTin.TabIndex = 1;
            this.lblTieuDeThongTin.Text = "THÔNG TIN PHIẾU NHẬP HÀNG TỪ NHÀ CUNG CẤP";
            // 
            // lblMaPN
            // 
            this.lblMaPN.AutoSize = true;
            this.lblMaPN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblMaPN.Location = new System.Drawing.Point(16, 46);
            this.lblMaPN.Name = "lblMaPN";
            this.lblMaPN.Size = new System.Drawing.Size(97, 17);
            this.lblMaPN.TabIndex = 2;
            this.lblMaPN.Text = "Mã phiếu nhập:";
            // 
            // txtMaPN
            // 
            this.txtMaPN.Location = new System.Drawing.Point(125, 43);
            this.txtMaPN.MaxLength = 15;
            this.txtMaPN.Name = "txtMaPN";
            this.txtMaPN.Size = new System.Drawing.Size(150, 25);
            this.txtMaPN.TabIndex = 3;
            // 
            // lblNgayNhap
            // 
            this.lblNgayNhap.AutoSize = true;
            this.lblNgayNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblNgayNhap.Location = new System.Drawing.Point(292, 46);
            this.lblNgayNhap.Name = "lblNgayNhap";
            this.lblNgayNhap.Size = new System.Drawing.Size(76, 17);
            this.lblNgayNhap.TabIndex = 4;
            this.lblNgayNhap.Text = "Ngày nhập:";
            // 
            // dtpNgayNhap
            // 
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayNhap.Location = new System.Drawing.Point(378, 43);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(155, 25);
            this.dtpNgayNhap.TabIndex = 5;
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblNhaCungCap.Location = new System.Drawing.Point(550, 46);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(95, 17);
            this.lblNhaCungCap.TabIndex = 6;
            this.lblNhaCungCap.Text = "Nhà cung cấp:";
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhaCungCap.FormattingEnabled = true;
            this.cboNhaCungCap.Location = new System.Drawing.Point(655, 43);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(229, 25);
            this.cboNhaCungCap.TabIndex = 7;
            // 
            // lblHangHoa
            // 
            this.lblHangHoa.AutoSize = true;
            this.lblHangHoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblHangHoa.Location = new System.Drawing.Point(16, 84);
            this.lblHangHoa.Name = "lblHangHoa";
            this.lblHangHoa.Size = new System.Drawing.Size(92, 17);
            this.lblHangHoa.TabIndex = 8;
            this.lblHangHoa.Text = "Vật tư / Hàng:";
            // 
            // cboHangHoa
            // 
            this.cboHangHoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHangHoa.FormattingEnabled = true;
            this.cboHangHoa.Location = new System.Drawing.Point(125, 81);
            this.cboHangHoa.Name = "cboHangHoa";
            this.cboHangHoa.Size = new System.Drawing.Size(240, 25);
            this.cboHangHoa.TabIndex = 9;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblSoLuong.Location = new System.Drawing.Point(382, 84);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(64, 17);
            this.lblSoLuong.TabIndex = 10;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(455, 81);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(100, 25);
            this.txtSoLuong.TabIndex = 11;
            this.txtSoLuong.Text = "1";
            this.txtSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDonGiaNhap
            // 
            this.lblDonGiaNhap.AutoSize = true;
            this.lblDonGiaNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblDonGiaNhap.Location = new System.Drawing.Point(572, 84);
            this.lblDonGiaNhap.Name = "lblDonGiaNhap";
            this.lblDonGiaNhap.Size = new System.Drawing.Size(92, 17);
            this.lblDonGiaNhap.TabIndex = 12;
            this.lblDonGiaNhap.Text = "Đơn giá nhập:";
            // 
            // txtDonGiaNhap
            // 
            this.txtDonGiaNhap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDonGiaNhap.Location = new System.Drawing.Point(675, 81);
            this.txtDonGiaNhap.Name = "txtDonGiaNhap";
            this.txtDonGiaNhap.Size = new System.Drawing.Size(209, 25);
            this.txtDonGiaNhap.TabIndex = 13;
            this.txtDonGiaNhap.Text = "0";
            this.txtDonGiaNhap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnThemDong
            // 
            this.btnThemDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.btnThemDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemDong.FlatAppearance.BorderSize = 0;
            this.btnThemDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnThemDong.ForeColor = System.Drawing.Color.White;
            this.btnThemDong.Location = new System.Drawing.Point(125, 126);
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Size = new System.Drawing.Size(135, 34);
            this.btnThemDong.TabIndex = 14;
            this.btnThemDong.Text = "Thêm vào phiếu";
            this.btnThemDong.UseVisualStyleBackColor = false;
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnXoaDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaDong.FlatAppearance.BorderSize = 0;
            this.btnXoaDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnXoaDong.ForeColor = System.Drawing.Color.White;
            this.btnXoaDong.Location = new System.Drawing.Point(272, 126);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(105, 34);
            this.btnXoaDong.TabIndex = 15;
            this.btnXoaDong.Text = "Xóa dòng";
            this.btnXoaDong.UseVisualStyleBackColor = false;
            // 
            // btnLuuPhieu
            // 
            this.btnLuuPhieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnLuuPhieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuuPhieu.FlatAppearance.BorderSize = 0;
            this.btnLuuPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuPhieu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Location = new System.Drawing.Point(389, 126);
            this.btnLuuPhieu.Name = "btnLuuPhieu";
            this.btnLuuPhieu.Size = new System.Drawing.Size(135, 34);
            this.btnLuuPhieu.TabIndex = 16;
            this.btnLuuPhieu.Text = "Lưu phiếu nhập";
            this.btnLuuPhieu.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(108)))), ((int)(((byte)(0)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(536, 126);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 34);
            this.btnLamMoi.TabIndex = 17;
            this.btnLamMoi.Text = "Tạo mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnThoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.btnThoat.Location = new System.Drawing.Point(784, 126);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 34);
            this.btnThoat.TabIndex = 18;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            // 
            // pnlDanhSach
            // 
            this.pnlDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDanhSach.BackColor = System.Drawing.Color.White;
            this.pnlDanhSach.Controls.Add(this.lblTieuDeDanhSach);
            this.pnlDanhSach.Controls.Add(this.lblTongTienNhap);
            this.pnlDanhSach.Controls.Add(this.lblDaThanhToan);
            this.pnlDanhSach.Controls.Add(this.txtDaThanhToan);
            this.pnlDanhSach.Controls.Add(this.lblConNoNCC);
            this.pnlDanhSach.Controls.Add(this.txtConNoNCC);
            this.pnlDanhSach.Controls.Add(this.dgvChiTietNhap);
            this.pnlDanhSach.Location = new System.Drawing.Point(15, 200);
            this.pnlDanhSach.Name = "pnlDanhSach";
            this.pnlDanhSach.Size = new System.Drawing.Size(900, 363);
            this.pnlDanhSach.TabIndex = 1;
            // 
            // lblTieuDeDanhSach
            // 
            this.lblTieuDeDanhSach.AutoSize = true;
            this.lblTieuDeDanhSach.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.lblTieuDeDanhSach.Location = new System.Drawing.Point(16, 12);
            this.lblTieuDeDanhSach.Name = "lblTieuDeDanhSach";
            this.lblTieuDeDanhSach.Size = new System.Drawing.Size(210, 19);
            this.lblTieuDeDanhSach.TabIndex = 0;
            this.lblTieuDeDanhSach.Text = "CHI TIẾT VẬT TƯ NHẬP KHO";
            // 
            // lblTongTienNhap
            // 
            this.lblTongTienNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienNhap.AutoSize = true;
            this.lblTongTienNhap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTienNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblTongTienNhap.Location = new System.Drawing.Point(650, 12);
            this.lblTongTienNhap.Name = "lblTongTienNhap";
            this.lblTongTienNhap.Size = new System.Drawing.Size(184, 19);
            this.lblTongTienNhap.TabIndex = 1;
            this.lblTongTienNhap.Text = "Tổng tiền phiếu nhập: 0 đ";
            // 
            // lblDaThanhToan
            // 
            this.lblDaThanhToan.AutoSize = true;
            this.lblDaThanhToan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblDaThanhToan.Location = new System.Drawing.Point(16, 46);
            this.lblDaThanhToan.Name = "lblDaThanhToan";
            this.lblDaThanhToan.Size = new System.Drawing.Size(121, 17);
            this.lblDaThanhToan.TabIndex = 2;
            this.lblDaThanhToan.Text = "Trả ngay NCC (đ):";
            // 
            // txtDaThanhToan
            // 
            this.txtDaThanhToan.Location = new System.Drawing.Point(145, 43);
            this.txtDaThanhToan.Name = "txtDaThanhToan";
            this.txtDaThanhToan.Size = new System.Drawing.Size(170, 25);
            this.txtDaThanhToan.TabIndex = 3;
            this.txtDaThanhToan.Text = "0";
            this.txtDaThanhToan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblConNoNCC
            // 
            this.lblConNoNCC.AutoSize = true;
            this.lblConNoNCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblConNoNCC.Location = new System.Drawing.Point(335, 46);
            this.lblConNoNCC.Name = "lblConNoNCC";
            this.lblConNoNCC.Size = new System.Drawing.Size(112, 17);
            this.lblConNoNCC.TabIndex = 4;
            this.lblConNoNCC.Text = "Ghi nợ NCC (đ):";
            // 
            // txtConNoNCC
            // 
            this.txtConNoNCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtConNoNCC.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtConNoNCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.txtConNoNCC.Location = new System.Drawing.Point(455, 43);
            this.txtConNoNCC.Name = "txtConNoNCC";
            this.txtConNoNCC.ReadOnly = true;
            this.txtConNoNCC.Size = new System.Drawing.Size(170, 25);
            this.txtConNoNCC.TabIndex = 5;
            this.txtConNoNCC.Text = "0";
            this.txtConNoNCC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgvChiTietNhap
            // 
            this.dgvChiTietNhap.AllowUserToAddRows = false;
            this.dgvChiTietNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChiTietNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTietNhap.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTietNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTietNhap.ColumnHeadersHeight = 35;
            this.dgvChiTietNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvChiTietNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaHH,
            this.colTenHH,
            this.colDVT,
            this.colSoLuong,
            this.colDonGiaNhap,
            this.colThanhTien});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTietNhap.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChiTietNhap.EnableHeadersVisualStyles = false;
            this.dgvChiTietNhap.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.dgvChiTietNhap.Location = new System.Drawing.Point(16, 82);
            this.dgvChiTietNhap.MultiSelect = false;
            this.dgvChiTietNhap.Name = "dgvChiTietNhap";
            this.dgvChiTietNhap.ReadOnly = true;
            this.dgvChiTietNhap.RowHeadersVisible = false;
            this.dgvChiTietNhap.RowHeadersWidth = 51;
            this.dgvChiTietNhap.RowTemplate.Height = 28;
            this.dgvChiTietNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietNhap.Size = new System.Drawing.Size(868, 268);
            this.dgvChiTietNhap.TabIndex = 6;
            // 
            // colMaHH
            // 
            this.colMaHH.DataPropertyName = "mahh";
            this.colMaHH.FillWeight = 70F;
            this.colMaHH.HeaderText = "Mã hàng";
            this.colMaHH.MinimumWidth = 6;
            this.colMaHH.Name = "colMaHH";
            this.colMaHH.ReadOnly = true;
            // 
            // colTenHH
            // 
            this.colTenHH.DataPropertyName = "tenhh";
            this.colTenHH.FillWeight = 160F;
            this.colTenHH.HeaderText = "Tên vật tư";
            this.colTenHH.MinimumWidth = 6;
            this.colTenHH.Name = "colTenHH";
            this.colTenHH.ReadOnly = true;
            // 
            // colDVT
            // 
            this.colDVT.DataPropertyName = "dvt";
            this.colDVT.FillWeight = 60F;
            this.colDVT.HeaderText = "ĐVT";
            this.colDVT.MinimumWidth = 6;
            this.colDVT.Name = "colDVT";
            this.colDVT.ReadOnly = true;
            // 
            // colSoLuong
            // 
            this.colSoLuong.DataPropertyName = "soluong";
            this.colSoLuong.FillWeight = 75F;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.MinimumWidth = 6;
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            // 
            // colDonGiaNhap
            // 
            this.colDonGiaNhap.DataPropertyName = "dongianhap";
            this.colDonGiaNhap.FillWeight = 95F;
            this.colDonGiaNhap.HeaderText = "Đơn giá nhập (đ)";
            this.colDonGiaNhap.MinimumWidth = 6;
            this.colDonGiaNhap.Name = "colDonGiaNhap";
            this.colDonGiaNhap.ReadOnly = true;
            // 
            // colThanhTien
            // 
            this.colThanhTien.DataPropertyName = "thanhtien";
            this.colThanhTien.FillWeight = 105F;
            this.colThanhTien.HeaderText = "Thành tiền (đ)";
            this.colThanhTien.MinimumWidth = 6;
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            // 
            // frmNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(930, 575);
            this.Controls.Add(this.pnlDanhSach);
            this.Controls.Add(this.pnlThongTin);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "frmNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhập hàng từ Nhà cung cấp";
            this.pnlThongTin.ResumeLayout(false);
            this.pnlThongTin.PerformLayout();
            this.pnlDanhSach.ResumeLayout(false);
            this.pnlDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietNhap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Panel pnlVienTren;
        private System.Windows.Forms.Label lblTieuDeThongTin;
        private System.Windows.Forms.Label lblMaPN;
        private System.Windows.Forms.TextBox txtMaPN;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Label lblHangHoa;
        private System.Windows.Forms.ComboBox cboHangHoa;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblDonGiaNhap;
        private System.Windows.Forms.TextBox txtDonGiaNhap;
        private System.Windows.Forms.Button btnThemDong;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Panel pnlDanhSach;
        private System.Windows.Forms.Label lblTieuDeDanhSach;
        private System.Windows.Forms.Label lblTongTienNhap;
        private System.Windows.Forms.Label lblDaThanhToan;
        private System.Windows.Forms.TextBox txtDaThanhToan;
        private System.Windows.Forms.Label lblConNoNCC;
        private System.Windows.Forms.TextBox txtConNoNCC;
        private System.Windows.Forms.DataGridView dgvChiTietNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDVT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
    }
}