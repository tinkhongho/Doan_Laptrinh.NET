namespace QuanLyCuaHangVTNN
{
    partial class frmTrangChu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuNhomHangHoa;
        private System.Windows.Forms.ToolStripMenuItem mnuLoaiHang;
        private System.Windows.Forms.ToolStripMenuItem mnuHangHoa;
        private System.Windows.Forms.ToolStripMenuItem mnuNhapHang;
        private System.Windows.Forms.ToolStripMenuItem mnuNhomBanHang;
        private System.Windows.Forms.ToolStripMenuItem mnuBanHang;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhSachHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuThongKeDoanhThu;
        private System.Windows.Forms.ToolStripMenuItem mnuNhomCongNo;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachHangCongNo;
        private System.Windows.Forms.ToolStripMenuItem mnuNhaCungCapCongNo;
        private System.Windows.Forms.ToolStripMenuItem mnuThuTraNo;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTrangThaiUser;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuNhomHangHoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoaiHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHangHoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhapHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhomBanHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBanHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhSachHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongKeDoanhThu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhomCongNo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachHangCongNo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhaCungCapCongNo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThuTraNo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTrangThaiUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuNhomHangHoa,
                this.mnuNhomBanHang,
                this.mnuNhomCongNo,
                this.mnuHeThong});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 24);
            this.menuStrip1.TabIndex = 0;
            // 
            // mnuNhomHangHoa
            // 
            this.mnuNhomHangHoa.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuLoaiHang,
                this.mnuHangHoa,
                this.mnuNhapHang});
            this.mnuNhomHangHoa.Name = "mnuNhomHangHoa";
            this.mnuNhomHangHoa.Size = new System.Drawing.Size(122, 20);
            this.mnuNhomHangHoa.Text = "1. Quản lý Hàng hóa";
            // 
            // mnuLoaiHang
            // 
            this.mnuLoaiHang.Name = "mnuLoaiHang";
            this.mnuLoaiHang.Size = new System.Drawing.Size(210, 22);
            this.mnuLoaiHang.Text = "Danh mục Loại vật tư";
            this.mnuLoaiHang.Click += new System.EventHandler(this.mnuLoaiHang_Click);
            // 
            // mnuHangHoa
            // 
            this.mnuHangHoa.Name = "mnuHangHoa";
            this.mnuHangHoa.Size = new System.Drawing.Size(210, 22);
            this.mnuHangHoa.Text = "Quản lý Hàng hóa & Tồn kho";
            this.mnuHangHoa.Click += new System.EventHandler(this.mnuHangHoa_Click);
            // 
            // mnuNhapHang
            // 
            this.mnuNhapHang.Name = "mnuNhapHang";
            this.mnuNhapHang.Size = new System.Drawing.Size(210, 22);
            this.mnuNhapHang.Text = "Nhập hàng từ Nhà cung cấp";
            this.mnuNhapHang.Click += new System.EventHandler(this.mnuNhapHang_Click);
            // 
            // mnuNhomBanHang
            // 
            this.mnuNhomBanHang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuBanHang,
                this.mnuDanhSachHoaDon,
                this.mnuThongKeDoanhThu});
            this.mnuNhomBanHang.Name = "mnuNhomBanHang";
            this.mnuNhomBanHang.Size = new System.Drawing.Size(156, 20);
            this.mnuNhomBanHang.Text = "2. Bán hàng & Doanh thu";
            // 
            // mnuBanHang
            // 
            this.mnuBanHang.Name = "mnuBanHang";
            this.mnuBanHang.Size = new System.Drawing.Size(220, 22);
            this.mnuBanHang.Text = "Lập Hóa đơn Bán hàng";
            this.mnuBanHang.Click += new System.EventHandler(this.mnuBanHang_Click);
            // 
            // mnuDanhSachHoaDon
            // 
            this.mnuDanhSachHoaDon.Name = "mnuDanhSachHoaDon";
            this.mnuDanhSachHoaDon.Size = new System.Drawing.Size(220, 22);
            this.mnuDanhSachHoaDon.Text = "Danh sách Hóa đơn đã bán";
            this.mnuDanhSachHoaDon.Click += new System.EventHandler(this.mnuDanhSachHoaDon_Click);
            // 
            // mnuThongKeDoanhThu
            // 
            this.mnuThongKeDoanhThu.Name = "mnuThongKeDoanhThu";
            this.mnuThongKeDoanhThu.Size = new System.Drawing.Size(220, 22);
            this.mnuThongKeDoanhThu.Text = "Thống kê Doanh thu & Lợi nhuận";
            this.mnuThongKeDoanhThu.Click += new System.EventHandler(this.mnuThongKeDoanhThu_Click);
            // 
            // mnuNhomCongNo
            // 
            this.mnuNhomCongNo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuKhachHangCongNo,
                this.mnuNhaCungCapCongNo,
                this.mnuThuTraNo});
            this.mnuNhomCongNo.Name = "mnuNhomCongNo";
            this.mnuNhomCongNo.Size = new System.Drawing.Size(118, 20);
            this.mnuNhomCongNo.Text = "3. Quản lý Công nợ";
            // 
            // mnuKhachHangCongNo
            // 
            this.mnuKhachHangCongNo.Name = "mnuKhachHangCongNo";
            this.mnuKhachHangCongNo.Size = new System.Drawing.Size(220, 22);
            this.mnuKhachHangCongNo.Text = "Khách hàng & Công nợ phải thu";
            this.mnuKhachHangCongNo.Click += new System.EventHandler(this.mnuKhachHangCongNo_Click);
            // 
            // mnuNhaCungCapCongNo
            // 
            this.mnuNhaCungCapCongNo.Name = "mnuNhaCungCapCongNo";
            this.mnuNhaCungCapCongNo.Size = new System.Drawing.Size(220, 22);
            this.mnuNhaCungCapCongNo.Text = "Nhà cung cấp & Công nợ phải trả";
            this.mnuNhaCungCapCongNo.Click += new System.EventHandler(this.mnuNhaCungCapCongNo_Click);
            // 
            // mnuThuTraNo
            // 
            this.mnuThuTraNo.Name = "mnuThuTraNo";
            this.mnuThuTraNo.Size = new System.Drawing.Size(220, 22);
            this.mnuThuTraNo.Text = "Lập phiếu Thu nợ / Trả nợ";
            this.mnuThuTraNo.Click += new System.EventHandler(this.mnuThuTraNo_Click);
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuDangXuat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(79, 20);
            this.mnuHeThong.Text = "4. Hệ thống";
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(150, 22);
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblTrangThaiUser});
            this.statusStrip1.Location = new System.Drawing.Point(0, 528);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // lblTrangThaiUser
            // 
            this.lblTrangThaiUser.Name = "lblTrangThaiUser";
            this.lblTrangThaiUser.Size = new System.Drawing.Size(100, 17);
            this.lblTrangThaiUser.Text = "Trạng thái";
            // 
            // frmTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmTrangChu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PHẦN MỀM QUẢN LÝ CỬA HÀNG VẬT TƯ NÔNG NGHIỆP";
            this.Load += new System.EventHandler(this.frmTrangChu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
