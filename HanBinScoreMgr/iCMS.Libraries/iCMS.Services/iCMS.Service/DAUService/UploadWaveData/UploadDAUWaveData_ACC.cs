using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Data.Request.DAUAgent;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Service.Web.DAUService.DAUAlarmParameter;

namespace iCMS.Service.Web.DAUService
{
    public partial class DAUManager
    {
        #region 保存和更新振动加速度波形数据
        /// <summary>
        /// 保存和更新振动加速度波形数据
        /// </summary>
        /// <param name="param">采集波形参数</param>
        /// <param name="site">测量位置信息</param>
        /// <param name="dev">设备信息</param>
        /// <param name="ws">WS信息</param>
        /// <param name="signalTypeName">振动信号类型名称</param>
        private void SaveAndUpdateSignalAccData(UploadWaveDataParameter param, MeasureSite site, Device dev, int signalTypId)
        {
            float? peakWarnValue = null;
            float? peakAlmValue = null;
            float? peakPeakWarnValue = null;
            float? peakPeakAlmValue = null;
            float? effWarnValue = null;
            float? effAlmValue = null;

            int peakStatus = 0;
            int peakPeakStatus = 0;
            int effStatus = 0;
            DateTime peakSamplingDate = DateTime.MinValue;
            DateTime peakPeakSamplingDate = DateTime.MinValue;
            DateTime effSamplingDate = DateTime.MinValue;
            float hisDataValue = 0f;

            int status = (int)EnumAlarmStatus.Normal;
            try
            {
                //根据信号类型ID获取振动信号信息
                //找到Vibscan Pro对应的振动信号（测量定义）
                VibSingal vibSignal = vibSignalRepository
                    .GetDatas<VibSingal>(p => p.SingalType == signalTypId
                        && p.MSiteID == site.MSiteID && p.SingalType == signalTypId, false)
                    .FirstOrDefault();

                if (vibSignal == null)
                {
                    //此处可添加错误代码
                    return;
                }
                //判断振动波形数据是否重复
                bool isRepeatData = vibratingSingalCharHisAccRepository
                     .GetDatas<VibratingSingalCharHisAcc>(p => p.SingalID == vibSignal.SingalID
                         && p.SamplingDate == param.SamplingDate && p.MonthDate == param.SamplingDate.Month, false).Any();

                if (isRepeatData)
                {
                    //防止重复上传
                    return;
                }

                //获取振动信号特征值及报警值设定
                List<SignalAlmSet> signalAlmList = signalAlmSetRepository
                    .GetDatas<SignalAlmSet>(p => p.SingalID == vibSignal.SingalID, false)
                    .ToList();
                //获取加速度中的特征值类型信息
                List<EigenValueType> eigenValueTypeList = cacheDICT.GetInstance()
                    .GetCacheType<EigenValueType>(p => p.VibratingSignalTypeID == signalTypId)
                    .ToList();
                if (signalAlmList != null)
                {
                    foreach (SignalAlmSet signalAlm in signalAlmList)
                    {
                        foreach (EigenValueType eigenValueType in eigenValueTypeList)
                        {
                            if (signalAlm.ValueType == eigenValueType.ID)
                            {
                                switch (eigenValueType.Name)
                                {
                                    case ConstObject.Peak_Value:
                                        {
                                            GetSamplingDataStatus(signalAlm.WarnValue, signalAlm.AlmValue,
                                                param.PeakValue, ref status, vibSignal.SingalID, signalAlm.ValueType);
                                            peakWarnValue = signalAlm.WarnValue;
                                            peakAlmValue = signalAlm.AlmValue;
                                            peakStatus = status;
                                            peakSamplingDate = param.SamplingDate;
                                            hisDataValue = param.PeakValue ?? 0;
                                            break;
                                        }

                                    case ConstObject.Peak_Peak_Value:
                                        {
                                            GetSamplingDataStatus(signalAlm.WarnValue, signalAlm.AlmValue,
                                                param.PeakPeakValue, ref status, vibSignal.SingalID, signalAlm.ValueType);
                                            peakPeakWarnValue = signalAlm.WarnValue;
                                            peakPeakAlmValue = signalAlm.AlmValue;
                                            peakPeakStatus = status;
                                            peakPeakSamplingDate = param.SamplingDate;
                                            hisDataValue = param.PeakPeakValue ?? 0;
                                            break;
                                        }

                                    case ConstObject.Effectivity_Value:
                                        {
                                            GetSamplingDataStatus(signalAlm.WarnValue, signalAlm.AlmValue,
                                                param.EffectiveValue, ref status, vibSignal.SingalID, signalAlm.ValueType);
                                            effWarnValue = signalAlm.WarnValue;
                                            effAlmValue = signalAlm.AlmValue;
                                            effStatus = status;
                                            effSamplingDate = param.SamplingDate;
                                            hisDataValue = param.EffectiveValue ?? 0;
                                            break;
                                        }

                                    default:
                                        break;
                                }
                                //判断上传临时采集数据时，不判断告警信息和入库
                                if (/*param.DAQStyle != "2"*/ true)
                                {
                                    //Vibscan Pro不进行持续报警，直接进行报警判断
                                    if (ConstObject.AlarmsConfirmed && false) //此处添加持续报警判断条件
                                    {
                                        //RecordAlarmCount recordAlarmCout = CollectionsExtensions.recordAlmCountList
                                        //    .Where(p => p.SignalID == vibSignal.SingalID && p.EigenValueTypeID == signalAlm.ValueType)
                                        //    .FirstOrDefault();
                                        //if (recordAlarmCout == null)
                                        //{
                                        //    continue;
                                        //}
                                        //if (recordAlarmCout.WarnCount == 3 || recordAlarmCout.AlarmCount == 3 || status == (int)EnumAlarmStatus.Normal)
                                        //{
                                        //    //更新振动信号特征值阈值设置表
                                        //    signalAlm.Status = status;
                                        //    signalAlmSetRepository.Update(signalAlm);

                                        //    #region QXM ADDED 2017/01/19
                                        //    ModifyStatusBeyondSignal(signalAlm.SingalID);
                                        //    #endregion

                                        //    UpdateVibratingSingalAlarmAcc(signalAlm, dev, site, vibSignal, param.SamplingTime, hisDataValue);
                                        //    //vibSignal.SingalStatus = status;
                                        //}
                                    }
                                    else
                                    {
                                        //signalAlm.Status != status || (signalAlm.Status == status && status != (int)EnumAlarmStatus.Normal)
                                        if (!(signalAlm.Status == (int)EnumAlarmStatus.Normal && status == (int)EnumAlarmStatus.Normal))//如果不是两次都正常，则必然更新报警状态
                                        {
                                            //更新振动信号特征值阈值设置表
                                            signalAlm.Status = status;
                                            vibSignal.SingalStatus = status;
                                            signalAlmSetRepository.Update(signalAlm);

                                            #region QXM ADDED 2017/01/19
                                            ModifyStatusBeyondSignal(signalAlm.SingalID);
                                            #endregion

                                            UpdateVibratingSingalAlarmAcc(signalAlm, dev, site, vibSignal, param.SamplingDate, hisDataValue);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //只有上传的采集数据特征值与配置的一致才保证存储
                if (FilterSamplingAcc(ref param, peakAlmValue, peakPeakAlmValue, effAlmValue))
                {
                    if (param.IsWave)
                    {
                        RecordWaveToTxt(vibSignal.SingalID, param);
                    }

                    //采样频率
                    WaveUpperLimitValues waveValuesObject =
                        cacheDICT.GetCacheType<WaveUpperLimitValues>(item => item.ID == vibSignal.UpLimitFrequency)
                        .FirstOrDefault();
                    InsterVibratingSingalHisAcc(param, site, dev, vibSignal, peakWarnValue, peakAlmValue,
                        peakPeakWarnValue, peakPeakAlmValue, effWarnValue, effAlmValue,
                        GetRealSamplingFrequency(waveValuesObject.WaveUpperLimitValue), param.IsWave);
                    //保存实时数据
                    SaveRealTimeCollectInfoWaveAcc(param, dev, null, site, vibSignal, peakStatus,
                        peakPeakStatus, effStatus, peakSamplingDate, peakPeakSamplingDate, effSamplingDate);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }
        #endregion

        #region 更新加速度振动信号特征值报警状态
        /// <summary>
        /// 更新加速度振动信号特征值报警状态
        /// </summary>
        /// <param name="signalAlmSet">振动信号特征值告警设置</param>
        /// <param name="dev">设备信息</param>
        /// <param name="site">测量位置信息</param>
        /// <param name="vibsingal">振动信号信息</param>
        /// <param name="samplingData">采集时间</param>
        /// <param name="hisDataValue">采集信号值</param>
        private void UpdateVibratingSingalAlarmAcc(SignalAlmSet signalAlmSet, Device dev, MeasureSite site,
            VibSingal vibsingal, DateTime samplingTime, float? hisDataValue)
        {
            DeviceVibtationAlarmParameter devVibtationAlarmParam = new DeviceVibtationAlarmParameter()
            {
                AlmSet = signalAlmSet,
                Dev = dev,
                MSite = site,
                Signal = vibsingal,
                SamplingTime = samplingTime,
                HisDataValue = hisDataValue
            };

            deviceVibrationAlarm.DevVibAlmRecordManager(devVibtationAlarmParam);
        }
        #endregion

        #region 加速度历史表中添加数据
        /// <summary>
        /// 加速度历史表中添加数据
        /// </summary>
        /// <param name="param">采集数据对象</param>
        /// <param name="site">测点信息</param>
        /// <param name="dev">设备信息</param>
        /// <param name="vibSignal">信号类型信息</param>
        /// <param name="peakWarnValue">峰值高报值</param>
        /// <param name="peakAlmValue">峰值高高报值</param>
        /// <param name="peakPeakWarnValue">峰峰值高报值</param>
        /// <param name="peakPeakAlmValue">峰峰值高高报值</param>
        /// <param name="effWarnValue">有效值高报值</param>
        /// <param name="effAlmValue">有效值高高报值</param>
        private void InsterVibratingSingalHisAcc(UploadWaveDataParameter param, MeasureSite site, Device dev, VibSingal vibSignal,
            float? peakWarnValue, float? peakAlmValue, float? peakPeakWarnValue, float? peakPeakAlmValue,
            float? effWarnValue, float? effAlmValue, float realSamplingFrequency, bool isSaveWaveData = true)
        {
            int status = GetSignalStatus(vibSignal.SingalID);
            VibratingSingalCharHisAcc vibSingalHisAcc = new VibratingSingalCharHisAcc()
            {
                SingalID = vibSignal.SingalID,
                MsiteID = site.MSiteID,
                DevID = dev.DevID,
                SamplingDate = param.SamplingDate,
                WaveDataPath = isSaveWaveData ? GetWaveTxtFileName(vibSignal.SingalID, param.SamplingDate) : string.Empty,
                TransformCofe = 1,
                SamplingPointData = param.WaveData == null ? 0 : param.WaveData.Length,
                AlmStatus = status,
                DAQStyle = 1,
                PeakValue = param.PeakValue,
                PeakPeakValue = param.PeakPeakValue,
                EffValue = param.EffectiveValue,
                PeakWarnValue = peakWarnValue,
                PeakAlmValue = peakAlmValue,
                PeakPeakWarnValue = peakPeakWarnValue,
                PeakPeakAlmValue = peakPeakAlmValue,
                EffWarnValue = effWarnValue,
                EffAlmValue = effAlmValue,
                MonthDate = param.SamplingDate.Month,
                RealSamplingFrequency = realSamplingFrequency,
                Rotate = GetRotationFrequency(site, param.SamplingDate, 1),

                SamplingDataType = (int)EnumSamplingDataType.WiredSensor
            };
            vibSignal.SingalStatus = status;
            //vibSignalRepository.Update(vibSignal);
            vibratingSingalCharHisAccRepository.AddNew(vibSingalHisAcc);
        }
        #endregion

        #region 保存加速度实时数据汇总
        /// <summary>
        /// 保存加速度实时数据汇总
        /// </summary>
        /// <param name="param">上传数据对象</param>
        /// <param name="dev">设备信息</param>
        /// <param name="ws">WS信息</param>
        /// <param name="site">测量位置信息</param>
        /// <param name="vibSingal">振动信号信息</param>
        /// <param name="status">测量位置状态</param>
        private void SaveRealTimeCollectInfoWaveAcc(UploadWaveDataParameter param, Device dev, WS ws, MeasureSite site,
            VibSingal vibSingal, int peakStatus, int peakPeakStatus, int effStatus,
            DateTime peakSamplingDate, DateTime peakPeakSamplingDate, DateTime effSamplingDate)
        {
            try
            {
                //重新获取测点信息
                MeasureSite newMeasureSite = measureSiteRepository.GetByKey(site.MSiteID);

                //获取实时数据信息
                RealTimeCollectInfo realTimeCollectInfo = realTimeCollectInfoRepository
                    .GetDatas<RealTimeCollectInfo>(p => p.MSID == newMeasureSite.MSiteID, false)
                    .FirstOrDefault();
                if (realTimeCollectInfo == null)
                {
                    realTimeCollectInfo = new RealTimeCollectInfo();
                    MeasureSiteType siteType = cacheDICT.GetInstance()
                        .GetCacheType<MeasureSiteType>(p => p.ID == newMeasureSite.MSiteName)
                        .FirstOrDefault();
                    realTimeCollectInfo.DevID = dev.DevID;
                    realTimeCollectInfo.MSID = newMeasureSite.MSiteID;
                    realTimeCollectInfo.MSName = siteType.Name;
                    realTimeCollectInfo.MSStatus = newMeasureSite.MSiteStatus;
                    realTimeCollectInfo.MSDesInfo = siteType.Describe;
                    //realTimeCollectInfo.MSDataStatus = ws.LinkStatus;
                    realTimeCollectInfo.MSACCSingalID = vibSingal.SingalID;
                    realTimeCollectInfo.MSACCVirtualValue = param.EffectiveValue;
                    realTimeCollectInfo.MSACCPeakValue = param.PeakValue;
                    realTimeCollectInfo.MSACCPeakPeakValue = param.PeakPeakValue;
                    realTimeCollectInfo.MSACCVirtualStatus = effStatus;
                    realTimeCollectInfo.MSACCPeakStatus = peakStatus;
                    realTimeCollectInfo.MSACCPeakPeakStatus = peakPeakStatus;
                    realTimeCollectInfo.MSACCPeakTime = (peakSamplingDate == DateTime.MinValue ? DateTime.Now : peakSamplingDate);
                    realTimeCollectInfo.MSACCPeakPeakTime = (peakPeakSamplingDate == DateTime.MinValue ? DateTime.Now : peakPeakSamplingDate);
                    realTimeCollectInfo.MSACCVirtualTime = (effSamplingDate == DateTime.MinValue ? DateTime.Now : effSamplingDate);

                    #region 添加网关连接状态 ADDED BY QXM, 2016/11/18
                    //var wg = gatewayRepository.GetByKey(ws.WGID);
                    //if (wg != null)
                    //{
                    //    realTimeCollectInfo.MSWGLinkStatus = wg.LinkStatus;
                    //}
                    #endregion

                    realTimeCollectInfoRepository.AddNew(realTimeCollectInfo);
                }
                else
                {
                    realTimeCollectInfo.MSStatus = newMeasureSite.MSiteStatus;
                    //realTimeCollectInfo.MSDataStatus = ws.LinkStatus;

                    realTimeCollectInfo.MSACCSingalID = vibSingal.SingalID;
                    realTimeCollectInfo.MSACCVirtualValue = param.EffectiveValue;
                    realTimeCollectInfo.MSACCPeakValue = param.PeakValue;
                    realTimeCollectInfo.MSACCPeakPeakValue = param.PeakPeakValue;
                    realTimeCollectInfo.MSACCVirtualStatus = effStatus;
                    realTimeCollectInfo.MSACCPeakStatus = peakStatus;
                    realTimeCollectInfo.MSACCPeakPeakStatus = peakPeakStatus;
                    realTimeCollectInfo.MSACCPeakTime = (peakSamplingDate == DateTime.MinValue ? DateTime.Now : peakSamplingDate);
                    realTimeCollectInfo.MSACCPeakPeakTime = (peakPeakSamplingDate == DateTime.MinValue ? DateTime.Now : peakPeakSamplingDate);
                    realTimeCollectInfo.MSACCVirtualTime = (effSamplingDate == DateTime.MinValue ? DateTime.Now : effSamplingDate);
                    realTimeCollectInfoRepository.Update(realTimeCollectInfo);

                    #region 添加报警阈值在时实表中
                    List<EigenValueType> eigenValueTypeList = cacheDICT.GetInstance()
                                     .GetCacheType<EigenValueType>()
                                     .ToList();
                    //获取阈值
                    var alarmThresholdList = signalAlmSetRepository.GetDatas<SignalAlmSet>(item => item.SingalID == vibSingal.SingalID, true);
                    if (alarmThresholdList != null && alarmThresholdList.Any())
                    {
                        //阈值
                        foreach (var alarmThreshold in alarmThresholdList)
                        {
                            var eigenValue = eigenValueTypeList.Where(item => item.ID == alarmThreshold.ValueType).FirstOrDefault();
                            var vibSignalType = (int)EnumMeasureSiteThresholdType.ACC;
                            var eigenValueType = (int)EnumEigenType.EffectivityValue;
                            var samplingDate = DateTime.Now;
                            switch (eigenValue.Code)
                            {
                                //有效值
                                case "EIGENVALUE_20_YXZ":
                                    {
                                        eigenValueType = (int)EnumEigenType.EffectivityValue;
                                        samplingDate = effSamplingDate;
                                    }
                                    break;
                                //峰值
                                case "EIGENVALUE_18_FZ":
                                    {
                                        eigenValueType = (int)EnumEigenType.PeakValue;
                                        samplingDate = peakSamplingDate;
                                    }
                                    break;
                                //峰峰值
                                case "EIGENVALUE_19_FFZ":
                                    {
                                        eigenValueType = (int)EnumEigenType.PeakPeakValue;
                                        samplingDate = peakPeakSamplingDate;
                                    }
                                    break;
                            }

                            var realTimeAlarmThreshold = realTimeAlarmThresholdRepository.GetDatas<RealTimeAlarmThreshold>(item =>
                            item.MeasureSiteID == site.MSiteID &&
                            item.MeasureSiteThresholdType == vibSignalType &&
                            item.EigenValueType == eigenValueType, true).FirstOrDefault();

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
                                realTimeAlarmThreshold.MeasureSiteID = site.MSiteID;
                                realTimeAlarmThreshold.MeasureSiteThresholdType = vibSignalType;
                                realTimeAlarmThreshold.AddDate = DateTime.Now;
                                realTimeAlarmThreshold.AlarmThresholdValue = alarmThreshold.WarnValue;
                                realTimeAlarmThreshold.DangerThresholdValue = alarmThreshold.AlmValue;
                                realTimeAlarmThreshold.EigenValueType = eigenValueType;
                                realTimeAlarmThreshold.SamplingDate = samplingDate;
                                realTimeAlarmThresholdRepository.AddNew(realTimeAlarmThreshold);
                            }
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

        #region 过滤上传数据与配置采集定义是否一致
        /// <summary>
        /// 过滤上传数据与配置采集定义是否一致  Add By LF
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private bool FilterSamplingAcc(ref UploadWaveDataParameter param, float? peakAlmValue, float? peakPeakAlmValue, float? effectiveAlmValue)
        {
            bool filter = false;
            if (peakAlmValue == null)
            {
                param.PeakValue = null;
            }
            else if (param.PeakValue != null)
            {
                filter = true;
            }

            if (peakPeakAlmValue == null)
            {
                param.PeakPeakValue = null;
            }
            else if (param.PeakPeakValue != null)
            {
                filter = true;
            }

            if (effectiveAlmValue == null)
            {
                param.EffectiveValue = null;
            }
            else if (param.EffectiveValue != null)
            {
                filter = true;
            }
            return filter;
        }
        #endregion
    }
}