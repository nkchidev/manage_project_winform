using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SimpleBroker;
using DevExpress.XtraEditors.Controls;
using VBSQLHelper;
using ProjectStorage.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace ProjectStorage
{
    public partial class FrmUsers : DevExpress.XtraEditors.XtraForm
    {
       

        public FrmUsers()
        {
            InitializeComponent();          
            this.Load += (s, e) => { this.Subscribe<MessageBroker>(OnNext);};
        }
        private void OnNext(MessageBroker value)
        {
            if (value.Task == "ADD_USER")
            {
                LoadData();
            }
        }
  
        public void LoadData()
        {

            var users = SQLHelper.ExecProcedureData<User>("sp_load_list_users");
            //var levels = SQLHelper.ExecQueryData<Level>("SELECT * FROM TBL_LEVEL WHERE id_level<>1 order by id_level desc");
           
            //CreateImageCollectionFromCategory2(levels);
            gridControl_Users.DataSource = users;
            gridView_Users.FocusedRowHandle = y;
           gridView_Users.TopRowIndex = x;

        }

        //public void CreateImageCollectionFromCategory2(IEnumerable<Level> levels)
        //{
        //    //cb_level.SmallImages = FrmAddLevel.Instance.svgImageCollection;
        //    //foreach (var item in levels)
        //    //{
        //    //    cb_level.Items.Add(new ImageComboBoxItem(item.level_name, item.level_name, item.icon_index));
        //    //}

        //}
      //  Permission permission;
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            LoadData();
            //permission = Authenticate.Instance.AuthenticateByAction(Mdl_Share.CURRENT_USER.Permission, this.Tag.ToString());
           
            //if (!permission.add)
            //{
            //    btn_addUser.Enabled = false;
            //}
            //if (!permission.edit)
            //{
            //    grid_edit.Visible = false;
            //}
            //if (!permission.delete)
            //{
            //    grid_xoa.Visible = false;
            //}
            //if (!permission.extra)
            //{
            //    grid_reset_password.Visible = false;
            //}

        }
        private void btn_addUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Mdl_Share.IS_EDIT_USER = false;
            Mdl_Share.USERNAME_EDIT ="";
            FrmMain.Instance.ShowDialogFormOverlay(new FrmAddUser());
          
        }

        private void gridView_Users_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if(e.Column.Name == "grid_xoa")
            {
                
                var user = gridView_Users.GetFocusedRow() as User;
                if (user.taikhoan.ToLower() == FrmMain.kUserName.ToLower())
                {
                    XtraMessageBox.Show($"Bạn không thể xóa tài khoản của chính mình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                args.Caption = "Cảnh báo!";
                var Cbitmap = SystemIcons.Warning.ToBitmap();
                IntPtr icH = Cbitmap.GetHicon();
                Icon ico = Icon.FromHandle(icH);
                args.Icon = ico;
                args.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
                args.Text = $"Bạn có chắc chắn muốn xóa tài khoản <color=red><b>{user.taikhoan}</b></color> phải không?";
                args.Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No };
                args.Showing += (ss, ee) => { Args_Showing(ss, ee, user.taikhoan); };
                args.AutoCloseOptions.ShowTimerOnDefaultButton = true;
                XtraMessageBox.Show(args).ToString();
            }

            if (e.Column.Name == "grid_edit")
            {
                //if (!permission.edit)
                //{
                //    FrmMain.Instance.ShowMessageError("Bạn không có quyền chỉnh sửa tài khoản ngưởi dùng");
                //    return;
                //}

                var user = gridView_Users.GetFocusedRow() as User;
                Mdl_Share.IS_EDIT_USER = true;
                Mdl_Share.USERNAME_EDIT = user.taikhoan;
              //  OverlayFormShow.Instance.ShowFormOverlay(FrmMain.Instance);
                y = gridView_Users.FocusedRowHandle;
                x = gridView_Users.TopRowIndex;
                // FrmAddUser.Instance.Show(this);
                FrmMain.Instance.ShowDialogFormOverlay(new FrmAddUser());
            }

            if(e.Column.Name == "grid_reset_password")
            {
                //if (!permission.extra)
                //{
                //    FrmMain.Instance.ShowMessageError("Bạn không có quyền reset mật khẩu. Phải có quyền mở rộng mới được reset.");
                //    return;
                //}
                //var user = gridView_Users.GetFocusedRow() as User;
                //XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                //args.Caption = "Cảnh báo!";
                //var Cbitmap = SystemIcons.Warning.ToBitmap();
                //IntPtr icH = Cbitmap.GetHicon();
                //Icon ico = Icon.FromHandle(icH);
                //args.Icon = ico;
                //args.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
                //args.Text = $"Bạn có chắc chắn Reset mật khẩu mặc định <color=red><b>{user.username}</b></color> phải không?";
                //args.Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No };
                //args.Showing += (ss, ee) => { Args_Showing_resetPassword(ss, ee, user.username); };
                //args.AutoCloseOptions.ShowTimerOnDefaultButton = true;
                //XtraMessageBox.Show(args).ToString();
            }
        }
        int x, y;

        private void Args_Showing(object sender, XtraMessageShowingArgs e, string username)
        {
            foreach (var control in e.Form.Controls)
            {
                SimpleButton button = control as SimpleButton;
                if (button != null)
                {
                    button.ImageOptions.SvgImageSize = new Size(16, 16);
                    switch (button.DialogResult.ToString())
                    {
                        case ("Yes"):
                            button.ImageOptions.SvgImage = FrmMain.Instance.svgImageCollection[2];
                            button.Text = "Đồng ý";
                            button.ForeColor = Color.Red;
                            button.Font = new Font(button.Font, FontStyle.Bold);
                            button.AllowFocus = false;
                            button.Width = 90;
                            button.Click += (ss, ee) =>
                            {
                                var sql = $"DELETE FROM NGUOIDUNG WHERE taikhoan='{username}'";
                                int effectRow = SQLHelper.ExecQueryNonData(sql);

                                if (effectRow > 0)
                                {
                                    XtraMessageBox.Show($"Đã xóa tài khoản {username} thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    gridView_Users.DeleteSelectedRows();
                                }
                            };
                            break;
                        case ("Đóng"):
                            button.Text = "No";
                            break;

                    }
                }
            }
        }

        private void Args_Showing_resetPassword(object sender, XtraMessageShowingArgs e, string username)
        {
            //foreach (var control in e.Form.Controls)
            //{
            //    SimpleButton button = control as SimpleButton;
            //    if (button != null)
            //    {
            //        button.ImageOptions.SvgImageSize = new Size(16, 16);
            //        switch (button.DialogResult.ToString())
            //        {
            //            case ("Yes"):
            //                button.ImageOptions.SvgImage = FrmMain.Instance.svgImageCollection[10];
            //                button.Text = "Đồng ý";
            //                button.ForeColor = Color.Red;
            //                button.Font = new Font(button.Font, FontStyle.Bold);
            //                button.AllowFocus = false;
            //                button.Width = 90;
            //                button.Click += (ss, ee) => {
            //                    var sql = $"UPDATE TBL_USERS SET password=CONVERT(VARCHAR(32), HashBytes('MD5', '{username.ToLower()}'), 2) WHERE USERNAME='{username}'";
            //                    int affectRow = SQLHelper.ExecQueryNonData(sql);

            //                    if (affectRow > 0)
            //                    {
            //                        FrmMain.Instance.SetTextMessage($"Đã reset mật khẩu {username} thành công.", Color.White, Color.Green);
            //                    }

            //                };
            //                break;
            //            case ("No"):
            //                button.Text = "Đóng";
            //                break;

            //        }
            //    }
            //}
        }
    }
}