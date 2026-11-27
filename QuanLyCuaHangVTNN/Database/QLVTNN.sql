-- CƠ SỞ DỮ LIỆU QUẢN LÝ CỬA HÀNG VẬT TƯ NÔNG NGHIỆP
CREATE DATABASE QLVTNN;
GO
USE QLVTNN;
GO

-- Bảng Tài khoản
CREATE TABLE taikhoan (
    tendangnhap NVARCHAR(30) PRIMARY KEY,
    matkhau     NVARCHAR(50) NOT NULL,
    hoten       NVARCHAR(100) NOT NULL,
    quyenhan    NVARCHAR(30) NOT NULL
);

-- Bảng Loại hàng
CREATE TABLE loaihang (
    maloai      NVARCHAR(10) PRIMARY KEY,
    tenloai     NVARCHAR(100) NOT NULL,
    ghichu      NVARCHAR(200)
);

-- Bảng Nhà cung cấp
CREATE TABLE nhacungcap (
    mancc       NVARCHAR(10) PRIMARY KEY,
    tenncc      NVARCHAR(150) NOT NULL,
    sdt         NVARCHAR(15),
    diachi      NVARCHAR(200),
    congnoncc   DECIMAL(18, 0) DEFAULT 0
);

-- Bảng Hàng hóa
CREATE TABLE hanghoa (
    mahh        NVARCHAR(10) PRIMARY KEY,
    tenhh       NVARCHAR(150) NOT NULL,
    maloai      NVARCHAR(10) REFERENCES loaihang(maloai),
    dvt         NVARCHAR(30) NOT NULL,
    hoatchat    NVARCHAR(150),
    giavon      DECIMAL(18, 0) DEFAULT 0,
    giaban      DECIMAL(18, 0) DEFAULT 0,
    soluongton  INT DEFAULT 0,
    hansudung   DATE
);

-- Bảng Khách hàng
CREATE TABLE khachhang (
    makh        NVARCHAR(10) PRIMARY KEY,
    tenkh       NVARCHAR(150) NOT NULL,
    sdt         NVARCHAR(15),
    diachi      NVARCHAR(200),
    hanmucno    DECIMAL(18, 0) DEFAULT 20000000,
    sonohientai DECIMAL(18, 0) DEFAULT 0
);

-- Bảng Phiếu nhập hàng
CREATE TABLE phieunhap (
    mapn        NVARCHAR(15) PRIMARY KEY,
    ngaynhap    DATE DEFAULT GETDATE(),
    mancc       NVARCHAR(10) REFERENCES nhacungcap(mancc),
    tongtien    DECIMAL(18, 0) DEFAULT 0,
    datrancc    DECIMAL(18, 0) DEFAULT 0,
    connoncc    DECIMAL(18, 0) DEFAULT 0
);

-- Bảng Chi tiết phiếu nhập
CREATE TABLE ct_phieunhap (
    mapn        NVARCHAR(15) REFERENCES phieunhap(mapn),
    mahh        NVARCHAR(10) REFERENCES hanghoa(mahh),
    soluong     INT NOT NULL,
    dongianhap  DECIMAL(18, 0) NOT NULL,
    thanhtien   DECIMAL(18, 0) NOT NULL,
    PRIMARY KEY (mapn, mahh)
);

-- Bảng Hóa đơn bán hàng
CREATE TABLE hoadon (
    mahd        NVARCHAR(15) PRIMARY KEY,
    ngayban     DATE DEFAULT GETDATE(),
    makh        NVARCHAR(10) REFERENCES khachhang(makh),
    tongtien    DECIMAL(18, 0) DEFAULT 0,
    dathanhtoan DECIMAL(18, 0) DEFAULT 0,
    conno       DECIMAL(18, 0) DEFAULT 0,
    ghichu      NVARCHAR(200)
);

-- Bảng Chi tiết hóa đơn
CREATE TABLE ct_hoadon (
    mahd        NVARCHAR(15) REFERENCES hoadon(mahd),
    mahh        NVARCHAR(10) REFERENCES hanghoa(mahh),
    soluong     INT NOT NULL,
    dongiaban   DECIMAL(18, 0) NOT NULL,
    giavon      DECIMAL(18, 0) NOT NULL,
    thanhtien   DECIMAL(18, 0) NOT NULL,
    PRIMARY KEY (mahd, mahh)
);

-- Bảng Phiếu thu trả nợ
CREATE TABLE phieuthutrano (
    maphieu     NVARCHAR(15) PRIMARY KEY,
    ngaylap     DATE DEFAULT GETDATE(),
    loaiphieu   NVARCHAR(30) NOT NULL,
    makh        NVARCHAR(10) NULL REFERENCES khachhang(makh),
    mancc       NVARCHAR(10) NULL REFERENCES nhacungcap(mancc),
    sotien      DECIMAL(18, 0) NOT NULL,
    ghichu      NVARCHAR(200)
);
GO

