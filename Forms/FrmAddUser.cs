using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Images;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Controls;
using SimpleBroker;
using System.Collections.Generic;
using VBSQLHelper;
using ProjectStorage.Forms;

namespace ProjectStorage
{
    public partial class FrmAddUser : DevExpress.XtraEditors.XtraForm
    {

        //private static FrmAddUser _defaultInstance;
        //public static FrmAddUser Instance
        //{
        //    get
        //    {
        //        if (_defaultInstance == null || _defaultInstance.IsDisposed)
        //        {
        //            _defaultInstance = new FrmAddUser();
        //        }
        //        return _defaultInstance;
        //    }
        //    set => _defaultInstance = value;
        //}
        public FrmAddUser()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += FrmLogin_KeyDown;
            this.FormClosing += FrmAddUser_FormClosing;
        }

        private void FrmAddUser_FormClosing(object sender, FormClosingEventArgs e)
        {
           // OverlayFormShow.Instance.CloseProgressPanel();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);         
            var sql = "SELECT * FROM tbl_level where id_level <> 1 order by id_level DESC";
            var levels = SQLHelper.ExecQueryData<Level>(sql);
            CreateImageCollectionFromCategory2(levels);

            var dataPhong = SQLHelper.ExecQueryDataAsDataTable("SELECT * FROM [dbo].[PHONG]");
            cb_phong.Properties.DataSource = dataPhong;
            cb_phong.Properties.ValueMember= "TenP";    
            cb_phong.Properties.DisplayMember= "TenP";

            if (Mdl_Share.IS_EDIT_USER)
            {
                this.Text = $"Chỉnh sửa thông tin tài khoản: {Mdl_Share.USERNAME_EDIT}";
              //  this.IconOptions.SvgImage = FrmMain.Instance.svgImageCollection[4];
                txt_username.Text = Mdl_Share.USERNAME_EDIT;
                txt_username.Enabled = false;
                btn_save.ImageOptions.SvgImage = FrmMain.Instance.svgImageCollection[3];
                btn_save.Text = "Cập nhật";
                //txt_password.Enabled = false;
                var sql_user = $"SELECT * FROM NGUOIDUNG WHERE TAIKHOAN='{Mdl_Share.USERNAME_EDIT}'";
                var user = SQLHelper.ExecQueryDataFistOrDefault<User>(sql_user);
                txt_fullname.Text = user.ten;
                txt_password.Text = user.matkhau;
                cb_level.EditValue = user.quyen;
                cb_phong.EditValue = user.PhongDoi;

            }
            else
            {
                this.Text = "Tạo tài khoản mới";
             //   this.IconOptions.SvgImage = FrmMain.Instance.svgImageCollection[5];
            }


        }

        public void CreateImageCollectionFromCategory2(IEnumerable<Level> levels)
        {
            cb_level.Properties.SmallImages = FrmMain.Instance.svgImageCollection;
            foreach (var item in levels)
            {
                cb_level.Properties.Items.Add(new ImageComboBoxItem(item.level_name, item.level_name, item.icon_index));
            }
            cb_level.SelectedIndex = 0;
            
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

            if (Mdl_Share.IS_EDIT_USER)
            {
                if (string.IsNullOrEmpty(txt_fullname.Text.Trim()))
                {
                    lbl_message.Text = $"Chưa nhập họ và tên.";
                    txt_fullname.SelectAll();
                    txt_fullname.Focus();
                    flyoutPanel1.ShowPopup();
                    CountDownloadHidePopup();
                    return;
                }

                var response = SQLHelper.ExecProcedureDataFistOrDefault<Response>("sp_edit_user_login", new
                {
                    username = txt_username.Text.Trim(),                   
                    id_level = cb_level.Text,
                    phong = cb_phong.Text,                  
                    fullname = txt_fullname.Text.Trim(),
                    password = txt_password.Text.Trim()

                });

                if (response.Status.Equals("OK"))
                {
                    //OverlayFormShow.Instance.CloseProgressPanel();
                    //FrmMain.Instance.lbl_message_internet.Text = response.Description;
                    //FrmMain.Instance.lbl_message_internet.BackColor = System.Drawing.Color.Green;
                    //FrmMain.Instance.lbl_message_internet.Width = FrmMain.Instance.Width;
                    //FrmMain.Instance.flyoutPanel_internet.ShowPopup();
                    //FrmMain.Instance.CountDownloadHidePopup();
                    var message = new MessageBroker
                    {
                        Task = "ADD_USER"
                    };
                    message.Publish();
                    //if (Mdl_Share.CURRENT_USER.User.username == txt_username.Text)
                    //{
                    //    FrmMain.Instance.txt_username.Caption = txt_fullname.Text;
                    //    FrmMain.Instance.btn_level.Caption = cb_level.Text;
                    //}

                    this.Close();
                }


                return;
            }

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
            if (string.IsNullOrEmpty(txt_fullname.Text.Trim()))
            {
                lbl_message.Text = $"Chưa nhập họ và tên.";
                txt_fullname.SelectAll();
                txt_fullname.Focus();
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
                return;
            }
            if (cb_phong.EditValue is null)
            {
                lbl_message.Text = $"Bạn chưa chọn Phòng/Đội.";               
                flyoutPanel1.ShowPopup();
                CountDownloadHidePopup();
                return;
            }

            var response_save = SQLHelper.ExecProcedureDataFistOrDefault<Response>("sp_create_user_login", new
            {
                username = txt_username.Text.Trim(),
                password = txt_password.Text.Trim(),               
                id_level = cb_level.Text,
                phong = cb_phong.Text.Trim(),   
                //username_created = Mdl_Share.CURRENT_USER.User.username,
                fullname = txt_fullname.Text.Trim()

            });



            if (response_save.Status.Equals("OK"))
            {
                //OverlayFormShow.Instance.CloseProgressPanel();
                //FrmMain.Instance.lbl_message_internet.Text = response_save.Description;
                //FrmMain.Instance.lbl_message_internet.BackColor = System.Drawing.Color.Green;
                //FrmMain.Instance.lbl_message_internet.Width = FrmMain.Instance.Width;
                //FrmMain.Instance.flyoutPanel_internet.ShowPopup();
                //FrmMain.Instance.CountDownloadHidePopup();
                var message = new MessageBroker
                {
                    Task = "ADD_USER"
                };
                message.Publish();
                this.Close();
            }
            else
            {
                lbl_message.Text = response_save.Description;
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

            this.Close();
        }

       
    }
}