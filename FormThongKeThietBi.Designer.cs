namespace ProjectStorage
{
    partial class FormThongKeThietBi
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
            this.dataGridView1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.toFileExcel = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDido = new System.Windows.Forms.CheckBox();
            this.checkBoxTrongkho = new System.Windows.Forms.CheckBox();
            this.comboBoxCodes = new System.Windows.Forms.ComboBox();
            this.buttonTimtheoCode = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.theoDangDiDo = new System.Windows.Forms.Button();
            this.theoTrongKho = new System.Windows.Forms.Button();
            this.theoBoPhan = new System.Windows.Forms.Button();
            this.theoCongtrinh = new System.Windows.Forms.Button();
            this.theoChungLoai = new System.Windows.Forms.Button();
            this.comboBoxBoPhan = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxCongtrinh = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxChungloai = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hieuchuanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(575, 544);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.onMouseClick);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onMouseClick2);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.onItemDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 37);
            this.panel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thống kê thiết bị";
            // 
            // panelRight
            // 
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRight.Controls.Add(this.dataGridView1);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(263, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(577, 546);
            this.panelRight.TabIndex = 11;
            // 
            // toFileExcel
            // 
            this.toFileExcel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toFileExcel.Location = new System.Drawing.Point(0, 480);
            this.toFileExcel.Name = "toFileExcel";
            this.toFileExcel.Size = new System.Drawing.Size(261, 64);
            this.toFileExcel.TabIndex = 13;
            this.toFileExcel.Text = "Xuất file Excel";
            this.toFileExcel.UseVisualStyleBackColor = true;
            this.toFileExcel.Click += new System.EventHandler(this.buttonExportToExcel);
            // 
            // panelLeft
            // 
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.groupBox1);
            this.panelLeft.Controls.Add(this.comboBoxCodes);
            this.panelLeft.Controls.Add(this.buttonTimtheoCode);
            this.panelLeft.Controls.Add(this.label3);
            this.panelLeft.Controls.Add(this.toFileExcel);
            this.panelLeft.Controls.Add(this.theoDangDiDo);
            this.panelLeft.Controls.Add(this.theoTrongKho);
            this.panelLeft.Controls.Add(this.theoBoPhan);
            this.panelLeft.Controls.Add(this.theoCongtrinh);
            this.panelLeft.Controls.Add(this.theoChungLoai);
            this.panelLeft.Controls.Add(this.comboBoxBoPhan);
            this.panelLeft.Controls.Add(this.label5);
            this.panelLeft.Controls.Add(this.comboBoxCongtrinh);
            this.panelLeft.Controls.Add(this.label9);
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Controls.Add(this.comboBoxChungloai);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(263, 546);
            this.panelLeft.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxDido);
            this.groupBox1.Controls.Add(this.checkBoxTrongkho);
            this.groupBox1.Location = new System.Drawing.Point(2, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 46);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc thiết bị";
            // 
            // checkBoxDido
            // 
            this.checkBoxDido.AutoSize = true;
            this.checkBoxDido.Checked = true;
            this.checkBoxDido.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDido.Location = new System.Drawing.Point(127, 19);
            this.checkBoxDido.Name = "checkBoxDido";
            this.checkBoxDido.Size = new System.Drawing.Size(68, 17);
            this.checkBoxDido.TabIndex = 2;
            this.checkBoxDido.Text = "Lọc đi đo";
            this.checkBoxDido.UseVisualStyleBackColor = true;
            this.checkBoxDido.CheckedChanged += new System.EventHandler(this.checkBoxDido_CheckedChanged);
            // 
            // checkBoxTrongkho
            // 
            this.checkBoxTrongkho.AutoSize = true;
            this.checkBoxTrongkho.Checked = true;
            this.checkBoxTrongkho.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTrongkho.Location = new System.Drawing.Point(6, 19);
            this.checkBoxTrongkho.Name = "checkBoxTrongkho";
            this.checkBoxTrongkho.Size = new System.Drawing.Size(91, 17);
            this.checkBoxTrongkho.TabIndex = 1;
            this.checkBoxTrongkho.Text = "Lọc trong kho";
            this.checkBoxTrongkho.UseVisualStyleBackColor = true;
            this.checkBoxTrongkho.CheckedChanged += new System.EventHandler(this.checkBoxTrongkho_CheckedChanged);
            // 
            // comboBoxCodes
            // 
            this.comboBoxCodes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxCodes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCodes.FormattingEnabled = true;
            this.comboBoxCodes.Location = new System.Drawing.Point(6, 81);
            this.comboBoxCodes.Name = "comboBoxCodes";
            this.comboBoxCodes.Size = new System.Drawing.Size(212, 21);
            this.comboBoxCodes.TabIndex = 1;
            // 
            // buttonTimtheoCode
            // 
            this.buttonTimtheoCode.Location = new System.Drawing.Point(222, 81);
            this.buttonTimtheoCode.Name = "buttonTimtheoCode";
            this.buttonTimtheoCode.Size = new System.Drawing.Size(31, 23);
            this.buttonTimtheoCode.TabIndex = 2;
            this.buttonTimtheoCode.Text = "->";
            this.buttonTimtheoCode.UseVisualStyleBackColor = true;
            this.buttonTimtheoCode.Click += new System.EventHandler(this.buttonTimtheoCode_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Tìm theo mã thiết bị";
            // 
            // theoDangDiDo
            // 
            this.theoDangDiDo.Location = new System.Drawing.Point(5, 337);
            this.theoDangDiDo.Name = "theoDangDiDo";
            this.theoDangDiDo.Size = new System.Drawing.Size(250, 23);
            this.theoDangDiDo.TabIndex = 10;
            this.theoDangDiDo.Text = "Tất cả các thiết bị đang đi đo ->";
            this.theoDangDiDo.UseVisualStyleBackColor = true;
            this.theoDangDiDo.Click += new System.EventHandler(this.theoDangDiDo_Click);
            // 
            // theoTrongKho
            // 
            this.theoTrongKho.Location = new System.Drawing.Point(6, 308);
            this.theoTrongKho.Name = "theoTrongKho";
            this.theoTrongKho.Size = new System.Drawing.Size(250, 23);
            this.theoTrongKho.TabIndex = 9;
            this.theoTrongKho.Text = "Tất cả các thiết bị trong kho ->";
            this.theoTrongKho.UseVisualStyleBackColor = true;
            this.theoTrongKho.Click += new System.EventHandler(this.theoTrongKho_Click);
            // 
            // theoBoPhan
            // 
            this.theoBoPhan.Location = new System.Drawing.Point(223, 258);
            this.theoBoPhan.Name = "theoBoPhan";
            this.theoBoPhan.Size = new System.Drawing.Size(31, 23);
            this.theoBoPhan.TabIndex = 8;
            this.theoBoPhan.Text = "->";
            this.theoBoPhan.UseVisualStyleBackColor = true;
            this.theoBoPhan.Click += new System.EventHandler(this.theoBoPhan_Click);
            // 
            // theoCongtrinh
            // 
            this.theoCongtrinh.Location = new System.Drawing.Point(223, 196);
            this.theoCongtrinh.Name = "theoCongtrinh";
            this.theoCongtrinh.Size = new System.Drawing.Size(31, 23);
            this.theoCongtrinh.TabIndex = 6;
            this.theoCongtrinh.Text = "->";
            this.theoCongtrinh.UseVisualStyleBackColor = true;
            this.theoCongtrinh.Click += new System.EventHandler(this.theoCongtrinh_Click);
            // 
            // theoChungLoai
            // 
            this.theoChungLoai.Location = new System.Drawing.Point(224, 138);
            this.theoChungLoai.Name = "theoChungLoai";
            this.theoChungLoai.Size = new System.Drawing.Size(31, 23);
            this.theoChungLoai.TabIndex = 4;
            this.theoChungLoai.Text = "->";
            this.theoChungLoai.UseVisualStyleBackColor = true;
            this.theoChungLoai.Click += new System.EventHandler(this.theoChungLoai_Click);
            // 
            // comboBoxBoPhan
            // 
            this.comboBoxBoPhan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxBoPhan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxBoPhan.FormattingEnabled = true;
            this.comboBoxBoPhan.Location = new System.Drawing.Point(6, 258);
            this.comboBoxBoPhan.Name = "comboBoxBoPhan";
            this.comboBoxBoPhan.Size = new System.Drawing.Size(212, 21);
            this.comboBoxBoPhan.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Thống kê theo bộ phận sử dụng";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // comboBoxCongtrinh
            // 
            this.comboBoxCongtrinh.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxCongtrinh.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCongtrinh.FormattingEnabled = true;
            this.comboBoxCongtrinh.Location = new System.Drawing.Point(6, 196);
            this.comboBoxCongtrinh.Name = "comboBoxCongtrinh";
            this.comboBoxCongtrinh.Size = new System.Drawing.Size(212, 21);
            this.comboBoxCongtrinh.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(169, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Thống kê theo công trình sử dụng";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Theo chủng loại";
            // 
            // comboBoxChungloai
            // 
            this.comboBoxChungloai.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxChungloai.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxChungloai.FormattingEnabled = true;
            this.comboBoxChungloai.Location = new System.Drawing.Point(6, 138);
            this.comboBoxChungloai.Name = "comboBoxChungloai";
            this.comboBoxChungloai.Size = new System.Drawing.Size(212, 21);
            this.comboBoxChungloai.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelRight);
            this.panel4.Controls.Add(this.panelLeft);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 37);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(840, 546);
            this.panel4.TabIndex = 13;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hieuchuanToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(183, 26);
            // 
            // hieuchuanToolStripMenuItem
            // 
            this.hieuchuanToolStripMenuItem.Name = "hieuchuanToolStripMenuItem";
            this.hieuchuanToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.hieuchuanToolStripMenuItem.Text = "Theo dõi hiệu chuẩn";
            this.hieuchuanToolStripMenuItem.Click += new System.EventHandler(this.hieuchuanToolStripMenuItem_Click);
            // 
            // FormThongKeThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 583);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "FormThongKeThietBi";
            this.Text = "Thống kê thiết bị";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.onLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox comboBoxChungloai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button toFileExcel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxCongtrinh;
        private System.Windows.Forms.ComboBox comboBoxBoPhan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button theoDangDiDo;
        private System.Windows.Forms.Button theoTrongKho;
        private System.Windows.Forms.Button theoBoPhan;
        private System.Windows.Forms.Button theoCongtrinh;
        private System.Windows.Forms.Button theoChungLoai;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonTimtheoCode;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem hieuchuanToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxCodes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxDido;
        private System.Windows.Forms.CheckBox checkBoxTrongkho;
    }
}