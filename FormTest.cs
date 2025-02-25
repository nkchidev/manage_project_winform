using DevExpress.XtraEditors;
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
    public partial class FormTest : DevExpress.XtraEditors.XtraForm
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            DataTable mDataSource = new DataTable();
            mDataSource.Columns.Add("Khối lượng").DataType = Type.GetType("System.String");
            mDataSource.Columns["Khối lượng"].ReadOnly = false;
            dataGridView1.DataSource = mDataSource;
            DataRow drNew = mDataSource.NewRow();

            drNew["Khối lượng"] = Utils.doubleToString(10.5);
            mDataSource.Rows.Add(drNew);
        }
    }
}