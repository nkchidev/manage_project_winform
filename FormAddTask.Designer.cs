namespace ProjectStorage
{
    partial class FormAddTask
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTask = new System.Windows.Forms.TextBox();
            this.buttonAddTask = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Công việc";
            // 
            // textBoxTask
            // 
            this.textBoxTask.Location = new System.Drawing.Point(5, 26);
            this.textBoxTask.Name = "textBoxTask";
            this.textBoxTask.Size = new System.Drawing.Size(267, 20);
            this.textBoxTask.TabIndex = 1;
            // 
            // buttonAddTask
            // 
            this.buttonAddTask.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAddTask.Location = new System.Drawing.Point(116, 76);
            this.buttonAddTask.Name = "buttonAddTask";
            this.buttonAddTask.Size = new System.Drawing.Size(75, 23);
            this.buttonAddTask.TabIndex = 2;
            this.buttonAddTask.Text = "OK";
            this.buttonAddTask.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.Location = new System.Drawing.Point(197, 76);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 3;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // FormAddTask
            // 
            this.AcceptButton = this.buttonAddTask;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonBack;
            this.ClientSize = new System.Drawing.Size(284, 111);
            this.ControlBox = false;
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonAddTask);
            this.Controls.Add(this.textBoxTask);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormAddTask";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Công việc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddTask;
        private System.Windows.Forms.Button buttonBack;
        public System.Windows.Forms.TextBox textBoxTask;
    }
}