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
    public partial class DialogSuaThietBi : Office2007Form
    {
        public DialogSuaThietBi(String code)
        {
            InitializeComponent();

            //  String sql = "SELECT IDTB,code,category_id,serial,in_store,status,description,createdate,modifydate,ngaysanxuat,ngaysudung FROM THIETBI where code=@code";
            if (code != null)
            {
                DataRow r = DatabaseUtils.getInstance().tbGetThietbiWithCode(code);

                if (r != null)
                {
                    this.textBoxCode.Text = code;
                    this.textBoxDesc.Text = Utils.getStringOfRow(r, "description");
                    this.textBoxSerial.Text = Utils.getStringOfRow(r, "serial");
                    this.textBoxStatus.Text = Utils.getStringOfRow(r, "status");

                    this.textBoxThietbikemtheo.Text = Utils.getStringOfRow(r, "thietbikemtheo");

                    this.dateTimePickerNgaySX.Value = Utils.getDateOfRow(r, "ngaysanxuat");
                    this.dateTimePickerNgaySD.Value = Utils.getDateOfRow(r, "ngaysudung");
                    this.textBoxChitiet.Text = Utils.getStringOfRow(r, "chitietthietbi");
                }
            }
        }

        private void onLoad(object sender, EventArgs e)
        {
            refreshForm();
        }

        void refreshForm()
        {
        }
    }
}
