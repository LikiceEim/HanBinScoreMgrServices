namespace iCMS.Setup.BDSetup
{
    partial class DBSetup
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
            this.DBNamelbl = new System.Windows.Forms.Label();
            this.DBNametb = new System.Windows.Forms.TextBox();
            this.LoginNamelbl = new System.Windows.Forms.Label();
            this.LoginNametb = new System.Windows.Forms.TextBox();
            this.PassWordlbl = new System.Windows.Forms.Label();
            this.PassWordtb = new System.Windows.Forms.TextBox();
            this.ServerNamelbl = new System.Windows.Forms.Label();
            this.ServerNametb = new System.Windows.Forms.TextBox();
            this.Partitionlbl = new System.Windows.Forms.Label();
            this.Partitiontb = new System.Windows.Forms.TextBox();
            this.Setupbtn = new System.Windows.Forms.Button();
            this.first = new System.Windows.Forms.RadioButton();
            this.second = new System.Windows.Forms.RadioButton();
            this.third = new System.Windows.Forms.RadioButton();
            this.fourth = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.fifth = new System.Windows.Forms.RadioButton();
            this.lbTrigger = new System.Windows.Forms.Label();
            this.lable6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sixth = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dbPathtb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.keyTb = new System.Windows.Forms.TextBox();
            this.secretTb = new System.Windows.Forms.TextBox();
            this.servertiplbl = new System.Windows.Forms.Label();
            this.dbtiplbl = new System.Windows.Forms.Label();
            this.dbaddresstiplbl = new System.Windows.Forms.Label();
            this.nametiplbl = new System.Windows.Forms.Label();
            this.passwordtiplbl = new System.Windows.Forms.Label();
            this.parttiplbl = new System.Windows.Forms.Label();
            this.keytiplbl = new System.Windows.Forms.Label();
            this.secrettiplbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbUse = new System.Windows.Forms.CheckBox();
            this.btnCreateTrigger = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DBNamelbl
            // 
            this.DBNamelbl.AutoSize = true;
            this.DBNamelbl.Location = new System.Drawing.Point(38, 56);
            this.DBNamelbl.Name = "DBNamelbl";
            this.DBNamelbl.Size = new System.Drawing.Size(77, 12);
            this.DBNamelbl.TabIndex = 0;
            this.DBNamelbl.Text = "数据库名称：";
            // 
            // DBNametb
            // 
            this.DBNametb.Location = new System.Drawing.Point(117, 52);
            this.DBNametb.Name = "DBNametb";
            this.DBNametb.Size = new System.Drawing.Size(227, 21);
            this.DBNametb.TabIndex = 1;
            this.DBNametb.Text = "iCMSDB";
            this.DBNametb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DBNametb_KeyUp);
            // 
            // LoginNamelbl
            // 
            this.LoginNamelbl.AutoSize = true;
            this.LoginNamelbl.Location = new System.Drawing.Point(62, 115);
            this.LoginNamelbl.Name = "LoginNamelbl";
            this.LoginNamelbl.Size = new System.Drawing.Size(53, 12);
            this.LoginNamelbl.TabIndex = 2;
            this.LoginNamelbl.Text = "登录名：";
            // 
            // LoginNametb
            // 
            this.LoginNametb.Location = new System.Drawing.Point(117, 112);
            this.LoginNametb.Name = "LoginNametb";
            this.LoginNametb.Size = new System.Drawing.Size(227, 21);
            this.LoginNametb.TabIndex = 3;
            this.LoginNametb.Text = "sa";
            this.LoginNametb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LoginNametb_KeyUp);
            // 
            // PassWordlbl
            // 
            this.PassWordlbl.AutoSize = true;
            this.PassWordlbl.Location = new System.Drawing.Point(73, 148);
            this.PassWordlbl.Name = "PassWordlbl";
            this.PassWordlbl.Size = new System.Drawing.Size(41, 12);
            this.PassWordlbl.TabIndex = 4;
            this.PassWordlbl.Text = "密码：";
            // 
            // PassWordtb
            // 
            this.PassWordtb.Location = new System.Drawing.Point(117, 144);
            this.PassWordtb.Name = "PassWordtb";
            this.PassWordtb.Size = new System.Drawing.Size(227, 21);
            this.PassWordtb.TabIndex = 5;
            this.PassWordtb.Text = "123456789";
            this.PassWordtb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PassWordtb_KeyUp);
            // 
            // ServerNamelbl
            // 
            this.ServerNamelbl.AutoSize = true;
            this.ServerNamelbl.Location = new System.Drawing.Point(38, 24);
            this.ServerNamelbl.Name = "ServerNamelbl";
            this.ServerNamelbl.Size = new System.Drawing.Size(77, 12);
            this.ServerNamelbl.TabIndex = 6;
            this.ServerNamelbl.Text = "服务器名称：";
            // 
            // ServerNametb
            // 
            this.ServerNametb.Location = new System.Drawing.Point(117, 21);
            this.ServerNametb.Name = "ServerNametb";
            this.ServerNametb.Size = new System.Drawing.Size(227, 21);
            this.ServerNametb.TabIndex = 7;
            this.ServerNametb.Text = "192.168.20.8";
            this.ServerNametb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ServerNametb_KeyUp);
            // 
            // Partitionlbl
            // 
            this.Partitionlbl.AutoSize = true;
            this.Partitionlbl.Location = new System.Drawing.Point(47, 178);
            this.Partitionlbl.Name = "Partitionlbl";
            this.Partitionlbl.Size = new System.Drawing.Size(65, 12);
            this.Partitionlbl.TabIndex = 8;
            this.Partitionlbl.Text = "分区路径：";
            // 
            // Partitiontb
            // 
            this.Partitiontb.Location = new System.Drawing.Point(117, 173);
            this.Partitiontb.Name = "Partitiontb";
            this.Partitiontb.Size = new System.Drawing.Size(227, 21);
            this.Partitiontb.TabIndex = 9;
            this.Partitiontb.Text = "D:\\ICMSPARTDB";
            this.Partitiontb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Partitiontb_KeyUp);
            // 
            // Setupbtn
            // 
            this.Setupbtn.Location = new System.Drawing.Point(117, 287);
            this.Setupbtn.Name = "Setupbtn";
            this.Setupbtn.Size = new System.Drawing.Size(75, 23);
            this.Setupbtn.TabIndex = 10;
            this.Setupbtn.Text = "开始安装";
            this.Setupbtn.UseVisualStyleBackColor = true;
            this.Setupbtn.Click += new System.EventHandler(this.Setupbtn_Click);
            // 
            // first
            // 
            this.first.AutoSize = true;
            this.first.Enabled = false;
            this.first.Location = new System.Drawing.Point(11, 12);
            this.first.Name = "first";
            this.first.Size = new System.Drawing.Size(14, 13);
            this.first.TabIndex = 11;
            this.first.TabStop = true;
            this.first.UseVisualStyleBackColor = true;
            // 
            // second
            // 
            this.second.AutoSize = true;
            this.second.Enabled = false;
            this.second.Location = new System.Drawing.Point(11, 34);
            this.second.Name = "second";
            this.second.Size = new System.Drawing.Size(14, 13);
            this.second.TabIndex = 12;
            this.second.TabStop = true;
            this.second.UseVisualStyleBackColor = true;
            // 
            // third
            // 
            this.third.AutoSize = true;
            this.third.Enabled = false;
            this.third.Location = new System.Drawing.Point(11, 56);
            this.third.Name = "third";
            this.third.Size = new System.Drawing.Size(14, 13);
            this.third.TabIndex = 13;
            this.third.TabStop = true;
            this.third.UseVisualStyleBackColor = true;
            // 
            // fourth
            // 
            this.fourth.AutoSize = true;
            this.fourth.Enabled = false;
            this.fourth.Location = new System.Drawing.Point(11, 78);
            this.fourth.Name = "fourth";
            this.fourth.Size = new System.Drawing.Size(14, 13);
            this.fourth.TabIndex = 14;
            this.fourth.TabStop = true;
            this.fourth.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.fifth);
            this.panel1.Controls.Add(this.lbTrigger);
            this.panel1.Controls.Add(this.lable6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.sixth);
            this.panel1.Controls.Add(this.first);
            this.panel1.Controls.Add(this.fourth);
            this.panel1.Controls.Add(this.second);
            this.panel1.Controls.Add(this.third);
            this.panel1.Location = new System.Drawing.Point(27, 332);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 146);
            this.panel1.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "第五步：创建触发器";
            // 
            // fifth
            // 
            this.fifth.AutoSize = true;
            this.fifth.Enabled = false;
            this.fifth.Location = new System.Drawing.Point(11, 98);
            this.fifth.Name = "fifth";
            this.fifth.Size = new System.Drawing.Size(14, 13);
            this.fifth.TabIndex = 21;
            this.fifth.TabStop = true;
            this.fifth.UseVisualStyleBackColor = true;
            // 
            // lbTrigger
            // 
            this.lbTrigger.AutoSize = true;
            this.lbTrigger.Location = new System.Drawing.Point(26, 119);
            this.lbTrigger.Name = "lbTrigger";
            this.lbTrigger.Size = new System.Drawing.Size(149, 12);
            this.lbTrigger.TabIndex = 20;
            this.lbTrigger.Text = "第六步：创建云通讯触发器";
            // 
            // lable6
            // 
            this.lable6.AutoSize = true;
            this.lable6.Location = new System.Drawing.Point(26, 79);
            this.lable6.Name = "lable6";
            this.lable6.Size = new System.Drawing.Size(101, 12);
            this.lable6.TabIndex = 19;
            this.lable6.Text = "第四步：创建分区";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "第三步：导入数据";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "第二步：创建数据库结构";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "第一步：创建数据库";
            // 
            // sixth
            // 
            this.sixth.AutoSize = true;
            this.sixth.Enabled = false;
            this.sixth.Location = new System.Drawing.Point(11, 118);
            this.sixth.Name = "sixth";
            this.sixth.Size = new System.Drawing.Size(14, 13);
            this.sixth.TabIndex = 15;
            this.sixth.TabStop = true;
            this.sixth.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "数据库地址：";
            // 
            // dbPathtb
            // 
            this.dbPathtb.Location = new System.Drawing.Point(117, 83);
            this.dbPathtb.Name = "dbPathtb";
            this.dbPathtb.Size = new System.Drawing.Size(227, 21);
            this.dbPathtb.TabIndex = 17;
            this.dbPathtb.Text = "d:\\iCMSDB";
            this.dbPathtb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dbPathtb_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "钥匙：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(69, 230);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "密钥：";
            // 
            // keyTb
            // 
            this.keyTb.Location = new System.Drawing.Point(117, 202);
            this.keyTb.Name = "keyTb";
            this.keyTb.Size = new System.Drawing.Size(227, 21);
            this.keyTb.TabIndex = 20;
            this.keyTb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyTb_KeyUp);
            // 
            // secretTb
            // 
            this.secretTb.Location = new System.Drawing.Point(117, 229);
            this.secretTb.Name = "secretTb";
            this.secretTb.Size = new System.Drawing.Size(227, 21);
            this.secretTb.TabIndex = 21;
            this.secretTb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.secretTb_KeyUp);
            // 
            // servertiplbl
            // 
            this.servertiplbl.AutoSize = true;
            this.servertiplbl.ForeColor = System.Drawing.Color.Red;
            this.servertiplbl.Location = new System.Drawing.Point(350, 24);
            this.servertiplbl.Name = "servertiplbl";
            this.servertiplbl.Size = new System.Drawing.Size(101, 12);
            this.servertiplbl.TabIndex = 22;
            this.servertiplbl.Text = "请输入服务器名称";
            // 
            // dbtiplbl
            // 
            this.dbtiplbl.AutoSize = true;
            this.dbtiplbl.ForeColor = System.Drawing.Color.Red;
            this.dbtiplbl.Location = new System.Drawing.Point(350, 56);
            this.dbtiplbl.Name = "dbtiplbl";
            this.dbtiplbl.Size = new System.Drawing.Size(101, 12);
            this.dbtiplbl.TabIndex = 23;
            this.dbtiplbl.Text = "请输入数据库名称";
            // 
            // dbaddresstiplbl
            // 
            this.dbaddresstiplbl.AutoSize = true;
            this.dbaddresstiplbl.ForeColor = System.Drawing.Color.Red;
            this.dbaddresstiplbl.Location = new System.Drawing.Point(350, 88);
            this.dbaddresstiplbl.Name = "dbaddresstiplbl";
            this.dbaddresstiplbl.Size = new System.Drawing.Size(101, 12);
            this.dbaddresstiplbl.TabIndex = 24;
            this.dbaddresstiplbl.Text = "请输入数据库地址";
            // 
            // nametiplbl
            // 
            this.nametiplbl.AutoSize = true;
            this.nametiplbl.ForeColor = System.Drawing.Color.Red;
            this.nametiplbl.Location = new System.Drawing.Point(350, 116);
            this.nametiplbl.Name = "nametiplbl";
            this.nametiplbl.Size = new System.Drawing.Size(77, 12);
            this.nametiplbl.TabIndex = 25;
            this.nametiplbl.Text = "请输入登录名";
            // 
            // passwordtiplbl
            // 
            this.passwordtiplbl.AutoSize = true;
            this.passwordtiplbl.ForeColor = System.Drawing.Color.Red;
            this.passwordtiplbl.Location = new System.Drawing.Point(350, 149);
            this.passwordtiplbl.Name = "passwordtiplbl";
            this.passwordtiplbl.Size = new System.Drawing.Size(65, 12);
            this.passwordtiplbl.TabIndex = 26;
            this.passwordtiplbl.Text = "请输入密码";
            // 
            // parttiplbl
            // 
            this.parttiplbl.AutoSize = true;
            this.parttiplbl.ForeColor = System.Drawing.Color.Red;
            this.parttiplbl.Location = new System.Drawing.Point(349, 177);
            this.parttiplbl.Name = "parttiplbl";
            this.parttiplbl.Size = new System.Drawing.Size(89, 12);
            this.parttiplbl.TabIndex = 27;
            this.parttiplbl.Text = "请输入分区路径";
            // 
            // keytiplbl
            // 
            this.keytiplbl.AutoSize = true;
            this.keytiplbl.ForeColor = System.Drawing.Color.Red;
            this.keytiplbl.Location = new System.Drawing.Point(350, 208);
            this.keytiplbl.Name = "keytiplbl";
            this.keytiplbl.Size = new System.Drawing.Size(65, 12);
            this.keytiplbl.TabIndex = 28;
            this.keytiplbl.Text = "请输入钥匙";
            // 
            // secrettiplbl
            // 
            this.secrettiplbl.AutoSize = true;
            this.secrettiplbl.ForeColor = System.Drawing.Color.Red;
            this.secrettiplbl.Location = new System.Drawing.Point(350, 235);
            this.secrettiplbl.Name = "secrettiplbl";
            this.secrettiplbl.Size = new System.Drawing.Size(65, 12);
            this.secrettiplbl.TabIndex = 29;
            this.secrettiplbl.Text = "请输入密钥";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 259);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "启用云平台：";
            // 
            // cbUse
            // 
            this.cbUse.AutoSize = true;
            this.cbUse.Checked = true;
            this.cbUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUse.Location = new System.Drawing.Point(118, 259);
            this.cbUse.Name = "cbUse";
            this.cbUse.Size = new System.Drawing.Size(15, 14);
            this.cbUse.TabIndex = 31;
            this.cbUse.UseVisualStyleBackColor = true;
            this.cbUse.CheckedChanged += new System.EventHandler(this.cbUse_CheckedChanged);
            // 
            // btnCreateTrigger
            // 
            this.btnCreateTrigger.Location = new System.Drawing.Point(198, 287);
            this.btnCreateTrigger.Name = "btnCreateTrigger";
            this.btnCreateTrigger.Size = new System.Drawing.Size(159, 23);
            this.btnCreateTrigger.TabIndex = 32;
            this.btnCreateTrigger.Text = "安装后创建云通讯触发器";
            this.btnCreateTrigger.UseVisualStyleBackColor = true;
            this.btnCreateTrigger.Click += new System.EventHandler(this.btnCreateTrigger_Click);
            // 
            // DBSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 487);
            this.Controls.Add(this.btnCreateTrigger);
            this.Controls.Add(this.cbUse);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.secrettiplbl);
            this.Controls.Add(this.keytiplbl);
            this.Controls.Add(this.parttiplbl);
            this.Controls.Add(this.passwordtiplbl);
            this.Controls.Add(this.nametiplbl);
            this.Controls.Add(this.dbaddresstiplbl);
            this.Controls.Add(this.dbtiplbl);
            this.Controls.Add(this.servertiplbl);
            this.Controls.Add(this.secretTb);
            this.Controls.Add(this.keyTb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dbPathtb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Setupbtn);
            this.Controls.Add(this.Partitiontb);
            this.Controls.Add(this.Partitionlbl);
            this.Controls.Add(this.ServerNametb);
            this.Controls.Add(this.ServerNamelbl);
            this.Controls.Add(this.PassWordtb);
            this.Controls.Add(this.PassWordlbl);
            this.Controls.Add(this.LoginNametb);
            this.Controls.Add(this.LoginNamelbl);
            this.Controls.Add(this.DBNametb);
            this.Controls.Add(this.DBNamelbl);
            this.Name = "DBSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库安装配置";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DBNamelbl;
        private System.Windows.Forms.TextBox DBNametb;
        private System.Windows.Forms.Label LoginNamelbl;
        private System.Windows.Forms.TextBox LoginNametb;
        private System.Windows.Forms.Label PassWordlbl;
        private System.Windows.Forms.TextBox PassWordtb;
        private System.Windows.Forms.Label ServerNamelbl;
        private System.Windows.Forms.TextBox ServerNametb;
        private System.Windows.Forms.Label Partitionlbl;
        private System.Windows.Forms.TextBox Partitiontb;
        private System.Windows.Forms.Button Setupbtn;
        private System.Windows.Forms.RadioButton first;
        private System.Windows.Forms.RadioButton second;
        private System.Windows.Forms.RadioButton third;
        private System.Windows.Forms.RadioButton fourth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dbPathtb;
        private System.Windows.Forms.RadioButton sixth;
        private System.Windows.Forms.Label lbTrigger;
        private System.Windows.Forms.Label lable6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox keyTb;
        private System.Windows.Forms.TextBox secretTb;
        private System.Windows.Forms.Label servertiplbl;
        private System.Windows.Forms.Label dbtiplbl;
        private System.Windows.Forms.Label dbaddresstiplbl;
        private System.Windows.Forms.Label nametiplbl;
        private System.Windows.Forms.Label passwordtiplbl;
        private System.Windows.Forms.Label parttiplbl;
        private System.Windows.Forms.Label keytiplbl;
        private System.Windows.Forms.Label secrettiplbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbUse;
        private System.Windows.Forms.Button btnCreateTrigger;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton fifth;
    }
}

