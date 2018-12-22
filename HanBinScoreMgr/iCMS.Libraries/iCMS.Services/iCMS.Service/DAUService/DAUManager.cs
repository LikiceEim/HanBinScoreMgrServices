using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

using iCMS.Service.Common;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Base.DB;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.DAUAgent;
using iCMS.Common.Component.Data.Request.DAUService;
using iCMS.Common.Component.Data.Response.DAUAgent;
using iCMS.Common.Component.Data.Response.DAUService;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Tool.Extensions;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Frameworks.Core.Repository;
using iCMS.Service.Web.DAUService.DAUAlarmManager;
using iCMS.Common.Component.Data.Response.Common;
using iCMS.Service.Web.DAUService.DAUAlarmParameter;

using Response = iCMS.Common.Component.Data.Response.DAUAgent;
using Request = iCMS.Common.Component.Data.Request.DAUAgent;

namespace iCMS.Service.Web.DAUService
{
    public partial class DAUManager : IDAUManager
    {
        /// <summary>
        /// 波形和特征值保存时间，单位天
        /// </summary>
        private readonly int WaveAndEigenValuePeriod = int.Parse(ConfigurationManager.AppSettings["WiredWaveAndEigenValuePeriod"]);

        /// <summary>
        /// 设备温度保存时间，单位天
        /// </summary>
        private readonly int DeviceTempePeriod = int.Parse(ConfigurationManager.AppSettings["WiredDeviceTempePeriod"]);

        /// <summary>
        /// 转速保存时间，单位天
        /// </summary>
        private readonly int RotatePeriod = int.Parse(ConfigurationManager.AppSettings["WiredRotatePeriod"]);

        #region  私有变量

        private readonly IRepository<WS> wsRepository;
        private readonly IRepository<MeasureSite> measureSiteRepository;
        private readonly IRepository<VibSingal> vibSignalRepository;
        private readonly IRepository<SpeedSamplingMDF> speedSamplingMDFRepository;
        private readonly IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository;
        private readonly IRepository<ModBusRegisterAddress> modbusRegisterAddressRepository;
        private readonly IRepository<Gateway> wgRepository;
        private readonly IRepository<Operation> operationRepository;
        private readonly IRepository<SignalAlmSet> signalAlmsetRepository;
        private readonly IRepository<EigenValueType> eigenValueTypeRepository;
        private readonly IRepository<MeasureSiteType> msTypeRepository;
        private readonly IRepository<Device> deviceRepository;
        private readonly IRepository<VibratingSingalCharHisAcc> vibratingSingalCharHisAccRepository;
        private readonly IRepository<VibratingSingalCharHisVel> vibratingSingalCharHisVelRepository;
        private readonly IRepository<VibratingSingalCharHisEnvl> vibratingSingalCharHisEnvlRepository;
        private readonly IRepository<SignalAlmSet> signalAlmSetRepository;
        private readonly IRepository<RealTimeCollectInfo> realTimeCollectInfoRepository;
        private readonly IRepository<RealTimeAlarmThreshold> realTimeAlarmThresholdRepository;
        private readonly IRepository<MonitorTree> monitorTreeRepository;
        private readonly IRepository<VibSingalRT> vibSingalRTRepository;
        private readonly IDeviceVibrationAlarm deviceVibrationAlarm;
        private readonly ICacheDICT cacheDICT;
        private readonly IRepository<ModBusRegisterAddress> modBusRegisterAddressRepository;

        private readonly IRepository<SpeedDeviceMSitedata_1> speedDeviceMSitedata_1Repository;
        private readonly IRepository<SpeedDeviceMSitedata_2> speedDeviceMSitedata_2Repository;
        private readonly IRepository<SpeedDeviceMSitedata_3> speedDeviceMSitedata_3Repository;
        private readonly IRepository<SpeedDeviceMSitedata_4> speedDeviceMSitedata_4Repository;

        private readonly IRepository<TempeDeviceMsitedata_1> tempeDeviceMsitedata_1Repository;
        private readonly IRepository<TempeDeviceMsitedata_2> tempeDeviceMsitedata_2Repository;
        private readonly IRepository<TempeDeviceMsitedata_3> tempeDeviceMsitedata_3Repository;
        private readonly IRepository<TempeDeviceMsitedata_4> tempeDeviceMsitedata_4Repository;

        private readonly IDeviceTemperatureAlarm deviceTemperatureAlarm;

        #endregion

        #region 构造函数

        public DAUManager(IRepository<WS> wsRepository
            , IRepository<MeasureSite> measureSiteRepository
            , IRepository<VibSingal> vibSignalRepository
            , IRepository<SpeedSamplingMDF> speedSamplingMDFRepository
            , IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository
            , IRepository<ModBusRegisterAddress> modbusRegisterAddressRepository
            , IRepository<Gateway> wgRepository
            , IRepository<Operation> operationRepository
            , IRepository<SignalAlmSet> signalAlmsetRepository
            , IRepository<EigenValueType> eigenValueTypeRepository
            , IRepository<MeasureSiteType> msTypeRepository
            , IRepository<Device> deviceRepository
            , IRepository<VibratingSingalCharHisAcc> vibratingSingalCharHisAccRepository
            , IRepository<VibratingSingalCharHisVel> vibratingSingalCharHisVelRepository
            , IRepository<VibratingSingalCharHisEnvl> vibratingSingalCharHisEnvlRepository
            , IRepository<SignalAlmSet> signalAlmSetRepository
            , IRepository<RealTimeCollectInfo> realTimeCollectInfoRepository
            , IRepository<RealTimeAlarmThreshold> realTimeAlarmThresholdRepository
            , IRepository<MonitorTree> monitorTreeRepository
            , IRepository<VibSingalRT> vibSingalRTRepository
            , IDeviceVibrationAlarm deviceVibrationAlarm
            , ICacheDICT cacheDICT
            , IRepository<ModBusRegisterAddress> modBusRegisterAddressRepository

            , IRepository<SpeedDeviceMSitedata_1> speedDeviceMSitedata_1Repository
            , IRepository<SpeedDeviceMSitedata_2> speedDeviceMSitedata_2Repository
            , IRepository<SpeedDeviceMSitedata_3> speedDeviceMSitedata_3Repository
            , IRepository<SpeedDeviceMSitedata_4> speedDeviceMSitedata_4Repository

            , IRepository<TempeDeviceMsitedata_1> tempeDeviceMsitedata_1Repository
            , IRepository<TempeDeviceMsitedata_2> tempeDeviceMsitedata_2Repository
            , IRepository<TempeDeviceMsitedata_3> tempeDeviceMsitedata_3Repository
            , IRepository<TempeDeviceMsitedata_4> tempeDeviceMsitedata_4Repository

            , IDeviceTemperatureAlarm deviceTemperatureAlarm)
        {
            this.wsRepository = wsRepository;
            this.measureSiteRepository = measureSiteRepository;
            this.vibSignalRepository = vibSignalRepository;
            this.speedSamplingMDFRepository = speedSamplingMDFRepository;
            this.tempeDeviceSetMSiteAlmRepository = tempeDeviceSetMSiteAlmRepository;
            this.modbusRegisterAddressRepository = modbusRegisterAddressRepository;
            this.wgRepository = wgRepository;
            this.operationRepository = operationRepository;
            this.signalAlmsetRepository = signalAlmsetRepository;
            this.eigenValueTypeRepository = eigenValueTypeRepository;
            this.msTypeRepository = msTypeRepository;
            this.deviceRepository = deviceRepository;
            this.vibratingSingalCharHisAccRepository = vibratingSingalCharHisAccRepository;
            this.vibratingSingalCharHisVelRepository = vibratingSingalCharHisVelRepository;
            this.vibratingSingalCharHisEnvlRepository = vibratingSingalCharHisEnvlRepository;
            this.signalAlmSetRepository = signalAlmSetRepository;
            this.realTimeCollectInfoRepository = realTimeCollectInfoRepository;
            this.realTimeAlarmThresholdRepository = realTimeAlarmThresholdRepository;
            this.monitorTreeRepository = monitorTreeRepository;
            this.vibSingalRTRepository = vibSingalRTRepository;
            this.deviceVibrationAlarm = deviceVibrationAlarm;
            this.cacheDICT = cacheDICT;
            this.modBusRegisterAddressRepository = modBusRegisterAddressRepository;

            this.speedDeviceMSitedata_1Repository = speedDeviceMSitedata_1Repository;
            this.speedDeviceMSitedata_2Repository = speedDeviceMSitedata_2Repository;
            this.speedDeviceMSitedata_3Repository = speedDeviceMSitedata_3Repository;
            this.speedDeviceMSitedata_4Repository = speedDeviceMSitedata_4Repository;

            this.tempeDeviceMsitedata_1Repository = tempeDeviceMsitedata_1Repository;
            this.tempeDeviceMsitedata_2Repository = tempeDeviceMsitedata_2Repository;
            this.tempeDeviceMsitedata_3Repository = tempeDeviceMsitedata_3Repository;
            this.tempeDeviceMsitedata_4Repository = tempeDeviceMsitedata_4Repository;

            this.deviceTemperatureAlarm = deviceTemperatureAlarm;
        }

        #endregion

