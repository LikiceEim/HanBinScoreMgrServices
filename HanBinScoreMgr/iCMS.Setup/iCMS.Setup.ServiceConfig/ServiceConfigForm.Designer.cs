namespace iCMS.Setup.ServiceConfig
{
    partial class ServiceConfigForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblsecret = new System.Windows.Forms.Label();
            this.lblkey = new System.Windows.Forms.Label();
            this.tbSecret = new System.Windows.Forms.TextBox();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblErrorUserID = new System.Windows.Forms.Label();
            this.lblErrorPwd = new System.Windows.Forms.Label();
            this.lblErrorDbName = new System.Windows.Forms.Label();
            this.lblErrorHost = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.txtUserPwd = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbServer = new System.Windows.Forms.GroupBox();
            this.lblServicePortTip = new System.Windows.Forms.Label();
            this.lblServiceDiscriptionTip = new System.Windows.Forms.Label();
            this.lblServiceDisplayNameTip = new System.Windows.Forms.Label();
            this.lblServiceNameTip = new System.Windows.Forms.Label();
            this.txtServiceDispscription = new System.Windows.Forms.TextBox();
            this.txtServiceDisplay = new System.Windows.Forms.TextBox();
            this.txtSericePort = new System.Windows.Forms.TextBox();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.btnSaveDB = new System.Windows.Forms.Button();
            this.txtTestResult = new System.Windows.Forms.Label();
            this.gbDb = new System.Windows.Forms.GroupBox();
            this.gbServerSetup = new System.Windows.Forms.GroupBox();
            this.btnSaveService = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gbServer.SuspendLayout();
            this.gbDb.SuspendLayout();
            this.gbServerSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblsecret);
            this.groupBox1.Controls.Add(this.lblkey);
            this.groupBox1.Controls.Add(this.tbSecret);
            this.groupBox1.Controls.Add(this.tbKey);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
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
            this.groupBox1.Location = new System.Drawing.Point(5, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库配置";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblsecret
            // 
            this.lblsecret.AutoSize = true;
            this.lblsecret.ForeColor = System.Drawing.Color.Red;
            this.lblsecret.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblsecret.Location = new System.Drawing.Point(416, 155);
            this.lblsecret.Name = "lblsecret";
            this.lblsecret.Size = new System.Drawing.Size(65, 12);
            this.lblsecret.TabIndex = 24;
            this.lblsecret.Text = "请输入密钥";
            this.lblsecret.Click += new System.EventHandler(this.lblsecret_Click);
            // 
            // lblkey
            // 
            this.lblkey.AutoSize = true;
            this.lblkey.ForeColor = System.Drawing.Color.Red;
            this.lblkey.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblkey.Location = new System.Drawing.Point(416, 130);
            this.lblkey.Name = "lblkey";
            this.lblkey.Size = new System.Drawing.Size(65, 12);
            this.lblkey.TabIndex = 23;
            this.lblkey.Text = "请输入钥匙";
            this.lblkey.Click += new System.EventHandler(this.lblkey_Click);
            // 
            // tbSecret
            // 
            this.tbSecret.Location = new System.Drawing.Point(110, 150);
            this.tbSecret.Name = "tbSecret";
            this.tbSecret.Size = new System.Drawing.Size(300, 21);
            this.tbSecret.TabIndex = 22;
            this.tbSecret.TextChanged += new System.EventHandler(this.tbSecret_TextChanged);
            this.tbSecret.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSecret_KeyUp);
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(110, 127);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(300, 21);
            this.tbKey.TabIndex = 21;
            this.tbKey.TextChanged += new System.EventHandler(this.tbKey_TextChanged);
            this.tbKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbKey_KeyUp);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(64, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "密钥：";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(64, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "钥匙：";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // lblErrorUserID
            // 
            this.lblErrorUserID.AutoSize = true;
            this.lblErrorUserID.ForeColor = System.Drawing.Color.Red;
            this.lblErrorUserID.Location = new System.Drawing.Point(416, 58);
            this.lblErrorUserID.Name = "lblErrorUserID";
            this.lblErrorUserID.Size = new System.Drawing.Size(77, 12);
            this.lblErrorUserID.TabIndex = 11;
            this.lblErrorUserID.Text = "请输入用户名";
            this.lblErrorUserID.Click += new System.EventHandler(this.lblErrorUserID_Click);
            // 
            // lblErrorPwd
            // 
            this.lblErrorPwd.AutoSize = true;
            this.lblErrorPwd.ForeColor = System.Drawing.Color.Red;
            this.lblErrorPwd.Location = new System.Drawing.Point(416, 83);
            this.lblErrorPwd.Name = "lblErrorPwd";
            this.lblErrorPwd.Size = new System.Drawing.Size(101, 12);
            this.lblErrorPwd.TabIndex = 10;
            this.lblErrorPwd.Text = "请输入数据库密码";
            this.lblErrorPwd.Click += new System.EventHandler(this.lblErrorPwd_Click);
            // 
            // lblErrorDbName
            // 
            this.lblErrorDbName.AutoSize = true;
            this.lblErrorDbName.ForeColor = System.Drawing.Color.Red;
            this.lblErrorDbName.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblErrorDbName.Location = new System.Drawing.Point(416, 107);
            this.lblErrorDbName.Name = "lblErrorDbName";
            this.lblErrorDbName.Size = new System.Drawing.Size(89, 12);
            this.lblErrorDbName.TabIndex = 9;
            this.lblErrorDbName.Text = "请输入数据库名";
            this.lblErrorDbName.Click += new System.EventHandler(this.lblErrorDbName_Click);
            // 
            // lblErrorHost
            // 
            this.lblErrorHost.AutoSize = true;
            this.lblErrorHost.ForeColor = System.Drawing.Color.Red;
            this.lblErrorHost.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblErrorHost.Location = new System.Drawing.Point(416, 34);
            this.lblErrorHost.Name = "lblErrorHost";
            this.lblErrorHost.Size = new System.Drawing.Size(77, 12);
            this.lblErrorHost.TabIndex = 8;
            this.lblErrorHost.Text = "请输入主机名";
            this.lblErrorHost.Click += new System.EventHandler(this.lblErrorHost_Click);
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(110, 55);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(300, 21);
            this.txtUserID.TabIndex = 7;
            this.txtUserID.Text = "sa";
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            this.txtUserID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUserID_KeyUp);
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(110, 103);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(300, 21);
            this.txtDbName.TabIndex = 6;
            this.txtDbName.Text = "iCMSDB";
            this.txtDbName.TextChanged += new System.EventHandler(this.txtDbName_TextChanged);
            this.txtDbName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDbName_KeyUp);
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.Location = new System.Drawing.Point(110, 79);
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.PasswordChar = '*';
            this.txtUserPwd.Size = new System.Drawing.Size(300, 21);
            this.txtUserPwd.TabIndex = 5;
            this.txtUserPwd.Text = "123456789";
            this.txtUserPwd.TextChanged += new System.EventHandler(this.txtUserPwd_TextChanged);
            this.txtUserPwd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUserPwd_KeyUp);
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(110, 31);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(300, 21);
            this.txtHost.TabIndex = 4;
            this.txtHost.Text = "192.168.50.203";
            this.txtHost.TextChanged += new System.EventHandler(this.txtHost_TextChanged);
            this.txtHost.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHost_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户名：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据库名：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "主机名：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // gbServer
            // 
            this.gbServer.Controls.Add(this.lblServicePortTip);
            this.gbServer.Controls.Add(this.lblServiceDiscriptionTip);
            this.gbServer.Controls.Add(this.lblServiceDisplayNameTip);
            this.gbServer.Controls.Add(this.lblServiceNameTip);
            this.gbServer.Controls.Add(this.txtServiceDispscription);
            this.gbServer.Controls.Add(this.txtServiceDisplay);
            this.gbServer.Controls.Add(this.txtSericePort);
            this.gbServer.Controls.Add(this.txtServiceName);
            this.gbServer.Controls.Add(this.label6);
            this.gbServer.Controls.Add(this.label7);
            this.gbServer.Controls.Add(this.label8);
            this.gbServer.Controls.Add(this.label11);
            this.gbServer.Controls.Add(this.label5);
            this.gbServer.Location = new System.Drawing.Point(6, 240);
            this.gbServer.Name = "gbServer";
            this.gbServer.Size = new System.Drawing.Size(526, 113);
            this.gbServer.TabIndex = 4;
            this.gbServer.TabStop = false;
            this.gbServer.Text = "服务配置";
            this.gbServer.Visible = false;
            // 
            // lblServicePortTip
            // 
            this.lblServicePortTip.AutoSize = true;
            this.lblServicePortTip.ForeColor = System.Drawing.Color.Red;
            this.lblServicePortTip.Location = new System.Drawing.Point(418, 89);
            this.lblServicePortTip.Name = "lblServicePortTip";
            this.lblServicePortTip.Size = new System.Drawing.Size(89, 12);
            this.lblServicePortTip.TabIndex = 18;
            this.lblServicePortTip.Text = "请输入服务端口";
            this.lblServicePortTip.Visible = false;
            // 
            // lblServiceDiscriptionTip
            // 
            this.lblServiceDiscriptionTip.AutoSize = true;
            this.lblServiceDiscriptionTip.ForeColor = System.Drawing.Color.Red;
            this.lblServiceDiscriptionTip.Location = new System.Drawing.Point(418, 65);
            this.lblServiceDiscriptionTip.Name = "lblServiceDiscriptionTip";
            this.lblServiceDiscriptionTip.Size = new System.Drawing.Size(89, 12);
            this.lblServiceDiscriptionTip.TabIndex = 17;
            this.lblServiceDiscriptionTip.Text = "请输入服务说明";
            this.lblServiceDiscriptionTip.Visible = false;
            // 
            // lblServiceDisplayNameTip
            // 
            this.lblServiceDisplayNameTip.AutoSize = true;
            this.lblServiceDisplayNameTip.ForeColor = System.Drawing.Color.Red;
            this.lblServiceDisplayNameTip.Location = new System.Drawing.Point(418, 40);
            this.lblServiceDisplayNameTip.Name = "lblServiceDisplayNameTip";
            this.lblServiceDisplayNameTip.Size = new System.Drawing.Size(89, 12);
            this.lblServiceDisplayNameTip.TabIndex = 16;
            this.lblServiceDisplayNameTip.Text = "请输入显示名称";
            this.lblServiceDisplayNameTip.Visible = false;
            // 
            // lblServiceNameTip
            // 
            this.lblServiceNameTip.AutoSize = true;
            this.lblServiceNameTip.ForeColor = System.Drawing.Color.Red;
            this.lblServiceNameTip.Location = new System.Drawing.Point(418, 16);
            this.lblServiceNameTip.Name = "lblServiceNameTip";
            this.lblServiceNameTip.Size = new System.Drawing.Size(89, 12);
            this.lblServiceNameTip.TabIndex = 15;
            this.lblServiceNameTip.Text = "请输入服务名称";
            this.lblServiceNameTip.Visible = false;
            // 
            // txtServiceDispscription
            // 
            this.txtServiceDispscription.Location = new System.Drawing.Point(109, 60);
            this.txtServiceDispscription.Name = "txtServiceDispscription";
            this.txtServiceDispscription.Size = new System.Drawing.Size(300, 21);
            this.txtServiceDispscription.TabIndex = 14;
            this.txtServiceDispscription.Text = "西安因联信息科技有限公司iCMS.Server服务 ®V 1.4.1.5";
            // 
            // txtServiceDisplay
            // 
            this.txtServiceDisplay.Location = new System.Drawing.Point(109, 36);
            this.txtServiceDisplay.Name = "txtServiceDisplay";
            this.txtServiceDisplay.Size = new System.Drawing.Size(300, 21);
            this.txtServiceDisplay.TabIndex = 13;
            this.txtServiceDisplay.Text = "iCMS.Server";
            // 
            // txtSericePort
            // 
            this.txtSericePort.Location = new System.Drawing.Point(109, 84);
            this.txtSericePort.Name = "txtSericePort";
            this.txtSericePort.Size = new System.Drawing.Size(300, 21);
            this.txtSericePort.TabIndex = 12;
            this.txtSericePort.Text = "2892";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(109, 12);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(300, 21);
            this.txtServiceName.TabIndex = 11;
            this.txtServiceName.Text = "iCMS.ServerService";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "服务说明：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "显示名称：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "服务端口：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(42, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 7;
            this.label11.Text = "服务名称：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 2;
            // 
            // btnTestConn
            // 
            this.btnTestConn.Location = new System.Drawing.Point(110, 16);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(138, 23);
            this.btnTestConn.TabIndex = 0;
            this.btnTestConn.Text = "测试数据库连接";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // btnSaveDB
            // 
            this.btnSaveDB.Location = new System.Drawing.Point(254, 16);
            this.btnSaveDB.Name = "btnSaveDB";
            this.btnSaveDB.Size = new System.Drawing.Size(156, 23);
            this.btnSaveDB.TabIndex = 1;
            this.btnSaveDB.Text = "保存数据库";
            this.btnSaveDB.UseVisualStyleBackColor = true;
            this.btnSaveDB.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtTestResult
            // 
            this.txtTestResult.AutoSize = true;
            this.txtTestResult.Location = new System.Drawing.Point(70, 16);
            this.txtTestResult.Name = "txtTestResult";
            this.txtTestResult.Size = new System.Drawing.Size(0, 12);
            this.txtTestResult.TabIndex = 2;
            this.txtTestResult.Click += new System.EventHandler(this.txtTestResult_Click);
            // 
            // gbDb
            // 
            this.gbDb.Controls.Add(this.txtTestResult);
            this.gbDb.Controls.Add(this.btnSaveDB);
            this.gbDb.Controls.Add(this.btnTestConn);
            this.gbDb.Location = new System.Drawing.Point(5, 193);
            this.gbDb.Name = "gbDb";
            this.gbDb.Size = new System.Drawing.Size(526, 46);
            this.gbDb.TabIndex = 1;
            this.gbDb.TabStop = false;
            this.gbDb.Text = "数据库操作";
            this.gbDb.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // gbServerSetup
            // 
            this.gbServerSetup.Controls.Add(this.btnSaveService);
            this.gbServerSetup.Location = new System.Drawing.Point(6, 360);
            this.gbServerSetup.Name = "gbServerSetup";
            this.gbServerSetup.Size = new System.Drawing.Size(525, 46);
            this.gbServerSetup.TabIndex = 5;
            this.gbServerSetup.TabStop = false;
            this.gbServerSetup.Text = "服务操作";
            // 
            // btnSaveService
            // 
            this.btnSaveService.Location = new System.Drawing.Point(109, 17);
            this.btnSaveService.Name = "btnSaveService";
            this.btnSaveService.Size = new System.Drawing.Size(138, 23);
            this.btnSaveService.TabIndex = 4;
            this.btnSaveService.Text = "安装服务";
            this.btnSaveService.UseVisualStyleBackColor = true;
            this.btnSaveService.Click += new System.EventHandler(this.btnSaveService_Click);
            // 
            // ServiceConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 420);
            this.Controls.Add(this.gbServerSetup);
            this.Controls.Add(this.gbServer);
            this.Controls.Add(this.gbDb);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iCMS配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceConfigForm_FormClosing_1);
            this.Load += new System.EventHandler(this.ServiceConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbServer.ResumeLayout(false);
            this.gbServer.PerformLayout();
            this.gbDb.ResumeLayout(false);
            this.gbDb.PerformLayout();
            this.gbServerSetup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.TextBox txtUserPwd;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblErrorUserID;
        private System.Windows.Forms.Label lblErrorPwd;
        private System.Windows.Forms.Label lblErrorDbName;
        private System.Windows.Forms.Label lblErrorHost;
        private System.Windows.Forms.TextBox tbSecret;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblsecret;
        private System.Windows.Forms.Label lblkey;
        private System.Windows.Forms.GroupBox gbServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtServiceDispscription;
        private System.Windows.Forms.TextBox txtServiceDisplay;
        private System.Windows.Forms.TextBox txtSericePort;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label lblServicePortTip;
        private System.Windows.Forms.Label lblServiceDiscriptionTip;
        private System.Windows.Forms.Label lblServiceDisplayNameTip;
        private System.Windows.Forms.Label lblServiceNameTip;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.Button btnSaveDB;
        private System.Windows.Forms.Label txtTestResult;
        private System.Windows.Forms.GroupBox gbDb;
        private System.Windows.Forms.GroupBox gbServerSetup;
        private System.Windows.Forms.Button btnSaveService;
    }
}


