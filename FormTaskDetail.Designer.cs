namespace ProjectStorage
{
    partial class FormTaskDetail
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
            this.buttonTaskDetail = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTaskDetail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDonViTinh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTaskKK = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTaskKL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTaskPriceUnit = new System.Windows.Forms.TextBox();
            this.labelThanhTien = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxDinhmuc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtThuTu = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDinhBien = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGhichu = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonTaskDetail
            // 
            this.buttonTaskDetail.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonTaskDetail.Location = new System.Drawing.Point(182, 415);
            this.buttonTaskDetail.Name = "buttonTaskDetail";
            this.buttonTaskDetail.Size = new System.Drawing.Size(75, 23);
            this.buttonTaskDetail.TabIndex = 9;
            this.buttonTaskDetail.Text = "OK";
            this.buttonTaskDetail.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.Location = new System.Drawing.Point(263, 415);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 10;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mô tả";
            // 
            // textBoxTaskDetail
            // 
            this.textBoxTaskDetail.Location = new System.Drawing.Point(13, 68);
            this.textBoxTaskDetail.Name = "textBoxTaskDetail";
            this.textBoxTaskDetail.Size = new System.Drawing.Size(322, 20);
            this.textBoxTaskDetail.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đơn vị tính";
            // 
            // textBoxDonViTinh
            // 
            this.textBoxDonViTinh.Location = new System.Drawing.Point(13, 107);
            this.textBoxDonViTinh.Name = "textBoxDonViTinh";
            this.textBoxDonViTinh.Size = new System.Drawing.Size(172, 20);
            this.textBoxDonViTinh.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mức khó khăn";
            // 
            // textBoxTaskKK
            // 
            this.textBoxTaskKK.Location = new System.Drawing.Point(12, 146);
            this.textBoxTaskKK.Name = "textBoxTaskKK";
            this.textBoxTaskKK.Size = new System.Drawing.Size(173, 20);
            this.textBoxTaskKK.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Khối lượng";
            // 
            // textBoxTaskKL
            // 
            this.textBoxTaskKL.Location = new System.Drawing.Point(12, 185);
            this.textBoxTaskKL.Name = "textBoxTaskKL";
            this.textBoxTaskKL.Size = new System.Drawing.Size(173, 20);
            this.textBoxTaskKL.TabIndex = 4;
            this.textBoxTaskKL.TextChanged += new System.EventHandler(this.calcThanhTien);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Đơn giá (vnđ)";
            // 
            // textBoxTaskPriceUnit
            // 
            this.textBoxTaskPriceUnit.Location = new System.Drawing.Point(12, 224);
            this.textBoxTaskPriceUnit.Name = "textBoxTaskPriceUnit";
            this.textBoxTaskPriceUnit.Size = new System.Drawing.Size(173, 20);
            this.textBoxTaskPriceUnit.TabIndex = 5;
            this.textBoxTaskPriceUnit.TextChanged += new System.EventHandler(this.calcThanhTien);
            // 
            // labelThanhTien
            // 
            this.labelThanhTien.AutoSize = true;
            this.labelThanhTien.Location = new System.Drawing.Point(6, 14);
            this.labelThanhTien.Name = "labelThanhTien";
            this.labelThanhTien.Size = new System.Drawing.Size(91, 13);
            this.labelThanhTien.TabIndex = 12;
            this.labelThanhTien.Text = "Thành tiền: 0.000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelThanhTien);
            this.groupBox1.Location = new System.Drawing.Point(12, 369);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 34);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // textBoxDinhmuc
            // 
            this.textBoxDinhmuc.Location = new System.Drawing.Point(10, 263);
            this.textBoxDinhmuc.Name = "textBoxDinhmuc";
            this.textBoxDinhmuc.Size = new System.Drawing.Size(175, 20);
            this.textBoxDinhmuc.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Định mức";
            // 
            // txtThuTu
            // 
            this.txtThuTu.Location = new System.Drawing.Point(14, 25);
            this.txtThuTu.Name = "txtThuTu";
            this.txtThuTu.Size = new System.Drawing.Size(175, 20);
            this.txtThuTu.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Thứ tự";
            // 
            // txtDinhBien
            // 
            this.txtDinhBien.Location = new System.Drawing.Point(12, 306);
            this.txtDinhBien.Name = "txtDinhBien";
            this.txtDinhBien.Size = new System.Drawing.Size(175, 20);
            this.txtDinhBien.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Định biên";
            // 
            // txtGhichu
            // 
            this.txtGhichu.Location = new System.Drawing.Point(15, 345);
            this.txtGhichu.Name = "txtGhichu";
            this.txtGhichu.Size = new System.Drawing.Size(175, 20);
            this.txtGhichu.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 329);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Ghi chú";
            // 
            // FormTaskDetail
            // 
            this.AcceptButton = this.buttonTaskDetail;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonBack;
            this.ClientSize = new System.Drawing.Size(347, 461);
            this.ControlBox = false;
            this.Controls.Add(this.txtGhichu);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDinhBien);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtThuTu);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxDinhmuc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxTaskPriceUnit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxTaskKL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxTaskKK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxDonViTinh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTaskDetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonTaskDetail);
            this.Name = "FormTaskDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết công việc";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonTaskDetail;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox textBoxTaskDetail;
        public System.Windows.Forms.TextBox textBoxDonViTinh;
        public System.Windows.Forms.TextBox textBoxTaskKK;
        public System.Windows.Forms.TextBox textBoxTaskKL;
        public System.Windows.Forms.TextBox textBoxTaskPriceUnit;
        public System.Windows.Forms.Label labelThanhTien;
        public System.Windows.Forms.TextBox textBoxDinhmuc;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtThuTu;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtDinhBien;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtGhichu;
        private System.Windows.Forms.Label label9;
    }
}