using System;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    internal static class Program
    {
        /// <summary>
        /// Điểm bắt đầu chạy chương trình Quản lý cửa hàng Vật tư nông nghiệp.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDangNhap());
        }
    }
}
