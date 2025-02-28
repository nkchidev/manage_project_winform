using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;

namespace ProjectStorage
{
    public partial class FrmDangnhap : Office2007Form
    {
        public FrmDangnhap()
        {
            InitializeComponent();
        }

        private void ButtonX1Click(object sender, EventArgs e)
        {
            var taikhoan = textBoxX1.Text.Trim();
            var matkhau = textBoxX2.Text.Trim();
            var cnn = new DataConnect();
            cnn.Mo();
            var cmd = new SqlCommand("select * from nguoidung where taikhoan=@taikhoan");
            cmd.Parameters.Add("taikhoan", SqlDbType.NVarChar).Value = taikhoan;
            cnn.Doc(cmd);
            DataTable tbl = cnn;
            foreach (DataRow row in tbl.Rows)
            {
                if (taikhoan == row[0].ToString())
                {
                    if (matkhau == row[1].ToString())
                    {
                        var form = (FrmMain1)this.MdiParent;
                        this.Close();
                        form.Mokhoa();
                        if (row[2].ToString().Trim()=="User")
                        {
                            form.Quyen();
                        }
                    }
                    else
                        MessageBoxEx.Show("Sai tên mật khẩu !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBoxEx.Show("Sai tên tài khoản !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cnn.Dong();
        }

        private void ButtonX2Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}