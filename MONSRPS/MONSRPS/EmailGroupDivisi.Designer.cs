namespace MONSRPS
{
    partial class EmailGroupDivisi
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_GroupDivisiProduk = new System.Windows.Forms.TextBox();
            this.txt_DirGroupDivisi = new System.Windows.Forms.TextBox();
            this.txt_EmailLAM = new System.Windows.Forms.TextBox();
            this.txtEmailLM = new System.Windows.Forms.TextBox();
            this.txt_SearchGroupEmail = new System.Windows.Forms.TextBox();
            this.txt_DirEmail = new System.Windows.Forms.TextBox();
            this.UploadGroup_btn = new System.Windows.Forms.Button();
            this.DownloadGroup_btn = new System.Windows.Forms.Button();
            this.UploadMail_btn = new System.Windows.Forms.Button();
            this.DownloadMail_btn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Group Divisi Produk";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 511);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dir :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 532);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Kode Divisi MD 000 - 999";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(542, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Email LAM";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(542, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Email LM";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(546, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Group Email";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(545, 511);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Dir :";
            // 
            // txt_GroupDivisiProduk
            // 
            this.txt_GroupDivisiProduk.Location = new System.Drawing.Point(267, 32);
            this.txt_GroupDivisiProduk.Name = "txt_GroupDivisiProduk";
            this.txt_GroupDivisiProduk.Size = new System.Drawing.Size(245, 20);
            this.txt_GroupDivisiProduk.TabIndex = 7;
            this.txt_GroupDivisiProduk.TextChanged += new System.EventHandler(this.txt_GroupDivisiProduk_TextChanged);
            // 
            // txt_DirGroupDivisi
            // 
            this.txt_DirGroupDivisi.Location = new System.Drawing.Point(45, 508);
            this.txt_DirGroupDivisi.Name = "txt_DirGroupDivisi";
            this.txt_DirGroupDivisi.ReadOnly = true;
            this.txt_DirGroupDivisi.Size = new System.Drawing.Size(305, 20);
            this.txt_DirGroupDivisi.TabIndex = 8;
            // 
            // txt_EmailLAM
            // 
            this.txt_EmailLAM.Location = new System.Drawing.Point(688, 56);
            this.txt_EmailLAM.Name = "txt_EmailLAM";
            this.txt_EmailLAM.Size = new System.Drawing.Size(353, 20);
            this.txt_EmailLAM.TabIndex = 9;
            this.txt_EmailLAM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_EmailLAM_KeyPress);
            // 
            // txtEmailLM
            // 
            this.txtEmailLM.Location = new System.Drawing.Point(688, 88);
            this.txtEmailLM.Name = "txtEmailLM";
            this.txtEmailLM.Size = new System.Drawing.Size(353, 20);
            this.txtEmailLM.TabIndex = 10;
            this.txtEmailLM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmailLM_KeyPress);
            // 
            // txt_SearchGroupEmail
            // 
            this.txt_SearchGroupEmail.BackColor = System.Drawing.SystemColors.Window;
            this.txt_SearchGroupEmail.Location = new System.Drawing.Point(688, 141);
            this.txt_SearchGroupEmail.Name = "txt_SearchGroupEmail";
            this.txt_SearchGroupEmail.Size = new System.Drawing.Size(353, 20);
            this.txt_SearchGroupEmail.TabIndex = 11;
            this.txt_SearchGroupEmail.TextChanged += new System.EventHandler(this.txt_SearchGroupEmail_TextChanged);
            // 
            // txt_DirEmail
            // 
            this.txt_DirEmail.Location = new System.Drawing.Point(577, 508);
            this.txt_DirEmail.Name = "txt_DirEmail";
            this.txt_DirEmail.ReadOnly = true;
            this.txt_DirEmail.Size = new System.Drawing.Size(302, 20);
            this.txt_DirEmail.TabIndex = 12;
            // 
            // UploadGroup_btn
            // 
            this.UploadGroup_btn.Location = new System.Drawing.Point(356, 506);
            this.UploadGroup_btn.Name = "UploadGroup_btn";
            this.UploadGroup_btn.Size = new System.Drawing.Size(75, 23);
            this.UploadGroup_btn.TabIndex = 13;
            this.UploadGroup_btn.Text = "Upload";
            this.UploadGroup_btn.UseVisualStyleBackColor = true;
            this.UploadGroup_btn.Click += new System.EventHandler(this.UploadGroup_btn_Click);
            // 
            // DownloadGroup_btn
            // 
            this.DownloadGroup_btn.Location = new System.Drawing.Point(437, 506);
            this.DownloadGroup_btn.Name = "DownloadGroup_btn";
            this.DownloadGroup_btn.Size = new System.Drawing.Size(75, 23);
            this.DownloadGroup_btn.TabIndex = 14;
            this.DownloadGroup_btn.Text = "Download";
            this.DownloadGroup_btn.UseVisualStyleBackColor = true;
            this.DownloadGroup_btn.Click += new System.EventHandler(this.DownloadGroup_btn_Click);
            // 
            // UploadMail_btn
            // 
            this.UploadMail_btn.Location = new System.Drawing.Point(885, 506);
            this.UploadMail_btn.Name = "UploadMail_btn";
            this.UploadMail_btn.Size = new System.Drawing.Size(75, 23);
            this.UploadMail_btn.TabIndex = 15;
            this.UploadMail_btn.Text = "Upload";
            this.UploadMail_btn.UseVisualStyleBackColor = true;
            this.UploadMail_btn.Click += new System.EventHandler(this.UploadMail_btn_Click);
            // 
            // DownloadMail_btn
            // 
            this.DownloadMail_btn.Location = new System.Drawing.Point(966, 506);
            this.DownloadMail_btn.Name = "DownloadMail_btn";
            this.DownloadMail_btn.Size = new System.Drawing.Size(75, 23);
            this.DownloadMail_btn.TabIndex = 16;
            this.DownloadMail_btn.Text = "Download";
            this.DownloadMail_btn.UseVisualStyleBackColor = true;
            this.DownloadMail_btn.Click += new System.EventHandler(this.DownloadMail_btn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(631, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "[A01]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(631, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "[B01]";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(12, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(500, 430);
            this.dataGridView1.TabIndex = 19;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.HeaderText = "DIV";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 153;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "DEP";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 152;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "KODE DIVISI MD";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 152;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5});
            this.dataGridView2.Location = new System.Drawing.Point(544, 182);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(497, 303);
            this.dataGridView2.TabIndex = 20;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4.FillWeight = 40F;
            this.Column4.HeaderText = "KODE DIVISI MD";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 130;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column5.HeaderText = "EMAIL";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 324;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label10.Location = new System.Drawing.Point(539, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(505, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "_________________________________________________________________________________" +
    "__";
            // 
            // EmailGroupDivisi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 553);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DownloadMail_btn);
            this.Controls.Add(this.UploadMail_btn);
            this.Controls.Add(this.DownloadGroup_btn);
            this.Controls.Add(this.UploadGroup_btn);
            this.Controls.Add(this.txt_DirEmail);
            this.Controls.Add(this.txt_SearchGroupEmail);
            this.Controls.Add(this.txtEmailLM);
            this.Controls.Add(this.txt_EmailLAM);
            this.Controls.Add(this.txt_DirGroupDivisi);
            this.Controls.Add(this.txt_GroupDivisiProduk);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EmailGroupDivisi";
            this.Text = "EmailGroupDivisi";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_GroupDivisiProduk;
        private System.Windows.Forms.TextBox txt_DirGroupDivisi;
        private System.Windows.Forms.TextBox txt_EmailLAM;
        private System.Windows.Forms.TextBox txtEmailLM;
        private System.Windows.Forms.TextBox txt_SearchGroupEmail;
        private System.Windows.Forms.TextBox txt_DirEmail;
        private System.Windows.Forms.Button UploadGroup_btn;
        private System.Windows.Forms.Button DownloadGroup_btn;
        private System.Windows.Forms.Button UploadMail_btn;
        private System.Windows.Forms.Button DownloadMail_btn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}