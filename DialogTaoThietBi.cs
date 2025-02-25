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
    public partial class DialogTaoThietBi : Office2007Form
    {
        int mCategoryID;
        String mCategoryName;
        int mAmount;
        String mCatCode;
        DateTime mNgaySX;
        DateTime mNgaySD;
        String mNuocSX;

        DataTable mDataTB;
        public DialogTaoThietBi(int catID, String catName, int amount, String catCode, int startCode, DateTime ngaySX, DateTime ngaySD, String nuocSX)
        {
            InitializeComponent();
            mCategoryID = catID;
            mCategoryName = catName;
            mAmount = amount;
            mCatCode = catCode;
            mNuocSX = nuocSX;

            mDataTB = new DataTable();
            mDataTB.Columns.Add(C.COL_CODE).DataType = Type.GetType("System.String");
            mDataTB.Columns.Add(C.COL_SERIAL).DataType = Type.GetType("System.String");
            mDataTB.Columns.Add(C.COL_TINHTRANG).DataType = Type.GetType("System.String");
            mDataTB.Columns.Add(C.COL_THIETBIKEMTHEO).DataType = Type.GetType("System.String");
            mDataTB.Columns.Add(C.COL_CHITIETTIHETBI).DataType = Type.GetType("System.String");
            mDataTB.Columns.Add(C.COL_GHICHU).DataType = Type.GetType("System.String");
            mDataTB.Columns.Add(C.COL_NGAYSANXUAT).DataType = Type.GetType("System.String");
            mDataTB.Columns.Add(C.COL_NGAYSUDUNG).DataType = Type.GetType("System.String");

            for (int i = 0; i < mAmount; i++)
            {
                DataRow r = mDataTB.NewRow();

                String code = mCatCode + "_" + startCode.ToString("00");
                r[C.COL_CODE] = code;
                r[C.COL_SERIAL] = "";
                r[C.COL_GHICHU] = "";
                r[C.COL_TINHTRANG] = "";
                r[C.COL_THIETBIKEMTHEO] = "";
                r[C.COL_CHITIETTIHETBI] = "";

                r[C.COL_NGAYSANXUAT] = ngaySX.ToString("dd/MM/yyyy");
                r[C.COL_NGAYSUDUNG] = ngaySX.ToString("dd/MM/yyyy");

                mDataTB.Rows.Add(r);

                startCode++;
            }

            //===============================
            this.dataGridView1.DataSource = mDataTB;

            disableSortofGridView(this.dataGridView1);
            //================================
            mNgaySX = ngaySX;
            mNgaySD = ngaySD;
        }

        private void onLoad(object sender, EventArgs e)
        {
            refreshForm();
        }

        void refreshForm()
        {
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
            suaThietBi();
        }

        void suaThietBi()
        {
            try
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = dataGridView1.SelectedRows[0];

                    DialogSuaThietBi dlg = new DialogSuaThietBi(null);

                    dlg.textBoxCode.Text = (String)r.Cells[C.COL_CODE].Value;
                    dlg.textBoxSerial.Text = (String)r.Cells[C.COL_SERIAL].Value;
                    dlg.textBoxDesc.Text = (String)r.Cells[C.COL_GHICHU].Value;
                    dlg.textBoxStatus.Text = (String)r.Cells[C.COL_TINHTRANG].Value;
                    dlg.textBoxThietbikemtheo.Text = (String)r.Cells[C.COL_THIETBIKEMTHEO].Value;
                    dlg.textBoxChitiet.Text = (String)r.Cells[C.COL_CHITIETTIHETBI].Value;

                    String ngaysx = (String)r.Cells[C.COL_NGAYSANXUAT].Value;
                    String ngaysd = (String)r.Cells[C.COL_NGAYSUDUNG].Value;

                    dlg.dateTimePickerNgaySX.Value = Utils.convert_ddMMyyyyStringToDateTime(ngaysx);
                    dlg.dateTimePickerNgaySD.Value = Utils.convert_ddMMyyyyStringToDateTime(ngaysd);

                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        r.Cells[C.COL_SERIAL].Value = dlg.textBoxSerial.Text;
                        r.Cells[C.COL_GHICHU].Value = dlg.textBoxDesc.Text;
                        r.Cells[C.COL_TINHTRANG].Value = dlg.textBoxStatus.Text;
                        r.Cells[C.COL_THIETBIKEMTHEO].Value = dlg.textBoxThietbikemtheo.Text;
                        r.Cells[C.COL_CHITIETTIHETBI].Value = dlg.textBoxChitiet.Text;

                        r.Cells[C.COL_NGAYSANXUAT].Value = dlg.dateTimePickerNgaySX.Value.ToString("dd/MM/yyyy");
                        r.Cells[C.COL_NGAYSUDUNG].Value = dlg.dateTimePickerNgaySD.Value.ToString("dd/MM/yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void createNewDeviceConfirm(object sender, EventArgs e)
        {
            //  validate first
            foreach (DataRow r in mDataTB.Rows)
            {
                String code = (String)r[C.COL_CODE];
                String serial = (String)r[C.COL_SERIAL];

                if (code.Length == 0 || serial.Length == 0)
                {
                    MessageBox.Show("Lỗi! Có thiết bị mà Mã hoặc Serial bị trống, vui lòng kiểm tra lại.");
                    return;
                }
            }
            foreach (DataRow r in mDataTB.Rows)
            {
                String code = (String)r[C.COL_CODE];
                String serial = (String)r[C.COL_SERIAL];
                String desc = (String)r[C.COL_GHICHU];
                String tinhtrang = (String)r[C.COL_TINHTRANG];
                String thietbikemtheo = (String)r[C.COL_THIETBIKEMTHEO];
                String chitietthietbi = (String)r[C.COL_CHITIETTIHETBI];

                String sngaysx = (String)r[C.COL_NGAYSANXUAT];
                String sngaysd = (String)r[C.COL_NGAYSUDUNG];

                DateTime ngaysx = Utils.convert_ddMMyyyyStringToDateTime(sngaysx);
                DateTime ngaysd = Utils.convert_ddMMyyyyStringToDateTime(sngaysd);

                DataRow thietbi = DatabaseUtils.getInstance().tbGetThietbiWithCode(code);
                if (thietbi == null)
                {
                    DatabaseUtils.getInstance().tbInsertDevices(code, mCategoryID, serial, tinhtrang, desc, ngaysx, ngaysd, mNuocSX, thietbikemtheo, chitietthietbi);
                }
                else
                {
                    MessageBox.Show("Mã thiết bị: " + code + " đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
