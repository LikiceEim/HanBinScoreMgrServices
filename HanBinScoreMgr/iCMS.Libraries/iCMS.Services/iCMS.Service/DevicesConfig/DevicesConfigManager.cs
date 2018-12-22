/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Service.DevicesConfig
 * 文件名：  IDevicesConfigManager
 * 创建人：  LF
 * 创建时间：2016-10-21
 * 描述：设备树业务处理类
 *
 * 修改人：张辽阔
 * 修改时间：2016-11-11
 * 描述：增加错误编码
/************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.ComponentModel;
using System.Text;

using Microsoft.Practices.Unity;

using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.DevicesConfig;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree;
using iCMS.Common.Component.Data.Response.Common;
using iCMS.Common.Component.Data.Response.DevicesConfig;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.Repository;
using iCMS.Common.Component.Data.Base.DB;
using iCMS.Service.Common;
using iCMS.Common.Component.Data.Response.DiagnosticControl;
using iCMS.Common.Component.Data.Request.DiagnosticControl;

namespace iCMS.Service.Web.DevicesConfig
{
    #region 设备树业务处理类
    /// <summary>
    /// 设备树业务处理类
    /// </summary>
    public class DevicesConfigManager : IDevicesConfigManager
    {
        #region 变更
        Stack nodeID = new Stack();
        string treeNodeStr = "";
        string treeNodeIDStr = "";
        // private readonly ICategoryService _categoryService;
        private readonly IRepository<Device> deviceRepository;
        private readonly IRepository<MeasureSite> measureSiteRepository;
        private readonly IRepository<VibSingal> vibSignalRepository;
        private readonly IRepository<MonitorTree> monitorTreeRepository;
        private readonly IRepository<MonitorTreeProperty> monitorTreePropertyRepository;
        private readonly IRepository<MonitorTreeType> monitorTreeTypeRepository;
        private readonly IRepository<WS> wsRepository;
        private readonly IRepository<Bearing> bearingRepository;
        private readonly IRepository<VibratingSingalType> vibratingSingalTypeRepository;
        private readonly IRepository<RealTimeCollectInfo> realTimeCollectInfoRepository;
        private readonly IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository;
        private readonly IRepository<VoltageSetMSiteAlm> voltageSetMSiteAlmRepository;
        private readonly IRepository<TempeWSSetMSiteAlm> tempeWSSetMSiteAlmRepository;
        private readonly IRepository<SignalAlmSet> signalAlmSetRepository;
        private readonly IRepository<DevAlmRecord> devAlmRecordRepository;
        private readonly IRepository<WSnAlmRecord> wsnAlmRecordRepository;
        private readonly ICacheDICT cacheDICT;

        [Dependency]
        public IRepository<UserRalationDevice> userRalationDeviceRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<UserRelationWS> userRelationWSRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<Operation> operationRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<MeasureSiteBearing> measureSiteBearingRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<Factory> factoryRepository
        {
            get;
            set;
        }

        [Dependency]
        public IRepository<Gateway> gatewayRepository
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-04
        /// 创建记录：设备关联网关数据访问对象
        /// </summary>
        [Dependency]
        public IRepository<DeviceRelationWG> deviceRelationWGRepository { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-09
        /// 创建记录：转速测量定义数据访问对象
        /// </summary>
        [Dependency]
        public IRepository<SpeedSamplingMDF> speedSamplingMDFRepository { get; set; }

        #endregion

        #region 设备配置构造函数
        /// <summary>
        /// 设备配置构造函数
        /// </summary>
        public DevicesConfigManager(IRepository<Device> deviceRepository,
            IRepository<MeasureSite> measureSiteRepository,
            IRepository<VibSingal> vibSignalRepository,
            IRepository<MonitorTree> monitorTreeRepository,
            IRepository<MonitorTreeProperty> monitorTreePropertyRepository,
            IRepository<MonitorTreeType> monitorTreeTypeRepository,
            IRepository<WS> wsRepository,
            IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository,
            IRepository<VoltageSetMSiteAlm> voltageSetMSiteAlmRepository,
            IRepository<TempeWSSetMSiteAlm> tempeWSSetMSiteAlmRepository,
            IRepository<SignalAlmSet> signalAlmSetRepository,
            IRepository<DevAlmRecord> devAlmRecordRepository,
            IRepository<WSnAlmRecord> wsnAlmRecordRepository,
            IRepository<Bearing> bearingRepository,
            IRepository<VibratingSingalType> vibratingSingalTypeRepository,
            IRepository<RealTimeCollectInfo> realTimeCollectInfoRepository,
            ICacheDICT cacheDICT)
        {
            this.deviceRepository = deviceRepository;
            this.measureSiteRepository = measureSiteRepository;
            this.vibSignalRepository = vibSignalRepository;
            this.monitorTreeRepository = monitorTreeRepository;
            this.monitorTreePropertyRepository = monitorTreePropertyRepository;
            this.monitorTreeTypeRepository = monitorTreeTypeRepository;
            this.tempeDeviceSetMSiteAlmRepository = tempeDeviceSetMSiteAlmRepository;
            this.wsRepository = wsRepository;
            this.voltageSetMSiteAlmRepository = voltageSetMSiteAlmRepository;
            this.tempeWSSetMSiteAlmRepository = tempeWSSetMSiteAlmRepository;
            this.signalAlmSetRepository = signalAlmSetRepository;
            this.devAlmRecordRepository = devAlmRecordRepository;
            this.wsnAlmRecordRepository = wsnAlmRecordRepository;
            this.bearingRepository = bearingRepository;
            this.vibratingSingalTypeRepository = vibratingSingalTypeRepository;
            this.realTimeCollectInfoRepository = realTimeCollectInfoRepository;
            this.cacheDICT = cacheDICT;
        }
        #endregion

        #region 设备树设置

        #region 获取设备下拉列表
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-11-10
        /// 创建记录：获取设备下拉列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetDeviceSelectListResult> GetDeviceSelectList(GetDeviceSelectListParameter parameter)
        {
            BaseResponse<GetDeviceSelectListResult> response = new BaseResponse<GetDeviceSelectListResult>();
            GetDeviceSelectListResult result = new GetDeviceSelectListResult();
            List<DeviceSelect> DeviceSelectList = new List<DeviceSelect>();

            try
            {
                using (var dataContext = new iCMSDbContext())
                {
                    if (parameter.UserID == -1)
                    { parameter.UserID = 1011; }
                    DeviceSelectList = (from urd in dataContext.UserRalationDevice
                                        join d in dataContext.Device on urd.DevId equals d.DevID
                                        where urd.UserID == parameter.UserID
                                        select new DeviceSelect
                                        {
                                            DeviceID = d.DevID,
                                            DeviceName = d.DevName
                                        }).Distinct().ToList();

                    response.IsSuccessful = true;
                    result.DeviceSelectList = DeviceSelectList;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "009631";
                return response;
            }
        }
        #endregion

        #region 获取设备类型数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取设备类型数据信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<DeviceTypeInfoResult> GetDeviceTypeForDeviceTree(BaseRequest parameter)
        {
            BaseResponse<DeviceTypeInfoResult> response = new BaseResponse<DeviceTypeInfoResult>();

            try
            {
                DeviceTypeInfoResult deviceTypeInfoResult = new DeviceTypeInfoResult();
                deviceTypeInfoResult.CommonInfos = new List<CommonInfo>();
                List<DeviceType> deviceTypes = cacheDICT.GetInstance()
                    .GetCacheType<DeviceType>()
                    .ToList();
                foreach (var deviceType in deviceTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = deviceType.ID,
                        PID = null,
                        Name = deviceType.Name,
                        Describe = deviceType.Describe,
                        IsUsable = deviceType.IsUsable,
                        IsDefault = deviceType.IsDefault,
                        AddDate = deviceType.AddDate.ToString()
                    };

                    deviceTypeInfoResult.CommonInfos.Add(commonInfo);
                }

                response.IsSuccessful = true;
                response.Result = deviceTypeInfoResult;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "001771";
                return response;
            }
        }
        #endregion

        #region 获取测量位置类型数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取测量位置类型数据信息
        /// </summary>
        /// <param name="parameter">获取测量位置类型数据信息参数</param>
        /// <returns></returns>
        public BaseResponse<MSiteTypeInfoResult> GetMSiteTypeForDeviceTree(MSiteTypeForDeviceTreeParameter parameter)
        {
            BaseResponse<MSiteTypeInfoResult> response = new BaseResponse<MSiteTypeInfoResult>();

            try
            {
                MSiteTypeInfoResult mSiteTypeInfoResult = new MSiteTypeInfoResult();
                mSiteTypeInfoResult.CommonInfos = new List<CommonInfo>();
                List<MeasureSiteType> msTypes = cacheDICT.GetInstance()
                    .GetCacheType<MeasureSiteType>(t => t.DeviceTypeID == parameter.DeviceTypeID)
                    .ToList();
                foreach (var msType in msTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = msType.ID,
                        PID = null,
                        Name = msType.Name,
                        Describe = msType.Describe,
                        IsUsable = msType.IsUsable,
                        IsDefault = msType.IsDefault,
                        AddDate = msType.AddDate.ToString()
                    };

                    mSiteTypeInfoResult.CommonInfos.Add(commonInfo);
                }

                response.IsSuccessful = true;
                response.Result = mSiteTypeInfoResult;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "001781";
                return response;
            }
        }
        #endregion

        #region 获取测量位置监测类型数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取测量位置监测类型数据信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<MSMTTypeInfoResult> GetMSMTTypeForDeviceTree(BaseRequest parameter)
        {
            BaseResponse<MSMTTypeInfoResult> response = new BaseResponse<MSMTTypeInfoResult>();

            try
            {
                MSMTTypeInfoResult msmtTypeInfoResult = new MSMTTypeInfoResult();
                msmtTypeInfoResult.CommonInfos = new List<CommonInfo>();
                List<MeasureSiteMonitorType> msmtTypes = cacheDICT.GetInstance()
                    .GetCacheType<MeasureSiteMonitorType>()
                    .ToList();
                foreach (var msmtType in msmtTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = msmtType.ID,
                        PID = null,
                        Name = msmtType.Name,
                        Describe = msmtType.Describe,
                        IsUsable = msmtType.IsUsable,
                        IsDefault = msmtType.IsDefault,
                        AddDate = msmtType.AddDate.ToString()
                    };

                    msmtTypeInfoResult.CommonInfos.Add(commonInfo);
                }

                response.IsSuccessful = true;
                response.Result = msmtTypeInfoResult;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "001791";
                return response;
            }
        }
        #endregion

        #region 获取振动信号类型数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取振动信号类型数据信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<VibSignalTypeForDeviceTreeResult> GetVibSignalTypeForDeviceTree(BaseRequest parameter)
        {
            BaseResponse<VibSignalTypeForDeviceTreeResult> response = new BaseResponse<VibSignalTypeForDeviceTreeResult>();

            try
            {
                VibSignalTypeForDeviceTreeResult vibSignalTypeForDeviceTreeResult = new VibSignalTypeForDeviceTreeResult();
                vibSignalTypeForDeviceTreeResult.CommonInfos = new List<CommonInfo>();
                List<VibratingSingalType> vibTypes = cacheDICT.GetInstance()
                    .GetCacheType<VibratingSingalType>()
                    .ToList();
                foreach (var vibType in vibTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = vibType.ID,
                        PID = null,
                        Name = vibType.Name,
                        Describe = vibType.Describe,
                        IsUsable = vibType.IsUsable,
                        IsDefault = vibType.IsDefault,
                        AddDate = vibType.AddDate.ToString()
                    };

                    vibSignalTypeForDeviceTreeResult.CommonInfos.Add(commonInfo);
                }

                response.IsSuccessful = true;
                response.Result = vibSignalTypeForDeviceTreeResult;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "001801";
                return response;
            }
        }
        #endregion

        #region 获取波形属性(波长，上限频率，下限频率)类型数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取波形属性(波长，上限频率，下限频率)类型数据信息
        /// </summary>
        /// <param name="parameter">获取波形属性(波长，上限频率，下限频率)类型数据信息参数</param>
        /// <returns></returns>
        public BaseResponse<WaveAttrTypeForDeviceTreeResult> GetWaveAttrTypeForDeviceTree(WaveAttrTypeForDeviceTreeParameter parameter)
        {
            BaseResponse<WaveAttrTypeForDeviceTreeResult> response = new BaseResponse<WaveAttrTypeForDeviceTreeResult>();

            try
            {
                WaveAttrTypeForDeviceTreeResult waveAttrTypeForDeviceTreeResult = new WaveAttrTypeForDeviceTreeResult();
                //波长
                waveAttrTypeForDeviceTreeResult.WaveLengthTypeInfoList = cacheDICT.GetInstance()
                    .GetCacheType<WaveLengthValues>(p => p.VibratingSignalTypeID == parameter.VibratingSignalTypeID)
                    .Select(p => new WaveLengthTypeInfo
                    {
                        ID = p.ID,
                        WaveLengthValue = p.WaveLengthValue,
                        IsUsable = p.IsUsable,
                    })
                    .ToList();
                //上限
                waveAttrTypeForDeviceTreeResult.WaveUpperLimitTypeInfoList = cacheDICT.GetInstance()
                    .GetCacheType<WaveUpperLimitValues>(p => p.VibratingSignalTypeID == parameter.VibratingSignalTypeID)
                    .Select(p => new WaveUpperLimitTypeInfo
                    {
                        ID = p.ID,
                        WaveUpperLimitValue = p.WaveUpperLimitValue,
                        IsUsable = p.IsUsable,
                    })
                    .ToList();
                //下限
                waveAttrTypeForDeviceTreeResult.WaveLowerLimitTypeInfoList = cacheDICT.GetInstance()
                    .GetCacheType<WaveLowerLimitValues>(p => p.VibratingSignalTypeID == parameter.VibratingSignalTypeID)
                    .Select(p => new WaveLowerLimitTypeInfo
                    {
                        ID = p.ID,
                        WaveLowerLimitValue = p.WaveLowerLimitValue,
                        IsUsable = p.IsUsable,
                    })
                    .ToList();

                response.IsSuccessful = true;
                response.Result = waveAttrTypeForDeviceTreeResult;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "001811";
                return response;
            }
        }
        #endregion

        #region 获取特征值类型数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取特征值类型数据信息
        /// </summary>
        /// <param name="parameter">获取特征值类型数据信息参数</param>
        /// <returns></returns>
        public BaseResponse<EigenvalueTypeForDeviceTreeResult> GetEigenvalueTypeForDeviceTree(EigenvalueTypeForDeviceTreeParameter parameter)
        {
            BaseResponse<EigenvalueTypeForDeviceTreeResult> response = new BaseResponse<EigenvalueTypeForDeviceTreeResult>();

            try
            {
                EigenvalueTypeForDeviceTreeResult eigenvalueTypeForDeviceTreeResult = new EigenvalueTypeForDeviceTreeResult();
                eigenvalueTypeForDeviceTreeResult.CommonInfos = new List<CommonInfo>();
                List<EigenValueType> eigenValueTypes = cacheDICT.GetInstance()
                    .GetCacheType<EigenValueType>(
                        t => t.VibratingSignalTypeID == parameter.VibratingSignalTypeID)
                    .ToList();
                foreach (var eigenValueType in eigenValueTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = eigenValueType.ID,
                        //振动信号ID
                        PID = parameter.VibratingSignalTypeID,
                        Name = eigenValueType.Name,
                        Describe = eigenValueType.Describe,
                        IsUsable = eigenValueType.IsUsable,
                        IsDefault = eigenValueType.IsDefault,
                        AddDate = eigenValueType.AddDate.ToString()
                    };

                    eigenvalueTypeForDeviceTreeResult.CommonInfos.Add(commonInfo);
                }

                response.IsSuccessful = true;
                response.Result = eigenvalueTypeForDeviceTreeResult;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "001821";
                return response;
            }
        }
        #endregion

        #region 获取设备和测量位置数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取设备和测量位置数据信息
        /// </summary>
        /// <param name="parameter">获取设备和测量位置数据信息参数</param>
        /// <returns></returns>
        public BaseResponse<DeviceAndMSiteDataForDeviceTreeResult> GetDeviceAndMSiteDataForDeviceTree(DeviceAndMSiteDataForDeviceTreeParameter parameter)
        {
            var begTime = DateTime.Now;
            BaseResponse<DeviceAndMSiteDataForDeviceTreeResult> response =
                new BaseResponse<DeviceAndMSiteDataForDeviceTreeResult>();

            try
            {
                #region 获取设备和测量位置数据信息,用户ID不正确
                if (parameter.UserID < -1 || parameter.UserID == 0)
                {
                    response.IsSuccessful = false;
                    response.Code = "009452";
                    return response;
                }
                #endregion
                DeviceAndMSiteDataForDeviceTreeResult deviceAndMSiteDataForDeviceTreeResult =
                    new DeviceAndMSiteDataForDeviceTreeResult();
                deviceAndMSiteDataForDeviceTreeResult.DeviceInfoList = new List<DeviceInfo>();
                deviceAndMSiteDataForDeviceTreeResult.MeasureInfoList = new List<MeasureInfo>();
                int total = 0;
                var deviceList = new List<Device>();

                ListSortDirection sortOrder = parameter.Order.ToLower().Equals("asc")
                    ? ListSortDirection.Ascending
                    : ListSortDirection.Descending;
                PropertySortCondition[] sortList = new PropertySortCondition[]
                {
                    new PropertySortCondition(parameter.Sort, sortOrder),
                    new PropertySortCondition("DevID", sortOrder),
                };

                if (parameter.Page == -1)
                {
                    if (parameter.UserID > 0)
                    {
                        var deviceIdList = userRalationDeviceRepository
                            .GetDatas<UserRalationDevice>(item => item.UserID == parameter.UserID, false)
                            .Select(item => item.DevId);
                        deviceList = deviceRepository
                          .GetDatas<Device>(p => deviceIdList.Contains(p.DevID), false)
                          .ToList();
                    }
                    else
                    {
                        deviceList = deviceRepository.GetDatas<Device>(p => true, false).ToList();
                    }

                    total = deviceList.Count();
                }
                else
                {
                    deviceList =
                        (from p in new iCMSDbContext().Device
                         select p)
                        .Where(parameter.Page, parameter.PageSize, out total, null)
                        .ToList();

                    if (parameter.UserID > 0)
                    {
                        var deviceIdList = userRalationDeviceRepository
                            .GetDatas<UserRalationDevice>(item => item.UserID == parameter.UserID, false)
                            .Select(item => item.DevId);
                        deviceList =
                            (from p in new iCMSDbContext().Device
                             where deviceIdList.Contains(p.DevID)
                             select p)
                            .Where(parameter.Page, parameter.PageSize, out total, sortList)
                            .ToList();
                    }
                    else
                    {
                        deviceList =
                            (from p in new iCMSDbContext().Device
                             select p)
                            .Where(parameter.Page, parameter.PageSize, out total, sortList)
                            .ToList();
                    }
                }

                var MonitorTreeIDList = new List<int>();

                Dictionary<int, List<string>> objectList = new Dictionary<int, List<string>>();

                var devIDList = deviceList.Select(item => item.DevID).ToList();
                var devRelaWGList = deviceRelationWGRepository
                    .GetDatas<DeviceRelationWG>(item => devIDList.Contains(item.DevId), false)
                    .Select(item => new { item.WGID, item.DevId, })
                    .ToList();
                var wgIDList = devRelaWGList.Select(item => item.WGID).ToList();
                var wgList = gatewayRepository
                    .GetDatas<Gateway>(item => wgIDList.Contains(item.WGID), true)
                    .ToList();
                foreach (var dev in deviceList)
                {
                    treeNodeStr = "";
                    treeNodeIDStr = "";

                    var list = new List<string>();

                    //如果已经存在，则进行取，如果不存在，则添加
                    if (MonitorTreeIDList.Contains(dev.MonitorTreeID))
                    {
                        list = objectList[dev.MonitorTreeID] as List<string>;
                    }
                    else
                    {
                        list = GetTreeNode(dev.MonitorTreeID);
                        objectList.Add(dev.MonitorTreeID, list);
                        MonitorTreeIDList.Add(dev.MonitorTreeID);
                    }

                    List<int> tempWGIDList = devRelaWGList
                        .Where(item => item.DevId == dev.DevID)
                        .Select(item => item.WGID)
                        .ToList();
                    List<WGSimpleInfoResult> wgInfoList = wgList
                        .Where(item => tempWGIDList.Contains(item.WGID))
                        .Select(item => new WGSimpleInfoResult
                        {
                            WGID = item.WGID,
                            WGName = item.WGName,
                            DevFormType = item.DevFormType,
                        })
                        .ToList();

                    string deviceTypeName = string.Empty;
                    var deviceType = cacheDICT.GetInstance()
                        .GetCacheType<DeviceType>(o => o.ID == dev.DevType)
                        .SingleOrDefault();
                    if (deviceType != null)
                    {
                        deviceTypeName = deviceType.Name;
                    }
                    deviceAndMSiteDataForDeviceTreeResult.DeviceInfoList.Add(new DeviceInfo()
                    {
                        AddDate = dev.AddDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        AlmStatus = dev.AlmStatus.ToString(),
                        CouplingType = dev.CouplingType,
                        DevID = dev.DevID,
                        DevMadeDate = dev.DevMadeDate.HasValue ? dev.DevMadeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                        DevManager = dev.DevManager,
                        DevManufacturer = dev.DevManufacturer,
                        DevMark = dev.DevMark,
                        DevModel = dev.DevModel,
                        DevName = dev.DevName,
                        DevNO = dev.DevNO,
                        DevPic = dev.DevPic,
                        DevSDate = dev.DevSDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        DevType = dev.DevType,
                        HeadOfDelivery = dev.HeadOfDelivery.HasValue ? dev.HeadOfDelivery.Value.ToString() : "",
                        Height = dev.Height.HasValue ? dev.Height.Value.ToString() : "",
                        ImageID = dev.ImageID,
                        LastCheckDate = dev.LastCheckDate.HasValue ? dev.LastCheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                        WID = dev.DevID,
                        Latitude = dev.Latitude.HasValue ? dev.Latitude.Value.ToString() : "",
                        Length = dev.Length.HasValue ? dev.Length.Value.ToString() : "",
                        Longitude = dev.Longitude.HasValue ? dev.Longitude.Value.ToString() : "",
                        Media = dev.Media,
                        MonitorTreeID = dev.MonitorTreeID,
                        DeviceTypeName = deviceTypeName,
                        OutputMaxPressure = dev.OutputMaxPressure.HasValue ? dev.OutputMaxPressure.Value.ToString() : "",
                        outputVolume = dev.outputVolume.HasValue ? dev.outputVolume.Value.ToString() : "",
                        PersonInCharge = dev.PersonInCharge,
                        PersonInChargeTel = dev.PersonInChargeTel,
                        Position = dev.Position,
                        Power = dev.Power.HasValue ? dev.Power.Value.ToString() : "",

                        RatedCurrent = dev.RatedCurrent.HasValue ? dev.RatedCurrent.Value.ToString() : "",
                        RatedVoltage = dev.RatedVoltage.HasValue ? dev.RatedVoltage.Value.ToString() : "",
                        Rotate = dev.Rotate,
                        RunStatus = dev.RunStatus,
                        SensorSize = dev.SensorSize,
                        StatusCriticalValue = dev.StatusCriticalValue,
                        UseType = dev.UseType,
                        Width = dev.Width.HasValue ? dev.Width.Value.ToString() : "",
                        WGInfo = wgInfoList,
                        OperationDate = dev.OperationDate,
                        Type = 0,
                        TypeName = "设备",
                        TreeNode = list[0],
                        TreeNodeID = list[1]
                    });
                }

                if (deviceList != null && deviceList.Any())
                {
                    var deviceIDList = deviceList.Select(item => item.DevID).ToList();
                    deviceAndMSiteDataForDeviceTreeResult.MeasureInfoList.AddRange(GetMeasureSiteInfo(deviceIDList));
                }

                deviceAndMSiteDataForDeviceTreeResult.Total = total;

                response.IsSuccessful = true;
                response.Result = deviceAndMSiteDataForDeviceTreeResult;
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001831";
                return response;
            }
        }
        #endregion

        #region 获取设备和测量位置数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取设备和测量位置数据信息
        /// </summary>
        /// <param name="parameter">获取设备和测量位置数据信息参数</param>
        /// <returns></returns>
        public BaseResponse<GetDeviceAndMSiteDataByDevIdForDeviceTreeResult> GetDeviceAndMSiteDataByDevIdForDeviceTree(DeviceAndMSiteDataByDevIdForDeviceTreeParameter parameter)
        {
            BaseResponse<GetDeviceAndMSiteDataByDevIdForDeviceTreeResult> response = new BaseResponse<GetDeviceAndMSiteDataByDevIdForDeviceTreeResult>();

            try
            {
                GetDeviceAndMSiteDataByDevIdForDeviceTreeResult deviceAndMSiteDataForDeviceTreeResult = new GetDeviceAndMSiteDataByDevIdForDeviceTreeResult();
                deviceAndMSiteDataForDeviceTreeResult.DeviceInfoList = new List<DeviceInfo>();
                deviceAndMSiteDataForDeviceTreeResult.MeasureInfoList = new List<MeasureInfo>();
                int deviceTotal = 0;
                var deviceList = deviceRepository
                    .GetDatas<Device>(obj => obj.DevID == parameter.DevId, false)
                    .ToList();
                deviceTotal = deviceList.Count;

                var devIDList = deviceList.Select(item => item.DevID).ToList();
                var devRelaWGList = deviceRelationWGRepository
                    .GetDatas<DeviceRelationWG>(item => devIDList.Contains(item.DevId), false)
                    .Select(item => new { item.WGID, item.DevId, })
                    .ToList();
                var wgIDList = devRelaWGList.Select(item => item.WGID).ToList();
                var wgList = gatewayRepository
                    .GetDatas<Gateway>(item => wgIDList.Contains(item.WGID), true)
                    .ToList();
                foreach (var dev in deviceList)
                {
                    treeNodeStr = "";
                    treeNodeIDStr = "";
                    var list = GetTreeNode(dev.MonitorTreeID);
                    string deviceTypeName = string.Empty;
                    var deviceType = cacheDICT.GetInstance()
                        .GetCacheType<DeviceType>(tObj => tObj.ID == dev.DevType)
                        .FirstOrDefault();
                    if (deviceType != null)
                    {
                        deviceTypeName = deviceType.Name;
                    }

                    List<int> tempWGIDList = devRelaWGList
                        .Where(item => item.DevId == dev.DevID)
                        .Select(item => item.WGID)
                        .ToList();
                    List<WGSimpleInfoResult> wgInfoList = wgList
                        .Where(item => tempWGIDList.Contains(item.WGID))
                        .Select(item => new WGSimpleInfoResult
                        {
                            WGID = item.WGID,
                            WGName = item.WGName,
                            DevFormType = item.DevFormType,
                        })
                        .ToList();

                    deviceAndMSiteDataForDeviceTreeResult.DeviceInfoList.Add(new DeviceInfo()
                    {
                        AddDate = dev.AddDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        AlmStatus = dev.AlmStatus.ToString(),
                        CouplingType = dev.CouplingType,
                        DevID = dev.DevID,
                        DevMadeDate = dev.DevMadeDate.HasValue ? dev.DevMadeDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                        DevManager = dev.DevManager,
                        DevManufacturer = dev.DevManufacturer,
                        DevMark = dev.DevMark,
                        DevModel = dev.DevModel,
                        DevName = dev.DevName,
                        DevNO = dev.DevNO,
                        DevPic = dev.DevPic,
                        DevSDate = dev.DevSDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        DevType = dev.DevType,
                        HeadOfDelivery = dev.HeadOfDelivery.HasValue ? dev.HeadOfDelivery.Value.ToString() : "",
                        Height = dev.Height.HasValue ? dev.Height.Value.ToString() : "",
                        ImageID = dev.ImageID,
                        LastCheckDate = dev.LastCheckDate.HasValue ? dev.LastCheckDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                        WID = dev.DevID,
                        Latitude = dev.Latitude.HasValue ? dev.Latitude.Value.ToString() : "",
                        Length = dev.Length.HasValue ? dev.Length.Value.ToString() : "",
                        Longitude = dev.Longitude.HasValue ? dev.Longitude.Value.ToString() : "",
                        Media = dev.Media,
                        MonitorTreeID = dev.MonitorTreeID,
                        DeviceTypeName = deviceTypeName,
                        OutputMaxPressure = dev.OutputMaxPressure.HasValue ? dev.OutputMaxPressure.Value.ToString() : "",
                        outputVolume = dev.outputVolume.HasValue ? dev.outputVolume.Value.ToString() : "",
                        PersonInCharge = dev.PersonInCharge,
                        PersonInChargeTel = dev.PersonInChargeTel,
                        Position = dev.Position,
                        Power = dev.Power.HasValue ? dev.Power.Value.ToString() : "",

                        RatedCurrent = dev.RatedCurrent.HasValue ? dev.RatedCurrent.Value.ToString() : "",
                        RatedVoltage = dev.RatedVoltage.HasValue ? dev.RatedVoltage.Value.ToString() : "",
                        Rotate = dev.Rotate,
                        RunStatus = dev.RunStatus,
                        SensorSize = dev.SensorSize,
                        StatusCriticalValue = dev.StatusCriticalValue,
                        UseType = dev.UseType,
                        Width = dev.Width.HasValue ? dev.Width.Value.ToString() : "",
                        WGInfo = wgInfoList,
                        OperationDate = dev.OperationDate,
                        Type = 0,
                        TypeName = "设备",
                        TreeNode = list[0],
                        TreeNodeID = list[1],
                    });

                    var deviceIDList = deviceList.Select(item => item.DevID).ToList();
                    deviceAndMSiteDataForDeviceTreeResult.MeasureInfoList.AddRange(
                        GetMeasureSiteInfo(deviceIDList, parameter.DAQStyle));
                }

                response.IsSuccessful = true;
                response.Result = deviceAndMSiteDataForDeviceTreeResult;
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001841";
                return response;
            }
        }
        #endregion

        #region 获取测量位置报警和振动信号数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取测量位置报警和振动信号数据信息
        /// </summary>
        /// <param name="parameter">获取测量位置报警和振动信号数据信息参数</param>
        /// <returns></returns>
        public BaseResponse<MSiteAlmAndSignalDataForDeviceTreeResult> GetMSiteAlmAndSignalDataForDeviceTree(MSiteAlmAndSignalDataForDeviceTreeParameter parameter)
        {
            BaseResponse<MSiteAlmAndSignalDataForDeviceTreeResult> response = new BaseResponse<MSiteAlmAndSignalDataForDeviceTreeResult>();

            try
            {
                MSiteAlmAndSignalDataForDeviceTreeResult mSiteAlmAndSignalDataForDeviceTreeResult = new MSiteAlmAndSignalDataForDeviceTreeResult();
                mSiteAlmAndSignalDataForDeviceTreeResult.VibSignalInfoList = new List<VibSignalInfo>();
                mSiteAlmAndSignalDataForDeviceTreeResult.MSiteAlmInfoList = new List<MSiteAlmInfo>();

                //获取振动信号信息
                List<VibSingal> vibSingalList =
                    vibSignalRepository
                    .GetDatas<VibSingal>(obj => obj.MSiteID == parameter.MSiteID, false)
                    .ToList();
                //获取振动类型
                List<VibratingSingalType> vibSingalTypeList =
                    cacheDICT.GetInstance()
                    .GetCacheType<VibratingSingalType>()
                    .ToList();
                //获取测量位置监测类型
                List<MeasureSiteMonitorType> measureSiteMonitorType =
                    cacheDICT.GetInstance()
                    .GetCacheType<MeasureSiteMonitorType>()
                    .ToList();
                //获取波长信息
                List<WaveLengthValues> waveLengthValuesList =
                    cacheDICT.GetInstance()
                    .GetCacheType<WaveLengthValues>()
                    .ToList();
                //获取波形下限信息
                List<WaveLowerLimitValues> waveLowerLimitValues =
                    cacheDICT.GetInstance()
                    .GetCacheType<WaveLowerLimitValues>()
                    .ToList();
                //获取波形上限信息
                List<WaveUpperLimitValues> waveUpperLimitValues =
                    cacheDICT.GetInstance()
                    .GetCacheType<WaveUpperLimitValues>()
                    .ToList();

                #region 获取振动信号下的特征值

                //获取振动信号报警阈值
                int[] signalIDList = vibSingalList
                    //王颖辉 修改 定时和临时
                    //  .Where(p => p.DAQStyle == 1)
                    .Select(p => p.SingalID)
                    .ToArray();
                var vibratingSignal = signalAlmSetRepository
                    .GetDatas<SignalAlmSet>(p => signalIDList.Contains(p.SingalID), false)
                    .ToArray();

                #endregion

                VibSignalInfo tempObj = null;
                foreach (VibSingal obj in vibSingalList)
                {
                    string vibSingalTypeName = string.Empty;
                    var vibSingalType = vibSingalTypeList
                        .Where(O => O.ID == obj.SingalType)
                        .FirstOrDefault();

                    if (vibSingalType != null)
                    {
                        vibSingalTypeName = vibSingalType.Name;
                    }
                    string vaveDataLengthName = string.Empty;
                    var vaveDataLengthInfo = waveLengthValuesList
                        .Where(O => O.ID == obj.WaveDataLength)
                        .FirstOrDefault();
                    if (vaveDataLengthInfo != null)
                    {
                        vaveDataLengthName = vaveDataLengthInfo.WaveLengthValue.ToString();
                    }
                    tempObj = new VibSignalInfo()
                    {
                        AddDate = obj.AddDate,
                        ChildrenCount = 1,
                        WID = obj.SingalID,
                        SingalID = obj.SingalID,
                        DAQStyle = obj.DAQStyle,
                        MSiteID = obj.MSiteID,
                        SignalStatus = obj.SingalStatus,
                        SignalSDate = obj.SingalSDate,
                        Remark = obj.Remark,
                        Type = 2,
                        TypeName = "振动信号",
                        SignalTypeId = obj.SingalType,
                        SignalTypeName = vibSingalTypeName,
                        WaveDataLengthTypeId = obj.WaveDataLength,
                        WaveDataLengthName = vaveDataLengthName
                    };
                    if (obj.UpLimitFrequency != null)
                    {
                        tempObj.UpLimitFrequencyId = obj.UpLimitFrequency;
                        string upLimitFrequencyName = string.Empty;
                        var waveUpperLimitValueInfo =
                            waveUpperLimitValues
                            .Where(O => O.ID == obj.UpLimitFrequency)
                            .FirstOrDefault();
                        if (waveUpperLimitValueInfo != null)
                        {
                            upLimitFrequencyName = waveUpperLimitValueInfo.WaveUpperLimitValue.ToString();
                        }
                        string lowLimitFrequencyName = string.Empty;
                        var waveLowerLimitInfo =
                            waveLowerLimitValues
                            .Where(O => O.ID == obj.LowLimitFrequency)
                            .FirstOrDefault();
                        if (waveLowerLimitInfo != null)
                        {
                            lowLimitFrequencyName = waveLowerLimitInfo.WaveLowerLimitValue.ToString();
                        }
                        tempObj.UpLimitFrequencyName = upLimitFrequencyName;
                        tempObj.LowLimitFrequencyId = obj.LowLimitFrequency;
                        tempObj.LowLimitFrequencyName = lowLimitFrequencyName;
                    }
                    else
                    {
                        string EnlvpBandWName = string.Empty;
                        var waveUpperLimitValueInfo =
                            waveUpperLimitValues
                            .Where(O => O.ID == obj.EnlvpBandW)
                            .FirstOrDefault();
                        if (waveUpperLimitValueInfo != null)
                        {
                            EnlvpBandWName = waveUpperLimitValueInfo.WaveUpperLimitValue.ToString();
                        }
                        string EnlvpFilterName = string.Empty;
                        var waveLowerLimitInfo =
                            waveLowerLimitValues
                            .Where(O => O.ID == obj.EnlvpFilter)
                            .FirstOrDefault();
                        if (waveLowerLimitInfo != null)
                        {
                            EnlvpFilterName = waveLowerLimitInfo.WaveLowerLimitValue.ToString();
                        }

                        tempObj.EnlvpBandWId = obj.EnlvpBandW;
                        tempObj.EnlvpBandWName = EnlvpBandWName;
                        tempObj.EnlvpFilterId = obj.EnlvpFilter;
                        tempObj.EnlvpFilterName = EnlvpFilterName;
                    }
                    tempObj.ChildrenCount = vibratingSignal.Count(p => p.SingalID == obj.SingalID);
                    mSiteAlmAndSignalDataForDeviceTreeResult.VibSignalInfoList.Add(tempObj);
                }

                var tempeDeviceSetMsitealmList =
                    tempeDeviceSetMSiteAlmRepository
                    .GetDatas<TempeDeviceSetMSiteAlm>(obj => obj.MsiteID == parameter.MSiteID, false)
                    .ToList();
                foreach (var almObj in tempeDeviceSetMsitealmList)
                {
                    MSiteAlmInfo MSAlmInfo = new MSiteAlmInfo();
                    MSAlmInfo.AddDate = almObj.AddDate;
                    MSAlmInfo.AlmValue = almObj.AlmValue;
                    MSAlmInfo.MsiteAlmID = almObj.MsiteAlmID;
                    MSAlmInfo.MsiteID = almObj.MsiteID;
                    MSAlmInfo.Status = almObj.Status;
                    MSAlmInfo.Type = 3;
                    MSAlmInfo.TypeName = "测量位置报警设置";
                    MSAlmInfo.WarnValue = almObj.WarnValue;
                    MSAlmInfo.MSDType = (int)EnumMSiteMonitorType.TempeDevice;
                    string name = string.Empty;
                    var measureSiteMonitorTypeInfo =
                        measureSiteMonitorType
                        .Where(O => O.ID == (int)EnumMSiteMonitorType.TempeDevice)
                        .FirstOrDefault();
                    if (measureSiteMonitorTypeInfo != null)
                    {
                        name = measureSiteMonitorTypeInfo.Name;
                    }
                    MSAlmInfo.Name = name;

                    mSiteAlmAndSignalDataForDeviceTreeResult.MSiteAlmInfoList.Add(MSAlmInfo);
                }
                var voltageSetMSiteAlmList =
                    voltageSetMSiteAlmRepository
                    .GetDatas<VoltageSetMSiteAlm>(obj => obj.MsiteID == parameter.MSiteID, false)
                    .ToList();
                foreach (var almObj in voltageSetMSiteAlmList)
                {
                    MSiteAlmInfo MSAlmInfo = new MSiteAlmInfo();

                    MSAlmInfo.AddDate = almObj.AddDate;
                    MSAlmInfo.AlmValue = almObj.AlmValue;
                    MSAlmInfo.MsiteAlmID = almObj.MsiteAlmID;
                    MSAlmInfo.MsiteID = almObj.MsiteID;
                    MSAlmInfo.Status = almObj.Status;
                    MSAlmInfo.Type = 3;
                    MSAlmInfo.TypeName = "测量位置报警设置";
                    MSAlmInfo.WarnValue = almObj.WarnValue;
                    MSAlmInfo.MSDType = (int)EnumMSiteMonitorType.VoltageSet;
                    string name = string.Empty;
                    var measureSiteMonitorTypeInfo =
                        measureSiteMonitorType
                        .Where(O => O.ID == (int)EnumMSiteMonitorType.VoltageSet)
                        .FirstOrDefault();
                    if (measureSiteMonitorTypeInfo != null)
                    {
                        name = measureSiteMonitorTypeInfo.Name;
                    }
                    MSAlmInfo.Name = name;

                    mSiteAlmAndSignalDataForDeviceTreeResult.MSiteAlmInfoList.Add(MSAlmInfo);
                }

                var tempeWSSetMSiteAlmList =
                    tempeWSSetMSiteAlmRepository
                    .GetDatas<TempeWSSetMSiteAlm>(obj => obj.MsiteID == parameter.MSiteID, false)
                    .ToList();
                foreach (var almObj in tempeWSSetMSiteAlmList)
                {
                    MSiteAlmInfo MSAlmInfo = new MSiteAlmInfo();

                    MSAlmInfo.AddDate = almObj.AddDate;
                    MSAlmInfo.AlmValue = almObj.AlmValue;
                    MSAlmInfo.MsiteAlmID = almObj.MsiteAlmID;
                    MSAlmInfo.MsiteID = almObj.MsiteID;
                    MSAlmInfo.Status = almObj.Status;
                    MSAlmInfo.Type = 3;
                    MSAlmInfo.TypeName = "测量位置报警设置";
                    MSAlmInfo.WarnValue = almObj.WarnValue;
                    MSAlmInfo.MSDType = (int)EnumMSiteMonitorType.TempeWS;
                    string name = string.Empty;
                    var measureSiteMonitorTypeInfo = measureSiteMonitorType
                        .Where(O => O.ID == (int)EnumMSiteMonitorType.TempeWS)
                        .FirstOrDefault();
                    if (measureSiteMonitorTypeInfo != null)
                    {
                        name = measureSiteMonitorTypeInfo.Name;
                    }
                    MSAlmInfo.Name = name;

                    mSiteAlmAndSignalDataForDeviceTreeResult.MSiteAlmInfoList.Add(MSAlmInfo);
                }

                response.IsSuccessful = true;
                response.Result = mSiteAlmAndSignalDataForDeviceTreeResult;
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001851";
                return response;
            }
        }
        #endregion

        #region 获取振动信号报警配置数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取振动信号报警配置数据信息
        /// </summary>
        /// <param name="parameter">获取振动信号报警配置数据信息参数</param>
        /// <returns></returns>   
        public BaseResponse<SignalAlmDataForDeviceTreeResult> GetSignalAlmDataForDeviceTree(SignalAlmDataForDeviceTreeParameter parameter)
        {
            BaseResponse<SignalAlmDataForDeviceTreeResult> response = new BaseResponse<SignalAlmDataForDeviceTreeResult>();

            try
            {
                SignalAlmDataForDeviceTreeResult backObj = new SignalAlmDataForDeviceTreeResult();
                backObj.VibSignalInfomation = new List<VibSignalInfomation>();

                List<EigenValueType> eigenValueTypeList = cacheDICT.GetInstance()
                    .GetCacheType<EigenValueType>()
                    .ToList();

                var signalAlmSetList = signalAlmSetRepository
                    .GetDatas<SignalAlmSet>(obj => obj.SingalID == parameter.SignalID, false)
                    .ToList();
                foreach (var almObj in signalAlmSetList)
                {
                    backObj.VibSignalInfomation.Add(new VibSignalInfomation()
                    {
                        AddDate = almObj.AddDate,
                        AlmValue = almObj.AlmValue,
                        SignalAlmID = almObj.SingalAlmID,
                        SignalID = almObj.SingalID,
                        Status = almObj.Status,
                        ValueTypeId = almObj.ValueType,
                        ValueTypeName = eigenValueTypeList
                            .Where(obj => obj.ID == almObj.ValueType)
                            .First().Name,
                        WarnValue = almObj.WarnValue,
                        WID = almObj.SingalAlmID,
                        Type = 4,
                        TypeName = "振动信号报警设置",
                        UploadTrigger = almObj.UploadTrigger,
                        ThrendAlarmPrvalue = almObj.ThrendAlarmPrvalue,
                    });
                }

                response.IsSuccessful = true;
                response.Result = backObj;
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001861";
                return response;
            }
        }
        #endregion

        #region 添加设备数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：添加设备数据信息
        /// 修改人:王颖辉
        /// 修改时间:2017-10-10
        /// 修改内容:添加三个字段
        /// </summary>
        /// <param name="parameter">添加设备数据信息参数</param>
        /// <returns></returns>
        public BaseResponse<ResponseResult> AddDeviceRecordForDeviceTree(AddDeviceRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<ResponseResult> response = new BaseResponse<ResponseResult>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "001872";
                    return response;
                }

                #region 添加设备信息

                Device device = new Device();
                device.MonitorTreeID = parameter.MonitorTreeID;
                device.DevNO = parameter.DevNo;
                device.DevType = parameter.DevType;
                device.DevName = parameter.DevName;
                device.UseType = parameter.UseType;
                device.Rotate = parameter.Rotate;
                device.Length = parameter.Length;
                device.Width = parameter.Width;
                device.Height = parameter.Height;
                device.DevManufacturer = parameter.DevManufacturer;
                device.DevModel = parameter.DevModel;
                device.Position = parameter.Position;
                device.Power = parameter.Power;
                device.RatedCurrent = parameter.RatedCurrent;
                device.RatedVoltage = parameter.RatedVoltage;
                device.Media = parameter.Media;
                device.OutputMaxPressure = parameter.OutputMaxPressure;
                device.CouplingType = parameter.CouplingType;
                device.outputVolume = parameter.OutputVolume;
                device.StatusCriticalValue = parameter.StatusCriticalValue;
                device.DevMark = parameter.DevMark;
                device.PersonInCharge = parameter.PersonInCharge;
                device.PersonInChargeTel = parameter.PersonInChargeTel;
                device.RunStatus = parameter.RunStatus;
                device.HeadOfDelivery = parameter.HeadOfDelivery;
                device.DevManager = parameter.DevManager;
                device.AddDate = System.DateTime.Now;
                device.AlmStatus = 0;
                device.DevSDate = System.DateTime.Now;
                device.ImageID = 0;
                device.LastCheckDate = System.DateTime.Now;
                device.OperationDate = parameter.OperationDate;
                device.LastUpdateTime = System.DateTime.Now;
                device.Latitude = parameter.Latitude;
                device.Longitude = parameter.Longitude;

                #endregion

                return ExecuteDB.ExecuteTrans((context) =>
                {
                    OperationResult operationResult = context.Device.AddNew(context, device);

                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        #region 添加用户和设备关系表

                        if (parameter.UserId == -1)
                        {
                            //添加超级管理员
                            UserRalationDevice userRalaDev = new UserRalationDevice();
                            userRalaDev.UserID = 1011;
                            userRalaDev.DevId = device.DevID;
                            context.UserRalationDevice.AddNew(context, userRalaDev);
                        }
                        else
                        {
                            //添加关联联系表
                            UserRalationDevice userRalaDev = new UserRalationDevice();
                            userRalaDev.UserID = parameter.UserId;
                            userRalaDev.DevId = device.DevID;
                            context.UserRalationDevice.AddNew(context, userRalaDev);
                        }

                        #endregion

                        #region 添加设备和网关关系表

                        if (parameter.WGID != null && parameter.WGID.Count > 0)
                        {
                            foreach (int item in parameter.WGID)
                            {
                                DeviceRelationWG devRelaWG = new DeviceRelationWG();
                                devRelaWG.DevId = device.DevID;
                                devRelaWG.WGID = item;
                                context.DeviceRelationWG.AddNew(context, devRelaWG);
                            }
                        }

                        #endregion

                        ResponseResult responseResult = new ResponseResult();
                        responseResult.INT = device.DevID;
                        response.IsSuccessful = true;
                        response.Result = responseResult;
                        return response;
                    }

                    response.IsSuccessful = false;
                    response.Code = "001881";
                    return response;
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001881";
                return response;
            }
        }
        #endregion

        #region 添加测量位置数据信息
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-10-10
        /// 创建记录：添加测量位置数据信息
        /// </summary>
        /// <param name="parameter">添加测量位置数据信息参数</param>
        /// <returns></returns>
        public BaseResponse<MSiteRecordForDeviceTreeResult> AddMSiteRecordForDeviceTree(AddMSiteRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<MSiteRecordForDeviceTreeResult> response = new BaseResponse<MSiteRecordForDeviceTreeResult>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "001892";
                    return response;
                }

                #region 添加测量位置数据信息时,位置名称类型不正确
                if (parameter.MSiteTypeId < 1)
                {
                    response.IsSuccessful = false;
                    response.Code = "009392";
                    return response;
                }
                #endregion

                #region 添加测量位置数据信息时,设备ID不正确
                if (parameter.DeviceID < 1)
                {
                    response.IsSuccessful = false;
                    response.Code = "009402";
                    return response;
                }
                #endregion

                MeasureSite measureSite = new MeasureSite();
                measureSite.MSiteName = parameter.MSiteTypeId;
                measureSite.DevID = parameter.DeviceID;
                measureSite.WSID = parameter.WSID;
                measureSite.SensorCosA = parameter.SensorCosA;
                measureSite.SensorCosB = parameter.SensorCosB;
                measureSite.WaveTime = parameter.WaveTime ?? "0#1";
                measureSite.FlagTime = parameter.FlagTime ?? "0#1";
                measureSite.TemperatureTime = parameter.TemperatureTime ?? "1";
                measureSite.Remark = parameter.Remark;
                measureSite.Position = parameter.Position;
                measureSite.AddDate = System.DateTime.Now;
                measureSite.MSiteSDate = System.DateTime.Now;
                measureSite.MSiteStatus = 0;
                measureSite.SerialNo = 0;
                measureSite.SensorCoefficient = parameter.SensorCoefficient;

                return ExecuteDB.ExecuteTrans((context) =>
                {
                    //检测传感器是否被使用,状态修改为1，未被使用
                    if (context.Measuresite
                        .GetDatas<MeasureSite>(context, p => p.WSID.HasValue
                            && p.WSID == measureSite.WSID)
                        .Any() && measureSite.WSID.HasValue && measureSite.WSID > 0)
                    {
                        WS wsUseStatusObj = context.WS
                            .GetByKey<WS>(context, measureSite.WSID);
                        if (wsUseStatusObj.UseStatus == 0)
                        {
                            wsUseStatusObj.UseStatus = 1;
                            context.WS.Update(context, wsUseStatusObj);
                        }

                        response.IsSuccessful = false;
                        response.Code = "001902";
                        return response;
                    }
                    else
                    {
                        //备用测量位置
                        OperationResult operationResult = context.Measuresite
                            .AddNew(context, measureSite);
                        if (operationResult.ResultType == EnumOperationResultType.Success)
                        {
                            //关联WS后，同步设置此WS的使用状态为 使用 
                            WS wsInDb = null;
                            if (measureSite.WSID.HasValue && measureSite.WSID.Value > 0)
                            {
                                wsInDb = context.WS
                                    .GetByKey(context, measureSite.WSID.Value);
                                wsInDb.UseStatus = 1;
                                context.WS.Update(context, wsInDb);
                            }
                            response.IsSuccessful = true;
                            response.Result = new MSiteRecordForDeviceTreeResult
                            {
                                MSiteID = measureSite.MSiteID
                            };

                            #region 添加设备温度
                            if (parameter.IfDeviceTemperature == 1)
                            {
                                TempeDeviceSetMSiteAlm tempeDeviceSetMSiteAlm = new TempeDeviceSetMSiteAlm();
                                tempeDeviceSetMSiteAlm.MsiteID = measureSite.MSiteID;
                                tempeDeviceSetMSiteAlm.WarnValue = parameter.DeviceTemperatureAlarmValue;
                                tempeDeviceSetMSiteAlm.AlmValue = parameter.DeviceTemperatureDangerValue;
                                tempeDeviceSetMSiteAlm.AddDate = DateTime.Now;
                                if (wsInDb != null && wsInDb.DevFormType == (int)EnumWSFormType.WiredSensor)
                                    tempeDeviceSetMSiteAlm.WGID = wsInDb.WGID;
                                context.TempeDeviceSetMSiteAlm.AddNew(context, tempeDeviceSetMSiteAlm);
                                if (wsInDb != null && wsInDb.DevFormType == (int)EnumWSFormType.WiredSensor)
                                {
                                    ModBusRegisterAddress regAddress = new ModBusRegisterAddress();
                                    regAddress.MDFID = tempeDeviceSetMSiteAlm.MsiteAlmID;
                                    regAddress.MDFResourceTable = "T_SYS_TEMPE_DEVICE_SET_MSITEALM";
                                    regAddress.RegisterAddress = parameter.TemperatureWSID;
                                    regAddress.StrEnumRegisterStorageMode = 2;
                                    regAddress.StrEnumRegisterStorageSequenceMode = 2;
                                    regAddress.StrEnumRegisterType = 1;
                                    regAddress.StrEnumRegisterInformation = 0;
                                    context.ModBusRegisterAddress.AddNew(context, regAddress);
                                }
                            }
                            #endregion

                            #region 添加传感器温度
                            if (parameter.IfWSTemperature == 1)
                            {
                                TempeWSSetMSiteAlm tempeWSSetMSiteAlm = new TempeWSSetMSiteAlm();
                                tempeWSSetMSiteAlm.MsiteID = measureSite.MSiteID;
                                tempeWSSetMSiteAlm.AddDate = DateTime.Now;
                                tempeWSSetMSiteAlm.WarnValue = parameter.WSTemperatureAlarmValue;
                                tempeWSSetMSiteAlm.AlmValue = parameter.WSTemperatureDangerValue;
                                context.TempeWSSetMsitealm.AddNew(context, tempeWSSetMSiteAlm);
                            }
                            #endregion

                            #region 添加电池电压
                            if (parameter.IfVoltage == 1)
                            {
                                VoltageSetMSiteAlm voltageSetMSiteAlm = new VoltageSetMSiteAlm();
                                voltageSetMSiteAlm.MsiteID = measureSite.MSiteID;
                                voltageSetMSiteAlm.AddDate = DateTime.Now;
                                voltageSetMSiteAlm.WarnValue = parameter.VoltageAlarmValue;
                                voltageSetMSiteAlm.AlmValue = parameter.VoltageDangerValue;
                                context.VoltageSetMsitealm.AddNew(context, voltageSetMSiteAlm);
                            }
                            #endregion

                            #region 添加振动
                            if (parameter.VibSignalInfo != null && parameter.VibSignalInfo.Any())
                            {
                                foreach (var info in parameter.VibSignalInfo)
                                {
                                    VibSingal vibSingal = new VibSingal();
                                    vibSingal.MSiteID = measureSite.MSiteID;
                                    vibSingal.DAQStyle = 1;//默认定时
                                    vibSingal.UpLimitFrequency = info.UpperLimitID;
                                    vibSingal.LowLimitFrequency = info.LowLimitID;
                                    vibSingal.WaveDataLength = info.WaveLengthID;
                                    vibSingal.SingalType = info.VibrationSignalTypeID;

                                    vibSingal.EigenValueWaveLength = info.EigenWaveLengthID;
                                    vibSingal.AddDate = DateTime.Now;
                                    vibSingal.SingalSDate = DateTime.Now;
                                    if (wsInDb != null && wsInDb.DevFormType == (int)EnumWSFormType.WiredSensor)
                                    {
                                        if (vibSingal.SingalType == 4)
                                        {
                                            vibSingal.EnlvpBandW = info.UpperLimitID;
                                            vibSingal.UpLimitFrequency = info.UpperLimitID;
                                            vibSingal.SamplingTimePeriod = info.SamplingTimePeriod;
                                            vibSingal.EnvelopeFilterUpLimitFreq = info.EnvelopeFilterUpLimitFreq;
                                            vibSingal.EnvelopeFilterLowLimitFreq = info.EnvelopeFilterLowLimitFreq;
                                        }
                                        else
                                            vibSingal.SamplingTimePeriod = info.SamplingTimePeriod;
                                    }
                                    context.VibSingal.AddNew(context, vibSingal);

                                    #region 添加特征值阈值
                                    if (info.EigenValueInfo != null && info.EigenValueInfo.Any())
                                    {
                                        foreach (var eigen in info.EigenValueInfo)
                                        {
                                            SignalAlmSet signalAlmSet = new SignalAlmSet();
                                            signalAlmSet.SingalID = vibSingal.SingalID;
                                            signalAlmSet.AddDate = DateTime.Now;
                                            signalAlmSet.WarnValue = eigen.AlarmValue;
                                            signalAlmSet.AlmValue = eigen.DangerValue;
                                            signalAlmSet.ValueType = eigen.EigenValueTypeID;
                                            signalAlmSet.UploadTrigger = eigen.UploadTrigger;
                                            signalAlmSet.ThrendAlarmPrvalue = eigen.ThrendAlarmPrvalue;
                                            if (vibSingal.SingalType == 2)
                                            {
                                                signalAlmSet.EnergyUpLimit = eigen.EnergyUpLimit;
                                                signalAlmSet.EnergyLowLimit = eigen.EnergyLowLimit;
                                            }
                                            context.SignalAlmSet.AddNew(context, signalAlmSet);
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion

                            #region 添加轴承信息
                            int measureSiteId = measureSite.MSiteID;
                            List<MeasureSiteBearing> list = new List<MeasureSiteBearing>();
                            if (parameter.BearingInfoList != null && parameter.BearingInfoList.Any())
                            {
                                foreach (var info in parameter.BearingInfoList)
                                {
                                    MeasureSiteBearing measureSiteBearing = new MeasureSiteBearing();
                                    measureSiteBearing.BearingID = info.BearingID;
                                    measureSiteBearing.MeasureSiteID = measureSiteId;
                                    measureSiteBearing.BearingType = info.BearingType;
                                    measureSiteBearing.LubricatingForm = info.LubricatingForm;
                                    measureSiteBearing.BearingNum = info.BearingNum;
                                    measureSiteBearing.AddDate = DateTime.Now;
                                    list.Add(measureSiteBearing);
                                }

                                context.MeasureSiteBearing.AddNew(context, list);
                            }
                            #endregion

                            #region 添加测点后，直接在实时数据表中写入测点
                            //添加测点后，直接在实时数据表中写入测点
                            MeasureSiteType siteType = cacheDICT.GetInstance()
                                .GetCacheType<MeasureSiteType>(p => p.ID == measureSite.MSiteName)
                                .FirstOrDefault();

                            RealTimeCollectInfo realTimeCollectInfo = new RealTimeCollectInfo();
                            realTimeCollectInfo.DevID = measureSite.DevID;
                            realTimeCollectInfo.MSID = measureSite.MSiteID;
                            realTimeCollectInfo.MSStatus = measureSite.MSiteStatus;
                            realTimeCollectInfo.MSDesInfo = siteType.Describe;
                            realTimeCollectInfo.MSDataStatus = measureSite.MSiteStatus;
                            realTimeCollectInfo.MSName = siteType.Name;
                            realTimeCollectInfo.AddDate = DateTime.Now;
                            context.RealTimeCollectInfo.AddNew(context, realTimeCollectInfo);

                            #endregion

                            return response;
                        }
                        else
                        {
                            response.IsSuccessful = false;
                            response.Code = "001911";
                            return response;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001911";
                return response;
            }
        }
        #endregion

        #region 添加振动信号数据信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：添加振动信号数据信息
        /// </summary>
        /// <param name="parameter">添加振动信号数据信息参数</param>
        /// <returns></returns>
        public BaseResponse<VibSignalRecordForDeviceTreeResult> AddVibSignalRecordForDeviceTree(AddVibSignalRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<VibSignalRecordForDeviceTreeResult> response = new BaseResponse<VibSignalRecordForDeviceTreeResult>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "001922";
                    return response;
                }

                VibSingal vibSingal = new VibSingal();
                vibSingal.DAQStyle = parameter.DAQStyle;
                vibSingal.MSiteID = parameter.MSiteID;
                vibSingal.SingalType = parameter.SignalType;
                vibSingal.UpLimitFrequency = parameter.UpLimitFrequency;
                vibSingal.LowLimitFrequency = parameter.LowLimitFrequency;
                vibSingal.WaveDataLength = parameter.WaveDataLength;
                vibSingal.EnlvpBandW = parameter.EnlvpBandW;
                vibSingal.EnlvpFilter = parameter.EnlvpFilter;
                vibSingal.Remark = parameter.Remark;
                vibSingal.AddDate = System.DateTime.Now;
                vibSingal.SingalSDate = System.DateTime.Now;
                vibSingal.SingalStatus = 0;

                OperationResult operationResult = vibSignalRepository
                    .AddNew<VibSingal>(vibSingal);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = new VibSignalRecordForDeviceTreeResult
                    {
                        SignalID = vibSingal.SingalID
                    };
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "001931";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001931";
                return response;
            }
        }
        #endregion

        #region 添加测量位置报警配置及报警值信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：添加测量位置报警配置及报警值信息
        /// </summary>
        /// <param name="parameter">添加测量位置报警配置及报警值信息参数</param>
        /// <returns></returns>
        public BaseResponse<MSiteAlmRecordForDeviceTreeResult> AddMSiteAlmRecordForDeviceTree(AddMSiteAlmRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<MSiteAlmRecordForDeviceTreeResult> response = new BaseResponse<MSiteAlmRecordForDeviceTreeResult>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "001942";
                    return response;
                }

                OperationResult operationResult = null;
                object obj = null;
                switch (parameter.MSDType)
                {
                    case (int)EnumMSiteMonitorType.TempeDevice:
                        obj = new TempeDeviceSetMSiteAlm()
                        {
                            AddDate = System.DateTime.Now,
                            AlmValue = float.Parse(parameter.AlmValue),
                            WarnValue = float.Parse(parameter.WarnValue),
                            MsiteID = parameter.MSiteID,
                        };
                        TempeDeviceSetMSiteAlm tempeDeviceSetMsitealm = obj as TempeDeviceSetMSiteAlm;
                        operationResult = tempeDeviceSetMSiteAlmRepository
                            .AddNew<TempeDeviceSetMSiteAlm>(obj as TempeDeviceSetMSiteAlm);
                        if (operationResult.ResultType == EnumOperationResultType.Success)
                        {
                            response.IsSuccessful = true;
                            response.Result = new MSiteAlmRecordForDeviceTreeResult
                            {
                                MSiteAlmID = (obj as TempeDeviceSetMSiteAlm).MsiteAlmID
                            };
                            return response;
                        }
                        else
                        {
                            response.IsSuccessful = false;
                            response.Code = "001951";
                            return response;
                        }

                    case (int)EnumMSiteMonitorType.TempeWS:
                        obj = new TempeWSSetMSiteAlm()
                        {
                            AddDate = System.DateTime.Now,
                            AlmValue = float.Parse(parameter.AlmValue),
                            WarnValue = float.Parse(parameter.WarnValue),
                            MsiteID = parameter.MSiteID,
                        };
                        operationResult = tempeWSSetMSiteAlmRepository
                            .AddNew<TempeWSSetMSiteAlm>(obj as TempeWSSetMSiteAlm);
                        if (operationResult.ResultType == EnumOperationResultType.Success)
                        {
                            response.IsSuccessful = true;
                            response.Result = new MSiteAlmRecordForDeviceTreeResult
                            {
                                MSiteAlmID = (obj as TempeWSSetMSiteAlm).MsiteAlmID
                            };
                            return response;
                        }
                        else
                        {
                            response.IsSuccessful = false;
                            response.Code = "001951";
                            return response;
                        }

                    case (int)EnumMSiteMonitorType.VoltageSet:
                        obj = new VoltageSetMSiteAlm()
                        {
                            AddDate = System.DateTime.Now,
                            AlmValue = float.Parse(parameter.AlmValue),
                            WarnValue = float.Parse(parameter.WarnValue),
                            MsiteID = parameter.MSiteID,
                        };
                        VoltageSetMSiteAlm voltageSetMSiteAlm = obj as VoltageSetMSiteAlm;
                        operationResult = voltageSetMSiteAlmRepository
                            .AddNew<VoltageSetMSiteAlm>(obj as VoltageSetMSiteAlm);
                        if (operationResult.ResultType == EnumOperationResultType.Success)
                        {
                            response.IsSuccessful = true;
                            response.Result = new MSiteAlmRecordForDeviceTreeResult
                            {
                                MSiteAlmID = (obj as VoltageSetMSiteAlm).MsiteAlmID
                            };
                            return response;
                        }
                        else
                        {
                            response.IsSuccessful = false;
                            response.Code = "001951";
                            return response;
                        }

                    default:
                        response.IsSuccessful = false;
                        response.Code = "001951";
                        return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001951";
                return response;
            }
        }
        #endregion

        #region 添加特征值及报警值信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：添加特征值及报警值信息
        /// </summary>
        /// <param name="parameter">添加特征值及报警值信息参数</param>
        /// <returns></returns>
        public BaseResponse<SignalAlmRecordForDeviceTreeResult> AddSignalAlmRecordForDeviceTree(AddSignalAlmRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<SignalAlmRecordForDeviceTreeResult> response = new BaseResponse<SignalAlmRecordForDeviceTreeResult>();

            try
            {
                OperationResult operationResult = null;

                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "001962";
                    return response;
                }

                SignalAlmSet signalAlmSet = new SignalAlmSet()
                {
                    AddDate = System.DateTime.Now,
                    SingalID = parameter.SignalID,
                    AlmValue = float.Parse(parameter.AlmValue),
                    ValueType = parameter.ValueType,
                    WarnValue = float.Parse(parameter.WarnValue),
                    Status = 0,
                    UploadTrigger = parameter.UploadTrigger,
                    ThrendAlarmPrvalue = parameter.ThrendAlarmPrvalue,
                };
                operationResult = signalAlmSetRepository
                    .AddNew<SignalAlmSet>(signalAlmSet);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = new SignalAlmRecordForDeviceTreeResult
                    {
                        SignalAlmID = signalAlmSet.SingalAlmID
                    };
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "001971";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001971";
                return response;
            }
        }
        #endregion

        #region 编辑设备信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑设备信息
        /// </summary>
        /// <param name="parameter">编辑设备信息参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditDeviceRecordForDeviceTree(EditDeviceRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "001982";
                    return response;
                }

                #region 修改设备信息

                Device oldDevice = deviceRepository
                    .GetDatas<Device>(obj => obj.DevID == parameter.DevID, false)
                    .SingleOrDefault();
                //记录设备原来的节点，以便与修改后的节点做比较
                //如果节点被修改，则结束设备下的所有报警记录
                int oldMonitorTreeID = oldDevice.MonitorTreeID;
                Dictionary<EntityBase, EntityState> operatpr = new Dictionary<EntityBase, EntityState>();

                oldDevice.DevMadeDate = parameter.DevMadeDate;
                oldDevice.MonitorTreeID = parameter.MonitorTreeID;
                oldDevice.DevNO = parameter.DevNo;
                oldDevice.DevType = parameter.DevType;
                oldDevice.DevName = parameter.DevName;
                oldDevice.Rotate = parameter.Rotate;
                oldDevice.Length = parameter.Length;
                oldDevice.Width = parameter.Width;
                oldDevice.Height = parameter.Height;
                oldDevice.DevManufacturer = parameter.DevManufacturer;
                oldDevice.DevManager = parameter.DevManager;
                oldDevice.DevModel = parameter.DevModel;
                oldDevice.Position = parameter.Position;
                oldDevice.Power = parameter.Power;
                oldDevice.RatedCurrent = parameter.RatedCurrent;
                oldDevice.RatedVoltage = parameter.RatedVoltage;
                oldDevice.Media = parameter.Media;
                oldDevice.OutputMaxPressure = parameter.OutputMaxPressure;
                oldDevice.CouplingType = parameter.CouplingType;
                oldDevice.outputVolume = parameter.OutputVolume;
                oldDevice.StatusCriticalValue = parameter.StatusCriticalValue;
                oldDevice.DevMark = parameter.DevMark;
                oldDevice.PersonInCharge = parameter.PersonInCharge;
                oldDevice.PersonInChargeTel = parameter.PersonInChargeTel;
                oldDevice.HeadOfDelivery = parameter.HeadOfDelivery;
                oldDevice.OperationDate = parameter.OperationDate;
                oldDevice.Latitude = parameter.Latitude;
                oldDevice.Longitude = parameter.Longitude;
                operatpr.Add(oldDevice, EntityState.Modified);

                #endregion

                #region 修改设备网关关系表

                //获取设备和网关关系信息
                List<DeviceRelationWG> devRelaWGList = deviceRelationWGRepository
                    .GetDatas<DeviceRelationWG>(item => item.DevId == parameter.DevID, false)
                    .ToList();
                foreach (DeviceRelationWG item in devRelaWGList)
                {
                    operatpr.Add(item, EntityState.Deleted);
                }
                if (parameter.WGID != null && parameter.WGID.Count > 0)
                {
                    foreach (int item in parameter.WGID)
                    {
                        DeviceRelationWG tempDevRelaWG = new DeviceRelationWG();
                        tempDevRelaWG.DevId = parameter.DevID;
                        tempDevRelaWG.WGID = item;
                        operatpr.Add(tempDevRelaWG, EntityState.Added);
                    }
                }

                #endregion

                //#region 如果设备的节点发生变化，则结束此设备下的所有报警记录，Added by QXM,2017/02/22
                //if (oldMonitorTreeID != parameter.MonitorTreeID)//节点发生变化
                //{
                //    var usedMeasureSiteList = measureSiteRepository.GetDatas<MeasureSite>(t => t.DevID == parameter.DevID, true).ToList();
                //    var usedMeasureSiteIDList = usedMeasureSiteList.Select(t => t.MSiteID).ToList();
                //    var usedVibsignalList = vibSignalRepository.GetDatas<VibSingal>(t => usedMeasureSiteIDList.Contains(t.MSiteID), true).ToList();
                //    var usedVibsignalIDList = usedVibsignalList.Select(t => t.SingalID).ToList();
                //    var usedWSIDList = usedMeasureSiteList.Where(t => t.WSID.HasValue).Select(t => t.WSID.Value).ToList();
                //    var usedWSList = wsRepository.GetDatas<WS>(t => usedWSIDList.Contains(t.WSID), true).ToList();

                //    //1.振动信号报警
                //    var vibSignalAlarms = devAlmRecordRepository.GetDatas<DevAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 1 && usedVibsignalIDList.Contains(t.SingalID), true).ToList();
                //    foreach (DevAlmRecord alm in vibSignalAlarms)
                //    {
                //        alm.EDate = DateTime.Now;
                //        operatpr.Add(alm, EntityState.Modified);
                //    }
                //    //2.设备温度报警,传感器温度报警
                //    var devTempAlarms = devAlmRecordRepository.GetDatas<DevAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 2 && usedMeasureSiteIDList.Contains(t.MSiteID), true).ToList();
                //    foreach (DevAlmRecord alm in devTempAlarms)
                //    {
                //        alm.EDate = DateTime.Now;
                //        operatpr.Add(alm, EntityState.Modified);
                //    }

                //    var wsTempAlarms = wsnAlmRecordRepository.GetDatas<WSnAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 3 && usedWSIDList.Contains(t.WSID), true).ToList();
                //    foreach (var alm in wsTempAlarms)
                //    {
                //        alm.EDate = DateTime.Now;
                //        operatpr.Add(alm, EntityState.Modified);
                //    }
                //    //3.电池电压报警
                //    var voltaAlarms = wsnAlmRecordRepository.GetDatas<WSnAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 4 && usedWSIDList.Contains(t.WSID), true).ToList();
                //    foreach (WSnAlmRecord alm in voltaAlarms)
                //    {
                //        alm.EDate = DateTime.Now;
                //        operatpr.Add(alm, EntityState.Modified);
                //    }
                //    //4.WS连接状态报警
                //    var wsLinkAlarms = wsnAlmRecordRepository.GetDatas<WSnAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 5 && usedWSIDList.Contains(t.WSID), true).ToList();
                //    foreach (WSnAlmRecord alm in wsLinkAlarms)
                //    {
                //        alm.EDate = DateTime.Now;
                //        operatpr.Add(alm, EntityState.Modified);
                //    }
                //}
                //#endregion
                //OperationResult operationResult = deviceRepository.Update<Device>(oldDevice);

                OperationResult operationResult = deviceRepository.TranMethod(operatpr);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "001991";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "001991";
                return response;
            }
        }
        #endregion

        #region 编辑测量位置信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑测量位置信息
        /// </summary>
        /// <param name="parameter">编辑测量位置信息参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditMSiteRecordForDeviceTree(EditMSiteRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            if (parameter == null)
            {
                response.IsSuccessful = false;
                response.Code = "002002";
                return response;
            }

            foreach (var info in parameter.MeasureSite)
            {
                #region 编辑测量位置数据信息时,位置名称类型不正确
                if (info.MSiteTypeId < 1)
                {
                    response.IsSuccessful = false;
                    response.Code = "009412";
                    return response;
                }
                #endregion

                #region 编辑测量位置数据信息时,设备ID不正确
                if (info.DeviceID < 1)
                {
                    response.IsSuccessful = false;
                    response.Code = "009422";
                    return response;
                }
                #endregion
            }

            try
            {
                ExecuteDB.ExecuteTrans((context) =>
                {
                    if (parameter.MeasureSite != null && parameter.MeasureSite.Any())
                    {
                        foreach (var info in parameter.MeasureSite)
                        {
                            MeasureSite oldmeasureSite = context.Measuresite
                                .GetDatas<MeasureSite>(context, obj => obj.MSiteID == info.MeasureSiteID)
                                .SingleOrDefault();

                            oldmeasureSite.MSiteName = info.MSiteTypeId;
                            oldmeasureSite.DevID = info.DeviceID;
                            WS newWS;
                            if (oldmeasureSite.WSID.HasValue && oldmeasureSite.WSID > 0)
                            {
                                if (oldmeasureSite.WSID != info.WSID)
                                {
                                    int newWSID = (int)info.WSID;
                                    newWS = context.WS
                                        .GetDatas<WS>(context, p => p.WSID == newWSID)
                                        .SingleOrDefault();
                                    if (newWS != null)
                                    {
                                        newWS.UseStatus = 1;
                                        context.WS.Update(context, newWS);
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }

                                    int oldWSID = (int)oldmeasureSite.WSID;
                                    WS oldWS = context.WS
                                        .GetDatas<WS>(context, p => p.WSID == oldWSID)
                                        .SingleOrDefault();
                                    if (oldWS != null)
                                    {
                                        oldWS.UseStatus = 0;
                                        context.WS.Update(context, oldWS);
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }

                                    //只有当主用机，并且WS有所更改时候，才更新wsID 
                                    oldmeasureSite.WSID = info.WSID;
                                }
                                else
                                {
                                    newWS = context.WS
                                        .GetDatas<WS>(context, p => p.WSID == info.WSID)
                                        .SingleOrDefault();
                                }
                            }
                            else
                            {
                                oldmeasureSite.WSID = info.WSID;
                                newWS = context.WS
                                    .GetDatas<WS>(context, p => p.WSID == info.WSID)
                                    .SingleOrDefault();
                            }
                            //oldmeasureSite.BearingID = info.BearingID;
                            oldmeasureSite.WaveTime = info.WaveTime;
                            oldmeasureSite.FlagTime = info.FlagTime;
                            oldmeasureSite.TemperatureTime = info.TemperatureTime;
                            //oldmeasureSite.BearingType = info.BearingType;
                            //oldmeasureSite.BearingModel = info.BearingModel;
                            //oldmeasureSite.LubricatingForm = info.LubricatingForm;
                            oldmeasureSite.SensorCosA = info.SensorCosA;
                            oldmeasureSite.SensorCosB = info.SensorCosB;
                            oldmeasureSite.Position = info.Position;
                            oldmeasureSite.Remark = info.Remark;
                            oldmeasureSite.SensorCoefficient = info.SensorCoefficient;
                            OperationResult operationResult = context.Measuresite
                               .Update(context, oldmeasureSite);
                            if (operationResult.ResultType == EnumOperationResultType.Success)
                            {
                                response.IsSuccessful = true;
                                response.Result = true;
                                RealTimeCollectInfo realTimeCollectInfo = context.RealTimeCollectInfo
                                    .GetDatas<RealTimeCollectInfo>(context, p => p.MSID == info.MeasureSiteID)
                                    .FirstOrDefault();

                                #region 振动编辑或添加
                                //删除已经没有的
                                if (info.VibSignalInfo != null && info.VibSignalInfo.Any())
                                {
                                    var signalIdList = info.VibSignalInfo.Select(item => item.SingalID).ToList();
                                    var deleteSignalList = context.VibSingal
                                        .Where(item => !signalIdList.Contains(item.SingalID)
                                            && item.MSiteID == info.MeasureSiteID
                                            && item.DAQStyle == 1)
                                        .ToList();

                                    //删除已经不存在的振动信号
                                    if (deleteSignalList != null && deleteSignalList.Any())
                                    {
                                        #region 删除报警记录关联表数据
                                        //删除报警记录提醒关系表
                                        var deleteSignalIDList = deleteSignalList
                                            .Select(item => item.SingalID)
                                            .ToList();
                                        var alarmRecordIDList = context.DevAlmRecord
                                            .Where(item => deleteSignalIDList.Contains(item.SingalID))
                                            .Select(item => item.AlmRecordID)
                                            .ToList();
                                        context.UserRelationDeviceAlmRecord
                                            .Delete(context, item => alarmRecordIDList.Contains(item.DeviceAlmRecordID));
                                        #endregion

                                        //删除振动
                                        context.VibSingal.Delete(context, deleteSignalList);

                                        // 删除特征值
                                        List<int> singalIDs = deleteSignalList.Select(s => s.SingalID).ToList();
                                        context.SignalAlmSet.Delete(context, d => singalIDs.Contains(d.SingalID));

                                        #region 更新实时数据
                                        if (realTimeCollectInfo != null)
                                        {
                                            foreach (var s in deleteSignalList)
                                            {
                                                switch (s.SingalType)
                                                {
                                                    //加速度
                                                    case 1:
                                                        realTimeCollectInfo.MSACCSingalID = null;
                                                        realTimeCollectInfo.MSACCPeakPeakStatus = null;
                                                        realTimeCollectInfo.MSACCPeakPeakValue = null;
                                                        realTimeCollectInfo.MSACCPeakPeakTime = null;
                                                        realTimeCollectInfo.MSACCPeakStatus = null;
                                                        realTimeCollectInfo.MSACCPeakValue = null;
                                                        realTimeCollectInfo.MSACCPeakTime = null;
                                                        realTimeCollectInfo.MSACCVirtualStatus = null;
                                                        realTimeCollectInfo.MSACCVirtualValue = null;
                                                        realTimeCollectInfo.MSACCVirtualTime = null;
                                                        break;

                                                    //速度
                                                    case 2:
                                                        realTimeCollectInfo.MSSpeedSingalID = null;
                                                        realTimeCollectInfo.MSSpeedPeakPeakStatus = null;
                                                        realTimeCollectInfo.MSSpeedPeakPeakValue = null;
                                                        realTimeCollectInfo.MSSpeedPeakPeakTime = null;
                                                        realTimeCollectInfo.MSSpeedPeakStatus = null;
                                                        realTimeCollectInfo.MSSpeedPeakValue = null;
                                                        realTimeCollectInfo.MSSpeedPeakTime = null;
                                                        realTimeCollectInfo.MSSpeedVirtualStatus = null;
                                                        realTimeCollectInfo.MSSpeedVirtualValue = null;
                                                        realTimeCollectInfo.MSSpeedVirtualTime = null;
                                                        break;

                                                    //位移
                                                    case 3:
                                                        realTimeCollectInfo.MSDispSingalID = null;
                                                        realTimeCollectInfo.MSDispPeakPeakStatus = null;
                                                        realTimeCollectInfo.MSDispPeakPeakValue = null;
                                                        realTimeCollectInfo.MSDispPeakPeakTime = null;
                                                        realTimeCollectInfo.MSDispPeakStatus = null;
                                                        realTimeCollectInfo.MSDispPeakValue = null;
                                                        realTimeCollectInfo.MSDispPeakTime = null;
                                                        realTimeCollectInfo.MSDispVirtualStatus = null;
                                                        realTimeCollectInfo.MSDispVirtualValue = null;
                                                        realTimeCollectInfo.MSDispVirtualTime = null;
                                                        break;

                                                    //包络
                                                    case 4:
                                                        realTimeCollectInfo.MSEnvelSingalID = null;
                                                        realTimeCollectInfo.MSEnvelopingCarpetStatus = null;
                                                        realTimeCollectInfo.MSEnvelopingCarpetValue = null;
                                                        realTimeCollectInfo.MSEnvelopingCarpetTime = null;
                                                        realTimeCollectInfo.MSEnvelopingPEAKStatus = null;
                                                        realTimeCollectInfo.MSEnvelopingPEAKValue = null;
                                                        realTimeCollectInfo.MSEnvelopingPEAKTime = null;
                                                        break;

                                                    //设备状态
                                                    case 5:
                                                        realTimeCollectInfo.MSLQSingalID = null;
                                                        realTimeCollectInfo.MSLQStatus = null;
                                                        realTimeCollectInfo.MSLQValue = null;
                                                        realTimeCollectInfo.MSLQTime = null;
                                                        break;

                                                    default:
                                                        break;
                                                }

                                                #region 删除时实表数据
                                                DeleteRealTimeVibSignal(context, info.MeasureSiteID, s.SingalType);
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                else
                                {
                                    #region 删除报警记录关联表数据
                                    //删除测量位置下的所有振动信号值
                                    var deleteSignalList = context.VibSingal
                                        .Where(item => item.MSiteID == info.MeasureSiteID && item.DAQStyle == 1)
                                        .ToList();

                                    if (deleteSignalList != null && deleteSignalList.Any())
                                    {
                                        //删除报警记录提醒关系表
                                        var deleteSignalIDList = deleteSignalList
                                            .Select(item => item.SingalID)
                                            .ToList();
                                        var alarmIDList = context.SignalAlmSet
                                            .Where(item => deleteSignalIDList.Contains(item.SingalID))
                                            .Select(item => item.SingalAlmID)
                                            .ToList();
                                        context.UserRelationDeviceAlmRecord
                                            .Delete(context, item => alarmIDList.Contains(item.DeviceAlmRecordID));

                                        //删除振动阈值表
                                        context.SignalAlmSet.Delete(context, item => deleteSignalIDList.Contains(item.SingalID));
                                    }
                                    #endregion

                                    //如果没有，删除原来所有的振动数据
                                    context.VibSingal.Delete(context, item => item.MSiteID == info.MeasureSiteID);

                                    #region 删除测点实时数据
                                    DeleteRealTimeMeasureSite(context, info.MeasureSiteID);
                                    #endregion
                                }

                                //添加或修改
                                if (info.VibSignalInfo != null && info.VibSignalInfo.Any())
                                {
                                    foreach (var vibsignal in info.VibSignalInfo)
                                    {
                                        if (vibsignal.SingalID > 0)
                                        {
                                            //编辑
                                            var vibsignalInfo = context.VibSingal.GetByKey<VibSingal>(context, vibsignal.SingalID);
                                            vibsignalInfo.MSiteID = info.MeasureSiteID;
                                            vibsignalInfo.DAQStyle = 1;//默认定时
                                            vibsignalInfo.UpLimitFrequency = vibsignal.UpperLimitID;
                                            vibsignalInfo.LowLimitFrequency = vibsignal.LowLimitID;
                                            vibsignalInfo.WaveDataLength = vibsignal.WaveLengthID;
                                            vibsignalInfo.SingalType = vibsignal.VibrationSignalTypeID;
                                            vibsignalInfo.EigenValueWaveLength = vibsignal.EigenWaveLengthID;
                                            vibsignalInfo.AddDate = DateTime.Now;
                                            if (newWS != null && newWS.DevFormType == (int)EnumWSFormType.WiredSensor)
                                            {
                                                if (vibsignalInfo.SingalType == 4)
                                                {
                                                    vibsignalInfo.EnlvpBandW = vibsignal.UpperLimitID;
                                                    vibsignalInfo.UpLimitFrequency = vibsignal.UpperLimitID;
                                                    vibsignalInfo.SamplingTimePeriod = vibsignal.SamplingTimePeriod;
                                                    vibsignalInfo.EnvelopeFilterUpLimitFreq = vibsignal.EnvelopeFilterUpLimitFreq;
                                                    vibsignalInfo.EnvelopeFilterLowLimitFreq = vibsignal.EnvelopeFilterLowLimitFreq;
                                                }
                                                else
                                                    vibsignalInfo.SamplingTimePeriod = vibsignal.SamplingTimePeriod;
                                            }
                                            context.VibSingal.Update<VibSingal>(context, vibsignalInfo);

                                            //删除已经特征值没有的
                                            if (vibsignal.EigenValueInfo != null && vibsignal.EigenValueInfo.Any())
                                            {
                                                var eigenIdList = vibsignal.EigenValueInfo.Select(item => item.SingalAlmID);

                                                #region 删除报警记录关联表记录
                                                context.UserRelationDeviceAlmRecord.Delete(context, item => eigenIdList.Contains(item.DeviceAlmRecordID));
                                                #endregion

                                                var deleteEigenList = context.SignalAlmSet
                                                    .GetDatas<SignalAlmSet>(context, item => !eigenIdList.Contains(item.SingalAlmID)
                                                        && item.SingalID == vibsignal.SingalID)
                                                    .ToList();
                                                context.SignalAlmSet.Delete(context, item => !eigenIdList.Contains(item.SingalAlmID)
                                                    && item.SingalID == vibsignal.SingalID);

                                                #region 更新实时数据
                                                if (realTimeCollectInfo != null && deleteEigenList.Count > 0)
                                                {
                                                    foreach (var e in deleteEigenList)
                                                    {
                                                        switch (e.ValueType)
                                                        {
                                                            //加速度峰值
                                                            case 18:
                                                                realTimeCollectInfo.MSACCPeakStatus = null;
                                                                realTimeCollectInfo.MSACCPeakValue = null;
                                                                realTimeCollectInfo.MSACCPeakTime = null;
                                                                break;

                                                            //加速度峰峰值
                                                            case 19:
                                                                realTimeCollectInfo.MSACCPeakPeakStatus = null;
                                                                realTimeCollectInfo.MSACCPeakPeakValue = null;
                                                                realTimeCollectInfo.MSACCPeakPeakTime = null;
                                                                break;

                                                            //加速度有效值
                                                            case 20:
                                                                realTimeCollectInfo.MSACCVirtualStatus = null;
                                                                realTimeCollectInfo.MSACCVirtualValue = null;
                                                                realTimeCollectInfo.MSACCVirtualTime = null;
                                                                break;

                                                            //速度有效值
                                                            case 21:
                                                                realTimeCollectInfo.MSSpeedVirtualStatus = null;
                                                                realTimeCollectInfo.MSSpeedVirtualValue = null;
                                                                realTimeCollectInfo.MSSpeedVirtualTime = null;
                                                                break;

                                                            //速度峰值
                                                            case 22:
                                                                realTimeCollectInfo.MSSpeedPeakStatus = null;
                                                                realTimeCollectInfo.MSSpeedPeakValue = null;
                                                                realTimeCollectInfo.MSSpeedPeakTime = null;
                                                                break;

                                                            //速度峰峰值
                                                            case 23:
                                                                realTimeCollectInfo.MSSpeedPeakPeakStatus = null;
                                                                realTimeCollectInfo.MSSpeedPeakPeakValue = null;
                                                                realTimeCollectInfo.MSSpeedPeakPeakTime = null;
                                                                break;

                                                            //位移有效值
                                                            case 24:
                                                                realTimeCollectInfo.MSDispVirtualStatus = null;
                                                                realTimeCollectInfo.MSDispVirtualValue = null;
                                                                realTimeCollectInfo.MSDispVirtualTime = null;
                                                                break;

                                                            //位移峰峰值
                                                            case 25:
                                                                realTimeCollectInfo.MSDispPeakPeakStatus = null;
                                                                realTimeCollectInfo.MSDispPeakPeakValue = null;
                                                                realTimeCollectInfo.MSDispPeakPeakTime = null;
                                                                break;

                                                            //位移峰值
                                                            case 26:
                                                                realTimeCollectInfo.MSDispPeakStatus = null;
                                                                realTimeCollectInfo.MSDispPeakValue = null;
                                                                realTimeCollectInfo.MSDispPeakTime = null;
                                                                break;

                                                            //包络峰值
                                                            case 27:
                                                                realTimeCollectInfo.MSEnvelopingPEAKStatus = null;
                                                                realTimeCollectInfo.MSEnvelopingPEAKValue = null;
                                                                realTimeCollectInfo.MSEnvelopingPEAKTime = null;
                                                                break;

                                                            //包络地毯值
                                                            case 28:
                                                                realTimeCollectInfo.MSEnvelopingCarpetStatus = null;
                                                                realTimeCollectInfo.MSEnvelopingCarpetValue = null;
                                                                realTimeCollectInfo.MSEnvelopingCarpetTime = null;
                                                                break;

                                                            //轴承状态
                                                            case 29:
                                                                realTimeCollectInfo.MSLQStatus = null;
                                                                realTimeCollectInfo.MSLQValue = null;
                                                                realTimeCollectInfo.MSLQTime = null;
                                                                break;

                                                            default:
                                                                break;
                                                        }

                                                        #region 删除实时表，阈值数据,特征值
                                                        DeleteRealTimeEigenValue(context, info.MeasureSiteID, vibsignal.VibrationSignalTypeID, e.ValueType);
                                                        #endregion
                                                    }
                                                }
                                                #endregion
                                            }

                                            #region 添加特征值阈值
                                            if (vibsignal.EigenValueInfo != null && vibsignal.EigenValueInfo.Any())
                                            {
                                                foreach (var eigen in vibsignal.EigenValueInfo)
                                                {
                                                    //编辑
                                                    if (eigen.SingalAlmID > 0)
                                                    {
                                                        SignalAlmSet signalAlmSet = context.SignalAlmSet.GetByKey<SignalAlmSet>(context, eigen.SingalAlmID);
                                                        signalAlmSet.SingalID = vibsignalInfo.SingalID;
                                                        signalAlmSet.AddDate = DateTime.Now;
                                                        signalAlmSet.WarnValue = eigen.AlarmValue;
                                                        signalAlmSet.AlmValue = eigen.DangerValue;
                                                        signalAlmSet.ValueType = eigen.EigenValueTypeID;
                                                        signalAlmSet.UploadTrigger = eigen.UploadTrigger;
                                                        signalAlmSet.ThrendAlarmPrvalue = eigen.ThrendAlarmPrvalue;
                                                        if (vibsignal.VibrationSignalTypeID == 2)
                                                        {
                                                            signalAlmSet.EnergyUpLimit = eigen.EnergyUpLimit;
                                                            signalAlmSet.EnergyLowLimit = eigen.EnergyLowLimit;
                                                        }
                                                        context.SignalAlmSet.Update(context, signalAlmSet);
                                                    }
                                                    else
                                                    {
                                                        //添加
                                                        SignalAlmSet signalAlmSet = new SignalAlmSet();
                                                        signalAlmSet.SingalID = vibsignalInfo.SingalID;
                                                        signalAlmSet.AddDate = DateTime.Now;
                                                        signalAlmSet.WarnValue = eigen.AlarmValue;
                                                        signalAlmSet.AlmValue = eigen.DangerValue;
                                                        signalAlmSet.ValueType = eigen.EigenValueTypeID;
                                                        signalAlmSet.UploadTrigger = eigen.UploadTrigger;
                                                        signalAlmSet.ThrendAlarmPrvalue = eigen.ThrendAlarmPrvalue;
                                                        if (vibsignal.VibrationSignalTypeID == 2)
                                                        {
                                                            signalAlmSet.EnergyUpLimit = eigen.EnergyUpLimit;
                                                            signalAlmSet.EnergyLowLimit = eigen.EnergyLowLimit;
                                                        }
                                                        context.SignalAlmSet.AddNew(context, signalAlmSet);
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            //添加 振动
                                            VibSingal vibSignalInfo = new VibSingal();
                                            vibSignalInfo.MSiteID = info.MeasureSiteID;
                                            vibSignalInfo.UpLimitFrequency = vibsignal.UpperLimitID;
                                            vibSignalInfo.LowLimitFrequency = vibsignal.LowLimitID;
                                            vibSignalInfo.WaveDataLength = vibsignal.WaveLengthID;
                                            vibSignalInfo.SingalType = vibsignal.VibrationSignalTypeID;
                                            vibSignalInfo.EigenValueWaveLength = vibSignalInfo.EigenValueWaveLength;
                                            vibSignalInfo.DAQStyle = 1;//默认定时
                                            vibSignalInfo.AddDate = DateTime.Now;
                                            vibSignalInfo.SingalSDate = DateTime.Now;
                                            if (newWS != null && newWS.DevFormType == (int)EnumWSFormType.WiredSensor)
                                            {
                                                if (vibSignalInfo.SingalType == 4)
                                                {
                                                    vibSignalInfo.EnlvpBandW = vibsignal.UpperLimitID;
                                                    vibSignalInfo.UpLimitFrequency = vibsignal.UpperLimitID;
                                                    vibSignalInfo.SamplingTimePeriod = vibsignal.SamplingTimePeriod;
                                                    vibSignalInfo.EnvelopeFilterUpLimitFreq = vibsignal.EnvelopeFilterUpLimitFreq;
                                                    vibSignalInfo.EnvelopeFilterLowLimitFreq = vibsignal.EnvelopeFilterLowLimitFreq;
                                                }
                                                else
                                                    vibSignalInfo.SamplingTimePeriod = vibsignal.SamplingTimePeriod;
                                            }
                                            context.VibSingal.AddNew(context, vibSignalInfo);

                                            #region 添加特征值阈值
                                            if (vibsignal.EigenValueInfo != null && vibsignal.EigenValueInfo.Any())
                                            {
                                                foreach (var eigen in vibsignal.EigenValueInfo)
                                                {
                                                    SignalAlmSet signalAlmSet = new SignalAlmSet();
                                                    signalAlmSet.SingalID = vibSignalInfo.SingalID;
                                                    signalAlmSet.AddDate = DateTime.Now;
                                                    signalAlmSet.WarnValue = eigen.AlarmValue;
                                                    signalAlmSet.AlmValue = eigen.DangerValue;
                                                    signalAlmSet.ValueType = eigen.EigenValueTypeID;
                                                    signalAlmSet.UploadTrigger = eigen.UploadTrigger;
                                                    signalAlmSet.ThrendAlarmPrvalue = eigen.ThrendAlarmPrvalue;
                                                    if (vibSignalInfo.SingalType == 2)
                                                    {
                                                        signalAlmSet.EnergyUpLimit = eigen.EnergyUpLimit;
                                                        signalAlmSet.EnergyLowLimit = eigen.EnergyLowLimit;
                                                    }
                                                    context.SignalAlmSet.AddNew(context, signalAlmSet);
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                }
                                #endregion

                                #region 添加设备温度
                                if (info.DeviceTemperatureAlarmValue == 0 && info.DeviceTemperatureDangerValue == 0)
                                {
                                    var tempepare = context.TempeDeviceSetMSiteAlm.Where(item => item.MsiteID == info.MeasureSiteID).ToList();
                                    if (tempepare != null && tempepare.Any())
                                    {
                                        List<int> temperDevSetAlm = tempepare.Select(item => item.MsiteAlmID).ToList();
                                        context.TempeDeviceSetMSiteAlm.Delete(context, tempepare);
                                        context.ModBusRegisterAddress.Delete(context, item => temperDevSetAlm.Contains(item.MDFID)
                                            && item.MDFResourceTable == "T_SYS_TEMPE_DEVICE_SET_MSITEALM");

                                        #region 删除时实表，设备温度
                                        context.RealTimeAlarmThreshold.Delete(context, item => item.MeasureSiteID == info.MeasureSiteID
                                            && item.MeasureSiteThresholdType == (int)EnumMeasureSiteThresholdType.DeviceTemperature);
                                        #endregion

                                        if (realTimeCollectInfo != null)
                                        {
                                            realTimeCollectInfo.MSDevTemperatureStatus = null;
                                            realTimeCollectInfo.MSDevTemperatureValue = null;
                                            realTimeCollectInfo.MSDevTemperatureTime = null;
                                        }
                                    }
                                }
                                else if (info.DeviceTemperatureMsiteAlmID > 0)
                                {
                                    //修改
                                    TempeDeviceSetMSiteAlm tempeDeviceSetMSiteAlm = context.TempeDeviceSetMSiteAlm
                                        .GetByKey(context, info.DeviceTemperatureMsiteAlmID);
                                    tempeDeviceSetMSiteAlm.MsiteID = info.MeasureSiteID;
                                    tempeDeviceSetMSiteAlm.WarnValue = info.DeviceTemperatureAlarmValue;
                                    tempeDeviceSetMSiteAlm.AlmValue = info.DeviceTemperatureDangerValue;
                                    if (newWS != null && newWS.DevFormType == (int)EnumWSFormType.WiredSensor)
                                        tempeDeviceSetMSiteAlm.WGID = newWS.WGID;
                                    context.TempeDeviceSetMSiteAlm.Update(context, tempeDeviceSetMSiteAlm);
                                    if (newWS != null && newWS.DevFormType == (int)EnumWSFormType.WiredSensor)
                                    {
                                        context.ModBusRegisterAddress.Delete<ModBusRegisterAddress>(context,
                                            item => item.MDFID == tempeDeviceSetMSiteAlm.MsiteAlmID
                                                && item.MDFResourceTable == "T_SYS_TEMPE_DEVICE_SET_MSITEALM");

                                        ModBusRegisterAddress regAddress = new ModBusRegisterAddress();
                                        regAddress.MDFID = tempeDeviceSetMSiteAlm.MsiteAlmID;
                                        regAddress.MDFResourceTable = "T_SYS_TEMPE_DEVICE_SET_MSITEALM";
                                        regAddress.RegisterAddress = info.TemperatureWSID;
                                        regAddress.StrEnumRegisterStorageMode = 2;
                                        regAddress.StrEnumRegisterStorageSequenceMode = 2;
                                        regAddress.StrEnumRegisterType = 1;
                                        regAddress.StrEnumRegisterInformation = 0;
                                        context.ModBusRegisterAddress.AddNew(context, regAddress);
                                    }
                                }
                                else
                                {
                                    TempeDeviceSetMSiteAlm tempeDeviceSetMSiteAlm = new TempeDeviceSetMSiteAlm();
                                    tempeDeviceSetMSiteAlm.MsiteID = info.MeasureSiteID;
                                    tempeDeviceSetMSiteAlm.WarnValue = info.DeviceTemperatureAlarmValue;
                                    tempeDeviceSetMSiteAlm.AlmValue = info.DeviceTemperatureDangerValue;
                                    tempeDeviceSetMSiteAlm.AddDate = DateTime.Now;
                                    if (newWS != null && newWS.DevFormType == (int)EnumWSFormType.WiredSensor)
                                        tempeDeviceSetMSiteAlm.WGID = newWS.WGID;
                                    context.TempeDeviceSetMSiteAlm.AddNew(context, tempeDeviceSetMSiteAlm);
                                    if (newWS != null && newWS.DevFormType == (int)EnumWSFormType.WiredSensor)
                                    {
                                        ModBusRegisterAddress regAddress = new ModBusRegisterAddress();
                                        regAddress.MDFID = tempeDeviceSetMSiteAlm.MsiteAlmID;
                                        regAddress.MDFResourceTable = "T_SYS_TEMPE_DEVICE_SET_MSITEALM";
                                        regAddress.RegisterAddress = info.TemperatureWSID;
                                        regAddress.StrEnumRegisterStorageMode = 2;
                                        regAddress.StrEnumRegisterStorageSequenceMode = 2;
                                        regAddress.StrEnumRegisterType = 1;
                                        regAddress.StrEnumRegisterInformation = 0;
                                        context.ModBusRegisterAddress.AddNew(context, regAddress);
                                    }
                                }
                                #endregion

                                #region 添加传感器温度
                                if (info.WSTemperatureAlarmValue == 0 && info.WSTemperatureDangerValue == 0)
                                {
                                    var tempews = context.TempeWSSetMsitealm.Where(item => item.MsiteID == info.MeasureSiteID).ToList();
                                    if (tempews != null && tempews.Any())
                                    {
                                        int measureSiteInfoId = tempews.FirstOrDefault().MsiteID;
                                        context.TempeWSSetMsitealm.Delete(context, tempews);

                                        #region 删除时实表，传感器温度
                                        context.RealTimeAlarmThreshold.Delete(context, item => item.MeasureSiteID == measureSiteInfoId && item.MeasureSiteThresholdType == (int)EnumMeasureSiteThresholdType.WSTemperature);
                                        #endregion

                                        if (realTimeCollectInfo != null)
                                        {
                                            realTimeCollectInfo.MSWSTemperatureStatus = null;
                                            realTimeCollectInfo.MSWSTemperatureValue = null;
                                            realTimeCollectInfo.MSWSTemperatureTime = null;
                                        }
                                    }
                                }
                                else if (info.WSTemperatureMsiteAlmID > 0)
                                {
                                    //修改
                                    TempeWSSetMSiteAlm tempeWSSetMSiteAlm = context.TempeWSSetMsitealm.GetByKey(context, info.WSTemperatureMsiteAlmID);
                                    tempeWSSetMSiteAlm.MsiteID = info.MeasureSiteID;
                                    tempeWSSetMSiteAlm.WarnValue = info.WSTemperatureAlarmValue;
                                    tempeWSSetMSiteAlm.AlmValue = info.WSTemperatureDangerValue;
                                    context.TempeWSSetMsitealm.Update(context, tempeWSSetMSiteAlm);
                                }
                                else
                                {
                                    TempeWSSetMSiteAlm tempeWSSetMSiteAlm = new TempeWSSetMSiteAlm();
                                    tempeWSSetMSiteAlm.MsiteID = info.MeasureSiteID;
                                    tempeWSSetMSiteAlm.WarnValue = info.WSTemperatureAlarmValue;
                                    tempeWSSetMSiteAlm.AlmValue = info.WSTemperatureDangerValue;
                                    tempeWSSetMSiteAlm.AddDate = DateTime.Now;
                                    context.TempeWSSetMsitealm.AddNew(context, tempeWSSetMSiteAlm);
                                }

                                #endregion

                                #region 添加电池电压
                                if (info.VoltageAlarmValue == 0 && info.VoltageDangerValue == 0)
                                {
                                    var voltage = context.VoltageSetMsitealm.Where(item => item.MsiteID == info.MeasureSiteID).ToList();
                                    if (voltage != null && voltage.Any())
                                    {
                                        int measureSiteInfoId = voltage.FirstOrDefault().MsiteID;
                                        context.VoltageSetMsitealm.Delete(context, voltage);

                                        #region 删除时实表，传感器温度
                                        context.RealTimeAlarmThreshold.Delete(context, item => item.MeasureSiteID == measureSiteInfoId && item.MeasureSiteThresholdType == (int)EnumMeasureSiteThresholdType.WSVoltage);
                                        #endregion

                                        if (realTimeCollectInfo != null)
                                        {
                                            realTimeCollectInfo.MSWSBatteryVolatageStatus = null;
                                            realTimeCollectInfo.MSWSBatteryVolatageValue = null;
                                            realTimeCollectInfo.MSWSBatteryVolatageTime = null;
                                        }
                                    }

                                }
                                else if (info.VoltageMsiteAlmID > 0)
                                {
                                    VoltageSetMSiteAlm voltageSetMSiteAlm = context.VoltageSetMsitealm.GetByKey(context, info.VoltageMsiteAlmID);
                                    voltageSetMSiteAlm.MsiteID = info.MeasureSiteID;
                                    voltageSetMSiteAlm.WarnValue = info.VoltageAlarmValue;
                                    voltageSetMSiteAlm.AlmValue = info.VoltageDangerValue;
                                    context.VoltageSetMsitealm.Update(context, voltageSetMSiteAlm);
                                }
                                else
                                {
                                    VoltageSetMSiteAlm voltageSetMSiteAlm = new VoltageSetMSiteAlm();
                                    voltageSetMSiteAlm.MsiteID = info.MeasureSiteID;
                                    voltageSetMSiteAlm.WarnValue = info.VoltageAlarmValue;
                                    voltageSetMSiteAlm.AlmValue = info.VoltageDangerValue;
                                    voltageSetMSiteAlm.AddDate = DateTime.Now;
                                    context.VoltageSetMsitealm.AddNew(context, voltageSetMSiteAlm);
                                }
                                #endregion

                                #region 添加轴承信息

                                var measureSiteBearingList = context.MeasureSiteBearing.Where(item => item.MeasureSiteID == info.MeasureSiteID);
                                if (measureSiteBearingList != null && measureSiteBearingList.Any())
                                {
                                    //删除原有的
                                    context.MeasureSiteBearing.Delete(context, measureSiteBearingList);
                                }

                                int measureSiteId = info.MeasureSiteID;
                                List<MeasureSiteBearing> list = new List<MeasureSiteBearing>();
                                if (info.BearingInfoList != null && info.BearingInfoList.Any())
                                {
                                    foreach (var bearing in info.BearingInfoList)
                                    {
                                        MeasureSiteBearing measureSiteBearing = new MeasureSiteBearing();
                                        measureSiteBearing.BearingID = bearing.BearingID;
                                        measureSiteBearing.MeasureSiteID = measureSiteId;
                                        measureSiteBearing.BearingType = bearing.BearingType;
                                        measureSiteBearing.LubricatingForm = bearing.LubricatingForm;
                                        measureSiteBearing.BearingNum = bearing.BearingNum;
                                        measureSiteBearing.AddDate = DateTime.Now;
                                        context.MeasureSiteBearing.AddNew(context, measureSiteBearing);
                                    }

                                }
                                #endregion

                                operationResult = context.RealTimeCollectInfo.Update(context, realTimeCollectInfo);
                            }
                            else
                            {
                                response.IsSuccessful = false;
                                response.Code = "002011";
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002011";
                return response;
            }

            response.IsSuccessful = true;
            return response;
        }
        #endregion

        #region 编辑测量位置报警配置及报警值信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑测量位置报警配置及报警值信息
        /// </summary>
        /// <param name="parameter">编辑测量位置报警配置及报警值信息参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditMSiteAlmRecordForDeviceTree(EditMSiteAlmRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                OperationResult operationResult = null;

                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "002022";
                    return response;
                }
                Dictionary<EntityBase, EntityState> operEntities = new Dictionary<EntityBase, EntityState>();
                //记录修改前的报警阈值
                float oldAlmValue = 0f;
                float oldWarnValue = 0f;
                switch (parameter.MSDType)
                {
                    case (int)EnumMSiteMonitorType.TempeDevice:
                        TempeDeviceSetMSiteAlm tempeDeviceSetMsitealm = tempeDeviceSetMSiteAlmRepository
                            .GetDatas<TempeDeviceSetMSiteAlm>(obj => obj.MsiteAlmID == parameter.MSiteAlmID, false)
                            .SingleOrDefault();

                        //记录修改前的报警阈值
                        oldAlmValue = tempeDeviceSetMsitealm.AlmValue;
                        oldWarnValue = tempeDeviceSetMsitealm.WarnValue;

                        tempeDeviceSetMsitealm.AlmValue = float.Parse(parameter.AlmValue);
                        tempeDeviceSetMsitealm.WarnValue = float.Parse(parameter.WarnValue);
                        operEntities.Add(tempeDeviceSetMsitealm, EntityState.Modified);
                        //operationResult = tempeDeviceSetMSiteAlmRepository
                        //.Update<TempeDeviceSetMSiteAlm>(tempeDeviceSetMsitealm);

                        #region 当设备温度阈值发生变化时候，结束此阈值对应的报警记录

                        if (oldAlmValue != tempeDeviceSetMsitealm.AlmValue || oldWarnValue != tempeDeviceSetMsitealm.WarnValue)
                        {
                            //由于报警记录已结束，故将起阈值状态设置为 正常
                            if (tempeDeviceSetMsitealm.Status == (int)EnumAlarmStatus.Warning || tempeDeviceSetMsitealm.Status == (int)EnumAlarmStatus.Danger)
                            {
                                tempeDeviceSetMsitealm.Status = (int)EnumAlarmStatus.Normal;
                            }

                            int msiteID = tempeDeviceSetMsitealm.MsiteID;
                            var deviceTempeAlarms = devAlmRecordRepository.GetDatas<DevAlmRecord>(t => t.MSAlmID == 2 && t.MSiteID == msiteID && t.BDate == t.EDate, true).ToList();
                            foreach (var alm in deviceTempeAlarms)
                            {
                                alm.EDate = DateTime.Now;
                                operEntities.Add(alm, EntityState.Modified);
                            }
                        }
                        #endregion

                        operationResult = tempeDeviceSetMSiteAlmRepository.TranMethod(operEntities);

                        if (operationResult.ResultType == EnumOperationResultType.Success)
                        {
                            //重新计算各个父节点的状态                 
                            ModifyStatusBeyondMSite(new iCMSDbContext(), tempeDeviceSetMsitealm.MsiteID);
                            response.IsSuccessful = true;
                            response.Result = true;
                            return response;
                        }
                        else
                        {
                            response.IsSuccessful = false;
                            response.Code = "002031";
                            return response;
                        }

                    case (int)EnumMSiteMonitorType.TempeWS:
                        TempeWSSetMSiteAlm tempeWSSetMSiteAlm = tempeWSSetMSiteAlmRepository
                            .GetDatas<TempeWSSetMSiteAlm>(obj => obj.MsiteAlmID == parameter.MSiteAlmID, false)
                            .SingleOrDefault();
                        oldAlmValue = tempeWSSetMSiteAlm.AlmValue;
                        oldWarnValue = tempeWSSetMSiteAlm.WarnValue;

                        tempeWSSetMSiteAlm.AlmValue = float.Parse(parameter.AlmValue);
                        tempeWSSetMSiteAlm.WarnValue = float.Parse(parameter.WarnValue);
                        operEntities.Add(tempeWSSetMSiteAlm, EntityState.Modified);

                        //找到 ms，然后找到 ws，目的是结束报警后重新计算WS状态
                        var ms = measureSiteRepository.GetDatas<MeasureSite>(t => t.MSiteID == tempeWSSetMSiteAlm.MsiteID, true).FirstOrDefault();
                        int wsID = (null != ms && ms.WSID.HasValue) ? ms.WSID.Value : 0;
                        #region 当传感器温度阈值发生变化时候，结束此阈值对应的报警记录
                        if (oldAlmValue != tempeWSSetMSiteAlm.AlmValue || oldWarnValue != tempeWSSetMSiteAlm.WarnValue)
                        {
                            if (tempeWSSetMSiteAlm.Status == (int)EnumAlarmStatus.Warning || tempeWSSetMSiteAlm.Status == (int)EnumAlarmStatus.Danger)
                            {
                                tempeWSSetMSiteAlm.Status = (int)EnumAlarmStatus.Normal;
                            }

                            int msiteID = tempeWSSetMSiteAlm.MsiteID;
                            var wsTempeAlarms = wsnAlmRecordRepository.GetDatas<WSnAlmRecord>(t => t.MSAlmID == 3 && t.MSiteID == msiteID && t.BDate == t.EDate, true).ToList();
                            foreach (var alm in wsTempeAlarms)
                            {
                                alm.EDate = DateTime.Now;
                                operEntities.Add(alm, EntityState.Modified);
                            }
                        }
                        #endregion

                        operationResult = tempeDeviceSetMSiteAlmRepository.TranMethod(operEntities);
                        if (operationResult.ResultType == EnumOperationResultType.Success)
                        {
                            //重新计算WS状态
                            ModifyWSStatus(wsID);

                            response.IsSuccessful = true;
                            response.Result = true;
                            return response;
                        }
                        else
                        {
                            response.IsSuccessful = false;
                            response.Code = "002031";
                            return response;
                        }

                    case (int)EnumMSiteMonitorType.VoltageSet:
                        VoltageSetMSiteAlm voltageSetMSiteAlm = voltageSetMSiteAlmRepository
                            .GetDatas<VoltageSetMSiteAlm>(obj => obj.MsiteAlmID == parameter.MSiteAlmID, false)
                            .SingleOrDefault();
                        oldAlmValue = voltageSetMSiteAlm.AlmValue;
                        oldWarnValue = voltageSetMSiteAlm.WarnValue;

                        voltageSetMSiteAlm.AlmValue = float.Parse(parameter.AlmValue);
                        voltageSetMSiteAlm.WarnValue = float.Parse(parameter.WarnValue);
                        operEntities.Add(voltageSetMSiteAlm, EntityState.Modified);

                        //找到 ms，然后找到 ws，目的是结束报警后重新计算WS状态
                        var ms2 = measureSiteRepository.GetDatas<MeasureSite>(t => t.MSiteID == voltageSetMSiteAlm.MsiteID, true).FirstOrDefault();
                        int wsID2 = (null != ms2 && ms2.WSID.HasValue) ? ms2.WSID.Value : 0;

                        #region 电池电压阈值发生变化时候，结束此阈值对应的报警记录
                        if (oldAlmValue != voltageSetMSiteAlm.AlmValue || oldWarnValue != voltageSetMSiteAlm.WarnValue)
                        {
                            if (voltageSetMSiteAlm.Status == (int)EnumAlarmStatus.Warning || voltageSetMSiteAlm.Status == (int)EnumAlarmStatus.Danger)
                            {
                                voltageSetMSiteAlm.Status = (int)EnumAlarmStatus.Normal;
                            }


                            int msiteID = voltageSetMSiteAlm.MsiteID;
                            var voltaAlarms = wsnAlmRecordRepository.GetDatas<WSnAlmRecord>(t => t.MSAlmID == 4 && t.MSiteID == msiteID && t.BDate == t.EDate, true).ToList();
                            foreach (var alm in voltaAlarms)
                            {
                                alm.EDate = DateTime.Now;
                                operEntities.Add(alm, EntityState.Modified);
                            }
                        }
                        #endregion

                        operationResult = voltageSetMSiteAlmRepository.TranMethod(operEntities);
                        if (operationResult.ResultType == EnumOperationResultType.Success)
                        {
                            //重新计算WS状态
                            ModifyWSStatus(wsID2);

                            response.IsSuccessful = true;
                            response.Result = true;
                            return response;
                        }
                        else
                        {
                            response.IsSuccessful = false;
                            response.Code = "002031";
                            return response;
                        }

                    default:
                        response.IsSuccessful = false;
                        response.Code = "002031";
                        return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002031";
                return response;
            }
        }
        #endregion

        #region 编辑振动信号信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑振动信号信息
        /// </summary>
        /// <param name="parameter">编辑振动信号信息参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditVibSignalRecordForDeviceTree(EditVibSignalRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "002042";
                    return response;
                }

                VibSingal oldVibsingal = vibSignalRepository
                    .GetDatas<VibSingal>(obj => obj.SingalID == parameter.SignalID, false)
                    .SingleOrDefault();
                oldVibsingal.DAQStyle = parameter.DAQStyle;
                oldVibsingal.MSiteID = parameter.MSiteID;
                oldVibsingal.SingalType = parameter.SignalType;
                oldVibsingal.UpLimitFrequency = parameter.UpLimitFrequency;
                oldVibsingal.LowLimitFrequency = parameter.LowLimitFrequency;
                oldVibsingal.WaveDataLength = parameter.WaveDataLength;
                oldVibsingal.EnlvpBandW = parameter.EnlvpBandW;
                oldVibsingal.EnlvpFilter = parameter.EnlvpFilter;
                oldVibsingal.Remark = parameter.Remark;
                OperationResult operationResult = vibSignalRepository
                    .Update<VibSingal>(oldVibsingal);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "002051";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002051";
                return response;
            }
        }
        #endregion

        #region 编辑特征值及报警值信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑特征值及报警值信息
        /// </summary>
        /// <param name="parameter">编辑特征值及报警值信息参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditSignalAlmRecordForDeviceTree(EditSignalAlmRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            float oldAlmValue = 0f;
            float oldWarnValue = 0f;
            Dictionary<EntityBase, EntityState> operEntities = new Dictionary<EntityBase, EntityState>();

            try
            {
                OperationResult operationResult = null;

                if (parameter == null)
                {
                    response.IsSuccessful = false;
                    response.Code = "002062";
                    return response;
                }

                SignalAlmSet signalAlmSet = signalAlmSetRepository
                    .GetDatas<SignalAlmSet>(obj => obj.SingalAlmID == parameter.SignalAlmID, false)
                    .SingleOrDefault();
                //记录修改前的振动信号阈值
                oldAlmValue = signalAlmSet.AlmValue;
                oldWarnValue = signalAlmSet.WarnValue;

                signalAlmSet.AlmValue = float.Parse(parameter.AlmValue);
                signalAlmSet.ValueType = parameter.ValueType;
                signalAlmSet.WarnValue = float.Parse(parameter.WarnValue);
                signalAlmSet.UploadTrigger = parameter.UploadTrigger;
                signalAlmSet.ThrendAlarmPrvalue = parameter.ThrendAlarmPrvalue;

                operEntities.Add(signalAlmSet, EntityState.Modified);

                #region 如果振动信号阈值发生更改，则结束此阈值对应的报警记录,由于此阈值下的报警记录被结束，所以设置此阈值的报警状态为 1，并重置所有父级监测树的状态
                if (oldAlmValue != signalAlmSet.AlmValue || oldWarnValue != signalAlmSet.WarnValue)
                {
                    if (signalAlmSet.Status == (int)EnumAlarmStatus.Warning || signalAlmSet.Status == (int)EnumAlarmStatus.Danger)
                    {
                        signalAlmSet.Status = (int)EnumAlarmStatus.Normal;
                    }

                    var signalAlmRecord = devAlmRecordRepository.GetDatas<DevAlmRecord>(t => t.MSAlmID == 1
                        && t.SingalAlmID == signalAlmSet.SingalAlmID
                        && t.BDate == t.EDate, true).ToList();

                    foreach (var alm in signalAlmRecord)
                    {
                        alm.EDate = DateTime.Now;
                        operEntities.Add(alm, EntityState.Modified);
                    }
                }
                #endregion

                //operationResult = signalAlmSetRepository
                //    .Update<SignalAlmSet>(signalAlmSet);
                operationResult = signalAlmSetRepository.TranMethod(operEntities);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    #region 清空三次报警 王颖辉 2017-03-08 如果阈值修改，三次报警清零
                    RecordAlarmCount recordAlarmCout = CollectionsExtensions.recordAlmCountList
                              .Where(p => p.SignalID == signalAlmSet.SingalID && p.EigenValueTypeID == signalAlmSet.ValueType)
                              .FirstOrDefault();
                    if (recordAlarmCout != null)
                    {
                        recordAlarmCout.AlarmCount = 0;
                        recordAlarmCout.WarnCount = 0;
                    }
                    #endregion




                    //重新计算父级监测树的状态
                    ModifyStatusBeyondSignal(new iCMSDbContext(), signalAlmSet.SingalID);
                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "002071";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002071";
                return response;
            }
        }
        #endregion

        #region 删除设备信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：删除设备信息
        /// </summary>
        /// <param name="parameter">删除设备信息参数</param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteDeviceRecordForDeviceTree(DeleteDeviceRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                SqlParameter[] delparms = new SqlParameter[1];
                delparms[0] = new SqlParameter("@DevId", parameter.DeviceID);

                return ExecuteDB.ExecuteTrans((context) =>
                {
                    #region 同步修改父节点状态，Added by QXM, 2017/01/09
                    int monitorTreeID = 0;
                    var device = context.Device.GetByKey(context, parameter.DeviceID);
                    if (device != null)
                    {
                        monitorTreeID = device.MonitorTreeID;
                    }
                    #endregion

                    OperationResult operationResult = context.Device
                        .ExecuteSqlCommand(context, ConstObject.SQL_DeleteDevice, delparms);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        #region 同步修改父节点状态，Added by QXM, 2017/01/06
                        UpdateMonitorTreeStatus(context, monitorTreeID);
                        #endregion

                        #region 删除实时数据表
                        //删除原有的
                        context.RealTimeCollectInfo.Delete(context, item => item.DevID == parameter.DeviceID);
                        #endregion

                        response.IsSuccessful = true;
                        response.Result = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccessful = false;
                        response.Code = "002081";
                        return response;
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002081";
                return response;
            }
        }
        #endregion

        #region 删除测量位置信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：删除测量位置信息
        /// </summary>
        /// <param name="parameter">删除测量位置信息信息参数</param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteMSiteRecordForDeviceTree(DeleteMSiteRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    var measureSite = context.Measuresite.GetByKey(context, parameter.MSiteID);

                    #region 同步修改父节点状态，Added by QXM, 2017/01/06
                    int deviceID = 0;

                    if (measureSite != null)
                    {
                        deviceID = measureSite.DevID;
                    }
                    #endregion

                    SqlParameter[] delparms = new SqlParameter[1];
                    delparms[0] = new SqlParameter("@MSiteID", parameter.MSiteID);
                    OperationResult operationResult = context.Measuresite
                        .ExecuteSqlCommand(context, ConstObject.SQL_DeleteMeasureSite, delparms);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        #region 同步修改父节点状态，Added by QXM, 2017/01/09
                        ModifyDeviceStatus(context, deviceID);
                        #endregion

                        //删除测点后，测点关联的WS状态设置为 未使用 0， Modified by QXM
                        if (measureSite.WSID.HasValue)
                        {
                            //var wsInDb = wsRepository.GetByKey(measureSite.WSID.Value);
                            var wsInDb = context.WS
                                .Where(p => p.WSID == measureSite.WSID.Value)
                                .SingleOrDefault();
                            if (wsInDb != null)
                            {
                                wsInDb.UseStatus = 0;
                                context.WS.Update(context, wsInDb);
                            }

                            #region 删除轴承关系表
                            //删除原有的
                            context.MeasureSiteBearing.Delete(context, item => item.MeasureSiteID == measureSite.WSID);
                            #endregion

                            #region 删除实时数据表
                            //删除原有的
                            context.RealTimeCollectInfo.Delete<RealTimeCollectInfo>(context, item => item.MSID == parameter.MSiteID);
                            #endregion

                        }


                        response.IsSuccessful = true;
                        response.Result = true;
                        return response;
                    }
                    else
                    {
                        response.IsSuccessful = false;
                        response.Code = "002091";
                        return response;
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002091";
                return response;
            }
        }
        #endregion

        #region 删除振动信号信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：删除振动信号信息
        /// </summary>
        /// <param name="parameter">删除振动信号信息参数</param>
        /// <returns></returns>
        public BaseResponse<bool> DeteleVibSignalRecordForDeviceTree(DeteleVibSignalRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                #region 同步修改父节点状态，Added by QXM, 2017/01/06
                int measureSiteID = 0;
                var vibSignal = vibSignalRepository.GetByKey(parameter.SignalID);
                if (vibSignal != null)
                {
                    measureSiteID = vibSignal.MSiteID;
                }
                #endregion

                SqlParameter[] delparms = new SqlParameter[1];
                delparms[0] = new SqlParameter("@SingalID", parameter.SignalID);



                OperationResult operationResult = vibSignalRepository
                    .ExecuteSqlCommand(ConstObject.SQL_DeleteVibSingal, delparms);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    #region 同步修改父节点状态，Added by QXM, 2017/01/06
                    ModifyStatusBeyondMSite(new iCMSDbContext(), measureSiteID);
                    #endregion

                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "002101";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002101";
                return response;
            }
        }
        #endregion

        #region 删除测量位置报警配置及报警值信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：删除测量位置报警配置及报警值信息
        /// </summary>
        /// <param name="parameter">删除测量位置报警配置及报警值信息参数</param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteMSiteAlmRecordForDeviceTree(DeleteMSiteAlmRecordForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            int MeasureSiteID = 0;
            try
            {
                OperationResult operationResult = null;

                return ExecuteDB.ExecuteTrans((context) =>
                {
                    switch (parameter.MSiteAlmType)
                    {
                        case (int)EnumMSiteMonitorType.TempeDevice:
                            List<DevAlmRecord> devTempAlarmsRecords = new List<DevAlmRecord>();

                            #region 记录此设备温度设置对应的msid

                            var msite = context.TempeDeviceSetMSiteAlm
                                .GetDatas<TempeDeviceSetMSiteAlm>(context, t => t.MsiteAlmID == parameter.MSiteAlmID)
                                .FirstOrDefault();
                            if (msite != null)
                            {
                                var msiteID = msite.MsiteID;
                                devTempAlarmsRecords = context.DevAlmRecord
                                    .GetDatas<DevAlmRecord>(context, t => t.MSAlmID == 2
                                        && t.BDate == t.EDate
                                        && t.MSiteID == msiteID)
                                    .ToList();

                                MeasureSiteID = msite.MsiteID;
                            }

                            #endregion

                            TempeDeviceSetMSiteAlm tempeDeviceSetMsitealm = context.TempeDeviceSetMSiteAlm
                                .GetByKey(context, parameter.MSiteAlmID);
                            operationResult = context.TempeDeviceSetMSiteAlm
                                .Delete(context, tempeDeviceSetMsitealm);
                            if (operationResult.ResultType == EnumOperationResultType.Success)
                            {
                                #region 同步修改父节点状态，Added by QXM, 2017/01/06
                                ModifyStatusBeyondMSite(context, MeasureSiteID);
                                #endregion

                                #region 将本地的设备温度告警记录结束掉

                                if (devTempAlarmsRecords.Any())
                                {
                                    foreach (var item in devTempAlarmsRecords)
                                    {
                                        item.EDate = DateTime.Now;
                                    }
                                    context.DevAlmRecord.Update(context, devTempAlarmsRecords);
                                }

                                #endregion

                                response.IsSuccessful = true;
                                response.Result = true;
                                return response;
                            }
                            else
                            {
                                response.IsSuccessful = false;
                                response.Code = "002111";
                                return response;
                            }

                        case (int)EnumMSiteMonitorType.TempeWS:
                            TempeWSSetMSiteAlm tempeWSSetMSiteAlm = context.TempeWSSetMsitealm
                                .GetByKey(context, parameter.MSiteAlmID);

                            if (tempeWSSetMSiteAlm != null)
                            {
                                MeasureSiteID = tempeWSSetMSiteAlm.MsiteID;
                            }

                            operationResult = context.TempeWSSetMsitealm
                                .Delete(context, tempeWSSetMSiteAlm);
                            if (operationResult.ResultType == EnumOperationResultType.Success)
                            {
                                #region 同步修改父节点状态，Added by QXM, 2017/01/06
                                ModifyStatusBeyondMSite(context, MeasureSiteID);
                                #endregion

                                response.IsSuccessful = true;
                                response.Result = true;
                                return response;
                            }
                            else
                            {
                                response.IsSuccessful = false;
                                response.Code = "002111";
                                return response;
                            }

                        case (int)EnumMSiteMonitorType.VoltageSet:
                            List<WSnAlmRecord> voltageAlarmsRecords = new List<WSnAlmRecord>();

                            #region 删除云平台的电池电压告警

                            var msiteVolat = context.VoltageSetMsitealm
                                .GetDatas<VoltageSetMSiteAlm>(context, t => t.MsiteAlmID == parameter.MSiteAlmID)
                                .FirstOrDefault();
                            if (msiteVolat != null)
                            {
                                var msiteID = msiteVolat.MsiteID;
                                voltageAlarmsRecords = context.WsnAlmrecord
                                    .GetDatas<WSnAlmRecord>(context, t => t.MSAlmID == 4
                                        && t.BDate == t.EDate
                                        && t.MSiteID == msiteID)
                                    .ToList();
                            }

                            #endregion

                            VoltageSetMSiteAlm voltageSetMSiteAlm = context.VoltageSetMsitealm
                                .GetByKey(context, parameter.MSiteAlmID);

                            if (voltageSetMSiteAlm != null)
                            {
                                MeasureSiteID = voltageSetMSiteAlm.MsiteID;
                            }

                            operationResult = context.VoltageSetMsitealm
                                .Delete<VoltageSetMSiteAlm>(context, voltageSetMSiteAlm);
                            if (operationResult.ResultType == EnumOperationResultType.Success)
                            {
                                #region 同步修改父节点状态，Added by QXM, 2017/01/06
                                ModifyStatusBeyondMSite(context, MeasureSiteID);
                                #endregion

                                #region 将本地的电池电压告警记录结束掉

                                if (voltageAlarmsRecords.Any())
                                {
                                    foreach (var item in voltageAlarmsRecords)
                                    {
                                        item.EDate = DateTime.Now;
                                    }
                                    context.WsnAlmrecord.Update(context, voltageAlarmsRecords);
                                }

                                #endregion

                                response.IsSuccessful = true;
                                response.Result = true;
                                return response;
                            }
                            else
                            {
                                response.IsSuccessful = false;
                                response.Code = "002111";
                                return response;
                            }

                        default:
                            response.IsSuccessful = false;
                            response.Code = "002111";
                            return response;
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "002111";
                return response;
            }
        }
        #endregion

        #region 获取特征值删除接口
        /// <summary>
        /// 获取特征值删除接口
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> DeleteSignalAlmRecordForDeviceTree(DeleteSignalAlmParameter param)
        {
            OperationResult operationResult = null;
            BaseResponse<bool> response = null;
            int SignalID = 0;
            try
            {
                #region 查询此振动信号阈值包含的报警记录，并结束掉本地的报警记录
                var vibrationAlmRecords = devAlmRecordRepository.GetDatas<DevAlmRecord>(
                        t => t.MSAlmID == 1
                    && t.SingalAlmID == param.SignalAlmID
                    && t.BDate == t.EDate, true).ToList();
                #endregion

                SignalAlmSet signalAlmSet = signalAlmSetRepository
                    .GetByKey(param.SignalAlmID);
                if (signalAlmSet != null)
                {
                    SignalID = signalAlmSet.SingalID;
                }

                operationResult = signalAlmSetRepository
                    .Delete<SignalAlmSet>(signalAlmSet);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    #region 同步修改父节点状态，Added by QXM, 2017/01/06
                    ModifyStatusBeyondSignal(new iCMSDbContext(), SignalID);
                    #endregion

                    foreach (var item in vibrationAlmRecords)
                    {
                        item.EDate = DateTime.Now;
                        devAlmRecordRepository.Update<DevAlmRecord>(item);
                    }
                    response = new BaseResponse<bool>();
                }
                else
                {
                    response = new BaseResponse<bool>("002121");
                }
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<bool>("002121");
                return response;
            }
        }
        #endregion

        #region 复制单个测点的测量定义
        /// <summary>
        /// 复制单个测点的测量定义
        /// </summary>
        /// <returns></returns>
        public BaseResponse<CopyMSResult> CopySingleMS(int SourceMSId, int TargetDevId, int TargetMsName, int? WSID = 0)
        {
            CopyMSResult result = new CopyMSResult();
            OperationResult dbOperationResult = null;
            BaseResponse<CopyMSResult> response = null;

            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    var srcMeasureSite = context.Measuresite
                        .GetByKey(context, SourceMSId);
                    if (srcMeasureSite == null)
                    {
                        response = new BaseResponse<CopyMSResult>("002132");
                        return response;
                    }
                    var newMeasureSite = new MeasureSite();
                    newMeasureSite.WSID = WSID;
                    newMeasureSite.DevID = TargetDevId;
                    newMeasureSite.MSiteName = TargetMsName;

                    //复制测量位置后，相应的WS状态设置为 已使用
                    if (WSID.HasValue && WSID != -1 && WSID != 0)
                    {
                        var wsInDb = context.WS.GetByKey(context, WSID);
                        if (wsInDb != null)
                        {
                            wsInDb.UseStatus = 1;
                            context.WS.Update(context, wsInDb);
                        }
                    }

                    //测量位置属性复制
                    FillMeasreSite(newMeasureSite, srcMeasureSite);
                    dbOperationResult = context.Measuresite.AddNew(context, newMeasureSite);

                    //WS温度报警设置
                    var wsTempAlmSet = context.TempeWSSetMsitealm
                        .GetDatas<TempeWSSetMSiteAlm>(context, t => t.MsiteID == SourceMSId)
                        .FirstOrDefault();
                    if (wsTempAlmSet != null)
                    {
                        TempeWSSetMSiteAlm newTempAlmSet = new TempeWSSetMSiteAlm();
                        newTempAlmSet.MsiteID = newMeasureSite.MSiteID;
                        newTempAlmSet.WarnValue = wsTempAlmSet.WarnValue;
                        newTempAlmSet.AlmValue = wsTempAlmSet.AlmValue;
                        newTempAlmSet.Status = 0;
                        dbOperationResult = context.TempeWSSetMsitealm.AddNew(context, newTempAlmSet);
                    }
                    //设备温度报警设置
                    TempeDeviceSetMSiteAlm deviceTempeAlmSet = context.TempeDeviceSetMSiteAlm
                        .GetDatas<TempeDeviceSetMSiteAlm>(context, t => t.MsiteID == SourceMSId)
                        .FirstOrDefault();
                    if (deviceTempeAlmSet != null)
                    {
                        TempeDeviceSetMSiteAlm newDeviceTempeAlmSet = new TempeDeviceSetMSiteAlm();
                        newDeviceTempeAlmSet.MsiteID = newMeasureSite.MSiteID;
                        newDeviceTempeAlmSet.WarnValue = deviceTempeAlmSet.WarnValue;
                        newDeviceTempeAlmSet.AlmValue = deviceTempeAlmSet.AlmValue;
                        newDeviceTempeAlmSet.Status = 0;
                        dbOperationResult = context.TempeDeviceSetMSiteAlm.AddNew(context, newDeviceTempeAlmSet);
                    }
                    //电池电压
                    VoltageSetMSiteAlm voltAlmSet = context.VoltageSetMsitealm
                        .GetDatas<VoltageSetMSiteAlm>(context, t => t.MsiteID == SourceMSId)
                        .FirstOrDefault();
                    if (voltAlmSet != null)
                    {
                        VoltageSetMSiteAlm newVoltaAlmSet = new VoltageSetMSiteAlm();
                        newVoltaAlmSet.MsiteID = newMeasureSite.MSiteID;
                        newVoltaAlmSet.WarnValue = voltAlmSet.WarnValue;
                        newVoltaAlmSet.AlmValue = voltAlmSet.AlmValue;
                        newVoltaAlmSet.Status = 0;
                        dbOperationResult = context.VoltageSetMsitealm.AddNew(context, newVoltaAlmSet);
                    }

                    //振动信号复制
                    var vibSignals = context.VibSingal
                        .GetDatas<VibSingal>(context, t => t.MSiteID == SourceMSId)
                        .ToList();
                    foreach (VibSingal vibSignal in vibSignals)
                    {
                        VibSingal newVibSignal = new VibSingal();
                        newVibSignal.MSiteID = newMeasureSite.MSiteID;
                        FillVibSignal(newVibSignal, vibSignal);
                        dbOperationResult = context.VibSingal.AddNew(context, newVibSignal);

                        var almSets = context.SignalAlmSet.Where(p => p.SingalID == vibSignal.SingalID);
                        foreach (var almSet in almSets)
                        {
                            SignalAlmSet newAlmSet = new SignalAlmSet();
                            newAlmSet.SingalID = newVibSignal.SingalID;

                            FillAlmSet(newAlmSet, almSet);
                            dbOperationResult = context.SignalAlmSet.AddNew(context, newAlmSet);
                        }
                    }

                    #region 添加测点后，直接在实时数据表中写入测点
                    //添加测点后，直接在实时数据表中写入测点
                    MeasureSiteType siteType = cacheDICT.GetInstance()
                        .GetCacheType<MeasureSiteType>(p => p.ID == newMeasureSite.MSiteName)
                        .FirstOrDefault();
                    RealTimeCollectInfo realTimeCollectInfo = new RealTimeCollectInfo();
                    realTimeCollectInfo.DevID = newMeasureSite.DevID;
                    realTimeCollectInfo.MSID = newMeasureSite.MSiteID;
                    realTimeCollectInfo.MSStatus = newMeasureSite.MSiteStatus;
                    realTimeCollectInfo.MSDesInfo = siteType.Describe;
                    realTimeCollectInfo.MSDataStatus = newMeasureSite.MSiteStatus;
                    realTimeCollectInfo.MSName = siteType.Name;
                    realTimeCollectInfo.AddDate = DateTime.Now;
                    context.RealTimeCollectInfo.AddNew(context, realTimeCollectInfo);

                    #endregion

                    #region 添加轴承信息
                    int measureSiteId = newMeasureSite.MSiteID;
                    List<MeasureSiteBearing> srcMeasureSiteBearing = context.MeasureSiteBearing
                        .GetDatas<MeasureSiteBearing>(context, p => p.MeasureSiteID == srcMeasureSite.MSiteID)
                        .ToList();
                    List<MeasureSiteBearing> list = new List<MeasureSiteBearing>();
                    if (srcMeasureSiteBearing != null && srcMeasureSiteBearing.Any())
                    {
                        foreach (var info in srcMeasureSiteBearing)
                        {
                            MeasureSiteBearing measureSiteBearing = new MeasureSiteBearing();
                            measureSiteBearing.BearingID = info.BearingID;
                            measureSiteBearing.MeasureSiteID = measureSiteId;
                            measureSiteBearing.BearingType = info.BearingType;
                            measureSiteBearing.LubricatingForm = info.LubricatingForm;
                            measureSiteBearing.BearingNum = info.BearingNum;
                            measureSiteBearing.AddDate = DateTime.Now;
                            list.Add(measureSiteBearing);
                        }

                        context.MeasureSiteBearing.AddNew(context, list);
                    }
                    #endregion

                    result.MSIDList = new List<int> { newMeasureSite.MSiteID };
                    response = new BaseResponse<CopyMSResult>();
                    response.Result = result;
                    return response;
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<CopyMSResult>("002141");
                return response;
            }
        }
        #endregion

        #region 复制设备下所有的测量定义
        /// <summary>
        /// 复制设备下所有的测量定义
        /// </summary>
        /// <returns></returns>
        public BaseResponse<CopyMSResult> CopyAllMS(int TargetDevId, string MSAndWSStr)
        {
            CopyMSResult result = new CopyMSResult();
            BaseResponse<CopyMSResult> response = new BaseResponse<CopyMSResult>();

            try
            {
                List<int> msidList = new List<int>();
                var msid_wsidMaps = MSAndWSStr.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < msid_wsidMaps.Length; i++)
                {
                    var map = msid_wsidMaps[i].Split(new char[] { '#' },
                        StringSplitOptions.RemoveEmptyEntries);

                    #region 传递MS TYPE 为了应用 CopySingleMS()

                    var srcMSID = Convert.ToInt32(map[0]);
                    var srcMSName = measureSiteRepository
                        .GetByKey(srcMSID).MSiteName;

                    #endregion

                    BaseResponse<CopyMSResult> tempCopyMSResult = new BaseResponse<CopyMSResult>();

                    if (map.Length == 1)
                    {
                        tempCopyMSResult = CopySingleMS(Convert.ToInt32(map[0]),
                            TargetDevId, srcMSName);
                    }
                    else
                    {
                        tempCopyMSResult = CopySingleMS(Convert.ToInt32(map[0]),
                            TargetDevId, srcMSName, Convert.ToInt32(map[1]));
                    }

                    msidList.AddRange(tempCopyMSResult.Result.MSIDList);
                }
                result.MSIDList = msidList;
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CopyMSResult>("002151");
                return response;
            }
        }
        #endregion

        #region 设备切换
        /// <summary>
        /// 设备切换
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> ChangeDevUsedType(ChangeDevUsedTypeParameter param)
        {
            BaseResponse<bool> response = null;

            #region 添加验证
            //设备切换时，要切换的设备ID不正确
            if (param.UsedDevId < 1)
            {
                response = new BaseResponse<bool>("009582");
                return response;
            }

            //设备切换时，要切换的备用设备ID不正确
            if (param.UnUsedDevId < 1)
            {
                response = new BaseResponse<bool>("009592");
                return response;
            }

            #endregion
            try
            {
                #region 主备机测点数不一致，禁止执行主备机切换动作

                List<MeasureSite> UsedDevMSList = measureSiteRepository
                                          .GetDatas<MeasureSite>(obj => obj.DevID == param.UsedDevId, false)
                                          .ToList();
                //设备切换时，主用设备未找到测量位置
                if (UsedDevMSList.Count == 0)
                {
                    response = new BaseResponse<bool>("009612");
                    return response;
                }
                List<MeasureSite> UnUsedDevMSList = measureSiteRepository
                                                    .GetDatas<MeasureSite>(obj => obj.DevID == param.UnUsedDevId, false)
                                                    .ToList();
                //设备切换时，备用设备未找到测量位置
                if (UnUsedDevMSList.Count == 0)
                {
                    response = new BaseResponse<bool>("009622");
                    return response;
                }
                List<int> UsedDevMS = UsedDevMSList
                    .Select(t => t.MSiteID)
                    .ToList();
                List<int> UnUsedDevMS = UnUsedDevMSList
                    .Select(t => t.MSiteID)
                    .ToList();

                if (UsedDevMS.Count() != UnUsedDevMS.Count())
                {
                    response = new BaseResponse<bool>("002162");
                    return response;
                }

                #endregion

                Dictionary<EntityBase, EntityState> operatpr = new Dictionary<EntityBase, EntityState>();
                Device UsedDev = deviceRepository.GetByKey(param.UsedDevId);
                UsedDev.RunStatus = 3;
                UsedDev.UseType = 1;
                UsedDev.AlmStatus = 0;
                operatpr.Add(UsedDev, EntityState.Modified);
                Device UnUsedDev = deviceRepository.GetByKey(param.UnUsedDevId);
                UnUsedDev.RunStatus = 1;
                UnUsedDev.UseType = 0;
                UnUsedDev.AlmStatus = 0;
                operatpr.Add(UnUsedDev, EntityState.Modified);

                List<int> MappedUsedDevMS = new List<int>();
                foreach (var measureSite in UsedDevMSList)
                {
                    var msidUsed = measureSite.MSiteID;

                    //未使用Id
                    var unUsedMeasureSite = UnUsedDevMSList.Where(item => item.MSiteName == measureSite.MSiteName).FirstOrDefault();

                    var msidUnUsed = unUsedMeasureSite.MSiteID;
                    MeasureSite UserMeasureSite = measureSiteRepository
                        .GetDatas<MeasureSite>(obj => obj.DevID == param.UsedDevId
                            && obj.MSiteID == msidUsed, false)
                        .FirstOrDefault();
                    MeasureSite UnUserMeasureSite = measureSiteRepository
                        .GetDatas<MeasureSite>(obj => obj.DevID == param.UnUsedDevId
                            && obj.MSiteID == msidUnUsed, false)
                        .FirstOrDefault();
                    UnUserMeasureSite.WSID = int.Parse(UserMeasureSite.WSID.ToString());
                    operatpr.Add(UnUserMeasureSite, EntityState.Modified);
                    UserMeasureSite.WSID = null;

                    UserMeasureSite.MSiteStatus = (int)EnumAlarmStatus.Unused;
                    operatpr.Add(UserMeasureSite, EntityState.Modified);

                    MappedUsedDevMS.Add(msidUsed);
                }

                #region 将没有映射的MS.WSID 置 null，并将 对应WS.UseStatus = 0

                var leftUsedDevMS = UsedDevMS.Except(MappedUsedDevMS);
                foreach (int msID in leftUsedDevMS)
                {
                    var ms = measureSiteRepository.GetByKey(msID);
                    var tempWs = wsRepository.GetByKey(ms.WSID);
                    if (tempWs != null)
                    {
                        tempWs.UseStatus = 0;
                        operatpr.Add(tempWs, EntityState.Modified);
                    }
                    ms.WSID = null;
                    operatpr.Add(ms, EntityState.Modified);
                }

                #endregion

                #region 主备机切换附加操作：结束主机振动信号报警，设备温度报警，电池电压报警，WS连接状态报警；重置主机下各个节点的报警状态为 0
                //Added by QXM, 2017/02/21
                var usedMeasureSiteList = measureSiteRepository.GetDatas<MeasureSite>(t => t.DevID == param.UsedDevId, true).ToList();
                var usedMeasureSiteIDList = usedMeasureSiteList.Select(t => t.MSiteID).ToList();
                var usedVibsignalList = vibSignalRepository.GetDatas<VibSingal>(t => usedMeasureSiteIDList.Contains(t.MSiteID), true).ToList();
                var usedVibsignalIDList = usedVibsignalList.Select(t => t.SingalID).ToList();
                var usedWSIDList = usedMeasureSiteList.Where(t => t.WSID.HasValue).Select(t => t.WSID.Value).ToList();
                var usedWSList = wsRepository.GetDatas<WS>(t => usedWSIDList.Contains(t.WSID), true).ToList();

                //1.振动信号报警
                var vibSignalAlarms = devAlmRecordRepository.GetDatas<DevAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 1 && usedVibsignalIDList.Contains(t.SingalID), true).ToList();
                foreach (DevAlmRecord alm in vibSignalAlarms)
                {
                    alm.EDate = DateTime.Now;
                    operatpr.Add(alm, EntityState.Modified);
                }
                //2.设备温度报警,传感器温度报警
                var devTempAlarms = devAlmRecordRepository.GetDatas<DevAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 2 && usedMeasureSiteIDList.Contains(t.MSiteID), true).ToList();
                foreach (DevAlmRecord alm in devTempAlarms)
                {
                    alm.EDate = DateTime.Now;
                    operatpr.Add(alm, EntityState.Modified);
                }

                var wsTempAlarms = wsnAlmRecordRepository.GetDatas<WSnAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 3 && usedWSIDList.Contains(t.WSID), true).ToList();
                foreach (var alm in wsTempAlarms)
                {
                    alm.EDate = DateTime.Now;
                    operatpr.Add(alm, EntityState.Modified);
                }
                //3.电池电压报警
                var voltaAlarms = wsnAlmRecordRepository.GetDatas<WSnAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 4 && usedWSIDList.Contains(t.WSID), true).ToList();
                foreach (WSnAlmRecord alm in voltaAlarms)
                {
                    alm.EDate = DateTime.Now;
                    operatpr.Add(alm, EntityState.Modified);
                }
                //4.WS连接状态报警
                var wsLinkAlarms = wsnAlmRecordRepository.GetDatas<WSnAlmRecord>(t => t.BDate == t.EDate && t.MSAlmID == 5 && usedWSIDList.Contains(t.WSID), true).ToList();
                foreach (WSnAlmRecord alm in wsLinkAlarms)
                {
                    alm.EDate = DateTime.Now;
                    operatpr.Add(alm, EntityState.Modified);
                }
                //5.重置主机下各个节点的报警状态为 0
                var sigalAlmsetList = signalAlmSetRepository.GetDatas<SignalAlmSet>(t => usedVibsignalIDList.Contains(t.SingalID), true).ToList();
                //5.1测点
                //foreach (MeasureSite ms in usedMeasureSiteList)
                //{
                //    ms.MSiteStatus = (int)EnumAlarmStatus.Unused;

                //    if (!operatpr.ContainsKey(ms))
                //    {
                //        operatpr.Add(ms, EntityState.Modified);
                //    }

                //}
                //5.2振动信号
                foreach (var vibSignal in usedVibsignalList)
                {
                    vibSignal.SingalStatus = (int)EnumAlarmStatus.Unused;
                    operatpr.Add(vibSignal, EntityState.Modified);
                }
                //5.3振动信号阈值
                foreach (var vibAlmset in sigalAlmsetList)
                {
                    vibAlmset.Status = (int)EnumAlarmStatus.Unused;
                    operatpr.Add(vibAlmset, EntityState.Modified);
                }
                //5.4设备温度阈值
                var deviceTempAlmsetList = tempeDeviceSetMSiteAlmRepository.GetDatas<TempeDeviceSetMSiteAlm>(t => usedMeasureSiteIDList.Contains(t.MsiteID), true).ToList();
                foreach (var item in deviceTempAlmsetList)
                {
                    item.Status = (int)EnumAlarmStatus.Unused;
                    operatpr.Add(item, EntityState.Modified);
                }
                //5.5电池电压阈值
                var voltqAlmsetList = voltageSetMSiteAlmRepository.GetDatas<VoltageSetMSiteAlm>(t => usedMeasureSiteIDList.Contains(t.MsiteID), true).ToList();
                foreach (var item in voltqAlmsetList)
                {
                    item.Status = (int)EnumAlarmStatus.Unused;
                    operatpr.Add(item, EntityState.Modified);
                }
                //5.6传感器温度
                var wsTempAlmsetList = tempeWSSetMSiteAlmRepository.GetDatas<TempeWSSetMSiteAlm>(t => usedMeasureSiteIDList.Contains(t.MsiteID), true).ToList();
                foreach (var item in wsTempAlmsetList)
                {
                    item.Status = (int)EnumAlarmStatus.Unused;
                    operatpr.Add(item, EntityState.Modified);
                }
                #endregion

                OperationResult operationResult = measureSiteRepository
                    .TranMethod(operatpr);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //#region 主备机切换后,结束被切换的主机的所有的报警记录,ADDED BY QXM,2017/02/21

                    //SqlParameter[] devParams = new SqlParameter[1];
                    //devParams[0] = new SqlParameter("@DevId", param.UsedDevId);
                    //int effectRows = new iCMSDbContext().Database.ExecuteSqlCommand(ConstObject.SQL_TerminateAllAlarmsOverDevice, devParams);
                    ////var effectRows = devAlmRecordRepository.ExecuteSqlCommand(string.Format("exec SP_TerminateAllAlarmsOverDevice {0}", param.UsedDevId));
                    //#endregion

                    //主备机切换后，更改起父级监测树状态
                    UpdateMonitorTreeStatus(new iCMSDbContext(), UsedDev.MonitorTreeID);

                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("002171");
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<bool>("002171");
                return response;
            }
        }

        #endregion

        #region 获取触发式上传，触发值为空的测量定义
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-10-16
        /// 创建内容：获取触发式上传，触发值为空的测量定义
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<TriggerUploadResult> GetTriggerUploadDataByMeasureSites(GetTriggerUploadDataParameter param)
        {
            #region 初始化

            BaseResponse<TriggerUploadResult> response = new BaseResponse<TriggerUploadResult>();
            TriggerUploadResult result = new TriggerUploadResult();

            string reason = string.Empty;
            List<TriggerUploadInfo> triggerUploadInfoList = new List<TriggerUploadInfo>();

            #endregion

            #region 业务处理

            try
            {
                var dbContext = new iCMSDbContext();


                //位置Id
                string[] measureSiteIdArray = param.MSiteIDs.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                List<int> measureSiteIdList = new List<int>();


                foreach (string id in measureSiteIdArray)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        measureSiteIdList.Add(Convert.ToInt32(id));
                    }
                }

                if (measureSiteIdList.Count == 0)
                {
                    response = new BaseResponse<TriggerUploadResult>("002182");
                    return response;
                }
                else
                {
                    var db = new iCMSDbContext();

                    #region 测量定义配置为空
                    //SingalType 去除LQ=5
                    var sqlIsTriggerSetConfig = @"SELECT  TSV.MSiteID
                                            FROM dbo.T_SYS_VIBRATING_SET_SIGNALALM AS TSVSS
                                                INNER JOIN dbo.T_SYS_VIBSINGAL AS TSV
                                                    ON TSV.SingalID = TSVSS.SingalID
                                            WHERE TSV.SingalType != 5 AND TSV.DAQStyle=1
                                            AND TSV.MSiteID IN ({0}) 
                                            GROUP BY TSVSS.SingalID,
                                                     TSV.DAQStyle,
                                                     TSV.MSiteID
                                            HAVING (COUNT(1) - COUNT(TSVSS.UploadTrigger)) < COUNT(1)";

                    //已经设置一个触发式上传阈值并且不为空
                    sqlIsTriggerSetConfig = string.Format(sqlIsTriggerSetConfig, param.MSiteIDs);
                    var isNotNullUploadTriggerMSiteIDList = db.Database.SqlQuery<int>(sqlIsTriggerSetConfig).ToList();
                    #endregion

                    #region 过滤未下发测量定义的数据

                    //计算下发测试失败的测量位置id
                    string sqlIsFail = @"SELECT TSO.MSID
                                            FROM dbo.T_SYS_OPERATION AS TSO
                                                INNER JOIN
                                                (
                                                    SELECT MSID,
                                                           OperationType,
                                                           DAQStyle,
                                                           MAX(AddDate) AddDate
                                                    FROM dbo.T_SYS_OPERATION
                                                    WHERE OperationType = 1
                                                          AND DAQStyle = 1
                                                    GROUP BY MSID,
                                                             OperationType,
                                                             DAQStyle
                                                ) TSO1
                                                    ON TSO.MSID = TSO1.MSID
                                                       AND TSO1.OperationType = TSO.OperationType
                                                       AND TSO1.DAQStyle = TSO.DAQStyle
                                                       AND TSO1.AddDate = TSO.AddDate
                                                WHERE TSO.OperationResult=1 and TSO.MSID IN ({0})";

                    sqlIsFail = string.Format(sqlIsFail, param.MSiteIDs);
                    //下发测量定义成功的
                    var operationMeasureSiteIdList = db.Database.SqlQuery<int>(sqlIsFail).ToList();

                    //获取触发式上传并且下发测量定义成功的，取交集
                    var unionList = isNotNullUploadTriggerMSiteIDList.Intersect(operationMeasureSiteIdList).ToList();

                    //去除两个都成功的数据
                    measureSiteIdList.RemoveAll(item => unionList.Contains(item));

                    #endregion

                    var measureSiteList = db.Measuresite.GetDatas<MeasureSite>(db, item => measureSiteIdList.Contains(item.MSiteID)).ToList();

                    List<MeasureSiteType> measureSiteTypeList = cacheDICT.GetInstance()
                                     .GetCacheType<MeasureSiteType>()
                                     .ToList();
                    foreach (var info in measureSiteList)
                    {
                        TriggerUploadInfo triggerUploadInfo = new TriggerUploadInfo();
                        triggerUploadInfo.MSiteID = info.MSiteID;
                        string name = string.Empty;
                        var type = measureSiteTypeList.Where(item => item.ID == info.MSiteName).FirstOrDefault();
                        if (type != null)
                        {
                            name = type.Name;
                        }
                        triggerUploadInfo.MSiteName = name;
                        triggerUploadInfoList.Add(triggerUploadInfo);

                    }

                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<TriggerUploadResult>("002191");
                return response;
            }

            #endregion

            #region 结果值

            result.TriggerUploadList = triggerUploadInfoList;
            //返回值
            response.Result = result;
            return response;

            #endregion
        }
        #endregion

        #region 获取网关下的传感器
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-10-16
        /// 创建内容：获取网关下的传感器
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetWSDataByWGIDResult> GetWSDataByWGID(GetWSDataByWGIDParameter parameter)
        {
            BaseResponse<GetWSDataByWGIDResult> baseResponse = new BaseResponse<GetWSDataByWGIDResult>();
            GetWSDataByWGIDResult result = new GetWSDataByWGIDResult();
            #region 获取网关下的传感器,网关Id错误
            if (parameter.WGID < -1 || parameter.WGID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008552";
                return baseResponse;
            }
            #endregion

            #region 获取网关下的传感器,网关Id错误
            if (parameter.UserID < -1 && parameter.UserID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008552";
                return baseResponse;
            }
            #endregion

            #region 获取网关下的传感器,使用状态不正确
            if (parameter.IsUsed < -1 && parameter.IsUsed > 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009672";
                return baseResponse;
            }
            #endregion


            try
            {
                var wsList = new List<WS>();
                if (parameter.WGID != -1)
                {
                    if (parameter.IsUsed == -1)
                    {
                        wsList = wsRepository.GetDatas<WS>(item => item.WGID == parameter.WGID, true).ToList();
                    }
                    else
                    {
                        wsList = wsRepository.GetDatas<WS>(item => item.WGID == parameter.WGID && item.UseStatus == parameter.IsUsed, true).ToList();
                    }

                }
                else
                {
                    if (parameter.IsUsed == -1)
                    {
                        wsList = wsRepository.GetDatas<WS>(item => true, true).ToList();
                    }
                    else
                    {
                        wsList = wsRepository.GetDatas<WS>(item => item.UseStatus == parameter.IsUsed, true).ToList();
                    }
                }

                List<SensorType> sensorTypeList = cacheDICT.GetInstance()
                   .GetCacheType<SensorType>()
                   .ToList();
                //过滤用户
                if (parameter.UserID > 0)
                {
                    var wsIDList = userRelationWSRepository.GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, true).Select(item => item.WSID).ToList();
                    if (wsIDList == null || !wsIDList.Any())
                    {
                        //没有数据
                        baseResponse.IsSuccessful = true;
                        return baseResponse;
                    }
                    else
                    {
                        wsList = wsList.Where(item => wsIDList.Contains(item.WSID)).ToList();
                    }
                }



                if (wsList != null && wsList.Any())
                {
                    result.WSInfoList = wsList.ToList().Select(item =>
                    {

                        string sensorTypeName = string.Empty;
                        var sensor = sensorTypeList.Where(info => info.ID == item.SensorType).FirstOrDefault();
                        if (sensor != null)
                        {
                            sensorTypeName = sensor.Name;
                        }
                        return new WSInfo
                        {
                            WSID = item.WSID,
                            WSNO = item.WSNO,
                            WSName = item.WSName,
                            SensorTypeName = sensorTypeName
                        };
                    }).ToList();
                }

                baseResponse.IsSuccessful = true;
                baseResponse.Result = result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008561";

            }

            return baseResponse;
        }
        #endregion

        #endregion

        #region 监测树设置

        #region 获取监测树类型数据
        /// <summary>
        /// 获取监测树类型数据
        /// </summary>
        /// <returns></returns>
        public BaseResponse<MonitorTreeTypeDataResult> QueryMonitorTreeType()
        {
            BaseResponse<MonitorTreeTypeDataResult> backObj = new BaseResponse<MonitorTreeTypeDataResult>();
            try
            {
                MonitorTreeTypeDataResult typeInfo = new MonitorTreeTypeDataResult()
                {
                    TypeInfos = new List<MonitorTreeTypeInfo>()
                };

                cacheDICT.GetInstance().GetCacheType<MonitorTreeType>().ToList()
                    .ForEach(obj => typeInfo.TypeInfos.Add(new MonitorTreeTypeInfo()
                    {
                        ID = obj.ID,
                        Name = obj.Name
                    }));
                backObj.Result = typeInfo;
                backObj.IsSuccessful = true;
                return backObj;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                backObj.Code = "002201";
                backObj.IsSuccessful = false;
                backObj.Result = null;
                return backObj;
            }
        }
        #endregion

        #region 获取监测树数据
        /// <summary>
        /// 获取监测树数据 
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<MonitorTreeResult> GetMonitorTreeDataInfo(MonitorTreeDatasParameter Parameter)
        {
            BaseResponse<MonitorTreeResult> backObj = new BaseResponse<MonitorTreeResult>();

            try
            {
                ListSortDirection direction = new ListSortDirection();
                if (Parameter.Order.ToLower().Equals("desc"))
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                }
                PropertySortCondition sortCondition = new PropertySortCondition(Parameter.Sort, direction);

                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    var mts = dbContext.MonitorTree
                                       .GetDatas<MonitorTree>(dbContext, t => t.PID == Parameter.Pid);
                    int total = 0;
                    if (Parameter.PageSize == -1)
                    {
                        var linq =
                       (from mt in mts.ToList()
                        join mtType in dbContext.MonitorTreeType
                            on mt.Type equals mtType.ID
                            into mtGroup
                        from mtg in mtGroup
                        from mtProperty in dbContext.MonitorTreeProperty.ToList()
                        where mt.MonitorTreePropertyID == mtProperty.MonitorTreePropertyID
                        select new MonitorTreeInfo
                        {
                            MonitorTreeID = mt.MonitorTreeID,
                            PID = mt.PID,
                            OID = mt.OID,
                            IsDefault = mt.IsDefault,
                            Name = mt.Name,
                            Des = mt.Des,
                            Type = mt.Type,
                            TypeName = mtg.Name,
                            ImageID = mt.ImageID ?? -1,
                            Status = mt.Status,
                            AddDate = mt.AddDate,

                            //其他属性

                            Address = mtProperty.Address,
                            URL = mtProperty.URL,
                            TelphoneNO = mtProperty.TelphoneNO,
                            FaxNO = mtProperty.FaxNO,
                            Latitude = mtProperty.Latitude,
                            Longtitude = mtProperty.Longtitude,
                            Length = mtProperty.Length,
                            Width = mtProperty.Width,
                            Area = mtProperty.Area,
                            PersonInCharge = mtProperty.PersonInCharge,
                            PersonInChargeTel = mtProperty.PersonInChargeTel,
                            Remark = mtProperty.Remark,
                            ChildCount =
                                (from tree in dbContext.MonitorTree
                                 where mt.MonitorTreeID == tree.PID
                                 select tree.MonitorTreeID)
                                .Distinct()
                                .Count()
                        }).ToList();
                        total = linq.Count();
                        backObj.Result = new MonitorTreeResult()
                        {
                            MTInfos = linq,
                            Total = total,
                        };
                        backObj.IsSuccessful = true;
                    }
                    else
                    {
                        var linq =
                           (from mt in mts
                            join mtType in dbContext.MonitorTreeType
                                on mt.Type equals mtType.ID
                                into mtGroup
                            from mtg in mtGroup
                            from mtProperty in dbContext.MonitorTreeProperty
                            where mt.MonitorTreePropertyID == mtProperty.MonitorTreePropertyID
                            select new MonitorTreeInfo
                            {
                                MonitorTreeID = mt.MonitorTreeID,
                                PID = mt.PID,
                                OID = mt.OID,
                                IsDefault = mt.IsDefault,
                                Name = mt.Name,
                                Des = mt.Des,
                                Type = mt.Type,
                                TypeName = mtg.Name,
                                ImageID = mt.ImageID ?? -1,
                                Status = mt.Status,
                                AddDate = mt.AddDate,

                                //其他属性

                                Address = mtProperty.Address,
                                URL = mtProperty.URL,
                                TelphoneNO = mtProperty.TelphoneNO,
                                FaxNO = mtProperty.FaxNO,
                                Latitude = mtProperty.Latitude,
                                Longtitude = mtProperty.Longtitude,
                                Length = mtProperty.Length,
                                Width = mtProperty.Width,
                                Area = mtProperty.Area,
                                PersonInCharge = mtProperty.PersonInCharge,
                                PersonInChargeTel = mtProperty.PersonInChargeTel,
                                Remark = mtProperty.Remark,
                                ChildCount =
                                    (from tree in dbContext.MonitorTree
                                     where mt.MonitorTreeID == tree.PID
                                     select tree.MonitorTreeID)
                                    .Distinct()
                                    .Count()
                            });

                        var tempLinq = linq.Where<MonitorTreeInfo>(Parameter.Page, Parameter.PageSize, out total, null).ToList();
                        backObj.Result = new MonitorTreeResult()
                        {
                            MTInfos = tempLinq,
                            Total = total,
                        };
                    }



                    return backObj;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                backObj.IsSuccessful = false;
                backObj.Code = "002211";
                return backObj;
            }
        }
        #endregion

        #region 添加监测树
        /// <summary>
        /// 添加监测树       
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<AddMonitorTreeResult> AddMonitorTree(AddMonitorTreeParameter Parameter)
        {
            BaseResponse<AddMonitorTreeResult> backObj = new BaseResponse<AddMonitorTreeResult>();
            try
            {
                if (Parameter == null)
                {
                    backObj.IsSuccessful = false;
                    backObj.Code = "002222";
                    return backObj;
                }
                #region 数据可用性验证

                float? len = Parameter.Length;
                float? wid = Parameter.Width;
                float? area = Parameter.Area;
                float? Longtitude = Parameter.Longtitude;
                float? Latitude = Parameter.Latitude;
                if (len.HasValue && wid.HasValue)
                {
                    area = len.Value * wid.Value;
                }

                #endregion

                MonitorTreeProperty mtp = new MonitorTreeProperty()
                {
                    AddDate = System.DateTime.Now,
                    Address = Parameter.Address,
                    Area = area,
                    ChildCount = 1,
                    FaxNO = Parameter.FaxNO,
                    Length = len,
                    Longtitude = Longtitude,
                    Latitude = Latitude,

                    PersonInCharge = Parameter.PersonInCharge,
                    PersonInChargeTel = Parameter.PersonInChargeTel,
                    Remark = Parameter.Remark,
                    TelphoneNO = Parameter.TelphoneNO,
                    URL = "",
                    Width = wid
                };

                MonitorTree mt = new MonitorTree()
                {
                    AddDate = System.DateTime.Now,
                    Des = "",
                    ImageID = 0,
                    IsDefault = 1,
                    Name = Parameter.Name,
                    OID = 0,
                    PID = Parameter.PID,
                    Status = 0,
                    Type = Parameter.Type
                };
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    OperationResult result = context.MonitorTreeProperty.AddNew(context, mtp);
                    if (result.ResultType == EnumOperationResultType.Success)
                    {
                        //Added by QXM
                        mt.MonitorTreePropertyID = mtp.MonitorTreePropertyID;
                        result = context.MonitorTree.AddNew<MonitorTree>(context, mt);
                        if (result.ResultType == EnumOperationResultType.Success)
                        {

                            backObj.Result = new AddMonitorTreeResult()
                            {
                                MonitorTreeID = mt.MonitorTreeID
                            };
                            backObj.IsSuccessful = true;

                            return backObj;
                        }
                        else
                        {
                            backObj.IsSuccessful = false;
                            backObj.Code = "002281";
                            return backObj;
                        }
                    }
                    else
                    {
                        backObj.IsSuccessful = false;
                        backObj.Code = "002281";
                        return backObj;
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                backObj.IsSuccessful = false;
                backObj.Code = "002281";
                return backObj;
            }
        }
        #endregion

        #region 编辑监测树
        /// <summary>
        /// 编辑监测树
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditMonitorTree(EditMonitorTreeParameter Parameter)
        {
            BaseResponse<bool> backObj = new BaseResponse<bool>();
            try
            {
                MonitorTree mt = monitorTreeRepository
                    .GetByKey(Parameter.MonitorTreeID);
                MonitorTreeProperty mtp = monitorTreePropertyRepository
                    .GetByKey(mt.MonitorTreePropertyID);
                if (mt == null || mtp == null)
                {
                    backObj.IsSuccessful = false;
                    backObj.Code = "002292";
                    return backObj;
                }

                #region 数据可用性验证

                float? len = Parameter.Length;
                float? wid = Parameter.Width;
                float? area = null;
                float? Longtitude = Parameter.Latitude;
                float? Latitude = Parameter.Longtitude;

                if (len.HasValue && wid.HasValue)
                {
                    area = len.Value * wid.Value;
                }

                #endregion

                mt.Name = Parameter.Name;
                mt.PID = Parameter.PID;

                mt.Type = Parameter.Type;

                mtp.Address = Parameter.Address;
                mtp.Area = area;

                mtp.FaxNO = Parameter.FaxNO;
                mtp.Latitude = Latitude;
                mtp.Length = len;
                mtp.Longtitude = Longtitude;
                mtp.PersonInCharge = Parameter.PersonInCharge;
                mtp.PersonInChargeTel = Parameter.PersonInChargeTel;
                mtp.Remark = Parameter.Remark;
                mtp.TelphoneNO = Parameter.TelphoneNO;
                mtp.URL = "";
                mtp.Width = wid;

                Dictionary<EntityBase, EntityState> dic = new Dictionary<EntityBase, EntityState>();
                dic.Add(mtp, System.Data.Entity.EntityState.Modified);
                dic.Add(mt, System.Data.Entity.EntityState.Modified);
                OperationResult operationResult = monitorTreeRepository.TranMethod(dic);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    backObj.IsSuccessful = true;
                    backObj.Result = true;
                    return backObj;
                }
                else
                {
                    backObj.IsSuccessful = false;
                    backObj.Code = "002351";
                    return backObj;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);

                backObj.IsSuccessful = false;
                backObj.Code = "002351";
                return backObj;
            }
        }
        #endregion

        #region 删除监测树
        /// <summary>
        /// 删除监测树
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteMonitorTree(DelteMonitorTreeParameter Parameter)
        {
            BaseResponse<bool> backObj = new BaseResponse<bool>();
            try
            {
                if (IsDefault(Parameter.MonitorTreeID))
                {
                    backObj.IsSuccessful = false;
                    backObj.Code = "002362";
                    return backObj;
                }

                //是否挂靠设备，查找子节点
                int count = 0;
                List<int> out_MonitorTreeIDList_Result;
                List<int?> out_MonitorTreePropertyIDList_Result;
                IsExistDeviceByMonitorTreeId(Parameter.MonitorTreeID,
                    out count,
                    out out_MonitorTreeIDList_Result,
                    out out_MonitorTreePropertyIDList_Result);

                //获取monitore 为云平台
                MonitorTree mt = monitorTreeRepository
                    .GetByKey(Parameter.MonitorTreeID);
                if (count == 1)
                {
                    backObj.IsSuccessful = false;
                    backObj.Code = "002372";
                    return backObj;
                }


                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                //本节点以及监测树所有的子节点ID
                var sqlParameter = new SqlParameter("@MonitorTreeID", Parameter.MonitorTreeID);
                sqlParameterList.Add(sqlParameter);
                var db = new iCMSDbContext();
                var result = db.Database.ExecuteSqlCommand("EXEC SP_DeleteMonitorTree @MonitorTreeID", sqlParameterList.ToArray());

                if (result > 0)
                {
                    backObj.IsSuccessful = true;
                    backObj.Result = true;
                    return backObj;
                }
                else
                {
                    backObj.IsSuccessful = false;
                    backObj.Code = "002381";
                    return backObj;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                backObj.IsSuccessful = false;
                backObj.Code = "002381";
                return backObj;
            }
        }
        #endregion

        #region 对应监测树类型是否有数据
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-0927
        /// 创建记录：对应监测树类型是否有数据
        /// </summary>
        /// <param name="parameter">请求参数</param>
        /// <returns></returns>
        public BaseResponse<IsExistMonitorTreeDataByTypeResult> IsExistMonitorTreeDataByType(IsExistMonitorTreeDataByTypeParameter parameter)
        {
            BaseResponse<IsExistMonitorTreeDataByTypeResult> baseResponse = new BaseResponse<IsExistMonitorTreeDataByTypeResult>();
            #region 验证数据
            //获取监测树类型是否有数据，测量定义类型不正确
            if (parameter.Type == null || parameter.Type.Count == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008572";
                return baseResponse;
            }
            #endregion

            try
            {
                IsExistMonitorTreeDataByTypeResult result = new IsExistMonitorTreeDataByTypeResult();

                result.IsExist = new List<bool>();
                bool isExist = false;
                //判断是否存在，并且为使用状态的监测树类型
                foreach (var type in parameter.Type)
                {

                    isExist = monitorTreeRepository.GetDatas<MonitorTree>(item => item.Type == type, true).Count() > 0;

                    result.IsExist.Add(isExist);
                }

                baseResponse.Result = result;
                baseResponse.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008581";
                return baseResponse;
            }

            return baseResponse;
        }
        #endregion

        /// <summary>
        /// 根据传入监测树类型，获取系统默认最高级别监测树节点，且返回节点均存在子节点
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetFullMonitorTreeDataByTypeResult> GetFullMonitorTreeDataByType(GetFullMonitorTreeDataByTypeParameter parameter)
        {
            BaseResponse<GetFullMonitorTreeDataByTypeResult> baseResponse = new BaseResponse<GetFullMonitorTreeDataByTypeResult>();
            GetFullMonitorTreeDataByTypeResult result = new GetFullMonitorTreeDataByTypeResult();
            result.MTInfoWithType = new List<MTInfoWithType>();
            baseResponse.Result = result;
            #region 参数验证
            //获取监测树时,定义类型不正确
            if (parameter.Type < -1 || parameter.Type == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008592";
                return baseResponse;
            }
            #endregion

            try
            {
                //监测树类型查找上层
                var db = new iCMSDbContext();

                #region 监测树类型和监测树
                var linq = (from tsmt in db.MonitorTree
                            join tdmt in db.MonitorTreeType on new { Type = tsmt.Type } equals new { Type = tdmt.ID } into tdmt_join
                            from tdmt in tdmt_join.DefaultIfEmpty()
                            where
                              tdmt.IsUsable == 1
                            select new
                            {
                                MonitorTreeID = tsmt.MonitorTreeID,
                                ParentID = tsmt.PID,
                                IsDefault = tsmt.IsDefault,
                                Name = tsmt.Name,
                                Description = tsmt.Des,
                                TypeID = tdmt.ID,
                                TypeName = tdmt.Name,
                                Order = tdmt.Describe,
                            }).ToList();
                #endregion

                #region 监测树类型
                var monitorTreeTypeLinq =
                      (from monitorTreeType in db.MonitorTreeType
                       where monitorTreeType.IsUsable == 1
                       select monitorTreeType).ToList();



                var tempLinq = new List<MonitorTreeType>();

                //设备级别
                if (parameter.Type == -1)
                {
                    tempLinq = monitorTreeTypeLinq.ToList().OrderByDescending(item => Convert.ToInt32(item.Describe)).ToList();
                }
                else
                {
                    tempLinq = monitorTreeTypeLinq.ToList().Where(item => item.ID < parameter.Type)
                                              .OrderByDescending(item => Convert.ToInt32(item.Describe)).ToList();
                }
                #endregion

                //上一次监测树类型
                int lastTypeID = parameter.Type;

                var monitorTreeIDParentIdList = new List<int>();
                //查询上层节点
                foreach (var info in tempLinq)
                {
                    MTInfoWithType mTInfoWithType = new MTInfoWithType();



                    var parnetMonitorTreeList = linq.Where(item => item.TypeID == info.ID).ToList();
                    if (monitorTreeIDParentIdList.Count > 0)
                    {
                        parnetMonitorTreeList = parnetMonitorTreeList.Where(item => monitorTreeIDParentIdList.Contains(item.MonitorTreeID)).ToList();
                    }

                    //获取父节点，下次过滤
                    monitorTreeIDParentIdList = parnetMonitorTreeList.Select(item => item.ParentID).ToList();


                    if (parnetMonitorTreeList != null && parnetMonitorTreeList.Any())
                    {
                        mTInfoWithType.TypeID = parnetMonitorTreeList.FirstOrDefault().TypeID;
                        mTInfoWithType.TypeName = parnetMonitorTreeList.FirstOrDefault().TypeName;
                        mTInfoWithType.Order = Convert.ToInt32(parnetMonitorTreeList.FirstOrDefault().Order);
                        mTInfoWithType.MTInfo = parnetMonitorTreeList.Select(item => new MTInfo
                        {
                            MonitorTreeID = item.MonitorTreeID,
                            ParentID = item.ParentID,
                            IsDefault = item.IsDefault,
                            Name = item.Name,
                            Description = item.Description,
                            TypeID = Convert.ToInt32(item.TypeID),
                            TypeName = item.Name
                        }).ToList();
                        result.MTInfoWithType.Add(mTInfoWithType);
                    }

                    lastTypeID = info.ID;
                }

                #region 添加设备时，如果监测树没有添加全部，则处理为空
                result.MTInfoWithType = result.MTInfoWithType.OrderBy(item => item.Order).ToList();
                if (parameter.Type == -1)
                {
                    if (result.MTInfoWithType == null || result.MTInfoWithType.Count < monitorTreeTypeLinq.Count)
                    {
                        result.MTInfoWithType = new List<MTInfoWithType>();
                    }
                }
                #endregion

                baseResponse.IsSuccessful = true;
                baseResponse.Result = result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008601";
            }

            return baseResponse;

        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取所有的子节点
        /// </summary>
        /// <param name="monitorTreeID"></param>
        /// <param name="allChildNodes"></param>
        private void GetAllChildren(int monitorTreeID, List<int> allChildNodes)
        {
            allChildNodes.Add(monitorTreeID);
            var childNodes = monitorTreeRepository
                .GetDatas<MonitorTree>(m => m.PID == monitorTreeID, false)
                .ToList();

            foreach (var mt in childNodes)
            {
                //找到包含当前节点及所有子节点
                GetAllChildren(mt.MonitorTreeID, allChildNodes);
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-09-19
        /// 创建记录：验证监测树是否关联设备
        /// </summary>
        /// <param name="monitorTreeID">监测树ID</param>
        /// <param name="count">1为关联了设备，否则没有关联设备</param>
        /// <param name="ref_MonitorTreeIDList_Result">要删除的监测树ID</param>
        /// <param name="ref_MonitorTreePropertyIDList_Result">要删除的监测树属性ID</param>
        private void IsExistDeviceByMonitorTreeId(int monitorTreeID,
            out int count,
            out List<int> out_MonitorTreeIDList_Result,
            out List<int?> out_MonitorTreePropertyIDList_Result)
        {
            using (var DBContext = new iCMSDbContext())
            {
                int temp_Count = 0;
                List<int> monitorTreeIDList_Result = new List<int>();
                List<int?> monitorTreePropertyIDList_Result = new List<int?>();
                //找出当前传入的监测树和它的子级节点是否挂靠设备
                Action temp_IsExistDeviceByMonitorTreeId = () =>
                {
                    //当前传入的监测树下是否挂靠设备
                    Action<int[]> temp_IsExistDevice = (monitorTreeIDList) =>
                    {
                        if (monitorTreeIDList.Any())
                        {
                            var deviceList =
                                from dev in DBContext.Device
                                join mTreeID in monitorTreeIDList
                                    on dev.MonitorTreeID equals mTreeID
                                select dev.DevID;
                            if (deviceList.Any())
                            {
                                temp_Count = 1;
                                return;
                            }
                        }
                    };

                    var temp_monitorTreeIDList = new int[] { monitorTreeID };

                    while (true)
                    {
                        if (temp_monitorTreeIDList.Any())
                        {
                            temp_IsExistDevice(temp_monitorTreeIDList);

                            if (temp_Count == 1)
                            {
                                break;
                            }
                            //找出当前传入的监测树子级节点
                            var mTreeIDAndmTreeProperIDList =
                                (from mTree in DBContext.MonitorTree
                                 join mTreeID in temp_monitorTreeIDList
                                    on mTree.PID equals mTreeID
                                 select new
                                 {
                                     mTree.MonitorTreeID,
                                     mTree.MonitorTreePropertyID
                                 })
                                .ToArray();
                            //将子级节点的监测树ID取出
                            temp_monitorTreeIDList = mTreeIDAndmTreeProperIDList
                                .Select(p => p.MonitorTreeID)
                                .ToArray();
                            monitorTreeIDList_Result.AddRange(temp_monitorTreeIDList);
                            //将子级节点的监测树属性ID取出
                            monitorTreePropertyIDList_Result.AddRange(mTreeIDAndmTreeProperIDList
                                .Select(p => p.MonitorTreePropertyID));
                        }
                        else
                        {
                            break;
                        }
                    }
                };
                temp_IsExistDeviceByMonitorTreeId();
                count = temp_Count;
                out_MonitorTreeIDList_Result = new List<int>();
                out_MonitorTreePropertyIDList_Result = new List<int?>();
                //如果没有找到挂靠的设备，则将当前传入的监测树ID和监测树属性ID加入
                if (count != 1)
                {
                    int? mTreePropertyID =
                        (from p in DBContext.MonitorTree
                         where p.MonitorTreeID == monitorTreeID
                         select p.MonitorTreePropertyID)
                        .SingleOrDefault();
                    out_MonitorTreeIDList_Result.Add(monitorTreeID);
                    out_MonitorTreePropertyIDList_Result.Add(mTreePropertyID);

                    out_MonitorTreeIDList_Result.AddRange(monitorTreeIDList_Result);
                    out_MonitorTreePropertyIDList_Result.AddRange(monitorTreePropertyIDList_Result);
                }
            }
        }

        /// <summary>
        /// 判断是否是系统默认，如果是系统默认，则禁止删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsDefault(int id)
        {
            var type = monitorTreeRepository
                .GetByKey(id);
            if (type.IsDefault == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetTreeNode(int id)
        {
            MonitorTree monitorTree = monitorTreeRepository
                .GetByKey(id);
            if (monitorTree.PID != 0)
            {
                nodeID.Push(monitorTree.Name + "|" + monitorTree.MonitorTreeID);
                GetTreeNode(monitorTree.PID);
            }
            else
            {
                nodeID.Push(monitorTree.Name + "|" + monitorTree.MonitorTreeID);
            }
            while (nodeID.Count > 0)
            {
                var obj = nodeID.Pop();
                if (treeNodeStr.Trim() == "")
                {
                    treeNodeStr += obj.ToString().Split('|')[0];
                    treeNodeIDStr += obj.ToString().Split('|')[1];
                }
                else
                {
                    treeNodeStr += "#" + obj.ToString().Split('|')[0];
                    treeNodeIDStr += "#" + obj.ToString().Split('|')[1];
                }
            }

            return new List<string>() { treeNodeStr, treeNodeIDStr };
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：根据设备ID获取该设备下的所有测点信息，包括：测点、WS、操作信息
        /// 修改人：王颖辉
        /// 修改时间：2017-10-08
        /// 修改记录：修改性能
        /// </summary>
        /// <param name="devID">设备id</param>
        /// <param name="operationType">操作类型;1=下发测量定义；2=升级;3:触发式上传</param>
        /// <param name="daqStyle">1：定时采集；2：临时采集;</param>
        /// <returns></returns>
        private List<MeasureInfo> GetMeasureSiteInfo(List<int> devIDList, int daqStyle = 1)
        {
            try
            {
                string devIDs = "";
                foreach (var info in devIDList)
                {
                    devIDs += info + ",";
                }
                devIDs = devIDs.TrimEnd(',');
                var sql = string.Empty;

                //获取定时
                if (daqStyle == 1)
                {
                    sql = "select * from View_GetTimingMeasureSiteInfo where DevID in (" + devIDs + ")";
                }
                else
                {
                    sql = "select * from View_GetTimporayMeasureSiteInfo where DevID in (" + devIDs + ")";
                }
                var measureSiteList = new iCMSDbContext().Database.SqlQuery<MeasureDBInfo>(sql).ToList();
                sql = "select * from View_GetOperationList where DevID in ("
                    + devIDs + ") and DAQStyle='" + daqStyle + "' order by EDate desc";
                var operationList = new iCMSDbContext().Database.SqlQuery<OperationInfo>(sql).ToList();

                List<MeasureInfo> msInfoList = new List<MeasureInfo>();

                var measureSiteArray = measureSiteRepository.GetDatas<MeasureSite>(t => true, true).ToList();
                var speedSamplingMDFArray = speedSamplingMDFRepository.GetDatas<SpeedSamplingMDF>(t => true, true).ToList();
                var wsList = wsRepository.GetDatas<WS>(t => true, true).ToList();
                //判断有数据时
                if (measureSiteList != null && measureSiteList.Any())
                {
                    var measureSiteIDList = measureSiteList.Select(item => item.WID);

                    //获取轴承信息和轴承厂商信息

                    var measureSiteBearingList = measureSiteBearingRepository
                        .GetDatas<MeasureSiteBearing>(item => measureSiteIDList.Contains(item.MeasureSiteID), true)
                        .ToList();
                    var bearingIDList = measureSiteBearingList.Select(item => item.BearingID);
                    var bearingList = new List<Bearing>();
                    var factoryList = new List<Factory>();
                    if (bearingIDList != null && bearingIDList.Any())
                    {
                        bearingList = bearingRepository
                            .GetDatas<Bearing>(item => bearingIDList.Contains(item.BearingID), true)
                            .ToList();
                        var factoryIDList = bearingList.Select(item => item.FactoryID).ToList();
                        if (bearingList != null && bearingList.Any())
                        {
                            factoryList = factoryRepository
                                .GetDatas<Factory>(item => factoryIDList.Contains(item.FactoryID), true)
                                .ToList();
                        }
                    }

                    foreach (var info in measureSiteList)
                    {
                        MeasureInfo measureInfo = new MeasureInfo();
                        measureInfo.WID = info.WID;
                        measureInfo.MSiteTypeId = info.MSiteTypeId;
                        measureInfo.MStieTypeName = info.MStieTypeName;
                        measureInfo.DevID = info.DevID;
                        measureInfo.DevFormType = info.DevFormType;
                        measureInfo.WSID = info.WSID.Value;
                        measureInfo.WSName = info.WSName;
                        measureInfo.LinkStatus = info.LinkStatus.Value;
                        measureInfo.MeasureSiteType = info.MeasureSiteType;
                        measureInfo.SensorCosA = info.SensorCosA;
                        measureInfo.SensorCosB = info.SensorCosB;
                        measureInfo.MSiteStatus = info.MSiteStatus;
                        measureInfo.MSiteSDate = info.MSiteSDate.ToString();
                        measureInfo.WaveTime = info.WaveTime;
                        measureInfo.FlagTime = info.FlagTime;
                        measureInfo.TemperatureTime = info.TemperatureTime;
                        measureInfo.Remark = info.Remark;
                        measureInfo.Position = info.Position;

                        measureInfo.AddDate = info.AddDate.ToString();

                        measureInfo.Type = info.Type;
                        measureInfo.TypeName = info.TypeName;
                        measureInfo.ChildrenCount = info.ChildrenCount;
                        measureInfo.WGName = info.WGName;
                        measureInfo.TriggerStatus = info.TriggerStatus;

                        #region 添加传感器采集类型,  Added by QXM, 2018/05/14
                        /*1：振动传感器，2：转速传感器，3：油液传感器，4：过程量传感器 */
                        var tempWS = wsList.Where(wds => wds.WSID == measureInfo.WSID).FirstOrDefault();
                        if (tempWS != null)
                        {
                            measureInfo.SensorCollectType = tempWS.SensorCollectType;
                        }

                        ////找到测点信息
                        //var tempeMeasureSite = measureSiteArray.Where(t => t.MSiteID == measureInfo.WID).FirstOrDefault();
                        //if (tempeMeasureSite != null && tempeMeasureSite.RelationMSiteID.HasValue)
                        //{
                        //    //转速测点
                        //    measureInfo.SensorCollectType = 2;
                        //}
                        //else
                        //{
                        //    //振动测点
                        //    measureInfo.SensorCollectType = 1;
                        //}
                        #endregion

                        #region 转速测点相关
                        var curMSite = measureSiteArray.Where(t => t.MSiteID == measureInfo.WID).FirstOrDefault();
                        if (curMSite != null)
                        {
                            int? relatedMSiteID = curMSite.RelationMSiteID;
                            if (relatedMSiteID.HasValue && relatedMSiteID.Value > 0)//具有关联测点，说明是转速测点
                            {
                                measureInfo.RelationMSiteID = relatedMSiteID;
                                var relatedMSite = measureSiteArray.Where(t => t.MSiteID == relatedMSiteID.Value).FirstOrDefault();
                                measureInfo.RelationMSName = cacheDICT.GetCacheType<MeasureSiteType>().Where(t => t.ID == relatedMSite.MSiteName).First().Name;

                                var speedMDF = speedSamplingMDFArray.Where(t => t.MSiteID == curMSite.MSiteID).FirstOrDefault();
                                if (speedMDF != null)
                                {
                                    measureInfo.PulsNumPerP = speedMDF.PulsNumPerP;
                                }
                            }

                        }
                        #endregion

                        //下发测量定义
                        var operation = operationList
                            .Where(item => item.MSID == info.WID
                                && item.OperationType == (int)EnumOperationType.Config)
                            .OrderByDescending(item => item.EDate)
                            .FirstOrDefault();
                        if (operation == null)
                        {
                            measureInfo.OperationStatus = 0;
                            measureInfo.ConfigMSDate = string.Empty;
                        }
                        else
                        {
                            if (operation.OperationResult == null)
                            {
                                measureInfo.OperationStatus = 0;
                                measureInfo.ConfigMSDate = string.Empty;
                            }
                            else
                            {
                                measureInfo.OperationStatus = Convert.ToInt16(operation.OperationResult);
                                measureInfo.ConfigMSDate = operation.EDate.ToString();
                            }
                        }

                        //触发式上传
                        operation = operationList
                            .Where(item => item.MSID == info.WID
                                && item.OperationType == (int)EnumOperationType.Trigger)
                            .OrderByDescending(item => item.EDate)
                            .FirstOrDefault();
                        if (operation == null)
                        {
                            measureInfo.TriggerOperationStatus = 0;
                            measureInfo.TriggerConfigMSDate = string.Empty;
                        }
                        else
                        {
                            if (operation.OperationResult == null)
                            {
                                measureInfo.TriggerOperationStatus = 0;
                                measureInfo.TriggerConfigMSDate = string.Empty;
                            }
                            else
                            {
                                measureInfo.TriggerOperationStatus = Convert.ToInt16(operation.OperationResult);
                                measureInfo.TriggerConfigMSDate = operation.EDate.ToString();
                            }
                        }

                        //触发式上传
                        operation = operationList
                            .Where(item => item.MSID == info.WID
                                && item.OperationType == (int)EnumOperationType.Updategrade)
                            .OrderByDescending(item => item.EDate)
                            .FirstOrDefault();
                        if (operation == null)
                        {
                            measureInfo.UpdateOperationStatus = 0;
                            measureInfo.UpdateConfigMSDate = string.Empty;
                        }
                        else
                        {
                            if (operation.OperationResult == null)
                            {
                                measureInfo.UpdateOperationStatus = 0;
                                measureInfo.UpdateConfigMSDate = string.Empty;
                            }
                            else
                            {
                                measureInfo.UpdateOperationStatus = Convert.ToInt16(operation.OperationResult);
                                measureInfo.UpdateConfigMSDate = operation.EDate.ToString();
                            }
                        }
                        msInfoList.Add(measureInfo);

                        #region 添加多轴承信息
                        measureInfo.BearingInfoList = new List<BearingInfoForResult>();

                        var queryMeasureSiteBearingList = measureSiteBearingList.Where(item => item.MeasureSiteID == info.WID).ToList();
                        if (queryMeasureSiteBearingList != null && queryMeasureSiteBearingList.Any())
                        {
                            measureInfo.BearingInfoList = queryMeasureSiteBearingList.Select(item =>
                            {
                                var factoryName = string.Empty;
                                var factoryID = string.Empty;
                                var bearingNum = string.Empty;
                                var bearingInfo = bearingList.Where(bearingbearing => bearingbearing.BearingID == item.BearingID).FirstOrDefault();
                                if (bearingInfo != null)
                                {
                                    bearingNum = bearingInfo.BearingNum;
                                    var factoryInfo = factoryList.Where(factory => factory.FactoryID == bearingInfo.FactoryID).FirstOrDefault();
                                    if (factoryInfo != null)
                                    {
                                        factoryID = factoryInfo.FactoryID;
                                        factoryName = factoryInfo.FactoryName;
                                    }
                                }

                                return new BearingInfoForResult
                                {
                                    BearingID = item.BearingID,
                                    FactoryID = factoryID,
                                    BearingType = item.BearingType,
                                    LubricatingForm = item.LubricatingForm,
                                    BearingNum = bearingNum,
                                    FactoryName = factoryName,
                                };
                            })
                            .ToList();
                        }

                        #endregion
                    }
                }
                return msInfoList;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                return null;
            }
        }

        private void FillMeasreSite(MeasureSite newMeasureSite, MeasureSite srcMeasureSite)
        {
            newMeasureSite.MeasureSiteType = srcMeasureSite.MeasureSiteType;
            newMeasureSite.SensorCosA = srcMeasureSite.SensorCosA;
            newMeasureSite.SensorCosB = srcMeasureSite.SensorCosB;
            newMeasureSite.MSiteStatus = 0;
            newMeasureSite.MSiteSDate = srcMeasureSite.MSiteSDate;
            newMeasureSite.WaveTime = srcMeasureSite.WaveTime;
            newMeasureSite.FlagTime = srcMeasureSite.FlagTime;
            newMeasureSite.TemperatureTime = srcMeasureSite.TemperatureTime;
            newMeasureSite.Remark = srcMeasureSite.Remark;
            newMeasureSite.Position = srcMeasureSite.Position;
            newMeasureSite.SerialNo = srcMeasureSite.SerialNo;
            newMeasureSite.BearingID = srcMeasureSite.BearingID;
            newMeasureSite.BearingType = srcMeasureSite.BearingType;
            newMeasureSite.BearingModel = srcMeasureSite.BearingModel;
            newMeasureSite.LubricatingForm = srcMeasureSite.LubricatingForm;
        }

        private void FillVibSignal(VibSingal newVibSignal, VibSingal vibSignal)
        {
            newVibSignal.DAQStyle = vibSignal.DAQStyle;
            newVibSignal.UpLimitFrequency = vibSignal.UpLimitFrequency;
            newVibSignal.LowLimitFrequency = vibSignal.LowLimitFrequency;
            newVibSignal.StorageTrighters = vibSignal.StorageTrighters;
            newVibSignal.EnlvpBandW = vibSignal.EnlvpBandW;
            newVibSignal.EnlvpFilter = vibSignal.EnlvpFilter;
            newVibSignal.SingalType = vibSignal.SingalType;
            newVibSignal.SingalStatus = 0;
            newVibSignal.SingalSDate = DateTime.Now;
            newVibSignal.WaveDataLength = vibSignal.WaveDataLength;
            newVibSignal.Remark = vibSignal.Remark;
        }

        private void FillAlmSet(SignalAlmSet newAlmSet, SignalAlmSet almSet)
        {
            newAlmSet.ValueType = almSet.ValueType;
            newAlmSet.WarnValue = almSet.WarnValue;
            newAlmSet.AlmValue = almSet.AlmValue;
            newAlmSet.UploadTrigger = almSet.UploadTrigger;
            newAlmSet.ThrendAlarmPrvalue = almSet.ThrendAlarmPrvalue;
            newAlmSet.Status = 0;
        }

        #endregion

        #region 删除监测节点时候同步更新父级节点的状态
        //Added by QXM, 2017/01/06
        /// <summary>
        /// 更新测点的报警状态
        /// </summary>
        /// <param name="signalID"></param>
        private void UpdateSignalStatus(iCMSDbContext iCMSDBContext, int signalID)
        {
            try
            {
                int status = 0;
                var almSets = iCMSDBContext.SignalAlmSet.Where(t => t.SingalID == signalID).ToList();
                foreach (var item in almSets)
                {
                    //遍历找到振动信号下特征值报警最大值
                    if (item.Status > status)
                    {
                        status = item.Status;
                    }
                }

                var vibSignal = iCMSDBContext.VibSingal.Where(t => t.SingalID == signalID).FirstOrDefault();

                if (vibSignal != null)
                {
                    vibSignal.SingalStatus = status;
                    iCMSDBContext.VibSingal.Update(iCMSDBContext, vibSignal);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        private void UpdateMSiteStatus(iCMSDbContext iCMSDBContext, int msiteID)
        {
            try
            {
                int status = 0;
                //获取测点下设备温度报警状态
                var deviceTempe = iCMSDBContext.TempeDeviceSetMSiteAlm.Where
                    (t => t.MsiteID == msiteID).FirstOrDefault();
                if (deviceTempe != null)
                {
                    if (deviceTempe.Status > status)
                    {
                        status = deviceTempe.Status;
                    }
                }

                var vibSignals = iCMSDBContext.VibSingal.Where(t => t.MSiteID == msiteID).ToList();
                foreach (var item in vibSignals)
                {
                    if (item.SingalStatus > status)
                    {
                        status = item.SingalStatus;
                    }
                }

                var msite = iCMSDBContext.Measuresite.Where(t => t.MSiteID == msiteID).FirstOrDefault();
                if (msite != null)
                {
                    msite.MSiteStatus = status;
                    iCMSDBContext.Measuresite.Update(iCMSDBContext, msite);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        private void UpdateDeviceStatus(iCMSDbContext iCMSDBContext, int devID)
        {
            try
            {
                int status = 0;
                var msites = iCMSDBContext.Measuresite.Where(t => t.DevID == devID).ToList();
                foreach (var item in msites)
                {
                    if (item.MSiteStatus > status)
                    {
                        status = item.MSiteStatus;
                    }
                }

                var device = iCMSDBContext.Device.Where(t => t.DevID == devID).FirstOrDefault();
                if (device != null)
                {
                    device.AlmStatus = status;
                    iCMSDBContext.Device.Update(iCMSDBContext, device);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        private void UpdateMonitorTreeStatus(iCMSDbContext iCMSDBContext, int mtID)
        {
            try
            {
                int status = 0;
                List<int> allChildNodes = new List<int>();
                GetAllChildren(iCMSDBContext, mtID, allChildNodes);

                var devices = iCMSDBContext.Device.Where(t => allChildNodes.Contains(t.MonitorTreeID)).ToList();
                foreach (var item in devices)
                {
                    if (item.AlmStatus > status)
                    {
                        status = item.AlmStatus;
                    }
                }

                var mt = iCMSDBContext.MonitorTree.Where(t => t.MonitorTreeID == mtID).FirstOrDefault();
                if (mt != null)
                {
                    mt.Status = status;
                    iCMSDBContext.MonitorTree.Update(iCMSDBContext, mt);

                    //递归
                    UpdateMonitorTreeStatus(iCMSDBContext, mt.PID);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        private void ModifyDeviceStatus(iCMSDbContext iCMSDBContext, int devID)
        {
            try
            {
                UpdateDeviceStatus(iCMSDBContext, devID);
                var device = iCMSDBContext.Device.Where(t => t.DevID == devID).FirstOrDefault();
                if (device != null)
                {
                    UpdateMonitorTreeStatus(iCMSDBContext, device.MonitorTreeID);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        private void ModifyStatusBeyondMSite(iCMSDbContext iCMSDBContext, int msiteID)
        {
            try
            {
                UpdateMSiteStatus(iCMSDBContext, msiteID);
                var msite = iCMSDBContext.Measuresite.Where(t => t.MSiteID == msiteID).FirstOrDefault();
                if (msite != null)
                {
                    UpdateDeviceStatus(iCMSDBContext, msite.DevID);

                    var device = iCMSDBContext.Device.Where(t => t.DevID == msite.DevID).FirstOrDefault();
                    if (device != null)
                    {
                        UpdateMonitorTreeStatus(iCMSDBContext, device.MonitorTreeID);
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        private void ModifyStatusBeyondSignal(iCMSDbContext iCMSDBContext, int signalID)
        {
            try
            {
                UpdateSignalStatus(iCMSDBContext, signalID);
                var signal = iCMSDBContext.VibSingal.Where(t => t.SingalID == signalID).FirstOrDefault();
                if (signal != null)
                {
                    int MSiteID = signal.MSiteID;
                    ModifyStatusBeyondMSite(iCMSDBContext, MSiteID);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        private void GetAllChildren(iCMSDbContext context, int monitorTreeID, List<int> allChildNodes)
        {
            allChildNodes.Add(monitorTreeID);
            var childNodes = context.MonitorTree
                .Where(m => m.PID == monitorTreeID)
                .ToList();

            foreach (var mt in childNodes)
            {
                //找到包含当前节点及所有子节点
                GetAllChildren(context, mt.MonitorTreeID, allChildNodes);
            }
        }

        /// <summary>
        /// 重新计算WS的报警状态
        /// ADDED BY QXM
        /// </summary>
        /// <param name="wsID"></param>
        private void ModifyWSStatus(int wsID)
        {
            int status = (int)EnumAlarmStatus.Normal;
            //1.找到WS对应的MS
            var measureSite = measureSiteRepository.GetDatas<MeasureSite>(t => t.WSID == wsID, true).FirstOrDefault();
            if (null == measureSite)
            {
                return;
            }

            //2.找到MS对应的传感器电池电压阈值，以及传感器温度阈值
            //3.根据2.步找到的阈值来计算传感器的报警状态
            var volateAlmset = voltageSetMSiteAlmRepository.GetDatas<VoltageSetMSiteAlm>(t => t.MsiteID == measureSite.MSiteID, true).FirstOrDefault();
            if (null != volateAlmset && volateAlmset.Status > status)
            {
                status = volateAlmset.Status;
            }
            var wsTempeAlmset = tempeWSSetMSiteAlmRepository.GetDatas<TempeWSSetMSiteAlm>(t => t.MsiteID == measureSite.MSiteID, true).FirstOrDefault();
            if (null != wsTempeAlmset && wsTempeAlmset.Status > status)
            {
                status = wsTempeAlmset.Status;
            }

            var wsInDb = wsRepository.GetByKey(wsID);
            if (null != wsInDb)
            {
                wsInDb.AlmStatus = status;
                wsRepository.Update<WS>(wsInDb);
            }

        }
        #endregion

        #region 获取测量位置详细信息
        /// <summary>
        /// 获取测量位置详细信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetMeasureSiteDetailInfoResult> GetMeasureSiteDetailInfo(GetMeasureSiteDetailInfoParameter parameter)
        {
            BaseResponse<GetMeasureSiteDetailInfoResult> baseResponse = new BaseResponse<GetMeasureSiteDetailInfoResult>();
            GetMeasureSiteDetailInfoResult result = new GetMeasureSiteDetailInfoResult();
            baseResponse.Result = result;
            baseResponse.IsSuccessful = true;
            #region 验证数据
            //获取测量位置详细信息,测量位置Id不能小于1
            if (parameter.MeasureSiteID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008612";
                return baseResponse;
            }

            //测量定义类型不能小于1或者大于2
            if (parameter.DAQStyle < 1 || parameter.DAQStyle > 2)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008622";
                return baseResponse;
            }
            #endregion

            try
            {
                var db = new iCMSDbContext();
                var sql = "select * from View_GetMeasureSiteInfo where MeasureSiteID=" + parameter.MeasureSiteID + "";
                result = db.Database.SqlQuery<GetMeasureSiteDetailInfoResult>(sql).ToList().FirstOrDefault();

                //SELECT TSV.SingalID ,
                //        TSV.SingalType VibrationSignalTypeID,
                //        TDVST.Name VibrationSignalName,
                //        TSV.UpLimitFrequency UpperLimitID,
                //        TSV.LowLimitFrequency LowLimitID,
                //        TSV.WaveDataLength WaveLengthID
                //FROM dbo.T_SYS_VIBSINGAL AS TSV
                //     LEFT JOIN dbo.T_DICT_VIBRATING_SIGNAL_TYPE AS TDVST ON TDVST.ID = TSV.SingalType;

                #region 获取轴承信息
                result.BearingInfoList =
                    (from tsb in db.Bearing
                     join tsf in db.Factories on tsb.FactoryID equals tsf.FactoryID into tsf_join
                     from tsf in tsf_join.DefaultIfEmpty()
                     join tsmb in db.MeasureSiteBearing on tsb.BearingID equals tsmb.BearingID into tsmb_join
                     from tsmb in tsmb_join.DefaultIfEmpty()
                     where tsmb.MeasureSiteID == parameter.MeasureSiteID
                     select new BearingInfoResult
                     {
                         BearingID = tsb.BearingID,
                         BearingNum = tsb.BearingNum,
                         FactoryID = tsf.FactoryID,
                         FactoryName = tsf.FactoryName,
                         BearingType = tsmb.BearingType,
                         LubricatingForm = tsmb.LubricatingForm
                     })
                    .ToList();
                #endregion
                #region 设备和测点
                //设备和测点
                if (result != null)
                {
                    sql = "select * from View_GetVibSignalAndEigenValue where MSiteID="
                        + parameter.MeasureSiteID + " and DAQStyle= " + parameter.DAQStyle + " ";
                    var linqVibSignal = db.Database.SqlQuery<VibAndEigenInfo>(sql).ToList();

                    #region 添加振动
                    //添加振动
                    if (linqVibSignal != null && linqVibSignal.Any())
                    {
                        result.VibAndEigenInfo = new List<VibAndEigenInfo>();

                        result.VibAndEigenInfo.AddRange(linqVibSignal);

                        var signalIdList = linqVibSignal.Select(item => item.SingalID);
                        //添加特征值
                        //SELECT TSVSS.SingalAlmID ,
                        //        TSVSS.SingalID ,
                        //        TSVSS.ValueType EigenValueTypeID,
                        //        TDEVT.Name EigenValueName,
                        //        TSVSS.UploadTrigger ,
                        //        TSVSS.WarnValue AlarmValue,
                        //        TSVSS.AlmValue DangerValue,
                        //        TSVSS.ThrendAlarmPrvalue ThrendAlarmPrvalue
                        //FROM dbo.T_SYS_VIBRATING_SET_SIGNALALM AS TSVSS
                        //     LEFT JOIN dbo.T_DICT_EIGEN_VALUE_TYPE AS TDEVT ON TSVSS.ValueType = TDEVT.ID
                        var linqEigenValue =
                            (from tsvss in db.SignalAlmSet
                             join tdevt in db.EigenValueType
                             on new { ValueType = tsvss.ValueType }
                                equals new { ValueType = tdevt.ID } into tdevt_join
                             from tdevt in tdevt_join.DefaultIfEmpty()
                             where signalIdList.Contains(tsvss.SingalID)
                             select new EigenValueInfo
                             {
                                 SingalAlmID = tsvss.SingalAlmID,
                                 SingalID = tsvss.SingalID,
                                 EigenValueTypeID = tsvss.ValueType,
                                 EigenValueName = tdevt.Name,
                                 UploadTrigger = tsvss.UploadTrigger,
                                 AlarmValue = tsvss.WarnValue,
                                 DangerValue = tsvss.AlmValue,
                                 AddDate = tsvss.AddDate,
                                 ThrendAlarmPrvalue = tsvss.ThrendAlarmPrvalue
                             })
                            .ToList();

                        #region 添加特征值
                        foreach (var vibsignal in linqVibSignal)
                        {
                            var eigenList = linqEigenValue.Where(item => item.SingalID == vibsignal.SingalID);
                            if (eigenList != null && eigenList.Any())
                            {
                                vibsignal.EigenValueInfo = new List<EigenValueInfo>();
                                vibsignal.EigenValueInfo.AddRange(eigenList);
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
                baseResponse.IsSuccessful = true;
                baseResponse.Result = result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008631";
            }
            return baseResponse;
        }
        #endregion

        #region 编辑临时测量定义
        /// <summary>
        ///创建人:王颖辉
        ///创建时间:2017-10-10
        /// 创建内容：编辑临时测量定义
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<ResponseResult> EditTemporaryVib(EditTemporaryVibParameter parameter)
        {
            BaseResponse<ResponseResult> baseResponse = new BaseResponse<ResponseResult>();
            ResponseResult result = new ResponseResult();
            baseResponse.Result = result;
            baseResponse.IsSuccessful = true;
            #region 验证数据
            //编辑临时测量定义,测量位置ID不正确
            if (parameter.MeasureSiteID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008642";
                return baseResponse;
            }

            //振动数据不能为空
            if (parameter.VibAndEigenInfo == null && parameter.VibAndEigenInfo.Any())
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008652";
                return baseResponse;
            }

            // 验证特征值是否为空
            foreach (var info in parameter.VibAndEigenInfo)
            {
                if (info.EigenValueInfo == null && info.EigenValueInfo.Any())
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "008662";
                    return baseResponse;
                }
            }
            #endregion

            try
            {


                return ExecuteDB.ExecuteTrans((context) =>
                {
                    //删除不存在振动的
                    var signalIdList = parameter.VibAndEigenInfo.Select(item => item.SingalID).ToList();
                    context.VibSingal.Delete(context, item => !signalIdList.Contains(item.SingalID) && item.MSiteID == parameter.MeasureSiteID && item.DAQStyle == 2);

                    foreach (var vibSignal in parameter.VibAndEigenInfo)
                    {
                        //编辑
                        if (vibSignal.SingalID.HasValue)
                        {
                            var vibSignalInfo = context.VibSingal.GetByKey(context, vibSignal.SingalID.Value);
                            vibSignalInfo.UpLimitFrequency = vibSignal.UpperLimitID;
                            vibSignalInfo.LowLimitFrequency = vibSignal.LowLimitID;
                            vibSignalInfo.WaveDataLength = vibSignal.WaveLengthID;
                            vibSignalInfo.EigenValueWaveLength = vibSignal.EigenWaveLengthID;
                            context.VibSingal.Update(context, vibSignalInfo);

                            //删除不存在的特征值
                            var eigenValueIdList = vibSignal.EigenValueInfo.Select(item => item.SingalAlmID);
                            context.SignalAlmSet.Delete(context, item => !eigenValueIdList.Contains(item.SingalAlmID) && item.SingalID == vibSignal.SingalID);

                            foreach (var eigenValue in vibSignal.EigenValueInfo)
                            {
                                //编辑
                                if (eigenValue.SingalAlmID.HasValue)
                                {
                                    var eigenValueInfo = context.SignalAlmSet.GetByKey(context, eigenValue.SingalAlmID.Value);
                                    eigenValueInfo.ValueType = eigenValue.EigenValueTypeID;
                                    context.SignalAlmSet.Update(context, eigenValueInfo);
                                }
                                else
                                {
                                    //添加
                                    var eigenValueInfo = new SignalAlmSet();
                                    eigenValueInfo.ValueType = eigenValue.EigenValueTypeID;
                                    eigenValueInfo.SingalID = vibSignalInfo.SingalID;
                                    eigenValueInfo.AddDate = DateTime.Now;
                                    context.SignalAlmSet.AddNew(context, eigenValueInfo);
                                }
                            }
                        }
                        else
                        {
                            //添加
                            var vibSignalInfo = new VibSingal();
                            vibSignalInfo.UpLimitFrequency = vibSignal.UpperLimitID;
                            vibSignalInfo.LowLimitFrequency = vibSignal.LowLimitID;
                            vibSignalInfo.WaveDataLength = vibSignal.WaveLengthID;
                            vibSignalInfo.EigenValueWaveLength = vibSignal.EigenWaveLengthID;
                            vibSignalInfo.MSiteID = parameter.MeasureSiteID;
                            vibSignalInfo.SingalSDate = DateTime.Now;
                            vibSignalInfo.AddDate = DateTime.Now;
                            vibSignalInfo.DAQStyle = 2;
                            vibSignalInfo.SingalType = (int)vibSignal.VibrationSignalTypeID;
                            context.VibSingal.AddNew(context, vibSignalInfo);

                            foreach (var eigenValue in vibSignal.EigenValueInfo)
                            {
                                //添加
                                var eigenValueInfo = new SignalAlmSet();
                                eigenValueInfo.ValueType = eigenValue.EigenValueTypeID;
                                eigenValueInfo.SingalID = vibSignalInfo.SingalID;
                                eigenValueInfo.AddDate = DateTime.Now;
                                context.SignalAlmSet.AddNew(context, eigenValueInfo);
                            }
                        }
                    }
                    baseResponse.IsSuccessful = true;

                    return baseResponse;
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008671";
            }

            return baseResponse;
        }
        #endregion

        #region 获取不同设备统计信息
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-12
        /// 创建内容:获取不同设备统计信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetDeviceRuningTypeCountResult> GetDeviceRuningTypeCount(GetDeviceRuningTypeCountParameter parameter)
        {
            BaseResponse<GetDeviceRuningTypeCountResult> baseResponse = new BaseResponse<GetDeviceRuningTypeCountResult>();
            GetDeviceRuningTypeCountResult result = new GetDeviceRuningTypeCountResult();
            baseResponse.Result = result;
            baseResponse.IsSuccessful = true;

            #region 验证数据
            //获取不同设备统计信息,监测树ID不正确
            if (parameter.ID < -1 || parameter.ID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008692";
                return baseResponse;
            }

            //获取不同设备统计信息,用户ID不正确
            if (parameter.UserID < -1 || parameter.UserID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008702";
                return baseResponse;
            }
            #endregion

            try
            {
                var deviceIDList = GetDeviceByMonitorTreeIdAndUserId(parameter.UserID, parameter.ID);

                if (deviceIDList.Count == 0)
                {
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }

                var deviceList = deviceRepository
                    .GetDatas<Device>(item => deviceIDList.Contains(item.DevID), true)
                    .ToList();
                if (deviceList != null && deviceList.Any())
                {
                    var devRelaWGIDList = deviceRelationWGRepository
                        .GetDatas<DeviceRelationWG>(item => deviceIDList.Contains(item.DevId), false)
                        .Select(item => new { item.DevId, item.WGID, })
                        .ToList();
                    var tempWGIDList = devRelaWGIDList.Select(item => item.WGID);
                    var wgInfoList = gatewayRepository
                        .GetDatas<Gateway>(item => tempWGIDList.Contains(item.WGID), false)
                        .ToList();

                    //设备运行总数
                    result.RunningDeviceCount = deviceList.Where(item => item.RunStatus == 1).Count();
                    //设备停机总数
                    result.StoppedDeviceCount = deviceList.Where(item => item.RunStatus == 3).Count();
                    //设备中级报警总数
                    result.AlertDeviceCount = deviceList.Where(item => item.AlmStatus == 2).Count();
                    //设备高级报警总数
                    result.WarnDeviceCount = deviceList.Where(item => item.AlmStatus == 3).Count();
                    //设备未采集总数
                    result.UnCollected = deviceList.Where(item => item.AlmStatus == 0).Count();
                    //设备总数
                    result.TotalCount = deviceList.Count();
                    //无线网关ID
                    var wirelessWGIDList = wgInfoList
                        .Where(item => item.DevFormType == (int)EnumDevFormType.SingleBoard
                            || item.DevFormType == (int)EnumDevFormType.iWSN)
                        .Select(item => item.WGID)
                        .ToList();
                    //关联无线网关的设备ID
                    var wirelessDevIDList = devRelaWGIDList
                        .Where(item => wirelessWGIDList.Contains(item.WGID))
                        .Select(item => item.DevId);
                    //关联无线网关的设备总数
                    result.WirelessDevice = deviceList.Where(item => wirelessDevIDList.Contains(item.DevID)).Count();
                    //有线网关ID
                    var wireWGIDList = wgInfoList
                        .Where(item => item.DevFormType == (int)EnumDevFormType.Wired)
                        .Select(item => item.WGID)
                        .ToList();
                    //关联有线网关的设备ID
                    var wireDevIDList = devRelaWGIDList
                        .Where(item => wireWGIDList.Contains(item.WGID))
                        .Select(item => item.DevId);
                    //关联有线网关的设备总数
                    result.WireDevice = deviceList.Where(item => wireDevIDList.Contains(item.DevID)).Count();
                    //设备正常总数
                    result.NormalDeviceCount = deviceList.Where(item => item.AlmStatus == 1).Count();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008711";
            }

            return baseResponse;

        }
        #endregion

        #region 获取近6个月不同报警类型的设备统计
        /// <summary>
        /// 获取近6个月不同报警类型的设备统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<Get6MonthAlarmTypeDeviceCountParameterResult> Get6MonthAlarmTypeDeviceCount(Get6MonthAlarmTypeDeviceCountParameter parameter)
        {
            BaseResponse<Get6MonthAlarmTypeDeviceCountParameterResult> baseResponse = new BaseResponse<Get6MonthAlarmTypeDeviceCountParameterResult>();
            Get6MonthAlarmTypeDeviceCountParameterResult result = new Get6MonthAlarmTypeDeviceCountParameterResult();
            result.AlarmTypeDeviceForMonth = new List<AlarmTypeDeviceForMonth>();
            baseResponse.Result = result;
            baseResponse.IsSuccessful = true;
            #region 验证数据
            //获取近6个月不同报警类型的设备统计,监测树ID不正确
            if (parameter.ID < -1 || parameter.ID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008722";
                return baseResponse;
            }

            //获取近6个月不同报警类型的设备统计,用户ID不正确
            if (parameter.UserID < -1 || parameter.UserID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008732";
                return baseResponse;
            }
            #endregion

            try
            {
                var deviceIDList = GetDeviceByMonitorTreeIdAndUserId(parameter.UserID, parameter.ID);

                if (deviceIDList.Count == 0)
                {
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }

                var deviceList = deviceRepository.GetDatas<Device>(item => deviceIDList.Contains(item.DevID), true).ToList();

                if (deviceList != null && deviceList.Any())
                {

                    int queryMonth = 6;
                    StringBuilder sqlAlmStatus = new StringBuilder();
                    DateTime beginTime = DateTime.Now;
                    DateTime endTime = DateTime.Now.AddMonths(queryMonth);//本月最后一天

                    string temp = string.Empty;
                    foreach (var device in deviceList)
                    {
                        temp += device.DevID + ",";
                    }
                    temp = temp.TrimEnd(',');

                    for (int i = 0; i < queryMonth; i++)
                    {
                        //上一个月的一号
                        beginTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i);
                        endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i + 1);//本月最后一天

                        if (i != 0)
                        {
                            sqlAlmStatus.Append(" UNION ");
                        }

                        #region 报警状态
                        string tempSqlAlmStatus = @"SELECT COUNT(DISTINCT DevID) DeviceCount,
                                                           ISNULL(MAX(AlmStatus), 0) AlmStatus,
                                                           '{0}' Month
                                                    FROM [dbo].[T_SYS_DEV_ALMRECORD]
                                                    WHERE DevID IN ({1})
                                                          AND (
                                                                  (
                                                                      BDate > '{2}'
                                                                      AND BDate < '{3}'
                                                                  )
                                                                  OR (
                                                                         LatestStartTime > '{2}'
                                                                         AND LatestStartTime < '{3}'
                                                                     )
                                                              )
                                                    GROUP BY AlmStatus";
                        tempSqlAlmStatus = string.Format(tempSqlAlmStatus, beginTime.ToString("yyyy-MM"), temp, beginTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd"));
                        sqlAlmStatus.Append(tempSqlAlmStatus);
                        #endregion
                    }

                    var dbContext = new iCMSDbContext();

                    var statList = dbContext.Database.SqlQuery<AlarmTypeDeviceForMonthStat>(sqlAlmStatus.ToString()).ToList<AlarmTypeDeviceForMonthStat>();
                    if (statList != null && statList.Any())
                    {
                        List<string> monthList = statList.Select(item => item.Month).Distinct().ToList();
                        int deviceCount = deviceList.Count;
                        foreach (var month in monthList)
                        {
                            AlarmTypeDeviceForMonth alarmTypeDeviceForMonth = new AlarmTypeDeviceForMonth();
                            alarmTypeDeviceForMonth.Month = month;
                            if (statList != null && statList.Any())
                            {
                                alarmTypeDeviceForMonth.AlertData = statList.Where(item => item.AlmStatus == 2 && item.Month == month).Sum(item => item.DeviceCount);
                                alarmTypeDeviceForMonth.DangerData = statList.Where(item => item.AlmStatus == 3 && item.Month == month).Sum(item => item.DeviceCount);
                                alarmTypeDeviceForMonth.NormalData = statList.Where(item => item.AlmStatus == 1 && item.Month == month).Sum(item => item.DeviceCount);
                                alarmTypeDeviceForMonth.UnCollected = statList.Where(item => item.AlmStatus == 0 && item.Month == month).Sum(item => item.DeviceCount);
                            }
                            result.AlarmTypeDeviceForMonth.Add(alarmTypeDeviceForMonth);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008741";

            }

            return baseResponse;

        }
        #endregion

        #region 获取某监测树下设备状态统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-12
        /// 创建内容:获取某监测树下设备状态统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetDeviceStatusStatisticResult> GetDeviceStatusStatistic(GetDeviceStatusStatisticParameter parameter)
        {
            BaseResponse<GetDeviceStatusStatisticResult> baseResponse = new BaseResponse<GetDeviceStatusStatisticResult>();
            GetDeviceStatusStatisticResult result = new GetDeviceStatusStatisticResult();
            baseResponse.Result = result;
            baseResponse.IsSuccessful = true;
            result.StatisticInfo = new List<DeviceStatusStatistic>();
            #region 验证
            if (parameter.ID > 0)
            {
                //获取某监测树下设备状态统计,监测树不存在
                var monitorTree = monitorTreeRepository.GetByKey(parameter.ID);
                if (monitorTree == null)
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "008752";
                    return baseResponse;
                }

                var monitorTreeType = cacheDICT.GetInstance()
                                      .GetCacheType<MonitorTreeType>()
                                      .Where(item => item.IsUsable == 1 && item.ID > monitorTree.Type).
                                      OrderBy(item => item.ID).OrderBy(item => Convert.ToInt32(item.Describe)).FirstOrDefault();
                if (null == monitorTreeType)
                {
                    monitorTreeType = cacheDICT.GetInstance()
                                      .GetCacheType<MonitorTreeType>()
                                      .Where(item => item.IsUsable == 1 && item.ID == monitorTree.Type).FirstOrDefault();
                }
                result.MonitorTreeType = monitorTreeType.ID;
                result.MonitorTreeCode = monitorTreeType.Code;

            }
            else
            {
                var monitorTreeType = cacheDICT.GetInstance()
              .GetCacheType<MonitorTreeType>()
              .Where(item => item.IsUsable == 1).OrderBy(item => Convert.ToInt32(item.Describe)).FirstOrDefault();

                result.MonitorTreeType = monitorTreeType.ID;
                result.MonitorTreeCode = monitorTreeType.Code;
            }

            #endregion

            try
            {
                var deviceIdList = new List<int>();
                if (parameter.UserID != -1)
                {
                    deviceIdList = userRalationDeviceRepository.GetDatas<UserRalationDevice>(item => item.UserID == parameter.UserID, true).Select(item => item.DevId.Value).ToList();
                }
                else
                {
                    deviceIdList = deviceRepository.GetDatas<Device>(item => true, true).Select(item => item.DevID).ToList();
                }

                string sqlAlarmStatusStat = string.Empty;
                string sqlRunStatusStat = string.Empty;

                string deivceArray = string.Empty;
                var sqlAlarmStatusStatCount = string.Empty;
                if (deviceIdList != null && deviceIdList.Any())
                {
                    foreach (var deviceId in deviceIdList)
                    {
                        deivceArray += deviceId + ",";
                    }
                    deivceArray = deivceArray.TrimEnd(',');
                }

                //没有设备信息
                if (deviceIdList == null || !deviceIdList.Any())
                {
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }

                if (deviceIdList != null && deviceIdList.Any())
                {
                    //报警状态统计
                    sqlAlarmStatusStat = @"SELECT  *
                                                FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY {0} {1} ) AS RowNumber ,
                                                                    *
                                                          FROM      ( SELECT    TSMT.MonitorTreeID,
                                                                                TSMT.Name ,
                                                                                TSMT.Type ,
                                                                                TSD.AlmStatus ,
                                                                                TSMT.PID ,
                                                                                COUNT(1) DeviceCount
                                                                      FROM      dbo.T_SYS_MONITOR_TREE AS TSMT
                                                                                CROSS APPLY F_GetDeivceByMonitorTreeId(TSMT.MonitorTreeID) F
                                                                                LEFT JOIN dbo.T_SYS_DEVICE AS TSD ON TSD.DevID = F.DeviceId
                                                                      where TSMT.PID={2} AND TSD.DevID IN ({5})
                                                                      GROUP BY  TSMT.MonitorTreeID ,
                                                                                TSMT.Name ,
                                                                                TSMT.Type ,
                                                                                TSD.AlmStatus ,
                                                                                TSMT.PID
                                                                    ) t1
                                                        ) t2
                                                WHERE   RowNumber BETWEEN {3} AND {4}";

                    sqlAlarmStatusStatCount = @"SELECT  count(1)
                                                FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY {0} {1} ) AS RowNumber ,
                                                                    *
                                                          FROM      ( SELECT    TSMT.MonitorTreeID,
                                                                                TSMT.Name ,
                                                                                TSMT.Type ,
                                                                                TSD.AlmStatus ,
                                                                                TSMT.PID ,
                                                                                COUNT(1) DeviceCount
                                                                      FROM      dbo.T_SYS_MONITOR_TREE AS TSMT
                                                                                CROSS APPLY F_GetDeivceByMonitorTreeId(TSMT.MonitorTreeID) F
                                                                                LEFT JOIN dbo.T_SYS_DEVICE AS TSD ON TSD.DevID = F.DeviceId
                                                                      where TSMT.PID={2} AND TSD.DevID IN ({3})
                                                                      GROUP BY  TSMT.MonitorTreeID ,
                                                                                TSMT.Name ,
                                                                                TSMT.Type ,
                                                                                TSD.AlmStatus ,
                                                                                TSMT.PID
                                                                    ) t1
                                                        ) t2 ";

                    //运行状态统计
                    sqlRunStatusStat = @"SELECT  *
                                                FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY {0} {1} ) AS RowNumber ,
                                                                    *
                                                          FROM      ( SELECT    TSMT.MonitorTreeID,
                                                                                TSMT.Name ,
                                                                                TSMT.Type ,
                                                                                TSD.RunStatus ,
                                                                                TSMT.PID ,
                                                                                COUNT(1) DeviceCount
                                                                      FROM      dbo.T_SYS_MONITOR_TREE AS TSMT
                                                                                CROSS APPLY F_GetDeivceByMonitorTreeId(TSMT.MonitorTreeID) F
                                                                                LEFT JOIN dbo.T_SYS_DEVICE AS TSD ON TSD.DevID = F.DeviceId
                                                                      where TSMT.PID={2} AND TSD.DevID IN ({5}) 
                                                                      GROUP BY  TSMT.MonitorTreeID ,
                                                                                TSMT.Name ,
                                                                                TSMT.Type ,
                                                                                TSD.RunStatus ,
                                                                                TSMT.PID
                                                                    ) t1
                                                        ) t2
                                                WHERE   RowNumber BETWEEN {3} AND {4}";
                    int beginIndex = (parameter.Page - 1) * parameter.PageSize + 1;
                    int endIndex = parameter.Page * parameter.PageSize;
                    if (parameter.Page == -1)
                    {
                        beginIndex = 1;
                        endIndex = 1000;
                    }

                    int monitorTreeId = 0;
                    if (parameter.ID > 0)
                    {
                        monitorTreeId = parameter.ID;
                    }
                    sqlAlarmStatusStat = string.Format(sqlAlarmStatusStat, parameter.Sort, parameter.Order, monitorTreeId, beginIndex, endIndex, deivceArray, parameter.UserID);

                    sqlAlarmStatusStatCount = string.Format(sqlAlarmStatusStatCount, parameter.Sort, parameter.Order, monitorTreeId, deivceArray);

                    sqlRunStatusStat = string.Format(sqlRunStatusStat, parameter.Sort, parameter.Order, monitorTreeId, beginIndex, endIndex, deivceArray);

                }

                var db = new iCMSDbContext();
                var sqlAlarmStatus = db.Database.SqlQuery<AlarmStatusResult>(sqlAlarmStatusStat).ToList();
                var sqlRunStatus = db.Database.SqlQuery<RunStatusResult>(sqlRunStatusStat).ToList();
                if (sqlAlarmStatus != null && sqlAlarmStatus.Any())
                {
                    var monitorTreeIdList = sqlAlarmStatus.Select(item => item.MonitorTreeID).Distinct().ToList();

                    //数量总条数
                    result.Total = db.Database.SqlQuery<int>(sqlAlarmStatusStatCount).Sum();

                    foreach (var monitorTreeId in monitorTreeIdList)
                    {
                        var stopDeviceCount = 0;
                        DeviceStatusStatistic deviceStatusStatistic = new DeviceStatusStatistic();

                        if (sqlAlarmStatus != null && sqlAlarmStatus.Any())
                        {
                            deviceStatusStatistic.AlertCount = sqlAlarmStatus.Where(item => item.AlmStatus == 2 && item.MonitorTreeID == monitorTreeId).Sum(item => item.DeviceCount);
                            deviceStatusStatistic.DangerCount = sqlAlarmStatus.Where(item => item.AlmStatus == 3 && item.MonitorTreeID == monitorTreeId).Sum(item => item.DeviceCount);
                            deviceStatusStatistic.NormalCount = sqlAlarmStatus.Where(item => item.AlmStatus == 1 && item.MonitorTreeID == monitorTreeId).Sum(item => item.DeviceCount);
                            deviceStatusStatistic.UnCollected = sqlAlarmStatus.Where(item => item.AlmStatus == 0 && item.MonitorTreeID == monitorTreeId).Sum(item => item.DeviceCount);
                            deviceStatusStatistic.MonitorTreeName = sqlRunStatus.Where(item => item.MonitorTreeID == monitorTreeId).First().Name;
                            stopDeviceCount = sqlRunStatus.Where(item => item.RunStatus == 3 && item.MonitorTreeID == monitorTreeId).Sum(item => item.DeviceCount);
                            deviceStatusStatistic.StoppingCount = stopDeviceCount;
                            deviceStatusStatistic.MonitorTreeID = monitorTreeId;
                        }

                        result.StatisticInfo.Add(deviceStatusStatistic);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008771";

            }

            return baseResponse;

        }
        #endregion

        #region 获取某监测树下所有设备状态
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-12
        /// 创建内容:获取某监测树下所有设备状态
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetDeviceStatusStatisticByMonitroIDResult> GetDeviceStatusStatisticByMonitroID(GetDeviceStatusStatisticByMonitroIDParameter parameter)
        {
            BaseResponse<GetDeviceStatusStatisticByMonitroIDResult> baseResponse = new BaseResponse<GetDeviceStatusStatisticByMonitroIDResult>();
            GetDeviceStatusStatisticByMonitroIDResult result = new GetDeviceStatusStatisticByMonitroIDResult();
            result.DevRunningStatListDataInfo = new System.Collections.Generic.List<DevRunningStatListDataInfo>();
            baseResponse.Result = result;
            baseResponse.IsSuccessful = true;

            #region 验证

            //获取某监测树下所有设备状态,监测树id不正确
            if (parameter.ID < -1 || parameter.ID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008782";
                return baseResponse;
            }

            //获取某监测树下所有设备状态，用户Id不正确
            if (parameter.UserID < -1 || parameter.UserID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008792";
                return baseResponse;
            }

            //获取某监测树下所有设备状态，排序名称不正确
            if (string.IsNullOrWhiteSpace(parameter.Sort))
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008802";
                return baseResponse;
            }

            //获取某监测树下所有设备状态，排序不正确
            if (string.IsNullOrWhiteSpace(parameter.OrderName))
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008812";
                return baseResponse;
            }

            #endregion

            try
            {
                var deviceIDList = GetDeviceByMonitorTreeIdAndUserId(parameter.UserID, parameter.ID);

                if (deviceIDList.Count == 0)
                {
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }

                var db = new iCMSDbContext();
                var linq = from tsd in db.Device
                           join tddt in db.DeviceType on new { ID = tsd.DevType } equals new { ID = tddt.ID } into tddt_join
                           from tddt in tddt_join.DefaultIfEmpty()
                           where deviceIDList.Contains(tsd.DevID)
                           select new DevRunningStatListDataInfo
                           {
                               DevID = tsd.DevID,
                               DevName = tsd.DevName,
                               DevNO = tsd.DevNO,
                               DevType = tsd.DevType,
                               DevTypeName = tddt.Name,
                               UseType = tsd.UseType,
                               DevRunningStat = tsd.RunStatus,
                               DevAlarmStat = tsd.AlmStatus,
                               LastUpdateTime = tsd.LastUpdateTime,
                               AddDate = tsd.AddDate,
                           };



                ListSortDirection direction = new ListSortDirection();
                if (parameter.OrderName.ToLower().Equals("desc"))
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                }
                PropertySortCondition sortCondition = new PropertySortCondition(parameter.Sort, direction);
                int total = 0;
                result.DevRunningStatListDataInfo = linq.Where<DevRunningStatListDataInfo>(parameter.Page, parameter.PageSize, out total, null).ToList();

                #region 运行状态
                foreach (var info in result.DevRunningStatListDataInfo)
                {
                    //运行状态
                    if (info.DevRunningStat != 1)
                    {
                        info.DevName = info.DevName + "(stop)";
                    }
                }
                #endregion

                result.Total = total;
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008821";

            }

            return baseResponse;

        }
        #endregion

        #region 获取用户所管理传感器连接状态统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-12
        /// 创建内容:获取用户所管理传感器连接状态统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetWSStatusCountParameterResult> GetWSStatusCount(GetWSStatusCountParameter parameter)
        {
            BaseResponse<GetWSStatusCountParameterResult> baseResponse = new BaseResponse<GetWSStatusCountParameterResult>();
            GetWSStatusCountParameterResult result = new GetWSStatusCountParameterResult();
            baseResponse.Result = result;
            baseResponse.IsSuccessful = true;
            #region 验证
            //获取用户所管理传感器连接状态统计,网关Id不正确
            if (parameter.ID < -1 || parameter.ID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008832";
                return baseResponse;
            }

            //获取用户所管理传感器连接状态统计,用户Id不正确
            if (parameter.UserID < -1 || parameter.UserID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008832";
                return baseResponse;
            }
            #endregion

            try
            {
                var wsIDList = new List<int>();
                var wgIDList = new List<int>();

                if (parameter.UserID != -1)
                {
                    wsIDList = userRelationWSRepository.GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, true).Select(item => item.WSID).ToList();
                }
                else
                {
                    wsIDList = wsRepository.GetDatas<WS>(item => true, true).Select(item => item.WSID).ToList();
                }

                if (parameter.ID != -1)
                {
                    var wsIDListByWG = wsRepository.GetDatas<WS>(item => item.WGID == parameter.ID, true).Select(item => item.WSID).ToList();
                    wsIDList = wsIDList.Intersect(wsIDListByWG).ToList();
                }

                //如果没有数据不进行数据库操作
                if (wsIDList.Count == 0)
                {
                    baseResponse.IsSuccessful = true;
                    return baseResponse;
                }

                var db = new iCMSDbContext();
                var linq = (from tsw in db.WS
                            where wsIDList.Contains(tsw.WSID)
                            group tsw by new
                            {
                                tsw.AlmStatus
                            } into g
                            select new
                            {
                                DeviceCount = g.Count(),
                                AlmStatus = g.Key.AlmStatus
                            }).ToList();

                var linqLinkStatus = (from tsw in db.WS
                                      where wsIDList.Contains(tsw.WSID)
                                      group tsw by new
                                      {
                                          tsw.LinkStatus
                                      } into g
                                      select new
                                      {
                                          DeviceCount = g.Count(),
                                          LinkStatus = g.Key.LinkStatus
                                      }).ToList();

                baseResponse.IsSuccessful = true;
                result.TotalCount = wsIDList.Count;
                result.AlertDeviceCount = linq.Where(item => item.AlmStatus == 2).Sum(item => item.DeviceCount);
                result.WarnDeviceCount = linq.Where(item => item.AlmStatus == 3).Sum(item => item.DeviceCount);
                result.NormalDeviceCount = linq.Where(item => item.AlmStatus == 1).Sum(item => item.DeviceCount);
                result.UnCollectedCount = linq.Where(item => item.AlmStatus == 0).Sum(item => item.DeviceCount);
                result.LinkStatusCount = linqLinkStatus.Where(item => item.LinkStatus == 1).Sum(item => item.DeviceCount);
                result.UnLinkCount = linqLinkStatus.Where(item => item.LinkStatus == 0).Sum(item => item.DeviceCount);

                var wsIDArray = string.Empty;
                foreach (var wsId in wsIDList)
                {
                    wsIDArray += wsId + ",";
                }
                wsIDArray = wsIDArray.TrimEnd(',');
                //获取网关信息
                var sqlMonitorStats = @"SELECT tsw.WGID ID,
                                           tsw.WGName Name,
                                           COUNT(tsw2.WSID) Count
                                    FROM dbo.T_SYS_WG AS tsw
                                        LEFT JOIN dbo.T_SYS_WS AS tsw2
                                            ON tsw2.WGID = tsw.WGID
                                    WHERE tsw.WGID IN
                                          (
                                              SELECT tsw2.WGID FROM dbo.T_SYS_WS AS tsw2 WHERE tsw2.WSID IN ({0})
                                          ) AND tsw2.WSID IN ({0})
                                    GROUP BY tsw.WGID,
                                             tsw.WGName";
                sqlMonitorStats = string.Format(sqlMonitorStats, wsIDArray);
                result.MonitorStats = db.Database.SqlQuery<MonitorInfo>(sqlMonitorStats).ToList();


            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008841";

            }

            return baseResponse;

        }
        #endregion

        #region 获取WG连接状态统计
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-10-16
        /// 创建内容：获取WG连接状态统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetWGLinkStatusCountResult> GetWGLinkStatusCount(GetWGLinkStatusCountParameter parameter)
        {
            BaseResponse<GetWGLinkStatusCountResult> baseResponse = new BaseResponse<GetWGLinkStatusCountResult>();
            GetWGLinkStatusCountResult result = new GetWGLinkStatusCountResult();
            baseResponse.Result = result;
            baseResponse.IsSuccessful = true;

            try
            {
                var wgIDList = GetWGIDListByUserIDAndWGID(parameter.UserID, parameter.ID);

                if (wgIDList == null)
                {
                    var db = new iCMSDbContext();
                    var linq = (from tsw in db.WG

                                group tsw by new
                                {
                                    tsw.LinkStatus
                                } into g
                                select new
                                {
                                    WGCount = g.Count(),
                                    LinkStatus = (System.Int32?)g.Key.LinkStatus
                                }).ToList();
                    result.LinkStatusCount = linq.Where(item => item.LinkStatus == 1).Sum(item => item.WGCount);
                    result.UnLinkCount = linq.Where(item => item.LinkStatus == 0).Sum(item => item.WGCount);
                }
                else
                {
                    var db = new iCMSDbContext();
                    var linq = (from tsw in db.WG
                                where wgIDList.Contains(tsw.WGID)
                                group tsw by new
                                {
                                    tsw.LinkStatus
                                } into g
                                select new
                                {
                                    WGCount = g.Count(),
                                    LinkStatus = (System.Int32?)g.Key.LinkStatus
                                }).ToList();
                    result.LinkStatusCount = linq.Where(item => item.LinkStatus == 1).Sum(item => item.WGCount);
                    result.UnLinkCount = linq.Where(item => item.LinkStatus == 0).Sum(item => item.WGCount);
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008841";

            }
            return baseResponse;
        }
        #endregion

        #region 获取用户所管理传感器在近6个月内，不同报警类型下设备总数
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-10-16
        /// 创建内容：获取用户所管理传感器在近6个月内，不同报警类型下设备总数
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetSixMonthAlarmTypeWSCountResult> GetSixMonthAlarmTypeWSCount(GetSixMonthAlarmTypeWSCountParameter parameter)
        {

            BaseResponse<GetSixMonthAlarmTypeWSCountResult> baseResponse = new BaseResponse<GetSixMonthAlarmTypeWSCountResult>();
            GetSixMonthAlarmTypeWSCountResult result = new GetSixMonthAlarmTypeWSCountResult();
            result.AlarmTypeWS = new List<AlarmTypeWS>();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;
            try
            {


                var wsIDList = GetWSIDListByUserIDAndWGID(parameter.UserID, parameter.ID);

                if (wsIDList != null && wsIDList.Any())
                {

                    int queryMonth = 6;
                    StringBuilder sqlAlmStatus = new StringBuilder();
                    DateTime beginTime = DateTime.Now;
                    DateTime endTime = DateTime.Now.AddMonths(queryMonth);//本月最后一天

                    string temp = string.Empty;
                    foreach (var wsId in wsIDList)
                    {
                        temp += wsId + ",";
                    }
                    temp = temp.TrimEnd(',');

                    for (int i = 0; i < queryMonth; i++)
                    {
                        //上一个月的一号
                        beginTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i);
                        endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-i + 1);//本月最后一天

                        if (i != 0)
                        {
                            sqlAlmStatus.Append(" UNION ");
                        }

                        #region 报警状态
                        string tempSqlAlmStatus = @"SELECT COUNT(DISTINCT DevID) DeviceCount,
                                                           ISNULL(MAX(AlmStatus), 0) AlmStatus,
                                                           '{0}' Month
                                                    FROM [dbo].[T_SYS_WSN_ALMRECORD]
                                                    WHERE WSID IN ({1})
                                                          AND (
                                                                  (
                                                                      BDate > '{2}'
                                                                      AND BDate < '{3}'
                                                                  )
                                                                  OR (
                                                                         LatestStartTime > '{2}'
                                                                         AND LatestStartTime < '{3}'
                                                                     )
                                                              )
                                                    GROUP BY AlmStatus";
                        tempSqlAlmStatus = string.Format(tempSqlAlmStatus, beginTime.ToString("yyyy-MM"), temp, beginTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd"));
                        sqlAlmStatus.Append(tempSqlAlmStatus);
                        #endregion
                    }



                    var dbContext = new iCMSDbContext();

                    var statList = dbContext.Database.SqlQuery<AlarmTypeDeviceForMonthStat>(sqlAlmStatus.ToString()).ToList<AlarmTypeDeviceForMonthStat>();
                    if (statList != null && statList.Any())
                    {
                        List<string> monthList = statList.Select(item => item.Month).Distinct().ToList();
                        foreach (var month in monthList)
                        {
                            AlarmTypeWS alarmTypeWS = new AlarmTypeWS();
                            alarmTypeWS.Month = month;
                            alarmTypeWS.DangerData = statList.Where(item => item.AlmStatus == 3).Sum(item => item.DeviceCount);
                            alarmTypeWS.AlertData = statList.Where(item => item.AlmStatus == 2).Sum(item => item.DeviceCount);
                            alarmTypeWS.NormalData = statList.Where(item => item.AlmStatus == 1).Sum(item => item.DeviceCount);
                            alarmTypeWS.UnCollectedData = statList.Where(item => item.AlmStatus == 0).Sum(item => item.DeviceCount);
                            result.AlarmTypeWS.Add(alarmTypeWS);
                        }
                    }
                }
                else
                {
                    result.AlarmTypeWS = new List<AlarmTypeWS>();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008861";

            }

            return baseResponse;

        }
        #endregion

        #region 获取用户管理的WS和网关信息
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-10-16
        /// 创建内容：获取用户管理的WS和网关信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetWSAndWGStatusInfoResult> GetWSAndWGStatusInfo(GetWSAndWGStatusInfoParameter parameter)
        {
            BaseResponse<GetWSAndWGStatusInfoResult> baseResponse = new BaseResponse<GetWSAndWGStatusInfoResult>();
            GetWSAndWGStatusInfoResult result = new GetWSAndWGStatusInfoResult();
            result.Sensorlist = new List<Sensorlist>();
            baseResponse.Result = result;
            baseResponse.IsSuccessful = true;

            #region 验证数据
            //获取用户管理的WS和网关信息时，用户id不正确
            if (parameter.UserID < -1 || parameter.UserID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009722";
                return baseResponse;
            }

            //获取用户管理的WS和网关信息时，网关id不正确
            if (parameter.ID < -1 || parameter.ID == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009732";
                return baseResponse;
            }
            #endregion
            try
            {
                var wgIDList = GetWGIDListByUserIDAndWGID(parameter.UserID, parameter.ID);

                //获取用户管理的ws
                var wsIDList = new List<int>();
                if (parameter.UserID == -1)
                {
                    wsIDList = wsRepository.GetDatas<WS>(item => true, true).Select(item => item.WSID).ToList();
                }
                else
                {
                    wsIDList = userRelationWSRepository.GetDatas<UserRelationWS>(item => item.UserID == parameter.UserID, true).Select(item => item.WSID).ToList();
                }
                ListSortDirection sortOrder = parameter.Order.ToLower().Equals("asc") ? ListSortDirection.Ascending : ListSortDirection.Descending;
                PropertySortCondition[] sortList = new PropertySortCondition[]
                {
                        new PropertySortCondition(parameter.OrderName, sortOrder),
                };

                //无条件，查询
                if (wgIDList == null)
                {

                    var db = new iCMSDbContext();
                    var linq = from t_sys_wg in db.WG
                               select new Sensorlist
                               {
                                   WGID = t_sys_wg.WGID,
                                   WGName = t_sys_wg.WGName,
                                   WGNO = t_sys_wg.WGNO,
                                   NetWorkID = t_sys_wg.NetWorkID,
                                   WGTypeId = t_sys_wg.WGType,
                                   LinkStatus = t_sys_wg.LinkStatus,
                                   AddDate = t_sys_wg.AddDate,
                                   WGModel = t_sys_wg.WGModel,
                                   SoftwareVersion = t_sys_wg.SoftwareVersion,
                                   RunStatus = t_sys_wg.RunStatus,
                                   ImageID = t_sys_wg.ImageID,
                                   Remark = t_sys_wg.Remark,
                                   PersonInCharge = t_sys_wg.PersonInCharge,
                                   PersonInChargeTel = t_sys_wg.PersonInChargeTel,
                                   MonitorTreeID = t_sys_wg.MonitorTreeID,
                                   AgentAddress = t_sys_wg.AgentAddress,
                               };
                    int total = 0;
                    result.Sensorlist = linq.Where<Sensorlist>(parameter.Page, parameter.PageSize, out total, sortList).ToList();

                    if (result.Sensorlist != null && result.Sensorlist.Any())
                    {
                        var wsList = wsRepository.GetDatas<WS>(item => true, true).ToList();
                        if (wsList != null && wsList.Any())
                        {
                            SetWGChild(result.Sensorlist, wsList, wsIDList);
                        }
                    }
                }
                else
                {
                    var db = new iCMSDbContext();
                    var linq = from t_sys_wg in db.WG
                               where wgIDList.Contains(t_sys_wg.WGID)
                               select new Sensorlist
                               {
                                   WGID = t_sys_wg.WGID,
                                   WGName = t_sys_wg.WGName,
                                   WGNO = t_sys_wg.WGNO,
                                   NetWorkID = t_sys_wg.NetWorkID,
                                   WGTypeId = t_sys_wg.WGType,
                                   LinkStatus = t_sys_wg.LinkStatus,
                                   AddDate = t_sys_wg.AddDate,
                                   WGModel = t_sys_wg.WGModel,
                                   SoftwareVersion = t_sys_wg.SoftwareVersion,
                                   RunStatus = t_sys_wg.RunStatus,
                                   ImageID = t_sys_wg.ImageID,
                                   Remark = t_sys_wg.Remark,
                                   PersonInCharge = t_sys_wg.PersonInCharge,
                                   PersonInChargeTel = t_sys_wg.PersonInChargeTel,
                                   MonitorTreeID = t_sys_wg.MonitorTreeID,
                                   AgentAddress = t_sys_wg.AgentAddress,
                               };
                    int total = 0;
                    result.Sensorlist = linq.Where<Sensorlist>(parameter.Page, parameter.PageSize, out total, sortList).ToList();
                    result.Total = total;
                    if (result.Sensorlist != null && result.Sensorlist.Any())
                    {


                        var wsList = wsRepository.GetDatas<WS>(item => true, true).ToList();
                        if (wsList != null && wsList.Any())
                        {
                            SetWGChild(result.Sensorlist, wsList, wsIDList);
                        }
                    }
                }

                #region 修改网关连接数量
                //修改网关连接数量
                List<WirelessGatewayType> wirelessGatewayTypeList = cacheDICT.GetInstance()
                                                    .GetCacheType<WirelessGatewayType>()
                                                    .ToList();
                //修改网关类型
                if (result.Sensorlist != null && result.Sensorlist.Any())
                {
                    foreach (var info in result.Sensorlist)
                    {
                        var gatevayType = wirelessGatewayTypeList.Where(item => item.ID == info.WGTypeId).FirstOrDefault();
                        if (gatevayType != null)
                        {
                            info.WGTypeName = gatevayType.Name;
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008871";

            }

            return baseResponse;

        }
        #endregion

        #region 私有方法

        #region 获取设备Id通过监测树Id和用户id
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-10-14
        /// 创建内容 ：获取设备Id通过监测树Id和用户id
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="monitorTreeId">监测树Id</param>
        /// <returns></returns>
        private List<int> GetDeviceByMonitorTreeIdAndUserId(int userId, int monitorTreeId)
        {
            var montorTreeList = monitorTreeRepository.GetDatas<MonitorTree>(item => true, true);
            var montorTreeIDList = new List<int>();
            var lastMontorTreeIDList = new List<int>();
            var deviceIDList = new List<int>();
            var montiorDeviceIDList = new List<int>();

            if (monitorTreeId != -1)
            {
                #region 查找监测树装置级别Id
                if (montorTreeList != null && montorTreeList.Any())
                {
                    montorTreeIDList = montorTreeList.Where(item => item.MonitorTreeID == monitorTreeId).Select(item => item.MonitorTreeID).ToList();
                    if (montorTreeIDList.Count > 0)
                    {
                        int mtTypeCount = monitorTreeTypeRepository.GetDatas<MonitorTreeType>(p => p.IsUsable == 1, true).Count();

                        for (int i = 0; i < mtTypeCount; i++)
                        {
                            lastMontorTreeIDList = montorTreeIDList;
                            montorTreeIDList = montorTreeList.Where(item => montorTreeIDList.Contains(item.PID)).Select(item => item.MonitorTreeID).ToList();
                            if (montorTreeIDList.Count() == 0)
                            {
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                //获取最后一个节点的监测树id
                lastMontorTreeIDList = deviceRepository.GetDatas<Device>(item => true, true).Select(item => item.MonitorTreeID).ToList();
            }


            #region 用户过滤设备信息
            if (userId != -1)
            {
                var userDeviceList = userRalationDeviceRepository.GetDatas<UserRalationDevice>(item => item.UserID == userId, true).ToList();
                if (userDeviceList != null)
                {
                    deviceIDList = userDeviceList.Select(item => item.DevId.Value).ToList();
                }
            }
            else
            {
                deviceIDList = deviceRepository.GetDatas<Device>(item => true, true).Select(item => item.DevID).ToList();
            }

            #endregion

            #region 查找监测树下的设备信息
            if (lastMontorTreeIDList != null && lastMontorTreeIDList.Any())
            {
                montiorDeviceIDList = deviceRepository.GetDatas<Device>(item => lastMontorTreeIDList.Contains(item.MonitorTreeID), true).Select(item => item.DevID).ToList();

                //用户过滤
                if (userId != -1)
                {
                    //取交集
                    deviceIDList = montiorDeviceIDList.Intersect(deviceIDList).ToList();
                }
                else
                {
                    deviceIDList = montiorDeviceIDList;
                }
            }
            #endregion

            return deviceIDList;
        }
        #endregion

        #endregion

        #region 获取网关ID
        /// <summary>
        /// 获取网关ID
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="wgId">网关Id</param>
        /// <returns></returns>
        private List<int> GetWGIDListByUserIDAndWGID(int userId, int wgId)
        {
            var wgIDList = new List<int>();

            var wgIDByUser = new List<int>();
            var wgIDByWG = new List<int>();

            if (userId != -1)
            {
                var relationWSIDList = userRelationWSRepository.GetDatas<UserRelationWS>(item => item.UserID == userId, true).Select(item => item.WSID).ToList();
                if (relationWSIDList != null && relationWSIDList.Any())
                {
                    wgIDByUser = wsRepository.GetDatas<WS>(item => relationWSIDList.Contains(item.WSID), true).Select(item => item.WGID).Distinct().ToList();
                }
            }
            else
            {
                wgIDByUser = gatewayRepository.GetDatas<Gateway>(item => true, true).Select(item => item.WGID).Distinct().ToList();
            }

            if (wgId != -1)
            {
                wgIDByWG.Add(wgId);
            }
            else
            {
                wgIDByWG = gatewayRepository.GetDatas<Gateway>(item => true, true).Select(item => item.WGID).Distinct().ToList();
            }

            if (wgIDByUser != null && wgIDByWG != null)
            {
                wgIDList = wgIDByUser.Intersect(wgIDByWG).ToList();
            }
            else if (wgIDByUser != null)
            {
                wgIDList = wgIDByUser;
            }
            else
            {
                wgIDList = wgIDByWG;
            }

            return wgIDList;
        }
        #endregion

        #region 获取WSID
        /// <summary>
        /// 获取WSID
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="wgId">网关Id</param>
        /// <returns></returns>
        private List<int> GetWSIDListByUserIDAndWGID(int userId, int wgId)
        {
            var wsIDList = new List<int>();

            var wsIDByUser = new List<int>();
            var wsIDByWG = new List<int>();

            if (userId != -1)
            {
                wsIDByUser = userRelationWSRepository.GetDatas<UserRelationWS>(item => item.UserID == userId, true).Select(item => item.WSID).ToList();
            }
            else
            {
                //管理员
                wsIDByUser = userRelationWSRepository.GetDatas<UserRelationWS>(item => item.UserID == 1011, true).Select(item => item.WSID).ToList();
            }

            if (wgId != -1)
            {
                wsIDByWG = wsRepository.GetDatas<WS>(item => item.WGID == wgId, true).Select(item => item.WSID).ToList();
            }
            else
            {
                wsIDByWG = wsRepository.GetDatas<WS>(item => true, true).Select(item => item.WSID).ToList();
            }

            if (wsIDByWG != null && wsIDByUser != null)
            {
                wsIDList = wsIDByUser.Intersect(wsIDByWG).ToList();
            }
            else if (wsIDByWG != null)
            {
                wsIDList = wsIDByWG;
            }
            else
            {
                wsIDList = wsIDByUser;
            }

            return wsIDList;
        }
        #endregion

        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-10-16
        /// 创建内容：设置设备报警状态统计
        /// </summary>
        /// <param name="deviceIDList">设备集合</param>
        /// <param name="info">监测树信息</param>
        /// <param name="deviceStatusStatistic">统计信息</param>
        private void SetDeviceAlarmStatus(
            List<int> deviceIDList,
            MonitorTreeStatusInfo info,
            DeviceStatusStatistic deviceStatusStatistic)
        {
            var db = new iCMSDbContext();
            #region 统计设备报警信息
            //统计设备报警信息
            var alarmLinq = (from tsd in db.Device
                             where deviceIDList.Contains(tsd.DevID)
                             group tsd by new
                             {
                                 tsd.AlmStatus
                             } into g
                             select new DeviceStatusInfo
                             {
                                 DeviceCount = g.Count(),
                                 AlmStatus = g.Key.AlmStatus
                             }).ToList();
            deviceStatusStatistic.MonitorTreeID = info.MonitorTreeID;
            deviceStatusStatistic.MonitorTreeName = info.Name;
            int alarmCount = 0;
            int dangerCount = 0;
            if (alarmLinq != null && alarmLinq.Any())
            {
                var alarmList = alarmLinq.Where(item => item.AlmStatus == 2);
                if (alarmList != null && alarmList.Any())
                {
                    alarmCount = alarmList.FirstOrDefault().DeviceCount;
                }

                alarmList = alarmLinq.Where(item => item.AlmStatus == 3);
                if (alarmList != null && alarmList.Any())
                {
                    dangerCount = alarmList.FirstOrDefault().DeviceCount;
                }
            }

            deviceStatusStatistic.AlertCount = alarmCount;
            deviceStatusStatistic.DangerCount = dangerCount;
            #endregion
        }

        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-10-16
        /// 创建内容：设置设备运行状态统计
        /// </summary>
        /// <param name="deviceIDList">设备集合</param>
        /// <param name="info">监测树信息</param>
        /// <param name="deviceStatusStatistic">统计信息</param>
        private void SetDeviceRunStatus(
            List<int> deviceIDList,
            MonitorTreeStatusInfo info,
            DeviceStatusStatistic deviceStatusStatistic)
        {
            var db = new iCMSDbContext();
            #region 统计运行状态
            //统计设备报警信息
            var runStatusLinq = (from tsd in db.Device
                                 where deviceIDList.Contains(tsd.DevID)
                                 group tsd by new
                                 {
                                     tsd.RunStatus
                                 } into g
                                 select new DeviceStatusInfo
                                 {
                                     DeviceCount = g.Count(),
                                     RunStatus = g.Key.RunStatus
                                 }).ToList();
            int normalCount = 0;
            int stopCount = 0;
            if (runStatusLinq != null && runStatusLinq.Any())
            {
                //正常
                var runStatusList = runStatusLinq.Where(item => item.RunStatus == 1);
                if (runStatusList != null && runStatusList.Any())
                {
                    normalCount = runStatusList.FirstOrDefault().DeviceCount;
                }

                //停机
                runStatusList = runStatusLinq.Where(item => item.RunStatus == 3);
                if (runStatusList != null && runStatusList.Any())
                {
                    stopCount = runStatusList.FirstOrDefault().DeviceCount;
                }
            }

            deviceStatusStatistic.NormalCount = normalCount;
            deviceStatusStatistic.StoppingCount = stopCount;
            #endregion
        }

        /// <summary>
        /// 主备切换时获取设备列表信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetMainStandbyDeviceListByDeviceIdResult> GetMainStandbyDeviceListByDeviceId(
            GetMainStandbyDeviceListByDeviceIdParameter parameter)
        {
            BaseResponse<GetMainStandbyDeviceListByDeviceIdResult> baseResponse = new BaseResponse<GetMainStandbyDeviceListByDeviceIdResult>();
            GetMainStandbyDeviceListByDeviceIdResult result = new GetMainStandbyDeviceListByDeviceIdResult();
            result.DeviceList = new List<DeviceListForDeviceChange>();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;
            #region 验证数据
            //主备切换时获取设备列表信息时,设备id不正确
            if (parameter.DeviceID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009522";
                return baseResponse;
            }

            //主备切换时获取设备列表信息时,用户id不正确
            if (parameter.UserId < -1 || parameter.UserId == 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009532";
                return baseResponse;
            }
            #endregion

            try
            {
                var device = deviceRepository.GetByKey(parameter.DeviceID);
                //主备切换时获取设备列表信息时,设备不存在
                if (device == null)
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "009552";
                    return baseResponse;
                }

                var measureSiteTypeIDList = measureSiteRepository
                    .GetDatas<MeasureSite>(item => item.DevID == device.DevID, true)
                    .Select(item => item.MSiteName)
                    .ToList();
                //主备切换时获取设备列表信息时,测量位置类型不存在
                if (measureSiteTypeIDList == null || !measureSiteTypeIDList.Any())
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "009562";
                    return baseResponse;
                }

                //查找配置的设备
                int deviceType = 0;
                //如果是主用的就取备用的
                if (device.UseType == 0)
                {
                    deviceType = 1;
                }
                else
                {
                    deviceType = 0;
                }

                var deviceIDList = new List<int>();
                var deviceUserList = new List<int>();
                if (parameter.UserId > 0)
                {
                    deviceUserList = userRalationDeviceRepository
                        .GetDatas<UserRalationDevice>(item => item.UserID == parameter.UserId, true)
                        .Select(item => item.DevId.Value)
                        .ToList();
                }
                else
                {
                    deviceUserList = null;
                }

                //测量位置数量
                int measureSiteTypeCount = measureSiteTypeIDList.Count();

                //过滤用户相关设备
                if (deviceUserList != null)
                {
                    string deviceIDArray = string.Empty;
                    foreach (var deviceId in deviceUserList)
                    {
                        deviceIDArray += deviceId + ",";
                    }
                    deviceIDArray = deviceIDArray.TrimEnd(',');

                    var db = new iCMSDbContext();
                    string sql = @"SELECT  TSD.DevID
                            FROM    dbo.T_SYS_DEVICE AS TSD
                            WHERE TSD.UseType={0} AND TSD.DevID IN ({1}) and
                                   (SELECT    COUNT(1)
                              FROM      dbo.T_SYS_MEASURESITE AS TSM
                              WHERE     TSM.DevID = TSD.DevID
                              )={2}";
                    sql = string.Format(sql, deviceType, deviceIDArray, measureSiteTypeCount);
                    deviceIDList = db.Database.SqlQuery<int>(sql).ToList();
                }
                else
                {
                    var db = new iCMSDbContext();
                    string sql = @"SELECT  TSD.DevID 
                            FROM    dbo.T_SYS_DEVICE AS TSD
                            WHERE TSD.UseType={0} and
                                   (SELECT    COUNT(1)
                              FROM      dbo.T_SYS_MEASURESITE AS TSM
                              WHERE     TSM.DevID = TSD.DevID
                              )={1}";
                    sql = string.Format(sql, deviceType, measureSiteTypeCount);
                    deviceIDList = db.Database.SqlQuery<int>(sql).ToList();
                }

                //主备切换时获取设备列表信息时,未找到相配置的设备信息
                if (deviceIDList == null && deviceIDList.Any())
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "009572";
                    return baseResponse;
                }

                //    var deviceList = deviceRepository.GetDatas<Device>(item => deviceIDList.Contains(item.DevID), true);
                if (deviceIDList != null && deviceIDList.Any())
                {
                    deviceIDList = measureSiteRepository.GetDatas<MeasureSite>(
                        item => deviceIDList.Contains(item.DevID)
                        && measureSiteTypeIDList.Contains(item.MSiteName), true)
                        .Select(item => item.DevID).ToList();

                    if (deviceIDList != null && deviceIDList.Any())
                    {
                        result.DeviceList = deviceRepository
                            .GetDatas<Device>(item => deviceIDList.Contains(item.DevID), true)
                            .Select(item => new DeviceListForDeviceChange
                            {
                                DeviceID = item.DevID,
                                DeviceName = item.DevName,
                            })
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009541";
            }
            return baseResponse;
        }

        #region 设置WG子节点
        /// <summary>
        /// 设置WG子节点
        /// </summary>
        /// <param name="sensorlist">传感器列表</param>
        /// <param name="wsList">传感器列表</param>
        public void SetWGChild(List<Sensorlist> sensorlist, List<WS> wsList, List<int> userRelationWSIDList)
        {
            List<SensorType> sensorTypeList = cacheDICT.GetInstance()
                .GetCacheType<SensorType>()
                .ToList();
            foreach (var wg in sensorlist)
            {
                wg.Children = wsList
                    .Where(item => item.WGID == wg.WGID && userRelationWSIDList.Contains(item.WSID))
                    .Select(item =>
                    {

                        var wsTypeName = string.Empty;
                        var sensorType = sensorTypeList.Where(info => info.ID == item.SensorType).FirstOrDefault();
                        if (sensorType != null)
                        {
                            wsTypeName = sensorType.Name;
                        }

                        var operation = operationRepository.GetDatas<Operation>(info => info.WSID == item.WSID, true).FirstOrDefault();
                        int operationStatus = 0;
                        var operationStatusName = string.Empty;
                        if (operation != null)
                        {
                            int.TryParse(operation.OperationResult, out operationStatus);

                            switch (operationStatus)
                            {
                                case 0:
                                    operationStatusName = "初始状态";
                                    break;
                                case 3:
                                    operationStatusName = "正在升级中";
                                    break;
                                default:
                                    operationStatusName = operation.EDate == null ? "" : operation.EDate.ToString();
                                    break;
                            }
                        }
                        return new WSWGStatusInfo
                        {
                            WSID = item.WSID,
                            WSNO = item.WSNO,
                            WSName = item.WSName,
                            BatteryVolatage = 0,//进行修改
                            UseStatus = item.UseStatus,
                            AlmStatus = item.AlmStatus,
                            MACADDR = item.MACADDR,
                            SensorTypeId = item.SensorType,
                            LinkStatus = item.LinkStatus,
                            OperationStatus = operationStatus,
                            OperationStatusName = operationStatusName,
                            SensorTypeName = wsTypeName,
                            AddDate = item.AddDate,
                            Vendor = item.Vendor,
                            WSModel = item.WSModel,
                            SetupTime = item.SetupTime,
                            SetupPersonInCharge = item.SetupPersonInCharge,
                            SNCode = item.SNCode,
                            FirmwareVersion = item.FirmwareVersion,
                            AntiExplosionSerialNo = item.AntiExplosionSerialNo,
                            RunStatus = item.RunStatus,
                            ImageID = item.ImageID,
                            PersonInCharge = item.PersonInCharge,
                            PersonInChargeTel = item.PersonInChargeTel,
                            Remark = item.Remark,
                        };
                    })
                    .ToList();
            }
        }
        #endregion

        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-12-19
        /// 创建内容：删除实时表，测点阈值数据,
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="measureSiteId">测点</param>
        /// <param name="vibsignalid">振动id</param>
        private void DeleteRealTimeMeasureSite(iCMSDbContext context, int measureSiteId)
        {
            context.RealTimeAlarmThreshold.Delete(context, item => item.MeasureSiteID == measureSiteId);
        }

        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-12-19
        /// 创建内容：删除实时表，阈值数据
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="measureSiteId">测点</param>
        /// <param name="vibsignalid">振动id</param>
        private void DeleteRealTimeVibSignal(iCMSDbContext context, int measureSiteId, int vibsignalType)
        {
            context.RealTimeAlarmThreshold.Delete(context,
                item => item.MeasureSiteID == measureSiteId
                    && item.MeasureSiteThresholdType == vibsignalType);
        }
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-12-19
        /// 创建内容：删除特征值时，删除实时数据库
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="measureSiteId">测点</param>
        /// <param name="vibsignalId">振动信号</param>
        /// <param name="setAlarmId">报警id</param>
        private void DeleteRealTimeEigenValue(iCMSDbContext context, int measureSiteId, int vibsignalType, int eigenValueTypeId)
        {
            var eigenValue = context.EigenValueType.GetByKey(context, eigenValueTypeId);
            EnumEigenType eigenValueType = EnumEigenType.PeakValue;
            if (eigenValue != null)
            {
                if (eigenValue.Code.Contains("_FZ"))
                {
                    eigenValueType = EnumEigenType.PeakValue;
                }

                if (eigenValue.Code.Contains("_FFZ"))
                {
                    eigenValueType = EnumEigenType.PeakPeakValue;
                }

                if (eigenValue.Code.Contains("_YXZ"))
                {
                    eigenValueType = EnumEigenType.EffectivityValue;
                }

                if (eigenValue.Code.Contains("_DTZ"))
                {
                    eigenValueType = EnumEigenType.CarpetValue;
                }

                if (eigenValue.Code.Contains("_ZCZT"))
                {
                    eigenValueType = EnumEigenType.LQValue;
                }
            }
            context.RealTimeAlarmThreshold.Delete(context,
                item => item.MeasureSiteID == measureSiteId
                    && item.MeasureSiteThresholdType == vibsignalType
                    && item.EigenValueType == (int)eigenValueType);
        }

        #region 通过设备ID获取设备上所有的未挂靠转速的非转速测点

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：通过设备ID获取设备上所有的未挂靠转速的非转速测点
        /// </summary>
        /// <param name="parameter">获取设备上所有的未挂靠转速的非转速测点参数</param>
        public BaseResponse<GetMSiteDataByDevIDForDeviceTreeResult> GetMSiteDataByDevIDForDeviceTree(GetMSiteDataByDevIDForDeviceTreeParameter parameter)
        {
            BaseResponse<GetMSiteDataByDevIDForDeviceTreeResult> baseResponse = new BaseResponse<GetMSiteDataByDevIDForDeviceTreeResult>();
            GetMSiteDataByDevIDForDeviceTreeResult result = new GetMSiteDataByDevIDForDeviceTreeResult();
            result.MeasureInfoList = new List<MeasureInfoResult>();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;

            //获取设备上所有的未挂靠转速的非转速测点时，设备id不正确
            if (parameter.DeviceID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }

            try
            {
                //获取该设备下所有的测点的简要信息集合
                var mSiteInfoList = measureSiteRepository
                    .GetDatas<MeasureSite>(item => item.DevID == parameter.DeviceID, false)
                    .Select(item => new { item.MSiteID, item.MSiteName, item.RelationMSiteID, })
                    .ToList();
                //测点类型ID集合
                var mSiteNameTypeIDList = mSiteInfoList.Select(item => item.MSiteName);
                //测点类型集合
                var mSiteNameTypeList = cacheDICT.GetInstance()
                    .GetCacheType<MeasureSiteType>(item => mSiteNameTypeIDList.Contains(item.ID))
                    .ToList();

                foreach (var mSiteInfo in mSiteInfoList.Where(item => !item.RelationMSiteID.HasValue))
                {
                    if (!mSiteInfoList.Any(item => item.RelationMSiteID == mSiteInfo.MSiteID))
                    {
                        string mSiteNameTypeStr = "";
                        var mSiteNameTypeObj = mSiteNameTypeList.FirstOrDefault(item => item.ID == mSiteInfo.MSiteName);
                        if (mSiteNameTypeObj != null)
                            mSiteNameTypeStr = mSiteNameTypeObj.Name;
                        result.MeasureInfoList.Add(new MeasureInfoResult
                        {
                            WID = mSiteInfo.MSiteID,
                            MSiteTypeId = mSiteInfo.MSiteName,
                            MStieTypeName = mSiteNameTypeStr,
                        });
                    }
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
            }
            return baseResponse;
        }

        #endregion

        #region 获取用户管理网关、采集单元下拉列表
        public BaseResponse<GetWGMaintainListResult> GetWGMaintainList(GetWGMaintainListParameter param)
        {
            BaseResponse<GetWGMaintainListResult> response = new BaseResponse<GetWGMaintainListResult>();
            GetWGMaintainListResult result = new GetWGMaintainListResult();

            try
            {
                List<WGInfoItem> wgInfoList = new List<WGInfoItem>();
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    //根据WS找到其关联的所有WG
                    var linq = (from wsr in dbContext.UserRelationWS
                                join ws in dbContext.WS on wsr.WSID equals ws.WSID
                                where wsr.UserID == param.UserID
                                select ws.WGID).Distinct().ToList();

                    wgInfoList = dbContext.WG
                        .Where(t => linq.Contains(t.WGID))
                        .ToArray()
                        .Select(t =>
                        {
                            string wgName = string.Empty;
                            switch (t.DevFormType)
                            {
                                case (int)EnumDevFormType.SingleBoard:
                                    wgName = "单板";
                                    break;
                                case (int)EnumDevFormType.iWSN:
                                    wgName = "轻量级";
                                    break;
                                case (int)EnumDevFormType.Wired:
                                    wgName = "采集单元";
                                    break;
                            }
                            wgName = string.Format("{0}({1})", t.WGName, wgName);
                            return new WGInfoItem
                            {
                                WGID = t.WGID,
                                WGName = wgName,
                                WGFormType = t.DevFormType,
                            };
                        })
                        .ToList();

                    result.WGInfoList.AddRange(wgInfoList);

                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "009541";

                return response;
            }
        }
        #endregion

        #region 转速测点相关接口

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：获取转速测量位置详细信息
        /// </summary>
        /// <param name="parameter">获取转速测量位置详细信息参数</param>
        /// <returns></returns>
        public BaseResponse<GetSpeedMSiteForDeviceTreeResult> GetSpeedMSiteForDeviceTree(GetSpeedMSiteForDeviceTreeParameter parameter)
        {
            BaseResponse<GetSpeedMSiteForDeviceTreeResult> baseResponse = new BaseResponse<GetSpeedMSiteForDeviceTreeResult>();
            GetSpeedMSiteForDeviceTreeResult result = new GetSpeedMSiteForDeviceTreeResult();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;

            //获取转速测量位置详细信息时，测点ID不对
            if (parameter.MSiteID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }

            try
            {
                //根据转速测点ID获取转速测量定义信息
                var speedSamplingMDFInfo = speedSamplingMDFRepository
                    .GetDatas<SpeedSamplingMDF>(item => item.MSiteID == parameter.MSiteID, false)
                    .SingleOrDefault();
                if (speedSamplingMDFInfo != null)
                {
                    result.SpeedMDFID = speedSamplingMDFInfo.SpeedMDFID;
                    result.PulsNumPerP = speedSamplingMDFInfo.PulsNumPerP;
                }

                //获取转速测点信息
                var speedMSiteInfo = measureSiteRepository
                    .GetDatas<MeasureSite>(item => item.MSiteID == parameter.MSiteID, false)
                    .SingleOrDefault();
                if (speedMSiteInfo != null)
                {
                    result.MSiteID = speedMSiteInfo.MSiteID;
                    result.WSID = speedMSiteInfo.WSID ?? 0;
                    if (speedMSiteInfo.WSID.HasValue && speedMSiteInfo.WSID.Value > 0)
                    {
                        var ws = wsRepository.GetByKey(speedMSiteInfo.WSID.Value);
                        if (ws != null)
                        {
                            result.WSName = ws.WSName;
                        }
                    }

                    //获取转速关联的测点
                    var speedRelationMSiteInfo = measureSiteRepository
                        .GetDatas<MeasureSite>(item => item.MSiteID == speedMSiteInfo.RelationMSiteID, false)
                        .SingleOrDefault();
                    if (speedRelationMSiteInfo != null)
                    {
                        result.RelationMSiteID = speedRelationMSiteInfo.MSiteID;
                        //获取转速关联的测点名称
                        var mSiteTypeInfo = cacheDICT.GetInstance()
                            .GetCacheType<MeasureSiteType>(item => item.ID == speedRelationMSiteInfo.MSiteName)
                            .FirstOrDefault();
                        if (mSiteTypeInfo != null)
                            result.RelatedMSiteName = mSiteTypeInfo.Name;
                    }
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
            }
            return baseResponse;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：添加转速测量位置
        /// </summary>
        /// <param name="parameter">添加转速测量位置参数</param>
        /// <returns></returns>
        public BaseResponse<bool> AddSpeedMSiteForDeviceTree(AddSpeedMSiteForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> baseResponse = new BaseResponse<bool>();
            baseResponse.IsSuccessful = true;

            //添加转速测量位置时，设备id不正确
            if (parameter.DeviceID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }
            //添加转速测量位置时，测点类型id不正确
            if (parameter.MSiteTypeId < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }
            //添加转速测量位置时，传感器id不正确
            if (parameter.WSID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }
            //添加转速测量位置时，关联测点id不正确
            if (parameter.RelationMSiteID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }

            try
            {
                MeasureSite measureSite = new MeasureSite();
                measureSite.MSiteName = parameter.MSiteTypeId;
                measureSite.DevID = parameter.DeviceID;
                measureSite.WSID = parameter.WSID;
                measureSite.RelationMSiteID = parameter.RelationMSiteID;
                measureSite.AddDate = System.DateTime.Now;
                measureSite.MSiteSDate = System.DateTime.Now;
                measureSite.MSiteStatus = 0;
                measureSite.SerialNo = 0;

                return ExecuteDB.ExecuteTrans((context) =>
                {
                    //检测传感器是否被使用,状态修改为1，未被使用
                    if (context.Measuresite
                        .GetDatas<MeasureSite>(context, p => p.WSID.HasValue
                            && p.WSID == measureSite.WSID)
                        .Any() && measureSite.WSID.HasValue && measureSite.WSID > 0)
                    {
                        WS wsUseStatusObj = context.WS
                            .GetByKey<WS>(context, measureSite.WSID);
                        if (wsUseStatusObj.UseStatus == 0)
                        {
                            wsUseStatusObj.UseStatus = 1;
                            context.WS.Update(context, wsUseStatusObj);
                        }

                        baseResponse.IsSuccessful = false;
                        baseResponse.Code = "000000";
                        return baseResponse;
                    }
                    else
                    {
                        OperationResult operationResult = context.Measuresite
                            .AddNew(context, measureSite);
                        if (operationResult.ResultType == EnumOperationResultType.Success)
                        {
                            //关联WS后，同步设置此WS的使用状态为 使用 
                            WS wsInDb = null;
                            if (measureSite.WSID.HasValue && measureSite.WSID.Value > 0)
                            {
                                wsInDb = context.WS
                                    .GetByKey(context, measureSite.WSID.Value);
                                wsInDb.UseStatus = 1;
                                context.WS.Update(context, wsInDb);
                            }

                            #region 添加转速测量定义

                            SpeedSamplingMDF speedSamplingMDF = new SpeedSamplingMDF();
                            speedSamplingMDF.MSiteID = measureSite.MSiteID;
                            speedSamplingMDF.PulsNumPerP = parameter.PulsNumPerP;
                            context.SpeedSamplingMDF.AddNew(context, speedSamplingMDF);

                            #endregion

                            #region 添加实时数据表

                            //添加测点后，直接在实时数据表中写入测点
                            MeasureSiteType siteType = cacheDICT.GetInstance()
                                .GetCacheType<MeasureSiteType>(p => p.ID == measureSite.MSiteName)
                                .FirstOrDefault();

                            RealTimeCollectInfo realTimeCollectInfo = new RealTimeCollectInfo();
                            realTimeCollectInfo.DevID = measureSite.DevID;
                            realTimeCollectInfo.MSID = measureSite.MSiteID;
                            realTimeCollectInfo.MSStatus = measureSite.MSiteStatus;
                            realTimeCollectInfo.MSDesInfo = siteType.Describe;
                            realTimeCollectInfo.MSDataStatus = measureSite.MSiteStatus;
                            realTimeCollectInfo.MSName = siteType.Name;
                            realTimeCollectInfo.AddDate = DateTime.Now;
                            context.RealTimeCollectInfo.AddNew(context, realTimeCollectInfo);

                            #endregion
                        }
                        return baseResponse;
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
            }
            return baseResponse;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：编辑转速测量位置
        /// </summary>
        /// <param name="parameter">编辑转速测量位置参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditSpeedMSiteForDeviceTree(EditSpeedMSiteForDeviceTreeParameter parameter)
        {
            BaseResponse<bool> baseResponse = new BaseResponse<bool>();
            baseResponse.IsSuccessful = true;

            //编辑转速测量位置时，设备id不正确
            if (parameter.DeviceID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }
            //编辑转速测量位置时，测点类型id不正确
            if (parameter.MSiteTypeId < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }
            //编辑转速测量位置时，传感器id不正确
            if (parameter.WSID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }
            //编辑转速测量位置时，关联测点id不正确
            if (parameter.RelationMSiteID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }

            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    var oldMSiteInfo = context.Measuresite.GetByKey(context, parameter.MeasureSiteID);
                    if (oldMSiteInfo != null)
                    {
                        #region 修改转速测量位置信息

                        if (oldMSiteInfo.WSID != parameter.WSID)
                        {
                            WS wsUseStatusObj = context.WS
                                .GetByKey<WS>(context, oldMSiteInfo.WSID);
                            if (wsUseStatusObj.UseStatus == 1)
                            {
                                wsUseStatusObj.UseStatus = 0;
                                context.WS.Update(context, wsUseStatusObj);
                            }
                        }
                        oldMSiteInfo.RelationMSiteID = parameter.RelationMSiteID;
                        oldMSiteInfo.WSID = parameter.WSID;
                        context.Measuresite.Update(context, oldMSiteInfo);

                        #endregion

                        #region 修改转速测量定义信息

                        var oldSpeedMDF = context.SpeedSamplingMDF.GetByKey(context, parameter.SpeedMDFID);
                        oldSpeedMDF.PulsNumPerP = parameter.PulsNumPerP;
                        context.SpeedSamplingMDF.Update(context, oldSpeedMDF);

                        #endregion

                        return baseResponse;
                    }
                    else
                    {
                        baseResponse.IsSuccessful = false;
                        baseResponse.Code = "000000";
                        return baseResponse;
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
            }
            return baseResponse;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：复制转速测量位置
        /// </summary>
        /// <param name="parameter">复制转速测量位置参数</param>
        /// <returns></returns>
        public BaseResponse<CopyAllSpeedMSResult> CopyAllSpeedMS(CopyAllSpeedMSParameter parameter)
        {
            BaseResponse<CopyAllSpeedMSResult> baseResponse = new BaseResponse<CopyAllSpeedMSResult>();
            CopyAllSpeedMSResult result = new CopyAllSpeedMSResult();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;

            //复制转速测量位置时，设备ID不对
            if (parameter.TargetDevId < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }

            try
            {
                return baseResponse;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
            }
            return baseResponse;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：通过设备ID读取设备上的采集单元
        /// </summary>
        /// <param name="parameter">通过设备ID读取设备上的采集单元参数</param>
        /// <returns></returns>
        public BaseResponse<GetDAUInfoByDevIDResult> GetDAUInfoByDevID(GetDAUInfoByDevIDParameter parameter)
        {
            BaseResponse<GetDAUInfoByDevIDResult> baseResponse = new BaseResponse<GetDAUInfoByDevIDResult>();
            GetDAUInfoByDevIDResult result = new GetDAUInfoByDevIDResult();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;

            //读取设备上的采集单元时，设备ID不对
            if (parameter.DevID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }

            try
            {
                //获取设备关系网关表数据
                var devRelaWGInfoList = deviceRelationWGRepository
                    .GetDatas<DeviceRelationWG>(item => item.DevId == parameter.DevID, false)
                    .Select(item => item.WGID)
                    .ToList();
                //获取网关表数据
                result.WGInfo = gatewayRepository
                    .GetDatas<Gateway>(item => devRelaWGInfoList.Contains(item.WGID)
                        && item.DevFormType == (int)EnumDevFormType.Wired, false)
                    .Select(item => new WGSimpleInfoResult
                    {
                        WGID = item.WGID,
                        WGName = item.WGName,
                        DevFormType = item.DevFormType,
                    })
                    .ToList();
                return baseResponse;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
            }
            return baseResponse;
        }

        #endregion

        #region 获取用户管理传感器下拉列表
        public BaseResponse<GetWSMaintainListResult> GetWSMaintainList(GetWSMaintainListParameter param)
        {
            BaseResponse<GetWSMaintainListResult> response = new BaseResponse<GetWSMaintainListResult>();
            GetWSMaintainListResult result = new GetWSMaintainListResult();

            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    //找到UserID关联的WS
                    IQueryable<WS> wsQuerable =
                        from wsr in dbContext.UserRelationWS
                        where wsr.UserID == param.UserID
                        join ws in dbContext.WS on wsr.WSID equals ws.WSID
                        select ws;

                    if (param.WGID != -1)
                    {
                        wsQuerable = wsQuerable.Where(t => t.WGID == param.WGID);
                    }

                    if (param.IsUsed != -1)
                    {
                        if (param.IsUsed == 1)//查询已经使用的WS
                        {
                            wsQuerable = wsQuerable.Where(t => t.UseStatus == 1);
                        }
                        else //查询未使用的WS
                        {
                            wsQuerable = wsQuerable.Where(t => t.UseStatus == 0);
                        }
                    }

                    var wireLessAndWiredWSInfoList = wsQuerable.ToArray();

                    #region 有线传感器

                    //有线传感器
                    var wireLessWSInfoList = wireLessAndWiredWSInfoList
                        .Where(t => string.IsNullOrWhiteSpace(t.MACADDR))
                        .Select(t =>
                        {
                            string wsName = string.Empty;
                            switch (t.DevFormType)
                            {
                                case (int)EnumWSFormType.WireLessSensor:
                                    wsName = "无线";
                                    break;
                                case (int)EnumWSFormType.WiredSensor:
                                    wsName = "有线";
                                    break;
                                case (int)EnumWSFormType.Triaxial:
                                    wsName = "三轴";
                                    break;
                            }
                            wsName = string.Format("{0}({1})", t.WSName, wsName);

                            return new WSInfoItem
                            {
                                WSID = t.WSID,
                                WSName = wsName,
                                WSFormType = t.DevFormType,
                            };
                        });
                    result.WSInfoList.AddRange(wireLessWSInfoList);

                    #endregion

                    #region 无线传感器

                    //无线传感器
                    var wiredWSInfoList = wireLessAndWiredWSInfoList
                        .Where(t => !string.IsNullOrWhiteSpace(t.MACADDR))
                        .DistinctBy(t => t.MACADDR)
                        .Select(t =>
                        {
                            string wsName = string.Empty;
                            switch (t.DevFormType)
                            {
                                case (int)EnumWSFormType.WireLessSensor:
                                    wsName = "无线";
                                    break;
                                case (int)EnumWSFormType.WiredSensor:
                                    wsName = "有线";
                                    break;
                                case (int)EnumWSFormType.Triaxial:
                                    wsName = "三轴";
                                    break;
                            }
                            wsName = string.Format("{0}({1})", t.WSName, wsName);

                            return new WSInfoItem
                            {
                                WSID = t.WSID,
                                WSName = wsName,
                                WSFormType = t.DevFormType,
                            };
                        });
                    result.WSInfoList.AddRange(wiredWSInfoList);

                    #endregion

                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "009541";

                return response;
            }
        }
        #endregion

        #region 获取采集单元信息

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-17
        /// 创建记录：获取采集单元信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<List<MTDauResult>> GetDauRelation(GetDauRelationParameter parameter)
        {
            try
            {
                BaseResponse<List<MTDauResult>> baseResponse = new BaseResponse<List<MTDauResult>>();
                baseResponse.IsSuccessful = true;
                List<MTDauResult> list = new List<MTDauResult>();
                List<Gateway> gatewayList;
                if (string.IsNullOrWhiteSpace(parameter.AgentHostIPAddress))
                {
                    baseResponse.IsSuccessful = false;
                    baseResponse.Code = "000000";
                    return baseResponse;
                }
                else
                {
                    gatewayList = gatewayRepository
                        .GetDatas<Gateway>(item => item.DevFormType == (int)EnumDevFormType.Wired
                            && item.AgentAddress != null
                            && item.AgentAddress.Contains(parameter.AgentHostIPAddress), false)
                        .ToList();
                }
                foreach (Gateway wgInfo in gatewayList)
                {
                    MTDauResult mtDauResult = new MTDauResult();
                    mtDauResult.DAUID = wgInfo.WGID;
                    mtDauResult.MonitorTreeID = wgInfo.MonitorTreeID.Value;
                    mtDauResult.DAUName = wgInfo.WGName;
                    mtDauResult.CurrentDAUStates = wgInfo.CurrentDAUStates;
                    mtDauResult.IPAddress = wgInfo.WGIP;
                    mtDauResult.Port = wgInfo.WGPort.Value;
                    list.Add(mtDauResult);
                }
                baseResponse.Result = list;
                return baseResponse;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                BaseResponse<List<MTDauResult>> baseResponse = new BaseResponse<List<MTDauResult>>();
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "000000";
                return baseResponse;
            }
        }

        #endregion
    }
    #endregion
}