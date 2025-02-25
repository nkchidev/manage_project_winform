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
    public partial class FormThongKeThietBi : XtraForm
    {
        DataTable mChungloaiTB;
        DataTable mContrinhTB;
        DataTable mGridTable;
        public FormThongKeThietBi()
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

                //  cong trinh
                mContrinhTB = DatabaseUtils.getInstance().GetListProject3(null);
                list = new List<string>();
                foreach (DataRow r in mContrinhTB.Rows)
                {
                    list.Add(Utils.getStringOfRow(r, 1));
                }
                this.comboBoxCongtrinh.DataSource = list;

                //  bo phan
                this.comboBoxBoPhan.DataSource = DatabaseUtils.getInstance().getTeamList();

                dataGridView1.ColumnHeadersHeight = 36;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

                this.comboBoxCodes.DataSource = DatabaseUtils.getInstance().tbGetCodes();
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

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

                        DataTable dt = DatabaseUtils.getInstance().tbThongkeThietbi(C.THONGKE_CHUNGLOAI, (Int32)catID);
                        showData(dt);
                    }
                }
            }
        }

        private void theoCongtrinh_Click(object sender, EventArgs e)
        {
            if (comboBoxCongtrinh.Text.Length > 0)
            {
                String s = comboBoxCongtrinh.Text;
                String maCT = null;

                foreach (DataRow r in mContrinhTB.Rows)
                {
                    String name = Utils.getStringOfRow(r, 1);
                    if (name.CompareTo(s) == 0)
                    {
                        maCT = Utils.getStringOfRow(r, 0);
                        break;
                    }
                }

                if (maCT != null)
                {
                    DataTable dt = DatabaseUtils.getInstance().tbThongkeThietbi(C.THONGKE_CONGTRINH, maCT);
                    showData(dt);
                }
            }
        }

        private void theoBoPhan_Click(object sender, EventArgs e)
        {
            if (comboBoxBoPhan.Text.Length > 0)
            {
                String s = comboBoxBoPhan.Text;

                DataTable dt = DatabaseUtils.getInstance().tbThongkeThietbi(C.THONGKE_BOPHAN, s);
                showData(dt);
            }
        }

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

        private void buttonTimtheoCode_Click(object sender, EventArgs e)
        {
            try
            {
                String code = this.comboBoxCodes.Text;
                DataTable dt = DatabaseUtils.getInstance().tbThongkeThietbi(C.THONGKE_THEO_CODE, code);
                showData(dt);
            }catch(Exception ex)
            {
            }
        }

        private void onTimTheoDeviceEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                buttonTimtheoCode_Click(null, null);
            }
        }

        void showData(DataTable dt)
        {
            if (dt == null)
                return;

            mGridTable = dt.Copy();
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool instore = false;
                DataRow r = dt.Rows[i];
                String s = Utils.getStringOfRow(r, C.COL_BOPHAN_SUDUNG);
                if (s.CompareTo("Trong kho") == 0)
                    instore = true;

                if (checkBoxDido.Checked && instore == false
                    || checkBoxTrongkho.Checked && instore)
                {
                }
                else
                {
                    dt.Rows.Remove(r);
                    i--;
                }
            }

            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["id"].Visible = false;
            this.dataGridView1.Columns["cat_id"].Visible = false;
        }

        private void hieuchuanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow r = this.dataGridView1.SelectedRows[0];
                String code = (String)r.Cells[1].Value;
                DialogHieuchuanHis dlg = new DialogHieuchuanHis(code, false);
                dlg.ShowDialog();
            }
        }

        private void onMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void onMouseClick2(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.contextMenuStrip2.Show(dataGridView1, e.Location);
            }
        }

        private void buttonExportToExcel(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count > 0)
            {
                ExcelExporter.dataGridViewToExcel2(this.dataGridView1);
            }
            else
            {
                MessageBox.Show("Warning", "Chưa có dữ liệu để xuất");
            }
        }

        private void checkBoxTrongkho_CheckedChanged(object sender, EventArgs e)
        {
            showData(mGridTable);
        }

        private void checkBoxDido_CheckedChanged(object sender, EventArgs e)
        {
            showData(mGridTable);
        }
    }
}
