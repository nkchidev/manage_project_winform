using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStorage
{
    public partial class FormTaskDetail : Form
    {
        public FormTaskDetail()
        {
            InitializeComponent();
        }

        public string mota
        {
            get
            {
                return textBoxTaskDetail.Text;
            }
            set
            {
                textBoxTaskDetail.Text = value;
            }
        }

        public string thutu
        {
            get
            {
                return txtThuTu.Text;
            }
            set
            {
                txtThuTu.Text = value;
            }
        }

        public string ghichu
        {
            get
            {
                return txtGhichu.Text;
            }
            set
            {
                txtGhichu.Text = value;
            }
        }

        public string donvitinh
        {
            get
            {
                return textBoxDonViTinh.Text;
            }
            set
            {
                textBoxDonViTinh.Text = value;
            }
        }

        public int mucKK
        {
            get
            {
                try
                {
                    return Int32.Parse(textBoxTaskKK.Text);
                }catch(Exception e){
                }
                return 0;
            }
            set
            {
                textBoxTaskKK.Text = "" + value;
            }
        }

        public float khoiluong
        {
            get
            {
                try
                {
                    return (float)Double.Parse(textBoxTaskKL.Text);
                }catch(Exception e){
                }
                return 0;
            }
            set
            {
                textBoxTaskKL.Text = "" + value;
            }
        }

        public double dongia
        {
            get
            {
                try
                {
                    return Double.Parse(textBoxTaskPriceUnit.Text);
                }catch(Exception e){
                }
                return 0;
            }
            set
            {
                textBoxTaskPriceUnit.Text = "" + value;
            }
        }

        public double dinhbien
        {
            get
            {
                try
                {
                    return Double.Parse(txtDinhBien.Text);
                }
                catch (Exception e)
                {
                }
                return 0;
            }
            set
            {
                txtDinhBien.Text = "" + value;
            }
        }

        public double dinhmuc
        {
            get
            {
                try
                {
                    return Double.Parse(textBoxDinhmuc.Text);
                }
                catch (Exception e)
                {
                }
                return 0;
            }
            set
            {
                textBoxDinhmuc.Text = "" + value;
            }
        }



        public double thanhtien
        {
            get
            {
                return khoiluong * dongia * dinhbien * dinhmuc ;
            }
            set
            {
                labelThanhTien.Text = "Thành tiền: " + (khoiluong * dongia * dinhbien * dinhmuc);
            }
        }

        private void calcThanhTien(object sender, EventArgs e)
        {
            labelThanhTien.Text = "Thành tiền: " + (khoiluong * dongia * dinhbien * dinhmuc);
        }
    }
}
