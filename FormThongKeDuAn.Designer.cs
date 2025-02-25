namespace ProjectStorage
{
    partial class FormThongKeDuAn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormThongKeDuAn));
            this.dataGridView1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.treeViewProjectTasks = new System.Windows.Forms.TreeView();
            this.toFileExcel = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.btnSearchRangeDate = new DevExpress.XtraEditors.SimpleButton();
            this.dateStart = new DevExpress.XtraEditors.DateEdit();
            this.dateEnd = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnTatCaDuAn = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.cb_quyetdinh = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cb_duan = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_startdate = new System.Windows.Forms.ComboBox();
            this.buttonThongkeTheoQuyetDinh = new System.Windows.Forms.Button();
            this.buttonThongKeTheoDuAn = new System.Windows.Forms.Button();
            this.thongkeAll = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_quyetdinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_duan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 22;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 7.8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(672, 670);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.cellFormating);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.onItemDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 46);
            this.panel1.TabIndex = 10;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelTitle.Location = new System.Drawing.Point(0, 5);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(197, 29);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Thống kê Dự án";
            // 
            // panelRight
            // 
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRight.Controls.Add(this.dataGridView1);
            this.panelRight.Controls.Add(this.treeViewProjectTasks);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(306, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(674, 672);
            this.panelRight.TabIndex = 11;
            // 
            // treeViewProjectTasks
            // 
            this.treeViewProjectTasks.Location = new System.Drawing.Point(141, 297);
            this.treeViewProjectTasks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeViewProjectTasks.Name = "treeViewProjectTasks";
            this.treeViewProjectTasks.Size = new System.Drawing.Size(102, 41);
            this.treeViewProjectTasks.TabIndex = 20;
            // 
            // toFileExcel
            // 
            this.toFileExcel.Location = new System.Drawing.Point(63, 551);
            this.toFileExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toFileExcel.Name = "toFileExcel";
            this.toFileExcel.Size = new System.Drawing.Size(190, 38);
            this.toFileExcel.TabIndex = 13;
            this.toFileExcel.Text = "Xuất báo cáo";
            this.toFileExcel.UseVisualStyleBackColor = true;
            this.toFileExcel.Visible = false;
            this.toFileExcel.Click += new System.EventHandler(this.toFileExcel_Click);
            // 
            // panelLeft
            // 
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.btnSearchRangeDate);
            this.panelLeft.Controls.Add(this.dateStart);
            this.panelLeft.Controls.Add(this.dateEnd);
            this.panelLeft.Controls.Add(this.labelControl2);
            this.panelLeft.Controls.Add(this.labelControl1);
            this.panelLeft.Controls.Add(this.simpleButton3);
            this.panelLeft.Controls.Add(this.btnTatCaDuAn);
            this.panelLeft.Controls.Add(this.simpleButton2);
            this.panelLeft.Controls.Add(this.cb_quyetdinh);
            this.panelLeft.Controls.Add(this.simpleButton1);
            this.panelLeft.Controls.Add(this.cb_duan);
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.comboBox_startdate);
            this.panelLeft.Controls.Add(this.buttonThongkeTheoQuyetDinh);
            this.panelLeft.Controls.Add(this.buttonThongKeTheoDuAn);
            this.panelLeft.Controls.Add(this.toFileExcel);
            this.panelLeft.Controls.Add(this.thongkeAll);
            this.panelLeft.Controls.Add(this.label9);
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(306, 672);
            this.panelLeft.TabIndex = 12;
            // 
            // btnSearchRangeDate
            // 
            this.btnSearchRangeDate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchRangeDate.ImageOptions.Image")));
            this.btnSearchRangeDate.Location = new System.Drawing.Point(213, 428);
            this.btnSearchRangeDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearchRangeDate.Name = "btnSearchRangeDate";
            this.btnSearchRangeDate.Size = new System.Drawing.Size(87, 27);
            this.btnSearchRangeDate.TabIndex = 32;
            this.btnSearchRangeDate.Text = "Thực hiện";
            this.btnSearchRangeDate.Click += new System.EventHandler(this.btnSearchRangeDate_Click);
            // 
            // dateStart
            // 
            this.dateStart.EditValue = null;
            this.dateStart.Location = new System.Drawing.Point(9, 385);
            this.dateStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateStart.Name = "dateStart";
            this.dateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStart.Size = new System.Drawing.Size(126, 22);
            this.dateStart.TabIndex = 31;
            // 
            // dateEnd
            // 
            this.dateEnd.EditValue = null;
            this.dateEnd.Location = new System.Drawing.Point(173, 385);
            this.dateEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Size = new System.Drawing.Size(126, 22);
            this.dateEnd.TabIndex = 30;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(175, 362);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(30, 17);
            this.labelControl2.TabIndex = 28;
            this.labelControl2.Text = "Đến:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 362);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 17);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "Từ:";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButton3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(0, 597);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(304, 73);
            this.simpleButton3.TabIndex = 26;
            this.simpleButton3.Text = "Xuất báo cáo";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // btnTatCaDuAn
            // 
            this.btnTatCaDuAn.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnTatCaDuAn.ImageOptions.SvgImage")));
            this.btnTatCaDuAn.Location = new System.Drawing.Point(9, 87);
            this.btnTatCaDuAn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTatCaDuAn.Name = "btnTatCaDuAn";
            this.btnTatCaDuAn.Size = new System.Drawing.Size(289, 63);
            this.btnTatCaDuAn.TabIndex = 25;
            this.btnTatCaDuAn.Text = "Tất cả dự án";
            this.btnTatCaDuAn.Click += new System.EventHandler(this.btnTatCaDuAn_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(213, 331);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(87, 27);
            this.simpleButton2.TabIndex = 24;
            this.simpleButton2.Text = "Thực hiện";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // cb_quyetdinh
            // 
            this.cb_quyetdinh.Location = new System.Drawing.Point(9, 299);
            this.cb_quyetdinh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_quyetdinh.Name = "cb_quyetdinh";
            this.cb_quyetdinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_quyetdinh.Properties.NullText = "";
            this.cb_quyetdinh.Properties.PopupView = this.gridView1;
            this.cb_quyetdinh.Size = new System.Drawing.Size(289, 22);
            this.cb_quyetdinh.TabIndex = 23;
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 431;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(211, 233);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(87, 27);
            this.simpleButton1.TabIndex = 22;
            this.simpleButton1.Text = "Thực hiện";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // cb_duan
            // 
            this.cb_duan.Location = new System.Drawing.Point(9, 201);
            this.cb_duan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_duan.Name = "cb_duan";
            this.cb_duan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_duan.Properties.NullText = "";
            this.cb_duan.Properties.PopupView = this.searchLookUpEdit1View;
            this.cb_duan.Size = new System.Drawing.Size(289, 22);
            this.cb_duan.TabIndex = 21;
            this.cb_duan.EditValueChanged += new System.EventHandler(this.cb_duan_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 431;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Theo năm (năm khởi công)";
            // 
            // comboBox_startdate
            // 
            this.comboBox_startdate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox_startdate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_startdate.FormattingEnabled = true;
            this.comboBox_startdate.Items.AddRange(new object[] {
            "*",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025"});
            this.comboBox_startdate.Location = new System.Drawing.Point(9, 32);
            this.comboBox_startdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_startdate.Name = "comboBox_startdate";
            this.comboBox_startdate.Size = new System.Drawing.Size(136, 24);
            this.comboBox_startdate.TabIndex = 2;
            this.comboBox_startdate.SelectedIndexChanged += new System.EventHandler(this.comboBox_startdate_SelectedIndexChanged);
            // 
            // buttonThongkeTheoQuyetDinh
            // 
            this.buttonThongkeTheoQuyetDinh.Location = new System.Drawing.Point(264, 263);
            this.buttonThongkeTheoQuyetDinh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonThongkeTheoQuyetDinh.Name = "buttonThongkeTheoQuyetDinh";
            this.buttonThongkeTheoQuyetDinh.Size = new System.Drawing.Size(35, 28);
            this.buttonThongkeTheoQuyetDinh.TabIndex = 19;
            this.buttonThongkeTheoQuyetDinh.Text = "->";
            this.buttonThongkeTheoQuyetDinh.UseVisualStyleBackColor = true;
            this.buttonThongkeTheoQuyetDinh.Visible = false;
            this.buttonThongkeTheoQuyetDinh.Click += new System.EventHandler(this.buttonThongkeTheoQuyetDinh_Click);
            // 
            // buttonThongKeTheoDuAn
            // 
            this.buttonThongKeTheoDuAn.Location = new System.Drawing.Point(264, 167);
            this.buttonThongKeTheoDuAn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonThongKeTheoDuAn.Name = "buttonThongKeTheoDuAn";
            this.buttonThongKeTheoDuAn.Size = new System.Drawing.Size(35, 28);
            this.buttonThongKeTheoDuAn.TabIndex = 18;
            this.buttonThongKeTheoDuAn.Text = "->";
            this.buttonThongKeTheoDuAn.UseVisualStyleBackColor = true;
            this.buttonThongKeTheoDuAn.Visible = false;
            this.buttonThongKeTheoDuAn.Click += new System.EventHandler(this.buttonThongKeTheoDuAn_Click);
            // 
            // thongkeAll
            // 
            this.thongkeAll.Location = new System.Drawing.Point(195, 34);
            this.thongkeAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.thongkeAll.Name = "thongkeAll";
            this.thongkeAll.Size = new System.Drawing.Size(106, 21);
            this.thongkeAll.TabIndex = 17;
            this.thongkeAll.Text = "Tất cả các dự án ->";
            this.thongkeAll.UseVisualStyleBackColor = true;
            this.thongkeAll.Visible = false;
            this.thongkeAll.Click += new System.EventHandler(this.theoTatcaDuAn);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 276);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 17);
            this.label9.TabIndex = 7;
            this.label9.Text = "Thống kê theo quyết định";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Theo dự án";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelRight);
            this.panel4.Controls.Add(this.panelLeft);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 46);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(980, 672);
            this.panel4.TabIndex = 13;
            // 
            // FormThongKeDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 718);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormThongKeDuAn";
            this.Text = "Thống kê dự án";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.onLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_quyetdinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_duan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button toFileExcel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button thongkeAll;
        private System.Windows.Forms.Button buttonThongkeTheoQuyetDinh;
        private System.Windows.Forms.Button buttonThongKeTheoDuAn;
        private System.Windows.Forms.TreeView treeViewProjectTasks;
        private System.Windows.Forms.ComboBox comboBox_startdate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit cb_duan;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SearchLookUpEdit cb_quyetdinh;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnTatCaDuAn;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.DateEdit dateEnd;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateStart;
        private DevExpress.XtraEditors.SimpleButton btnSearchRangeDate;
        //private System.Windows.Forms.Button theoDuAn;
    }
}