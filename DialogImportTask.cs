using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;

using ExcelTools = Microsoft.Office;
using ex = Microsoft.Office.Interop.Excel;
using DevExpress.XtraEditors;
using DevComponents.DotNetBar.Controls;

namespace ProjectStorage
{
    public partial class DialogImportTask : XtraForm
    {
        const String noidung = "noidung";
        const String donvitinh = "donvitinh";
        const String khokhan = "kk";
        const String khoiluong = "khoiluong";
        const String dinhmuc = "dinhmuc";
        const String dongia = "dongia";
        const String dinhbien = "dinhbien";
        const String thutu = "thutu";
        const String ghichu = "ghichu";
        const String thanhtien = "thanhtien";

        const int TT_LAMA = 1;
        const int TT_UPPER_ABC = 2;
        const int TT_lower_abc = 3;
        const int TT_number = 4;

        String mMaCT;

        List<ImportDuAnElement> mTasks = new List<ImportDuAnElement>(100);

        DataTable mProjectsTB;
        public DialogImportTask(String mact)
        {
            InitializeComponent();

            mMaCT = mact;
        }

        class ImportDuAnElement
        {
            public String noidung;
            public int level;
            public bool isLeave;
            public String TT;
            public String donvitinh;
            public String mucKK;
            public double khoiluong;
            public double dongia;
            public double dinhmuc;
            public double dinhbien;
            public string thutu;
            public string ghichu;
            public int seq;
            public double thanhtien;
            public int job_id;
        }

        String getStringOfCell(ex.Worksheet sheet, String cell)
        {
            try
            {
                ex.Range r = sheet.get_Range(cell);
                return r.Text;
            }
            catch (Exception ex)
            {
            }

            return "";
        }

        double getDoubleOfCell(ex.Worksheet sheet, String cell)
        {
            try
            {
                ex.Range r = sheet.get_Range(cell);

                String s = r.Text;
//                s = s.Replace(".", "");
                double d = Utils.convertStringToDouble(s);
                return d;
            }
            catch (Exception ex)
            {
            }

            return 0;
        }

        private void onLoad(object sender, EventArgs e)
        {
            initGrid();

            DatabaseUtils db = DatabaseUtils.getInstance();
            mProjectsTB = db.GetListProject3(null);

            List<String> projects = new List<String>();

            String selectedPro = "";
            foreach (DataRow r in mProjectsTB.Rows)
            {
                String name = Utils.getStringOfRow(r, 1);
                String mact = Utils.getStringOfRow(r, 0);

                if (mMaCT != null)
                {
                    if (mMaCT.CompareTo(mact) == 0)
                    {
                        selectedPro = name;
                    }
                }

                projects.Add(name);
            }

            this.comboBoxProjectList.DataSource = projects;
            this.comboBoxProjectList.Text = selectedPro;

            this.comboBoxTeamlist.DataSource = db.getTeamList();
        }

        void initGrid()
        {
            //==============================================
            this.dataGridView1.Columns.Clear();

            this.dataGridView1.Columns.Add("TT", "TT");
            this.dataGridView1.Columns.Add(noidung, "Nội dung công việc");
            this.dataGridView1.Columns.Add(donvitinh, "ĐVT");
            this.dataGridView1.Columns.Add(khokhan, "KK");
            this.dataGridView1.Columns.Add(dinhbien, "Định biên");
            this.dataGridView1.Columns.Add(dinhmuc, "Định mức");
            this.dataGridView1.Columns.Add(dongia, "Đơn giá");
            this.dataGridView1.Columns.Add(khoiluong, "Khối lượng");

            //if (checkBox1.Checked)
            //{
           // }

            this.dataGridView1.Columns.Add(thanhtien, "Thành tiền");
            this.dataGridView1.Columns.Add(ghichu, "Ghi chú");
            this.dataGridView1.Columns[0].Width = 40;
            this.dataGridView1.Columns[1].Width = 250;
            this.dataGridView1.Columns[3].Width = 40;
            this.dataGridView1.Columns[2].Width = 50;
            this.dataGridView1.Columns[7].Width = 70;
            /*
            this.dataGridView1.DataSource = tb;

            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[1].Width = 50;
            this.dataGridView1.Columns[2].Width = 150;
            this.dataGridView1.Columns[3].Width = 150;

            dataGridView1.ColumnHeadersHeight = 36;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

            this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            disableSortofGridView(this.dataGridView1);
             */
        }

