using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmLoaiHang : Form
    {
        DataSet ds = new DataSet("dsQLVTNN");
        SqlDataAdapter daLoaiHang;

        public frmLoaiHang()
        {
            InitializeComponent();
        }
    }
}
