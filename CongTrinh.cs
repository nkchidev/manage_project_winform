using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;

namespace ProjectStorage
{
    public partial class CongTrinh : Office2007Form
    {
        readonly DataConnect _cnn = new DataConnect();
        public CongTrinh()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            txt_Khoi.Text = "";
            txt_MaKhoi.Focus();
        }

        private void load()
        {
            _cnn.Mo();
            var cmd = new SqlCommand("Select * from Congtrinh");
            _cnn.Doc(cmd);
            dgv_khoi.DataSource = _cnn;
            _cnn.Dong();


        }
        private void CongTrinhLoad(object sender, EventArgs e)
        {
            load();
        }

        private void BtnClick(object sender, EventArgs e)
        {
            if (txt_MaKhoi.Text != "" &&
                 txt_Khoi.Text != "")
            {
                var dr = MessageBoxEx.Show("Bạn có chắc chắn thêm phần tử không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    var nMaKhoi = txt_MaKhoi.Text;
                    var nKhoi = txt_Khoi.Text;
                    _cnn.Mo();
                    var cmd = new SqlCommand("INSERT congtrinh VALUES(@MaCT,@MaCT)");
                    cmd.Parameters.Add("MaCT", SqlDbType.Char).Value = nMaKhoi;
                    cmd.Parameters.Add("Congtrinh", SqlDbType.NVarChar).Value = nKhoi;
                    _cnn.ThucThi(cmd);
                    _cnn.Dong();
                    load();
                }
            }
            else
            {
                MessageBoxEx.Show("Chưa Khai Báo Đầy Đủ Thông Tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DgvKhoiClick(object sender, EventArgs e)
        {
            if (dgv_khoi.Rows.Count <= 0) return;
            btn_sua.Enabled = true;
            btn_xoa.Enabled = true;
            if (dgv_khoi.CurrentRow != null)
            {
                txt_MaKhoi.Text = dgv_khoi.CurrentRow.Cells[0].Value.ToString();
                txt_Khoi.Text = dgv_khoi.CurrentRow.Cells[1].Value.ToString();
            }
        }

       

        private void BtnSuaClick(object sender, EventArgs e)
        {
            var dr = MessageBoxEx.Show("Bạn có chắc chắn thêm phần tử không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr != DialogResult.Yes) return;
            var nMaKhoi = txt_MaKhoi.Text;
            var nKhoi = txt_Khoi.Text;
            _cnn.Mo();
            var cmd = new SqlCommand("UPDATE congtrinh SET congtrinh=@congtrinh WHERE MaCT=@MaCT");
            cmd.Parameters.Add("MaCT", SqlDbType.Char).Value = nMaKhoi;
            cmd.Parameters.Add("congtrinh", SqlDbType.NVarChar).Value = nKhoi;
            _cnn.ThucThi(cmd);
            _cnn.Dong();
            load();
        }

        private void DgvKhoiKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;
            var dr = MessageBoxEx.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr != DialogResult.Yes) return;
            var nMaKhoi = txt_MaKhoi.Text;
            _cnn.Mo();
            var cmd = new SqlCommand("DELETE FROM congtrinh WHERE MaCT=@MaCT");
            cmd.Parameters.Add("MaCT", SqlDbType.Char).Value = nMaKhoi;
            _cnn.ThucThi(cmd);
            _cnn.Dong();
            load();
        }

        private void BtnXoaClick(object sender, EventArgs e)
        {

            var dr = MessageBoxEx.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr != DialogResult.Yes) return;
            var nMaKhoi = txt_MaKhoi.Text;
            _cnn.Mo();
            var cmd = new SqlCommand("DELETE FROM congtrinh WHERE MaCT=@MaCT");
            cmd.Parameters.Add("MaCT", SqlDbType.Char).Value = nMaKhoi;
            _cnn.ThucThi(cmd);
            _cnn.Dong();
            load();
            txt_MaKhoi.Text = string.Empty;
            txt_Khoi.Text = string.Empty;
        }

    }
}