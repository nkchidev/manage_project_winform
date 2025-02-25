using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using System.Collections;
using ex = Microsoft.Office.Interop.Excel;
using DevExpress.XtraEditors;
using DevComponents.DotNetBar.Controls;
using System.Reflection;
using VBSQLHelper;
using MiniExcelLibs;
using System.IO;
using System.Diagnostics;
using ProjectStorage.Helpers;

namespace ProjectStorage
{
    public partial class ProjectForm : XtraForm
    {
        private static String COL_JOB_ID = "job_id";
        private static String COL_TT = "TT";
        private static String COL_NOIDUNG = "Nội dung công việc";

        private static String COL_JOB_LEVEL = "job_level";
        private static String COL_KHOKHAN = "Mức K.Khăn";
        private static String COL_KHOILUONG = "Khối lượng";
      //  private static String COL_DONGIA = "Đơn giá";

        private static String COL_DONGIA_CONG = "Đơn giá";
        private static String COL_DINHMUC = "Đ.Mức";
        private static String COL_DINHBIEN = "Đ.Biên";

        private static String COL_DONVITINH = "Đơn vị tính";
        private static String COL_THANHTIEN = "Thành tiền";
        private static String COL_TONG_THANHLY_KL = "Tổng KL T.lý";
        private static String COL_TONG_THANHLY_GT = "Tổng GT T.lý";
        private static String COL_TONG_CONLAI_KL = "Tổng KL còn lại";
        private static String COL_TONG_CONLAI_GT = "Tổng GT còn lại";

        private String mProjectID;
        private String mProjectName;
        private int mPhieuID;
        private String mPhieuName;
        private String _MaCT;
        private int mCachTinhChiPhi = 0;

