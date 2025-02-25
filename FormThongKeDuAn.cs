using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ex = Microsoft.Office.Interop.Excel;
using DevExpress.XtraEditors;
using System.Globalization;
using System.Threading;
using DevExpress.Skins;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;

namespace ProjectStorage
{
    public partial class FormThongKeDuAn : XtraForm
    {
        int STATE_THONGKE_CAC_DUAN = 0;
        int STATE_THONGKE_1_DUAN = 1;
        int STATE_THONGKE_1_QUYETDINH = 2;

        static String COL_PHIEU_ID = "id";
        static String COL_PROJECT_CODE = "Mã dự án";
        static String COL_PHIEU_NO = "Số quyết định";
        static String COL_PHIEU_NAME = "Tên quyết định";
        static String COL_TEAM = "Bộ phận thi công";
        static String COL_TONG_KHOAN = "Tổng khoán";
        static String COL_DA_THANH_TOAN = "Đã thanh toán";

        //===============================
        static String COL_JOB_ID = "job_id";
        static String COL_NOIDUNG = "Nội dung công việc";
        static String COL_JOB_LEVEL = "job_level";
        static String COL_KHOKHAN = "Mức K.Khăn";
        static String COL_KHOILUONG = "Khối lượng";
        static String COL_DONGIA = "Đơn giá";
        static String COL_DONGIA_CONG = "Công";
        static String COL_DINHMUC = "ĐMức";
        static String COL_DONVITINH = "Đơn vị tính";
        static String COL_THANHTIEN = "Thành tiền";
        static String COL_TONG_THANHLY_KL = "Tổng KL T.lý"; // fdat
        static String COL_TONG_THANHLY_GT = "Tổng GT T.lý";
        static String COL_TONG_CONLAI_KL = "Còn lại KL";
        static String COL_TONG_CONLAI_GT = "Còn lại GT";
        //===================================
        int COLUMN_TongTLKL = -1;
        int COLUMN_TongTLGT = -1;
        int COLUMN_ConlaiKL = -1;
        int COLUMN_ConlaiGT = -1;
        int COLUMN_ThanhLy0 = 1000;
        int COLUMN_ThanhLy_Count = 0;

        Hashtable mHashColumnData = new Hashtable();
        Hashtable mHashDataColumn = new Hashtable();
        Hashtable mHashColumnToIndex = new Hashtable();

        List<String> mThanhlyKL = new List<string>(5);
        List<String> mThanhlyGT = new List<string>(5);
        //====================================

        DataTable mProjectsTB;
        DataTable mQuyetDinhTB;
        DataTable mDataSource;
        int mCachTinhChiPhi = 0;

        int mState = 0;

        bool mIsShowingDetail = false;

        public FormThongKeDuAn()
        {
            InitializeComponent();

            dataGridView1.ColumnHeadersHeight = 36;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

            treeViewProjectTasks.Hide();

            loadProjectList(null);
            btnTatCaDuAn_Click(null, null);
        }

        void loadProjectList(String startYear)
        {
            try
            {
                //  du an
                mProjectsTB = DatabaseUtils.getInstance().GetListProject3(startYear);

                List<String> projects = new List<String>();
                foreach (DataRow r in mProjectsTB.Rows)
                {
                    String name = Utils.getStringOfRow(r, 1);
                    projects.Add(name);
                }
                cb_duan.Properties.DataSource = projects;
                

                if (projects.Count > 0)
                {
                    cb_duan.EditValue = projects.FirstOrDefault();
                }
                else
                {
                    cb_duan.Text = "";
                }
                //this.cb_quyetdinh.Text = "";

                //this.comboBoxDuAn.DataSource = projects;

                //if (projects.Count > 0)
                //{
                //    this.comboBoxDuAn.SelectedIndex = 0;
                //}
                //else
                //{
                //    this.comboBoxDuAn.Text = "";
                //}
                //this.comboBoxQuyetDinh.Text = "";
            }
            catch (Exception ex)
            {
            }
        }

        String getProjectIDFromName(String name)
        {
            try
            {
                foreach (DataRow r in mProjectsTB.Rows)
                {
                    String _name = Utils.getStringOfRow(r, 1);
                    if (_name.CompareTo(name) == 0)
                    {
                        return Utils.getStringOfRow(r, 0);
                    }
                }
            }
            catch(Exception ex)
            {
            }

            return "";
        }

        int getQuyetDinhIDFromName(String name)
        {
            try
            {
                foreach (DataRow r in mQuyetDinhTB.Rows)
                {
                    String _name = (String)r["phieu_desc"];
                    if (_name.CompareTo(name) == 0)
                    {
                        return (int)r[0];
                    }
                }
            }catch(Exception ex)
            {
            }

            return 0;
        }

        private void onLoad(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string[] names = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "" };
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames = names;
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthGenitiveNames = names;
            dateStart.Properties.Mask.EditMask = "MM/yyyy";
            dateStart.Properties.Mask.UseMaskAsDisplayFormat = true;          
            dateStart.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;

            dateEnd.Properties.Mask.EditMask = "MM/yyyy";
            dateEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
            dateEnd.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;

            dateStart.EditValue = DateTime.Now;
            dateEnd.EditValue = DateTime.Now;



            //dateEnd.Properties.Mask.EditMask = "MM/yyyy";
            //dateEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
            //dateEnd.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;




            refreshForm();
        }

       

        void refreshForm()
        {
        }

