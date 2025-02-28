using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;

namespace ProjectStorage
{
    public partial class FrmTaikhoan : Office2007Form
    {
        private bool _click;
        readonly DataConnect _cnn = new DataConnect();
        public FrmTaikhoan()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            txt_taikhoan.Text = "";
            txt_matkhau.Text = "";
            txt_taikhoan.Focus();
            txt_tenND.Text = "";
        }

        private void load()
        {
            _cnn.Mo();
            var cmd = new SqlCommand("Select * from nguoidung");
            _cnn.Doc(cmd);
            dgv_HocKy.DataSource = _cnn;
            txt_taikhoan.Focus();
            DataTable dt = _cnn;
            _cnn.Dong();
            cmb_phanquyen.DataSource = dt;
            cmb_phanquyen.DisplayMember = "quyen";
            cmb_phanquyen.ValueMember = "quyen";
            cb_chucvu.DataSource = dt;
            cb_chucvu.DisplayMember = "chucvu";
            cb_chucvu.ValueMember = "chucvu";
            cb_phongdoi.DataSource = dt;
            cb_phongdoi.DisplayMember = "phongdoi";
            cb_phongdoi.ValueMember = "phongdoi";
        }


        private void DgvHocKyKeyDown1(object sender, KeyEventArgs e)
        {
            btn_add.Visible = false;
            if (e.KeyCode != Keys.Delete) return;
            var nTaikhoan = txt_taikhoan.Text;
            var dr = MessageBoxEx.Show("Bạn có chắc chắn thêm phần tử không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr != DialogResult.Yes) return;
            _cnn.Mo();
            var cmd = new SqlCommand("DELETE FROM nguoidung WHERE taikhoan=@taikhoan");
            cmd.Parameters.Add("taikhoan", SqlDbType.NVarChar).Value = nTaikhoan;
            _cnn.ThucThi(cmd);
            _cnn.Dong();
            load();
            _cnn.Dong();
        }

        private void DgvHocKyClick1(object sender, EventArgs e)
        {
            _click = true;
            if (dgv_HocKy.Rows.Count <= 0) return;
            btn_edit.Visible = true;
            btn_del.Visible = true;
            if (dgv_HocKy.CurrentRow == null) return;
            txt_tenND.Text = dgv_HocKy.CurrentRow.Cells[3].Value.ToString();
            cb_chucvu.Text = dgv_HocKy.CurrentRow.Cells[4].Value.ToString();
            txt_taikhoan.Text = dgv_HocKy.CurrentRow.Cells[0].Value.ToString();
            txt_matkhau.Text = dgv_HocKy.CurrentRow.Cells[1].Value.ToString();
            cmb_phanquyen.Text = dgv_HocKy.CurrentRow.Cells[2].Value.ToString();
            cb_phongdoi.Text = dgv_HocKy.CurrentRow.Cells[5].Value.ToString();
        }

        private void FrmTaikhoanLoad(object sender, EventArgs e)
        {
            load();
        }

        private void BtnDelClick(object sender, EventArgs e)
        {
            var nTaikhoan = txt_taikhoan.Text;
            var dr = MessageBoxEx.Show("Bạn có chắc chắn xóa phần tử không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr != DialogResult.Yes) return;
            _cnn.Mo();
            var cmd = new SqlCommand("DELETE FROM nguoidung WHERE taikhoan=@taikhoan");
            cmd.Parameters.Add("taikhoan", SqlDbType.NVarChar).Value = nTaikhoan;
            _cnn.ThucThi(cmd);
            _cnn.Dong();
            load();
            _cnn.Dong();
        }

        private void BtnEditClick(object sender, EventArgs e)
        {
            var dr = MessageBoxEx.Show("Bạn có chắc chắn thêm phần tử không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr != DialogResult.Yes) return;
            var nTaikhoan = txt_taikhoan.Text;
            var nMatkhau = txt_matkhau.Text;
            var nQuyen = cmb_phanquyen.Text;
            var nTen = txt_tenND.Text;
            var nChucVu = cb_chucvu.Text;
            var nPhongdoi = cb_phongdoi.Text;
            _cnn.Mo();
            var cmd = new SqlCommand("UPDATE nguoidung SET matkhau=@matkhau,quyen=@quyen,ten=@ten,chucvu=@chucvu,phongdoi=@phongdoi WHERE taikhoan=@taikhoan");
            cmd.Parameters.Add("taikhoan", SqlDbType.NVarChar).Value = nTaikhoan;
            cmd.Parameters.Add("matkhau", SqlDbType.NVarChar).Value = nMatkhau;
            cmd.Parameters.Add("quyen", SqlDbType.NVarChar).Value = nQuyen;
            cmd.Parameters.Add("ten", SqlDbType.NVarChar).Value = nTen;
            cmd.Parameters.Add("chucvu", SqlDbType.NVarChar).Value = nChucVu;
            cmd.Parameters.Add("phongdoi", SqlDbType.NVarChar).Value = nPhongdoi;
            _cnn.ThucThi(cmd);
            _cnn.Dong();
            load();
            _cnn.Dong();
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            btn_add.Visible = false;
            btn_del.Visible = false;
            btn_edit.Visible = false;
            btn_can.Visible = true;
            btn_create.Visible = true;
            lbl_MK.Visible = true;
            txt_matkhau.Enabled = true;
            txt_taikhoan.Enabled = true;
            Reset();
        }

        private void BtnCanClick(object sender, EventArgs e)
        {
            load();
            btn_add.Visible = true;
            btn_del.Visible = false;
            btn_edit.Visible = false;
            btn_can.Visible = false;
            btn_create.Visible = false;
            txt_taikhoan.Enabled = false;
            lbl_MK.Visible = false;
            txt_matkhau.Enabled = false;
            Reset();
        }

        private void BtnCreateClick(object sender, EventArgs e)
        {
            var nTaikhoan = txt_taikhoan.Text;
            var nMatkhau = txt_matkhau.Text;
            var nQuyen = cmb_phanquyen.Text;
            var nTen = txt_tenND.Text;
            var nChucVu = cb_chucvu.Text;
            var nPhongdoi = cb_phongdoi.Text;
            if (txt_taikhoan.Text != "" &&
                 txt_matkhau.Text != "" &&
                 cmb_phanquyen.Text != "" &&
                 txt_tenND.Text != "" && 
                 cb_chucvu.Text!=""&& 
                 cb_phongdoi.Text!="")
            {
                var dr = MessageBoxEx.Show("Bạn có chắc chắn thêm không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    _cnn.Mo();
                    var cmd = new SqlCommand("INSERT INTO nguoidung VALUES(@taikhoan,@matkhau,@quyen,@ten,@chucvu,@phongdoi)");
                    cmd.Parameters.Add("taikhoan", SqlDbType.NVarChar).Value = nTaikhoan;
                    cmd.Parameters.Add("matkhau", SqlDbType.NVarChar).Value = nMatkhau;
                    cmd.Parameters.Add("quyen", SqlDbType.NVarChar).Value = nQuyen;
                    cmd.Parameters.Add("ten", SqlDbType.NVarChar).Value = nTen;
                    cmd.Parameters.Add("chucvu", SqlDbType.NVarChar).Value = nChucVu;
                    cmd.Parameters.Add("phongdoi", SqlDbType.NVarChar).Value = nPhongdoi;
                    _cnn.ThucThi(cmd);
                    _cnn.Dong();
                    load();
                    _cnn.Dong();
                }
            }
            else
            {
                MessageBoxEx.Show("Chưa Khai Báo Đầy Đủ Thông Tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}