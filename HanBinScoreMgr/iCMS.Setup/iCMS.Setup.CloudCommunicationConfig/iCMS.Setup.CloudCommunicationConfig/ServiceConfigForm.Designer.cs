namespace iCMS.Setup.CloudCommunicationConfig
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTestResult = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.codeTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.tbSecret = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cloudlbl = new System.Windows.Forms.Label();
            this.ploxylbl = new System.Windows.Forms.Label();
            this.serveraddresslbl = new System.Windows.Forms.Label();
            this.secretlbl = new System.Windows.Forms.Label();
            this.keylbl = new System.Windows.Forms.Label();
            this.codelbl = new System.Windows.Forms.Label();
            this.tbCloudAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_CloudProxyAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbServerUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTestResult);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(7, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 56);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtTestResult
            // 
            this.txtTestResult.AutoSize = true;
            this.txtTestResult.Location = new System.Drawing.Point(70, 16);
            this.txtTestResult.Name = "txtTestResult";
            this.txtTestResult.Size = new System.Drawing.Size(0, 12);
            this.txtTestResult.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(277, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保    存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "佳讯Code：";
            // 
            // codeTxt
            // 
            this.codeTxt.Location = new System.Drawing.Point(110, 18);
            this.codeTxt.Name = "codeTxt";
            this.codeTxt.Size = new System.Drawing.Size(244, 21);
            this.codeTxt.TabIndex = 15;
            this.codeTxt.Text = "1002200000000000";
            this.codeTxt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.codeTxt_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "钥匙：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(64, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "密钥：";
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(110, 47);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(244, 21);
            this.tbKey.TabIndex = 21;
            this.tbKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbKey_KeyUp);
            // 
            // tbSecret
            // 
            this.tbSecret.Location = new System.Drawing.Point(111, 77);
            this.tbSecret.Name = "tbSecret";
            this.tbSecret.Size = new System.Drawing.Size(243, 21);
            this.tbSecret.TabIndex = 22;
            this.tbSecret.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSecret_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cloudlbl);
            this.groupBox1.Controls.Add(this.ploxylbl);
            this.groupBox1.Controls.Add(this.serveraddresslbl);
            this.groupBox1.Controls.Add(this.secretlbl);
            this.groupBox1.Controls.Add(this.keylbl);
            this.groupBox1.Controls.Add(this.codelbl);
            this.groupBox1.Controls.Add(this.tbCloudAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_CloudProxyAddress);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbServerUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSecret);
            this.groupBox1.Controls.Add(this.tbKey);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.codeTxt);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(5, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 217);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cloudlbl
            // 
            this.cloudlbl.AutoSize = true;
            this.cloudlbl.ForeColor = System.Drawing.Color.Red;
            this.cloudlbl.Location = new System.Drawing.Point(360, 169);
            this.cloudlbl.Name = "cloudlbl";
            this.cloudlbl.Size = new System.Drawing.Size(101, 12);
            this.cloudlbl.TabIndex = 35;
            this.cloudlbl.Text = "请输入云平台地址";
            // 
            // ploxylbl
            // 
            this.ploxylbl.AutoSize = true;
            this.ploxylbl.ForeColor = System.Drawing.Color.Red;
            this.ploxylbl.Location = new System.Drawing.Point(360, 140);
            this.ploxylbl.Name = "ploxylbl";
            this.ploxylbl.Size = new System.Drawing.Size(101, 12);
            this.ploxylbl.TabIndex = 34;
            this.ploxylbl.Text = "请输入云代理地址";
            // 
            // serveraddresslbl
            // 
            this.serveraddresslbl.AutoSize = true;
            this.serveraddresslbl.ForeColor = System.Drawing.Color.Red;
            this.serveraddresslbl.Location = new System.Drawing.Point(360, 110);
            this.serveraddresslbl.Name = "serveraddresslbl";
            this.serveraddresslbl.Size = new System.Drawing.Size(101, 12);
            this.serveraddresslbl.TabIndex = 33;
            this.serveraddresslbl.Text = "请输入Server地址";
            // 
            // secretlbl
            // 
            this.secretlbl.AutoSize = true;
            this.secretlbl.ForeColor = System.Drawing.Color.Red;
            this.secretlbl.Location = new System.Drawing.Point(360, 80);
            this.secretlbl.Name = "secretlbl";
            this.secretlbl.Size = new System.Drawing.Size(65, 12);
            this.secretlbl.TabIndex = 32;
            this.secretlbl.Text = "请输入密钥";
            // 
            // keylbl
            // 
            this.keylbl.AutoSize = true;
            this.keylbl.ForeColor = System.Drawing.Color.Red;
            this.keylbl.Location = new System.Drawing.Point(360, 51);
            this.keylbl.Name = "keylbl";
            this.keylbl.Size = new System.Drawing.Size(65, 12);
            this.keylbl.TabIndex = 31;
            this.keylbl.Text = "请输入钥匙";
            // 
            // codelbl
            // 
            this.codelbl.AutoSize = true;
            this.codelbl.ForeColor = System.Drawing.Color.Red;
            this.codelbl.Location = new System.Drawing.Point(360, 25);
            this.codelbl.Name = "codelbl";
            this.codelbl.Size = new System.Drawing.Size(65, 12);
            this.codelbl.TabIndex = 30;
            this.codelbl.Text = "请输入Code";
            // 
            // tbCloudAddress
            // 
            this.tbCloudAddress.Location = new System.Drawing.Point(112, 166);
            this.tbCloudAddress.Name = "tbCloudAddress";
            this.tbCloudAddress.Size = new System.Drawing.Size(242, 21);
            this.tbCloudAddress.TabIndex = 29;
            this.tbCloudAddress.Text = "http://121.69.3.130:8160/";
            this.tbCloudAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCloudAddress_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "云平台地址：";
            // 
            // txt_CloudProxyAddress
            // 
            this.txt_CloudProxyAddress.Location = new System.Drawing.Point(112, 137);
            this.txt_CloudProxyAddress.Name = "txt_CloudProxyAddress";
            this.txt_CloudProxyAddress.Size = new System.Drawing.Size(242, 21);
            this.txt_CloudProxyAddress.TabIndex = 26;
            this.txt_CloudProxyAddress.Text = "http://192.168.20.8:2893/";
            this.txt_CloudProxyAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_CloudProxyAddress_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "云代理地址：";
            // 
            // tbServerUrl
            // 
            this.tbServerUrl.Location = new System.Drawing.Point(110, 107);
            this.tbServerUrl.Name = "tbServerUrl";
            this.tbServerUrl.Size = new System.Drawing.Size(244, 21);
            this.tbServerUrl.TabIndex = 24;
            this.tbServerUrl.Text = "http://192.168.20.8:2892/";
            this.tbServerUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbServerUrl_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "Server地址：";
            // 
            // ServiceConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 308);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iCMS云通讯配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceConfigForm_FormClosing_1);
            this.Load += new System.EventHandler(this.ServiceConfigForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label txtTestResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox codeTxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.TextBox tbSecret;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbServerUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_CloudProxyAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbCloudAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label cloudlbl;
        private System.Windows.Forms.Label ploxylbl;
        private System.Windows.Forms.Label serveraddresslbl;
        private System.Windows.Forms.Label secretlbl;
        private System.Windows.Forms.Label keylbl;
        private System.Windows.Forms.Label codelbl;
    }
}