        // Hàm dùng làm giảm hiện tượng flicker khi control vẽ lại
        private static void SetDoubleBuffer(Control dgv, bool DoubleBuffered)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgv, new object[] { DoubleBuffered });
        }

        public ProjectForm(String projectID, String projectName, int phieuID, String phieuName, String MaCT)
        {
            InitializeComponent();
            SetDoubleBuffer(dataGridView1, true);
            mProjectID = projectID;
            mProjectName = projectName;
            mPhieuID = phieuID;
            mPhieuName = phieuName;
            _MaCT = MaCT;

            mCachTinhChiPhi = DatabaseUtils.getInstance().getCachTinhThanhTienOfPhieu(mPhieuID);
        }

        private DataTable mProjectList;

        //DataTable mDataSourceRaw;
        private DataTable mDataSource;

        private int COLUMN_TongTLKL = -1;
        private int COLUMN_TongTLGT = -1;
        private int COLUMN_ConlaiKL = -1;
        private int COLUMN_ConlaiGT = -1;
        private int COLUMN_ThanhLy0 = 1000;
        private int COLUMN_ThanhLy_Count = 0;

        private Hashtable mHashColumnData = new Hashtable();
        private Hashtable mHashDataColumn = new Hashtable();
        private Hashtable mHashColumnToIndex = new Hashtable();

        private List<String> mThanhlyKL = new List<string>(5);
        private List<String> mThanhlyGT = new List<string>(5);
        private List<String> mThanhlyShortName = new List<string>(5);
        private List<String> mHiddenItems = new List<string>(5);
        private bool isAllowEdit = true;

        private void onLoad(object sender, EventArgs e)
        {
            if (Mdl_Share.currentUser.quyen == "USERS")
            {
                var permissionDuAn2 = Mdl_Share.currentPermission.Where(x => x.MaCT == _MaCT).FirstOrDefault();
                isAllowEdit = permissionDuAn2.edit;
            }

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeight = 36;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

            dataGridView1.RowHeadersVisible = false;

            linkLabelPhieuName.Text = "[" + mProjectID + "] " + mPhieuName;

            //  splitContainer1.SplitterDistance = 200;

            //initProjectList();

            // Lấy dữ liệu cho datasource table
            refreshProject();
            // Kiểm tra tính hợp lệ của dữ liệu nhập vào
            dataGridView1.CellValidating += DataGridView1_CellValidating;
            // dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void refreshProject()
        {
            Cursor.Current = Cursors.WaitCursor;
            initDataSourceTable();

            addRootNode();

            refreshTree();

            loadMonthExport();

            treeViewProjectTasks.SelectedNode = mRoot;

            Cursor.Current = Cursors.Default;
            dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
        }

        private void loadMonthExport()
        {
            var dataThang = SQLHelper.ExecQueryDataAsDataTable(@"SELECT *,
               CONCAT(RIGHT(CONVERT(VARCHAR(6), month), 2), '/', LEFT(CONVERT(VARCHAR(6), month), 4)) AS month_text
                    FROM dbo.THANHTOAN
                    WHERE phieu_id = @phieu_id ORDER BY LEFT(CONVERT(VARCHAR(6), month), 4) desc, RIGHT(CONVERT(VARCHAR(6), month), 2) desc", new { phieu_id = mPhieuID });

            cb_thang_export.Properties.DataSource = dataThang;
            cb_thang_export.Properties.ValueMember = "month";
            cb_thang_export.Properties.DisplayMember = "month_text";

            if(dataThang.Rows.Count > 0)
            cb_thang_export.EditValue = dataThang.Rows[0]["month"].ToString();

        }

        private void initDataSourceTable()
        {
            if (getPhieuID() == -1)
                return;

            //mKi

            mHashColumnData.Clear();
            mHashDataColumn.Clear();
            mHashColumnToIndex.Clear();

            mThanhlyShortName.Clear();

            mDataSource = new DataTable();
            mDataSource.Columns.Add(COL_JOB_ID).DataType = Type.GetType("System.Int32");
         
             mDataSource.Columns.Add(COL_TT).DataType = Type.GetType("System.String");
            mDataSource.Columns.Add(COL_NOIDUNG).DataType = Type.GetType("System.String");
            mDataSource.Columns.Add(COL_JOB_LEVEL).DataType = Type.GetType("System.Int32");
            mDataSource.Columns.Add(COL_KHOKHAN).DataType = Type.GetType("System.Int32");

            mDataSource.Columns.Add(COL_DONVITINH).DataType = Type.GetType("System.String");
            mDataSource.Columns.Add(COL_KHOILUONG).DataType = Type.GetType("System.String");

            //if (mCachTinhChiPhi == 0)
            //{
            //    mDataSource.Columns.Add(COL_DONGIA).DataType = Type.GetType("System.String");
            //    COLUMN_ThanhLy0 = 8;
            //}
            //else
            //{
                mDataSource.Columns.Add(COL_DONGIA_CONG).DataType = Type.GetType("System.String");
                mDataSource.Columns.Add(COL_DINHBIEN).DataType = Type.GetType("System.String");
                mDataSource.Columns.Add(COL_DINHMUC).DataType = Type.GetType("System.String");
                COLUMN_ThanhLy0 =11;
          //  }

            foreach (System.Data.DataColumn dc in mDataSource.Columns)
            {
                if (isAllowEdit && dc.ColumnName == COL_KHOILUONG)
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
            }
            mDataSource.Columns.Add(COL_THANHTIEN).DataType = Type.GetType("System.String");
            //====================
            DatabaseUtils db = DatabaseUtils.getInstance();
            DataTable thanhtoan = db.getThanhtoanOfProjectID(getPhieuID());

            int idx = COLUMN_ThanhLy0;
            mThanhlyKL.Clear();
            mThanhlyGT.Clear();

            foreach (DataRow r in thanhtoan.Rows)
            {
                int d = (int)r["month"];
                int thanhly_id = (int)r["thanhtoan_id"];
                String shortName = toKLThanhtoanShortnameFieldName(d); //11/2022
                String name = toKLThanhtoanFieldName(d); //T.lý KL\n11 / 2022
                //  check a bit
                if (mThanhlyKL.Contains(name))
                {
                    continue;
                }
                //================================

                COLUMN_ThanhLy_Count++;
                idx += 2;

                mDataSource.Columns.Add(name).DataType = Type.GetType("System.String");
                mDataSource.Columns[name].ReadOnly = false;
                mThanhlyKL.Add(name); //"Thànhtiền\n11/2022"
                mThanhlyShortName.Add(shortName); //"Thànhtiền\n11/2022"

                String name2 = toThanhtoanFieldName(d); //"Thànhtiền\n11/2022"
                mDataSource.Columns.Add(name2).DataType = Type.GetType("System.String");
                mDataSource.Columns[name2].ReadOnly = false;
                mThanhlyGT.Add(name2); //"Thànhtiền\n11/2022"

                mHashColumnData.Add(name, thanhly_id); //"Thànhtiền\n11/2022" "Thànhtiền\n11/2022"
                mHashDataColumn.Add(thanhly_id, name); //101167 "T.lý KL\n11/2022"
            }
            //=====================
            mDataSource.Columns.Add(COL_TONG_THANHLY_KL).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_THANHLY_KL].ReadOnly = false;
            COLUMN_TongTLKL = (idx++); 

            mDataSource.Columns.Add(COL_TONG_THANHLY_GT).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_THANHLY_GT].ReadOnly = false;
            COLUMN_TongTLGT = (idx++);
            //=====================
            mDataSource.Columns.Add(COL_TONG_CONLAI_KL).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_CONLAI_KL].ReadOnly = false;
            COLUMN_ConlaiKL = (idx++);

            mDataSource.Columns.Add(COL_TONG_CONLAI_GT).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_CONLAI_GT].ReadOnly = false;
            COLUMN_ConlaiGT = (idx++);

            //=============================
            for (int i = 0; i < mDataSource.Columns.Count; i++)
            {
                String s = mDataSource.Columns[i].Caption;
                mHashColumnToIndex[s] = i; // Ten cot la ky gia tri la index
            }
        }

        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                if (Mdl_Share.currentUser.quyen == "USERS")
                {
                    var permissionDuAn2 = Mdl_Share.currentPermission.Where(x => x.MaCT == _MaCT).FirstOrDefault();
                    if (!permissionDuAn2.edit)
                    {
                        var dlg = XtraMessageBox.Show("Bạn chưa được cấp quyền chỉnh sửa KL dự toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, allowHtmlText: DevExpress.Utils.DefaultBoolean.True);
                        e.Cancel = true;
                        if (dlg == DialogResult.OK)
                        {
                            SendKeys.Send("{ESC}");
                        }
                        return;
                    }
                }

                var totalKL = 0.0d;
                var kl_bandau = 0.0d;
                DataColumn dc = mDataSource.Columns[e.ColumnIndex];

                //check cột đang edit là tên gì, nếu cột khối lượng khỏi kiểm tra.
                if (dc.ColumnName == COL_KHOILUONG)
                {
                    return;
                }

                Double currentKLDangnhap = 0;
                Double.TryParse(e.FormattedValue.ToString(), out currentKLDangnhap);
                //if (currentKLDangnhap < 0.0) {
                //    XtraMessageBox.Show($@"Khối lượng bạn nhập là số âm, không hợp lệ. Vui lòng nhập lại, hoặc nhấn nút <color=red><b> {{ESC}} </b></color> để bỏ qua.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, allowHtmlText: DevExpress.Utils.DefaultBoolean.True);
                //    e.Cancel = true;
                //}
                foreach (DataColumn item in mDataSource.Columns)
                {
                    if (item.ColumnName.Contains("T.lý KL") && item.ColumnName != dc.ColumnName)
                    {
                        Double currentKL = 0;
                        Double.TryParse(mDataSource.Rows[e.RowIndex][item.ColumnName].ToString(), out currentKL);
                        totalKL += currentKL;
                    }

                    if (item.ColumnName == COL_KHOILUONG)
                    {
                        Double.TryParse(mDataSource.Rows[e.RowIndex][item.ColumnName].ToString(), out kl_bandau);
                    }
                }
                Double SlToida = kl_bandau - totalKL;

                totalKL += currentKLDangnhap;

                var conlai = kl_bandau - totalKL;
                if (conlai < 0.0d)
                {
                  //var dlg =  XtraMessageBox.Show($@"Khối lượng bạn nhập đã vượt quá: <color=red><b>{(conlai * -1.0).ToString("#,#")}</b></color>. Tối đa là: <color=red><b>{SlToida}</b></color>. Vui lòng nhập lại, hoặc nhấn nút <color=red><b> {{ESC}} </b></color> để bỏ qua.", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, allowHtmlText: DevExpress.Utils.DefaultBoolean.True);

                    var dlg = XtraMessageBox.Show($@"Khối lượng bạn nhập đã vượt quá: <color=red><b>{(conlai * -1.0).ToString("#,#")}</b></color>. Tối đa là: <color=red><b>{SlToida}</b></color>.", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, allowHtmlText: DevExpress.Utils.DefaultBoolean.True);

                    if (dlg == DialogResult.No)
                    {
                        e.Cancel = true;

                        SendKeys.Send("{ESC}");
                    }

                   
                }
            }
        }

        private bool addRootNode()
        {
            /*
            string projectName = comboBoxProjectList.SelectedItem.ToString();
            if (projectName == null || projectName.Length == 0)
                return false;
            */
            treeViewProjectTasks.Nodes.Clear();

            TreeNode root = new TreeNode() { Text = mPhieuName };
            root.Tag = getPhieuID();
            mRoot = root;

            treeViewProjectTasks.Nodes.Add(root);

            return true;
        }

        private void refreshTree()
        {
            DataTable dt;
            DatabaseUtils db = DatabaseUtils.getInstance();
            //mDataSourceRaw = db.getAllJobsOfproject(getPhieuID());

            treeViewProjectTasks.Nodes.Clear();

            if (addRootNode())
            {
                dt = db.getJobOfProject(getPhieuID(), 0); // lay trong tabl CONGVIEC job_level = 1

                treeViewProjectTasks.SelectedNode = treeViewProjectTasks.Nodes[0];

                mDataSource.Clear(); // Xoa du lieu cu neu co
                //  add project as the first row
                DataRow r = mDataSource.NewRow();
                r[COL_JOB_LEVEL] = 0;
                r[COL_JOB_ID] = 0;
                r[COL_NOIDUNG] = treeViewProjectTasks.TopNode.Text;
                mDataSource.Rows.Add(r); // dong tieu de tren cung

                addJobsToNode(treeViewProjectTasks.Nodes[0], dt); // cac dong du lieu ben trong
            }

            //DataTable all = db.getDataSourceOfProject(getPhieuID());
            /*
            foreach (DataColumn dc in mDataSource.Columns)
            {
                System.Console.Out.WriteLine(dc.ToString());
            }
             */

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;    //  disable/prevent reorder columns
            dataGridView1.DataSource = mDataSource;
            //dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns[0].Visible = false;   //  id
            dataGridView1.Columns[3].Visible = false;   //  level
            this.disableSortofGridView(dataGridView1);

            for (int i = 0; i < mDataSource.Rows.Count; i++)
            {
                DataRow r = mDataSource.Rows[i];

                if (i == 0) //  project name
                {
                    dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.DarkGreen;
                    //dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.White;
                    dataGridView1.Rows[0].DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                    continue;
                }
                int level = 0;
                try
                {
                    level = (int)r[3];
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }

                if (level != C.JOB_LEVEL_DETAIL)
                {
                    if (level == 1)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    dataGridView1.Rows[i].DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                }
            }

            //  set  column width
            dataGridView1.Columns[COL_KHOKHAN].Width = 54;
            dataGridView1.Columns[COL_NOIDUNG].Width = 280;
            dataGridView1.Columns[COL_TT].Width = 50;

            dataGridView1.Columns[COL_DONVITINH].Width = 54;
            dataGridView1.Columns[COL_KHOILUONG].Width = 60;
            //if (mCachTinhChiPhi == 0)
            //{
            //    dataGridView1.Columns[COL_DONGIA].Width = 80;
            //}
            //else
            //{
                dataGridView1.Columns[COL_DONGIA_CONG].Width = 80;
                dataGridView1.Columns[COL_DINHMUC].Width = 60;
                dataGridView1.Columns[COL_DINHBIEN].Width = 60;
         //   }
        

            if (COLUMN_ThanhLy0 < 11)
            {
                for (int i = COLUMN_ThanhLy0; i < COLUMN_TongTLKL; i += 2)
                {
                    dataGridView1.Columns[i].Width = 55;
                    dataGridView1.Columns[i + 1].Width = 80;
                }
            }
            dataGridView1.Columns[COLUMN_TongTLKL].Width = 60;
            dataGridView1.Columns[COLUMN_TongTLGT].Width = 90;

            dataGridView1.Columns[COLUMN_ConlaiKL].Width = 60;
            dataGridView1.Columns[COLUMN_ConlaiGT].Width = 90;
            dataGridView1.Columns[COL_THANHTIEN].Width = 90;
            //==============================
            updateUIThanhTien();
            //=================================
            treeViewProjectTasks.ExpandAll();
            // dataGridView1.Columns[COL_NOIDUNG].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void initProjectList()
        {
            DatabaseUtils db = DatabaseUtils.getInstance();

            DataTable tb = db.GetListProject(null);
            mProjectList = tb;
        }

        private void disableSortofGridView(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void addRowToDataSourceList(DataRow r)
        {
            var drNew = mDataSource.NewRow();
            int level = (int)r["job_level"];
            int job_id = (int)r["job_id"];

            drNew[COL_JOB_ID] = r["job_id"];

            String jobname = "";
            int xxx = level;
            if (xxx == C.JOB_LEVEL_DETAIL)
            {
                xxx = 4;
            }

            for (int i = 0; i < xxx; i++)
            {
                jobname += " "; //tao khoang trang
            }

            jobname += (String)r["job_name"];
            drNew[COL_TT] = $"{r["thutu"]}";
            drNew[COL_NOIDUNG] = $"{jobname}";


            drNew[COL_JOB_LEVEL] = r["job_level"];

            drNew[COL_DONVITINH] = r["unit_name"];

            if (level > 0)
                drNew[COL_KHOKHAN] = r["difficult"];
            Int64 calc_price = 0;
            Int64 dongia = 0;
            Double amount = 0;
            Double dinhmuc = 0;
            Double dinhbien = 0;
            /*
            if (level < C.JOB_LEVEL_DETAIL)
            {
                DatabaseUtils db = DatabaseUtils.getInstance();
                Double tongChiPhi = db.getTotalMoneyOfJob(getPhieuID(), job_id);
                drNew[COL_THANHTIEN] = doubleToMoneyString(tongChiPhi);

                Double tongDaThanhLy = 0;
                //  tinh tong so tien da thanh ly cua children, assign vao cac column name2
                for (int i = 0; i < mThanhlyGT.Count; i++)
                {
                    string name = mThanhlyKL[i];
                    string name2 = mThanhlyGT[i];
                    int thanhly_id = (int)mHashColumnData[name];
                    Double totalCuaThang = db.getTotalMoneyThanhLyOfJob(getPhieuID(), job_id, thanhly_id);
                    drNew[name2] = doubleToMoneyString(totalCuaThang);

                    tongDaThanhLy += totalCuaThang;
                }
                //  tong Gia tri cua thanh ly
                drNew[COLUMN_TongTLGT] = doubleToMoneyString(tongDaThanhLy);
                Double conlai = tongChiPhi - tongDaThanhLy;
                drNew[COLUMN_ConlaiGT] = doubleToMoneyString(conlai);
            }
            else
             */
            if (level == C.JOB_LEVEL_DETAIL)
            {
                try
                {
                    amount = (Double)r["unit_amount"];

                    dongia = (Int64)r["price"];
                    dinhmuc = 1;
                    dinhbien = 1;
                    //if (mCachTinhChiPhi == 1)
                    //{
                        dinhmuc = Utils.getDoubleValueOfRow(r, "dinhmuc");
                        dinhbien = Utils.getDoubleValueOfRow(r, "dinhbien");
                    //}

                    Double total = dongia * dinhmuc * amount * dinhbien;
                    calc_price = (long)total; // thanh tien

                    //drNew[COL_THANHTIEN] = Utils.doubleToMoneyString(calc_price);
                }
                catch (Exception e2)
                {
                    Console.WriteLine( e2.Message);
                }
                //unit_amount là cột khối lượng
                drNew[COL_KHOILUONG] = Utils.doubleToString(amount);// amount.ToString("0.00"); Khoi luong
                //if (mCachTinhChiPhi == 0)
                //{
                //    drNew[COL_DONGIA] = Utils.doubleToMoneyString(dongia); // Don gia
                //}
                //else
                //{
                    drNew[COL_DONGIA_CONG] = Utils.doubleToMoneyString(dongia);
                    drNew[COL_DINHMUC] = ((float)dinhmuc).ToString("F2");
                    drNew[COL_DINHBIEN] = Utils.doubleToString(dinhbien);
                //}
                drNew[COL_THANHTIEN] = Utils.doubleToMoneyString(calc_price); // them thanh tien vao

                //  thanh toan hang thang
                try
                {
                    DatabaseUtils db = DatabaseUtils.getInstance();
                    DataTable dt = db.getThanhToanHangThangOfJobID(job_id);

                    Double totalKL = 0;
                    Double totalGT = 0;

                    //  just init every field to ZERO
                    for (int i = 0; i < mThanhlyKL.Count; i++)
                    {
                        String name = mThanhlyKL.ElementAt(i);
                        String name2 = mThanhlyGT.ElementAt(i);
                        drNew[name] = "0.0";
                        drNew[name2] = "0";
                    }
                    //  import data from table
                    if (dt != null)
                    {
                        foreach (DataRow tt in dt.Rows)
                        {
                            int d = (int)tt["month"];
                            String name = toKLThanhtoanFieldName(d);
                            String name2 = toThanhtoanFieldName(d);

                            if (isValidThanhlyKL(name))
                            {
                                Double kl = Utils.getDoubleValueOfRow(tt, "khoiluong");

                                totalKL += kl;
                                drNew[name] = Utils.doubleToString(kl);// kl.ToString("0.00");
                                kl = kl * dongia * dinhbien * dinhmuc;

                                String s = Utils.doubleToMoneyString(kl);
                                drNew[name2] = s;
                            }
                        }
                    }
                    //  tinh tong thanh ly
                    totalGT = totalKL * dongia * dinhbien * dinhmuc;
                    drNew[COLUMN_TongTLKL] = Utils.doubleToString(totalKL);// totalKL.ToString("0.00");
                    var a = totalGT;
                    drNew[COLUMN_TongTLGT] = Utils.doubleToMoneyString(totalGT);

                    //  tinh tong con lai
                    String stongKL = (String)drNew[COL_KHOILUONG];

                    Double tongKLOfProj = Utils.convertStringToDouble(stongKL);
                    Double remainKL = tongKLOfProj - totalKL;
                    Double remainGT = remainKL * dongia  * dinhbien * dinhmuc;

                    drNew[COLUMN_ConlaiKL] = Utils.doubleToString(remainKL);// remainKL.ToString("0.00");
                    drNew[COLUMN_ConlaiGT] = remainGT.ToString("#,###");
                }
                catch (Exception e)
                {
                    Console.WriteLine (e.Message);
                }
            }
            //=======================================

            mDataSource.Rows.Add(drNew);
        }

        private void addJobsToNode(TreeNode node, DataTable dt)
        {
            if (node == null)
                return;
            foreach (DataRow r in dt.Rows)
            {
                addRowToDataSourceList(r);

                int id = (int)r["job_id"];
                String name = (String)r["job_name"];
                int job_level = (int)r["job_level"];

                TreeNode n = new TreeNode() { Text = name };
                n.Tag = id;
                if (job_level == 0)
                {
                    n.NodeFont = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);
                }
                else if (job_level == C.JOB_LEVEL_DETAIL)
                {
                    n.ForeColor = Color.Blue;
                }

                node.Nodes.Add(n);

                DatabaseUtils db = DatabaseUtils.getInstance();
                DataTable tt = db.getJobOfProject(getPhieuID(), id);
                if (tt != null && tt.Rows.Count > 0)
                {
                    addJobsToNode(n, tt);
                }
            }
        }

        private bool validField(String s)
        {
            if (s == null || s.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void addProjectClick(object sender, EventArgs e)
        {
            /*
            FormAddProject form = new FormAddProject();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String maCT = form.textBoxProjectCode.Text;
                string name = form.getProjectName();
                string desc = form.getProjectDescription();

                if (!validField(maCT)
                    || !validField(name)
                    || !validField(team))
                {
                    MessageBox.Show("Dữ liệu chưa được nhập đúng hoặc thiếu, mời nhập lại.");

                    addProjectClick(null, null);

                    return;
                }

                //Database: insert here
                DatabaseUtils db = DatabaseUtils.getInstance();
                db.insertProject(maCT, name, desc, team);

                initProjectList();
            }
             * */
        }

        private TreeNode mRoot = null;

        private TreeNode getTaskTreeNodeWithJobID(int job_id, TreeNode node)
        {
            foreach (TreeNode aNode in node.Nodes)
            {
                int _id = (int)aNode.Tag;
                if (_id == job_id)
                    return aNode;
                else
                {
                    TreeNode n = getTaskTreeNodeWithJobID(job_id, aNode);
                    if (n != null)
                    {
                        return n;
                    }
                }
            }

            return null;
        }

        private int getPhieuID()
        {
            return mPhieuID;
            /*
            try
            {
                int idx = comboBoxProjectList.SelectedIndex;
                if (idx >= 0 && idx < comboBoxProjectList.Items.Count)
                {
                    DataRow r = mProjectList.Rows[idx];
                    return (int)r["IDCT"];
                }
            }
            catch(Exception e){
            }

            return -1;
             */
        }

        private int getCurrentNodeID()
        {
            int id = -1;
            try
            {
                id = (int)treeViewProjectTasks.SelectedNode.Tag;
                return id;
            }
            catch (Exception e)
            {
            }
            return -1;
        }

        private void addTaskToProject(object sender, EventArgs e)
        {
            if (isJobDetailSelected())
                return;

            FormAddTask form = new FormAddTask();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String task = form.getTaskName();

                //  insert new node to database
                DatabaseUtils db = DatabaseUtils.getInstance();
                int projectID = getPhieuID();
                int job_parent_id = (int)treeViewProjectTasks.SelectedNode.Tag;
                DataRow r = getJobAsDataRow(job_parent_id);
                if (r == null && !isRootSelected())
                    return; //  invalid node, something wrong
                int level = 0;
                if (projectID != -1)
                {
                    if (isRootSelected())
                        job_parent_id = 0;
                    else
                    {
                        level = Utils.getIntValueOfRow(r, COL_JOB_LEVEL);
                    }
                    level++;    //  for child, increase 1

                    db.insertJobToProject(projectID, task, job_parent_id, level, "I");
                }

                TreeNode newNode = new TreeNode() { Text = task };
                newNode.Tag = 123;  //  database row id
                treeViewProjectTasks.SelectedNode.Nodes.Add(newNode);

                refreshTree();
            }
        }

        /*
        bool isJobSuperParentOf(int job_parrent_id, int job_id)
        {
            if (mDataSourceRaw != null)
            {
                foreach (DataRow r in mDataSourceRaw.Rows)
                {
                    int _id = getIntValueOfRow(r, "job_id");
                    if (_id == job_id)
                    {
                        int _parrent = getIntValueOfRow(r, "job_parrent_id");
                        if (_parrent == job_parrent_id)
                            return true;
                    }
                }
            }
            return false;
        }
        */

        private int getJobIDOfRow(int idx)
        {
            try
            {
                DataRow r = mDataSource.Rows[idx];

                return (int)r[COL_JOB_ID];
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        private DataRow getJobAsDataRow(int jobID)
        {
            if (mDataSource == null)
                return null;

            try
            {
                foreach (DataRow r in mDataSource.Rows)
                {
                    int id = (int)r["job_id"];
                    if (id == jobID)
                        return r;
                }
            }
            catch (Exception e)
            {
            }

            return null;
        }

        private void updateTongKLThanhToan(int thanhlyID, int job_id)
        {
            DatabaseUtils db = DatabaseUtils.getInstance();

            DataRow r = getJobAsDataRow(job_id);

            int sub_job_id = Utils.getIntValueOfRow(r, COL_JOB_ID);
            int jobLevel = Utils.getIntValueOfRow(r, COL_JOB_LEVEL);
            int thanhly_id = thanhlyID;
            if (jobLevel == C.JOB_LEVEL_DETAIL)
            {
                //  tinh tong da thanh toan cho job nay
                Double totalKLTT = db.getTotalKhoiluongThanhLyOfJobDetail(getPhieuID(), sub_job_id);
                r[COL_TONG_THANHLY_KL] = Utils.doubleToString(totalKLTT);

                Double tongKLBanDau = Utils.convertStringToDouble((String)r[COL_KHOILUONG]);
                Double remain = tongKLBanDau - totalKLTT;
                r[COL_TONG_CONLAI_KL] = Utils.doubleToString(remain);
            }

            //refreshTongDaThanhToan(job_id);
        }

        /*
        void refreshTongDaThanhToan(int job_id)
        {
            //  sau khi 1 thang thanh ly dc cap nhat
            //  can cap nhat lai gia tri tong da thang toan & con lai
            DatabaseUtils db = DatabaseUtils.getInstance();
            Double tongChiPhi = db.getTotalMoneyOfJob(getPhieuID(), job_id);
            DataRow drNew = getJobAsDataRow(job_id);

            Double tongDaThanhLy = 0;
            //  tong cua tat ca cac tha'ng  da thanh toan
            for (int i = 0; i < mThanhlyGT.Count; i++)
            {
                string name = mThanhlyKL[i];
                string name2 = mThanhlyGT[i];
                int thanhly_id = (int)mHashColumnData[name];
                Double totalCuaThang = db.getTotalMoneyThanhLyOfJob(getPhieuID(), job_id, thanhly_id);
                //drNew[name2] = doubleToMoneyString(totalCuaThang);

                tongDaThanhLy += totalCuaThang;
            }
            //  tong Gia tri cua thanh ly
            Double oldDaThanhLy = convertMoneyStringToDouble((String)drNew[COLUMN_TongTLGT]);
            drNew[COLUMN_TongTLGT] = doubleToMoneyString(tongDaThanhLy);
            Double changed = tongDaThanhLy - oldDaThanhLy;

            Double conlai = tongChiPhi - tongDaThanhLy;
            drNew[COLUMN_ConlaiGT] = doubleToMoneyString(conlai);

            /*
            //  walk through the tree, up to root
            TreeNode node = treeViewProjectTasks.TopNode;

            node = getTaskTreeNodeWithJobID(job_id, node);
            if (node != null)
            {
                TreeNode nodeParent = node.Parent;
                while (true)
                {
                    if (nodeParent == null)
                        break;
                    int parrentID = (int)nodeParent.Tag;
                    DataRow r = getJobAsDataRow(parrentID);
                    if (r != null)
                    {
                        Double current = convertMoneyStringToDouble((String)r[COLUMN_TongTLGT]);
                        Double newVal = current + changed;

                        r[COLUMN_TongTLGT] = doubleToMoneyString(newVal);
                    }
                    //=============
                    nodeParent = nodeParent.Parent;
                }
            }
             * /
            /*
            foreach (DataRow r in mDataSource.Rows)
            {
                int parrent = getIntValueOfRow(r, COL_JOB_ID);
                if (isJobSuperParentOf(parrent, job_id))
                {
                    Double current = convertMoneyStringToDouble((String)r[COLUMN_TongTLGT]);
                    Double newVal = current + changed;

                    r[COLUMN_TongTLGT] = doubleToMoneyString(newVal);
                }
            }
            * /
        }
    */

        private bool isValidThanhlyKL(String name)
        {
            for (int i = 0; i < mThanhlyKL.Count; i++)
            {
                String s = mThanhlyKL[i];
                if (s.CompareTo(name) == 0)
                    return true;
            }

            return false;
        }

        private void updatePresentThanhtoan(DataRow dbRow, DataRow uiRow, Int64 dongia)
        {
            int d = (int)dbRow["month"];
            String name = toKLThanhtoanFieldName(d);
            String name2 = toThanhtoanFieldName(d);

            if (!isValidThanhlyKL(name))
                return;
            try
            {
                Double kl = Utils.getDoubleValueOfRow(dbRow, "khoiluong");
                uiRow[name] = Utils.doubleToString(kl);// kl.ToString("0.00");
                kl = kl * dongia;

                String s = Utils.doubleToMoneyString(kl);
                uiRow[name2] = s;

                return;
            }
            catch (Exception e)
            {
            }

            uiRow[name] = "0.0";
            uiRow[name2] = "0";
        }

        private String toKLThanhtoanFieldName(int d)
        {
            int y = d / 100;
            int m = d % 100;

            String name = "T.lý KL\n" + m + "/" + y + "";

            return name;
        }

        private String toKLThanhtoanShortnameFieldName(int d)
        {
            int y = d / 100;
            int m = d % 100;

            String name = "" + m + "/" + y + "";

            return name;
        }

        private String toThanhtoanFieldName(int d)
        {
            int y = d / 100;
            int m = d % 100;

            String name = "Thành tiền\n" + m + "/" + y + "";

            return name;
        }

        private bool isJobDetailSelected()
        {
            try
            {
                TreeNode n = treeViewProjectTasks.SelectedNode;
                int job_id = (int)n.Tag;

                DataRow r = getJobAsDataRow(job_id);
                if (r != null)
                {
                    int level = (int)r[COL_JOB_LEVEL];

                    return level == C.JOB_LEVEL_DETAIL;
                }
            }
            catch (Exception e)
            {
            }

            return false;
        }

        private void addTaskDetail(object sender, EventArgs e)
        {
            if (isJobDetailSelected())
                return;
            FormTaskDetail form = new FormTaskDetail();
            if (mCachTinhChiPhi == 0)
                form.textBoxDinhmuc.Enabled = false;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //  insert mota, mucKK, khoiluong, dongia, thanhtien vao database
                DatabaseUtils db = DatabaseUtils.getInstance();

                int projectID = getPhieuID();
                int parentID = getCurrentNodeID();

                if (parentID == -1 || isRootSelected())
                {
                    MessageBox.Show("Bạn hãy chọn mục để thêm công việc cụ thể!", "Lỗi", MessageBoxButtons.OK);
                    return;
                }

                String jobname = form.mota;

                jobname = jobname.Replace("+", ""); ////    haha, fix by hack

                String donvitinh = form.donvitinh;
                int kk = form.mucKK;
                float kl = form.khoiluong;
                float dinhmuc = (float)Utils.convertStringToDouble(form.textBoxDinhmuc.Text);
                Int64 dongia = (Int64)form.dongia;

                string thutu = form.thutu;
                var dinhbien = (float)Utils.convertStringToDouble(form.dinhbien.ToString());
                string ghichu = form.ghichu.Trim();

                db.insertJobToProject(projectID, parentID, jobname, donvitinh, kk, kl, dongia, dinhmuc, 9, dinhbien, thutu, ghichu);
                refreshTree();
            }
        }

        private void onNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Mdl_Share.currentUser.quyen == "USERS")
                {
                    var permissionDuAn2 = Mdl_Share.currentPermission.Where(x => x.MaCT == _MaCT).FirstOrDefault();
                    menuTaskSumary.Enabled = permissionDuAn2.add;
                    menuTaskDetail.Enabled = permissionDuAn2.add;
                    monthlyPaidToolStripMenuItem.Enabled = permissionDuAn2.add;
                    removeToolStripMenuItem.Enabled = permissionDuAn2.delete;
                }
                contextMenuStripThem.Show(treeViewProjectTasks, e.Location);
            }
        }

        private bool isRootSelected()
        {
            if (treeViewProjectTasks.GetNodeCount(false) == 0)
                return false;
            TreeNode node = treeViewProjectTasks.SelectedNode;
            if (treeViewProjectTasks.Nodes[0] == node)
            {
                return true;
            }

            return false;
        }

        private void removeNodeFromDatabase(TreeNode node)
        {
            foreach (TreeNode sub in node.Nodes)
            {
                removeNodeFromDatabase(sub);
            }

            int job_id = (int)node.Tag;
            DatabaseUtils db = DatabaseUtils.getInstance();
            db.removeJob(getPhieuID(), job_id);
        }

        private void removeTask(object sender, EventArgs e)
        {
            if (isRootSelected())
                return;

            TreeNode node = treeViewProjectTasks.SelectedNode;
            String msg = "Xóa công việc: " + node.Text;
            if (MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OKCancel)
                == System.Windows.Forms.DialogResult.OK)
            {
                removeNodeFromDatabase(node);

                refreshTree();
            }
        }

        private float getKhoiluongOfRow(int idx)
        {
            try
            {
                DataRow r = mDataSource.Rows[idx];

                String s = (String)r[COL_KHOILUONG];
                Double d = Utils.convertStringToDouble(s);

                return (float)d;
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        private int getLevelOfRow(int idx)
        {
            try
            {
                int level = (int)mDataSource.Rows[idx][COL_JOB_LEVEL];
                return level;
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        private void modifyJob(object sender, EventArgs e)
        {
            if (isRootSelected())
                return;

            if (!isAllowEdit)
            {
                return;
            }

            int jobID = getCurrentNodeID();
            if (jobID != -1)
            {
                DataRow r = getJobAsDataRow(jobID);

                int level = 0;
                try { level = (int)r[COL_JOB_LEVEL]; }
                catch (Exception ex) { }
                if (level == 0)
                {
                    String name = (String)r[1];
                    //Form
                    name = name.Trim();
                    FormAddTask form = new FormAddTask();

                    form.textBoxTask.Text = name;

                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DatabaseUtils db = DatabaseUtils.getInstance();
                        db.updateJobToProject(jobID, form.textBoxTask.Text, 0, 0);

                        refreshTree();
                    }
                }
                else
                {
                    String name = (String)r[1];
                    name = name.Trim();
                    FormTaskDetail form = new FormTaskDetail();
                    form.textBoxTaskKL.ReadOnly = true;
                    form.textBoxTaskPriceUnit.ReadOnly = true;
                    form.textBoxDonViTinh.ReadOnly = true;

                    form.textBoxDinhmuc.Enabled = false;
                    //if (mCachTinhChiPhi == 0)
                    //{
                    //    String sdongia = Utils.getStringOfRow(r, COL_DONGIA);
                    //    form.textBoxTaskPriceUnit.Text = "" + sdongia;
                    //}
                    //else
                    //{
                        String sdongia = Utils.getStringOfRow(r, COL_DONGIA_CONG);
                        form.textBoxTaskPriceUnit.Text = "" + sdongia;

                        String sdinhmuc = Utils.getStringOfRow(r, COL_DINHMUC);
                        form.textBoxDinhmuc.Text = "" + sdinhmuc;

                        String sdinhbien = Utils.getStringOfRow(r, COL_DINHBIEN);
                        form.txtDinhBien.Text = "" + sdinhbien;
                    //}

                    form.textBoxTaskDetail.Text = name;
                    if (level == C.JOB_LEVEL_DETAIL)
                    {
                        String skl = (String)r[COL_KHOILUONG];
                        form.textBoxTaskKL.ReadOnly = false;
                        form.textBoxTaskKL.Text = skl;

                        form.textBoxTaskKK.Text = "" + r[3];
                    }
                    else
                    {
                        form.textBoxTaskKL.ReadOnly = true;
                        form.textBoxTaskKK.ReadOnly = true;
                    }

                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DatabaseUtils db = DatabaseUtils.getInstance();
                        Double kl = 0;
                        int kk = 0;
                        if (level == C.JOB_LEVEL_DETAIL)
                        {
                            kl = Utils.convertStringToDouble(form.textBoxTaskKL.Text);
                            kk = form.mucKK;
                        }

                        db.updateJobToProject(jobID, form.textBoxTaskDetail.Text, kk, kl);

                        refreshTree();
                    }
                }
            }
        }

        public void insertThanhToanHangThang()
        {
            if (getPhieuID() <= 0)
            {
                MessageBox.Show("Hãy Open dự án trước khi thanh toán");
                return;
            }

            DateTime d = DateTime.Now;

            FormThanhToanHangThang form = new FormThanhToanHangThang();
            form.textBoxMonth.Text = d.ToString("MM/yyyy");
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String s = form.textBoxMonth.Text;

                try
                {
                    string[] arr = s.Split(new char[] { '/' });
                    int m = Int32.Parse(arr[0]);
                    int y = Int32.Parse(arr[1]);

                    //  date: YYYYMM
                    int dInt = y * 100 + m;

                    DatabaseUtils db = DatabaseUtils.getInstance();
                    if (!db.isThanhtoanAvailable(getPhieuID(), dInt))
                    {
                        db.insertThanhtoanHangThang(getPhieuID(), dInt);
                        //  refresh
                        refreshProject();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi. Thanh toán cho tháng: " + s + " đã có.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nhập sai tháng/năm");
                }
            }
        }

        private void cellFormating(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int level = getLevelOfRow(e.RowIndex);

            DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
            System.Drawing.Color bc = System.Drawing.Color.White;

            if (level == 0 && e.ColumnIndex < COLUMN_ThanhLy0)
            {
                //e.CellStyle.BackColor = Color.DarkGreen;
                bc = ColorTranslator.FromHtml("#95de64") ;
                e.CellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            }
            else if (level < C.JOB_LEVEL_DETAIL && e.ColumnIndex < COLUMN_ThanhLy0)
            {
                if (level == 1)
                    bc = Color.LightCyan;
                else if (level == 2)
                    bc = Color.Cyan;
                else
                    bc = Color.LightYellow;
            }
            else//if (level == C.JOB_LEVEL_DETAIL)
            {
                if (e.ColumnIndex == COLUMN_TongTLKL || e.ColumnIndex == COLUMN_TongTLGT)
                {
                    bc = Color.Orange;
                }
                else if (e.ColumnIndex == COLUMN_ConlaiKL || e.ColumnIndex == COLUMN_ConlaiGT)
                {
                    bc = Color.Yellow;
                }
                else if (e.ColumnIndex >= COLUMN_ThanhLy0)
                {
                    if (COLUMN_ThanhLy_Count > 0)
                    {
                        int tmp = e.ColumnIndex - COLUMN_ThanhLy0;
                        tmp /= 2;
                        tmp = tmp % 2;
                        if (tmp == 0)
                            bc = Color.LightPink;
                        else
                            bc = Color.LightGreen;
                    }
                }
                //r.Cells[e.ColumnIndex].Style.BackColor = e.CellStyle.BackColor;
            }

            if (bc != null)
                r.Cells[e.ColumnIndex].Style.BackColor = bc;

            //e.CellStyle.BackColor
        }

        private void cellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;

            try
            {
                //if (Mdl_Share.currentUser.quyen == "USERS")
                //{
                //    var permissionDuAn2 = Mdl_Share.currentPermission.Where(x => x.MaCT == _MaCT).FirstOrDefault();
                //   if(!permissionDuAn2.edit) {
                //        XtraMessageBox.Show("Bạn chưa được cấp quyền chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //   }
                //}

                //var totalKL = 0.0d;
                //var kl_bandau = 0.0d;
                //foreach (DataColumn item in mDataSource.Columns)
                //{
                //    if(item.ColumnName.Contains("T.lý KL"))
                //    {
                //        totalKL += Convert.ToDouble(mDataSource.Rows[row][item.ColumnName]);
                //    }
                //    if (item.ColumnName == COL_KHOILUONG) {
                //        kl_bandau = Convert.ToDouble(mDataSource.Rows[row][item.ColumnName]);
                //    }
                //}

                //var conlai = kl_bandau - totalKL;
                //if(conlai < 0.0d)
                //{
                //    XtraMessageBox.Show($"Khối lượng bạn nhập đã vượt quá: <color=red><b>{(conlai * -1.0).ToString("#,#")}<b></color>, vui lòng nhập lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, allowHtmlText: DevExpress.Utils.DefaultBoolean.True);

                //    return;
                //}
                DataColumn dc = mDataSource.Columns[col];
                if (dc.ColumnName == COL_KHOILUONG)
                {
                    Double _kl = 0;
                    String _s = (String)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    _kl = Utils.convertStringToDouble(_s);
                    DataRow _r = mDataSource.Rows[row];
                    int _job_id = (int)_r[COL_JOB_ID];
                    SQLHelper.ExecQueryNonData($"update CONGVIEC set unit_amount='{_kl}' where job_id='{_job_id}'");
                    updateUIThanhTien();
                    return;
                }

                //Double totalKLTT = db.getTotalKhoiluongThanhLyOfJobDetail(getPhieuID(), sub_job_id);
                //r[COL_TONG_THANHLY_KL] = Utils.doubleToString(totalKLTT);

                //Double tongKLBanDau = Utils.convertStringToDouble((String)r[COL_KHOILUONG]);
                //Double remain = tongKLBanDau - totalKLTT;
                String name = dc.Caption;
                int thanhtoan_id = (int)mHashColumnData[name];

                Double kl = 0;
                String s = (String)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                kl = Utils.convertStringToDouble(s);

                DataRow r = mDataSource.Rows[row];
                int job_id = (int)r[COL_JOB_ID];
                //String sdongia = (String)r[COL_DONGIA];
                //Double dongia = Double.Parse(sdongia);
                //Double dinhmuc = Utils.getDoubleValueOfRow(r, COL_DINHMUC);

                //double tien = kl * dongia;
                DatabaseUtils db = DatabaseUtils.getInstance();
                db.updateThanhToanHangThangOfJob(job_id, thanhtoan_id, (float)kl); // update khoi luong, month theo job_id va thanhtoan_id, neu khong update dc thi insert, thuc hien vao bang thanhtoan_id

                updateTongKLThanhToan(thanhtoan_id, job_id);
                updateUIThanhTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int col = e.ColumnIndex;
            DataColumn dc = mDataSource.Columns[col];
            int jobLevel = getLevelOfRow(e.RowIndex);
            if (jobLevel == C.JOB_LEVEL_DETAIL)
            {
                //int col = e.ColumnIndex;
                col -= COLUMN_ThanhLy0;
                col = col % 2;
                bool editListColumns = (col == 0 && col < COLUMN_ThanhLy_Count && dc.ColumnName != COL_TONG_THANHLY_KL && dc.ColumnName != COL_TONG_CONLAI_KL) || (dc.ColumnName == COL_KHOILUONG);
                if (editListColumns)   //  colume ThanhToan GT
                {
                    e.Cancel = false;
                }
                else
                    e.Cancel = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void updateUIThanhTien()
        {
            //TreeNode node = treeViewProjectTasks.TopNode;
            TreeNode node = treeViewProjectTasks.Nodes[0];

            var conlai = calcThanhTienOfColumn(COL_KHOILUONG, COL_THANHTIEN, node, false); // Ham cap nhat cac gia tri dong tong
            var a = calcThanhTienOfColumn(COL_TONG_THANHLY_KL, COL_TONG_THANHLY_GT, node, false);
            var b = calcThanhTienOfColumn(COL_TONG_CONLAI_KL, COL_TONG_CONLAI_GT, node, false);

            for (int i = 0; i < mThanhlyKL.Count; i++)
            {
                String colKL = mThanhlyKL[i];
                String colGT = mThanhlyGT[i];

                calcThanhTienOfColumn(colKL, colGT, node, true); // Ham cap nhat cac gia tri dong tong
            }

            try
            {
                //  update to phieugiaonhan table
                DataRow r = mDataSource.Rows[0];
                String sTongChiPhi = (String)r[COL_THANHTIEN];
                String sDaThanhtoan = (String)r[COLUMN_TongTLGT];

                Double v1 = Utils.convertMoneyStringToDouble(sTongChiPhi);
                Double v2 = Utils.convertMoneyStringToDouble(sDaThanhtoan);

                DatabaseUtils.getInstance().updateTongTienForPhieu(getPhieuID(), v1, v2);
            }
            catch (Exception ex)
            {
            }
        }

        // Ham cap nhat giao dien cac dong tong
        private Double calcThanhTienOfColumn(String klCol, String thanhtienCol, TreeNode node, bool isThanhly)
        {
            Double v = 0;

            DataRow r = null;

            if (node == treeViewProjectTasks.TopNode)
            {
                r = mDataSource.Rows[0];
            }
            else
            {
                int jobID = (int)node.Tag;
                r = getJobAsDataRow(jobID);
            }
            if (r != null)
            {
                int level = (int)r[COL_JOB_LEVEL];

                if (level == C.JOB_LEVEL_DETAIL)
                {
                    Double dongia = 0;

                    //if (mCachTinhChiPhi == 0)
                    //{
                    //    String sdongia = (String)r[COL_DONGIA];
                    //    dongia = Utils.convertMoneyStringToDouble(sdongia);
                    //}
                    //else
                    //{
                        String sdongia = (String)r[COL_DONGIA_CONG];
                        dongia = Utils.convertMoneyStringToDouble(sdongia);

                    String sdinhbien = (String)r[COL_DINHBIEN];
                    String sdinhmuc = (String)r[COL_DINHMUC];

                            dongia = dongia * Utils.convertStringToDouble(sdinhmuc) * Utils.convertStringToDouble(sdinhbien);
                        
                   // }

                    String skl = r[klCol] == DBNull.Value ? "" : r[klCol].ToString();
                    Double kl = Utils.convertStringToDouble(skl);

                    v = dongia * kl;
                    String s = Utils.doubleToMoneyString(v);

                    r[thanhtienCol] = s;
                }
                else
                {
                    foreach (TreeNode sub in node.Nodes)
                    {
                        v += calcThanhTienOfColumn(klCol, thanhtienCol, sub, isThanhly);
                    }
                    if (v < 0.0d)
                    {
                        return v;
                    }
                    String s = Utils.doubleToMoneyString(v);
                    r[thanhtienCol] = s; // Cap nhat gia tri vao cac dong tong
                }
            }

            return v;
        }

        private void onProjectClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //FormMDIMainApplication.mInstance.showPhieuListOfProject(mProjectID, mProjectName);
            this.Close();
        }

        private void toolStripMenuItemThanhtoan_Click(object sender, EventArgs e)
        {
            insertThanhToanHangThang();
        }

        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            ExcelExporter.dataGridViewToExcel(this.dataGridView1);
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xuatHopDong();
        }

        private void xuatHopDong()
        {
            if (mPhieuID == 0)
                return;
            DataTable tableQuyetdinh = DatabaseUtils.getInstance().getPhieuGiaoViecOfByPhieuID(mPhieuID);
            if (tableQuyetdinh == null || tableQuyetdinh.Rows.Count == 0)
                return;
            //==========================================
            ExcelExporter excel = new ExcelExporter();
            ex.Range range;

            //==================================
            excel.writeLine("A1", "B1", "CÔNG TY TNHH MTV TĐBĐ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A2", "B2", "XN PHÁT TRIỂN CÔNG NGHỆ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A3", "B3", "TRẮC ĐỊA BẢN ĐỒ", 10, ExcelExporter.ALIGN_CENTER);

            excel.writeLine("C1", "G1", "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("C2", "G2", "Độc lập – Tự do – Hạnh Phúc", 10, ExcelExporter.ALIGN_CENTER, false, true);

            range = excel.writeLine("C5", "G5", "Ngày........tháng........năm........................", 10, ExcelExporter.ALIGN_RIGHT);
            excel.writeLine("A7", "G7", "DỰ TOÁN KINH PHÍ THI CÔNG", 10, ExcelExporter.ALIGN_CENTER, true, false);
            excel.writeLine("A8", "G9", "Công trình: " + mProjectName, 10, ExcelExporter.ALIGN_CENTER, true, false);
            excel.writeLine("A10", "G11", "Quyết định: " + mPhieuName, 10, ExcelExporter.ALIGN_CENTER, true, false);

            //  columns width
            int[] cw = { 5, 28, 6, 4, 6, 10, 14 };
            int i = 0;
            for (i = 0; i < cw.Length; i++)
            {
                String col = "" + (char)('A' + i) + "1";
                excel.setColumnWidth(col, cw[i]);
            }
            //  header
            int tableY0 = 12;
            int tableY = tableY0;
            int stt = tableY0;
            String[] ss = { "STT", "Nội dung công việc", "ĐVT", "KK", "KL", "Đơn giá", "TT (đồng)" };
            for (i = 0; i < ss.Length; i++)
            {
                char a = (char)('A' + i);
                String b = "" + a + stt.ToString();
                String e = "" + a + stt.ToString();

                excel.writeLine(b, e, ss[i], 9, ExcelExporter.ALIGN_CENTER, true, false);
            }

            range = excel.getRange("A" + stt.ToString(), "G" + stt.ToString());
            range.WrapText = true;
            tableY += 1;
            //=================================================================
            i = 0;
            int tmp = 1;

            List<Int64> Tongkhoan = new List<Int64>();

            int level;
            bool first = false;

            int[] _level = { 0, 0, 0, 0 };
            char[] _levelC = { 'A', 'a', '1', 'a' };
            String s;
            foreach (DataGridViewRow vr in dataGridView1.Rows)
            {
                if (first == false)
                {
                    first = true;
                    continue;
                }

                level = (int)vr.Cells[2].Value;

                //  danh index
                char indexC = '~';
                if (level < C.JOB_LEVEL_DETAIL)
                {
                    range = excel.getRange("A" + tableY.ToString(), "J" + tableY.ToString());
                    range.Font.Italic = true;
                    range.Font.Bold = true;

                    int kk = level - 1;    //  level == 0 la quyetdinh-row, reject it
                    if (kk < 4)
                    {
                        indexC = (char)(_levelC[kk] + _level[kk]);
                        for (int j = kk + 1; j < 4; j++) { _level[j] = 0; }
                        tmp = 1;
                        _level[kk]++;
                    }
                }
                if (level == C.JOB_LEVEL_DETAIL)
                {
                    excel.ap.Cells[tableY, 1] = "" + tmp++;
                }
                else { excel.ap.Cells[tableY, 1] = "" + indexC; }
                //=============================

                excel.ap.Cells[tableY, 2] = vr.Cells[1].Value;  //  noi dung
                excel.ap.Cells[tableY, 4] = vr.Cells[3].Value;  //  kho khan
                excel.ap.Cells[tableY, 5] = vr.Cells[5].Value;  //  khoi luong khoan

                if (vr.Cells[6].Value != null && vr.Cells[6].Value != DBNull.Value)
                {
                    s = (String)vr.Cells[6].Value;
                    Double d = Utils.convertMoneyStringToDouble(s);
                    s = Utils.doubleToString(d);
                    excel.ap.Cells[tableY, 6] = s;
                }

                s = "";
                //  tong gia tri khoan
                if (vr.Cells[7].Value != null && vr.Cells[7].Value != DBNull.Value)
                {
                    s = (String)vr.Cells[7].Value;
                    Double d = Utils.convertMoneyStringToDouble(s);
                    s = Utils.doubleToString(d);
                    excel.ap.Cells[tableY, 7] = s;
                }

                tableY++;
            }
            //======================================
            excel.ap.Cells[tableY, 2] = "Tổng cộng";
            String c = "B" + tableY.ToString();
            String c2 = "J" + tableY.ToString();
            excel.getRange(c, c2).Font.Bold = true;

            if (tableQuyetdinh != null && tableQuyetdinh.Rows.Count > 0)
            {
                DataRow row = tableQuyetdinh.Rows[0];
                Int64 chiphi = Utils.sqlNumberToInt64(row["chi_phi"]);
                excel.ap.Cells[tableY, 7] = Utils.doubleToString(chiphi);
            }
            //======================================
            range = excel.getRange("A" + tableY0.ToString(), "G" + tableY.ToString());
            range.Font.Size = 9;
            range.WrapText = true;

            range.HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = ex.XlHAlign.xlHAlignCenter;

            range.Borders.LineStyle = ex.Constants.xlSolid;
            range.RowHeight = 20;
            //========================
            range = excel.getRange("B" + tableY0.ToString(), "B" + tableY.ToString());
            range.HorizontalAlignment = ex.XlHAlign.xlHAlignLeft;
            //===================="H" + (3 + stt).ToString()===================
            stt = tableY + 2;
            excel.writeLine("A" + stt.ToString(), "B" + stt.ToString(), "Người lập", 10, ExcelExporter.ALIGN_CENTER, true, false);
            excel.writeLine("C" + stt.ToString(), "G" + stt.ToString(), "Phòng KHTH", 10, ExcelExporter.ALIGN_CENTER, true, false);

            excel.completeDocument("quyetdinhgiaoviec");
        }

        private int mRemovingColumnIDX = -1;

        private void onColumnClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int col = e.ColumnIndex;
                if (col >= COLUMN_ThanhLy0 && col < COLUMN_ThanhLy0 + 2 * COLUMN_ThanhLy_Count)
                {
                    mRemovingColumnIDX = (col - COLUMN_ThanhLy0) / 2;
                    this.contextMenuColumn.Show(Cursor.Position);
                }
            }
        }

        private void xoaThanhtoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mRemovingColumnIDX >= 0 && mRemovingColumnIDX < COLUMN_ThanhLy_Count)
            {
                String month = (String)dataGridView1.Columns[COLUMN_ThanhLy0 + mRemovingColumnIDX * 2].Name;
                if (MessageBox.Show("Bạn có chắc chắn xóa toàn bộ cột thanh toán hàng tháng: " + month + " không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    int thanhtoan_id = (int)mHashColumnData[month];
                    DatabaseUtils.getInstance().removeThanhtoanhangthang(thanhtoan_id);

                    refreshProject();
                }
            }
        }

        private void hideShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideThanhtoanColumnsDialog dialog = new HideThanhtoanColumnsDialog(mThanhlyShortName, mHiddenItems);

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<String> checkedItems = dialog.getCheckedItems();
                mHiddenItems.Clear();

                for (int i = 0; i < checkedItems.Count; i++)
                {
                    String s = checkedItems.ElementAt(i);
                    mHiddenItems.Add(s);
                }

                //==============reset first=============
                for (int i = COLUMN_ThanhLy0; i < COLUMN_TongTLKL; i += 2)
                {
                    dataGridView1.Columns[i].Visible = true;
                    dataGridView1.Columns[i + 1].Visible = true;
                    //dataGridView1.Columns[i].Width = 55;
                    //dataGridView1.Columns[i + 1].Width = 80;
                }
                //==================================
                for (int j = 0; j < mHiddenItems.Count; j++)
                {
                    String s = "|" + mHiddenItems.ElementAt(j);

                    for (int i = COLUMN_ThanhLy0; i < COLUMN_TongTLKL; i += 2)
                    {
                        //  if (dataGridView1.Columns[i].HeaderText.IndexOf(s) >= 0)
                        var headerText = dataGridView1.Columns[i].HeaderText.Replace("\n", "|");
                        if (headerText.Contains(s))
                        {
                            dataGridView1.Columns[i].Visible = false;
                            dataGridView1.Columns[i + 1].Visible = false;
                            //dataGridView1.Columns[i].Width = 0;
                            //dataGridView1.Columns[i + 1].Width = 0;
                        }
                    }
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            var thangSelected = cb_thang_export.EditValue;
            if(thangSelected == null)
            {
                XtraMessageBox.Show("Vui lòng chọn tháng thanh toán để xuất, bạn ơi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dataTable = null;

            if (dataGridView1.DataSource is DataTable dt)
            {
                dataTable = dt;
            }
            else if (dataGridView1.DataSource is DataView dv)
            {
                dataTable = dv.Table;
            }

          //  var b = dataTable;

            var listTemplate1 = new List<ExportTemplate>();
            var listTemplateFull = new List<ExportTemplate>();


            int index = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["job_level"].ToString() == "0")
                {
                    index++;
                    continue;
                }
                double khoiluong = 0.0;
                var khoiluong_fieldname = toKLThanhtoanFieldName(Convert.ToInt32(thangSelected));
                var thanhtien_fieldname = toThanhtoanFieldName(Convert.ToInt32(thangSelected));

                Double.TryParse(row[khoiluong_fieldname].ToString(), out khoiluong);
                //if (row["job_level"].ToString() == "9" && khoiluong <= 0.0)
                //{
                //    index++;
                //    continue;
                //}

                //if (row["TT"].ToString() == "II")
                //{
                //    isMuc1 = false;
                //}

                var item = new ExportTemplate();
                item.tt = row[1].ToString();
                item.noidung = row[2].ToString();
                item.dvt = row[5].ToString();
                item.khokhan = row[4].ToString();
                item.dinhbien = row[8].ToString();
                item.dinhmuc = row[9].ToString();
                item.dongia = row[7].ToString();
               
                item.khoiluong = Convert.ToDouble(row[khoiluong_fieldname] == DBNull.Value ?  0 :  row[khoiluong_fieldname]);
                item.thanhtien = row[thanhtien_fieldname].ToString();
                double tongkhoiluong = 0;
                double tongkhoiluong_thanhly = 0;

                double.TryParse(row[6].ToString(), out tongkhoiluong);
                double.TryParse(row["Tổng KL T.lý"].ToString(), out tongkhoiluong_thanhly);

                item.tongkhoiluong = tongkhoiluong;
                item.tongkhoiluong_thanhly = tongkhoiluong_thanhly;
                listTemplateFull.Add(item);

                var arr = new string[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };

                if (row["job_level"].ToString() == "1")
                {
                    string nextItemTT = "I";

                    if (index + 1 < dataTable.Rows.Count)
                    {
                        nextItemTT = dataTable.Rows[index + 1]["TT"].ToString();
                    }
                    else
                    {
                        nextItemTT = "I";
                    }

                    if (arr.Contains(nextItemTT))
                    {
                        item = new ExportTemplate();
                        listTemplateFull.Add(item);
                    }
                }


                index++;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["job_level"].ToString() == "0")
                {
                    index++;
                    continue;
                }
                double khoiluong = 0.0;
                var khoiluong_fieldname = toKLThanhtoanFieldName(Convert.ToInt32(thangSelected));
                var thanhtien_fieldname = toThanhtoanFieldName(Convert.ToInt32(thangSelected));

                Double.TryParse(row[khoiluong_fieldname].ToString(), out khoiluong);
                if (row["job_level"].ToString() == "9" && khoiluong <= 0.0)
                {
                    index++;
                    continue;
                }

                //if (row["TT"].ToString() == "II")
                //{
                //    isMuc1 = false;
                //}

                var item = new ExportTemplate();
                item.tt = row[1].ToString();
                item.noidung = row[2].ToString();
                item.dvt = row[5].ToString();
                item.khokhan = row[4].ToString();
                item.dinhbien = row[8].ToString();
                item.dinhmuc = row[9].ToString();
                item.dongia = row[7].ToString();
                item.khoiluong = Convert.ToDouble(row[khoiluong_fieldname] == DBNull.Value ? 0 : row[khoiluong_fieldname]);
                item.thanhtien = row[thanhtien_fieldname].ToString();
                listTemplate1.Add(item);

                var arr = new string[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };

                if (row["job_level"].ToString() == "1")
                {
                    string nextItemTT = "I";
                
                    if (index + 1 < dataTable.Rows.Count)
                    {
                        nextItemTT = dataTable.Rows[index + 1]["TT"].ToString();
                    }
                    else
                    {                       
                        nextItemTT = "I";
                    }

                    if (arr.Contains(nextItemTT))
                    {
                        item = new ExportTemplate();
                        listTemplate1.Add(item);
                    }
                }


                index++;
            }

         

             var dataSet = new DataSet();
            var dataTable1 = ConvertToDataTable(listTemplate1);
            dataTable1.TableName = "giaokhoankp";
            dataSet.Tables.Add(dataTable1);

            var dataTable2 = ConvertToDataTable(listTemplate1);
            dataTable2.TableName = "bienban";
            dataSet.Tables.Add(dataTable2);

            var dataTable3 = ConvertToDataTable(listTemplateFull);         
            dataTable3.TableName = "xacnhan";

            dataSet.Tables.Add(dataTable3);

            var dataTable4 = ConvertToDataTable(listTemplateFull);
            dataTable4.TableName = "thanhly";

            dataSet.Tables.Add(dataTable4);

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {                
                saveFileDialog.FileName = $"{thangSelected}_ExportedData.xlsx";                                                              
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
              
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

               
                DialogResult result = saveFileDialog.ShowDialog();

                // If the user clicks the "Save" button
                if (result == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    var options = new MiniExcelLibs.OpenXml.OpenXmlConfiguration
                    {
                       //EnableWriteNullValueCell = true,
                       //FastMode = true,
                       //IgnoreTemplateParameterMissing = true,
                       
                       
                    };
                    TemplateExcel.FillReport(path, PATH_TEMPLATE, dataSet, new string[] { "{", "}" });


                    Process.Start(path);
                }
            }

        }


        public readonly string PATH_TEMPLATE = Application.StartupPath + @"\template_export\template.xlsx";

        public static DataTable ConvertToDataTable(List<ExportTemplate> list)
        {
            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Define the columns
            dataTable.Columns.Add("tt", typeof(string));
            dataTable.Columns.Add("noidung", typeof(string));
            dataTable.Columns.Add("dvt", typeof(string));
            dataTable.Columns.Add("khokhan", typeof(string));
            dataTable.Columns.Add("dinhbien", typeof(string));
            dataTable.Columns.Add("dinhmuc", typeof(string));
            dataTable.Columns.Add("dongia", typeof(Int64));
            dataTable.Columns.Add("khoiluong", typeof(double));
            dataTable.Columns.Add("thanhtien", typeof(Int64));
            dataTable.Columns.Add("ghichu", typeof(string));
            dataTable.Columns.Add("tongkhoiluong", typeof(double));
            dataTable.Columns.Add("tongkhoiluong_thanhly", typeof(double));
            dataTable.Columns.Add("thanhtoancuoikytruoc", typeof(double));

            dataTable.Columns.Add("luykedenhetkynay", typeof(double));
            dataTable.Columns.Add("tt_phieugiaokhoan", typeof(double));
            dataTable.Columns.Add("tt_cuoikytruoc", typeof(double));
            dataTable.Columns.Add("tt_thanhtoankynay", typeof(double));
            dataTable.Columns.Add("tt_luykekynay", typeof(double));
            dataTable.Columns.Add("tt_conlai", typeof(double));

            dataTable.Columns.Add("hoanthanh", typeof(double));
            dataTable.Columns.Add("tt_cackytruoc", typeof(double));
            dataTable.Columns.Add("conduocthanhtoan", typeof(double));





            //            hoanthanh
            //   tt_cackytruoc
            //   conduocthanhtoan


            //  luykedenhetkynay
            //   tt_phieugiaokhoan
            //   tt_cuoikytruoc
            //   tt_thanhtoankynay
            //  tt_luykekynay
            //  tt_conlai

            // Populate the DataTable with data from the list
            foreach (var item in list)
            {
                var row = dataTable.NewRow();
                row["tt"] = item.tt;
                row["noidung"] = item.noidung;
                row["dvt"] = item.dvt;
                row["khokhan"] = item.khokhan;
                row["dinhbien"] = item.dinhbien;
                row["dinhmuc"] = item.dinhmuc;
                row["dongia"] = item.dongia == null ? 0 : Convert.ToInt64(item.dongia == "" ? "0" : item.dongia.Replace(".", ""));
                row["khoiluong"] = item.khoiluong == null ? 0 : Convert.ToDouble( item.khoiluong);
                row["thanhtien"] = item.thanhtien == null ? 0 : Convert.ToInt64( item.thanhtien.Replace(".", ""));
                row["ghichu"] = "";
                row["tongkhoiluong"] = item.tongkhoiluong == null ? 0 : Convert.ToDouble(item.tongkhoiluong);
                row["tongkhoiluong_thanhly"] = item.tongkhoiluong_thanhly == null ? 0 : Convert.ToDouble( item.tongkhoiluong_thanhly);
                row["thanhtoancuoikytruoc"] = item.thanhtoancuoikytruoc == null ? 0 : Convert.ToDouble(item.thanhtoancuoikytruoc);

                row["luykedenhetkynay"] = item.luykedenhetkynay == null ? 0 : Convert.ToDouble(item.luykedenhetkynay);
                row["tt_phieugiaokhoan"] = item.tt_phieugiaokhoan == null ? 0 : Convert.ToDouble(item.tt_phieugiaokhoan);
                row["tt_cuoikytruoc"] = item.tt_cuoikytruoc == null ? 0 : Convert.ToDouble(item.tt_cuoikytruoc);
                row["tt_thanhtoankynay"] = item.tt_thanhtoankynay == null ? 0 : Convert.ToDouble(item.tt_thanhtoankynay);
                row["tt_luykekynay"] = item.tt_luykekynay == null ? 0 : Convert.ToDouble(item.tt_luykekynay);
                row["tt_conlai"] = item.tt_conlai == null ? 0 : Convert.ToDouble(item.tt_conlai);

                row["hoanthanh"] = item.hoanthanh == null ? 0 : Convert.ToDouble(item.hoanthanh);
                row["tt_cackytruoc"] = item.tt_cackytruoc == null ? 0 : Convert.ToDouble(item.tt_cackytruoc);
                row["conduocthanhtoan"] = item.conduocthanhtoan == null ? 0 : Convert.ToDouble(item.conduocthanhtoan);





                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }




    
    public class ExportTemplate
    {
        public string tt { get; set; }
        public string noidung { get; set; }
        public string dvt { get; set; }
        public string khokhan { get; set; }
        public string dinhbien { get; set; }
        public string dinhmuc { get; set; }
        public string dongia { get; set; }
        public double? khoiluong { get; set; }
        public string thanhtien { get; set; }
        public string ghichu { get; set; } = "";

        public double? tongkhoiluong { get; set; }
        public double? tongkhoiluong_thanhly { get; set; }
        public double? thanhtoancuoikytruoc {
            get {
               

                return tongkhoiluong_thanhly - khoiluong;
            }        
        }

        public double? luykedenhetkynay
        {
            get
            {
                return thanhtoancuoikytruoc + khoiluong;
            }
        }

        public double? tt_phieugiaokhoan
        {
            get
            {
                return tongkhoiluong * ConvertStringToDouble( dinhbien) * ConvertStringToDouble(dinhmuc) * ConvertStringToDouble( dongia == null ? "" : dongia .Replace(".", ""));
            }
        }

        public double? tt_cuoikytruoc
        {
            get
            {
                return thanhtoancuoikytruoc * ConvertStringToDouble(dinhbien) * ConvertStringToDouble(dinhmuc) * ConvertStringToDouble(dongia == null ? "" : dongia.Replace(".", ""));
            }
        }

        public double? tt_thanhtoankynay
        {
            get
            {
                return khoiluong * ConvertStringToDouble(dinhbien) * ConvertStringToDouble(dinhmuc) * ConvertStringToDouble(dongia == null ? "" : dongia.Replace(".", ""));
            }
        }

        public double? tt_luykekynay
        {
            get
            {
                return luykedenhetkynay * ConvertStringToDouble(dinhbien) * ConvertStringToDouble(dinhmuc) * ConvertStringToDouble(dongia == null ? "" : dongia.Replace(".", ""));
            }
        }
        public double? tt_conlai
        {
            get
            {
                return tt_phieugiaokhoan - tt_luykekynay;
            }
        }




        public double? hoanthanh
        {
            get
            {
                return ConvertStringToDouble(dinhbien) * ConvertStringToDouble(dinhmuc) * ConvertStringToDouble(dongia == null ? "" : dongia.Replace(".", "")) * tongkhoiluong_thanhly;
            }
        }

        public double? tt_cackytruoc
        {
            get
            {
                return ConvertStringToDouble(dinhbien) * ConvertStringToDouble(dinhmuc) * ConvertStringToDouble(dongia == null ? "" : dongia.Replace(".", "")) * thanhtoancuoikytruoc;
            }
        }

        public double? conduocthanhtoan
        {
            get
            {
                return tt_cackytruoc - hoanthanh;
            }
        }

        public double ConvertStringToDouble(string input)
        {
            // Try to parse the input string as a double
            if (double.TryParse(input, out double result))
            {
                return result;
            }
            // Return 1 if conversion fails
            return 0;
        }



    }
}