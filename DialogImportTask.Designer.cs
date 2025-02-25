namespace ProjectStorage
{
    partial class DialogImportTask
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxTeamlist = new System.Windows.Forms.ComboBox();
            this.textBoxPhieuNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPhieuName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxProjectList = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxTableToRow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTableFromRow = new System.Windows.Forms.TextBox();
            this.buttonStartProcess = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxExcelFile = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPhieuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyPhieuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.comboBoxTeamlist);
            this.panel1.Controls.Add(this.textBoxPhieuNo);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBoxPhieuName);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.comboBoxProjectList);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 566);
            this.panel1.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Đội thi công";
            // 
            // comboBoxTeamlist
            // 
            this.comboBoxTeamlist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxTeamlist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxTeamlist.FormattingEnabled = true;
            this.comboBoxTeamlist.Location = new System.Drawing.Point(2, 152);
            this.comboBoxTeamlist.Name = "comboBoxTeamlist";
            this.comboBoxTeamlist.Size = new System.Drawing.Size(285, 21);
            this.comboBoxTeamlist.TabIndex = 4;
            // 
            // textBoxPhieuNo
            // 
            this.textBoxPhieuNo.Location = new System.Drawing.Point(3, 113);
            this.textBoxPhieuNo.Name = "textBoxPhieuNo";
            this.textBoxPhieuNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxPhieuNo.Size = new System.Drawing.Size(284, 21);
            this.textBoxPhieuNo.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Số quyết định";
            // 
            // textBoxPhieuName
            // 
            this.textBoxPhieuName.Location = new System.Drawing.Point(3, 74);
            this.textBoxPhieuName.Name = "textBoxPhieuName";
            this.textBoxPhieuName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxPhieuName.Size = new System.Drawing.Size(284, 21);
            this.textBoxPhieuName.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Tên quyết định";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2, 440);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(285, 65);
            this.button1.TabIndex = 14;
            this.button1.Text = "Import vào Database";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Chọn dự án:";
            // 
            // comboBoxProjectList
            // 
            this.comboBoxProjectList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxProjectList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxProjectList.FormattingEnabled = true;
            this.comboBoxProjectList.Location = new System.Drawing.Point(2, 24);
            this.comboBoxProjectList.Name = "comboBoxProjectList";
            this.comboBoxProjectList.Size = new System.Drawing.Size(285, 21);
            this.comboBoxProjectList.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxTableToRow);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxTableFromRow);
            this.groupBox2.Controls.Add(this.buttonStartProcess);
            this.groupBox2.Controls.Add(this.buttonOpenFile);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxExcelFile);
            this.groupBox2.Location = new System.Drawing.Point(2, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 162);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dữ liệu bảng công việc";
            // 
            // textBoxTableToRow
            // 
            this.textBoxTableToRow.Location = new System.Drawing.Point(237, 83);
            this.textBoxTableToRow.Name = "textBoxTableToRow";
            this.textBoxTableToRow.Size = new System.Drawing.Size(42, 21);
            this.textBoxTableToRow.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "đến";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = " Bảng công việc:  từ hàng";
            // 
            // textBoxTableFromRow
            // 
            this.textBoxTableFromRow.Location = new System.Drawing.Point(148, 83);
            this.textBoxTableFromRow.Name = "textBoxTableFromRow";
            this.textBoxTableFromRow.Size = new System.Drawing.Size(42, 21);
            this.textBoxTableFromRow.TabIndex = 8;
            this.textBoxTableFromRow.Text = "10";
            // 
            // buttonStartProcess
            // 
            this.buttonStartProcess.Location = new System.Drawing.Point(117, 121);
            this.buttonStartProcess.Name = "buttonStartProcess";
            this.buttonStartProcess.Size = new System.Drawing.Size(162, 28);
            this.buttonStartProcess.TabIndex = 11;
            this.buttonStartProcess.Text = "Tải dữ liệu từ file excel";
            this.buttonStartProcess.UseVisualStyleBackColor = true;
            this.buttonStartProcess.Click += new System.EventHandler(this.buttonStartProcess_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(63, 19);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(33, 23);
            this.buttonOpenFile.TabIndex = 6;
            this.buttonOpenFile.Text = "...";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "File excel";
            // 
            // textBoxExcelFile
            // 
            this.textBoxExcelFile.Location = new System.Drawing.Point(2, 45);
            this.textBoxExcelFile.Name = "textBoxExcelFile";
            this.textBoxExcelFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxExcelFile.Size = new System.Drawing.Size(271, 21);
            this.textBoxExcelFile.TabIndex = 7;
            this.textBoxExcelFile.Tag = "6";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPhieuToolStripMenuItem,
            this.modifyPhieuToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(215, 76);
            // 
            // addPhieuToolStripMenuItem
            // 
            this.addPhieuToolStripMenuItem.Name = "addPhieuToolStripMenuItem";
            this.addPhieuToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.addPhieuToolStripMenuItem.Text = "Thêm quyết định giao việc";
            // 
            // modifyPhieuToolStripMenuItem
            // 
            this.modifyPhieuToolStripMenuItem.Name = "modifyPhieuToolStripMenuItem";
            this.modifyPhieuToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.modifyPhieuToolStripMenuItem.Text = "Sửa quyết định giao việc";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(211, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.removeToolStripMenuItem.Text = "Xóa quyết định giao việc";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(311, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1241, 566);
            this.panel4.TabIndex = 13;
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
            this.dataGridView1.Size = new System.Drawing.Size(1239, 564);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.onItemClick);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cellMouseClick);
            // 
            // DialogImportTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1552, 566);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogImportTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import công việc";
            this.Load += new System.EventHandler(this.onLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addPhieuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyPhieuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.TextBox textBoxExcelFile;
        private System.Windows.Forms.TextBox textBoxTableToRow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTableFromRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonStartProcess;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxProjectList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridView1;
        private System.Windows.Forms.TextBox textBoxPhieuNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPhieuName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxTeamlist;
    }
}