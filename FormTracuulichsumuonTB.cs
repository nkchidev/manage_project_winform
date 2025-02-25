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
    public partial class FormTracuulichsumuonTB : XtraForm
    {
        public FormTracuulichsumuonTB()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            refreshForm();
        }

        void refreshForm()
        {
            this.comboBoxCode.DataSource = DatabaseUtils.getInstance().tbGetCodes();
        }

        void disableSortofGridView(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void searchTheoma(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                String code = this.comboBoxCode.Text;// this.textBoxCode.Text;

                DataTable dt = DatabaseUtils.getInstance().getLichsuMuonTBFull(code);

                if (dt != null)
                {
                    this.dataGridView1.DataSource = dt;

                    this.dataGridView1.Columns[0].Visible = false;

                    dataGridView1.ColumnHeadersHeight = 36;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

                    this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
            }
        }

        private void showMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.contextMenuStrip2.Show(this.dataGridView1, e.Location);
            }
        }

        private void deleteRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                List<DataGridViewRow> removing = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    int id = (int)r.Cells[0].Value;
                    DatabaseUtils.getInstance().deleteLichsuMuonTBFull(id);

                    removing.Add(r);
                }

                while (removing.Count > 0)
                {
                    DataGridViewRow r = removing.Last();
                    this.dataGridView1.Rows.Remove(r);

                    removing.Remove(r);
                }
            }
        }
    }
}
