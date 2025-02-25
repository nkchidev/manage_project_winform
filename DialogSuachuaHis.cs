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

namespace ProjectStorage
{
    public partial class DialogSuachuaHis : Office2007Form
    {
        String mCode;
        bool mEditable;
        public DialogSuachuaHis(String code, bool editable)
        {
            InitializeComponent();

            mCode = code;
            mEditable = editable;

            if (!mEditable)
            {
                buttonHieuchuan.Visible = false;
                this.buttonDelete.Visible = false;
            }
        }

        private void onLoad(object sender, EventArgs e)
        {
            refreshForm();

            this.labelTitle.Text = "Lịch sử sửa chữa: " + mCode;
        }

        void refreshForm()
        {
            DataTable tb = DatabaseUtils.getInstance().getSuachuaHistoryOfThietbi(mCode);

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

        private void cellMouseClick(object sender, MouseEventArgs e)
        {
        }

        private void onItemClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void buttonHieuchuan_Click(object sender, EventArgs e)
        {
            DialogHieuchuan dlg = new DialogHieuchuan();

            dlg.textBoxCode.Text = mCode;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    DateTime date = dlg.dateTimePicker1.Value;
                    String noidung = dlg.textBoxNoidung.Text;
                    String donvi = dlg.textBoxDonvi.Text;
                    if (noidung.Length > 0)
                    {
                        DatabaseUtils.getInstance().insertSuachuaOfThietbi(mCode, date, noidung,donvi);

                        refreshForm();
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa nhập nội dung hiệu chuẩn.");
                    }
                }
                catch(Exception ex)
                {
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa các hiệu chuẩn đã chọn?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {
                        int id = (int)r.Cells[0].Value;

                        DatabaseUtils.getInstance().deleteSuachua(id);
                    }

                    refreshForm();
                }
            }
        }

        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            ExcelExporter.dataGridViewToExcel(this.dataGridView1);
        }
    }
}
