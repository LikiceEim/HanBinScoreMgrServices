namespace iCMS.Setup.WebConfig
{
    partial class WebConfigForm
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
            this.serverlbl = new System.Windows.Forms.Label();
            this.secretlbl = new System.Windows.Forms.Label();
            this.keylbl = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtSecret = new System.Windows.Forms.TextBox();
            this.lblSecret = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.serverlbl);
            this.groupBox1.Controls.Add(this.secretlbl);
            this.groupBox1.Controls.Add(this.keylbl);
            this.groupBox1.Controls.Add(this.txtKey);
            this.groupBox1.Controls.Add(this.lblKey);
            this.groupBox1.Controls.Add(this.txtSecret);
            this.groupBox1.Controls.Add(this.lblSecret);
            this.groupBox1.Controls.Add(this.lblError);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIPAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 124);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // serverlbl
            // 
            this.serverlbl.AutoSize = true;
            this.serverlbl.ForeColor = System.Drawing.Color.Red;
            this.serverlbl.Location = new System.Drawing.Point(295, 90);
            this.serverlbl.Name = "serverlbl";
            this.serverlbl.Size = new System.Drawing.Size(89, 12);
            this.serverlbl.TabIndex = 11;
            this.serverlbl.Text = "请输入服务地址";
            // 
            // secretlbl
            // 
            this.secretlbl.AutoSize = true;
            this.secretlbl.ForeColor = System.Drawing.Color.Red;
            this.secretlbl.Location = new System.Drawing.Point(295, 57);
            this.secretlbl.Name = "secretlbl";
            this.secretlbl.Size = new System.Drawing.Size(65, 12);
            this.secretlbl.TabIndex = 10;
            this.secretlbl.Text = "请输入密钥";
            // 
            // keylbl
            // 
            this.keylbl.AutoSize = true;
            this.keylbl.ForeColor = System.Drawing.Color.Red;
            this.keylbl.Location = new System.Drawing.Point(295, 24);
            this.keylbl.Name = "keylbl";
            this.keylbl.Size = new System.Drawing.Size(65, 12);
            this.keylbl.TabIndex = 9;
            this.keylbl.Text = "请输入钥匙";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(95, 20);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(194, 21);
            this.txtKey.TabIndex = 0;
            this.txtKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyUp);
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblKey.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblKey.Location = new System.Drawing.Point(55, 24);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(41, 12);
            this.lblKey.TabIndex = 8;
            this.lblKey.Text = "钥匙：";
            // 
            // txtSecret
            // 
            this.txtSecret.Location = new System.Drawing.Point(95, 53);
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(194, 21);
            this.txtSecret.TabIndex = 7;
            this.txtSecret.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSecret_KeyUp);
            // 
            // lblSecret
            // 
            this.lblSecret.AutoSize = true;
            this.lblSecret.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSecret.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSecret.Location = new System.Drawing.Point(54, 57);
            this.lblSecret.Name = "lblSecret";
            this.lblSecret.Size = new System.Drawing.Size(41, 12);
            this.lblSecret.TabIndex = 6;
            this.lblSecret.Text = "密钥：";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(90, 68);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 12);
            this.lblError.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 4;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(95, 86);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(194, 21);
            this.txtIPAddress.TabIndex = 1;
            this.txtIPAddress.Text = "http://192.168.20.8:2892/";
            this.txtIPAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIPAddress_KeyUp);
            this.txtIPAddress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtIPAddress_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(24, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = " 服务地址：";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(215, 18);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "保    存";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(93, 18);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "重    置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.btnConfirm);
            this.groupBox2.Location = new System.Drawing.Point(5, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // WebConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 183);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Web.config 配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtSecret;
        private System.Windows.Forms.Label lblSecret;
        private System.Windows.Forms.Label serverlbl;
        private System.Windows.Forms.Label secretlbl;
        private System.Windows.Forms.Label keylbl;
    }
}

