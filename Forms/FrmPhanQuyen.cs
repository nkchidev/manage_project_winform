using Dapper.Contrib.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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

namespace ProjectStorage.Forms
{
    public partial class FrmPhanQuyen : DevExpress.XtraEditors.XtraForm
    {
        public FrmPhanQuyen()
        {
            InitializeComponent();
        }
        bool isUser = false;
        private void FrmPhanQuyen_Load(object sender, EventArgs e)
        {
            isUser = Mdl_Share.currentUser.quyen == "ADMINS" ? false : true;

            var listUserQuery = "SELECT * FROM [dbo].[NGUOIDUNG] WHERE [quyen] = 'users'";
            var dataUser = SQLHelper.ExecQueryDataAsDataTable(listUserQuery);
            cb_users.Properties.DataSource= dataUser;
            cb_users.Properties.ValueMember = "taikhoan";
            cb_users.Properties.DisplayMember = "taikhoan";
            cb_users.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

            if (isUser) {
                cb_users.EditValue = Mdl_Share.currentUser.taikhoan;
                cb_users.Enabled = false;
                btnBochonTatca.Enabled = false;
                btnSelectall.Enabled = false;
                cb_action.Enabled = false;
                gridAdd.OptionsColumn.ReadOnly = true;
                gridEdit.OptionsColumn.ReadOnly = true;
                gridDelete.OptionsColumn.ReadOnly = true;
            }
            
        }      


        private void cb_users_EditValueChanged(object sender, EventArgs e)
        {
            var taikhoan = cb_users.EditValue;
            if(taikhoan != null ) { 
                taikhoan = taikhoan.ToString();
                var sql = $@"SELECT
                            [ct].[MaCT],
                            [ct].[TenCT],
                            [pq].[id],
                                   [pq].[taikhoan],    
                                   [pq].[add],
                                   [pq].[edit],
                                   [pq].[delete] FROM (
								   
								   
								  
								   SELECT 'TB1' AS MaCT, N'Danh mục thiết bị' AS TenCT
								   UNION all
								   SELECT 'TB2' AS MaCT, N'Mượn thiết bị' AS TenCT
								   UNION all
								   SELECT 'TB3' AS MaCT, N'Trả thiết bị' AS TenCT
								   UNION all
								   SELECT 'ALL' AS MaCT, N'Thêm mới dự án' AS TenCT
                            UNION all
                            SELECT [MaCT], [TenCT] FROM [dbo].[CONGTRINH]) ct
                            LEFT JOIN (SELECT * FROM  [dbo].[PHANQUYEN] WHERE [taikhoan]=N'{taikhoan}') pq
                            ON ct.[MaCT] = [pq].[MaCT]";

                var dataPhanQuyen = SQLHelper.ExecQueryData<Permission>(sql);
                gridControl1.DataSource = dataPhanQuyen;    
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            cb_users_EditValueChanged(null, null);
        }

        private void InsertOrUpdatePermission( string action, string taikhoan, bool ischecked) {
            if (isUser) {
                XtraMessageBox.Show("Bạn không có quyền chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var handle = gridView1.FocusedRowHandle;
            if (handle >= 0) {
                string MaCT = (gridView1.GetRow(handle) as Permission).MaCT;
                SQLHelper.ExecProcedureNonData("sp_insertUpdatePermission", new { action = action, taikhoan = taikhoan, ischecked = ischecked, MaCT = MaCT });
            }
           

        }

        private void chk_Them_CheckedChanged(object sender, EventArgs e)
        {
            
            string action = "Add";
            string taikhoan = cb_users.Text;
            bool ischecked = (sender as CheckEdit).Checked;
            InsertOrUpdatePermission( action, taikhoan, ischecked);
        }

        private void chkXoa_CheckedChanged(object sender, EventArgs e)
        {
            string action = "Delete";
            string taikhoan = cb_users.Text;
            bool ischecked = (sender as CheckEdit).Checked;
            InsertOrUpdatePermission(action, taikhoan, ischecked);
        }

        private void chkSua_CheckedChanged(object sender, EventArgs e)
        {
            string action = "Edit";
            string taikhoan = cb_users.Text;
            bool ischecked = (sender as CheckEdit).Checked;
            InsertOrUpdatePermission(action, taikhoan, ischecked);
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {
            var taikhoan = cb_users.EditValue;
            if (taikhoan == null) {
                XtraMessageBox.Show("Bạn chưa chọn tài khoản để phân quyền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            taikhoan = taikhoan.ToString();
            var chucnang = cb_action.Text;
            if (chucnang == "") {
                XtraMessageBox.Show("Bạn chưa chọn chức năng: thêm/xóa/sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var data = gridView1.DataSource as List<Permission>;
            var action = "";
            if (chucnang == "Thêm")
            {
                action = "Add";
            }
            else if (chucnang == "Sửa") {
                action = "Edit";
            }
            else { 
                action = "Delete";
            }

            foreach (var item in data)
            {
                SQLHelper.ExecProcedureNonData("sp_insertUpdatePermission", new { action = action, taikhoan = taikhoan, ischecked = true, MaCT = item.MaCT });
            }

            simpleButton1_Click(null, null);

        }

        private void btnBochonTatca_Click(object sender, EventArgs e)
        {
            var taikhoan = cb_users.EditValue;
            if (taikhoan == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn tài khoản để phân quyền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            taikhoan = taikhoan.ToString();
            var chucnang = cb_action.Text;
            if (chucnang == "")
            {
                XtraMessageBox.Show("Bạn chưa chọn chức năng: thêm/xóa/sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var data = gridView1.DataSource as List<Permission>;
            var action = "";
            if (chucnang == "Thêm")
            {
                action = "Add";
            }
            else if (chucnang == "Sửa")
            {
                action = "Edit";
            }
            else
            {
                action = "Delete";
            }

            foreach (var item in data)
            {
                SQLHelper.ExecProcedureNonData("sp_insertUpdatePermission", new { action = action, taikhoan = taikhoan, ischecked = false, MaCT = item.MaCT });
            }

            simpleButton1_Click(null, null);
        }
    }
}