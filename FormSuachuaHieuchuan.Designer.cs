namespace ProjectStorage
{
    partial class FormSuachuaHieuchuan
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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.comboBoxDeviceCode = new System.Windows.Forms.ComboBox();
            this.buttonDeviceCode = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.theoChungLoai = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxChungloai = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.suachuaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hieuchuanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panel4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            this.dataGridView1.Size = new System.Drawing.Size(575, 550);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onMouseClick);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.onItemDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 31);
            this.panel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sửa chữa - Hiệu chuẩn";
            // 
            // panelRight
            // 
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRight.Controls.Add(this.dataGridView1);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(263, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(577, 552);
            this.panelRight.TabIndex = 11;
            // 
            // panelLeft
            // 
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.comboBoxDeviceCode);
            this.panelLeft.Controls.Add(this.buttonDeviceCode);
            this.panelLeft.Controls.Add(this.label3);
            this.panelLeft.Controls.Add(this.theoChungLoai);
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Controls.Add(this.comboBoxChungloai);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(263, 552);
            this.panelLeft.TabIndex = 12;
            // 
            // comboBoxDeviceCode
            // 
            this.comboBoxDeviceCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDeviceCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDeviceCode.FormattingEnabled = true;
            this.comboBoxDeviceCode.Location = new System.Drawing.Point(9, 88);
            this.comboBoxDeviceCode.Name = "comboBoxDeviceCode";
            this.comboBoxDeviceCode.Size = new System.Drawing.Size(209, 21);
            this.comboBoxDeviceCode.TabIndex = 18;
            // 
            // buttonDeviceCode
            // 
            this.buttonDeviceCode.Location = new System.Drawing.Point(224, 88);
            this.buttonDeviceCode.Name = "buttonDeviceCode";
            this.buttonDeviceCode.Size = new System.Drawing.Size(31, 23);
            this.buttonDeviceCode.TabIndex = 17;
            this.buttonDeviceCode.Text = "->";
            this.buttonDeviceCode.UseVisualStyleBackColor = true;
            this.buttonDeviceCode.Click += new System.EventHandler(this.buttonDeviceCode_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Theo mã thiết bị";
            // 
            // theoChungLoai
            // 
            this.theoChungLoai.Location = new System.Drawing.Point(224, 19);
            this.theoChungLoai.Name = "theoChungLoai";
            this.theoChungLoai.Size = new System.Drawing.Size(31, 23);
            this.theoChungLoai.TabIndex = 14;
            this.theoChungLoai.Text = "->";
            this.theoChungLoai.UseVisualStyleBackColor = true;
            this.theoChungLoai.Click += new System.EventHandler(this.theoChungLoai_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
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
            this.comboBoxChungloai.Location = new System.Drawing.Point(6, 19);
            this.comboBoxChungloai.Name = "comboBoxChungloai";
            this.comboBoxChungloai.Size = new System.Drawing.Size(212, 21);
            this.comboBoxChungloai.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelRight);
            this.panel4.Controls.Add(this.panelLeft);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 31);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(840, 552);
            this.panel4.TabIndex = 13;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.suachuaToolStripMenuItem,
            this.hieuchuanToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 48);
            // 
            // suachuaToolStripMenuItem
            // 
            this.suachuaToolStripMenuItem.Name = "suachuaToolStripMenuItem";
            this.suachuaToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.suachuaToolStripMenuItem.Text = "Sửa chữa thiết bị";
            this.suachuaToolStripMenuItem.Click += new System.EventHandler(this.suachuaToolStripMenuItem_Click);
            // 
            // hieuchuanToolStripMenuItem
            // 
            this.hieuchuanToolStripMenuItem.Name = "hieuchuanToolStripMenuItem";
            this.hieuchuanToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.hieuchuanToolStripMenuItem.Text = "Hiệu chuẩn thiết bị";
            this.hieuchuanToolStripMenuItem.Click += new System.EventHandler(this.hieuchuanToolStripMenuItem_Click);
            // 
            // FormSuachuaHieuchuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 583);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "FormSuachuaHieuchuan";
            this.Text = "Sửa chữa - Hiệu chuẩn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.onLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.Button theoChungLoai;
        private System.Windows.Forms.Button buttonDeviceCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxDeviceCode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem suachuaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hieuchuanToolStripMenuItem;
    }
}