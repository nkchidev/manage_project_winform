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
    public partial class HideThanhtoanColumnsDialog : Form
    {
        List<String> mCheckedItems = new List<string>(5);
        public HideThanhtoanColumnsDialog(List<String> thanhlyHangthangs, List<String> currentChecked)
        {
            InitializeComponent();

            for (int i = 0; i < thanhlyHangthangs.Count; i++ )
            {
                String s = thanhlyHangthangs.ElementAt(i);
                this.listView1.Items.Add(s);
            }

            this.listView1.CheckBoxes = true;

            for (int j = 0; j < currentChecked.Count; j++)
            {
                String sc = currentChecked.ElementAt(j);

                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    String s = this.listView1.Items[i].Text;
                    if (s.CompareTo(sc) == 0)
                    {
                        this.listView1.Items[i].Checked = true;
                    }
                }
            }
        }

        private void HideThanhtoanColumnsDialog_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                if (this.listView1.Items[i].Checked)
                {
                    String s = this.listView1.Items[i].Text;
                    mCheckedItems.Add(s);
                }
            }
        }

        public List<String> getCheckedItems()
        {
            return mCheckedItems;
        }


        private void button_CheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                this.listView1.Items[i].Checked = true;
            }
        }

        private void button_ClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                this.listView1.Items[i].Checked = false;
            }
        }
    }
}
