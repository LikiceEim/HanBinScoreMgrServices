namespace iCMS.Communication.PushManually
{
    partial class PushManually
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtBeginTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbVibration = new System.Windows.Forms.CheckBox();
            this.cbVoltage = new System.Windows.Forms.CheckBox();
            this.cbDeviceTempe = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPush = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtEndTime);
            this.groupBox1.Controls.Add(this.dtBeginTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dtEndTime
            // 
            this.dtEndTime.Location = new System.Drawing.Point(318, 28);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(156, 21);
            this.dtEndTime.TabIndex = 3;
            // 
            // dtBeginTime
            // 
            this.dtBeginTime.Location = new System.Drawing.Point(112, 28);
            this.dtBeginTime.Name = "dtBeginTime";
            this.dtBeginTime.Size = new System.Drawing.Size(169, 21);
            this.dtBeginTime.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "~";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择推送时间：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbVibration);
            this.groupBox2.Controls.Add(this.cbVoltage);
            this.groupBox2.Controls.Add(this.cbDeviceTempe);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 53);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // cbVibration
            // 
            this.cbVibration.AutoSize = true;
            this.cbVibration.Location = new System.Drawing.Point(337, 23);
            this.cbVibration.Name = "cbVibration";
            this.cbVibration.Size = new System.Drawing.Size(84, 16);
            this.cbVibration.TabIndex = 4;
            this.cbVibration.Text = "波形特征值";
            this.cbVibration.UseVisualStyleBackColor = true;
            // 
            // cbVoltage
            // 
            this.cbVoltage.AutoSize = true;
            this.cbVoltage.Location = new System.Drawing.Point(245, 23);
            this.cbVoltage.Name = "cbVoltage";
            this.cbVoltage.Size = new System.Drawing.Size(72, 16);
            this.cbVoltage.TabIndex = 3;
            this.cbVoltage.Text = "电池电压";
            this.cbVoltage.UseVisualStyleBackColor = true;
            // 
            // cbDeviceTempe
            // 
            this.cbDeviceTempe.AutoSize = true;
            this.cbDeviceTempe.Location = new System.Drawing.Point(156, 23);
            this.cbDeviceTempe.Name = "cbDeviceTempe";
            this.cbDeviceTempe.Size = new System.Drawing.Size(72, 16);
            this.cbDeviceTempe.TabIndex = 2;
            this.cbDeviceTempe.Text = "设备温度";
            this.cbDeviceTempe.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "请选择推送数据类型：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnPush);
            this.groupBox3.Location = new System.Drawing.Point(12, 161);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(500, 63);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // btnPush
            // 
            this.btnPush.Location = new System.Drawing.Point(354, 20);
            this.btnPush.Name = "btnPush";
            this.btnPush.Size = new System.Drawing.Size(75, 28);
            this.btnPush.TabIndex = 0;
            this.btnPush.Text = "推    送";
            this.btnPush.UseVisualStyleBackColor = true;
            this.btnPush.Click += new System.EventHandler(this.btnPush_Click);
            // 
            // PushManually
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 239);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PushManually";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "采集数据推送";
            this.Load += new System.EventHandler(this.PushManually_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtEndTime;
        private System.Windows.Forms.DateTimePicker dtBeginTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbVibration;
        private System.Windows.Forms.CheckBox cbVoltage;
        private System.Windows.Forms.CheckBox cbDeviceTempe;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnPush;
    }
}

