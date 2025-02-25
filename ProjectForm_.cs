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


namespace ProjectStorage
{
    public partial class ProjectForm : Office2007Form
    {
        static String COL_JOB_ID = "job_id";
        static String COL_NOIDUNG = "Nội dung công việc";
        static String COL_JOB_LEVEL = "job_level";
        static String COL_KHOKHAN = "Mức KK";
        static String COL_KHOILUONG = "Khối lượng";
        static String COL_DONGIA = "Đơn giá";
        static String COL_DONVITINH = "Đơn vị tính";
        static String COL_THANHTIEN = "Thành tiền";
        static String COL_TONG_THANHLY_KL = "Tổng TLKL";
        static String COL_TONG_THANHLY_GT = "Tổng TLGT";
        static String COL_TONG_CONLAI_KL = "Còn lại KL";
        static String COL_TONG_CONLAI_GT = "Còn lại GT";

        public ProjectForm()
        {
            InitializeComponent();
        }

        DataTable mProjectList;
        DataTable mAllJobs;
        int COLUMN_TongTLKL = -1;
        int COLUMN_TongTLGT = -1;
        int COLUMN_ConlaiKL = -1;
        int COLUMN_ConlaiGT = -1;
        int COLUMN_ThanhLy0 = 1000;

        Hashtable mHashColumnData = new Hashtable();

        private void onLoad(object sender, EventArgs e)
        {
            initProjectList();
        }

        void initProjectList()
        {
            DatabaseUtils db = new DatabaseUtils();

            DataTable tb = db.GetListProject();
            mProjectList = tb;

            this.comboBoxProjectList.Items.Clear();
            if (tb != null)
            {
                foreach (DataRow dr in tb.Rows)
                {
                    String s = (String)dr["TenCT"];

                    this.comboBoxProjectList.Items.Add(s);
                }
            }
        }

        void initAllJobsTable()
        {
            if (getProjectID() == -1)
                return;

            mHashColumnData.Clear();

            mAllJobs = new DataTable();
            mAllJobs.Columns.Add(COL_JOB_ID).DataType = Type.GetType("System.Int32");
            mAllJobs.Columns.Add(COL_NOIDUNG).DataType = Type.GetType("System.String");
            mAllJobs.Columns.Add(COL_JOB_LEVEL).DataType = Type.GetType("System.Int32");
            mAllJobs.Columns.Add(COL_KHOKHAN).DataType = Type.GetType("System.Int32");
            mAllJobs.Columns.Add(COL_KHOILUONG).DataType = Type.GetType("System.String");
            mAllJobs.Columns.Add(COL_DONGIA).DataType = Type.GetType("System.String");
            mAllJobs.Columns.Add(COL_DONVITINH).DataType = Type.GetType("System.String");
            mAllJobs.Columns.Add(COL_THANHTIEN).DataType = Type.GetType("System.String");

            foreach (System.Data.DataColumn dc in mAllJobs.Columns)
            {
                dc.ReadOnly = true;
            }
            //====================
            DatabaseUtils db = new DatabaseUtils();
            DataTable thanhtoan = db.getThanhtoanOfProjectID(getProjectID());

            COLUMN_ThanhLy0 = 8;
            int idx = COLUMN_ThanhLy0;
            foreach (DataRow r in thanhtoan.Rows)
            {
                idx += 2;
                DateTime d = (DateTime)r["month"];
                int thanhly_id = (int)r["thanhtoan_id"];

                String name = toKLThanhtoanFieldName(d);
                mAllJobs.Columns.Add(name).DataType = Type.GetType("System.String");
                mAllJobs.Columns[name].ReadOnly = false;

                String name2 = toThanhtoanFieldName(d);
                mAllJobs.Columns.Add(name2).DataType = Type.GetType("System.String");
                mAllJobs.Columns[name2].ReadOnly = true;

                mHashColumnData.Add(name, thanhly_id);
            }
            //=====================
            mAllJobs.Columns.Add(COL_TONG_THANHLY_KL).DataType = Type.GetType("System.Double");
            mAllJobs.Columns[COL_TONG_THANHLY_KL].ReadOnly = true;
            COLUMN_TongTLKL = idx++;

            mAllJobs.Columns.Add(COL_TONG_THANHLY_GT).DataType = Type.GetType("System.Int64");
            mAllJobs.Columns[COL_TONG_THANHLY_GT].ReadOnly = true;
            COLUMN_TongTLGT = idx++;
            //=====================
            mAllJobs.Columns.Add(COL_TONG_CONLAI_KL).DataType = Type.GetType("System.Double");
            mAllJobs.Columns[COL_TONG_CONLAI_KL].ReadOnly = true;
            COLUMN_ConlaiKL = idx++;

            mAllJobs.Columns.Add(COL_TONG_CONLAI_GT).DataType = Type.GetType("System.Int64");
            mAllJobs.Columns[COL_TONG_CONLAI_GT].ReadOnly = true;
            COLUMN_ConlaiGT = idx++;
        }

