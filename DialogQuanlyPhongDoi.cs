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
    public partial class DialogQuanlyPhongDoi : Office2007Form
    {
        public DialogQuanlyPhongDoi()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            refreshForm();
        }

        void refreshForm()
        {
            try
            {
                this.dataGridView1.DataSource = DatabaseUtils.getInstance().getPhongs();
                this.dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
            }
        }

        private void onItemDoubleClick(object sender, MouseEventArgs e)
        {
            suaPhong();
        }

        void suaPhong()
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = dataGridView1.SelectedRows[0];

                    DialogSuaPhong dlg = new DialogSuaPhong();

                    int id = (int)r.Cells[0].Value;
                    dlg.textBoxName.Text = (String)r.Cells[1].Value;

                    if (r.Cells[2].Value != DBNull.Value)
                    {
                        dlg.textBoxDesc.Text = (String)r.Cells[2].Value;
                    }

                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!DatabaseUtils.getInstance().updatePhong(id, dlg.textBoxName.Text, dlg.textBoxDesc.Text))
                        {
                            MessageBox.Show("Lỗi", "Lỗi database, không sửa được dữ liệu");
                        }
                        else
                        {
                            refreshForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void suaphongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            suaPhong();
        }

        private void addPhongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            themPhong();
        }

        void themPhong()
        {
            DialogSuaPhong dlg = new DialogSuaPhong();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.textBoxName.Text.Length == 0)
                {
                    MessageBox.Show("Lỗi", "Tên phòng không hợp lệ");
                    return;
                }
                if (!DatabaseUtils.getInstance().insertPhong(dlg.textBoxName.Text, dlg.textBoxDesc.Text))
                {
                    MessageBox.Show("Lỗi", "Lỗi database, không cập nhật được dữ liệu");
                }
                else
                {
                    refreshForm();
                }
            }
        }

        private void deletePhongToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
            }
        }

        private void suaphongToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            suaPhong();
        }

        private void addPhongToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            themPhong();
        }

        private void deletePhongToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow r = dataGridView1.SelectedRows[0];

                String phong = (String)r.Cells[1].Value;

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phòng: " + phong + " khỏi hệ thống không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                {
                    DialogSuaPhong dlg = new DialogSuaPhong();

                    int id = (int)r.Cells[0].Value;

                    DatabaseUtils.getInstance().deletePhong(id);

                    this.refreshForm();
                }
            }        
        }
    }
}
