using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using SimpleBroker;
using VBSQLHelper;
using ProjectStorage.Models;
using ProjectStorage.Forms;

namespace ProjectStorage
{
    public partial class FrmGroupDuAn : DevExpress.XtraEditors.XtraForm
    {
        public FrmGroupDuAn()
        {
            InitializeComponent();           
            this.Load += (s, e) => { this.Subscribe<MessageBroker>(OnNext); };
        }
        private void OnNext(MessageBroker value)
        {
            if (value.Task == "ADD_GROUP_CUSTOMER")
            {
                LoadData();
            }
            else if (value.Task == "CLOSE_GROUP_CUSTOMER")
            {
                bar_save.Enabled = true;
            }
        }

        public void LoadData()
        {
            var data =  SQLHelper.ExecQueryData<GroupDuAn>("SELECT * FROM NHOMCONGTRINH");
            gridControl1.DataSource = data;
        }
     //   Permission permission;
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            gridView1.CellValueChanged += (ss, ee) =>
            {
                if (!modified.Contains(ee.RowHandle))
                {
                    modified.Add(ee.RowHandle);
                }
            };
            LoadData();
            //permission = Authenticate.Instance.AuthenticateByAction(Mdl_Share.CURRENT_USER.Permission, this.Tag.ToString());
           modified = new List<int>();
            //if (!permission.edit)
            //{
            //    bar_capnhat.Enabled = false;
            //}
            //if (!permission.add)
            //{
            //    bar_save.Enabled = false;
            //}
            //if (!permission.delete)
            //{
            //    grid_xoa.Visible = false;
            //}
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == grid_xoa)
            {
                //if (!permission.delete)
                //{
                //    FrmMain.Instance.ShowMessageError("Bạn không có quyền xóa nhóm khách hàng!");
                //    return;
                //}
                var group_duan = gridView1.GetFocusedRow() as GroupDuAn;
                // kiểm tra xem nhóm dự án này có đang sử dụng không
                var isUsed = Convert.ToInt32(SQLHelper.ExecQuerySacalar($"select count(*) from congtrinh where NhomCT='{group_duan.Id}'")) > 0;
                if (isUsed) {
                    XtraMessageBox.Show($"Dự án <b><color=red>{group_duan.TenNhom}</color></b> đang sử dụng, bạn không thể xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information, allowHtmlText: DevExpress.Utils.DefaultBoolean.True);
                    return;
                }

                var dlg = XtraMessageBox.Show($"Bạn có chắc chắn muốn xóa nhóm <b><color=red>{group_duan.TenNhom}</color></b> này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, allowHtmlText: DevExpress.Utils.DefaultBoolean.True);
                if (dlg == DialogResult.Yes)
                {
                    var response = SQLHelper.Delete(group_duan);
                    if (response)
                    {
                        gridView1.DeleteRow(e.RowHandle);
                        XtraMessageBox.Show($"Đã xóa nhóm {group_duan.TenNhom} thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        List<int> modified;
        

        private void bar_save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!permission.add)
            //{
            //    FrmMain.Instance.ShowMessageError("Bạn không có quyền thêm nhóm khách hàng!");
            //    return;
            //}
            bar_save.Enabled = false;
            var f = new FrmAddGroupDuAn();
            FrmMain.Instance.ShowDialogFormOverlay(f);  

        }

        private void bar_capnhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.PostEditor();
            if (modified.Count > 0)
            {
                var dlg = XtraMessageBox.Show($"Bạn có muốn lưu lại <b><color=red>{modified.Count}</color></b> thay đổi của danh mục nhóm dự án không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question,allowHtmlText: DevExpress.Utils.DefaultBoolean.True);
                if (dlg == DialogResult.Yes)
                {
                   var list_group_customer = new List<GroupDuAn>();
                    foreach (var item in modified)
                    {
                        var groupCustomer = gridView1.GetRow(item) as GroupDuAn;                     
                        list_group_customer.Add(groupCustomer);
                    }
                    bool isSuccess = SQLHelper.Update(list_group_customer);
                    if (isSuccess)
                    {
                        XtraMessageBox.Show($"Hệ thống đã cập nhật {modified.Count} dòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        modified.Clear();
                        LoadData();
                    }
                    else
                    {
                        XtraMessageBox.Show($"Hệ thống không cập nhật được dữ liệu do nhóm khách hàng trùng nhau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        modified.Clear();
                        LoadData();
                    }

                }
            }
            else
            {
               XtraMessageBox.Show("Hệ thống chưa thấy dữ liệu thay đổi trên danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bar_naplai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}