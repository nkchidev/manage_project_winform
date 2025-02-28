using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace ProjectStorage
{
    public partial class FrmMain1 : Office2007RibbonForm
    {
        static public int MENUSTRIP_QUANLY_DUAN = 0;
        static public int MENUSTRIP_QUANLY_THIETBI = 1;
        static public int MENUSTRIP_ADMIN = 2;
        //Office2007Form mCurrentForm;
        Form mCurrentForm;
        MenuStrip menuStrip;

        static public FrmMain1 mInstance = null;

        public FrmMain1()
        {
            InitializeComponent();

            menuStrip = null;
            setMenuStrip(-1);

            mInstance = this;
        }

        public void setMenuStrip(int type)
        {
            if (menuStrip != null)
            {
                this.bar2.Controls.Remove(menuStrip);

                menuStrip = null;
            }
            //=========================================
            if (type == MENUSTRIP_QUANLY_DUAN)
            {
                menuStrip = menuStripQuanLyDuAn;
            }
            else if (type == MENUSTRIP_QUANLY_THIETBI)
            {
                menuStrip = menuStripQuanLyThietBi;
            }
            else if (type == MENUSTRIP_ADMIN)
            {
                menuStrip = menuStripAdmin;
            }
            //=========================================

            if (menuStrip != null)
            {
                this.bar2.Controls.Add(menuStrip);
            }
        }

        public  void Quyen()
        {
            Menu_NguoiDung.Enabled = false;
        }
        private void Khoa()
        {
            btn_dangnhap.Enabled = true;
            btn_dangxuat.Enabled = false;
            btn_doimatkhau.Enabled = false;
            Menu_NguoiDung.Enabled = false;
            Menu_ThietBi.Enabled = false;
            Menu_CongTrinh.Enabled = false;
            //Menu_BanGiao.Enabled = false;
            //Menu_HieuChuan.Enabled = false;
            //Menu_SuaChua.Enabled = false;
            //Menu_ThongKe.Enabled = false;
        }

        public void Mokhoa()
        {
            btn_dangnhap.Enabled = false;
            btn_dangxuat.Enabled = true;
            btn_doimatkhau.Enabled = true;
            Menu_NguoiDung.Enabled = true;
            Menu_ThietBi.Enabled = true;
            Menu_CongTrinh.Enabled = true;
            //Menu_BanGiao.Enabled = true;
            //Menu_HieuChuan.Enabled = true;
            //Menu_SuaChua.Enabled = true;
            //Menu_ThongKe.Enabled = true;

            setMenuStrip(-1);
        }

        private void closeCurrentForm()
        {
            if (mCurrentForm != null)
            {
                mCurrentForm.Close();
                mCurrentForm = null;
            }
        }

        private void FrmMain1Load(object sender, EventArgs e)
        {
        }

        private void BtnDangxuatClick(object sender, EventArgs e)
        {
            var dlg = MessageBoxEx.Show("Bạn có muốn đăng xuất không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dlg != DialogResult.Yes) return;
            Khoa();
            var forms = MdiChildren; // lay ve cac mdi child form tren parent form
            if (forms.Length > 0)
                foreach (var form in forms)
                    form.Close();
        }

        private void BtnDangnhapClick(object sender, EventArgs e)
        {
        }

        private void ButtonItem25Click(object sender, EventArgs e)
        {
            var dlg = MessageBoxEx.Show("Bạn có muốn thoát khỏi chương trình không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dlg == DialogResult.Yes)
                Application.Exit();
        }
        
        private void BtnDoimatkhauClick(object sender, EventArgs e)
        {
            var form = new FrmDoimatkhau();
            form.Show();
        }

        private void MenuThietBiClick(object sender, EventArgs e)
        {
            closeCurrentForm();

            setMenuStrip(MENUSTRIP_QUANLY_THIETBI);
            //var form = new FrmThietBi {MdiParent = this};
            //form.Show();
        }

        private void MenuNguoiDungClick(object sender, EventArgs e)
        {
            setMenuStrip(MENUSTRIP_ADMIN);

            closeCurrentForm();

            mCurrentForm = new QlNguoiDung() {MdiParent = this};
            mCurrentForm.Show();
        }

        private void MenuCongTrinhClick(object sender, EventArgs e)
        {
            setMenuStrip(MENUSTRIP_QUANLY_DUAN);

            closeCurrentForm();

            //mCurrentForm = new ProjectForm() { MdiParent = this };
            //mCurrentForm.Show();

            //mCurrentForm = new FrmCongTrinh() { MdiParent = this };
            //mCurrentForm.Show();
        }

        private void MenuLienHeClick(object sender, EventArgs e)
        {
            setMenuStrip(-1);
            closeCurrentForm();

            mCurrentForm = new LienHe() { MdiParent = this };
            mCurrentForm.Show();
        }

        private void createDevice_Click(object sender, EventArgs e)
        {
            closeCurrentForm();

            mCurrentForm = new FrmThietBi {MdiParent = this};
            mCurrentForm.Show();
        }

        private void exportDeviceClick(object sender, EventArgs e)
        {
            closeCurrentForm();

            mCurrentForm = new BanGiao() { MdiParent = this };
            mCurrentForm.Show();
        }

        private void importDeviceClick(object sender, EventArgs e)
        {

        }

        private void modifyDeviceClick(object sender, EventArgs e)
        {
            closeCurrentForm();

            mCurrentForm = new HieuChuan() { MdiParent = this };
            mCurrentForm.Show();
        }

        private void statisticsClick(object sender, EventArgs e)
        {
            closeCurrentForm();

            mCurrentForm = new ThongKe() { MdiParent = this };
            mCurrentForm.Show();
        }

        private void mdiClient1_Click(object sender, EventArgs e)
        {

        }

        private void quanlydoiClick(object sender, EventArgs e)
        {
            closeCurrentForm();

            mCurrentForm = new QuanLyDoi() { MdiParent = this };
            mCurrentForm.Show();
        }

        private void quanlyuserClick(object sender, EventArgs e)
        {
            closeCurrentForm();

            mCurrentForm = new QlNguoiDung() { MdiParent = this };
            mCurrentForm.Show();
        }

        private void toolStripMenuItemThanhtoan_Click(object sender, EventArgs e)
        {
            if (mCurrentForm != null)
            {
                if (mCurrentForm is ProjectForm)
                {
                    ProjectForm form = (ProjectForm)mCurrentForm;
                    form.insertThanhToanHangThang();
                }
            }

            
        }

        private void quanLyDuAnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeCurrentForm();

            mCurrentForm = new FormProjectList() { MdiParent = this };
            mCurrentForm.Show();
        }

        public void showPhieuListOfProject(String projectID, String projectName)
        {
            closeCurrentForm();

            FormProjectPhieuGiaoViec form = new FormProjectPhieuGiaoViec(projectID, projectName) { MdiParent = this };
            form.Show();

            mCurrentForm = form;
        }

        public void showJobListForm(String projectID, String projectName, int phieuID, String phieuName)
        {
            closeCurrentForm();

            ProjectForm form = new ProjectForm(projectID, projectName, phieuID, phieuName) { MdiParent = this };
            form.Show();

            mCurrentForm = form;
        }
    }
}
