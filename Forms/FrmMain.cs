using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Humanizer;
using ProjectStorage.Properties;
using SimpleBroker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStorage.Forms
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private static FrmMain _defaultInstance;
        public static string kUserName;
        public static FrmMain Instance
        {
            get
            {
                if (_defaultInstance == null)
                {
                    _defaultInstance = new FrmMain();
                }
                return _defaultInstance;
            }
            set => _defaultInstance = value;
        }

        public FrmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;
            
        }

        public void ShowDialogFormOverlay(Form formDialog, Color backgroundColor = default)
        {
            var overlayform = new Form();
            overlayform.StartPosition = FormStartPosition.Manual;
            overlayform.FormBorderStyle = FormBorderStyle.None;
            overlayform.Opacity = 0.5d;
            overlayform.BackColor = backgroundColor == default ? Color.Black : backgroundColor;
            overlayform.Size = this.Size;
            overlayform.Location = this.Location;
            overlayform.ShowInTaskbar = false;
            overlayform.Show(this);

            formDialog.Owner = overlayform;
            formDialog.ShowDialog(overlayform);
            overlayform.Dispose();
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {         

            UserLookAndFeel.Default.SkinName = Properties.Settings.Default["ApplicationSkinName"].ToString();
            var skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
            this.Subscribe<MessageBroker>(OnNext);
            try
            {
                SvgPalette fireBall = skin.CustomSvgPalettes[Properties.Settings.Default["ApplicationPalletName"].ToString()];
                if (fireBall != null)
                {
                    skin.SvgPalettes[Skin.DefaultSkinPaletteName].SetCustomPalette(fireBall);
                }
            }
            catch (Exception)
            {
            }

            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            this.FormClosing += (ss, ee) =>
            {
                Settings.Default["ApplicationSkinName"] = UserLookAndFeel.Default.SkinName;
                Settings.Default["ApplicationPalletName"] = UserLookAndFeel.Default.ActiveSvgPaletteName;
                Settings.Default.Save();
            };

            LoginForm();

            //if (Mdl_Share.currentUser.quyen == "USERS")
            //{
            //    var permissionMuonThietBi = Mdl_Share.currentPermission.Where(x => x.MaCT == "TB2").FirstOrDefault();
            //    var permissionTraThietBi = Mdl_Share.currentPermission.Where(x => x.MaCT == "TB3").FirstOrDefault();
            //    menu_muon_thietbi.Enabled = permissionMuonThietBi.add;
            //    menu_tra_thietbi.Enabled = permissionTraThietBi.add;



            //}

        }

        private void OnNext(MessageBroker value)
        {
            if (value.Task == "LOGGINED")
            {
                var username = value.Data.ToString();
                kUserName = username.ToUpper();
                bar_kUserName.Caption = $"Xin chào, {kUserName}";

                var quyen = Mdl_Share.currentUser.quyen;
                menu_danhsach_phongdoi.Enabled = (quyen == "ADMINS") ? true : false;
                menu_danhsach_nguoidung.Enabled = (quyen == "ADMINS") ? true : false;
               // menu_phanquyen_duan.Enabled = (quyen == "ADMINS") ? true : false;
            }
        }

        private void LoginForm()
        {
            var frm = new FrmLogin();
            ShowDialogFormOverlay(frm);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ribbonControl.SelectedPage = ribbonControl.Pages[1];
        }

        public void OpenForm(Type typeform, object[] listparams = null, bool reLoad = false)
        {
           
            foreach (var frm in MdiChildren.Where(frm => frm.GetType() == typeform))
            {
                if (reLoad)
                {
                    frm.Close();
                }
                else {
                    frm.Activate();
                    return;
                }
                   
            }
           
           
           
          
          
            var form = (Form)(Activator.CreateInstance(typeform, listparams));
            BeginInvoke(new Action(() =>
            {
                form.MdiParent = this;              
                form.Show();
            }));

        }

        

        private void menu_danhmuc_duan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FormProjectList));
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        public void CloseAllChridrenForm(Form form)
        {
            ArrayList list = new ArrayList(MdiChildren);
            foreach (Form f in list)
            {
                f.Close();
            }
        }

        private void barStaticItem5_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var dialog = XtraMessageBox.Show($"Bạn có muốn đăng xuất tài khoản <color=red><b>{kUserName}</b></color> ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, allowHtmlText: DevExpress.Utils.DefaultBoolean.True);
            if (dialog == DialogResult.Yes)
            {
                CloseAllChridrenForm(this);
                LoginForm();
            }
        }

        private void menu_doimatkhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowDialogFormOverlay(new FrmChangePassword());
        }

        private void menu_danhsach_phongdoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(DialogQuanlyPhongDoi));
        }

        private void menu_danhsach_nguoidung_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmUsers));
        }

        private void menu_thongke_duan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FormThongKeDuAn));
        }

        private void menu_import_excel_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogImportTask dlg = new DialogImportTask(null);
            //dlg.Show();
            ShowDialogFormOverlay(dlg);
        }

        private void menu_danhmuc_thietbi_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FormChungLoai));
        }

        private void menu_thongke_thietbi_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FormThongKeThietBi)); 
        }

        private void menu_muon_thietbi_ItemClick(object sender, ItemClickEventArgs e)
        {
           // OpenForm(typeof(FormXuatKho));
           ShowDialogFormOverlay(new FormXuatKho());
        }

        private void menu_tra_thietbi_ItemClick(object sender, ItemClickEventArgs e)
        {
            //OpenForm(typeof(FormTraThietBi));
           ShowDialogFormOverlay(new FormTraThietBi());
        }

        private void menu_lichsu_muonthietbi_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FormLichSuMuonTB));
        }

        private void menu_lichsu_trathietbi_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FormLichSuTraTB));
        }

        private void menu_tracuu_lichsu_muontb_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FormTracuulichsumuonTB));
        }

        private void menu_theodoi_suachua_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FormSuachuaHieuchuan));
        }

        private void menu_thongtin_phanmem_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show("Chương trình Quản lý và thống kê Dự án và Thiết bị. \nPhiên bản 2.0.0");
        }

        private void menu_nhomduan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmGroupDuAn));
        }

        private void menu_phanquyen_duan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmPhanQuyen));
        }

        private void menu_phieugiaonhan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmPhieuGiaoNhan));
        }
    }
}