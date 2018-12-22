namespace iCMS.Setup.CloudPushManually
{
    partial class FormWorkBackGround
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
            this.progressBarForm = new System.Windows.Forms.ProgressBar();
            this.labelText = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBarForm
            // 
            this.progressBarForm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBarForm.Location = new System.Drawing.Point(7, 5);
            this.progressBarForm.Name = "progressBarForm";
            this.progressBarForm.Size = new System.Drawing.Size(355, 23);
            this.progressBarForm.TabIndex = 1;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelText.Location = new System.Drawing.Point(157, 25);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(131, 12);
            this.labelText.TabIndex = 3;
            this.labelText.Text = "数据推送中，请稍等...";
            this.labelText.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(366, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormWorkBackGround
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 32);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.progressBarForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWorkBackGround";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormWorkBackGround";
            this.Load += new System.EventHandler(this.FormWorkBackGround_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarForm;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Button btnCancel;
    }
}