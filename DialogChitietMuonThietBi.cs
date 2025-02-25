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
    public partial class DialogChitietMuonThietBi : Form
    {
        public DialogChitietMuonThietBi(String code, String tinhtrang, string thietbikemtheo)
        {
            InitializeComponent();

            this.textBoxCode.Text = code;
            this.textBoxTinhTrang.Text = tinhtrang;
            this.textBoxThietbiKemTheo.Text = thietbikemtheo;
        }
    }
}
