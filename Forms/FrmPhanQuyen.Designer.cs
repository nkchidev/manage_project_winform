namespace ProjectStorage.Forms
{
    partial class FrmPhanQuyen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPhanQuyen));
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cb_action = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnBochonTatca = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectall = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cb_users = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridAdd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chk_Them = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkXoa = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkSua = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_action.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_users.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Them)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.panelControl1);
            this.dataLayoutControl1.Controls.Add(this.gridControl1);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(917, 522);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.cb_action);
            this.panelControl1.Controls.Add(this.btnBochonTatca);
            this.panelControl1.Controls.Add(this.btnSelectall);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.cb_users);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(913, 35);
            this.panelControl1.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(425, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Chức năng:";
            // 
            // cb_action
            // 
            this.cb_action.Location = new System.Drawing.Point(492, 8);
            this.cb_action.Name = "cb_action";
            this.cb_action.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_action.Properties.Items.AddRange(new object[] {
            "Thêm",
            "Sửa",
            "Xóa"});
            this.cb_action.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cb_action.Size = new System.Drawing.Size(100, 20);
            this.cb_action.TabIndex = 5;
            // 
            // btnBochonTatca
            // 
            this.btnBochonTatca.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBochonTatca.ImageOptions.Image")));
            this.btnBochonTatca.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnBochonTatca.Location = new System.Drawing.Point(718, 6);
            this.btnBochonTatca.Name = "btnBochonTatca";
            this.btnBochonTatca.Size = new System.Drawing.Size(113, 23);
            this.btnBochonTatca.TabIndex = 4;
            this.btnBochonTatca.Text = "Bỏ chọn tất cả";
            this.btnBochonTatca.Click += new System.EventHandler(this.btnBochonTatca_Click);
            // 
            // btnSelectall
            // 
            this.btnSelectall.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectall.ImageOptions.Image")));
            this.btnSelectall.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnSelectall.Location = new System.Drawing.Point(608, 6);
            this.btnSelectall.Name = "btnSelectall";
            this.btnSelectall.Size = new System.Drawing.Size(96, 23);
            this.btnSelectall.TabIndex = 3;
            this.btnSelectall.Text = "Chọn tất cả";
            this.btnSelectall.Click += new System.EventHandler(this.btnSelectall_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.simpleButton1.Location = new System.Drawing.Point(297, 7);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(96, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Refresh Data";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // cb_users
            // 
            this.cb_users.Location = new System.Drawing.Point(102, 9);
            this.cb_users.Name = "cb_users";
            this.cb_users.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_users.Properties.NullText = "";
            this.cb_users.Properties.PopupView = this.gridLookUpEdit1View;
            this.cb_users.Size = new System.Drawing.Size(176, 20);
            this.cb_users.TabIndex = 1;
            this.cb_users.EditValueChanged += new System.EventHandler(this.cb_users_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ColumnAutoWidth = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tài khoản";
            this.gridColumn5.FieldName = "taikhoan";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 138;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Họ tên";
            this.gridColumn6.FieldName = "ten";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 161;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Phòng/Đội";
            this.gridColumn7.FieldName = "PhongDoi";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 228;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Chọn tài khoản:";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(2, 41);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chk_Them,
            this.chkXoa,
            this.chkSua});
            this.gridControl1.Size = new System.Drawing.Size(913, 479);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridAdd,
            this.gridDelete,
            this.gridEdit});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên dự án";
            this.gridColumn1.FieldName = "TenCT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 511;
            // 
            // gridAdd
            // 
            this.gridAdd.AppearanceCell.Options.UseTextOptions = true;
            this.gridAdd.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridAdd.AppearanceHeader.Options.UseTextOptions = true;
            this.gridAdd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridAdd.Caption = "Thêm";
            this.gridAdd.ColumnEdit = this.chk_Them;
            this.gridAdd.FieldName = "add";
            this.gridAdd.Name = "gridAdd";
            this.gridAdd.Visible = true;
            this.gridAdd.VisibleIndex = 1;
            // 
            // chk_Them
            // 
            this.chk_Them.AutoHeight = false;
            this.chk_Them.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            this.chk_Them.Name = "chk_Them";
            this.chk_Them.CheckedChanged += new System.EventHandler(this.chk_Them_CheckedChanged);
            // 
            // gridDelete
            // 
            this.gridDelete.AppearanceCell.Options.UseTextOptions = true;
            this.gridDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.gridDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridDelete.Caption = "Xóa";
            this.gridDelete.ColumnEdit = this.chkXoa;
            this.gridDelete.FieldName = "delete";
            this.gridDelete.Name = "gridDelete";
            this.gridDelete.Visible = true;
            this.gridDelete.VisibleIndex = 3;
            // 
            // chkXoa
            // 
            this.chkXoa.AutoHeight = false;
            this.chkXoa.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            this.chkXoa.Name = "chkXoa";
            this.chkXoa.CheckedChanged += new System.EventHandler(this.chkXoa_CheckedChanged);
            // 
            // gridEdit
            // 
            this.gridEdit.AppearanceCell.Options.UseTextOptions = true;
            this.gridEdit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridEdit.AppearanceHeader.Options.UseTextOptions = true;
            this.gridEdit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridEdit.Caption = "Sửa";
            this.gridEdit.ColumnEdit = this.chkSua;
            this.gridEdit.FieldName = "edit";
            this.gridEdit.Name = "gridEdit";
            this.gridEdit.Visible = true;
            this.gridEdit.VisibleIndex = 2;
            // 
            // chkSua
            // 
            this.chkSua.AutoHeight = false;
            this.chkSua.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            this.chkSua.Name = "chkSua";
            this.chkSua.CheckedChanged += new System.EventHandler(this.chkSua_CheckedChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(917, 522);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(917, 483);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 39);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(5, 39);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(917, 39);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // FrmPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 522);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "FrmPhanQuyen";
            this.Text = "Phân Quyền Dự Án";
            this.Load += new System.EventHandler(this.FrmPhanQuyen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_action.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_users.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Them)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GridLookUpEdit cb_users;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnBochonTatca;
        private DevExpress.XtraEditors.SimpleButton btnSelectall;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridAdd;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chk_Them;
        private DevExpress.XtraGrid.Columns.GridColumn gridDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkXoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkSua;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cb_action;
    }
}