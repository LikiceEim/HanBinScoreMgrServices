using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Frameworks.Core.Repository;
using iCMS.Service.Web.DAUService.DAUAlarmParameter;

namespace iCMS.Service.Web.DAUService.DAUAlarmManager
{
    #region 设备温度报警
    /// <summary>
    /// 设备温度报警
    /// </summary>
    public class DeviceTemperatureAlarm : IDeviceTemperatureAlarm
    {
        private IRepository<Gateway> wgRepository = null;
        private IRepository<Device> deviceRepository = null;
        private IRepository<MonitorTree> monitorTreeRepository = null;
        private IRepository<DevAlmRecord> devAlmRecordRepository = null;
        private readonly ICacheDICT cacheDICT;
        public DeviceTemperatureAlarm(IRepository<Gateway> wgRepository,
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

        #region 设备温度报警管理

        #region 设备温度报警管理
        /// <summary>
        /// 设备温度报警管理
        /// </summary>
        /// <param name="param"></param>
        public void DevTemperatureAlmRecordManager(DeviceTemperatureAlarmParameter param)
        {
            try
            {
                if (param.HisDataValue == null)
                    return;
                DevAlmRecord oRecord = null;
                DevAlmRecord nRecord = null;
                List<DevAlmRecord> almList = devAlmRecordRepository
                    .GetDatas<DevAlmRecord>(obj => obj.MSiteID == param.MSite.MSiteID
                        && obj.MSAlmID == (int)EnumAlarmRecordType.DeviceTemperature
                        && obj.BDate == obj.EDate, false)
                    .ToList();
                if (almList != null && almList.Count > 0)
                {
                    oRecord = almList.FirstOrDefault();
                }

                if (oRecord == null)
                {
                    if (param.MSiteAlmSet.Status != (int)EnumAlarmStatus.Normal)
                    {
                        //新增报警
                        nRecord = CreatNewTempAlmRecord(param.Dev, param.MSite, param.MSiteAlmSet, param.SamplingTime, (float)param.HisDataValue);
                    }
                }
                else
                {
                    if (param.MSiteAlmSet.Status == (int)EnumAlarmStatus.Normal)
                    {
                        //报警结束
                        oRecord.EDate = param.SamplingTime;
                    }
                    else
                    {
                        if (param.MSiteAlmSet.Status == oRecord.AlmStatus)
                        {
                            //报警持续
                            oRecord.SamplingValue = param.HisDataValue;
                            oRecord.WarningValue = param.MSiteAlmSet.WarnValue;
                            oRecord.DangerValue = param.MSiteAlmSet.AlmValue;
                            oRecord.LatestStartTime = param.SamplingTime;
                        }
                        else
                        {
                            //原报警结束
                            oRecord.EDate = param.SamplingTime;
                            //新增报警
                            nRecord = CreatNewTempAlmRecord(param.Dev, param.MSite, param.MSiteAlmSet, param.SamplingTime, (float)param.HisDataValue);
                        }
                    }
                }
                if (oRecord != null)
                {
                    //更新原有报警
                    devAlmRecordRepository.Update(oRecord);
                }
                if (nRecord != null)
                {
                    //添加新的报警
                    devAlmRecordRepository.AddNew(nRecord);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
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
        /// <param name="msiteAlmSite">设备温度告警设置</param>
        /// <param name="samplingTime">采集时间</param>
        /// <param name="value">采集值</param>
        /// <returns></returns>
        private DevAlmRecord CreatNewTempAlmRecord(Device dev, MeasureSite msite, TempeDeviceSetMSiteAlm msiteAlmSite, DateTime samplingTime, float value)
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
                nRecord.MSAlmID = (int)EnumAlarmRecordType.DeviceTemperature;
                nRecord.AlmStatus = msiteAlmSite.Status;
                nRecord.SamplingValue = value;
                nRecord.WarningValue = msiteAlmSite.WarnValue;
                nRecord.DangerValue = msiteAlmSite.AlmValue;
                nRecord.BDate = samplingTime;
                nRecord.EDate = samplingTime;
                nRecord.LatestStartTime = samplingTime;
                nRecord.MonitorTreeID = string.Empty; //getMonitorTree.ConvertMonitorTreeIDString(dev.DevID, null);
                nRecord.Content = string.Format("{0}{1}{2}{3}", nRecord.DevName, nRecord.MSiteName,
                    "设备温度", ConvertStatusToString(msiteAlmSite.Status));

                return nRecord;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }
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

        #endregion
    }
    #endregion
}