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
    public partial class FormAddPhieuGiaoViec : Form
    {
        public FormAddPhieuGiaoViec()
        {
            InitializeComponent();

            DatabaseUtils db = DatabaseUtils.getInstance();

            List<String> l = db.getTeamList();
            comboBoxTeam.DataSource = l;
        }
    }
}
