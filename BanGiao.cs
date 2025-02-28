using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using ex = Microsoft.Office.Interop.Excel;

namespace ProjectStorage
{
    public partial class BanGiao : Office2007Form
    {
        readonly DataConnect _cnn = new DataConnect();
        public BanGiao()
        {
            InitializeComponent();
        }

        private static DataTable _dtChucVu = new DataTable();
        private void Hienthi()
        {
            var cnn = new DataConnect();
            cnn.Mo();
            var cmd = new SqlCommand("select DISTINCT ten from NguoiDung");
            cnn.Doc(cmd);
            DataTable dt = cnn;
            cbHoTen.DataSource = dt;
            cbHoTen.DisplayMember = "ten";
            cbHoTen.ValueMember = "ten";

            var cnn7 = new DataConnect();
            cnn7.Mo();
            var cmd7 = new SqlCommand("select * from phong");
            cnn7.Doc(cmd7);
            DataTable dt7 = cnn7;
            cbPhongDoi.DataSource = dt7;
            cbPhongDoi.DisplayMember = "TenP";
            cbPhongDoi.ValueMember = "TenP";

            var cnn6 = new DataConnect();
            cnn6.Mo();
            var cmd6 = new SqlCommand("select * from chucvu");
            cnn6.Doc(cmd6);
            DataTable dt6 = cnn6;
            cbChucVu.DataSource = dt6;
            cbChucVu.DisplayMember = "TenCVP";
            cbChucVu.ValueMember = "TenCVP";

            var cnn3 = new DataConnect();
            cnn3.Mo();
            var cmd3 = new SqlCommand("select DISTINCT TenCT from congtrinh");
            cnn3.Doc(cmd3);
            DataTable dt3 = cnn3;
            cb_CongTrinh.DataSource = dt3;
            cb_CongTrinh.DisplayMember = "TenCT";
            cb_CongTrinh.ValueMember = "TenCT";

            var cnn1 = new DataConnect();
            cnn1.Mo();
            var cmd1 = new SqlCommand("select DISTINCT ten from NguoiDung");
            cnn1.Doc(cmd1);
            DataTable dt1 = cnn1;
            cbHoTen1.DataSource = dt1;
            cbHoTen1.DisplayMember = "ten";
            cbHoTen1.ValueMember = "ten";

            var cnn5 = new DataConnect();
            cnn5.Mo();
            var cmd5 = new SqlCommand("select * from phong");
            cnn5.Doc(cmd5);
            DataTable dt5 = cnn5;
            cbPhongDoi1.DataSource = dt5;
            cbPhongDoi1.DisplayMember = "TenP";
            cbPhongDoi1.ValueMember = "TenP";

            var cnn4 = new DataConnect();
            cnn4.Mo();
            var cmd4 = new SqlCommand("select * from chucvu");
            cnn4.Doc(cmd4);
            DataTable dt4 = cnn4;
            cbChucVu1.DataSource = dt4;
            cbChucVu1.DisplayMember = "TenCVP";
            cbChucVu1.ValueMember = "TenCVP";

            var cnn2 = new DataConnect();
            cnn2.Mo();
            var cmd2 = new SqlCommand("select * from thietbi WHERE TinhTrang=N'Máy còn trong kho'");
            cnn2.Doc(cmd2);
            DataTable dt2 = cnn2;
            cbTb.DataSource = dt2;
            cbTb.DisplayMember = "ChungLoai";
            cbTb.ValueMember = "MaTB";
            _dtChucVu = dt2;
            //_cnn.Dong();
        }

