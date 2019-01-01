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
        #region 保存和更新振动包络波形数据
        /// <summary>
        /// 保存和更新振动包络波形数据
        /// </summary>
        /// <param name="param">采集波形参数</param>
        /// <param name="site">测量位置信息</param>
        /// <param name="dev">设备信息</param>
        /// <param name="ws">WS信息</param>
        /// <param name="signalTypeName">振动信号类型名称</param>
        private void SaveAndUpdateSignalEnvlData(UploadWaveDataParameter param, MeasureSite site, Device dev, int signalTypeID)
        {
            int status = (int)EnumAlarmStatus.Normal;

            float? peakWarnValue = null;
            float? peakAlmValue = null;
            float? carpetWarnValue = null;
            float? carpetAlmValue = null;

            int peakStatus = 0;
            int carpetStatus = 0;

            DateTime peakSamplingDate = DateTime.MinValue;
            DateTime carpetSamplingDate = DateTime.MinValue;

            float hisDataValue = 0f;

            try
            {
                //根据信号类型ID获取振动信号信息
                VibSingal vibSignal = vibSignalRepository
                    .GetDatas<VibSingal>(p => p.SingalType == signalTypeID && p.MSiteID == site.MSiteID, false)
                    .FirstOrDefault();
                if (vibSignal == null)
                {
                    //此处可添加错误代码
                    return;
                }
                //判断振动波形数据是否重复
                bool isDataRepeat = vibratingSingalCharHisEnvlRepository
                    .GetDatas<VibratingSingalCharHisEnvl>(p => p.SingalID == vibSignal.SingalID
                         && p.SamplingDate == param.SamplingDate
                         && p.MonthDate == param.SamplingDate.Month, false)
                    .Any();
                if (isDataRepeat)
                {
                    //此处可添加错误代码
                    return;
                }

                //获取振动信号类型的警告和告警阈值
                List<SignalAlmSet> signalAlmList = signalAlmSetRepository
                    .GetDatas<SignalAlmSet>(p => p.SingalID == vibSignal.SingalID, false)
                    .ToList();
                //获取速度中的特征值类型信息
                List<EigenValueType> eigenValueTypeList = cacheDICT.GetInstance()
                    .GetCacheType<EigenValueType>(p => p.VibratingSignalTypeID == signalTypeID)
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

                                    case ConstObject.Carpet_Value:
                                        {
                                            GetSamplingDataStatus(signalAlm.WarnValue, signalAlm.AlmValue,
                                                param.CarpetValue, ref status, vibSignal.SingalID, signalAlm.ValueType);
                                            carpetWarnValue = signalAlm.WarnValue;
                                            carpetAlmValue = signalAlm.AlmValue;
                                            carpetStatus = status;
                                            carpetSamplingDate = param.SamplingDate;
                                            hisDataValue = param.CarpetValue ?? 0;
                                            break;
                                        }

                                    default:
                                        break;
                                }
                                //判断上传临时采集数据时，不判断告警信息和入库
                                if (/*param.DAQStyle != "2"*/true)
                                {
                                    if (ConstObject.AlarmsConfirmed && false)//此处添加持续报警判断条件
                                    {
                                        //RecordAlarmCount recordAlarmCout = CollectionsExtensions.recordAlmCountList
                                        //   .Where(p => p.SignalID == vibSignal.SingalID && p.EigenValueTypeID == signalAlm.ValueType).FirstOrDefault();
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

                                        //    UpdateVibratingSingalAlarmEnvl(signalAlm, dev, site, vibSignal, param.SamplingTime, hisDataValue);

                                        //    //vibSignal.SingalStatus = status;
                                        //}
                                    }
                                    else
                                    {
                                        if (signalAlm.Status != status || (signalAlm.Status == status && status != (int)EnumAlarmStatus.Normal))
                                        {
                                            //更新振动信号特征值阈值设置表
                                            signalAlm.Status = status;
                                            signalAlmSetRepository.Update(signalAlm);

                                            #region QXM ADDED 2017/01/19
                                            ModifyStatusBeyondSignal(signalAlm.SingalID);
                                            #endregion

                                            UpdateVibratingSingalAlarmEnvl(signalAlm, dev, site, vibSignal, param.SamplingDate, hisDataValue);

                                            //vibSignal.SingalStatus = status;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (FilterSamplingEnvl(ref param, peakAlmValue, carpetAlmValue))
                {
                    if (param.IsWave)
                    {
                        RecordWaveToTxt(vibSignal.SingalID, param);
                    }

                    WaveUpperLimitValues waveValuesObject =
                        cacheDICT.GetCacheType<WaveUpperLimitValues>(item => item.ID == vibSignal.EnlvpBandW)
                        .FirstOrDefault();
                    InsterVibratingSingalHisEnvl(param, site, dev, vibSignal, peakWarnValue,
                        peakAlmValue, carpetWarnValue, carpetAlmValue,
                        GetRealSamplingFrequency(waveValuesObject.WaveUpperLimitValue), param.IsWave);
                    SaveRealTimeCollectInfoWaveEnvl(param, dev, site, vibSignal, peakStatus,
                        carpetStatus, peakSamplingDate, carpetSamplingDate);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }
        #endregion

        #region 更新包络振动信号特征值报警状态
        /// <summary>
        /// 更新包络振动信号特征值报警状态
        /// </summary>
        /// <param name="signalAlmSet">振动信号特征值告警设置</param>
        /// <param name="dev">设备信息</param>
        /// <param name="site">测量位置信息</param>
        /// <param name="vibsingal">振动信号信息</param>
        /// <param name="samplingData">采集时间</param>
        private void UpdateVibratingSingalAlarmEnvl(SignalAlmSet signalAlmSet, Device dev, MeasureSite site,
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

        #region 包络历史表中添加数据
        /// <summary>
        /// 包络历史表中添加数据
        /// </summary>
        /// <param name="param">采集数据对象</param>
        /// <param name="site">测点信息</param>
        /// <param name="dev">设备信息</param>
        /// <param name="vibSignal">信号类型信息</param>
        /// <param name="peakWarnValue">峰值高报值</param>
        /// <param name="peakAlmValue">峰值高高报值</param>
        /// <param name="carpetWarnValue">地毯值高报值</param>
        /// <param name="carpetAlmValue">地毯值高高报值</param>
        private void InsterVibratingSingalHisEnvl(UploadWaveDataParameter param, MeasureSite site, Device dev, VibSingal vibSignal,
            float? peakWarnValue, float? peakAlmValue, float? carpetWarnValue, float? carpetAlmValue, float realSamplingFrequency, bool isSaveWave = true)
        {
            int status = GetSignalStatus(vibSignal.SingalID);
            VibratingSingalCharHisEnvl vibSingalHisAcc = new VibratingSingalCharHisEnvl()
            {
                SingalID = vibSignal.SingalID,
                MsiteID = site.MSiteID,
                DevID = dev.DevID,
                SamplingDate = param.SamplingDate,
                WaveDataPath = isSaveWave ? GetWaveTxtFileName(vibSignal.SingalID, param.SamplingDate) : string.Empty,
                TransformCofe = 1,
                SamplingPointData = param.WaveData == null ? 0 : param.WaveData.Length,
                AlmStatus = status,
                DAQStyle = 1,
                PeakValue = param.PeakValue,
                CarpetValue = param.CarpetValue,
                PeakWarnValue = peakWarnValue,
                PeakAlmValue = peakAlmValue,
                CarpetWarnValue = carpetWarnValue,
                CarpetAlmValue = carpetAlmValue,
                MonthDate = param.SamplingDate.Month,
                RealSamplingFrequency = realSamplingFrequency,
                Rotate = GetRotationFrequency(site, param.SamplingDate, 1),

                SamplingDataType = (int)EnumSamplingDataType.WiredSensor
            };
            //vibSignalRepository.Update(vibSignal);
            vibratingSingalCharHisEnvlRepository.AddNew(vibSingalHisAcc);
        }
        #endregion

        #region 保存包络实时数据汇总
        /// <summary>
        /// 保存包络实时数据汇总
        /// </summary>
        /// <param name="param">上传数据对象</param>
        /// <param name="dev">设备信息</param>
        /// <param name="ws">WS信息</param>
        /// <param name="site">测量位置信息</param>
        /// <param name="vibSingal">振动信号信息</param>
        /// <param name="status">测量位置状态</param>
        private void SaveRealTimeCollectInfoWaveEnvl(UploadWaveDataParameter param, Device dev, MeasureSite site,
            VibSingal vibSingal, int peakStatus, int carpetStatus,
            DateTime peakSamplingDate, DateTime carpetSamplingDate)
        {
            try
            {
                //重新获取测点信息
                MeasureSite newMeasureSite = measureSiteRepository.GetByKey(site.MSiteID);

                //获取实时数据信息
                RealTimeCollectInfo realTimeCollectInfo = realTimeCollectInfoRepository
                    .GetDatas<RealTimeCollectInfo>(p => p.MSID == site.MSiteID, false)
                    .FirstOrDefault();
                if (realTimeCollectInfo == null)
                {
                    realTimeCollectInfo = new RealTimeCollectInfo();
                    MeasureSiteType siteType = cacheDICT.GetInstance()
                        .GetCacheType<MeasureSiteType>(p => p.ID == site.MSiteName)
                        .FirstOrDefault();
                    realTimeCollectInfo.DevID = dev.DevID;
                    realTimeCollectInfo.MSID = site.MSiteID;
                    realTimeCollectInfo.MSName = siteType.Name;
                    realTimeCollectInfo.MSStatus = newMeasureSite.MSiteStatus;
                    realTimeCollectInfo.MSDesInfo = siteType.Describe;
                    realTimeCollectInfo.MSDataStatus = 1;
                    realTimeCollectInfo.MSEnvelSingalID = vibSingal.SingalID;
                    realTimeCollectInfo.MSEnvelopingPEAKValue = param.PeakValue;
                    realTimeCollectInfo.MSEnvelopingCarpetValue = param.CarpetValue;
                    realTimeCollectInfo.MSEnvelopingPEAKStatus = peakStatus;
                    realTimeCollectInfo.MSEnvelopingCarpetStatus = carpetStatus;
                    realTimeCollectInfo.MSEnvelopingPEAKTime = (peakSamplingDate == DateTime.MinValue ? DateTime.Now : peakSamplingDate);
                    realTimeCollectInfo.MSEnvelopingCarpetTime = (carpetSamplingDate == DateTime.MinValue ? DateTime.Now : carpetSamplingDate);

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
                    realTimeCollectInfo.MSDataStatus = 1;
                    realTimeCollectInfo.MSEnvelSingalID = vibSingal.SingalID;
                    realTimeCollectInfo.MSEnvelopingPEAKValue = param.PeakValue;
                    realTimeCollectInfo.MSEnvelopingCarpetValue = param.CarpetValue;
                    realTimeCollectInfo.MSEnvelopingPEAKStatus = peakStatus;
                    realTimeCollectInfo.MSEnvelopingCarpetStatus = carpetStatus;
                    realTimeCollectInfo.MSEnvelopingPEAKTime = (peakSamplingDate == DateTime.MinValue ? DateTime.Now : peakSamplingDate);
                    realTimeCollectInfo.MSEnvelopingCarpetTime = (carpetSamplingDate == DateTime.MinValue ? DateTime.Now : carpetSamplingDate);

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
                            var vibSignalType = (int)EnumMeasureSiteThresholdType.ENVEL;
                            var eigenValueType = (int)EnumEigenType.EffectivityValue;
                            var samplingDate = DateTime.Now;
                            switch (eigenValue.Code)
                            {
                                //峰值
                                case "EIGENVALUE_27_FZ":
                                    {
                                        eigenValueType = (int)EnumEigenType.PeakValue;
                                        samplingDate = peakSamplingDate;
                                    }
                                    break;
                                //地毯值
                                case "EIGENVALUE_28_DTZ":
                                    {
                                        eigenValueType = (int)EnumEigenType.CarpetValue;
                                        samplingDate = carpetSamplingDate;
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
        private bool FilterSamplingEnvl(ref UploadWaveDataParameter param, float? peakAlmValue, float? carpetAlmValue)
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

            if (carpetAlmValue == null)
            {
                param.CarpetValue = null;
            }
            else if (param.CarpetValue != null)
            {
                filter = true;
            }

            return filter;
        }
        #endregion
    }
}