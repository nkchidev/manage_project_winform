using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Images;
using System.Data;
using System.Data.SqlClient;
using ProjectStorage.Forms;
using VBSQLHelper;
using DevExpress.XtraEditors;

namespace ProjectStorage
{
    public partial class FrmChangePassword : DevExpress.XtraEditors.XtraForm
    {

        private static FrmChangePassword _defaultInstance;
        public static FrmChangePassword Instance
        {
            get
            {
                if (_defaultInstance == null || _defaultInstance.IsDisposed)
                {
                    _defaultInstance = new FrmChangePassword();
                }
                return _defaultInstance;
            }
            set => _defaultInstance = value;
        }
        public FrmChangePassword()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += FrmLogin_KeyDown;
            this.FormClosing += FrmChangePassword_FormClosing;
        }

        private void FrmChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
           // OverlayFormShow.Instance.CloseProgressPanel();
        }
       // Permission permission;
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txt_username.Text = FrmMain.kUserName;
            txt_username.Enabled = false;
            txt_password.Focus();
          //  permission = Authenticate.Instance.AuthenticateByAction(Mdl_Share.CURRENT_USER.Permission, this.Tag.ToString());

            //if (!permission.edit)
            //{
            //    btn_login.Enabled = false;
            //    txt_password.Enabled = false;
            //    txt_PasswordNew.Enabled = false;
            //    txt_RePasswordNew.Enabled = false;
            //}
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btn_login_Click(sender, e);
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
          
           

            if (string.IsNullOrEmpty(txt_password.Text.Trim()))
            {
                lbl_message.Text = $"Chưa nhập mật khẩu hiện tại.";
                txt_password.SelectAll();
                txt_password.Focus();
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
                return;
            }


            if (string.IsNullOrEmpty(txt_PasswordNew.Text.Trim()))
            {
                lbl_message.Text = $"Chưa nhập mật khẩu mới.";
                txt_PasswordNew.Focus();
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
                return;
            }

            if (string.IsNullOrEmpty(txt_RePasswordNew.Text.Trim()))
            {
                lbl_message.Text = $"Chưa nhập mật khẩu mới lần nữa.";
                txt_RePasswordNew.Focus();
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
                return;
            }

            if (txt_RePasswordNew.Text.Trim() != txt_PasswordNew.Text.Trim())
            {
                lbl_message.Text = $"Hai mật khẩu mới không giống nhau.";
                txt_RePasswordNew.Focus();
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
                return;
            }

            var data = new DataTable();


            var response = SQLHelper.ExecProcedureDataFirstOrDefaultAsync<Response>("sp_change_password_account", new
            {
                username = txt_username.Text,
                password_old = txt_password.Text.Trim(),
                password_new = txt_PasswordNew.Text.Trim()

            });



            if (response.Status.Equals("OK"))
            {
                XtraMessageBox.Show("Đã cập nhật mật khẩu mới thành công, bạn ơi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                lbl_message.Text = response.Description;
                txt_password.SelectAll();
                txt_password.Focus();
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
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
        public bool IsShowPassword, IsShowPasswordNew,  IsShowPasswordReNew;
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
            this.Close();
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IsShowPasswordNew = !IsShowPasswordNew;
            if (IsShowPasswordNew)
            {
                txt_PasswordNew.Properties.UseSystemPasswordChar = false;
                e.Button.ImageOptions.Image = ImageResourceCache.Default.GetImage("images/actions/hide_16x16.png");
            }
            else
            {
                txt_PasswordNew.Properties.UseSystemPasswordChar = true;
                e.Button.ImageOptions.Image = ImageResourceCache.Default.GetImage("images/actions/show_16x16.png");
            }
        }

        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IsShowPasswordReNew = !IsShowPasswordReNew;
            if (IsShowPasswordReNew)
            {
                txt_RePasswordNew.Properties.UseSystemPasswordChar = false;
                e.Button.ImageOptions.Image = ImageResourceCache.Default.GetImage("images/actions/hide_16x16.png");
            }
            else
            {
                txt_RePasswordNew.Properties.UseSystemPasswordChar = true;
                e.Button.ImageOptions.Image = ImageResourceCache.Default.GetImage("images/actions/show_16x16.png");
            }
        }
    }
}