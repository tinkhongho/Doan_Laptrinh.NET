using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangVTNN
{
    public static class KetNoiDB
    {
        public static string strKetNoi = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLVTNN;Integrated Security=True";

        public static string TenDangNhap = "";
        public static string HoTen = "";
        public static string QuyenHan = "";

        /// <summary>
        /// Khởi tạo đối tượng kết nối SQL Server.
        /// </summary>
        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(strKetNoi);
        }

        /// <summary>
        /// Kiểm tra và mở kết nối cơ sở dữ liệu.
        /// </summary>
        public static void MoKetNoi(SqlConnection cn)
        {
            if (cn.State == ConnectionState.Open)
                cn.Close();
            cn.Open();
        }
    }
}
