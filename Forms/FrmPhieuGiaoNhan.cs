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
using DevExpress.XtraEditors.Controls;
using VBSQLHelper;
using ProjectStorage.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;

namespace ProjectStorage
{
    public partial class FrmPhieuGiaoNhan : DevExpress.XtraEditors.XtraForm
    {


        public FrmPhieuGiaoNhan()
        {
            InitializeComponent();
            gv_phieugiaonhan.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
        }
        bool cal(Int32 _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
        }

        void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!gv_phieugiaonhan.IsGroupRow(e.RowHandle)) //Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) //Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; //Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); //Số thứ tự tăng dần
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); //Lấy kích thước của vùng hiển thị Text
                    Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_Width, gv_phieugiaonhan); })); //Tăng kích thước nếu Text vượt quá
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1)); //Nhân -1 để đánh lại số thứ tự tăng dần
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gv_phieugiaonhan); }));
            }
        }


        public void LoadData()
        {

            var data = SQLHelper.ExecQueryDataAsDataTable(@"SELECT phieu_id,
                           MaCT,
                           phieu_no,
                           phieu_desc,
                           chi_phi,
                           da_thanh_toan,
                           doi_thi_cong,
                           kieu_tinh_chi_phi, SUBSTRING(phieu_no, 4, 4) AS nam FROM phieugiaonhan ");
            
            gridControl_phieugiaonhan.DataSource = data;
       

        }

      
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            LoadData();
           

        }
        private void btn_addUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView gridView = gv_phieugiaonhan; 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FileName = "data_phieugiaonhan.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {               
                XlsxExportOptions options = new XlsxExportOptions();
                options.TextExportMode = TextExportMode.Text;
                gridView.ExportToXlsx(saveFileDialog.FileName, options);           
                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }

        }

        private void btnRefreshData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}