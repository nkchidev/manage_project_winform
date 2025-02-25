using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ExcelTools = Microsoft.Office;
using ex = Microsoft.Office.Interop.Excel;

namespace ProjectStorage
{
    public partial class FormMDIMainApplication : Form
    {
        private int childFormNumber = 0;
        Form mCurrentForm;
        static public FormMDIMainApplication mInstance;
        bool mIsLogin = false;

        public FormMDIMainApplication()
        {
            InitializeComponent();
            mInstance = this;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void closeCurrentForm()
        {
            if (mCurrentForm != null)
            {
                mCurrentForm.Close();
                mCurrentForm = null;
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void manageProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeCurrentForm();
            //true
            mCurrentForm = new FormProjectList() { MdiParent = this };
            mCurrentForm.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chương trình Quản lý và thống kê Dự án và Thiết bị. \nPhiên bản 1.5.0");
        }

        public void showPhieuListOfProject(String projectID, String projectName)
        {
            //closeCurrentForm();

          //  FormProjectPhieuGiaoViec form = new FormProjectPhieuGiaoViec(projectID, projectName) { MdiParent = this };
            //FormProjectPhieuGiaoViec form = new FormProjectPhieuGiaoViec(projectID, projectName) { };
            //form.Show();

            //mCurrentForm = form;
        }

        public void showJobListForm(String projectID, String projectName, int phieuID, String phieuName)
        {
            //closeCurrentForm();

           //ProjectForm form = new ProjectForm(projectID, projectName, phieuID, phieuName) { MdiParent = this };
           // ProjectForm form = new ProjectForm(projectID, projectName, phieuID, phieuName) {  };
           // form.Show();

            //mCurrentForm = form;
        }

        private void manageCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeCurrentForm();

            FormChungLoai form = new FormChungLoai() { MdiParent = this };
            mCurrentForm = form;

            form.Show();
        }

        private void muonThietBi(object sender, EventArgs e)
        {
            FormXuatKho form = new FormXuatKho();
            form.ShowDialog();
        }

        private void trathietbi(object sender, EventArgs e)
        {
            FormTraThietBi form = new FormTraThietBi();
            form.ShowDialog();
        }

        private void theodoiHieuchuan(object sender, EventArgs e)
        {
            closeCurrentForm();
            FormSuachuaHieuchuan form = new FormSuachuaHieuchuan() { MdiParent = this };

            mCurrentForm = form;
            form.Show();
        }

        private void thongkeThietbi(object sender, EventArgs e)
        {
            closeCurrentForm();

            FormThongKeThietBi thongke = new FormThongKeThietBi() { MdiParent = this };
            thongke.Show();

            mCurrentForm = thongke;
        }

        private void manageProjectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            closeCurrentForm();
            //false
            mCurrentForm = new FormProjectList() { MdiParent = this };
            mCurrentForm.Show();
        }

        private void thongkeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeCurrentForm();

            mCurrentForm = new FormThongKeDuAn() { MdiParent = this };
            mCurrentForm.Show();
        }

        private void muonTBHisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLichSuMuonTB form = new FormLichSuMuonTB();
            form.ShowDialog();
        }

        private void traTBHisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLichSuTraTB form = new FormLichSuTraTB();
            form.ShowDialog();
        }

        private void onLoad(object sender, EventArgs e)
        {
            login();
        }

        void login()
        {
            DialogLogin dlg = new DialogLogin();
            dlg.textBoxAccount.Text = "admin";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mIsLogin = true;
            }
            else
            {
                this.Close();
            }
        }

        private void toolStripLogin_Click(object sender, EventArgs e)
        {
            mIsLogin = false;
            login();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeCurrentForm();

            mIsLogin = false;
            login();
        }

        private void tracuulichsumuonTBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTracuulichsumuonTB dlg = new FormTracuulichsumuonTB();
            dlg.ShowDialog();
        }

        private void roomManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogQuanlyPhongDoi dlg = new DialogQuanlyPhongDoi();
            dlg.ShowDialog();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importDuAnFromExcel();
        }

        void importDuAnFromExcel()
        {
            DialogImportTask dlg = new DialogImportTask(null);
            dlg.Show();


            /*
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel files (*.xls)|*.xls";
            DialogResult result = dlg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = dlg.FileName;

                ex.Application excel = null;
                ex.Workbook wkb = null;

                try
                {
                    excel = new ex.Application();

                    wkb = excel.Workbooks.Open(
         file, 0, true, 5,
          "", "", true, ex.XlPlatform.xlWindows, "\t", false, false,
          0, true);

                    int t = 0;
                    foreach (ex.Worksheet sheet in wkb.Sheets)
                    {
                        importAProject(sheet);

                        if (t++ == 2)
                            break;
                    }
                }
                catch (Exception ex)
                {
                }
            }
             * */
        }

        

        void importAProject(ex.Worksheet sheet)
        {
            
        }
    }
}