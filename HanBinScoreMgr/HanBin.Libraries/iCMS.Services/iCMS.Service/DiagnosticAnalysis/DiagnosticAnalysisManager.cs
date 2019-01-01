/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Service.DiagnosticAnalysis
 * 文件名：  DiagnosticAnalysisManager
 * 创建人：  王颖辉
 * 创建时间：2016-10-21
 * 描述：诊断分析业务处理类
 *
 * 修改人：张辽阔
 * 修改时间：2016-11-14
 * 描述：增加错误编码
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Text;

using Microsoft.Practices.Unity;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Response.Statistics;
using iCMS.Service.Common;
using iCMS.Common.Component.Data.Response.WirelessDevicesConfig;
using iCMS.Frameworks.Core.Repository;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree;
using iCMS.Common.Component.Data.Request.DevicesConfig;
using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Service.Web.DiagnosticAnalysis
{
    #region 诊断分析
    /// <summary>
    /// 诊断分析
    /// </summary>
    public class DiagnosticAnalysisManager : IDiagnosticAnalysisManager
    {
        #region 变量
        private readonly IRepository<Device> deviceRepository;
        private readonly IRepository<DeviceType> deviceTypeRepository;
        private readonly IRepository<MeasureSite> measureSiteRepository;
        private readonly IRepository<Config> configRepository;

        private readonly IRepository<WS> wsRepository;
        private readonly IRepository<Gateway> gatewayRepository;

        private readonly IRepository<RealTimeCollectInfo> realTimeCollectInfoRepository;
        private readonly ICacheDICT cacheDICT;

        [Dependency]
        public IRepository<UserRelationWS> userRelationWSRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<UserRalationDevice> userRalationDeviceRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<WirelessGatewayType> wirelessGatewayTypeRepository
        {
            get;
            set;
        }


        [Dependency]
        public IRepository<WSnAlmRecord> wsnAlmRecordRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<MonitorTree> monitorTreeRepository
        {
            get;
            set;
        }


        [Dependency]
        public IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository
        {
            get;
            set;
        }


        [Dependency]
        public IRepository<TempeWSSetMSiteAlm> tempeWSSetMSiteAlmRepository
        {
            get;
            set;
        }


        [Dependency]
        public IRepository<VoltageSetMSiteAlm> voltageSetMSiteAlmRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<SignalAlmSet> signalAlmSetRepository
        {
            get;
            set;
        }


        [Dependency]
        public IRepository<VibSingal> vibSingalRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<RealTimeAlarmThreshold> realTimeAlarmThresholdRepository
        {
            get;
            set;
        }
        #endregion

        #region 构造函数
        public DiagnosticAnalysisManager(IRepository<Device> deviceRepository,
           IRepository<DeviceType> deviceTypeRepository,
           IRepository<MeasureSite> measureSiteRepository,
           IRepository<Config> configRepository,
           IRepository<WS> wsRepository,
           IRepository<Gateway> gatewayRepository,
           IRepository<RealTimeCollectInfo> realTimeCollectInfoRepository,
           ICacheDICT cacheDICT)
        {
            this.deviceRepository = deviceRepository;
            this.deviceTypeRepository = deviceTypeRepository;
            this.measureSiteRepository = measureSiteRepository;
            this.configRepository = configRepository;
            this.wsRepository = wsRepository;
            this.gatewayRepository = gatewayRepository;
            this.realTimeCollectInfoRepository = realTimeCollectInfoRepository;
            this.cacheDICT = cacheDICT;
        }
        #endregion

        #region 接口方法

        #region 形貌图展示

        /// <summary>
        /// 形貌图展示
        /// 
        /// 修改人：张辽阔
        /// 修改时间：2016-11-08
        /// 修改记录：返回Result值
        /// </summary>
        /// <returns></returns>
        public BaseResponse<DevImgDataResult> GetDevImgData(DevImgDataParameter parameter)
        {
            BaseResponse<DevImgDataResult> response = new BaseResponse<DevImgDataResult>();
            DevImgDataResult result = new DevImgDataResult();
            response.Result = result;
            try
            {
                if (parameter.DevID == 0)
                {
                    response.Code = "002392";
                    response.IsSuccessful = false;
                    return response;
                }
                //获取设备信息
                Device dev = deviceRepository
                    .GetByKey(Convert.ToInt32(parameter.DevID));
                if (dev == null)
                {
                    response.Code = "002402";
                    response.IsSuccessful = false;
                    return response;
                }
                result.DevID = dev.DevID.ToString();
                result.DevName = dev.DevName;
                result.DevRoatate = dev.Rotate.ToString();
                result.DevStatus = dev.AlmStatus.ToString();
                result.DevRunningStatus = dev.RunStatus.ToString();
                result.UseType = dev.UseType;
                if (dev.OperationDate.HasValue)
                {
                    result.OperationDate = dev.OperationDate.ToString();
                }

                int paramIntDev = Convert.ToInt32(parameter.DevID);
                result.DeviceTypeID = dev.DevType.ToString();
                var deviceTypeInDb = deviceTypeRepository
                    .GetByKey(dev.DevType);
                if (deviceTypeInDb != null)
                {
                    result.DeviceTypeName = deviceTypeInDb.Name;
                }

                #region 过滤用户管理的传感器
                // var wsIDListByUser = userRelationWSRepository.GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, true).Select(item => item.WSID).ToList();
                #endregion

                List<MeasureSite> measureSiteList = measureSiteRepository
                    .GetDatas<MeasureSite>(p => p.DevID == paramIntDev, false)
                    .ToList();
                if (measureSiteList == null || measureSiteList.Count == 0)
                {
                    List<MSStatusInfo> infos = new List<MSStatusInfo>();
                    result.MSStatusInfos = infos;

                    return response;
                }

                List<MSStatusInfo> msStatusInfoList = new List<MSStatusInfo>();

                if (parameter.UserID == -1)
                {
                    parameter.UserID = 1011;
                }

                if (measureSiteList != null)
                {

                    var measureSiteMSIDList = measureSiteList.Select(item => item.MSiteID).ToList();
                    MSStatusInfo msStatusInfo = null;


                    //获取实时数据信息 ,添加测点时已经添加到时实时数据表中,所以删除 王颖辉 2017-10-30
                    List<RealTimeCollectInfo> realTimeInfoList = realTimeCollectInfoRepository
                        .GetDatas<RealTimeCollectInfo>(p => p.DevID == paramIntDev && measureSiteMSIDList.Contains(p.MSID.Value), false)
                        .ToList();


                    #region 振动报警阈值
                    var vibsignalList = vibSingalRepository.GetDatas<VibSingal>(item => measureSiteMSIDList.Contains(item.MSiteID), true).ToList();

                    var vibisignalAlarmList = new List<SignalAlmSet>();
                    if (vibsignalList != null && vibsignalList.Any())
                    {
                        var vibsignalIDList = vibsignalList.Select(item => item.SingalID).ToList();
                        vibisignalAlarmList = signalAlmSetRepository.GetDatas<SignalAlmSet>(item => vibsignalIDList.Contains(item.SingalID), true).ToList();
                    }

                    #endregion

                    #region 设备温度阈值
                    var deivceTemperatureAlarmList = tempeDeviceSetMSiteAlmRepository.GetDatas<TempeDeviceSetMSiteAlm>(item => measureSiteMSIDList.Contains(item.MsiteID), true).ToList();
                    #endregion

                    #region 振动信号和特征值
                    var vibratingSingalTypeList = cacheDICT.GetInstance()
                                                       .GetCacheType<VibratingSingalType>(p => true).ToList();
                    var eigenValueTypeList = cacheDICT.GetInstance()
                                                     .GetCacheType<EigenValueType>(p => true).ToList();
                    List<MeasureSiteType> measureSiteTypeList = cacheDICT.GetInstance()
                                                             .GetCacheType<MeasureSiteType>()
                                                             .ToList();
                    #endregion

                    #region 设置阈值
                    foreach (RealTimeCollectInfo realTimeInfo in realTimeInfoList)
                    {
                        msStatusInfo = GetMSStatusInfo(realTimeInfo);

                        if (measureSiteTypeList != null && measureSiteTypeList.Any())
                        {
                            var measureSite = measureSiteList.Where(item => item.MSiteID == msStatusInfo.MSiteID).FirstOrDefault();
                            if (measureSite != null)
                            {
                                var measureSiteType = measureSiteTypeList.Where(item => item.ID == measureSite.MSiteName).FirstOrDefault();

                                if (measureSiteType != null)
                                {
                                    msStatusInfo.MeasureSiteTypeCode = measureSiteType.Code;
                                    msStatusInfo.MSiteName = measureSiteType.Name;
                                }
                            }
                        }

                        var vibsignalInfoList = vibsignalList.Where(item => item.MSiteID == msStatusInfo.MSiteID).ToList();

                        #region 振动阈值
                        if (vibsignalInfoList != null && vibsignalInfoList.Any())
                        {
                            //查找振动
                            foreach (var vibsignal in vibsignalInfoList)
                            {
                                //设备为运行状态时，返回实时数据，其它状态不返回数据
                                if (dev.RunStatus == (int)EnumRunStatus.RunNormal)
                                {
                                    #region 获取特征值
                                    SetEigenValueForImg(vibisignalAlarmList, vibsignal, eigenValueTypeList, msStatusInfo);
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        #region 设备温度报警阈值
                        //设备为运行状态时，返回实时数据，其它状态不返回数据
                        if (dev.RunStatus == (int)EnumRunStatus.RunNormal)
                        {
                            var deviceTemperatureAlarm = realTimeAlarmThresholdRepository.GetDatas<RealTimeAlarmThreshold>(item =>
                                item.MeasureSiteID == msStatusInfo.MSiteID &&
                                item.MeasureSiteThresholdType == (int)EnumMeasureSiteThresholdType.DeviceTemperature, true).FirstOrDefault();
                            if (deviceTemperatureAlarm != null)
                            {
                                msStatusInfo.MSDevTemperatureAlarmValue = deviceTemperatureAlarm.AlarmThresholdValue;
                                msStatusInfo.MSDevTemperatureDangerValue = deviceTemperatureAlarm.DangerThresholdValue;
                            }
                        }
                        #endregion

                        msStatusInfoList.Add(msStatusInfo);
                    }
                    #endregion
                }

                result.MSStatusInfos = msStatusInfoList;
                response.Result = result;

                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002411";
                response.Result = null;
                return response;
            }
        }

        #endregion

        #region 通过设备Id,获取形貌图配置下的所有位置信息

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-09-03
        /// 通过设备Id,获取形貌图配置下的所有位置信息
        /// 
        /// 修改人：张辽阔
        /// 修改时间：2016-11-08
        /// 修改记录：增加是否有子节点的逻辑
        /// </summary>
        /// <param name="deviceId">设备id</param>
        /// <returns></returns>
        public BaseResponse<ConfigListByDeviceIDResult> GetConfigListByDeviceID(ConfigListByDeviceIDParameter parameter)
        {
            int deviceId = parameter.DeviceID;

            #region 初始化
            BaseResponse<ConfigListByDeviceIDResult> response = new BaseResponse<ConfigListByDeviceIDResult>();
            ConfigListByDeviceIDResult result = new ConfigListByDeviceIDResult();
            #endregion
            try
            {
                #region 业务处理

                Device device = deviceRepository
                    .GetDatas<Device>(item => item.DevID == deviceId, false)
                    .FirstOrDefault();
                //设备是否存在
                if (device != null)
                {
                    DeviceType deviceType = deviceTypeRepository
                        .GetDatas<DeviceType>(item => item.ID == device.DevType, false)
                        .FirstOrDefault();
                    //设备类型是否存在
                    if (deviceType != null)
                    {
                        string deviceTypeName = deviceType.Name;
                        //形貌图配置
                        string rootName = EnumHelper.GetDescription(EnumConfig.TopographicMapConfig);
                        Config rootConfig = configRepository
                            .GetDatas<Config>(item => item.Name == rootName
                                && item.ParentId == 0, false)
                            .FirstOrDefault();
                        //根节点是否存在
                        if (rootConfig != null)
                        {
                            //二级节点信息,泵
                            Config secondLevelConfig = configRepository
                                .GetDatas<Config>(item => item.Name == deviceTypeName
                                    && item.ParentId == rootConfig.ID, false)
                                .FirstOrDefault();
                            if (secondLevelConfig != null)
                            {
                                //获取设备下所有测点下，位置类型
                                var dbContext = new iCMSDbContext();
                                var query =
                                    from measureSite in dbContext.Measuresite
                                    join measureSiteType in dbContext.MeasureSiteType
                                        on measureSite.MSiteName equals measureSiteType.ID
                                    where measureSite.DevID == deviceId
                                    select new
                                    {
                                        measureSiteType.Describe,//描述，实际为id
                                    };

                                var describeList = query.Select(item => item.Describe).ToList<string>();

                                //获取二级节点下的所有子节点，如泵下的所有位置信息
                                //张辽阔 2016-11-08 注释
                                //List<Config> list = configRepository
                                //    .GetDatas<Config>(item => item.ParentId == secondLevelConfig.ID
                                //        && describeList.Contains(item.Describe), false)
                                //    .ToList<Config>();
                                //张辽阔 2016-11-08 添加
                                result.ConfigInfoList = configRepository
                                    .GetDatas<Config>(item => item.ParentId == secondLevelConfig.ID
                                        && describeList.Contains(item.Describe), false)
                                    .Select(p => new ConfigInfo
                                    {
                                        ID = p.ID,
                                        Name = p.Name,
                                        Describe = p.Describe,
                                        Value = p.Value,
                                        IsUsed = p.IsUsed,
                                        IsDefault = p.IsDefault,
                                        ParentId = p.ParentId,
                                        IsExistChild =
                                           (from IsExist in dbContext.Config
                                            where IsExist.ParentId == p.ID
                                            select IsExist)
                                            .Any(),
                                    })
                                    .ToList();
                            }
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002421";
            }
            finally
            {

            }
            response.Result = result;
            //返回值
            return response;
        }

        #endregion

        #region 左侧导航监测树

        public BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeDataForNavigation(GetMonitorTreeDataForNavigationParameter Parameter)
        {
            #region 初始化
            BaseResponse<MonitorTreeDataForNavigationResult> response = new BaseResponse<MonitorTreeDataForNavigationResult>();
            MonitorTreeDataForNavigationResult monitorTreeResult = new MonitorTreeDataForNavigationResult();
            //最小刷新时间
            string minFreshTime = "";

            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();
            List<MTStatusInfo> mTWSNStatusInfos = new List<MTStatusInfo>();

            #endregion

            try
            {
                #region 监测树
                if (Parameter.UserID.HasValue && (Parameter.UserID.Value == -1 || Parameter.UserID.Value == 1011))
                {
                    //监测设备
                    mTDevStatusInfos = GetMonitorTreeList();
                    //被监测设备 
                    mTWSNStatusInfos = GetServerTreeList();
                    //最小刷新时间
                    minFreshTime = GetMinFreshTime();
                }
                else if (Parameter.UserID.HasValue && Parameter.UserID.Value > 0)
                {
                    //监测设备
                    mTDevStatusInfos = GetMonitorTreeListByUserId(Parameter.UserID.Value);
                    //被监测设备 
                    mTWSNStatusInfos = GetServerTreeListByUserId(Parameter.UserID.Value);
                    //最小刷新时间
                    minFreshTime = GetMinFreshTime();
                }
                else
                {
                    throw new Exception("获取左侧导航监测树参数异常");
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<MonitorTreeDataForNavigationResult>("002431");
                return response;
            }

            //同步设备状态给监测树节点
            //var devList = (from mt in mTDevStatusInfos
            //               join temp in (from mt1 in mTDevStatusInfos
            //                             where mt1.MTType == "DEVICE"
            //                             group mt1 by mt1.MTPid into mtDevice
            //                             select new
            //                             {
            //                                 MonitorTreeID = mtDevice.Key,
            //                                 MaxAlmStatus = mtDevice.Max(m => m.MTStatus)
            //                             })
            //               on mt.MTStatus equals temp.MaxAlmStatus
            //               where mt.MTType == "DEVICE"
            //               select new
            //               {
            //                   MTPid = mt.MTPid,
            //                   MTStatus = mt.MTStatus
            //               }).Distinct().ToList();
            //foreach (var subMT in devList)
            //{
            //    FormatMTStatus(mTDevStatusInfos, subMT.MTPid, subMT.MTStatus);
            //}

            #region 标注设备状态 王颖辉 添加设备主备机
            var deviceList = deviceRepository.GetDatas<Device>(item => true, true).ToList();
            if (mTDevStatusInfos != null && mTDevStatusInfos.Any())
            {
                var deviceMTList = mTDevStatusInfos.Where(item => item.MTType == "DEVICE").ToList();
                foreach (var device in deviceMTList)
                {
                    if (deviceList != null && deviceList.Any())
                    {
                        int deviceID = 0;
                        int.TryParse(device.RecordID, out deviceID);
                        var deviceInfo = deviceList.Where(item => item.DevID == deviceID).FirstOrDefault();
                        if (deviceInfo != null)
                        {
                            device.Remark = deviceInfo.UseType.ToString();
                        }
                    }
                }
            }
            #endregion

            #region 处理备用加停机
            if (mTDevStatusInfos != null && mTDevStatusInfos.Any())
            {
                var deviceInfoList = mTDevStatusInfos.Where(item => item.MTType == "DEVICE").ToList();

                if (deviceInfoList != null && deviceInfoList.Any())
                {
                    var deviceIDList = deviceInfoList.Select(item => Convert.ToInt32(item.RecordID)).ToList();
                    var deviceDBList = deviceRepository.GetDatas<Device>(item => deviceIDList.Contains(item.DevID), true).ToList();

                    foreach (var info in deviceInfoList)
                    {
                        var device = deviceDBList.Where(item => item.DevID == Convert.ToInt32(info.RecordID)).FirstOrDefault();
                        if (device != null)
                        {
                            if (device.UseType == 1)
                            {
                                info.MTName = info.MTName.Replace("(stop)", "") + "(备用)";
                            }
                        }
                    }
                }
            }

            #endregion

            monitorTreeResult.MTDevStatusInfos = mTDevStatusInfos;
            monitorTreeResult.MTWSNStatusInfos = mTWSNStatusInfos;
            monitorTreeResult.MinFreshTime = minFreshTime;

            response = new BaseResponse<MonitorTreeDataForNavigationResult>();
            response.Result = monitorTreeResult;
            return response;
        }

        /// <summary>
        /// 同步设备状态给监测树节点
        /// </summary>
        private void FormatMTStatus(List<MTStatusInfo> mTDevStatusInfos, string MTPid, string MTStatus)
        {
            List<MTStatusInfo> tempMTList = mTDevStatusInfos.Where(w => w.MTId == MTPid && w.MTId != "0").ToList();
            foreach (MTStatusInfo tempMT in tempMTList)
            {
                tempMT.MTStatus = MTStatus;
                FormatMTStatus(mTDevStatusInfos, tempMT.MTPid, MTStatus);
            }
        }

        #endregion

        #region 获取无线传感器数据接口

        public BaseResponse<WSInfoResult> GetWSStatusData(GetWSStatusParameter param)
        {
            BaseResponse<WSInfoResult> response = null;
            WSInfoResult wsInfoResult = new WSInfoResult();

            #region 验证参数
            if (string.IsNullOrEmpty(param.Sort))
            {
                response = new BaseResponse<WSInfoResult>("002442");
                return response;
            }
            if (string.IsNullOrEmpty(param.Order))
            {
                response = new BaseResponse<WSInfoResult>("002452");
                return response;
            }
            #endregion

            #region 获取集合数据

            using (var dataContext = new iCMSDbContext())
            {
                if (param.Sort.Trim().ToLower().Equals("AddData".Trim().ToLower()))
                {
                    param.Sort = "AddDate";
                }

                IQueryable<WS> wsQuery = dataContext.WS;
                if (param.UseStatus.HasValue && param.UseStatus.Value != -1)
                {
                    wsQuery = wsQuery.Where(p => p.UseStatus == param.UseStatus.Value);
                }
                //查询网关信息
                if (param.Type == 1)
                {
                    //ID
                    if (param.ID != 0)
                    {
                        wsQuery = wsQuery.Where(item => item.WGID == param.ID);
                    }
                }
                //查询无线传感器
                if (param.Type == 2)
                {
                    //ID
                    if (param.ID != 0)
                    {
                        wsQuery = wsQuery.Where(item => item.WSID == param.ID);
                    }
                }
                //传感器编号
                int wsNo = 0;
                int.TryParse(param.WSNo, out wsNo);
                //传感器编号
                if (wsNo != 0)
                {
                    wsQuery = wsQuery.Where(item => item.WSNO == wsNo);
                }
                //连接状态
                if (param.LinkStatus.HasValue && param.LinkStatus.Value != -1)
                {
                    wsQuery = wsQuery.Where(item => item.LinkStatus == param.LinkStatus);
                }
                //报警状态
                if (param.AlmStatus.HasValue && param.AlmStatus.Value != -1)
                {
                    wsQuery = wsQuery.Where(item => item.AlmStatus == param.AlmStatus);
                }

                int count = 0;
                ListSortDirection sortOrder = param.Order.Trim().ToLower().Equals("asc")
                    ? ListSortDirection.Ascending
                    : ListSortDirection.Descending;
                PropertySortCondition[] sortList = new PropertySortCondition[]
                {
                    new PropertySortCondition(param.Sort, sortOrder),
                    new PropertySortCondition("WSID", sortOrder),
                };
                var wsInfoQuery = wsQuery
                    .Where(param.Page, param.PageSize, out count, sortList)
                    .ToArray()
                    .Select(info =>
                    {
                        //获取网关
                        var wgInfo =
                            (from wg in dataContext.WG
                             where wg.WGID == info.WGID
                             select wg)
                            .FirstOrDefault();
                        //位置信息
                        var measureSiteInfo =
                            (from msite in dataContext.Measuresite
                             where msite.WSID == info.WSID
                             select msite)
                            .FirstOrDefault();
                        int measureSiteId = 0;
                        int operationStatus = 0;
                        string operationStatusName = string.Empty;
                        if (measureSiteInfo != null)
                        {
                            measureSiteId = measureSiteInfo.MSiteID;
                            var opInfo =
                                (from o in dataContext.Operation
                                 where o.MSID == measureSiteId
                                 select o)
                                .FirstOrDefault();
                            //WS的操作记录
                            if (opInfo != null)
                            {
                                int.TryParse(opInfo.OperationResult, out operationStatus);
                                operationStatusName = opInfo.EDate == null ? "" : opInfo.EDate.ToString();
                            }
                        }
                        //传感器类型
                        var sensorType =
                            (from st in dataContext.SensorType
                             where st.ID == info.SensorType
                             select st)
                            .SingleOrDefault();
                        return new WSInfo
                        {

                            WSID = info.WSID,
                            WGID = info.WGID,
                            WGName = wgInfo == null ? "" : wgInfo.WGName,
                            WSNO = info.WSNO,
                            WSName = info.WSName,
                            BatteryVolatage = info.BatteryVolatage,
                            UseStatus = info.UseStatus,
                            AlmStatus = info.AlmStatus,
                            MACADDR = info.MACADDR,
                            SensorTypeId = info.SensorType,
                            LinkStatus = info.LinkStatus,
                            OperationStatus = operationStatus,
                            OperationStatusName = operationStatusName,
                            SensorTypeName = sensorType == null ? "" : sensorType.Name,
                            AddData = info.AddDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            Vendor = info.Vendor,
                            WSModel = info.WSModel,
                            SetupTime = info.SetupTime.HasValue ? info.SetupTime.Value.ToString("yyyy-MM-dd") : "",
                            SetupPersonInCharge = info.SetupPersonInCharge,
                            SNCode = info.SNCode,
                            FirmwareVersion = info.FirmwareVersion,
                            AntiExplosionSerialNo = info.AntiExplosionSerialNo,
                            RunStatus = info.RunStatus,
                            ImageID = info.ImageID,
                            PersonInCharge = info.PersonInCharge,
                            PersonInChargeTel = info.PersonInChargeTel,
                            Remark = info.Remark,
                        };
                    });

                wsInfoResult.WSInfo = wsInfoQuery.ToList<WSInfo>();
                wsInfoResult.Total = count;
                response = new BaseResponse<WSInfoResult>();
                response.Result = wsInfoResult;
                return response;
            }

            #endregion
        }

        #endregion

        #endregion

        #region 公共方法

        #region 获取形貌图定义对象model

        /// <summary>
        /// 获取形貌图定义对象model
        /// </summary>
        /// <param name="realTimeInfo">实时数据对象</param>
        /// <returns></returns>
        private MSStatusInfo GetMSStatusInfo(RealTimeCollectInfo realTimeInfo)
        {
            var siteInfo = measureSiteRepository.GetByKey(realTimeInfo.MSID);

            #region Modified
            int? linkStatus = null;
            var wsIndb = wsRepository.GetByKey(siteInfo.WSID);
            if (wsIndb != null)
            {
                linkStatus = wsIndb.LinkStatus;
            }

            #endregion

            //王颖辉修改,没有做空判断  2016-08-06
            MSStatusInfo msStatusInfo = new MSStatusInfo()
            {
                DevID = realTimeInfo.DevID,
                MSiteID = realTimeInfo.MSID,
                MSiteName = realTimeInfo.MSName,
                MSStatus = realTimeInfo.MSStatus,
                MSDesInfo = realTimeInfo.MSDesInfo,
                // MSiteDataStatus = realTimeInfo.MSDataStatus,
                MSSpeedSingalID = realTimeInfo.MSSpeedSingalID,
                MSSpeedUnit = realTimeInfo.MSSpeedUnit,
                MSSpeedVirtualValue = realTimeInfo.MSSpeedVirtualValue,
                MSSpeedPeakValue = realTimeInfo.MSSpeedPeakValue,
                MSSpeedPeakPeakValue = realTimeInfo.MSSpeedPeakPeakValue,
                MSSpeedVirtualStatus = realTimeInfo.MSSpeedVirtualStatus,
                MSSpeedPeakStatus = realTimeInfo.MSSpeedPeakStatus,
                MSSpeedPeakPeakStatus = realTimeInfo.MSSpeedPeakPeakStatus,
                MSSpeedVirtualTime = realTimeInfo.MSSpeedVirtualTime,
                MSSpeedPeakTime = realTimeInfo.MSSpeedPeakTime,
                MSSpeedPeakPeakTime = realTimeInfo.MSSpeedPeakPeakTime,
                MSACCSingalID = realTimeInfo.MSACCSingalID,
                MSACCUnit = realTimeInfo.MSACCUnit,
                MSACCVirtualValue = realTimeInfo.MSACCVirtualValue,
                MSACCPeakValue = realTimeInfo.MSACCPeakValue,
                MSACCPeakPeakValue = realTimeInfo.MSACCPeakPeakValue,
                MSACCVirtualStatus = realTimeInfo.MSACCVirtualStatus,
                MSACCPeakStatus = realTimeInfo.MSACCPeakStatus,
                MSACCPeakPeakStatus = realTimeInfo.MSACCPeakPeakStatus,
                MSACCVirtualTime = realTimeInfo.MSACCVirtualTime,
                MSACCPeakTime = realTimeInfo.MSACCPeakTime,
                MSACCPeakPeakTime = realTimeInfo.MSACCPeakPeakTime,
                MSDispSingalID = realTimeInfo.MSDispSingalID,
                MSDispUnit = realTimeInfo.MSDispUnit,
                MSDispVirtualValue = realTimeInfo.MSDispVirtualValue,
                MSDispPeakValue = realTimeInfo.MSDispPeakValue,
                MSDispPeakPeakValue = realTimeInfo.MSDispPeakPeakValue,
                MSDispVirtualStatus = realTimeInfo.MSDispVirtualStatus,
                MSDispPeakStatus = realTimeInfo.MSDispPeakStatus,
                MSDispPeakPeakStatus = realTimeInfo.MSDispPeakPeakStatus,
                MSDispVirtualTime = realTimeInfo.MSDispVirtualTime,
                MSDispPeakTime = realTimeInfo.MSDispPeakTime,
                MSDispPeakPeakTime = realTimeInfo.MSDispPeakPeakTime,
                MSEnvelSingalID = realTimeInfo.MSEnvelSingalID,
                MSEnvelopingPEAKValue = realTimeInfo.MSEnvelopingPEAKValue,
                MSEnvelopingCarpetValue = realTimeInfo.MSEnvelopingCarpetValue,
                MSEnvelopingUnit = realTimeInfo.MSEnvelopingUnit,
                MSEnvelopingPEAKStatus = realTimeInfo.MSEnvelopingPEAKStatus,
                MSEnvelopingCarpetStatus = realTimeInfo.MSEnvelopingCarpetStatus,
                MSEnvelopingPEAKTime = realTimeInfo.MSEnvelopingPEAKTime,
                MSEnvelopingCarpetTime = realTimeInfo.MSEnvelopingCarpetTime,
                MSLQSingalID = realTimeInfo.MSLQSingalID,
                MSLQValue = realTimeInfo.MSLQValue,
                MSLQStatus = realTimeInfo.MSLQStatus,
                MSLQUnit = realTimeInfo.MSLQUnit,
                MSLQTime = realTimeInfo.MSLQTime,
                MSDevTemperatureStatus = realTimeInfo.MSDevTemperatureStatus,
                MSDevTemperatureValue = realTimeInfo.MSDevTemperatureValue,
                MSDevTemperatureUnit = realTimeInfo.MSDevTemperatureUnit,
                MSDevTemperatureTime = realTimeInfo.MSDevTemperatureTime,
                MSWSTemperatureStatus = realTimeInfo.MSWSTemperatureStatus,
                MSWSTemperatureValue = realTimeInfo.MSWSTemperatureValue,
                MSWSTemperatureUnit = realTimeInfo.MSWSTemperatureUnit,
                MSWSTemperatureTime = realTimeInfo.MSWSTemperatureTime,
                MSWSBatteryVolatageValue = realTimeInfo.MSWSBatteryVolatageValue,
                MSWSBatteryVolatageUnit = realTimeInfo.MSWSBatteryVolatageUnit,
                MSWSBatteryVolatageStatus = realTimeInfo.MSWSBatteryVolatageStatus,
                MSWSBatteryVolatageTime = realTimeInfo.MSWSBatteryVolatageTime,
                MSWGLinkStatus = realTimeInfo.MSWGLinkStatus,
                MSWSLinkStatus = linkStatus,

                //解决方案融合， Added by QXM, 2018/05/18
                MSSpeedLPEValue = realTimeInfo.MSSpeedLPEValue,
                MSSpeedLPEStatus = realTimeInfo.MSSpeedLPEStatus,
                MSSpeedLPETime = realTimeInfo.MSSpeedLPETime,

                MSSpeedMPEValue = realTimeInfo.MSSpeedMPEValue,
                MSSpeedMPEStatus = realTimeInfo.MSSpeedMPEStatus,
                MSSpeedMPETime = realTimeInfo.MSSpeedMPETime,

                MSSpeedHPEValue = realTimeInfo.MSSpeedHPEValue,
                MSSpeedHPEStatus = realTimeInfo.MSSpeedHPEStatus,
                MSSpeedHPETime = realTimeInfo.MSSpeedHPETime,

                MSEnvelopeMeanValue = realTimeInfo.MSEnvelopingMEANValue,
                MSEnvelopeMeanStatus = realTimeInfo.MSEnvelopingMEANStatus,
                MSEnvelopeMeanTime = realTimeInfo.MSEnvelopingMEANTime,
            };
            return msStatusInfo;
        }

        #endregion

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-29
        /// 创建记录：获取监测设备列表树
        /// 
        /// 修改人：QXM
        /// 修改时间：2018/05/03
        /// 修改记录：解决方案融合相关问题修改
        /// </summary>
        /// <param name="useType">设备状态：0主设备，1备用设备,2:全部</param>
        /// <returns>监测设备列表树</returns>
        private List<MTStatusInfo> GetMonitorTreeList(int useType = 0)
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点

            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo()
            {
                MTId = "0",
                MTPid = "0"
            };
            mTDevStatusInfos.Add(mTDevStatusInfo);

            #endregion

            var dataContext = new iCMSDbContext();

            List<MTStatusDBInfo> monitorDBList =
                      dataContext.Database.SqlQuery<MTStatusDBInfo>("execute SetMonitorTree").ToList<MTStatusDBInfo>();
            var monitorList = monitorDBList.Select(item => new MTStatusInfo
            {
                MTId = item.MTId.ToString(),
                MTName = item.MTName,
                MTStatus = item.MTStatus.ToString(),
                MTPid = item.MTPid.ToString(),
                MTType = item.MTType.ToString(),
                RecordID = item.RecordID.ToString(),
                Remark = item.Remark,
            }).ToList();

            #region 处理解决方案融合新增字段，Added by QXM, 2018/05/03
            var deviceList = deviceRepository.GetDatas<Device>(t => true, true).ToList();
            var wgList = gatewayRepository.GetDatas<Gateway>(t => true, true).ToList();
            var measureSiteList = measureSiteRepository.GetDatas<MeasureSite>(t => true, true).ToList();

            foreach (MTStatusInfo item in monitorList)
            {
                if (!string.IsNullOrEmpty(item.MTType))
                {
                    switch (item.MTType)
                    {
                        case "DEVICE"://如果是设备                            
                            break;
                        case "MEASURESITE":// 如果是测点，判断是否是转速测点
                            int msid = int.Parse(item.RecordID);
                            var ms = measureSiteList.Where(t => t.MSiteID == msid).FirstOrDefault();
                            if (ms != null)
                            {
                                if (ms.RelationMSiteID.HasValue && ms.RelationMSiteID.Value > 0)//具有关联测点，则是转速测点
                                {
                                    item.IsSpeed = true;
                                }
                            }

                            var relatedSpeedMS = measureSiteList.Where(t => t.RelationMSiteID.HasValue && t.RelationMSiteID.Value == msid).FirstOrDefault();//找到关联的转速测点
                            if (relatedSpeedMS != null)
                            {
                                item.RelationSpeedMSiteID = relatedSpeedMS.MSiteID;
                                item.RelationSpeedMSiteName = cacheDICT.GetCacheType<MeasureSiteType>().Where(t => t.ID == relatedSpeedMS.MSiteName).First().Name;
                            }

                            break;
                    }
                }
            }
            #endregion

            mTDevStatusInfos.AddRange(monitorList);
            return mTDevStatusInfos;
        }

        #region 获取监测设备列表树
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-07-24
        /// 创建记录：获取监测设备列表树
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="useType">设备状态：0主设备，1备用设备,2:全部</param>
        /// <returns>监测设备列表树</returns>
        private List<MTStatusInfo> GetMonitorTreeListByUserId(int userId, int useType = 0)
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点

            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo()
            {
                MTId = "0",
                MTPid = "0"
            };
            mTDevStatusInfos.Add(mTDevStatusInfo);

            #endregion

            var dataContext = new iCMSDbContext();

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@userID", userId);

            List<MTStatusDBInfo> monitorDBList =
                      dataContext.Database.SqlQuery<MTStatusDBInfo>("execute SetMonitorTreeByUserID @userID", sqlParameter).ToList<MTStatusDBInfo>();
            var monitorList = monitorDBList.Select(item => new MTStatusInfo
            {
                MTId = item.MTId.ToString(),
                MTName = item.MTName,
                MTStatus = item.MTStatus.ToString(),
                MTPid = item.MTPid.ToString(),
                MTType = item.MTType.ToString(),
                RecordID = item.RecordID.ToString(),
                Remark = item.Remark,
            });
            mTDevStatusInfos.AddRange(monitorList);
            return mTDevStatusInfos;
        }
        #endregion

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-29
        /// 创建记录：获取被监测设备列表树
        /// 
        /// 修改人：张辽阔
        /// 修改时间：2016-12-01
        /// 修改记录：当WS的连接状态为断开时，电池电压和传感器温度的状态为0
        /// </summary>
        /// <returns>监测设备列表树</returns>
        private List<MTStatusInfo> GetServerTreeList()
        {
            List<MTStatusInfo> mTWSNStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTWSStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTWSNStatusInfos.Add(mTWSStatusInfo);
            #endregion

            //添加根节点
            MTStatusInfo rootNode = new MTStatusInfo()
            {
                MTId = "10000",
                MTPid = mTWSStatusInfo.MTPid,
                MTName = "Server",
                MTType = "SERVER",
                RecordID = "0",
            };
            mTWSNStatusInfos.Add(rootNode);

            MTStatusInfo wirelessRootNode = new MTStatusInfo
            {
                MTId = "10001",
                MTPid = rootNode.MTId,
                MTName = "无线",
                MTType = "WIRELESS",
                RecordID = "0",
            };
            MTStatusInfo wiredRootNode = new MTStatusInfo
            {
                MTId = "10002",
                MTPid = rootNode.MTId,
                MTName = "有线",
                MTType = "WIRED",
                RecordID = "0",
            };
            mTWSNStatusInfos.Add(wirelessRootNode);
            mTWSNStatusInfos.Add(wiredRootNode);


            //相关sql 
            //SELECT TSW.WGID ,
            //TSW.WGName ,
            //TSW2.WSID ,
            //TSW2.WSName ,
            //TSW.LinkStatus WGLinkStatus,
            //TSW2.LinkStatus WSLinkStatus,
            //TSW2.UseStatus ,
            //TSTWSM.MsiteAlmID TemperatureId,
            //TSTWSM.Status TemperatureStatus,
            //TSVSM.MsiteAlmID VoltageId,
            //TSVSM.Status VoltageStatus
            //FROM dbo.T_SYS_WG AS TSW
            //LEFT JOIN dbo.T_SYS_WS AS TSW2 ON TSW.WGID = TSW2.WGID
            //LEFT JOIN dbo.T_SYS_MEASURESITE AS TSM ON TSM.WSID = TSW2.WSID
            //LEFT JOIN dbo.T_SYS_TEMPE_WS_SET_MSITEALM AS TSTWSM ON TSTWSM.MsiteID = TSM.MSiteID
            //LEFT JOIN dbo.T_SYS_VOLTAGE_SET_MSITEALM AS TSVSM ON TSVSM.MsiteID = TSM.MSiteID;
            //--WHERE TSW2.UseStatus = 1

            var dbContext = new iCMSDbContext();
            var query =
                (from wg in dbContext.WG
                 join ws in dbContext.WS on wg.WGID equals ws.WGID
                    into wg_ws
                 from ws in wg_ws.DefaultIfEmpty()
                 join measureSite in dbContext.Measuresite on ws.WSID equals measureSite.WSID
                    into ws_measureSite
                 from measureSite in ws_measureSite.DefaultIfEmpty()
                 join measureSiteType in dbContext.MeasureSiteType on measureSite.MSiteName equals measureSiteType.ID
                    into measureSite_Type
                 from measureSiteType in measureSite_Type.DefaultIfEmpty()
                 join d in dbContext.Device on measureSite.DevID equals d.DevID
                    into measureSite_device
                 from measureSiteDevice in measureSite_device.DefaultIfEmpty()
                 join temperature in dbContext.TempeWSSetMsitealm on measureSite.MSiteID equals temperature.MsiteID
                    into measureSite_temperature
                 from temperature in measureSite_temperature.DefaultIfEmpty()
                 join voltage in dbContext.VoltageSetMsitealm on measureSite.MSiteID equals voltage.MsiteID
                    into measureSite_voltage
                 from voltage in measureSite_voltage.DefaultIfEmpty()
                 select new
                 {
                     DeviceName = measureSiteDevice.DevName,
                     MSiteName = measureSiteType.Name,
                     WGId = wg.WGID,//网关id
                     DevFormType = wg.DevFormType,//设备形态类型,Added by QXM, 2018/05/03
                     WGName = wg.WGName,//网关名称
                     WSID = ws == null ? 0 : ws.WSID,
                     WSName = ws.WSName,//传感器名称
                     WSMAC = ws.MACADDR,
                     WGLinkStatus = wg.LinkStatus,//网关状态
                     WSLinkStatus = ws == null ? -1 : ws.LinkStatus,//传感器状态
                     TemperatureId = temperature == null ? 0 : temperature.MsiteAlmID,//温度id
                     TemperatureStatus = temperature == null ? 0 : ws != null && ws.LinkStatus == 1 ? temperature.Status : 0,//温度状态
                     VoltageId = voltage == null ? 0 : voltage.MsiteAlmID,//电压id
                     VoltageStatus = voltage == null ? 0 : ws != null && ws.LinkStatus == 1 ? voltage.Status : 0,//电压状态
                 })
                .ToList();

            //增加自增id
            var queryList = query.OrderBy(item => item.WGId)
                .Select((item, index) => new
                {
                    RowId = index + 1,
                    DeviceName = item.DeviceName,
                    MSiteName = item.MSiteName,
                    WGId = item.WGId,//网关id
                    DevFormType = item.DevFormType,//设备形态类型，Added by QXM, 2018/05/03
                    WGName = item.WGName,//网关名称
                    WSId = item.WSID,
                    WSName = item.WSName,//传感器名称
                    WGLinkStatus = item.WGLinkStatus,//网关状态
                    WSLinkStatus = item.WSLinkStatus,//传感器状态
                    TemperatureId = item.TemperatureId,//温度id
                    TemperatureStatus = item.TemperatureStatus,//温度状态
                    VoltageId = item.VoltageId,//电压id
                    VoltageStatus = item.VoltageStatus,//电压状态
                    WSMAC = item.WSMAC//WS WSMAC
                })
                .ToList();

            var wgIdList = queryList.Select(item => item.WGId).Distinct().ToList<int>();

            int mtId = 1;//为树的自增id
            foreach (var wgId in wgIdList)
            {
                #region 添加网关信息
                //添加网关信息
                var wg = queryList.Where(item => item.WGId == wgId).FirstOrDefault();
                if (wg != null)
                {
                    //设备形态类型：1、单板；2、轻量级网关；3、有线
                    int DevFormType = wg.DevFormType;

                    MTStatusInfo wGMTStatusInfo = new MTStatusInfo();
                    wGMTStatusInfo.MTId = (mtId++).ToString();
                    //wGMTStatusInfo.MTPid = wirelessNode.MTId.ToString();
                    wGMTStatusInfo.MTStatus = wg.WGLinkStatus.ToString();

                    //如果是单板或者轻量级，那么都是无线
                    if (DevFormType == (int)EnumDevFormType.SingleBoard || DevFormType == (int)EnumDevFormType.iWSN)
                    {
                        wGMTStatusInfo.MTType = "WIRELESS_GATE";//网关
                        wGMTStatusInfo.MTPid = wirelessRootNode.MTId.ToString();
                    }
                    else
                    {
                        wGMTStatusInfo.MTType = "WIRED_GATE";//网关
                        wGMTStatusInfo.MTPid = wiredRootNode.MTId.ToString();
                    }

                    wGMTStatusInfo.DevFormType = DevFormType;//WG设备形态类型

                    wGMTStatusInfo.MTName = wg.WGName;
                    wGMTStatusInfo.Remark = wg.WGName;
                    wGMTStatusInfo.RecordID = wg.WGId.ToString();
                    //添加 
                    mTWSNStatusInfos.Add(wGMTStatusInfo);


                    List<int> wsIdList = null;
                    //添加传感器，一个网关对应多个传感器
                    //var wsIdList = queryList.Where(item => item.WGId == wgId).Select(item => item.WSId).Distinct();

                    if (DevFormType == (int)EnumDevFormType.SingleBoard || DevFormType == (int)EnumDevFormType.iWSN)
                    {
                        /* 有线传感器时候，左侧监测树三轴传感器显示为一个WS，故根据WSMAC地址进行分组， Modified by QXM, 2018/05/14*/
                        wsIdList = queryList.Where(t => t.WGId == wgId).DistinctBy(t => t.WSMAC).Select(t => t.WSId).Distinct().ToList();
                    }
                    else
                    {
                        wsIdList = queryList.Where(t => t.WGId == wgId).Select(t => t.WSId).Distinct().ToList();
                    }

                    //var wsIdList2 = queryList.Where(t => t.WGId == wgId).DistinctBy(t => t.WSMAC).Select(t => t.WSMAC).ToList();

                    foreach (var wsId in wsIdList)
                    {
                        if (wsId != 0)
                        {
                            var ws = queryList.Where(item => item.WSId == wsId).FirstOrDefault();
                            if (ws != null)
                            {
                                MTStatusInfo wSMTStatusInfo = new MTStatusInfo();
                                wSMTStatusInfo.MTId = (mtId++).ToString();
                                wSMTStatusInfo.MTPid = wGMTStatusInfo.MTId;
                                wSMTStatusInfo.MTStatus = ws.WSLinkStatus.ToString();
                                //如果是单板或者轻量级，那么都是无线
                                if (DevFormType == (int)EnumDevFormType.SingleBoard || DevFormType == (int)EnumDevFormType.iWSN)
                                {
                                    wSMTStatusInfo.MTType = "WIRELESS_SENSOR";//传感器
                                }
                                else
                                {
                                    wSMTStatusInfo.MTType = "WIRED_SENSOR";//传感器
                                }

                                wSMTStatusInfo.DevFormType = ws.DevFormType;//传感器设备形态，表示 三轴，无线，有线

                                //wSMTStatusInfo.MTType = "WIRELESS_SENSOR";//传感器
                                wSMTStatusInfo.MTName = ws.WSName;
                                wSMTStatusInfo.Remark = ws.DeviceName + "-" + ws.MSiteName;
                                wSMTStatusInfo.RecordID = ws.WSId.ToString();
                                //添加 
                                mTWSNStatusInfos.Add(wSMTStatusInfo);

                                #region 传感器温度
                                //添加 
                                var temperature = queryList.Where(item => item.WSId == ws.WSId && item.TemperatureId != 0).FirstOrDefault();
                                if (temperature != null)
                                {
                                    if (temperature.TemperatureId != 0)
                                    {
                                        MTStatusInfo tempeWSMTStatusInfo = new MTStatusInfo();
                                        tempeWSMTStatusInfo.MTId = (mtId++).ToString();
                                        tempeWSMTStatusInfo.MTPid = wSMTStatusInfo.MTId;
                                        tempeWSMTStatusInfo.MTStatus = temperature.TemperatureStatus.ToString();
                                        tempeWSMTStatusInfo.MTType = "TEMPE_WS";//传感器温度
                                        tempeWSMTStatusInfo.MTName = "传感器温度";
                                        tempeWSMTStatusInfo.Remark = "传感器温度";
                                        tempeWSMTStatusInfo.RecordID = temperature.TemperatureId.ToString();

                                        //添加 
                                        mTWSNStatusInfos.Add(tempeWSMTStatusInfo);
                                    }
                                }
                                #endregion

                                #region 电池电压
                                //添加 
                                var voltage = queryList.Where(item => item.WSId == ws.WSId && item.VoltageId != 0).FirstOrDefault();
                                if (voltage != null)
                                {
                                    if (voltage.VoltageId != 0)
                                    {
                                        MTStatusInfo voltageMTStatusInfo = new MTStatusInfo();
                                        voltageMTStatusInfo.MTId = (mtId++).ToString();
                                        voltageMTStatusInfo.MTPid = wSMTStatusInfo.MTId;
                                        voltageMTStatusInfo.MTStatus = voltage.VoltageStatus.ToString();
                                        voltageMTStatusInfo.MTType = "VOLTAGE_WS";//电池电压
                                        voltageMTStatusInfo.MTName = "电池电压";
                                        voltageMTStatusInfo.Remark = "电池电压";
                                        voltageMTStatusInfo.RecordID = voltage.VoltageId.ToString();

                                        //添加 
                                        mTWSNStatusInfos.Add(voltageMTStatusInfo);
                                    }
                                }
                                #endregion
                            }
                        }

                    }
                }
                #endregion
            }

            return mTWSNStatusInfos;
        }

        #region 获取被监测设备列表树
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-07-24
        /// 创建记录：获取被监测设备列表树
        /// </summary>
        /// <returns>监测设备列表树</returns>
        private List<MTStatusInfo> GetServerTreeListByUserId(int userId)
        {
            List<MTStatusInfo> mTWSNStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTWSStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTWSNStatusInfos.Add(mTWSStatusInfo);
            #endregion

            //添加根节点
            MTStatusInfo rootNode = new MTStatusInfo()
            {
                MTId = "10000",
                MTPid = mTWSStatusInfo.MTPid,
                MTName = "Server",
                MTType = "SERVER",
                RecordID = "0",
            };
            mTWSNStatusInfos.Add(rootNode);

            #region
            //相关sql 
            //SELECT TSW.WGID ,
            //TSW.WGName ,
            //TSW2.WSID ,
            //TSW2.WSName ,
            //TSW.LinkStatus WGLinkStatus,
            //TSW2.LinkStatus WSLinkStatus,
            //TSW2.UseStatus ,
            //TSTWSM.MsiteAlmID TemperatureId,
            //TSTWSM.Status TemperatureStatus,
            //TSVSM.MsiteAlmID VoltageId,
            //TSVSM.Status VoltageStatus
            //FROM dbo.T_SYS_WG AS TSW
            //LEFT JOIN dbo.T_SYS_WS AS TSW2 ON TSW.WGID = TSW2.WGID
            //LEFT JOIN dbo.T_SYS_MEASURESITE AS TSM ON TSM.WSID = TSW2.WSID
            //LEFT JOIN dbo.T_SYS_TEMPE_WS_SET_MSITEALM AS TSTWSM ON TSTWSM.MsiteID = TSM.MSiteID
            //LEFT JOIN dbo.T_SYS_VOLTAGE_SET_MSITEALM AS TSVSM ON TSVSM.MsiteID = TSM.MSiteID;
            //--WHERE TSW2.UseStatus = 1
            #endregion

            var dbContext = new iCMSDbContext();
            var query =
                (from wg in dbContext.WG
                 join ws in dbContext.WS
                    on wg.WGID equals ws.WGID
                    into wg_ws
                 from ws in wg_ws.DefaultIfEmpty()

                 join measureSite in dbContext.Measuresite
                    on ws.WSID equals measureSite.WSID
                    into ws_measureSite
                 from measureSite in ws_measureSite.DefaultIfEmpty()
                 join temperature in dbContext.TempeWSSetMsitealm
                    on measureSite.MSiteID equals temperature.MsiteID
                    into measureSite_temperature
                 from temperature in measureSite_temperature.DefaultIfEmpty()
                 join voltage in dbContext.VoltageSetMsitealm
                    on measureSite.MSiteID equals voltage.MsiteID
                    into measureSite_voltage
                 from voltage in measureSite_voltage.DefaultIfEmpty()
                 where ((from urws in dbContext.UserRelationWS
                         where urws.UserID == userId
                         select urws.WSID).Distinct().ToList()).Contains(ws.WSID)
                 select new
                 {
                     WGId = wg.WGID,//网关id
                     WGName = wg.WGName,//网关名称
                     WSID = ws == null ? 0 : ws.WSID,
                     WSName = ws.WSName,//传感器名称
                     WGLinkStatus = wg.LinkStatus,//网关状态
                     WSLinkStatus = ws == null ? -1 : ws.LinkStatus,//传感器状态
                     TemperatureId = temperature == null ? 0 : temperature.MsiteAlmID,//温度id
                     //张辽阔 2016-12-01 注释
                     //TemperatureStatus = temperature == null ? 0 : temperature.Status,//温度状态
                     //张辽阔 2016-12-01 添加
                     TemperatureStatus = temperature == null ? 0 : ws != null && ws.LinkStatus == 1 ? temperature.Status : 0,//温度状态
                     VoltageId = voltage == null ? 0 : voltage.MsiteAlmID,//电压id
                     //张辽阔 2016-12-01 注释
                     //VoltageStatus = voltage == null ? -1 : voltage.Status,//电压状态
                     //张辽阔 2016-12-01 添加
                     VoltageStatus = voltage == null ? 0 : ws != null && ws.LinkStatus == 1 ? voltage.Status : 0,//电压状态
                 })
                .ToList();

            //增加自增id
            var queryList = query.OrderBy(item => item.WGId)
                .Select((item, index) => new
                {
                    RowId = index + 1,
                    WGId = item.WGId,//网关id
                    WGName = item.WGName,//网关名称
                    WSId = item.WSID,
                    WSName = item.WSName,//传感器名称
                    WGLinkStatus = item.WGLinkStatus,//网关状态
                    WSLinkStatus = item.WSLinkStatus,//传感器状态
                    TemperatureId = item.TemperatureId,//温度id
                    TemperatureStatus = item.TemperatureStatus,//温度状态
                    VoltageId = item.VoltageId,//电压id
                    VoltageStatus = item.VoltageStatus,//电压状态
                })
                .ToList();
            var wsDBList = (from urws in dbContext.UserRelationWS
                            join ws in dbContext.WS on urws.WSID equals ws.WSID
                            where urws.UserID == userId
                            select ws).ToList();
            var wsIDList = wsDBList.Select(s => s.WGID).ToList();
            var wgDBList = gatewayRepository.GetDatas<Gateway>(itme => wsIDList.Contains(itme.WGID), false);
            var serverTreeList = GetServerTree(userId);

            int maxId = Convert.ToInt32(rootNode.MTId + 1);
            //if (serverTreeList != null && serverTreeList.Count > 0)
            //{
            //    maxId = serverTreeList.Max(item => item.TreeId) + 1;
            //}
            if (serverTreeList != null && serverTreeList.Any())
            {
                var wgTreeList = serverTreeList.Where(item => item.Level == 1);
                foreach (var wgInfo in wgTreeList)
                {
                    var wg = wgDBList.Where(item => item.WGID == wgInfo.TrueId).FirstOrDefault();
                    if (wg != null)
                    {
                        MTStatusInfo wGMTStatusInfo = new MTStatusInfo();
                        wGMTStatusInfo.MTId = (maxId++).ToString();
                        wGMTStatusInfo.MTPid = rootNode.MTId.ToString();
                        wGMTStatusInfo.MTStatus = wg.LinkStatus.ToString();

                        wGMTStatusInfo.MTType = "WIRELESS_GATE";//网关
                        wGMTStatusInfo.MTName = wg.WGName;
                        wGMTStatusInfo.Remark = wg.WGName;
                        wGMTStatusInfo.RecordID = wg.WGID.ToString();
                        //添加
                        mTWSNStatusInfos.Add(wGMTStatusInfo);

                        var wsTreeList = serverTreeList.Where(item => item.Level == 2 && item.ParentId == wgInfo.TreeId);

                        if (wsTreeList != null && wsTreeList.Any())
                        {
                            foreach (var wsInfo in wsTreeList)
                            {
                                var ws = wsDBList.Where(item => item.WSID == wsInfo.TrueId).FirstOrDefault();
                                if (ws != null)
                                {
                                    MTStatusInfo wSMTStatusInfo = new MTStatusInfo();
                                    wSMTStatusInfo.MTId = (maxId++).ToString();
                                    wSMTStatusInfo.MTPid = wGMTStatusInfo.MTId;
                                    wSMTStatusInfo.MTStatus = ws.LinkStatus.ToString();
                                    wSMTStatusInfo.MTType = "WIRELESS_SENSOR";//传感器
                                    wSMTStatusInfo.MTName = ws.WSName;
                                    wSMTStatusInfo.Remark = (from ms in dbContext.Measuresite
                                                             join d in dbContext.Device on ms.DevID equals d.DevID
                                                             where ms.WSID == ws.WSID
                                                             select d.DevName).FirstOrDefault() + "-" +
                                                        (from ms in dbContext.Measuresite
                                                         join mst in dbContext.MeasureSiteType on ms.MSiteName equals mst.ID
                                                         where ms.WSID == ws.WSID
                                                         select mst.Name).FirstOrDefault();
                                    wSMTStatusInfo.RecordID = wsInfo.TrueId.ToString();
                                    //添加
                                    mTWSNStatusInfos.Add(wSMTStatusInfo);

                                    #region 传感器温度
                                    //添加
                                    var temperature = queryList.Where(item => item.WSId == ws.WSID && item.TemperatureId != 0).FirstOrDefault();
                                    if (temperature != null)
                                    {
                                        if (temperature.TemperatureId != 0)
                                        {
                                            MTStatusInfo tempeWSMTStatusInfo = new MTStatusInfo();
                                            tempeWSMTStatusInfo.MTId = (maxId++).ToString();
                                            tempeWSMTStatusInfo.MTPid = wSMTStatusInfo.MTId;
                                            tempeWSMTStatusInfo.MTStatus = temperature.TemperatureStatus.ToString();
                                            tempeWSMTStatusInfo.MTType = "TEMPE_WS";//传感器温度
                                            tempeWSMTStatusInfo.MTName = "传感器温度";
                                            tempeWSMTStatusInfo.Remark = "传感器温度";
                                            tempeWSMTStatusInfo.RecordID = temperature.TemperatureId.ToString();

                                            //添加
                                            mTWSNStatusInfos.Add(tempeWSMTStatusInfo);
                                        }
                                    }
                                    #endregion

                                    #region 电池电压
                                    //添加
                                    var voltage = queryList.Where(item => item.WSId == ws.WSID && item.VoltageId != 0).FirstOrDefault();
                                    if (voltage != null)
                                    {
                                        if (voltage.VoltageId != 0)
                                        {
                                            MTStatusInfo voltageMTStatusInfo = new MTStatusInfo();
                                            voltageMTStatusInfo.MTId = (maxId++).ToString();
                                            voltageMTStatusInfo.MTPid = wSMTStatusInfo.MTId;
                                            voltageMTStatusInfo.MTStatus = voltage.VoltageStatus.ToString();
                                            voltageMTStatusInfo.MTType = "VOLTAGE_WS";//电池电压
                                            voltageMTStatusInfo.MTName = "电池电压";
                                            voltageMTStatusInfo.Remark = "电池电压";
                                            voltageMTStatusInfo.RecordID = voltage.VoltageId.ToString();

                                            //添加
                                            mTWSNStatusInfos.Add(voltageMTStatusInfo);
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
            }

            return mTWSNStatusInfos;
        }
        #endregion

        #region 获取Server树信息
        /// <summary>
        /// 获取Server树信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        private List<Tree> GetServerTree(int userId)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@userId", userId);
            sqlParam[1] = new SqlParameter("@level", 2);
            OperationResult operationResult = measureSiteRepository
                .SqlQuery<Tree>("EXEC GetServerTree @userId,@level", sqlParam);
            List<Tree> treeList = new List<Tree>();
            if (operationResult.ResultType == EnumOperationResultType.Success)
            {
                treeList = operationResult.AppendData as List<Tree>;
            }
            return treeList;
        }
        #endregion

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-28
        /// 创建记录：获取最小刷新时间
        /// </summary>
        /// <returns>最小刷新时间</returns>
        private string GetMinFreshTime()
        {
            //测量位置
            var measureSiteList = GetMeasureSiteList();
            //最小刷新时间，单位：分钟,通过测量位置计算最小间隔时间
            string minFreshTime = measureSiteList.Min(item => item.TemperatureTime);
            return minFreshTime;
        }

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-27
        /// 创建记录：获取设备列表信息
        /// </summary>
        /// <returns>设备列表信息</returns>
        private List<MeasureSite> GetMeasureSiteList()
        {
            var list = measureSiteRepository.GetDatas<MeasureSite>(t => true, false);
            return list.ToList<MeasureSite>();
        }
        #endregion

        #region 报警提醒
        public BaseResponse<DevAlarmRemindDataResult> GetDevWarningDataByUser(QueryDevWarningDataParameter Parameter)
        {
            BaseResponse<DevAlarmRemindDataResult> baseResponse = new BaseResponse<DevAlarmRemindDataResult>();
            try
            {
                baseResponse = Validate.ValidateQueryDevAlarmRemindDataByUserIdParams(Parameter);
                if (!baseResponse.IsSuccessful)
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Result = new DevAlarmRemindDataResult() { DevAlarmRemindDataInfo = new List<DevAlarmRemindInfo>(), Total = 0, MinFreshTime = 5 };
                    return baseResponse;
                }

                if (Parameter.Page == 0)
                {
                    Parameter.Page = 1;
                }
                #region 使用XML来表示设备ID与最后一次报警时间得对应关系，作为存储过程参数传入
                string devIDLastAlarmTimeXMLStr = "";
                if (!string.IsNullOrWhiteSpace(Parameter.DevIDLastAlarmTime))
                {
                    List<string[]> temp_DevIDLastAlarmTime = Parameter.DevIDLastAlarmTime.Split(',')
                        .Where(p => !string.IsNullOrWhiteSpace(p))
                        .Select(p => p.Split('_'))
                        .Where(p => p.Length == 2)
                        .ToList();

                    devIDLastAlarmTimeXMLStr = "<devIDLastAlarmTimeList>"
                        + string.Join("", temp_DevIDLastAlarmTime.Select(p =>
                        {
                            return "<devIDLastAlarmTime DevID=\"" + p[0] + "\"" + " LastAlarmTime=\"" + p[1] + "\"" + "></devIDLastAlarmTime>";
                        }))
                        + "</devIDLastAlarmTimeList>";
                }
                #endregion

                using (var dataContext = new iCMSDbContext())
                {
                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@userid", Parameter.UserID),
                        new SqlParameter("@devAlmStat", Parameter.DevAlmStat),
                        new SqlParameter("@bDate", Parameter.BDate),
                        new SqlParameter("@eDate", Parameter.EDate),
                        new SqlParameter("@devIDLastAlarmTime", devIDLastAlarmTimeXMLStr),
                        new SqlParameter("@sort", Parameter.Sort),
                        new SqlParameter("@order", Parameter.Order),
                        new SqlParameter("@page", Parameter.Page),
                        new SqlParameter("@pageSize", Parameter.PageSize),
                        new SqlParameter("@count", 0),
                    };
                    sqlParameters[sqlParameters.Length - 1].Direction = ParameterDirection.Output;

                    List<DevAlarmRemindInfo> devAlarmRemindDataInfo =
                        dataContext.Database.SqlQuery<DevAlarmRemindInfo>(
                            "execute GetDevAlarmRemindDataByUserId @userid, @devAlmStat, @bDate, @eDate,"
                                + "@devIDLastAlarmTime, @sort, @order, @page, @pageSize, @count output",
                            sqlParameters)
                        .Select(p =>
                        {
                            string MTIds = "";
                            string MTNames = "";
                            if (!string.IsNullOrWhiteSpace(p.MTIDs))
                            {
                                var temp_MTIds = p.MTIDs.Split('#')
                                    .Select(tempTreeID => Convert.ToInt32(tempTreeID))
                                    .OrderBy(tempTreeID => tempTreeID)
                                    .ToArray();
                                MTIds = string.Join("#", temp_MTIds);
                                MTNames = string.Join("#",
                                    from mt in dataContext.MonitorTree
                                    join treeID in temp_MTIds on mt.MonitorTreeID equals treeID
                                    orderby mt.MonitorTreeID ascending
                                    select mt.Name);
                            }
                            return new DevAlarmRemindInfo
                            {
                                MTIDs = MTIds,
                                MTNames = MTNames,
                                DevID = p.DevID,
                                DevName = p.DevName,
                                DevType = p.DevType,
                                AlarmStat = p.AlarmStat,
                                LastAlarmTime = p.LastAlarmTime,
                            };
                        })
                        .ToList();

                    var MinFreshTime =
                        (from ms in dataContext.Measuresite
                         join devID in devAlarmRemindDataInfo.Select(p => p.DevID) on ms.DevID equals devID
                         select ms)
                        .Min(p => p.TemperatureTime);

                    baseResponse.Result = new DevAlarmRemindDataResult
                    {
                        Total = Convert.ToInt32(sqlParameters[sqlParameters.Length - 1].Value),
                        DevAlarmRemindDataInfo = devAlarmRemindDataInfo,
                        MinFreshTime = Convert.ToInt32(MinFreshTime),
                    };
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.Code = "002461";
                baseResponse.IsSuccessful = false;
                baseResponse.Result = new DevAlarmRemindDataResult()
                {
                    DevAlarmRemindDataInfo = new List<DevAlarmRemindInfo>(),
                    Total = 0,
                    MinFreshTime = 5
                };
                return baseResponse;
            }
        }
        #endregion

        #region 系统配置信息通用查询接口
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：系统配置信息通用查询接口
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetConfigListForDeviceMonitorCenterResult> GetConfigListForDeviceMonitorCenter(GetConfigListForDeviceMonitorCenterParameter parameter)
        {
            BaseResponse<GetConfigListForDeviceMonitorCenterResult> baseResponse = new BaseResponse<GetConfigListForDeviceMonitorCenterResult>();
            GetConfigListForDeviceMonitorCenterResult result = new GetConfigListForDeviceMonitorCenterResult();
            result.MSConfigByDeviceID = new List<ConfigInfo>();
            result.TopographicMapSetsConfig = new List<ConfigInfo>();
            result.TopographicMapPictureConfig = new List<ConfigInfo>();
            baseResponse.Result = result;

            #region 验证
            //获取系统配置信息通用查询时，设备id和设备类型code错误
            if (parameter.DeviceID < 1 && string.IsNullOrWhiteSpace(parameter.DeviceTypeCode))
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009322";
                return baseResponse;
            }
            #endregion
            try
            {
                baseResponse.IsSuccessful = true;



                //形貌图显示配置
                var topographicMapShowConfig = configRepository.GetDatas<Config>(item => item.Code == "DeviceImageDisplayConfiguration", true).FirstOrDefault();

                if (parameter.DeviceID > 0)
                {

                    var device = deviceRepository.GetDatas<Device>(item => item.DevID == parameter.DeviceID, true).FirstOrDefault();

                    if (device != null)
                    {
                        var deviceType = cacheDICT.GetInstance()
                       .GetCacheType<DeviceType>(p => p.ID == device.DevType)
                       .FirstOrDefault();
                        if (deviceType != null)
                        {
                            //形貌图配置
                            var topographicMapConfig = configRepository.GetDatas<Config>(item => item.CommonDataCode == deviceType.Code, true).FirstOrDefault();
                            if (topographicMapConfig != null)
                            {
                                //设备下测点配置
                                var configInfoList = configRepository.GetDatas<Config>(item => item.ParentId == topographicMapConfig.ID && item.IsUsed == 1, true).ToList();
                                if (configInfoList != null && configInfoList.Any())
                                {
                                    result.MSConfigByDeviceID = configInfoList.Select(item =>
                                    {
                                        var isExsit = configRepository.GetDatas<Config>(info => info.ParentId == item.ID && item.IsUsed == 1, true).Count() > 0;
                                        return new ConfigInfo
                                        {
                                            ID = item.ID,
                                            Code = item.Code,
                                            Name = item.Name,
                                            OrderNo = item.OrderNo,
                                            Value = item.Value,
                                            Describe = item.Describe,
                                            IsDefault = item.IsDefault,
                                            AddDate = item.AddDate,
                                            IsExistChild = isExsit,
                                            CommonDataType = item.CommonDataType,
                                            CommonDataCode = item.CommonDataCode,
                                        };

                                    }).ToList();
                                }

                                //形貌图设备图片配置
                                result.TopographicMapSetsConfig = new List<ConfigInfo>();
                                ConfigInfo configInfo = new ConfigInfo();
                                configInfo.ID = topographicMapConfig.ID;
                                configInfo.Code = topographicMapConfig.Code;
                                configInfo.Name = topographicMapConfig.Name;
                                configInfo.OrderNo = topographicMapConfig.OrderNo;
                                configInfo.Value = topographicMapConfig.Value;
                                configInfo.Describe = topographicMapConfig.Describe;
                                configInfo.IsDefault = topographicMapConfig.IsDefault;
                                configInfo.AddDate = topographicMapConfig.AddDate;
                                configInfo.CommonDataType = topographicMapConfig.CommonDataType;
                                configInfo.CommonDataCode = topographicMapConfig.CommonDataCode;
                                configInfo.IsExistChild = configRepository.GetDatas<Config>(item => item.ParentId == configInfo.ID && item.IsUsed == 1, true).Count() > 0;
                                result.TopographicMapPictureConfig.Add(configInfo);
                            }


                        }
                    }



                    if (topographicMapShowConfig != null)
                    {
                        //形貌图显示配置
                        var configInfoList = configRepository.GetDatas<Config>(item => item.ParentId == topographicMapShowConfig.ID && item.IsUsed == 1, true).ToList();
                        if (configInfoList != null && configInfoList.Any())
                        {
                            result.TopographicMapSetsConfig = configInfoList.Select(item =>
                            {
                                var isExsit = configRepository.GetDatas<Config>(info => info.ParentId == item.ID && item.IsUsed == 1, true).Count() > 0;
                                return new ConfigInfo
                                {
                                    ID = item.ID,
                                    Code = item.Code,
                                    Name = item.Name,
                                    OrderNo = item.OrderNo,
                                    Value = item.Value,
                                    Describe = item.Describe,
                                    IsDefault = item.IsDefault,
                                    AddDate = item.AddDate,
                                    IsExistChild = isExsit,
                                    CommonDataType = item.CommonDataType,
                                    CommonDataCode = item.CommonDataCode,
                                };

                            }).ToList();
                        }
                    }
                }

                else
                {
                    var config = configRepository.GetDatas<Config>(item => item.CommonDataCode == parameter.DeviceTypeCode, true).FirstOrDefault();
                    if (config != null)
                    {
                        //获取所有父节点配置
                        var configInfoList = configRepository.GetDatas<Config>(item => item.ParentId == config.ID, true).ToList();
                        if (configInfoList != null && configInfoList.Any())
                        {
                            result.MSConfigByDeviceID = configInfoList.Select(item =>
                            {
                                var isExsit = configRepository.GetDatas<Config>(info => info.ParentId == item.ID, true).Count() > 0;
                                return new ConfigInfo
                                {
                                    ID = item.ID,
                                    Code = item.Code,
                                    Name = item.Name,
                                    OrderNo = item.OrderNo,
                                    Value = item.Value,
                                    Describe = item.Describe,
                                    IsDefault = item.IsDefault,
                                    AddDate = item.AddDate,
                                    IsExistChild = isExsit,
                                    CommonDataType = item.CommonDataType,
                                    CommonDataCode = item.CommonDataCode,
                                };

                            }).ToList();
                        }


                        //获取当前节点图片信息
                        result.TopographicMapSetsConfig = new List<ConfigInfo>();
                        ConfigInfo configInfo = new ConfigInfo();
                        configInfo.ID = config.ID;
                        configInfo.Code = config.Code;
                        configInfo.Name = config.Name;
                        configInfo.OrderNo = config.OrderNo;
                        configInfo.Value = config.Value;
                        configInfo.Describe = config.Describe;
                        configInfo.IsDefault = config.IsDefault;
                        configInfo.AddDate = config.AddDate;
                        configInfo.CommonDataType = config.CommonDataType;
                        configInfo.CommonDataCode = config.CommonDataCode;
                        configInfo.IsExistChild = configRepository.GetDatas<Config>(item => item.ParentId == configInfo.ID, true).Count() > 0;
                        result.TopographicMapPictureConfig.Add(configInfo);


                    }

                    if (topographicMapShowConfig != null)
                    {
                        //形貌图显示配置
                        var configInfoList = configRepository.GetDatas<Config>(item => item.ParentId == topographicMapShowConfig.ID, true).ToList();
                        if (configInfoList != null && configInfoList.Any())
                        {
                            result.TopographicMapSetsConfig = configInfoList.Select(item =>
                            {
                                var isExsit = configRepository.GetDatas<Config>(info => info.ParentId == item.ID, true).Count() > 0;
                                return new ConfigInfo
                                {
                                    ID = item.ID,
                                    Code = item.Code,
                                    Name = item.Name,
                                    OrderNo = item.OrderNo,
                                    Value = item.Value,
                                    Describe = item.Describe,
                                    IsDefault = item.IsDefault,
                                    AddDate = item.AddDate,
                                    IsExistChild = isExsit,
                                    CommonDataType = item.CommonDataType,
                                    CommonDataCode = item.CommonDataCode,
                                };

                            }).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008881";

            }

            return baseResponse;

        }
        #endregion

        //#region 向调用者暴露对形貌图数据展示
        ///// <summary>
        ///// 创建人：王颖辉
        ///// 创建时间:2017-10-13
        ///// 创建内容:向调用者暴露对形貌图数据展示
        ///// </summary>
        ///// <param name="parameter">参数</param>
        ///// <returns></returns>
        //public BaseResponse<GetDevImgDataResult> GetDevImgData(GetDevImgDataParameter parameter)
        //{
        //    BaseResponse<GetDevImgDataResult> baseResponse = new BaseResponse<GetDevImgDataResult>();
        //    GetDevImgDataResult result = new GetDevImgDataResult();

        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog(ex);
        //        baseResponse.IsSuccessful = false;
        //        baseResponse.Code = "000000";

        //    }

        //    return baseResponse;
        //}
        //#endregion

        #region 获取当前设备状态及报告统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：获取当前设备状态及报告统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetDeviceTopographicMapStatisticsResult> GetDeviceTopographicMapStatistics(GetDeviceTopographicMapStatisticsParameter parameter)
        {
            BaseResponse<GetDeviceTopographicMapStatisticsResult> baseResponse = new BaseResponse<GetDeviceTopographicMapStatisticsResult>();
            GetDeviceTopographicMapStatisticsResult result = new GetDeviceTopographicMapStatisticsResult();
            result.DeviceAlarmInfoList = new List<DeviceAlarmInfoList>();
            baseResponse.Result = result;
            try
            {
                #region 验证设备信息
                //获取当前设备状态及报告统计时,设备id不正确
                if (parameter.DeviceID < 1)
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "009742";
                    return baseResponse;
                }

                var device = deviceRepository.GetByKey(parameter.DeviceID);
                //获取当前设备状态及报告统计时,设备不存在
                if (device == null)
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "009752";
                    return baseResponse;
                }

                #endregion


                if (parameter.Type == 1)
                {
                    //查询几个月
                    int queryMonth = parameter.Num;
                    #region 验证月数
                    //获取当前设备状态及报告统计,月份不正确
                    if (queryMonth < 1 || queryMonth > 12)
                    {
                        baseResponse.IsSuccessful = false;
                        baseResponse.Code = "009682";
                        return baseResponse;
                    }
                    #endregion


                    //最近12个月
                    StringBuilder sqlAlmStatus = new StringBuilder();
                    DateTime beginTime = DateTime.Now;
                    DateTime endTime = DateTime.Now.AddMonths(-queryMonth);//本月最后一天
                    for (int i = 0; i < queryMonth; i++)
                    {
                        //上一个月的一号
                        beginTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i);
                        endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i + 1).AddDays(-1);//本月最后一天

                        if (i != 0)
                        {
                            sqlAlmStatus.Append(" UNION ");
                        }

                        #region 报警状态
                        string tempSqlAlmStatus = @" SELECT ISNULL(MAX(TSDA.AlmStatus),0)  AlmStatus,
                                                   '{0}' Month
                                            FROM dbo.T_SYS_DEV_ALMRECORD AS TSDA
                                            WHERE (
                                                       TSDA.BDate <= '{2} 23:59:59'
                                                  and
                                                         TSDA.LatestStartTime >= '{1} 00:00:00'
                                                     )  AND DevID ={3}";
                        tempSqlAlmStatus = string.Format(tempSqlAlmStatus, beginTime.ToString("yyyy-MM"), beginTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd"), parameter.DeviceID);
                        sqlAlmStatus.Append(tempSqlAlmStatus);
                        #endregion
                    }

                    beginTime = DateTime.Now;
                    endTime = DateTime.Now.AddMonths(-queryMonth);
                    var dbContext = new iCMSDbContext();
                    var alartStatList = dbContext.Database.SqlQuery<DeviceMaintenanceLogMonthAlarmStatResult>(sqlAlmStatus.ToString()).ToList();

                    string sql = @"SELECT  COUNT(1) DeviceMaintenanceLogCount,
                                    convert(nvarchar(7),UpdateDate,120) AS Month
                                    FROM    dbo.T_SYS_MAINTAIN_REPORT
                                    WHERE UpdateDate BETWEEN '{0}' AND '{1}' and ReportType=1 And IsDeleted = 0 AND IsTemplate = 0
                                    AND DeviceID ={2}
                                    GROUP BY convert(nvarchar(7),UpdateDate,120)";
                    sql = string.Format(sql, endTime.ToString("yyyy-MM-dd"), beginTime.ToString("yyyy-MM-dd"), parameter.DeviceID);
                    var deviceMaintenanceLogStatList = dbContext.Database.SqlQuery<DeviceMaintenanceMonthLogStat>(sql).ToList();

                    sql = @"SELECT  COUNT(1) DeviceDiagnosticReport,
                                convert(nvarchar(7),UpdateDate,120) AS Month
                                FROM    dbo.T_SYS_DIAGNOSE_REPORT
                                WHERE UpdateDate BETWEEN '{0}' AND '{1}'  And IsDeleted = 0 AND IsTemplate = 0
                                AND DeviceID ={2}
                                GROUP BY convert(nvarchar(7),UpdateDate,120)";
                    sql = string.Format(sql, endTime.ToString("yyyy-MM-dd"), beginTime.ToString("yyyy-MM-dd"), parameter.DeviceID);
                    var deviceDiagnosticReportStatList = dbContext.Database.SqlQuery<DeviceDiagnosticReportMonthStat>(sql).ToList();

                    for (int i = 0; i < queryMonth; i++)
                    {

                        string month = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i).ToString("yyyy-MM");
                        DeviceAlarmInfoList deviceAlarmInfoList = new DeviceAlarmInfoList();
                        deviceAlarmInfoList.Date = month.ToString();

                        var dstatusList = alartStatList.Where(item => item.Month == month);
                        if (dstatusList != null && dstatusList.Any())
                        {
                            deviceAlarmInfoList.DeviceStatus = dstatusList.FirstOrDefault().AlmStatus;
                        }

                        var reportList = deviceMaintenanceLogStatList.Where(item => item.Month == month);
                        if (reportList != null && reportList.Any())
                        {
                            deviceAlarmInfoList.DeviceMaintenanceLogCount = reportList.FirstOrDefault().DeviceMaintenanceLogCount;
                        }

                        var logCountList = deviceDiagnosticReportStatList.Where(item => item.Month == month);
                        if (logCountList != null && logCountList.Any())
                        {
                            deviceAlarmInfoList.DeviceDiagnosticReport = logCountList.FirstOrDefault().DeviceDiagnosticReport;
                        }

                        result.DeviceAlarmInfoList.Add(deviceAlarmInfoList);
                    }


                    #region 处理未采集状态
                    //处理未采集状态
                    var measureSiteIDList = measureSiteRepository.GetDatas<MeasureSite>(item => item.DevID == parameter.DeviceID, true).Select(item => item.MSiteID).ToList();
                    //6为设备采集信息
                    var realTimeAlarmThresholdList = realTimeAlarmThresholdRepository.GetDatas<RealTimeAlarmThreshold>(item => item.MeasureSiteThresholdType <= 6 && measureSiteIDList.Contains(item.MeasureSiteID), true).ToList();
                    //计算最小采集时间
                    if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                    {
                        DateTime minDateTime = realTimeAlarmThresholdList.Min(item => item.SamplingDate);
                        foreach (var info in result.DeviceAlarmInfoList)
                        {
                            DateTime dateTime = Convert.ToDateTime(info.Date + "-01");

                            minDateTime = Convert.ToDateTime(minDateTime.ToString("yyyy-MM-01") + " 00:00:00");
                            //当前时间小于最小采集时间为未采集
                            if (dateTime < minDateTime)
                            {
                                info.DeviceStatus = 0;//未采集状态
                            }

                            #region 处理停机状态
                            if (device.RunStatus == (int)EnumRunStatus.Stop)
                            {
                                if (device.DeviceStopDate.HasValue)
                                {
                                    if (dateTime > device.DeviceStopDate)
                                    {
                                        info.DeviceStatus = 0;//未采集状态
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        //如果没有采集数据，则都为未采集状态
                        foreach (var info in result.DeviceAlarmInfoList)
                        {
                            info.DeviceStatus = 0;//未采集状态
                        }
                    }
                    #endregion
                }
                else
                {
                    //按照30天
                    DateTime beginTime = DateTime.Now;
                    DateTime endTime = DateTime.Now.AddDays(-30 * 1);
                    StringBuilder sqlAlmStatus = new StringBuilder();
                    var queryDay = parameter.Num;
                    #region 验证日期数量
                    //获取当前设备状态及报告统计,日期数不正确
                    if (queryDay < 1 || queryDay > 30)
                    {
                        baseResponse.IsSuccessful = false;
                        baseResponse.Code = "009692";
                        return baseResponse;
                    }
                    #endregion

                    for (int i = 0; i < queryDay; i++)
                    {
                        //上一个月的一号
                        beginTime = DateTime.Now.AddDays(-i);
                        endTime = DateTime.Now.AddDays(-i);//本月最后一天

                        if (i != 0)
                        {
                            sqlAlmStatus.Append(" UNION ");
                        }

                        #region 报警状态
                        string tempSqlAlmStatus = @" SELECT ISNULL(MAX(TSDA.AlmStatus),0)  AlmStatus,
                                                   '{0}' Day
                                            FROM dbo.T_SYS_DEV_ALMRECORD AS TSDA
                                            WHERE (
                                                       TSDA.BDate <= '{2} 23:59:59'
                                                  and
                                                         TSDA.LatestStartTime >= '{1} 00:00:00'
                                                     ) AND DevID ={3}";
                        tempSqlAlmStatus = string.Format(tempSqlAlmStatus, beginTime.ToString("yyyy-MM-dd"), beginTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd"), parameter.DeviceID);
                        sqlAlmStatus.Append(tempSqlAlmStatus);
                        #endregion
                    }

                    var dbContext = new iCMSDbContext();
                    var alartStatList = dbContext.Database.SqlQuery<AlartDayStat>(sqlAlmStatus.ToString()).ToList();


                    beginTime = DateTime.Now;
                    endTime = DateTime.Now.AddDays(-queryDay);//本月最后一天

                    var sql = @"SELECT  COUNT(1) DeviceMaintenanceLogCount,
                                    CONVERT(nvarchar(10),UpdateDate,120) AS Day
                                    FROM    dbo.T_SYS_MAINTAIN_REPORT
                                    WHERE UpdateDate BETWEEN '{0}' AND '{1}' and ReportType=1 And IsDeleted = 0 AND IsTemplate = 0
                                    AND DeviceID ={2}
                                    GROUP BY CONVERT(nvarchar(10),UpdateDate,120)";
                    sql = string.Format(sql, endTime, beginTime, parameter.DeviceID);
                    var deviceMaintenanceLogStatList = dbContext.Database.SqlQuery<DeviceMaintenanceLogDayStatResult>(sql).ToList();

                    sql = @"SELECT  COUNT(1) DeviceDiagnosticReport,
                            CONVERT(nvarchar(10),UpdateDate,120) AS Day
                            FROM    dbo.T_SYS_DIAGNOSE_REPORT
                            WHERE UpdateDate BETWEEN '{0}' AND '{1}' And IsDeleted = 0 AND IsTemplate = 0
                            AND DeviceID ={2}
                            GROUP BY CONVERT(nvarchar(10),UpdateDate,120)";
                    sql = string.Format(sql, endTime, beginTime, parameter.DeviceID);
                    var deviceDiagnosticReportStatList = dbContext.Database.SqlQuery<DeviceDiagnosticReportDayStatResult>(sql).ToList();

                    for (int i = 0; i < queryDay; i++)
                    {
                        string day = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                        DeviceAlarmInfoList deviceAlarmInfoList = new DeviceAlarmInfoList();
                        deviceAlarmInfoList.Date = day;

                        var dstatusList = alartStatList.Where(item => item.Day == day);
                        if (dstatusList != null && dstatusList.Any())
                        {
                            deviceAlarmInfoList.DeviceStatus = dstatusList.FirstOrDefault().AlmStatus;
                        }

                        var reportList = deviceMaintenanceLogStatList.Where(item => item.Day == day);
                        if (reportList != null && reportList.Any())
                        {
                            deviceAlarmInfoList.DeviceMaintenanceLogCount = reportList.FirstOrDefault().DeviceMaintenanceLogCount;
                        }

                        var logCountList = deviceDiagnosticReportStatList.Where(item => item.Day == day);
                        if (logCountList != null && logCountList.Any())
                        {
                            deviceAlarmInfoList.DeviceDiagnosticReport = logCountList.FirstOrDefault().DeviceDiagnosticReport;
                        }
                        result.DeviceAlarmInfoList.Add(deviceAlarmInfoList);
                    }

                    #region 处理未采集状态
                    //处理未采集状态
                    var measureSiteIDList = measureSiteRepository.GetDatas<MeasureSite>(item => item.DevID == parameter.DeviceID, true).Select(item => item.MSiteID).ToList();
                    //6为设备采集信息
                    var realTimeAlarmThresholdList = realTimeAlarmThresholdRepository.GetDatas<RealTimeAlarmThreshold>(item => item.MeasureSiteThresholdType <= 6 && measureSiteIDList.Contains(item.MeasureSiteID), true).ToList();
                    //计算最小采集时间
                    if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                    {
                        DateTime minDateTime = realTimeAlarmThresholdList.Min(item => item.SamplingDate);
                        foreach (var info in result.DeviceAlarmInfoList)
                        {
                            minDateTime = Convert.ToDateTime(minDateTime.ToString("yyyy-MM-dd") + " 00:00:00");
                            DateTime dateTime = Convert.ToDateTime(info.Date);
                            //当前时间小于最小采集时间为未采集
                            if (dateTime < minDateTime)
                            {
                                info.DeviceStatus = 0;//未采集状态
                            }

                            #region 处理停机状态
                            if (device.RunStatus == (int)EnumRunStatus.Stop)
                            {
                                if (device.DeviceStopDate.HasValue)
                                {
                                    var deviceStopDate = device.DeviceStopDate.Value.AddMonths(1).AddDays(-1);//月底
                                    if (dateTime > device.DeviceStopDate)
                                    {
                                        info.DeviceStatus = 0;//未采集状态
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        //如果没有采集数据，则都为未采集状态
                        foreach (var info in result.DeviceAlarmInfoList)
                        {
                            info.DeviceStatus = 0;//未采集状态
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008891";

            }
            return baseResponse;
        }
        #endregion

        #region 获取对形貌图数据展示
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：获取对形貌图数据展示
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetWSImgDataResult> GetWSImgData(GetWSImgDataParameter parameter)
        {
            BaseResponse<GetWSImgDataResult> baseResponse = new BaseResponse<GetWSImgDataResult>();
            GetWSImgDataResult result = new GetWSImgDataResult();
            result.MSStatusInfo = new List<WSStatusInfoForWSImg>();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;
            try
            {
                if (parameter.UserID == -1)
                {
                    parameter.UserID = 1011;
                }

                #region 过滤用户管理的传感器
                var wsIDListByUser = userRelationWSRepository
                    .GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, true)
                    .Select(item => item.WSID)
                    .ToList();
                #endregion

                List<WSStatusInfoForWSImg> wsStatusInfoForWSImgList = new List<WSStatusInfoForWSImg>();
                result.MSStatusInfo = wsStatusInfoForWSImgList;
                var measureSiteList = measureSiteRepository
                    .GetDatas<MeasureSite>(item => item.DevID == parameter.DeviceID
                        && wsIDListByUser.Contains(item.WSID.Value), true);
                if (measureSiteList != null && measureSiteList.Any())
                {
                    var measureSiteMSIDList = measureSiteList.Select(item => item.MSiteID).ToList();

                    //获取实时数据信息 ,添加测点时已经添加到时实时数据表中,所以删除 王颖辉 2017-10-30
                    List<RealTimeCollectInfo> realTimeInfoList = realTimeCollectInfoRepository
                        .GetDatas<RealTimeCollectInfo>(p => p.DevID == parameter.DeviceID
                            && measureSiteMSIDList.Contains(p.MSID.Value), false)
                        .ToList();

                    var deviceName = string.Empty;

                    var device = deviceRepository.GetByKey(parameter.DeviceID);
                    if (device != null)
                    {
                        deviceName = device.DevName;
                    }

                    var wsIDList = measureSiteList.Select(item => item.WSID).ToList();
                    var wsList = wsRepository.GetDatas<WS>(item => wsIDList.Contains(item.WSID), true).ToList();

                    List<MeasureSiteType> measureSiteTypeList = cacheDICT.GetInstance()
                        .GetCacheType<MeasureSiteType>()
                        .ToList();
                    var wsTemmperatureList = tempeWSSetMSiteAlmRepository
                        .GetDatas<TempeWSSetMSiteAlm>(item => measureSiteMSIDList.Contains(item.MsiteID), true)
                        .ToList();
                    var wsVoltageList = voltageSetMSiteAlmRepository
                        .GetDatas<VoltageSetMSiteAlm>(item => measureSiteMSIDList.Contains(item.MsiteID), true)
                        .ToList();

                    var wgId = wsList[0].WGID;
                    // var ws=wsList.Where(item=>item.WSID==parameter)
                    var wg = gatewayRepository.GetByKey(wgId);
                    result.WGID = wg.WGID;
                    result.WGName = wg.WGName;
                    result.WGLinkStatus = wg.LinkStatus;
                    result.WGNO = wg.WGNO;

                    var wgType = wirelessGatewayTypeRepository.GetByKey(wg.WGType);
                    if (wgType != null)
                    {
                        int count = 0;
                        int.TryParse(wgType.Name, out count);
                        result.WSNum = count;
                    }
                    result.NetworkID = wg.NetWorkID;
                    result.Agent = wg.AgentAddress;
                    result.DeviceName = deviceName;

                    foreach (RealTimeCollectInfo realTimeInfo in realTimeInfoList)
                    {
                        WSStatusInfoForWSImg wSStatusInfoForWSImg = new WSStatusInfoForWSImg();
                        MSStatusInfo msStatusInfo = GetMSStatusInfo(realTimeInfo);

                        double? wsTemperatureAlarmValue = null;
                        double? wsTemperatureDangerValue = null;
                        double? wsVoltageAlarmValue = null;
                        double? wsVoltageDangerValue = null;

                        #region 获取传感器温度阈值
                        var temperatureAlarm = realTimeAlarmThresholdRepository.GetDatas<RealTimeAlarmThreshold>(item =>
                           item.MeasureSiteID == msStatusInfo.MSiteID &&
                           item.MeasureSiteThresholdType == (int)EnumMeasureSiteThresholdType.WSTemperature, true).FirstOrDefault();
                        if (temperatureAlarm != null)
                        {
                            wsTemperatureAlarmValue = temperatureAlarm.AlarmThresholdValue;
                            wsTemperatureDangerValue = temperatureAlarm.DangerThresholdValue;
                        }

                        #endregion

                        #region 获取电压阈值
                        var voltageInfo = realTimeAlarmThresholdRepository.GetDatas<RealTimeAlarmThreshold>(item =>
                         item.MeasureSiteID == msStatusInfo.MSiteID &&
                         item.MeasureSiteThresholdType == (int)EnumMeasureSiteThresholdType.WSVoltage, true).FirstOrDefault();
                        if (voltageInfo != null)
                        {
                            wsVoltageAlarmValue = voltageInfo.AlarmThresholdValue;
                            wsVoltageDangerValue = voltageInfo.DangerThresholdValue;
                        }

                        #endregion

                        #region 获取code,wsid
                        var measureSiteID = 0;
                        var wsId = 0;
                        var meausreSiteTypeCode = string.Empty;
                        var measureSiteName = string.Empty;
                        var measureSite = measureSiteList.Where(info => info.MSiteID == msStatusInfo.MSiteID).FirstOrDefault();
                        if (measureSite != null)
                        {
                            measureSiteID = measureSite.MSiteID;
                            var meausreSiteType = measureSiteTypeList.Where(info => info.ID == measureSite.MSiteName).FirstOrDefault();
                            if (meausreSiteType != null)
                            {
                                meausreSiteTypeCode = meausreSiteType.Code;
                                measureSiteName = meausreSiteType.Name;
                            }
                            wsId = measureSite.WSID.Value;
                        }
                        #endregion

                        var ws = wsList.Where(item => item.WSID == wsId).FirstOrDefault();
                        wSStatusInfoForWSImg.MSiteID = msStatusInfo.MSiteID;
                        wSStatusInfoForWSImg.WSID = wsId;
                        wSStatusInfoForWSImg.WSName = ws.WSName;

                        int wsAlmStatus = 0;
                        if (ws.LinkStatus == 1)
                        {
                            wsAlmStatus = ws.AlmStatus;
                        }

                        wSStatusInfoForWSImg.WSLinkStatus = ws.LinkStatus;

                        #region 有连接状态时，返回阈值
                        //有连接状态时，返回阈值
                        if (ws.LinkStatus == (int)EnumWSLinkStatus.Connect)
                        {
                            wSStatusInfoForWSImg.WSStatus = wsAlmStatus;
                            wSStatusInfoForWSImg.WSTemperatureStatus = msStatusInfo.MSWSTemperatureStatus;
                            wSStatusInfoForWSImg.WSTemperatureValue = msStatusInfo.MSWSTemperatureValue;
                            wSStatusInfoForWSImg.WSTemperatureTime = msStatusInfo.MSWSTemperatureTime;
                            wSStatusInfoForWSImg.WSTemperatureAlarmValue = wsTemperatureAlarmValue;
                            wSStatusInfoForWSImg.WSTemperatureDangerValue = wsTemperatureDangerValue;

                            wSStatusInfoForWSImg.WSBatteryVolatageValue = msStatusInfo.MSWSBatteryVolatageValue;
                            wSStatusInfoForWSImg.WSBatteryVolatageStatus = msStatusInfo.MSWSBatteryVolatageStatus;
                            wSStatusInfoForWSImg.WSBatteryVolatageTime = msStatusInfo.MSWSBatteryVolatageTime;
                            wSStatusInfoForWSImg.WSBatteryVolatageAlarmValue = wsVoltageAlarmValue;
                            wSStatusInfoForWSImg.WSBatteryVolatageDangerValue = wsVoltageDangerValue;
                        }
                        #endregion

                        wSStatusInfoForWSImg.MeasureSiteTypeCode = meausreSiteTypeCode;
                        wSStatusInfoForWSImg.WSTemperatureUnit = msStatusInfo.MSWSTemperatureUnit;
                        wSStatusInfoForWSImg.WSBatteryVolatageUnit = msStatusInfo.MSWSBatteryVolatageUnit;

                        #region 添加测量位置名称
                        wSStatusInfoForWSImg.MeasureSiteName = measureSiteName;
                        #endregion
                        wsStatusInfoForWSImgList.Add(wSStatusInfoForWSImg);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008901";
            }
            return baseResponse;
        }
        #endregion

        #region 获取当前传感器状态及报告统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：获取当前传感器状态及报告统计
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetWSStatusAndReportStatisticsResult> GetWSStatusAndReportStatistics(GetWSStatusAndReportStatisticsParameter parameter)
        {
            BaseResponse<GetWSStatusAndReportStatisticsResult> baseResponse = new BaseResponse<GetWSStatusAndReportStatisticsResult>();
            GetWSStatusAndReportStatisticsResult result = new GetWSStatusAndReportStatisticsResult();
            result.WSStatusInfoList = new List<WSStatusInfoList>();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;
            try
            {

                //获取当前ws挂靠的设备
                var deviceID = 0;
                var measureSiteID = 0;
                var measureSite = measureSiteRepository.GetDatas<MeasureSite>(item => item.WSID == parameter.WSID, true).FirstOrDefault();
                if (measureSite != null)
                {
                    deviceID = measureSite.DevID;
                    measureSiteID = measureSite.MSiteID;
                }
                if (parameter.Type == 1)
                {
                    int queryMonth = parameter.Num;

                    #region 验证数据
                    //获取当前传感器状态及报告统计,月份不正确
                    if (queryMonth < 1 || queryMonth > 12)
                    {
                        baseResponse.IsSuccessful = false;
                        baseResponse.Code = "009702";
                        return baseResponse;
                    }
                    #endregion

                    StringBuilder sqlAlmStatus = new StringBuilder();
                    DateTime beginTime = DateTime.Now;
                    DateTime endTime = DateTime.Now.AddMonths(queryMonth);//本月最后一天



                    for (int i = 0; i < queryMonth; i++)
                    {
                        //上一个月的一号
                        beginTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i);
                        endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i + 1).AddDays(-1);//本月最后一天

                        if (i != 0)
                        {
                            sqlAlmStatus.Append(" UNION ");
                        }

                        #region 报警状态 备注 报警时间 开始时间小于最晚时间，持续时间大于最早时间
                        string tempSqlAlmStatus = @" SELECT ISNULL(MAX(TSDA.AlmStatus),0)  AlmStatus,
                                                   '{0}' Month
                                            FROM dbo.T_SYS_WSN_ALMRECORD AS TSDA
                                            WHERE ( TSDA.BDate <= '{2} 23:59:59' and  TSDA.LatestStartTime >= '{1} 00:00:00')  AND  WSID={3} and DevID={4} and MSiteID={5}";
                        tempSqlAlmStatus = string.Format(tempSqlAlmStatus, beginTime.ToString("yyyy-MM"), beginTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd"), parameter.WSID, deviceID, measureSiteID);
                        sqlAlmStatus.Append(tempSqlAlmStatus);
                        #endregion
                    }

                    var dbContext = new iCMSDbContext();
                    var alartStatList = dbContext.Database.SqlQuery<WSAlartMonthStatResult>(sqlAlmStatus.ToString()).ToList();


                    beginTime = DateTime.Now;
                    endTime = DateTime.Now.AddMonths(-queryMonth);
                    var sql = @"SELECT  COUNT(1) DeviceDiagnosticReport,
                                   convert(nvarchar(7),UpdateDate,120) AS Month
                                    FROM    dbo.T_SYS_MAINTAIN_REPORT
                                    WHERE UpdateDate BETWEEN '{0}' AND '{1}' and ReportType=2 and IsDeleted = 0 AND IsTemplate = 0
                                    and DeviceID={2}
                                    GROUP BY convert(nvarchar(7),UpdateDate,120) ";
                    sql = string.Format(sql, endTime, beginTime, parameter.WSID);
                    var deviceMaintenanceLogStatList = dbContext.Database.SqlQuery<DeviceMaintenanceLogMonthStat>(sql).ToList();

                    for (int i = 0; i < queryMonth; i++)
                    {
                        string month = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i).ToString("yyyy-MM");
                        WSStatusInfoList wSStatusInfoList = new WSStatusInfoList();
                        wSStatusInfoList.Date = month.ToString();

                        var dstatusList = alartStatList.Where(item => item.Month == month);
                        if (dstatusList != null && dstatusList.Any())
                        {
                            wSStatusInfoList.WSStatus = dstatusList.FirstOrDefault().AlmStatus;
                        }

                        var logCountList = deviceMaintenanceLogStatList.Where(item => item.Month == month);
                        if (logCountList != null && logCountList.Any())
                        {
                            wSStatusInfoList.DeviceMaintenanceLogCount = logCountList.FirstOrDefault().DeviceDiagnosticReport;
                        }
                        result.WSStatusInfoList.Add(wSStatusInfoList);
                    }

                    #region 处理未采集状态
                    //6以上为WS采集信息
                    var realTimeAlarmThresholdList = realTimeAlarmThresholdRepository.GetDatas<RealTimeAlarmThreshold>(item => item.MeasureSiteThresholdType > 6 && item.MeasureSiteID == measureSite.MSiteID, true).ToList();
                    //计算最小采集时间
                    if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                    {
                        DateTime minDateTime = realTimeAlarmThresholdList.Min(item => item.SamplingDate);
                        foreach (var info in result.WSStatusInfoList)
                        {
                            DateTime dateTime = Convert.ToDateTime(info.Date + "-01");

                            minDateTime = Convert.ToDateTime(minDateTime.ToString("yyyy-MM-01") + " 00:00:00");
                            //当前时间小于最小采集时间为未采集
                            if (dateTime < minDateTime)
                            {
                                info.WSStatus = 0;//未采集状态
                            }
                        }
                    }
                    else
                    {
                        //如果没有采集数据，则都为未采集状态
                        foreach (var info in result.WSStatusInfoList)
                        {
                            info.WSStatus = 0;//未采集状态
                        }
                    }
                    #endregion
                }
                else
                {
                    //按照30天
                    DateTime beginTime = DateTime.Now;
                    DateTime endTime = DateTime.Now.AddDays(-30 * 1);

                    int queryDay = parameter.Num;
                    #region 验证数据
                    //获取当前传感器状态及报告统计,日期数不正确
                    if (queryDay < 1 || queryDay > 30)
                    {
                        baseResponse.IsSuccessful = false;
                        baseResponse.Code = "009712";
                        return baseResponse;
                    }
                    #endregion


                    //按照30天

                    StringBuilder sqlAlmStatus = new StringBuilder();
                    for (int i = 0; i < queryDay; i++)
                    {
                        //上一个月的一号
                        beginTime = DateTime.Now.AddDays(-i);
                        endTime = DateTime.Now.AddDays(-i);//本月最后一天

                        if (i != 0)
                        {
                            sqlAlmStatus.Append(" UNION ");
                        }

                        #region 报警状态
                        string tempSqlAlmStatus = @" SELECT ISNULL(MAX(TSDA.AlmStatus),0)  AlmStatus,
                                                   '{0}' Day
                                            FROM dbo.T_SYS_WSN_ALMRECORD AS TSDA
                                            WHERE (
                                                       TSDA.BDate <= '{2} 23:59:59'
                                                  and
                                                         TSDA.LatestStartTime >= '{1} 00:00:00'
                                                     ) AND  WSID={3} and DevID={4} and MSiteID={5}";
                        tempSqlAlmStatus = string.Format(tempSqlAlmStatus, beginTime.ToString("yyyy-MM-dd"), beginTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd"), parameter.WSID, deviceID, measureSiteID);
                        sqlAlmStatus.Append(tempSqlAlmStatus);
                        #endregion
                    }




                    var dbContext = new iCMSDbContext();
                    var alartStatList = dbContext.Database.SqlQuery<AlartDayStat>(sqlAlmStatus.ToString()).ToList();


                    beginTime = DateTime.Now;
                    endTime = DateTime.Now.AddDays(-queryDay);

                    var sql = @"SELECT  COUNT(1) DeviceDiagnosticReport,
                                    CONVERT(nvarchar(10),UpdateDate,120) AS Day
                                    FROM    dbo.T_SYS_MAINTAIN_REPORT
                                    WHERE UpdateDate BETWEEN '{0}' AND '{1}'  and ReportType=2 and IsDeleted = 0 AND IsTemplate = 0
                                    and DeviceID={2}
                                    GROUP BY CONVERT(nvarchar(10),UpdateDate,120)";
                    sql = string.Format(sql, endTime, beginTime, parameter.WSID);
                    var deviceMaintenanceLogStatList = dbContext.Database.SqlQuery<DeviceMaintenanceLogDayStat>(sql).ToList();


                    for (int i = 0; i < queryDay; i++)
                    {
                        string day = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                        WSStatusInfoList wSStatusInfoList = new WSStatusInfoList();
                        wSStatusInfoList.Date = day;

                        var dstatusList = alartStatList.Where(item => item.Day == day);
                        if (dstatusList != null && dstatusList.Any())
                        {
                            wSStatusInfoList.WSStatus = dstatusList.FirstOrDefault().AlmStatus;
                        }

                        var logCountList = deviceMaintenanceLogStatList.Where(item => item.Day == day);
                        if (logCountList != null && logCountList.Any())
                        {
                            wSStatusInfoList.DeviceMaintenanceLogCount = logCountList.FirstOrDefault().DeviceDiagnosticReport;
                        }
                        result.WSStatusInfoList.Add(wSStatusInfoList);
                    }

                    #region 处理未采集状态
                    //6以上为WS采集信息
                    var realTimeAlarmThresholdList = realTimeAlarmThresholdRepository.GetDatas<RealTimeAlarmThreshold>(item => item.MeasureSiteThresholdType > 6 && item.MeasureSiteID == measureSite.MSiteID, true).ToList();
                    //计算最小采集时间
                    if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                    {
                        DateTime minDateTime = realTimeAlarmThresholdList.Min(item => item.SamplingDate);
                        foreach (var info in result.WSStatusInfoList)
                        {
                            DateTime dateTime = Convert.ToDateTime(info.Date);
                            minDateTime = Convert.ToDateTime(minDateTime.ToString("yyyy-MM-dd") + " 00:00:00");
                            //当前时间小于最小采集时间为未采集
                            if (dateTime < minDateTime)
                            {
                                info.WSStatus = 0;//未采集状态
                            }
                        }
                    }
                    else
                    {
                        //如果没有采集数据，则都为未采集状态
                        foreach (var info in result.WSStatusInfoList)
                        {
                            info.WSStatus = 0;//未采集状态
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008911";

            }

            return baseResponse;

        }
        #endregion

        #region 通过WSID获取挂靠设备信息
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：通过WSID获取挂靠设备信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetDeviceInfoByWSIDResult> GetDeviceInfoByWSID(GetDeviceInfoByWSIDParameter parameter)
        {
            BaseResponse<GetDeviceInfoByWSIDResult> baseResponse = new BaseResponse<GetDeviceInfoByWSIDResult>();
            GetDeviceInfoByWSIDResult result = new GetDeviceInfoByWSIDResult();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;

            try
            {
                var measureSite = measureSiteRepository
                    .GetDatas<MeasureSite>(item => item.WSID == parameter.WSID, true)
                    .FirstOrDefault();

                //通过WSID获取挂靠设备信息,未找到测点信息
                if (measureSite == null)
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "008922";
                    return baseResponse;
                }

                var device = deviceRepository
                    .GetDatas<Device>(item => item.DevID == measureSite.DevID, true)
                    .FirstOrDefault();
                //通过WSID获取挂靠设备信息,未找到设备信息
                if (device == null)
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "008932";
                    return baseResponse;
                }

                result.DeviceID = device.DevID;
                result.DeviceName = device.DevName;
                result.DeviceType = device.DevType;
                result.DeviceNO = device.DevNO;
                string monitorTreeRoute = string.Empty;
                List<string> nameList = new List<string>();
                GetMonitorTreeName(device.MonitorTreeID, nameList);
                nameList.Reverse();
                if (nameList != null && nameList.Any())
                {
                    foreach (var name in nameList)
                    {
                        monitorTreeRoute = name + "-";
                    }
                    monitorTreeRoute = monitorTreeRoute.TrimEnd('-');
                }

                #region 获取网关名称
                var ws = wsRepository.GetByKey(parameter.WSID);
                if (ws != null)
                {
                    var wg = gatewayRepository.GetByKey(ws.WGID);
                    if (wg != null)
                    {
                        result.WG = ws.WSName;
                    }
                }
                #endregion

                result.MonitorTreeRoute = monitorTreeRoute;
                result.DeviceManage = device.PersonInCharge;
                result.AlmStatus = device.AlmStatus;
                result.UseType = device.UseType;
                result.RunStatus = device.RunStatus;
                result.OperationDate = device.OperationDate;
                result.DeviceID = device.DevID;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008941";
            }

            return baseResponse;
        }
        #endregion

        #region 通过设备ID获取传感器
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：通过设备ID获取传感器
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetWSByDeviceIDResult> GetWSByDeviceID(GetWSByDeviceIDParameter parameter)
        {
            BaseResponse<GetWSByDeviceIDResult> baseResponse = new BaseResponse<GetWSByDeviceIDResult>();
            GetWSByDeviceIDResult result = new GetWSByDeviceIDResult
            {
                WSInfoList = new List<WSInfoList>()
            };

            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;

            try
            {
                var wsIDList = new List<int>();
                var wgIDList = deviceTypeRepository
                    .GetDatas<DeviceRelationWG>(item => item.DevId == parameter.DeviceID, true)
                    .Select(item => item.WGID)
                    .ToList();
                if (parameter.UserId != -1)
                {
                    var userWSIDList = userRelationWSRepository
                        .GetDatas<UserRelationWS>(item => item.UserID == parameter.UserId, true)
                        .Select(item => item.WSID)
                        .ToList();
                    wsIDList = wsIDList.Intersect(userWSIDList).ToList();
                }
                else
                {
                    wsIDList = null;
                }
                int use = parameter.IsUse == true ? 1 : 0;
                var wsList = new List<WS>();

                //没有用过过滤
                if (wsIDList == null)
                {
                    wsList = wsRepository
                        .GetDatas<WS>(item => item.UseStatus == use && wgIDList.Contains(item.WGID), true)
                        .ToList();
                }
                else
                {
                    wsList = wsRepository
                        .GetDatas<WS>(item => wsIDList.Contains(item.WSID)
                            && item.UseStatus == use && wgIDList.Contains(item.WGID), true)
                        .ToList();
                }

                if (wsList != null && wsList.Any())
                {
                    List<SensorType> sensorTypeList = cacheDICT.GetInstance()
                        .GetCacheType<SensorType>()
                        .ToList();

                    //返回传感器的采集类型
                    Func<int, int?, int?> getSensorCollectType = (devFormType, channelId) =>
                    {
                        switch (devFormType)
                        {
                            case (int)EnumWSFormType.WireLessSensor:
                            case (int)EnumWSFormType.Triaxial:
                                {
                                    return 1;
                                }

                            case (int)EnumWSFormType.WiredSensor:
                                {
                                    if (channelId >= 0 && channelId <= 15)
                                        return 1;
                                    else if (channelId >= 16 && channelId <= 19)
                                        return 4;
                                    else if (channelId >= 20 && channelId <= 21)
                                        return 2;
                                    else
                                        return null;
                                }

                            default:
                                return null;
                        }
                    };

                    result.WSInfoList = wsList.Select(item =>
                    {
                        string sensorTypeName = string.Empty;
                        var sensor = sensorTypeList.Where(info => info.ID == item.SensorType).FirstOrDefault();
                        if (sensor != null)
                        {
                            sensorTypeName = sensor.Name;
                        }

                        return new WSInfoList
                        {
                            WSID = item.WSID,
                            WSName = item.WSName,
                            WSNO = item.WSNO,
                            SN = item.WSName,
                            SensorTypeName = sensorTypeName,
                            AlmStatus = item.AlmStatus,
                            UseStatus = item.UseStatus,
                            LinkStatus = item.LinkStatus,
                            SensorType = item.SensorType,
                            MACADDR = item.MACADDR,
                            DevFormType = item.DevFormType,
                            SensorCollectType = getSensorCollectType(item.DevFormType, item.ChannelId),
                        };
                    })
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008951";
            }

            return baseResponse;
        }
        #endregion

        #region 获取父节点名称
        /// <summary>
        /// 获取父节点名称 
        /// </summary>
        /// <param name="monitorId">监测树id</param>
        /// <param name="nameList">名称集合</param>
        private void GetMonitorTreeName(int monitorId, List<string> nameList)
        {
            var monitorTree = monitorTreeRepository.GetByKey(monitorId);
            if (monitorTree != null)
            {
                nameList.Add(monitorTree.Name);
                GetMonitorTreeName(monitorTree.PID, nameList);
            }
        }
        #endregion

        #region 设置特征值为形貌图
        /// <summary>
        /// 创建人:王颖辉 
        /// 创建时间:2017-10-30
        /// 创建内容:设置特征值为形貌图
        /// </summary>
        /// <param name="vibisignalAlarmList"></param>
        /// <param name="vibsignal"></param>
        /// <param name="eigenValueTypeList"></param>
        /// <param name="info"></param>
        private void SetEigenValueForImg(List<SignalAlmSet> vibisignalAlarmList, VibSingal vibsignal, List<EigenValueType> eigenValueTypeList, MSStatusInfo info
        )
        {
            #region 获取特征值
            var realTimeAlarmThresholdList = realTimeAlarmThresholdRepository.GetDatas<RealTimeAlarmThreshold>(item =>
                                                 item.MeasureSiteID == vibsignal.MSiteID, true).ToList();
            //获取特征值
            if (vibisignalAlarmList != null && vibisignalAlarmList.Any())
            {
                //找到此振动信号配置的特征值
                var vibisignalAlarmInfoList = vibisignalAlarmList.Where(item => item.SingalID == vibsignal.SingalID).ToList();
                if (vibisignalAlarmInfoList != null && vibisignalAlarmInfoList.Any())
                {
                    //获取特征值
                    foreach (var vibisignalAlarmInfo in vibisignalAlarmInfoList)
                    {
                        if (eigenValueTypeList != null && eigenValueTypeList.Any())
                        {
                            var gigenValueInfo = eigenValueTypeList.Where(item => item.ID == vibisignalAlarmInfo.ValueType).FirstOrDefault();
                            if (gigenValueInfo != null)
                            {
                                var vibSignalType = vibsignal.SingalType;
                                var eigenValueType = (int)EnumEigenType.EffectivityValue;
                                switch ((EnumVibSignalType)vibsignal.SingalType)
                                {
                                    case EnumVibSignalType.Accelerated:
                                        {
                                            switch (gigenValueInfo.Code)
                                            {
                                                //加速度有效值
                                                case "EIGENVALUE_20_YXZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.EffectivityValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSACCVirtualAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSACCVirtualDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                //加速度峰值
                                                case "EIGENVALUE_18_FZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.PeakValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSACCPeakAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSACCPeakDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                //加速度峰峰值
                                                case "EIGENVALUE_19_FFZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.PeakPeakValue;

                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSACCPeakPeakAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSACCPeakPeakDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                    case EnumVibSignalType.Velocity:
                                        {
                                            switch (gigenValueInfo.Code)
                                            {
                                                //速度有效值
                                                case "EIGENVALUE_21_YXZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.EffectivityValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSSpeedVirtualAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSSpeedVirtualDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                //速度峰值
                                                case "EIGENVALUE_22_FZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.PeakValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSSpeedPeakAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSSpeedPeakDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                //速度峰峰值
                                                case "EIGENVALUE_23_FFZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.PeakPeakValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSSpeedPeakPeakAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSSpeedPeakPeakDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "1Mock"://速度 - 低频能量值
                                                    break;
                                                case "2Mock"://速度 - 中频能量值
                                                    break;
                                                case "3Mock"://速度 - 高频能量值
                                                    break;
                                            }
                                        }
                                        break;
                                    case EnumVibSignalType.Envelope:
                                        {
                                            switch (gigenValueInfo.Code)
                                            {
                                                //包络峰值
                                                case "EIGENVALUE_27_FZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.PeakValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSEnvelopingPEAKAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSEnvelopingPEAKDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                //包络地毯值
                                                case "EIGENVALUE_28_DTZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.CarpetValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSEnvelopingCarpetAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSEnvelopingCarpetDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "":
                                                    //包络均值
                                                    break;
                                            }
                                        }
                                        break;
                                    case EnumVibSignalType.Displacement:
                                        {
                                            switch (gigenValueInfo.Code)
                                            {
                                                //位移有效值
                                                case "EIGENVALUE_24_YXZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.EffectivityValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSDispVirtualAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSDispVirtualDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                //位移峰值
                                                case "EIGENVALUE_26_FZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.PeakValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSDispPeakAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSDispPeakDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                //位移峰峰值
                                                case "EIGENVALUE_25_FFZ":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.PeakPeakValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSDispPeakPeakAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSDispPeakPeakDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                    case EnumVibSignalType.LQ:
                                        {
                                            #region 轴承状态
                                            switch (gigenValueInfo.Code)
                                            {
                                                //轴承状态
                                                case "EIGENVALUE_29_ZCZT":
                                                    {
                                                        eigenValueType = (int)EnumEigenType.LQValue;
                                                        if (realTimeAlarmThresholdList != null && realTimeAlarmThresholdList.Any())
                                                        {
                                                            var realTimeAlarmThreshold = realTimeAlarmThresholdList.Where(item => item.MeasureSiteThresholdType == vibSignalType &&
                                                              item.EigenValueType == eigenValueType).FirstOrDefault();
                                                            if (realTimeAlarmThreshold != null)
                                                            {
                                                                info.MSLQAlarmValue = realTimeAlarmThreshold.AlarmThresholdValue;
                                                                info.MSLQDangerValue = realTimeAlarmThreshold.DangerThresholdValue;
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                            #endregion
                                        }
                                        break;

                                }


                            }
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region 获取设备数量
        /// <summary>
        /// 获取设备数量
        /// </summary>
        /// <param name="parameter"></param>
        public BaseResponse<ResponseResult> GetDeviceCount(GetDeviceCountParameter parameter)
        {
            BaseResponse<ResponseResult> baseResponse = new BaseResponse<ResponseResult>();
            ResponseResult responseResult = new ResponseResult();
            responseResult.INT = 0;
            baseResponse.Result = responseResult;
            baseResponse.IsSuccessful = false;
            #region
            //获取设备数量,用户id不正确
            if (parameter.UserID < -1 || parameter.UserID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009783";
                return baseResponse;
            }

            //获取设备数量,传感器id不正确
            if (parameter.WSID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009793";
                return baseResponse;
            }
            #endregion

            try
            {
                var deviceInfoId = 0;
                var measureSiteInfo = measureSiteRepository.GetDatas<MeasureSite>(item => item.WSID == parameter.WSID, true).FirstOrDefault();
                if (measureSiteInfo == null)
                {
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }
                deviceInfoId = measureSiteInfo.DevID;


                var deviceIDList = userRalationDeviceRepository.GetDatas<UserRalationDevice>(item => item.UserID == parameter.UserID, true).Select(item => item.DevId).ToList();
                if (deviceIDList != null)
                {

                    if (deviceIDList.Contains(deviceInfoId))
                    {
                        baseResponse.IsSuccessful = true;
                        responseResult.INT = 1;
                        baseResponse.Result = responseResult;
                    }
                    else
                    {
                        baseResponse.IsSuccessful = true;
                        responseResult.INT = 0;
                        baseResponse.Result = responseResult;
                    }
                }
                else
                {
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009803";
            }
            return baseResponse;
        }
        #endregion

        #region 获取WS数量
        /// <summary>
        /// 获取WS数量
        /// </summary>
        /// <param name="parameter"></param>
        public BaseResponse<ResponseResult> GetWSCount(GetWSCountParameter parameter)
        {
            BaseResponse<ResponseResult> baseResponse = new BaseResponse<ResponseResult>();
            ResponseResult responseResult = new ResponseResult();
            responseResult.INT = 0;
            baseResponse.Result = responseResult;
            baseResponse.IsSuccessful = false;
            #region
            //获取WS数量,用户id不正确
            if (parameter.UserID < -1 || parameter.UserID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009813";
                return baseResponse;
            }

            //获取WS数量,设备id不正确
            if (parameter.DeviceID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009823";
                return baseResponse;
            }
            #endregion

            try
            {
                var wsIDListByDeviceID = measureSiteRepository.GetDatas<MeasureSite>(item => item.DevID == parameter.DeviceID, true).Select(item => item.WSID.Value).ToList();
                if (wsIDListByDeviceID == null || !wsIDListByDeviceID.Any())
                {
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }

                var wsIDList = userRelationWSRepository.GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, true).Select(item => item.WSID).ToList();
                if (wsIDList != null && wsIDList.Any())
                {
                    wsIDList = wsIDList.Intersect(wsIDListByDeviceID).ToList();
                    baseResponse.IsSuccessful = true;
                    responseResult.INT = wsIDList.Count;
                    baseResponse.Result = responseResult;
                }
                else
                {
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009833";
            }
            return baseResponse;
        }
        #endregion
    }
    #endregion
}