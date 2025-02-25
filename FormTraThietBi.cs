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
using ex = Microsoft.Office.Interop.Excel;

namespace ProjectStorage
{
    public partial class FormTraThietBi : XtraForm
    {
        DataTable mSelectedThietbiTB;
        DataTable mChungLoaiTB;
        DataTable mThietBiSeTraTB;

        public FormTraThietBi()
        {
            InitializeComponent();

            DataTable tb2 = new DataTable();

            tb2.Columns.Add(C.COL_CODE).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_SERIAL).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_TINHTRANG).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_GHICHU).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_THIETBIKEMTHEO).DataType = Type.GetType("System.String");

            this.mSelectedThietbiTB = tb2;
            this.dataGridViewThietBiTra.DataSource = mSelectedThietbiTB;

            //  chung loai
            DataTable tb = DatabaseUtils.getInstance().tbGetChungLoai();
            mChungLoaiTB = tb;

            List<String> list = new List<string>();

            foreach (DataRow r in tb.Rows)
            {
                String s = (String)r[1];
                list.Add(s);
            }

            this.comboBoxChungLoai.DataSource = list;

            //  time
            this.dateTimePicker1.Value = DateTime.Now;

            //  bo phan nguoi tra
            List<String> bophan = DatabaseUtils.getInstance().getTeamList();
            this.comboBoxBoPhanTra.DataSource = bophan;

            //  some other
            this.labelBoPhan.Text = "";
            this.labelNguoiMuon.Text = "";
            this.labelNgayMuon.Text = "";
            var permissionTraThietBi = Mdl_Share.currentPermission.Where(x => x.MaCT == "TB3").FirstOrDefault();
            buttonOK.Enabled = permissionTraThietBi.add;
            buttonAddToDSTra.Enabled = permissionTraThietBi.add;
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
            try
            {
                if (dataGridViewThietBiTra.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = dataGridViewThietBiTra.SelectedRows[0];
                    String code = (String)r.Cells[C.COL_CODE].Value;
                    String tinhtrang = (String)r.Cells[C.COL_TINHTRANG].Value;
                    String thietbikemtheo = (String)r.Cells[C.COL_THIETBIKEMTHEO].Value;

                    DialogChitietMuonThietBi dlg = new DialogChitietMuonThietBi(code, tinhtrang, thietbikemtheo);
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        r.Cells[C.COL_TINHTRANG].Value = dlg.textBoxTinhTrang.Text;
                        r.Cells[C.COL_THIETBIKEMTHEO].Value = dlg.textBoxThietbiKemTheo.Text;
                    }
                }
            }
            catch (Exception ex)
            { 
            }
        }

        private void cellMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridViewThietBiTra, e.Location);
            }
        }

        DataRow getSelectedRowOfThietBiSeTra()
        {
            if (this.dataGridViewThietBiDangMuon.SelectedRows.Count > 0)
            {
                int index = this.dataGridViewThietBiDangMuon.SelectedRows[0].Index;
                DataRow r = this.mThietBiSeTraTB.Rows[index];

                return r;
            }

            return null;
        }

        /*
        private void onThietbiDangMuonClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow r = getSelectedRowOfThietBiSeTra();

            if (r != null)
            {
                String code = (String)r[C.COL_CODE];

                DataRow thongtin = DatabaseUtils.getInstance().tbGetThongtinThietBiMuon(code);
                if (thongtin != null)
                {
                    String nguoimuon = (String)thongtin[0];
                    String bophan = (String)thongtin[1];
                    String date = Utils.dateToStringOfRow(thongtin, 4);

                    this.labelNgayMuon.Text = date;
                    this.labelNguoiMuon.Text = nguoimuon;
                    this.labelBoPhan.Text = bophan;

                    this.textBoxGhichuStatusOfDevice.Text = Utils.getStringOfRow(r, C.COL_TINHTRANG);
                }
            }
        }
         * */

        private void onChungloaiChanged(object sender, EventArgs e)
        {
            try
            {
                String s = this.comboBoxChungLoai.Text;
                if (s.Length > 0)
                {
                    foreach (DataRow r in mChungLoaiTB.Rows)
                    {
                        String ss = (String)r[1];
                        if (s.CompareTo(ss) == 0)
                        {
                            int catID = (int)r[0];
                            DataTable dt = DatabaseUtils.getInstance().tbGetThietbiOfChungLoai(catID, 0);

                            DataTable uiTB = new DataTable();

                            uiTB.Columns.Add(C.COL_CODE).DataType = System.Type.GetType("System.String");
                            uiTB.Columns.Add("Chọn").DataType = System.Type.GetType("System.Boolean");

                            foreach (DataRow tr in dt.Rows)
                            {
                                DataRow nr = uiTB.NewRow();
                                String code = (String)tr[1];

                                nr[0] = code;
                                nr[1] = false;

                                uiTB.Rows.Add(nr);
                            }

                            mThietBiSeTraTB = dt;

                            this.dataGridViewThietBiDangMuon.DataSource = null;
                            this.dataGridViewThietBiDangMuon.DataSource = uiTB;
                            this.dataGridViewThietBiDangMuon.Columns[0].ReadOnly = true;

                            disableSortofGridView(this.dataGridViewThietBiDangMuon);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        DataRow getSelectedThietbiWithCode(String code)
        {
            foreach (DataRow r in mSelectedThietbiTB.Rows)
            {
                String code2 = (String)r[C.COL_CODE];
                if (code2.CompareTo(code) == 0)
                {
                    return r;
                }
            }

            return null;
        }

        private void buttonAddToDSTra_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)this.dataGridViewThietBiDangMuon.DataSource;
                foreach (DataRow r in dt.Rows)
                {
                    bool isSelected = (bool)r[1];
                    string code = (String)r[C.COL_CODE];

                    //  remove previous items
                    {
                        DataRow old = getSelectedThietbiWithCode(code);
                        if (old != null)
                        {
                            mSelectedThietbiTB.Rows.Remove(old);
                        }
                    }

                    if (isSelected)
                    {
                        foreach (DataRow r2 in mThietBiSeTraTB.Rows)
                        {
                            String code2 = (String)r2[C.COL_CODE];
                            if (code2.CompareTo(code) == 0)
                            {
                                //DataRow old = getSelectedThietbiWithCode(code);
                                //if (old == null)
                                {
                                    DataRow nr = mSelectedThietbiTB.NewRow();
                                    nr[C.COL_CODE] = r2[C.COL_CODE];
                                    nr[C.COL_SERIAL] = r2[C.COL_SERIAL];
                                    nr[C.COL_TINHTRANG] = r2[C.COL_TINHTRANG];
                                    DataRow muonR = DatabaseUtils.getInstance().tbGetThongtinThietBiMuon(code);
                                    if (muonR != null)
                                        nr[C.COL_THIETBIKEMTHEO] = Utils.getStringOfRow(muonR, "thietbikemtheo");
                                    else
                                        nr[C.COL_THIETBIKEMTHEO] = "";
                                    //nr[C.COL_SUAGANNHAT] = r2[C.COL_SUAGANNHAT];
                                    nr[C.COL_GHICHU] = r2[C.COL_GHICHU];
                                    //nr[C.COL_THIETBIKEMTHEO] = "";

                                    mSelectedThietbiTB.Rows.Add(nr);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void nhapkho(object sender, EventArgs e)
        {
            try
            {
                String nguoitra = this.textBoxNguoitra.Text;
                String nguoinhan = this.textBoxNguoinhan.Text;
                String ghichu_tra = this.textBoxGhichu.Text;
                String bophan_tra = this.comboBoxBoPhanTra.Text;
                DateTime date_tra = this.dateTimePicker1.Value;

                //  validation
                String msg = null;
                if (nguoitra.Length == 0) { msg = "Người trả chưa hợp lệ."; }
                if (nguoinhan.Length == 0) { msg = "Người nhận chưa hợp lệ."; }
                if (bophan_tra.Length == 0) { msg = "Bộ phận chưa hợp lệ."; }

                if (msg != null)
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool error = false;

                DatabaseUtils dbUtils = new DatabaseUtils();
                DataConnect database = dbUtils.getConnection();
                
                database.transStartTransaction();
                List<String> codes = new List<string>();
                List<String> tinhtrangs = new List<string>();
                List<String> thietbikemtheos = new List<string>();
                foreach (DataGridViewRow r in this.dataGridViewThietBiTra.Rows)
                {
                    String code = (String)r.Cells[C.COL_CODE].Value;
                    String tinhtrang_tra = "";
                    if (r.Cells[C.COL_TINHTRANG].Value != DBNull.Value)
                        tinhtrang_tra = (String)r.Cells[C.COL_TINHTRANG].Value;
                    String thietbikemtheo = "";
                    if (r.Cells[C.COL_THIETBIKEMTHEO].Value != DBNull.Value)
                        thietbikemtheo = (String)r.Cells[C.COL_THIETBIKEMTHEO].Value;

                    codes.Add(code);
                    tinhtrangs.Add(tinhtrang_tra);
                    thietbikemtheos.Add(thietbikemtheo);

                    DataRow r2 = DatabaseUtils.getInstance().tbGetThongtinThietBiMuon(code);
                    String tinhtrang_muon = Utils.getStringOfRow(r2, "tinhtrang");
                    DateTime date_muon = Utils.getDateOfRow(r2, "date");
                    String nguoimuon = Utils.getStringOfRow(r2, "nguoimuon");
                    String nguoixuat = Utils.getStringOfRow(r2, "nguoixuat");
                    String bophan_muon = Utils.getStringOfRow(r2, "bophan");
                    String ghichu_muon = Utils.getStringOfRow(r2, "ghichu");

                    bool ok = dbUtils.transTraThietBi(code, nguoimuon, nguoixuat, bophan_muon, date_muon,
                                           tinhtrang_muon, ghichu_muon, nguoitra, nguoinhan, bophan_tra, tinhtrang_tra, date_tra, ghichu_tra, thietbikemtheo);

                    if (!ok)
                    {
                        error = true;
                        break;
                    }
                }
                if (!error)
                {
                    foreach (DataRow r in mSelectedThietbiTB.Rows)
                    {
                        String code = (String)r[C.COL_CODE];
                        bool ok = dbUtils.transTraThietBi_DeleteMUONTHIETBI(code);
                        if (!ok)
                        {
                            error = true;
                            break;
                        }
                    }
                }
                //=======if error, rollback
                if (error)
                {
                    database.transRollback();

                    MessageBox.Show("Lỗi nghiêm trọng không thể cập nhật dữ liệu vào Database!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    database.transCommit();

                    DatabaseUtils.getInstance().createPhieuTraThietbi(nguoitra, bophan_tra, nguoinhan, ghichu_tra, date_tra, codes, tinhtrangs,thietbikemtheos);

                    xuatPhieu();
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void xuatPhieu()
        {
            int i = 0;
            String nguoitra = this.textBoxNguoitra.Text;
            String nguoinhan = this.textBoxNguoinhan.Text;
            String ghichu_tra = this.textBoxGhichu.Text;
            String bophan_tra = this.comboBoxBoPhanTra.Text;
            DateTime date_tra = this.dateTimePicker1.Value;
            DateTime date = this.dateTimePicker1.Value;
            //==========================================
            ExcelExporter excel = new ExcelExporter();
            excel.writeLine("A1", "C1", "CÔNG TY TNHH MTV TĐBĐ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A2", "C2", "XN PHÁT TRIỂN CÔNG NGHỆ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A3", "C3", "TRẮC ĐỊA BẢN ĐỒ", 10, ExcelExporter.ALIGN_CENTER);

            excel.writeLine("D1", "H1", "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("D2", "H2", "Độc lập – Tự do – Hạnh Phúc", 10, ExcelExporter.ALIGN_CENTER, false, true);

            excel.writeLine("A5", "H5", "BIÊN BẢN TRẢ THIẾT BỊ", 10, ExcelExporter.ALIGN_CENTER, true, false);

            String s = "Ngày: " + DateTime.Now.Day + " Tháng: " + DateTime.Now.Month + " Năm: " + DateTime.Now.Year + " Tại Xí nghiệp PTCN Trắc địa Bản đồ";
            excel.writeLine("A7", "H7", s, 10, ExcelExporter.ALIGN_LEFT, true, false);

            //  nguoi xuat
            s = "Họ tên người trả: " + nguoitra;
            excel.writeLine("A9", "H9", s, 10, ExcelExporter.ALIGN_LEFT, true, false);
            excel.writeLine("A10", "H10", "Chức vụ: ", 10, ExcelExporter.ALIGN_LEFT, true, false);
            excel.writeLine("A11", "H11", "Phòng (đội): " + bophan_tra, 10, ExcelExporter.ALIGN_LEFT, true, false);
            //  nguoi nhan
            s = "Họ tên người nhận: " + nguoinhan;
            excel.writeLine("A13", "H13", s, 10, ExcelExporter.ALIGN_LEFT, true, false);
            excel.writeLine("A14", "H14", "Chức vụ: ", 10, ExcelExporter.ALIGN_LEFT, true, false);
            excel.writeLine("A15", "H15", "Phòng (đội): ", 10, ExcelExporter.ALIGN_LEFT, true, false);

            //
            s = "Hai bên thống nhất giao nhận máy,thiết bị gồm:";
            excel.writeLine("A17", "H17", s, 10, ExcelExporter.ALIGN_LEFT, true, false);
            int stt = 19;

            excel.getRange("A19", "AK" + (3 + stt).ToString()).Cells.Font.Name = "Arial";
            excel.getRange("A19", "A" + (3 + stt).ToString()).Cells.ColumnWidth = 5;
            excel.getRange("B19", "B" + (3 + stt).ToString()).Cells.ColumnWidth = 11;
            excel.getRange("C19", "C" + (3 + stt).ToString()).Cells.ColumnWidth = 14;
            excel.getRange("D19", "D" + (3 + stt).ToString()).Cells.ColumnWidth = 12;
            excel.getRange("E19", "E" + (3 + stt).ToString()).Cells.ColumnWidth = 8;
            excel.getRange("F19", "F" + (3 + stt).ToString()).Cells.ColumnWidth = 8;
            excel.getRange("G19", "G" + (3 + stt).ToString()).Cells.ColumnWidth = 8;
            excel.getRange("H19", "G" + (3 + stt).ToString()).Cells.ColumnWidth = 8;
            /*
            excel.eSheet.get_Range("A19", "H21").HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            excel.eSheet.get_Range("A19", "H21").VerticalAlignment = ex.XlHAlign.xlHAlignCenter;

            excel.eSheet.get_Range("D19", "H" + (3 + stt + 3)).HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            excel.eSheet.get_Range("A19", "H" + (3 + stt + 3)).VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
             */

            //  header
            String[] ss = { "STT", "Mã máy", "Serial", "Chủng loại", "Thiết bị\nkèm theo", "Đơn vị\ntính", "Tình trạng\nlúc giao", "Ghi chú" };
            for (i = 0; i < ss.Length; i++)
            {
                char a = (char)('A' + i);
                String b = "" + a + "19";
                String e = "" + a + "21";

                excel.writeLine(b, e, ss[i], 9, ExcelExporter.ALIGN_CENTER, true, false);
            }
            //  fill data to the table
            int cnt = mSelectedThietbiTB.Rows.Count;
            stt += 3;
            i = 0;
            foreach (DataGridViewRow r in this.dataGridViewThietBiTra.Rows)
            {
                String code = (String)r.Cells[C.COL_CODE].Value;

                //  "SELECT IDTB,code,category_id,serial,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung FROM THIETBI where code=@code";
                DataRow device = DatabaseUtils.getInstance().tbGetThietbiWithCode(code);
                excel.ap.Cells[22 + i, 1] = "" + (i + 1);
                excel.ap.Cells[22 + i, 2] = code;
                excel.ap.Cells[22 + i, 3] = (String)device[3];

                int catID = (int)device[2];
                //"SELECT id,category,amount,description,in_store,donvitinh FROM CHUNGLOAI where id="
                DataRow category = DatabaseUtils.getInstance().getChungloaiByChungloaiID(catID);

                if (category != null)
                {
                    excel.ap.Cells[22 + i, 4] = Utils.getStringOfRow(category, 1);    //  chung loai 
                    excel.ap.Cells[22 + i, 6] = Utils.getStringOfRow(category, 5);  //  don vi tinh
                }

                if (r.Cells[C.COL_THIETBIKEMTHEO].Value != DBNull.Value)
                    excel.ap.Cells[22 + i, 5] = (String)r.Cells[C.COL_THIETBIKEMTHEO].Value; //  thiet bi kem theo

                excel.ap.Cells[22 + i, 7] = Utils.getStringOfRow(device, 5);  //  tinh trang luc giao
                excel.ap.Cells[22 + i, 8] = "";

                stt++;
                i++;
            }

            ex.Range line;

            stt--;

            //  format table cell
            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).Font.Size = 9;
            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).VerticalAlignment = ex.XlHAlign.xlHAlignCenter;

            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).Borders.LineStyle = ex.Constants.xlSolid;
            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).RowHeight = 20;
            //===================================

            //  ghi chu
            stt -= 7;
            s = "*Ghi chú: " + ghichu_tra;
            line = excel.writeLine("A" + (8 + stt).ToString(), "H" + (9 + stt).ToString(), s, 10, ExcelExporter.ALIGN_LEFT);
            line.WrapText = true;
            line.Font.Italic = true;

            //  Ky ten nguoi xuat
            line = excel.writeLine("A" + (11 + stt).ToString(), "C" + (11 + stt).ToString(), "Người trả", 10, ExcelExporter.ALIGN_CENTER, true, false);
            line = excel.writeLine("A" + (12 + stt).ToString(), "C" + (12 + stt).ToString(), "Ký và ghi họ tên", 10, ExcelExporter.ALIGN_CENTER, false, false);

            //  Nguoi nhan ky ten
            line = excel.writeLine("E" + (11 + stt).ToString(), "H" + (11 + stt).ToString(), "Người nhận", 10, ExcelExporter.ALIGN_CENTER, true, false);
            line = excel.writeLine("E" + (12 + stt).ToString(), "H" + (12 + stt).ToString(), "Ký và ghi họ tên", 10, ExcelExporter.ALIGN_CENTER, false, false);
            //===================="H" + (3 + stt).ToString()===================
            excel.completeDocument("trathietbi");
        }
        /*
        private void onTinhTrangOfDeviceChanged(object sender, EventArgs e)
        {
            DataRow r = getSelectedRowOfThietBiSeTra();
            if (r != null)
            {
                String s = this.textBoxGhichuStatusOfDevice.Text;
                if (s.Length > 0)
                {
                    r[C.COL_TINHTRANG] = s;
                }
            }
        }
        */
        private void onTinhTrangOfDeviceChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow r = getSelectedRowOfThietBiSeTra();
                if (r != null)
                {
                    /*
                    String s = this.textBoxGhichuStatusOfDevice.Text;
                    s = s.Replace('\r', ' ');
                    s = s.Replace('\n', ' ');
                    s = s.Trim();
                    if (s.Length > 0)
                    {
                        r[C.COL_TINHTRANG] = s;
                        this.textBoxGhichuStatusOfDevice.Text = s;
                    }
                     */
                }
            }
        }

        private void onThietbiDangMuonClick(object sender, MouseEventArgs e)
        {
            DataRow r = getSelectedRowOfThietBiSeTra();

            if (r != null)
            {
                String code = (String)r[C.COL_CODE];

                DataRow thongtin = DatabaseUtils.getInstance().tbGetThongtinThietBiMuon(code);
                if (thongtin != null)
                {
                    String nguoimuon = (String)thongtin[0];
                    String bophan = (String)thongtin[1];
                    String date = Utils.dateToStringOfRow(thongtin, 4);

                    this.labelNgayMuon.Text = date;
                    this.labelNguoiMuon.Text = nguoimuon;
                    this.labelBoPhan.Text = bophan;

                    //this.textBoxGhichuStatusOfDevice.Text = Utils.getStringOfRow(r, C.COL_TINHTRANG);
                }
            }
        }
    }
}
