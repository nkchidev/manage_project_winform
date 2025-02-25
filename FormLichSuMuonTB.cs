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
using DevExpress.XtraEditors;

namespace ProjectStorage
{
    public partial class FormLichSuMuonTB : XtraForm
    {
        public FormLichSuMuonTB()
        {
            InitializeComponent();

            dataGridView1.ColumnHeadersHeight = 36;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
        }

        private void onLoad(object sender, EventArgs e)
        {
            DataTable dt = DatabaseUtils.getInstance().getLichSuMuonTB();
            this.dataGridView1.DataSource = dt;
            try
            {
                this.dataGridView1.Columns[0].Width = 36;
            }
            catch (Exception ex) { }
        }

        void refreshForm()
        {
            this.dataGridView1.DataSource = null;

            DataTable dt = DatabaseUtils.getInstance().getLichSuMuonTB();
            this.dataGridView1.DataSource = dt;
            try
            {
                this.dataGridView1.Columns[0].Width = 36;
            }
            catch (Exception ex) { }
        }

        void disableSortofGridView(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void cellFormating(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                try
                {
                    if (this.dataGridView1.SelectedRows.Count > 0)
                    {
                        DataGridViewRow r = this.dataGridView1.SelectedRows[0];
                        int id = (int)r.Cells[0].Value;

                        this.dataGridViewDSTB.DataSource = DatabaseUtils.getInstance().getDSMuonThietBiOfPhieuID(id);

                        DateTime t = (DateTime)r.Cells[1].Value;
                        this.textBoxNgaymuon.Text = t.ToString("HH:mm:ss dd:MM:yyyy");
                        this.textBoxNguoiMuon.Text = (String)r.Cells[2].Value;
                        this.textBoxBophan.Text = (String)r.Cells[3].Value;
                        this.textBoxNguoixuat.Text = (String)r.Cells[4].Value;
                        this.textBoxCongtrinh.Text = (String)r.Cells[5].Value;
                        this.labelGhichu.Text = "Ghi chú: " + (String)r.Cells[6].Value;
                    }
                }catch(Exception ex)
                {
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = this.dataGridView1.SelectedRows[0];
                    int id = (int)r.Cells[0].Value;

                    DatabaseUtils.getInstance().deleteLichsuMuonTB(id);

                    this.dataGridView1.Rows.Remove(r);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
