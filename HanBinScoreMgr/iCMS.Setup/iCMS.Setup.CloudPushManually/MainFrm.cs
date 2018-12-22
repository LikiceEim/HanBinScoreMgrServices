using iCMS.Cloud.JiaXun.Commons;
using iCMS.Common.Component.Tool;
using iCMS.Communication.PushManually;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Frameworks.Core.Repository;
using iCMS.Setup.CloudPushManually.Function;
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
    public partial class MainFrm : Form
    {
        private IRepository<DevAlmRecord> devAmRecordRepository = new Repository<DevAlmRecord>();
        private IRepository<WSnAlmRecord> wsnAlmRecordRepository = new Repository<WSnAlmRecord>();


        private bool IsDBConfigOK = false;
        public MainFrm()
        {
            InitializeComponent();

            this.btnPushConfigData.Enabled = false;
            this.btnPushAlarmData.Enabled = false;
            this.btnPushCollectData.Enabled = false;
        }

        private void btnPushConfigData_Click(object sender, EventArgs e)
        {
            //#region 后台线程
            //DoWorkBackGround workFunc = new DoWorkBackGround(LoadConfigForm);

            //FormWorkBackGround frm = new FormWorkBackGround(workFunc);

            //// 程序更新进度
            //frm.AutoUpdateProgressBar = true;
            //// 可以取消
            //frm.SupportsCancellation = false;

            //if (frm.ShowDialog() == DialogResult.Yes)
            //{
            //    //MessageBox.Show("报警数据推送成功！", "提示");
            //}
            //#endregion

            CustomizedPushFrm frm = new CustomizedPushFrm();
            frm.ShowDialog();
        }

        private void LoadConfigForm(BackgroundWorker worker, DoWorkEventArgs e)
        {
            CustomizedPushFrm frm = new CustomizedPushFrm();
            frm.ShowDialog();
        }

        private void btnPushAlarmData_Click(object sender, EventArgs e)
        {
            #region 后台线程
            DoWorkBackGround workFunc = new DoWorkBackGround(PushAlarmRecord);

            FormWorkBackGround frm = new FormWorkBackGround(workFunc);

            // 程序更新进度
            frm.AutoUpdateProgressBar = true;
            // 可以取消
            frm.SupportsCancellation = false;

            if (frm.ShowDialog() == DialogResult.Yes)
            {
                MessageBox.Show("报警数据推送成功！", "提示");
            }
            #endregion
        }

        private void PushAlarmRecord(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                CloudDataProvider dataProvider = new CloudDataProvider();
                string cloudData = string.Empty;
                List<int> devAlmIDs = devAmRecordRepository.GetDatas<DevAlmRecord>(t => t.BDate == t.EDate && (t.MSAlmID == 1 || t.MSAlmID == 2), true).Select(t => t.AlmRecordID).ToList();
                List<int> wsnAlarmIDs = wsnAlmRecordRepository.GetDatas<WSnAlmRecord>(t => t.BDate == t.EDate && (t.MSAlmID == 4 || t.MSAlmID == 5 || t.MSAlmID == 6), true).Select(t => t.AlmRecordID).ToList();
                foreach (int id in devAlmIDs)
                {
                    cloudData = dataProvider.GetDevAlmRecord(id);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_Alarms);
                }

                foreach (int id in wsnAlarmIDs)
                {
                    cloudData = dataProvider.GetWsnAlmRecord(id);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_Alarms);
                }

                int totalAlmRecord = devAlmIDs.Count + wsnAlarmIDs.Count;

                if (totalAlmRecord < 1)
                {
                    return;
                }
                int progress = 10;
                for (int inxRoute = 0; inxRoute < totalAlmRecord; inxRoute++)
                {
                    progress += 90 / totalAlmRecord;
                    worker.ReportProgress(progress);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                //MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void btnPushCollectData_Click(object sender, EventArgs e)
        {
            PushManually frm = new PushManually();
            frm.ShowDialog();
        }

        public void ExecSend(string cloudData, string url)
        {
            if (!string.IsNullOrEmpty(cloudData))
            {
                CommunicationHelper.SendData(cloudData, url, CommonVariate.Cloud_URL_Method_Add);
            }
        }

        private void btnDBConfig_Click(object sender, EventArgs e)
        {
            DBConfigFrm frm = new DBConfigFrm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.btnPushConfigData.Enabled = true;
                this.btnPushAlarmData.Enabled = true;
                this.btnPushCollectData.Enabled = true;
            }
        }
    }
}
