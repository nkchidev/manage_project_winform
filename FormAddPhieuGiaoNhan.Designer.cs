namespace ProjectStorage
{
    partial class FormAddPhieuGiaoViec
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonAddNewProject = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPhieuGiao = new System.Windows.Forms.TextBox();
            this.textBoxTongKhoan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDaThanhToan = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxTeam = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxProjectCode = new System.Windows.Forms.TextBox();
            this.checkBoxCachtinhchiphi = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã dự án";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên quyết định";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(115, 73);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(215, 20);
            this.textBoxDescription.TabIndex = 3;
            // 
            // buttonAddNewProject
            // 
            this.buttonAddNewProject.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAddNewProject.Location = new System.Drawing.Point(173, 247);
            this.buttonAddNewProject.Name = "buttonAddNewProject";
            this.buttonAddNewProject.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNewProject.TabIndex = 8;
            this.buttonAddNewProject.Text = "Thêm";
            this.buttonAddNewProject.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(254, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Quay lại";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Số quyết định";
            // 
            // textBoxPhieuGiao
            // 
            this.textBoxPhieuGiao.Location = new System.Drawing.Point(114, 41);
            this.textBoxPhieuGiao.Name = "textBoxPhieuGiao";
            this.textBoxPhieuGiao.Size = new System.Drawing.Size(215, 20);
            this.textBoxPhieuGiao.TabIndex = 2;
            // 
            // textBoxTongKhoan
            // 
            this.textBoxTongKhoan.Location = new System.Drawing.Point(114, 168);
            this.textBoxTongKhoan.Name = "textBoxTongKhoan";
            this.textBoxTongKhoan.ReadOnly = true;
            this.textBoxTongKhoan.Size = new System.Drawing.Size(215, 20);
            this.textBoxTongKhoan.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Tổng khoán";
            // 
            // textBoxDaThanhToan
            // 
            this.textBoxDaThanhToan.Location = new System.Drawing.Point(114, 200);
            this.textBoxDaThanhToan.Name = "textBoxDaThanhToan";
            this.textBoxDaThanhToan.ReadOnly = true;
            this.textBoxDaThanhToan.Size = new System.Drawing.Size(215, 20);
            this.textBoxDaThanhToan.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Đã thanh toán";
            // 
            // comboBoxTeam
            // 
            this.comboBoxTeam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxTeam.FormattingEnabled = true;
            this.comboBoxTeam.Location = new System.Drawing.Point(114, 105);
            this.comboBoxTeam.Name = "comboBoxTeam";
            this.comboBoxTeam.Size = new System.Drawing.Size(215, 21);
            this.comboBoxTeam.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Đội thi công";
            // 
            // textBoxProjectCode
            // 
            this.textBoxProjectCode.Location = new System.Drawing.Point(114, 9);
            this.textBoxProjectCode.Multiline = true;
            this.textBoxProjectCode.Name = "textBoxProjectCode";
            this.textBoxProjectCode.ReadOnly = true;
            this.textBoxProjectCode.Size = new System.Drawing.Size(215, 20);
            this.textBoxProjectCode.TabIndex = 1;
            // 
            // checkBoxCachtinhchiphi
            // 
            this.checkBoxCachtinhchiphi.AutoSize = true;
            this.checkBoxCachtinhchiphi.Location = new System.Drawing.Point(15, 141);
            this.checkBoxCachtinhchiphi.Name = "checkBoxCachtinhchiphi";
            this.checkBoxCachtinhchiphi.Size = new System.Drawing.Size(222, 17);
            this.checkBoxCachtinhchiphi.TabIndex = 5;
            this.checkBoxCachtinhchiphi.Text = "Cách tính chi phí theo công && định lượng";
            this.checkBoxCachtinhchiphi.UseVisualStyleBackColor = true;
            // 
            // FormAddPhieuGiaoViec
            // 
            this.AcceptButton = this.buttonAddNewProject;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(340, 287);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxCachtinhchiphi);
            this.Controls.Add(this.textBoxProjectCode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxTeam);
            this.Controls.Add(this.textBoxDaThanhToan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxTongKhoan);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxPhieuGiao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonAddNewProject);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormAddPhieuGiaoViec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quyết định giao nhiệm vụ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxPhieuGiao;
        public System.Windows.Forms.TextBox textBoxDescription;
        public System.Windows.Forms.TextBox textBoxTongKhoan;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxDaThanhToan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox comboBoxTeam;
        public System.Windows.Forms.Button buttonAddNewProject;
        public System.Windows.Forms.TextBox textBoxProjectCode;
        public System.Windows.Forms.CheckBox checkBoxCachtinhchiphi;
    }
}