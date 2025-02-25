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
    public partial class LienHe : Office2007Form
    {
        public LienHe()
        {
            InitializeComponent();
            panel1.Location = new Point(
            ClientSize.Width / 2 - panel1.Size.Width / 2,
            ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;
        }
    }
}
