namespace QuanLyCuaHangVTNN
{
    partial class frmDanhSachHoaDon
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.Panel pnlVienBoLoc;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.CheckBox chkLocNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.ComboBox cboTrangThaiNo;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlTongHopLoc;
        private System.Windows.Forms.Label lblTongSoHD;
        private System.Windows.Forms.Label lblTongDoanhThuLoc;
        private System.Windows.Forms.Label lblTongDaThuLoc;
        private System.Windows.Forms.Label lblTongNoLoc;
        private System.Windows.Forms.Panel pnlDanhSachHD;
        private System.Windows.Forms.Label lblTieuDeHD;
        private System.Windows.Forms.Button btnXoaHoaDon;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.Panel pnlChiTietHD;
        private System.Windows.Forms.Label lblTieuDeCT;
        private System.Windows.Forms.DataGridView dgvChiTietHoaDon;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBoLoc = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cboTrangThaiNo = new System.Windows.Forms.ComboBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.chkLocNgay = new System.Windows.Forms.CheckBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTim = new System.Windows.Forms.Label();
            this.pnlVienBoLoc = new System.Windows.Forms.Panel();
            this.pnlTongHopLoc = new System.Windows.Forms.Panel();
            this.lblTongNoLoc = new System.Windows.Forms.Label();
            this.lblTongDaThuLoc = new System.Windows.Forms.Label();
            this.lblTongDoanhThuLoc = new System.Windows.Forms.Label();
            this.lblTongSoHD = new System.Windows.Forms.Label();
            this.pnlDanhSachHD = new System.Windows.Forms.Panel();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.btnXoaHoaDon = new System.Windows.Forms.Button();
            this.lblTieuDeHD = new System.Windows.Forms.Label();
            this.pnlChiTietHD = new System.Windows.Forms.Panel();
            this.dgvChiTietHoaDon = new System.Windows.Forms.DataGridView();
            this.lblTieuDeCT = new System.Windows.Forms.Label();
            this.pnlBoLoc.SuspendLayout();
            this.pnlTongHopLoc.SuspendLayout();
            this.pnlDanhSachHD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.pnlChiTietHD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBoLoc
            // 
            this.pnlBoLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBoLoc.BackColor = System.Drawing.Color.White;
            this.pnlBoLoc.Controls.Add(this.btnLamMoi);
            this.pnlBoLoc.Controls.Add(this.btnTimKiem);
            this.pnlBoLoc.Controls.Add(this.cboTrangThaiNo);
            this.pnlBoLoc.Controls.Add(this.dtpDenNgay);
            this.pnlBoLoc.Controls.Add(this.lblDen);
            this.pnlBoLoc.Controls.Add(this.dtpTuNgay);
            this.pnlBoLoc.Controls.Add(this.chkLocNgay);
            this.pnlBoLoc.Controls.Add(this.txtTimKiem);
            this.pnlBoLoc.Controls.Add(this.lblTim);
            this.pnlBoLoc.Controls.Add(this.pnlVienBoLoc);
            this.pnlBoLoc.Location = new System.Drawing.Point(16, 14);
            this.pnlBoLoc.Name = "pnlBoLoc";
            this.pnlBoLoc.Size = new System.Drawing.Size(1013, 62);
            this.pnlBoLoc.TabIndex = 0;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.btnLamMoi.Location = new System.Drawing.Point(926, 16);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(73, 32);
            this.btnLamMoi.TabIndex = 8;
            this.btnLamMoi.Text = "Mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(838, 16);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(80, 32);
            this.btnTimKiem.TabIndex = 7;
            this.btnTimKiem.Text = "Tìm lọc";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cboTrangThaiNo
            // 
            this.cboTrangThaiNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTrangThaiNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiNo.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.cboTrangThaiNo.FormattingEnabled = true;
            this.cboTrangThaiNo.Items.AddRange(new object[] {
            "Tất cả hóa đơn",
            "Đã thanh toán đủ",
            "Còn nợ mùa vụ"});
            this.cboTrangThaiNo.Location = new System.Drawing.Point(664, 17);
            this.cboTrangThaiNo.Name = "cboTrangThaiNo";
            this.cboTrangThaiNo.Size = new System.Drawing.Size(164, 29);
            this.cboTrangThaiNo.TabIndex = 6;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(532, 17);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(118, 29);
            this.dtpDenNgay.TabIndex = 5;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.lblDen.Location = new System.Drawing.Point(492, 21);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(38, 21);
            this.lblDen.TabIndex = 4;
            this.lblDen.Text = "Đến:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(366, 17);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(118, 29);
            this.dtpTuNgay.TabIndex = 3;
            // 
            // chkLocNgay
            // 
            this.chkLocNgay.AutoSize = true;
            this.chkLocNgay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkLocNgay.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.chkLocNgay.Location = new System.Drawing.Point(306, 20);
            this.chkLocNgay.Name = "chkLocNgay";
            this.chkLocNgay.Size = new System.Drawing.Size(55, 25);
            this.chkLocNgay.TabIndex = 2;
            this.chkLocNgay.Text = "Từ:";
            this.chkLocNgay.UseVisualStyleBackColor = true;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTimKiem.Location = new System.Drawing.Point(92, 17);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(202, 29);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblTim
            // 
            this.lblTim.AutoSize = true;
            this.lblTim.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold);
            this.lblTim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblTim.Location = new System.Drawing.Point(14, 21);
            this.lblTim.Name = "lblTim";
            this.lblTim.Size = new System.Drawing.Size(76, 21);
            this.lblTim.TabIndex = 0;
            this.lblTim.Text = "Tìm kiếm:";
            // 
            // pnlVienBoLoc
            // 
            this.pnlVienBoLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.pnlVienBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVienBoLoc.Location = new System.Drawing.Point(0, 0);
            this.pnlVienBoLoc.Name = "pnlVienBoLoc";
            this.pnlVienBoLoc.Size = new System.Drawing.Size(1013, 4);
            this.pnlVienBoLoc.TabIndex = 9;
            // 
            // pnlTongHopLoc
            // 
            this.pnlTongHopLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTongHopLoc.BackColor = System.Drawing.Color.White;
            this.pnlTongHopLoc.Controls.Add(this.lblTongNoLoc);
            this.pnlTongHopLoc.Controls.Add(this.lblTongDaThuLoc);
            this.pnlTongHopLoc.Controls.Add(this.lblTongDoanhThuLoc);
            this.pnlTongHopLoc.Controls.Add(this.lblTongSoHD);
            this.pnlTongHopLoc.Location = new System.Drawing.Point(16, 86);
            this.pnlTongHopLoc.Name = "pnlTongHopLoc";
            this.pnlTongHopLoc.Size = new System.Drawing.Size(1013, 42);
            this.pnlTongHopLoc.TabIndex = 1;
            // 
            // lblTongNoLoc
            // 
            this.lblTongNoLoc.AutoSize = true;
            this.lblTongNoLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongNoLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this.lblTongNoLoc.Location = new System.Drawing.Point(760, 10);
            this.lblTongNoLoc.Name = "lblTongNoLoc";
            this.lblTongNoLoc.Size = new System.Drawing.Size(105, 21);
            this.lblTongNoLoc.TabIndex = 3;
            this.lblTongNoLoc.Text = "Còn nợ: 0 đ";
            // 
            // lblTongDaThuLoc
            // 
            this.lblTongDaThuLoc.AutoSize = true;
            this.lblTongDaThuLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongDaThuLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblTongDaThuLoc.Location = new System.Drawing.Point(505, 10);
            this.lblTongDaThuLoc.Name = "lblTongDaThuLoc";
            this.lblTongDaThuLoc.Size = new System.Drawing.Size(98, 21);
            this.lblTongDaThuLoc.TabIndex = 2;
            this.lblTongDaThuLoc.Text = "Đã thu: 0 đ";
            // 
            // lblTongDoanhThuLoc
            // 
            this.lblTongDoanhThuLoc.AutoSize = true;
            this.lblTongDoanhThuLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThuLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.lblTongDoanhThuLoc.Location = new System.Drawing.Point(215, 10);
            this.lblTongDoanhThuLoc.Name = "lblTongDoanhThuLoc";
            this.lblTongDoanhThuLoc.Size = new System.Drawing.Size(166, 21);
            this.lblTongDoanhThuLoc.TabIndex = 1;
            this.lblTongDoanhThuLoc.Text = "Tổng doanh thu: 0 đ";
            // 
            // lblTongSoHD
            // 
            this.lblTongSoHD.AutoSize = true;
            this.lblTongSoHD.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongSoHD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.lblTongSoHD.Location = new System.Drawing.Point(14, 10);
            this.lblTongSoHD.Name = "lblTongSoHD";
            this.lblTongSoHD.Size = new System.Drawing.Size(113, 21);
            this.lblTongSoHD.TabIndex = 0;
            this.lblTongSoHD.Text = "Số hóa đơn: 0";
            // 
            // pnlDanhSachHD
            // 
            this.pnlDanhSachHD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDanhSachHD.BackColor = System.Drawing.Color.White;
            this.pnlDanhSachHD.Controls.Add(this.dgvHoaDon);
            this.pnlDanhSachHD.Controls.Add(this.btnXoaHoaDon);
            this.pnlDanhSachHD.Controls.Add(this.lblTieuDeHD);
            this.pnlDanhSachHD.Location = new System.Drawing.Point(16, 138);
            this.pnlDanhSachHD.Name = "pnlDanhSachHD";
            this.pnlDanhSachHD.Size = new System.Drawing.Size(1013, 232);
            this.pnlDanhSachHD.TabIndex = 2;
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHoaDon.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHoaDon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHoaDon.ColumnHeadersHeight = 36;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHoaDon.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHoaDon.EnableHeadersVisualStyles = false;
            this.dgvHoaDon.Location = new System.Drawing.Point(14, 40);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.RowHeadersVisible = false;
            this.dgvHoaDon.RowTemplate.Height = 30;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.Size = new System.Drawing.Size(985, 180);
            this.dgvHoaDon.TabIndex = 2;
            this.dgvHoaDon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoaDon_CellClick);
            // 
            // btnXoaHoaDon
            // 
            this.btnXoaHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoaHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnXoaHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaHoaDon.FlatAppearance.BorderSize = 0;
            this.btnXoaHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaHoaDon.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnXoaHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnXoaHoaDon.Location = new System.Drawing.Point(849, 6);
            this.btnXoaHoaDon.Name = "btnXoaHoaDon";
            this.btnXoaHoaDon.Size = new System.Drawing.Size(150, 28);
            this.btnXoaHoaDon.TabIndex = 1;
            this.btnXoaHoaDon.Text = "Xóa hóa đơn chọn";
            this.btnXoaHoaDon.UseVisualStyleBackColor = false;
            this.btnXoaHoaDon.Click += new System.EventHandler(this.btnXoaHoaDon_Click);
            // 
            // lblTieuDeHD
            // 
            this.lblTieuDeHD.AutoSize = true;
            this.lblTieuDeHD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeHD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.lblTieuDeHD.Location = new System.Drawing.Point(14, 10);
            this.lblTieuDeHD.Name = "lblTieuDeHD";
            this.lblTieuDeHD.Size = new System.Drawing.Size(446, 23);
            this.lblTieuDeHD.TabIndex = 0;
            this.lblTieuDeHD.Text = "DANH SÁCH HÓA ĐƠN ĐÃ BÁN (Chọn dòng để xem chi tiết)";
            // 
            // pnlChiTietHD
            // 
            this.pnlChiTietHD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlChiTietHD.BackColor = System.Drawing.Color.White;
            this.pnlChiTietHD.Controls.Add(this.dgvChiTietHoaDon);
            this.pnlChiTietHD.Controls.Add(this.lblTieuDeCT);
            this.pnlChiTietHD.Location = new System.Drawing.Point(16, 380);
            this.pnlChiTietHD.Name = "pnlChiTietHD";
            this.pnlChiTietHD.Size = new System.Drawing.Size(1013, 205);
            this.pnlChiTietHD.TabIndex = 3;
            // 
            // dgvChiTietHoaDon
            // 
            this.dgvChiTietHoaDon.AllowUserToAddRows = false;
            this.dgvChiTietHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChiTietHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTietHoaDon.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTietHoaDon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvChiTietHoaDon.ColumnHeadersHeight = 36;
            this.dgvChiTietHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTietHoaDon.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvChiTietHoaDon.EnableHeadersVisualStyles = false;
            this.dgvChiTietHoaDon.Location = new System.Drawing.Point(14, 38);
            this.dgvChiTietHoaDon.Name = "dgvChiTietHoaDon";
            this.dgvChiTietHoaDon.ReadOnly = true;
            this.dgvChiTietHoaDon.RowHeadersVisible = false;
            this.dgvChiTietHoaDon.RowTemplate.Height = 30;
            this.dgvChiTietHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietHoaDon.Size = new System.Drawing.Size(985, 155);
            this.dgvChiTietHoaDon.TabIndex = 1;
            // 
            // lblTieuDeCT
            // 
            this.lblTieuDeCT.AutoSize = true;
            this.lblTieuDeCT.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeCT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.lblTieuDeCT.Location = new System.Drawing.Point(14, 10);
            this.lblTieuDeCT.Name = "lblTieuDeCT";
            this.lblTieuDeCT.Size = new System.Drawing.Size(277, 23);
            this.lblTieuDeCT.TabIndex = 0;
            this.lblTieuDeCT.Text = "CHI TIẾT MẶT HÀNG CỦA HÓA ĐƠN";
            // 
            // frmDanhSachHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1045, 599);
            this.Controls.Add(this.pnlChiTietHD);
            this.Controls.Add(this.pnlDanhSachHD);
            this.Controls.Add(this.pnlTongHopLoc);
            this.Controls.Add(this.pnlBoLoc);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "frmDanhSachHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh sách Hóa đơn đã bán";
            this.Load += new System.EventHandler(this.frmDanhSachHoaDon_Load);
            this.pnlBoLoc.ResumeLayout(false);
            this.pnlBoLoc.PerformLayout();
            this.pnlTongHopLoc.ResumeLayout(false);
            this.pnlTongHopLoc.PerformLayout();
            this.pnlDanhSachHD.ResumeLayout(false);
            this.pnlDanhSachHD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.pnlChiTietHD.ResumeLayout(false);
            this.pnlChiTietHD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHoaDon)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}