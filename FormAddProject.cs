using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VBSQLHelper;

namespace ProjectStorage
{
    public partial class FormAddProject : XtraForm
    {
        private bool mIsModifiedForm;
        private int _selectIndex;

        public FormAddProject(bool isModified, int selectIndex = 0)
        {
            mIsModifiedForm = isModified;
            _selectIndex = selectIndex;

            InitializeComponent();
            //cb_trangthai.SelectedIndex = 0;
            radioGroupTrangThai.SelectedIndex = 0;
            txtNgayKy.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtNgayKy.Properties.Mask.EditMask = "dd/MM/yyyy";

            txtNgayHoanThanh.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtNgayHoanThanh.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.Load += FormAddProject_Load;

            //   DatabaseUtils db = DatabaseUtils.getInstance();

            // List<String> l = db.getTeamList();
        }

        private void FormAddProject_Load(object sender, EventArgs e)
        {
            var dataNhomDuAn = SQLHelper.ExecQueryDataAsDataTable("select * from nhomcongtrinh");

            cb_nhomduan.Properties.DataSource = dataNhomDuAn;
            cb_nhomduan.Properties.ValueMember = "Id";
            cb_nhomduan.Properties.DisplayMember = "TenNhom";
            cb_nhomduan.EditValue = _selectIndex;

            //cb_trangthai.SelectedIndex = 0;
            txtNgayKy.Properties.ShowClear = true;
            txtNgayHoanThanh.Properties.ShowClear = true;
        }

        public string getProjectName()
        {
            return textBoxProjectName.Text;
        }

        public string getProjectDescription()
        {
            return textBoxDescription.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            String code = this.textBoxProjectCode.Text.Trim();

            //if (code.Length == 0)
            //{
            //    MessageBox.Show("Mã công trình không hợp lệ.");
            //    return;
            //}

            String name = this.textBoxProjectName.Text.Trim();
            if (name.Length == 0)
            {
                XtraMessageBox.Show("Bạn chưa nhập tên công trình.");
                textBoxProjectName.Focus();
                return;
            }
            if (cb_nhomduan.Text == "")
            {
                XtraMessageBox.Show("Bạn chưa chọn nhóm dự án.");
                return;
            }

            //if (!mIsModifiedForm && DatabaseUtils.getInstance().isProjectExistWithMaCT(code))
            //{
            //    MessageBox.Show("Mã công trình bị trùng.");
            //    return;
            //}

            String maCT = null;
            if (mIsModifiedForm)
            {
                maCT = code;
            }

            if (!mIsModifiedForm && DatabaseUtils.getInstance().isProjectExistWithName(name, maCT))
            {
                XtraMessageBox.Show($"Tên công trình: {name} đã tồn tại.");
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}