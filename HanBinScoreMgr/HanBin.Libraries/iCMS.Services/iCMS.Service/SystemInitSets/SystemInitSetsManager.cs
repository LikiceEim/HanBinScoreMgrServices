/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 * 命名空间：iCMS.Presentation.Server.SystemManager
 * 文件名：  SystemManagerService
 * 创建人：  QXM
 * 创建时间：2016/10/28 10:10:19
 * 描述：服务表现层，响应调用方对用户、权限组、日志的操作请求
 *
 * 修改人：张辽阔
 * 修改时间：2016-11-14
 * 描述：增加错误编码
 *=====================================================================**/

using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Practices.Unity;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Response.SystemInitSets;
using iCMS.Common.Component.Data.Request.SystemInitSets;
using iCMS.Common.Component.Data.Response.Common;
using iCMS.Service.Common;
using iCMS.Frameworks.Core.Repository;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Service.Web.Utility;

namespace iCMS.Service.Web.SystemInitSets
{
    #region 系统配置
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemInitSetsManager : ISystemInitManager
    {
        #region 变量
        private readonly IRepository<VibSingal> vibSignalRepository;
        private readonly IRepository<Device> deviceRepository;
        private readonly IRepository<DeviceType> deviceTypeRepository;
        private readonly IRepository<MeasureSite> measureSiteRepository;
        private readonly IRepository<MonitorTree> monitorTreeRepository;
        private readonly IRepository<MonitorTreeProperty> monitorTreePropertyRepository;
        private readonly IRepository<MeasureSiteMonitorType> measureSiteMonitorTypeRepository;
        private readonly IRepository<Module> moduleRepository;
        private readonly IRepository<RoleModule> roleModuleRepository;
        private readonly IRepository<Config> configRepository;
        private readonly IRepository<MeasureSiteType> measureSiteTypeRepository;
        private readonly IRepository<EigenValueType> eigenValueTypeRepository;
        private readonly IRepository<WaveLengthValues> waveLengthValuesRepository;
        private readonly IRepository<WaveUpperLimitValues> waveUpperLimitValuesRepository;
        private readonly IRepository<WaveLowerLimitValues> waveLowerLimitValuesRepository;
        private readonly IRepository<VibratingSingalType> vibratingSingalTypeRepository;
        private readonly IRepository<SignalAlmSet> signalAlmSetRepository;
        private readonly IRepository<MonitorTreeType> monitorTreeTypeRepository;
        private readonly IRepository<SensorType> sensorTypeRepository;
        private readonly IRepository<WS> wsRepository;
        private readonly IRepository<WirelessGatewayType> wirelessGatewayTypeRepository;
        private readonly IRepository<Gateway> gatewayRepository;
        private readonly IRepository<ConnectStatusType> connectStatusTypeRepository;
        private readonly ICacheDICT cacheDICT;
        private readonly IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository;
        private readonly IRepository<TempeWSSetMSiteAlm> tempeWSSetMSiteAlmRepository;
        private readonly IRepository<VoltageSetMSiteAlm> voltageSetMSiteAlmRepository;

        [Dependency]
        public IInitializeServer initializeServer
        {
            get;
            set;
        }
        #endregion

        #region 构造函数
        public SystemInitSetsManager(IRepository<VibSingal> vibSignalRepository,
            IRepository<Device> deviceRepository,
            IRepository<DeviceType> deviceTypeRepository,
            IRepository<MeasureSite> measureSiteRepository,
            IRepository<MonitorTree> monitorTreeRepository,
            IRepository<MonitorTreeProperty> monitorTreePropertyRepository,
            IRepository<MeasureSiteMonitorType> measureSiteMonitorTypeRepository,
            IRepository<Module> moduleRepository,
            IRepository<RoleModule> roleModuleRepository,
            IRepository<Config> configRepository,
            IRepository<MeasureSiteType> measureSiteTypeRepository,
            IRepository<EigenValueType> eigenValueTypeRepository,
            IRepository<WaveLengthValues> waveLengthValuesRepository,
            IRepository<WaveUpperLimitValues> waveUpperLimitValuesRepository,
            IRepository<WaveLowerLimitValues> waveLowerLimitValuesRepository,
            IRepository<VibratingSingalType> vibratingSingalTypeRepository,
            IRepository<SignalAlmSet> signalAlmSetRepository,
            IRepository<MonitorTreeType> monitorTreeTypeRepository,
            IRepository<SensorType> sensorTypeRepository,
            IRepository<WS> wsRepository,
            IRepository<WirelessGatewayType> wirelessGatewayTypeRepository,
            IRepository<Gateway> gatewayRepository,
            IRepository<ConnectStatusType> connectStatusTypeRepository,
            ICacheDICT cacheDICT,
            IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository,
            IRepository<TempeWSSetMSiteAlm> tempeWSSetMSiteAlmRepository,
            IRepository<VoltageSetMSiteAlm> voltageSetMSiteAlmRepository)
        {
            this.vibSignalRepository = vibSignalRepository;
            this.deviceRepository = deviceRepository;
            this.deviceTypeRepository = deviceTypeRepository;
            this.measureSiteRepository = measureSiteRepository;
            this.monitorTreeRepository = monitorTreeRepository;
            this.monitorTreePropertyRepository = monitorTreePropertyRepository;
            this.measureSiteMonitorTypeRepository = measureSiteMonitorTypeRepository;
            this.moduleRepository = moduleRepository;
            this.roleModuleRepository = roleModuleRepository;
            this.configRepository = configRepository;
            this.measureSiteTypeRepository = measureSiteTypeRepository;
            this.eigenValueTypeRepository = eigenValueTypeRepository;
            this.waveLengthValuesRepository = waveLengthValuesRepository;
            this.waveUpperLimitValuesRepository = waveUpperLimitValuesRepository;
            this.waveLowerLimitValuesRepository = waveLowerLimitValuesRepository;
            this.vibratingSingalTypeRepository = vibratingSingalTypeRepository;
            this.signalAlmSetRepository = signalAlmSetRepository;
            this.monitorTreeTypeRepository = monitorTreeTypeRepository;
            this.sensorTypeRepository = sensorTypeRepository;
            this.wsRepository = wsRepository;
            this.wirelessGatewayTypeRepository = wirelessGatewayTypeRepository;
            this.gatewayRepository = gatewayRepository;
            this.connectStatusTypeRepository = connectStatusTypeRepository;
            this.cacheDICT = cacheDICT;
            this.tempeDeviceSetMSiteAlmRepository = tempeDeviceSetMSiteAlmRepository;
            this.tempeWSSetMSiteAlmRepository = tempeWSSetMSiteAlmRepository;
            this.voltageSetMSiteAlmRepository = voltageSetMSiteAlmRepository;
        }
        #endregion

        #region 获取功能信息接口
        public BaseResponse<ModuleResult> GetModuleData(ModuleDataParameter param)
        {
            BaseResponse<ModuleResult> response = null;
            ModuleResult result = new ModuleResult();
            List<Module> modules = new List<Module>();
            List<ModuleInfo> moduleInfoList = new List<ModuleInfo>();
            try
            {
                if (param.ParID == -1) // 当 PID == -1 时，只返回可用的模块
                {
                    modules = moduleRepository.GetDatas<Module>(t => t.IsUsed == 1, false).ToList();
                }
                else
                {
                    modules = moduleRepository.GetDatas<Module>(p => p.ParID == param.ParID, false).ToList();
                }

                var linq = from m in modules
                           select new ModuleInfo
                           {
                               ModuleID = m.ModuleID,
                               ModuleName = m.ModuleName,
                               ParID = m.ParID,
                               OID = m.OID,
                               IsUsed = m.IsUsed,
                               IsDefault = m.IsDeault,
                               Code = m.Code,
                               AddDate = m.AddDate.ToString(),
                               IsExistChild = (from mm in new iCMSDbContext().Module
                                               where mm.ParID == m.ModuleID
                                               select mm).Any(),
                               RelationTableName = m.CommonDataType,
                               RelationCode = m.CommonDataCode,
                               Describe = m.Describe
                           };
                moduleInfoList.AddRange(linq.ToList());
                result.ModuleInfo = moduleInfoList;

                response = new BaseResponse<ModuleResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<ModuleResult>("002781");
                return response;
            }
        }
        #endregion

        #region 添加模块信息接口
        public BaseResponse<bool> AddModuleData(AddModuleDataParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                Module module = new Module();
                module.ParID = param.ParID;
                module.OID = param.OID;
                module.ModuleName = param.ModuleName;
                module.Code = param.Code;
                module.IsUsed = param.IsUsed;
                module.IsDeault = param.IsDefault;
                module.CommonDataType = param.RelationTableName;
                module.CommonDataCode = param.RelationCode;
                module.Describe = param.Describe;
                OperationResult operationResult = moduleRepository.AddNew<Module>(module);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    #region 重新初始化变量 王颖辉 2017-11-11
                    string code = ConfigHelper.GetValueFromConfig("DevStartStopFunc");
                    if (param.Code == "DevStartStopFunc")
                    {
                        initializeServer.InitializeEnvironmentVariables();
                    }
                    #endregion

                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("002791");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("002791");
                return response;
            }
        }
        #endregion

        #region 编辑模块信息接口
        public BaseResponse<bool> EditModuleData(EditModuleDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                var moduleInDb = moduleRepository.GetByKey(param.ModuleID);

                //判断父节点是否启用
                if (param.IsUsed == 1 && moduleInDb.ParID != 0)
                {
                    var IsUsed = moduleRepository.GetDatas<Module>(p => p.ModuleID == moduleInDb.ParID, false).Select(s => s.IsUsed).FirstOrDefault();
                    if (IsUsed == 0)
                    {
                        response = new BaseResponse<bool>("009862");
                        return response;
                    }
                }

                int tempIsDefalut = moduleInDb.IsDeault;
                int tempIsUsed = moduleInDb.IsUsed;
                moduleInDb.OID = param.OID;
                moduleInDb.ParID = param.ParID;
                moduleInDb.ModuleName = param.ModuleName;
                moduleInDb.Code = param.Code;
                moduleInDb.IsDeault = param.IsDefault;
                moduleInDb.IsUsed = param.IsUsed;
                moduleInDb.CommonDataType = param.RelationTableName;
                moduleInDb.CommonDataCode = param.RelationCode;
                moduleInDb.Describe = param.Describe;
                OperationResult operResu = moduleRepository.Update<Module>(moduleInDb);
                if (operResu.ResultType == EnumOperationResultType.Success)
                {

                    #region 修改所有子节点状态
                    if (tempIsUsed != param.IsUsed)
                    {
                        //遍历所有子节点状态
                        SetChildModuleIsUseStatus(moduleInDb.ModuleID, moduleInDb.IsUsed);
                    }

                    //如果是必选，则修改子节点状态
                    if (tempIsDefalut != param.IsDefault)
                    {
                        SetChildModuleIsDefault(moduleInDb.ModuleID, moduleInDb.IsDeault);
                    }

                    //功能禁用，关联角色权限
                    if (tempIsUsed == 1 && param.IsUsed == 0)
                    {
                        List<string> childrenCodes = new List<string>();
                        GetAllChildrenCode(param.ModuleID, childrenCodes);
                        roleModuleRepository.Delete(p => childrenCodes.Contains(p.ModuleCode));
                    }

                    #endregion

                    #region 修改所有父节点状态

                    if (tempIsUsed != param.IsUsed)
                    {
                        SetParentModuleIsUseStatus(moduleInDb.ParID);
                    }

                    if (tempIsDefalut != param.IsDefault)
                    {
                        SetParentModuleIsDefault(moduleInDb.ParID);
                    }

                    #endregion

                    #region 重新初始化变量 王颖辉 2017-11-11
                    string code = ConfigHelper.GetValueFromConfig("DevStartStopFunc");
                    if (param.Code == "DevStartStopFunc")
                    {
                        initializeServer.InitializeEnvironmentVariables();
                    }
                    #endregion

                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("002801");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("002801");
                return response;
            }
        }
        #endregion

        #region 删除模块信息
        public BaseResponse<bool> DeleteModuleData(DeleteModuleDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultModule(param.ModuleID))
                {
                    response = new BaseResponse<bool>("002812");
                    return response;
                }

                #region 删除父节点，同步删除所有子节点
                List<int> deletedIDs = new List<int>();
                GetAllChildren(param.ModuleID, deletedIDs);

                //删除时，是否存在启停机开关 王颖辉 2017-11-11
                var isExist = moduleRepository.GetDatas<Module>(item => deletedIDs.Contains(item.ModuleID) && item.Code == "DevStartStopFunc", true).Count() > 0;

