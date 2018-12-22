namespace iCMS.Setup.CloudProxyConfig
{
    partial class CloudProxyConfigForm
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
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.lbl_ErrorMessage = new System.Windows.Forms.Label();
            this.txtCloudCommunicationAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.tbSecret = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(177, 181);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 9;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(74, 181);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 8;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // lbl_ErrorMessage
            // 
            this.lbl_ErrorMessage.AutoSize = true;
            this.lbl_ErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.lbl_ErrorMessage.Location = new System.Drawing.Point(116, 28);
            this.lbl_ErrorMessage.Name = "lbl_ErrorMessage";
            this.lbl_ErrorMessage.Size = new System.Drawing.Size(125, 12);
            this.lbl_ErrorMessage.TabIndex = 7;
            this.lbl_ErrorMessage.Text = "此处放置错误提示信息";
            // 
            // txtCloudCommunicationAddress
            // 
            this.txtCloudCommunicationAddress.Location = new System.Drawing.Point(118, 63);
            this.txtCloudCommunicationAddress.Name = "txtCloudCommunicationAddress";
            this.txtCloudCommunicationAddress.Size = new System.Drawing.Size(231, 21);
            this.txtCloudCommunicationAddress.TabIndex = 6;
            this.txtCloudCommunicationAddress.Text = "http://192.168.20.8:2894/";
            this.txtCloudCommunicationAddress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtCloudCommunicationAddress_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "云通讯地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "钥匙：";
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(118, 100);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(231, 21);
            this.tbKey.TabIndex = 11;
            // 
            // tbSecret
            // 
            this.tbSecret.Location = new System.Drawing.Point(118, 137);
            this.tbSecret.Name = "tbSecret";
            this.tbSecret.Size = new System.Drawing.Size(231, 21);
            this.tbSecret.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "密钥：";
            // 
            // CloudProxyConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 216);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSecret);
            this.Controls.Add(this.tbKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.lbl_ErrorMessage);
            this.Controls.Add(this.txtCloudCommunicationAddress);
            this.Controls.Add(this.label1);
            this.Name = "CloudProxyConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "云代理配置 【V1.4.1】";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloudProxyConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.CloudProxyConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Label lbl_ErrorMessage;
        private System.Windows.Forms.TextBox txtCloudCommunicationAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.TextBox tbSecret;
        private System.Windows.Forms.Label label3;
    }
}