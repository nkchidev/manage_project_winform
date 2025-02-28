using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;

namespace ProjectStorage
{
    public partial class ThietBi : Office2007Form
    {
        DataConnect _cnn = new DataConnect();
        public ThietBi()
        {
            InitializeComponent();
        }

        private void load()
        {
            _cnn.Mo();
            var cmd = new SqlCommand("select ROW_NUMBER() OVER(ORDER BY MaTB) AS TT,MaTB,ChungLoai,PhuKien,DonVi,SoLuong,TinhTrang,GhiChu from thietbi");
            _cnn.Doc(cmd);
            dgv_hocsinh.DataSource = _cnn;
            _cnn.Dong();
            nhap.Checked = true;
        }
        private void FrmLopLoad(object sender, EventArgs e)
        {
            load();
        }

        private void BtnClick(object sender, EventArgs e)
        {
            buttonX3.Enabled = false;
            nhap.Checked = true;
        }

        private void DgvLopClick(object sender, EventArgs e)
        {
            if (dgv_hocsinh.Rows.Count <= 0) return;
            buttonX3.Enabled = true;
            sua.Checked = true;
        }

        private void ButtonX3Click(object sender, EventArgs e)
        {
            var dr = MessageBoxEx.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    _cnn = new DataConnect();
                    _cnn.Mo();
                    if (dgv_hocsinh.CurrentRow != null)
                    {
                        var nMaTb = dgv_hocsinh.CurrentRow.Cells[1].Value.ToString();
                        var cmd = new SqlCommand("DELETE FROM thietbi WHERE MaTB=@matb");
                        cmd.Parameters.Add("matb", SqlDbType.NVarChar).Value = nMaTb;
                        _cnn.ThucThi(cmd);
                    }
                    _cnn.Dong();
                }

                load();
        }

