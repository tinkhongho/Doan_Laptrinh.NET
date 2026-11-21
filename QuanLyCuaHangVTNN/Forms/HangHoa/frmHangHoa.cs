using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangVTNN
{
    public partial class frmHangHoa : Form
    {
        DataSet ds = new DataSet("dsQLVTNN");
        SqlDataAdapter daLoaiHang;
        SqlDataAdapter daHangHoa;

        public frmHangHoa()
        {
            InitializeComponent();
        }
    }
}
