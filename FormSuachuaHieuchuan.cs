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
using DevExpress.XtraEditors;

namespace ProjectStorage
{
    public partial class FormSuachuaHieuchuan : XtraForm
    {
        DataTable mChungloaiTB;

        int mCurrentThongKe = 0;
        object mCurrentThongKeObj = null;

        public FormSuachuaHieuchuan()
        {
            InitializeComponent();

            try
            {
                //  chung loai
                mChungloaiTB = DatabaseUtils.getInstance().tbGetChungLoai();

                List<String> list;
                list = new List<string>();
                foreach (DataRow r in mChungloaiTB.Rows)
                {
                    list.Add(Utils.getStringOfRow(r, 1));
                }
                this.comboBoxChungloai.DataSource = list;

                DataTable dt = DatabaseUtils.getInstance().tbGetCodesOfThietBiOfChungloai(0);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<String> codes = new List<string>(dt.Rows.Count);
                    foreach (DataRow r in dt.Rows)
                    {
                        String code = (String)r[0];
                        codes.Add(code);
                    }
                    this.comboBoxDeviceCode.DataSource = codes;
                }

                dataGridView1.ColumnHeadersHeight = 36;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            }
            catch (Exception ex)
            {
            }
        }

        private void onLoad(object sender, EventArgs e)
        {
            refreshForm();
        }

        void refreshForm()
        {
            if (mCurrentThongKeObj != null && mCurrentThongKe != 0)
            {
                DataTable tb = DatabaseUtils.getInstance().tbThongkeThietbiHieuChuan(mCurrentThongKe, mCurrentThongKeObj);
                showData(tb);
            }
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

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExcelExporter.dataGridViewToExcel(this.dataGridView1);
        }

        private void theoChungLoai_Click(object sender, EventArgs e)
        {
            if (comboBoxChungloai.Text.Length > 0)
            {
                String s = comboBoxChungloai.Text;
                foreach (DataRow r in mChungloaiTB.Rows)
                {
                    String name = Utils.getStringOfRow(r, 1);
                    if (name.CompareTo(s) == 0)
                    {
                        int catID = Utils.getIntValueOfRow(r, 0);

                        mCurrentThongKeObj = (Int32)catID;
                        mCurrentThongKe = C.THONGKE_CHUNGLOAI;

                        refreshForm();
                    }
                }
            }
        }
        /*
        private void theoTrongKho_Click(object sender, EventArgs e)
        {
            DataTable dt = DatabaseUtils.getInstance().tbThongkeThietbi(C.THONGKE_TRONGKHO, null);
            showData(dt);
        }

        private void theoDangDiDo_Click(object sender, EventArgs e)
        {
            DataTable dt = DatabaseUtils.getInstance().tbThongkeThietbi(C.THONGKE_DIDO, null);
            showData(dt);
        }
         */

        private void buttonDeviceCode_Click(object sender, EventArgs e)
        {
            String code = this.comboBoxDeviceCode.Text;
            mCurrentThongKeObj = code;
            mCurrentThongKe = C.THONGKE_THEO_CODE;

            refreshForm();
        }

        void showData(DataTable dt)
        {
            this.dataGridView1.DataSource = dt;
            //this.dataGridView1.Columns[2].Visible = false;
            this.dataGridView1.Columns[0].Visible = false;
        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
            }
        }

        private void suachuaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    String code = (String)dataGridView1.SelectedRows[0].Cells[1].Value;

                    //DataTable tb = (DataTable)this.dataGridView1.DataSource;
                    //DataRow r = tb.Rows[index];
                    //String code = Utils.getStringOfRow(r, 1);

                    DialogSuachuaHis dlg = new DialogSuachuaHis(code, true);
                    dlg.ShowDialog();

                    refreshForm();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void hieuchuanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    String code = (String)dataGridView1.SelectedRows[0].Cells[1].Value;

                    //DataTable tb = (DataTable)this.dataGridView1.DataSource;
                    //DataRow r = tb.Rows[index];
                    //String code = Utils.getStringOfRow(r, 1);

                    DialogHieuchuanHis dlg = new DialogHieuchuanHis(code, true);
                    dlg.ShowDialog();

                    refreshForm();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
