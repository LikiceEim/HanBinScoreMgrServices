using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Frameworks.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Service.Web.DAUService.DAUAlarmParameter;

namespace iCMS.Service.Web.DAUService.DAUAlarmManager
{
    public class DeviceVibrationAlarm : IDeviceVibrationAlarm
    {
        private IRepository<Gateway> wgRepository = null;
        private IRepository<Device> deviceRepository = null;
        private IRepository<MonitorTree> monitorTreeRepository = null;
        IRepository<DevAlmRecord> devAlmRecordRepository = null;
        private readonly ICacheDICT cacheDICT;
        public DeviceVibrationAlarm(IRepository<Gateway> wgRepository,
            IRepository<Device> deviceRepository,
            IRepository<MonitorTree> monitorTreeRepository,
            IRepository<DevAlmRecord> devAlmRecordRepository,
            ICacheDICT cacheDICT)
        {
            this.wgRepository = wgRepository;
            this.deviceRepository = deviceRepository;
            this.monitorTreeRepository = monitorTreeRepository;
            this.devAlmRecordRepository = devAlmRecordRepository;
            this.cacheDICT = cacheDICT;
        }

        #region 设备振动报警管理

        #region 设备振动报警管理
        /// <summary>
        /// 设备振动报警管理
        /// </summary>
        /// <param name="param"></param>
        public void DevVibAlmRecordManager(DeviceVibtationAlarmParameter param)
        {
            try
            {
                if (param.HisDataValue == null)
                    return;

                DevAlmRecord oRecord = null;
                DevAlmRecord nRecord = null;

                //是否使用最后一次更新时间
                List<DevAlmRecord> almList = devAlmRecordRepository
                    .GetDatas<DevAlmRecord>(obj => obj.SingalAlmID == param.AlmSet.SingalAlmID
                        && obj.MSAlmID == (int)EnumAlarmRecordType.DeviceVibration
                        && obj.BDate == obj.EDate, false)
                    .ToList();

                //如果有数据，获取第一条
                if (almList != null && almList.Count > 0)
                {
                    oRecord = almList.FirstOrDefault();

                    //不为空时，操作  王颖辉 2016-10-12
                    if (oRecord != null)
                    {
                        //处理有数据情况
                        #region 有数据处理
                        if (param.AlmSet.Status == (int)EnumAlarmStatus.Normal)
                        {
                            //报警结束
                            oRecord.EDate = param.SamplingTime;

                            //更新最后一次时间 王颖辉 2016-08-12 
                            oRecord.LatestStartTime = param.SamplingTime;
                        }
                        else
                        {
                            if (param.AlmSet.Status == oRecord.AlmStatus)
                            {
                                //报警持续
                                oRecord.SamplingValue = param.HisDataValue;
                                oRecord.WarningValue = param.AlmSet.WarnValue;
                                oRecord.DangerValue = param.AlmSet.AlmValue;
                                oRecord.LatestStartTime = param.SamplingTime;
                            }
                            else
                            {
                                //原报警结束
                                oRecord.EDate = param.SamplingTime;

                                //更新最后一次时间 王颖辉 2016-08-12 
                                oRecord.LatestStartTime = param.SamplingTime;

                                //新增报警
                                nRecord = CreatNewVibrationAlmRecord(param.Dev, param.MSite, param.Signal,
                                    param.AlmSet, param.SamplingTime, param.HisDataValue);
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    //报警则添加数据
                    if (param.AlmSet.Status != (int)EnumAlarmStatus.Normal)
                    {
                        //新增报警
                        nRecord = CreatNewVibrationAlmRecord(param.Dev, param.MSite, param.Signal,
                                    param.AlmSet, param.SamplingTime, param.HisDataValue);
                    }
                }

                #region 更新数据,如果为空，则说表状态正常 王颖辉 2016-08-12
                //更新数据,如果为空，则说表状态正常 王颖辉 2016-08-12
                if (oRecord != null)
                {
                    //更新原有报警
                    devAlmRecordRepository.Update(oRecord);
                }
                #endregion

                if (nRecord != null)
                {
                    //添加新的报警
                    devAlmRecordRepository.AddNew(nRecord);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 创建新的报警记录对象
        /// <summary>
        /// 创建新的报警记录对象
        /// </summary>
        /// <param name="dev">设备信息</param>
        /// <param name="msite">测量位置信息</param>
        /// <param name="signal">振动信号信息</param>
        /// <param name="signalAlmSet">振动信号报警设置信息</param>
        /// <param name="samplingData">采集时间</param>
        /// <param name="value">采集数据值</param>
        /// <returns></returns>
        private DevAlmRecord CreatNewVibrationAlmRecord(Device dev, MeasureSite msite, VibSingal signal,
                    SignalAlmSet signalAlmSet, DateTime samplingTime, float? value)
        {

            try
            {
                DevAlmRecord nRecord = new DevAlmRecord();
                //GetMonitorTree getMonitorTree = new GetMonitorTree(wgRepository, deviceRepository, monitorTreeRepository);
                nRecord.DevID = dev.DevID;
                nRecord.DevName = dev.DevName;
                nRecord.DevNO = dev.DevNO.ToString();
                nRecord.MSiteID = msite.MSiteID;

                string measureSiteName = string.Empty;
                var measureSiteType = cacheDICT.GetInstance().GetCacheType<MeasureSiteType>(p => p.ID == msite.MSiteName).FirstOrDefault();
                //空判断 王颖辉  2016-08-30
                if (measureSiteType != null)
                {
                    measureSiteName = measureSiteType.Name;
                }

                nRecord.MSiteName = measureSiteName;
                nRecord.SingalID = signal.SingalID;

                string singalName = string.Empty;
                var vibratingSingalType = cacheDICT.GetInstance().GetCacheType<VibratingSingalType>(p => p.ID == signal.SingalType).FirstOrDefault();
                if (vibratingSingalType != null)
                {
                    singalName = vibratingSingalType.Name;
                }
                nRecord.SingalName = singalName;
                nRecord.SingalAlmID = signalAlmSet.SingalAlmID;
                string eigenValueName = string.Empty;
                var eigenValueType = cacheDICT.GetInstance().GetCacheType<EigenValueType>(p => p.ID == signalAlmSet.ValueType).FirstOrDefault();
                if (eigenValueType != null)
                {
                    eigenValueName = eigenValueType.Name;
                }
                nRecord.SingalValue = eigenValueName;
                nRecord.MSAlmID = (int)EnumAlarmRecordType.DeviceVibration;
                nRecord.AlmStatus = signalAlmSet.Status;
                nRecord.SamplingValue = value;
                nRecord.WarningValue = signalAlmSet.WarnValue;
                nRecord.DangerValue = signalAlmSet.AlmValue;
                nRecord.BDate = samplingTime;
                nRecord.EDate = samplingTime;
                nRecord.LatestStartTime = samplingTime;
                nRecord.MonitorTreeID = string.Empty;//getMonitorTree.ConvertMonitorTreeIDString(dev.DevID, null);
                nRecord.Content = string.Format("{0}{1}{2}{3}{4}", nRecord.DevName, nRecord.MSiteName,
                    nRecord.SingalName, nRecord.SingalValue,
                    ConvertStatusToString(signalAlmSet.Status));

                return nRecord;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }
        #endregion

        #endregion

        #region 取得状态
        /// <summary>
        /// 取得状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string ConvertStatusToString(int status)
        {
            string result = string.Empty;

            switch (status)
            {
                case (int)EnumAlarmStatus.Normal:
                    result = "正常";
                    break;
                case (int)EnumAlarmStatus.Warning:
                    result = "高报";
                    break;
                case (int)EnumAlarmStatus.Danger:
                    result = "高高报";
                    break;
                default:
                    break;
            }
            return result;
        }
        #endregion
    }
}