        private void addProjectClick(object sender, EventArgs e)
        {
            FormAddProject form = new FormAddProject();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String maCT = form.textBoxProjectCode.Text;
                string name = form.getProjectName();
                string desc = form.getProjectDescription();
                String team = form.textBoxProjectCode.Text;

                //Database: insert here
                DatabaseUtils db = new DatabaseUtils();
                db.insertProject(maCT, name, desc, team);

                initProjectList();
            }
        }

        private void onSelectProject(object sender, EventArgs e)
        {
            initAllJobsTable();

            addRootNode();

            refreshTree();
        }

        bool addRootNode()
        {
            string projectName = comboBoxProjectList.SelectedItem.ToString();
            if (projectName == null || projectName.Length == 0)
                return false;

            treeViewProjectTasks.Nodes.Clear();

            TreeNode root = new TreeNode() { Text = projectName };
            root.Tag = getProjectID();
            treeViewProjectTasks.Nodes.Add(root);

            return true;
        }

        int getProjectID()
        {
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
        }

        int getCurrentNodeID()
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
            FormAddTask form = new FormAddTask();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String task = form.getTaskName();

                //  insert new node to database
                DatabaseUtils db = new DatabaseUtils();
                int projectID = getProjectID();
                int job_parent_id = (int)treeViewProjectTasks.SelectedNode.Tag;
                if (projectID != -1)
                {
                    if (isRootSelected())
                        job_parent_id = 0;

                    db.insertJobToProject(projectID, task, job_parent_id);
                }

                TreeNode newNode = new TreeNode() { Text = task };
                newNode.Tag = 123;  //  database row id
                treeViewProjectTasks.SelectedNode.Nodes.Add(newNode);