        void disableSortofGridView(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void onItemDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
            }
        }

        private void buttonThongKeTheoDuAn_Click(object sender, EventArgs e)
        {
            mIsShowingDetail = false;
            String name = cb_duan.Text;
            if (name == null || name.Length == 0)
                return;

            String projectID = getProjectIDFromName(name);
            labelTitle.Text = name + ": danh sách quyết định giao việc";

            DataTable dtRaw = DatabaseUtils.getInstance().getPhieuGiaoViecOfProject(projectID);
            DataTable dt = new DataTable();
            mQuyetDinhTB = dtRaw;

            dt.Columns.Add(COL_PHIEU_ID).DataType = Type.GetType("System.Int32");
            dt.Columns.Add(COL_PROJECT_CODE).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_PHIEU_NO).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_PHIEU_NAME).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_TEAM).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_TONG_KHOAN).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_DA_THANH_TOAN).DataType = Type.GetType("System.String");

            List<String> quyetdinh = new List<string>();

            foreach (DataRow r in dtRaw.Rows)
            {
                DataRow nr = dt.NewRow();

                String phieu_no = (String)r["phieu_no"];
                if (phieu_no == null || phieu_no.Length == 0)
                    continue;

                //nr[COL_PHIEU_ID] = r["phieu_id"];
                nr[COL_PROJECT_CODE] = r["MaCT"];
                nr[COL_PHIEU_NO] = phieu_no;
                nr[COL_TEAM] = r["doi_thi_cong"];
                nr[COL_PHIEU_NAME] = r["phieu_desc"];
                quyetdinh.Add((String)r["phieu_desc"]);

                Double v1 = Utils.getBigIntValueOfRow(r, "chi_phi");
                Double v2 = Utils.getBigIntValueOfRow(r, "da_thanh_toan");

                String s1 = Utils.doubleToMoneyString(v1);
                String s2 = Utils.doubleToMoneyString(v2);

                nr[COL_TONG_KHOAN] = s1;
                nr[COL_DA_THANH_TOAN] = s2;

                dt.Rows.Add(nr);
            }

            this.cb_quyetdinh.Properties.DataSource = quyetdinh;

            //this.linkLabelProjectName.Text = "[" + mProjectID + "] " + mProjectName;
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = dt;

            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[COL_TONG_KHOAN].Width = 90;
            this.dataGridView1.Columns[COL_DA_THANH_TOAN].Width = 90;

            disableSortofGridView(dataGridView1);
            mState = STATE_THONGKE_1_DUAN;
        }

        int mPhieuID = 0;
        int getPhieuID()
        {
            return mPhieuID;
        }

        private void buttonThongkeTheoQuyetDinh_Click(object sender, EventArgs e)
        {
            mIsShowingDetail = true;
            treeViewProjectTasks.Show();
            String quyetdinh = cb_quyetdinh.Text;
            if (quyetdinh == null || quyetdinh.Length == 0)
                return;

            Cursor.Current = Cursors.WaitCursor;
            labelTitle.Text = cb_duan.Text + ": " + cb_quyetdinh.Text;

            mPhieuID = getQuyetDinhIDFromName(quyetdinh);

            initDataSourceTable();

            refreshTree();

            treeViewProjectTasks.Hide();

            Cursor.Current = Cursors.Default;
            mState = STATE_THONGKE_1_QUYETDINH;
        }

       

        void refreshTree()
        {
            DataTable dt;
            DatabaseUtils db = DatabaseUtils.getInstance();

            treeViewProjectTasks.Nodes.Clear();

            if (addRootNode())
            {
                dt = db.getJobOfProject(getPhieuID(), 0);

                treeViewProjectTasks.SelectedNode = treeViewProjectTasks.Nodes[0];

                mDataSource.Clear();
                //  add project as the first row
                DataRow r = mDataSource.NewRow();
                r[COL_JOB_LEVEL] = 0;
                r[COL_JOB_ID] = 0;
                r[COL_NOIDUNG] = treeViewProjectTasks.TopNode.Text;
                mDataSource.Rows.Add(r);

                addJobsToNode(treeViewProjectTasks.Nodes[0], dt);
            }

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;    //  disable/prevent reorder columns
            dataGridView1.DataSource = mDataSource;
            for (int i = 0; i < mThanhlyGT.Count; i++)
            {
                String n1 = mThanhlyGT[i];
                String n2 = mThanhlyKL[i];

                dataGridView1.Columns[n1].Visible = false;
                dataGridView1.Columns[n2].Visible = false;
            }
            //dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns[0].Visible = false;   //  id
            dataGridView1.Columns[2].Visible = false;   //  level
            this.disableSortofGridView(dataGridView1);

            for (int i = 0; i < mDataSource.Rows.Count; i++)
            {
                DataRow r = mDataSource.Rows[i];

                int level = 0;
                try
                {
                    level = (int)r[2];
                }
                catch (Exception e) { }
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
            dataGridView1.Columns[COL_NOIDUNG].Width = 130;
            dataGridView1.Columns[COL_DONVITINH].Width = 54;
            dataGridView1.Columns[COL_KHOILUONG].Width = 60;
            if (mCachTinhChiPhi == 0)
            {
                dataGridView1.Columns[COL_DONGIA].Width = 80;
            }else{
                dataGridView1.Columns[COL_DONGIA_CONG].Width = 80;
                dataGridView1.Columns[COL_DINHMUC].Width = 60;
            }
            dataGridView1.Columns[COL_THANHTIEN].Width = 92;

            if (COLUMN_ThanhLy0 < 10)
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

            //==============================
            updateUIThanhTien();
        }

        void updateUIThanhTien()
        {
            if (treeViewProjectTasks.Nodes.Count == 0)
                return;
//            TreeNode node = treeViewProjectTasks.TopNode;
            TreeNode node = treeViewProjectTasks.Nodes[0];// .TopNode;
            if (mDataSource.Rows.Count == 0)
                return;

            calcThanhTienOfColumn(COL_KHOILUONG, COL_THANHTIEN, node);
            calcThanhTienOfColumn(COL_TONG_THANHLY_KL, COL_TONG_THANHLY_GT, node);
            calcThanhTienOfColumn(COL_TONG_CONLAI_KL, COL_TONG_CONLAI_GT, node);

            for (int i = 0; i < mThanhlyKL.Count; i++)
            {
                String colKL = mThanhlyKL[i];
                String colGT = mThanhlyGT[i];

                calcThanhTienOfColumn(colKL, colGT, node);
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

        Double calcThanhTienOfColumn(String klCol, String thanhtienCol, TreeNode node)
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
                    if (mCachTinhChiPhi == 0)
                    {
                        String sdongia = (String)r[COL_DONGIA];
                        dongia = Utils.convertMoneyStringToDouble(sdongia);
                    }
                    else
                    {
                        String sdongia = (String)r[COL_DONGIA_CONG];
                        dongia = Utils.convertMoneyStringToDouble(sdongia);
                        String sdinhmuc = (String)r[COL_DINHMUC];
                        dongia = dongia * Utils.convertStringToDouble(sdinhmuc);
                    }

                    String skl = (String)r[klCol];
                    Double kl = Utils.convertStringToDouble(skl);

                    v = dongia * kl;
                    String s = Utils.doubleToMoneyString(v);

                    r[thanhtienCol] = s;
                }
                else
                {
                    foreach (TreeNode sub in node.Nodes)
                    {
                        v += calcThanhTienOfColumn(klCol, thanhtienCol, sub);
                    }

                    String s = Utils.doubleToMoneyString(v);
                    r[thanhtienCol] = s;
                }
            }

            return v;
        }

        bool addRootNode()
        {
            /*
            string projectName = comboBoxProjectList.SelectedItem.ToString();
            if (projectName == null || projectName.Length == 0)
                return false;
            */
            treeViewProjectTasks.Nodes.Clear();

            TreeNode root = new TreeNode() { Text = cb_duan.Text};
            root.Tag = getPhieuID();

            treeViewProjectTasks.Nodes.Add(root);

            return true;
        }

        private void theoTatcaDuAn(object sender, EventArgs e)
        {
            mIsShowingDetail = false;
            labelTitle.Text = "Danh sách các dự án";

            String year = this.comboBox_startdate.Text;
            year = year.Trim();
            DataTable dt = DatabaseUtils.getInstance().GetListProject(year, true, true);

            dataGridView1.DataSource = null;    //  disable/prevent reorder columns
            this.dataGridView1.DataSource = dt;

            if (dataGridView1.Columns.Count > 4)
            {
                this.dataGridView1.Columns[3].Width = 96;
                this.dataGridView1.Columns[4].Width = 96;

                this.dataGridView1.Columns[5].Width = 96;
                this.dataGridView1.Columns[6].Width = 96;
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;

            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            disableSortofGridView(dataGridView1);
            mState = STATE_THONGKE_CAC_DUAN;
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 9) {
                if (e.Value.ToString() == "Đã hoàn thành") {
                    e.CellStyle.BackColor = ColorTranslator.FromHtml("#d9f7be");
                }
            } 
        }

        void initDataSourceTable()
        {
            if (getPhieuID() == -1)
                return;

            mHashColumnData.Clear();
            mHashDataColumn.Clear();
            mHashColumnToIndex.Clear();

            mCachTinhChiPhi = DatabaseUtils.getInstance().getCachTinhThanhTienOfPhieu(getPhieuID());

            mDataSource = new DataTable();
            mDataSource.Columns.Add(COL_JOB_ID).DataType = Type.GetType("System.Int32");
            mDataSource.Columns.Add(COL_NOIDUNG).DataType = Type.GetType("System.String");
            mDataSource.Columns.Add(COL_JOB_LEVEL).DataType = Type.GetType("System.Int32");
            mDataSource.Columns.Add(COL_KHOKHAN).DataType = Type.GetType("System.Int32");
            mDataSource.Columns.Add(COL_DONVITINH).DataType = Type.GetType("System.String");

            if (mCachTinhChiPhi == 0)
            {
                mDataSource.Columns.Add(COL_DONGIA).DataType = Type.GetType("System.String");
                COLUMN_ThanhLy0 = 8;
            }
            else
            {
                mDataSource.Columns.Add(COL_DONGIA_CONG).DataType = Type.GetType("System.String");
                mDataSource.Columns.Add(COL_DINHMUC).DataType = Type.GetType("System.String");
                COLUMN_ThanhLy0 = 9;
            }
            mDataSource.Columns.Add(COL_KHOILUONG).DataType = Type.GetType("System.String");
            mDataSource.Columns.Add(COL_THANHTIEN).DataType = Type.GetType("System.String");
            //====================
            DatabaseUtils db = DatabaseUtils.getInstance();
            DataTable thanhtoan = db.getThanhtoanOfProjectID(getPhieuID());

            int idx = COLUMN_ThanhLy0;
            mThanhlyKL.Clear();
            mThanhlyGT.Clear();

            foreach (DataRow r in thanhtoan.Rows)
            {
                COLUMN_ThanhLy_Count++;
                idx += 2;
                int d = (int)r["month"];
                int thanhly_id = (int)r["thanhtoan_id"];

                String name = toKLThanhtoanFieldName(d);
                mDataSource.Columns.Add(name).DataType = Type.GetType("System.String");
                mDataSource.Columns[name].ReadOnly = false;
                mThanhlyKL.Add(name);

                String name2 = toThanhtoanFieldName(d);
                mDataSource.Columns.Add(name2).DataType = Type.GetType("System.String");
                mDataSource.Columns[name2].ReadOnly = false;
                mThanhlyGT.Add(name2);

                mHashColumnData.Add(name, thanhly_id);
                mHashDataColumn.Add(thanhly_id, name);
            }
            //=====================
            mDataSource.Columns.Add(COL_TONG_THANHLY_KL).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_THANHLY_KL].ReadOnly = false;
            COLUMN_TongTLKL = idx++;

            mDataSource.Columns.Add(COL_TONG_THANHLY_GT).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_THANHLY_GT].ReadOnly = false;
            COLUMN_TongTLGT = idx++;
            //=====================
            mDataSource.Columns.Add(COL_TONG_CONLAI_KL).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_CONLAI_KL].ReadOnly = false;
            COLUMN_ConlaiKL = idx++;

            mDataSource.Columns.Add(COL_TONG_CONLAI_GT).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_CONLAI_GT].ReadOnly = false;
            COLUMN_ConlaiGT = idx++;

            //=============================
            for (int i = 0; i < mDataSource.Columns.Count; i++)
            {
                String s = mDataSource.Columns[i].Caption;
                mHashColumnToIndex[s] = i;
            }
        }
        int getJobIDOfRow(int idx)
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

        DataRow getJobAsDataRow(int jobID)
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
        void addJobsToNode(TreeNode node, DataTable dt)
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

        void addRowToDataSourceList(DataRow r)
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
                jobname += " ";
            }

            jobname += (String)r["job_name"];
            drNew[COL_NOIDUNG] = jobname;

            drNew[COL_JOB_LEVEL] = r["job_level"];

            drNew[COL_DONVITINH] = r["unit_name"];

            if (level > 0)
                drNew[COL_KHOKHAN] = r["difficult"];
            Int64 calc_price = 0;
            Double dongia = 0;
            Double dinhmuc = 0;
            Double amount = 0;

            if (level == C.JOB_LEVEL_DETAIL)
            {
                try
                {
                    amount = (Double)r["unit_amount"];
                    dinhmuc = 1;
                    if (mCachTinhChiPhi == 0)
                    {
                        dongia = Utils.getDoubleValueOfRow(r, "price");
                    }
                    else
                    {
                        dongia = Utils.getBigIntValueOfRow(r, "price");
                        dinhmuc = Utils.getDoubleValueOfRow(r, "dinhmuc");
                        //dongia = dongia * dinhmuc;
                    }

                    Double total = dongia * dinhmuc * amount;
                    calc_price = (long)total;

                    //drNew[COL_THANHTIEN] = Utils.doubleToMoneyString(calc_price);
                }
                catch (Exception e2)
                {
                }

                drNew[COL_KHOILUONG] = Utils.doubleToString(amount);
                if (mCachTinhChiPhi == 0)
                {
                    drNew[COL_DONGIA] = Utils.doubleToMoneyString(dongia);
                }
                else
                {
                    drNew[COL_DONGIA_CONG] = Utils.doubleToMoneyString(dongia);
                    drNew[COL_DINHMUC] = Utils.floatToString((float)dinhmuc);
                }
                drNew[COL_THANHTIEN] = Utils.doubleToMoneyString(calc_price);

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
                                drNew[name] = Utils.doubleToString(kl);
                                kl = kl * dongia;

                                String s = Utils.doubleToMoneyString(kl);
                                drNew[name2] = s;
                            }
                        }
                    }
                    //  tinh tong thanh ly
                    totalGT = totalKL * dongia;
                    drNew[COLUMN_TongTLKL] = Utils.doubleToString(totalKL);
                    drNew[COLUMN_TongTLGT] = Utils.doubleToMoneyString(totalGT);

                    //  tinh tong con lai
                    String stongKL = (String)drNew[COL_KHOILUONG];

                    Double tongKLOfProj = Utils.convertStringToDouble(stongKL);
                    Double remainKL = tongKLOfProj - totalKL;
                    Double remainGT = remainKL * dongia;

                    drNew[COLUMN_ConlaiKL] = Utils.doubleToString(remainKL);
                    drNew[COLUMN_ConlaiGT] = Utils.doubleToMoneyString(remainGT);
                }
                catch (Exception e)
                {
                    System.Console.Out.WriteLine(e.Message);
                }
            }
            //=======================================

            mDataSource.Rows.Add(drNew);
        }
        String toKLThanhtoanFieldName(int d)
        {
            int y = d / 100;
            int m = d % 100;

            String name = "T.lý KL\n" + m + "/" + y + "";

            return name;
        }

        String toThanhtoanFieldName(int d)
        {
            int y = d / 100;
            int m = d % 100;

            String name = "Thànhtiền\n" + m + "/" + y + "";

            return name;
        }

        bool isValidThanhlyKL(String name)
        {
            for (int i = 0; i < mThanhlyKL.Count; i++)
            {
                String s = mThanhlyKL[i];
                if (s.CompareTo(name) == 0)
                    return true;
            }

            return false;
        }
        int getLevelOfRow(int idx)
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


        private void cellFormating(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!mIsShowingDetail)
                return;
            int level = getLevelOfRow(e.RowIndex);
            if (level == 0 && e.ColumnIndex < COLUMN_ThanhLy0)
            {
                e.CellStyle.BackColor = Color.DarkGreen;
                e.CellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.White;
            }
            else if (level < C.JOB_LEVEL_DETAIL && e.ColumnIndex < COLUMN_ThanhLy0)
            {
                if (level == 1)
                    e.CellStyle.BackColor = Color.LightCyan;
                else if (level == 2)
                    e.CellStyle.BackColor = Color.Cyan;
                else
                    e.CellStyle.BackColor = Color.LightYellow;

            }
            else//if (level == C.JOB_LEVEL_DETAIL)
            {
                if (e.ColumnIndex == COLUMN_TongTLKL || e.ColumnIndex == COLUMN_TongTLGT)
                {
                    e.CellStyle.BackColor = Color.Orange;
                }
                else if (e.ColumnIndex == COLUMN_ConlaiKL || e.ColumnIndex == COLUMN_ConlaiGT)
                {
                    e.CellStyle.BackColor = Color.Yellow;
                }
                else if (e.ColumnIndex >= COLUMN_ThanhLy0)
                {
                    if (COLUMN_ThanhLy_Count > 0)
                    {
                        int tmp = e.ColumnIndex - COLUMN_ThanhLy0;
                        tmp /= 2;
                        tmp = tmp % 2;
                        if (tmp == 0)
                            e.CellStyle.BackColor = Color.LightPink;
                        else
                            e.CellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private void onDuAnChanged(object sender, EventArgs e)
        {
            cb_quyetdinh.Text = "";
            String name = this.cb_duan.Text;
            if (name == null || name.Length == 0)
                return;

            String projectID = getProjectIDFromName(name);

            DataTable dtRaw = DatabaseUtils.getInstance().getPhieuGiaoViecOfProject(projectID);
            DataTable dt = new DataTable();
            mQuyetDinhTB = dtRaw;

            List<String> quyetdinh = new List<string>();

            foreach (DataRow r in dtRaw.Rows)
            {
                quyetdinh.Add((String)r["phieu_desc"]);
            }

            this.cb_quyetdinh.Properties.DataSource = quyetdinh;
        }

        private void toFileExcel_Click(object sender, EventArgs ea)
        {
            if (mState == STATE_THONGKE_CAC_DUAN)
            {
                xuatBaocaoCacDuAn();
            }
            else if (mState == STATE_THONGKE_1_DUAN)
            {
                xuatBaocao1Duan();
            }
            else if (mState == STATE_THONGKE_1_QUYETDINH)
            {
                xuatBaocao1QuyetDinh();
            }
            else
            {
                MessageBox.Show("Hãy chọn loại thống kê trước.");
            }
        }

        void xuatBaocao1QuyetDinh()
        {
            String name = this.cb_duan.Text;
            String projectID = getProjectIDFromName(name);
            if (projectID == null || projectID.Length == 0)
            {
                return;
            }
            String quyetdinh = cb_quyetdinh.Text;
            if (quyetdinh == null || quyetdinh.Length == 0)
                return;
            if (mPhieuID == 0)
                return;
            DataTable tableQuyetdinh = DatabaseUtils.getInstance().getPhieuGiaoViecOfByPhieuID(mPhieuID);
            if (tableQuyetdinh == null || tableQuyetdinh.Rows.Count == 0)
                return;
            //==========================================
            ExcelExporter excel = new ExcelExporter();
            ex.Range range;
            excel.writeLine("A1", "D1", "CÔNG TY TNHH MTV TĐBĐ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A2", "D2", "XN PHÁT TRIỂN CÔNG NGHỆ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A3", "D3", "TRẮC ĐỊA BẢN ĐỒ", 10, ExcelExporter.ALIGN_CENTER);

            excel.writeLine("E1", "H1", "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("E2", "H2", "Độc lập – Tự do – Hạnh Phúc", 10, ExcelExporter.ALIGN_CENTER, false, true);

            range = excel.writeLine("E5", "J5", "Ngày........tháng........năm........................", 10, ExcelExporter.ALIGN_RIGHT);
            range.Font.Italic = true;

            excel.writeLine("A6", "J6", "THỐNG KÊ TÌNH HÌNH THỰC HIỆN QUYẾT ĐỊNH GIAO NHIỆM VỤ", 10, ExcelExporter.ALIGN_CENTER, true, false);

            String s = "Số QĐ: " + Utils.getStringOfRow(tableQuyetdinh.Rows[0], "phieu_no") + " - " + name;
            range = excel.writeLine("A7", "J7", s, 10, ExcelExporter.ALIGN_CENTER, true, false);
            range.Font.Italic = true;

            //  columns width
            int[] cw = {5, 36, 6, 10, 10, 10, 10, 10, 10, 10};
            int i = 0;
            for (i = 0; i < cw.Length; i++)
            {
                String col = "" + (char)('A' + i) + "1";
                excel.setColumnWidth(col, cw[i]);
            }
            //  header
            int tableY = 9;
            int tableY0 = 9;
            String[] ss = { "STT", "Nội dung công việc", "Khó khăn", "Khối lượng khoán", "Giá trị khoán", "Tổng KL thanh lý", "Tổng giá trị thanh lý", "Tổng KL còn lại", "Tổng giá trị còn lại", "Ghi chú" };
            for (i = 0; i < ss.Length; i++)
            {
                char a = (char)('A' + i);
                String b = "" + a + "9";
                String e = "" + a + "11";

                excel.writeLine(b, e, ss[i], 9, ExcelExporter.ALIGN_CENTER, true, false);
            }

            range = excel.getRange("A9", "L11");
            range.WrapText = true;
            tableY += 3;

            //  fill data to the table
            DataTable dt = DatabaseUtils.getInstance().getPhieuGiaoViecOfProject(projectID);
            int cnt = dt.Rows.Count;

            i = 0;
            int tmp = 1;

            List<Int64> Tongkhoan = new List<Int64>();
            List<Int64> Tonggiatridathanhtoan = new List<Int64>();
            List<Int64> Tongconlai = new List<Int64>();

            int level;
            bool first = false;

            int[] _level = { 0, 0, 0,0};
            char[] _levelC = { 'A', 'a', '1', 'a' };

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

                excel.ap.Cells[tableY, 2] = vr.Cells[COL_NOIDUNG].Value;  //  noi dung
                excel.ap.Cells[tableY, 3] = vr.Cells[COL_KHOKHAN].Value;  //  kho khan
                excel.ap.Cells[tableY, 4] = vr.Cells[COL_KHOILUONG].Value;  //  khoi luong khoan

                s = "";
                //  tong gia tri khoan
                /*
                if (vr.Cells[7].Value != null && vr.Cells[7].Value != DBNull.Value)
                {
                    s = (String)vr.Cells[7].Value;
                    Double d = Utils.convertMoneyStringToDouble(s);
                    s = Utils.doubleToString(d);
                    excel.ap.Cells[tableY, 5] = s;
                }
                 */
                excel.ap.Cells[tableY, 5] = vr.Cells[COL_THANHTIEN].Value;
                //  thanh ly
                excel.ap.Cells[tableY, 6] = vr.Cells[COL_TONG_THANHLY_KL].Value;  //  tong KL thanh ly
                /*
                if (vr.Cells[COL_TONG_THANHLY_GT].Value != null && vr.Cells[COL_TONG_THANHLY_GT].Value != DBNull.Value)
                {
                    s = (String)vr.Cells[COL_TONG_THANHLY_GT].Value;
                    Double d = Utils.convertMoneyStringToDouble(s);
                    s = Utils.doubleToString(d);
                    excel.ap.Cells[tableY, 7] = s;
                }
                */
                excel.ap.Cells[tableY, 7] = vr.Cells[COL_TONG_THANHLY_GT].Value;
                //  con lai

                excel.ap.Cells[tableY, 8] = vr.Cells[COL_TONG_CONLAI_KL].Value;  //  tong KL con lai
                /*
                if (vr.Cells[COL_TONG_CONLAI_GT].Value != null && vr.Cells[COL_TONG_CONLAI_GT].Value != DBNull.Value)
                {
                    s = (String)vr.Cells[COL_TONG_CONLAI_GT].Value;
                    Double d = Utils.convertMoneyStringToDouble(s);
                    s = Utils.doubleToString(d);
                    excel.ap.Cells[tableY, 9] = s;
                }
                */
                excel.ap.Cells[tableY, 9] = vr.Cells[COL_TONG_CONLAI_GT].Value;
                excel.ap.Cells[tableY, 10] = "";    //  ghi chu

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
                Int64 dathanhtoan = Utils.sqlNumberToInt64(row["da_thanh_toan"]);
                excel.ap.Cells[tableY, 5] = Utils.doubleToString(chiphi);
                excel.ap.Cells[tableY, 7] = Utils.doubleToString(dathanhtoan);
                excel.ap.Cells[tableY, 9] = Utils.doubleToString(chiphi-dathanhtoan);
            }
            //======================================
            range = excel.getRange("A" + tableY0.ToString(), "J"+tableY.ToString());
            range.Font.Size = 9;
            range.WrapText = true;

            range.HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = ex.XlHAlign.xlHAlignCenter;

            range.Borders.LineStyle = ex.Constants.xlSolid;
            range.RowHeight = 20;
            //========================
            range = excel.getRange("B" + tableY0.ToString(), "B" + tableY.ToString());
            range.HorizontalAlignment = ex.XlHAlign.xlHAlignLeft;
            
            excel.completeDocument("tkquyetdinh");
        }

        void xuatBaocao1Duan()
        {
            String name = this.cb_duan.Text;
            String projectID = getProjectIDFromName(name);
            if (projectID == null || projectID.Length == 0)
            {
                return;
            }
            //==========================================
            ExcelExporter excel = new ExcelExporter();
            ex.Range range;
            excel.writeLine("A1", "C1", "CÔNG TY TNHH MTV TĐBĐ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A2", "C2", "XN PHÁT TRIỂN CÔNG NGHỆ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A3", "C3", "TRẮC ĐỊA BẢN ĐỒ", 10, ExcelExporter.ALIGN_CENTER);

            excel.writeLine("D1", "H1", "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("D2", "H2", "Độc lập – Tự do – Hạnh Phúc", 10, ExcelExporter.ALIGN_CENTER, false, true);

            range = excel.writeLine("C5", "H5", "Ngày........tháng........năm........................", 10, ExcelExporter.ALIGN_RIGHT);
            range.Font.Italic = true;

            excel.writeLine("A6", "H6", "THỐNG KÊ TÌNH HÌNH THỰC HIỆN DỰ ÁN", 10, ExcelExporter.ALIGN_CENTER, true, false);

            range = excel.writeLine("A7", "H7", name, 10, ExcelExporter.ALIGN_CENTER, true, false);
            range.Font.Italic = true;

            //  columns width
            int[] cw = {5, 36, 10, 30, 10, 10, 10, 10};
            int i = 0;
            for (i = 0; i < cw.Length; i++)
            {
                String col = "" + (char)('A' + i) + "1";
                excel.setColumnWidth(col, cw[i]);
            }
            //  header
            int tableY = 9;
            int tableY0 = 9;
            String[] ss = { "STT", "Nội dung quyết định giao nhiệm vụ", "Số quyết định", "Bộ phận thực hiện", "Giá trị khoán", "Giá trị đã thanh toán", "Giá trị còn lại", "Ghi chú" };
            for (i = 0; i < ss.Length; i++)
            {
                char a = (char)('A' + i);
                String b = "" + a + "9";
                String e = "" + a + "11";

                excel.writeLine(b, e, ss[i], 9, ExcelExporter.ALIGN_CENTER, true, false);
            }

            range = excel.getRange("A9", "L11");
            range.WrapText = true;
            tableY += 3;

            //  fill data to the table
            DataTable dt = DatabaseUtils.getInstance().getPhieuGiaoViecOfProject(projectID);
            int cnt = dt.Rows.Count;

            i = 0;
            int tmp = 1;

            List<Int64> Tongkhoan = new List<Int64>();
            List<Int64> Tonggiatridathanhtoan = new List<Int64>();
            List<Int64> Tongconlai = new List<Int64>();

            foreach (DataRow vr in dt.Rows)
            {
                excel.ap.Cells[tableY,1] = "" + tmp++;

                excel.ap.Cells[tableY, 2] = Utils.getStringOfRow(vr, "phieu_desc");  //  ten quyet dinh
                excel.ap.Cells[tableY, 3] = Utils.getStringOfRow(vr, "phieu_no");  //  so quyet dinh
                excel.ap.Cells[tableY, 4] = Utils.getStringOfRow(vr, "doi_thi_cong");    //  bo phan

                Int64 giatri = Utils.sqlNumberToInt64(vr["chi_phi"]);
                Int64 dathanhtoan = Utils.sqlNumberToInt64(vr["da_thanh_toan"]);
                Int64 conlai = giatri - dathanhtoan;

                excel.ap.Cells[tableY, 5] = Utils.doubleToString(giatri);  //  gia tri khoan
                excel.ap.Cells[tableY, 6] = Utils.doubleToString(dathanhtoan);    //  gia  tri da thanh toan
                excel.ap.Cells[tableY, 7] = Utils.doubleToString(conlai);    //  gia  tri con lai

                excel.ap.Cells[tableY, 8] = ""; //  ghi chu
                //-----------------
                Tongkhoan.Add(Utils.sqlNumberToInt64(vr[4]));
                Tonggiatridathanhtoan.Add(Utils.sqlNumberToInt64(vr[5]));
                Tongconlai.Add(conlai);
                //-----------------

                excel.ap.Cells[tableY, 11] = Utils.getStringOfRow(vr, 7);//  ghi chu

                tableY++;
            }
            //  tinh tong cong
            Int64[] _t = {0, 0, 0};
            for (i = 0; i < Tongkhoan.Count; i++)
            {
                _t[0] += Tongkhoan[i];
                _t[1] += Tonggiatridathanhtoan[i];
                _t[2] += Tongconlai[i];
            }

            excel.ap.Cells[tableY, 2] = "Tổng cộng";
            String c = "B" + tableY.ToString();
            String c2 = "H" + tableY.ToString();
            excel.getRange(c, c2).Font.Bold = true;

            excel.ap.Cells[tableY, 5] = Utils.doubleToString(_t[0]);
            excel.ap.Cells[tableY, 6] = Utils.doubleToString(_t[1]);
            excel.ap.Cells[tableY, 7] = Utils.doubleToString(_t[2]);
            //--------------------------------

            range = excel.getRange("A" + tableY0.ToString(), "H"+tableY.ToString());
            range.Font.Size = 9;
            range.WrapText = true;

            range.HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = ex.XlHAlign.xlHAlignCenter;

            range.Borders.LineStyle = ex.Constants.xlSolid;
            range.RowHeight = 20;
            
            excel.completeDocument("tk1duan");
        }

        void xuatBaocaoCacDuAn()
        {
            //==========================================
            ExcelExporter excel = new ExcelExporter();
            ex.Range range;
            excel.writeLine("A1", "D1", "CÔNG TY TNHH MTV TĐBĐ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A2", "D2", "XN PHÁT TRIỂN CÔNG NGHỆ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A3", "D3", "TRẮC ĐỊA BẢN ĐỒ", 10, ExcelExporter.ALIGN_CENTER);

            excel.writeLine("E1", "K1", "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("E2", "K2", "Độc lập – Tự do – Hạnh Phúc", 10, ExcelExporter.ALIGN_CENTER, false, true);

            range = excel.writeLine("C5", "L5", "Ngày........tháng........năm........................", 10, ExcelExporter.ALIGN_RIGHT);
            excel.writeLine("A6", "H6", "THỐNG KÊ TÌNH HÌNH THỰC HIỆN CÁC DỰ ÁN", 10, ExcelExporter.ALIGN_CENTER, true, false);

            range.Font.Italic = true;
            //  columns width
            int[] cw = {5, 33, 9, 20, 9, 9, 9, 9, 9, 9, 9};
            int i = 0;
            for (i = 0; i < cw.Length; i++)
            {
                String col = "" + (char)('A' + i) + "1";
                excel.setColumnWidth(col, cw[i]);
            }
            //  header
            int tableY = 8;
            int tableY0 = 8;
            String[] ss = { "STT", "Tên dự án", "Số hợp đống", "Chủ đầu tư", "Ngày bắt đầu", "Ngày kết thúc", "Giá trị hợp đồng", "Tổng giá trị khoán", "Giá trị đã thanh toán", "Giá trị còn lại", "Ghi chú"};
            for (i = 0; i < ss.Length; i++)
            {
                char a = (char)('A' + i);
                String b = "" + a + "8";
                String e = "" + a + "10";

                excel.writeLine(b, e, ss[i], 9, ExcelExporter.ALIGN_CENTER, true, false);
            }

            range = excel.getRange("A8", "L10");
            range.WrapText = true;
            tableY += 3;

            //  fill data to the table
            String year = this.comboBox_startdate.Text;
            year = year.Trim();
            DataTable dt = DatabaseUtils.getInstance().GetListProject(year);
            int cnt = dt.Rows.Count;

            i = 0;
            int tmp = 1;

            List<Int64> Tonggiatri = new List<Int64>();
            List<Int64> Tongkhoan = new List<Int64>();
            List<Int64> Tonggiatridathanhtoan = new List<Int64>();
            List<Int64> Tongconlai = new List<Int64>();

            foreach (DataRow vr in dt.Rows)
            {
                excel.ap.Cells[tableY,1] = "" + tmp++;    

                excel.ap.Cells[tableY, 2] = Utils.getStringOfRow(vr, 1);  //  ten du an
                excel.ap.Cells[tableY, 3] = Utils.getStringOfRow(vr, 2);  //  so hop dong
                excel.ap.Cells[tableY, 4] = Utils.getStringOfRow(vr, 6);  //  chu dau tu

                excel.ap.Cells[tableY, 5] = Utils.getStringOfRow(vr, 8);    //  ngay bat dau
                excel.ap.Cells[tableY, 6] = Utils.getStringOfRow(vr, 9);    //  ngay ket thuc

                excel.ap.Cells[tableY, 7] = Utils.doubleToString(Utils.sqlNumberToInt64(vr[3]));//  tong gia tri
                excel.ap.Cells[tableY, 8] = Utils.doubleToString(Utils.sqlNumberToInt64(vr[4]));//  tong khoan
                excel.ap.Cells[tableY, 9] = Utils.doubleToString(Utils.sqlNumberToInt64(vr[5]));//  gia tri da thanh toan

                Int64 tongkhoan = Utils.sqlNumberToInt64(vr[4]);
                Int64 giatridathanhtoan = Utils.sqlNumberToInt64(vr[5]);
                Int64 conlai = tongkhoan - giatridathanhtoan;

                excel.ap.Cells[tableY, 10] = Utils.doubleToString(conlai);   //  con lai

                //-----------------
                Tonggiatri.Add(Utils.sqlNumberToInt64(vr[3]));
                Tongkhoan.Add(Utils.sqlNumberToInt64(vr[4]));
                Tonggiatridathanhtoan.Add(Utils.sqlNumberToInt64(vr[5]));
                Tongconlai.Add(conlai);
                //-----------------

                excel.ap.Cells[tableY, 11] = Utils.getStringOfRow(vr, 7);//  ghi chu

                tableY++;
            }
            //  tinh tong cong
            Int64[] _t = {0, 0, 0, 0}; 
            for (i = 0; i < Tonggiatri.Count; i++)
            {
                _t[0] += Tonggiatri[i];
                _t[1] += Tongkhoan[i];
                _t[2] += Tonggiatridathanhtoan[i];
                _t[3] += Tongconlai[i];
            }

            excel.ap.Cells[tableY, 2] = "Tổng cộng";
            String c = "B" + tableY.ToString();
            String c2 = "K" + tableY.ToString();
            excel.getRange(c, c2).Font.Bold = true;

            excel.ap.Cells[tableY, 7] = Utils.doubleToString(_t[0]);
            excel.ap.Cells[tableY, 8] = Utils.doubleToString(_t[1]);
            excel.ap.Cells[tableY, 9] = Utils.doubleToString(_t[2]);
            excel.ap.Cells[tableY, 10] = Utils.doubleToString(_t[3]);
            //--------------------------------

            range = excel.getRange("A" + tableY0.ToString(), "K"+tableY.ToString());
            range.Font.Size = 9;
            range.WrapText = true;

            range.HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = ex.XlHAlign.xlHAlignCenter;

            range.Borders.LineStyle = ex.Constants.xlSolid;
            range.RowHeight = 20;
            
            excel.completeDocument("tkcacduan");
        }

        private void comboBox_startdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            String year = this.comboBox_startdate.Text;
            year = year.Trim();

            if (year.CompareTo("*") == 0)
            {
                year = null;
            }

            loadProjectList(year);
        }

        private void cb_duan_EditValueChanged(object sender, EventArgs e)
        {
            cb_quyetdinh.Text = "";
            String name = cb_duan.Text;
            if (name == null || name.Length == 0)
                return;

            String projectID = getProjectIDFromName(name);

            DataTable dtRaw = DatabaseUtils.getInstance().getPhieuGiaoViecOfProject(projectID);
            DataTable dt = new DataTable();
            mQuyetDinhTB = dtRaw;

            List<String> quyetdinh = new List<string>();

            foreach (DataRow r in dtRaw.Rows)
            {
                quyetdinh.Add((String)r["phieu_desc"]);
            }

            this.cb_quyetdinh.Properties.DataSource = quyetdinh;
            if (quyetdinh.Count > 0) {
                cb_quyetdinh.EditValue = quyetdinh.FirstOrDefault();
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cb_duan.Text == "") return;
            mIsShowingDetail = false;
            String name = cb_duan.Text;
            if (name == null || name.Length == 0)
                return;

            String projectID = getProjectIDFromName(name);
            labelTitle.Text = name + ": danh sách quyết định giao việc";

            DataTable dtRaw = DatabaseUtils.getInstance().getPhieuGiaoViecOfProject(projectID);
            DataTable dt = new DataTable();
            mQuyetDinhTB = dtRaw;

            dt.Columns.Add(COL_PHIEU_ID).DataType = Type.GetType("System.Int32");
            dt.Columns.Add(COL_PROJECT_CODE).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_PHIEU_NO).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_PHIEU_NAME).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_TEAM).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_TONG_KHOAN).DataType = Type.GetType("System.String");
            dt.Columns.Add(COL_DA_THANH_TOAN).DataType = Type.GetType("System.String");

            List<String> quyetdinh = new List<string>();

            foreach (DataRow r in dtRaw.Rows)
            {
                DataRow nr = dt.NewRow();

                String phieu_no = (String)r["phieu_no"];
                if (phieu_no == null || phieu_no.Length == 0)
                    continue;

                //nr[COL_PHIEU_ID] = r["phieu_id"];
                nr[COL_PROJECT_CODE] = r["MaCT"];
                nr[COL_PHIEU_NO] = phieu_no;
                nr[COL_TEAM] = r["doi_thi_cong"];
                nr[COL_PHIEU_NAME] = r["phieu_desc"];
                quyetdinh.Add((String)r["phieu_desc"]);

                Double v1 = Utils.getBigIntValueOfRow(r, "chi_phi");
                Double v2 = Utils.getBigIntValueOfRow(r, "da_thanh_toan");

                String s1 = Utils.doubleToMoneyString(v1);
                String s2 = Utils.doubleToMoneyString(v2);

                nr[COL_TONG_KHOAN] = s1;
                nr[COL_DA_THANH_TOAN] = s2;

                dt.Rows.Add(nr);
            }

            this.cb_quyetdinh.Properties.DataSource = quyetdinh;

            //this.linkLabelProjectName.Text = "[" + mProjectID + "] " + mProjectName;
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = dt;

            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[COL_TONG_KHOAN].Width = 90;
            this.dataGridView1.Columns[COL_DA_THANH_TOAN].Width = 90;

            disableSortofGridView(dataGridView1);
            mState = STATE_THONGKE_1_DUAN;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (cb_quyetdinh.Text == "")
            {
                XtraMessageBox.Show("Bạn chưa chọn quyết định.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mIsShowingDetail = true;
            treeViewProjectTasks.Show();
            String quyetdinh = cb_quyetdinh.Text;
            if (quyetdinh == null || quyetdinh.Length == 0)
                return;

            Cursor.Current = Cursors.WaitCursor;
            labelTitle.Text = cb_duan.Text + ": " + cb_quyetdinh.Text;

            mPhieuID = getQuyetDinhIDFromName(quyetdinh);

            initDataSourceTable();

            refreshTree();

            treeViewProjectTasks.Hide();

            Cursor.Current = Cursors.Default;
            mState = STATE_THONGKE_1_QUYETDINH;
        }

        private void btnTatCaDuAn_Click(object sender, EventArgs e)
        {
            mIsShowingDetail = false;
            labelTitle.Text = "Danh sách các dự án";

            String year = this.comboBox_startdate.Text;
            year = year.Trim();
            DataTable dt = DatabaseUtils.getInstance().GetListProject(year, true, true);

            dataGridView1.DataSource = null;    //  disable/prevent reorder columns
            this.dataGridView1.DataSource = dt;

            if (dataGridView1.Columns.Count > 4)
            {
                this.dataGridView1.Columns[3].Width = 96;
                this.dataGridView1.Columns[4].Width = 96;

                this.dataGridView1.Columns[5].Width = 96;
                this.dataGridView1.Columns[6].Width = 96;
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;

            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            disableSortofGridView(dataGridView1);
            mState = STATE_THONGKE_CAC_DUAN;
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (mState == STATE_THONGKE_CAC_DUAN)
            {
                xuatBaocaoCacDuAn();
            }
            else if (mState == STATE_THONGKE_1_DUAN)
            {
                xuatBaocao1Duan();
            }
            else if (mState == STATE_THONGKE_1_QUYETDINH)
            {
                xuatBaocao1QuyetDinh();
            }
            else
            {
                MessageBox.Show("Hãy chọn loại thống kê trước.");
            }
        }

        private void btnSearchRangeDate_Click(object sender, EventArgs e)
        {
            if (cb_quyetdinh.Text == "") {
                XtraMessageBox.Show("Bạn chưa chọn quyết định.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);  
                return;
            }
            var startDate = new DateTime(Convert.ToDateTime(dateStart.EditValue).Year, Convert.ToDateTime(dateStart.EditValue).Month, 1);
            var endDate = new DateTime(Convert.ToDateTime(dateEnd.EditValue).Year, Convert.ToDateTime(dateEnd.EditValue).Month, 1);
            var sothang = endDate.Subtract(startDate).TotalDays;
            if (sothang < 0) {
                XtraMessageBox.Show("Ngày bắt đầu phải < hơn ngày kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // xử lý get số lượng còn lại\\
            simpleButton2_Click(null, null);
            var table_temp = mDataSource.Copy();

            // end get sluong conlai


            mIsShowingDetail = true;
            treeViewProjectTasks.Show();
            String quyetdinh = cb_quyetdinh.Text;
            if (quyetdinh == null || quyetdinh.Length == 0)
                return;

            Cursor.Current = Cursors.WaitCursor;
            labelTitle.Text = cb_duan.Text + ": " + cb_quyetdinh.Text;

            mPhieuID = getQuyetDinhIDFromName(quyetdinh);

            initDataSourceTable2();

            refreshTree();

            treeViewProjectTasks.Hide();

            var dataMain = dataGridView1.DataSource as DataTable;
            //dataGridView1.Columns[COLUMN_ConlaiKL].Width = 60;
            //dataGridView1.Columns[COLUMN_ConlaiGT].Width = 90;

            for (int i = 0; i < dataMain.Rows.Count; i++)
            {
                //Lấy giá trị từ table1 tương ứng với hàng hiện tại trong table2
                var conlaiKL = table_temp.Rows[i][COL_TONG_CONLAI_KL];
                var conlaiGT = table_temp.Rows[i][COL_TONG_CONLAI_GT];

                //Gán giá trị từ table1 vào table2
                dataMain.Rows[i][COL_TONG_CONLAI_KL] = conlaiKL;
                dataMain.Rows[i][COL_TONG_CONLAI_GT] = conlaiGT;
            }
            dataGridView1.DataSource = dataMain;


            Cursor.Current = Cursors.Default;
            mState = STATE_THONGKE_1_QUYETDINH;
        }

       

        public List<string> GetMonthsInRange(DateTime startDate, DateTime endDate)
        {
            List<string> months = new List<string>();

            for (DateTime date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                months.Add(date.ToString("yyyyMM"));
            }

            return months;
        }

        void initDataSourceTable2()
        {
            if (getPhieuID() == -1)
                return;

            mHashColumnData.Clear();
            mHashDataColumn.Clear();
            mHashColumnToIndex.Clear();
       
            var listMonth = GetMonthsInRange(Convert.ToDateTime(dateStart.EditValue), Convert.ToDateTime(dateEnd.EditValue));
            var listMonString =  "'" + string.Join("','", listMonth) + "'";


            mCachTinhChiPhi = DatabaseUtils.getInstance().getCachTinhThanhTienOfPhieu(getPhieuID());

            mDataSource = new DataTable();
            mDataSource.Columns.Add(COL_JOB_ID).DataType = Type.GetType("System.Int32");
            mDataSource.Columns.Add(COL_NOIDUNG).DataType = Type.GetType("System.String");
            mDataSource.Columns.Add(COL_JOB_LEVEL).DataType = Type.GetType("System.Int32");
            mDataSource.Columns.Add(COL_KHOKHAN).DataType = Type.GetType("System.Int32");
            mDataSource.Columns.Add(COL_DONVITINH).DataType = Type.GetType("System.String");

            if (mCachTinhChiPhi == 0)
            {
                mDataSource.Columns.Add(COL_DONGIA).DataType = Type.GetType("System.String");
                COLUMN_ThanhLy0 = 8;
            }
            else
            {
                mDataSource.Columns.Add(COL_DONGIA_CONG).DataType = Type.GetType("System.String");
                mDataSource.Columns.Add(COL_DINHMUC).DataType = Type.GetType("System.String");
                COLUMN_ThanhLy0 = 9;
            }
            mDataSource.Columns.Add(COL_KHOILUONG).DataType = Type.GetType("System.String");
            mDataSource.Columns.Add(COL_THANHTIEN).DataType = Type.GetType("System.String");
            //====================
            DatabaseUtils db = DatabaseUtils.getInstance();
            DataTable thanhtoan = db.getThanhtoanOfProjectID2(getPhieuID(), listMonString);

            

            int idx = COLUMN_ThanhLy0;
            mThanhlyKL.Clear();
            mThanhlyGT.Clear();

            foreach (DataRow r in thanhtoan.Rows)
            {
                COLUMN_ThanhLy_Count++;
                idx += 2;
                int d = (int)r["month"];
                int thanhly_id = (int)r["thanhtoan_id"];

                String name = toKLThanhtoanFieldName(d);
                mDataSource.Columns.Add(name).DataType = Type.GetType("System.String");
                mDataSource.Columns[name].ReadOnly = false;
                mThanhlyKL.Add(name);

                String name2 = toThanhtoanFieldName(d);
                mDataSource.Columns.Add(name2).DataType = Type.GetType("System.String");
                mDataSource.Columns[name2].ReadOnly = false;
                mThanhlyGT.Add(name2);

                mHashColumnData.Add(name, thanhly_id);
                mHashDataColumn.Add(thanhly_id, name);
            }
            //=====================
            mDataSource.Columns.Add(COL_TONG_THANHLY_KL).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_THANHLY_KL].ReadOnly = false;
            COLUMN_TongTLKL = idx++;

            mDataSource.Columns.Add(COL_TONG_THANHLY_GT).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_THANHLY_GT].ReadOnly = false;
            COLUMN_TongTLGT = idx++;
            //=====================
            mDataSource.Columns.Add(COL_TONG_CONLAI_KL).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_CONLAI_KL].ReadOnly = false;
            COLUMN_ConlaiKL = idx++;

            mDataSource.Columns.Add(COL_TONG_CONLAI_GT).DataType = Type.GetType("System.String");
            mDataSource.Columns[COL_TONG_CONLAI_GT].ReadOnly = false;
            COLUMN_ConlaiGT = idx++;

            //=============================
            for (int i = 0; i < mDataSource.Columns.Count; i++)
            {
                String s = mDataSource.Columns[i].Caption;
                mHashColumnToIndex[s] = i;
            }
        }
    }
}