        public void Export(string hoTenNguoiGiao, string chucVu1, string phong1, string hoTenNguoiNhan, string chucVu2, string phong2, string congtrinh)
        {
            var dt = dtExport;

            int columnsIndex = 0;
            int rowsindex = 0;
            int stt = 0;
            //lấy ra trẻ và chitieets trẻ trong thôn
            //xử lý dữ liệu
            #region thiết kế mẫu
            string[] Char = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK" };
            var ap = new ex.Application();
            var eWork = ap.Workbooks.Add(Type.Missing);

            ex.Sheets sheets = ap.Sheets;
            var eSheet = ((ex._Worksheet)(sheets[1]));

            eSheet.PageSetup.Orientation = ex.XlPageOrientation.xlPortrait;
            eSheet.PageSetup.PaperSize = ex.XlPaperSize.xlPaperA4;

            eSheet.get_Range("A1", "C3").Font.Size = 10;
            eSheet.get_Range("A1", "C1").MergeCells = true;
            eSheet.get_Range("A1", "C1").Value2 = "CÔNG TY TNHH MTV TĐBĐ";

            eSheet.get_Range("A1", "C1").HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("A1", "C1").VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("A2", "C2").MergeCells = true;
            eSheet.get_Range("A2", "C2").Value2 = "XN PHÁT TRIỂN CÔNG NGHỆ";

            eSheet.get_Range("A2", "C2").HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("A2", "C2").VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("A3", "C3").MergeCells = true;
            eSheet.get_Range("A3", "C3").Value2 = "TRẮC ĐỊA BẢN ĐỒ";
            eSheet.get_Range("A3", "C3").HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("A3", "C3").VerticalAlignment = ex.XlHAlign.xlHAlignCenter;


            eSheet.get_Range("D1", "H3").Font.Size = 10;
            eSheet.get_Range("D1", "H1").MergeCells = true;
            eSheet.get_Range("D1", "H1").Value2 = "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            eSheet.get_Range("D1", "H1").HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("D1", "H1").VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("D2", "H2").MergeCells = true;
            eSheet.get_Range("D2", "H2").Value2 = "Độc lập – Tự do – Hạnh Phúc";
            eSheet.get_Range("D2", "H2").Font.Underline = true;
            eSheet.get_Range("D2", "H2").HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("D2", "H2").VerticalAlignment = ex.XlHAlign.xlHAlignCenter;

            eSheet.get_Range("A5", "H5").MergeCells = true;
            eSheet.get_Range("A5", "H5").Value2 = "Biên bản bàn giao";
            eSheet.get_Range("A5", "H5").Font.Bold = true;
            eSheet.get_Range("A5", "H5").HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("A5", "H5").VerticalAlignment = ex.XlHAlign.xlHAlignCenter;


            eSheet.get_Range("A7", "H7").MergeCells = true;
            eSheet.get_Range("A7", "H7").Value2 = "Ngày : " + DateTime.Now.Day + " Tháng: " + DateTime.Now.Month + " Năm: " + DateTime.Now.Year + " Tại Xí nghiệp PTCN Trắc địa Bản đồ";
            eSheet.get_Range("A7", "H7").Font.Bold = true;


            eSheet.get_Range("A8", "H8").MergeCells = true;
            eSheet.get_Range("A8", "H8").Value2 = "Họ Tên người giao:" + hoTenNguoiGiao;
            eSheet.get_Range("A8", "H8").Font.Bold = true;


            eSheet.get_Range("A9", "H9").MergeCells = true;
            eSheet.get_Range("A9", "H9").Value2 = "Chức vụ:" + chucVu1;
            eSheet.get_Range("A9", "H9").Font.Bold = true;


            eSheet.get_Range("A10", "H10").MergeCells = true;
            eSheet.get_Range("A10", "H10").Value2 = "Phòng(đội):" + phong1;
            eSheet.get_Range("A10", "H10").Font.Bold = true;


            eSheet.get_Range("A11", "H11").MergeCells = true;
            eSheet.get_Range("A11", "H11").Value2 = "Họ tên người nhận:" + hoTenNguoiNhan;
            eSheet.get_Range("A11", "H11").Font.Bold = true;

            eSheet.get_Range("A12", "H12").MergeCells = true;
            eSheet.get_Range("A12", "H12").Value2 = "Chức vụ :" + phong1;
            eSheet.get_Range("A12", "H12").Font.Bold = true;

            eSheet.get_Range("A13", "H13").MergeCells = true;
            eSheet.get_Range("A13", "H13").Value2 = "Phòng(đội):" + phong2;
            eSheet.get_Range("A13", "H13").Font.Bold = true;

            eSheet.get_Range("A14", "H14").MergeCells = true;
            eSheet.get_Range("A14", "H14").Value2 = "Hai bên thống nhất giao nhận máy,thiết bị đo đạc gồm:";
            eSheet.get_Range("A14", "H14").Font.Bold = true;

            stt = 16;
            eSheet.Range["A16", "AK" + (3 + stt).ToString()].Cells.Font.Name = "Arial";
            //định dạng cột
            eSheet.Range["A16", "A" + (3 + stt).ToString()].Cells.ColumnWidth = 5;
            eSheet.Range["B16", "B" + (3 + stt).ToString()].Cells.ColumnWidth = 12;
            eSheet.Range["C16", "C" + (3 + stt).ToString()].Cells.ColumnWidth = 14;
            eSheet.Range["D16", "D" + (3 + stt).ToString()].Cells.ColumnWidth = 14;
            eSheet.Range["E16", "E" + (3 + stt).ToString()].Cells.ColumnWidth = 8;
            eSheet.Range["F16", "F" + (3 + stt).ToString()].Cells.ColumnWidth = 8;
            eSheet.Range["G16", "G" + (3 + stt).ToString()].Cells.ColumnWidth = 8;
            eSheet.Range["H16", "H" + (3 + stt).ToString()].Cells.ColumnWidth = 8;


            eSheet.get_Range("A16", "H18").HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("A16", "H18").VerticalAlignment = ex.XlHAlign.xlHAlignCenter;


            eSheet.get_Range("D16", "H" + (3 + stt + 3)).HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("A16", "H" + (3 + stt + 3)).VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.get_Range("A16", "H18").Font.Bold = true;

            eSheet.get_Range("A16", "A18").MergeCells = true;
            eSheet.get_Range("A16", "A18").Value2 = "STT";

            eSheet.get_Range("B16", "B18").MergeCells = true;
            eSheet.get_Range("B16", "B18").Value2 = "Mã máy";


            eSheet.get_Range("C16", "C18").MergeCells = true;
            eSheet.get_Range("C16", "C18").Value2 = "Chủng loại";

            eSheet.get_Range("D16", "D18").MergeCells = true;
            eSheet.get_Range("D16", "D18").Value2 = "Thiết bị kèm theo";
            eSheet.get_Range("D16", "D18").WrapText = true;

            eSheet.get_Range("E16", "E18").MergeCells = true;
            eSheet.get_Range("E16", "E18").Value2 = "Đơn vị tính";
            eSheet.get_Range("E16", "E18").WrapText = true;

            eSheet.get_Range("F16", "F18").MergeCells = true;
            eSheet.get_Range("F16", "F18").Value2 = "Số lượng";
            eSheet.get_Range("F16", "F18").WrapText = true;


            eSheet.get_Range("G16", "G18").MergeCells = true;
            eSheet.get_Range("G16", "G18").Value2 = "Tình trạng lúc giao";
            eSheet.get_Range("G16", "G18").WrapText = true;

            eSheet.get_Range("H16", "H18").MergeCells = true;
            eSheet.get_Range("H16", "H18").Value2 = "Ghi\nchú";
            #endregion
            stt = 18;
            foreach (DataRow row in dt.Rows)
            {
                rowsindex = rowsindex + 1;
                columnsIndex = 0; ++stt;
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != "IDTB")
                    {
                        columnsIndex++;
                        ap.Cells[18 + rowsindex, columnsIndex] = row[col.ColumnName];
                    }
                }
            }