        public BaseResponse<bool> SetDAUMDF(SetDAUMDFParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                //1. 遍历DAUID, 根据DAUID找到WSID, 再找到MSiteID, 根据测点ID找到相应的振动信号测量定义， 转速测量定义， 温度测量定义  
                //2. 填充数据
                //3. 从DAUID找到DAU Agent Server， 根据Server地址将测量定义分组
                //4. 向Operation表插入数据，调用Agent服务，下发测量定义
                //5. 等待Agent服务返回结果，更新Operaton表操作状态
                //6. 完成

                if (!param.DAUID.Any())
                {
                    response.IsSuccessful = false;
                    response.Code = "000000";
                    //response.Reason = "设置DAU测量定义参数异常";
                    return response;
                }

                //获取采集单元下的传感器信息集合
                var wsInfoList = wsRepository
                    .GetDatas<WS>(wsInfo => param.DAUID.Contains(wsInfo.WGID), false)
                    .ToList();
                var wsIDList = wsInfoList.Select(wsInfo => wsInfo.WSID as int?).ToList();
                //获取测量位置信息集合
                var mSiteInfoList = measureSiteRepository
                    .GetDatas<MeasureSite>(mSiteInfo => wsIDList.Contains(mSiteInfo.WSID), false)
                    .ToList();
                var mSiteIDList = mSiteInfoList.Select(mSiteInfo => mSiteInfo.MSiteID).ToList();
                //获取振动信号测量定义集合
                var vibSignalInfoList = vibSignalRepository
                    .GetDatas<VibSingal>(vibInfo => mSiteIDList.Contains(vibInfo.MSiteID), true)
                    .ToList();
                //获取转速测量定义集合
                var speedSamplingMDFInfoList = speedSamplingMDFRepository
                    .GetDatas<SpeedSamplingMDF>(speedMDFInfo => mSiteIDList.Contains(speedMDFInfo.MSiteID), true)
                    .ToList();

                #region 获取温度测量定义

                var tempDAUIDList = param.DAUID.Select(dauID => dauID as int?);
                //获取设备温度阈值信息集合
                var temperatureDeviceInfoList = tempeDeviceSetMSiteAlmRepository
                    .GetDatas<TempeDeviceSetMSiteAlm>(temperDevInfo => tempDAUIDList.Contains(temperDevInfo.WGID), false)
                    .ToList();
                //设备温度阈值ID集合
                var temperatureDeviceIDList = temperatureDeviceInfoList
                    .Select(temperDevInfo => temperDevInfo.MsiteAlmID)
                    .ToList();
                //获取温度通道信息集合
                var temperatureChannelInfoList = modbusRegisterAddressRepository
                    .GetDatas<ModBusRegisterAddress>(regAdd => temperatureDeviceIDList.Contains(regAdd.MDFID)
                        && regAdd.MDFResourceTable == "T_SYS_TEMPE_DEVICE_SET_MSITEALM", false)
                    .ToList();

                #endregion

                //获取上限频率
                Func<VibSingal, bool, int?> getUpLimitFreq = (vib, isFilterUpFreq) =>
                {
                    if (vib == null)
                        return null;

                    switch (vib.SingalType)
                    {
                        case (int)EnumVibSignalType.Accelerated:
                        case (int)EnumVibSignalType.Velocity:
                            {
                                WaveUpperLimitValues upperLimit =
                                    cacheDICT.GetCacheType<WaveUpperLimitValues>(
                                        item => item.ID == vib.UpLimitFrequency)
                                    .SingleOrDefault();
                                return upperLimit == null ? null : upperLimit.WaveUpperLimitValue as int?;
                            }

                        case (int)EnumVibSignalType.Envelope:
                            {
                                WaveUpperLimitValues upperLimit;
                                if (isFilterUpFreq)
                                {
                                    upperLimit = cacheDICT.GetCacheType<WaveUpperLimitValues>(
                                           item => item.ID == vib.EnvelopeFilterUpLimitFreq)
                                       .SingleOrDefault();
                                }
                                else
                                {
                                    upperLimit = cacheDICT.GetCacheType<WaveUpperLimitValues>(
                                           item => item.ID == vib.EnlvpBandW)
                                       .SingleOrDefault();
                                }
                                return upperLimit == null ? null : upperLimit.WaveUpperLimitValue as int?;
                            }

                        default:
                            return null;
                    }
                };

                //获取下限频率
                Func<VibSingal, int?> getLowLimitFreq = (vib) =>
                {
                    if (vib == null)
                        return null;

                    switch (vib.SingalType)
                    {
                        case (int)EnumVibSignalType.Accelerated:
                        case (int)EnumVibSignalType.Velocity:
                            {
                                WaveLowerLimitValues lowerLimit =
                                    cacheDICT.GetCacheType<WaveLowerLimitValues>(
                                        item => item.ID == vib.LowLimitFrequency)
                                    .SingleOrDefault();
                                return lowerLimit == null ? null : lowerLimit.WaveLowerLimitValue as int?;
                            }

                        case (int)EnumVibSignalType.Envelope:
                            {
                                WaveLowerLimitValues lowerLimit =
                                    cacheDICT.GetCacheType<WaveLowerLimitValues>(
                                        item => item.ID == vib.EnvelopeFilterLowLimitFreq)
                                    .SingleOrDefault();
                                return lowerLimit == null ? null : lowerLimit.WaveLowerLimitValue as int?;
                            }

                        default:
                            return null;
                    }
                };

                //获取传感器校准参数A
                Func<VibSingal, float> getSensorCalibParamsA = (vib) =>
                {
                    if (vib == null)
                        return 0;

                    MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == vib.MSiteID);
                    return mSiteInfo == null ? 0 : mSiteInfo.SensorCosA;
                };
                //获取传感器校准参数B
                Func<VibSingal, float> getSensorCalibParamsB = (vib) =>
                {
                    if (vib == null)
                        return 0;

                    MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == vib.MSiteID);
                    return mSiteInfo == null ? 0 : mSiteInfo.SensorCosB;
                };
                //获取传感器灵敏度系数
                Func<VibSingal, float?> getSenserCoefficient = (vib) =>
                {
                    if (vib == null)
                        return null;

                    MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == vib.MSiteID);
                    return mSiteInfo == null ? null : mSiteInfo.SensorCoefficient;
                };
                //获取振动采集通道
                Func<VibSingal, int?> getVibChannelID = (vib) =>
                {
                    if (vib == null)
                        return null;

                    MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == vib.MSiteID);
                    if (mSiteInfo != null)
                    {
                        WS wsInfo = wsInfoList.SingleOrDefault(item => item.WSID == mSiteInfo.WSID);
                        return wsInfo == null ? null : wsInfo.ChannelId;
                    }
                    else
                        return null;
                };

                //Agent测量定义列表
                List<DAUAgentMDFParameter> DAUAgentMDFParamsList = new List<DAUAgentMDFParameter>();
                foreach (int dauID in param.DAUID)
                {
                    DAUAgentMDFParameter dauAgentMDF = new DAUAgentMDFParameter();
                    dauAgentMDF.DAUID = dauID;

                    //找到DAU下对应的WS
                    var wsList = wsInfoList
                        .Where(t => t.WGID == dauID)
                        .Select(t => t.WSID)
                        .ToList();

                    //找到WS对应的测点信息
                    var measureSiteList = mSiteInfoList
                        .Where(t => t.WSID.HasValue && wsList.Contains(t.WSID.Value))
                        .ToList();
                    var measureSiteIDList = measureSiteList.Select(t => t.MSiteID).ToList();

                    #region 封装数据

                    List<Request.WaveMDF> wavePDFList = new List<Request.WaveMDF>();

                    #region 封装振动信号测量定义

                    //振动信号测量定义
                    var vibSignalList = vibSignalInfoList
                        .Where(t => measureSiteIDList.Contains(t.MSiteID))
                        .ToList();

                    vibSignalList.ForEach((vib) =>
                    {
                        Request.WaveMDF wavePDF = new Request.WaveMDF();
                        wavePDF.VibSingalType = vib.SingalType;
                        wavePDF.MSiteID = vib.MSiteID;
                        int? channelID = getVibChannelID(vib);
                        if (!channelID.HasValue)
                            return;
                        wavePDF.ChannelId = channelID.Value;
                        wavePDF.RelateChnId = 65535;

                        switch (wavePDF.VibSingalType)
                        {
                            case (int)EnumVibSignalType.Accelerated:
                                wavePDF.ACCSamplingTimePeriod = vib.SamplingTimePeriod;
                                wavePDF.ACCFreqUpperLimit = getUpLimitFreq(vib, false);
                                wavePDF.ACCFreqLowerLimit = getLowLimitFreq(vib);
                                break;

                            case (int)EnumVibSignalType.Velocity:
                                wavePDF.VELSamplingTimePeriod = vib.SamplingTimePeriod;
                                wavePDF.VELFreqUpperLimit = getUpLimitFreq(vib, false);
                                wavePDF.VELFreqLowerLimit = getLowLimitFreq(vib);
                                break;

                            case (int)EnumVibSignalType.Displacement:
                                break;

                            case (int)EnumVibSignalType.Envelope:
                                wavePDF.ENVLSamplingTimePeriod = vib.SamplingTimePeriod;
                                wavePDF.ENVLFilterUpperLimit = getUpLimitFreq(vib, true);
                                wavePDF.ENVLFilterLowerLimit = getLowLimitFreq(vib);
                                wavePDF.ENVLBand = getUpLimitFreq(vib, false);
                                break;

                            case (int)EnumVibSignalType.LQ:
                                break;
                        }
                        var tempMS = measureSiteList.Where(t => t.MSiteID == vib.MSiteID).FirstOrDefault();
                        if (tempMS != null)
                        {
                            wavePDF.SenserCalibrateparamaA = tempMS.SensorCosA;
                            wavePDF.SenserCalibrateparamaB = tempMS.SensorCosB;
                            wavePDF.SensorCoefficient = tempMS.SensorCoefficient;
                        }

                        wavePDFList.Add(wavePDF);
                    });

                    dauAgentMDF.WaveMDFList = wavePDFList;

                    #endregion

                    #region 封装转速测量定义

                    //获取转速采集通道
                    Func<SpeedSamplingMDF, int?> getSPDChannelID = (speed) =>
                    {
                        if (speed == null)
                            return null;

                        MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == speed.MSiteID);
                        if (mSiteInfo != null)
                        {
                            WS wsInfo = wsInfoList.SingleOrDefault(item => item.WSID == mSiteInfo.WSID);
                            return wsInfo == null ? null : wsInfo.ChannelId;
                        }
                        else
                            return null;
                    };

                    dauAgentMDF.SpdMDFList = speedSamplingMDFInfoList
                        .Where(t => measureSiteIDList.Contains(t.MSiteID))
                        .Select(t => new Request.SpdMDF
                        {
                            MSiteID = t.MSiteID,
                            ChannelId = getSPDChannelID(t) ?? -1,
                            PulsNumPerP = t.PulsNumPerP,
                        })
                        .Where(t => t.ChannelId > -1)
                        .ToList();

                    #endregion

                    #region 封装温度测量定义

                    var temperDevIDList = temperatureDeviceInfoList
                        .Where(temperDevInfo => temperDevInfo.WGID == dauID)
                        .Select(temperDevInfo => temperDevInfo.MsiteAlmID);

                    dauAgentMDF.TemperatureMDFList = temperatureChannelInfoList
                        .Where(regAdd => temperDevIDList.Contains(regAdd.MDFID)
                            && regAdd.MDFResourceTable == "T_SYS_TEMPE_DEVICE_SET_MSITEALM")
                        .Select(regAdd => new Request.TemperatureMDF
                        {
                            RegisterAddress = regAdd.RegisterAddress,
                            StrEnumRegisterType = regAdd.StrEnumRegisterType,
                            StrEnumRegisterStorageMode = regAdd.StrEnumRegisterStorageMode,
                            StrEnumRegisterStorageSequenceMode = regAdd.StrEnumRegisterStorageSequenceMode,
                            StrEnumRegisterInformation = regAdd.StrEnumRegisterInformation,
                        })
                        .ToList();

                    #endregion

                    #endregion

                    DAUAgentMDFParamsList.Add(dauAgentMDF);
                }

                #region Agent测量定义参数根据ServerAddress分组

                //获取有线网关信息
                var wgInfoList = wgRepository
                    .GetDatas<Gateway>(wgInfo => param.DAUID.Contains(wgInfo.WGID), false)
                    .ToList();
                //保存根据Agent地址分组的信息
                Dictionary<string, List<DAUAgentMDFParameter>> agentParamDIC =
                    new Dictionary<string, List<DAUAgentMDFParameter>>();

                DAUAgentMDFParamsList.ForEach((dauMDFParam) =>
                {
                    var dau = wgInfoList.SingleOrDefault(wgInfo => wgInfo.WGID == dauMDFParam.DAUID);
                    if (dau == null)
                        return;

                    List<DAUAgentMDFParameter> innerMDFParameter = null;
                    if (agentParamDIC.TryGetValue(dau.AgentAddress, out innerMDFParameter))
                        innerMDFParameter.Add(dauMDFParam);
                    else
                    {
                        innerMDFParameter = new List<DAUAgentMDFParameter>();
                        innerMDFParameter.Add(dauMDFParam);
                        agentParamDIC.Add(dau.AgentAddress, innerMDFParameter);
                    }
                });

                #endregion

                #region 向Operation表插入操作状态

                List<int> MSiteList = new List<int>();
                DAUAgentMDFParamsList.ForEach((dauAgentMDF) =>
                {
                    MSiteList.AddRange(dauAgentMDF.WaveMDFList.Select(t => t.MSiteID).Distinct().ToList());
                    MSiteList.AddRange(dauAgentMDF.SpdMDFList.Select(t => t.MSiteID).Distinct().ToList());
                });

                Dictionary<EntityBase, EntityState> dicOperators = new Dictionary<EntityBase, EntityState>();
                MSiteList.ForEach((msid) =>
                {
                    int wsid = 0;
                    var measureSite = mSiteInfoList.SingleOrDefault(mSiteInfo => mSiteInfo.MSiteID == msid);
                    if (measureSite != null)
                        wsid = measureSite.WSID ?? 0;

                    string strGUID = System.Guid.NewGuid().ToString();
                    Operation oper = new Operation();
                    oper.Bdate = DateTime.Now;
                    oper.MSID = msid;
                    oper.WSID = wsid;
                    oper.OperatorKey = strGUID;
                    oper.OperationType = 1;//下发测量定义
                    oper.OperationResult = "3";//操作中
                    oper.DAQStyle = 1;
                    //其余字段在Vibscan Pro中赋值为空，无意义
                    dicOperators.Add(oper, EntityState.Added);
                });

                OperationResult operResult = operationRepository.TranMethod(dicOperators);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("下发测量定义发生异常");
                }

                dicOperators.Clear();

                #endregion

                #region 分组下发测量定义

                agentParamDIC.Keys.ToList().ForEach((agentAddress) =>
                {
                    string tempAgentAddress = agentAddress;
                    Task.Run(() =>
                    {
                        SetDAUAgentMDFParameter DAUAgentMDFParam = new SetDAUAgentMDFParameter();
                        DAUAgentMDFParam.DAUAgentMDFParams = agentParamDIC[tempAgentAddress];
                        RestClient restClient = new RestClient(tempAgentAddress);

                        string backStr = restClient.Post(DAUAgentMDFParam.ToClientString(), "SetDAUMDF", 10 * 60 * 1000);

                        BaseResponse<SetDAUAgentMDFResult> agentResponse =
                            Json.JsonDeserialize<BaseResponse<SetDAUAgentMDFResult>>(backStr);

                        if (agentResponse != null && agentResponse.Result != null
                            && agentResponse.Result.DAUAgentMDFItemResultList.Count > 0)
                        {
                            //取出当前所有的ws操作信息数据
                            var operationInfoList = operationRepository
                                .GetDatas<Operation>(operInfo => wsIDList.Contains(operInfo.WSID)
                                    && operInfo.OperationType == 1 && operInfo.OperationResult == "3", false)
                                .ToList();

                            //TODO:A更改Operation表状态
                            agentResponse.Result.DAUAgentMDFItemResultList.ForEach((item) =>
                            {
                                int dauID = item.DAUID;
                                bool isSuccess = item.IsSuccess;

                                //DAU 对应的WS列表
                                var wsidList = wsInfoList
                                    .Where(t => t.WGID == dauID)
                                    .Select(t => t.WSID)
                                    .ToList();

                                //修改WS的操作状态
                                var oprations = operationInfoList
                                    .Where(t => t.WSID.HasValue && wsidList.Contains(t.WSID.Value))
                                    .ToList();

                                oprations.ForEach((oper) =>
                                {
                                    oper.OperationResult = isSuccess ? "1" : "2";
                                    oper.EDate = DateTime.Now;

                                    dicOperators.Add(oper, EntityState.Modified);
                                });

                                if (item.IsSuccess)
                                {
                                    Gateway dau = wgInfoList.SingleOrDefault(wgInfo => wgInfo.WGID == item.DAUID);
                                    if (dau != null)
                                    {
                                        dau.CurrentDAUStates = (int)EnumDAUStatus.ConfigingStatus;
                                        dicOperators.Add(dau, EntityState.Modified);
                                    }
                                }
                            });

                            operationRepository.TranMethod(dicOperators);

                            if (operResult.ResultType != EnumOperationResultType.Success)
                            {
                                throw new Exception("下发测量定义发生异常");
                            }
                        }
                    });
                });

                #endregion

                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "0000000";

                return response;
            }
        }

        public BaseResponse<bool> StartCollection(StartCollectionParameter param)
        {
            BaseResponse<bool> respo = new BaseResponse<bool>();
            BaseResponse<StartCollectionResult> response = new BaseResponse<StartCollectionResult>();
            try
            {
                #region 根据 DAUAgentURL分组下发开始采集命令

                #region 分组

                Dictionary<string, List<int>> groupeStartCollectionParam = new Dictionary<string, List<int>>();
                foreach (int dauID in param.DAUIDList)
                {
                    List<int> innerParam = null;
                    string agentURL = wgRepository.GetByKey(dauID).AgentAddress;
                    if (!groupeStartCollectionParam.Keys.Contains(agentURL))
                    {
                        innerParam = new List<int>();
                        innerParam.Add(dauID);

                        groupeStartCollectionParam.Add(agentURL, innerParam);
                    }
                    else
                    {
                        innerParam = groupeStartCollectionParam[agentURL];
                        innerParam.Add(dauID);
                    }
                }

                #endregion

                #region 下发开始采集

                foreach (string agentURL in groupeStartCollectionParam.Keys)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            StartCollectionParameter PARAM = new StartCollectionParameter();
                            PARAM.DAUIDList = groupeStartCollectionParam[agentURL];
                            RestClient restClient = new RestClient(agentURL);

                            string backStr = restClient.Post(PARAM.ToClientString(), "StartCollection");

                            //与ZLK确认：调用Agent的开始采集命令是 同步方法 Added by QXM 
                            response = Json.JsonDeserialize<BaseResponse<StartCollectionResult>>(backStr);
                            StartCollectionResult result = response.Result;

                            foreach (var item in result.StartCollectionResultList)
                            {
                                if (!item.IsSuccess)
                                {
                                    throw new Exception("开始启动采集发生异常");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteLog(e);
                        }
                    });
                }

                #endregion

                #endregion

                return respo;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                respo.IsSuccessful = false;
                respo.Code = "0000000";

                return respo;
            }
        }

        #region 设置DAU停止采集

        /// <summary>
        /// 返回结果无意义，不作为命令执行结果
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> StopCollection(StopCollectionParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            StopCollectionResult result = new StopCollectionResult();

            try
            {
                #region 根据DAU Agent地址来分组设置停止采集

                Dictionary<string, List<int>> groupedStopCollectionParam =
                    new Dictionary<string, List<int>>();
                string agentURL = string.Empty;
                Gateway wg = null;
                List<Gateway> wgList = wgRepository
                    .GetDatas<Gateway>(t => param.DAUIDList.Contains(t.WGID), true)
                    .ToList();
                foreach (int dauID in param.DAUIDList)
                {
                    List<int> innerParam = null;
                    wg = wgList.Where(t => t.WGID == dauID).FirstOrDefault();
                    if (null != wg)
                    {
                        agentURL = wg.AgentAddress;
                        if (!groupedStopCollectionParam.Keys.Contains(agentURL))
                        {
                            innerParam = new List<int>();
                            innerParam.Add(dauID);

                            groupedStopCollectionParam.Add(agentURL, innerParam);
                        }
                        else
                        {
                            innerParam = groupedStopCollectionParam[agentURL];
                            innerParam.Add(dauID);
                        }
                    }
                }

                #endregion

                foreach (string keyAgentURL in groupedStopCollectionParam.Keys)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            List<int> dauIDListPerUrl = groupedStopCollectionParam[keyAgentURL];
                            StopCollectionParameter PARAM = new StopCollectionParameter();
                            PARAM.DAUIDList = dauIDListPerUrl;

                            RestClient restClient = new RestClient(keyAgentURL);

                            string backStr = restClient.Post(PARAM.ToClientString(), "StopCollection");

                            var res = Json.JsonDeserialize<BaseResponse<StopCollectionResult>>(backStr);
                            //更新数据库DAU表当前状态字段，前端循环刷新获取DAU.CurrentStates字段
                            result = res.Result;

                            if (result != null && result.StopCollectionResultList != null
                                && result.StopCollectionResultList.Count > 0)
                            {
                                var dauIDList = result.StopCollectionResultList
                                    .Where(t => t.IsSuccess).Select(t => t.DAUID)
                                    .ToList();
                                var DAUs = wgRepository
                                    .GetDatas<Gateway>(t => dauIDList.Contains(t.WGID), true)
                                    .ToList();
                                foreach (StopCollectionItemResult item in result.StopCollectionResultList)
                                {
                                    if (item.IsSuccess)
                                    {
                                        var innerDAU = DAUs.Where(t => t.WGID == item.DAUID).FirstOrDefault();
                                        if (innerDAU != null)
                                        {
                                            innerDAU.CurrentDAUStates = (int)EnumDAUStatus.IdleStatus;
                                            wgRepository.Update(innerDAU);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteLog(e);
                        }
                    });
                }

                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "010491";

                return response;
            }
        }

        #endregion

        #region 获取DAU测量定义

        public BaseResponse<GetDAUMDFResult> GetDAUMDF(GetDAUMDFParameter param)
        {
            BaseResponse<GetDAUMDFResult> response = new BaseResponse<GetDAUMDFResult>();
            GetDAUMDFResult result = new GetDAUMDFResult();

            try
            {
                var DAU = wgRepository.GetByKey(param.DAUID);
                if (null == DAU)
                {
                    response.IsSuccessful = false;
                    response.Code = "000000";

                    return response;
                }

                //获取上限频率
                Func<VibSingal, bool, int?> getUpLimitFreq = (vib, isFilterUpFreq) =>
                {
                    if (vib == null)
                        return null;

                    switch (vib.SingalType)
                    {
                        case (int)EnumVibSignalType.Accelerated:
                        case (int)EnumVibSignalType.Velocity:
                            {
                                WaveUpperLimitValues upperLimit =
                                    cacheDICT.GetCacheType<WaveUpperLimitValues>(
                                        item => item.ID == vib.UpLimitFrequency)
                                    .SingleOrDefault();
                                return upperLimit == null ? null : upperLimit.WaveUpperLimitValue as int?;
                            }

                        case (int)EnumVibSignalType.Envelope:
                            {
                                WaveUpperLimitValues upperLimit;
                                if (isFilterUpFreq)
                                {
                                    upperLimit = cacheDICT.GetCacheType<WaveUpperLimitValues>(
                                           item => item.ID == vib.EnvelopeFilterUpLimitFreq)
                                       .SingleOrDefault();
                                }
                                else
                                {
                                    upperLimit = cacheDICT.GetCacheType<WaveUpperLimitValues>(
                                           item => item.ID == vib.EnlvpBandW)
                                       .SingleOrDefault();
                                }
                                return upperLimit == null ? null : upperLimit.WaveUpperLimitValue as int?;
                            }

                        default:
                            return null;
                    }
                };

                //获取下限频率
                Func<VibSingal, int?> getLowLimitFreq = (vib) =>
                {
                    if (vib == null)
                        return null;

                    switch (vib.SingalType)
                    {
                        case (int)EnumVibSignalType.Accelerated:
                        case (int)EnumVibSignalType.Velocity:
                            {
                                WaveLowerLimitValues lowerLimit =
                                    cacheDICT.GetCacheType<WaveLowerLimitValues>(
                                        item => item.ID == vib.LowLimitFrequency)
                                    .SingleOrDefault();
                                return lowerLimit == null ? null : lowerLimit.WaveLowerLimitValue as int?;
                            }

                        case (int)EnumVibSignalType.Envelope:
                            {
                                WaveLowerLimitValues lowerLimit =
                                    cacheDICT.GetCacheType<WaveLowerLimitValues>(
                                        item => item.ID == vib.EnvelopeFilterLowLimitFreq)
                                    .SingleOrDefault();
                                return lowerLimit == null ? null : lowerLimit.WaveLowerLimitValue as int?;
                            }

                        default:
                            return null;
                    }
                };

                MDPPerDAU MDFPerDau = new MDPPerDAU();

                //获取采集单元下的传感器信息集合
                var wsInfoList = wsRepository
                    .GetDatas<WS>(wsInfo => wsInfo.WGID == param.DAUID, false)
                    .ToList();
                var wsIDList = wsInfoList.Select(wsInfo => wsInfo.WSID as int?).ToList();
                //获取测量位置信息集合
                var mSiteInfoList = measureSiteRepository
                    .GetDatas<MeasureSite>(mSiteInfo => wsIDList.Contains(mSiteInfo.WSID), false)
                    .ToList();
                var mSiteIDList = mSiteInfoList.Select(mSiteInfo => mSiteInfo.MSiteID).ToList();
                //获取振动信号测量定义集合
                var vibSignalInfoList = vibSignalRepository
                    .GetDatas<VibSingal>(vibInfo => mSiteIDList.Contains(vibInfo.MSiteID), true)
                    .ToList();
                //获取转速测量定义集合
                var speedSamplingMDFInfoList = speedSamplingMDFRepository
                    .GetDatas<SpeedSamplingMDF>(speedMDFInfo => mSiteIDList.Contains(speedMDFInfo.MSiteID), true)
                    .ToList();

                #region 获取温度测量定义

                //获取设备温度阈值信息集合
                var temperatureDeviceInfoList = tempeDeviceSetMSiteAlmRepository
                    .GetDatas<TempeDeviceSetMSiteAlm>(temperDevInfo => mSiteIDList.Contains(temperDevInfo.MsiteID), false)
                    .ToList();
                //设备温度阈值ID集合
                var temperatureDeviceIDList = temperatureDeviceInfoList
                    .Select(temperDevInfo => temperDevInfo.MsiteAlmID)
                    .ToList();
                //获取温度通道信息集合
                var temperatureChannelInfoList = modbusRegisterAddressRepository
                    .GetDatas<ModBusRegisterAddress>(regAdd => temperatureDeviceIDList.Contains(regAdd.MDFID)
                        && regAdd.MDFResourceTable == "T_SYS_TEMPE_DEVICE_SET_MSITEALM", false)
                    .ToList();

                #endregion

                //特征值阈值配置信息
                List<SignalAlmSet> signalAlmSetList = new List<SignalAlmSet>();
                //特征值类型信息
                List<EigenValueType> eigenValueTypeList = new List<EigenValueType>();
                if (vibSignalInfoList != null && vibSignalInfoList.Any())
                {
                    //获取特征值阈值配置信息
                    int[] singalIDList = vibSignalInfoList.Select(item => item.SingalID).ToArray();
                    signalAlmSetList.AddRange(signalAlmSetRepository
                        .GetDatas<SignalAlmSet>(item => singalIDList.Contains(item.SingalID), false));
                    //获取特征值类型信息
                    int[] eigengValueIDList = signalAlmSetList
                        .Select(item => item.ValueType)
                        .Distinct()
                        .ToArray();
                    eigenValueTypeList.AddRange(cacheDICT
                        .GetCacheType<EigenValueType>(item => eigengValueIDList.Contains(item.ID)));
                }

                //获取传感器校准参数A
                Func<VibSingal, float> getSensorCalibParamsA = (vib) =>
                {
                    if (vib == null)
                        return 0;

                    MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == vib.MSiteID);
                    return mSiteInfo == null ? 0 : mSiteInfo.SensorCosA;
                };
                //获取传感器校准参数B
                Func<VibSingal, float> getSensorCalibParamsB = (vib) =>
                {
                    if (vib == null)
                        return 0;

                    MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == vib.MSiteID);
                    return mSiteInfo == null ? 0 : mSiteInfo.SensorCosB;
                };
                //获取传感器灵敏度系数
                Func<VibSingal, float?> getSenserCoefficient = (vib) =>
                {
                    if (vib == null)
                        return null;

                    MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == vib.MSiteID);
                    return mSiteInfo == null ? null : mSiteInfo.SensorCoefficient;
                };
                //获取振动采集通道
                Func<VibSingal, int?> getVibChannelID = (vib) =>
                {
                    if (vib == null)
                        return null;

                    MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == vib.MSiteID);
                    if (mSiteInfo != null)
                    {
                        WS wsInfo = wsInfoList.SingleOrDefault(item => item.WSID == mSiteInfo.WSID);
                        return wsInfo == null ? null : wsInfo.ChannelId;
                    }
                    else
                        return null;
                };
                //获取报警规则
                Func<VibSingal, List<AlarmRulesParams>> getAlarmRules = (vib) =>
                {
                    List<AlarmRulesParams> alarmRulesParamsList = new List<AlarmRulesParams>();
                    if (vib == null || signalAlmSetList.Count <= 0 || eigenValueTypeList.Count <= 0)
                        return alarmRulesParamsList;

                    foreach (var item in signalAlmSetList.Where(item => item.SingalID == vib.SingalID))
                    {
                        AlarmRulesParams alarmRulesParams = new AlarmRulesParams();
                        alarmRulesParams.WarnValue = item.WarnValue;
                        EigenValueType eigenValueType = eigenValueTypeList
                            .FirstOrDefault(firstItem => firstItem.ID == item.ValueType);
                        if (eigenValueType == null)
                            continue;
                        alarmRulesParams.EigenValueName = eigenValueType.Name;
                        alarmRulesParamsList.Add(alarmRulesParams);
                    }
                    return alarmRulesParamsList;
                };

                MDFPerDau.DAUID = param.DAUID;
                MDFPerDau.WaveMDFList = new List<Response.WaveMDF>();
                //波形测量定义
                vibSignalInfoList.ForEach((vib) =>
                {
                    Response.WaveMDF wavePDF = new Response.WaveMDF();
                    wavePDF.VibSingalType = vib.SingalType;
                    wavePDF.MSiteID = vib.MSiteID;
                    int? channelID = getVibChannelID(vib);
                    if (!channelID.HasValue)
                        return;
                    wavePDF.ChannelId = channelID.Value;
                    wavePDF.RelateChnId = 65535;

                    switch (wavePDF.VibSingalType)
                    {
                        case (int)EnumVibSignalType.Accelerated:
                            wavePDF.ACCSamplingTimePeriod = vib.SamplingTimePeriod;
                            wavePDF.ACCFreqUpperLimit = getUpLimitFreq(vib, false);
                            wavePDF.ACCFreqLowerLimit = getLowLimitFreq(vib);
                            break;

                        case (int)EnumVibSignalType.Velocity:
                            wavePDF.VELSamplingTimePeriod = vib.SamplingTimePeriod;
                            wavePDF.VELFreqUpperLimit = getUpLimitFreq(vib, false);
                            wavePDF.VELFreqLowerLimit = getLowLimitFreq(vib);
                            break;

                        case (int)EnumVibSignalType.Displacement:
                            break;

                        case (int)EnumVibSignalType.Envelope:
                            wavePDF.ENVLSamplingTimePeriod = vib.SamplingTimePeriod;
                            wavePDF.ENVLFilterUpperLimit = getUpLimitFreq(vib, true);
                            wavePDF.ENVLFilterLowerLimit = getLowLimitFreq(vib);
                            wavePDF.ENVLBand = getUpLimitFreq(vib, false);
                            break;

                        case (int)EnumVibSignalType.LQ:
                            break;
                    }
                    var tempMS = mSiteInfoList.Where(t => t.MSiteID == vib.MSiteID).FirstOrDefault();
                    if (tempMS != null)
                    {
                        wavePDF.SenserCalibrateparamaA = tempMS.SensorCosA;
                        wavePDF.SenserCalibrateparamaB = tempMS.SensorCosB;
                        wavePDF.SensorCoefficient = tempMS.SensorCoefficient;
                    }

                    wavePDF.AlarmRulesParams = getAlarmRules(vib);

                    MDFPerDau.WaveMDFList.Add(wavePDF);
                });

                #region 封装转速测量定义

                //获取转速采集通道
                Func<SpeedSamplingMDF, int?> getSPDChannelID = (speed) =>
                {
                    if (speed == null)
                        return null;

                    MeasureSite mSiteInfo = mSiteInfoList.SingleOrDefault(item => item.MSiteID == speed.MSiteID);
                    if (mSiteInfo != null)
                    {
                        WS wsInfo = wsInfoList.SingleOrDefault(item => item.WSID == mSiteInfo.WSID);
                        return wsInfo == null ? null : wsInfo.ChannelId;
                    }
                    else
                        return null;
                };
                //转速测量定义
                MDFPerDau.SpdMDFList = speedSamplingMDFInfoList.Select(t => new Response.SpdMDF
                {
                    MSiteID = t.MSiteID,
                    ChannelId = getSPDChannelID(t) ?? -1,
                    PulsNumPerP = t.PulsNumPerP,
                })
                .Where(t => t.ChannelId > -1)
                .ToList();

                #endregion

                #region 封装温度测量定义

                var temperDevIDList = temperatureDeviceInfoList
                    .Where(temperDevInfo => temperDevInfo.WGID == param.DAUID)
                    .Select(temperDevInfo => temperDevInfo.MsiteAlmID);

                MDFPerDau.TemperatureMDFList = temperatureChannelInfoList
                    .Where(regAdd => temperDevIDList.Contains(regAdd.MDFID)
                        && regAdd.MDFResourceTable == "T_SYS_TEMPE_DEVICE_SET_MSITEALM")
                    .Select(regAdd => new Response.TemperatureMDF
                    {
                        RegisterAddress = regAdd.RegisterAddress,
                        StrEnumRegisterType = regAdd.StrEnumRegisterType,
                        StrEnumRegisterStorageMode = regAdd.StrEnumRegisterStorageMode,
                        StrEnumRegisterStorageSequenceMode = regAdd.StrEnumRegisterStorageSequenceMode,
                        StrEnumRegisterInformation = regAdd.StrEnumRegisterInformation,
                    })
                    .ToList();

                #endregion

                if (MDFPerDau.TemperatureMDFList.Any()
                    || MDFPerDau.WaveMDFList.Any() || MDFPerDau.SpdMDFList.Any())
                {
                    result.DAUMDFList.Add(MDFPerDau);
                }

                response.Result = result;
                response.IsSuccessful = true;

                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "010471";

                return response;
            }
        }

        #endregion

        #region 验证DAUAgent 地址是否可用

        public BaseResponse<ResponseResult> IsDAUAgentAccess(IsDAUAgentAccessParameter param)
        {
            try
            {
                RestClient restClient = new RestClient(param.DAUAgentUrlAddress);

                BaseRequest baseRequest = new BaseRequest();
                string responseStr = restClient.Post(baseRequest.ToClientString(), "CheckAgentService", 5000);

                BaseResponse<ResponseResult> response = Json.JsonDeserialize<BaseResponse<ResponseResult>>(responseStr);

                //如果有返回值且能正常解析，说明服务确实可用，返回 true
                response = new BaseResponse<ResponseResult>();
                return response;
            }
            catch (Exception e)
            {
                //1. 请求超时异常
                //2. 返回URL然后解析异常  则服务不可用
                LogHelper.WriteLog(e);
                BaseResponse<ResponseResult> response = new BaseResponse<ResponseResult>();
                response.IsSuccessful = false;
                response.Code = "000000";
                return response;
            }
        }

        #endregion

        #region DAU数据上传-波形数据上传

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：加速度数据表最后一次保存时间
        /// </summary>
        private static DateTime? AccLastSaveTime = null;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：速度数据表最后一次保存时间
        /// </summary>
        private static DateTime? VelLastSaveTime = null;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：包络数据表最后一次保存时间
        /// </summary>
        private static DateTime? EnvlLastSaveTime = null;

        public void UploadWaveData(UploadWaveDataParameter param)
        {
            int signalType = param.WaveType;
            int msiteID = param.MSiteID;

            //1. 根据DAUID, ChannelID,SignalType找到相应的振动信号
            VibSingal vibSignal = vibSignalRepository
                .GetDatas<VibSingal>(t => t.MSiteID == msiteID && t.SingalType == signalType, true)
                .FirstOrDefault();
            if (null == vibSignal)
            {
                //没有找到振动信号
                return;
            }
            //2. 查询振动信号类型及测点ID
            MeasureSite measureSite = measureSiteRepository.GetByKey(msiteID);
            if (null == measureSite)
            {
                //没有找到测点
                return;
            }
            int deviceID = measureSite.DevID;
            Device device = deviceRepository.GetByKey(deviceID);
            if (null == device)
            {
                //没有找到设备
                return;
            }

            if (device.RunStatus == (int)EnumRunStatus.Stop)
            {
                //设备停机状态，波形数据不进行保存逻辑
                return;
            }

            switch (signalType)
            {
                case (int)EnumVibSignalType.Accelerated:
                    {
                        #region 波形数据删除逻辑

                        bool isDelete = false;
                        if (AccLastSaveTime == null)
                        {
                            isDelete = true;
                            AccLastSaveTime = DateTime.Now;
                        }
                        if ((AccLastSaveTime.Value - DateTime.Now).TotalMinutes > 60)
                            isDelete = true;

                        if (isDelete)
                        {
                            Task.Run(() =>
                            {
                                VibratingSingalCharHisAcc[] accWaveData = null;
                                StringBuilder sqlBuilder = new StringBuilder();
                                sqlBuilder.Append("select * from ( ");
                                sqlBuilder.Append("select *, datediff(day, SamplingDate, GETDATE()) as DiffDay ");
                                sqlBuilder.Append("from T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC ");
                                sqlBuilder.AppendFormat(") as temp where temp.DiffDay > {0} and SamplingDataType = 1", WaveAndEigenValuePeriod);
                                ExecuteDB.ExecuteTrans((context) =>
                                {
                                    accWaveData = context.Database.SqlQuery<VibratingSingalCharHisAcc>(sqlBuilder.ToString()).ToArray();
                                    if (accWaveData != null && accWaveData.Length > 0)
                                        context.VibratingSingalCharHisAcc.Delete(context, accWaveData);
                                });

                                if (accWaveData != null && accWaveData.Length > 0)
                                {
                                    foreach (var item in accWaveData.Where(item => item != null
                                        && !string.IsNullOrWhiteSpace(item.WaveDataPath)))
                                    {
                                        if (string.IsNullOrWhiteSpace(item.WaveDataPath))
                                            continue;

                                        try
                                        {
                                            FileInfo fileInfo = new FileInfo(item.WaveDataPath);
                                            if (fileInfo.Exists)
                                                fileInfo.Delete();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            });
                        }

                        #endregion

                        #region 加速度振动信号数据存储逻辑

                        SaveAndUpdateSignalAccData(param, measureSite, device, (int)EnumVibSignalType.Accelerated);
                        int accTypeID = (int)EnumVibSignalType.Accelerated;
                        VibSingal accVibSignal = vibSignalRepository
                            .GetDatas<VibSingal>(t => t.MSiteID == msiteID
                                && t.SingalType == accTypeID, true)
                            .FirstOrDefault();
                        if (accVibSignal == null)
                        {
                            //没有找到振动信号
                            return;
                        }

                        #endregion
                        break;
                    }

                case (int)EnumVibSignalType.Velocity:
                    {
                        #region 波形数据删除逻辑

                        bool isDelete = false;
                        if (VelLastSaveTime == null)
                        {
                            isDelete = true;
                            VelLastSaveTime = DateTime.Now;
                        }
                        if ((VelLastSaveTime.Value - DateTime.Now).TotalMinutes > 60)
                            isDelete = true;

                        if (isDelete)
                        {
                            Task.Run(() =>
                            {
                                VibratingSingalCharHisVel[] velWaveData = null;
                                StringBuilder sqlBuilder = new StringBuilder();
                                sqlBuilder.Append("select * from ( ");
                                sqlBuilder.Append("select *, datediff(day, SamplingDate, GETDATE()) as DiffDay ");
                                sqlBuilder.Append("from T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL ");
                                sqlBuilder.AppendFormat(") as temp where temp.DiffDay > {0} and SamplingDataType = 1", WaveAndEigenValuePeriod);
                                ExecuteDB.ExecuteTrans((context) =>
                                {
                                    velWaveData = context.Database.SqlQuery<VibratingSingalCharHisVel>(sqlBuilder.ToString()).ToArray();
                                    if (velWaveData != null && velWaveData.Length > 0)
                                        context.VibratingSingalCharHisVel.Delete(context, velWaveData);
                                });

                                if (velWaveData != null && velWaveData.Length > 0)
                                {
                                    foreach (var item in velWaveData.Where(item => item != null
                                        && !string.IsNullOrWhiteSpace(item.WaveDataPath)))
                                    {
                                        if (string.IsNullOrWhiteSpace(item.WaveDataPath))
                                            continue;

                                        try
                                        {
                                            FileInfo fileInfo = new FileInfo(item.WaveDataPath);
                                            if (fileInfo.Exists)
                                                fileInfo.Delete();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            });
                        }

                        #endregion

                        #region 速度振动信号数据存储逻辑

                        SaveAndUpdateSignalVelData(param, measureSite, device, (int)EnumVibSignalType.Velocity);

                        int velTypeID = (int)EnumVibSignalType.Velocity;

                        VibSingal velVibSignal = vibSignalRepository
                            .GetDatas<VibSingal>(t => t.MSiteID == msiteID
                                && t.SingalType == velTypeID, true)
                            .FirstOrDefault();
                        if (velVibSignal == null)
                        {
                            return;
                        }

                        #endregion
                        break;
                    }

                case (int)EnumVibSignalType.Envelope:
                    {
                        #region 波形数据删除逻辑

                        bool isDelete = false;
                        if (EnvlLastSaveTime == null)
                        {
                            isDelete = true;
                            EnvlLastSaveTime = DateTime.Now;
                        }
                        if ((EnvlLastSaveTime.Value - DateTime.Now).TotalMinutes > 60)
                            isDelete = true;

                        if (isDelete)
                        {
                            Task.Run(() =>
                            {
                                VibratingSingalCharHisEnvl[] envlWaveData = null;
                                StringBuilder sqlBuilder = new StringBuilder();
                                sqlBuilder.Append("select * from ( ");
                                sqlBuilder.Append("select *, datediff(day, SamplingDate, GETDATE()) as DiffDay ");
                                sqlBuilder.Append("from T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL ");
                                sqlBuilder.AppendFormat(") as temp where temp.DiffDay > {0} and SamplingDataType = 1", WaveAndEigenValuePeriod);
                                ExecuteDB.ExecuteTrans((context) =>
                                {
                                    envlWaveData = context.Database.SqlQuery<VibratingSingalCharHisEnvl>(sqlBuilder.ToString()).ToArray();
                                    if (envlWaveData != null && envlWaveData.Length > 0)
                                        context.VibratingSingalCharHisEnvl.Delete(context, envlWaveData);
                                });

                                if (envlWaveData != null && envlWaveData.Length > 0)
                                {
                                    foreach (var item in envlWaveData.Where(item => item != null
                                        && !string.IsNullOrWhiteSpace(item.WaveDataPath)))
                                    {
                                        if (string.IsNullOrWhiteSpace(item.WaveDataPath))
                                            continue;

                                        try
                                        {
                                            FileInfo fileInfo = new FileInfo(item.WaveDataPath);
                                            if (fileInfo.Exists)
                                                fileInfo.Delete();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            });
                        }

                        #endregion

                        #region 包络振动信号数据存储逻辑

                        SaveAndUpdateSignalEnvlData(param, measureSite, device, (int)EnumVibSignalType.Envelope);

                        int envlTypeID = (int)EnumVibSignalType.Envelope;
                        VibSingal envlVibSignal = vibSignalRepository
                            .GetDatas<VibSingal>(t => t.MSiteID == msiteID
                                && t.SingalType == envlTypeID, true)
                            .FirstOrDefault();
                        if (envlVibSignal == null)
                        {
                            return;
                        }

                        #endregion
                        break;
                    }
            }

            //更改左侧监测树状态， Added by QXM, 2018/01/29
            UpdateMonitorTreeLeafNodeStatus(deviceID);
            return;
        }

        #endregion

        #region DAU数据上传-转速数据

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：转速数据表1最后一次保存时间
        /// </summary>
        private static DateTime? SpeedLastSaveTime1 = null;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：转速数据表2最后一次保存时间
        /// </summary>
        private static DateTime? SpeedLastSaveTime2 = null;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：转速数据表3最后一次保存时间
        /// </summary>
        private static DateTime? SpeedLastSaveTime3 = null;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：转速数据表4最后一次保存时间
        /// </summary>
        private static DateTime? SpeedLastSaveTime4 = null;

        public void UploadSpeedData(UploadSpeedDataParameter param)
        {
            try
            {
                int msiteID = param.MSiteID;

                var speedSamplingChannelMDF = speedSamplingMDFRepository
                    .GetDatas<SpeedSamplingMDF>(t => t.MSiteID == msiteID, true)
                    .FirstOrDefault();
                if (null == speedSamplingChannelMDF)
                {
                    return;
                }

                MeasureSite measureSite = measureSiteRepository.GetByKey(msiteID);
                if (null == measureSite)
                {
                    return;
                }

                Device device = deviceRepository.GetByKey(measureSite.DevID);
                if (null == device)
                {
                    return;
                }

                //2. 根据MSiteID 决定转速数据存储于哪张转速表
                int leftNumber = msiteID % 4;

                //2.1 读取配置节，判断转速数据是否存储并同时更新相关设备启停机状态

                bool isSaveSpeedData = true;

                int IsSaveDataSpeedLimit = int.Parse(ConfigurationManager.AppSettings["IsSaveDataSpeedLimit"].ToString());
                int SaveDataSpeedLimit = int.Parse(ConfigurationManager.AppSettings["SaveDataSpeedLimit"].ToString());

                if ((IsSaveDataSpeedLimit == 1 && param.SpeedData >= SaveDataSpeedLimit)
                    || (IsSaveDataSpeedLimit == 0 && param.SpeedData > 0))
                {
                    isSaveSpeedData = true;
                }
                else
                {
                    isSaveSpeedData = false;
                }

                if (!isSaveSpeedData)
                {
                    //更改设备状态为停机
                    if (device.RunStatus != (int)EnumRunStatus.Stop)
                    {
                        device.RunStatus = (int)EnumRunStatus.Stop;
                        deviceRepository.Update<Device>(device);
                    }
                }
                else
                {
                    //更改设备为开机
                    if (device.RunStatus != (int)EnumRunStatus.RunNormal)
                    {
                        device.RunStatus = (int)EnumRunStatus.RunNormal;
                        deviceRepository.Update<Device>(device);
                    }

                    //判断是否重复上传
                    bool isRepeat = false;
                    switch (leftNumber)
                    {
                        case 0:
                            {
                                #region 删除规定时间内的转速数据

                                bool isDelete = false;
                                if (SpeedLastSaveTime1 == null)
                                {
                                    isDelete = true;
                                    SpeedLastSaveTime1 = DateTime.Now;
                                }
                                if ((SpeedLastSaveTime1.Value - DateTime.Now).TotalMinutes > 60)
                                    isDelete = true;

                                if (isDelete)
                                {
                                    Task.Run(() =>
                                    {
                                        SpeedDeviceMSitedata_1[] speedData = null;
                                        StringBuilder sqlBuilder = new StringBuilder();
                                        sqlBuilder.Append("select * from ( ");
                                        sqlBuilder.Append("select *, datediff(day,SamplingDate, GETDATE()) as DiffDay ");
                                        sqlBuilder.Append("from T_DATA_SPEED_DEVICE_MSITEDATA_1 ");
                                        sqlBuilder.AppendFormat(") as temp where temp.DiffDay > {0} and SamplingDataType = 1", RotatePeriod);
                                        ExecuteDB.ExecuteTrans((context) =>
                                        {
                                            speedData = context.Database.SqlQuery<SpeedDeviceMSitedata_1>(sqlBuilder.ToString()).ToArray();
                                            if (speedData != null && speedData.Length > 0)
                                                context.SpeedDeviceMSitedata_1.Delete(context, speedData);
                                        });

                                        if (speedData != null && speedData.Length > 0)
                                        {
                                            foreach (var item in speedData.Where(item => item != null && !string.IsNullOrWhiteSpace(item.WaveDataPath)))
                                            {
                                                if (string.IsNullOrWhiteSpace(item.WaveDataPath))
                                                    continue;

                                                try
                                                {
                                                    FileInfo fileInfo = new FileInfo(item.WaveDataPath);
                                                    if (fileInfo.Exists)
                                                        fileInfo.Delete();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                    });
                                }

                                #endregion

                                isRepeat = speedDeviceMSitedata_1Repository
                                    .GetDatas<SpeedDeviceMSitedata_1>(t => t.MsiteDataID == msiteID
                                        && t.SamplingDate == param.SamplingDate, true)
                                    .Any();
                                break;
                            }

                        case 1:
                            {
                                #region 删除规定时间内的转速数据

                                bool isDelete = false;
                                if (SpeedLastSaveTime2 == null)
                                {
                                    isDelete = true;
                                    SpeedLastSaveTime2 = DateTime.Now;
                                }
                                if ((SpeedLastSaveTime2.Value - DateTime.Now).TotalMinutes > 60)
                                    isDelete = true;

                                if (isDelete)
                                {
                                    Task.Run(() =>
                                    {
                                        SpeedDeviceMSitedata_2[] speedData = null;
                                        StringBuilder sqlBuilder = new StringBuilder();
                                        sqlBuilder.Append("select * from ( ");
                                        sqlBuilder.Append("select *, datediff(day,SamplingDate, GETDATE()) as DiffDay ");
                                        sqlBuilder.Append("from T_DATA_SPEED_DEVICE_MSITEDATA_2 ");
                                        sqlBuilder.AppendFormat(") as temp where temp.DiffDay > {0} and SamplingDataType = 1", RotatePeriod);
                                        ExecuteDB.ExecuteTrans((context) =>
                                        {
                                            speedData = context.Database.SqlQuery<SpeedDeviceMSitedata_2>(sqlBuilder.ToString()).ToArray();
                                            if (speedData != null && speedData.Length > 0)
                                                context.SpeedDeviceMSitedata_2.Delete(context, speedData);
                                        });

                                        if (speedData != null && speedData.Length > 0)
                                        {
                                            foreach (var item in speedData.Where(item => item != null && !string.IsNullOrWhiteSpace(item.WaveDataPath)))
                                            {
                                                if (string.IsNullOrWhiteSpace(item.WaveDataPath))
                                                    continue;

                                                try
                                                {
                                                    FileInfo fileInfo = new FileInfo(item.WaveDataPath);
                                                    if (fileInfo.Exists)
                                                        fileInfo.Delete();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                    });
                                }

                                #endregion

                                isRepeat = speedDeviceMSitedata_2Repository
                                    .GetDatas<SpeedDeviceMSitedata_2>(t => t.MsiteDataID == msiteID
                                        && t.SamplingDate == param.SamplingDate, true)
                                    .Any();
                                break;
                            }

                        case 2:
                            {
                                #region 删除规定时间内的转速数据

                                bool isDelete = false;
                                if (SpeedLastSaveTime3 == null)
                                {
                                    isDelete = true;
                                    SpeedLastSaveTime3 = DateTime.Now;
                                }
                                if ((SpeedLastSaveTime3.Value - DateTime.Now).TotalMinutes > 60)
                                    isDelete = true;

                                if (isDelete)
                                {
                                    Task.Run(() =>
                                    {
                                        SpeedDeviceMSitedata_3[] speedData = null;
                                        StringBuilder sqlBuilder = new StringBuilder();
                                        sqlBuilder.Append("select * from ( ");
                                        sqlBuilder.Append("select *, datediff(day,SamplingDate, GETDATE()) as DiffDay ");
                                        sqlBuilder.Append("from T_DATA_SPEED_DEVICE_MSITEDATA_3 ");
                                        sqlBuilder.AppendFormat(") as temp where temp.DiffDay > {0} and SamplingDataType = 1", RotatePeriod);
                                        ExecuteDB.ExecuteTrans((context) =>
                                        {
                                            speedData = context.Database.SqlQuery<SpeedDeviceMSitedata_3>(sqlBuilder.ToString()).ToArray();
                                            if (speedData != null && speedData.Length > 0)
                                                context.SpeedDeviceMSitedata_3.Delete(context, speedData);
                                        });

                                        if (speedData != null && speedData.Length > 0)
                                        {
                                            foreach (var item in speedData.Where(item => item != null && !string.IsNullOrWhiteSpace(item.WaveDataPath)))
                                            {
                                                if (string.IsNullOrWhiteSpace(item.WaveDataPath))
                                                    continue;

                                                try
                                                {
                                                    FileInfo fileInfo = new FileInfo(item.WaveDataPath);
                                                    if (fileInfo.Exists)
                                                        fileInfo.Delete();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                    });
                                }

                                #endregion

                                isRepeat = speedDeviceMSitedata_3Repository
                                    .GetDatas<SpeedDeviceMSitedata_3>(t => t.MsiteDataID == msiteID
                                        && t.SamplingDate == param.SamplingDate, true)
                                    .Any();
                                break;
                            }

                        case 3:
                            {
                                #region 删除规定时间内的转速数据

                                bool isDelete = false;
                                if (SpeedLastSaveTime4 == null)
                                {
                                    isDelete = true;
                                    SpeedLastSaveTime4 = DateTime.Now;
                                }
                                if ((SpeedLastSaveTime4.Value - DateTime.Now).TotalMinutes > 60)
                                    isDelete = true;

                                if (isDelete)
                                {
                                    Task.Run(() =>
                                    {
                                        SpeedDeviceMSitedata_4[] speedData = null;
                                        StringBuilder sqlBuilder = new StringBuilder();
                                        sqlBuilder.Append("select * from ( ");
                                        sqlBuilder.Append("select *, datediff(day,SamplingDate, GETDATE()) as DiffDay ");
                                        sqlBuilder.Append("from T_DATA_SPEED_DEVICE_MSITEDATA_4 ");
                                        sqlBuilder.AppendFormat(") as temp where temp.DiffDay > {0} and SamplingDataType = 1", RotatePeriod);
                                        ExecuteDB.ExecuteTrans((context) =>
                                        {
                                            speedData = context.Database.SqlQuery<SpeedDeviceMSitedata_4>(sqlBuilder.ToString()).ToArray();
                                            if (speedData != null && speedData.Length > 0)
                                                context.SpeedDeviceMSitedata_4.Delete(context, speedData);
                                        });

                                        if (speedData != null && speedData.Length > 0)
                                        {
                                            foreach (var item in speedData.Where(item => item != null && !string.IsNullOrWhiteSpace(item.WaveDataPath)))
                                            {
                                                if (string.IsNullOrWhiteSpace(item.WaveDataPath))
                                                    continue;

                                                try
                                                {
                                                    FileInfo fileInfo = new FileInfo(item.WaveDataPath);
                                                    if (fileInfo.Exists)
                                                        fileInfo.Delete();
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                    });
                                }

                                #endregion

                                isRepeat = speedDeviceMSitedata_4Repository
                                    .GetDatas<SpeedDeviceMSitedata_4>(t => t.MsiteDataID == msiteID
                                        && t.SamplingDate == param.SamplingDate, true)
                                    .Any();
                                break;
                            }
                    }

                    if (isRepeat)
                    {
                        return;
                    }

                    #region 创建波形数据文件并写入转速波形数据

                    RecordSpeedWaveToTxt(msiteID, param);
                    //获取转速波形采集时间路径
                    string speedWaveDataPath = GetSpeedWaveTxtFileName(msiteID, param.SamplingDate);

                    #endregion

                    //保存转速数据
                    switch (leftNumber)
                    {
                        case 0:
                            #region T_DATA_SPEED_DEVICE_MSITEDATA_1 表存储逻辑

                            SpeedDeviceMSitedata_1 speedData1 = new SpeedDeviceMSitedata_1();
                            speedData1.MsiteID = msiteID;
                            speedData1.SamplingDate = param.SamplingDate;
                            speedData1.MsDataValue = param.SpeedData;
                            speedData1.LineCnt = param.LineCnt;
                            speedData1.WaveDataPath = speedWaveDataPath;

                            //写入数据库
                            speedDeviceMSitedata_1Repository.AddNew<SpeedDeviceMSitedata_1>(speedData1);

                            #endregion
                            break;
                        case 1:
                            #region T_DATA_SPEED_DEVICE_MSITEDATA_2 表存储逻辑

                            SpeedDeviceMSitedata_2 speedData2 = new SpeedDeviceMSitedata_2();
                            speedData2.MsiteID = msiteID;
                            speedData2.SamplingDate = param.SamplingDate;
                            speedData2.MsDataValue = param.SpeedData;
                            speedData2.LineCnt = param.LineCnt;
                            speedData2.WaveDataPath = speedWaveDataPath;

                            //写入数据库
                            speedDeviceMSitedata_2Repository.AddNew<SpeedDeviceMSitedata_2>(speedData2);

                            #endregion
                            break;
                        case 2:
                            #region T_DATA_SPEED_DEVICE_MSITEDATA_3 表存储逻辑

                            SpeedDeviceMSitedata_3 speedData3 = new SpeedDeviceMSitedata_3();
                            speedData3.MsiteID = msiteID;
                            speedData3.SamplingDate = param.SamplingDate;
                            speedData3.MsDataValue = param.SpeedData;
                            speedData3.LineCnt = param.LineCnt;
                            speedData3.WaveDataPath = speedWaveDataPath;

                            //写入数据库
                            speedDeviceMSitedata_3Repository.AddNew<SpeedDeviceMSitedata_3>(speedData3);

                            #endregion
                            break;
                        case 3:
                            #region T_DATA_SPEED_DEVICE_MSITEDATA_4 表存储逻辑

                            SpeedDeviceMSitedata_4 speedData4 = new SpeedDeviceMSitedata_4();
                            speedData4.MsiteID = msiteID;
                            speedData4.SamplingDate = param.SamplingDate;
                            speedData4.MsDataValue = param.SpeedData;
                            speedData4.LineCnt = param.LineCnt;
                            speedData4.WaveDataPath = speedWaveDataPath;

                            //写入数据库
                            speedDeviceMSitedata_4Repository.AddNew<SpeedDeviceMSitedata_4>(speedData4);

                            #endregion
                            break;
                    }

                    #region 上传转速数据时候 修改形貌图转速数据

                    var realTimeCollectionInfo = realTimeCollectInfoRepository
                        .GetDatas<RealTimeCollectInfo>(t => t.MSID.HasValue && t.MSID.Value == msiteID, true)
                        .FirstOrDefault();

                    if (null != realTimeCollectionInfo)
                    {
                        realTimeCollectionInfo.MSRealTimeSpeedTime = param.SamplingDate;
                        realTimeCollectionInfo.MSRealTimeSpeedValue = param.SpeedData;
                        realTimeCollectInfoRepository.Update<RealTimeCollectInfo>(realTimeCollectionInfo);
                    }
                    else
                    {
                        realTimeCollectionInfo = new RealTimeCollectInfo();
                        realTimeCollectionInfo.DevID = device.DevID;
                        realTimeCollectionInfo.MSID = msiteID;

                        var msType = cacheDICT.GetCacheType<MeasureSiteType>().Where(t => t.ID == measureSite.MSiteName).First();
                        realTimeCollectionInfo.MSName = msType.Name;
                        realTimeCollectionInfo.MSRealTimeSpeedTime = param.SamplingDate;
                        realTimeCollectionInfo.MSRealTimeSpeedValue = param.SpeedData;

                        realTimeCollectInfoRepository.AddNew<RealTimeCollectInfo>(realTimeCollectionInfo);
                    }

                    #endregion
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                return;
            }
        }
        #endregion

        #region DAU数据上传-温度数据上传

        public void UploadTemperatureData(UploadTemperatureDataParameter param)
        {
            try
            {
                ///TODO:
                //1. 根据采集单元ID找到温度阈值设置
                var temperatureMDFList = tempeDeviceSetMSiteAlmRepository
                    .GetDatas<TempeDeviceSetMSiteAlm>(t => t.WGID == param.DAUID, true)
                    .ToList();
                //查询测量位置信息
                var mSiteIDList = temperatureMDFList.Select(mSiteInfo => mSiteInfo.MsiteID).ToList();
                var mSiteInfoList = measureSiteRepository
                    .GetDatas<MeasureSite>(mSiteInfo => mSiteIDList.Contains(mSiteInfo.MSiteID), false)
                    .ToList();
                //查询设备信息
                var devIDList = mSiteInfoList.Select(mSiteInfo => mSiteInfo.DevID).ToList();
                var devInfoList = deviceRepository
                    .GetDatas<Device>(devInfo => devIDList.Contains(devInfo.DevID), false)
                    .ToList();
                //根据测量定义ID找到寄存器地址数据
                int[] regAddressIDArray = temperatureMDFList.Select(t => t.MsiteAlmID).ToArray();
                var modbusRegisterAddressList = modBusRegisterAddressRepository
                    .GetDatas<ModBusRegisterAddress>(t => regAddressIDArray.Contains(t.MDFID)
                        && t.MDFResourceTable == "T_SYS_TEMPE_DEVICE_SET_MSITEALM", true)
                    .ToList();
                //2. 根据TemperatureChannelID(寄存器地址ID=>Modbus注册地址ID=>测量定义ID=>测点ID)
                param.TemperatureDataInfoList.ForEach(tDic =>
                {
                    LogHelper.WriteLog("采集单元【" + param.DAUID + "】温度通道【"
                        + tDic.TemperatureChannelID + "】温度值【" + tDic.TemperatureData + "】");

                    var registerAddress = modbusRegisterAddressList
                        .Where(t => t.RegisterAddress == tDic.TemperatureChannelID)
                        .FirstOrDefault();
                    if (registerAddress != null)
                    {
                        var temperatureMDF = temperatureMDFList.Where(t => t.MsiteAlmID == registerAddress.MDFID).FirstOrDefault();
                        if (temperatureMDF != null)
                        {
                            // 找到温度数据对应的测点ID
                            MeasureSite msite = mSiteInfoList.SingleOrDefault(mSiteInfo => mSiteInfo.MSiteID == temperatureMDF.MsiteID);
                            if (msite != null)
                            {
                                var device = devInfoList.SingleOrDefault(devInfo => devInfo.DevID == msite.DevID);
                                if (device != null)
                                {
                                    DAUTemperatureParameter temperatureParam = new DAUTemperatureParameter();
                                    temperatureParam.SamplingDate = param.SamplingDate;
                                    temperatureParam.TemperatureData = tDic.TemperatureData;

                                    SaveDeviceTemperatureData(temperatureParam, device, msite, null, 0);

                                    //更改左侧监测树状态， Added by QXM, 2018/01/29
                                    UpdateMonitorTreeLeafNodeStatus(device.DevID);
                                }
                            }
                        }
                        else
                        {
                            LogHelper.WriteLog("没有找到采集单元【" + param.DAUID + "】温度通道【" + tDic.TemperatureChannelID + "】阈值配置信息");
                        }
                    }
                    else
                    {
                        LogHelper.WriteLog("没有找到采集单元【" + param.DAUID + "】温度通道【" + tDic.TemperatureChannelID + "】的信息");
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        #region 保存设备温度数据

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：温度数据表1最后一次保存时间
        /// </summary>
        private static DateTime? TemperatureLastSaveTime1 = null;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：温度数据表2最后一次保存时间
        /// </summary>
        private static DateTime? TemperatureLastSaveTime2 = null;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：温度数据表3最后一次保存时间
        /// </summary>
        private static DateTime? TemperatureLastSaveTime3 = null;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-02-03
        /// 创建记录：温度数据表4最后一次保存时间
        /// </summary>
        private static DateTime? TemperatureLastSaveTime4 = null;

        /// <summary>
        /// 保存设备温度数据
        /// </summary>
        /// <param name="param">采集数据对象</param>
        /// <param name="dev">设备信息</param>
        /// <param name="ws">传感器信息</param>
        /// <param name="site">测量位置信息</param>
        /// <param name="monitorName">测量位置监测类型名称</param>
        private void SaveDeviceTemperatureData(DAUTemperatureParameter param, Device dev, MeasureSite site, string monitorName, int oldDevStatus)
        {
            if (dev.RunStatus == (int)EnumRunStatus.Stop)
                return;

            int status = (int)EnumAlarmStatus.Normal;
            try
            {
                //获取设备温度警阈值
                TempeDeviceSetMSiteAlm tempDevMSiteAlm = tempeDeviceSetMSiteAlmRepository
                    .GetDatas<TempeDeviceSetMSiteAlm>(p => p.MsiteID == site.MSiteID, false).FirstOrDefault();
                if (tempDevMSiteAlm == null)
                    return;
                GetSamplingDataStatus(tempDevMSiteAlm.WarnValue, tempDevMSiteAlm.AlmValue, param.TemperatureData, ref status);

                switch (site.MSiteID % 4)
                {
                    case 0:
                        {
                            #region 删除规定时间内的温度数据

                            bool isDelete = false;
                            if (TemperatureLastSaveTime1 == null)
                            {
                                isDelete = true;
                                TemperatureLastSaveTime1 = DateTime.Now;
                            }
                            if ((TemperatureLastSaveTime1.Value - DateTime.Now).TotalMinutes > 60)
                                isDelete = true;

                            if (isDelete)
                            {
                                Task.Run(() =>
                                {
                                    StringBuilder sqlBuilder = new StringBuilder();
                                    sqlBuilder.Append("delete from T_DATA_TEMPE_DEVICE_MSITEDATA_1 ");
                                    sqlBuilder.AppendFormat("where datediff(day,SamplingDate, GETDATE()) > {0} and SamplingDataType = 1", DeviceTempePeriod);
                                    ExecuteDB.ExecuteTrans((context) =>
                                    {
                                        context.Database.ExecuteSqlCommand(sqlBuilder.ToString());
                                    });
                                });
                            }

                            #endregion

                            //判断表1是否有重复数据
                            List<TempeDeviceMsitedata_1> hisDatas1 = tempeDeviceMsitedata_1Repository
                                .GetDatas<TempeDeviceMsitedata_1>(p => p.MsiteID == site.MSiteID
                                    && p.SamplingDate == param.SamplingDate
                                    && p.MonthDate == param.SamplingDate.Month, false)
                                .ToList();
                            if (hisDatas1 == null || hisDatas1.Count <= 0)
                            {
                                TempeDeviceMsitedata_1 tempDevMSiteData_1 = new TempeDeviceMsitedata_1()
                                {
                                    MsiteID = site.MSiteID,
                                    MsDataValue = param.TemperatureData,
                                    WarnValue = tempDevMSiteAlm.WarnValue,
                                    AlmValue = tempDevMSiteAlm.AlmValue,
                                    SamplingDate = param.SamplingDate,
                                    Status = status,
                                    MonthDate = param.SamplingDate.Month
                                };
                                tempeDeviceMsitedata_1Repository.AddNew(tempDevMSiteData_1);
                            }
                            break;
                        }

                    case 1:
                        {
                            #region 删除规定时间内的温度数据

                            bool isDelete = false;
                            if (TemperatureLastSaveTime2 == null)
                            {
                                isDelete = true;
                                TemperatureLastSaveTime2 = DateTime.Now;
                            }
                            if ((TemperatureLastSaveTime2.Value - DateTime.Now).TotalMinutes > 60)
                                isDelete = true;

                            if (isDelete)
                            {
                                Task.Run(() =>
                                {
                                    StringBuilder sqlBuilder = new StringBuilder();
                                    sqlBuilder.Append("delete from T_DATA_TEMPE_DEVICE_MSITEDATA_2 ");
                                    sqlBuilder.AppendFormat("where datediff(day,SamplingDate, GETDATE()) > {0} and SamplingDataType = 1", DeviceTempePeriod);
                                    ExecuteDB.ExecuteTrans((context) =>
                                    {
                                        context.Database.ExecuteSqlCommand(sqlBuilder.ToString());
                                    });
                                });
                            }

                            #endregion

                            //判断表2是否有重复数据
                            List<TempeDeviceMsitedata_2> hisDatas2 = tempeDeviceMsitedata_2Repository
                                .GetDatas<TempeDeviceMsitedata_2>(p => p.MsiteID == site.MSiteID
                                    && p.SamplingDate == param.SamplingDate
                                    && p.MonthDate == param.SamplingDate.Month, false)
                                .ToList();
                            if (hisDatas2 == null || hisDatas2.Count <= 0)
                            {
                                TempeDeviceMsitedata_2 tempDevMSiteData_2 = new TempeDeviceMsitedata_2()
                                {
                                    MsiteID = site.MSiteID,
                                    MsDataValue = param.TemperatureData,
                                    WarnValue = tempDevMSiteAlm.WarnValue,
                                    AlmValue = tempDevMSiteAlm.AlmValue,
                                    SamplingDate = param.SamplingDate,
                                    Status = status,
                                    MonthDate = param.SamplingDate.Month
                                };
                                tempeDeviceMsitedata_2Repository.AddNew(tempDevMSiteData_2);
                            }
                            break;
                        }

                    case 2:
                        {
                            #region 删除规定时间内的温度数据

                            bool isDelete = false;
                            if (TemperatureLastSaveTime3 == null)
                            {
                                isDelete = true;
                                TemperatureLastSaveTime3 = DateTime.Now;
                            }
                            if ((TemperatureLastSaveTime3.Value - DateTime.Now).TotalMinutes > 60)
                                isDelete = true;

                            if (isDelete)
                            {
                                Task.Run(() =>
                                {
                                    StringBuilder sqlBuilder = new StringBuilder();
                                    sqlBuilder.Append("delete from T_DATA_TEMPE_DEVICE_MSITEDATA_3 ");
                                    sqlBuilder.AppendFormat("where datediff(day,SamplingDate, GETDATE()) > {0} and SamplingDataType = 1", DeviceTempePeriod);
                                    ExecuteDB.ExecuteTrans((context) =>
                                    {
                                        context.Database.ExecuteSqlCommand(sqlBuilder.ToString());
                                    });
                                });
                            }

                            #endregion

                            //判断表3是否有重复数据
                            List<TempeDeviceMsitedata_3> hisDatas3 = tempeDeviceMsitedata_3Repository
                                .GetDatas<TempeDeviceMsitedata_3>(p => p.MsiteID == site.MSiteID
                                    && p.SamplingDate == param.SamplingDate
                                    && p.MonthDate == param.SamplingDate.Month, false)
                                .ToList();
                            if (hisDatas3 == null || hisDatas3.Count <= 0)
                            {
                                TempeDeviceMsitedata_3 tempDevMSiteData_3 = new TempeDeviceMsitedata_3()
                                {
                                    MsiteID = site.MSiteID,
                                    MsDataValue = param.TemperatureData,
                                    WarnValue = tempDevMSiteAlm.WarnValue,
                                    AlmValue = tempDevMSiteAlm.AlmValue,
                                    SamplingDate = param.SamplingDate,
                                    Status = status,
                                    MonthDate = param.SamplingDate.Month
                                };
                                tempeDeviceMsitedata_3Repository.AddNew(tempDevMSiteData_3);
                            }
                            break;
                        }

                    case 3:
                        {
                            #region 删除规定时间内的温度数据

                            bool isDelete = false;
                            if (TemperatureLastSaveTime4 == null)
                            {
                                isDelete = true;
                                TemperatureLastSaveTime4 = DateTime.Now;
                            }
                            if ((TemperatureLastSaveTime4.Value - DateTime.Now).TotalMinutes > 60)
                                isDelete = true;

                            if (isDelete)
                            {
                                Task.Run(() =>
                                {
                                    StringBuilder sqlBuilder = new StringBuilder();
                                    sqlBuilder.Append("delete from T_DATA_TEMPE_DEVICE_MSITEDATA_4 ");
                                    sqlBuilder.AppendFormat("where datediff(day,SamplingDate, GETDATE()) > {0} and SamplingDataType = 1", DeviceTempePeriod);
                                    ExecuteDB.ExecuteTrans((context) =>
                                    {
                                        context.Database.ExecuteSqlCommand(sqlBuilder.ToString());
                                    });
                                });
                            }

                            #endregion

                            //判断表4是否有重复数据
                            List<TempeDeviceMsitedata_4> hisDatas4 = tempeDeviceMsitedata_4Repository
                                 .GetDatas<TempeDeviceMsitedata_4>(p => p.MsiteID == site.MSiteID
                                     && p.SamplingDate == param.SamplingDate
                                     && p.MonthDate == param.SamplingDate.Month, false)
                                 .ToList();
                            if (hisDatas4 == null || hisDatas4.Count <= 0)
                            {
                                TempeDeviceMsitedata_4 tempDevMSiteData_4 = new TempeDeviceMsitedata_4()
                                {
                                    MsiteID = site.MSiteID,
                                    MsDataValue = param.TemperatureData,
                                    WarnValue = tempDevMSiteAlm.WarnValue,
                                    AlmValue = tempDevMSiteAlm.AlmValue,
                                    SamplingDate = param.SamplingDate,
                                    Status = status,
                                    MonthDate = param.SamplingDate.Month
                                };
                                tempeDeviceMsitedata_4Repository.AddNew(tempDevMSiteData_4);
                            }
                            break;
                        }

                    default:
                        break;
                }

                tempDevMSiteAlm.Status = status;
                tempeDeviceSetMSiteAlmRepository.Update(tempDevMSiteAlm);

                #region ADDED BY QXM 2017/01/19
                ModifyStatusBeyondMSite(tempDevMSiteAlm.MsiteID);
                #endregion

                SaveDeviceTemperatureAlmRecord(param, dev, site, tempDevMSiteAlm);
                SaveRealTimeCollectInfoDevTemp(param, site, status);

                #region QXM 屏蔽 2017/01/19

                #endregion
                #region QXM ADDED 更新设备状态更新时间
                //更新设备状态更新时间
                if (dev != null)
                {
                    dev = deviceRepository.GetByKey(dev.DevID);
                    if (dev != null && oldDevStatus != dev.AlmStatus)
                    {
                        dev.DevSDate = param.SamplingDate;
                        deviceRepository.Update(dev);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        #endregion

        #region 保存设备温度报警记录

        /// <summary>
        /// 保存设备温度报警记录
        /// </summary>
        /// <param name="param">采集数据对象</param>
        /// <param name="dev">设备信息对象</param>
        /// <param name="site">测量位置对象</param>
        /// <param name="wg">WG信息对象</param>
        /// <param name="ws">WS信息对象</param>
        /// <param name="volMSiteAlm">WS电池电压告警设置</param>
        private void SaveDeviceTemperatureAlmRecord(DAUTemperatureParameter param, Device dev, MeasureSite site,
            TempeDeviceSetMSiteAlm tempDevMSiteAlm)
        {
            DeviceTemperatureAlarmParameter devTempAlarmParam = new DeviceTemperatureAlarmParameter()
            {
                Dev = dev,
                MSite = site,
                MSiteAlmSet = tempDevMSiteAlm,
                HisDataValue = param.TemperatureData,
                SamplingTime = param.SamplingDate
            };
            deviceTemperatureAlarm.DevTemperatureAlmRecordManager(devTempAlarmParam);
        }

        #endregion

        #region 保存设备温度实时数据汇总

        /// <summary>
        /// 保存WS电池电压实时数据汇总
        /// </summary>
        /// <param name="param">上传数据对象</param>
        /// <param name="dev">设备信息</param>
        /// <param name="ws">WS信息</param>
        /// <param name="site">测量位置信息</param>
        /// <param name="vibSingal">振动信号信息</param>
        /// <param name="status">测量位置状态</param>
        private void SaveRealTimeCollectInfoDevTemp(DAUTemperatureParameter param, MeasureSite site, int devTempStatus)
        {
            try
            {
                //重新获取测点信息
                MeasureSite newMeasureSite = measureSiteRepository.GetByKey(site.MSiteID);

                //获取实时数据信息
                RealTimeCollectInfo realTimeCollectInfo = realTimeCollectInfoRepository
                    .GetDatas<RealTimeCollectInfo>(p => p.MSID == newMeasureSite.MSiteID, false).FirstOrDefault();
                if (realTimeCollectInfo == null)
                {
                    realTimeCollectInfo = new RealTimeCollectInfo();
                    MeasureSiteType siteType = cacheDICT.GetInstance()
                        .GetCacheType<MeasureSiteType>(p => p.ID == newMeasureSite.MSiteName)
                        .FirstOrDefault();
                    realTimeCollectInfo.DevID = newMeasureSite.DevID;
                    realTimeCollectInfo.MSID = newMeasureSite.MSiteID;
                    realTimeCollectInfo.MSName = siteType.Name;
                    realTimeCollectInfo.MSStatus = newMeasureSite.MSiteStatus;
                    realTimeCollectInfo.MSDesInfo = siteType.Describe;
                    realTimeCollectInfo.MSDataStatus = newMeasureSite.MSiteStatus;
                    realTimeCollectInfo.MSDevTemperatureStatus = devTempStatus;
                    realTimeCollectInfo.MSDevTemperatureValue = param.TemperatureData;
                    realTimeCollectInfo.MSDevTemperatureTime = param.SamplingDate;

                    realTimeCollectInfoRepository.AddNew(realTimeCollectInfo);

                    #region 添加报警阈值在时实表中

                    //获取阈值
                    var alarmThreshold = tempeDeviceSetMSiteAlmRepository
                        .GetDatas<TempeDeviceSetMSiteAlm>(item => item.MsiteID == newMeasureSite.MSiteID, true)
                        .FirstOrDefault();
                    if (alarmThreshold != null)
                    {
                        //添加关联阈值表数据
                        RealTimeAlarmThreshold realTimeAlarmThreshold = new RealTimeAlarmThreshold();
                        realTimeAlarmThreshold.MeasureSiteID = newMeasureSite.MSiteID;
                        realTimeAlarmThreshold.MeasureSiteThresholdType = (int)EnumMeasureSiteThresholdType.DeviceTemperature;
                        realTimeAlarmThreshold.AddDate = DateTime.Now;
                        realTimeAlarmThreshold.AlarmThresholdValue = alarmThreshold.WarnValue;
                        realTimeAlarmThreshold.DangerThresholdValue = alarmThreshold.AlmValue;
                        realTimeAlarmThresholdRepository.AddNew(realTimeAlarmThreshold);
                    }

                    #endregion
                }
                else
                {
                    realTimeCollectInfo.MSStatus = newMeasureSite.MSiteStatus;
                    realTimeCollectInfo.MSDataStatus = newMeasureSite.MSiteStatus;
                    realTimeCollectInfo.MSDevTemperatureStatus = devTempStatus;
                    realTimeCollectInfo.MSDevTemperatureValue = param.TemperatureData;
                    realTimeCollectInfo.MSDevTemperatureTime = param.SamplingDate;
                    realTimeCollectInfoRepository.Update(realTimeCollectInfo);

                    #region 添加报警阈值在时实表中
                    //获取阈值
                    var alarmThreshold = tempeDeviceSetMSiteAlmRepository
                        .GetDatas<TempeDeviceSetMSiteAlm>(item => item.MsiteID == newMeasureSite.MSiteID, true)
                        .FirstOrDefault();
                    if (alarmThreshold != null)
                    {
                        var realTimeAlarmThreshold = realTimeAlarmThresholdRepository
                            .GetDatas<RealTimeAlarmThreshold>(item => item.MeasureSiteID == newMeasureSite.MSiteID
                                && item.MeasureSiteThresholdType == (int)EnumMeasureSiteThresholdType.DeviceTemperature, true)
                            .FirstOrDefault();

                        //更新
                        if (realTimeAlarmThreshold != null)
                        {
                            //添加关联阈值表数据
                            realTimeAlarmThreshold.AlarmThresholdValue = alarmThreshold.WarnValue;
                            realTimeAlarmThreshold.DangerThresholdValue = alarmThreshold.AlmValue;
                            realTimeAlarmThresholdRepository.Update(realTimeAlarmThreshold);
                        }
                        else
                        {
                            //添加关联阈值表数据
                            realTimeAlarmThreshold = new RealTimeAlarmThreshold();
                            realTimeAlarmThreshold.MeasureSiteID = newMeasureSite.MSiteID;
                            realTimeAlarmThreshold.MeasureSiteThresholdType = (int)EnumMeasureSiteThresholdType.DeviceTemperature;
                            realTimeAlarmThreshold.AddDate = DateTime.Now;
                            realTimeAlarmThreshold.AlarmThresholdValue = alarmThreshold.WarnValue;
                            realTimeAlarmThreshold.DangerThresholdValue = alarmThreshold.AlmValue;
                            realTimeAlarmThreshold.SamplingDate = param.SamplingDate;
                            realTimeAlarmThresholdRepository.AddNew(realTimeAlarmThreshold);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        #endregion

        #endregion

        #region DAU数据上传-DAU属性数据上传

        public void UploadDAUInfo(UploadDAUInfoParameter param)
        {
            try
            {
                int dauID = param.DAUID;
                Gateway dau = wgRepository.GetByKey(dauID);
                if (null == dau)
                {
                    return;
                }

                dau.WGIP = param.IPAddress;
                dau.WGPort = param.Port;
                dau.GateWayMAC = param.MACAddress;
                dau.SubNetMask = param.SubNetMask;
                dau.GateWay = param.Gateway;
                dau.SerizeCode = param.SerizeCode;
                dau.MainBoardSerizeCode = param.MainBoardSerizeCode;
                dau.BESPSerizeCode = param.BESPSerizeCode;
                dau.ProductInfoSerizeCode = param.ProductInfoSerizeCode;
                dau.PowerSupplySerizeCode = param.PowerSupplySerizeCode;
                dau.CoreBoardSerizeCode = param.CoreBoardSerizeCode;
                dau.CurrentDAUStates = param.CurrentDAUStates;
                dau.MinibootVersion = param.MinibootVersion;
                dau.SndbootVersion = param.SndbootVersion;
                dau.FirmwareVersion = param.FirmwareVersion;
                dau.FPGAVersion = param.FPGAVersion;

                OperationResult operResult = wgRepository.Update<Gateway>(dau);

                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("上传DAU属性信息发生异常");
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                return;
            }
        }

        #endregion

        #region DAU数据上传-DAU当前状态上传

        public void UploadCurrentDAUStates(UploadCurrentDAUStatesParameter param)
        {
            try
            {
                int dauID = param.DAUID;
                Gateway dau = wgRepository.GetByKey(dauID);
                if (null == dau)
                {
                    return;
                }
                dau.CurrentDAUStates = param.CurrentDAUStates;
                OperationResult operResult = wgRepository.Update<Gateway>(dau);
                if (operResult.ResultType != EnumOperationResultType.Success)
                {
                    throw new Exception("上传DAU当前状态发生异常");
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        #endregion

        #region 取得由速度波形计算的转频

        /// <summary>
        /// 取得由速度波形计算的转频
        /// </summary>
        /// <param name="msiteIDList"></param>
        /// <returns></returns>
        private string GetRotationFrequency(MeasureSite site, DateTime samplingDate, int DAQStyle)
        {
            string rotationFreq = ConstObject.RotationFrequency_Default;
            try
            {
                int velID = 0;
                var vibratingSingalTypeInfo = cacheDICT.GetInstance()
                          .GetCacheType<VibratingSingalType>(p => p.ID == (int)EnumVibSignalType.Velocity)
                          .FirstOrDefault();
                LogHelper.WriteLog("速度波形计算的转频：从缓存中获取振动信号类型完成");
                //修改做空判断  2016-08-30
                if (vibratingSingalTypeInfo != null)
                {
                    velID = vibratingSingalTypeInfo.ID;
                }

                //获取信号类型SingalID
                var signal = vibSignalRepository
                    .GetDatas<VibSingal>(Vel => Vel.MSiteID == site.MSiteID
                        && Vel.SingalType == velID && Vel.DAQStyle == DAQStyle, false)
                    .FirstOrDefault();
                LogHelper.WriteLog("速度波形计算的转频：从缓存中获取振动信号类型完成");

                if (signal == null)
                    return rotationFreq;
                else
                {
                    LogHelper.WriteLog("速度波形计算的转频：开始获取速度波形的数据");
                    int signalID = signal.SingalID;
                    int samplingInterval = 0 - (Convert.ToInt32(site.WaveTime.Split('#')[0]) * 60 + Convert.ToInt32(site.WaveTime.Split('#')[1]));
                    var searchDate = samplingDate.AddMinutes(samplingInterval);
                    VibratingSingalCharHisVel VelvelHisData = vibSingalRTRepository
                        .GetDatas<VibratingSingalCharHisVel>(obj => obj.SingalID == signalID && obj.SamplingDate >= searchDate, false)
                        .OrderByDescending(obj => obj.AddDate)
                        .ToList()
                        .FirstOrDefault();
                    if (VelvelHisData != null)
                        rotationFreq = VelvelHisData.Rotate;
                    LogHelper.WriteLog("速度波形计算的转频：开始获取速度波形的数据完成");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return rotationFreq;
        }

        #endregion

        #region 波形数据保存至文件中

        /// <summary>
        /// 波形数据保存至文件中
        /// </summary>
        /// <param name="msiteIDList"></param>
        /// <param name="waveData"></param>
        /// <param name="waveInfo"></param>
        private void RecordWaveToTxt(int signalID, UploadWaveDataParameter param)
        {
            string dirWave = ConfigurationManager.AppSettings["WaveDirectory"];
            if (Directory.Exists(dirWave) == false)
            {
                //文件夹不存在创建
                Directory.CreateDirectory(dirWave);
            }

            string dirYear = dirWave + @"\" + param.SamplingDate.Year;
            if (Directory.Exists(dirYear) == false)
            {
                //年文件夹不存在创建
                Directory.CreateDirectory(dirYear);
            }

            string dirMonth = dirYear + @"\" + param.SamplingDate.Month.ToString("00");
            if (Directory.Exists(dirMonth) == false)
            {
                //年文件夹不存在创建
                Directory.CreateDirectory(dirMonth);
            }

            string dirDay = dirMonth + @"\" + param.SamplingDate.Day.ToString("00");
            if (Directory.Exists(dirDay) == false)
            {
                //年文件夹不存在创建
                Directory.CreateDirectory(dirDay);
            }

            string fileName = GetWaveTxtFileName(signalID, param.SamplingDate);

            if (File.Exists(fileName) == true)
            {
                //文件存在删除
                File.Delete(fileName);
            }

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, true);
                StringBuilder sb = new StringBuilder();
                string chara = ",";

                for (int i = 0; i < param.WaveData.Length; i++)
                {
                    sb.Append(param.WaveData[i].ToString("0.0000") + chara);
                }
                sw.Write(sb.ToString().Substring(0, sb.ToString().LastIndexOf(chara)));
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        #endregion

        #region 转速波形数据保存至文件中

        /// <summary>
        /// 波形数据保存至文件中
        /// </summary>
        /// <param name="msiteIDList"></param>
        /// <param name="waveData"></param>
        /// <param name="waveInfo"></param>
        private void RecordSpeedWaveToTxt(int msiteID, UploadSpeedDataParameter param)
        {
            string dirWave = ConfigurationManager.AppSettings["SpeedWaveDirectory"];
            if (Directory.Exists(dirWave) == false)
            {
                //文件夹不存在创建
                Directory.CreateDirectory(dirWave);
            }

            string dirYear = dirWave + @"\" + param.SamplingDate.Year;
            if (Directory.Exists(dirYear) == false)
            {
                //年文件夹不存在创建
                Directory.CreateDirectory(dirYear);
            }

            string dirMonth = dirYear + @"\" + param.SamplingDate.Month.ToString("00");
            if (Directory.Exists(dirMonth) == false)
            {
                //年文件夹不存在创建
                Directory.CreateDirectory(dirMonth);
            }

            string dirDay = dirMonth + @"\" + param.SamplingDate.Day.ToString("00");
            if (Directory.Exists(dirDay) == false)
            {
                //年文件夹不存在创建
                Directory.CreateDirectory(dirDay);
            }

            string fileName = GetWaveTxtFileName(msiteID, param.SamplingDate);

            if (File.Exists(fileName) == true)
            {
                //文件存在删除
                File.Delete(fileName);
            }

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, true);
                StringBuilder sb = new StringBuilder();
                string chara = ",";

                for (int i = 0; i < param.SpeedWaveData.Length; i++)
                {
                    sb.Append(param.SpeedWaveData[i].ToString("0.0000") + chara);
                }
                sw.Write(sb.ToString().Substring(0, sb.ToString().LastIndexOf(chara)));
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        #endregion

        #region 取得保存波形文件名称

        /// <summary>
        /// 取得保存波形文件名称
        /// </summary>
        /// <param name="msiteIDList">振动信号ID</param>
        /// <param name="samplingData">采集时间</param>
        /// <returns></returns>
        private string GetWaveTxtFileName(int signalid, DateTime samplingTime)
        {
            return ConfigurationManager.AppSettings["WaveDirectory"] +
                    @"\" + samplingTime.Year + @"\" + samplingTime.Month.ToString("00") +
                    @"\" + samplingTime.Day.ToString("00") +
                    @"\" + signalid.ToString() + "_" + samplingTime.Year + samplingTime.Month.ToString("00") +
                    samplingTime.Day.ToString("00") + samplingTime.Hour.ToString("00") + samplingTime.Minute.ToString("00") +
                    samplingTime.Second.ToString("00") + ".txt";
        }

        #endregion

        #region 取得转速保存波形文件名称

        /// <summary>
        /// 取得转速保存波形文件名称
        /// </summary>
        /// <param name="msiteIDList">振动信号ID</param>
        /// <param name="samplingData">采集时间</param>
        /// <returns></returns>
        private string GetSpeedWaveTxtFileName(int msiteID, DateTime samplingData)
        {
            return ConfigurationManager.AppSettings["SpeedWaveDirectory"] +
                    @"\" + samplingData.Year + @"\" + samplingData.Month.ToString("00") +
                    @"\" + samplingData.Day.ToString("00") +
                    @"\" + msiteID.ToString() + "_" + samplingData.Year + samplingData.Month.ToString("00") +
                    samplingData.Day.ToString("00") + samplingData.Hour.ToString("00") + samplingData.Minute.ToString("00") +
                    samplingData.Second.ToString("00") + ".txt";
        }

        #endregion

        public BaseResponse<GetDAUInfoListByDAUIDResult> GetDAUInfoListByDAUID(GetDAUInfoListByDAUIDParameter param)
        {
            BaseResponse<GetDAUInfoListByDAUIDResult> response = new BaseResponse<GetDAUInfoListByDAUIDResult>();
            GetDAUInfoListByDAUIDResult result = new GetDAUInfoListByDAUIDResult();
            iCMSDbContext dbContext = new iCMSDbContext();

            try
            {
                //找到UserID关联的WS所对应的WG
                List<int> linq =
                    (from wsr in dbContext.UserRelationWS
                     where wsr.UserID == param.UserID
                     join ws in dbContext.WS on wsr.WSID equals ws.WSID into group1
                     from g1 in group1
                     join wg in dbContext.WG on g1.WGID equals wg.WGID into group2

                     from g2 in group2
                     where g2.DevFormType == 3
                     select g2.WGID)
                     .Distinct()
                     .ToList();

                IQueryable<Gateway> wgQuerable = dbContext.WG.Where(t => linq.Contains(t.WGID));
                if (param.DAUID.HasValue && param.DAUID.Value > 0)
                {
                    wgQuerable = wgQuerable.Where(t => t.WGID == param.DAUID.Value);
                }

                ListSortDirection sortOrder = param.Order.ToLower().Equals("asc")
                    ? ListSortDirection.Ascending
                    : ListSortDirection.Descending;
                PropertySortCondition[] sortList = new PropertySortCondition[]
                    {
                        new PropertySortCondition(param.Sort, sortOrder),
                        new PropertySortCondition("WGID", sortOrder),
                    };

                wgQuerable = wgQuerable.OrderBy(sortList);

                int total = wgQuerable.Count();
                if (param.Page > -1)
                {
                    wgQuerable = wgQuerable
                        .Skip((param.Page - 1) * param.PageSize)
                        .Take(param.PageSize);
                }

                List<DAUInfo> dauInfoList = new List<DAUInfo>();
                var monitortTreeList = dbContext.MonitorTree.ToList();

                dauInfoList = wgQuerable.ToArray().Select((wg) =>
                {
                    string monitorTreeNames = string.Empty;

                    var monitorTree = monitortTreeList
                        .Where(t => t.MonitorTreeID == wg.MonitorTreeID)
                        .FirstOrDefault();
                    if (monitorTree != null)
                    {
                        monitorTreeNames = monitorTree.Name;
                    }
                    return new DAUInfo
                    {
                        DAUID = wg.WGID,
                        DAUName = wg.WGName,
                        CurrentDAUStates = wg.CurrentDAUStates ?? 0,
                        MonitorTreeNames = monitorTreeNames
                    };
                }).ToList();
                result.DAUInfoList.AddRange(dauInfoList);
                result.Total = total;

                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                throw;
            }
        }

        #region 通过采集单元id读取所有传感器的详细信息

        /// <summary>
        /// 此方法可进行优化，优化点：循环中访问数据库
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<GetAllWSInfoByDAUIDResult> GetAllWSInfoByDAUID(GetAllWSInfoByDAUIDParameter param)
        {
            BaseResponse<GetAllWSInfoByDAUIDResult> response = new BaseResponse<GetAllWSInfoByDAUIDResult>();
            GetAllWSInfoByDAUIDResult result = new GetAllWSInfoByDAUIDResult();

            try
            {
                if (!param.DAUID.Any())
                {
                    response.IsSuccessful = false;
                    response.Code = "000000";
                    //response.Reason = "获取所有WS信息参数异常";
                    return response;
                }

                List<WGInfoItem> wgInfoList = new List<WGInfoItem>();
                foreach (int wgid in param.DAUID)
                {
                    WGInfoItem wgInfo = new WGInfoItem();

                    #region 组装WG数据

                    var wg = wgRepository.GetByKey(wgid);
                    if (wg == null)
                    {
                        continue;
                    }
                    wgInfo.DAUID = wg.WGID;
                    wgInfo.DAUName = wg.WGName;

                    iCMSDbContext dbContext = new iCMSDbContext();
                    List<WSInfoItem> wsInfoList = new List<WSInfoItem>();

                    #region 组装WS数据

                    var wsList = wsRepository.GetDatas<WS>(t => t.WGID == wgid, true).ToList();//找到WG下挂靠的所有WS
                    var measureSiteTypeArray = msTypeRepository.GetDatas<MeasureSiteType>(t => true, true).ToList();
                    var deviceArray = deviceRepository.GetDatas<Device>(t => true, true).ToList();
                    if (wsList.Any())
                    {
                        foreach (WS ws in wsList)
                        {
                            WSInfoItem wsInfoItem = new WSInfoItem();

                            wsInfoItem.WSID = ws.WSID;
                            wsInfoItem.WSName = ws.WSName;
                            wsInfoItem.ChannelId = ws.ChannelId;

                            List<VibsignalInfoItem> vibsignalInfoList = new List<VibsignalInfoItem>();

                            #region 组装振动信号数据

                            //通过WS找到测点，通过测点找到振动信号数据
                            var curMeasureSite = measureSiteRepository
                                .GetDatas<MeasureSite>(t => t.WSID.HasValue && t.WSID.Value == ws.WSID, true)
                                .FirstOrDefault();
                            if (curMeasureSite != null)//WS已经挂靠测点，已经被使用
                            {
                                wsInfoItem.MesureSiteID = curMeasureSite.MSiteID;
                                wsInfoItem.MesureSiteName = measureSiteTypeArray.Where(t => t.ID == curMeasureSite.MSiteName).First().Name;
                                wsInfoItem.DevID = curMeasureSite.DevID;

                                var curDevice = deviceArray.Where(t => t.DevID == wsInfoItem.DevID).FirstOrDefault();
                                if (curDevice != null)
                                {
                                    wsInfoItem.DevName = curDevice.DevName;
                                }

                                wsInfoItem.IsUsed = true;

                                //判断是否是转速测点
                                var isSpeed = speedSamplingMDFRepository
                                    .GetDatas<SpeedSamplingMDF>(t => t.MSiteID == curMeasureSite.MSiteID, true)
                                    .Any();
                                wsInfoItem.IsSpeed = isSpeed;

                                //通过测点找到振动信号信息
                                var vibsignalList = vibSignalRepository
                                    .GetDatas<VibSingal>(t => t.MSiteID == curMeasureSite.MSiteID, true)
                                    .ToList();

                                if (vibsignalList.Any())
                                {
                                    wsInfoItem.IsVibration = true;

                                    foreach (VibSingal vib in vibsignalList)
                                    {
                                        VibsignalInfoItem vibsignalInfoItem = new VibsignalInfoItem();

                                        #region 组装振动信号数据

                                        vibsignalInfoItem.VibsignalID = vib.SingalID;
                                        vibsignalInfoItem.SignalType = vib.SingalType;

                                        List<EigenValueInfoItem> eigenValueInfoList = new List<EigenValueInfoItem>();

                                        #region 组装特征值数据

                                        var signalAlmsetList = signalAlmsetRepository
                                            .GetDatas<SignalAlmSet>(t => t.SingalID == vib.SingalID, true)
                                            .ToList();
                                        if (signalAlmsetList.Any())
                                        {
                                            foreach (SignalAlmSet almset in signalAlmsetList)
                                            {
                                                EigenValueInfoItem eigenValueItem = new EigenValueInfoItem();
                                                eigenValueItem.EigenValueID = almset.SingalAlmID;
                                                eigenValueItem.EigenValueTypeID = almset.ValueType;

                                                var eigenValueType = eigenValueTypeRepository.GetByKey(almset.ValueType);
                                                if (eigenValueType != null)
                                                {
                                                    eigenValueItem.EigenValueTypeName = eigenValueType.Name;
                                                }

                                                eigenValueInfoList.Add(eigenValueItem);
                                            }
                                        }

                                        #endregion

                                        vibsignalInfoItem.EigenValueInfoList.AddRange(eigenValueInfoList);

                                        #endregion

                                        vibsignalInfoList.Add(vibsignalInfoItem);
                                    }
                                }
                            }

                            #endregion

                            wsInfoItem.VibsignalInfoList.AddRange(vibsignalInfoList);
                            wsInfoList.Add(wsInfoItem);
                        }
                    }

                    #endregion

                    #region 如果是有线WG，需要添加温度传感器信息

                    if (wg.DevFormType == (int)EnumDevFormType.Wired)
                    {
                        List<int> usedTempeChannelIDArray = (from alsm in dbContext.TempeDeviceSetMSiteAlm
                                                             where alsm.WGID == wg.WGID
                                                             join mdf in dbContext.ModBusRegisterAddress on alsm.MsiteAlmID equals mdf.MDFID
                                                             select mdf.RegisterAddress).ToList();

                        for (int i = 0; i <= 15; i++)
                        {
                            WSInfoItem temperatureWS = new WSInfoItem();

                            temperatureWS.WSID = i;
                            temperatureWS.WSName = string.Format("T/{0}", i);
                            temperatureWS.IsTemperature = true;
                            temperatureWS.ChannelId = i;

                            //MSID
                            int? curMSiteID =
                                (from alms in dbContext.TempeDeviceSetMSiteAlm
                                 where alms.WGID == wgid
                                 join mdf in dbContext.ModBusRegisterAddress on alms.MsiteAlmID equals mdf.MDFID
                                 where mdf.RegisterAddress == i
                                 select alms.MsiteID)
                                 .ToList()
                                 .FirstOrDefault();

                            if (curMSiteID.HasValue && curMSiteID.Value > 0)
                            {
                                temperatureWS.MesureSiteID = curMSiteID.Value;

                                var curMeasureSite = measureSiteRepository
                                    .GetDatas<MeasureSite>(t => t.MSiteID == temperatureWS.MesureSiteID, true)
                                    .FirstOrDefault();

                                if (curMeasureSite != null)//WS已经挂靠测点，已经被使用
                                {
                                    temperatureWS.MesureSiteID = curMeasureSite.MSiteID;
                                    temperatureWS.MesureSiteName = measureSiteTypeArray.Where(t => t.ID == curMeasureSite.MSiteName).First().Name;
                                    temperatureWS.DevID = curMeasureSite.DevID;

                                    var curDevice = deviceArray.Where(t => t.DevID == curMeasureSite.DevID).FirstOrDefault();
                                    if (curDevice != null)
                                    {
                                        temperatureWS.DevName = curDevice.DevName;
                                    }
                                }
                            }

                            //是否使用
                            temperatureWS.IsUsed = usedTempeChannelIDArray.Contains(i);
                            wsInfoList.Add(temperatureWS);
                        }
                    }

                    #endregion

                    wgInfo.WSInfoList.AddRange(wsInfoList);

                    #endregion

                    wgInfoList.Add(wgInfo);
                }

                result.WGInfoList.AddRange(wgInfoList);
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                //response.Reason = "获取所有WS信息发生异常";
                return response;
            }
        }

        #endregion

        #region 读取某一采集单元下的可用的温度传感器

        public BaseResponse<GetAvailableTemperatureWSByDAUIDResult> GetAvailableTemperatureWSByDAUID(GetAvailableTemperatureWSByDAUIDParameter param)
        {
            BaseResponse<GetAvailableTemperatureWSByDAUIDResult> response = new BaseResponse<GetAvailableTemperatureWSByDAUIDResult>();
            GetAvailableTemperatureWSByDAUIDResult result = new GetAvailableTemperatureWSByDAUIDResult();

            try
            {
                int wgID = param.DAUID;

                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    var wsLinq =
                        (from mdf in dbContext.TempeDeviceSetMSiteAlm
                         join re in dbContext.ModBusRegisterAddress
                         on mdf.MsiteAlmID equals re.MDFID
                         where mdf.WGID == wgID
                         select re.RegisterAddress)
                         .ToList();

                    List<int> allTempeChannel = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

                    var availableChannels = allTempeChannel.Except(wsLinq);
                    List<TemperatureWS> tempeChannelList = new List<TemperatureWS>();
                    if (availableChannels.Any())
                    {
                        foreach (int id in availableChannels)
                        {
                            TemperatureWS info = new TemperatureWS();
                            info.WSID = id;
                            info.WSName = string.Format("T/{0}", id);

                            tempeChannelList.Add(info);
                        }
                    }

                    result.TemperatureWSInfo.AddRange(tempeChannelList);
                    response.Result = result;

                    return response;
                }
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                //response.Reason = "获取可用的温度传感器信息发生异常";
                return response;
            }
        }

        #endregion

        #region 根据某一采集单元获取所有可用采集通道信息（除温度）

        public BaseResponse<GetUsableChannelByDAUIDResult> GetUsableChannelByDAUID(GetUsableChannelByDAUIDParameter param)
        {
            BaseResponse<GetUsableChannelByDAUIDResult> response = new BaseResponse<GetUsableChannelByDAUIDResult>();
            GetUsableChannelByDAUIDResult result = new GetUsableChannelByDAUIDResult();

            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    //振动通道列表
                    var allVibChannelArray = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
                    //已添加的通道
                    var addedChnnel =
                        (from ws in dbContext.WS
                         where ws.WGID == param.DAUID
                         select ws.ChannelId ?? 0)
                        .ToList();

                    //取差集
                    var avalChannel = allVibChannelArray.Except(addedChnnel).ToList();

                    foreach (int chid in avalChannel)
                    {
                        string wsName = string.Empty;
                        if (chid <= 15)
                        {
                            wsName = string.Format("V/{0}", chid);
                        }
                        else if (chid > 15 && chid < 20)
                        {
                            wsName = string.Format("P/{0}", chid);
                        }
                        else
                        {
                            wsName = string.Format("S/{0}", chid);
                        }

                        result.ChannelInfo.Add(new ChannelInfo
                        {
                            WSID = chid,
                            WSName = wsName
                        });
                    }

                    response.Result = result;
                    return response;
                }
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.Code = "000000";
                //response.Reason = "获取可用的DAU通道信息发生异常";
                return response;
            }
        }

        #endregion

        #region 更新特征值/测点告警阈值的时候，同步更新父级节点的状态

        //Added by QXM, 2017/01/06
        /// <summary>
        /// 更新测点的报警状态
        /// </summary>
        /// <param name="msiteIDList"></param>
        private void UpdateSignalStatus(int signalID)
        {
            int status = 0;

            var almSets = signalAlmSetRepository.GetDatas<SignalAlmSet>(t => t.SingalID == signalID, true).ToList();
            foreach (var item in almSets)
            {
                //遍历找到振动信号下特征值报警最大值
                if (item.Status > status)
                {
                    status = item.Status;
                }
            }

            var vibSignal = vibSignalRepository.GetByKey(signalID);
            vibSignal.SingalStatus = status;
            vibSignalRepository.Update<VibSingal>(vibSignal);
        }

        private void UpdateMSiteStatus(int msiteID)
        {
            int status = 0;
            //获取测点下设备温度报警状态
            var deviceTempe = tempeDeviceSetMSiteAlmRepository.GetDatas<TempeDeviceSetMSiteAlm>
                (t => t.MsiteID == msiteID, true).FirstOrDefault();
            if (deviceTempe != null)
            {
                if (deviceTempe.Status > status)
                {
                    status = deviceTempe.Status;
                }
            }

            var vibSignals = vibSignalRepository.GetDatas<VibSingal>(t => t.MSiteID == msiteID, true).ToList();
            foreach (var item in vibSignals)
            {
                if (item.SingalStatus > status)
                {
                    status = item.SingalStatus;
                }
            }

            var msite = measureSiteRepository.GetByKey(msiteID);
            if (msite != null)
            {
                msite.MSiteStatus = status;
                measureSiteRepository.Update<MeasureSite>(msite);
            }
        }

        private void UpdateDeviceStatus(int devID)
        {
            int status = 0;
            var msites = measureSiteRepository.GetDatas<MeasureSite>(t => t.DevID == devID, true).ToList();
            foreach (var item in msites)
            {
                if (item.MSiteStatus > status)
                {
                    status = item.MSiteStatus;
                }
            }

            var device = deviceRepository.GetByKey(devID);
            if (device != null)
            {
                device.AlmStatus = status;
                deviceRepository.Update<Device>(device);
            }
        }

        private void UpdateMonitorTreeStatus_MU(int mtID)
        {
            int status = 0;
            List<int> allChildNodes = new List<int>();
            GetAllChildren(mtID, allChildNodes);

            var devices = deviceRepository.GetDatas<Device>(t => allChildNodes.Contains(t.MonitorTreeID), true).ToList();
            foreach (var item in devices)
            {
                if (item.AlmStatus > status)
                {
                    status = item.AlmStatus;
                }
            }

            var mt = monitorTreeRepository.GetByKey(mtID);
            if (mt != null)
            {
                mt.Status = status;
                monitorTreeRepository.Update<MonitorTree>(mt);
            }
        }

        private void ModifyStatusBeyondMSite(int msiteID)
        {
            UpdateMSiteStatus(msiteID);
            var msite = measureSiteRepository.GetByKey(msiteID);
            if (msite != null)
            {
                UpdateDeviceStatus(msite.DevID);

                var device = deviceRepository.GetByKey(msite.DevID);
                if (device != null)
                {
                    UpdateMonitorTreeStatus(device.MonitorTreeID);
                }
            }
        }

        private void ModifyStatusBeyondSignal(int signalID)
        {
            UpdateSignalStatus(signalID);
            var signal = vibSignalRepository.GetByKey(signalID);
            if (signal != null)
            {
                int MSiteID = signal.MSiteID;
                ModifyStatusBeyondMSite(MSiteID);
            }
        }

        #endregion

        #region 更新检测树节点状态

        private void UpdateMonitorTreeLeafNodeStatus(int deviceID)
        {
            var device = deviceRepository.GetByKey(deviceID);
            if (null != device)
            {
                int leafID = device.MonitorTreeID;
                var deviceList = deviceRepository.GetDatas<Device>(t => t.MonitorTreeID == leafID, true).ToList();
                int maxStatus = deviceList.Select(t => t.AlmStatus).Max();


                var monitorTree = monitorTreeRepository.GetByKey(leafID);
                if (null != monitorTree)
                {
                    monitorTree.Status = maxStatus;
                    monitorTreeRepository.Update<MonitorTree>(monitorTree);

                    UpdateMonitorTreeStatus(monitorTree.MonitorTreeID);
                }
            }
        }

        private void UpdateMonitorTreeStatus(int nodeID)
        {
            var node = monitorTreeRepository.GetByKey(nodeID);
            if (node != null)
            {
                if (node.PID >= 0)//不是根节点
                {
                    int parentID = node.PID;
                    var monitorTreeList = monitorTreeRepository.GetDatas<MonitorTree>(t => t.PID == node.PID, true).ToList();
                    int maxStatus = monitorTreeList.Select(t => t.Status).Max();

                    var parentNode = monitorTreeRepository.GetByKey(parentID);
                    if (null != parentNode)
                    {
                        parentNode.Status = maxStatus;
                        monitorTreeRepository.Update<MonitorTree>(parentNode);

                        UpdateMonitorTreeStatus(parentNode.PID);
                    }
                }
            }
        }

        #endregion

        #region 取得振动信号报警状态

        private int GetSignalStatus(int signalId)
        {
            int status = (int)EnumAlarmStatus.Normal;
            List<SignalAlmSet> almSets = signalAlmSetRepository
                .GetDatas<SignalAlmSet>(obj => obj.SingalID == signalId, false)
                .ToList();
            if (almSets != null && almSets.Count > 0)
            {
                foreach (SignalAlmSet alm in almSets)
                {
                    if (alm.Status > status)
                    {
                        status = alm.Status;
                    }
                }
            }
            return status;
        }

        #endregion

        #region 判特采集数据的状态

        /// <summary>
        /// 判特采集数据的状态
        /// </summary>
        /// <param name="warnValue">高报值</param>
        /// <param name="almValue">高高报值</param>
        /// <param name="values">采集数据值</param>
        /// <param name="status">状态</param>
        private void GetSamplingDataStatus(float warnValue, float almValue, float? values, ref int status, int? signalID = null, int? eigenValueTypeID = null)
        {
            int resultStatus = 0;

            if (values.HasValue) //如果未采集，则状态为 0
            {
                //可能都不会为空
                if (signalID == null && eigenValueTypeID == null)
                {
                    if (values < warnValue)
                    {
                        resultStatus = (int)EnumAlarmStatus.Normal;
                    }
                    else if (values >= warnValue && values < almValue)
                    {
                        resultStatus = (int)EnumAlarmStatus.Warning;
                    }
                    else if (values >= almValue)
                    {
                        resultStatus = (int)EnumAlarmStatus.Danger;
                    }
                }
                else
                {
                    if (values < warnValue)
                    {
                        resultStatus = (int)EnumAlarmStatus.Normal;

                        //如果是正常则进行重置数据
                        RecordAlarmCount recordAlarmCount = CollectionsExtensions.recordAlmCountList
                            .Where(p => p.SignalID == signalID && p.EigenValueTypeID == eigenValueTypeID)
                            .FirstOrDefault();

                        //如果为空则进行添加 王颖辉 进行修改 2016-08-12
                        if (recordAlarmCount == null)
                        {
                            recordAlarmCount = new RecordAlarmCount();
                            recordAlarmCount.SignalID = signalID;
                            recordAlarmCount.EigenValueTypeID = eigenValueTypeID;
                            recordAlarmCount.WarnCount = 0;//高报
                            recordAlarmCount.AlarmCount = 0;//高高报
                            CollectionsExtensions.recordAlmCountList.Add(recordAlarmCount);
                        }
                        else
                        {
                            recordAlarmCount.WarnCount = 0;
                            recordAlarmCount.AlarmCount = 0;
                        }
                    }
                    else if (values >= warnValue && values < almValue)
                    {
                        resultStatus = (int)EnumAlarmStatus.Warning;

                        #region 高报，及高高报累加及重置

                        List<RecordAlarmCount> recordList = CollectionsExtensions.recordAlmCountList
                            .Where(p => p.SignalID == signalID && p.EigenValueTypeID == eigenValueTypeID)
                            .ToList<RecordAlarmCount>();

                        //列表中，不存在信号类型和特征值对应的数据，则进行添加
                        if (recordList != null && recordList.Count == 0)
                        {
                            RecordAlarmCount recordAlarmCount = new RecordAlarmCount();
                            recordAlarmCount.SignalID = signalID;
                            recordAlarmCount.EigenValueTypeID = eigenValueTypeID;
                            recordAlarmCount.WarnCount = 1;//高报
                            recordAlarmCount.AlarmCount = 0;//高高报
                            CollectionsExtensions.recordAlmCountList.Add(recordAlarmCount);
                        }
                        else
                        {
                            //有存在数据，则进行修改
                            if (recordList.Count > 0)
                            {
                                var recordInfo = recordList.FirstOrDefault();
                                if (recordInfo != null)
                                {
                                    recordInfo.WarnCount++;//高报累加
                                    recordInfo.AlarmCount = 0;//高高报
                                    if (recordInfo.WarnCount > 3)
                                    {
                                        recordInfo.WarnCount = 1;
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    else if (values >= almValue)
                    {
                        resultStatus = (int)EnumAlarmStatus.Danger;

                        #region 高报，及高高报累加及重置

                        List<RecordAlarmCount> recordList = CollectionsExtensions.recordAlmCountList
                            .Where(p => p.SignalID == signalID && p.EigenValueTypeID == eigenValueTypeID)
                            .ToList<RecordAlarmCount>();

                        //列表中，不存在信号类型和特征值对应的数据，则进行添加
                        if (recordList != null && recordList.Count == 0)
                        {
                            RecordAlarmCount recordAlarmCount = new RecordAlarmCount();
                            recordAlarmCount.SignalID = signalID;
                            recordAlarmCount.EigenValueTypeID = eigenValueTypeID;
                            recordAlarmCount.WarnCount = 0;//高报
                            recordAlarmCount.AlarmCount = 1;//高高报
                            CollectionsExtensions.recordAlmCountList.Add(recordAlarmCount);
                        }
                        else
                        {
                            //有存在数据，则进行修改
                            if (recordList.Count > 0)
                            {
                                var recordInfo = recordList.FirstOrDefault();
                                if (recordInfo != null)
                                {
                                    recordInfo.WarnCount = 0;//高报
                                    recordInfo.AlarmCount++;//高高报累加
                                    if (recordInfo.AlarmCount > 3)
                                    {
                                        recordInfo.AlarmCount = 1;
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                }
            }
            else
            {
                //未采集
            }
            status = resultStatus;
        }

        #endregion

        #region  获取实际采样率

        /// <summary>
        /// 获取实际采样率
        /// </summary>
        /// <param name="inputNum"></param>
        /// <returns></returns>
        private float GetRealSamplingFrequency(float inputNum)
        {
            LogHelper.WriteLog(inputNum.ToString());
            return inputNum * ConstObject.Measure_Define_SamplingFrequency_256;
        }

        #endregion

        #region 监测树获取所有子节点

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
        /// 找到所有的父节点
        /// </summary>
        /// <param name="monitorTreeID"></param>
        /// <param name="allParents"></param>
        private void GetAllParent(int monitorTreeID, List<int> allParents)
        {
            allParents.Add(monitorTreeID);

            var monitorTree = monitorTreeRepository.GetByKey(monitorTreeID);
            if (null != monitorTree && monitorTree.PID != 0 && monitorTree.PID != -1)
            {
                GetAllParent(monitorTree.PID, allParents);
            }
        }

        #endregion

        #region 根据速度波形计算转频

        /// <summary>
        /// 根据速度波形计算转频
        /// </summary>
        /// <returns>异常返回“-”</returns>
        private string CalculateRotationFrequencyByVelocityWave(UploadWaveDataParameter param, float rotate, VibSingal velSignal)
        {
            string rotationFreq = ConstObject.RotationFrequency_Default;
            try
            {
                if (velSignal.UpLimitFrequency == 0 || velSignal.WaveDataLength == 0)
                {
                    return rotationFreq;
                }

                float realSamplingFrequency = (velSignal.UpLimitFrequency ?? 0) * ConstObject.Measure_Define_SamplingFrequency_256;
                int waveLength = Convert.ToInt32(velSignal.WaveDataLength);
                //取得频域X轴数据
                List<double> FDXData = CalculateWaveXYValue.GetFDXData(waveLength, realSamplingFrequency);

                //取得频域Y轴数据
                List<double> FDYData = CalculateWaveXYValue.GetFDYData(param.WaveData.ToList(), realSamplingFrequency, false);

                float specifiedRF = rotate / 60;
                bool isFirst = true;
                double maxValue = 0;
                List<double> fdX = new List<double>();

                for (int i = 0; i < FDXData.Count; i++)
                {
                    if (FDXData[i] < specifiedRF * 0.5)
                    {
                        continue;
                    }

                    if (FDXData[i] >= specifiedRF * 0.5 && FDXData[i] <= specifiedRF * 1.1)
                    {
                        if (isFirst == true)
                        {
                            maxValue = FDYData[i];
                            fdX.Add(FDXData[i]);
                            isFirst = false;
                        }
                        else
                        {
                            if (FDYData[i] > maxValue)
                            {
                                maxValue = FDYData[i];
                                fdX.Clear();
                                fdX.Add(FDXData[i]);
                            }
                            if (FDYData[i] == maxValue)
                            {
                                fdX.Add(FDXData[i]);
                            }
                        }
                    }

                    if (FDXData[i] > specifiedRF * 1.1)
                    {
                        break;
                    }
                }
                if (fdX.Count > 0)
                    rotationFreq = fdX.Average().ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return rotationFreq;
        }

        #endregion
    }
}