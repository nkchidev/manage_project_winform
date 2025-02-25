using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SimpleBroker;
using DevExpress.XtraEditors;
using VBSQLHelper;
using ProjectStorage.Models;

namespace ProjectStorage
{
    public partial class FrmAddGroupDuAn : DevExpress.XtraEditors.XtraForm
    {
        //private static FrmAddGroupCustomer _defaultInstance;
        //public static FrmAddGroupCustomer Instance
        //{
        //    get
        //    {
        //        if (_defaultInstance == null || _defaultInstance.IsDisposed)
        //        {
        //            _defaultInstance = new FrmAddGroupCustomer();
        //        }
        //        return _defaultInstance;
        //    }
        //    set => _defaultInstance = value;
        //}
        public FrmAddGroupDuAn()
        {
            InitializeComponent();
            this.FormClosing += FrmAddProduct_FormClosing;
        }

        private void FrmAddProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            var message = new MessageBroker
            {
                Task = "CLOSE_GROUP_CUSTOMER"
            };
            message.Publish();
        }

        private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
                return cp;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text.Trim()))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên nhóm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_name.Focus();
                return;
            }
        

            var sql_checkexits = $"SELECT COUNT(*) FROM NHOMCONGTRINH where LOWER(TenNhom) =N'{txt_name.Text.ToLower()}'";
            int count = Convert.ToInt32(SQLHelper.ExecQuerySacalar(sql_checkexits));
            if (count > 0)
            {    
                XtraMessageBox.Show($"Tên nhóm  {txt_name.Text} đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var groupDuAn = new GroupDuAn()
            {
                TenNhom = txt_name.Text.Trim(),
               
            };

            SQLHelper.Insert(groupDuAn);
            XtraMessageBox.Show($"Đã thêm {groupDuAn.TenNhom} thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
          

            var message = new MessageBroker
            {
                Task = "ADD_GROUP_CUSTOMER"
            };
            message.Publish();
            txt_name.Text = "";            
            txt_name.Focus();



        }

        private void FrmAddGroupCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_save_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }

}