            eSheet.get_Range("A16", "H" + (stt).ToString()).Font.Size = "8";
            eSheet.get_Range("A16", "H" + (stt).ToString()).Borders.LineStyle = ex.Constants.xlSolid;
            eSheet.get_Range("A16", "H" + (stt).ToString()).RowHeight = 20;

            stt--;
            eSheet.Range["A" + (3 + stt).ToString(), "H" + (7 + stt).ToString()].MergeCells = true;
            eSheet.Range["A" + (3 + stt).ToString(), "H" + (7 + stt).ToString()].Font.Bold = true;
            eSheet.Range["A" + (3 + stt).ToString(), "H" + (7 + stt).ToString()].WrapText = true;
            eSheet.Range["A" + (3 + stt).ToString(), "H" + (7 + stt).ToString()].Value2 = @"Mục đích sử dụng: sử dụng cho công trình " + congtrinh + " .Trong quá trình sử dụng người được giao máy, thiết bị đo đạc phải có trách nhiệm bảo quản máy và thiết bị đo đạc theo đúng qui trình qui định . Nếu hư hỏng thì phải bồi thường theo qui định .";


            eSheet.Range["A" + (8 + stt).ToString(), "C" + (8 + stt).ToString()].MergeCells = true;
            eSheet.Range["A" + (8 + stt).ToString(), "C" + (8 + stt).ToString()].Font.Bold = true;
            eSheet.Range["A" + (8 + stt).ToString(), "C" + (8 + stt).ToString()].Value2 = @"Người giao";
            eSheet.Range["A" + (8 + stt).ToString(), "C" + (8 + stt).ToString()].HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["A" + (8 + stt).ToString(), "C" + (8 + stt).ToString()].VerticalAlignment = ex.XlHAlign.xlHAlignCenter;


