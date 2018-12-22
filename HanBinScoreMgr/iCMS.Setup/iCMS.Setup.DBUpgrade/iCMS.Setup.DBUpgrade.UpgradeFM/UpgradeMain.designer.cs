namespace iCMS.Setup.DBUpgrade.UpgradeFM
{
    partial class UpgradeMain
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
            this.transferBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.messageLbl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.oldDbPassWordTxt = new System.Windows.Forms.TextBox();
            this.oldDBUserTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.oldDBNameTxt = new System.Windows.Forms.TextBox();
            this.oldDBServerNameTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newDBPassWordTxt = new System.Windows.Forms.TextBox();
            this.newDBUserTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.newDBNameTxt = new System.Windows.Forms.TextBox();
            this.newDBServerNameTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.vibsignalDataRbt = new System.Windows.Forms.RadioButton();
            this.deviceDataRbt = new System.Windows.Forms.RadioButton();
            this.wsAlarmRbt = new System.Windows.Forms.RadioButton();
            this.deviceAlarmRbt = new System.Windows.Forms.RadioButton();
            this.measureSiteSetRbt = new System.Windows.Forms.RadioButton();
            this.vibsignalSetRbt = new System.Windows.Forms.RadioButton();
            this.vibsignalRbt = new System.Windows.Forms.RadioButton();
            this.wsRbt = new System.Windows.Forms.RadioButton();
            this.wgRbt = new System.Windows.Forms.RadioButton();
            this.measureSiteRbt = new System.Windows.Forms.RadioButton();
            this.deviceRbt = new System.Windows.Forms.RadioButton();
            this.monitorTreeRbt = new System.Windows.Forms.RadioButton();
            this.monitorPRbt = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // transferBtn
            // 
            this.transferBtn.Location = new System.Drawing.Point(130, 271);
            this.transferBtn.Name = "transferBtn";
            this.transferBtn.Size = new System.Drawing.Size(75, 23);
            this.transferBtn.TabIndex = 0;
            this.transferBtn.Text = "数据迁移";
            this.transferBtn.UseVisualStyleBackColor = true;
            this.transferBtn.Click += new System.EventHandler(this.transferBtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 492);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(382, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // messageLbl
            // 
            this.messageLbl.AutoSize = true;
            this.messageLbl.Location = new System.Drawing.Point(12, 527);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(53, 12);
            this.messageLbl.TabIndex = 9;
            this.messageLbl.Text = "总数量：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.oldDbPassWordTxt);
            this.groupBox1.Controls.Add(this.oldDBUserTxt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.oldDBNameTxt);
            this.groupBox1.Controls.Add(this.oldDBServerNameTxt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 116);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "历史数据库配置";
            // 
            // oldDbPassWordTxt
            // 
            this.oldDbPassWordTxt.Location = new System.Drawing.Point(108, 87);
            this.oldDbPassWordTxt.Name = "oldDbPassWordTxt";
            this.oldDbPassWordTxt.Size = new System.Drawing.Size(235, 21);
            this.oldDbPassWordTxt.TabIndex = 8;
            this.oldDbPassWordTxt.Text = "123456789";
            // 
            // oldDBUserTxt
            // 
            this.oldDBUserTxt.Location = new System.Drawing.Point(108, 64);
            this.oldDBUserTxt.Name = "oldDBUserTxt";
            this.oldDBUserTxt.Size = new System.Drawing.Size(235, 21);
            this.oldDBUserTxt.TabIndex = 6;
            this.oldDBUserTxt.Text = "sa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "数据库密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据库用户名：";
            // 
            // oldDBNameTxt
            // 
            this.oldDBNameTxt.Location = new System.Drawing.Point(108, 41);
            this.oldDBNameTxt.Name = "oldDBNameTxt";
            this.oldDBNameTxt.Size = new System.Drawing.Size(235, 21);
            this.oldDBNameTxt.TabIndex = 3;
            this.oldDBNameTxt.Text = "dezhou";
            // 
            // oldDBServerNameTxt
            // 
            this.oldDBServerNameTxt.Location = new System.Drawing.Point(108, 18);
            this.oldDBServerNameTxt.Name = "oldDBServerNameTxt";
            this.oldDBServerNameTxt.Size = new System.Drawing.Size(235, 21);
            this.oldDBServerNameTxt.TabIndex = 2;
            this.oldDBServerNameTxt.Text = "192.168.20.14";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据库名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库服务名：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.newDBPassWordTxt);
            this.groupBox2.Controls.Add(this.newDBUserTxt);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.newDBNameTxt);
            this.groupBox2.Controls.Add(this.newDBServerNameTxt);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(22, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 116);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "现实数据库配置";
            // 
            // newDBPassWordTxt
            // 
            this.newDBPassWordTxt.Location = new System.Drawing.Point(108, 87);
            this.newDBPassWordTxt.Name = "newDBPassWordTxt";
            this.newDBPassWordTxt.Size = new System.Drawing.Size(235, 21);
            this.newDBPassWordTxt.TabIndex = 8;
            this.newDBPassWordTxt.Text = "123456789";
            // 
            // newDBUserTxt
            // 
            this.newDBUserTxt.Location = new System.Drawing.Point(108, 64);
            this.newDBUserTxt.Name = "newDBUserTxt";
            this.newDBUserTxt.Size = new System.Drawing.Size(235, 21);
            this.newDBUserTxt.TabIndex = 6;
            this.newDBUserTxt.Text = "sa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "数据库密码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "数据库用户名：";
            // 
            // newDBNameTxt
            // 
            this.newDBNameTxt.Location = new System.Drawing.Point(108, 41);
            this.newDBNameTxt.Name = "newDBNameTxt";
            this.newDBNameTxt.Size = new System.Drawing.Size(235, 21);
            this.newDBNameTxt.TabIndex = 3;
            this.newDBNameTxt.Text = "iCMSDB";
            // 
            // newDBServerNameTxt
            // 
            this.newDBServerNameTxt.Location = new System.Drawing.Point(108, 18);
            this.newDBServerNameTxt.Name = "newDBServerNameTxt";
            this.newDBServerNameTxt.Size = new System.Drawing.Size(235, 21);
            this.newDBServerNameTxt.TabIndex = 2;
            this.newDBServerNameTxt.Text = "192.168.20.14";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "数据库名称：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "数据库服务名：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Location = new System.Drawing.Point(20, 293);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 193);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据信息";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.vibsignalDataRbt);
            this.panel1.Controls.Add(this.deviceDataRbt);
            this.panel1.Controls.Add(this.wsAlarmRbt);
            this.panel1.Controls.Add(this.deviceAlarmRbt);
            this.panel1.Controls.Add(this.measureSiteSetRbt);
            this.panel1.Controls.Add(this.vibsignalSetRbt);
            this.panel1.Controls.Add(this.vibsignalRbt);
            this.panel1.Controls.Add(this.wsRbt);
            this.panel1.Controls.Add(this.wgRbt);
            this.panel1.Controls.Add(this.measureSiteRbt);
            this.panel1.Controls.Add(this.deviceRbt);
            this.panel1.Controls.Add(this.monitorTreeRbt);
            this.panel1.Controls.Add(this.monitorPRbt);
            this.panel1.Location = new System.Drawing.Point(6, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 170);
            this.panel1.TabIndex = 2;
            // 
            // vibsignalDataRbt
            // 
            this.vibsignalDataRbt.AutoSize = true;
            this.vibsignalDataRbt.Location = new System.Drawing.Point(22, 151);
            this.vibsignalDataRbt.Name = "vibsignalDataRbt";
            this.vibsignalDataRbt.Size = new System.Drawing.Size(119, 16);
            this.vibsignalDataRbt.TabIndex = 12;
            this.vibsignalDataRbt.TabStop = true;
            this.vibsignalDataRbt.Text = "振动信号采集数据";
            this.vibsignalDataRbt.UseVisualStyleBackColor = true;
            // 
            // deviceDataRbt
            // 
            this.deviceDataRbt.AutoSize = true;
            this.deviceDataRbt.Location = new System.Drawing.Point(210, 129);
            this.deviceDataRbt.Name = "deviceDataRbt";
            this.deviceDataRbt.Size = new System.Drawing.Size(95, 16);
            this.deviceDataRbt.TabIndex = 11;
            this.deviceDataRbt.TabStop = true;
            this.deviceDataRbt.Text = "设备采集数据";
            this.deviceDataRbt.UseVisualStyleBackColor = true;
            // 
            // wsAlarmRbt
            // 
            this.wsAlarmRbt.AutoSize = true;
            this.wsAlarmRbt.Location = new System.Drawing.Point(22, 129);
            this.wsAlarmRbt.Name = "wsAlarmRbt";
            this.wsAlarmRbt.Size = new System.Drawing.Size(143, 16);
            this.wsAlarmRbt.TabIndex = 10;
            this.wsAlarmRbt.TabStop = true;
            this.wsAlarmRbt.Text = "无线传感器报警器记录";
            this.wsAlarmRbt.UseVisualStyleBackColor = true;
            // 
            // deviceAlarmRbt
            // 
            this.deviceAlarmRbt.AutoSize = true;
            this.deviceAlarmRbt.Location = new System.Drawing.Point(210, 107);
            this.deviceAlarmRbt.Name = "deviceAlarmRbt";
            this.deviceAlarmRbt.Size = new System.Drawing.Size(95, 16);
            this.deviceAlarmRbt.TabIndex = 9;
            this.deviceAlarmRbt.TabStop = true;
            this.deviceAlarmRbt.Text = "设备报警记录";
            this.deviceAlarmRbt.UseVisualStyleBackColor = true;
            // 
            // measureSiteSetRbt
            // 
            this.measureSiteSetRbt.AutoSize = true;
            this.measureSiteSetRbt.Location = new System.Drawing.Point(22, 107);
            this.measureSiteSetRbt.Name = "measureSiteSetRbt";
            this.measureSiteSetRbt.Size = new System.Drawing.Size(95, 16);
            this.measureSiteSetRbt.TabIndex = 8;
            this.measureSiteSetRbt.TabStop = true;
            this.measureSiteSetRbt.Text = "测点报警预值";
            this.measureSiteSetRbt.UseVisualStyleBackColor = true;
            // 
            // vibsignalSetRbt
            // 
            this.vibsignalSetRbt.AutoSize = true;
            this.vibsignalSetRbt.Location = new System.Drawing.Point(210, 85);
            this.vibsignalSetRbt.Name = "vibsignalSetRbt";
            this.vibsignalSetRbt.Size = new System.Drawing.Size(119, 16);
            this.vibsignalSetRbt.TabIndex = 7;
            this.vibsignalSetRbt.TabStop = true;
            this.vibsignalSetRbt.Text = "振动信号报警预值";
            this.vibsignalSetRbt.UseVisualStyleBackColor = true;
            // 
            // vibsignalRbt
            // 
            this.vibsignalRbt.AutoSize = true;
            this.vibsignalRbt.Location = new System.Drawing.Point(22, 85);
            this.vibsignalRbt.Name = "vibsignalRbt";
            this.vibsignalRbt.Size = new System.Drawing.Size(71, 16);
            this.vibsignalRbt.TabIndex = 6;
            this.vibsignalRbt.TabStop = true;
            this.vibsignalRbt.Text = "振动信号";
            this.vibsignalRbt.UseVisualStyleBackColor = true;
            // 
            // wsRbt
            // 
            this.wsRbt.AutoSize = true;
            this.wsRbt.Location = new System.Drawing.Point(210, 63);
            this.wsRbt.Name = "wsRbt";
            this.wsRbt.Size = new System.Drawing.Size(83, 16);
            this.wsRbt.TabIndex = 5;
            this.wsRbt.TabStop = true;
            this.wsRbt.Text = "无线传感器";
            this.wsRbt.UseVisualStyleBackColor = true;
            // 
            // wgRbt
            // 
            this.wgRbt.AutoSize = true;
            this.wgRbt.Location = new System.Drawing.Point(22, 63);
            this.wgRbt.Name = "wgRbt";
            this.wgRbt.Size = new System.Drawing.Size(71, 16);
            this.wgRbt.TabIndex = 4;
            this.wgRbt.TabStop = true;
            this.wgRbt.Text = "无线网关";
            this.wgRbt.UseVisualStyleBackColor = true;
            // 
            // measureSiteRbt
            // 
            this.measureSiteRbt.AutoSize = true;
            this.measureSiteRbt.Location = new System.Drawing.Point(210, 41);
            this.measureSiteRbt.Name = "measureSiteRbt";
            this.measureSiteRbt.Size = new System.Drawing.Size(47, 16);
            this.measureSiteRbt.TabIndex = 3;
            this.measureSiteRbt.TabStop = true;
            this.measureSiteRbt.Text = "测点";
            this.measureSiteRbt.UseVisualStyleBackColor = true;
            // 
            // deviceRbt
            // 
            this.deviceRbt.AutoSize = true;
            this.deviceRbt.Location = new System.Drawing.Point(22, 41);
            this.deviceRbt.Name = "deviceRbt";
            this.deviceRbt.Size = new System.Drawing.Size(47, 16);
            this.deviceRbt.TabIndex = 2;
            this.deviceRbt.TabStop = true;
            this.deviceRbt.Text = "设备";
            this.deviceRbt.UseVisualStyleBackColor = true;
            // 
            // monitorTreeRbt
            // 
            this.monitorTreeRbt.AutoSize = true;
            this.monitorTreeRbt.Location = new System.Drawing.Point(210, 19);
            this.monitorTreeRbt.Name = "monitorTreeRbt";
            this.monitorTreeRbt.Size = new System.Drawing.Size(59, 16);
            this.monitorTreeRbt.TabIndex = 1;
            this.monitorTreeRbt.TabStop = true;
            this.monitorTreeRbt.Text = "监测树";
            this.monitorTreeRbt.UseVisualStyleBackColor = true;
            // 
            // monitorPRbt
            // 
            this.monitorPRbt.AutoSize = true;
            this.monitorPRbt.Location = new System.Drawing.Point(22, 19);
            this.monitorPRbt.Name = "monitorPRbt";
            this.monitorPRbt.Size = new System.Drawing.Size(83, 16);
            this.monitorPRbt.TabIndex = 0;
            this.monitorPRbt.TabStop = true;
            this.monitorPRbt.Text = "监测树属性";
            this.monitorPRbt.UseVisualStyleBackColor = true;
            // 
            // UpgradeMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 550);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.messageLbl);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.transferBtn);
            this.Name = "UpgradeMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库升级【1.2升级到1.4】";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button transferBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label messageLbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox oldDbPassWordTxt;
        private System.Windows.Forms.TextBox oldDBUserTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox oldDBNameTxt;
        private System.Windows.Forms.TextBox oldDBServerNameTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox newDBPassWordTxt;
        private System.Windows.Forms.TextBox newDBUserTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox newDBNameTxt;
        private System.Windows.Forms.TextBox newDBServerNameTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton vibsignalDataRbt;
        private System.Windows.Forms.RadioButton deviceDataRbt;
        private System.Windows.Forms.RadioButton wsAlarmRbt;
        private System.Windows.Forms.RadioButton deviceAlarmRbt;
        private System.Windows.Forms.RadioButton measureSiteSetRbt;
        private System.Windows.Forms.RadioButton vibsignalSetRbt;
        private System.Windows.Forms.RadioButton vibsignalRbt;
        private System.Windows.Forms.RadioButton wsRbt;
        private System.Windows.Forms.RadioButton wgRbt;
        private System.Windows.Forms.RadioButton measureSiteRbt;
        private System.Windows.Forms.RadioButton deviceRbt;
        private System.Windows.Forms.RadioButton monitorTreeRbt;
        private System.Windows.Forms.RadioButton monitorPRbt;
    }
}

