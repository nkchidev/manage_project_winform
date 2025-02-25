namespace ProjectStorage
{
    partial class HideThanhtoanColumnsDialog
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.button_CheckAll = new System.Windows.Forms.Button();
            this.button_ClearAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(12, 355);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(170, 46);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "Hide checked";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(188, 378);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(251, 316);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // button_CheckAll
            // 
            this.button_CheckAll.Location = new System.Drawing.Point(93, 4);
            this.button_CheckAll.Name = "button_CheckAll";
            this.button_CheckAll.Size = new System.Drawing.Size(75, 23);
            this.button_CheckAll.TabIndex = 7;
            this.button_CheckAll.Text = "Check all";
            this.button_CheckAll.UseVisualStyleBackColor = true;
            this.button_CheckAll.Click += new System.EventHandler(this.button_CheckAll_Click);
            // 
            // button_ClearAll
            // 
            this.button_ClearAll.Location = new System.Drawing.Point(188, 4);
            this.button_ClearAll.Name = "button_ClearAll";
            this.button_ClearAll.Size = new System.Drawing.Size(75, 23);
            this.button_ClearAll.TabIndex = 8;
            this.button_ClearAll.Text = "Clear all";
            this.button_ClearAll.UseVisualStyleBackColor = true;
            this.button_ClearAll.Click += new System.EventHandler(this.button_ClearAll_Click);
            // 
            // HideThanhtoanColumnsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 405);
            this.ControlBox = false;
            this.Controls.Add(this.button_ClearAll);
            this.Controls.Add(this.button_CheckAll);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "HideThanhtoanColumnsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check to HIDE ThanhToanHangThang";
            this.Load += new System.EventHandler(this.HideThanhtoanColumnsDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button_CheckAll;
        private System.Windows.Forms.Button button_ClearAll;
    }
}