            eSheet.Range["A" + (9 + stt).ToString(), "C" + (9 + stt).ToString()].HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["A" + (9 + stt).ToString(), "C" + (9 + stt).ToString()].VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["A" + (9 + stt).ToString(), "C" + (9 + stt).ToString()].MergeCells = true;
            eSheet.Range["A" + (9 + stt).ToString(), "C" + (9 + stt).ToString()].Value2 = @"Ký ghi họ tên";

            eSheet.Range["E" + (8 + stt).ToString(), "H" + (8 + stt).ToString()].MergeCells = true;
            eSheet.Range["E" + (8 + stt).ToString(), "H" + (8 + stt).ToString()].Font.Bold = true;
            eSheet.Range["E" + (8 + stt).ToString(), "H" + (8 + stt).ToString()].Value2 = @"Người nhận";
            eSheet.Range["E" + (8 + stt).ToString(), "H" + (8 + stt).ToString()].HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["E" + (8 + stt).ToString(), "H" + (8 + stt).ToString()].VerticalAlignment = ex.XlHAlign.xlHAlignCenter;


            eSheet.Range["E" + (9 + stt).ToString(), "H" + (9 + stt).ToString()].HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["E" + (9 + stt).ToString(), "H" + (9 + stt).ToString()].VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["E" + (9 + stt).ToString(), "H" + (9 + stt).ToString()].MergeCells = true;
            eSheet.Range["E" + (9 + stt).ToString(), "H" + (9 + stt).ToString()].Value2 = @"Ký ghi họ tên";


            eSheet.Range["A" + (13 + stt).ToString(), "H" + (13 + stt).ToString()].HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["A" + (13 + stt).ToString(), "H" + (13 + stt).ToString()].VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["A" + (13 + stt).ToString(), "H" + (13 + stt).ToString()].MergeCells = true;
            eSheet.Range["A" + (13 + stt).ToString(), "H" + (13 + stt).ToString()].Value2 = @"Trả máy : hôm nay, ngày:" + DateTime.Now.Day + " Tháng: " + DateTime.Now.Month + " Năm: " + DateTime.Now.Year;
            eSheet.Range["E" + (13 + stt).ToString(), "H" + (13 + stt).ToString()].Font.Bold = true;

            eSheet.Range["A" + (15 + stt).ToString(), "C" + (15 + stt).ToString()].MergeCells = true;
            eSheet.Range["A" + (15 + stt).ToString(), "C" + (15 + stt).ToString()].Font.Bold = true;
            eSheet.Range["A" + (15 + stt).ToString(), "C" + (15 + stt).ToString()].Value2 = @"Người trả máy";
            eSheet.Range["A" + (15 + stt).ToString(), "C" + (15 + stt).ToString()].HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["A" + (15 + stt).ToString(), "C" + (15 + stt).ToString()].VerticalAlignment = ex.XlHAlign.xlHAlignCenter;


            eSheet.Range["A" + (16 + stt).ToString(), "C" + (16 + stt).ToString()].HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["A" + (16 + stt).ToString(), "C" + (16 + stt).ToString()].VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["A" + (16 + stt).ToString(), "C" + (16 + stt).ToString()].MergeCells = true;
            eSheet.Range["A" + (16 + stt).ToString(), "C" + (16 + stt).ToString()].Value2 = @"Ký ghi họ tên";

