namespace iCMS.Setup.CloudPushManually
{
    partial class DBConfigFrm
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
            this.btnTestConn = new System.Windows.Forms.Button();
            this.lblErrorUserID = new System.Windows.Forms.Label();
            this.lblErrorPwd = new System.Windows.Forms.Label();
            this.lblErrorDbName = new System.Windows.Forms.Label();
            this.lblErrorHost = new System.Windows.Forms.Label();
            this.txtTestResult = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserPwd = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTestConn
            // 
            this.btnTestConn.Location = new System.Drawing.Point(60, 32);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(79, 23);
            this.btnTestConn.TabIndex = 0;
            this.btnTestConn.Text = "测 试 连 接";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // lblErrorUserID
            // 
            this.lblErrorUserID.AutoSize = true;
            this.lblErrorUserID.ForeColor = System.Drawing.Color.Red;
            this.lblErrorUserID.Location = new System.Drawing.Point(258, 74);
            this.lblErrorUserID.Name = "lblErrorUserID";
            this.lblErrorUserID.Size = new System.Drawing.Size(77, 12);
            this.lblErrorUserID.TabIndex = 11;
            this.lblErrorUserID.Text = "请输入用户名";
            // 
            // lblErrorPwd
            // 
            this.lblErrorPwd.AutoSize = true;
            this.lblErrorPwd.ForeColor = System.Drawing.Color.Red;
            this.lblErrorPwd.Location = new System.Drawing.Point(258, 110);
            this.lblErrorPwd.Name = "lblErrorPwd";
            this.lblErrorPwd.Size = new System.Drawing.Size(101, 12);
            this.lblErrorPwd.TabIndex = 10;
            this.lblErrorPwd.Text = "请输入数据库密码";
            // 
            // lblErrorDbName
            // 
            this.lblErrorDbName.AutoSize = true;
            this.lblErrorDbName.ForeColor = System.Drawing.Color.Red;
            this.lblErrorDbName.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblErrorDbName.Location = new System.Drawing.Point(258, 145);
            this.lblErrorDbName.Name = "lblErrorDbName";
            this.lblErrorDbName.Size = new System.Drawing.Size(89, 12);
            this.lblErrorDbName.TabIndex = 9;
            this.lblErrorDbName.Text = "请输入数据库名";
            // 
            // lblErrorHost
            // 
            this.lblErrorHost.AutoSize = true;
            this.lblErrorHost.ForeColor = System.Drawing.Color.Red;
            this.lblErrorHost.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblErrorHost.Location = new System.Drawing.Point(258, 34);
            this.lblErrorHost.Name = "lblErrorHost";
            this.lblErrorHost.Size = new System.Drawing.Size(77, 12);
            this.lblErrorHost.TabIndex = 8;
            this.lblErrorHost.Text = "请输入主机名";
            // 
            // txtTestResult
            // 
            this.txtTestResult.AutoSize = true;
            this.txtTestResult.Location = new System.Drawing.Point(70, 16);
            this.txtTestResult.Name = "txtTestResult";
            this.txtTestResult.Size = new System.Drawing.Size(0, 12);
            this.txtTestResult.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTestResult);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnTestConn);
            this.groupBox2.Location = new System.Drawing.Point(16, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 71);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(196, 33);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保    存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(110, 68);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(142, 21);
            this.txtUserID.TabIndex = 7;
            this.txtUserID.Text = "sa";
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(110, 137);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(142, 21);
            this.txtDbName.TabIndex = 6;
            this.txtDbName.Text = "iCMSDB";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(110, 31);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(142, 21);
            this.txtHost.TabIndex = 4;
            this.txtHost.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据库名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "主机名：";
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.Location = new System.Drawing.Point(110, 103);
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.PasswordChar = '*';
            this.txtUserPwd.Size = new System.Drawing.Size(142, 21);
            this.txtUserPwd.TabIndex = 5;
            this.txtUserPwd.Text = "123456789";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblErrorUserID);
            this.groupBox1.Controls.Add(this.lblErrorPwd);
            this.groupBox1.Controls.Add(this.lblErrorDbName);
            this.groupBox1.Controls.Add(this.lblErrorHost);
            this.groupBox1.Controls.Add(this.txtUserID);
            this.groupBox1.Controls.Add(this.txtDbName);
            this.groupBox1.Controls.Add(this.txtUserPwd);
            this.groupBox1.Controls.Add(this.txtHost);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 185);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // DBConfigFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 286);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DBConfigFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库配置";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.Label lblErrorUserID;
        private System.Windows.Forms.Label lblErrorPwd;
        private System.Windows.Forms.Label lblErrorDbName;
        private System.Windows.Forms.Label lblErrorHost;
        private System.Windows.Forms.Label txtTestResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserPwd;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}