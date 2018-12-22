namespace iCMS.Setup.ClientSecret
{
    partial class Main
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tPKey = new System.Windows.Forms.TabPage();
            this.CreateBtn = new System.Windows.Forms.Button();
            this.secretTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.keyTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.originalTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpconnectString = new System.Windows.Forms.TabPage();
            this.btnDo = new System.Windows.Forms.Button();
            this.rtxtjiami = new System.Windows.Forms.RichTextBox();
            this.rtxtyuanshi = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblyuanshi = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tPKey.SuspendLayout();
            this.tpconnectString.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tPKey);
            this.tabControl.Controls.Add(this.tpconnectString);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(354, 192);
            this.tabControl.TabIndex = 7;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tPKey
            // 
            this.tPKey.Controls.Add(this.CreateBtn);
            this.tPKey.Controls.Add(this.secretTxt);
            this.tPKey.Controls.Add(this.label3);
            this.tPKey.Controls.Add(this.keyTxt);
            this.tPKey.Controls.Add(this.label2);
            this.tPKey.Controls.Add(this.originalTxt);
            this.tPKey.Controls.Add(this.label1);
            this.tPKey.Location = new System.Drawing.Point(4, 22);
            this.tPKey.Name = "tPKey";
            this.tPKey.Padding = new System.Windows.Forms.Padding(3);
            this.tPKey.Size = new System.Drawing.Size(346, 166);
            this.tPKey.TabIndex = 0;
            this.tPKey.Text = "密钥生成";
            this.tPKey.UseVisualStyleBackColor = true;
            // 
            // CreateBtn
            // 
            this.CreateBtn.Location = new System.Drawing.Point(114, 109);
            this.CreateBtn.Name = "CreateBtn";
            this.CreateBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateBtn.TabIndex = 13;
            this.CreateBtn.Text = "生成";
            this.CreateBtn.UseVisualStyleBackColor = true;
            this.CreateBtn.Click += new System.EventHandler(this.CreateBtn_Click);
            // 
            // secretTxt
            // 
            this.secretTxt.Location = new System.Drawing.Point(114, 70);
            this.secretTxt.Name = "secretTxt";
            this.secretTxt.Size = new System.Drawing.Size(209, 21);
            this.secretTxt.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "密钥(Secret)：";
            // 
            // keyTxt
            // 
            this.keyTxt.Location = new System.Drawing.Point(114, 38);
            this.keyTxt.Name = "keyTxt";
            this.keyTxt.Size = new System.Drawing.Size(209, 21);
            this.keyTxt.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "钥匙(key)：";
            // 
            // originalTxt
            // 
            this.originalTxt.Location = new System.Drawing.Point(114, 8);
            this.originalTxt.Name = "originalTxt";
            this.originalTxt.Size = new System.Drawing.Size(209, 21);
            this.originalTxt.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "请输入加密符：";
            // 
            // tpconnectString
            // 
            this.tpconnectString.Controls.Add(this.btnDo);
            this.tpconnectString.Controls.Add(this.rtxtjiami);
            this.tpconnectString.Controls.Add(this.rtxtyuanshi);
            this.tpconnectString.Controls.Add(this.label4);
            this.tpconnectString.Controls.Add(this.lblyuanshi);
            this.tpconnectString.Location = new System.Drawing.Point(4, 22);
            this.tpconnectString.Name = "tpconnectString";
            this.tpconnectString.Padding = new System.Windows.Forms.Padding(3);
            this.tpconnectString.Size = new System.Drawing.Size(346, 166);
            this.tpconnectString.TabIndex = 1;
            this.tpconnectString.Text = "数据库连接串加、解密";
            this.tpconnectString.UseVisualStyleBackColor = true;
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(135, 126);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(75, 23);
            this.btnDo.TabIndex = 2;
            this.btnDo.Text = "执行";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // rtxtjiami
            // 
            this.rtxtjiami.Location = new System.Drawing.Point(9, 77);
            this.rtxtjiami.Name = "rtxtjiami";
            this.rtxtjiami.Size = new System.Drawing.Size(329, 35);
            this.rtxtjiami.TabIndex = 1;
            this.rtxtjiami.Text = "";
            // 
            // rtxtyuanshi
            // 
            this.rtxtyuanshi.Location = new System.Drawing.Point(9, 22);
            this.rtxtyuanshi.Name = "rtxtyuanshi";
            this.rtxtyuanshi.Size = new System.Drawing.Size(329, 35);
            this.rtxtyuanshi.TabIndex = 1;
            this.rtxtyuanshi.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "加密后连接串";
            // 
            // lblyuanshi
            // 
            this.lblyuanshi.AutoSize = true;
            this.lblyuanshi.Location = new System.Drawing.Point(4, 6);
            this.lblyuanshi.Name = "lblyuanshi";
            this.lblyuanshi.Size = new System.Drawing.Size(77, 12);
            this.lblyuanshi.TabIndex = 0;
            this.lblyuanshi.Text = "加密前连接串";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 192);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(370, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 230);
            this.Name = "Main";
            this.Text = "客户端密钥生成工具";
            this.tabControl.ResumeLayout(false);
            this.tPKey.ResumeLayout(false);
            this.tPKey.PerformLayout();
            this.tpconnectString.ResumeLayout(false);
            this.tpconnectString.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tPKey;
        private System.Windows.Forms.Button CreateBtn;
        private System.Windows.Forms.TextBox secretTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox keyTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox originalTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpconnectString;
        private System.Windows.Forms.Button btnDo;
        private System.Windows.Forms.RichTextBox rtxtjiami;
        private System.Windows.Forms.RichTextBox rtxtyuanshi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblyuanshi;

    }
}