        void disableSortofGridView(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void cellMouseClick(object sender, MouseEventArgs e)
        {
        }

        private void onItemClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private async void buttonOpenFile_Click(object sender, EventArgs e)
        {            
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel files (*.xls)|*.xls";
            DialogResult result = dlg.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                this.textBoxExcelFile.Text = dlg.FileName;

               await CalcStartEndData(dlg.FileName);

                buttonStartProcess_Click(null, null);
            }
        }
        public ExcelHelper _excelHelper = new ExcelHelper();
        private async Task CalcStartEndData(string fileName)
        {
            var data = await _excelHelper.ImportExcelToDataTableAsync(fileName);
            int startRow = 0;
            int endRow = 0;
            var dataTable = data.DATA.Tables[0];

            for (int i = 0; i <= 1000; i++)
            {
                var text = dataTable.Rows[i][1].ToString().Trim();

                if (text == "(2)")
                {
                    startRow = i + 2;
                }

                if (text == "Tổng cộng (I+II+III)")
                {
                    endRow = i + 1;
                    break;
                }
            }

            textBoxTableFromRow.Text = startRow.ToString();
            textBoxTableToRow.Text = endRow.ToString();
        }


        private void buttonStartProcess_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            processExcel();
            Cursor.Current = Cursors.Default;
        }       