-- Dữ liệu mẫu
INSERT INTO taikhoan VALUES (N'admin', N'123', N'Nguyễn Văn Chủ (Quản lý)', N'Quản lý');
INSERT INTO taikhoan VALUES (N'nv01',  N'123', N'Trần Thị Nhân Viên',       N'Nhân viên');

INSERT INTO loaihang VALUES (N'LH01', N'Phân bón',              N'Phân vô cơ, hữu cơ, phân lá');
INSERT INTO loaihang VALUES (N'LH02', N'Thuốc bảo vệ thực vật', N'Thuốc trừ sâu, trừ bệnh, trừ cỏ');
INSERT INTO loaihang VALUES (N'LH03', N'Hạt giống',             N'Giống lúa, giống rau màu');
INSERT INTO loaihang VALUES (N'LH04', N'Dụng cụ nông nghiệp',   N'Bình xịt, vòi phun, lưới');

INSERT INTO nhacungcap VALUES (N'NCC01', N'Công ty Phân bón Bình Điền', N'02837560111', N'Long An',   3000000);
INSERT INTO nhacungcap VALUES (N'NCC02', N'Tập đoàn Lộc Trời',          N'02963841299', N'An Giang',  0);

INSERT INTO hanghoa VALUES (N'HH01', N'Phân NPK Đầu Trâu 20-20-15 (Bao 50kg)', N'LH01', N'Bao',  N'Đạm 20%, Lân 20%, Kali 15%', 650000, 720000, 45, '2027-12-31');
INSERT INTO hanghoa VALUES (N'HH02', N'Phân Urê Phú Mỹ (Bao 50kg)',             N'LH01', N'Bao',  N'Đạm 46%',                    480000, 540000, 60, '2027-10-15');
INSERT INTO hanghoa VALUES (N'HH03', N'Thuốc trừ bệnh đạo ôn Filia 525SE',      N'LH02', N'Chai', N'Tricyclazole + Propiconazole',120000, 145000, 80, '2027-06-30');
INSERT INTO hanghoa VALUES (N'HH04', N'Thuốc trừ rầy Chess 50WG (Gói 15g)',     N'LH02', N'Gói',  N'Pymetrozine 50%',             18000,  25000, 200, '2027-05-20');
INSERT INTO hanghoa VALUES (N'HH05', N'Lúa giống xác nhận OM18 (Bao 20kg)',     N'LH03', N'Bao',  N'Giống lúa thơm ngắn ngày',   360000, 420000, 35, '2027-03-01');

INSERT INTO khachhang VALUES (N'KH01', N'Nguyễn Văn Tư (Ấp 1)', N'0918111222', N'Ấp 1, Xã Tân Phú', 30000000, 3200000);
INSERT INTO khachhang VALUES (N'KH02', N'Lê Văn Bảy (Ấp 3)',    N'0909333444', N'Ấp 3, Xã Hòa Bình',20000000, 0);
INSERT INTO khachhang VALUES (N'KH03', N'Trần Quốc Hòa',        N'0988555666', N'Thị trấn Phú Mỹ',  50000000, 11500000);

INSERT INTO phieunhap VALUES (N'PN001', '2026-11-01', N'NCC01', 15000000, 10000000, 5000000);
INSERT INTO ct_phieunhap VALUES (N'PN001', N'HH01', 10, 650000, 6500000);
INSERT INTO ct_phieunhap VALUES (N'PN001', N'HH02', 10, 480000, 4800000);
INSERT INTO ct_phieunhap VALUES (N'PN001', N'HH05', 10, 370000, 3700000);

INSERT INTO hoadon VALUES (N'HD001', '2026-11-10', N'KH01', 7200000, 3000000, 4200000, N'Mua phân bón vụ Đông Xuân - Hẹn cuối vụ trả');
INSERT INTO ct_hoadon VALUES (N'HD001', N'HH01', 10, 720000, 650000, 7200000);

INSERT INTO hoadon VALUES (N'HD002', '2026-11-15', N'KH02', 1450000, 1450000, 0, N'Thanh toán đủ tiền mặt');
INSERT INTO ct_hoadon VALUES (N'HD002', N'HH03', 10, 145000, 120000, 1450000);

INSERT INTO hoadon VALUES (N'HD003', '2026-11-16', N'KH03', 13500000, 2000000, 11500000, N'Mua phân Urê bón đợt 1 - Ghi nợ mùa vụ');
INSERT INTO ct_hoadon VALUES (N'HD003', N'HH02', 25, 540000, 480000, 13500000);

INSERT INTO phieuthutrano VALUES (N'PT001', '2026-11-18', N'Thu nợ khách', N'KH01', NULL, 1000000, N'Khách trả bớt tiền phân bón đợt 1');
INSERT INTO phieuthutrano VALUES (N'PC001', '2026-11-19', N'Trả nợ NCC',   NULL, N'NCC01', 2000000, N'Thanh toán bớt nợ cho Công ty Bình Điền');
GO
