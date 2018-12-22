namespace iCMS.Setup.CloudPushManually
{
    partial class MainFrm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPushCollectData = new System.Windows.Forms.Button();
            this.btnPushAlarmData = new System.Windows.Forms.Button();
            this.btnPushConfigData = new System.Windows.Forms.Button();
            this.btnDBConfig = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDBConfig);
            this.groupBox1.Controls.Add(this.btnPushCollectData);
            this.groupBox1.Controls.Add(this.btnPushAlarmData);
            this.groupBox1.Controls.Add(this.btnPushConfigData);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnPushCollectData
            // 
            this.btnPushCollectData.Location = new System.Drawing.Point(281, 49);
            this.btnPushCollectData.Name = "btnPushCollectData";
            this.btnPushCollectData.Size = new System.Drawing.Size(75, 45);
            this.btnPushCollectData.TabIndex = 2;
            this.btnPushCollectData.Text = "采集数据";
            this.btnPushCollectData.UseVisualStyleBackColor = true;
            this.btnPushCollectData.Click += new System.EventHandler(this.btnPushCollectData_Click);
            // 
            // btnPushAlarmData
            // 
            this.btnPushAlarmData.Location = new System.Drawing.Point(196, 49);
            this.btnPushAlarmData.Name = "btnPushAlarmData";
            this.btnPushAlarmData.Size = new System.Drawing.Size(75, 45);
            this.btnPushAlarmData.TabIndex = 1;
            this.btnPushAlarmData.Text = "报警数据";
            this.btnPushAlarmData.UseVisualStyleBackColor = true;
            this.btnPushAlarmData.Click += new System.EventHandler(this.btnPushAlarmData_Click);
            // 
            // btnPushConfigData
            // 
            this.btnPushConfigData.Location = new System.Drawing.Point(111, 49);
            this.btnPushConfigData.Name = "btnPushConfigData";
            this.btnPushConfigData.Size = new System.Drawing.Size(75, 45);
            this.btnPushConfigData.TabIndex = 0;
            this.btnPushConfigData.Text = "配置数据";
            this.btnPushConfigData.UseVisualStyleBackColor = true;
            this.btnPushConfigData.Click += new System.EventHandler(this.btnPushConfigData_Click);
            // 
            // btnDBConfig
            // 
            this.btnDBConfig.Location = new System.Drawing.Point(26, 49);
            this.btnDBConfig.Name = "btnDBConfig";
            this.btnDBConfig.Size = new System.Drawing.Size(75, 45);
            this.btnDBConfig.TabIndex = 3;
            this.btnDBConfig.Text = "数据库配置";
            this.btnDBConfig.UseVisualStyleBackColor = true;
            this.btnDBConfig.Click += new System.EventHandler(this.btnDBConfig_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 168);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请选择推送类型";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPushCollectData;
        private System.Windows.Forms.Button btnPushAlarmData;
        private System.Windows.Forms.Button btnPushConfigData;
        private System.Windows.Forms.Button btnDBConfig;
    }
}