        void importCongviecSheet(ex.Worksheet sheet)
        {
            ex.Range r = null;

            int row = 0;
            int rowLast = 0;
            try
            {
                row = Int32.Parse(this.textBoxTableFromRow.Text);
                row++;  //  skip header
                rowLast = Int32.Parse(this.textBoxTableToRow.Text);
            }
            catch
            {
            }

            if (rowLast <= row)
            {
                MessageBox.Show("Lỗi tọa độ bảng dữ liệu!");
                return;
            }


            //==============================================
            int[] levels_title = { 0, 0, 0, 0 };
            int[] levels = { 0, 0, 0, 0 };
            List<ImportDuAnElement> tasks = mTasks;
            tasks.Clear();

            while (row <= rowLast)
            {
                ImportDuAnElement e = new ImportDuAnElement();

                String tt = "A" + row;
                tt = getStringOfCell(sheet, tt);
                e.TT = tt;
                e.thutu = tt;

                //  ten du an
                String cell = "B" + row;
                e.noidung = getStringOfCell(sheet, cell);

                if (e.noidung == null || e.noidung.Length == 0)
                {
                    e.noidung = "-";
                }

                //  donvitinh
                cell = "C" + row;
                e.donvitinh = getStringOfCell(sheet, cell);

                //  mucKK
                cell = "D" + row;
                e.mucKK = getStringOfCell(sheet, cell);

                //  dinh bien
                cell = "E" + row;
                e.dinhbien = getDoubleOfCell(sheet, cell);

                //  dinhmuc
                cell = "F" + row;
                e.dinhmuc = getDoubleOfCell(sheet, cell);

                //  dongia
                cell = "G" + row;
                e.dongia = getDoubleOfCell(sheet, cell);

                //  khoi luong
                cell = "H" + row;
                e.khoiluong = getDoubleOfCell(sheet, cell);

                //  ghi chu
                cell = "J" + row;
                e.ghichu = getStringOfCell(sheet, cell);

                String sthanhtien = "I";
                //if (this.checkBox1.Checked)
                //{
                //    cell = "G" + row;
                //    e.dinhmuc = getDoubleOfCell(sheet, cell);

                //    sthanhtien = "H";
                //}

                //  thanhtien
                cell = sthanhtien + row;
                e.thanhtien = getDoubleOfCell(sheet, cell);
                //============================
                if (e.dongia > 0)
                    e.level = C.JOB_LEVEL_DETAIL;
                int levelTitle = getLevelOfString(tt);
                if (levelTitle == TT_UPPER_ABC || levelTitle == TT_lower_abc || levelTitle == TT_LAMA)
                {
                    e.dongia = 0;
                }

                if (levelTitle != C.JOB_LEVEL_DETAIL && e.dongia == 0)
                {
                    for (int i = 0; i < levels.Length; i++)
                    {
                        if (levels_title[i] == 0)
                        {
                            levels_title[i] = levelTitle;
                            if (i > 0)
                                levels[i] = levels[i - 1] + 1;
                            else
                                levels[i] = 1;

                        }
                        if (levels_title[i] == levelTitle)
                        {
                            e.level = levels[i];
                            break;
                        }
                    }
                }
                else
                {
                    e.level = C.JOB_LEVEL_DETAIL;
                }
                //============================
                tasks.Add(e);
                row++;
            }

            //==========================================================
            foreach (ImportDuAnElement e in tasks)
            {
                //============================
                int index = this.dataGridView1.Rows.Add(1);
                DataGridViewRow re = this.dataGridView1.Rows[index];
                re.Cells[0].Value = e.TT;

                String tmp = "";
                for (int i = 0; i < e.level; i++)
                    tmp += " ";

                re.Cells[1].Value = tmp + e.noidung;
                re.Cells[2].Value = e.donvitinh;
                re.Cells[3].Value = e.mucKK;
                if (e.dinhbien != 0)
                    re.Cells[4].Value = e.dinhbien;

                if (e.dinhmuc != 0)
                    re.Cells[5].Value = e.dinhmuc;
                if (e.dongia != 0)
                    re.Cells[6].Value = Utils.doubleToMoneyString(e.dongia);

                if (e.khoiluong != 0)
                    re.Cells[7].Value = e.khoiluong;

                re.Cells[8].Value = Utils.doubleToMoneyString(e.thanhtien);
                re.Cells[9].Value = e.ghichu;
            }

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        void processExcel()
        {
            initGrid();
            //  verify somethings
            if (this.textBoxExcelFile.Text == null || this.textBoxExcelFile.Text.Length == 0)
            {
                MessageBox.Show("Chưa chọn file Excel!");
                return;
            }

            if ((this.textBoxTableFromRow.Text == null || this.textBoxTableFromRow.Text.Length == 0)
                || (this.textBoxTableToRow.Text == null || this.textBoxTableToRow.Text.Length == 0))
            {
                MessageBox.Show("Tọa độ bảng dữ liệu không hợp lệ!");
                return;
            }
            //===========================================
            ex.Application excel = null;
            ex.Workbook wkb = null;

            try
            {
                excel = new ex.Application();

                wkb = excel.Workbooks.Open(
                    this.textBoxExcelFile.Text, 0, true, 5,
                    "", "", true, ex.XlPlatform.xlWindows, "\t", false, false,
                    0, true);

                foreach (ex.Worksheet sheet in wkb.Sheets)
                {
                    importCongviecSheet(sheet);
                    break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.checkBox1.Checked)
            //{
            //    this.dataGridView1.Columns[5].HeaderText = "Định mức";
            //}
            //else
            //{
            //    this.dataGridView1.Columns[5].HeaderText = "Đơn giá";
            //}
        }

        //========================================
        bool isLamaNumber(String s)
        {
            s = s.Trim();
            String[] ss = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX"};

            foreach (String lama in ss)
            {
                if (s.CompareTo(lama) == 0)
                    return true;
            }

            return false;
        }
        //========================================
        bool isUpperABC(String s)
        {
            if (s.Length == 0)
                return false;
            s = s.Trim();

            if (s[0] >= 'A' && s[0] <= 'Z')
                return true;

            return false;
        }

        bool isLowercaseabc(String s)
        {
            if (s.Length == 0)
                return false;
            s = s.Trim();

            if (s[0] >= 'a' && s[0] <= 'z')
                return true;

            return false;
        }

        bool isNumber(String s)
        {
            try
            {
                int v = Int32.Parse(s);

                return true;
            }
            catch (Exception e)
            {
            }

            return false;
        }

        int getLevelOfString(String s)
        {
            if (isLamaNumber(s)) return TT_LAMA;

            if (isUpperABC(s)) return TT_UPPER_ABC;

            if (isLowercaseabc(s)) return TT_lower_abc;

            if (isNumber(s)) return TT_number;

            return C.JOB_LEVEL_DETAIL;
        }

        bool isvalidString(String s)
        {
            s = s.Trim();
            if (s != null && s.Length > 0)
            {
                return true;
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            startImportToDatabase();
            Cursor.Current = Cursors.Default;
        }

        void startImportToDatabase()
        {
            if (!isvalidString(this.textBoxPhieuName.Text))
            {
                MessageBox.Show("Tên quyết định không hợp lệ!");
                return;
            }
            if (!isvalidString(this.textBoxPhieuNo.Text))
            {
                MessageBox.Show("Số quyết địnhkhông hợp lệ!");
                return;
            }

            String mact = getMaCT();
            if (mact == null)
            {
                MessageBox.Show("Công trình không hợp lệ!");
                return;
            }

            String phieu_no = this.textBoxPhieuNo.Text;
            if (DatabaseUtils.getInstance().isPhieuNoExist(mact, phieu_no))
            {
                MessageBox.Show("Lỗi. Số quyết định đã tồn tại.");
                return;
            }

            if (!isvalidString(this.comboBoxTeamlist.Text))
            {
                MessageBox.Show("Đội thi công không hợp lệ!");
                return;
            }

            int conglaodong = 0;
           
            //==============insert phieu=====================
            DatabaseUtils db = DatabaseUtils.getInstance();
            int kieutinhchiphi = 1;
            //if (this.checkBox1.Checked)
            //    kieutinhchiphi = 1;
            db.insertPhieuToProject(mact, this.textBoxPhieuNo.Text, this.textBoxPhieuName.Text, this.comboBoxTeamlist.Text, kieutinhchiphi);

            int phieuID = db.getPhieuID(mact, this.textBoxPhieuNo.Text);
            if (phieuID == 0)
            {
                MessageBox.Show("Không insert được phiếu vào database!");
                return;
            }
            //===============================================
            for (int i = 0; i < mTasks.Count; i++)
            {
                ImportDuAnElement e = mTasks[i];

                int job_parent = lookForJobParent(i);

                if (e.level != C.JOB_LEVEL_DETAIL)
                {
                    int job_id = db.insertJobToProject(phieuID, e.noidung, job_parent, e.level,e.thutu, i);
                    e.job_id = job_id;
                }
                else
                {
                    int mucKK = (int)Utils.convertStringToInt(e.mucKK);
                    int job_id = 0;
                    //if (this.checkBox1.Checked)
                    {
                        job_id = db.insertJobToProject(phieuID,
                                            job_parent,
                                            e.noidung,
                                            e.donvitinh,
                                            mucKK,
                                            (float)e.khoiluong,
                                            (Int64)e.dongia,
                                            (float)e.dinhmuc,
                                            e.level,
                                            (float)e.dinhbien,
                                            e.thutu,
                                            e.ghichu,
                                            i
                                            
                                            
                                            );
                    }
                    /*
                    else
                    {
                        job_id = db.insertJobToProject(phieuID,
                                            job_parent,
                                            e.noidung,
                                            e.donvitinh,
                                            mucKK,
                                            (float)e.khoiluong,
                                            (Int64)e.dongia,
                                            e.level,
                                            1);
                    }
                     */
                    e.job_id = job_id;
                }
            }
            //===================================
            MessageBox.Show("Import thành công.");
            this.Close();
        }

        int lookForJobParent(int fromIndex)
        {
            ImportDuAnElement current = mTasks[fromIndex];
            for (int i = fromIndex-1; i >= 0; i--)
            {
                ImportDuAnElement e = mTasks[i];

                if (e.level < current.level)
                    return e.job_id;
            }

            return 0;
        }

        String getMaCT()
        {
            String name = this.comboBoxProjectList.Text;

            if (!isvalidString(name))
            {
                return null;
            }

            foreach (DataRow r in mProjectsTB.Rows)
            {
                String _n = Utils.getStringOfRow(r, 1);
                if (_n.CompareTo(name) == 0)
                {
                    return Utils.getStringOfRow(r, 0);
                }
            }
            return null;
        }

        //==============================================
    }
}

