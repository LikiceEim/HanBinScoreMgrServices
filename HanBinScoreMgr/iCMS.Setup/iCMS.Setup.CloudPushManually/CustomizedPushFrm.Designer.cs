namespace iCMS.Setup.CloudPushManually
{
    partial class CustomizedPushFrm
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
            this.tviCMSDatas = new System.Windows.Forms.TreeView();
            this.btnPushCloud = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tviCMSDatas);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(422, 362);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tviCMSDatas
            // 
            this.tviCMSDatas.Location = new System.Drawing.Point(13, 20);
            this.tviCMSDatas.Name = "tviCMSDatas";
            this.tviCMSDatas.Size = new System.Drawing.Size(370, 335);
            this.tviCMSDatas.TabIndex = 0;
            this.tviCMSDatas.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tviCMSDatas_AfterCheck);
            // 
            // btnPushCloud
            // 
            this.btnPushCloud.Location = new System.Drawing.Point(307, 17);
            this.btnPushCloud.Name = "btnPushCloud";
            this.btnPushCloud.Size = new System.Drawing.Size(75, 34);
            this.btnPushCloud.TabIndex = 1;
            this.btnPushCloud.Text = "开始推送";
            this.btnPushCloud.UseVisualStyleBackColor = true;
            this.btnPushCloud.Click += new System.EventHandler(this.btnPushCloud_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar);
            this.groupBox2.Controls.Add(this.btnPushCloud);
            this.groupBox2.Location = new System.Drawing.Point(13, 385);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(421, 60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(42, 20);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(231, 23);
            this.progressBar.TabIndex = 2;
            // 
            // CustomizedPushFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 471);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CustomizedPushFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置数据推送";
            this.Load += new System.EventHandler(this.CustomizedPushFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView tviCMSDatas;
        private System.Windows.Forms.Button btnPushCloud;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}