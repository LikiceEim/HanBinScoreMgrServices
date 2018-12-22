using iCMS.Cloud.JiaXun.Commons;
using iCMS.Cloud.JiaXun.DataConvert;
using iCMS.Cloud.JiaXun.Interface;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Tool.Extensions;
using iCMS.Frameworks.Core.DB;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Frameworks.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace iCMS.Setup.CloudPushManually.Function
{
    public class CloudDataProvider
    {
        GetYunyiInterfaceData getdata = null;
        private IRepository<MonitorTree> monitorTreeRepository = null;
        private IRepository<MonitorTreeProperty> monitorTreePropertyRepository = null;
        private IRepository<MeasureSite> measureSiteRepository = null;
        private IRepository<Device> deviceRepository = null;
        private IRepository<MonitorTreeType> monitorTreeTypeRepository = null;
        private IRepository<WS> wsRepository = null;
        private IRepository<MeasureSiteType> measureSiteTypeRepository = null;
        private IRepository<VibSingal> vibSignalRepository = null;
        private IRepository<WaveLowerLimitValues> waveLowerLimitValueRepository = null;
        private IRepository<WaveUpperLimitValues> waveUpperLimitValueRepository = null;
        private IRepository<WaveLengthValues> waveLengthValueRepository = null;
        private IRepository<VoltageSetMSiteAlm> voltageSetMSiteAlmRepository = null;
        private IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository = null;
        private IRepository<SignalAlmSet> signalAlmSetRepository = null;
        private IRepository<EigenValueType> eigenValueTypeRepository = null;
        private IRepository<VibratingSingalType> vibratingSingalTypeRepository = null;
        private IRepository<DevAlmRecord> devAlmRecordRepository = null;
        private IRepository<WSnAlmRecord> wsnAlmRecordRepository = null;
        private IRepository<Gateway> wgRepository = null;

        public CloudDataProvider()
        {
            getdata = new GetYunyiInterfaceData();
            monitorTreeRepository = new Repository<MonitorTree>();
            monitorTreePropertyRepository = new Repository<MonitorTreeProperty>();
            measureSiteRepository = new Repository<MeasureSite>();
            deviceRepository = new Repository<Device>();
            monitorTreeTypeRepository = new Repository<MonitorTreeType>();
            wsRepository = new Repository<WS>();
            measureSiteTypeRepository = new Repository<MeasureSiteType>();
            vibSignalRepository = new Repository<VibSingal>();
            waveLowerLimitValueRepository = new Repository<WaveLowerLimitValues>();
            waveUpperLimitValueRepository = new Repository<WaveUpperLimitValues>();
            waveLengthValueRepository = new Repository<WaveLengthValues>();
            voltageSetMSiteAlmRepository = new Repository<VoltageSetMSiteAlm>();
            tempeDeviceSetMSiteAlmRepository = new Repository<TempeDeviceSetMSiteAlm>();
            signalAlmSetRepository = new Repository<SignalAlmSet>();
            eigenValueTypeRepository = new Repository<EigenValueType>();
            vibratingSingalTypeRepository = new Repository<VibratingSingalType>();
            devAlmRecordRepository = new Repository<DevAlmRecord>();
            wsnAlmRecordRepository = new Repository<WSnAlmRecord>();
            wgRepository = new Repository<Gateway>();
        }

        public string GetEnterprise(int id)
        {
            try
            {
                MonitorTree monitorTree = monitorTreeRepository.GetByKey(id);
                if (monitorTree != null)
                {
                    MonitorTreeProperty enterprop = monitorTreePropertyRepository.GetByKey(monitorTree.MonitorTreePropertyID);
                    getdata = new GetYunyiInterfaceData();
                    return Json.Stringify(getdata.GetEnterprise(monitorTree, enterprop));
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                return string.Empty;
            }
        }

        public string GetWorkshop(int id)
        {
            string result = string.Empty;

            MonitorTree monitorTree = monitorTreeRepository.GetByKey(id);
            if (monitorTree != null)
            {
                MonitorTreeProperty enterprop = monitorTreePropertyRepository.GetByKey(monitorTree.MonitorTreePropertyID);
                result = Json.Stringify(getdata.GetWorkShop(monitorTree, enterprop));
            }
            return result;
        }

        public string GetDevice(int id)
        {
            string result = string.Empty;
            var monitors = monitorTreeRepository.GetDatas<MonitorTree>(t => true, false).ToList();
            List<MonitorTreeType> mtTypes = monitorTreeTypeRepository.GetDatas<MonitorTreeType>(t => true, true).ToList();
            Device device = deviceRepository.GetByKey(id);

            if (device != null)
            {
                GetYunyiInterfaceData getData = new GetYunyiInterfaceData();
                int siteCount = measureSiteRepository.GetDatas<MeasureSite>(t => t.DevID == device.DevID, true).Count();
                result = Json.Stringify(getData.GetDevice(device, monitors, siteCount, mtTypes, ""));
            }

            return result;
        }

        public string GetMeasureSite(int id)
        {
            string result = string.Empty;
            List<MonitorTree> monitors = monitorTreeRepository.GetDatas<MonitorTree>(t => true, true).ToList();
            List<MonitorTreeType> mtTypes = monitorTreeTypeRepository.GetDatas<MonitorTreeType>(t => true, true).ToList();

            MeasureSite msite = measureSiteRepository.GetByKey(id);
            if (msite != null)
            {
                int serialNo = 0;
                string msName = string.Empty;
                var msType = measureSiteTypeRepository.GetByKey(msite.MSiteName);
                if (msType != null)
                {
                    msName = msType.Name;
                    Int32.TryParse(msType.Describe, out serialNo);
                }

                var device = deviceRepository.GetByKey(msite.DevID);
                if (device != null)
                {
                    result = Json.Stringify(getdata.GetMeasuresite(msite, monitors, device, msName, serialNo, mtTypes));
                }
            }

            return result;
        }

        public string GetWS(int id)
        {
            string result = string.Empty;
            WS ws = wsRepository.GetByKey(id);
            if (ws != null)
            {
                Sensor sensor = GetSensor(ws);
                if (sensor != null)
                {
                    result = Json.Stringify(sensor);
                }
            }

            return result;
        }

        private Sensor GetSensor(WS ws)
        {
            Sensor sensor = null;
            List<MonitorTree> monitors = monitorTreeRepository.GetDatas<MonitorTree>(t => true, true).ToList();
            List<MonitorTreeType> mtTypes = monitorTreeTypeRepository.GetDatas<MonitorTreeType>(t => true, true).ToList();
            MeasureSite site = measureSiteRepository.GetDatas<MeasureSite>(t => t.WSID == ws.WSID, false).FirstOrDefault();
            if (site != null)
            {
                var msType = measureSiteTypeRepository.GetByKey(site.MSiteName);
                if (msType != null)
                {
                    string siteName = msType.Name;
                    Device device = deviceRepository.GetDatas<Device>(t => t.DevID == site.DevID, false).FirstOrDefault();
                    if (device != null)
                    {
                        sensor = getdata.GetSensor(ws, site, device, monitors, siteName, mtTypes);
                    }
                }
            }
            return sensor;
        }

        public string GetVibSignal(int id)
        {
            var signal = vibSignalRepository.GetByKey(id);
            if (signal != null)
            {
                return Json.Stringify(GetCloudSignal(signal));
            }

            return string.Empty;
        }

        private CloudSignal GetCloudSignal(VibSingal signal)
        {
            CloudSignal cSignal = null;
            List<MonitorTree> monitors = monitorTreeRepository.GetDatas<MonitorTree>(t => true, true).ToList();
            MeasureSite site = measureSiteRepository.GetByKey(signal.MSiteID);
            List<MonitorTreeType> mtTypes = monitorTreeTypeRepository.GetDatas<MonitorTreeType>(t => true, true).ToList();
            if (site != null)
            {
                Device device = deviceRepository.GetByKey(site.DevID);
                if (device != null)
                {

                    //将波长上下限，带宽，滤波等信息转换为实际的值
                    GetVibSignalPara(signal);

                    cSignal = getdata.GetSignal(signal, site, device, monitors, mtTypes);
                }
            }
            return cSignal;
        }

        private void GetVibSignalPara(VibSingal signal)
        {
            int value = -1;
            if (signal.LowLimitFrequency.HasValue)
            {
                var lowLimitType = waveLowerLimitValueRepository.GetByKey(signal.LowLimitFrequency.Value);
                if (lowLimitType != null)
                {
                    value = lowLimitType.WaveLowerLimitValue;
                }
                signal.LowLimitFrequency = value;
            }

            if (signal.UpLimitFrequency.HasValue)
            {
                var upperLimitType = waveUpperLimitValueRepository.GetByKey(signal.UpLimitFrequency.Value);
                if (upperLimitType != null)
                {
                    value = upperLimitType.WaveUpperLimitValue;
                }
                signal.UpLimitFrequency = value;
            }

            if (signal.EnlvpBandW.HasValue)
            {
                var enlvBandType = waveUpperLimitValueRepository.GetByKey(signal.EnlvpBandW.Value);
                if (enlvBandType != null)
                {
                    value = enlvBandType.WaveUpperLimitValue;
                }
                signal.EnlvpBandW = value;
            }

            if (signal.EnlvpFilter.HasValue)
            {
                var envlFilterType = waveLowerLimitValueRepository.GetByKey(signal.EnlvpFilter.Value);
                if (envlFilterType != null)
                {
                    value = envlFilterType.WaveLowerLimitValue;
                }
                signal.EnlvpFilter = value;
            }

            var waveLenType = waveLengthValueRepository.GetByKey(signal.WaveDataLength);
            if (waveLenType != null)
            {
                value = waveLenType.WaveLengthValue;
            }
            signal.WaveDataLength = value;
        }

        public string GetVolateAlmSet(int id)
        {
            string result = string.Empty;
            VoltageSetMSiteAlm voltAlmset = voltageSetMSiteAlmRepository.GetByKey(id);
            if (voltAlmset != null)
            {
                AlarmThreshold alm = GetVoltAlmSet(voltAlmset);
                if (alm != null)
                {
                    result = Json.Stringify(alm);
                }
            }
            return result;
        }

        private AlarmThreshold GetVoltAlmSet(VoltageSetMSiteAlm voltAlmSet)
        {
            AlarmThreshold alarm = null;
            MeasureSite site = measureSiteRepository.GetByKey(voltAlmSet.MsiteID);

            if (site != null)
            {
                YunyiCloudDataType type = YunyiCloudDataType.Sensor;
                string valueType = CommonVariate.WS_VOLTAGE_SET;
                int objectid = (int)site.WSID;
                alarm = getdata.GetMeasureSiteAlmSet(voltAlmSet, type, objectid, valueType);
            }
            return alarm;
        }

        public string GetDeviceTempeAlmSet(int id)
        {
            string result = string.Empty;
            TempeDeviceSetMSiteAlm tempeAlmset = tempeDeviceSetMSiteAlmRepository.GetByKey(id);
            if (tempeAlmset != null)
            {
                AlarmThreshold alm = GetTempeAlmSet(tempeAlmset);
                if (alm != null)
                {
                    result = Json.Stringify(alm);
                }
            }

            return result;
        }

        private AlarmThreshold GetTempeAlmSet(TempeDeviceSetMSiteAlm almset)
        {
            AlarmThreshold alarm = null;
            GetYunyiInterfaceData getData = new GetYunyiInterfaceData();

            MeasureSite site = measureSiteRepository.GetByKey(almset.MsiteID);
            if (site != null)
            {
                YunyiCloudDataType type = YunyiCloudDataType.MeasureSite;
                string valueType = CommonVariate.DEVICE_TEMPE_SET;
                int objectid = almset.MsiteID;
                type = YunyiCloudDataType.MeasureSite;

                alarm = getData.GetMeasureSiteAlmSet(almset, type, objectid, valueType);
            }
            return alarm;
        }

        public string GetVibSignalAlmSet(int id)
        {
            string result = string.Empty;
            SignalAlmSet almset = signalAlmSetRepository.GetByKey(id);
            if (almset != null)
            {
                AlarmThreshold alarm = GetVibrationAlarmThreshold(almset);
                if (alarm != null)
                {
                    result = Json.Stringify(alarm);
                }
            }
            return result;
        }

        private AlarmThreshold GetVibrationAlarmThreshold(SignalAlmSet almSet)
        {
            AlarmThreshold alarm = null;
            GetYunyiInterfaceData getData = new GetYunyiInterfaceData();
            EigenValueType type = eigenValueTypeRepository.GetByKey(almSet.ValueType);
            if (type != null)
            {
                alarm = getData.GetVibrationAlmSet(almSet, type.Name);
            }

            return alarm;
        }

        public string GetDevAlmRecord(int id)
        {
            string result = string.Empty;

            DevAlmRecord almRecord = devAlmRecordRepository.GetByKey(id);
            if (almRecord != null)
            {
                Alarm alarm = GetDevAlmRecord(almRecord);
                if(alarm!=null)
                {
                    result = Json.Stringify(alarm);
                }          
            }

            return result;
        }

        private Alarm GetDevAlmRecord(DevAlmRecord record)
        {
            Alarm alarm = null;
            MeasureSite site = measureSiteRepository.GetByKey(record.MSiteID);
            Device device = deviceRepository.GetByKey(record.DevID);

            switch (record.MSAlmID)
            {
                case 1:
                    //振动信号报警                
                    VibSingal signal = vibSignalRepository.GetByKey(record.SingalID);
                    //过滤掉位移,LQ振动信号的报警记录
                    List<string> selectedVibsignalNames = new List<string> { "加速度", "速度", "包络" };
                    //取得佳讯需要的振动信号类型ID
                    List<int> selectedTypeIDList = vibratingSingalTypeRepository.GetDatas<VibratingSingalType>
                        (t => selectedVibsignalNames.Contains(t.Name), true).Select(t => t.ID).ToList();
                    if (signal != null && signal.DAQStyle == 1 && selectedTypeIDList.Contains(signal.SingalType))
                    {
                        if (site != null && device != null)
                        {
                            alarm = getdata.GetVibrateionAlarm(record, signal, site, device);
                        }
                    }
                    break;
                case 2:
                    //设备温度
                    if (site != null && device != null)
                    {
                        alarm = getdata.GetDevTempAlarm(record, site, device);
                    }
                    break;
                case 3:
                    break;
                case 4:
                    break;

                case 5:
                    break;
                case 6:
                    break;
                case 8:
                    //趋势报警推送
                    //趋势报警，慧萌新增报警类型，报警内容格式暂时不确定，暂定和之前一致
                    //如果是临时测量定义 或者 LQ 不推送
                    //VibSingal signalForThrend = DBServices.GetInstance().VibSingalProxy.GetByKey(record.SingalID);
                    //过滤掉位移,LQ振动信号的报警记录

                    //List<string> selectedVibsignalNamesForThrend = new List<string> { "加速度", "速度", "包络" };
                    //取得佳讯需要的振动信号类型ID
                    //List<int> selectedTypeIDListForThrend = DBServices.GetInstance().VibratingSingalTypeProxy.GetDatas<VibratingSingalType>
                    //    (t => selectedVibsignalNamesForThrend.Contains(t.Name), true).Select(t => t.ID).ToList();

                    //if (signalForThrend != null && signalForThrend.DAQStyle == 1 && selectedTypeIDListForThrend.Contains(signalForThrend.SingalType))
                    //{
                    //if (site != null && device != null)
                    // {
                    //    alarm = getData.GetThrendAlarm(record, signalForThrend, site, device);
                    //}
                    //  }
                    break;
            }
            return alarm;
        }

        public string GetWsnAlmRecord(int id)
        {
            string result = string.Empty;
            WSnAlmRecord record = wsnAlmRecordRepository.GetByKey(id);
            Alarm alarm = GetWsnAlarmRecord(record);

            if(alarm!=null)
            {
                result = Json.Stringify(alarm);
            }
            
            return result;
        }

        private Alarm GetWsnAlarmRecord(WSnAlmRecord record)
        {
            Alarm alarm = null;
            GetYunyiInterfaceData getData = new GetYunyiInterfaceData();
            MeasureSite site = measureSiteRepository.GetByKey(record.MSiteID);
            Device device = deviceRepository.GetByKey(record.DevID);
            if (site == null || device == null)
            {
                return alarm;
            }

            switch (record.MSAlmID)
            {
                //传感器温度  不推送
                case 3:
                    break;
                //传感器电池电压
                case 4:
                    alarm = getData.GetWSAlarm(record, site, device);
                    break;
                //传感器连接
                case 5:
                    alarm = getData.GetWSAlarm(record, site, device);
                    break;
                //6 网关连接
                case 6:
                    alarm = getData.GetWGAlarm(record);
                    break;
                default:
                    break;
            }
            return alarm;
        }

        public string GetWG(int id)
        {
            string result = string.Empty;

            Gateway wg = wgRepository.GetByKey(id);
            if (wg != null)
            {
              var  monitors = monitorTreeRepository.GetDatas<MonitorTree>(p => true, false).ToList();
                result = Json.Stringify(getdata.GetGateway(wg, monitors));
            }

            return result;
        }
    }
}