            eSheet.Range["E" + (15 + stt).ToString(), "H" + (15 + stt).ToString()].MergeCells = true;
            eSheet.Range["E" + (15 + stt).ToString(), "H" + (15 + stt).ToString()].Font.Bold = true;
            eSheet.Range["E" + (15 + stt).ToString(), "H" + (15 + stt).ToString()].Value2 = @"Người nhận";
            eSheet.Range["E" + (15 + stt).ToString(), "H" + (15 + stt).ToString()].HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["E" + (15 + stt).ToString(), "H" + (15 + stt).ToString()].VerticalAlignment = ex.XlHAlign.xlHAlignCenter;


            eSheet.Range["E" + (16 + stt).ToString(), "H" + (16 + stt).ToString()].HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["E" + (16 + stt).ToString(), "H" + (16 + stt).ToString()].VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            eSheet.Range["E" + (16 + stt).ToString(), "H" + (16 + stt).ToString()].MergeCells = true;
            eSheet.Range["E" + (16 + stt).ToString(), "H" + (16 + stt).ToString()].Value2 = @"Ký ghi họ tên";

            eSheet.Activate();
            eSheet.Name = "BienBanBanGiao";
            System.Runtime.InteropServices.Marshal.ReleaseComObject(eWork);
            eSheet.Activate();
            ap.Visible = true;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(eWork);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ap);


        }

        private void load()
        {
            Hienthi();
        }
        private void FrmLopLoad(object sender, EventArgs e)
        {
            load();
        }

        private void ButtonX1Click(object sender, EventArgs e)
        {
            //get list
            _cnn.Mo();
            var cmd2 = new SqlCommand("Select * from thietbi");
            _cnn.Doc(cmd2);
            DataTable dtOld = _cnn;
            foreach (DataRow drOld in dtOld.Rows)
            {
                foreach (DataRow dr in dtExport.Rows)
                {
                    if (dr["IDTB"].ToString().Trim() == drOld["IDTB"].ToString().Trim())
                    {
                        //Insert
                        var nSoLuong = int.Parse(txt_SL.Text);
                        //var soluongcon = int.Parse(txt_SLHC.Text) - int.Parse(txt_SL.Text);
                        const string nTinhTrang = "Đang Đi Đo";
                        _cnn.Mo();
                        var cmd =
                            new SqlCommand(
                                "INSERT INTO thietbi VALUES(@MaTB,@ChungLoai,@PhuKien,@DonVi,@SoLuong,@TinhTrang,@GhiChu,@HienTrangMay)");
                        cmd.Parameters.Add("MaTB", SqlDbType.NVarChar).Value = dr["MaTB"];
                        cmd.Parameters.Add("ChungLoai", SqlDbType.NVarChar).Value = dr["ChungLoai"];
                        cmd.Parameters.Add("PhuKien", SqlDbType.NVarChar).Value = dr["PhuKien"];
                        cmd.Parameters.Add("DonVi", SqlDbType.NVarChar).Value = dr["DonVi"];
                        cmd.Parameters.Add("SoLuong", SqlDbType.NVarChar).Value = dr["SoLuong"];
                        cmd.Parameters.Add("TinhTrang", SqlDbType.NVarChar).Value = nTinhTrang;
                        cmd.Parameters.Add("GhiChu", SqlDbType.NVarChar).Value = dr["GhiChu"];
                        cmd.Parameters.Add("HienTrangMay", SqlDbType.NVarChar).Value = dr["HienTrangMay"];
                        _cnn.ThucThi(cmd);

                        //update
                        var soluongcon = int.Parse(drOld["SoLuong"].ToString()) - int.Parse(dr["SoLuong"].ToString());
                        const string nTinhTrang1 = "Máy Còn Trong Kho";
                        _cnn.Mo();
                        var cmd1 =
                            new SqlCommand(
                                "UPDATE thietbi SET SoLuong=@SoLuong,TinhTrang=@TinhTrang WHERE IDTB=@IDTB");
                        cmd1.Parameters.Add("IDTB", SqlDbType.NVarChar).Value = dr["IDTB"];
                        cmd1.Parameters.Add("SoLuong", SqlDbType.NVarChar).Value = soluongcon;
                        cmd1.Parameters.Add("TinhTrang", SqlDbType.NVarChar).Value = nTinhTrang1;
                        _cnn.ThucThi(cmd1);
                    }
                }
            }
           
         //   InsertData();
          //  UpdateDaTa();
            if (dgv_ThietBi.DataSource != null)
            {
                var ten = cbHoTen.Text;
                var chucvu = cbChucVu.Text;
                var phongdoi = cbPhongDoi.Text;
                var ten1 = cbHoTen1.Text;
                var chucvu1 = cbChucVu1.Text;
                var phongdoi1 = cbPhongDoi1.Text;
                var congtrinh = cb_CongTrinh.Text;
                Export(ten, chucvu, phongdoi, ten1, chucvu1, phongdoi1, congtrinh);
            }
            else
            {
                MessageBoxEx.Show("Bạn Chưa chọn thiết bị nào", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static DataTable dtExport = new DataTable();
        private Boolean _ok = false;
        private void ButtonX3Click(object sender, EventArgs e)
        {
            if (txt_SL.Text != "")
            {
                var soluong = int.Parse(txt_SL.Text);
                var soluongco = int.Parse(txt_SLHC.Text);
                var soluongcon = soluongco - soluong;
                if (soluongcon >= 0)
                {
                    var cnn2 = new DataConnect();
                    cnn2.Mo();
                    var nChungLoai = cbTb.Text;
                    var cmd1 =
                        new SqlCommand(
                            "select ROW_NUMBER() OVER(ORDER BY MaTB) AS TT,IDTB,MaTB,ChungLoai,PhuKien,DonVi,SoLuong,HienTrangMay,GhiChu from thietbi Where ChungLoai=@ChungLoai ");
                    cmd1.Parameters.Add("ChungLoai", SqlDbType.NVarChar).Value = nChungLoai;
                    cnn2.Doc(cmd1);

                    DataTable dt1 = cnn2;
                    if (!_ok)
                    {
                        dtExport.Clear();
                        dtExport.Columns.Clear();
                        dtExport.Columns.Add("IDTB").DataType = Type.GetType("System.String");
                        dtExport.Columns.Add("TT").DataType = Type.GetType("System.String");
                        dtExport.Columns.Add("MaTB").DataType = Type.GetType("System.String");
                        dtExport.Columns.Add("ChungLoai").DataType = Type.GetType("System.String");
                        dtExport.Columns.Add("PhuKien").DataType = Type.GetType("System.String");
                        dtExport.Columns.Add("DonVi").DataType = Type.GetType("System.String");
                        dtExport.Columns.Add("SoLuong").DataType = Type.GetType("System.String");
                        dtExport.Columns.Add("HienTrangMay").DataType = Type.GetType("System.String");
                        dtExport.Columns.Add("GhiChu").DataType = Type.GetType("System.String");
                        _ok = true;
                    }
                    foreach (DataRow drItem in dt1.Rows)
                    {
                        var isCheck = false;
                        foreach (DataRow drOld in dtExport.Rows)
                        {
                            if (drItem["MaTB"].ToString().Trim() == drOld["MaTB"].ToString().Trim())
                            {
                                isCheck = true;
                            }
                        }
                        if (isCheck) continue;
                        var drNew = dtExport.NewRow();
                        drNew["IDTB"] = drItem["IDTB"];
                        drNew["TT"] = drItem["TT"];
                        drNew["MaTB"] = drItem["MaTB"];
                        drNew["ChungLoai"] = drItem["ChungLoai"];
                        drNew["PhuKien"] = drItem["PhuKien"];
                        drNew["DonVi"] = drItem["DonVi"];
                        drNew["SoLuong"] = txt_SL.Text;
                        drNew["HienTrangMay"] = drItem["HienTrangMay"];
                        drNew["GhiChu"] = drItem["GhiChu"];
                        dtExport.Rows.Add(drNew);
                    }
                    dgv_ThietBi.DataSource = dtExport;
                    cnn2.Dong();
                }
                else
                {
                    MessageBoxEx.Show("Số lượng vượt quá số lượng hiện có", "Thông Báo", MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBoxEx.Show("Bạn chưa chọn số lượng", "Thông Báo", MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
            }
        }

        private void CbTbSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow drtem in _dtChucVu.Rows)
            {
                if (drtem["MaTB"] == cbTb.SelectedValue)
                {
                    txt_SLHC.Text = drtem["SoLuong"].ToString();
                }
            }
        }

        private void BtnHuyClick(object sender, EventArgs e)
        {
            dgv_ThietBi.DataSource = null;
        }
    }
}