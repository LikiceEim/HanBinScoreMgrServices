namespace iCMS.Setup.AgentConfig
{
    partial class AgentConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentConfigForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblErrorMaxTemperatureTimeMulti = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaxTemperatureTimeMulti = new System.Windows.Forms.TextBox();
            this.rbtn_Stop = new System.Windows.Forms.RadioButton();
            this.rbtn_Start = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lblErrorSecrect = new System.Windows.Forms.Label();
            this.lblErrorKey = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSecret = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblErrorWGID = new System.Windows.Forms.Label();
            this.lblErrorServiceAdd = new System.Windows.Forms.Label();
            this.lblErrorCLIPortName = new System.Windows.Forms.Label();
            this.lblErrorPortName = new System.Windows.Forms.Label();
            this.txtServiceAdd = new System.Windows.Forms.TextBox();
            this.txtCLIPortName = new System.Windows.Forms.TextBox();
            this.txtPortName = new System.Windows.Forms.TextBox();
            this.lblServiceAddress = new System.Windows.Forms.Label();
            this.lblCLIPortName = new System.Windows.Forms.Label();
            this.lblWGID = new System.Windows.Forms.Label();
            this.lblPortName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblErrorMaxTemperatureTimeMulti);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMaxTemperatureTimeMulti);
            this.groupBox1.Controls.Add(this.rbtn_Stop);
            this.groupBox1.Controls.Add(this.rbtn_Start);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblErrorSecrect);
            this.groupBox1.Controls.Add(this.lblErrorKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSecret);
            this.groupBox1.Controls.Add(this.txtKey);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.lblErrorWGID);
            this.groupBox1.Controls.Add(this.lblErrorServiceAdd);
            this.groupBox1.Controls.Add(this.lblErrorCLIPortName);
            this.groupBox1.Controls.Add(this.lblErrorPortName);
            this.groupBox1.Controls.Add(this.txtServiceAdd);
            this.groupBox1.Controls.Add(this.txtCLIPortName);
            this.groupBox1.Controls.Add(this.txtPortName);
            this.groupBox1.Controls.Add(this.lblServiceAddress);
            this.groupBox1.Controls.Add(this.lblCLIPortName);
            this.groupBox1.Controls.Add(this.lblWGID);
            this.groupBox1.Controls.Add(this.lblPortName);
            this.groupBox1.Location = new System.Drawing.Point(10, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(625, 239);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblErrorMaxTemperatureTimeMulti
            // 
            this.lblErrorMaxTemperatureTimeMulti.AutoSize = true;
            this.lblErrorMaxTemperatureTimeMulti.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMaxTemperatureTimeMulti.Location = new System.Drawing.Point(410, 208);
            this.lblErrorMaxTemperatureTimeMulti.Name = "lblErrorMaxTemperatureTimeMulti";
            this.lblErrorMaxTemperatureTimeMulti.Size = new System.Drawing.Size(185, 12);
            this.lblErrorMaxTemperatureTimeMulti.TabIndex = 27;
            this.lblErrorMaxTemperatureTimeMulti.Text = "请输入最大温度采集时间间隔倍数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "最大温度采集时间间隔倍数：";
            // 
            // txtMaxTemperatureTimeMulti
            // 
            this.txtMaxTemperatureTimeMulti.Location = new System.Drawing.Point(184, 205);
            this.txtMaxTemperatureTimeMulti.Name = "txtMaxTemperatureTimeMulti";
            this.txtMaxTemperatureTimeMulti.Size = new System.Drawing.Size(220, 21);
            this.txtMaxTemperatureTimeMulti.TabIndex = 25;
            this.txtMaxTemperatureTimeMulti.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMaxTemperatureTimeMulti_KeyUp);
            // 
            // rbtn_Stop
            // 
            this.rbtn_Stop.AutoSize = true;
            this.rbtn_Stop.Checked = true;
            this.rbtn_Stop.Location = new System.Drawing.Point(237, 182);
            this.rbtn_Stop.Name = "rbtn_Stop";
            this.rbtn_Stop.Size = new System.Drawing.Size(59, 16);
            this.rbtn_Stop.TabIndex = 24;
            this.rbtn_Stop.TabStop = true;
            this.rbtn_Stop.Text = "不启动";
            this.rbtn_Stop.UseVisualStyleBackColor = true;
            // 
            // rbtn_Start
            // 
            this.rbtn_Start.AutoSize = true;
            this.rbtn_Start.Location = new System.Drawing.Point(184, 182);
            this.rbtn_Start.Name = "rbtn_Start";
            this.rbtn_Start.Size = new System.Drawing.Size(47, 16);
            this.rbtn_Start.TabIndex = 23;
            this.rbtn_Start.Text = "启动";
            this.rbtn_Start.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "是否启动看门狗：";
            // 
            // lblErrorSecrect
            // 
            this.lblErrorSecrect.AutoSize = true;
            this.lblErrorSecrect.ForeColor = System.Drawing.Color.Red;
            this.lblErrorSecrect.Location = new System.Drawing.Point(410, 158);
            this.lblErrorSecrect.Name = "lblErrorSecrect";
            this.lblErrorSecrect.Size = new System.Drawing.Size(65, 12);
            this.lblErrorSecrect.TabIndex = 19;
            this.lblErrorSecrect.Text = "请输入密钥";
            // 
            // lblErrorKey
            // 
            this.lblErrorKey.AutoSize = true;
            this.lblErrorKey.ForeColor = System.Drawing.Color.Red;
            this.lblErrorKey.Location = new System.Drawing.Point(408, 133);
            this.lblErrorKey.Name = "lblErrorKey";
            this.lblErrorKey.Size = new System.Drawing.Size(65, 12);
            this.lblErrorKey.TabIndex = 18;
            this.lblErrorKey.Text = "请输入钥匙";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "密钥：";
            // 
            // txtSecret
            // 
            this.txtSecret.Location = new System.Drawing.Point(184, 155);
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(220, 21);
            this.txtSecret.TabIndex = 16;
            this.txtSecret.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSecret_KeyUp);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(184, 128);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(220, 21);
            this.txtKey.TabIndex = 15;
            this.txtKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbKey_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "钥匙：";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(184, 101);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(220, 21);
            this.txtIP.TabIndex = 1;
            this.txtIP.Text = "http://localhost:2895";
            this.txtIP.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIP_KeyUp);
            // 
            // lblErrorWGID
            // 
            this.lblErrorWGID.AutoSize = true;
            this.lblErrorWGID.ForeColor = System.Drawing.Color.Red;
            this.lblErrorWGID.Location = new System.Drawing.Point(408, 106);
            this.lblErrorWGID.Name = "lblErrorWGID";
            this.lblErrorWGID.Size = new System.Drawing.Size(77, 12);
            this.lblErrorWGID.TabIndex = 11;
            this.lblErrorWGID.Text = "请输入本机IP";
            // 
            // lblErrorServiceAdd
            // 
            this.lblErrorServiceAdd.AutoSize = true;
            this.lblErrorServiceAdd.ForeColor = System.Drawing.Color.Red;
            this.lblErrorServiceAdd.Location = new System.Drawing.Point(408, 78);
            this.lblErrorServiceAdd.Name = "lblErrorServiceAdd";
            this.lblErrorServiceAdd.Size = new System.Drawing.Size(89, 12);
            this.lblErrorServiceAdd.TabIndex = 10;
            this.lblErrorServiceAdd.Text = "请输入服务地址";
            // 
            // lblErrorCLIPortName
            // 
            this.lblErrorCLIPortName.AutoSize = true;
            this.lblErrorCLIPortName.ForeColor = System.Drawing.Color.Red;
            this.lblErrorCLIPortName.Location = new System.Drawing.Point(408, 51);
            this.lblErrorCLIPortName.Name = "lblErrorCLIPortName";
            this.lblErrorCLIPortName.Size = new System.Drawing.Size(95, 12);
            this.lblErrorCLIPortName.TabIndex = 9;
            this.lblErrorCLIPortName.Text = "请输入CLI端口号";
            // 
            // lblErrorPortName
            // 
            this.lblErrorPortName.AutoSize = true;
            this.lblErrorPortName.ForeColor = System.Drawing.Color.Red;
            this.lblErrorPortName.Location = new System.Drawing.Point(408, 25);
            this.lblErrorPortName.Name = "lblErrorPortName";
            this.lblErrorPortName.Size = new System.Drawing.Size(77, 12);
            this.lblErrorPortName.TabIndex = 8;
            this.lblErrorPortName.Text = "请输入端口号";
            // 
            // txtServiceAdd
            // 
            this.txtServiceAdd.Location = new System.Drawing.Point(184, 74);
            this.txtServiceAdd.Name = "txtServiceAdd";
            this.txtServiceAdd.Size = new System.Drawing.Size(220, 21);
            this.txtServiceAdd.TabIndex = 6;
            this.txtServiceAdd.Text = "http://localhost:2892";
            this.txtServiceAdd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtServiceAdd_KeyUp);
            // 
            // txtCLIPortName
            // 
            this.txtCLIPortName.Location = new System.Drawing.Point(184, 47);
            this.txtCLIPortName.Name = "txtCLIPortName";
            this.txtCLIPortName.Size = new System.Drawing.Size(220, 21);
            this.txtCLIPortName.TabIndex = 5;
            this.txtCLIPortName.Text = "COM1";
            this.txtCLIPortName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCLIPortName_KeyUp);
            // 
            // txtPortName
            // 
            this.txtPortName.Location = new System.Drawing.Point(184, 20);
            this.txtPortName.Name = "txtPortName";
            this.txtPortName.Size = new System.Drawing.Size(220, 21);
            this.txtPortName.TabIndex = 4;
            this.txtPortName.Text = "COM4";
            this.txtPortName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPortName_KeyUp);
            // 
            // lblServiceAddress
            // 
            this.lblServiceAddress.AutoSize = true;
            this.lblServiceAddress.Location = new System.Drawing.Point(112, 75);
            this.lblServiceAddress.Name = "lblServiceAddress";
            this.lblServiceAddress.Size = new System.Drawing.Size(65, 12);
            this.lblServiceAddress.TabIndex = 3;
            this.lblServiceAddress.Text = "服务地址：";
            // 
            // lblCLIPortName
            // 
            this.lblCLIPortName.AutoSize = true;
            this.lblCLIPortName.Location = new System.Drawing.Point(106, 47);
            this.lblCLIPortName.Name = "lblCLIPortName";
            this.lblCLIPortName.Size = new System.Drawing.Size(71, 12);
            this.lblCLIPortName.TabIndex = 2;
            this.lblCLIPortName.Text = "CLI端口名：";
            // 
            // lblWGID
            // 
            this.lblWGID.AutoSize = true;
            this.lblWGID.Location = new System.Drawing.Point(107, 104);
            this.lblWGID.Name = "lblWGID";
            this.lblWGID.Size = new System.Drawing.Size(71, 12);
            this.lblWGID.TabIndex = 1;
            this.lblWGID.Text = "Agent地址：";
            // 
            // lblPortName
            // 
            this.lblPortName.AutoSize = true;
            this.lblPortName.Location = new System.Drawing.Point(124, 20);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(53, 12);
            this.lblPortName.TabIndex = 0;
            this.lblPortName.Text = "端口名：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(12, 244);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(623, 48);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(408, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保    存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AgentConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 301);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgentConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agent配置【版本1.4.1】";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgentConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.AgentConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblServiceAddress;
        private System.Windows.Forms.Label lblCLIPortName;
        private System.Windows.Forms.Label lblWGID;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblErrorWGID;
        private System.Windows.Forms.Label lblErrorServiceAdd;
        private System.Windows.Forms.Label lblErrorCLIPortName;
        private System.Windows.Forms.Label lblErrorPortName;
        private System.Windows.Forms.TextBox txtServiceAdd;
        private System.Windows.Forms.TextBox txtCLIPortName;
        private System.Windows.Forms.TextBox txtPortName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSecret;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblErrorSecrect;
        private System.Windows.Forms.Label lblErrorKey;
        private System.Windows.Forms.RadioButton rbtn_Stop;
        private System.Windows.Forms.RadioButton rbtn_Start;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblErrorMaxTemperatureTimeMulti;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMaxTemperatureTimeMulti;
    }
}