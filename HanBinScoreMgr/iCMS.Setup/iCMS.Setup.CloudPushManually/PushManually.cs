/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Frameworks.Core.Repository
 *文件名：  PushManually
 *创建人：  QXM
 *创建时间：2017-01-16
 *描述：   手动推送
/************************************************************************************/
using iCMS.Cloud.JiaXun.Commons;
using iCMS.Cloud.JiaXun.DataConvert;
using iCMS.Cloud.JiaXun.Interface;
using iCMS.Frameworks.Core.DB;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Frameworks.Core.Repository;
using iCMS.Setup.CloudPushManually;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace iCMS.Communication.PushManually
{
    public partial class PushManually : Form
    {
        GetYunyiInterfaceData getData = null;
        private int pushCount = Int32.Parse(ConfigurationManager.AppSettings["PushCount"]);

        #region 数据库访问类
        private IRepository<TempeDeviceMsitedata_1> TempeDeviceMsitedata_1Repository = null;
        private IRepository<TempeDeviceMsitedata_2> TempeDeviceMsitedata_2Repository = null;
        private IRepository<TempeDeviceMsitedata_3> TempeDeviceMsitedata_3Repository = null;
        private IRepository<TempeDeviceMsitedata_4> TempeDeviceMsitedata_4Repository = null;
        private IRepository<VoltageWSMSiteData_1> VoltageWSMSiteData_1Repository = null;
        private IRepository<VoltageWSMSiteData_2> VoltageWSMSiteData_2Repository = null;
        private IRepository<VoltageWSMSiteData_3> VoltageWSMSiteData_3Repository = null;
        private IRepository<VoltageWSMSiteData_4> VoltageWSMSiteData_4Repository = null;
        private IRepository<VibratingSingalCharHisAcc> VibratingSingalCharHisAccRepository = null;
        private IRepository<VibratingSingalCharHisVel> VibratingSingalCharHisVelRepository = null;
        private IRepository<VibratingSingalCharHisEnvl> VibratingSingalCharHisEnvlRepository = null;
        private IRepository<MeasureSite> MeasureSiteRepository = null;
        #endregion

        public PushManually()
        {
            InitializeComponent();
            if (getData == null)
            {
                getData = new GetYunyiInterfaceData();
            }

            //初始化私有变量
            InitDAO();
        }

        private void PushManually_Load(object sender, EventArgs e)
        {
            #region 时间控件显示分秒
            //控制日期或时间的显示格式
            this.dtBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            //使用自定义格式
            this.dtBeginTime.Format = DateTimePickerFormat.Custom;
            //时间控件的启用
            this.dtBeginTime.ShowUpDown = true;

            //控制日期或时间的显示格式
            this.dtEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            //使用自定义格式
            this.dtEndTime.Format = DateTimePickerFormat.Custom;
            //时间控件的启用
            this.dtEndTime.ShowUpDown = true;
            #endregion
        }

        #region 点击事件
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPush_Click(object sender, EventArgs e)
        {
            #region 后台线程
            // 后台执行连接手持
            DoWorkBackGround workFunc = new DoWorkBackGround(LoadData);

            FormWorkBackGround frm = new FormWorkBackGround(workFunc);

            // 程序更新进度
            frm.AutoUpdateProgressBar = true;
            // 可以取消
            frm.SupportsCancellation = false;

            if (frm.ShowDialog() == DialogResult.Yes)
            {
                MessageBox.Show("采集数据推送完成！", "提示");
            }
            #endregion



            //btnPush.Enabled = false;
            //Thread thread = new Thread(new ThreadStart(LoadData));
            //thread.IsBackground = true;
            //thread.Start();

            //this.btnPush.Enabled = true;
        }
        #endregion

        #region 加载数据
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData(BackgroundWorker worker, DoWorkEventArgs e)
        {
            ///TODO:2017/02/21
            ///1.添加无任何采集数据时候的异常处理
            ///2.添加ReportProgress方法
            int totalCount = 0;
            int pageNum = 0;

            //推送总数
            int pushTotalCount = 0;

            var beginTime = this.dtBeginTime.Value;
            var endTime = this.dtEndTime.Value;
            try
            {
                //如果设备温度勾选，则推送设备温度数据
                if (this.cbDeviceTempe.Checked)
                {
                    #region 设备温度
                    List<TempData> cloudDeviceTempeDatas = new List<TempData>();

                    #region DeviceTempe1
                    //取得总数
                    totalCount = TempeDeviceMsitedata_1Repository
                       .GetDatas<TempeDeviceMsitedata_1>(t => t.SamplingDate >= beginTime && t.SamplingDate < endTime, true).Count();

                    pushTotalCount += totalCount;
                    pageNum = (totalCount + pushCount - 1) / pushCount; //(int)Math.Floor(Convert.ToDouble((totalCount1 / pushCount))) ;              
                    for (int i = 0; i < pageNum; i++)
                    {
                        List<TempData> deviceTempeList = TempeDeviceMsitedata_1Repository
                       .GetDatas<TempeDeviceMsitedata_1>(t => t.SamplingDate >= beginTime && t.SamplingDate < endTime, true).OrderBy(t => t.SamplingDate)
                       .Skip(i * pushCount).Take(pushCount)
                       .Select(t => new TempData { MSiteID = t.MsiteID, CollectTime = t.SamplingDate, MSData = t.MsDataValue }).ToList();

                        foreach (var tempData in deviceTempeList)
                        {
                            DeviceTempePost(tempData);
                        }
                    }
                    #endregion

                    #region DeviceTempe2
                    //取得总数
                    totalCount = TempeDeviceMsitedata_2Repository
                       .GetDatas<TempeDeviceMsitedata_2>(t => t.SamplingDate >= beginTime && t.SamplingDate < endTime, true).Count();

                    pageNum = (totalCount + pushCount - 1) / pushCount; //(int)Math.Floor(Convert.ToDouble((totalCount1 / pushCount))) ;              
                    for (int i = 0; i < pageNum; i++)
                    {
                        List<TempData> deviceTempeList = TempeDeviceMsitedata_2Repository
                       .GetDatas<TempeDeviceMsitedata_2>(t => t.SamplingDate >= beginTime && t.SamplingDate < endTime, true).OrderBy(t => t.SamplingDate)
                       .Skip(i * pushCount).Take(pushCount)
                       .Select(t => new TempData { MSiteID = t.MsiteID, CollectTime = t.SamplingDate, MSData = t.MsDataValue }).ToList();

                        foreach (var tempData in deviceTempeList)
                        {
                            DeviceTempePost(tempData);
                        }
                    }
                    #endregion

                    #region DeviceTempe3
                    //取得总数
                    totalCount = TempeDeviceMsitedata_3Repository
                       .GetDatas<TempeDeviceMsitedata_3>(t => t.SamplingDate >= beginTime && t.SamplingDate < endTime, true).Count();

                    pageNum = (totalCount + pushCount - 1) / pushCount; //(int)Math.Floor(Convert.ToDouble((totalCount1 / pushCount))) ;              
                    for (int i = 0; i < pageNum; i++)
                    {
                        List<TempData> deviceTempeList = TempeDeviceMsitedata_3Repository
                       .GetDatas<TempeDeviceMsitedata_3>(t => t.SamplingDate >= beginTime && t.SamplingDate < endTime, true).OrderBy(t => t.SamplingDate)
                       .Skip(i * pushCount).Take(pushCount)
                       .Select(t => new TempData { MSiteID = t.MsiteID, CollectTime = t.SamplingDate, MSData = t.MsDataValue }).ToList();

                        foreach (var tempData in deviceTempeList)
                        {
                            DeviceTempePost(tempData);
                        }
                    }
                    #endregion

                    #region DeviceTempe4
                    //取得总数
                    totalCount = TempeDeviceMsitedata_4Repository
                       .GetDatas<TempeDeviceMsitedata_4>(t => t.SamplingDate >= beginTime && t.SamplingDate < endTime, true).Count();

                    pageNum = (totalCount + pushCount - 1) / pushCount; //(int)Math.Floor(Convert.ToDouble((totalCount1 / pushCount))) ;              
                    for (int i = 0; i < pageNum; i++)
                    {
                        List<TempData> deviceTempeList = TempeDeviceMsitedata_3Repository
                       .GetDatas<TempeDeviceMsitedata_4>(t => t.SamplingDate >= beginTime && t.SamplingDate < endTime, true).OrderBy(t => t.SamplingDate)
                       .Skip(i * pushCount).Take(pushCount)
                       .Select(t => new TempData { MSiteID = t.MsiteID, CollectTime = t.SamplingDate, MSData = t.MsDataValue }).ToList();

                        foreach (var tempData in deviceTempeList)
                        {
                            DeviceTempePost(tempData);
                        }
                    }
                    #endregion


                    #endregion
                }
                if (this.cbVoltage.Checked)
                {
                    #region 电池电压

                    #region 电池电压表 1

                    totalCount = VoltageWSMSiteData_1Repository
                        .GetDatas<VoltageWSMSiteData_1>(t => t.SamplingDate > beginTime && t.SamplingDate <= endTime, true).Count();
                    pageNum = (totalCount + pushCount - 1) / pushCount;

                    for (int i = 0; i < pageNum; i++)
                    {
                        List<VoltageData> voltageList1 = VoltageWSMSiteData_1Repository
                        .GetDatas<VoltageWSMSiteData_1>(t => t.SamplingDate > beginTime && t.SamplingDate <= endTime, true).Skip(i * pushCount).Take(pushCount)
                        .Select(t => new VoltageData { CollectTime = t.SamplingDate, MSData = t.MsDataValue, MSiteID = t.MsiteID }).ToList();

                        foreach (var voltage in voltageList1)
                        {
                            VoltagePost(voltage);
                        }
                    }
                    #endregion

                    #region 电池电压表 2

                    totalCount = VoltageWSMSiteData_2Repository
                        .GetDatas<VoltageWSMSiteData_2>(t => t.SamplingDate > beginTime && t.SamplingDate <= endTime, true).Count();
                    pageNum = (totalCount + pushCount - 1) / pushCount;

                    for (int i = 0; i < pageNum; i++)
                    {
                        List<VoltageData> voltageList2 = VoltageWSMSiteData_2Repository
                        .GetDatas<VoltageWSMSiteData_2>(t => t.SamplingDate > beginTime && t.SamplingDate <= endTime, true).Skip(i * pushCount).Take(pushCount)
                        .Select(t => new VoltageData { CollectTime = t.SamplingDate, MSData = t.MsDataValue, MSiteID = t.MsiteID }).ToList();

                        foreach (var voltage in voltageList2)
                        {
                            VoltagePost(voltage);
                        }
                    }
                    #endregion

                    #region 电池电压表 3

                    totalCount = VoltageWSMSiteData_3Repository
                        .GetDatas<VoltageWSMSiteData_3>(t => t.SamplingDate > beginTime && t.SamplingDate <= endTime, true).Count();
                    pageNum = (totalCount + pushCount - 1) / pushCount;

                    for (int i = 0; i < pageNum; i++)
                    {
                        List<VoltageData> voltageList3 = VoltageWSMSiteData_3Repository
                        .GetDatas<VoltageWSMSiteData_3>(t => t.SamplingDate > beginTime && t.SamplingDate <= endTime, true).Skip(i * pushCount).Take(pushCount)
                        .Select(t => new VoltageData { CollectTime = t.SamplingDate, MSData = t.MsDataValue, MSiteID = t.MsiteID }).ToList();

                        foreach (var voltage in voltageList3)
                        {
                            VoltagePost(voltage);
                        }
                    }
                    #endregion

                    #region 电池电压表 4

                    totalCount = VoltageWSMSiteData_4Repository
                        .GetDatas<VoltageWSMSiteData_4>(t => t.SamplingDate > beginTime && t.SamplingDate <= endTime, true).Count();
                    pageNum = (totalCount + pushCount - 1) / pushCount;

                    for (int i = 0; i < pageNum; i++)
                    {
                        List<VoltageData> voltageList4 = VoltageWSMSiteData_4Repository
                        .GetDatas<VoltageWSMSiteData_4>(t => t.SamplingDate > beginTime && t.SamplingDate <= endTime, true).Skip(i * pushCount).Take(pushCount)
                        .Select(t => new VoltageData { CollectTime = t.SamplingDate, MSData = t.MsDataValue, MSiteID = t.MsiteID }).ToList();

                        foreach (var voltage in voltageList4)
                        {
                            VoltagePost(voltage);
                        }
                    }
                    #endregion

                    #endregion
                }
                if (this.cbVibration.Checked)
                {
                    #region 加速度
                    totalCount = VibratingSingalCharHisAccRepository
                         .GetDatas<VibratingSingalCharHisAcc>(t => t.SamplingDate >= beginTime && t.SamplingDate <= endTime, true).Count();
                    pageNum = (totalCount + pushCount - 1) / pushCount;

                    for (int i = 0; i < pageNum; i++)
                    {
                        var accDataList = VibratingSingalCharHisAccRepository
                           .GetDatas<VibratingSingalCharHisAcc>(t => t.SamplingDate >= beginTime && t.SamplingDate <= endTime, true).Skip(i * pushCount).Take(pushCount).ToList();

                        foreach (var accData in accDataList)
                        {
                            if (!string.IsNullOrEmpty(accData.WaveDataPath))
                            {
                                //波形，推送波形与特征值
                                //获取波形数据
                                var waveData = getData.GetWavePerformanceForAcc(accData);
                                if (waveData != null)
                                {
                                    WavePost(waveData);
                                }
                                //获取特征值数据
                                var perg = getData.GetSignalPerformanceForAcc(accData);

                                if (perg != null)
                                {
                                    EiganValuePost(perg);
                                }
                            }
                            else
                            {
                                //获取特征值数据
                                var perg = getData.GetSignalPerformanceForAcc(accData);

                                if (perg != null)
                                {
                                    EiganValuePost(perg);
                                }
                            }
                        }
                    }
                    #endregion

                    #region 速度

                    totalCount = VibratingSingalCharHisVelRepository
                      .GetDatas<VibratingSingalCharHisVel>(t => t.SamplingDate >= beginTime && t.SamplingDate <= endTime, true).Count();
                    pageNum = (totalCount + pushCount - 1) / pushCount;

                    for (int i = 0; i < pageNum; i++)
                    {
                        var velDataList = VibratingSingalCharHisVelRepository
                          .GetDatas<VibratingSingalCharHisVel>(t => t.SamplingDate >= beginTime && t.SamplingDate <= endTime, true).Skip(i * pushCount).Take(pushCount).ToList();
                        foreach (var velData in velDataList)
                        {
                            if (!string.IsNullOrEmpty(velData.WaveDataPath))
                            {
                                //获取波形数据
                                var waveData = getData.GetWavePerformanceForVel(velData);
                                if (waveData != null)
                                {
                                    WavePost(waveData);
                                }
                                //获取特征值数据
                                var perg = getData.GetSignalPerformanceForVel(velData);
                                if (perg != null)
                                {
                                    EiganValuePost(perg);
                                }
                            }
                            else
                            {
                                //获取特征值数据
                                var perg = getData.GetSignalPerformanceForVel(velData);

                                if (perg != null)
                                {
                                    EiganValuePost(perg);
                                }
                            }
                        }
                    }
                    #endregion

                    #region 包络

                    totalCount = VibratingSingalCharHisEnvlRepository
                      .GetDatas<VibratingSingalCharHisEnvl>(t => t.SamplingDate >= beginTime && t.SamplingDate <= endTime, true).Count();
                    pageNum = (totalCount + pushCount - 1) / pushCount;

                    for (int i = 0; i < pageNum; i++)
                    {
                        List<VibratingSingalCharHisEnvl> envlDataList = VibratingSingalCharHisEnvlRepository
                         .GetDatas<VibratingSingalCharHisEnvl>(t => t.SamplingDate >= beginTime && t.SamplingDate <= endTime, true).Skip(i * pushCount).Take(pushCount).ToList();

                        foreach (var envlData in envlDataList)
                        {
                            if (!string.IsNullOrEmpty(envlData.WaveDataPath))
                            {
                                //波形，推送波形与特征值

                                //获取波形数据
                                var waveData = getData.GetWavePerformanceForEnvl(envlData);
                                if (waveData != null)
                                {
                                    WavePost(waveData);
                                }
                                //获取特征值数据
                                var perg = getData.GetSignalPerformanceForEnvl(envlData);

                                if (perg != null)
                                {
                                    EiganValuePost(perg);
                                }
                            }
                            else
                            {
                                //获取特征值数据
                                var perg = getData.GetSignalPerformanceForEnvl(envlData);
                                if (perg != null)
                                {
                                    EiganValuePost(perg);
                                }
                            }
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                iCMS.Common.Component.Tool.LogHelper.WriteLog(ex);
                throw;
            }

            this.BeginInvoke(new MethodInvoker(delegate()
            {
                btnPush.Enabled = true;
            }));
        }
        #endregion

        #region 推送温度
        private void DeviceTempePost(TempData tempData)
        {
            MeasureSitePerformance perg = getData.GetMeasuresitePerformance(tempData);
            if (perg != null)
            {
                List<MeasureSitePerformance> lt = new List<MeasureSitePerformance>();
                lt.Add(perg);
                string result = iCMS.Common.Component.Tool.Extensions.Json.Stringify(lt);

                string response = CommunicationHelper.SendData(result, CommonVariate.Cloud_URL_MeasuresitePerfs);
            }
        }
        #endregion

        #region 推送电池电压
        private void VoltagePost(VoltageData voltage)
        {
            MeasureSite site = MeasureSiteRepository.GetByKey(voltage.MSiteID);
            if (site != null)
            {
                GetYunyiInterfaceData getData = new GetYunyiInterfaceData();
                SensorPerformance perg = getData.GetSensorPerformance(voltage, site);
                if (perg != null)
                {
                    List<SensorPerformance> lt = new List<SensorPerformance>();
                    lt.Add(perg);
                    string result = iCMS.Common.Component.Tool.Extensions.Json.Stringify(lt);
                    string response = CommunicationHelper.SendData(result, CommonVariate.Cloud_URL_Sensorperfs);

                }
            }
        }
        #endregion

        #region 推送波形
        private void WavePost(WaveData waveData)
        {
            List<WaveData> wv = new List<WaveData>();
            wv.Add(waveData);
            string content = iCMS.Common.Component.Tool.Extensions.Json.Stringify(wv);

            string response = CommunicationHelper.SendWaveData(content, CommonVariate.Cloud_URL_WaveBinary);
        }
        #endregion

        #region 推送特征值
        private void EiganValuePost(SignalPerformance eiganValue)
        {
            List<SignalPerformance> eig = new List<SignalPerformance>();
            eig.Add(eiganValue);
            string content = iCMS.Common.Component.Tool.Extensions.Json.Stringify(eig);

            string response = CommunicationHelper.SendData(content, CommonVariate.Cloud_URL_SignalPerfs);
        }
        #endregion

        #region 数据库访问类初始化
        private void InitDAO()
        {
            TempeDeviceMsitedata_1Repository = new Repository<TempeDeviceMsitedata_1>();
            TempeDeviceMsitedata_2Repository = new Repository<TempeDeviceMsitedata_2>();
            TempeDeviceMsitedata_3Repository = new Repository<TempeDeviceMsitedata_3>();
            TempeDeviceMsitedata_4Repository = new Repository<TempeDeviceMsitedata_4>();
            VoltageWSMSiteData_1Repository = new Repository<VoltageWSMSiteData_1>();
            VoltageWSMSiteData_2Repository = new Repository<VoltageWSMSiteData_2>();
            VoltageWSMSiteData_3Repository = new Repository<VoltageWSMSiteData_3>();
            VoltageWSMSiteData_4Repository = new Repository<VoltageWSMSiteData_4>();
            VibratingSingalCharHisAccRepository = new Repository<VibratingSingalCharHisAcc>();
            VibratingSingalCharHisVelRepository = new Repository<VibratingSingalCharHisVel>(); ;
            VibratingSingalCharHisEnvlRepository = new Repository<VibratingSingalCharHisEnvl>(); ;

            MeasureSiteRepository = new Repository<MeasureSite>();
        }
        #endregion
    }
}

