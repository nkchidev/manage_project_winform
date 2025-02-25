using DevExpress.Images;
using ProjectStorage.Properties;
using SimpleBroker;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VBSQLHelper;

namespace ProjectStorage    
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {

        //private static FrmLogin _defaultInstance;
        //public static FrmLogin Instance
        //{
        //    get
        //    {
        //        if (_defaultInstance == null || _defaultInstance.IsDisposed)
        //        {
        //            _defaultInstance = new FrmLogin();
        //        }
        //        return _defaultInstance;
        //    }
        //    set => _defaultInstance = value;
        //}
        public FrmLogin()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += FrmLogin_KeyDown;
            txt_username.Text = Properties.Settings.Default.currentusername;
           
        }

        protected override void OnShown(EventArgs e)
        {
#if DEBUG
            //txt_username.Text = "admin";
            //txt_password.Text = "123456";
#endif
            this.BringToFront();

            if (txt_username.Text != "")
            {
                Task.Factory.StartNew(() =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        txt_password.Focus();
                    }));
                });

            }
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btn_login_Click(sender, e);
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }

        private  void btn_login_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txt_username.Text.Trim()))
            {
                lbl_message.Text = $"Chưa nhập tên tài khoản.";
                txt_username.Focus();
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
                return;
            }

            if (string.IsNullOrEmpty(txt_password.Text.Trim()))
            {
                lbl_message.Text = $"Chưa nhập mật khẩu.";
                txt_password.SelectAll();
                txt_password.Focus();
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
                return;
            }
            DatabaseUtils.getInstance().initUserSystem();

            int per = DatabaseUtils.getInstance().getUserPermission(txt_username.Text.Trim(), txt_password.Text.Trim());

            if (per == 0)
            {
                lbl_message.Text = $"Tên đăng nhập hoặc mật khẩu không đúng.";
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
            }
            else
            {
               
                // đăng nhập ok store data
                Mdl_Share.currentUser = SQLHelper.ExecQueryDataFirstOrDefaultAsync<User>($"select * from NGUOIDUNG WHERE taikhoan=N'{txt_username.Text.Trim()}'");
                var sql = $@"SELECT
                            [ct].[MaCT],
                            [ct].[TenCT],
                            [pq].[id],
                                   [pq].[taikhoan],    
                                   isnull([pq].[add],0) as [add],
                                   isnull([pq].[edit],0) as [edit],
                                   isnull([pq].[delete],0) as [delete] FROM (
                                    SELECT 'TB1' AS MaCT, N'Danh mục thiết bị' AS TenCT
								   UNION all
								   SELECT 'TB2' AS MaCT, N'Mượn thiết bị' AS TenCT
								   UNION all
								   SELECT 'TB3' AS MaCT, N'Trả thiết bị' AS TenCT
								   UNION all
                            SELECT 'ALL' AS MaCT, N'Thêm mới dự án' AS TenCT
                            UNION all
                            SELECT [MaCT], [TenCT] FROM [dbo].[CONGTRINH]) ct
                            LEFT JOIN (SELECT * FROM  [dbo].[PHANQUYEN] WHERE [taikhoan]=N'{txt_username.Text.Trim()}') pq
                            ON ct.[MaCT] = [pq].[MaCT]";
                var listPer = SQLHelper.ExecQueryData<Permission>(sql).ToList();
                Mdl_Share.currentPermission = listPer;
                Properties.Settings.Default.currentusername = txt_username.Text.Trim();
                Properties.Settings.Default.Save();
                var message = new MessageBroker
                {
                    Task = "LOGGINED",
                    Data = txt_username.Text.Trim()

                };
                message.Publish();
                this.Close();
            }


        }
        public void CountDownloadHidePopup()
        {
            timer.Enabled = true;
        }

        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            if (i == 2)
            {
                flyoutPanel1.HidePopup();
                i = 0;
                timer.Enabled = false;
            }
        }
        public bool IsShowPassword;
        private void txt_password_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IsShowPassword = !IsShowPassword;
            if (IsShowPassword)
            {
                txt_password.Properties.UseSystemPasswordChar = false;
                e.Button.ImageOptions.Image = ImageResourceCache.Default.GetImage("images/actions/hide_16x16.png");
            }
            else
            {
                txt_password.Properties.UseSystemPasswordChar = true;
                e.Button.ImageOptions.Image = ImageResourceCache.Default.GetImage("images/actions/show_16x16.png");
            }
        }

        private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
                return cp;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.isRemember = chkRemember.Checked;
            Settings.Default.Save();
        }
    }
}