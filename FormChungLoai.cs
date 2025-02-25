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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace ProjectStorage
{
    public partial class FormChungLoai : XtraForm
    {
        DataTable mDataTB;
        public FormChungLoai()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {            
            refreshForm();
        }

        void refreshForm()
        {
            DataTable dt = DatabaseUtils.getInstance().tbGetChungLoai();

            foreach (DataRow r in dt.Rows)
            {
                int catID = Utils.getIntValueOfRow(r, 0);
                int amount = DatabaseUtils.getInstance().getAmountOfThietbiWithCatID(catID);
                int instore = DatabaseUtils.getInstance().getAmountOfInstoreThietbiWithCatID(catID);

                r[2] = amount;
                r[3] = instore;
            }

            this.dataGridView1.DataSource = dt;
            this.dataGridView1.ColumnHeadersHeight = 36;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

            mDataTB = dt;
            this.dataGridView1.Columns[0].Visible = false;
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
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    DataRow r = mDataTB.Rows[index];

                    int id = (int)r[0];
                    String name = (String)r[1];

                    FormThietbiOfChungloai form = new FormThietbiOfChungloai(id, name) { MdiParent = this.MdiParent };
                    form.Show();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cellMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (Mdl_Share.currentUser.quyen == "USERS")
                {
                    var permissionDuAn = Mdl_Share.currentPermission.Where(x => x.MaCT == "TB1").FirstOrDefault();
                    addChungLoaiToolStripMenuItem.Enabled = permissionDuAn.add;
                   
                      
                    //    modifyToolStripMenuItem.Enabled = permissionDuAn2.edit;
                        removeChungLoaiToolStripMenuItem.Enabled = permissionDuAn.delete;
                      //  contextMenuStrip1.Show(gridControl1, e.Location);

                       // contextMenuStrip1.Show(dataGridView1, e.Location);
                   

                }
                contextMenuStrip1.Show(dataGridView1, e.Location);

            }
        }

        private void addChungLoaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogAddChungLoai dlg = new DialogAddChungLoai();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String name = dlg.textBoxCatName.Text;
                String desc = dlg.textBoxCatDesc.Text;

                if (DatabaseUtils.getInstance().tbInsertChungLoai(name, desc))
                {
                    refreshForm();
                }
                else
                {
                    MessageBox.Show("Lỗi không thêm được chủng loại mới!");
                }
            }
        }

        private void removeChungLoaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = dataGridView1.SelectedRows[0];

                    String chungloai = (String)r.Cells[1].Value;
                    int id = (int)r.Cells[0].Value;
                    String msg = "Bạn có chắc chắn muốn xóa chủng loại " + chungloai + " và tất cả các thiết bị thuộc chủng loại này không?";
                    if (MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                    {
                        DatabaseUtils db = DatabaseUtils.getInstance();
                        db.tbDeleteChungloai(id);

                        refreshForm();
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }

    }
}
