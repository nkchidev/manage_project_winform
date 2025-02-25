namespace ProjectStorage
{
    partial class DialogQuanlyPhongDoi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.addPhieuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyPhieuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.suaphongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPhongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePhongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelRight.SuspendLayout();
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(840, 470);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onMouseClick);
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quản lý phòng/đội";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.dataGridView1);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(840, 470);
            this.panelRight.TabIndex = 11;
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
            // panel4
            // 
            this.panel4.Controls.Add(this.panelRight);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 37);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(840, 470);
            this.panel4.TabIndex = 13;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.suaphongToolStripMenuItem,
            this.addPhongToolStripMenuItem,
            this.deletePhongToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 92);
            // 
            // suaphongToolStripMenuItem
            // 
            this.suaphongToolStripMenuItem.Name = "suaphongToolStripMenuItem";
            this.suaphongToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.suaphongToolStripMenuItem.Text = "Sửa phòng/đội";
            this.suaphongToolStripMenuItem.Click += new System.EventHandler(this.suaphongToolStripMenuItem_Click_1);
            // 
            // addPhongToolStripMenuItem
            // 
            this.addPhongToolStripMenuItem.Name = "addPhongToolStripMenuItem";
            this.addPhongToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addPhongToolStripMenuItem.Text = "Thêm phòng/đội";
            this.addPhongToolStripMenuItem.Click += new System.EventHandler(this.addPhongToolStripMenuItem_Click_1);
            // 
            // deletePhongToolStripMenuItem
            // 
            this.deletePhongToolStripMenuItem.Name = "deletePhongToolStripMenuItem";
            this.deletePhongToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.deletePhongToolStripMenuItem.Text = "Xóa phòng/đội";
            this.deletePhongToolStripMenuItem.Click += new System.EventHandler(this.deletePhongToolStripMenuItem_Click_1);
            // 
            // DialogQuanlyPhongDoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 507);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "DialogQuanlyPhongDoi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý phòng/đội";
            this.Load += new System.EventHandler(this.onLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelRight;
        //private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addPhieuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyPhieuToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem suaphongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPhongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePhongToolStripMenuItem;
    }
}