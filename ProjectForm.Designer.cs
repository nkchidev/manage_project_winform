namespace ProjectStorage
{
    partial class ProjectForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectForm));
            this.contextMenuStripThem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuTaskSumary = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTaskDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.hideShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyPaidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuColumn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xoaThanhtoanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabelPhieuName = new System.Windows.Forms.LinkLabel();
            this.treeViewProjectTasks = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cb_thang_export = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.contextMenuStripThem.SuspendLayout();
            this.contextMenuColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_thang_export.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripThem
            // 
            this.contextMenuStripThem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTaskSumary,
            this.menuTaskDetail,
            this.toolStripMenuItem2,
            this.hideShowToolStripMenuItem,
            this.monthlyPaidToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exportToExcelToolStripMenuItem});
            this.contextMenuStripThem.Name = "contextMenuStrip1";
            this.contextMenuStripThem.Size = new System.Drawing.Size(271, 154);
            // 
            // menuTaskSumary
            // 
            this.menuTaskSumary.Name = "menuTaskSumary";
            this.menuTaskSumary.Size = new System.Drawing.Size(270, 22);
            this.menuTaskSumary.Text = "+ Danh mục công việc";
            this.menuTaskSumary.Click += new System.EventHandler(this.addTaskToProject);
            // 
            // menuTaskDetail
            // 
            this.menuTaskDetail.Name = "menuTaskDetail";
            this.menuTaskDetail.Size = new System.Drawing.Size(270, 22);
            this.menuTaskDetail.Text = "+ Công việc cụ thể";
            this.menuTaskDetail.Click += new System.EventHandler(this.addTaskDetail);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(267, 6);
            // 
            // hideShowToolStripMenuItem
            // 
            this.hideShowToolStripMenuItem.Name = "hideShowToolStripMenuItem";
            this.hideShowToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.hideShowToolStripMenuItem.Text = "Hide/Show \"Thanh toán hàng tháng\"";
            this.hideShowToolStripMenuItem.Click += new System.EventHandler(this.hideShowToolStripMenuItem_Click);
            // 
            // monthlyPaidToolStripMenuItem
            // 
            this.monthlyPaidToolStripMenuItem.Name = "monthlyPaidToolStripMenuItem";
            this.monthlyPaidToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.monthlyPaidToolStripMenuItem.Text = "Thanh toán hàng tháng";
            this.monthlyPaidToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemThanhtoan_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(267, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.removeToolStripMenuItem.Text = "Xóa mục";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeTask);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(267, 6);
            // 
            // exportToExcelToolStripMenuItem
            // 
            this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
            this.exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.exportToExcelToolStripMenuItem.Text = "Xuất QĐ giao nhiệm vụ";
            this.exportToExcelToolStripMenuItem.Click += new System.EventHandler(this.exportToExcelToolStripMenuItem_Click);
            // 
            // contextMenuColumn
            // 
            this.contextMenuColumn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xoaThanhtoanToolStripMenuItem});
            this.contextMenuColumn.Name = "contextMenuColumn";
            this.contextMenuColumn.Size = new System.Drawing.Size(178, 26);
            // 
            // xoaThanhtoanToolStripMenuItem
            // 
            this.xoaThanhtoanToolStripMenuItem.Name = "xoaThanhtoanToolStripMenuItem";
            this.xoaThanhtoanToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.xoaThanhtoanToolStripMenuItem.Text = "Xóa cột Thanh toán";
            this.xoaThanhtoanToolStripMenuItem.Click += new System.EventHandler(this.xoaThanhtoanToolStripMenuItem_Click);
            // 
            // linkLabelPhieuName
            // 
            this.linkLabelPhieuName.AutoSize = true;
            this.linkLabelPhieuName.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabelPhieuName.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelPhieuName.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelPhieuName.Location = new System.Drawing.Point(0, 0);
            this.linkLabelPhieuName.Name = "linkLabelPhieuName";
            this.linkLabelPhieuName.Size = new System.Drawing.Size(88, 23);
            this.linkLabelPhieuName.TabIndex = 2;
            this.linkLabelPhieuName.TabStop = true;
            this.linkLabelPhieuName.Text = "linkLabel1";
            this.linkLabelPhieuName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.onProjectClick);
            // 
            // treeViewProjectTasks
            // 
            this.treeViewProjectTasks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewProjectTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewProjectTasks.HideSelection = false;
            this.treeViewProjectTasks.Location = new System.Drawing.Point(0, 23);
            this.treeViewProjectTasks.Name = "treeViewProjectTasks";
            this.treeViewProjectTasks.Size = new System.Drawing.Size(403, 580);
            this.treeViewProjectTasks.TabIndex = 0;
            this.treeViewProjectTasks.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.onNodeClick);
            this.treeViewProjectTasks.DoubleClick += new System.EventHandler(this.modifyJob);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 22;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(933, 597);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.cellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.cellEndEdit);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.cellFormating);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.onColumnClick);
            // 
            // dockManager1
            // 
            this.dockManager1.AutoHideSpeed = 10;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("fbbc26e8-ef36-4d03-9dd2-5a750d0e2805");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(410, 200);
            this.dockPanel1.Size = new System.Drawing.Size(410, 636);
            this.dockPanel1.Text = "Nội dung dự án";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.treeViewProjectTasks);
            this.dockPanel1_Container.Controls.Add(this.linkLabelPhieuName);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 30);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(403, 603);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(410, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 636);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cb_thang_export);
            this.panel2.Controls.Add(this.btnExportExcel);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(933, 39);
            this.panel2.TabIndex = 13;
            // 
            // cb_thang_export
            // 
            this.cb_thang_export.Location = new System.Drawing.Point(58, 10);
            this.cb_thang_export.Name = "cb_thang_export";
            this.cb_thang_export.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_thang_export.Properties.NullText = "[Chọn tháng]";
            this.cb_thang_export.Properties.PopupView = this.gridLookUpEdit1View;
            this.cb_thang_export.Size = new System.Drawing.Size(137, 20);
            this.cb_thang_export.TabIndex = 11;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tháng";
            this.gridColumn1.FieldName = "month_text";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.ImageOptions.Image")));
            this.btnExportExcel.Location = new System.Drawing.Point(206, 8);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(91, 23);
            this.btnExportExcel.TabIndex = 12;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(14, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(38, 13);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "Tháng:";
            // 
            // ProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 636);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dockPanel1);
            this.Name = "ProjectForm";
            this.Text = "Danh sách dự toán";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.onLoad);
            this.contextMenuStripThem.ResumeLayout(false);
            this.contextMenuColumn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel1_Container.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_thang_export.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripThem;
        private System.Windows.Forms.ToolStripMenuItem menuTaskSumary;
        private System.Windows.Forms.ToolStripMenuItem menuTaskDetail;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem monthlyPaidToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuColumn;
        private System.Windows.Forms.ToolStripMenuItem xoaThanhtoanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideShowToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabelPhieuName;
        private System.Windows.Forms.TreeView treeViewProjectTasks;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridView1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.GridLookUpEdit cb_thang_export;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.Panel panel2;
    }
}