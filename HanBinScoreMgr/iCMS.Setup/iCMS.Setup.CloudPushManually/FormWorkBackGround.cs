using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iCMS.Setup.CloudPushManually
{
    public delegate void DoWorkBackGround(BackgroundWorker worker, DoWorkEventArgs e);

    public partial class FormWorkBackGround : Form
    {
        public FormWorkBackGround()
        {
            InitializeComponent();
        }


        public FormWorkBackGround(DoWorkBackGround onDoWorkBackGround)
        {
            InitializeComponent();

            m_OnDoWorkBackGround = onDoWorkBackGround;
        }

        private BackgroundWorker m_worker;

        private DoWorkBackGround m_OnDoWorkBackGround;

        private bool m_AutoProgress;
        /// <summary>
        /// 是否自动更新进度
        /// </summary>
        public bool AutoUpdateProgressBar
        {
            get { return m_AutoProgress; }
            set
            {
                m_AutoProgress = value;
            }
        }

        private bool m_SupportsCancellation;
        /// <summary>
        /// 是否可以取消
        /// </summary>
        public bool SupportsCancellation
        {
            get { return m_SupportsCancellation; }
            set
            {
                m_SupportsCancellation = value;
            }
        }


        private string m_WorkState;
        /// <summary>
        /// 工作状态
        /// </summary>
        public string WorkState
        {
            get { return m_WorkState; }
            set { m_WorkState = value; }
        }


        private object m_Argument;
        /// <summary>
        /// 工作函数的参数
        /// </summary>
        public object Argument
        {
            get { return m_Argument; }
            set { m_Argument = value; }
        }


        /// <summary>
        /// 运行结果
        /// </summary>
        public object WorkResult
        {
            get;
            set;
        }




        /// <summary>
        /// 窗体装载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormWorkBackGround_Load(object sender, EventArgs e)
        {
            m_worker = new BackgroundWorker();

            m_worker.DoWork += new DoWorkEventHandler(m_worker_DoWork);

            m_worker.ProgressChanged += new ProgressChangedEventHandler(m_worker_ProgressChanged);

            m_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_worker_RunWorkerCompleted);

            if (m_OnDoWorkBackGround == null)
            {
                this.Close();
            }

            //是否自动更新进度
            if (AutoUpdateProgressBar)
            {
                m_worker.WorkerReportsProgress = true;

                this.progressBarForm.Style = ProgressBarStyle.Marquee;

                this.labelText.Text = WorkState;
            }
            else
            {
                // 报告进度
                m_worker.WorkerReportsProgress = true;
            }


            // 能否取消
            if (SupportsCancellation)
            {
                this.btnCancel.Visible = true;

                this.progressBarForm.Width = 365;

                m_worker.WorkerSupportsCancellation = true;

            }
            else
            {
                this.btnCancel.Visible = false;

                this.progressBarForm.Width = 415;
            }

            // 启动程序
            m_worker.RunWorkerAsync(Argument);
        }


        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                this.DialogResult = DialogResult.No;

                this.Close();
            }
            else
            {
                WorkResult = e.Result;

                this.DialogResult = DialogResult.Yes;

                this.Close();
            }
        }

        /// <summary>
        /// 进度改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarForm.Value = e.ProgressPercentage;

            if (e.UserState != null)
            {
                this.labelText.Text = e.UserState.ToString();
            }
        }

        /// <summary>
        /// 工作函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            //System.Threading.Thread.CurrentThread.CurrentCulture = WindDAQ.Utility.ResourceLocator.This.CurrentCI;
            m_OnDoWorkBackGround(worker, e);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {       
            m_worker.CancelAsync();
        }

        private void FormWorkBackGround_Load_1(object sender, EventArgs e)
        {
            m_worker = new BackgroundWorker();

            m_worker.DoWork += new DoWorkEventHandler(m_worker_DoWork);

            m_worker.ProgressChanged += new ProgressChangedEventHandler(m_worker_ProgressChanged);

            m_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_worker_RunWorkerCompleted);

            if (m_OnDoWorkBackGround == null)
            {
                this.Close();
            }

            //是否自动更新进度
            if (AutoUpdateProgressBar)
            {
                m_worker.WorkerReportsProgress = true;

                this.progressBarForm.Style = ProgressBarStyle.Marquee;

                this.labelText.Text = WorkState;
            }
            else
            {
                // 报告进度
                m_worker.WorkerReportsProgress = true;
            }


            // 能否取消
            if (SupportsCancellation)
            {
                this.btnCancel.Visible = true;

                this.progressBarForm.Width = 365;

                m_worker.WorkerSupportsCancellation = true;

            }
            else
            {
                this.btnCancel.Visible = false;

                this.progressBarForm.Width = 420;
            }

            // 启动程序
            m_worker.RunWorkerAsync(Argument);
        }
    }
}
