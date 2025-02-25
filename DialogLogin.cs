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
    public partial class DialogLogin : Form
    {
        public DialogLogin()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            String pass = this.textBoxPassword.Text;

            DatabaseUtils.getInstance().initUserSystem();

            int per = DatabaseUtils.getInstance().getUserPermission(textBoxAccount.Text, textBoxPassword.Text);

            if (per == 0)
            {
                this.error.Text = "Sai mật khẩu!";
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
