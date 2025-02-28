using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
namespace ProjectStorage
{
    public partial class FrmDoimatkhau : Office2007Form
    {
        public FrmDoimatkhau()
        {
            InitializeComponent();
        }
        
        private void ButtonX1Click(object sender, EventArgs e)
        {
            var cnn = new DataConnect();
            cnn.Mo();
            var cmd = new SqlCommand("select * from nguoidung");
            cnn.Doc(cmd);
            DataTable tbl = cnn;
            cnn.Dong();
            foreach (DataRow row in tbl.Rows)
            {
                if (textBoxX1.Text.Trim() == row[0].ToString())
                {
                    if (txtOldPassword.Text.Trim() == row[1].ToString())
                    {
                        if (txtNewPassword.Text == txtReNPassword.Text)
                        {
                            var cnn2 = new DataConnect();
                            cnn2.Mo();
                            var cmd2 = new SqlCommand("update nguoidung set matkhau=@matkhau where taikhoan=@taikhoan");
                            cmd2.Parameters.Add("matkhau", SqlDbType.NVarChar).Value = txtReNPassword.Text;
                            cmd2.Parameters.Add("taikhoan", SqlDbType.NVarChar).Value = textBoxX1.Text;
                            cnn2.ThucThi(cmd2);
                            MessageBoxEx.Show("Đổi Mật Khẩu Thành Công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else

                        MessageBoxEx.Show("Xác nhận mật khẩu không đúng !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            cnn.Dong();
        }

        private void ButtonX2Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}