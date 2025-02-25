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
    public partial class FormXuatKho : XtraForm
    {
        DataTable mChungLoaiTB;
        DataTable mThietbiTrongKhoTB;
        DataTable mSelectedThietbiTB;

        public FormXuatKho()
        {
            InitializeComponent();

            DataTable tb2 = new DataTable();

            tb2.Columns.Add(C.COL_CODE).DataType = Type.GetType("System.String");
            tb2.Columns.Add("category_id").DataType = Type.GetType("System.Int32");
            tb2.Columns.Add(C.COL_SERIAL).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_TINHTRANG).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_GHICHU).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_THIETBIKEMTHEO).DataType = Type.GetType("System.String");

            mSelectedThietbiTB = tb2;
            this.dataGridView1.DataSource = tb2;
            this.dataGridView1.Columns["category_id"].Visible = false;

            this.dateTimePicker1.Value = DateTime.Now;

            List<String> congtrinh = DatabaseUtils.getInstance().GetListProject2();
            this.comboBoxProject.DataSource = congtrinh;

            List<String> bophan = DatabaseUtils.getInstance().getTeamList();
            this.comboBoxBoPhan.DataSource = bophan;

            var permissionMuonThietBi = Mdl_Share.currentPermission.Where(x => x.MaCT == "TB2").FirstOrDefault();
            buttonXuatkho.Enabled = permissionMuonThietBi.add;
            buttonAddToList.Enabled = permissionMuonThietBi.add;
        }

        private void onLoad(object sender, EventArgs e)
        {
            refreshForm();
        }

        void refreshForm()
        {
            DataTable tb = DatabaseUtils.getInstance().tbGetChungLoai();
            mChungLoaiTB = tb;

            List<String> list = new List<string>();

            foreach (DataRow r in tb.Rows)
            {
                String s = (String)r[1];
                list.Add(s);
            }

            this.comboBoxChungLoai.DataSource = list;

            onSelectChungLoai(null, null);
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
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = dataGridView1.SelectedRows[0];
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

        private void buttonAddToList_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)this.dataGridViewThietBiTrongKho.DataSource;
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
                        foreach (DataRow r2 in mThietbiTrongKhoTB.Rows)
                        {
                            String code2 = (String)r2[C.COL_CODE];
                            if (code2.CompareTo(code) == 0)
                            {
                                //DataRow old = getSelectedThietbiWithCode(code);
                                //if (old == null)
                                {
                                    DataRow nr = mSelectedThietbiTB.NewRow();
                                    nr[C.COL_CODE] = r2[C.COL_CODE];
                                    nr["category_id"] = r2["cat_id"];
                                    nr[C.COL_SERIAL] = r2[C.COL_SERIAL];
                                    nr[C.COL_TINHTRANG] = r2[C.COL_TINHTRANG];

                                    //  lay thiet bi kem theo
                                    nr[C.COL_THIETBIKEMTHEO] = Utils.getStringOfRow(r2, C.COL_THIETBIKEMTHEO);
                                    nr[C.COL_GHICHU] = r2[C.COL_GHICHU];

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

        private void onSelectChungLoai(object sender, EventArgs e)
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
                            DataTable dt = DatabaseUtils.getInstance().tbGetThietbiOfChungLoai(catID, 1);

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

                            mThietbiTrongKhoTB = dt;

                            this.dataGridViewThietBiTrongKho.DataSource = null;
                            this.dataGridViewThietBiTrongKho.DataSource = uiTB;
                            this.dataGridViewThietBiTrongKho.Columns[0].ReadOnly = true;

                            disableSortofGridView(this.dataGridViewThietBiTrongKho);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }

        private void xuatkho(object sender, EventArgs e)
        {
            try{
                String nguoimuon = this.textBoxNguoiMuon.Text;
                String nguoixuat = this.textBoxNguoiXuat.Text;
                String ghichu = this.textBoxGhichu.Text;
                String bophan = this.comboBoxBoPhan.Text;
                String congtrinh = this.comboBoxProject.Text;
                DateTime date = this.dateTimePicker1.Value;

                //  validation
                String msg = null;
                if (nguoimuon.Length == 0) { msg = "Người mượn chưa hợp lệ.";}
                if (nguoixuat.Length == 0) { msg = "Người xuất chưa hợp lệ.";}
                if (bophan.Length == 0) { msg = "Bộ phận chưa hợp lệ.";}
                if (congtrinh.Length == 0) { msg = "Công trình chưa hợp lệ."; }

                congtrinh = DatabaseUtils.getInstance().getMaCTByName(congtrinh);
                if (congtrinh == null)    
                { msg = "Công trình chưa hợp lệ."; }


                if (msg != null)
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool error = false;
                DatabaseUtils db = new DatabaseUtils();
                db.getConnection().transStartTransaction();
                List<String> codes = new List<string>();
                List<String> tinhtrangs = new List<string>();
                List<String> thietbikemtheos = new List<string>();

                List<int> catIDs = new List<int>();

                foreach (DataGridViewRow r in this.dataGridView1.Rows)
                {
                    String code = (String)r.Cells[C.COL_CODE].Value;
                    String thietbikemtheo = (String)r.Cells[C.COL_THIETBIKEMTHEO].Value;
                    int catID = (int)r.Cells[1].Value;

                    if (!db.transInsertMuonThietBi(code, catID, nguoimuon, bophan, congtrinh, ghichu, nguoixuat, date, thietbikemtheo))
                    {
                        error = true;
                        break;
                    }
                    codes.Add(code);
                    tinhtrangs.Add("");
                    thietbikemtheos.Add(thietbikemtheo);
                }

                //=======if error, rollback
                if (error)
                {
                    db.getConnection().transRollback();

                    MessageBox.Show("Lỗi nghiêm trọng không thể cập nhật dữ liệu vào Database!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    db.getConnection().transCommit();

                    db.transCreatePhieuMuonThietBi(nguoimuon, bophan, nguoixuat, congtrinh, ghichu, date, codes, tinhtrangs, thietbikemtheos);
                    //  xuat phiet
                    xuatPhieu();
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
            }
        }

        void xuatPhieu()
        {
            String nguoimuon = this.textBoxNguoiMuon.Text;
            String nguoixuat = this.textBoxNguoiXuat.Text;
            String ghichu = this.textBoxGhichu.Text;
            String bophan = this.comboBoxBoPhan.Text;
            String congtrinh = this.comboBoxProject.Text;
            DateTime date = this.dateTimePicker1.Value;
            //==========================================
            ExcelExporter excel = new ExcelExporter();
            excel.writeLine("A1", "C1", "CÔNG TY TNHH MTV TĐBĐ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A2", "C2", "XN PHÁT TRIỂN CÔNG NGHỆ", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("A3", "C3", "TRẮC ĐỊA BẢN ĐỒ", 10, ExcelExporter.ALIGN_CENTER);

            excel.writeLine("D1", "H1", "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM", 10, ExcelExporter.ALIGN_CENTER);
            excel.writeLine("D2", "H2", "Độc lập – Tự do – Hạnh Phúc", 10, ExcelExporter.ALIGN_CENTER, false, true);

            excel.writeLine("A5", "H5", "BIÊN BẢN MƯỢN THIẾT BỊ", 10, ExcelExporter.ALIGN_CENTER, true, false);

            String s = "Ngày: " + DateTime.Now.Day + " Tháng: " + DateTime.Now.Month + " Năm: " + DateTime.Now.Year + " Tại Xí nghiệp PTCN Trắc địa Bản đồ";
            excel.writeLine("A7", "H7", s, 10, ExcelExporter.ALIGN_LEFT, true, false);

            //  nguoi xuat
            s = "Họ tên người giao: " + nguoixuat;
            excel.writeLine("A9", "H9", s, 10, ExcelExporter.ALIGN_LEFT, true, false);
            excel.writeLine("A10", "H10", "Chức vụ: ", 10, ExcelExporter.ALIGN_LEFT, true, false);
            excel.writeLine("A11", "H11", "Phòng (đội): ", 10, ExcelExporter.ALIGN_LEFT, true, false);
            //  nguoi nhan
            s = "Họ tên người nhận: " + nguoimuon;
            excel.writeLine("A13", "H13", s, 10, ExcelExporter.ALIGN_LEFT, true, false);
            excel.writeLine("A14", "H14", "Chức vụ: ", 10, ExcelExporter.ALIGN_LEFT, true, false);
            excel.writeLine("A15", "H15", "Phòng (đội): " + bophan, 10, ExcelExporter.ALIGN_LEFT, true, false);

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
            String[] ss = {"STT", "Mã máy", "Serial", "Chủng loại", "Thiết bị\nkèm theo", "Đơn vị\ntính", "Tình trạng\nlúc giao", "Ghi chú"};
            int i = 0;
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
            foreach (DataGridViewRow vr in this.dataGridView1.Rows)
            {
                String code = (String)vr.Cells[C.COL_CODE].Value;

                //  "SELECT IDTB,code,category_id,serial,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung FROM THIETBI where code=@code";
                DataRow device = DatabaseUtils.getInstance().tbGetThietbiWithCode(code);
                excel.ap.Cells[22 + i, 1] = "" + (i+1);
                excel.ap.Cells[22 + i, 2] = code;
                excel.ap.Cells[22 + i, 3] = (String)device[3];

                int catID = (int)device["category_id"];
                //"SELECT id,category,amount,description,in_store,donvitinh FROM CHUNGLOAI where id="
                DataRow category = DatabaseUtils.getInstance().getChungloaiByChungloaiID(catID);

                if (category != null)
                {
                    excel.ap.Cells[22 + i, 4] = (String)category[1];    //  chung loai 
                    excel.ap.Cells[22 + i, 6] = Utils.getStringOfRow(category, 5);  //  don vi tinh
                }

                excel.ap.Cells[22 + i, 5] = vr.Cells[C.COL_THIETBIKEMTHEO].Value; //  thiet bi kem theo

                excel.ap.Cells[22 + i, 7] = (String)device[5];  //  tinh trang luc giao
                excel.ap.Cells[22 + i, 8] = "";

                stt++;
                i++;
            }

            ex.Range line;

            stt--;

            //  format table cell
            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).Font.Size = 9;
            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).WrapText = true;
            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).VerticalAlignment = ex.XlHAlign.xlHAlignCenter;

            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).Borders.LineStyle = ex.Constants.xlSolid;
            excel.eSheet.get_Range("A19", "H" + (stt).ToString()).RowHeight = 20;
            //===================================

            s = "Mục đích sử dụng: " + congtrinh;
            line = excel.writeLine("A" + (3 + stt).ToString(), "H" + (4 + stt).ToString(), s, 10, ExcelExporter.ALIGN_LEFT | ExcelExporter.ALIGN_VCENTER, true, false);
            line.WrapText = true;

            s = "Trong quá trình sử dụng người được giao máy, thiết bị đo đạc phải có trách"
                +" nhiệm bảo quản máy và thiết bị đo đạc theo đúng qui trình qui định . Nếu hư"
                +" hỏng thì phải bồi thường theo qui định.";

            line = excel.writeLine("A" + (5 + stt).ToString(), "H" + (7 + stt).ToString(), s, 10, ExcelExporter.ALIGN_LEFT | ExcelExporter.ALIGN_VCENTER, true, false);
            line.WrapText = true;

            //  ghi chu
            s = "*Ghi chú: " + ghichu;
            line = excel.writeLine("A" + (8 + stt).ToString(), "H" + (9 + stt).ToString(), s, 10, ExcelExporter.ALIGN_LEFT);
            line.WrapText = true;
            line.Font.Italic = true;

            //  Ky ten nguoi xuat
            line = excel.writeLine("A" + (11 + stt).ToString(), "C" + (11 + stt).ToString(), "Người giao", 10, ExcelExporter.ALIGN_CENTER, true, false);
            line = excel.writeLine("A" + (12 + stt).ToString(), "C" + (12 + stt).ToString(), "Ký và ghi họ tên", 10, ExcelExporter.ALIGN_CENTER, false, false);

            //  Nguoi nhan ky ten
            line = excel.writeLine("E" + (11 + stt).ToString(), "H" + (11 + stt).ToString(), "Người nhận", 10, ExcelExporter.ALIGN_CENTER, true, false);
            line = excel.writeLine("E" + (12 + stt).ToString(), "H" + (12 + stt).ToString(), "Ký và ghi họ tên", 10, ExcelExporter.ALIGN_CENTER, false, false);
            //===================="H" + (3 + stt).ToString()===================
            excel.completeDocument("muonthietbi");
        }

        private void onThietbiTrongKhoClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.dataGridViewThietBiTrongKho.SelectedRows.Count > 0)
                {
                    this.textBoxDesc.Text = "";

                    int index = this.dataGridViewThietBiTrongKho.SelectedRows[0].Index;

                    DataRow r = mThietbiTrongKhoTB.Rows[index];

                    String desc = (String)r[6];

                    this.textBoxDesc.Text = desc;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