                refreshTree();
            }
        }

        void refreshTree()
        {
            DataTable dt;
            DatabaseUtils db = new DatabaseUtils();

            treeViewProjectTasks.Nodes.Clear();

            if (addRootNode())
            {
                dt = db.getJobOfProject(getProjectID(), 0);
                treeViewProjectTasks.SelectedNode = treeViewProjectTasks.Nodes[0];

                mAllJobs.Clear();

                addJobsToNode(treeViewProjectTasks.Nodes[0], dt);
            }

            //DataTable all = db.getAllJobsOfProject(getProjectID());
            dataGridView1.DataSource = mAllJobs;
            dataGridView1.Columns[0].Visible = false;   //  id
            dataGridView1.Columns[2].Visible = false;   //  level

            for (int i = 0; i < mAllJobs.Rows.Count; i++)
            {
                DataRow r = mAllJobs.Rows[i];
                int level = 0;
                try {
                    level = (int)r[2];
                }catch(Exception e){}
                if (level == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                    dataGridView1.Rows[i].DefaultCellStyle.Font = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);
                }
            }

            //mAllJobs = all;
        }

        DataRow getJobAsDataRow(int jobID)
        {
            if (mAllJobs == null)
                return null;

            try
            {
                foreach (DataRow r in mAllJobs.Rows)
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
                addRowToAllJobList(r);

                int id = (int)r["job_id"];
                String name = (String)r["job_name"];

                TreeNode n = new TreeNode() { Text = name};
                n.Tag = id;

                node.Nodes.Add(n);

                DatabaseUtils db = new DatabaseUtils();
                DataTable tt = db.getJobOfProject(getProjectID(), id);
                if (tt != null && tt.Rows.Count > 0)
                {
                    addJobsToNode(n, tt);
                }
            }
        }

        void addRowToAllJobList(DataRow r)
        {
            var drNew = mAllJobs.NewRow();
            drNew[COL_JOB_ID] = r["job_id"];
            drNew[COL_NOIDUNG] = r["job_name"];
            drNew[COL_JOB_LEVEL] = r["job_level"];
            int level = (int)r["job_level"];
            drNew[COL_DONVITINH] = r["unit_name"];

            if (level > 0)
                drNew[COL_KHOKHAN] = r["difficult"];
            Int64 calc_price = 0;
            Int64 dongia = 0;
            Double amount = 0;

            if (level > 0)
            {
                try
                {
                    amount = (Double)r["unit_amount"];

                    dongia = (Int64)r["price"];

                    Double total = dongia * amount;
                    calc_price = (long)total;
                }
                catch (Exception e2)
                {
                }

                drNew[COL_KHOILUONG] = amount.ToString("0.00");

                drNew[COL_DONGIA] = formatMoney(dongia);

                drNew[COL_THANHTIEN] = formatMoney(calc_price);

                //  thanh toan hang thang
                try
                {
                    int job_id = (int)r["job_id"];
                    DatabaseUtils db = new DatabaseUtils();
                    DataTable dt = db.getThanhToanHangThangOfJobID(job_id);

                    if (dt != null)
                    {
                        foreach (DataRow tt in dt.Rows)
                        {
                            updatePresentThanhtoan(tt, drNew, dongia);
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
            //=======================================

            mAllJobs.Rows.Add(drNew);
        }

        void updatePresentThanhtoan(DataRow dbRow, DataRow uiRow, Int64 dongia)
        {
            try
            {
                DateTime d = (DateTime)dbRow["month"];
                String name = toKLThanhtoanFieldName(d);
                String name2 = toThanhtoanFieldName(d);

                Double kl = (Double)dbRow["khoiluong"];
                uiRow[name] = kl.ToString("0.00");
                kl = kl * dongia;

                String s = formatMoney(kl);
                uiRow[name2] = s;
            }
            catch (Exception e)
            {
            }
        }

        String toKLThanhtoanFieldName(DateTime d)
        {
            String sd = d.ToString("MM/yy");
            String name = "KL (" + sd + ")";

            return name;
        }

        String toThanhtoanFieldName(DateTime d)
        {
            String sd = d.ToString("MM/yy");
            String name = "TT (" + sd + ")";

            return name;
        }

        private void menuAddTaskDetail(object sender, EventArgs e)
        {
            FormTaskDetail form = new FormTaskDetail();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //  insert mota, mucKK, khoiluong, dongia, thanhtien vao database
                DatabaseUtils db = new DatabaseUtils();

                int projectID = getProjectID();
                int parentID = getCurrentNodeID();

                if (parentID == -1 || isRootSelected())
                {
                    MessageBox.Show("Bạn hãy chọn mục để thêm công việc cụ thể!", "Lỗi", MessageBoxButtons.OK);
                    return;
                }

                String jobname = form.mota;
                String donvitinh = form.donvitinh;
                int kk = form.mucKK;
                float kl = form.khoiluong;
                Int64 dongia = (Int64)form.dongia;

                db.insertJobToProject(projectID, parentID, jobname, donvitinh, kk, kl, dongia);

                refreshTree();
            }
        }

        private void onNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripThem.Show(treeViewProjectTasks, e.Location);
            }
        }

        bool isRootSelected()
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

        private void removeTask(object sender, EventArgs e)
        {
            if (isRootSelected())
                return;

            TreeNode node = treeViewProjectTasks.SelectedNode;
            String msg = "Xóa công việc: " + node.Text;
            if (MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OKCancel)
                == System.Windows.Forms.DialogResult.OK)
            {
                int rowID = (int)node.Tag;
                treeViewProjectTasks.Nodes.Remove(node);

                //  remove row from database with rowID
                DatabaseUtils db = new DatabaseUtils();
                db.removeJob(getProjectID(), rowID);

                refreshTree();
            }
        }

        private void modifyJob(object sender, EventArgs e)
        {
            if (isRootSelected())
                return;

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
                    FormAddTask form = new FormAddTask();
                    form.textBoxTask.Text = name;

                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DatabaseUtils db = new DatabaseUtils();
                        db.updateJobToProject(jobID, form.textBoxTask.Text, 0);

                        refreshTree();
                    }
                }
                else
                {
                    String name = (String)r[1];
                    FormTaskDetail form = new FormTaskDetail();
                    form.textBoxTaskKL.ReadOnly = true;
                    form.textBoxTaskPriceUnit.ReadOnly = true;
                    form.textBoxDonViTinh.ReadOnly = true;

                    form.textBoxTaskDetail.Text = name;
                    try
                    {
                        form.mucKK = (int)r[3];
                    }
                    catch (Exception ex) { }
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DatabaseUtils db = new DatabaseUtils();
                        db.updateJobToProject(jobID, form.textBoxTaskDetail.Text, form.mucKK);

                        refreshTree();
                    }
                }
            }
        }

        public void insertThanhToanHangThang()
        {
            DateTime d = DateTime.Now;

            FormThanhToanHangThang form = new FormThanhToanHangThang();
            form.textBoxMonth.Text = d.ToString("MM/yyyy");
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String s = form.textBoxMonth.Text;

                try
                {
                    string[] arr = s.Split(new char[]{'/'});
                    int m = Int32.Parse(arr[0]);
                    int y = Int32.Parse(arr[1]);
                    d = new DateTime(y, m, 1);

                    DatabaseUtils db = new DatabaseUtils();
                    db.insertThanhtoanHangThang(getProjectID(), d);
                }catch(Exception ex)
                {
                    MessageBox.Show("Lỗi nhập sai tháng/năm");
                }
            }
        }

        private void cellFormating(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == COLUMN_TongTLKL || e.ColumnIndex == COLUMN_TongTLGT)
                    e.CellStyle.BackColor = Color.Orange;
            else if (e.ColumnIndex == COLUMN_ConlaiKL || e.ColumnIndex == COLUMN_ConlaiGT)
                    e.CellStyle.BackColor = Color.Yellow;
            else if (e.ColumnIndex >= COLUMN_ThanhLy0)
            {
                e.CellStyle.BackColor = Color.LightCyan;
            }
        }

        Double convertStringToDouble(String s)
        {
            if (s == null || s.Length == 0)
                return 0;

            s = s.Replace(".", ",");

            try
            {
                return Double.Parse(s);
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        private void cellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;

            try
            {
                DataColumn dc = mAllJobs.Columns[col];
                String name = dc.Caption;
                int thanhtoan_id = (int)mHashColumnData[name];

                Double kl = 0;
                String s = (String)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                kl = convertStringToDouble(s);

                DataRow r = mAllJobs.Rows[row];
                int job_id = (int)r[COL_JOB_ID];
                String sdongia = (String)r[COL_DONGIA];
                Double dongia = Double.Parse(sdongia);

                //double tien = kl * dongia;
                DatabaseUtils db = new DatabaseUtils();
                db.updateThanhToanHangThangOfJob(job_id, thanhtoan_id, (float)kl);

                //  refresh
                DataTable tb = db.getThanhToanHangThangOfJobID(job_id);
                if (db != null)
                {
                    foreach (DataRow dbRow in tb.Rows)
                    {
                        DateTime d = (DateTime)dbRow["month"];
                        String name2 = toThanhtoanFieldName(d);

                        kl = (Double)dbRow["khoiluong"];
                        r[name] = kl.ToString("0.00");

                        dataGridView1[e.ColumnIndex, e.RowIndex].Value = r[name];
                        //===============
                        kl = kl * dongia;
                        s = formatMoney(kl);

                        mAllJobs.Columns[e.ColumnIndex + 1].ReadOnly = false;
                        r[name2] = s;
                        //mAllJobs.Columns[e.ColumnIndex + 1].ReadOnly = true;
                        dataGridView1[e.ColumnIndex+1, e.RowIndex].Value = r[name2];
                        //================
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        String formatMoney(Double m)
        {
            try
            {
                String s = m.ToString("0,0");
                return s;
            }
            catch (Exception e)
            {
            }

            return "";
        }
    }
}