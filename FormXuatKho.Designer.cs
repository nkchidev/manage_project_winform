namespace ProjectStorage
{
    partial class FormXuatKho
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonXuatkho = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxNguoiXuat = new System.Windows.Forms.TextBox();
            this.comboBoxBoPhan = new System.Windows.Forms.ComboBox();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxGhichu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxNguoiMuon = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addPhieuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyPhieuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.dataGridViewThietBiTrongKho = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxChungLoai = new System.Windows.Forms.ComboBox();
            this.buttonAddToList = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThietBiTrongKho)).BeginInit();
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridView1.Location = new System.Drawing.Point(22, 274);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(549, 231);
            this.dataGridView1.TabIndex = 10;
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
            this.label1.Size = new System.Drawing.Size(177, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cho mượn thiết bị";
            // 
            // panelRight
            // 
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRight.Controls.Add(this.label7);
            this.panelRight.Controls.Add(this.button3);
            this.panelRight.Controls.Add(this.buttonXuatkho);
            this.panelRight.Controls.Add(this.groupBox2);
            this.panelRight.Controls.Add(this.dataGridView1);
            this.panelRight.Location = new System.Drawing.Point(269, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(571, 546);
            this.panelRight.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Danh sách thiết bị mượn";
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(493, 511);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Hủy yêu cầu";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // buttonXuatkho
            // 
            this.buttonXuatkho.Location = new System.Drawing.Point(303, 511);
            this.buttonXuatkho.Name = "buttonXuatkho";
            this.buttonXuatkho.Size = new System.Drawing.Size(174, 23);
            this.buttonXuatkho.TabIndex = 11;
            this.buttonXuatkho.Text = "Xác nhận mượn và xuất phiếu";
            this.buttonXuatkho.UseVisualStyleBackColor = true;
            this.buttonXuatkho.Click += new System.EventHandler(this.xuatkho);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBoxNguoiXuat);
            this.groupBox2.Controls.Add(this.comboBoxBoPhan);
            this.groupBox2.Controls.Add(this.comboBoxProject);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxGhichu);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxNguoiMuon);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(22, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(543, 240);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(278, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Công trình sử dụng";
            // 
            // textBoxNguoiXuat
            // 
            this.textBoxNguoiXuat.Location = new System.Drawing.Point(279, 37);
            this.textBoxNguoiXuat.Name = "textBoxNguoiXuat";
            this.textBoxNguoiXuat.Size = new System.Drawing.Size(252, 21);
            this.textBoxNguoiXuat.TabIndex = 5;
            // 
            // comboBoxBoPhan
            // 
            this.comboBoxBoPhan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxBoPhan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxBoPhan.FormattingEnabled = true;
            this.comboBoxBoPhan.Location = new System.Drawing.Point(12, 75);
            this.comboBoxBoPhan.Name = "comboBoxBoPhan";
            this.comboBoxBoPhan.Size = new System.Drawing.Size(252, 21);
            this.comboBoxBoPhan.TabIndex = 6;
            // 
            // comboBoxProject
            // 
            this.comboBoxProject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxProject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Location = new System.Drawing.Point(279, 76);
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.Size = new System.Drawing.Size(251, 21);
            this.comboBoxProject.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(276, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Họ và tên người xuất";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 202);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Ngày";
            // 
            // textBoxGhichu
            // 
            this.textBoxGhichu.Location = new System.Drawing.Point(12, 119);
            this.textBoxGhichu.Multiline = true;
            this.textBoxGhichu.Name = "textBoxGhichu";
            this.textBoxGhichu.Size = new System.Drawing.Size(252, 61);
            this.textBoxGhichu.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Ghi chú";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Bộ phận";
            // 
            // textBoxNguoiMuon
            // 
            this.textBoxNguoiMuon.Location = new System.Drawing.Point(12, 36);
            this.textBoxNguoiMuon.Name = "textBoxNguoiMuon";
            this.textBoxNguoiMuon.Size = new System.Drawing.Size(252, 21);
            this.textBoxNguoiMuon.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Họ và tên người mượn";
            // 
            // addPhieuToolStripMenuItem
            // 
            this.addPhieuToolStripMenuItem.Name = "addPhieuToolStripMenuItem";
            this.addPhieuToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.addPhieuToolStripMenuItem.Text = "Thêm quyết định giao việc";
            // 
            // modifyPhieuToolStripMenuItem
            // 
            this.modifyPhieuToolStripMenuItem.Name = "modifyPhieuToolStripMenuItem";
            this.modifyPhieuToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.modifyPhieuToolStripMenuItem.Text = "Sửa quyết định giao việc";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(212, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.removeToolStripMenuItem.Text = "Xóa quyết định giao việc";
            // 
            // panelLeft
            // 
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.dataGridViewThietBiTrongKho);
            this.panelLeft.Controls.Add(this.textBoxDesc);
            this.panelLeft.Controls.Add(this.label3);
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Controls.Add(this.comboBoxChungLoai);
            this.panelLeft.Controls.Add(this.buttonAddToList);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(263, 546);
            this.panelLeft.TabIndex = 12;
            // 
            // dataGridViewThietBiTrongKho
            // 
            this.dataGridViewThietBiTrongKho.AllowUserToAddRows = false;
            this.dataGridViewThietBiTrongKho.AllowUserToDeleteRows = false;
            this.dataGridViewThietBiTrongKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewThietBiTrongKho.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewThietBiTrongKho.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(250)))));
            this.dataGridViewThietBiTrongKho.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewThietBiTrongKho.ColumnHeadersHeight = 22;
            this.dataGridViewThietBiTrongKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewThietBiTrongKho.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewThietBiTrongKho.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewThietBiTrongKho.Location = new System.Drawing.Point(6, 73);
            this.dataGridViewThietBiTrongKho.Name = "dataGridViewThietBiTrongKho";
            this.dataGridViewThietBiTrongKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewThietBiTrongKho.Size = new System.Drawing.Size(248, 269);
            this.dataGridViewThietBiTrongKho.TabIndex = 2;
            this.dataGridViewThietBiTrongKho.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onThietbiTrongKhoClick);
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDesc.Location = new System.Drawing.Point(9, 379);
            this.textBoxDesc.Multiline = true;
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.ReadOnly = true;
            this.textBoxDesc.Size = new System.Drawing.Size(248, 150);
            this.textBoxDesc.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Thiết bị trong kho";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Chủng loại";
            // 
            // comboBoxChungLoai
            // 
            this.comboBoxChungLoai.FormattingEnabled = true;
            this.comboBoxChungLoai.Location = new System.Drawing.Point(6, 16);
            this.comboBoxChungLoai.Name = "comboBoxChungLoai";
            this.comboBoxChungLoai.Size = new System.Drawing.Size(251, 21);
            this.comboBoxChungLoai.TabIndex = 1;
            this.comboBoxChungLoai.SelectedIndexChanged += new System.EventHandler(this.onSelectChungLoai);
            // 
            // buttonAddToList
            // 
            this.buttonAddToList.Location = new System.Drawing.Point(179, 348);
            this.buttonAddToList.Name = "buttonAddToList";
            this.buttonAddToList.Size = new System.Drawing.Size(75, 23);
            this.buttonAddToList.TabIndex = 3;
            this.buttonAddToList.Text = "Chuyển ->";
            this.buttonAddToList.UseVisualStyleBackColor = true;
            this.buttonAddToList.Click += new System.EventHandler(this.buttonAddToList_Click);
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
            // FormXuatKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 583);
            this.ControlBox = false;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "FormXuatKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mượn thiết bị";
            this.Load += new System.EventHandler(this.onLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThietBiTrongKho)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelRight;

        private System.Windows.Forms.ToolStripMenuItem addPhieuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyPhieuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.Button buttonAddToList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxChungLoai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonXuatkho;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxGhichu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox textBoxNguoiMuon;
        public System.Windows.Forms.ComboBox comboBoxBoPhan;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.ComboBox comboBoxProject;
        public System.Windows.Forms.TextBox textBoxNguoiXuat;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewThietBiTrongKho;
    }
}