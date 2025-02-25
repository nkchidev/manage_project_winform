using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using ProjectStorage.Forms;
using VBSQLHelper;

namespace ProjectStorage
{
    public partial class FormProjectPhieuGiaoViec : XtraForm
    {
        private static String COL_PHIEU_ID = "id";
        private static String COL_PROJECT_CODE = "Mã dự án";
        private static String COL_PHIEU_NO = "Số quyết định";
        private static String COL_PHIEU_NAME = "Tên quyết định";
        private static String COL_TEAM = "Bộ phận thi công";
        private static String COL_TONG_KHOAN = "Tổng khoán";
        private static String COL_DA_THANH_TOAN = "Đã thanh toán";

        private DataTable mDataTable;
        private String mProjectID;
        private String mProjectName;
        private string MaCT;

        private int mState = 0;

        public FormProjectPhieuGiaoViec(String projectID, String projectName)
        {
            mProjectID = projectID;
            mProjectName = projectName;

            mState = 0;
            this.MaCT = projectID;

            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            refreshForm();
            LoadTaiLieuByDuAn(MaCT);
        }

        private void refreshForm()
        {
            DataTable dtRaw = DatabaseUtils.getInstance().getPhieuGiaoViecOfProject(mProjectID);
            DataTable dt = new DataTable();

            dt.Columns.Add(COL_PHIEU_ID).DataType = Type.GetType("System.Int32");
            dt.Columns.Add(COL_PROJECT_CODE).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_PHIEU_NO).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_PHIEU_NAME).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_TEAM).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_TONG_KHOAN).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_DA_THANH_TOAN).DataType = Type.GetType("System.String");

            foreach (DataRow r in dtRaw.Rows)
            {
                DataRow nr = dt.NewRow();

                String phieu_no = (String)r["phieu_no"];
                if (phieu_no == null || phieu_no.Length == 0)
                    continue;

                nr[COL_PHIEU_ID] = r["phieu_id"];
                nr[COL_PROJECT_CODE] = r["MaCT"];
                nr[COL_PHIEU_NO] = phieu_no;
                nr[COL_TEAM] = r["doi_thi_cong"];
                nr[COL_PHIEU_NAME] = r["phieu_desc"];

                Double v1 = Utils.getBigIntValueOfRow(r, "chi_phi");
                Double v2 = Utils.getBigIntValueOfRow(r, "da_thanh_toan");

                String s1 = Utils.doubleToMoneyString(v1);
                String s2 = Utils.doubleToMoneyString(v2);

                nr[COL_TONG_KHOAN] = s1;
                nr[COL_DA_THANH_TOAN] = s2;

                dt.Rows.Add(nr);
            }

            //this.linkLabelProjectName.Text = "[" + mProjectID + "] " + mProjectName;
            this.linkLabelProjectName.Text = mProjectName;

            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = dt;
            mDataTable = dt;

            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[1].Visible = false;
            this.dataGridView1.Columns[5].Width = 90;
            this.dataGridView1.Columns[6].Width = 90;

            disableSortofGridView(dataGridView1);
        }

        private void disableSortofGridView(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataRow r = mDataTable.Rows[e.RowIndex];
                String projectID = (String)r[0];
            }
            catch (Exception ex)
            {
            }
        }

        private void onProjectClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (mState == 1)
            {
            }
        }

        private void onItemDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int row = dataGridView1.SelectedRows[0].Index;
                    DataRow r = mDataTable.Rows[row];

                    int phieuID = (int)r[0];
                    String phieuName = (String)r[2];
                    String MaCT = (String)r[1];

                    //  FormMDIMainApplication.mInstance.showJobListForm(mProjectID, mProjectName, phieuID, phieuName);
                    FrmMain.Instance.OpenForm(typeof(ProjectForm), new object[] { mProjectID, mProjectName, phieuID, phieuName, MaCT }, true);
                    //FrmMain.Instance.OpenForm(typeof(FormTest), null, true);

                    /*
                    ProjectForm formJobs = new ProjectForm(mProjectID, mProjectName, phieuID, phieuName);
                    panel2.Controls.Remove(dataGridView1);
                    panel2.Controls.Add(formJobs);

                    mState = 1;
                     * */
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void addPhieuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddPhieuGiaoViec form = new FormAddPhieuGiaoViec();
            form.textBoxProjectCode.Text = mProjectID;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String phieu_no = form.textBoxPhieuGiao.Text;

                if (DatabaseUtils.getInstance().isPhieuNoExist(mProjectID, phieu_no))
                {
                    MessageBox.Show("Lỗi. Số quyết định đã tồn tại.");
                    return;
                }

                String phieu_name = form.textBoxDescription.Text;
                String team = form.comboBoxTeam.Text;

                if (phieu_no.Length == 0)
                {
                    MessageBox.Show("Số phiếu giao nhận không hợp lệ.");
                    return;
                }
                if (team.Length == 0)
                {
                    MessageBox.Show("Chưa chọn đội thi công.");
                    return;
                }

                int kieutinhchiphi = 0;
                if (form.checkBoxCachtinhchiphi.Checked)
                    kieutinhchiphi = 1;
                DatabaseUtils.getInstance().insertPhieuToProject(mProjectID, phieu_no, phieu_name, team, kieutinhchiphi);

                refreshForm();
            }
        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
        }

        private void cellMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (Mdl_Share.currentUser.quyen == "USERS")
                {
                    var permissionDuAn2 = Mdl_Share.currentPermission.Where(x => x.MaCT == MaCT).FirstOrDefault();
                    addPhieuToolStripMenuItem.Enabled = permissionDuAn2.add;
                    importFromExcelToolStripMenuItem.Enabled = permissionDuAn2.add;
                    modifyPhieuToolStripMenuItem.Enabled = permissionDuAn2.edit;
                    removeToolStripMenuItem.Enabled = permissionDuAn2.delete;
                }
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void modifyPhieuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowIDX = dataGridView1.SelectedRows[0].Index;
                    DataRow r = mDataTable.Rows[rowIDX];

                    FormAddPhieuGiaoViec form = new FormAddPhieuGiaoViec();
                    form.textBoxProjectCode.Text = mProjectID;

                    String maCT = Utils.getStringOfRow(r, COL_PROJECT_CODE);
                    String phieu_no = Utils.getStringOfRow(r, COL_PHIEU_NO);
                    String phieu_name = Utils.getStringOfRow(r, COL_PHIEU_NAME);
                    String team = Utils.getStringOfRow(r, COL_TEAM);

                    int phieu_id = Utils.getIntValueOfRow(r, COL_PHIEU_ID);

                    int kieutinhchiphi = DatabaseUtils.getInstance().getCachTinhThanhTienOfPhieu(phieu_id);
                    if (kieutinhchiphi == 0)
                        form.checkBoxCachtinhchiphi.Checked = false;
                    else
                        form.checkBoxCachtinhchiphi.Checked = true;
                    form.checkBoxCachtinhchiphi.Enabled = false;

                    form.textBoxPhieuGiao.Text = phieu_no;
                    form.textBoxPhieuGiao.ReadOnly = true;

                    form.textBoxDescription.Text = phieu_name;
                    form.comboBoxTeam.Text = team;

                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        phieu_name = form.textBoxDescription.Text;
                        team = form.comboBoxTeam.Text;

                        if (team.Length == 0)
                        {
                            MessageBox.Show("Chưa chọn đội thi công.");
                            return;
                        }

                        DatabaseUtils.getInstance().updatePhieuOfProject(maCT, phieu_no, phieu_name, team);

                        refreshForm();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowIDX = dataGridView1.SelectedRows[0].Index;
                    DataRow r = mDataTable.Rows[rowIDX];

                    int phieu_id = Utils.getIntValueOfRow(r, COL_PHIEU_ID);
                    String phieu_no = Utils.getStringOfRow(r, COL_PHIEU_NO);

                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa Phiếu quyết định số: " + phieu_no + " không?", "Cảnh báo", MessageBoxButtons.YesNo)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        DatabaseUtils.getInstance().removePhieuOfProject(phieu_id);
                    }

                    refreshForm();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void gotoProjectList(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void importFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogImportTask dlg = new DialogImportTask(mProjectID);
            dlg.ShowDialog();

            refreshForm();
        }

        private void btnThemFile_Click(object sender, EventArgs e)
        {
            if (MaCT == "")
            {
                XtraMessageBox.Show("Bạn chưa chọn dự án để import tài liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // kiểm tra xem có quyền import tài liệu không
            if (Mdl_Share.currentUser.quyen == "USERS")
            {
                var isAdd = Mdl_Share.currentPermission.Where(x => x.MaCT == MaCT).FirstOrDefault().add;
                if (!isAdd)
                {
                    XtraMessageBox.Show($"Bạn không có quyền thêm tài liệu vào dự án: {mProjectName}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            var dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "Document files|*.xlsx;*.xls;*.doc;*.docx;*.pdf;*.ppt;*.pptx|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var fileNames = dlg.FileNames;
                foreach (var fileName in fileNames)
                {
                    var byteData = File.ReadAllBytes(fileName);
                    var onlyFile = Path.GetFileName(fileName);

                    // kiểm tra tên file đã tồn tại thì ko insert
                    var isNotExisted = SQLHelper.ExecQuerySacalar("select count(*) from tailieu where tenfile=@tenfile and mact=@mact", new { tenfile = onlyFile, mact = MaCT }).ToString() == "0";
                    if (!isNotExisted)
                    {
                        XtraMessageBox.Show($"Tên file {onlyFile} đã tồn tại trên hệ thống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var sql = "Insert into TAILIEU(mact, tenfile, [file]) VALUES(@mact, @tenfile, @file)";
                    SQLHelper.ExecQueryNonData(sql, new { mact = MaCT, tenfile = onlyFile, file = byteData });
                }
                LoadTaiLieuByDuAn(MaCT);
            }
        }

        private void LoadTaiLieuByDuAn(string MaCT)
        {
            var dataTailieu = SQLHelper.ExecQueryData<TAILIEU>("select * from tailieu where mact=@mact", new { mact = MaCT });
            gridControl2.DataSource = dataTailieu;
        }

        private class TAILIEU
        {
            public int Id { get; set; }
            public string MaCT { get; set; }
            public string TenFile { get; set; }
            public byte[] File { get; set; }

            public Image imageIcon
            {
                get
                {
                    var extfile = Path.GetExtension(TenFile).ToLower();
                    if (extfile.Contains("xls"))
                    {
                        return FrmMain.Instance.imageCollection1.Images[5];
                    }
                    if (extfile.Contains("doc"))
                    {
                        return FrmMain.Instance.imageCollection1.Images[6];
                    }
                    if (extfile.Contains("ppt"))
                    {
                        return FrmMain.Instance.imageCollection1.Images[3];
                    }
                    if (extfile.Contains("pdf"))
                    {
                        return FrmMain.Instance.imageCollection1.Images[4];
                    }
                    return FrmMain.Instance.imageCollection1.Images[2];
                }
            }
        }

        public bool ExploreFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }

            filePath = System.IO.Path.GetFullPath(filePath);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }

        private void btnTaiTatCaFile_Click(object sender, EventArgs e)
        {
            var dataSource = gridView2.DataSource as List<TAILIEU>;
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var i = 0;
                var total = dataSource.Count;
                foreach (var item in dataSource)
                {
                    var pathFile = dlg.SelectedPath + "\\" + item.TenFile;
                    File.WriteAllBytes(pathFile, item.File);
                    i++;
                    if (i == total)
                    {
                        XtraMessageBox.Show($"Đã tải tất cả tài liệu dự án thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ExploreFile(pathFile);
                    }
                }
            }
        }

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == gridXem)
            {
                var rowHanlde = e.RowHandle;
                string result = System.IO.Path.GetTempPath();
                if (rowHanlde >= 0)
                {
                    var data = gridView2.GetRow(rowHanlde) as TAILIEU;
                    var pathFile = result + data.TenFile;
                    File.WriteAllBytes(pathFile, data.File);
                    Process.Start(pathFile);
                }
            }

            if (e.Column == gridXoa)
            {
                var rowHanlde = e.RowHandle;
                if (rowHanlde >= 0)
                {
                    // kiểm tra xem có quyền import tài liệu không
                    if (Mdl_Share.currentUser.quyen == "USERS")
                    {
                        var isDelete = Mdl_Share.currentPermission.Where(x => x.MaCT == MaCT).FirstOrDefault().delete;
                        if (!isDelete)
                        {
                            XtraMessageBox.Show($"Bạn không có quyền xóa tài liệu dự án: {mProjectName}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    var data = gridView2.GetRow(rowHanlde) as TAILIEU;
                    var dlg = XtraMessageBox.Show($"Bạn muốn xóa file tài liệu: {data.TenFile} ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlg == DialogResult.Yes)
                    {
                        SQLHelper.ExecQueryNonData("delete from tailieu where id=@id", new { id = data.Id });
                        gridView2.DeleteRow(rowHanlde);
                    }
                }
            }

            if (e.Column == gridTai)
            {
                var rowHanlde = e.RowHandle;
                string result = System.IO.Path.GetTempPath();
                if (rowHanlde >= 0)
                {
                    var data = gridView2.GetRow(rowHanlde) as TAILIEU;
                    var dlg = new SaveFileDialog();
                    dlg.FileName = data.TenFile;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(dlg.FileName, data.File);
                        ExploreFile(dlg.FileName);
                        // XtraMessageBox.Show($"Tải file {dlg.FileName} thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}