                OperationResult operationResult = moduleRepository.Delete(p => deletedIDs.Contains(p.ModuleID));
                #endregion

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {

                    #region 重新初始化变量 王颖辉 2017-11-11
                    if (isExist)
                    {
                        initializeServer.InitializeEnvironmentVariables();
                    }
                    #endregion

                    //关联角色权限
                    List<string> deletedCodes = new List<string>();
                    deletedCodes.AddRange(moduleRepository.GetDatas<Module>(item => deletedIDs.Contains(item.ModuleID), true).
                        Select(s => s.Code).ToList());
                    roleModuleRepository.Delete(p => deletedCodes.Contains(p.ModuleCode));

                    response = new BaseResponse<bool>();
                    return response; ;
                }
                else
                {
                    response = new BaseResponse<bool>("002821");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("002821");
                return response;
            }
        }
        #endregion

        #region 获取二级及以上模型，通过父节点ID
        public BaseResponse<ModuleResult> GetSecondLevelModuleByParentId(SecondLevelModuleByParentIdParameter param)
        {
            //张辽阔 2016-11-08 修改
            ModuleResult result = new ModuleResult();
            BaseResponse<ModuleResult> response = null;
            List<Module> modules = null;
            List<ModuleInfo> moduleInfos = new List<ModuleInfo>();
            try
            {
                modules = moduleRepository.GetDatas<Module>(t => true, false).ToList();
                var rootModuleList = modules.Where(item => item.ParID == param.ParID).ToList<Module>();
                if (rootModuleList != null && rootModuleList.Count() > 0)
                {
                    //获取一级
                    foreach (var info in rootModuleList)
                    {
                        ModuleInfo module = new ModuleInfo
                        {
                            ModuleID = info.ModuleID,
                            ModuleName = info.ModuleName,
                            ParID = info.ParID,
                            OID = info.OID,
                            IsUsed = info.IsUsed,
                            IsDefault = info.IsDeault,
                            Code = info.Code,
                            AddDate = info.AddDate.ToString()
                        };
                        moduleInfos.Add(module);

                        //查找二级节点
                        var childModuleList = modules.Where(item => item.ParID == info.ModuleID);
                        if (childModuleList != null && childModuleList.Count() > 0)
                        {
                            foreach (var child in childModuleList)
                            {
                                ModuleInfo childModule = new ModuleInfo
                                {
                                    ModuleID = child.ModuleID,
                                    ModuleName = child.ModuleName,
                                    ParID = child.ParID,
                                    OID = child.OID,
                                    IsUsed = child.IsUsed,
                                    IsDefault = child.IsDeault,
                                    Code = child.Code,
                                    AddDate = child.AddDate.ToString()
                                };
                                moduleInfos.Add(childModule);
                            }
                        }
                    }
                }

                result.ModuleInfo = moduleInfos;
                response = new BaseResponse<ModuleResult>(true, string.Empty, string.Empty, result);
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<ModuleResult>(false, string.Empty, "002831", result);
                return response;
            }
        }
        #endregion

        #region 通用配置查看，通过父ID
        public BaseResponse<ConfigResult> GetConfigByParentID(ConfigByParentIDParameter param)
        {
            ConfigResult result = new ConfigResult();
            BaseResponse<ConfigResult> response = null;
            List<Config> config = new List<Config>();
            List<ConfigData> configList = new List<ConfigData>();
            try
            {
                //返回全部
                if (param.ParentID == -1)
                {
                    config = configRepository.GetDatas<Config>(item => item.IsUsed == 1, false).ToList();
                }
                else
                {
                    config = configRepository.GetDatas<Config>(item => item.ParentId == param.ParentID, false).ToList();
                }
                var linq = from t in config
                           select new ConfigData
                           {
                               ID = t.ID,
                               Code = t.Code,
                               Name = t.CommonDataType.HasValue ?
                                        GetComSetNameByCode(t.ParentId, t.CommonDataType.Value, t.CommonDataCode) :
                                        t.Name,
                               Describe = t.Describe,
                               IsDefault = t.IsDefault,
                               IsUsed = t.IsUsed,
                               ParentId = t.ParentId,
                               Value = t.Value,
                               OrderNo = t.OrderNo,
                               CommonDataType = t.CommonDataType,
                               CommonDataCode = t.CommonDataCode,
                               IsExistChild = (configRepository.GetDatas<Config>(tt => tt.ParentId == t.ID, true)).Any()
                           };
                configList.AddRange(linq.ToList());
                result.ConfigList = configList;
                response = new BaseResponse<ConfigResult>();
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<ConfigResult>(false, string.Empty, "002841", null);
                return response;
            }
        }

        private string GetComSetNameByCode(int ParentId, int CommonDataType, string CommonDataCode)
        {
            string strComSetName = string.Empty;
            switch (CommonDataType)
            {
                case 1:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<MonitorTreeType>(t => t.Code == CommonDataCode).
                        Select(s => s.Name).FirstOrDefault();
                    break;
                case 2:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<DeviceType>(t => t.Code == CommonDataCode).
                        Select(s => s.Name).FirstOrDefault();
                    break;
                case 3:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<MeasureSiteType>(t => t.Code == CommonDataCode).
                        Select(s => s.Name).FirstOrDefault();
                    break;
                case 4:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<MeasureSiteMonitorType>(t => t.Code == CommonDataCode).
                        Select(s => s.Name).FirstOrDefault();
                    break;
                case 5:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<VibratingSingalType>(t => t.Code == CommonDataCode).
                        Select(s => s.Name).FirstOrDefault();
                    break;
                case 6:
                    EigenValueType eigenValueType = cacheDICT.GetInstance().GetCacheType<EigenValueType>(t => t.Code == CommonDataCode).
                            FirstOrDefault();
                    if (null != eigenValueType)
                    {
                        string vibratingSingalTypeName = vibratingSingalTypeRepository.GetByKey(eigenValueType.VibratingSignalTypeID).Name;
                        string parentConfigCode = configRepository.GetByKey(ParentId).Code;
                        if (parentConfigCode == "DeviceImageDisplayConfiguration")
                        {
                            strComSetName = vibratingSingalTypeName + "-" + eigenValueType.Name;
                        }
                        else if (parentConfigCode == "CONFIG_55_LSSJXSXG")
                        {
                            strComSetName = vibratingSingalTypeName + eigenValueType.Name;
                        }
                    }
                    break;
                case 7:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<WaveLengthValues>(t => t.Code == CommonDataCode).
                       Select(s => s.WaveLengthValue).FirstOrDefault().ToString();
                    break;
                case 8:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<WaveUpperLimitValues>(t => t.Code == CommonDataCode).
                       Select(s => s.WaveUpperLimitValue).FirstOrDefault().ToString();
                    break;
                case 9:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<WaveLowerLimitValues>(t => t.Code == CommonDataCode).
                       Select(s => s.WaveLowerLimitValue).FirstOrDefault().ToString();
                    break;
                case 10:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<SensorType>(t => t.Code == CommonDataCode).
                       Select(s => s.Name).FirstOrDefault().ToString();
                    break;
                case 11:
                    strComSetName = cacheDICT.GetInstance().GetCacheType<WirelessGatewayType>(t => t.Code == CommonDataCode).
                       Select(s => s.Name).FirstOrDefault().ToString();
                    break;
                default:
                    break;
            }

            return strComSetName;
        }

        #endregion

        #region Add Config
        public BaseResponse<bool> AddConfig(AddConfigParameter param)
        {
            BaseResponse<bool> response = null;

            #region 验证数据
            if (string.IsNullOrEmpty(param.Name))
            {
                response = new BaseResponse<bool>(false, null, "002852", false);
                return response;
            }
            #endregion

            Config configInfo = new Config();
            configInfo.Name = param.Name;
            configInfo.Describe = param.Describe;
            configInfo.Value = param.Value;
            configInfo.IsUsed = param.IsUsed;
            configInfo.IsDefault = param.IsDefault;
            configInfo.ParentId = param.ParentID;
            configInfo.Code = Guid.NewGuid().ToString();
            configInfo.CommonDataType = param.CommonDataType;
            configInfo.CommonDataCode = param.CommonDataCode;

            try
            {
                OperationResult operationResult = configRepository.AddNew<Config>(configInfo);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {

                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<Config>();
                    response = new BaseResponse<bool>(true, null, null, true);
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>(false, null, "002862", true);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<bool>(false, null, "002871", true);
                return response;
            }
        }
        #endregion

        #region Edit Config
        /// <summary>
        /// 可用状态发生改变，可用，子节点全部可用，对应父节点可用；不可用子节点全部不可用，父节点不变
        /// TODO:
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditConfig(EditConfigParameter param)
        {
            BaseResponse<bool> response = null;
            var configInDB = configRepository.GetByKey(param.ID);
            if (configInDB == null)
            {
                response = new BaseResponse<bool>(false, null, "002901", true);
                return response;
            }
            #region 验证数据
            if (string.IsNullOrEmpty(param.Name))
            {
                response = new BaseResponse<bool>(false, null, "002882", true);
                return response;
            }
            #endregion

            try
            {
                string rootName = EnumHelper.GetDescription(EnumConfig.HistoryDataShow);
                #region 数据验证，历史记录配置父节点，子节点配置不能都为 0
                var devHistoryConfigID = configRepository.GetDatas<Config>(t => t.Name.Trim().ToLower().Equals(rootName.Trim().ToLower()), true).Single().ID;
                if (configInDB.ParentId == devHistoryConfigID)
                {
                    int countZero = configRepository
                        .GetDatas<Config>(t => t.ParentId == devHistoryConfigID
                            && t.ID != configInDB.ID
                            && t.IsUsed == 0, true)
                        .ToList().Count;
                    int countAll = configRepository
                        .GetDatas<Config>(t => t.ParentId == devHistoryConfigID, true)
                        .ToList().Count;
                    if (param.IsUsed == 0)
                    {
                        if (countZero == (countAll - 1))
                        {
                            response = new BaseResponse<bool>("002892");
                            return response;
                        }
                    }
                }
                #endregion

                #region Update logic
                configInDB.Name = param.Name;
                configInDB.Describe = param.Describe;
                configInDB.Value = param.Value;
                configInDB.IsUsed = param.IsUsed;
                configInDB.IsDefault = param.IsDefault;
                configInDB.ParentId = param.ParentID;
                configInDB.CommonDataType = param.CommonDataType;
                configInDB.CommonDataCode = param.CommonDataCode;
                #endregion

                OperationResult operationResult = configRepository.Update<Config>(configInDB);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    #region 编辑后 重新定义 View_DevHistoryData 视图
                    if (configInDB.ParentId == 55)
                    {
                        string deviceTemp = string.Empty;
                        string lq = string.Empty;
                        string vel = string.Empty;
                        string acc = string.Empty;
                        string disp = string.Empty;
                        string envl = string.Empty;

                        List<Config> hisConfigs = configRepository.GetDatas<Config>(t => t.ParentId == 55, true).ToList();
                        foreach (Config tempConfig in hisConfigs)
                        {
                            switch (tempConfig.Name)
                            {
                                case "速度有效值":
                                    vel = tempConfig.IsUsed.ToString();
                                    break;
                                case "加速度峰值":
                                    acc = tempConfig.IsUsed.ToString();
                                    break;
                                case "包络峰值":
                                    envl = tempConfig.IsUsed.ToString();
                                    break;
                                case "位移峰峰值":
                                    disp = tempConfig.IsUsed.ToString();
                                    break;
                                case "设备状态轴承状态":
                                    lq = tempConfig.IsUsed.ToString();
                                    break;
                                case "设备温度":
                                    deviceTemp = tempConfig.IsUsed.ToString();
                                    break;
                            }
                        }

                        string SQL_ModifyHistoryView = string.Format("exec SP_ModifyHistoryView {0},{1},{2},{3},{4},{5}", deviceTemp, lq, vel, acc, disp, envl);
                        var intRes = new iCMSDbContext().Database.ExecuteSqlCommand(SQL_ModifyHistoryView);
                    }
                    #endregion

                    #region 设置子节点
                    //设置子节点
                    SetChildConfigIsUseStatus(configInDB.ID, configInDB.IsUsed);
                    #endregion

                    #region 设置父节点
                    //设置子节点
                    SetParentConfigIsUseStatus(configInDB.ParentId);

                    #endregion

                    #region 重新初始化变量 王颖辉 2017-11-11
                    //趋势报警和三次确认报警
                    if (configInDB.Code == "CONFIG_49_BJFSXZ" || configInDB.Code == "CONFIG_50_QSBJKG")
                    {
                        initializeServer.InitializeEnvironmentVariables();
                    }
                    #endregion

                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<Config>();

                    response = new BaseResponse<bool>(true, null, null, true);
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>(false, null, "002912", true);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<bool>(false, null, "002921", true);
                return response;
            }
        }
        #endregion

        #region Edit Config
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间:2017-10-21
        /// 创建内容:批量编辑
        /// </summary>
        /// <param name="parameter"参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditConfigList(EditConfigListParameter parameter)
        {
            BaseResponse<bool> response = null;

            if (parameter.ConfigList == null && !parameter.ConfigList.Any())
            {
                response = new BaseResponse<bool>(false, null, "009332", true);
                return response;
            }
            foreach (var info in parameter.ConfigList)
            {
                var configInDB = configRepository.GetByKey(info.ID);
                if (configInDB == null)
                {
                    response = new BaseResponse<bool>(false, null, "002901", true);
                    return response;
                }
            }

            try
            {
                List<Config> configList = new List<Config>();
                foreach (var config in parameter.ConfigList)
                {
                    var info = configRepository.GetByKey(config.ID);
                    info.Value = config.Value;
                    configList.Add(info);
                }
                OperationResult operationResult = configRepository.Update<Config>(configList);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {

                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<Config>();

                    response = new BaseResponse<bool>(true, null, null, true);
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>(false, null, "002912", true);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<bool>(false, null, "002921", true);
                return response;
            }
        }
        #endregion

        #region Delete Config
        public BaseResponse<bool> DeleteConfig(DeleteConfigParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                List<int> allConfigIds = new List<int>();
                GetAllConfigChildren(param.ID, allConfigIds);

                //系统默认参数不允许删除
                if (allConfigIds.Count > 0)
                {
                    if (configRepository.GetDatas<Config>(m => allConfigIds.Contains(m.ID) && m.IsDefault == 1, false).Any())
                    {
                        response = new BaseResponse<bool>(false, null, "002362", true);
                        return response;
                    }
                }

                OperationResult operationResult = configRepository.Delete(t => allConfigIds.Contains(t.ID));
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {

                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<Config>();
                    response = new BaseResponse<bool>(true, null, null, true);
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>(false, null, "002932", true);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                //result.Reason = "#209010045001";
                response = new BaseResponse<bool>(false, null, "002941", false);
                return response;
            }
        }

        /// <summary>
        /// 获取Config ID以及其所有的级联子节点，用作级联删除
        /// </summary>
        /// <param name="configID"></param>
        /// <param name="childrenIDs"></param>
        private void GetAllConfigChildren(int configID, List<int> childrenIDs)
        {
            childrenIDs.Add(configID);
            var childNodes = configRepository
                .GetDatas<Config>(m => m.ParentId == configID, false)
                .ToList();

            foreach (var mt in childNodes)
            {
                //找到包含当前节点及所有子节点
                GetAllChildren(mt.ID, childrenIDs);
            }
        }
        #endregion

        #region 相同节点下是否有重名，通过父ID和名称
        /// <summary>
        /// response.IsSuccessful 参数代表请求服务是否成功，Result的值代表是否有重名
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<ExistConfigNameResult> IsExistConfigNameByNameAndParnetId(ExistConfigNameByNameAndParnetIdParameter param)
        {
            BaseResponse<ExistConfigNameResult> response = new BaseResponse<ExistConfigNameResult>();
            ExistConfigNameResult result = new ExistConfigNameResult();
            result.IsExisted = false;
            #region 验证数据
            if (string.IsNullOrEmpty(param.Name))
            {
                //result.Reason = "#209010082";
                response = new BaseResponse<ExistConfigNameResult>(false, null, "002952", result);
                return response;
            }
            #endregion
            try
            {
                bool isExisted = configRepository.GetDatas<Config>(item => item.Name == param.Name && item.ParentId == param.ParentID, false).Any();
                result.IsExisted = isExisted;
                response = new BaseResponse<ExistConfigNameResult>(true, null, null, result);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                //result.Reason = "#209010075001";
                response = new BaseResponse<ExistConfigNameResult>(false, null, "002961", result);
                return response;
            }
        }
        #endregion

        #region 相同节点下是否有重名，通过id和名称
        public BaseResponse<ExistConfigNameResult> IsExistConfigNameByIDAndName(ExistConfigNameByIDAndNameParameter param)
        {
            BaseResponse<ExistConfigNameResult> response = new BaseResponse<ExistConfigNameResult>();
            ExistConfigNameResult result = new ExistConfigNameResult();
            result.IsExisted = false;
            #region 验证数据
            if (string.IsNullOrEmpty(param.Name))
            {
                //result.Reason = "#209010092";
                response = new BaseResponse<ExistConfigNameResult>(false, null, "002972", result);
                return response;
            }
            #endregion
            try
            {
                Config currentConfig = configRepository.GetDatas<Config>(item => item.ID == param.ID, false).FirstOrDefault();
                if (currentConfig != null)
                {
                    bool isExisted = configRepository
                        .GetDatas<Config>(item => item.Name == param.Name
                            && item.ParentId == currentConfig.ParentId
                            && item.ID != currentConfig.ID, false)
                        .Any();
                    result.IsExisted = isExisted;
                    response = new BaseResponse<ExistConfigNameResult>(true, null, null, result);
                    return response;
                }
                else
                {
                    result.IsExisted = false;
                    response = new BaseResponse<ExistConfigNameResult>(true, null, null, result);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                //result.Reason = "#209010095001";
                response = new BaseResponse<ExistConfigNameResult>(false, null, "002981", result);
                return response;
            }
        }
        #endregion

        #region 通过父Name 和 Name获取系统配置信息，与通过Name获取Config合并
        public BaseResponse<ConfigResult> GetConfigByName(ConfigByNameParameter param)
        {
            ConfigResult result = new ConfigResult();
            BaseResponse<ConfigResult> response = null;
            List<ConfigData> configList = new List<ConfigData>();
            if (!string.IsNullOrEmpty(param.RootName) && !string.IsNullOrEmpty(param.Name))
            {
                try
                {
                    var pid = 0;
                    var root = configRepository.GetDatas<Config>(t => t.Name.Equals(param.RootName), true).FirstOrDefault();
                    if (root == null)
                    {
                        response = new BaseResponse<ConfigResult>(false, null, "002992", null);
                        return response;
                    }

                    pid = root.ID;
                    Config config = configRepository.GetDatas<Config>(item => item.ParentId == pid && item.Name == param.Name, false).FirstOrDefault();

                    if (config == null)
                    {
                        response = new BaseResponse<ConfigResult>(false, null, "002992", null);
                        return response;
                    }

                    var isExistChild = configRepository.GetDatas<Config>(t => t.ParentId == config.ID, true).Any();
                    ConfigData configData = new ConfigData
                    {
                        ID = config.ID,
                        Name = config.Name,
                        Value = config.Value,
                        Describe = config.Describe,
                        IsUsed = config.IsUsed,
                        IsDefault = config.IsDefault,
                        IsExistChild = isExistChild
                    };
                    configList.Add(configData);
                    result.ConfigList = configList;
                    response = new BaseResponse<ConfigResult>(true, null, null, result);
                    return response;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                    response = new BaseResponse<ConfigResult>(false, null, "003001", null);
                    return response;
                }
            }
            else if (!string.IsNullOrEmpty(param.RootName) && string.IsNullOrEmpty(param.Name))
            {
                try
                {
                    var root = configRepository.GetDatas<Config>(item => item.Name == param.RootName, false).FirstOrDefault();
                    if (root != null)
                    {
                        var pid = root.ID;
                        configList = configRepository.GetDatas<Config>(item => item.ParentId == pid, false).Select(t => new ConfigData
                        {
                            ID = t.ID,
                            Name = t.Name,
                            Describe = t.Describe,
                            IsDefault = t.IsDefault,
                            IsUsed = t.IsUsed,
                            ParentId = t.ParentId,
                            Value = t.Value

                        }).ToList();
                        foreach (ConfigData configData in configList)
                        {
                            var isExistChild = configRepository.GetDatas<Config>(item => item.ParentId == configData.ID, false).Any();
                            configData.IsExistChild = isExistChild;
                        }
                    }

                    result.ConfigList = configList;
                    response = new BaseResponse<ConfigResult>();
                    response.Result = result;
                    return response;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                    response = new BaseResponse<ConfigResult>("003011");
                    return response;
                }
            }
            else
            {
                response = new BaseResponse<ConfigResult>("003011");
                return response;
            }
        }
        #endregion

        #region 通过ID获取系统参数  Added by QXM, 2017/06/19
        public BaseResponse<ConfigResult> GetConfigByID(GetConfigByIDParam param)
        {
            ConfigResult result = new ConfigResult();
            BaseResponse<ConfigResult> response = null;
            List<ConfigData> configList = new List<ConfigData>();

            try
            {
                configList = configRepository.GetDatas<Config>(item => item.ParentId == param.ParentID, false).Select(t => new ConfigData
                {
                    ID = t.ID,
                    Name = t.Name,
                    Describe = t.Describe,
                    IsDefault = t.IsDefault,
                    IsUsed = t.IsUsed,
                    ParentId = t.ParentId,
                    Value = t.Value

                }).ToList();
                foreach (ConfigData configData in configList)
                {
                    var isExistChild = configRepository.GetDatas<Config>(item => item.ParentId == configData.ID, false).Any();
                    configData.IsExistChild = isExistChild;
                }
                if (param.ID.HasValue)
                {
                    configList = configList.Where(t => t.ID == param.ID.Value).ToList();
                }

                result.ConfigList = configList;
                response = new BaseResponse<ConfigResult>(true, null, null, result);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<ConfigResult>("003011");
                return response;
            }
        }

        #endregion

        #region 判断系统中是否已存在监测树
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-12-14
        /// 创建记录：判断系统中是否已存在监测树
        /// </summary>
        /// <returns></returns>
        public BaseResponse<ExistMonitorTreeResult> IsExistMonitorTree()
        {
            BaseResponse<ExistMonitorTreeResult> response = new BaseResponse<ExistMonitorTreeResult>();
            ExistMonitorTreeResult result = new ExistMonitorTreeResult();

            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    result.IsExisted = (from mt in dbContext.MonitorTree
                                        select mt).Any();
                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response = new BaseResponse<ExistMonitorTreeResult>("010041");
                return response;
            }
        }

        #endregion

        #region 判断监测树节点下是否存在设备
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-12-13
        /// 创建记录：判断监测树节点下是否存在设备
        /// </summary>
        /// <returns></returns>
        public BaseResponse<ExistDeviceInMonitorTreeResult> IsExistDeviceInMonitorTree()
        {
            BaseResponse<ExistDeviceInMonitorTreeResult> response = new BaseResponse<ExistDeviceInMonitorTreeResult>();
            ExistDeviceInMonitorTreeResult result = new ExistDeviceInMonitorTreeResult();

            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    var mtIDList = (from mt in dbContext.MonitorTree select mt.MonitorTreeID).Distinct().ToList();
                    result.IsExisted = (from d in dbContext.Device
                                        where mtIDList.Contains(d.MonitorTreeID)
                                        select d).Any();
                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response = new BaseResponse<ExistDeviceInMonitorTreeResult>("010041");
                return response;
            }
        }

        #endregion

        #region 通用数据监测树类型
        #region 获取通用数据监测树类型接口
        public BaseResponse<CommonDataResult> GetMonitorTreeTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            CommonDataResult result = new CommonDataResult();
            List<CommonInfo> commonInfoList = new List<CommonInfo>();
            try
            {
                List<MonitorTreeType> monitorTreeList = cacheDICT.GetInstance().GetCacheType<MonitorTreeType>().ToList();
                foreach (var monitorTree in monitorTreeList)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = monitorTree.ID,
                        PID = null,
                        Code = monitorTree.Code,
                        Name = monitorTree.Name,
                        Describe = monitorTree.Describe,
                        IsUsable = monitorTree.IsUsable,
                        IsDefault = monitorTree.IsDefault,
                        AddDate = monitorTree.AddDate.ToString(),
                        OrderNo = monitorTree.OrderNo
                    };
                    commonInfoList.Add(commonInfo);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                //Reason = "#204070035001"
                response = new BaseResponse<CommonDataResult>(false, null, "003021", null);
                return response;
            }
        }
        #endregion

        #region 获取通用数据监测树类型接口
        public BaseResponse<CommonDataResult> GetMonitorTreeTypeDataForCommon1(MonitorTreeTypeParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            CommonDataResult result = new CommonDataResult();
            List<CommonInfo> commonInfoList = new List<CommonInfo>();
            try
            {
                List<MonitorTreeType> monitorTreeList = cacheDICT.GetInstance().GetCacheType<MonitorTreeType>().ToList();
                foreach (var monitorTree in monitorTreeList)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = monitorTree.ID,
                        PID = null,
                        Name = monitorTree.Name,
                        Describe = monitorTree.Describe,
                        IsUsable = monitorTree.IsUsable,
                        IsDefault = monitorTree.IsDefault,
                        AddDate = monitorTree.AddDate.ToString()
                    };

                    commonInfoList.Add(commonInfo);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                //Reason = "#204070035001"
                response = new BaseResponse<CommonDataResult>(false, null, "003021", null);
                return response;
            }
        }
        #endregion

        #region 新增监测树类型
        public BaseResponse<bool> AddMonitorTreeTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            string message = string.Empty;
            try
            {
                if (!IsMonitorTreeTypeNameAvailable(param.Name, out message, null))
                {
                    response = new BaseResponse<bool>(false, null, message, false);
                    return response;
                }

                MonitorTreeType mtType = new MonitorTreeType();
                mtType.Code = "MT_" + Guid.NewGuid().ToString();
                mtType.Name = param.Name;
                mtType.Describe = param.Describe;
                mtType.IsDefault = param.IsDefault;
                mtType.IsUsable = param.IsUsable;
                int? orderNo = new iCMSDbContext().MonitorTreeType.Select(e => e.OrderNo).Max();
                if (orderNo.HasValue)
                    mtType.OrderNo = orderNo.Value + 1;
                else
                    mtType.OrderNo = 1;
                OperationResult operationResult = monitorTreeTypeRepository.AddNew<MonitorTreeType>(mtType);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<MonitorTreeType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    //reason = new DicOperationResult { Reason = "#204070015001" };
                    response = new BaseResponse<bool>(false, null, "003031", false);
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>(false, null, "003031", false);
                return response;
            }
        }
        #endregion

        #region 编辑监测树类型
        public BaseResponse<bool> EditMonitorTreeTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            string message = string.Empty;
            try
            {
                if (!IsMonitorTreeTypeNameAvailable(param.Name, out message, param.ID))
                {
                    response = new BaseResponse<bool>(false, null, message, false);
                    return response;
                }
                var monitorTreeType = cacheDICT.GetInstance().GetCacheType<MonitorTreeType>(p => p.ID == param.ID).SingleOrDefault();
                if (monitorTreeType == null)
                {
                    response = new BaseResponse<bool>(false, null, "003042", false);
                    return response;
                }

                #region 系统中添加设备后，不允许修改监测树类型 王龙杰 2017-10-09
                if (deviceRepository.GetDatas<Device>(t => 1 == 1, true).Any() && param.IsUsable != monitorTreeType.IsUsable)
                {
                    response = new BaseResponse<bool>("008682");
                    return response;
                }
                #endregion

                #region 如果类型被使用，则不能停用 王颖辉 2017-02-20
                //如果监测树类型已经使用，则不能停用 0为停用
                if (param.IsUsable == 0)
                {
                    //获取监测树是否被使用
                    int count = monitorTreeRepository.GetDatas<MonitorTree>(item => item.Type == param.ID, true).Count();

                    //已经被使用
                    if (count > 0)
                    {
                        response = new BaseResponse<bool>(false, null, "007922", false);
                        return response;
                    }
                }
                #endregion

                #region 监测树类型不能全部禁用，移植iPVP 王龙杰 2017-10-09
                var elseMonitorTreeTypeUsed = monitorTreeTypeRepository.GetDatas<MonitorTreeType>(t =>
                                    t.IsUsable == 1 && t.ID != monitorTreeType.ID, true).Any();
                if (!elseMonitorTreeTypeUsed && param.IsUsable == 0)
                {
                    //监测树类型不能全部禁用
                    response = new BaseResponse<bool>("009902");
                    return response;
                }
                #endregion

                monitorTreeType.Name = param.Name;
                monitorTreeType.Describe = param.Describe;
                monitorTreeType.IsUsable = param.IsUsable;
                monitorTreeType.IsDefault = param.IsDefault;
                OperationResult operationResult = monitorTreeTypeRepository.Update<MonitorTreeType>(monitorTreeType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<MonitorTreeType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>(false, null, "003051", false);
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>(false, null, "003051", false);
                return response;
            }
        }
        #endregion

        #region 删除监测树类型
        public BaseResponse<bool> DeleteMonitorTreeTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                #region 系统中添加设备后，不允许修改监测树类型 王龙杰 2017-10-09
                if (deviceRepository.GetDatas<Device>(t => 1 == 1, true).Any())
                {
                    response = new BaseResponse<bool>("008682");
                    return response;
                }
                #endregion
                if (IsDefaultMonitorTreeType(param.ID))
                {
                    response = new BaseResponse<bool>(false, null, "003062", false);
                    return response;
                }
                if (IsExistMonitorTree(param.ID))
                {
                    response = new BaseResponse<bool>(false, null, "003072", false);
                    return response;
                }

                OperationResult operationResult = monitorTreeTypeRepository.Delete<MonitorTreeType>(param.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<MonitorTreeType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    //reason = new DicOperationResult { Reason = "#204070045001" };
                    response = new BaseResponse<bool>("003081");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                //reason = new DicOperationResult { Reason = "#204070045001" };
                response = new BaseResponse<bool>("003081");
                return response;
            }
        }

        private bool IsDefaultMonitorTreeType(int id)
        {
            var monitorTreeType = monitorTreeTypeRepository.GetByKey(id);
            if (monitorTreeType != null && monitorTreeType.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            return false;
        }

        private bool IsExistMonitorTree(int id)
        {
            var isExist = monitorTreeTypeRepository.GetDatas<MonitorTree>(t => t.Type == id, true).Any();
            return isExist;
        }
        #endregion

        #region 监测树类型级别(Describe)是否重复

        /// <summary>
        /// 监测树类型级别(Describe)是否重复
        /// 创建人:王龙杰
        /// 创建时间：2017-11-21
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<IsRepeatResult> IsExistDescribeInMonitorTree(IsExistDescribeInMonitorTreeParameter Para)
        {
            BaseResponse<IsRepeatResult> response = new BaseResponse<IsRepeatResult>();
            IsRepeatResult IsRepeatResult = new IsRepeatResult();

            if (string.IsNullOrEmpty(Para.Describe))
            {
                //Describes不能为空
                response.IsSuccessful = false;
                response.Code = "009763";
                return response;
            }
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    var linq = dbContext.MonitorTreeType.Where<MonitorTreeType>(w => w.Describe == Para.Describe);

                    if (Para.ID != -1)
                    {
                        linq = linq.Where(w => w.ID != Para.ID);
                    }

                    IsRepeatResult.IsRepeat = linq.Any();
                    response.IsSuccessful = true;
                    response.Result = IsRepeatResult;
                    return response;
                }
            }
            catch (Exception ex)
            {
                //验证通用数据是否有相同Code 出错
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008521";
                return response;
            }
        }

        #endregion

        #endregion

        #region 通用数据设备类型
        #region 获取通用数据设备类型接口
        public BaseResponse<CommonDataResult> GetDeviceTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            CommonDataResult result = new CommonDataResult();
            List<CommonInfo> deviceTypeList = new List<CommonInfo>();
            try
            {
                List<DeviceType> deviceTypes = cacheDICT.GetInstance().GetCacheType<DeviceType>().ToList();

                deviceTypeList = (from dt in deviceTypes
                                  select new CommonInfo
                                  {
                                      ID = dt.ID,
                                      Code = dt.Code,
                                      Name = dt.Name,
                                      Describe = dt.Describe,
                                      IsUsable = dt.IsUsable,
                                      IsDefault = dt.IsDefault,
                                      AddDate = dt.AddDate.ToString(),
                                      OrderNo = dt.OrderNo,
                                      IsExistChild = cacheDICT.GetInstance().GetCacheType<MeasureSiteType>(t => t.DeviceTypeID == dt.ID).Any()
                                  }).ToList();

                result.CommonInfoList = deviceTypeList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>(false, null, "003091", null);
                return response;
            }
        }
        #endregion

        #region 添加设备类型接口
        public BaseResponse<bool> AddDeviceTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    DeviceType deviceType = new DeviceType();
                    deviceType.Code = Guid.NewGuid().ToString();
                    deviceType.Name = param.Name;
                    deviceType.Describe = param.Describe;
                    deviceType.IsDefault = param.IsDefault;
                    deviceType.IsUsable = param.IsUsable;
                    int? orderNoDeviceType = dbContext.DeviceType.Select(s => s.OrderNo).Max();
                    if (orderNoDeviceType.HasValue)
                        deviceType.OrderNo = orderNoDeviceType.Value + 1;
                    else
                        deviceType.OrderNo = 1;
                    OperationResult operationResult = dbContext.DeviceType.AddNew<DeviceType>(dbContext, deviceType);

                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        //向系统参数表中添加，父节点：形貌图配置
                        Config rootConfig = dbContext.Config.GetDatas<Config>(dbContext, t => t.Code == "TopographicMap").FirstOrDefault();
                        if (rootConfig != null)
                        {
                            Config config = new Config();
                            config.Code = Guid.NewGuid().ToString();
                            config.Name = deviceType.Name;
                            config.IsUsed = param.IsUsable;
                            config.IsDefault = param.IsDefault;
                            config.ParentId = rootConfig.ID;
                            int? orderNoConfig = dbContext.Config.Where(w => w.ParentId == rootConfig.ID).
                                                                            Select(s => s.OrderNo).Max();
                            if (orderNoConfig.HasValue)
                                config.OrderNo = orderNoConfig.Value + 1;
                            else
                                config.OrderNo = 1;
                            config.CommonDataType = 2;
                            config.CommonDataCode = deviceType.Code;

                            operationResult = dbContext.Config.AddNew<Config>(dbContext, config);
                        }

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<DeviceType>();
                        cacheDICT.GetInstance().UpdateCacheType<Config>();
                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        //dicOperRes = new DicOperationResult { Reason = "#204030015001" };
                        response = new BaseResponse<bool>("003101");
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003101");
                return response;
            }
        }
        #endregion

        #region 编辑设备类型
        public BaseResponse<bool> EditDeviceTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    DeviceType deviceType = cacheDICT.GetInstance(context).GetCacheType<DeviceType>(p => p.ID == param.ID).SingleOrDefault();
                    if (deviceType == null)
                    {
                        response = new BaseResponse<bool>("003112");
                        return response;
                    }

                    bool IsUsableChenged = false;
                    if (deviceType.IsUsable != param.IsUsable)
                    {
                        IsUsableChenged = true;
                    }

                    #region 设备树类型已经被使用，则不能停用 王颖辉 2017-02-20
                    if (param.IsUsable == 0)
                    {
                        int count = deviceRepository.GetDatas<Device>(item => item.DevType == param.ID, true).Count();
                        if (count > 0)
                        {
                            response = new BaseResponse<bool>("007932");
                            return response;
                        }
                    }
                    #endregion

                    #region 设备类型不能全部禁用，移植iPVP 王龙杰 2017-10-09
                    var elseUsed = deviceTypeRepository.GetDatas<DeviceType>(t => t.IsUsable == 1 && t.ID != deviceType.ID, true).Any();
                    if (!elseUsed && param.IsUsable == 0)
                    {
                        //设备类型不能全部禁用
                        response = new BaseResponse<bool>("009892");
                        return response;
                    }
                    #endregion

                    deviceType.Name = param.Name;
                    deviceType.Describe = param.Describe;
                    deviceType.IsUsable = param.IsUsable;
                    deviceType.IsDefault = param.IsDefault;
                    OperationResult operationResult = context.DeviceType.Update(context, deviceType);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        if (IsUsableChenged)
                        {
                            #region 修改相关的测量位置类型
                            var measureSiteTypes = cacheDICT.GetInstance().GetCacheType<MeasureSiteType>(a => a.DeviceTypeID == param.ID).ToList();
                            if (measureSiteTypes != null && measureSiteTypes.Count > 0)
                            {
                                foreach (var msType in measureSiteTypes)
                                {
                                    msType.IsUsable = deviceType.IsUsable;
                                    context.MeasureSiteType.Update(context, msType);
                                }

                                //更新缓存
                                cacheDICT.GetInstance().UpdateCacheType<MeasureSiteType>(context);
                            }
                            #endregion

                            #region 修改对应系统参数
                            var config = cacheDICT.GetInstance().GetCacheType<Config>(a => a.CommonDataType == 2 && a.CommonDataCode == deviceType.Code).FirstOrDefault();
                            if (config != null)
                            {
                                config.IsUsed = deviceType.IsUsable;
                                context.Config.Update(context, config);
                                var subConfig = cacheDICT.GetInstance().GetCacheType<Config>(a => a.ParentId == config.ID).ToList();

                                foreach (var sub in subConfig)
                                {
                                    sub.IsUsed = config.IsUsed;
                                    context.Config.Update(context, sub);
                                }

                                //更新缓存
                                cacheDICT.GetInstance().UpdateCacheType<Config>(context);
                            }
                            #endregion
                        }

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<DeviceType>(context);

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003121");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003121");
                return response;
            }
        }
        #endregion

        #region 删除设备类型
        /// <summary>
        /// 删除设备类型的时候，需要级联删除设备类型所包含的测点类型
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteDeviceTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultDeviceType(param.ID))
                {
                    response = new BaseResponse<bool>("003132");
                    return response;
                }
                if (IsExistDevices(param.ID))
                {
                    response = new BaseResponse<bool>("003142");
                    return response;
                }

                return ExecuteDB.ExecuteTrans((context) =>
                {
                    if (string.IsNullOrEmpty(param.Code.Trim()))
                    {
                        param.Code = context.DeviceType.Where(w => w.ID == param.ID).Select(s => s.Code).FirstOrDefault();
                    }
                    OperationResult operationResult = context.DeviceType.Delete(context, param.ID);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        //删除设备类型对应的测点类型
                        var msTypes = context.MeasureSiteType
                            .GetDatas<MeasureSiteType>(context, t => t.DeviceTypeID == param.ID);
                        foreach (MeasureSiteType msType in msTypes)
                        {
                            context.MeasureSiteType.Delete(context, msType);
                        }

                        //删除对应系统参数，通过Code
                        context.Config.Delete(context, t => t.CommonDataType == 2 && t.CommonDataCode == param.Code);

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<DeviceType>(context);
                        cacheDICT.GetInstance().UpdateCacheType<MeasureSiteType>(context);
                        cacheDICT.GetInstance().UpdateCacheType<Config>(context);

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003151");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003151");
                return response;
            }
        }

        private bool IsDefaultDeviceType(int id)
        {
            var devType = deviceTypeRepository.GetByKey(id);
            if (devType != null && devType.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            return false;
        }

        private bool IsExistDevices(int id)
        {
            var isExistDevices = deviceRepository.GetDatas<Device>(t => t.DevType == id, true).Any();
            return isExistDevices;
        }
        #endregion
        #endregion

        #region 通用数据测量位置类型
        #region 获取通用数据测量位置类型接口
        public BaseResponse<CommonDataResult> GetMSiteTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            CommonDataResult result = new CommonDataResult();
            List<CommonInfo> commonInfoList = new List<CommonInfo>();

            try
            {
                List<MeasureSiteType> msTypes = cacheDICT.GetInstance().GetCacheType<MeasureSiteType>(t => t.DeviceTypeID == param.ParentID).ToList();
                foreach (var msType in msTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = msType.ID,
                        Code = msType.Code,
                        PID = null,
                        Name = msType.Name,
                        Describe = msType.Describe,
                        IsUsable = msType.IsUsable,
                        IsDefault = msType.IsDefault,
                        AddDate = msType.AddDate.ToString(),
                        OrderNo = msType.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003161");
                return response;
            }
        }
        #endregion

        #region 新增测量位置类型
        public BaseResponse<bool> AddMSiteTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    DeviceType DeviceType = dbContext.DeviceType.Where(g => g.ID == param.ParentID).FirstOrDefault();
                    MeasureSiteType msType = new MeasureSiteType();
                    msType.DeviceTypeID = param.ParentID;
                    msType.Code = Guid.NewGuid().ToString();
                    msType.Name = param.Name;
                    msType.Describe = param.Describe;
                    msType.IsUsable = param.IsUsable;
                    msType.IsDefault = param.IsDefault;
                    int? orderNoMeasureSiteType = dbContext.MeasureSiteType.Where(w => w.DeviceTypeID == param.ParentID).Select(s => s.OrderNo).Max();
                    if (orderNoMeasureSiteType.HasValue)
                        msType.OrderNo = orderNoMeasureSiteType.Value + 1;
                    else
                        msType.OrderNo = 1;
                    OperationResult operationResult = dbContext.MeasureSiteType.AddNew<MeasureSiteType>(dbContext, msType);

                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        //向系统参数表中添加，父节点：测点类型对应父设备类型。
                        Config rootConfig = dbContext.Config.GetDatas<Config>(dbContext, t =>
                                            t.CommonDataType == 2 && t.CommonDataCode == DeviceType.Code).FirstOrDefault();
                        if (rootConfig != null)
                        {
                            Config config = new Config();
                            config.Code = Guid.NewGuid().ToString();
                            config.Name = msType.Name;
                            config.IsUsed = param.IsUsable;
                            config.IsDefault = param.IsDefault;
                            config.Describe = param.Describe;
                            config.ParentId = rootConfig.ID;
                            int? orderNoConfig = dbContext.Config.Where(w => w.ParentId == rootConfig.ID).
                                                                            Select(s => s.OrderNo).Max();
                            if (orderNoConfig.HasValue)
                                config.OrderNo = orderNoConfig.Value + 1;
                            else
                                config.OrderNo = 1;
                            config.CommonDataType = 3;
                            config.CommonDataCode = msType.Code;

                            configRepository.AddNew<Config>(config);
                        }

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<MeasureSiteType>();
                        cacheDICT.GetInstance().UpdateCacheType<Config>();

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003171");
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003171");
                return response;
            }
        }
        #endregion

        #region 编辑测量位置类型
        /// <summary>
        /// 可用状态发生改变，变为可用，对应设备类型可用，变为不可用，对应设备类型不变
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditMSiteTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    var msType = cacheDICT.GetInstance(context).GetCacheType<MeasureSiteType>(p => p.ID == param.ID).SingleOrDefault();
                    if (msType == null)
                    {
                        response = new BaseResponse<bool>("003182");
                        return response;
                    }

                    bool IsUsableChenged = false;
                    if (msType.IsUsable != param.IsUsable)
                    {
                        IsUsableChenged = true;
                    }

                    #region 测量位置类型已经被使用，不能进行停用 王颖辉 2017-02-20
                    if (param.IsUsable == 0)
                    {
                        int count = measureSiteRepository.GetDatas<MeasureSite>(item => item.MSiteName == param.ID, true).Count();
                        if (count > 0)
                        {
                            response = new BaseResponse<bool>("007942");
                            return response;
                        }
                    }
                    #endregion

                    #region 父设备类型被禁用，测量位置不能启用 王龙杰 2017-10-09
                    if (param.IsUsable == 1)
                    {
                        int deviceTypeIsUsable = deviceTypeRepository.GetDatas<DeviceType>(item => item.ID == msType.DeviceTypeID, true).
                                        Select(s => s.IsUsable).FirstOrDefault();
                        if (deviceTypeIsUsable == 0)
                        {
                            //父设备类型被禁用，测量位置不能启用
                            response = new BaseResponse<bool>("009862");
                            return response;
                        }
                    }
                    #endregion

                    #region 同一设备类型下测量位置类型不能全部禁用 王龙杰 2017-10-09
                    var elseUsed = measureSiteTypeRepository.GetDatas<MeasureSiteType>(t =>
                                       t.DeviceTypeID == msType.DeviceTypeID && t.IsUsable == 1 && t.ID != msType.ID, true).Any();
                    if (!elseUsed && param.IsUsable == 0)
                    {
                        //同一设备类型下测量位置类型不能全部禁用
                        response = new BaseResponse<bool>("009882");
                        return response;
                    }
                    #endregion

                    msType.Name = param.Name;
                    msType.Describe = param.Describe;
                    msType.IsUsable = param.IsUsable;
                    msType.IsDefault = param.IsDefault;
                    OperationResult operationResult = context.MeasureSiteType.Update(context, msType);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        List<Config> configList = context.Config.GetDatas<Config>(context, t =>
                                            t.CommonDataType == 3 && t.CommonDataCode == msType.Code).ToList();
                        if (configList.Count > 0)
                        {
                            foreach (Config c in configList)
                            {
                                c.Describe = param.Describe;
                            }
                            context.Config.Update<Config>(context, configList);
                        }

                        if (IsUsableChenged)
                        {
                            #region 可用状态发生改变，变为可用，对应设备类型可用，变为不可用，对应设备类型不变

                            if (msType.IsUsable == 1)
                            {
                                var deviceType = context.DeviceType.GetByKey(context, msType.DeviceTypeID);
                                if (deviceType != null && deviceType.IsUsable != 1)
                                {
                                    deviceType.IsUsable = 1;
                                    context.DeviceType.Update(context, deviceType);

                                    //更新缓存
                                    cacheDICT.GetInstance().UpdateCacheType<DeviceType>(context);
                                }
                            }

                            #endregion

                            #region 修改对应系统参数
                            var config = context.Config.GetDatas(context, a => a.CommonDataType == 3 && a.CommonDataCode == msType.Code).ToList();
                            if (config != null && config.Count > 0)
                            {
                                foreach (var c in config)
                                {
                                    c.IsUsed = msType.IsUsable;
                                }
                                context.Config.Update(context, config);

                                //更新缓存
                                cacheDICT.GetInstance().UpdateCacheType<Config>(context);
                            }
                            #endregion
                        }

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<MeasureSiteType>(context);

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003191");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003191");
                return response;
            }
        }
        #endregion

        #region 删除测量位置类型
        public BaseResponse<bool> DeleteMSiteTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultMSiteType(param.ID))
                {
                    response = new BaseResponse<bool>("003202");
                    return response;
                }
                //如果存在此类型测点，则不能删除
                if (IsMSiteExisted(param.ID))
                {
                    response = new BaseResponse<bool>("003212");
                    return response;
                }

                return ExecuteDB.ExecuteTrans((context) =>
                {
                    if (string.IsNullOrEmpty(param.Code.Trim()))
                    {
                        param.Code = context.MeasureSiteType.Where(w => w.ID == param.ID).Select(s => s.Code).FirstOrDefault();
                    }
                    OperationResult operationResult = context.MeasureSiteType.Delete(context, param.ID);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        //删除对应系统参数，通过Code
                        context.Config.Delete(context, t => t.CommonDataType == 3 && t.CommonDataCode == param.Code);

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<MeasureSiteType>(context);
                        cacheDICT.GetInstance().UpdateCacheType<Config>(context);
                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003221");
                        return response;
                    }
                });

            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003221");
                return response;
            }
        }
        #endregion
        #endregion

        #region 通用数据测量位置监测类型
        #region 获取通用数据测量位置监测类型接口
        public BaseResponse<CommonDataResult> GetMSiteMTTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();
            CommonDataResult result = new CommonDataResult();

            try
            {
                List<MeasureSiteMonitorType> msmtTypes = cacheDICT.GetInstance().GetCacheType<MeasureSiteMonitorType>().ToList();
                foreach (var msmtType in msmtTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = msmtType.ID,
                        Code = msmtType.Code,
                        PID = null,
                        Name = msmtType.Name,
                        Describe = msmtType.Describe,
                        IsUsable = msmtType.IsUsable,
                        IsDefault = msmtType.IsDefault,
                        AddDate = msmtType.AddDate.ToString(),
                        OrderNo = msmtType.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003231");
                return response;
            }
        }
        #endregion

        #region 新增测量位置监测类型
        public BaseResponse<bool> AddMSiteMTTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                MeasureSiteMonitorType msmtType = new MeasureSiteMonitorType();
                msmtType.Code = Guid.NewGuid().ToString();
                msmtType.Name = param.Name;
                msmtType.Describe = param.Describe;
                msmtType.IsUsable = param.IsUsable;
                msmtType.IsDefault = param.IsDefault;
                int? orderNoMeasureSiteMonitorType = new iCMSDbContext().MeasureSiteMonitorType.Select(s => s.OrderNo).Max();
                if (orderNoMeasureSiteMonitorType.HasValue)
                    msmtType.OrderNo = orderNoMeasureSiteMonitorType.Value + 1;
                else
                    msmtType.OrderNo = 1;
                OperationResult operationResult = measureSiteMonitorTypeRepository.AddNew<MeasureSiteMonitorType>(msmtType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<MeasureSiteMonitorType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003241");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003241");
                return response;
            }
        }
        #endregion

        #region 编辑测量位置监测类型
        public BaseResponse<bool> EditMSiteMTTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                var msmtType = cacheDICT.GetInstance().GetCacheType<MeasureSiteMonitorType>(p => p.ID == param.ID).SingleOrDefault();
                if (msmtType == null)
                {
                    response = new BaseResponse<bool>("003252");
                    return response;
                }

                bool IsUsableChenged = false;
                if (msmtType.IsUsable != param.IsUsable)
                {
                    IsUsableChenged = true;
                }

                #region 测量位置监测类型如果已经被使用，则不能停用 王颖辉 2017-02-20
                if (param.IsUsable == 0)
                {
                    int count = 0;
                    switch (param.ID)
                    {

                        case 1://设备温度
                            count = tempeDeviceSetMSiteAlmRepository.GetDatas<TempeDeviceSetMSiteAlm>(item => item.MsiteAlmID > 0, true).Count();
                            break;
                        case 2://电池电压 
                            count = voltageSetMSiteAlmRepository.GetDatas<VoltageSetMSiteAlm>(item => item.MsiteAlmID > 0, true).Count();
                            break;
                        case 3://传感器温度
                            count = tempeWSSetMSiteAlmRepository.GetDatas<TempeWSSetMSiteAlm>(item => item.MsiteAlmID > 0, true).Count();
                            break;
                    }
                    if (count > 0)
                    {
                        response = new BaseResponse<bool>("007952");
                        return response;
                    }
                }
                #endregion

                #region 测量位置监测类型不能全部禁用 王龙杰 2017-10-09
                var elseUsed = measureSiteMonitorTypeRepository.GetDatas<MeasureSiteMonitorType>(t =>
                                   t.IsUsable == 1 && t.ID != msmtType.ID, true).Any();
                if (!elseUsed && param.IsUsable == 0)
                {
                    //测量位置监测类型不能全部禁用
                    response = new BaseResponse<bool>("009872");
                    return response;
                }
                #endregion

                msmtType.Name = param.Name;
                msmtType.Describe = param.Describe;
                msmtType.IsUsable = param.IsUsable;
                msmtType.IsDefault = param.IsDefault;
                OperationResult operationResult = measureSiteMonitorTypeRepository.Update<MeasureSiteMonitorType>(msmtType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    if (IsUsableChenged)
                    {
                        #region 修改对应系统参数
                        var config = cacheDICT.GetInstance().GetCacheType<Config>(a => a.CommonDataType == 4 && a.CommonDataCode == msmtType.Code).ToList();
                        foreach (var c in config)
                        {
                            c.IsUsed = msmtType.IsUsable;
                            configRepository.Update(c);
                        }
                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<Config>();
                        #endregion

                        #region 修改对应系统功能
                        var module = moduleRepository.GetDatas<Module>(a => a.CommonDataType == 4 && a.CommonDataCode == msmtType.Code, false).ToList();
                        foreach (var m in module)
                        {
                            m.IsUsed = msmtType.IsUsable;
                            moduleRepository.Update(m);
                        }
                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<Config>();
                        #endregion
                    }

                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<MeasureSiteMonitorType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003261");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);

                response = new BaseResponse<bool>("003261");
                return response;
            }
        }
        #endregion

        #region 删除测量位置监测类型
        public BaseResponse<bool> DeleteMSiteMTTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultMSiteMTType(param.ID))
                {
                    response = new BaseResponse<bool>("003272");
                    return response;
                }

                OperationResult operationResult = measureSiteMonitorTypeRepository.Delete<MeasureSiteMonitorType>(param.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<MeasureSiteMonitorType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003281");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003281");
                return response;
            }
        }
        #endregion
        #endregion

        #region 通用数据振动信号类型
        #region 获取通用数据振动类型数据

        public BaseResponse<CommonDataResult> GetVIBTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();
            CommonDataResult result = new CommonDataResult();

            try
            {
                List<VibratingSingalType> vibTypes = cacheDICT.GetInstance().GetCacheType<VibratingSingalType>().ToList();
                foreach (var vibType in vibTypes)
                {
                    CommonInfo vibTypeData = new CommonInfo
                    {
                        ID = vibType.ID,
                        Code = vibType.Code,
                        Name = vibType.Name,
                        Describe = vibType.Describe,
                        IsUsable = vibType.IsUsable,
                        IsDefault = vibType.IsDefault,
                        AddDate = vibType.AddDate.ToString(),
                        OrderNo = vibType.OrderNo
                    };

                    #region 计算是否有各个子节点

                    var isExistEigenChild = eigenValueTypeRepository
                        .GetDatas<EigenValueType>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WireLessSensor_EigenValue", true)
                        .Any();
                    var isExistWaveLengthChild = waveLengthValuesRepository
                        .GetDatas<WaveLengthValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WireLessSensor_WaveLength", true)
                        .Any();
                    var isExistUpperLimit = waveUpperLimitValuesRepository
                        .GetDatas<WaveUpperLimitValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WireLessSensor_UpLimit", true)
                        .Any();
                    var isExistLowerLimit = waveLowerLimitValuesRepository
                        .GetDatas<WaveLowerLimitValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WireLessSensor_LowLimit", true)
                        .Any();
                    var isExistEigenWaveLength = waveLengthValuesRepository
                        .GetDatas<WaveLengthValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WireLessSensor_EigenWaveLength", true)
                        .Any();
                    vibTypeData.IsExistEigenChild = isExistEigenChild;
                    vibTypeData.IsExistWaveLengthChild = isExistWaveLengthChild;
                    vibTypeData.IsExistUpperLimitChild = isExistUpperLimit;
                    vibTypeData.IsExistLowerLimitChild = isExistLowerLimit;
                    vibTypeData.IsExistEigenWaveLengthChild = isExistEigenWaveLength;

                    #endregion

                    commonInfoList.Add(vibTypeData);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003291");
                return response;
            }
        }

        #endregion

        #region 添加通用数据振动类型
        public BaseResponse<bool> AddVIBTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                VibratingSingalType vibSignalType = new VibratingSingalType();
                vibSignalType.Code = Guid.NewGuid().ToString();
                vibSignalType.Name = param.Name;
                vibSignalType.Describe = param.Describe;
                vibSignalType.IsUsable = param.IsUsable;
                vibSignalType.IsDefault = param.IsDefault;
                int? orderNoVibratingSingalType = new iCMSDbContext().VibratingSingalType.Select(s => s.OrderNo).Max();
                if (orderNoVibratingSingalType.HasValue)
                    vibSignalType.OrderNo = orderNoVibratingSingalType.Value + 1;
                else
                    vibSignalType.OrderNo = 1;
                OperationResult operationResult = vibratingSingalTypeRepository.AddNew<VibratingSingalType>(vibSignalType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<VibratingSingalType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003301");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003301");
                return response;
            }
        }
        #endregion

        #region 编辑通用数据振动类型
        public BaseResponse<bool> EditVIBTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    var vibSignalType = cacheDICT.GetInstance(context).GetCacheType<VibratingSingalType>(p => p.ID == param.ID).SingleOrDefault();
                    if (vibSignalType == null)
                    {
                        response = new BaseResponse<bool>("003312");
                        return response;
                    }

                    #region 振动类型如果已经被使用，则不能进行停用 王颖辉 2017-02-20
                    if (param.IsUsable == 0)
                    {
                        int count = vibSignalRepository.GetDatas<VibSingal>(item => item.SingalType == param.ID, true).Count();
                        if (count > 0)
                        {
                            response = new BaseResponse<bool>("007962");
                            return response;
                        }
                    }
                    #endregion

                    #region 振动类型不能全部禁用 王龙杰 2017-10-09
                    var elseUsed = vibratingSingalTypeRepository.GetDatas<VibratingSingalType>(t =>
                                       t.IsUsable == 1 && t.ID != vibSignalType.ID, true).Any();
                    if (!elseUsed && param.IsUsable == 0)
                    {
                        //振动类型不能全部禁用
                        response = new BaseResponse<bool>("009912");
                        return response;
                    }
                    #endregion

                    vibSignalType.Name = param.Name;
                    vibSignalType.Describe = param.Describe;
                    vibSignalType.IsUsable = param.IsUsable;
                    vibSignalType.IsDefault = param.IsDefault;
                    OperationResult operationResult = context.VibratingSingalType.Update(context, vibSignalType);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        #region 振动类型的IsUsable 应与 波长，特征值，上限频率，下限频率  保持一致

                        var waveLenTypes = cacheDICT.GetInstance().GetCacheType<WaveLengthValues>(t => t.VibratingSignalTypeID == vibSignalType.ID);
                        foreach (var waveLenType in waveLenTypes)
                        {
                            waveLenType.IsUsable = vibSignalType.IsUsable;
                        }
                        context.WaveLengthValues.Update(context, waveLenTypes);
                        var eigenValus = cacheDICT.GetInstance().GetCacheType<EigenValueType>(t => t.VibratingSignalTypeID == vibSignalType.ID);
                        foreach (var eigenVal in eigenValus)
                        {
                            eigenVal.IsUsable = vibSignalType.IsUsable;
                        }
                        context.EigenValueType.Update(context, eigenValus);

                        List<string> eigenCode = eigenValus.Select(s => s.Code).Distinct().ToList();
                        #region 修改对应系统参数
                        var config = cacheDICT.GetInstance().GetCacheType<Config>(a => a.CommonDataType == 6 && eigenCode.Contains(a.CommonDataCode)).ToList();
                        if (config != null && config.Count > 0)
                        {
                            foreach (var c in config)
                            {
                                c.IsUsed = vibSignalType.IsUsable;
                            }
                            context.Config.Update(context, config);
                            //更新缓存
                            cacheDICT.GetInstance().UpdateCacheType<Config>(context);
                        }
                        #endregion

                        var waveUpperTypes = cacheDICT.GetInstance().GetCacheType<WaveUpperLimitValues>(t => t.VibratingSignalTypeID == vibSignalType.ID);
                        foreach (var waveUpperType in waveUpperTypes)
                        {
                            waveUpperType.IsUsable = vibSignalType.IsUsable;
                        }
                        context.WaveUpperLimitValues.Update(context, waveUpperTypes);
                        var waveLowerTypes = cacheDICT.GetInstance().GetCacheType<WaveLowerLimitValues>(t => t.VibratingSignalTypeID == vibSignalType.ID);
                        foreach (var waveLowerType in waveLowerTypes)
                        {
                            waveLowerType.IsUsable = vibSignalType.IsUsable;
                        }
                        context.WaveLowerLimitValues.Update(context, waveLowerTypes);
                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<VibratingSingalType>(context);
                        cacheDICT.GetInstance().UpdateCacheType<WaveLengthValues>(context);
                        cacheDICT.GetInstance().UpdateCacheType<EigenValueType>(context);
                        cacheDICT.GetInstance().UpdateCacheType<WaveUpperLimitValues>(context);
                        cacheDICT.GetInstance().UpdateCacheType<WaveLowerLimitValues>(context);

                        #endregion

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003321");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003321");
                return response;
            }
        }
        #endregion

        #region 删除通用数据振动类型
        /// <summary>
        /// 删除振动类型需要级联删除 特征值，波形上限，波形下限，波长
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteVIBTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                if (IsDefaultVibType(param.ID))
                {
                    response = new BaseResponse<bool>("003332");
                    return response;
                }

                return ExecuteDB.ExecuteTrans((context) =>
                {
                    //振动信号类型已被使用，不能删除
                    if (IsVIBTypeUsed(param.ID, context))
                    {
                        response = new BaseResponse<bool>("010052");
                        return response;
                    }
                    //振动信号类型为系统默认，不能删除
                    if (IsVIBTypeDefault(param.ID, context))
                    {
                        response = new BaseResponse<bool>("010062");
                        return response;
                    }

                    OperationResult operationResult = context.VibratingSingalType.Delete(context, param.ID);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        //级联删除
                        var eigenValueTypes = context.EigenValueType
                            .GetDatas<EigenValueType>(context, t => t.VibratingSignalTypeID == param.ID).ToList();
                        var waveLengthTypes = context.WaveLengthValues
                            .GetDatas<WaveLengthValues>(context, t => t.VibratingSignalTypeID == param.ID);
                        var waveUpperLimitTypes = context.WaveUpperLimitValues
                            .GetDatas<WaveUpperLimitValues>(context, t => t.VibratingSignalTypeID == param.ID);
                        var waveLowerLimitTypes = context.WaveLowerLimitValues
                            .GetDatas<WaveLowerLimitValues>(context, t => t.VibratingSignalTypeID == param.ID);
                        foreach (var eigen in eigenValueTypes)
                        {
                            context.EigenValueType.Delete(context, eigen);
                        }
                        foreach (var waveLength in waveLengthTypes)
                        {
                            context.WaveLengthValues.Delete(context, waveLength);
                        }
                        foreach (var waveUpper in waveUpperLimitTypes)
                        {
                            context.WaveUpperLimitValues.Delete(context, waveUpper);
                        }
                        foreach (var waveLower in waveLowerLimitTypes)
                        {
                            context.WaveLowerLimitValues.Delete(context, waveLower);
                        }

                        //删除振动信号下特征值对应系统参数
                        List<string> eigenValueTypeCodes = eigenValueTypes.Select(s => s.Code).ToList();
                        var config = context.Config.
                            GetDatas<Config>(context, t => t.CommonDataType == 6 && eigenValueTypeCodes.Contains(t.CommonDataCode)).ToList();
                        foreach (var c in config)
                        {
                            context.Config.Delete(context, c);
                        }

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<VibratingSingalType>(context);
                        cacheDICT.GetInstance().UpdateCacheType<EigenValueType>(context);
                        cacheDICT.GetInstance().UpdateCacheType<WaveUpperLimitValues>(context);
                        cacheDICT.GetInstance().UpdateCacheType<WaveLowerLimitValues>(context);
                        cacheDICT.GetInstance().UpdateCacheType<WaveLengthValues>(context);
                        cacheDICT.GetInstance().UpdateCacheType<Config>(context);

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003341");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003341");
                return response;
            }
        }

        private bool IsVIBTypeUsed(int id, iCMSDbContext context)
        {
            bool IsVIBTypeUsed = context.VibSingal.GetDatas<VibSingal>(context, t => t.SingalType == id).Any();
            return IsVIBTypeUsed;
        }

        private bool IsVIBTypeDefault(int id, iCMSDbContext context)
        {
            bool IsVIBTypeDefault = context.VibratingSingalType.GetDatas<VibratingSingalType>(context, t => t.ID == id && t.IsDefault == 0).Any();
            return IsVIBTypeDefault;
        }

        #endregion
        #endregion

        #region 通用数据特征值类型

        #region 获取通用数据特征值类型接口

        public BaseResponse<CommonDataResult> GetEigenTypeDataForCommon(GetComSetDataParameter param)
        {
            CommonDataResult result = new CommonDataResult();
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();

            try
            {
                string describe = "WireLessSensor_EigenValue";
                switch (param.DevFormType)
                {
                    case 1:
                        {
                            describe = "WireLessSensor_EigenValue";
                            break;
                        }

                    case 2:
                        {
                            describe = "WiredSensor_EigenValue";
                            break;
                        }

                    case 3:
                        {
                            describe = "Triaxial_EigenValue";
                            break;
                        }

                    default:
                        {
                            describe = "WireLessSensor_EigenValue";
                            break;
                        }
                }
                List<EigenValueType> eigenValueTypes = cacheDICT.GetInstance()
                    .GetCacheType<EigenValueType>(t => t.VibratingSignalTypeID == param.ParentID
                        && t.Describe == describe)
                    .ToList();
                foreach (var eigenValueType in eigenValueTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = eigenValueType.ID,
                        Code = eigenValueType.Code,
                        //振动信号ID
                        PID = param.ParentID,
                        Name = eigenValueType.Name,
                        Describe = eigenValueType.Describe,
                        IsUsable = eigenValueType.IsUsable,
                        IsDefault = eigenValueType.IsDefault,
                        AddDate = eigenValueType.AddDate.ToString(),
                        OrderNo = eigenValueType.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003351");
                return response;
            }
        }

        #endregion

        #region 新增特征值类型

        public BaseResponse<bool> AddEigenTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    EigenValueType eigenValueType = new EigenValueType();
                    eigenValueType.VibratingSignalTypeID = param.ParentID;
                    eigenValueType.Code = Guid.NewGuid().ToString();
                    eigenValueType.Name = param.Name;
                    eigenValueType.Describe = param.Describe;
                    eigenValueType.IsUsable = param.IsUsable;
                    eigenValueType.IsDefault = param.IsDefault;
                    int? orderNoEigenValueType = dbContext.EigenValueType.Where(w => w.VibratingSignalTypeID == param.ParentID).
                                                                           Select(s => s.OrderNo).Max();
                    if (orderNoEigenValueType.HasValue)
                        eigenValueType.OrderNo = orderNoEigenValueType.Value + 1;
                    else
                        eigenValueType.OrderNo = 1;
                    OperationResult operationResult = dbContext.EigenValueType.AddNew<EigenValueType>(dbContext, eigenValueType);

                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        string parentName = dbContext.VibratingSingalType.Where(w => w.ID == param.ParentID).
                                                    Select(s => s.Name).FirstOrDefault();
                        //向config“形貌图显示配置”下添加
                        Config rootConfigDIDC = dbContext.Config.GetDatas<Config>(dbContext, t => t.Code == "DeviceImageDisplayConfiguration").FirstOrDefault();
                        if (rootConfigDIDC != null)
                        {
                            Config config = new Config();
                            config.Code = Guid.NewGuid().ToString();
                            config.Name = parentName + "-" + param.Name;
                            config.IsUsed = param.IsUsable;
                            config.IsDefault = param.IsDefault;
                            config.ParentId = rootConfigDIDC.ID;
                            int? orderNoConfig = dbContext.Config.Where(w => w.ParentId == rootConfigDIDC.ID).
                                                                            Select(s => s.OrderNo).Max();
                            if (orderNoConfig.HasValue)
                                config.OrderNo = orderNoConfig.Value + 1;
                            else
                                config.OrderNo = 1;
                            config.CommonDataType = 6;
                            config.CommonDataCode = eigenValueType.Code;

                            configRepository.AddNew<Config>(config);
                        }

                        //向config“历史数据显示相关”下添加
                        Config rootConfigLSSJ = dbContext.Config.GetDatas<Config>(dbContext, t => t.Code == "CONFIG_55_LSSJXSXG").FirstOrDefault();
                        if (rootConfigLSSJ != null)
                        {
                            Config config = new Config();
                            config.Code = Guid.NewGuid().ToString();
                            config.Name = parentName + param.Name;
                            config.IsUsed = param.IsUsable;
                            config.IsDefault = param.IsDefault;
                            config.ParentId = rootConfigLSSJ.ID;
                            int? orderNoConfig = dbContext.Config.Where(w => w.ParentId == rootConfigLSSJ.ID).
                                                                            Select(s => s.OrderNo).Max();
                            if (orderNoConfig.HasValue)
                                config.OrderNo = orderNoConfig.Value + 1;
                            else
                                config.OrderNo = 1;
                            config.CommonDataType = 6;
                            config.CommonDataCode = eigenValueType.Code;

                            configRepository.AddNew<Config>(config);
                        }

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<EigenValueType>();
                        cacheDICT.GetInstance().UpdateCacheType<Config>();
                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003361");
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003361");
                return response;
            }
        }

        #endregion

        #region 编辑特征值类型

        /// <summary>
        /// 可用状态发生改变，变为可用，对应振动信号可用，变为不可用，对应振动信号不变
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditEigenTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    var eigenValueType = cacheDICT.GetInstance(context).GetCacheType<EigenValueType>(p => p.ID == param.ID).SingleOrDefault();
                    if (eigenValueType == null)
                    {
                        response = new BaseResponse<bool>("003371");
                        return response;
                    }

                    bool IsUsableChenged = false;
                    if (eigenValueType.IsUsable != param.IsUsable)
                    {
                        IsUsableChenged = true;
                    }

                    #region 特征值类型已经被使用，则不能进行停用 王颖辉 2012-02-20

                    if (param.IsUsable == 0)
                    {
                        int count = signalAlmSetRepository.GetDatas<SignalAlmSet>(item => item.ValueType == param.ID, true).Count();
                        if (count > 0)
                        {
                            response = new BaseResponse<bool>("007972");
                            return response;
                        }
                    }

                    #endregion

                    #region 父振动信号类型被禁用，特征值类型不能启用 王龙杰 2017-10-09

                    if (param.IsUsable == 1)
                    {
                        int vibratingSingalTypeIsUsable = vibratingSingalTypeRepository.GetDatas<VibratingSingalType>
                                        (item => item.ID == eigenValueType.VibratingSignalTypeID, true).
                                        Select(s => s.IsUsable).FirstOrDefault();
                        if (vibratingSingalTypeIsUsable == 0)
                        {
                            //父振动信号类型被禁用，特征值类型不能启用
                            response = new BaseResponse<bool>("009862");
                            return response;
                        }
                    }

                    #endregion

                    #region 同一振动信号类型下特征值类型不能全部禁用 王龙杰 2017-10-09

                    var elseUsed = eigenValueTypeRepository.GetDatas<EigenValueType>(t =>
                                       t.VibratingSignalTypeID == eigenValueType.VibratingSignalTypeID &&
                                       t.IsUsable == 1 && t.ID != eigenValueType.ID, true).Any();
                    if (!elseUsed && param.IsUsable == 0)
                    {
                        //同一振动信号类型下特征值类型不能全部禁用
                        response = new BaseResponse<bool>("009922");
                        return response;
                    }

                    #endregion

                    eigenValueType.Name = param.Name;
                    eigenValueType.Describe = param.Describe;
                    eigenValueType.IsUsable = param.IsUsable;
                    eigenValueType.IsDefault = param.IsDefault;
                    OperationResult operationResult = context.EigenValueType.Update(context, eigenValueType);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        if (IsUsableChenged)
                        {
                            #region 可用状态发生改变，变为可用，对应振动信号可用，变为不可用，对应振动信号不变

                            if (eigenValueType.IsUsable == 1)
                            {
                                var signalType = context.VibratingSingalType.GetByKey(context, eigenValueType.VibratingSignalTypeID);
                                if (signalType != null && signalType.IsUsable != 1)
                                {
                                    signalType.IsUsable = 1;
                                    context.VibratingSingalType.Update(context, signalType);

                                    //更新缓存
                                    cacheDICT.GetInstance().UpdateCacheType<VibratingSingalType>(context);
                                }
                            }

                            #endregion

                            #region 修改对应系统参数

                            var config = cacheDICT.GetInstance().GetCacheType<Config>(a => a.CommonDataType == 6 && a.CommonDataCode == eigenValueType.Code).ToList();
                            foreach (var c in config)
                            {
                                c.IsUsed = eigenValueType.IsUsable;
                                context.Config.Update(context, c);
                            }
                            //更新缓存
                            cacheDICT.GetInstance().UpdateCacheType<Config>(context);

                            #endregion
                        }

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<EigenValueType>(context);

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003381");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003381");
                return response;
            }
        }

        #endregion

        #region 删除特征值类型

        public BaseResponse<bool> DeleteEigenTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                var eigenType = eigenValueTypeRepository.GetByKey(param.ID);
                if (eigenType == null)
                {
                    response = new BaseResponse<bool>("003371");
                    return response;
                }
                if (eigenType != null && eigenType.IsDefault == (int)EnumCommonDataType.Default)
                {
                    response = new BaseResponse<bool>("003392");
                    return response;
                }
                if (IsExistAlmSet(param.ID))
                {
                    response = new BaseResponse<bool>("003402");
                    return response;
                }

                OperationResult operationResult = eigenValueTypeRepository.Delete<EigenValueType>(param.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //删除关联系统参数
                    operationResult = configRepository.Delete(p => p.CommonDataType == 6 && p.CommonDataCode == eigenType.Code);

                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<EigenValueType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003411");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003411");
                return response;
            }
        }

        private bool IsExistAlmSet(int id)
        {
            var isExistSignalAlmSet = signalAlmSetRepository.GetDatas<SignalAlmSet>(t => t.ValueType == id, true).Any();
            return isExistSignalAlmSet;
        }

        #endregion

        #endregion

        #region 通用数据波长

        #region  获取通用数据波长类型接口

        public BaseResponse<CommonDataResult> GetWaveLengthTypeDataForCommon(GetComSetDataParameter param)
        {
            CommonDataResult result = new CommonDataResult();
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();

            try
            {
                string describe = "WireLessSensor_WaveLength";
                switch (param.DevFormType)
                {
                    case 1:
                        {
                            describe = "WireLessSensor_WaveLength";
                            break;
                        }

                    case 3:
                        {
                            describe = "Triaxial_WaveLength";
                            break;
                        }

                    default:
                        {
                            describe = "WireLessSensor_WaveLength";
                            break;
                        }
                }
                List<WaveLengthValues> waveLengthVales = cacheDICT.GetInstance()
                    .GetCacheType<WaveLengthValues>(t => t.VibratingSignalTypeID == param.ParentID
                        && t.Describe == describe)
                    .ToList();
                foreach (var waveLengthVal in waveLengthVales)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = waveLengthVal.ID,
                        Code = waveLengthVal.Code,
                        //振动信号ID
                        PID = param.ParentID,
                        Name = waveLengthVal.WaveLengthValue.ToString(),
                        Describe = waveLengthVal.Describe,
                        IsUsable = waveLengthVal.IsUsable,
                        IsDefault = waveLengthVal.IsDefault,
                        AddDate = waveLengthVal.AddDate.ToString(),
                        OrderNo = waveLengthVal.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003421");
                return response;
            }
        }

        #endregion

        #region 获取通用数据特征值波长类型接口

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-11
        /// 创建记录：获取通用数据特征值波长类型
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<CommonDataResult> GetEigenWaveLengthTypeDataForCommon(GetComSetDataParameter param)
        {
            CommonDataResult result = new CommonDataResult();
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();

            try
            {
                string describe = "WireLessSensor_EigenWaveLength";
                switch (param.DevFormType)
                {
                    case 1:
                        {
                            describe = "WireLessSensor_EigenWaveLength";
                            break;
                        }

                    case 3:
                        {
                            describe = "Triaxial_EigenWaveLength";
                            break;
                        }

                    default:
                        {
                            describe = "WireLessSensor_EigenWaveLength";
                            break;
                        }
                }
                List<WaveLengthValues> waveLengthVales = cacheDICT.GetInstance()
                    .GetCacheType<WaveLengthValues>(t => t.VibratingSignalTypeID == param.ParentID
                        && t.Describe == describe)
                    .ToList();
                foreach (var waveLengthVal in waveLengthVales)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = waveLengthVal.ID,
                        Code = waveLengthVal.Code,
                        //振动信号ID
                        PID = param.ParentID,
                        Name = waveLengthVal.WaveLengthValue.ToString(),
                        Describe = waveLengthVal.Describe,
                        IsUsable = waveLengthVal.IsUsable,
                        IsDefault = waveLengthVal.IsDefault,
                        AddDate = waveLengthVal.AddDate.ToString(),
                        OrderNo = waveLengthVal.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003421");
                return response;
            }
        }

        #endregion

        #region  新增波长类型

        public BaseResponse<bool> AddWaveLengthTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    WaveLengthValues waveLengthType = new WaveLengthValues();
                    waveLengthType.Code = Guid.NewGuid().ToString();
                    waveLengthType.VibratingSignalTypeID = param.ParentID;
                    waveLengthType.WaveLengthValue = Convert.ToInt32(param.Name);
                    //waveLengthType.Describe = param.Describe;
                    waveLengthType.IsUsable = param.IsUsable;
                    waveLengthType.IsDefault = param.IsDefault;
                    #region 区分设备形态的波长
                    if (!param.DevFormType.HasValue)
                    {
                        waveLengthType.Describe = "WireLessSensor_WaveLength";
                    }
                    else
                    {
                        switch (param.DevFormType.Value)
                        {
                            case 1:
                                waveLengthType.Describe = "WireLessSensor_WaveLength";
                                break;
                            case 2:
                                //无
                                break;
                            case 3:
                                waveLengthType.Describe = "Triaxial_WaveLength";
                                break;
                        }
                    }
                    #endregion
                    int? orderNoWaveLengthValues = dbContext.WaveLengthValues
                        .Where(w => w.Describe.Equals(waveLengthType.Describe)
                            && w.VibratingSignalTypeID == param.ParentID)
                        .Select(s => s.OrderNo)
                        .Max();
                    if (orderNoWaveLengthValues.HasValue)
                        waveLengthType.OrderNo = orderNoWaveLengthValues.Value + 1;
                    else
                        waveLengthType.OrderNo = 1;
                    OperationResult operationResult = dbContext.WaveLengthValues.AddNew<WaveLengthValues>(dbContext, waveLengthType);

                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<WaveLengthValues>();
                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003431");
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003431");
                return response;
            }
        }

        #endregion

        #region  新增特征值波长类型
        /// <summary>
        /// Added by QXM, 2018/05/11
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> AddEigenValueWaveLengthTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    WaveLengthValues waveLengthType = new WaveLengthValues();
                    waveLengthType.Code = Guid.NewGuid().ToString();
                    waveLengthType.VibratingSignalTypeID = param.ParentID;
                    waveLengthType.WaveLengthValue = Convert.ToInt32(param.Name);
                    waveLengthType.Describe = param.Describe;
                    waveLengthType.IsUsable = param.IsUsable;
                    waveLengthType.IsDefault = param.IsDefault;

                    #region 区分设备形态的波长
                    if (!param.DevFormType.HasValue)
                    {
                        waveLengthType.Describe = "WireLessSensor_EigenWaveLength";
                    }
                    else
                    {
                        switch (param.DevFormType.Value)
                        {
                            case 1:
                                waveLengthType.Describe = "WireLessSensor_EigenWaveLength";
                                break;
                            case 2:
                                //无
                                break;
                            case 3:
                                waveLengthType.Describe = "Triaxial_EigenWaveLength";
                                break;
                        }
                    }
                    #endregion

                    int? orderNoWaveLengthValues = dbContext.WaveLengthValues.Where(w => w.Describe.Equals(waveLengthType.Describe) && w.VibratingSignalTypeID == param.ParentID).
                                                                          Select(s => s.OrderNo).Max();
                    if (orderNoWaveLengthValues.HasValue)
                        waveLengthType.OrderNo = orderNoWaveLengthValues.Value + 1;
                    else
                        waveLengthType.OrderNo = 1;
                    OperationResult operationResult = dbContext.WaveLengthValues.AddNew<WaveLengthValues>(dbContext, waveLengthType);

                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<WaveLengthValues>();
                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003431");
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region 编辑波长类型

        public BaseResponse<bool> EditWaveLengthTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                var waveLengthType = cacheDICT.GetInstance().GetCacheType<WaveLengthValues>(p => p.ID == param.ID).SingleOrDefault();
                if (waveLengthType == null)
                {
                    response = new BaseResponse<bool>("003442");
                    return response;
                }

                #region 波长类型如果已经被使用，则不能进行停用 王颖辉 2017-02-20

                if (param.IsUsable == 0)
                {
                    int count = vibSignalRepository.GetDatas<VibSingal>(item => item.WaveDataLength == param.ID, true).Count();
                    if (count > 0)
                    {
                        response = new BaseResponse<bool>("007982");
                        return response;
                    }
                }

                #endregion

                #region 父振动信号类型被禁用，波长类型不能启用 王龙杰 2017-10-09

                if (param.IsUsable == 1)
                {
                    int vibratingSingalTypeIsUsable = vibratingSingalTypeRepository.GetDatas<VibratingSingalType>
                                    (item => item.ID == waveLengthType.VibratingSignalTypeID, true).
                                    Select(s => s.IsUsable).FirstOrDefault();
                    if (vibratingSingalTypeIsUsable == 0)
                    {
                        //父振动信号类型被禁用，波长类型不能启用
                        response = new BaseResponse<bool>("009862");
                        return response;
                    }
                }

                #endregion

                #region 同一振动信号类型下波长类型不能全部禁用 王龙杰 2017-10-09

                var elseUsed = waveLengthValuesRepository.GetDatas<WaveLengthValues>(t =>
                                   t.VibratingSignalTypeID == waveLengthType.VibratingSignalTypeID &&
                                   t.IsUsable == 1 && t.ID != waveLengthType.ID, true).Any();
                if (!elseUsed && param.IsUsable == 0)
                {
                    //同一振动信号类型下波长类型不能全部禁用
                    response = new BaseResponse<bool>("009932");
                    return response;
                }

                #endregion

                waveLengthType.WaveLengthValue = Convert.ToInt32(param.Name);
                waveLengthType.Describe = param.Describe;
                waveLengthType.IsUsable = param.IsUsable;
                waveLengthType.IsDefault = param.IsDefault;
                OperationResult operationResult = waveLengthValuesRepository.Update<WaveLengthValues>(waveLengthType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WaveLengthValues>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003451");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003451");
                return response;
            }
        }

        #endregion

        #region 删除波长类型

        public BaseResponse<bool> DeleteWaveLengthTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultWaveLetngth(param.ID))
                {
                    response = new BaseResponse<bool>("003462");
                    return response;
                }

                if (IsExistedVibSignal(param.ID))
                {
                    response = new BaseResponse<bool>("003472");
                    return response;
                }

                OperationResult operationResult = waveLengthValuesRepository.Delete<WaveLengthValues>(param.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WaveLengthValues>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003481");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003481");
                return response;
            }
        }

        #endregion

        private bool IsDefaultWaveLetngth(int id)
        {
            var waveLengthType = waveLengthValuesRepository.GetByKey(id);
            if (waveLengthType != null && waveLengthType.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsExistedVibSignal(int id)
        {
            var isExistVibSignal = vibSignalRepository.GetDatas<VibSingal>(t => t.WaveDataLength == id, true).Any();
            return isExistVibSignal;
        }

        #endregion

        #region 通用数据下限频率

        #region 获取通用数据波长下限频率类型接口

        public BaseResponse<CommonDataResult> GetWaveLowerLimitTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();
            CommonDataResult result = new CommonDataResult();

            try
            {
                string describe = "WireLessSensor_LowLimit";
                switch (param.DevFormType)
                {
                    case 1:
                        {
                            describe = "WireLessSensor_LowLimit";
                            break;
                        }

                    case 2:
                        {
                            describe = "WiredSensor_LowLimit";
                            break;
                        }

                    case 3:
                        {
                            describe = "Triaxial_LowLimit";
                            break;
                        }

                    default:
                        {
                            describe = "WireLessSensor_LowLimit";
                            break;
                        }
                }
                List<WaveLowerLimitValues> waveLowers = cacheDICT.GetInstance()
                    .GetCacheType<WaveLowerLimitValues>(t => t.VibratingSignalTypeID == param.ParentID
                        && t.Describe == describe)
                    .ToList();
                foreach (var waveLower in waveLowers)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = waveLower.ID,
                        Code = waveLower.Code,
                        PID = param.ParentID,
                        Name = waveLower.WaveLowerLimitValue.ToString(),
                        Describe = waveLower.Describe,
                        IsUsable = waveLower.IsUsable,
                        IsDefault = waveLower.IsDefault,
                        AddDate = waveLower.AddDate.ToString(),
                        OrderNo = waveLower.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }

                response = new BaseResponse<CommonDataResult>();
                result.CommonInfoList = commonInfoList;
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003561");
                return response;
            }
        }

        #endregion

        #region 添加波长下限频率类型

        public BaseResponse<bool> AddWaveLowerLimitTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                WaveLowerLimitValues waveLowerLimitType = new WaveLowerLimitValues();
                waveLowerLimitType.VibratingSignalTypeID = param.ParentID;
                waveLowerLimitType.Code = Guid.NewGuid().ToString();
                waveLowerLimitType.WaveLowerLimitValue = Convert.ToInt32(param.Name);
                waveLowerLimitType.Describe = param.Describe;
                waveLowerLimitType.IsUsable = param.IsUsable;
                waveLowerLimitType.IsDefault = param.IsDefault;

                #region  区分设备形态, Added by QXM, 2018/05/11
                if (!param.DevFormType.HasValue)
                {
                    waveLowerLimitType.Describe = "WireLessSensor_LowLimit";
                }
                else
                {
                    switch (param.DevFormType.Value)
                    {
                        case (int)EnumWSFormType.WireLessSensor:
                            waveLowerLimitType.Describe = "WireLessSensor_LowLimit";
                            break;
                        case (int)EnumWSFormType.WiredSensor:
                            waveLowerLimitType.Describe = "WiredSensor_LowLimit";
                            break;
                        case (int)EnumWSFormType.Triaxial:
                            waveLowerLimitType.Describe = "Triaxial_LowLimit";
                            break;
                    }
                }
                #endregion

                int? orderNoWaveLowerLimitValues = new iCMSDbContext().WaveLowerLimitValues.
                                                                            Where(w => w.Describe.Equals(waveLowerLimitType.Describe) && w.VibratingSignalTypeID == param.ParentID).
                                                                            Select(s => s.OrderNo).Max();
                if (orderNoWaveLowerLimitValues.HasValue)
                    waveLowerLimitType.OrderNo = orderNoWaveLowerLimitValues.Value + 1;
                else
                    waveLowerLimitType.OrderNo = 1;
                OperationResult operationResult = waveLowerLimitValuesRepository.AddNew<WaveLowerLimitValues>(waveLowerLimitType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WaveLowerLimitValues>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003571");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003571");
                return response;
            }
        }

        #endregion

        #region 编辑波长下限频率类型

        /// <summary>
        /// 可用状态发生改变，变为可用，对应振动信号可用，变为不可用，对应振动信号不变
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditWaveLowerLimitTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    var waveLowerLimitType = cacheDICT.GetInstance(context).GetCacheType<WaveLowerLimitValues>(p => p.ID == param.ID).SingleOrDefault();
                    if (waveLowerLimitType == null)
                    {
                        response = new BaseResponse<bool>("003582");
                        return response;
                    }

                    #region 波长类型如果已经被使用，则不能进行停用 王颖辉 2017-02-20

                    if (param.IsUsable == 0)
                    {
                        int count = vibSignalRepository.GetDatas<VibSingal>(item => item.LowLimitFrequency == param.ID, true).Count();
                        if (count > 0)
                        {
                            response = new BaseResponse<bool>("008002");
                            return response;
                        }
                    }

                    #endregion

                    #region 父振动信号类型被禁用，下限频率类型不能启用 王龙杰 2017-10-09

                    if (param.IsUsable == 1)
                    {
                        int vibratingSingalTypeIsUsable = vibratingSingalTypeRepository.GetDatas<VibratingSingalType>
                                        (item => item.ID == waveLowerLimitType.VibratingSignalTypeID, true).
                                        Select(s => s.IsUsable).FirstOrDefault();
                        if (vibratingSingalTypeIsUsable == 0)
                        {
                            //父振动信号类型被禁用，下限频率类型不能启用
                            response = new BaseResponse<bool>("009862");
                            return response;
                        }
                    }
                    #endregion

                    #region 同一振动信号类型下下限频率类型不能全部禁用 王龙杰 2017-10-09

                    var elseUsed = waveLowerLimitValuesRepository.GetDatas<WaveLowerLimitValues>(t =>
                                       t.VibratingSignalTypeID == waveLowerLimitType.VibratingSignalTypeID &&
                                       t.IsUsable == 1 && t.ID != waveLowerLimitType.ID, true).Any();
                    if (!elseUsed && param.IsUsable == 0)
                    {
                        //同一振动信号类型下下限频率类型不能全部禁用
                        response = new BaseResponse<bool>("009942");
                        return response;
                    }

                    #endregion

                    waveLowerLimitType.WaveLowerLimitValue = Convert.ToInt32(param.Name);
                    waveLowerLimitType.Describe = param.Describe;
                    waveLowerLimitType.IsUsable = param.IsUsable;
                    waveLowerLimitType.IsDefault = param.IsDefault;
                    OperationResult operationResult = context.WaveLowerLimitValues.Update(context, waveLowerLimitType);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        if (waveLowerLimitType.IsUsable == 1)
                        {

                            var vibSignalType = context.VibratingSingalType.GetByKey(context, waveLowerLimitType.VibratingSignalTypeID);
                            if (vibSignalType != null && vibSignalType.IsUsable != 1)
                            {
                                vibSignalType.IsUsable = 1;
                                context.VibratingSingalType.Update(context, vibSignalType);

                                //更新缓存
                                cacheDICT.GetInstance().UpdateCacheType<VibratingSingalType>(context);
                            }
                        }

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<WaveLowerLimitValues>(context);

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003591");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003591");
                return response;
            }
        }
        #endregion

        #region 删除波长下限频率类型

        public BaseResponse<bool> DeleteWaveLowerLimitTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultWaveLowerLimit(param.ID))
                {
                    response = new BaseResponse<bool>("003602");
                    return response;
                }
                if (IsExistVibSignalViaWaveLowerLimit(param.ID))
                {
                    response = new BaseResponse<bool>("003612");
                    return response;
                }

                OperationResult operationResult = waveLowerLimitValuesRepository.Delete<WaveLowerLimitValues>(param.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WaveLowerLimitValues>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003621");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003621");
                return response;
            }
        }

        private bool IsDefaultWaveLowerLimit(int id)
        {
            var waveLowerLimit = waveLowerLimitValuesRepository.GetByKey(id);
            if (waveLowerLimit != null && waveLowerLimit.IsDefault == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsExistVibSignalViaWaveLowerLimit(int id)
        {
            //判断此波长上限的SignalType判断其振动信号类型
            //如果是包络，则判断其EnlvpBandW是否存在
            //其他振动类型，则判断其 UpLimitFrequency
            bool isExistVibSignal = false;
            var signalType = waveLowerLimitValuesRepository.GetByKey(id).VibratingSignalTypeID;
            if (signalType == 4)
            {
                isExistVibSignal = vibSignalRepository.GetDatas<VibSingal>(t => t.EnlvpFilter == id, true).Any();
            }
            else
            {
                isExistVibSignal = vibSignalRepository.GetDatas<VibSingal>(t => t.LowLimitFrequency == id, true).Any();
            }

            return isExistVibSignal;
        }

        #endregion

        #endregion

        #region 通用数据 - 包络滤波器上限

        #region 获取包络滤波器上限频率
        /// <summary>
        /// 获取包络滤波器上限
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<CommonDataResult> GetEnvlFilterUpperTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            CommonDataResult result = new CommonDataResult();
            List<CommonInfo> commonInfoList = new List<CommonInfo>();

            try
            {
                List<WaveUpperLimitValues> envlFilerUpperTypes = cacheDICT.GetInstance()
                    .GetCacheType<WaveUpperLimitValues>(t => t.VibratingSignalTypeID == param.ParentID
                        && t.Describe.Equals("WiredSensor_EnvlFilterUpLimit"))
                    .ToList();
                foreach (var waveUpper in envlFilerUpperTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = waveUpper.ID,
                        Code = waveUpper.Code,
                        PID = param.ParentID,
                        Name = waveUpper.WaveUpperLimitValue.ToString(),
                        Describe = waveUpper.Describe,
                        IsUsable = waveUpper.IsUsable,
                        IsDefault = waveUpper.IsDefault,
                        AddDate = waveUpper.AddDate.ToString(),
                        OrderNo = waveUpper.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }
                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003491");
                return response;
            }
        }

        #endregion

        #region 添加包络滤波器上限
        public BaseResponse<bool> AddEnvlFilterUpperTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                WaveUpperLimitValues waveUpperLimitType = new WaveUpperLimitValues();
                waveUpperLimitType.VibratingSignalTypeID = param.ParentID;
                waveUpperLimitType.Code = Guid.NewGuid().ToString();
                waveUpperLimitType.WaveUpperLimitValue = Convert.ToInt32(param.Name);
                waveUpperLimitType.Describe = param.Describe;

                #region  区分设备形态, Added by QXM, 2018/05/11
                if (!param.DevFormType.HasValue)
                {
                    //waveUpperLimitType.Describe = "WireLessSensor_UpLimit";
                }
                else
                {
                    switch (param.DevFormType.Value)
                    {
                        case (int)EnumWSFormType.WireLessSensor:
                            //waveUpperLimitType.Describe = "WireLessSensor_UpLimit";
                            break;
                        case (int)EnumWSFormType.WiredSensor:
                            waveUpperLimitType.Describe = "WiredSensor_EnvlFilterUpLimit";
                            break;
                        case (int)EnumWSFormType.Triaxial:
                            //waveUpperLimitType.Describe = "Triaxial_UpLimit";
                            break;
                    }
                }
                #endregion

                waveUpperLimitType.IsUsable = param.IsUsable;
                waveUpperLimitType.IsDefault = param.IsDefault;
                int? orderNoWaveUpperLimitValues = new iCMSDbContext().WaveUpperLimitValues.
                                                                            Where(w => w.Describe.Equals(waveUpperLimitType.Describe) && w.VibratingSignalTypeID == param.ParentID).
                                                                            Select(s => s.OrderNo).Max();
                if (orderNoWaveUpperLimitValues.HasValue)
                    waveUpperLimitType.OrderNo = orderNoWaveUpperLimitValues.Value + 1;
                else
                    waveUpperLimitType.OrderNo = 1;
                OperationResult operationResult = waveUpperLimitValuesRepository.AddNew<WaveUpperLimitValues>(waveUpperLimitType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WaveUpperLimitValues>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003501");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003501");
                return response;
            }
        }
        #endregion

        #region 编辑包络滤波器上限
        public BaseResponse<bool> EditEnvlFilterUpperType(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    var waveUpperLimitType = cacheDICT.GetInstance(context).GetCacheType<WaveUpperLimitValues>(p => p.ID == param.ID).SingleOrDefault();
                    if (waveUpperLimitType == null)
                    {
                        response = new BaseResponse<bool>("003512");
                        return response;
                    }

                    waveUpperLimitType.WaveUpperLimitValue = Convert.ToInt32(param.Name);
                    waveUpperLimitType.Describe = param.Describe;
                    waveUpperLimitType.IsUsable = param.IsUsable;
                    waveUpperLimitType.IsDefault = param.IsDefault;
                    OperationResult operationResult = context.WaveUpperLimitValues.Update(context, waveUpperLimitType);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        #region  可用状态发生改变，变为可用，对应振动信号可用，变为不可用

                        if (waveUpperLimitType.IsUsable == 1)
                        {

                            var vibSignalType = context.VibratingSingalType.GetByKey(context, waveUpperLimitType.VibratingSignalTypeID);
                            if (vibSignalType != null && vibSignalType.IsUsable != 1)
                            {
                                vibSignalType.IsUsable = 1;
                                context.VibratingSingalType.Update(context, vibSignalType);

                                //更新缓存
                                cacheDICT.GetInstance().UpdateCacheType<VibratingSingalType>(context);
                            }
                        }

                        #endregion

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<WaveUpperLimitValues>(context);

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003521");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region 删除包络滤波器上限
        public BaseResponse<bool> DeleteEnvlFilterUpperType(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            OperationResult operationResult = waveUpperLimitValuesRepository.Delete<WaveUpperLimitValues>(param.ID);
            if (operationResult.ResultType == EnumOperationResultType.Success)
            {
                //更新缓存
                cacheDICT.GetInstance().UpdateCacheType<WaveUpperLimitValues>();
                response = new BaseResponse<bool>();
                return response;
            }
            else
            {
                response = new BaseResponse<bool>("003551");
                return response;
            }
        }
        #endregion
        #endregion

        #region 通用数据 - 包络滤波器下限

        #region 获取包络滤波器下限频率
        /// <summary>
        /// 获取包络滤波器下限
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<CommonDataResult> GetEnvlFilterLowerTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            CommonDataResult result = new CommonDataResult();
            List<CommonInfo> commonInfoList = new List<CommonInfo>();

            try
            {
                List<WaveLowerLimitValues> envlFilerUpperTypes = cacheDICT.GetInstance()
                    .GetCacheType<WaveLowerLimitValues>(t => t.VibratingSignalTypeID == param.ParentID
                        && t.Describe.Equals("WiredSensor_EnvlFilterLowLimit"))
                    .ToList();
                foreach (var waveUpper in envlFilerUpperTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = waveUpper.ID,
                        Code = waveUpper.Code,
                        PID = param.ParentID,
                        Name = waveUpper.WaveLowerLimitValue.ToString(),
                        Describe = waveUpper.Describe,
                        IsUsable = waveUpper.IsUsable,
                        IsDefault = waveUpper.IsDefault,
                        AddDate = waveUpper.AddDate.ToString(),
                        OrderNo = waveUpper.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }
                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003491");
                return response;
            }
        }

        #endregion

        #region 添加包络滤波器下限
        public BaseResponse<bool> AddEnvlFilterLowerTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                WaveLowerLimitValues waveLowerLimitType = new WaveLowerLimitValues();
                waveLowerLimitType.VibratingSignalTypeID = param.ParentID;
                waveLowerLimitType.Code = Guid.NewGuid().ToString();
                waveLowerLimitType.WaveLowerLimitValue = Convert.ToInt32(param.Name);
                waveLowerLimitType.Describe = param.Describe;
                waveLowerLimitType.IsUsable = param.IsUsable;
                waveLowerLimitType.IsDefault = param.IsDefault;

                #region  区分设备形态, Added by QXM, 2018/05/11
                if (!param.DevFormType.HasValue)
                {
                    waveLowerLimitType.Describe = "WireLessSensor_LowLimit";
                }
                else
                {
                    switch (param.DevFormType.Value)
                    {
                        case (int)EnumWSFormType.WireLessSensor:

                            break;
                        case (int)EnumWSFormType.WiredSensor:
                            waveLowerLimitType.Describe = "WiredSensor_EnvlFilterLowLimit";
                            break;
                        case (int)EnumWSFormType.Triaxial:
                            break;
                    }
                }
                #endregion

                int? orderNoWaveLowerLimitValues = new iCMSDbContext().WaveLowerLimitValues.
                                                                            Where(w => w.Describe.Equals(waveLowerLimitType.Describe) && w.VibratingSignalTypeID == param.ParentID).
                                                                            Select(s => s.OrderNo).Max();
                if (orderNoWaveLowerLimitValues.HasValue)
                    waveLowerLimitType.OrderNo = orderNoWaveLowerLimitValues.Value + 1;
                else
                    waveLowerLimitType.OrderNo = 1;
                OperationResult operationResult = waveLowerLimitValuesRepository.AddNew<WaveLowerLimitValues>(waveLowerLimitType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WaveLowerLimitValues>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003571");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003571");
                return response;
            }
        }
        #endregion

        #region 编辑包络滤波器下限
        public BaseResponse<bool> EditEnvlFilterLowerType(EditComSetDataParameter para)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                ExecuteDB.ExecuteTrans((dataContext) =>
                {
                    WaveLowerLimitValues envlFilterLowerType = dataContext.WaveLowerLimitValues.Where(t => t.ID == para.ID).FirstOrDefault();
                    if (envlFilterLowerType == null)
                    {
                        throw new Exception();
                    }

                    envlFilterLowerType.WaveLowerLimitValue = Convert.ToInt32(para.Name);
                    envlFilterLowerType.Describe = para.Describe;
                    envlFilterLowerType.IsUsable = para.IsUsable;
                    envlFilterLowerType.IsDefault = para.IsDefault;

                    OperationResult operationResult = dataContext.WaveLowerLimitValues.Update(dataContext, envlFilterLowerType);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        ///TODO:
                    }
                    else
                    {
                        throw new Exception();
                    }
                });

                return response;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region 删除包络滤波器下限
        public BaseResponse<bool> DeleteEnvlFilterLowerType(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            OperationResult operationResult = waveLowerLimitValuesRepository.Delete<WaveLowerLimitValues>(param.ID);
            if (operationResult.ResultType == EnumOperationResultType.Success)
            {
                //更新缓存
                cacheDICT.GetInstance().UpdateCacheType<WaveLowerLimitValues>();
                response = new BaseResponse<bool>();
                return response;
            }
            else
            {
                response = new BaseResponse<bool>("003551");
                return response;
            }
        }
        #endregion
        #endregion

        #region 通用数据上限频率

        #region 获取通用数据波长上限频率类型接口

        public BaseResponse<CommonDataResult> GetWaveUpperLimitTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();
            CommonDataResult result = new CommonDataResult();

            try
            {
                string describe = "WireLessSensor_UpLimit";
                switch (param.DevFormType)
                {
                    case 1:
                        {
                            describe = "WireLessSensor_UpLimit";
                            break;
                        }

                    case 2:
                        {
                            describe = "WiredSensor_UpLimit";
                            break;
                        }

                    case 3:
                        {
                            describe = "Triaxial_UpLimit";
                            break;
                        }

                    default:
                        {
                            describe = "WireLessSensor_UpLimit";
                            break;
                        }
                }
                List<WaveUpperLimitValues> waveUppers = cacheDICT.GetInstance()
                    .GetCacheType<WaveUpperLimitValues>(t => t.VibratingSignalTypeID == param.ParentID
                        && t.Describe == describe)
                    .ToList();
                foreach (var waveUpper in waveUppers)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = waveUpper.ID,
                        Code = waveUpper.Code,
                        PID = param.ParentID,
                        Name = waveUpper.WaveUpperLimitValue.ToString(),
                        Describe = waveUpper.Describe,
                        IsUsable = waveUpper.IsUsable,
                        IsDefault = waveUpper.IsDefault,
                        AddDate = waveUpper.AddDate.ToString(),
                        OrderNo = waveUpper.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }
                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003491");
                return response;
            }
        }

        #endregion

        #region 添加波长上限

        public BaseResponse<bool> AddWaveUpperLimitTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                WaveUpperLimitValues waveUpperLimitType = new WaveUpperLimitValues();
                waveUpperLimitType.VibratingSignalTypeID = param.ParentID;
                waveUpperLimitType.Code = Guid.NewGuid().ToString();
                waveUpperLimitType.WaveUpperLimitValue = Convert.ToInt32(param.Name);
                waveUpperLimitType.Describe = param.Describe;

                #region  区分设备形态, Added by QXM, 2018/05/11
                if (!param.DevFormType.HasValue)
                {
                    waveUpperLimitType.Describe = "WireLessSensor_UpLimit";
                }
                else
                {
                    switch (param.DevFormType.Value)
                    {
                        case (int)EnumWSFormType.WireLessSensor:
                            waveUpperLimitType.Describe = "WireLessSensor_UpLimit";
                            break;
                        case (int)EnumWSFormType.WiredSensor:
                            waveUpperLimitType.Describe = "WiredSensor_UpLimit";
                            break;
                        case (int)EnumWSFormType.Triaxial:
                            waveUpperLimitType.Describe = "Triaxial_UpLimit";
                            break;
                    }
                }
                #endregion

                waveUpperLimitType.IsUsable = param.IsUsable;
                waveUpperLimitType.IsDefault = param.IsDefault;
                int? orderNoWaveUpperLimitValues = new iCMSDbContext().WaveUpperLimitValues.
                                                                            Where(w => w.Describe.Equals(waveUpperLimitType.Describe) && w.VibratingSignalTypeID == param.ParentID).
                                                                            Select(s => s.OrderNo).Max();
                if (orderNoWaveUpperLimitValues.HasValue)
                    waveUpperLimitType.OrderNo = orderNoWaveUpperLimitValues.Value + 1;
                else
                    waveUpperLimitType.OrderNo = 1;
                OperationResult operationResult = waveUpperLimitValuesRepository.AddNew<WaveUpperLimitValues>(waveUpperLimitType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WaveUpperLimitValues>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003501");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003501");
                return response;
            }
        }

        #endregion

        #region 编辑波长上限

        /// <summary>
        /// 可用状态发生改变，变为可用，对应振动信号可用，变为不可用
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditWaveUpperLimitTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    var waveUpperLimitType = cacheDICT.GetInstance(context).GetCacheType<WaveUpperLimitValues>(p => p.ID == param.ID).SingleOrDefault();
                    if (waveUpperLimitType == null)
                    {
                        response = new BaseResponse<bool>("003512");
                        return response;
                    }

                    #region 上限类型如果已经被使用，则不能进行停用 王颖辉 2017-02-20

                    if (param.IsUsable == 0)
                    {
                        int count = vibSignalRepository.GetDatas<VibSingal>(item => item.UpLimitFrequency == param.ID, true).Count();
                        if (count > 0)
                        {
                            response = new BaseResponse<bool>("007992");
                            return response;
                        }
                    }

                    #endregion

                    #region 父振动信号类型被禁用，上限频率类型不能启用 王龙杰 2017-10-09

                    if (param.IsUsable == 1)
                    {
                        int vibratingSingalTypeIsUsable = vibratingSingalTypeRepository.GetDatas<VibratingSingalType>
                                        (item => item.ID == waveUpperLimitType.VibratingSignalTypeID, true).
                                        Select(s => s.IsUsable).FirstOrDefault();
                        if (vibratingSingalTypeIsUsable == 0)
                        {
                            //父振动信号类型被禁用，上限频率类型不能启用
                            response = new BaseResponse<bool>("009862");
                            return response;
                        }
                    }

                    #endregion

                    #region 同一振动信号类型下上限频率类型不能全部禁用 王龙杰 2017-10-09

                    var elseUsed = waveUpperLimitValuesRepository.GetDatas<WaveUpperLimitValues>(t =>
                                       t.VibratingSignalTypeID == waveUpperLimitType.VibratingSignalTypeID &&
                                       t.IsUsable == 1 && t.ID != waveUpperLimitType.ID, true).Any();
                    if (!elseUsed && param.IsUsable == 0)
                    {
                        //同一振动信号类型下上限频率类型不能全部禁用
                        response = new BaseResponse<bool>("009952");
                        return response;
                    }

                    #endregion

                    waveUpperLimitType.WaveUpperLimitValue = Convert.ToInt32(param.Name);
                    waveUpperLimitType.Describe = param.Describe;
                    waveUpperLimitType.IsUsable = param.IsUsable;
                    waveUpperLimitType.IsDefault = param.IsDefault;
                    OperationResult operationResult = context.WaveUpperLimitValues.Update(context, waveUpperLimitType);
                    if (operationResult.ResultType == EnumOperationResultType.Success)
                    {
                        #region  可用状态发生改变，变为可用，对应振动信号可用，变为不可用

                        if (waveUpperLimitType.IsUsable == 1)
                        {

                            var vibSignalType = context.VibratingSingalType.GetByKey(context, waveUpperLimitType.VibratingSignalTypeID);
                            if (vibSignalType != null && vibSignalType.IsUsable != 1)
                            {
                                vibSignalType.IsUsable = 1;
                                context.VibratingSingalType.Update(context, vibSignalType);

                                //更新缓存
                                cacheDICT.GetInstance().UpdateCacheType<VibratingSingalType>(context);
                            }
                        }

                        #endregion

                        //更新缓存
                        cacheDICT.GetInstance().UpdateCacheType<WaveUpperLimitValues>(context);

                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("003521");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003521");
                return response;
            }
        }

        #endregion

        #region 删除波长上限

        public BaseResponse<bool> DeleteWaveUpperLimitTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultWaveUpperLimit(param.ID))
                {
                    response = new BaseResponse<bool>("003532");
                    return response;
                }
                if (IsExistVibSignal(param.ID))
                {
                    response = new BaseResponse<bool>("003542");
                    return response;
                }

                OperationResult operationResult = waveUpperLimitValuesRepository.Delete<WaveUpperLimitValues>(param.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WaveUpperLimitValues>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003551");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003551");
                return response;
            }
        }

        private bool IsDefaultWaveUpperLimit(int id)
        {
            var waveUpperLimit = waveUpperLimitValuesRepository.GetByKey(id);
            if (waveUpperLimit != null && waveUpperLimit.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsExistVibSignal(int id)
        {
            //判断此波长上限的SignalType判断其振动信号类型
            //如果是包络，则判断其EnlvpBandW是否存在
            //其他振动类型，则判断其 UpLimitFrequency
            bool isExistVibSignal = false;
            var signalType = waveUpperLimitValuesRepository.GetByKey(id).VibratingSignalTypeID;
            if (signalType == 4)
            {
                isExistVibSignal = vibSignalRepository.GetDatas<VibSingal>(t => t.EnlvpBandW == id, true).Any();
            }
            else
            {
                isExistVibSignal = vibSignalRepository.GetDatas<VibSingal>(t => t.UpLimitFrequency == id, true).Any();
            }

            return isExistVibSignal;
        }

        #endregion

        #endregion

        #region 通用数据传感器类型
        public BaseResponse<CommonDataResult> GetWSTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();
            CommonDataResult result = new CommonDataResult();

            try
            {
                List<SensorType> sensorTypes = cacheDICT.GetInstance().GetCacheType<SensorType>().ToList();
                foreach (var sensorType in sensorTypes)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = sensorType.ID,
                        Code = sensorType.Code,
                        PID = null,
                        Name = sensorType.Name,
                        Describe = sensorType.Describe,
                        IsUsable = sensorType.IsUsable,
                        IsDefault = sensorType.IsDefault,
                        AddDate = sensorType.AddDate.ToString(),
                        OrderNo = sensorType.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003651");
                return response;
            }
        }

        public BaseResponse<bool> AddWSTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                SensorType wsType = new SensorType();
                wsType.Code = Guid.NewGuid().ToString();
                wsType.Name = param.Name;
                wsType.Describe = param.Describe;
                wsType.IsUsable = param.IsUsable;
                wsType.IsDefault = param.IsDefault;
                int? orderNoSensorType = new iCMSDbContext().SensorType.Select(s => s.OrderNo).Max();
                if (orderNoSensorType.HasValue)
                    wsType.OrderNo = orderNoSensorType.Value + 1;
                else
                    wsType.OrderNo = 1;
                OperationResult operationResult = sensorTypeRepository.AddNew<SensorType>(wsType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<SensorType>();

                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003661");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003661");
                return response;
            }
        }

        public BaseResponse<bool> EditWSTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var wsType = cacheDICT.GetInstance().GetCacheType<SensorType>(p => p.ID == param.ID).SingleOrDefault();
                if (wsType == null)
                {
                    response = new BaseResponse<bool>("003672");
                    return response;
                }

                #region 无线传感器类型已经使用，则不能停用 王颖辉 2017-02-20
                if (param.IsUsable == 0)
                {
                    int count = wsRepository.GetDatas<WS>(item => item.SensorType == param.ID, true).Count();
                    if (count > 0)
                    {
                        response = new BaseResponse<bool>("008012");
                        return response;
                    }
                }

                #endregion

                #region 传感器类型不能全部禁用 王龙杰 2017-10-09
                var elseUsed = sensorTypeRepository.GetDatas<SensorType>(t =>
                                   t.IsUsable == 1 && t.ID != wsType.ID, true).Any();
                if (!elseUsed && param.IsUsable == 0)
                {
                    //传感器类型不能全部禁用
                    response = new BaseResponse<bool>("009972");
                    return response;
                }
                #endregion

                wsType.Name = param.Name;
                wsType.Describe = param.Describe;
                wsType.IsUsable = param.IsUsable;
                wsType.IsDefault = param.IsDefault;
                OperationResult operationResult = sensorTypeRepository.Update<SensorType>(wsType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<SensorType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003681");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003681");
                return response;
            }
        }

        public BaseResponse<bool> DeleteWSTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultWSType(param.ID))
                {
                    response = new BaseResponse<bool>("003692");
                    return response;
                }
                if (IsExistWS(param.ID))
                {
                    response = new BaseResponse<bool>("003702");
                    return response;
                }

                OperationResult operationResult = sensorTypeRepository.Delete<SensorType>(param.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<SensorType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003711");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003711");
                return response;
            }
        }

        private bool IsDefaultWSType(int id)
        {
            var wsType = sensorTypeRepository.GetByKey(id);
            if (wsType != null && wsType.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsExistWS(int id)
        {
            var isExist = wsRepository.GetDatas<WS>(t => t.SensorType == id, true).Any();
            return isExist;
        }
        #endregion

        #region 通用数据传感器挂靠个数
        public BaseResponse<CommonDataResult> GetWGTypeDataForCommon(GetComSetDataParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            CommonDataResult result = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();

            try
            {
                List<WirelessGatewayType> wgs = cacheDICT.GetInstance().GetCacheType<WirelessGatewayType>().ToList();
                foreach (var wg in wgs)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = wg.ID,
                        Code = wg.Code,
                        PID = null,
                        Name = wg.Name,
                        Describe = wg.Describe,
                        IsUsable = wg.IsUsable,
                        IsDefault = wg.IsDefault,
                        AddDate = wg.AddDate.ToString(),
                        OrderNo = wg.OrderNo
                    };

                    commonInfoList.Add(commonInfo);
                }

                result = new CommonDataResult { CommonInfoList = commonInfoList };
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003721");
                return response;
            }
        }

        public BaseResponse<bool> AddWGTypeData(AddComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                WirelessGatewayType wgType = new WirelessGatewayType();
                wgType.Code = Guid.NewGuid().ToString();
                wgType.Name = param.Name;
                wgType.Describe = param.Describe;
                wgType.IsUsable = param.IsUsable;
                wgType.IsDefault = param.IsDefault;
                int? orderNoWirelessGatewayType = new iCMSDbContext().WirelessGatewayType.Select(s => s.OrderNo).Max();
                if (orderNoWirelessGatewayType.HasValue)
                    wgType.OrderNo = orderNoWirelessGatewayType.Value + 1;
                else
                    wgType.OrderNo = 1;
                OperationResult operationResult = wirelessGatewayTypeRepository.AddNew<WirelessGatewayType>(wgType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WirelessGatewayType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003731");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003731");
                return response;
            }
        }

        public BaseResponse<bool> EditWGTypeData(EditComSetDataParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                var wgType = cacheDICT.GetInstance().GetCacheType<WirelessGatewayType>(p => p.ID == param.ID).SingleOrDefault();
                if (wgType == null)
                {
                    response = new BaseResponse<bool>("003742");
                    return response;
                }

                #region 如果无线网关类型使用，则不能停用 王颖辉 2017-02-20
                if (param.IsUsable == 0)
                {
                    int count = gatewayRepository.GetDatas<Gateway>(item => item.WGType == param.ID, true).Count();
                    if (count > 0)
                    {
                        response = new BaseResponse<bool>("008022");
                        return response;
                    }
                }
                #endregion

                #region 传感器挂靠个数不能全部禁用 王龙杰 2017-10-09
                var elseUsed = wirelessGatewayTypeRepository.GetDatas<WirelessGatewayType>(t =>
                                   t.IsUsable == 1 && t.ID != wgType.ID, true).Any();
                if (!elseUsed && param.IsUsable == 0)
                {
                    //传感器挂靠个数不能全部禁用
                    response = new BaseResponse<bool>("009962");
                    return response;
                }
                #endregion

                wgType.Name = param.Name;
                wgType.Describe = param.Describe;
                wgType.IsUsable = param.IsUsable;
                wgType.IsDefault = param.IsDefault;
                OperationResult operationResult = wirelessGatewayTypeRepository.Update<WirelessGatewayType>(wgType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WirelessGatewayType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003751");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003751");
                return response;
            }
        }

        public BaseResponse<bool> DeleteWGTypeData(DeleteComSetDataParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultWGType(param.ID))
                {
                    response = new BaseResponse<bool>("003762");
                    return response;
                }

                if (IsExistWG(param.ID))
                {
                    response = new BaseResponse<bool>("003772");
                    return response;
                }
                OperationResult operationResult = wirelessGatewayTypeRepository.Delete<WirelessGatewayType>(param.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<WirelessGatewayType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003781");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003781");
                return response;
            }
        }

        private bool IsDefaultWGType(int id)
        {
            var wgType = wirelessGatewayTypeRepository.GetByKey(id);
            if (wgType != null && wgType.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsExistWG(int id)
        {
            var isExistWG = gatewayRepository.GetDatas<Gateway>(t => t.WGType == id, true).Any();
            return isExistWG;
        }
        #endregion

        #region 共有方法
        /// <summary>
        /// 判断是否是系统默认，如果是系统默认，则禁止删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsDefaultModule(int id)
        {
            var module = moduleRepository.GetByKey(id);
            if (module.IsDeault == (int)EnumModuleDataType.Default)
            {
                return true;
            }
            return false;
        }

        private bool IsDefaultMSiteType(int id)
        {
            var msType = measureSiteTypeRepository.GetByKey(id);
            if (msType != null && msType.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            return false;
        }

        private bool IsDefaultVibType(int id)
        {
            var vibType = vibratingSingalTypeRepository.GetByKey(id);
            if (vibType != null && vibType.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsDefaultMSiteMTType(int id)
        {
            var msiteMonitorType = measureSiteMonitorTypeRepository.GetByKey(id);
            if (msiteMonitorType != null && msiteMonitorType.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetAllChildren(int moduleID, List<int> childrenIDs)
        {
            childrenIDs.Add(moduleID);
            var childNodes = moduleRepository
                .GetDatas<Module>(m => m.ParID == moduleID, false)
                .ToList();

            foreach (var mt in childNodes)
            {
                //找到包含当前节点及所有子节点
                GetAllChildren(mt.ModuleID, childrenIDs);
            }
        }

        private void GetAllChildrenCode(int moduleID, List<string> childrenCodes)
        {
            var node = moduleRepository.GetByKey(moduleID);

            if (node != null)
            {
                childrenCodes.Add(node.Code);

                var childNodes = moduleRepository.GetDatas<Module>(m => m.ParID == moduleID, false).ToList();
                if (childNodes.Count > 0)
                {
                    foreach (var mt in childNodes)
                    {
                        //找到包含当前节点及所有子节点
                        GetAllChildrenCode(mt.ModuleID, childrenCodes);
                    }
                }
            }
        }

        /// <summary>
        ///新增或者更改的名字是否已经存在 
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool IsMonitorTreeTypeNameAvailable(string mtName, out string message, int? monitorTreeTypeID)
        {
            if (string.IsNullOrEmpty(mtName))
            {
                message = "003632";
                return false;
            }

            bool isExist = false;
            if (monitorTreeTypeID.HasValue)//编辑
            {
                isExist = cacheDICT.GetInstance().GetCacheType<MonitorTreeType>(t => t.Name.ToLower().Equals(mtName.ToLower()) && t.ID != monitorTreeTypeID).Any();
                if (isExist)
                {
                    message = "003642";
                    return false;
                }
            }
            else //新增
            {
                isExist = cacheDICT.GetInstance().GetCacheType<MonitorTreeType>(t => t.Name.ToLower().Equals(mtName.ToLower())).Any();
                if (isExist)
                {
                    message = "003642";
                    return false;
                }
            }
            message = string.Empty;
            return true;
        }

        private bool CanDelete(int id)
        {
            var isExistMT = monitorTreeTypeRepository.GetDatas<MonitorTree>(t => t.Type == id, true).Any();
            return !isExistMT;
        }

        private bool IsMSiteExisted(int id)
        {
            var isExistMS = measureSiteRepository.GetDatas<MeasureSite>(t => t.MSiteName == id, true).Any();
            return isExistMS;
        }

        #region 修改所有子节点状态
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-11-07
        /// 修改所有子节点状态
        /// </summary>
        /// <param name="pid">父节点</param>
        /// <param name="useStatus">父状态</param>
        public void SetChildModuleIsUseStatus(int pid, int useStatus)
        {
            var moduleList = moduleRepository.GetDatas<Module>(t => t.ParID == pid, false).ToList<Module>();

            //是否有数据
            if (moduleList != null && moduleList.Count() > 0)
            {
                //遍历所有儿子
                foreach (var module in moduleList)
                {
                    module.IsUsed = useStatus;
                    int id = module.ModuleID;
                    moduleRepository.Update<Module>(module);
                    SetChildModuleIsUseStatus(id, useStatus);
                }
            }

        }
        #endregion

        #region 修改所有子节点状态
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-11-07
        /// 修改所有子节点状态
        /// </summary>
        /// <param name="pid">父节点</param>
        /// <param name="isDefault">父状态</param>
        public void SetChildModuleIsDefault(int pid, int isDefault)
        {
            var moduleList = moduleRepository.GetDatas<Module>(t => t.ParID == pid, false).ToList<Module>();

            //是否有数据
            if (moduleList != null && moduleList.Count() > 0)
            {
                //遍历所有儿子
                foreach (var module in moduleList)
                {
                    module.IsDeault = isDefault;
                    int id = module.ModuleID;
                    moduleRepository.Update<Module>(module);
                    SetChildModuleIsDefault(id, isDefault);
                }
            }

        }
        #endregion

        #region 修改所有父节点状态
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-11-07
        /// 修改所有子节点状态
        /// </summary>
        /// <param name="pid">父节点</param>
        /// <param name="useStatus">子状态</param>
        public void SetParentModuleIsUseStatus(int pid)
        {
            //当前节点父节点为停用
            var module = moduleRepository.GetDatas<Module>(t => t.ModuleID == pid, false).FirstOrDefault();

            //如果没有找到父节点，结束.
            if (module == null)
            {
                return;
            }

            //当前节点所有子节点
            var moduleList = moduleRepository.GetDatas<Module>(t => t.ParID == pid, false).ToList<Module>();

            //启用数量
            var isUseStatusCount = moduleList.Count(item => item.IsUsed == 1);

            //总数量
            var allCount = moduleList.Count();

            //所有的为停用,否则为启用
            if (isUseStatusCount == 0)
            {
                module.IsUsed = 0;

                //关联角色权限
                roleModuleRepository.Delete(p => p.ModuleCode == module.Code);
            }
            else
            {
                module.IsUsed = 1;
            }

            moduleRepository.Update<Module>(module);
            SetParentModuleIsUseStatus(module.ParID);
        }
        #endregion


        #region 修改所有父节点状态
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-11-07
        /// 修改所有子节点状态
        /// </summary>
        /// <param name="pid">父节点</param>
        /// <param name="useStatus">子状态</param>
        public void SetParentModuleIsDefault(int pid)
        {
            //当前节点父节点为停用
            var module = moduleRepository.GetDatas<Module>(t => t.ModuleID == pid, false).FirstOrDefault();

            //如果没有找到父节点，结束.
            if (module == null)
            {
                return;
            }

            //当前节点所有子节点
            var moduleList = moduleRepository.GetDatas<Module>(t => t.ParID == pid, false).ToList<Module>();


            //必选数量
            var isSelectCount = moduleList.Count(item => item.IsDeault == 0);

            //总数量
            var allCount = moduleList.Count();


            //所有的为必选,则为必选
            if (isSelectCount == allCount)
            {
                module.IsDeault = 0;
            }
            else
            {
                module.IsDeault = 1;
            }
            moduleRepository.Update<Module>(module);
            SetParentModuleIsDefault(module.ParID);
        }
        #endregion

        #endregion

        #region 连接状态 CRUD
        public BaseResponse<CommonDataResult> GetConnectTypeDataForCommon(ViewConnectTypeParameter param)
        {
            BaseResponse<CommonDataResult> response = null;
            CommonDataResult result = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();

            try
            {
                List<ConnectStatusType> connectType = cacheDICT.GetInstance().GetCacheType<ConnectStatusType>().ToList();
                foreach (var connect in connectType)
                {
                    CommonInfo commonInfo = new CommonInfo
                    {
                        ID = connect.ID,
                        PID = null,
                        Name = connect.Name,
                        Describe = connect.Describe,
                        IsUsable = connect.IsUsable,
                        IsDefault = connect.IsDefault,
                        AddDate = connect.AddDate.ToString()
                    };

                    commonInfoList.Add(commonInfo);
                }

                result = new CommonDataResult { CommonInfoList = commonInfoList };
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003791");
                return response;
            }
        }

        public BaseResponse<bool> AddConnectTypeData(AddConnectTypeParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                ConnectStatusType connectType = new ConnectStatusType();

                connectType.Name = param.Name;
                connectType.Describe = param.Describe;
                connectType.IsUsable = param.IsUsable;
                connectType.IsDefault = param.IsDefault;
                OperationResult operationResult = connectStatusTypeRepository.AddNew<ConnectStatusType>(connectType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<ConnectStatusType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003801");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003801");
                return response;
            }
        }

        public BaseResponse<bool> EditConnectTypeData(EditConnectTypeParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                var connectType = cacheDICT.GetInstance().GetCacheType<ConnectStatusType>(p => p.ID == param.ID).SingleOrDefault();
                if (connectType == null)
                {
                    response = new BaseResponse<bool>("003812");
                    return response;
                }

                #region 连接类型如果已经使用，则不能停用 王颖辉 2017-02-20

                #endregion
                connectType.Name = param.Name;
                connectType.Describe = param.Describe;
                connectType.IsUsable = param.IsUsable;
                connectType.IsDefault = param.IsDefault;
                OperationResult operationResult = connectStatusTypeRepository.Update<ConnectStatusType>(connectType);

                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<ConnectStatusType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003822");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003822");
                return response;
            }
        }

        public BaseResponse<bool> DeleteConnectTypeData(DeleteConnectTypeParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (IsDefaultConnectStatus(param.ID))
                {
                    response = new BaseResponse<bool>("003832");
                    return response;
                }

                OperationResult operationResult = connectStatusTypeRepository.Delete<ConnectStatusType>(param.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    //更新缓存
                    cacheDICT.GetInstance().UpdateCacheType<ConnectStatusType>();
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("003841");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("003841");
                return response;
            }
        }

        private bool IsDefaultConnectStatus(int id)
        {
            var connectStatus = connectStatusTypeRepository.GetByKey(id);
            if (connectStatus != null && connectStatus.IsDefault == (int)EnumCommonDataType.Default)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 形貌图 LF
        /// <summary>
        /// 获取形貌图显示配置
        /// 
        /// 修改人：张辽阔
        /// 修改时间：2016-11-08
        /// 修改记录：Result 字段赋值
        /// </summary>
        /// <returns></returns>
        public BaseResponse<SystemConfigResult> GetTopographicMapSets()
        {
            #region 初始化
            BaseResponse<SystemConfigResult> baseResponse = new BaseResponse<SystemConfigResult>();
            SystemConfigResult result = new SystemConfigResult();

            #endregion

            try
            {
                #region 业务处理
                //形貌图配置
                string rootName = EnumHelper.GetDescription(EnumConfig.TopographicMapConfig);
                Config currentConfig = configRepository
                    .GetDatas<Config>(item => item.Name == rootName
                        && item.ParentId == 0, false)
                    .FirstOrDefault();
                //不为空
                if (currentConfig != null)
                {
                    List<Config> list = configRepository
                        .GetDatas<Config>(item => item.ParentId == currentConfig.ID, false)
                    .ToList<Config>();
                    result.ConfigList = list;
                }
                else
                {
                    result.ConfigList = new List<Config>();
                }
                baseResponse.Result = result;
                baseResponse.IsSuccessful = true;
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "003851";
            }
            return baseResponse;
        }

        /// <summary>
        /// 获取形貌图设备图片信息
        /// 
        /// 修改人：张辽阔
        /// 修改时间：2016-11-08
        /// 修改记录：Result 字段赋值
        /// </summary>
        /// <returns></returns>
        public BaseResponse<SystemConfigResult> GetTopographicMapPictureInfo()
        {
            #region 初始化

            BaseResponse<SystemConfigResult> baseResponse = new BaseResponse<SystemConfigResult>();
            SystemConfigResult result = new SystemConfigResult();

            #endregion

            try
            {
                #region 业务处理
                //形貌图配置
                string rootName = EnumHelper.GetDescription(EnumConfig.TopographicMapShowConfig);
                Config currentConfig = configRepository
                    .GetDatas<Config>(item => item.Name == rootName
                        && item.ParentId == 0, false)
                    .FirstOrDefault();
                //不为空
                if (currentConfig != null)
                {
                    List<Config> list = configRepository
                        .GetDatas<Config>(item => item.ParentId == currentConfig.ID, false)
                    .ToList<Config>();
                    result.ConfigList = list;
                }
                else
                {
                    result.ConfigList = new List<Config>();
                }
                baseResponse.Result = result;
                baseResponse.IsSuccessful = true;
                #endregion

                //#region 业务处理
                ////形貌图配置
                //string rootName = EnumHelper.GetDescription(EnumConfig.TopographicMapConfig);
                //Config currentConfig = configRepository
                //    .GetDatas<Config>(item => item.Name == rootName
                //        && item.ParentId == 0, false)
                //    .FirstOrDefault();
                ////不为空
                //if (currentConfig != null)
                //{
                //    List<Config> list = configRepository
                //        .GetDatas<Config>(item => item.ParentId == currentConfig.ID, false)
                //    .ToList<Config>();
                //    result.ConfigList = list;
                //}
                //else
                //{
                //    result.ConfigList = new List<Config>();
                //}
                //baseResponse.Result = result;
                //baseResponse.IsSuccessful = true;
                //#endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "003861";
            }

            //返回值
            return baseResponse;
        }
        #endregion

        #region 获取设备树和Server树
        /// <summary>
        /// 获取设备树和Server树
        /// 创建人:王龙杰
        /// 创建时间：2017-09-28
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<MonitorTreeDataForNavigationResult> GetAllMonitorTreeAndServerTreebyRole(RoleForMonitorTreeAndServerTreeParameter Para)
        {
            #region 初始化
            BaseResponse<MonitorTreeDataForNavigationResult> response = new BaseResponse<MonitorTreeDataForNavigationResult>();

            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();
            List<MTStatusInfo> mTWSNStatusInfos = new List<MTStatusInfo>();
            MonitorTreeDataForNavigationResult monitorTreeResult = new MonitorTreeDataForNavigationResult();

            string RoleCode = Para.RoleCode;
            #endregion

            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    var queryRoleNodule =
                       (from roleModule in dbContext.RoleModule
                        where roleModule.RoleCode == RoleCode
                        select new RoleModule
                        {
                            ModuleCode = roleModule.ModuleCode
                        }).ToList();
                    //判断是否有“监测功能”权限
                    if (queryRoleNodule.Where(t => t.ModuleCode == "MonitorModule") != null)
                    {
                        mTDevStatusInfos = MonitorTreeHelper.GetDeviceLevelTree();
                    }
                    //判断是否有“被监测功能”权限
                    if (queryRoleNodule.Where(t => t.ModuleCode == "PassivityMonitorModule") != null)
                    {
                        mTWSNStatusInfos = MonitorTreeHelper.GetServerTreeList();
                    }

                    monitorTreeResult.MTDevStatusInfos = mTDevStatusInfos;
                    monitorTreeResult.MTWSNStatusInfos = mTWSNStatusInfos;

                    response = new BaseResponse<MonitorTreeDataForNavigationResult>();
                    response.Result = monitorTreeResult;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response = new BaseResponse<MonitorTreeDataForNavigationResult>("002431");
                return response;
            }

            return response;
        }
        #endregion

        #region 验证通用数据是否有相同Code、Name
        /// <summary>
        /// 验证通用数据是否有相同Code
        /// 创建人:王龙杰
        /// 创建时间：2017-09-28
        /// </summary>
        /// <param name="Para"></param>
        /// <param name="isHaveID">为true时，表示编辑操作</param>
        /// <returns></returns>
        public BaseResponse<IsRepeatResult> IsExistComSetCode(IsExistComSetCodeParameter Para, bool isHaveID)
        {
            BaseResponse<IsRepeatResult> response = new BaseResponse<IsRepeatResult>();
            IsRepeatResult IsRepeatResult = new IsRepeatResult();

            if (string.IsNullOrEmpty(Para.Code))
            {
                //通用数据Code不能为空
                response.IsSuccessful = false;
                response.Code = "003633";
                return response;
            }
            string excuteSql = string.Empty;
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    if (!GetSqlParameterForIsExistCode(Para, isHaveID, ref excuteSql))
                    {
                        //传入通用数据类型不正确
                        response.IsSuccessful = false;
                        response.Code = "";
                        return response;
                    }
                    int sqlResult = dbContext.Database.SqlQuery<int>(excuteSql).FirstOrDefault();

                    if (sqlResult > 0)
                        IsRepeatResult.IsRepeat = true;
                    else
                        IsRepeatResult.IsRepeat = false;

                    response.IsSuccessful = true;
                    response.Result = IsRepeatResult;
                    return response;
                }
            }
            catch (Exception ex)
            {
                //验证通用数据是否有相同Code 出错
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008521";
                return response;
            }
        }

        /// <summary>
        /// 验证通用数据是否有相同Name
        /// 监测树类型、设备类型、测量位置监测类型、振动信号类型、无线网关类型、传感器类型 全表不重复
        /// 测量位置类型、特征值类型、波长、上、下限频率 同一父节点下不重复
        /// </summary>
        /// <param name="Para"></param>
        /// <param name="isHaveID">为true时，表示编辑操作</param>
        /// <returns></returns>
        public BaseResponse<IsRepeatResult> IsExistComSetName(IsExistComSetNameParameter Para, bool isHaveID)
        {
            BaseResponse<IsRepeatResult> response = new BaseResponse<IsRepeatResult>();
            IsRepeatResult IsRepeatResult = new IsRepeatResult();

            if (string.IsNullOrEmpty(Para.Name))
            {
                //通用数据名称不能为空
                response.IsSuccessful = false;
                response.Code = "003632";
                return response;
            }

            string excuteSql = string.Empty;
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    if (!GetSqlParameterForIsExistName(Para, isHaveID, ref excuteSql))
                    {
                        //传入通用数据类型不正确
                        response.IsSuccessful = false;
                        response.Code = "010032";
                        return response;
                    }

                    int sqlResult = dbContext.Database.SqlQuery<int>(excuteSql).FirstOrDefault();
                    if (sqlResult > 0)
                        IsRepeatResult.IsRepeat = true;
                    else
                        IsRepeatResult.IsRepeat = false;

                    response.IsSuccessful = true;
                    response.Code = "";
                    response.Result = IsRepeatResult;
                    return response;
                }
            }
            catch (Exception ex)
            {
                //验证通用数据是否有相同Code 出错
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008531";
                return response;
            }
        }

        public bool GetSqlParameterForIsExistCode(IsExistComSetCodeParameter Para, bool isHaveID, ref string excuteSql)
        {
            int table = Para.table;
            string code = Para.Code;
            //操作表名
            string tableName = GetTableNameByType(table);
            if (string.IsNullOrEmpty(tableName))
            {
                //传入通用数据类型不正确
                return false;
            }
            string strSql = @"select count(0) from {0} where code='{1}'";

            excuteSql = string.Format(strSql, tableName, code);
            //通过ID验证通用数据是否有相同Code
            if (isHaveID)
            {
                int? id = Para.ID;
                string idCondition = " and ID!=" + id + ";";
                excuteSql = excuteSql + idCondition;
            }
            return true;
        }

        public bool GetSqlParameterForIsExistName(IsExistComSetNameParameter Para, bool isHaveID, ref string excuteSql)
        {
            int table = Para.table;

            //当执行编辑操作时,ParentID可为空，根据主键ID获取ParentID
            int parentID = 0;
            string name = Para.Name;
            //操作表名
            string tableName = GetTableNameByType(table);
            if (string.IsNullOrEmpty(tableName))
            {
                //传入通用数据类型不正确
                return false;
            }
            //测量位置类型、特征值类型 中外键字段名称
            string culumnKeyName = string.Empty;
            //需要验证重复的字段名称
            string culumnName = string.Empty;
            //监测树类型、设备类型、测量位置监测类型、振动信号类型、无线网关类型、传感器类型 执行语句
            string strSqlAllTable = @"select count(0) from {0} where {1}='{2}'";
            //测量位置类型、特征值类型、系统参数、系统功能 执行语句
            string strSqlSingleParentID = @"select count(0) from {0} where {1}='{2}' and {3}={4}";

            switch (table)
            {
                case 1:
                    culumnName = "Name";
                    excuteSql = string.Format(strSqlAllTable, tableName, culumnName, name);
                    break;
                case 2:
                    culumnName = "Name";
                    excuteSql = string.Format(strSqlAllTable, tableName, culumnName, name);
                    break;
                case 3:
                    culumnKeyName = "DeviceTypeID";
                    culumnName = "Name";
                    parentID = isHaveID ? measureSiteTypeRepository.GetByKey(Para.ID).DeviceTypeID : Para.ParentID.Value;
                    excuteSql = string.Format(strSqlSingleParentID, tableName, culumnName, name, culumnKeyName, parentID);
                    break;
                case 4:
                    culumnName = "Name";
                    excuteSql = string.Format(strSqlAllTable, tableName, culumnName, name);
                    break;
                case 5:
                    culumnName = "Name";
                    excuteSql = string.Format(strSqlAllTable, tableName, culumnName, name);
                    break;
                case 6:
                    culumnKeyName = "VibratingSignalTypeID";
                    culumnName = "Name";
                    parentID = isHaveID ? eigenValueTypeRepository.GetByKey(Para.ID).VibratingSignalTypeID : Para.ParentID.Value;
                    excuteSql = string.Format(strSqlSingleParentID, tableName, culumnName, name, culumnKeyName, parentID);
                    break;
                case 7:
                    try
                    {
                        Convert.ToInt32(name);
                    }
                    catch
                    {
                        return false;
                    }
                    culumnKeyName = "VibratingSignalTypeID";
                    culumnName = "WaveLengthValue";
                    parentID = isHaveID ? waveLengthValuesRepository.GetByKey(Para.ID).VibratingSignalTypeID : Para.ParentID.Value;
                    excuteSql = string.Format(strSqlSingleParentID, tableName, culumnName, name, culumnKeyName, parentID);
                    break;
                case 8:
                    try
                    {
                        Convert.ToInt32(name);
                    }
                    catch
                    {
                        return false;
                    }
                    culumnKeyName = "VibratingSignalTypeID";
                    culumnName = "WaveUpperLimitValue";
                    parentID = isHaveID ? waveUpperLimitValuesRepository.GetByKey(Para.ID).VibratingSignalTypeID : Para.ParentID.Value;
                    excuteSql = string.Format(strSqlSingleParentID, tableName, culumnName, name, culumnKeyName, parentID);
                    break;
                case 9:
                    try
                    {
                        Convert.ToInt32(name);
                    }
                    catch
                    {
                        return false;
                    }
                    culumnKeyName = "VibratingSignalTypeID";
                    culumnName = "WaveLowerLimitValue";
                    parentID = isHaveID ? waveLowerLimitValuesRepository.GetByKey(Para.ID).VibratingSignalTypeID : Para.ParentID.Value;
                    excuteSql = string.Format(strSqlSingleParentID, tableName, culumnName, name, culumnKeyName, parentID);
                    break;
                case 10:
                    culumnName = "Name";
                    excuteSql = string.Format(strSqlAllTable, tableName, culumnName, name);
                    break;
                case 11:
                    culumnName = "Name";
                    excuteSql = string.Format(strSqlAllTable, tableName, culumnName, name);
                    break;
                case 12:
                    culumnKeyName = "ParentId";
                    culumnName = "Name";
                    parentID = isHaveID ? configRepository.GetByKey(Para.ID).ParentId : Para.ParentID.Value;
                    excuteSql = string.Format(strSqlSingleParentID, tableName, culumnName, name, culumnKeyName, parentID);
                    break;
                case 13:
                    culumnKeyName = "ParID";
                    culumnName = "ModuleName";
                    parentID = isHaveID ? moduleRepository.GetByKey(Para.ID).ParID : Para.ParentID.Value;
                    excuteSql = string.Format(strSqlSingleParentID, tableName, culumnName, name, culumnKeyName, parentID);
                    break;
                default:
                    tableName = ""; break;
            }
            //通过ID验证通用数据是否有相同Name
            if (isHaveID)
            {
                int? id = Para.ID;
                string idCondition = " and ID!=" + id + ";";
                excuteSql = excuteSql + idCondition;
            }

            return true;
        }

        #endregion

        #region 设置通用数据显示顺序
        /// <summary>
        /// 设置通用数据显示顺序
        /// 创建人:王龙杰
        /// 创建时间：2017-09-28
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<bool> SetComSetOrder(SetComSetOrderParameter Para)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            int table = Para.table;
            int id1 = Para.ID1;
            int id2 = Para.ID2;
            int order1 = Para.Order1;
            int order2 = Para.Order2;
            //操作表名
            string tableName = GetTableNameByType(table);
            if (string.IsNullOrEmpty(tableName))
            {
                //传入通用数据类型不正确
                response.IsSuccessful = false;
                response.Code = "";
                return response;
            }
            string updateComSetOrder = @"update " + tableName + " set OrderNo={0} where ID={1}";
            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    int commonInfo1 = dbContext.Database.ExecuteSqlCommand(string.Format(updateComSetOrder, order2, id1));
                    int commonInfo2 = dbContext.Database.ExecuteSqlCommand(string.Format(updateComSetOrder, order1, id2));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "";
                return response;
            }

            return response;
        }
        #endregion

        #region 获取振动信号类型和特征值类型(可用状态)
        /// <summary>
        /// 获取振动信号类型和特征值类型(可用状态)
        /// 创建人:王龙杰
        /// 创建时间：2017-09-28
        /// </summary>
        /// <returns></returns>
        public BaseResponse<ComSetDataForVibAndEigenResult> GetComSetDataForVibAndEigen()
        {
            BaseResponse<ComSetDataForVibAndEigenResult> response = new BaseResponse<ComSetDataForVibAndEigenResult>();
            ComSetDataForVibAndEigenResult comSetDataForVibAndEigenResult = new ComSetDataForVibAndEigenResult();
            List<CommonInfo> vibsInfo = new List<CommonInfo>();
            List<CommonInfo> eigenInfo = new List<CommonInfo>();

            try
            {
                //振动信号类型集合
                List<VibratingSingalType> vibratingSingalType =
                    vibratingSingalTypeRepository.GetDatas<VibratingSingalType>(t => t.IsUsable == 1, false).ToList();
                vibsInfo = vibratingSingalType.Select(VibType =>
                {
                    return new CommonInfo
                    {
                        ID = VibType.ID,
                        Name = VibType.Name,
                        Describe = VibType.Describe,
                        AddDate = VibType.AddDate.ToString(),
                        IsDefault = VibType.ID,
                        OrderNo = VibType.OrderNo
                    };
                }
                ).ToList();

                //特征值类型集合
                List<EigenValueType> eigenValueType =
                    eigenValueTypeRepository.GetDatas<EigenValueType>(t => t.IsUsable == 1, false).ToList();
                eigenInfo = eigenValueType.Select(EigenType =>
                {
                    return new CommonInfo
                    {
                        ID = EigenType.ID,
                        VibrationSignalTypeID = EigenType.VibratingSignalTypeID,
                        Name = EigenType.Name,
                        Describe = EigenType.Describe,
                        AddDate = EigenType.AddDate.ToString(),
                        IsDefault = EigenType.ID,
                        OrderNo = EigenType.OrderNo
                    };
                }
                ).ToList();

                comSetDataForVibAndEigenResult.VibsInfo = vibsInfo;
                comSetDataForVibAndEigenResult.EigenInfo = eigenInfo;
                response.Result = comSetDataForVibAndEigenResult;
                response.IsSuccessful = true;
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "";
                return response;
            }
        }
        #endregion

        #region 通过父Code 和 Code获取系统配置信息，与通过Code获取Config合并
        public BaseResponse<ConfigResult> GetConfigByCode(ConfigByCodeParameter param)
        {
            ConfigResult result = new ConfigResult();
            BaseResponse<ConfigResult> response = null;
            List<ConfigData> configList = new List<ConfigData>();
            if (!string.IsNullOrEmpty(param.RootCode) && !string.IsNullOrEmpty(param.Code))
            {
                try
                {
                    var pid = 0;
                    Config root = configRepository.GetDatas<Config>(t => t.Code.Equals(param.RootCode), true).FirstOrDefault();
                    if (root == null)
                    {
                        response = new BaseResponse<ConfigResult>(false, null, "002992", null);
                        return response;
                    }

                    pid = root.ID;
                    Config config = configRepository.GetDatas<Config>(item => item.ParentId == pid && item.Code == param.Code, false).FirstOrDefault();

                    if (config == null)
                    {
                        response = new BaseResponse<ConfigResult>(false, null, "002992", null);
                        return response;
                    }

                    var isExistChild = configRepository.GetDatas<Config>(t => t.ParentId == config.ID, true).Any();
                    ConfigData configData = new ConfigData
                    {
                        ID = config.ID,
                        Name = config.Name,
                        Code = config.Code,
                        Value = config.Value,
                        Describe = config.Describe,
                        IsUsed = config.IsUsed,
                        IsDefault = config.IsDefault,
                        IsExistChild = isExistChild,
                        OrderNo = config.OrderNo,
                        CommonDataType = config.CommonDataType,
                        CommonDataCode = config.CommonDataCode
                    };
                    configList.Add(configData);
                    result.ConfigList = configList;
                    response = new BaseResponse<ConfigResult>(true, null, null, result);
                    return response;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                    response = new BaseResponse<ConfigResult>(false, null, "003011", null);
                    return response;
                }
            }
            else if (!string.IsNullOrEmpty(param.RootCode) && string.IsNullOrEmpty(param.Code))
            {
                try
                {
                    var root = configRepository.GetDatas<Config>(item => item.Code == param.RootCode, false).FirstOrDefault();
                    if (root != null)
                    {
                        var pid = root.ID;
                        configList = configRepository.GetDatas<Config>(item => item.ParentId == pid, false).Select(t => new ConfigData
                        {
                            ID = t.ID,
                            Name = t.Name,
                            Code = t.Code,
                            Value = t.Value,
                            Describe = t.Describe,
                            IsUsed = t.IsUsed,
                            IsDefault = t.IsDefault,
                            OrderNo = t.OrderNo,
                            CommonDataType = t.CommonDataType,
                            CommonDataCode = t.CommonDataCode

                        }).ToList();
                        foreach (ConfigData configData in configList)
                        {
                            var isExistChild = configRepository.GetDatas<Config>(item => item.ParentId == configData.ID, false).Any();
                            configData.IsExistChild = isExistChild;
                        }
                    }

                    result.ConfigList = configList;
                    response = new BaseResponse<ConfigResult>();
                    response.Result = result;
                    return response;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                    response = new BaseResponse<ConfigResult>("003011");
                    return response;
                }
            }
            else
            {
                result.ConfigList = new List<ConfigData>();
                response = new BaseResponse<ConfigResult>(true, null, null, result);
                return response;
            }
        }
        #endregion

        #region 私有方法

        #region 修改所有子节点状态
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-11-07
        /// 修改所有子节点状态
        /// </summary>
        /// <param name="pid">父节点</param>
        /// <param name="useStatus">父状态</param>
        public void SetChildConfigIsUseStatus(int pid, int useStatus)
        {
            var moduleList = configRepository.GetDatas<Config>(t => t.ParentId == pid, false).ToList<Config>();

            //是否有数据
            if (moduleList != null && moduleList.Count() > 0)
            {
                //遍历所有儿子
                foreach (var module in moduleList)
                {
                    module.IsUsed = useStatus;
                    int id = module.ID;
                    configRepository.Update<Config>(module);
                    SetChildConfigIsUseStatus(id, useStatus);
                }
            }

        }
        #endregion

        #region 修改所有父节点状态
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-11-07
        /// 修改所有子节点状态
        /// </summary>
        /// <param name="pid">父节点</param>
        /// <param name="useStatus">子状态</param>
        public void SetParentConfigIsUseStatus(int pid)
        {
            //当前节点所有子节点
            var moduleList = configRepository.GetDatas<Config>(t => t.ParentId == pid, false).ToList<Config>();

            //启用数量
            var isUseStatusCount = moduleList.Count(item => item.IsUsed == 1);

            //当前节点父节点为停用
            var module = configRepository.GetDatas<Config>(t => t.ID == pid, false).FirstOrDefault();

            //如果没有找到父节点，结束.
            if (module == null)
            {
                return;
            }
            //所有的为停用,否则为启用
            if (isUseStatusCount == 0)
            {
                module.IsUsed = 0;
            }
            else
            {
                module.IsUsed = 1;
            }
            configRepository.Update<Config>(module);
            SetParentConfigIsUseStatus(module.ParentId);

        }
        #endregion

        #region 获取通用数据表名
        /// <summary>
        /// 获取通用数据表名
        /// 创建人:王龙杰
        /// 创建时间：2017-09-28
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GetTableNameByType(int table)
        {
            string tableName = string.Empty;
            switch (table)
            {
                case 1:
                    tableName = "T_DICT_MONITORTREE_TYPE"; break;
                case 2:
                    tableName = "T_DICT_DEVICE_TYPE"; break;
                case 3:
                    tableName = "T_DICT_MEASURE_SITE_TYPE"; break;
                case 4:
                    tableName = "T_DICT_MEASURE_SITE_MONITOR_TYPE"; break;
                case 5:
                    tableName = "T_DICT_VIBRATING_SIGNAL_TYPE"; break;
                case 6:
                    tableName = "T_DICT_EIGEN_VALUE_TYPE"; break;
                case 7:
                    tableName = "T_DICT_WAVE_LENGTH_VALUE"; break;
                case 8:
                    tableName = "T_DICT_WAVE_UPPERLIMIT_VALUE"; break;
                case 9:
                    tableName = "T_DICT_WAVE_LOWERLIMIT_VALUE"; break;
                case 10:
                    tableName = "T_DICT_SENSOR_TYPE"; break;
                case 11:
                    tableName = "T_DICT_WIRELESS_GATEWAY_TYPE"; break;
                case 12:
                    tableName = "T_DICT_CONFIG"; break;
                case 13:
                    tableName = " T_SYS_MODULE"; break;
                default:
                    tableName = ""; break;
            }
            return tableName;
        }
        #endregion

        #endregion

        #region 获取通用数据振动及特征值信息
        /// <summary>
        /// 获取通用数据振动及特征值信息
        /// </summary>
        public BaseResponse<GetVibAndEigenComSetInfoResult> GetVibAndEigenComSetInfo()
        {
            BaseResponse<GetVibAndEigenComSetInfoResult> baseResponse = new BaseResponse<GetVibAndEigenComSetInfoResult>();
            GetVibAndEigenComSetInfoResult result = new GetVibAndEigenComSetInfoResult();
            result.MSMonitorType = new List<MSMonitorType>();
            result.WireLessVibTypeinfo = new List<VibTypeinfo>();
            result.WiredVibTypeinfo = new List<VibTypeinfo>();
            result.TriaxialVibTypeinfo = new List<VibTypeinfo>();

            try
            {
                //振动信号类型
                List<VibratingSingalType> vibratingSingalTypeList =
                    cacheDICT.GetInstance().GetCacheType<VibratingSingalType>()
                    .Where(item => item.IsUsable == 1)
                    .ToList();
                //下限频率
                List<WaveLowerLimitValues> waveLowerLimitValuesList =
                    cacheDICT.GetInstance().GetCacheType<WaveLowerLimitValues>()
                    .Where(item => item.IsUsable == 1)
                    .ToList();
                //上限频率
                List<WaveUpperLimitValues> waveUpperLimitValuesList =
                    cacheDICT.GetInstance().GetCacheType<WaveUpperLimitValues>()
                    .Where(item => item.IsUsable == 1)
                    .ToList();
                //特征值类型
                List<EigenValueType> eigenValueTypeList =
                    cacheDICT.GetInstance().GetCacheType<EigenValueType>()
                    .Where(item => item.IsUsable == 1)
                    .ToList();
                //测量位置监测类型
                List<MeasureSiteMonitorType> measureSiteMonitorTypeList =
                    cacheDICT.GetInstance().GetCacheType<MeasureSiteMonitorType>()
                    .Where(item => item.IsUsable == 1)
                    .ToList();
                //波长
                List<WaveLengthValues> waveLengthValueList =
                    cacheDICT.GetInstance().GetCacheType<WaveLengthValues>()
                    .Where(item => item.IsUsable == 1)
                    .ToList();

                #region 添加无线传感器振动信息

                foreach (var vibsignal in vibratingSingalTypeList)
                {
                    VibTypeinfo vibTypeinfo = new VibTypeinfo();
                    vibTypeinfo.Code = vibsignal.Code;
                    vibTypeinfo.VibrationSignalTypeID = vibsignal.ID;
                    vibTypeinfo.VibrationSignalName = vibsignal.Name;
                    vibTypeinfo.Description = vibsignal.Describe;

                    #region 上限

                    if (waveUpperLimitValuesList != null && waveUpperLimitValuesList.Any())
                    {
                        var upperLimitList = waveUpperLimitValuesList
                            .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                && item.Describe == "WireLessSensor_UpLimit")
                            .ToList();
                        if (upperLimitList != null && upperLimitList.Any())
                        {
                            vibTypeinfo.UpperLimitList = upperLimitList
                                .Select(item => new SelectItemResult
                                {
                                    Text = item.WaveUpperLimitValue.ToString(),
                                    Value = item.ID.ToString(),
                                    Description = item.Describe,
                                })
                                .ToList();
                        }
                    }

                    #endregion

                    #region 下限

                    if (waveLowerLimitValuesList != null && waveLowerLimitValuesList.Any())
                    {
                        var lowLimitList = waveLowerLimitValuesList
                            .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                && item.Describe == "WireLessSensor_LowLimit")
                            .ToList();
                        if (lowLimitList != null && lowLimitList.Any())
                        {
                            vibTypeinfo.LowLimitList = lowLimitList
                                .Select(item => new SelectItemResult
                                {
                                    Text = item.WaveLowerLimitValue.ToString(),
                                    Value = item.ID.ToString(),
                                    Description = item.Describe,
                                })
                                .ToList();
                        }
                    }

                    #endregion

                    #region 波长

                    var waveLengthList = waveLengthValueList
                        .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                            && item.Describe == "WireLessSensor_WaveLength")
                        .ToList();
                    if (waveLengthList != null && waveLengthList.Any())
                    {
                        vibTypeinfo.WaveLengthList = waveLengthList
                            .Select(item => new SelectItemResult
                            {
                                Text = item.WaveLengthValue.ToString(),
                                Value = item.ID.ToString(),
                                Description = item.Describe,
                            })
                            .ToList();
                    }

                    #endregion

                    #region 特征值波长

                    var eigenValueWaveLengthList = waveLengthValueList
                        .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                            && item.Describe == "WireLessSensor_EigenWaveLength")
                        .ToList();
                    if (eigenValueWaveLengthList != null && eigenValueWaveLengthList.Any())
                    {
                        vibTypeinfo.EigenWaveLengthList = eigenValueWaveLengthList
                            .Select(item => new SelectItemResult
                            {
                                Text = item.WaveLengthValue.ToString(),
                                Value = item.ID.ToString(),
                                Description = item.Describe,
                            })
                            .ToList();
                    }

                    #endregion

                    #region 特征值

                    if (eigenValueTypeList != null && eigenValueTypeList.Any())
                    {
                        var eigenValueInfoList = eigenValueTypeList
                            .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                && item.Describe == "WireLessSensor_EigenValue")
                            .ToList();
                        if (eigenValueInfoList != null && eigenValueInfoList.Any())
                        {
                            vibTypeinfo.EigenValueInfo = eigenValueInfoList
                                .Select(item => new EigenValueInfo
                                {
                                    EigenValueTypeID = item.ID,
                                    EigenValueName = item.Name,
                                    Description = item.Describe,
                                })
                                .ToList();
                        }
                    }

                    #endregion

                    result.WireLessVibTypeinfo.Add(vibTypeinfo);
                }

                #endregion

                #region 添加有线传感器振动信息

                foreach (var vibsignal in vibratingSingalTypeList.Where(item => new int[] { 1, 2, 4 }.Contains(item.ID)))
                {
                    VibTypeinfo vibTypeinfo = new VibTypeinfo();
                    vibTypeinfo.Code = vibsignal.Code;
                    vibTypeinfo.VibrationSignalTypeID = vibsignal.ID;
                    vibTypeinfo.VibrationSignalName = vibsignal.Name;
                    vibTypeinfo.Description = vibsignal.Describe;

                    #region 上限

                    if (waveUpperLimitValuesList != null && waveUpperLimitValuesList.Any())
                    {
                        var upperLimitList = waveUpperLimitValuesList
                            .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                && item.Describe == "WiredSensor_UpLimit")
                            .ToList();
                        if (upperLimitList != null && upperLimitList.Any())
                        {
                            vibTypeinfo.UpperLimitList = upperLimitList
                                .Select(item => new SelectItemResult
                                {
                                    Text = item.WaveUpperLimitValue.ToString(),
                                    Value = item.ID.ToString(),
                                    Description = item.Describe,
                                })
                                .ToList();
                        }
                    }

                    #endregion

                    if (vibsignal.ID == 1 || vibsignal.ID == 2)
                    {
                        #region 下限

                        if (waveLowerLimitValuesList != null && waveLowerLimitValuesList.Any())
                        {
                            var lowLimitList = waveLowerLimitValuesList
                                .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                    && item.Describe == "WiredSensor_LowLimit")
                                .ToList();
                            if (lowLimitList != null && lowLimitList.Any())
                            {
                                vibTypeinfo.LowLimitList = lowLimitList
                                    .Select(item => new SelectItemResult
                                    {
                                        Text = item.WaveLowerLimitValue.ToString(),
                                        Value = item.ID.ToString(),
                                        Description = item.Describe,
                                    })
                                    .ToList();
                            }
                        }

                        #endregion
                    }
                    else if (vibsignal.ID == 4)
                    {
                        #region 包络滤波器上限

                        if (waveUpperLimitValuesList != null && waveUpperLimitValuesList.Any())
                        {
                            var upperLimitList = waveUpperLimitValuesList
                                .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                    && item.Describe == "WiredSensor_EnvlFilterUpLimit")
                                .ToList();
                            if (upperLimitList != null && upperLimitList.Any())
                            {
                                vibTypeinfo.EnvlFilterUpperLimitList = upperLimitList
                                    .Select(item => new SelectItemResult
                                    {
                                        Text = item.WaveUpperLimitValue.ToString(),
                                        Value = item.ID.ToString(),
                                        Description = item.Describe,
                                    })
                                    .ToList();
                            }
                        }

                        #endregion

                        #region 包络滤波器下限

                        if (waveLowerLimitValuesList != null && waveLowerLimitValuesList.Any())
                        {
                            var lowLimitList = waveLowerLimitValuesList
                                .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                    && item.Describe == "WiredSensor_EnvlFilterLowLimit")
                                .ToList();
                            if (lowLimitList != null && lowLimitList.Any())
                            {
                                vibTypeinfo.EnvlFilterLowLimitList = lowLimitList
                                    .Select(item => new SelectItemResult
                                    {
                                        Text = item.WaveLowerLimitValue.ToString(),
                                        Value = item.ID.ToString(),
                                        Description = item.Describe,
                                    })
                                    .ToList();
                            }
                        }

                        #endregion
                    }

                    #region 特征值

                    if (eigenValueTypeList != null && eigenValueTypeList.Any())
                    {
                        var eigenValueInfoList = eigenValueTypeList
                            .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                && item.Describe == "WiredSensor_EigenValue")
                            .ToList();
                        if (eigenValueInfoList != null && eigenValueInfoList.Any())
                        {
                            vibTypeinfo.EigenValueInfo = eigenValueInfoList
                                .Select(item => new EigenValueInfo
                                {
                                    EigenValueTypeID = item.ID,
                                    EigenValueName = item.Name,
                                    Description = item.Describe,
                                })
                                .ToList();
                        }
                    }

                    #endregion

                    result.WiredVibTypeinfo.Add(vibTypeinfo);
                }

                #endregion

                #region 添加三轴传感器振动信息

                foreach (var vibsignal in vibratingSingalTypeList.Where(item => new int[] { 1, 2, 3 }.Contains(item.ID)))
                {
                    VibTypeinfo vibTypeinfo = new VibTypeinfo();
                    vibTypeinfo.Code = vibsignal.Code;
                    vibTypeinfo.VibrationSignalTypeID = vibsignal.ID;
                    vibTypeinfo.VibrationSignalName = vibsignal.Name;
                    vibTypeinfo.Description = vibsignal.Describe;

                    #region 上限

                    if (waveUpperLimitValuesList != null && waveUpperLimitValuesList.Any())
                    {
                        var upperLimitList = waveUpperLimitValuesList
                            .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                && item.Describe == "Triaxial_UpLimit")
                            .ToList();
                        if (upperLimitList != null && upperLimitList.Any())
                        {
                            vibTypeinfo.UpperLimitList = upperLimitList
                                .Select(item => new SelectItemResult
                                {
                                    Text = item.WaveUpperLimitValue.ToString(),
                                    Value = item.ID.ToString(),
                                    Description = item.Describe,
                                })
                                .ToList();
                        }
                    }

                    #endregion

                    #region 下限

                    if (waveLowerLimitValuesList != null && waveLowerLimitValuesList.Any())
                    {
                        var lowLimitList = waveLowerLimitValuesList
                            .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                && item.Describe == "Triaxial_LowLimit")
                            .ToList();
                        if (lowLimitList != null && lowLimitList.Any())
                        {
                            vibTypeinfo.LowLimitList = lowLimitList
                                .Select(item => new SelectItemResult
                                {
                                    Text = item.WaveLowerLimitValue.ToString(),
                                    Value = item.ID.ToString(),
                                    Description = item.Describe,
                                })
                                .ToList();
                        }
                    }

                    #endregion

                    #region 波长

                    var waveLengthList = waveLengthValueList
                        .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                            && item.Describe == "Triaxial_WaveLength")
                        .ToList();
                    if (waveLengthList != null && waveLengthList.Any())
                    {
                        vibTypeinfo.WaveLengthList = waveLengthList
                            .Select(item => new SelectItemResult
                            {
                                Text = item.WaveLengthValue.ToString(),
                                Value = item.ID.ToString(),
                                Description = item.Describe,
                            })
                            .ToList();
                    }

                    #endregion

                    #region 特征值波长

                    var eigenValueWaveLengthList = waveLengthValueList
                        .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                            && item.Describe == "Triaxial_EigenWaveLength")
                        .ToList();
                    if (eigenValueWaveLengthList != null && eigenValueWaveLengthList.Any())
                    {
                        vibTypeinfo.EigenWaveLengthList = eigenValueWaveLengthList
                            .Select(item => new SelectItemResult
                            {
                                Text = item.WaveLengthValue.ToString(),
                                Value = item.ID.ToString(),
                                Description = item.Describe,
                            })
                            .ToList();
                    }

                    #endregion

                    #region 特征值

                    if (eigenValueTypeList != null && eigenValueTypeList.Any())
                    {
                        var eigenValueInfoList = eigenValueTypeList
                            .Where(item => item.VibratingSignalTypeID == vibsignal.ID
                                && item.Describe == "Triaxial_EigenValue")
                            .ToList();
                        if (eigenValueInfoList != null && eigenValueInfoList.Any())
                        {
                            vibTypeinfo.EigenValueInfo = eigenValueInfoList
                                .Select(item => new EigenValueInfo
                                {
                                    EigenValueTypeID = item.ID,
                                    EigenValueName = item.Name,
                                    Description = item.Describe,
                                })
                                .ToList();
                        }
                    }

                    #endregion

                    result.TriaxialVibTypeinfo.Add(vibTypeinfo);
                }

                #endregion

                #region 测量位置检测类型集合

                if (measureSiteMonitorTypeList != null && measureSiteMonitorTypeList.Any())
                {
                    result.MSMonitorType = measureSiteMonitorTypeList
                        .Select(item => new MSMonitorType
                        {
                            MSMonitorTypeID = item.ID,
                            MSMonitorTypeCode = item.Code,
                            MSMonitorTypeName = item.Name,
                        })
                        .ToList();
                }

                #endregion

                baseResponse.IsSuccessful = true;
                baseResponse.Result = result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008961";
            }
            return baseResponse;
        }
        #endregion

        #region 获取通用数据三轴传感器振动类型数据

        /// <summary>
        /// 创建人：张辽阔 
        /// 创建时间：2018-05-11
        /// 创建内容：获取三轴传感器振动类型数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<CommonDataResult> GetTriaxialVIBTypeDataForCommon(GetComSetDataParameter parameter)
        {
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();
            CommonDataResult result = new CommonDataResult();

            try
            {
                List<VibratingSingalType> vibTypes = cacheDICT.GetInstance().GetCacheType<VibratingSingalType>().ToList();
                foreach (var vibType in vibTypes)
                {
                    CommonInfo vibTypeData = new CommonInfo
                    {
                        ID = vibType.ID,
                        Code = vibType.Code,
                        Name = vibType.Name,
                        Describe = vibType.Describe,
                        IsUsable = vibType.IsUsable,
                        IsDefault = vibType.IsDefault,
                        AddDate = vibType.AddDate.ToString(),
                        OrderNo = vibType.OrderNo
                    };

                    #region 计算是否有各个子节点

                    var isExistEigenChild = eigenValueTypeRepository
                        .GetDatas<EigenValueType>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "Triaxial_EigenValue", true)
                        .Any();
                    var isExistWaveLengthChild = waveLengthValuesRepository
                        .GetDatas<WaveLengthValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "Triaxial_WaveLength", true)
                        .Any();
                    var isExistUpperLimit = waveUpperLimitValuesRepository
                        .GetDatas<WaveUpperLimitValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "Triaxial_UpLimit", true)
                        .Any();
                    var isExistLowerLimit = waveLowerLimitValuesRepository
                        .GetDatas<WaveLowerLimitValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "Triaxial_LowLimit", true)
                        .Any();
                    var isExistEigenWaveLength = waveLengthValuesRepository
                        .GetDatas<WaveLengthValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "Triaxial_EigenWaveLength", true)
                        .Any();
                    vibTypeData.IsExistEigenChild = isExistEigenChild;
                    vibTypeData.IsExistWaveLengthChild = isExistWaveLengthChild;
                    vibTypeData.IsExistUpperLimitChild = isExistUpperLimit;
                    vibTypeData.IsExistLowerLimitChild = isExistLowerLimit;
                    vibTypeData.IsExistEigenWaveLengthChild = isExistEigenWaveLength;

                    #endregion

                    commonInfoList.Add(vibTypeData);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003291");
                return response;
            }
        }

        #endregion

        #region 获取通用数据有线传感器振动类型数据

        /// <summary>
        /// 创建人：张辽阔 
        /// 创建时间：2018-05-11
        /// 创建内容：获取有线传感器振动类型数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<CommonDataResult> GetWiredVIBTypeDataForCommon(GetComSetDataParameter parameter)
        {
            BaseResponse<CommonDataResult> response = null;
            List<CommonInfo> commonInfoList = new List<CommonInfo>();
            CommonDataResult result = new CommonDataResult();

            try
            {
                List<VibratingSingalType> vibTypes = cacheDICT.GetInstance().GetCacheType<VibratingSingalType>().ToList();
                foreach (var vibType in vibTypes)
                {
                    CommonInfo vibTypeData = new CommonInfo
                    {
                        ID = vibType.ID,
                        Code = vibType.Code,
                        Name = vibType.Name,
                        Describe = vibType.Describe,
                        IsUsable = vibType.IsUsable,
                        IsDefault = vibType.IsDefault,
                        AddDate = vibType.AddDate.ToString(),
                        OrderNo = vibType.OrderNo
                    };

                    #region 计算是否有各个子节点

                    var isExistEigenChild = eigenValueTypeRepository
                        .GetDatas<EigenValueType>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WiredSensor_EigenValue", true)
                        .Any();
                    var isExistUpperLimit = waveUpperLimitValuesRepository
                        .GetDatas<WaveUpperLimitValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WiredSensor_UpLimit", true)
                        .Any();
                    var isExistLowerLimit = waveLowerLimitValuesRepository
                        .GetDatas<WaveLowerLimitValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WiredSensor_LowLimit", true)
                        .Any();
                    var isExistEnvlFilterUpperLimit = waveUpperLimitValuesRepository
                        .GetDatas<WaveUpperLimitValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WiredSensor_EnvlFilterUpLimit", true)
                        .Any();
                    var isExistEnvlFilterLowerLimit = waveLowerLimitValuesRepository
                        .GetDatas<WaveLowerLimitValues>(t => t.VibratingSignalTypeID == vibType.ID
                            && t.Describe == "WiredSensor_EnvlFilterLowLimit", true)
                        .Any();
                    vibTypeData.IsExistEigenChild = isExistEigenChild;
                    vibTypeData.IsExistUpperLimitChild = isExistUpperLimit;
                    vibTypeData.IsExistLowerLimitChild = isExistLowerLimit;
                    vibTypeData.IsExistEnvlFilterUpperLimitChild = isExistEnvlFilterUpperLimit;
                    vibTypeData.IsExistEnvlFilterLowerLimitChild = isExistEnvlFilterLowerLimit;

                    #endregion

                    commonInfoList.Add(vibTypeData);
                }

                result.CommonInfoList = commonInfoList;
                response = new BaseResponse<CommonDataResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<CommonDataResult>("003291");
                return response;
            }
        }

        #endregion
    }
    #endregion
}