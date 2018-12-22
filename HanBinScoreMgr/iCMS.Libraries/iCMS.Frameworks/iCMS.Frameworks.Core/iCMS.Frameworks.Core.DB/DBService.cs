using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Frameworks.Core.DB
{
    public class DBServices
    {
        private static DBServices m_instance = null;
        private static readonly object m_s_lock = new object();

        #region 服务实例

        public IUSERService USERProxy = null;
        public IDeviceTypeService DeviceTypeProxy = null;
        public IUserLogService UserLogProxy = null;
        public IConnectStatusTypeService ConnectStatusTypeProxy = null;
        public ISysLogService SysLogProxy = null;
        public IEigenValueTypeService EigenValueTypeProxy = null;
        public IMeasureSiteMonitorTypeService MeasureSiteMonitorTypeProxy = null;
        public IMeasureSiteTypeService MeasureSiteTypeProxy = null;
        public IMonitorTreeTypeService MonitorTreeTypeProxy = null;
        public ISensorTypeService SensorTypeProxy = null;
        public IVibratingSingalTypeService VibratingSingalTypeProxy = null;
        public IWaveLengthValuesService WaveLengthValuesProxy = null;
        public IWaveLowerLimitValuesService WaveLowerLimitValuesProxy = null;
        public IWaveUpperLimitValuesService WaveUpperLimitValuesProxy = null;
        public IWirelessGatewayTypeService WirelessGatewayTypeProxy = null;
        public IWSService WSProxy = null;
        public IDeviceService DeviceProxy = null;
        public IVibSingalService VibSingalProxy = null;
        public IVibratingSingalCharHisAccService VibratingSingalCharHisAccProxy = null;
        public IVibratingSingalCharHisDispService VibratingSingalCharHisDispProxy = null;
        public IVibratingSingalCharHisEnvlService VibratingSingalCharHisEnvlProxy = null;
        public IVibratingSingalCharHisLQService VibratingSingalCharHisLQProxy = null;
        public IVibratingSingalCharHisVelService VibratingSingalCharHisVelProxy = null;
        public ISignalAlmSetService SignalAlmSetProxy = null;
        public IBearingService BearingProxy = null;
        public IDevAlmRecordService DevAlmRecordProxy = null;
        public IImageService ImageProxy = null;
        public ILogService LogProxy = null;
        public IMeasureSiteService MeasureSiteProxy = null;
        public IModuleService ModuleProxy = null;
        public IMonitorTreePropertyService MonitorTreePropertyProxy = null;
        public IMonitorTreeService MonitorTreeProxy = null;
        public IOperationService OperationProxy = null;
        public IRealTimeCollectInfoService RealTimeCollectInfoProxy = null;
        public IRoleModuleService RoleModuleProxy = null;
        public ITempeDeviceMsitedata_1_Service TempeDeviceMsitedata_1_Proxy = null;
        public ITempeDeviceMsitedata_2_Service TempeDeviceMsitedata_2_Proxy = null;
        public ITempeDeviceMsitedata_3_Service TempeDeviceMsitedata_3_Proxy = null;
        public ITempeDeviceMsitedata_4_Service TempeDeviceMsitedata_4_Proxy = null;
        public ITempeDeviceSetMsiteAlmService TempeDeviceSetMsiteAlmProxy = null;
        public ITempeWSMsitedata_1_Service TempeWSMsitedata_1_Proxy = null;
        public ITempeWSMsitedata_2_Service TempeWSMsitedata_2_Proxy = null;
        public ITempeWSMsitedata_3_Service TempeWSMsitedata_3_Proxy = null;
        public ITempeWSMsitedata_4_Service TempeWSMsitedata_4_Proxy = null;

        public ITempeWSSetMSiteAlmService TempeWSSetMSiteAlmProxy = null;
        public IUserRalationDeviceService UserRalationDeviceProxy = null;
        public IVibSingalRTService VibSingalRTServicProxy = null;
        public IVoltageSetMSiteAlmService VoltageSetMSiteAlmProxy = null;
        public IVoltageWSMSiteData_1_Service VoltageWSMSiteData_1_Proxy = null;
        public IVoltageWSMSiteData_2_Service VoltageWSMSiteData_2_Proxy = null;
        public IVoltageWSMSiteData_3_Service VoltageWSMSiteData_3_Proxy = null;
        public IVoltageWSMSiteData_4_Service VoltageWSMSiteData_4_Proxy = null;
        public IWGService WGProxy = null;
        public IWsnAlmrecordService WsnAlmRecordProxy = null;

        public IRoleService RoleProxy = null;

        public IFactoryService FactoryProxy = null;
        public IConfigService ConfigProxy = null;
        #endregion


        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        private DBServices()
        {
            //DI via app.config
            //ServiceLocator.RegisterTypesFromConfig();

            #region 注入核心服务

            //用户
            ServiceLocator.RegisterService<IUSERService, USERService>();
            //设备类型信息
            ServiceLocator.RegisterService<IDeviceTypeService, DeviceTypeService>();
            //连接状态类型
            ServiceLocator.RegisterService<IConnectStatusTypeService, ConnectStatusTypeService>();
            ServiceLocator.RegisterService<IUserLogService, UserLogService>();
            ServiceLocator.RegisterService<ISysLogService, SysLogService>();
            //特征值类型
            ServiceLocator.RegisterService<IEigenValueTypeService, EigenValueTypeService>();
            //测量位置监测类型
            ServiceLocator.RegisterService<IMeasureSiteMonitorTypeService, MeasureSiteMonitorTypeService>();
            //测量位置类型
            ServiceLocator.RegisterService<IMeasureSiteTypeService, MeasureSiteTypeService>();
            //监测树类型
            ServiceLocator.RegisterService<IMonitorTreeTypeService, MonitorTreeTypeService>();
            //传感器类型
            ServiceLocator.RegisterService<ISensorTypeService, SensorTypeService>();
            //振动信号类型
            ServiceLocator.RegisterService<IVibratingSingalTypeService, VibratingSingalTypeService>();
            //波形长度数值
            ServiceLocator.RegisterService<IWaveLengthValuesService, WaveLengthValuesService>();
            //波形下限频率值
            ServiceLocator.RegisterService<IWaveLowerLimitValuesService, WaveLowerLimitValuesService>();
            //波形上限频率值
            ServiceLocator.RegisterService<IWaveUpperLimitValuesService, WaveUpperLimitValuesService>();
            //无线网关类型
            ServiceLocator.RegisterService<IWirelessGatewayTypeService, WirelessGatewayTypeService>();
            //WS信息
            ServiceLocator.RegisterService<IWSService, WSService>();
            //设备信息
            ServiceLocator.RegisterService<IDeviceService, DeviceService>();
            //振动信号信息
            ServiceLocator.RegisterService<IVibSingalService, VibSingalService>();
            //速度振动信号历史数据表
            ServiceLocator.RegisterService<IVibratingSingalCharHisVelService, VibratingSingalCharHisVelService>();
            //振动信号报警阈值设置
            ServiceLocator.RegisterService<ISignalAlmSetService, SignalAlmSetService>();
            ServiceLocator.RegisterService<IVibratingSingalCharHisAccService, VibratingSingalCharHisAccService>();
            ServiceLocator.RegisterService<IBearingService, BearingService>();
            ServiceLocator.RegisterService<IDevAlmRecordService, DevAlmRecordService>();
            ServiceLocator.RegisterService<IImageService, ImageService>();

            //ServiceLocator.RegisterService<ILogService, LogService>();

            ServiceLocator.RegisterService<IMeasureSiteService, MeasureSiteService>();
            ServiceLocator.RegisterService<IModuleService, ModuleService>();
            ServiceLocator.RegisterService<IMonitorTreePropertyService, MonitorTreePropertyService>();

            ServiceLocator.RegisterService<IMonitorTreeService, MonitorTreeService>();
            ServiceLocator.RegisterService<IOperationService, OperationService>();
            ServiceLocator.RegisterService<IRealTimeCollectInfoService, RealTimeCollectInfoService>();

            ServiceLocator.RegisterService<IRoleModuleService, RoleModuleService>();
            ServiceLocator.RegisterService<ITempeDeviceMsitedata_1_Service, TempeDeviceMsitedata_1_Service>();
            ServiceLocator.RegisterService<ITempeDeviceMsitedata_2_Service, TempeDeviceMsitedata_2_Service>();
            ServiceLocator.RegisterService<ITempeDeviceMsitedata_3_Service, TempeDeviceMsitedata_3_Service>();
            ServiceLocator.RegisterService<ITempeDeviceMsitedata_4_Service, TempeDeviceMsitedata_4_Service>();

            ServiceLocator.RegisterService<ITempeWSMsitedata_1_Service, TempeWSMsitedata_1_Service>();
            ServiceLocator.RegisterService<ITempeWSMsitedata_2_Service, TempeWSMsitedata_2_Service>();
            ServiceLocator.RegisterService<ITempeWSMsitedata_3_Service, TempeWSMsitedata_3_Service>();
            ServiceLocator.RegisterService<ITempeWSMsitedata_4_Service, TempeWSMsitedata_4_Service>();

            ServiceLocator.RegisterService<ITempeWSSetMSiteAlmService, TempeWSSetMSiteAlmService>();
            ServiceLocator.RegisterService<IUserRalationDeviceService, UserRalationDeviceService>();

            ServiceLocator.RegisterService<IVibSingalRTService, VibSingalRTService>();
            ServiceLocator.RegisterService<IVoltageSetMSiteAlmService, VoltageSetMSiteAlmService>();

            ServiceLocator.RegisterService<IVoltageWSMSiteData_1_Service, VoltageWSMSiteData_1_Service>();
            ServiceLocator.RegisterService<IVoltageWSMSiteData_2_Service, VoltageWSMSiteData_2_Service>();
            ServiceLocator.RegisterService<IVoltageWSMSiteData_3_Service, VoltageWSMSiteData_3_Service>();
            ServiceLocator.RegisterService<IVoltageWSMSiteData_4_Service, VoltageWSMSiteData_4_Service>();

            ServiceLocator.RegisterService<IWGService, WGService>();
            ServiceLocator.RegisterService<IWsnAlmrecordService, WsnAlmrecordService>();
            ServiceLocator.RegisterService<IRoleService, RoleService>();
            ServiceLocator.RegisterService<ITempeDeviceSetMsiteAlmService, TempeDeviceSetMsiteAlmService>();
            ServiceLocator.RegisterService<IVibratingSingalCharHisEnvlService, VibratingSingalCharHisEnvlService>();

            ServiceLocator.RegisterService<IFactoryService, FactoryService>();

            ServiceLocator.RegisterService<IVibratingSingalCharHisLQService, VibratingSingalCharHisLQService>();
            ServiceLocator.RegisterService<IVibratingSingalCharHisDispService, VibratingSingalCharHisDispService>();
            ServiceLocator.RegisterService<IConfigService, ConfigService>();
            #endregion

            #region 服务实例赋值

            //用户
            USERProxy = ServiceLocator.GetService<IUSERService>();
            //设备类型信息
            DeviceTypeProxy = ServiceLocator.GetService<IDeviceTypeService>();
            //连接状态类型
            ConnectStatusTypeProxy = ServiceLocator.GetService<IConnectStatusTypeService>();
            UserLogProxy = ServiceLocator.GetService<IUserLogService>();
            SysLogProxy = ServiceLocator.GetService<ISysLogService>();
            //特征值类型
            EigenValueTypeProxy = ServiceLocator.GetService<IEigenValueTypeService>();
            //测量位置监测类型
            MeasureSiteMonitorTypeProxy = ServiceLocator.GetService<IMeasureSiteMonitorTypeService>();
            //测量位置类型
            MeasureSiteTypeProxy = ServiceLocator.GetService<IMeasureSiteTypeService>();
            //监测树类型
            MonitorTreeTypeProxy = ServiceLocator.GetService<IMonitorTreeTypeService>();
            //传感器类型
            SensorTypeProxy = ServiceLocator.GetService<ISensorTypeService>();
            //振动信号类型
            VibratingSingalTypeProxy = ServiceLocator.GetService<IVibratingSingalTypeService>();
            //波形长度数值
            WaveLengthValuesProxy = ServiceLocator.GetService<IWaveLengthValuesService>();
            //波形下限频率值
            WaveLowerLimitValuesProxy = ServiceLocator.GetService<IWaveLowerLimitValuesService>();
            //波形上限频率值
            WaveUpperLimitValuesProxy = ServiceLocator.GetService<IWaveUpperLimitValuesService>();
            //无线网关类型
            WirelessGatewayTypeProxy = ServiceLocator.GetService<IWirelessGatewayTypeService>();
            //WS信息
            WSProxy = ServiceLocator.GetService<IWSService>();
            //设备信息
            DeviceProxy = ServiceLocator.GetService<IDeviceService>();
            //振动信号信息
            VibSingalProxy = ServiceLocator.GetService<IVibSingalService>();
            //速度振动信号历史数据表
            VibratingSingalCharHisVelProxy = ServiceLocator.GetService<IVibratingSingalCharHisVelService>();
            //振动信号报警阈值设置
            SignalAlmSetProxy = ServiceLocator.GetService<ISignalAlmSetService>();

            BearingProxy = ServiceLocator.GetService<IBearingService>();
            DevAlmRecordProxy = ServiceLocator.GetService<IDevAlmRecordService>();
            ImageProxy = ServiceLocator.GetService<IImageService>();
            //LogProxy = ServiceLocator.GetService<ILogService>();
            MeasureSiteProxy = ServiceLocator.GetService<IMeasureSiteService>();
            ModuleProxy = ServiceLocator.GetService<IModuleService>();
            MonitorTreePropertyProxy = ServiceLocator.GetService<IMonitorTreePropertyService>();
            MonitorTreeProxy = ServiceLocator.GetService<IMonitorTreeService>();
            OperationProxy = ServiceLocator.GetService<IOperationService>();
            RealTimeCollectInfoProxy = ServiceLocator.GetService<IRealTimeCollectInfoService>();
            RoleModuleProxy = ServiceLocator.GetService<IRoleModuleService>();
            TempeDeviceMsitedata_1_Proxy = ServiceLocator.GetService<ITempeDeviceMsitedata_1_Service>();
            TempeDeviceMsitedata_2_Proxy = ServiceLocator.GetService<ITempeDeviceMsitedata_2_Service>();
            TempeDeviceMsitedata_3_Proxy = ServiceLocator.GetService<ITempeDeviceMsitedata_3_Service>();
            TempeDeviceMsitedata_4_Proxy = ServiceLocator.GetService<ITempeDeviceMsitedata_4_Service>();

            TempeWSMsitedata_1_Proxy = ServiceLocator.GetService<ITempeWSMsitedata_1_Service>();
            TempeWSMsitedata_2_Proxy = ServiceLocator.GetService<ITempeWSMsitedata_2_Service>();
            TempeWSMsitedata_3_Proxy = ServiceLocator.GetService<ITempeWSMsitedata_3_Service>();
            TempeWSMsitedata_4_Proxy = ServiceLocator.GetService<ITempeWSMsitedata_4_Service>();

            TempeWSSetMSiteAlmProxy = ServiceLocator.GetService<ITempeWSSetMSiteAlmService>();
            UserRalationDeviceProxy = ServiceLocator.GetService<IUserRalationDeviceService>();
            VibSingalRTServicProxy = ServiceLocator.GetService<IVibSingalRTService>();
            VoltageSetMSiteAlmProxy = ServiceLocator.GetService<IVoltageSetMSiteAlmService>();
            VoltageWSMSiteData_1_Proxy = ServiceLocator.GetService<IVoltageWSMSiteData_1_Service>();
            VoltageWSMSiteData_2_Proxy = ServiceLocator.GetService<IVoltageWSMSiteData_2_Service>();
            VoltageWSMSiteData_3_Proxy = ServiceLocator.GetService<IVoltageWSMSiteData_3_Service>();
            VoltageWSMSiteData_4_Proxy = ServiceLocator.GetService<IVoltageWSMSiteData_4_Service>();
            WGProxy = ServiceLocator.GetService<IWGService>();
            WsnAlmRecordProxy = ServiceLocator.GetService<IWsnAlmrecordService>();
            RoleProxy = ServiceLocator.GetService<IRoleService>();
            TempeDeviceSetMsiteAlmProxy = ServiceLocator.GetService<ITempeDeviceSetMsiteAlmService>();

            VibratingSingalCharHisAccProxy = ServiceLocator.GetService<IVibratingSingalCharHisAccService>();
            VibratingSingalCharHisEnvlProxy = ServiceLocator.GetService<IVibratingSingalCharHisEnvlService>();

            FactoryProxy = ServiceLocator.GetService<IFactoryService>();

            VibratingSingalCharHisLQProxy = ServiceLocator.GetService<IVibratingSingalCharHisLQService>();

            VibratingSingalCharHisDispProxy = ServiceLocator.GetService<IVibratingSingalCharHisDispService>();

            ConfigProxy = ServiceLocator.GetService<IConfigService>();

            #endregion
        }

        #endregion

        #region 取得单例

        /// <summary>
        /// 取得单例
        /// </summary>
        /// <returns></returns>
        public static DBServices GetInstance()
        {
            if (m_instance == null)
            {
                lock (m_s_lock)
                {
                    if (m_instance == null)
                    {
                        m_instance = new DBServices();
                    }
                }
            }
            return m_instance;
        }

        #endregion
    }
}