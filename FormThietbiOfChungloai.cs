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
    public partial class FormThietbiOfChungloai : Office2007Form
    {
        DataTable mDataTB;
        int mCategoryID;
        String mCategoryName;
        public FormThietbiOfChungloai(int catID, String catName)
        {
            InitializeComponent();

            mCategoryID = catID;
            mCategoryName = catName;
        }

        private void onLoad(object sender, EventArgs e)
        {
            refreshForm();
        }

        void refreshForm()
        {
            DataTable dt = DatabaseUtils.getInstance().tbGetThietbiOfChungLoai(mCategoryID, -1);

            this.dataGridView1.DataSource = dt;
            this.dataGridView1.ColumnHeadersHeight = 36;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

            mDataTB = dt;
            this.dataGridView1.Columns["id"].Visible = false;
            this.dataGridView1.Columns["cat_id"].Visible = false;
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
                modifyToolStripMenuItem_Click(null, null);  //  show modify dialog
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
                    addDeviceToolStripMenuItem.Enabled = permissionDuAn.add;
                    removeDeviceToolStripMenuItem.Enabled = permissionDuAn.delete;                   
                    modifyToolStripMenuItem.Enabled = permissionDuAn.edit;                   


                }

                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void addChungLoaiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogCreateNewAmountOfDevices dlgAmount = new DialogCreateNewAmountOfDevices();
            dlgAmount.textBoxCategory.Text = mCategoryName;
            if (dlgAmount.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int amount = 0;
                String code = "";
                int index0 = 0;
                try
                {
                    amount = Int32.Parse(dlgAmount.textBoxAmount.Text);
                    code = dlgAmount.textBoxCode.Text;
                    index0 = Int32.Parse(dlgAmount.textBoxIndex0.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Số lượng hoặc mã thiết bị hoặc Index0 không hợp lệ");
                    return;
                }
                if (amount <= 0 || amount > 1000)
                {
                    MessageBox.Show("Số lượng phải nằm trong khoảng [1-1000]");
                }
                if (code == null || code.Length == 0)
                {
                    MessageBox.Show("Mã chủng loại không hợp lệ");
                }

                DateTime ngaySanxuat = dlgAmount.dateTimePickerNgaySX.Value;
                DateTime ngaySuDung = dlgAmount.dateTimePickerNgaySuDung.Value;
                String nuocSX = dlgAmount.textBoxNuocSX.Text;

                DialogTaoThietBi form = new DialogTaoThietBi(mCategoryID, mCategoryName, amount, code, index0, ngaySanxuat, ngaySuDung,nuocSX);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    refreshForm();
                }
            }
        }

        private void removeDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    int index = this.dataGridView1.SelectedRows[0].Index;
                    DataRow r = mDataTB.Rows[index];
                    int id = (int)r[0];
                    String name = (String)r[1];
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa thiết bị: " + name + " không?", "Cảnh báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        DatabaseUtils.getInstance().tbRemoveDevice(id);

                        refreshForm();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void linkLabelChungloai_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Mdl_Share.currentUser.quyen == "USERS")
            {
                var permissionDuAn = Mdl_Share.currentPermission.Where(x => x.MaCT == "TB1").FirstOrDefault();
                if (!permissionDuAn.edit) return;


            }

           

            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int index = this.dataGridView1.SelectedRows[0].Index;
                DataRow r = mDataTB.Rows[index];
                int id = (int)r[0];
                String code = (String)r[1];

                DialogSuaThietBi dlg = new DialogSuaThietBi(code);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    String serial = dlg.textBoxSerial.Text;
                    String tinhtrang = dlg.textBoxStatus.Text;
                    DateTime ngaysx = dlg.dateTimePickerNgaySX.Value;
                    DateTime ngaysd = dlg.dateTimePickerNgaySD.Value;
                    String ghichu = dlg.textBoxDesc.Text;
                    String thietbikemtheo = dlg.textBoxThietbikemtheo.Text;
                    String chiettietthietbi = dlg.textBoxChitiet.Text;

                    DatabaseUtils.getInstance().tbSuaThietBi(code, serial, tinhtrang, ghichu, ngaysx, ngaysd, thietbikemtheo, chiettietthietbi);

                    refreshForm();
                }
            }
        }

    }
}