        private void DgvLopKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            var dr = MessageBoxEx.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                _cnn = new DataConnect();
                _cnn.Mo();
                var cmd = new SqlCommand("EXECUTE HOCSINH_xoa @mahs");
                _cnn.ThucThi(cmd);
                _cnn.Dong();
            }

            load();
        }

        private void ButtonX2Click1(object sender, EventArgs e)
        {
            load();
        }

        private void ButtonX4Click(object sender, EventArgs e)
        {
            timkiem.Checked = true;
        }

        private void ButtonX4Click(object sender, EventArgs e)
        {
            timkiem.Checked = true;
        }

        private void SuaClick(object sender, Eve
            if (txt_timkiem.Text == "" && cb_Hong.Checked != true) return;ntArgs e)
        {
            

        }

        priv
            var strTk = txt_timkiem.Text;ate void BtnSearchClick(object sender, Ev
            if (cb_TB.Checked)
            {
                cmd .CommandText="SELECT * FROM thietbi WHERE MaTB like '%' +@MaTB+ '%'";
                cmd.Parameters.Add("MaTB", SqlDbType.Char).Value = strTk;
            }
            if (cb_Dido.Checked)
            {
                cmd.CommandText = "SELECT * FROM thietbi WHERE tenhs like '%' +@Ten+ '%'";
                cmd.Parameters.Add("Ten", SqlDbType.NVarChar).Value = strTk;
            }
            if (cb_Hong.Checked)
            {
                cmd.CommandText = "SELECT * FROM thietbi WHERE malop=@malop";
                cmd.Parameters.Add("malop", SqlDbType.Char).Value = cmb_tk.SelectedValue.ToString();
            }
                cmd .CommandText="SELECT * FROM thietbi WHERE MaTB like '%' +@MaTB+ '%'";
                cmd.Parameters.Add("MaTB", SqlDbType.Char).Value = strTk;
            }
            if (cb_Dido.Checked)
            {
                cmd.CommandText = "SELECT * FROM thietbi WHERE tenhs like '%' +@Ten+ '%'";
                cmd.Parameters.Add("Ten", SqlDbType.NVarChar).Value = strTk;
            }
            if (cb_Hong.Checked)
            {
                cmd.CommandText = "SELECT * FROM thietbi WHERE malop=@malop";
                cmd.Parameters.Add("malop", SqlDbType.Char).Value = cmb_tk.SelectedValue.ToString();
            }
            _cnn.Doc(cmd);
            DataTable tk = _cnn;
            MessageBoxEx.Show("Tim Thay " + tk.Rows.Count + " Ket Qua!", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgv_hocsinh.DataSource = tk;
            _cnn.Dong();
        }

        private void DgvHocsinhCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_hocsinh.Rows.Count <= 0) return;
            if (dgv_hocsinh.CurrentRow != null)
            {
                txt_Ma.Text = dgv_hocsinh.CurrentRow.Cells[1].Value.ToString();
                txt_CL.Text = dgv_hocsinh.CurrentRow.Cells[2].Value.ToString();
                txt_PK.Text = dgv_hocsinh.CurrentRow.Cells[3].Value.ToString();
                txt_DV.Text = dgv_hocsinh.CurrentRow.Cells[4].Value.ToString();
                txt_SL.Text = dgv_hocsinh.CurrentRow.Cells[5].Value.ToString();
                txt_TT.Text = dgv_hocsinh.CurrentRow.Cells[6].Value.ToString();
                txt_GC.Text = dgv_hocsinh.CurrentRow.Cells[7].Value.ToString();
            }
            sua.Checked = true;
        }

        private void BtnRepairClick(object sender, EventArgs e)
        {
            var dr = MessageBoxEx.Show("Bạn có chắc chắn sửa phần tử không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr != DialogResult.Yes) return;
            var nMaTb = txt_Ma.Text;
            var nChungLoai = txt_CL.Text;
            var nPhuKien = txt_PK.Text;
            var nDonVi = txt_DV.Text;
            var nSoLuong = txt_SL.Text;
            var nTinhTrang = txt_TT.Text;
            var nGhiChu = txt_GC.Text;
            _cnn.Mo();
            var cmd = new SqlCommand("UPDATE thietbi SET ChungLoai=@ChungLoai,PhuKien=@PhuKien,DonVi=@DonVi,SoLuong=@SoLuong,TinhTrang=@TinhTrang,GhiChu=@GhiChu WHERE MaTB=@MaTB");
            cmd.Parameters.Add("MaTB", SqlDbType.NVarChar).Value = nMaTb;
            cmd.Parameters.Add("ChungLoai", SqlDbType.NVarChar).Value = nChungLoai;
            cmd.Parameters.Add("PhuKien", SqlDbType.NVarChar).Value = nPhuKien;
            cmd.Parameters.Add("DonVi", SqlDbType.NVarChar).Value = nDonVi;
            cmd.Parameters.Add("SoLuong", SqlDbType.NVarChar).Value = nSoLuong;
            cmd.Parameters.Add("TinhTrang", SqlDbType.NVarChar).Value = nTinhTrang;
            cmd.Parameters.Add("GhiChu", SqlDbType.NVarChar).Value = nGhiChu;
            _cnn.ThucThi(cmd);
            _cnn.Dong();
            load();
            _cnn.Dong();
        }

        private void ButtonX9Click(object sender, EventArgs e)
        {
            var nMaTb = _txtMaTb.Text;
            var nChungLoai = _txtChungLoai.Text;
            var nPhuKien = _txtPhuKien.Text;
            var nDonVi = _txtDonVi.Text;
            var nSoLuong = _txtSoluong.Text;
            var nTinhTrang = _txtTinhTrang.Text;
            var nGhiChu = _txtGhiChu.Text;
            if (_txtMaTb.Text != "" &&
                 _txtChungLoai.Text != "" &&
                 _txtDonVi.Text != "" &&
                 _txtSoluong.Text != "" &&
                 _txtTinhTrang.Text != "")
            {
                var dr = MessageBoxEx.Show("Bạn có chắc chắn thêm không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    _cnn.Mo();
                    var cmd = new SqlCommand("INSERT INTO thietbi VALUES(@matb,@chungloai,@phukien,@donvi,@soluong,@tinhtrang,@ghichu)");
                    cmd.Parameters.Add("matb", SqlDbType.NVarChar).Value = nMaTb;
                    cmd.Parameters.Add("chungloai", SqlDbType.NVarChar).Value = nChungLoai;
                    cmd.Parameters.Add("phukien", SqlDbType.NVarChar).Value = nPhuKien;
                    cmd.Parameters.Add("donvi", SqlDbTyp           _cnn.Dong();
                }
            }
            else
            {
                MessageBoxEx.Show("Chưa Khai Báo Đầy Đủ Thông Tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}