using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iCMS.Setup.CloudPushManually
{
    public partial class AlarmDataPushFrm : Form
    {
        public AlarmDataPushFrm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            #region 后台线程
            // 后台执行连接手持
            DoWorkBackGround workFunc = new DoWorkBackGround(BeginTimeCount);

            FormWorkBackGround frm = new FormWorkBackGround(workFunc);

            // 程序更新进度
            frm.AutoUpdateProgressBar = true;
            // 可以取消
            frm.SupportsCancellation = true;

            if (frm.ShowDialog() == DialogResult.Yes)
            {
                MessageBox.Show("计时结束！", "提示");
            }
            #endregion
        }

        private void BeginTimeCount(BackgroundWorker worker, DoWorkEventArgs e)
        {
            int secords = 10;
            int progress = 0;

            int te = 100 / 10;

            for (int i = 1; i <= 10;i++ )
            {
                //this.lblText.Text = i.ToString();
                progress += te;

                worker.ReportProgress(progress);
                Thread.Sleep(1000);
            }
        }
    }
}
