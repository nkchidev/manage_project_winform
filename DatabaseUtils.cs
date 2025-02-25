using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using ex = Microsoft.Office.Interop.Excel;

namespace ProjectStorage
{
    internal class DatabaseUtils
    {
        private static DatabaseUtils mInstance = null;
        private DataConnect mConnection;

        public static DatabaseUtils getInstance()
        {
            if (mInstance == null)
            {
                mInstance = new DatabaseUtils();
            }

            return mInstance;
        }

        public DataConnect getConnection()
        {
            return mConnection;
        }

        public DatabaseUtils()
        {
            mConnection = new DataConnect();
            mConnection.Mo();
        }

        public void closeDatabase()
        {
            mConnection.Dong();
            mConnection = null;
            mInstance = null;
        }

        //===============================
        public List<String> getTeamList()
        {
            var cmd = new SqlCommand("select TenP from PHONG");
            DataTable dt = mConnection.queryTable(cmd);

            List<String> l = new List<String>(5);
            foreach (DataRow r in dt.Rows)
            {
                String s = (String)r["tenP"];

                l.Add(s);
            }

            return l;
        }

        public DataTable getPhongs()
        {
            var cmd = new SqlCommand("select MaP,TenP as 'Phòng/Đội',mota as 'Ghi chú' from PHONG");
            DataTable dt = mConnection.queryTable(cmd);

            return dt;
        }

        public bool insertPhong(String ten, String mota)
        {
            var cmd = new SqlCommand("insert into PHONG(TenP,mota) values(@ten,@mota)");
            cmd.Parameters.Add("ten", SqlDbType.NVarChar).Value = ten;
            cmd.Parameters.Add("mota", SqlDbType.NVarChar).Value = mota;

            return mConnection.execute(cmd);
        }

        public bool updatePhong(int id, String ten, String mota)
        {
            var cmd = new SqlCommand("update PHONG set TenP=@ten, mota=@mota where MaP=@id");

            cmd.Parameters.Add("ten", SqlDbType.NVarChar).Value = ten;
            cmd.Parameters.Add("mota", SqlDbType.NVarChar).Value = mota;
            cmd.Parameters.Add("id", SqlDbType.Int).Value = id;

            return mConnection.execute(cmd);
        }

        public bool deletePhong(int id)
        {
            var cmd = new SqlCommand("delete PHONG where MaP=@id");

            cmd.Parameters.Add("id", SqlDbType.Int).Value = id;

            return mConnection.execute(cmd);
        }

        //====================================================
        public String getMaCTByName(String tenct)
        {
            String sql = "select MaCT from CONGTRINH where TenCT=@tenct";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("tenct", SqlDbType.NVarChar).Value = tenct;

            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Utils.getStringOfRow(dt.Rows[0], 0);
            }
            return null;
        }

        public bool isProjectExistWithMaCT(String maCT)
        {
            String sql = "select MaCT from CONGTRINH where MaCT=@MaCT";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("MaCT", SqlDbType.NVarChar).Value = maCT;

            DataTable dt = mConnection.queryTable(cmd);

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                return true;

            return false;
        }

