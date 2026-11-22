using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    internal static class Program
    {
        // Bật chế độ High-DPI giúp chữ và giao diện sắc nét 100% trên màn hình Laptop (125% - 150% scale)
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// Điểm bắt đầu chạy chương trình Quản lý cửa hàng Vật tư nông nghiệp.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDangNhap());
        }
    }
}
