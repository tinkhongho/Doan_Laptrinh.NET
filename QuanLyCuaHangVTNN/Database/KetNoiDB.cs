using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangVTNN
{
    /// <summary>
    /// Lớp dùng chung để quản lý chuỗi kết nối SQL Server và thông tin tài khoản đăng nhập.
    /// Thiết kế đúng theo mô hình ADO.NET (SqlConnection, SqlDataAdapter, DataSet) trên lớp.
    /// </summary>
    public static class KetNoiDB
    {
        // Máy của bạn là TrongTin_PC\SQLEXPRESS (dùng .\SQLEXPRESS sẽ chạy được cho cả máy bạn và máy nhóm)
        public static string strKetNoi = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLVTNN;Integrated Security=True";

        // Lưu thông tin người dùng sau khi đăng nhập để phân quyền
        public static string TenDangNhap = "";
        public static string HoTen = "";
        public static string QuyenHan = ""; // "Quản lý" hoặc "Nhân viên"

        /// <summary>
        /// Tạo mới một đối tượng SqlConnection từ chuỗi kết nối chuẩn
        /// </summary>
        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(strKetNoi);
        }

        /// <summary>
        /// Mở kết nối an toàn (theo đúng hàm MoKetNoi trong bài thực hành của cô)
        /// </summary>
        public static void MoKetNoi(SqlConnection cn)
        {
            if (cn.State == ConnectionState.Open)
                cn.Close();
            cn.Open();
        }
    }
}