        public bool isProjectExistWithName(String tenct, String excludeMaCT)
        {
            String sql = "select MaCT from CONGTRINH where TenCT=@tenct";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("tenct", SqlDbType.NVarChar).Value = tenct;

            DataTable dt = mConnection.queryTable(cmd);

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                if (excludeMaCT != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        String maCT = Utils.getStringOfRow(r, 0);
                        if (maCT.CompareTo(excludeMaCT) == 0)
                            return true;
                    }
                    return false;
                }
                return true;
            }

            return false;
        }

        public DataTable GetListProject(String startYear, bool isThongKe = true, bool is_formatNumber = false, int isAnHien = 1)
        {
            var cnn2 = mConnection;

            String yearCondition = "";
            if (startYear == "*") { startYear = null; }
            if (startYear != null && startYear.Length > 0)
            {
                yearCondition = " where start_date like '%" + startYear + "'";
            }

            string sql = "";

            if (isThongKe)
            {
                if (!is_formatNumber)
                {
                    sql = "SELECT MaCT as 'Mã Dự án'"
                                + " ,TenCT as 'Tên Dự án'"
                                + " ,sohopdong as 'Số hợp đồng'"
                                + ",giatrihopdong as 'Giá trị hợp đồng'"
                                + " ,(select sum(chi_phi) from PHIEUGIAONHAN WHERE PHIEUGIAONHAN.MaCT=t.MACT) as 'Tổng khoán'"
                                + " ,(select sum(da_thanh_toan) from PHIEUGIAONHAN WHERE PHIEUGIAONHAN.MaCT=t.MACT) as 'Đã thanh toán'"
                                + " ,chudautu as 'Chủ đầu tư'"
                                + " ,mo_ta as 'Ghi chú'"
                                + " ,start_date as 'Ngày bắt đầu'"
                                + " ,trangthai as 'Trạng thái'"
                            + " FROM [dbo].[CONGTRINH] as t"
                                + yearCondition
                                + " ORDER BY t.ngay_ky DESC";
                }
                else
                {
                    sql = "SELECT MaCT as 'Mã Dự án'"
                            + " ,TenCT as 'Tên Dự án'"
                            + " ,sohopdong as 'Số hợp đồng'"
                            + ",giatrihopdong as 'Giá trị hợp đồng'"
                            + " , FORMAT((select sum(chi_phi) from PHIEUGIAONHAN WHERE PHIEUGIAONHAN.MaCT=t.MACT), '#,#') as 'Tổng khoán'"
                            + " ,FORMAT((select sum(da_thanh_toan) from PHIEUGIAONHAN WHERE PHIEUGIAONHAN.MaCT=t.MACT), '#,#') as 'Đã thanh toán'"
                            + " ,chudautu as 'Chủ đầu tư'"
                            + " ,mo_ta as 'Ghi chú'"
                            + " ,start_date as 'Ngày bắt đầu'"
                            + " ,trangthai as 'Trạng thái'"
                        + " FROM [dbo].[CONGTRINH] as t"
                            + yearCondition
                            + " ORDER BY t.ngay_ky DESC";
                }
            }
            else
            {
                sql = @"SELECT t.MaCT,
                            t.TenCT,
                            t.ngay_ky,
                            t.ngay_hoan_thanh,
                            t.trangthai,
                            t.giatrihopdong,
                            t.giatringhiemthu,
                            DATEDIFF(DAY, GETDATE(), t.ngay_hoan_thanh) AS songay,
                            t.trangthai AS GhiChu,
                            t.sohopdong,
                            t.giatrihopdong,
                            (SELECT SUM(chi_phi) FROM PHIEUGIAONHAN WHERE PHIEUGIAONHAN.MaCT = t.MACT) AS 'TongKhoan',
                            (SELECT SUM(da_thanh_toan) FROM PHIEUGIAONHAN WHERE PHIEUGIAONHAN.MaCT = t.MACT) AS 'DaThanhToan',
                            chudautu, t.NhomCT,
                            t.start_date AS 'ngaybatdau',
                            t.finish_date AS 'ngayhoanthanh',
                            nhom.TenNhom,
                            t.anhien
                        FROM[dbo].[CONGTRINH] AS t
                        LEFT JOIN nhomcongtrinh nhom ON t.NhomCT = nhom.id 
                        WHERE t.anhien = " + isAnHien +
                        " ORDER BY t.ngay_ky DESC";
            }

            var cmd = new SqlCommand(sql);

            DataTable dt1 = cnn2.queryTable(cmd);

            return dt1;
        }

        public DataTable getPhieuGiaoViecOfProject(String MaCT)
        {
            var cnn2 = mConnection;

            String sql = "SELECT phieu_id, MaCT"
                            + " ,phieu_no"
                          + " ,doi_thi_cong"
                        + " ,chi_phi"
                        + " ,da_thanh_toan"
                          + " ,phieu_desc"
                      + " FROM [dbo].[PHIEUGIAONHAN]"
                      + " WHERE MaCT=@MaCT";

            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("MaCT", SqlDbType.NVarChar).Value = MaCT;

            DataTable dt1 = cnn2.queryTable(cmd);

            return dt1;
        }

        public int getCachTinhThanhTienOfPhieu(int phieu_id)
        {
            String sql = "select kieu_tinh_chi_phi FROM [dbo].[PHIEUGIAONHAN] where phieu_id=@phieu_id";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;

            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                return Utils.getIntValueOfRow(dt.Rows[0], 0);
            }

            return 0;
        }

        public DataTable getPhieuGiaoViecOfByPhieuID(int phieuID)
        {
            var cnn2 = mConnection;

            String sql = "SELECT phieu_id, MaCT"
                            + " ,phieu_no"
                          + " ,doi_thi_cong"
                        + " ,chi_phi"
                        + " ,da_thanh_toan"
                          + " ,phieu_desc"
                      + " FROM [dbo].[PHIEUGIAONHAN]"
                      + " WHERE phieu_id=@phieuID";

            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("phieuID", SqlDbType.Int).Value = phieuID;

            DataTable dt1 = cnn2.queryTable(cmd);

            return dt1;
        }

        public int getPhieuID(String mact, String phieuNo)
        {
            var cnn2 = mConnection;

            String sql = "SELECT phieu_id"
                      + " FROM [dbo].[PHIEUGIAONHAN]"
                      + " WHERE MaCT=@mact and phieu_no=@phieuNo";

            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("mact", SqlDbType.NVarChar).Value = mact;
            cmd.Parameters.Add("phieuNo", SqlDbType.NVarChar).Value = phieuNo;

            DataTable dt1 = cnn2.queryTable(cmd);
            if (dt1 != null && dt1.Rows != null && dt1.Rows.Count > 0)
            {
                int id = Utils.getIntValueOfRow(dt1.Rows[0], 0);
                return id;
            }

            return 0;
        }

        public List<String> GetListProject2()
        {
            List<String> list = new List<string>();

            var cnn2 = mConnection;

            var cmd1 =
                new SqlCommand(
                    "SELECT MaCT,TenCT from CONGTRINH");

            cnn2.Doc(cmd1);

            DataTable dt1 = cnn2;

            foreach (DataRow drItem in dt1.Rows)
            {
                String s = (String)drItem["TenCT"];
                list.Add(s);
            }

            return list;
        }

        public DataTable GetListProject3(String startYear)
        {
            var cnn2 = mConnection;

            String s;
            if (startYear != null)
            {
                s = "SELECT MaCT,TenCT from CONGTRINH where start_date like '%" + startYear + "'" + "order by TenCT";
            }
            else
            {
                s = "SELECT MaCT,TenCT from CONGTRINH order by TenCT";
            }
            var cmd = new SqlCommand(s);

            DataTable tb = cnn2.queryTable(cmd);

            return tb;
        }

        //===============project=================
        public int insertProject(string name, string manhomct, string nhomct, long giatri, long giatrint, DateTime ngay_ky, DateTime ngay_hoanthanh, string trangthai, string desc)
        {
            var cnn2 = mConnection;

            var cmd = new SqlCommand(@"insert into congtrinh (MaCT,tenct,mo_ta,ngay_ky,ngay_hoan_thanh,trangthai,giatrihopdong,giatringhiemthu,nhomct,start_date) SELECT ( SELECT CONCAT('MACT', RIGHT(CONCAT('0000',ISNULL(right(max([MaCT]),4),0) + 1),4)) FROM dbo.[CONGTRINH] where [MaCT] like 'MACT%'), @tenct,@mo_ta,@ngay_ky,@ngay_hoan_thanh,@trangthai,@giatri,@giatrint,@nhomct,@start_date");

            cmd.Parameters.Add("tenct", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("mo_ta", SqlDbType.NVarChar).Value = desc;
            cmd.Parameters.Add("ngay_ky", SqlDbType.Date).Value = ngay_ky.Year == 1 ? (object)DBNull.Value : ngay_ky;
            cmd.Parameters.Add("ngay_hoan_thanh", SqlDbType.Date).Value = ngay_hoanthanh.Year == 1 ? (object)DBNull.Value : ngay_hoanthanh;
            cmd.Parameters.Add("trangthai", SqlDbType.NVarChar).Value = trangthai;
            cmd.Parameters.Add("giatri", SqlDbType.BigInt).Value = giatri;
            cmd.Parameters.Add("giatrint", SqlDbType.BigInt).Value = giatrint;
            cmd.Parameters.Add("nhomct", SqlDbType.Int).Value = Convert.ToInt32(manhomct);
            cmd.Parameters.Add("start_date", SqlDbType.NVarChar).Value = ngay_ky.Year == 1 ? "" : ngay_ky.ToString("yyyy");

            cnn2.ThucThi(cmd);

            return 0;
        }

        public int updateProject(string code, string name, string desc, string manhomct, long giatri, long giatrint, DateTime? ngay_ky, DateTime? ngay_hoanthanh, string trangthai)
        {
            var cnn2 = mConnection;

            var cmd = new SqlCommand("update congtrinh set TenCT=@tenct,mo_ta=@mo_ta,start_date=@start_date,giatrihopdong=@giatri,giatringhiemthu=@giatrint,nhomct=@nhomct,ngay_ky=@ngay_ky,ngay_hoan_thanh=@ngay_hoan_thanh,trangthai=@trangthai where MaCT=@MaCt");

            cmd.Parameters.Add("MaCT", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("tenct", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("mo_ta", SqlDbType.NVarChar).Value = desc;
            cmd.Parameters.Add("ngay_ky", SqlDbType.Date).Value = ngay_ky == null ? (object)DBNull.Value : ngay_ky;
            cmd.Parameters.Add("ngay_hoan_thanh", SqlDbType.Date).Value = ngay_hoanthanh == null ? (object)DBNull.Value : ngay_hoanthanh;
            cmd.Parameters.Add("trangthai", SqlDbType.NVarChar).Value = trangthai;
            cmd.Parameters.Add("giatri", SqlDbType.BigInt).Value = giatri;
            cmd.Parameters.Add("giatrint", SqlDbType.BigInt).Value = giatrint;
            cmd.Parameters.Add("nhomct", SqlDbType.Int).Value = Convert.ToInt32(manhomct);
            cmd.Parameters.Add("start_date", SqlDbType.NVarChar).Value = ngay_ky == null ? "" : Convert.ToDateTime(ngay_ky).ToString("yyyy");

            cnn2.ThucThi(cmd);

            return 0;
        }

        public void removeProject(String MaCT)
        {
            var cnn2 = mConnection;

            DataTable phieu = getPhieuGiaoViecOfProject(MaCT);

            //  xoa phieus
            foreach (DataRow r in phieu.Rows)
            {
                int phieu_id = Utils.getIntValueOfRow(r, "phieu_id");
                if (phieu_id != 0)
                {
                    removePhieuOfProject(phieu_id);
                }
            }

            //  xoa project
            var cmd = new SqlCommand("delete congtrinh WHERE MaCT=@MaCT");

            cmd.Parameters.Add("MaCT", SqlDbType.NVarChar).Value = MaCT;

            cnn2.ThucThi(cmd);

            //  xoa cac phieu + thanh toan lien quan.....
        }

        //==============phieu==================
        public bool isPhieuNoExist(String maCT, String phieuno)
        {
            var cmd = new SqlCommand("select * from phieugiaonhan where MaCT=@mact and phieu_no=@phieuno");
            cmd.Parameters.Add("mact", SqlDbType.NVarChar).Value = maCT;
            cmd.Parameters.Add("phieuno", SqlDbType.NVarChar).Value = phieuno;

            DataTable tb = mConnection.queryTable(cmd);
            if (tb != null && tb.Rows != null && tb.Rows.Count > 0)
                return true;

            return false;
        }

        public int insertPhieuToProject(String mact, String phieu_no, String phieu_name, String doi_thi_cong, int kieu_tinh_chi_phi)
        {
            var cnn2 = mConnection;

            var cmd = new SqlCommand("insert into phieugiaonhan (MaCT,phieu_no,phieu_desc,doi_thi_cong,kieu_tinh_chi_phi) values(@mact,@phieu_no,@phieu_name,@doi_thi_cong,@kieu_tinh_chi_phi)");

            cmd.Parameters.Add("mact", SqlDbType.NVarChar).Value = mact;
            cmd.Parameters.Add("phieu_no", SqlDbType.NVarChar).Value = phieu_no;
            cmd.Parameters.Add("phieu_name", SqlDbType.NVarChar).Value = phieu_name;
            cmd.Parameters.Add("doi_thi_cong", SqlDbType.NVarChar).Value = doi_thi_cong;
            cmd.Parameters.Add("kieu_tinh_chi_phi", SqlDbType.Int).Value = kieu_tinh_chi_phi;

            cnn2.ThucThi(cmd);

            return 0;
        }

        public int updatePhieuOfProject(String maCT, String phieu_no, String phieu_name, String doi_thi_cong)
        {
            var cnn2 = mConnection;

            var cmd = new SqlCommand("update phieugiaonhan set phieu_desc=@phieu_name, doi_thi_cong=@doi_thi_cong where phieu_no=@phieu_no and MaCT=@mact");

            cmd.Parameters.Add("mact", SqlDbType.NVarChar).Value = maCT;
            cmd.Parameters.Add("phieu_no", SqlDbType.NVarChar).Value = phieu_no;
            cmd.Parameters.Add("phieu_name", SqlDbType.NVarChar).Value = phieu_name;
            cmd.Parameters.Add("doi_thi_cong", SqlDbType.NVarChar).Value = doi_thi_cong;

            cnn2.ThucThi(cmd);

            return 0;
        }

        public int removePhieuOfProject(int phieu_id)
        {
            var cnn2 = mConnection;

            //  xoa jobs
            SqlCommand cmd = new SqlCommand("delete CONGVIEC where phieu_id=@phieu_id");
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cnn2.ThucThi(cmd);

            //  xoa phieu
            cmd = new SqlCommand("delete phieugiaonhan where phieu_id=@phieu_id");
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;

            cnn2.ThucThi(cmd);

            return 0;
        }

        //===============job=========================
        /*
        public DataTable getAllJobsOfproject(int phieu_id)
        {
            String sql = "SELECT job_id,job_parrent_id,job_name,job_level,difficult,unit_amount,price,unit_name FROM CONGVIEC where phieu_id=@phieu_id";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;

            var cnn = mConnection;

            DataTable tb = cnn.queryTable(cmd);

            return tb;
        }
        */

        public DataTable getJobOfProject(int phieu_id, int job_parrent_id)
        {
            String sql = "SELECT job_id,job_parrent_id,job_name,job_level,difficult,unit_amount,price,dinhmuc,unit_name,thutu,dinhbien,ghichu FROM CONGVIEC where phieu_id=@phieu_id and job_parrent_id=@job_parrent_id";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cmd.Parameters.Add("job_parrent_id", SqlDbType.Int).Value = job_parrent_id;
            var cnn = mConnection;

            DataTable tb = cnn.queryTable(cmd);

            return tb;
        }

        public int getJobParrentIDOfJob(int job_id)
        {
            String sql = "SELECT job_parrent_id FROM CONGVIEC where job_id=@job_id";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = job_id;

            var cnn = mConnection;

            DataTable tb = cnn.queryTable(cmd);
            if (tb != null && tb.Rows.Count > 0)
            {
                DataRow r = tb.Rows[0];
                int parentId = (int)r["job_parrent_id"];
            }

            return 0;
        }

        public DataRow getJobRowWithID(int jobID)
        {
            String sql = "SELECT job_id,job_parrent_id,job_name,job_level,difficult,unit_amount,price,unit_name FROM CONGVIEC where job_id=@job_id";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = jobID;

            var cnn = mConnection;

            DataTable tb = cnn.queryTable(cmd);

            if (tb != null && tb.Rows.Count > 0)
            {
                DataRow r = tb.Rows[0];
                return r;
            }

            return null;
        }

        public int insertJobToProject(int phieu_id, String job_name, int job_parrent_id, int level, string thutu, int seq = 0)
        {
            var cnn2 = mConnection;

            var cmd =
                new SqlCommand("insert into congviec(phieu_id,job_name,job_level,job_parrent_id, seq, thutu) OUTPUT inserted.job_id values(@phieu_id,@job_name,@level,@job_parrent_id,@seq, @thutu)");

            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cmd.Parameters.Add("job_name", SqlDbType.NVarChar).Value = job_name;
            cmd.Parameters.Add("job_parrent_id", SqlDbType.NVarChar).Value = job_parrent_id;
            cmd.Parameters.Add("level", SqlDbType.Int).Value = level;
            cmd.Parameters.Add("seq", SqlDbType.Int).Value = seq;
            cmd.Parameters.Add("thutu", SqlDbType.NVarChar).Value = thutu;

            int jobId = cnn2.executeCMD(cmd);

            //==============================
            return jobId;
        }

        /*
        public int insertJobToProject(int phieu_id, int job_parrent_id, String job_name, String donvitinh, int kk, float kl, Int64 dongia, int level, float dinhmuc)
        {
            var cnn2 = mConnection;

            var cmd =
                new SqlCommand("insert into congviec(phieu_id,job_name,job_level,job_parrent_id,difficult,unit_amount,price,unit_name,dinhmuc) OUTPUT inserted.job_id values(@phieu_id,@job_name,@level,@job_parrent_id,@kk,@kl,@dongia,@donvitinh,@dinhmuc)");

            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cmd.Parameters.Add("job_name", SqlDbType.NVarChar).Value = job_name;
            cmd.Parameters.Add("job_parrent_id", SqlDbType.Int).Value = job_parrent_id;
            cmd.Parameters.Add("kk", SqlDbType.Float).Value = kk;
            cmd.Parameters.Add("kl", SqlDbType.Int).Value = kl;
            cmd.Parameters.Add("dongia", SqlDbType.BigInt).Value = dongia;
            cmd.Parameters.Add("donvitinh", SqlDbType.NVarChar).Value = donvitinh;
            cmd.Parameters.Add("level", SqlDbType.Int).Value = level;
            cmd.Parameters.Add("dinhmuc", SqlDbType.Float).Value = dinhmuc;

            int jobId = cnn2.executeCMD(cmd);

            //==============================
            return jobId;
        }
        */

        public int insertJobToProject(int phieu_id, int job_parrent_id, String job_name, String donvitinh, int kk, float kl, Int64 dongia, float dinhmuc, int level, float dinhbien, string thutu, string ghichu = "", int seq = 0)
        {
            if (string.IsNullOrEmpty(thutu))
            {
                return 0;
            }

            var cnn2 = mConnection;

            var cmd =
                new SqlCommand("insert into congviec(phieu_id,job_name,job_level,job_parrent_id,difficult,unit_amount,price,dinhmuc,unit_name, thutu, dinhbien, ghichu, seq) OUTPUT inserted.job_id values(@phieu_id,@job_name,@level,@job_parrent_id,@kk,@kl,@dongia,@dinhmuc,@donvitinh, @thutu, @dinhbien, @ghichu, @seq)");

            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cmd.Parameters.Add("job_name", SqlDbType.NVarChar).Value = job_name;
            cmd.Parameters.Add("job_parrent_id", SqlDbType.Int).Value = job_parrent_id;
            cmd.Parameters.Add("kk", SqlDbType.Int).Value = kk;
            cmd.Parameters.Add("kl", SqlDbType.Float).Value = kl;
            cmd.Parameters.Add("dongia", SqlDbType.BigInt).Value = dongia;
            cmd.Parameters.Add("dinhmuc", SqlDbType.Float).Value = dinhmuc;
            cmd.Parameters.Add("donvitinh", SqlDbType.NVarChar).Value = donvitinh;
            cmd.Parameters.Add("level", SqlDbType.Int).Value = level;


            cmd.Parameters.Add("dinhbien", SqlDbType.Float).Value = dinhbien;
            cmd.Parameters.Add("thutu", SqlDbType.NVarChar).Value = thutu;
            cmd.Parameters.Add("ghichu", SqlDbType.NVarChar).Value = ghichu;
            cmd.Parameters.Add("seq", SqlDbType.Int).Value = seq;

          


            int jobId = cnn2.executeCMD(cmd);

            //==============================
            return jobId;
        }

        private int getJobID(int phieu_id, String job_name, int job_parrent_id)
        {
            SqlCommand cmd = new SqlCommand("select job_id from CONGVIEC where phieu_id=@phieu_id and job_name=@job_name and job_parrent_id=@job_parrent_id");
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cmd.Parameters.Add("job_name", SqlDbType.NVarChar).Value = job_name;
            cmd.Parameters.Add("job_parrent_id", SqlDbType.Int).Value = job_parrent_id;

            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                int jobID = Utils.getIntValueOfRow(dt.Rows[0], 0);
                return jobID;
            }

            return 0;
        }

        public void updateJobToProject(int job_id, String job_name, int kk, double kl)
        {
            var cnn2 = mConnection;

            var cmd =
                new SqlCommand("update congviec set job_name=@job_name, difficult=@kk,unit_amount=@kl where job_id=@job_id");

            cmd.Parameters.Add("job_name", SqlDbType.NVarChar).Value = job_name;
            cmd.Parameters.Add("kk", SqlDbType.Int).Value = kk;
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = job_id;
            cmd.Parameters.Add("kl", SqlDbType.Float).Value = kl;

            cnn2.ThucThi(cmd);
        }

        public void removeJob(int phieu_id, int jobID)
        {
            var cmd = new SqlCommand("DELETE FROM CONGVIEC WHERE phieu_id=@phieu_id and job_id=@job_id");
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = jobID;

            var cnn2 = mConnection;

            cnn2.ThucThi(cmd);

            //===================================
            cmd = new SqlCommand("DELETE FROM THANHTOAN WHERE job_id=@job_id");
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = jobID;

            cnn2.ThucThi(cmd);
            //===================================
            cmd = new SqlCommand("DELETE FROM THANHTOAN_JOB WHERE job_id=@job_id");
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = jobID;

            cnn2.ThucThi(cmd);
        }

        public bool isThanhtoanAvailable(int phieu_id, int YYYYMM)
        {
            SqlCommand cmd = new SqlCommand("select thanhtoan_id from THANHTOAN where phieu_id=@phieu_id and month=@month");
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cmd.Parameters.Add("month", SqlDbType.Int).Value = YYYYMM;
            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count > 0)
                return true;

            return false;
        }

        public void insertThanhtoanHangThang(int phieu_id, int YYYYMM)
        {
            var cnn2 = mConnection;

            var cmd = new SqlCommand("insert into thanhtoan (phieu_id,month) values(@phieu_id,@month)");

            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cmd.Parameters.Add("month", SqlDbType.Int).Value = YYYYMM;

            cnn2.ThucThi(cmd);
        }

        public bool removeThanhtoanhangthang(int thanhtoan_id)
        {
            SqlCommand cmd = new SqlCommand("delete thanhtoan_job where thanhtoan_id=" + thanhtoan_id);
            mConnection.execute(cmd);

            cmd = new SqlCommand("delete thanhtoan where thanhtoan_id=" + thanhtoan_id);
            mConnection.execute(cmd);

            return true;
        }

        private int getDateOfThanhtoan(int thanhtoan_id)
        {
            String sql = "select month from thanhtoan where thanhtoan_id=@thanhtoan_id";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("thanhtoan_id", SqlDbType.Int).Value = thanhtoan_id;

            var cnn2 = mConnection;

            cnn2.Doc(cmd);
            DataTable dt = cnn2;

            int date = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                date = (int)dt.Rows[0]["month"];
            }

            return date;
        }

        public void updateThanhToanHangThangOfJob(int job_id, int thanhtoan_id, float khoiluong)
        {
            // try to update first
            int d = getDateOfThanhtoan(thanhtoan_id);
            if (d == 0)
                return;

            var cnn2 = mConnection;

            var cmd = new SqlCommand("update thanhtoan_job set khoiluong=@khoiluong,month=@month where job_id=@job_id and thanhtoan_id=@thanhtoan_id");

            cmd.Parameters.Add("thanhtoan_id", SqlDbType.Int).Value = thanhtoan_id;
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = job_id;
            cmd.Parameters.Add("month", SqlDbType.Int).Value = d;
            cmd.Parameters.Add("khoiluong", SqlDbType.Float).Value = khoiluong;

            bool ok = cnn2.update(cmd);

            if (!ok)
            {
                cmd = new SqlCommand("insert into thanhtoan_job (thanhtoan_id,job_id,khoiluong,month) values(@thanhtoan_id,@job_id, @khoiluong,@month)");

                cmd.Parameters.Add("thanhtoan_id", SqlDbType.Int).Value = thanhtoan_id;
                cmd.Parameters.Add("job_id", SqlDbType.Int).Value = job_id;
                cmd.Parameters.Add("month", SqlDbType.Int).Value = d;
                cmd.Parameters.Add("khoiluong", SqlDbType.Float).Value = khoiluong;

                cnn2.ThucThi(cmd);
            }
        }

        public DataTable getThanhToanHangThangOfJobID(int job_id)
        {
            String sql = "select khoiluong,month from thanhtoan_job where job_id=@job_id";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = job_id;

            var cnn2 = mConnection;

            cnn2.Doc(cmd);
            DataTable dt = cnn2;

            return dt;
        }

        public DataTable getThanhtoanOfProjectID2(int phieu_id, string listmonth)
        {
            String sql = "select thanhtoan_id, month from thanhtoan where phieu_id=@phieu_id and month in (" + listmonth + ") order by month";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;

            var cnn2 = mConnection;

            cnn2.Doc(cmd);
            DataTable dt = cnn2;

            return dt;
        }

        public DataTable getThanhtoanOfProjectID(int phieu_id)
        {
            String sql = "select thanhtoan_id, month from thanhtoan where phieu_id=@phieu_id  order by month";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;

            var cnn2 = mConnection;

            cnn2.Doc(cmd);
            DataTable dt = cnn2;

            return dt;
        }

        private int getIntOfRow(DataRow r, String field)
        {
            try
            {
                int v = (int)r[field];
                return v;
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        private Double getDoubleOfRow(DataRow r, String field)
        {
            try
            {
                Double v = (Double)r[field];
                return v;
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        private Int64 getInt64OfRow(DataRow r, String field)
        {
            try
            {
                Int64 v = (Int64)r[field];
                return v;
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        public Double getTotalMoneyOfJob(int phieu_id, int jobID)
        {
            DataConnect conn = mConnection;

            Double total = _getTotalMoneyOfJob(conn, phieu_id, jobID);

            return total;
        }

        private Double _getTotalMoneyOfJob(DataConnect conn, int phieu_id, int jobID)
        {
            DataTable dt = getJobsByParentID(conn, phieu_id, jobID);
            Double total = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    int _jid = getIntOfRow(r, "job_id");
                    int _level = getIntOfRow(r, "job_level");

                    if (_level == 9)    //  leave
                    {
                        Double kl = getDoubleOfRow(r, "unit_amount");
                        Double dongia = getInt64OfRow(r, "price");

                        total += dongia * kl;
                    }
                    else
                    {
                        total += _getTotalMoneyOfJob(conn, phieu_id, _jid);
                    }
                }
            }
            else
            {
                DataRow r = getJobRowWithID(jobID);
                if (r != null)
                {
                    Double kl = getDoubleOfRow(r, "unit_amount");
                    Double dongia = getInt64OfRow(r, "price");

                    total += dongia * kl;
                }
            }

            return total;
        }

        //=================================================
        public Double getTotalMoneyThanhLyOfJob(int phieu_id, int jobID, int thanhlyID)
        {
            DataTable dt = getJobsByParentID(mConnection, phieu_id, jobID);
            Double total = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    int _jid = getIntOfRow(r, "job_id");
                    int _level = getIntOfRow(r, "job_level");

                    if (_level == 9)    //  leave
                    {
                        total += getTotalMoneyThanhLyOfJobDetail(phieu_id, _jid, thanhlyID);
                    }
                    else
                    {
                        total += getTotalMoneyThanhLyOfJob(phieu_id, _jid, thanhlyID);
                    }
                }
            }
            else
            {
                DataRow r = getJobRowWithID(jobID);
                if (r != null)
                {
                    total += getTotalMoneyThanhLyOfJobDetail(phieu_id, jobID, thanhlyID);
                }
            }

            return total;
        }

        public Double getTotalMoneyThanhLyOfJobDetail(int phieu_id, int jobID, int thanhlyID)
        {
            String sql = "SELECT sum(t.khoiluong*t1.price) as paid"
+ " from thanhtoan_job as t"
+ " left join"
+ " ("
+ " select price from congviec where job_id=@job_id"
+ " )t1 on job_id=@job_id"
+ " WHERE job_id=@job_id and thanhtoan_id=@thanhtoan_id";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = jobID;
            cmd.Parameters.Add("thanhtoan_id", SqlDbType.Int).Value = thanhlyID;
            DataConnect conn = mConnection;
            DataTable dt = conn.queryTable(cmd);

            Double v = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                v = getDoubleOfRow(r, "paid");
            }

            return v;
        }

        public Double getTotalKhoiluongThanhLyOfJobDetail(int phieu_id, int jobID)
        {
            String sql = "SELECT sum(khoiluong) as total_khoiluong from thanhtoan_job where job_id=@job_id";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("job_id", SqlDbType.Int).Value = jobID;
            DataConnect conn = mConnection;
            DataTable dt = conn.queryTable(cmd);

            Double v = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                v = getDoubleOfRow(r, "total_khoiluong");
            }

            return v;
        }

        private DataTable getJobsByParentID(DataConnect conn, int phieu_id, int job_parrent_id)
        {
            String sql = "select job_id, job_level, unit_amount, price from congviec where job_parrent_id=@job_parrent_id and phieu_id=@phieu_id";

            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("job_parrent_id", SqlDbType.Int).Value = job_parrent_id;
            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;

            DataTable dt = conn.queryTable(cmd);

            return dt;
        }

        public void updateTongTienForPhieu(int phieu_id, Double chi_phi, Double da_thanh_toan)
        {
            var cnn2 = mConnection;

            var cmd = new SqlCommand("update phieugiaonhan set chi_phi=@chi_phi, da_thanh_toan=@da_thanh_toan where phieu_id=@phieu_id");

            cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
            cmd.Parameters.Add("chi_phi", SqlDbType.BigInt).Value = chi_phi;
            cmd.Parameters.Add("da_thanh_toan", SqlDbType.BigInt).Value = da_thanh_toan;

            cnn2.ThucThi(cmd);
        }

        //====================================================
        public DataTable tbGetChungLoai()
        {
            String sql = "SELECT id,category as 'Chủng Loại',amount as 'Số lượng',in_store as 'Số lượng trong kho',description as 'Mô tả' FROM CHUNGLOAI order by category";
            var cmd = new SqlCommand(sql);

            DataTable dt = mConnection.queryTable(cmd);

            return dt;
        }

        public DataRow getChungloaiByChungloaiID(int catID)
        {
            String sql = "SELECT id,category,amount,description,in_store,donvitinh FROM CHUNGLOAI where id=" + catID;
            var cmd = new SqlCommand(sql);

            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }

            return null;
        }

        public bool tbInsertChungLoai(String name, String desc)
        {
            String sql = "insert into CHUNGLOAI(category,amount,description,in_store) values(@name,0,@desc,0)";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("name", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("desc", SqlDbType.NVarChar).Value = desc;

            bool r = mConnection.execute(cmd);

            return r;
        }

        public void tbDeleteChungloai(int chungloaiID)
        {
            //  xoa thiet bi
            String sql = "delete THIETBI where category_id=" + chungloaiID;
            var cmd = new SqlCommand(sql);

            mConnection.execute(cmd);

            //  xoa chung loai
            sql = "delete CHUNGLOAI where id=" + chungloaiID;
            cmd = new SqlCommand(sql);
            mConnection.execute(cmd);
        }

        private DataRow getBophansudungOfDevice(String code)
        {
            String sql = "select bophan, mact,date from muonthietbi where code=@code";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
            DataTable dt = mConnection.queryTable(cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }

            return null;
        }

        public DataTable tbGetCodesOfThietBiOfChungloai(int catID)
        {
            String sql = "select code from THIETBI";
            SqlCommand cmd = new SqlCommand(sql);
            return mConnection.queryTable(cmd);
        }

        public bool tbSuaThietBi(String code, String serial, String status, String description, DateTime ngaysanxuat, DateTime ngaysudung, String thietbikemtheo, String chitietthietbi)
        {
            String sql = "UPDATE THIETBI set serial=@serial,status=@status,description=@description,ngaysanxuat=@ngaysanxuat,ngaysudung=@ngaysudung,thietbikemtheo=@thietbikemtheo,chitietthietbi=@chitietthietbi where code=@code";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("serial", SqlDbType.NVarChar).Value = serial;
            cmd.Parameters.Add("status", SqlDbType.NVarChar).Value = status;
            cmd.Parameters.Add("description", SqlDbType.NVarChar).Value = description;
            cmd.Parameters.Add("ngaysanxuat", SqlDbType.DateTime).Value = ngaysanxuat;
            cmd.Parameters.Add("ngaysudung", SqlDbType.DateTime).Value = ngaysudung;
            cmd.Parameters.Add("thietbikemtheo", SqlDbType.NVarChar).Value = thietbikemtheo;
            cmd.Parameters.Add("chitietthietbi", SqlDbType.NVarChar).Value = chitietthietbi;

            return mConnection.execute(cmd);
        }

        public DataRow tbGetThietbiWithCode(String code)
        {
            String sql = "SELECT IDTB,code,category_id,serial,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung,thietbikemtheo,chitietthietbi FROM THIETBI where code=@code";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;

            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }

            return null;
        }

        public DataTable tbGetThietbiOfChungLoai(int catID, int inStore)
        {
            String sql = "";
            SqlCommand cmd;
            if (inStore == -1)
            {
                sql = "SELECT IDTB,code,category_id,nuocchetao,serial,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung,thietbikemtheo,chitietthietbi FROM THIETBI where category_id=@catID order by code";
                cmd = new SqlCommand(sql);
            }
            else
            {
                sql = "SELECT IDTB,code,category_id,nuocchetao,serial,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung,thietbikemtheo,chitietthietbi FROM THIETBI where category_id=@catID and in_store=@in_store order by code";
                cmd = new SqlCommand(sql);
                cmd.Parameters.Add("in_store", SqlDbType.Int).Value = inStore;
            }

            cmd.Parameters.Add("catID", SqlDbType.Int).Value = catID;

            DataTable tb = mConnection.queryTable(cmd);
            DataTable tb2 = new DataTable();
            tb2.Columns.Add("id").DataType = Type.GetType("System.Int32");
            tb2.Columns.Add(C.COL_CODE).DataType = Type.GetType("System.String");
            tb2.Columns.Add("cat_id").DataType = Type.GetType("System.Int32");
            tb2.Columns.Add(C.COL_NUOCSX).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_SERIAL).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_THIETBIKEMTHEO).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_CHITIETTIHETBI).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_BOPHAN_SUDUNG).DataType = Type.GetType("System.String");  //  4
            tb2.Columns.Add(C.COL_CONGTRINHSUDUNG).DataType = Type.GetType("System.String");  //  5
            tb2.Columns.Add(C.COL_TINHTRANG).DataType = Type.GetType("System.String"); //  6
            tb2.Columns.Add(C.COL_GHICHU).DataType = Type.GetType("System.String");      //  7

            tb2.Columns.Add(C.COL_NGAYCAPNHAT).DataType = Type.GetType("System.String");     //  8
            tb2.Columns.Add(C.COL_SUAGANNHAT).DataType = Type.GetType("System.String"); //  9

            tb2.Columns.Add(C.COL_NGAYSANXUAT).DataType = Type.GetType("System.String"); //  10
            tb2.Columns.Add(C.COL_NGAYSUDUNG).DataType = Type.GetType("System.String");      //  11

            foreach (DataRow r in tb.Rows)
            {
                DataRow nr = tb2.NewRow();

                try
                {
                    nr[0] = r[0];
                    String code = (String)r[1];
                    String ngayCapNhat = null;
                    nr[C.COL_CODE] = r["code"];   //  code
                    nr[C.COL_THIETBIKEMTHEO] = r["thietbikemtheo"];
                    nr[C.COL_CHITIETTIHETBI] = r["chitietthietbi"];
                    nr[C.COL_NUOCSX] = r["nuocchetao"];
                    nr[C.COL_SERIAL] = r["serial"];   //  serial
                    nr["cat_id"] = r["category_id"];   //  cat_id
                    int t = (int)r["in_store"];
                    if (t == 1)
                    {
                        nr[C.COL_BOPHAN_SUDUNG] = "Trong kho";
                        nr[C.COL_CONGTRINHSUDUNG] = "";
                        ngayCapNhat = "";
                    }
                    else if (t == 0)
                    {
                        DataRow r2 = getBophansudungOfDevice(code);
                        nr[C.COL_BOPHAN_SUDUNG] = Utils.getStringOfRow(r2, 0);
                        nr[C.COL_CONGTRINHSUDUNG] = Utils.getStringOfRow(r2, 1);
                        ngayCapNhat = Utils.dateToStringOfRow(r2, 2);
                    }

                    nr[C.COL_TINHTRANG] = r["status"];
                    nr[C.COL_GHICHU] = r["description"];

                    nr[C.COL_NGAYCAPNHAT] = ngayCapNhat;

                    //if (r["createdate"] != null)
                    //{
                    //nr[C.COL_NGAYCAPNHAT] = Utils.dateToStringOfRow(r, "createdate");
                    //}
                    if (r["hieuchuan_date"] != null)
                    {
                        nr[C.COL_SUAGANNHAT] = Utils.dateToStringOfRow(r, "hieuchuan_date");
                    }

                    if (r["ngaysanxuat"] != null)
                    {
                        nr[C.COL_NGAYSANXUAT] = Utils.dateToStringOfRow(r, "ngaysanxuat");
                    }
                    if (r["ngaysudung"] != null)
                    {
                        nr[C.COL_NGAYSUDUNG] = Utils.dateToStringOfRow(r, "ngaysudung");
                    }

                    tb2.Rows.Add(nr);
                }
                catch (Exception ex)
                {
                }
            }

            return tb2;
        }

        public int tbGetCategoryIDOfThietBi(String code)
        {
            String sql = "select category_id from THIETBI where code=@code";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;

            DataTable tb = mConnection.queryTable(cmd);
            if (tb.Rows.Count > 0)
            {
                DataRow r = tb.Rows[0];

                int catID = (int)r[0];
                return catID;
            }

            return 0;
        }

        public bool tbInsertDevices(String code, int catID, String serial, String status, String desc, DateTime ngaysanxuat, DateTime ngaysudung, String nuocsx, String thietbikemtheo, String chitietthietbi)
        {
            String sql = "insert into THIETBI(code,category_id,serial,in_store,status,description,createdate,ngaysanxuat,ngaysudung,nuocchetao,thietbikemtheo,chitietthietbi)"
                        + " values(@code,@catID,@serial,1,@status,@desc,@date,@ngaysanxuat,@ngaysudung,@nuocsx,@thietbikemtheo,@chitietthietbi)";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("catID", SqlDbType.Int).Value = catID;
            cmd.Parameters.Add("serial", SqlDbType.NVarChar).Value = serial;
            cmd.Parameters.Add("status", SqlDbType.NVarChar).Value = status;
            cmd.Parameters.Add("desc", SqlDbType.NVarChar).Value = desc;
            cmd.Parameters.Add("date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("ngaysanxuat", SqlDbType.DateTime).Value = ngaysanxuat;
            cmd.Parameters.Add("ngaysudung", SqlDbType.DateTime).Value = ngaysudung;
            cmd.Parameters.Add("nuocsx", SqlDbType.NVarChar).Value = nuocsx;
            cmd.Parameters.Add("thietbikemtheo", SqlDbType.NVarChar).Value = thietbikemtheo;
            cmd.Parameters.Add("chitietthietbi", SqlDbType.NVarChar).Value = chitietthietbi;

            bool r = mConnection.execute(cmd);

            return r;
        }

        public bool tbRemoveDevice(int deviceID)
        {
            String sql = "delete THIETBI where IDTB=@deviceID";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("deviceID", SqlDbType.Int).Value = deviceID;

            bool r = mConnection.execute(cmd);

            return r;
        }

        public int getAmountOfThietbiWithCatID(int catID)
        {
            String sql = "select count(*) from THIETBI where category_id=@catID";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("catID", SqlDbType.Int).Value = catID;

            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                int cnt = Utils.getIntValueOfRow(r, 0);
                return cnt;
            }

            return 0;
        }

        public int getAmountOfInstoreThietbiWithCatID(int catID)
        {
            String sql = "select count(*) from THIETBI where category_id=@catID and in_store=1";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("catID", SqlDbType.Int).Value = catID;

            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                int cnt = Utils.getIntValueOfRow(r, 0);
                return cnt;
            }

            return 0;
        }

        public bool transInsertMuonThietBi(String code, int catID, String nguoimuon, String bophan, String congtrinh, String ghichu, String nguoixuat, DateTime date, String thietbikemtheo)
        {
            String sql = "insert into MUONTHIETBI(code,nguoimuon,bophan,nguoixuat,mact,ghichu,date,thietbikemtheo)"
            + " values(@code,@nguoimuon,@bophan,@nguoixuat,@mact,@ghichu,@date,@thietbikemtheo)";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("nguoimuon", SqlDbType.NVarChar).Value = nguoimuon;
            cmd.Parameters.Add("bophan", SqlDbType.NVarChar).Value = bophan;
            cmd.Parameters.Add("nguoixuat", SqlDbType.NVarChar).Value = nguoixuat;
            cmd.Parameters.Add("mact", SqlDbType.NVarChar).Value = congtrinh;
            cmd.Parameters.Add("ghichu", SqlDbType.NVarChar).Value = ghichu;
            cmd.Parameters.Add("date", SqlDbType.DateTime).Value = date;
            cmd.Parameters.Add("thietbikemtheo", SqlDbType.NVarChar).Value = thietbikemtheo;

            bool r = mConnection.transExecuteNonQuery(cmd);

            if (r)
            {
                sql = "update THIETBI set in_store=0 where code=@code";
                cmd = new SqlCommand(sql);
                cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;

                r = mConnection.transExecuteNonQuery(cmd);
                if (!r)
                {
                    Utils.log("Lỗi update THIETBI");
                }
            }
            else
            {
                Utils.log("Không insert vào MUONTHIBI được");
            }

            return r;
        }

        public bool transCreatePhieuMuonThietBi(String nguoimuon, String bophan, String nguoixuat, String mact, String ghichu, DateTime date, List<String> codes, List<String> tinhtrangs, List<String> thietbikemtheos)
        {
            String sql;
            SqlCommand cmd;
            sql = "insert into PHIEUMUONTHIETBI(nguoimuon,bophan,nguoixuat,mact,ghichu,date) VALUES(@nguoimuon,@bophan,@nguoixuat,@mact,@ghichu,@date)";
            cmd = new SqlCommand(sql);
            cmd.Parameters.Add("nguoimuon", SqlDbType.NVarChar).Value = nguoimuon;
            cmd.Parameters.Add("bophan", SqlDbType.NVarChar).Value = bophan;
            cmd.Parameters.Add("nguoixuat", SqlDbType.NVarChar).Value = nguoixuat;
            cmd.Parameters.Add("mact", SqlDbType.NVarChar).Value = mact;
            cmd.Parameters.Add("ghichu", SqlDbType.NVarChar).Value = ghichu;
            cmd.Parameters.Add("date", SqlDbType.DateTime).Value = date;

            bool r = getInstance().mConnection.execute(cmd);
            if (!r)
            {
                Utils.log("Không insert vào PHIEUMUONTHIETBI được");
                return false;
            }

            sql = "select id from PHIEUMUONTHIETBI where nguoimuon=@nguoimuon and date=@date";
            cmd = new SqlCommand(sql);
            cmd.Parameters.Add("nguoimuon", SqlDbType.NVarChar).Value = nguoimuon;
            cmd.Parameters.Add("date", SqlDbType.DateTime).Value = date;
            DataTable dt = getInstance().mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count == 1)   //  I'm sure :)
            {
                DataRow r0 = dt.Rows[0];
                int phieuID = (int)r0[0];

                for (int i = 0; i < codes.Count; i++)
                {
                    String code = codes[i];
                    String tinhtrang = tinhtrangs[i];
                    String thietbikemtheo = thietbikemtheos[i];

                    sql = "insert into PHIEUMUONTHIETBI_DS(phieu_id,code,tinhtrang,thietbikemtheo) VALUES(@phieu_id,@code,@tinhtrang,@thietbikemtheo)";
                    cmd = new SqlCommand(sql);
                    cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieuID;
                    cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
                    cmd.Parameters.Add("tinhtrang", SqlDbType.NVarChar).Value = tinhtrang;
                    cmd.Parameters.Add("thietbikemtheo", SqlDbType.NVarChar).Value = thietbikemtheo;

                    r = getInstance().mConnection.execute(cmd);
                    if (!r)
                    {
                        Utils.log("Không insert vào PHIEUMUONTHIETBI_DS được");
                        return false;
                    }
                }
            }

            return true;
        }

        public void createPhieuTraThietbi(String nguoitra, String bophan, String nguoinhan, String ghichu, DateTime ngaytra, List<String> codes, List<String> tinhtrangs, List<String> thietbikemtheos)
        {
            String sql;
            SqlCommand cmd;

            sql = "insert INTO PHIEUTRATHIETBI(nguoitra,bophan,nguoinhan,ghichu,ngaytra) VALUES(@nguoitra,@bophan,@nguoinhan,@ghichu,@ngaytra)";
            cmd = new SqlCommand(sql);
            cmd.Parameters.Add("nguoitra", SqlDbType.NVarChar).Value = nguoitra;
            cmd.Parameters.Add("bophan", SqlDbType.NVarChar).Value = bophan;
            cmd.Parameters.Add("nguoinhan", SqlDbType.NVarChar).Value = nguoinhan;
            cmd.Parameters.Add("ghichu", SqlDbType.NVarChar).Value = ghichu;
            cmd.Parameters.Add("ngaytra", SqlDbType.DateTime).Value = ngaytra;

            bool r = mConnection.execute(cmd);

            if (!r)
            {
                Utils.log("Không insert vao PHIEUTRATHIETBI được");
                return;
            }

            sql = "select id from PHIEUTRATHIETBI where nguoitra=@nguoitra and ngaytra=@ngaytra";
            cmd = new SqlCommand(sql);
            cmd.Parameters.Add("nguoitra", SqlDbType.NVarChar).Value = nguoitra;
            cmd.Parameters.Add("ngaytra", SqlDbType.DateTime).Value = ngaytra;
            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow r0 = dt.Rows[0];
                int phieu_id = (int)r0[0];
                for (int i = 0; i < codes.Count; i++)
                {
                    String code = codes[i];
                    String tinhtrang = tinhtrangs[i];
                    String thietbikemtheo = thietbikemtheos[i];

                    sql = "insert into PHIEUTRATHIETBI_DS(phieu_id,code,tinhtrang,thietbikemtheo) VALUES(@phieu_id,@code,@tinhtrang,@thietbikemtheo)";
                    cmd = new SqlCommand(sql);
                    cmd.Parameters.Add("phieu_id", SqlDbType.Int).Value = phieu_id;
                    cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
                    cmd.Parameters.Add("tinhtrang", SqlDbType.NVarChar).Value = tinhtrang;
                    cmd.Parameters.Add("thietbikemtheo", SqlDbType.NVarChar).Value = thietbikemtheo;

                    mConnection.execute(cmd);
                }
            }
        }

        public DataRow tbGetThongtinThietBiMuon(String code)
        {
            String sql = "select nguoimuon,bophan,nguoixuat,mact,date,tinhtrang,ghichu,thietbikemtheo from MUONTHIETBI where code=@code";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;

            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                return r;
            }

            return null;
        }

        public void deleteLichsuMuonTBFull(int id)
        {
            String sql = "delete MUONTHIETBI_HIS where id=" + id;

            mConnection.execute(new SqlCommand(sql));
        }

        public DataTable getLichsuMuonTBFull(String code)
        {
            String sql = "select id, ROW_NUMBER() OVER(order by CONVERT(DateTime, date_muon) desc) as 'STT', code as 'Mã thiết bị'"
                            + " ,nguoixuat as 'Người xuất', nguoimuon as 'Người mượn', bophan_muon as 'Bộ phận mượn', date_muon as 'Ngày mượn', tinhtrang_muon as 'Tình trạng khi mượn', ghichu_muon as 'Ghi chú khi mượn'"
                            + " ,nguoinhan as 'Người nhận', nguoitra as 'Người trả', bophan_tra as 'Bộ phận trả', tinhtrang_tra as 'Tình trạng khi trả', date_tra as 'Ngày trả', ghichu_tra as 'Ghi chú khi trả'"
                            + " ,thietbikemtheo as 'Thiết bị kèm theo'"
                            + " from MUONTHIETBI_HIS where code like '%" + code + "%'";
            SqlCommand cmd = new SqlCommand(sql);

            return mConnection.queryTable(cmd);
        }

        public bool transTraThietBi(String code, String nguoimuon, String nguoixuat, String bophan_muon, DateTime date_muon, String tinhtrang_muon, String ghichu_muon,
                                String nguoitra, String nguoinhan, String bophan_tra, String tinhtrang_tra, DateTime date_tra, String ghichu_tra, String thietbikemtheo)
        {
            //  update in_store
            String sql = "update THIETBI set in_store=1,status=@tinhtrang where code=@code";
            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("tinhtrang", SqlDbType.NVarChar).Value = tinhtrang_tra;
            bool r = mConnection.transExecuteNonQuery(cmd);

            if (r)
            {
                //  move from MUONTHIETBI to HISTORY_MUONTHIETBI
                sql = "insert into MUONTHIETBI_HIS(code,nguoimuon,nguoixuat,bophan_muon,date_muon,tinhtrang_muon,ghichu_muon,"
                        + "nguoitra,nguoinhan,bophan_tra,tinhtrang_tra,date_tra,ghichu_tra,thietbikemtheo)"
                        + " values(@code,@nguoimuon,@nguoixuat,@bophan_muon,@date_muon,@tinhtrang_muon,@ghichu_muon,"
                        + "@nguoitra,@nguoinhan,@bophan_tra,@tinhtrang_tra,@date_tra,@ghichu_tra,@thietbikemtheo)";
                cmd = new SqlCommand(sql);
                cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;

                cmd.Parameters.Add("nguoimuon", SqlDbType.NVarChar).Value = nguoimuon;
                cmd.Parameters.Add("nguoixuat", SqlDbType.NVarChar).Value = nguoixuat;
                cmd.Parameters.Add("bophan_muon", SqlDbType.NVarChar).Value = bophan_muon;
                cmd.Parameters.Add("date_muon", SqlDbType.DateTime).Value = date_muon;
                cmd.Parameters.Add("tinhtrang_muon", SqlDbType.NVarChar).Value = tinhtrang_muon;
                cmd.Parameters.Add("ghichu_muon", SqlDbType.NVarChar).Value = ghichu_muon;

                cmd.Parameters.Add("nguoitra", SqlDbType.NVarChar).Value = nguoitra;
                cmd.Parameters.Add("nguoinhan", SqlDbType.NVarChar).Value = nguoinhan;
                cmd.Parameters.Add("bophan_tra", SqlDbType.NVarChar).Value = bophan_tra;
                cmd.Parameters.Add("date_tra", SqlDbType.DateTime).Value = date_tra;
                cmd.Parameters.Add("tinhtrang_tra", SqlDbType.NVarChar).Value = tinhtrang_tra;
                cmd.Parameters.Add("ghichu_tra", SqlDbType.NVarChar).Value = ghichu_tra;
                cmd.Parameters.Add("thietbikemtheo", SqlDbType.NVarChar).Value = thietbikemtheo;

                r = mConnection.transExecuteNonQuery(cmd);
                if (!r)
                    return false;
                /*
                //  delete MUONTHIETBI
                sql = "delete MUONTHIETBI where code=@code";
                cmd = new SqlCommand(sql);
                cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
                r = mConnection.transExecuteNonQuery(cmd);

                return r;
                 */
            }
            return r;
        }

        public bool transTraThietBi_DeleteMUONTHIETBI(String code)
        {
            //  update in_store
            String sql = "delete MUONTHIETBI where code=@code";

            var cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
            bool r = mConnection.transExecuteNonQuery(cmd);

            return r;
        }

        //============== thong ke thiet bi[================
        public List<String> tbGetCodes()
        {
            String sql = "select code from THIETBI order by code";
            SqlCommand cmd = new SqlCommand(sql);

            DataTable tb = mConnection.queryTable(cmd);

            List<String> list = new List<string>(200);
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow r in tb.Rows)
                {
                    String code = Utils.getStringOfRow(r, 0);
                    list.Add(code);
                }
            }

            return list;
        }

        public DataTable tbThongkeThietbi(int type, object param)
        {
            String sql = "";

            SqlCommand cmd = null;
            if (type == C.THONGKE_THEO_CODE)
            {
                String code = (String)param;
                sql = "SELECT IDTB,code,category_id,nuocchetao,serial,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung,thietbikemtheo,chitietthietbi FROM THIETBI where code like '%" + code + "%' order by code";
                cmd = new SqlCommand(sql);
            }
            else if (type == C.THONGKE_CHUNGLOAI)
            {
                sql = "SELECT IDTB,code,category_id,nuocchetao,serial,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung,thietbikemtheo,chitietthietbi FROM THIETBI where category_id=@catID order by code";
                cmd = new SqlCommand(sql);
                Int32 catID = (Int32)param;
                cmd.Parameters.Add("catID", SqlDbType.Int).Value = catID;
            }
            else if (type == C.THONGKE_CONGTRINH)
            {
                sql = "SELECT IDTB,[THIETBI].[code],category_id,nuocchetao,serial,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung,[THIETBI].[thietbikemtheo],chitietthietbi FROM THIETBI "
                        + "INNER JOIN [MUONTHIETBI] on [MUONTHIETBI].[code]=[THIETBI].[code] and [MUONTHIETBI].[mact]=@mact";
                cmd = new SqlCommand(sql);
                String congtrinh = (String)param;
                cmd.Parameters.Add("mact", SqlDbType.NVarChar).Value = congtrinh;
            }
            else if (type == C.THONGKE_BOPHAN)
            {
                sql = "SELECT IDTB,[THIETBI].[code],category_id,nuocchetao,serial,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung,[THIETBI].[thietbikemtheo],chitietthietbi FROM THIETBI "
                        + "INNER JOIN [MUONTHIETBI] on [THIETBI].[in_store]=0 and [MUONTHIETBI].[code]=[THIETBI].[code] and [MUONTHIETBI].[bophan]=@bophan";
                cmd = new SqlCommand(sql);
                String bophan = (String)param;
                cmd.Parameters.Add("bophan", SqlDbType.NVarChar).Value = bophan;
            }
            else if (type == C.THONGKE_DIDO)
            {
                sql = "SELECT IDTB,code,category_id,serial,nuocchetao,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung,thietbikemtheo,chitietthietbi FROM THIETBI where in_store=0";
                cmd = new SqlCommand(sql);
            }
            else if (type == C.THONGKE_TRONGKHO)
            {
                sql = "SELECT IDTB,code,category_id,serial,nuocchetao,in_store,status,description,createdate,hieuchuan_date,ngaysanxuat,ngaysudung,thietbikemtheo,chitietthietbi FROM THIETBI where in_store=1";
                cmd = new SqlCommand(sql);
            }

            DataTable tb = mConnection.queryTable(cmd);
            if (tb == null)
                return null;
            DataTable tb2 = new DataTable();
            tb2.Columns.Add("id").DataType = Type.GetType("System.Int32");
            tb2.Columns.Add(C.COL_CODE).DataType = Type.GetType("System.String");
            tb2.Columns.Add("cat_id").DataType = Type.GetType("System.Int32");
            tb2.Columns.Add(C.COL_NUOCSX).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_SERIAL).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_THIETBIKEMTHEO).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_CHITIETTIHETBI).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_BOPHAN_SUDUNG).DataType = Type.GetType("System.String");  //  4
            tb2.Columns.Add(C.COL_CONGTRINHSUDUNG).DataType = Type.GetType("System.String");  //  4
            tb2.Columns.Add(C.COL_TINHTRANG).DataType = Type.GetType("System.String"); //  5
            tb2.Columns.Add(C.COL_GHICHU).DataType = Type.GetType("System.String");      //  6
            //===tb2.Columns.Add(C.COL_NGAYCAPNHAT).DataType = Type.GetType("System.String");     //  7
            //===tb2.Columns.Add(C.COL_SUAGANNHAT).DataType = Type.GetType("System.String"); //  8

            tb2.Columns.Add(C.COL_NGAYSANXUAT).DataType = Type.GetType("System.String"); //  10
            tb2.Columns.Add(C.COL_NGAYSUDUNG).DataType = Type.GetType("System.String");      //  11

            foreach (DataRow r in tb.Rows)
            {
                DataRow nr = tb2.NewRow();

                try
                {
                    nr[0] = r[0];
                    String code = (String)r[1];
                    String ngayCapNhat = null;
                    nr[C.COL_CODE] = r["code"];   //  code
                    nr[C.COL_NUOCSX] = r["nuocchetao"];
                    nr[C.COL_THIETBIKEMTHEO] = r["thietbikemtheo"];
                    nr[C.COL_CHITIETTIHETBI] = r["chitietthietbi"];
                    nr[C.COL_SERIAL] = r["serial"];   //  serial
                    nr["cat_id"] = r["category_id"];   //  cat_id
                    int t = (int)r["in_store"];
                    if (t == 1)
                    {
                        nr[C.COL_BOPHAN_SUDUNG] = "Trong kho";
                        nr[C.COL_CONGTRINHSUDUNG] = "";
                        ngayCapNhat = "";
                    }
                    else if (t == 0)
                    {
                        DataRow r2 = getBophansudungOfDevice(code);
                        nr[C.COL_BOPHAN_SUDUNG] = Utils.getStringOfRow(r2, 0);
                        nr[C.COL_CONGTRINHSUDUNG] = Utils.getStringOfRow(r2, 1);
                        ngayCapNhat = Utils.dateToStringOfRow(r2, 2);
                    }

                    nr[C.COL_TINHTRANG] = r["status"];
                    nr[C.COL_GHICHU] = r["description"];

                    //=====nr[C.COL_NGAYCAPNHAT] = ngayCapNhat;

                    //if (r["createdate"] != null)
                    //{
                    //nr[C.COL_NGAYCAPNHAT] = Utils.dateToStringOfRow(r, "createdate");
                    //}
                    /*
                    if (r["hieuchuan_date"] != null)
                    {
                        nr[C.COL_SUAGANNHAT] = Utils.dateToStringOfRow(r, "hieuchuan_date");
                    }
                    */
                    if (r["ngaysanxuat"] != null)
                    {
                        nr[C.COL_NGAYSANXUAT] = Utils.dateToStringOfRow(r, "ngaysanxuat");
                    }
                    if (r["ngaysudung"] != null)
                    {
                        nr[C.COL_NGAYSUDUNG] = Utils.dateToStringOfRow(r, "ngaysudung");
                    }

                    tb2.Rows.Add(nr);
                }
                catch (Exception ex)
                {
                }
            }

            return tb2;
        }

        //==============sua chua-hieu chuan=================
        public DataTable tbThongkeThietbiHieuChuan(int type, object param)
        {
            String sql = "";

            SqlCommand cmd = null;
            if (type == C.THONGKE_THEO_CODE)
            {
                String code = (String)param;
                sql = "SELECT IDTB,code,serial,in_store,status,description,hieuchuan_date,hieuchuan_noidung FROM THIETBI where code like '%" + code + "%' order by code";
                cmd = new SqlCommand(sql);
            }
            else if (type == C.THONGKE_CHUNGLOAI)
            {
                sql = "SELECT IDTB,code,serial,in_store,status,description,hieuchuan_date,hieuchuan_noidung FROM THIETBI where category_id=@catID order by code";
                cmd = new SqlCommand(sql);
                Int32 catID = (Int32)param;
                cmd.Parameters.Add("catID", SqlDbType.Int).Value = catID;
            }
            else if (type == C.THONGKE_CONGTRINH)
            {
            }
            else if (type == C.THONGKE_BOPHAN)
            {
            }
            else if (type == C.THONGKE_DIDO)
            {
            }
            else if (type == C.THONGKE_TRONGKHO)
            {
            }

            DataTable tb = mConnection.queryTable(cmd);
            DataTable tb2 = new DataTable();
            tb2.Columns.Add("id").DataType = Type.GetType("System.Int32");
            tb2.Columns.Add(C.COL_CODE).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_SERIAL).DataType = Type.GetType("System.String");
            tb2.Columns.Add(C.COL_BOPHAN_SUDUNG).DataType = Type.GetType("System.String");  //  4

            //if (type != C.THONGKE_DIDO)
            //{
            tb2.Columns.Add(C.COL_CONGTRINHSUDUNG).DataType = Type.GetType("System.String");  //  4
            //}
            tb2.Columns.Add(C.COL_TINHTRANG).DataType = Type.GetType("System.String"); //  5
            tb2.Columns.Add(C.COL_GHICHU).DataType = Type.GetType("System.String");      //  6
            tb2.Columns.Add(C.COL_NGAYHIEUCHUAN).DataType = Type.GetType("System.String");     //  7
            tb2.Columns.Add(C.COL_NOIDUNGHIEUCHUAN).DataType = Type.GetType("System.String"); //  8

            foreach (DataRow r in tb.Rows)
            {
                DataRow nr = tb2.NewRow();
                tb2.Rows.Add(nr);

                try
                {
                    nr[0] = r[0];
                    nr[1] = r[1];
                    nr[2] = r[2];

                    String code = (String)r[1];
                    int t = (int)r[3];
                    if (t == 1)
                    {
                        nr[C.COL_BOPHAN_SUDUNG] = "Trong kho";
                        nr[C.COL_CONGTRINHSUDUNG] = "";
                        //ngayCapNhat = "";
                    }
                    else if (t == 0)
                    {
                        DataRow r2 = getBophansudungOfDevice(code);
                        nr[C.COL_BOPHAN_SUDUNG] = Utils.getStringOfRow(r2, 0);
                        nr[C.COL_CONGTRINHSUDUNG] = Utils.getStringOfRow(r2, 1);
                        //ngayCapNhat = Utils.dateToStringOfRow(r2, 2);
                    }

                    nr[5] = r[4];   //  status
                    nr[6] = r[5];   //  gopy
                    nr[7] = r[6];   //  ngaysuachua
                    nr[8] = r[7];   //  noi dung sua chua
                }
                catch (Exception ex)
                {
                }
            }

            return tb2;
        }

        //  hieu chuan
        public DataTable getHieuchuanHistoryOfThietbi(String code)
        {
            String sql = "select IDHC, ROW_NUMBER() OVER(order by CONVERT(DateTime, date) desc) as 'STT', code as 'Mã thiết bị',date as 'Ngày hiệu chuẩn',description as 'Nội dung',donvi as 'Đơn vị hiệu chuẩn' from HIEUCHUAN where code=@code";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;

            DataTable tb = mConnection.queryTable(cmd);

            return tb;
        }

        public void insertHieuchuanOfThietbi(String code, DateTime date, String noidung, String donvi)
        {
            String sql = "insert into HIEUCHUAN(code,date,description,donvi) values(@code,@date,@noidung,@donvi)";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("date", SqlDbType.DateTime).Value = date;
            cmd.Parameters.Add("noidung", SqlDbType.NVarChar).Value = noidung;
            cmd.Parameters.Add("donvi", SqlDbType.NVarChar).Value = donvi;
            mConnection.execute(cmd);

            //  update last modify of THIETBI
            sql = "UPDATE THIETBI set hieuchuan_date=@date,hieuchuan_noidung=@noidung where code=@code";
            cmd = new SqlCommand(sql);
            cmd.Parameters.Add("date", SqlDbType.DateTime).Value = date;
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("noidung", SqlDbType.NVarChar).Value = noidung;
            mConnection.execute(cmd);
        }

        public void deleteHieuchuan(int id)
        {
            String sql = "delete HIEUCHUAN where IDHC=@id";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("id", SqlDbType.Int).Value = id;

            mConnection.execute(cmd);
        }

        //==sua chua
        public DataTable getSuachuaHistoryOfThietbi(String code)
        {
            String sql = "select id, ROW_NUMBER() OVER(order by CONVERT(DateTime, date) desc) as 'STT', code as 'Mã thiết bị',date as 'Ngày sửa chữa',description as 'Nội dung',donvi as 'Đơn vị sửa chữa' from SUACHUA where code=@code";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;

            DataTable tb = mConnection.queryTable(cmd);

            return tb;
        }

        public void insertSuachuaOfThietbi(String code, DateTime date, String noidung, String donvi)
        {
            String sql = "insert into SUACHUA(code,date,description,donvi) values(@code,@date,@noidung,@donvi)";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("code", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("date", SqlDbType.DateTime).Value = date;
            cmd.Parameters.Add("noidung", SqlDbType.NVarChar).Value = noidung;
            cmd.Parameters.Add("donvi", SqlDbType.NVarChar).Value = donvi;
            mConnection.execute(cmd);
        }

        public void deleteSuachua(int id)
        {
            String sql = "delete SUACHUA where id=@id";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("id", SqlDbType.Int).Value = id;

            mConnection.execute(cmd);
        }

        //==========================
        //==========================
        public void deleteLichsuMuonTB(int id)
        {
            String sql = "delete PHIEUMUONTHIETBI where id=" + id;
            SqlCommand cmd = new SqlCommand(sql);
            mConnection.execute(cmd);
        }

        public DataTable getLichSuMuonTB()
        {
            String sql = "select id, date as 'Ngày', nguoimuon as 'Người mượn', bophan as 'Bộ phận', nguoixuat as 'Người xuất', mact as 'Công trình', ghichu as 'Ghi chú' from PHIEUMUONTHIETBI order by CONVERT(DateTime, date) desc";
            SqlCommand cmd = new SqlCommand(sql);

            return mConnection.queryTable(cmd);
        }

        public DataTable getDSMuonThietBiOfPhieuID(int phieu_id)
        {
            String sql = "select code as 'Mã thiết bị', tinhtrang as 'Tình trạng khi mượn',thietbikemtheo as 'Thiết bị kèm theo' from PHIEUMUONTHIETBI_DS where phieu_id=" + phieu_id;
            SqlCommand cmd = new SqlCommand(sql);

            return mConnection.queryTable(cmd);
        }

        //==========================
        public void deleteLichsuTraTB(int id)
        {
            String sql = "delete PHIEUTRATHIETBI where id=" + id;
            SqlCommand cmd = new SqlCommand(sql);

            mConnection.execute(cmd);
        }

        public DataTable getLichSuTraTB()
        {
            String sql = "select id, ngaytra as 'Ngày trả', nguoitra as 'Người trả', bophan as 'Bộ phận', nguoinhan as 'Người nhận', ghichu as 'Ghi chú' from PHIEUTRATHIETBI order by CONVERT(DateTime, ngaytra) desc";
            SqlCommand cmd = new SqlCommand(sql);

            return mConnection.queryTable(cmd);
        }

        public DataTable getDSTraThietBiOfPhieuID(int phieu_id)
        {
            String sql = "select code as 'Mã thiết bị', tinhtrang as 'Tình trạng khi trả', thietbikemtheo as 'Thiết bị kèm theo' from PHIEUTRATHIETBI_DS where phieu_id=" + phieu_id;
            SqlCommand cmd = new SqlCommand(sql);

            return mConnection.queryTable(cmd);
        }

        //=========================
        public void initUserSystem()
        {
            String sql = "select taikhoan from NGUOIDUNG";
            SqlCommand cmd = new SqlCommand(sql);
            DataTable dt = mConnection.queryTable(cmd);
            if (dt.Rows.Count == 0)
            {
                sql = "insert into NGUOIDUNG(taikhoan,matkhau,quyen,ten,ChucVu,PhongDoi) values('admin','admin','admin','admin','admin','admin')";
                cmd = new SqlCommand(sql);
                mConnection.execute(cmd);
            }
        }

        public int getUserPermission(String acc, String pass)
        {
            String sql = "select quyen from NGUOIDUNG where taikhoan=@acc and matkhau=@pass";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("acc", SqlDbType.NChar).Value = acc;
            cmd.Parameters.Add("pass", SqlDbType.NChar).Value = pass;
            DataTable dt = mConnection.queryTable(cmd);
            if (dt != null && dt.Rows.Count > 0)
                return 1;

            return 0;
        }

        public void updateProjectVisibility(String maCT, bool anhien)
        {
            var cmd = new SqlCommand("UPDATE CONGTRINH SET anhien = @anhien WHERE MaCT = @mact");
            cmd.Parameters.Add("anhien", SqlDbType.Bit).Value = anhien;
            cmd.Parameters.Add("mact", SqlDbType.NVarChar).Value = maCT;
            mConnection.execute(cmd);
        }
    }
}