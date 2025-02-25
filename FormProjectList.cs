using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraEditors;
using ProjectStorage.Forms;
using VBSQLHelper;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using System.Diagnostics;

namespace ProjectStorage
{
    public partial class FormProjectList : XtraForm
    {
        private DataTable mDataTable;
        private bool mShouldCreateNewDialogAtFirst;
        private System.Threading.Timer timer;
        private string nhomduan = "0";
        private bool isDisableListProjects = false;
        
        public FormProjectList()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberGroupSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            mShouldCreateNewDialogAtFirst = false;
            InitializeComponent();
        }

        public readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public string GetFileSizeFromFileNameURL(string filename)
        {
            FileInfo file_info = new FileInfo(filename);
            long value = file_info.Length;
            if (value < 0) { return "-"; }

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue / 1024) >= 1)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n1} {1}", dValue, SizeSuffixes[i]);
        }

        private void onLoad(object sender, EventArgs e)
        {
            treeList1.ForceInitialize();

            treeList1.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.Highlight;
            treeList1.Appearance.FocusedCell.ForeColor = System.Drawing.SystemColors.HighlightText;
            treeList1.OptionsBehavior.Editable = false;

            treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            treeList1.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            treeList1.Cursor = Cursors.Hand;
            splitterItem1.Location = new Point(250, 0);

            LoadNhomDuAn();
            refreshForm();
            // treeList1.SelectNode(treeList1.Nodes[0]);

            DataRow dataRow = gridView1.GetFocusedDataRow();
            if (dataRow != null)
            {
                string projectID = dataRow["MaCT"].ToString();
                LoadTaiLieuByDuAn(projectID);
                string projectName = dataRow["TenCT"].ToString();
                lblTenDuAn.Text = string.Format("Tên dự án: {0}", projectName);
            };

            if (mShouldCreateNewDialogAtFirst)
            {
                TimerCallback cb = this.timerCB;
                timer = new System.Threading.Timer(cb, null, 0, 200);
            }
        }

        private TreeListColumn treeListColumn2;
        private TreeListColumn treeListColumn3;

        public void InitColumns()
        {
            treeListColumn1 = new TreeListColumn();
            treeListColumn2 = new TreeListColumn();
            treeListColumn3 = new TreeListColumn();

            treeListColumn1.Caption = "Id";
            treeListColumn1.FieldName = "Id";

            treeListColumn2.Caption = "TenNhom2";
            treeListColumn2.FieldName = "TenNhom2";

            treeListColumn3.Caption = "SL";
            treeListColumn3.FieldName = "SL";

            treeListColumn2.VisibleIndex = 0;
            treeListColumn2.Visible = true;

            treeList1.Columns.AddRange(new TreeListColumn[] {
            treeListColumn1,
            treeListColumn2,
            treeListColumn3
            });
        }

        private TreeListNode nodeCurrent;

        public void RefreshTreeView(TreeListNode node)
        {
            treeList1.Columns.Clear();
            treeList1.ClearNodes();
            treeList1.Appearance.FocusedCell.BackColor = System.Drawing.SystemColors.Highlight;
            treeList1.Appearance.FocusedCell.ForeColor = System.Drawing.SystemColors.HighlightText;
            treeList1.OptionsBehavior.Editable = false;
            treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;

            LoadNhomDuAn();
            treeList1.FocusedNode = treeList1.GetNodeByVisibleIndex(node.Id);
            treeList1.ExpandAll();
        }

        private void LoadNhomDuAn()
        {
            var sql = @"SELECT b.[Id], b.[TenNhom], c.[SL], CONCAT([b].[TenNhom], ' (', c.[SL], ')') AS TenNhom2 FROM (SELECT nhomct FROM [dbo].[CONGTRINH] GROUP BY [NhomCT]) a
                         INNER JOIN [dbo].[NHOMCONGTRINH] b ON a.[NhomCT] = b.[Id]
                         OUTER APPLY (SELECT COUNT(*) AS SL FROM [dbo].[CONGTRINH] WHERE [NhomCT] = b.[Id])c";

            var tableNhomDuAn = SQLHelper.ExecQueryDataAsDataTable(sql);

            InitColumns();
            TreeListNode root = treeList1.AppendNode(new object[] { "0", "Tất cả dự án", "Tất cả dự án" }, null);
            foreach (DataRow item in tableNhomDuAn.Rows)
            {
                TreeListNode driverNode = treeList1.AppendNode(new object[] { item["Id"], item["TenNhom2"], item["SL"] }, root);
            }

            treeList1.ExpandAll();
        }

        public void timerCB(Object stateInfo)
        {
            timer.Dispose();
            showCreateProjectDialog();
        }
 
        private void addProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCreateProjectDialog();
        }

        private void showCreateProjectDialog()
        {
            FormAddProject form = new FormAddProject(false);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = form.textBoxProjectName.Text;
                string manhomct = form.cb_nhomduan.EditValue.ToString();
                string nhomct = form.cb_nhomduan.Text;
                long giatri = Convert.ToInt64(form.txtGiaTriHD.EditValue);
                long giatrint = Convert.ToInt64(form.txtGiaTriNT.EditValue);
                var ngay_ky = Convert.ToDateTime(form.txtNgayKy.EditValue);
                var ngay_hoanthanh = Convert.ToDateTime(form.txtNgayHoanThanh.EditValue);

                string trangthai = form.radioGroupTrangThai.EditValue.ToString();
                string desc = form.textBoxDescription.Text;

                DatabaseUtils db = DatabaseUtils.getInstance();

                db.insertProject(name, manhomct, nhomct, giatri, giatrint, ngay_ky, ngay_hoanthanh, trangthai, desc);
                if (nodeCurrent != null)
                {
                    RefreshTreeView(nodeCurrent);
                }

                refreshForm(true, gridView1.FocusedRowHandle);
            }
        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //contextMenuStrip1.Show();
            }
        }

        private void cellMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //  contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private DataTable dataDuAn;

        private void refreshForm(bool isupdate = false, int rowHanlde = 0)
        {
            try
            {
                String year = null;

                dataDuAn = DatabaseUtils.getInstance().GetListProject(year, false, false, isDisableListProjects ? 0 : 1);
                if (dataDuAn == null)
                {
                    XtraMessageBox.Show("Không thể tải dữ liệu dự án", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridControl1.DataSource = dataDuAn;
                gridView1.BestFitColumns();

                if (isupdate && dataDuAn.Rows.Count > 0)
                {
                    var newTable = dataDuAn.Copy();
                    DataTable dataFilter;
                    if (nhomduan == "0")
                    {
                        dataFilter = newTable;
                        gridControl1.DataSource = dataFilter;
                    }
                    else
                    {
                        try
                        {
                            var query = newTable.AsEnumerable()
                                .Where(x => x.Field<int>("NhomCT").ToString() == nhomduan);
                            
                            if (query.Any())
                            {
                                dataFilter = query.CopyToDataTable();
                                gridControl1.DataSource = dataFilter;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi khi lọc dữ liệu: {ex.Message}");
                        }
                    }

                    if (rowHanlde > 0 && gridView1.RowCount > 0)
                    {
                        gridView1.FocusedRowHandle = Math.Min(rowHanlde, gridView1.RowCount - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow r = gridView1.GetFocusedDataRow();
            if (r == null) return;

            String projectName = (String)r["TenCT"];
            String msg = "Bạn có chắc chắn xóa dự án: " + projectName;
            if (XtraMessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                String projectCode = (String)r["MaCT"];

                DatabaseUtils.getInstance().removeProject(projectCode);

                refreshForm();
            }

            //try
            //{
            //    if (dataGridView1.SelectedRows.Count > 0)
            //    {
            //        int rowIDX = dataGridView1.SelectedRows[0].Index;
            //        DataRow r = mDataTable.Rows[rowIDX];

            //        String projectName = (String)r[1];
            //        String msg = "Bạn có chắc chắn xóa dự án: " + projectName;
            //        if (MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            String projectCode = (String)r[0];

            //            DatabaseUtils.getInstance().removeProject(projectCode);

            //            refreshForm();
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Hãy chọn dự án để xóa");
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow r = gridView1.GetFocusedDataRow();
            if (r == null) return;

            String code = r["MaCT"].ToString();
            String name = r["TenCT"].ToString();
            String trangthai = r["trangthai"].ToString();
            String desc = r["GhiChu"].ToString();
            int nhomct = Convert.ToInt32(r["NhomCT"].ToString());
            string tennhom = r["TenNhom"].ToString();

            FormAddProject form = new FormAddProject(true, nhomct);
            form.textBoxProjectCode.Text = code;
            form.textBoxProjectCode.ReadOnly = true;
            form.cb_nhomduan.EditValue = r["NhomCT"];
            form.textBoxProjectName.Text = name;
            form.txtNgayKy.EditValue = r["ngay_ky"];
            form.txtNgayHoanThanh.EditValue = r["ngay_hoan_thanh"];
            form.txtGiaTriHD.EditValue = r["giatrihopdong"];
            form.txtGiaTriNT.EditValue = r["giatringhiemthu"];

            form.textBoxDescription.Text = desc;

            //form.cb_trangthai.SelectedIndex = trangthai == "Đang thực hiện" ? 0 : 1;
            form.radioGroupTrangThai.SelectedIndex = trangthai == "Đang thực hiện" ? 0 : 1;

            form.Text = "Sửa dự án";
            form.btnOK.Text = "Sửa";

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                code = form.textBoxProjectCode.Text;
                name = form.textBoxProjectName.Text;
                desc = form.textBoxDescription.Text;

                if (code == null || code.Length == 0
                    || name == null || name.Length == 0)
                {
                    MessageBox.Show("Mã dự án hoặc tên dự án không hợp lệ.");
                }

                string manhomct = form.cb_nhomduan.EditValue.ToString();

                long giatri = Convert.ToInt64(form.txtGiaTriHD.EditValue);
                long giatrint = Convert.ToInt64(form.txtGiaTriNT.EditValue);
                var ngay_ky = (form.txtNgayKy.EditValue is DBNull) ? (DateTime?)null : Convert.ToDateTime(form.txtNgayKy.EditValue);

                var ngay_hoanthanh = (form.txtNgayHoanThanh.EditValue is DBNull) ? (DateTime?)null : Convert.ToDateTime(form.txtNgayHoanThanh.EditValue);

                trangthai = form.radioGroupTrangThai.EditValue.ToString();

                DatabaseUtils db = DatabaseUtils.getInstance();
                db.updateProject(code, name, desc, manhomct, giatri, giatrint, ngay_ky, ngay_hoanthanh, trangthai);

                if (nodeCurrent != null)
                {
                    RefreshTreeView(nodeCurrent);
                }
                refreshForm(true, gridView1.FocusedRowHandle);
            }
        }

        private void comboBox_startdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshForm();
        }

        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            string color = e.Node.GetDisplayText("Id");

            if (color.Equals("0"))
            {
                e.NodeImageIndex = 0;
            }
            else
            {
                e.NodeImageIndex = 1;
            }
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeList treeList = sender as TreeList;
            TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
            if (info.HitInfoType == HitInfoType.Cell)
            {
                nodeCurrent = info.Node;
                var idNhom = info.Node.GetValue(treeList1.Columns[0]).ToString();

                var newTable = dataDuAn.Copy();
                nhomduan = idNhom;
                DataTable dataFilter;
                if (idNhom == "0")
                {
                    dataFilter = newTable;
                }
                else
                {
                    var filteredRows = newTable.AsEnumerable().Where(x => x.Field<int>("NhomCT").ToString() == idNhom);
                    if (filteredRows.Any()) // Kiểm tra có dữ liệu không
                        dataFilter = filteredRows.CopyToDataTable();
                    else
                        dataFilter = newTable.Clone(); // Trả về bảng rỗng có cùng cấu trúc
                }

                gridControl1.DataSource = dataFilter;
                gridView1.ClearColumnsFilter();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //DXMouseEventArgs ea = e as DXMouseEventArgs;
            //GridView view = sender as GridView;
            //GridHitInfo info = view.CalcHitInfo(ea.Location);
            //if (info.InRow || info.InRowCell)
            //{
            //    //string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
            //    //MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));

            //    var dataRow = gridView1.GetDataRow(info.RowHandle);
            //    string projectID = dataRow["MaCT"].ToString();
            //    String projectName = dataRow["TenCT"].ToString();

            //    if (projectID != null)
            //    {
            //        FrmMain.Instance.OpenForm(typeof(FormProjectPhieuGiaoViec), new object[] { projectID, projectName }, reLoad: true);
            //    }

            //}
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);

                //if (Mdl_Share.currentUser.quyen == "USERS")
                //{
                //    var permissionDuAn = Mdl_Share.currentPermission.Where(x => x.MaCT == "ALL").FirstOrDefault();
                //    addProjectToolStripMenuItem.Enabled = permissionDuAn.add;

                //    GridView view = sender as GridView;
                //    GridHitInfo hi = view.CalcHitInfo(e.Location);

                //    if (hi.RowHandle > 0)
                //    {
                //        var dataRow = view.GetDataRow(hi.RowHandle);
                //        var MaCT = dataRow["MaCT"].ToString();
                //        var permissionDuAn2 = Mdl_Share.currentPermission.Where(x => x.MaCT == MaCT).FirstOrDefault();
                //        modifyToolStripMenuItem.Enabled = permissionDuAn2.edit;
                //        removeProjectToolStripMenuItem.Enabled = permissionDuAn2.delete;
                //        contextMenuStrip1.Show(gridControl1, e.Location);
                //    }
                //}
                //else
                //{
                //    contextMenuStrip1.Show(gridControl1, e.Location);
                //}
            }
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {
        }

        private string MaCT = "";

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if(e.FocusedRowHandle >= 0)
            //{
            //    var data = gridView1.GetFocusedDataRow();
            //    MaCT = data["MaCT"].ToString();
            //    var tenDuAn = data["TenCT"].ToString();

            //}
            if (e.FocusedRowHandle >= 0)
            {
                DataRow dataRowSelected = gridView1.GetFocusedDataRow();
                string maCT = dataRowSelected["MaCT"].ToString();
                string tenCT = dataRowSelected["TenCT"].ToString();

                LoadTaiLieuByDuAn(maCT);
                lblTenDuAn.Text = string.Format("Tên dự án: {0}", tenCT);
            }
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            ToolTipController controller = sender as ToolTipController;
            if (controller != null)
            {
                GridHitInfo hitInfo = gridView1.CalcHitInfo(e.ControlMousePosition);
                if (hitInfo.Column != null)
                {
                    if (hitInfo.Column.Name == "colThemDuAn")
                    {
                        e.Info = new ToolTipControlInfo(hitInfo.Column, "Thêm dự án");
                    }
                    else if (hitInfo.Column.Name == "colSuaDuAn")
                    {
                        e.Info = new ToolTipControlInfo(hitInfo.Column, "Sửa dự án");
                    }
                    else if (hitInfo.Column.Name == "colXoaDuAn")
                    {
                        e.Info = new ToolTipControlInfo(hitInfo.Column, "Xóa dự án");
                    }
                }
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                if (e.Column == colThemDuAn)
                {
                    if (Mdl_Share.currentUser.quyen == "USERS")
                    {
                        var permissionDuAn = Mdl_Share.currentPermission.Where(x => x.MaCT == "ALL").FirstOrDefault();
                        if (!permissionDuAn.add)
                        {
                            String msg = "Bạn không được phân quyền để tạo mới dự án. Hãy liên hệ với quản trị viên để có thêm thông tin.";
                            if (XtraMessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                            {
                                return;
                            }
                        }
                        showCreateProjectDialog();
                    }
                    else
                    {
                        showCreateProjectDialog();
                    }
                }

                if (e.Column == colSuaDuAn)
                {
                    if (Mdl_Share.currentUser.quyen == "USERS")
                    {
                        GridView view = sender as GridView;
                        GridHitInfo hi = view.CalcHitInfo(e.Location);

                        //var dataRow = view.GetDataRow(hi.RowHandle);
                        DataRow r = gridView1.GetFocusedDataRow();
                        if (r == null) return;
                        var MaCT = r["MaCT"].ToString();
                        string TenCT = r["TenCT"].ToString();
                        var permissionDuAn2 = Mdl_Share.currentPermission.Where(x => x.MaCT == MaCT).FirstOrDefault();

                        if (!permissionDuAn2.edit)
                        {
                            String msg = string.Format("Bạn không được phân quyền để chỉnh sửa dự án: {0} . Hãy liên hệ với quản trị viên để có thêm thông tin.", TenCT);
                            if (XtraMessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                            {
                                return;
                            }
                        }

                        String code = r["MaCT"].ToString();
                        String name = r["TenCT"].ToString();
                        String trangthai = r["trangthai"].ToString();
                        String desc = r["GhiChu"].ToString();
                        int nhomct = Convert.ToInt32(r["NhomCT"].ToString());
                        string tennhom = r["TenNhom"].ToString();

                        FormAddProject form = new FormAddProject(true, nhomct);
                        form.textBoxProjectCode.Text = code;
                        form.textBoxProjectCode.ReadOnly = true;
                        form.cb_nhomduan.EditValue = r["NhomCT"];
                        form.textBoxProjectName.Text = name;
                        form.txtNgayKy.EditValue = r["ngay_ky"];
                        form.txtNgayHoanThanh.EditValue = r["ngay_hoan_thanh"];
                        form.txtGiaTriHD.EditValue = r["giatrihopdong"];
                        form.txtGiaTriNT.EditValue = r["giatringhiemthu"];

                        form.textBoxDescription.Text = desc;

                        //form.cb_trangthai.SelectedIndex = trangthai == "Đang thực hiện" ? 0 : 1;
                        form.radioGroupTrangThai.SelectedIndex = trangthai == "Đang thực hiện" ? 0 : 1;

                        form.Text = "Sửa dự án";
                        form.btnOK.Text = "Sửa";

                        if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            code = form.textBoxProjectCode.Text;
                            name = form.textBoxProjectName.Text;
                            desc = form.textBoxDescription.Text;

                            if (code == null || code.Length == 0
                                || name == null || name.Length == 0)
                            {
                                MessageBox.Show("Mã dự án hoặc tên dự án không hợp lệ.");
                            }

                            string manhomct = form.cb_nhomduan.EditValue.ToString();

                            long giatri = Convert.ToInt64(form.txtGiaTriHD.EditValue);
                            long giatrint = Convert.ToInt64(form.txtGiaTriNT.EditValue);
                            var ngay_ky = (form.txtNgayKy.EditValue is DBNull) ? (DateTime?)null : Convert.ToDateTime(form.txtNgayKy.EditValue);

                            var ngay_hoanthanh = (form.txtNgayHoanThanh.EditValue is DBNull) ? (DateTime?)null : Convert.ToDateTime(form.txtNgayHoanThanh.EditValue);

                            trangthai = form.radioGroupTrangThai.EditValue.ToString();

                            DatabaseUtils db = DatabaseUtils.getInstance();
                            db.updateProject(code, name, desc, manhomct, giatri, giatrint, ngay_ky, ngay_hoanthanh, trangthai);

                            if (nodeCurrent != null)
                            {
                                RefreshTreeView(nodeCurrent);
                            }
                            refreshForm(true, gridView1.FocusedRowHandle);
                        }
                    }
                    else
                    {
                        DataRow r = gridView1.GetFocusedDataRow();
                        if (r == null) return;

                        String code = r["MaCT"].ToString();
                        String name = r["TenCT"].ToString();
                        String trangthai = r["trangthai"].ToString();
                        String desc = r["GhiChu"].ToString();
                        int nhomct = Convert.ToInt32(r["NhomCT"].ToString());
                        string tennhom = r["TenNhom"].ToString();

                        FormAddProject form = new FormAddProject(true, nhomct);
                        form.textBoxProjectCode.Text = code;
                        form.textBoxProjectCode.ReadOnly = true;
                        form.cb_nhomduan.EditValue = r["NhomCT"];
                        form.textBoxProjectName.Text = name;
                        form.txtNgayKy.EditValue = r["ngay_ky"];
                        form.txtNgayHoanThanh.EditValue = r["ngay_hoan_thanh"];
                        form.txtGiaTriHD.EditValue = r["giatrihopdong"];
                        form.txtGiaTriNT.EditValue = r["giatringhiemthu"];

                        form.textBoxDescription.Text = desc;

                        //form.cb_trangthai.SelectedIndex = trangthai == "Đang thực hiện" ? 0 : 1;
                        form.radioGroupTrangThai.SelectedIndex = trangthai == "Đang thực hiện" ? 0 : 1;

                        form.Text = "Sửa dự án";
                        form.btnOK.Text = "Sửa";

                        if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            code = form.textBoxProjectCode.Text;
                            name = form.textBoxProjectName.Text;
                            desc = form.textBoxDescription.Text;

                            if (code == null || code.Length == 0
                                || name == null || name.Length == 0)
                            {
                                MessageBox.Show("Mã dự án hoặc tên dự án không hợp lệ.");
                            }

                            string manhomct = form.cb_nhomduan.EditValue.ToString();

                            long giatri = Convert.ToInt64(form.txtGiaTriHD.EditValue);
                            long giatrint = Convert.ToInt64(form.txtGiaTriNT.EditValue);
                            var ngay_ky = (form.txtNgayKy.EditValue is DBNull) ? (DateTime?)null : Convert.ToDateTime(form.txtNgayKy.EditValue);

                            var ngay_hoanthanh = (form.txtNgayHoanThanh.EditValue is DBNull) ? (DateTime?)null : Convert.ToDateTime(form.txtNgayHoanThanh.EditValue);

                            trangthai = form.radioGroupTrangThai.EditValue.ToString();

                            DatabaseUtils db = DatabaseUtils.getInstance();
                            db.updateProject(code, name, desc, manhomct, giatri, giatrint, ngay_ky, ngay_hoanthanh, trangthai);

                            if (nodeCurrent != null)
                            {
                                RefreshTreeView(nodeCurrent);
                            }
                            refreshForm(true, gridView1.FocusedRowHandle);
                        }
                    }
                }

                if (e.Column == colXoaDuAn)
                {
                    if (Mdl_Share.currentUser.quyen == "USERS")
                    {
                        GridView view = sender as GridView;
                        GridHitInfo hi = view.CalcHitInfo(e.Location);

                        if (hi.RowHandle > 0)
                        {
                            return;
                        }

                        //var dataRow = view.GetDataRow(hi.RowHandle);
                        DataRow r = gridView1.GetFocusedDataRow();
                        if (r == null) return;
                        var MaCT = r["MaCT"].ToString();
                        string TenCT = r["TenCT"].ToString();
                        var permissionDuAn2 = Mdl_Share.currentPermission.Where(x => x.MaCT == MaCT).FirstOrDefault();

                        if (!permissionDuAn2.delete)
                        {
                            String msg = string.Format("Bạn không được phân quyền để xóa dự án: {0} . Hãy liên hệ với quản trị viên để có thêm thông tin.", TenCT);
                            if (XtraMessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                            {
                                return;
                            }
                        }

                        String projectName = (String)r["TenCT"];
                        String msgConfirm = "Bạn có chắc chắn xóa dự án: " + projectName;
                        if (XtraMessageBox.Show(msgConfirm, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            String projectCode = (String)r["MaCT"];

                            DatabaseUtils.getInstance().removeProject(projectCode);

                            refreshForm();
                        }
                    }
                    else
                    {
                        DataRow r = gridView1.GetFocusedDataRow();
                        if (r == null) return;

                        String projectName = (String)r["TenCT"];
                        String msg = "Bạn có chắc chắn xóa dự án: " + projectName;
                        if (XtraMessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            String projectCode = (String)r["MaCT"];

                            DatabaseUtils.getInstance().removeProject(projectCode);

                            refreshForm();
                        }
                    }
                }

                if (e.Column == colAnHien)
                {
                    DataRow r = gridView1.GetFocusedDataRow();
                    if (r == null) return;

                    string maCT = r["MaCT"].ToString();
                    string tenCT = r["TenCT"].ToString();
                    
                    string msg = $"Bạn có chắc chắn muốn ẩn dự án: {tenCT}?";
                    if (XtraMessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DatabaseUtils.getInstance().updateProjectVisibility(maCT, false);
                        refreshForm();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            //if (e.Column == colQuyetDinh)
            //{
            //    var dataRow = gridView1.GetDataRow(e.RowHandle);
            //    string projectID = dataRow["MaCT"].ToString();
            //    String projectName = dataRow["TenCT"].ToString();

            //    if (projectID != null)
            //    {
            //        FrmMain.Instance.OpenForm(typeof(FormProjectPhieuGiaoViec), new object[] { projectID, projectName }, reLoad: true);
            //    }
            //}

            //if (e.Column == colSanLuong)
            //{
            //    // phần này xử lý sau
            //}
        }

        private void qlqdgnvtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = gridView1.GetFocusedDataRow();
                if (dataRow == null) return;

                //var dataRow = gridView1.GetDataRow(e.RowHandle);
                string projectID = dataRow["MaCT"].ToString();
                string projectName = dataRow["TenCT"].ToString();

                if (projectID != null)
                {
                    FrmMain.Instance.OpenForm(typeof(FormProjectPhieuGiaoViec), new object[] { projectID, projectName }, reLoad: true);
                }
            }
            catch (Exception)
            {
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

        private void btnThemFile_Click(object sender, EventArgs e)
        {
            DataRow dataRow = gridView1.GetFocusedDataRow();
            if (dataRow == null) return;

            //var dataRow = gridView1.GetDataRow(e.RowHandle);
            string projectID = dataRow["MaCT"].ToString();
            string projectName = dataRow["TenCT"].ToString();

            if (projectID == "")
            {
                XtraMessageBox.Show("Bạn chưa chọn dự án để import tài liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // kiểm tra xem có quyền import tài liệu không
            if (Mdl_Share.currentUser.quyen == "USERS")
            {
                var isAdd = Mdl_Share.currentPermission.Where(x => x.MaCT == projectID).FirstOrDefault().add;
                if (!isAdd)
                {
                    XtraMessageBox.Show($"Bạn không có quyền thêm tài liệu vào dự án: {projectName}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    var isNotExisted = SQLHelper.ExecQuerySacalar("select count(*) from tailieu where tenfile=@tenfile and mact=@mact", new { tenfile = onlyFile, mact = projectID }).ToString() == "0";
                    if (!isNotExisted)
                    {
                        XtraMessageBox.Show($"Tên file {onlyFile} đã tồn tại trên hệ thống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var sql = "Insert into TAILIEU(mact, tenfile, [file]) VALUES(@mact, @tenfile, @file)";
                    SQLHelper.ExecQueryNonData(sql, new { mact = projectID, tenfile = onlyFile, file = byteData });
                }
                LoadTaiLieuByDuAn(projectID);
            }
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

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dataRowSelected = gridView1.GetFocusedDataRow();
            if (dataRowSelected == null)
            {
                return;
            }
            string MaCT = dataRowSelected["MaCT"].ToString();
            string projectName = dataRowSelected["TenCT"].ToString();

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
                            XtraMessageBox.Show($"Bạn không có quyền xóa tài liệu dự án: {projectName}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void handleClickListProjectDisable(object sender, EventArgs e)
        {
            try
            {
                isDisableListProjects = true;
                UpdateButtonText();
                refreshForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHideSelectedProjects_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            if (selectedRows == null || selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các dự án cần ẩn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string msg = "Bạn có chắc chắn muốn ẩn các dự án đã chọn?";
            if (XtraMessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (int rowHandle in selectedRows)
                {
                    DataRow row = gridView1.GetDataRow(rowHandle);
                    if (row != null)
                    {
                        string maCT = row["MaCT"].ToString();
                        DatabaseUtils.getInstance().updateProjectVisibility(maCT, false);
                    }
                }
                refreshForm();
                XtraMessageBox.Show("Đã ẩn các dự án thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dsDuAn_Click(object sender, EventArgs e)
        {
            try
            {
                isDisableListProjects = false;
                UpdateButtonText();
                refreshForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowSelectedProjects_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            if (selectedRows == null || selectedRows.Length == 0)
            {
                string warningMsg = isDisableListProjects ? 
                    "Vui lòng chọn các dự án cần hiện" : 
                    "Vui lòng chọn các dự án cần ẩn";
                XtraMessageBox.Show(warningMsg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string confirmMsg = isDisableListProjects ?
                "Bạn có chắc chắn muốn hiện lại các dự án đã chọn?" :
                "Bạn có chắc chắn muốn ẩn các dự án đã chọn?";

            if (XtraMessageBox.Show(confirmMsg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (int rowHandle in selectedRows)
                {
                    DataRow row = gridView1.GetDataRow(rowHandle);
                    if (row != null)
                    {
                        string maCT = row["MaCT"].ToString();
                        DatabaseUtils.getInstance().updateProjectVisibility(maCT, isDisableListProjects);
                    }
                }
                refreshForm();

                string successMsg = isDisableListProjects ?
                    "Đã hiện các dự án thành công!" :
                    "Đã ẩn các dự án thành công!";
                XtraMessageBox.Show(successMsg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}