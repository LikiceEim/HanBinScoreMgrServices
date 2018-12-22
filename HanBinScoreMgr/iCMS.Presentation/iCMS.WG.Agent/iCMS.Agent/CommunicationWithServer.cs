/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent
 *文件名：  CommunicationWithServer
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：iCMS.WG.Agent与iCMS.Server通讯主类
 * 修改记录：
    R1：
     修改作者：李峰
     修改时间：2016/5/30 13:30:00
     修改原因：①  增加上传启停机特征值至Server的处理函数；
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using iMesh;
using iCMS.WG.Agent.Common;
using iCMS.WG.Agent.Common.Enum;
using iCMS.WG.Agent.Model;
using iCMS.WG.Agent.Model.Receive;
using iCMS.Common.Component.Data.Request.WirelessSensors;

namespace iCMS.WG.Agent
{
    public class CommunicationWithServer
    {
        #region  Agent与上层iCMS.Server通讯

        #region 上传配置信息反馈信息
        public void UploadConfigResponse(string mac, Enum_ConfigType configType, int status, string reson)
        {

            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {

                    ConfigResponseParameter uploadConfigResponseParameter = new ConfigResponseParameter() 
                    {
                        WSMAC = mac.ToUpper(),
                        ConfigType = Convert.ToInt32(configType),
                        Status = status,
                        Reson = reson,
                        
                    };
                    ComFunction.CreateRequest(EnumRequestType.UpLoadData, "UploadConfigResponse", uploadConfigResponseParameter.ToClientString());
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithServer.UploadConfigResponse执行失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());

                }
            });

        }
        #endregion


        #region 上传WG状态
        public void UploadWGStatus(string WGID, iCMS.WG.Agent.Common.Enum.WGStatus status)
        {

            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    ComFunction.CreateRequest(EnumRequestType.UpLoadData, "UploadWGStatus",new WGStatusParameter() { WGID = WGID, Status = (int)status, Version = "" }.ToClientString());


                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithServer.UploadWGStatus执行失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());



                }
            });

        }
        #endregion

        #region 上传WG NetWorkID 
        public void UploadWGNetWorkIDStatus(string WGID, string NetWorkID)
        {

            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    ComFunction.CreateRequest(EnumRequestType.UpLoadData, "UploadWGStatus", new WGStatusParameter() { WGID = WGID, Status = (int)iCMS.WG.Agent.Common.Enum.WGStatus.WGOn, NetWorkID = NetWorkID, Version = ""}.ToClientString());
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithServer.UploadWGNetWorkIDStatus执行失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());



                }
            });

        }
        #endregion

        #region 上传WS状态
        public void UploadWSStatus(string mac, string Version, iCMS.WG.Agent.Common.Enum.EnumWSStatus status)
        {

            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {

                    if (status==iCMS.WG.Agent.Common.Enum.EnumWSStatus.WSOn)
                         iCMS.WG.Agent.ComFunction.UpdateConnectionWSStatu(mac.ToUpper(), "1");
                    else
                        iCMS.WG.Agent.ComFunction.UpdateConnectionWSStatu(mac.ToUpper(), "0");


                    ComFunction.CreateRequest(EnumRequestType.UpLoadData, "UploadWSStatus", 
                        new WSStatusParameter()
                        {
                            Status = (int)status,
                            Version = Version,
                            WSMAC = mac.ToUpper()                           
                        }.ToClientString());


                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithServer.UploadWSStatus执行失败，异常" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());



                }
            });
        }
        #endregion

        #region 上传波形数据
        public void UploadVibrationWave(WSWaveInfo _WSWaveInfo, int waveLength)
        {
            string cacheData = string.Empty;
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    if (iCMS.WG.Agent.ComFunction.IsExisted(iCMS.WG.Agent.ComFunction.CreateCacheData(_WSWaveInfo, (EnumCacheType)_WSWaveInfo.WaveType)))
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.InvalidData.ToString(), "重复波形数据，MAC: " + _WSWaveInfo.MAC + " 类型：" + GetWaveTypeString.GetString((int)_WSWaveInfo.WaveType) + "采集时间：" + _WSWaveInfo.WaveDescInfo.SamplingTime);
                        return;
                    }

                    VibrationWaveParamter uploadData = new VibrationWaveParamter();
                    uploadData.WSMAC = _WSWaveInfo.MAC.ToUpper();
                    uploadData.DAQStyle = _WSWaveInfo.WaveDescInfo.DAQStyle.ToString();
                    uploadData.SamplingTime = _WSWaveInfo.WaveDescInfo.SamplingTime;
                    uploadData.SignalType = (int)_WSWaveInfo.WaveType;
                    uploadData.WaveData = CalculateWaveData(_WSWaveInfo.WaveData, _WSWaveInfo.WaveDescInfo.AmplitueScaler, waveLength);
                    uploadData.TranceformCofe = _WSWaveInfo.WaveDescInfo.AmplitueScaler;
                    uploadData.WaveLength = waveLength / 2;
                    if ((int)_WSWaveInfo.WaveType == 4)
                    {
                        uploadData.EnlvpBandWidth = _WSWaveInfo.WaveDescInfo.UpperLimit;   //上限表示包络带宽
                        uploadData.EnlvpFilter = _WSWaveInfo.WaveDescInfo.LowerLimit;      //下限表示滤波器
                    }
                    else
                    {
                        uploadData.UpLimitFrequency = _WSWaveInfo.WaveDescInfo.UpperLimit;
                        uploadData.LowLimitFrequency = _WSWaveInfo.WaveDescInfo.LowerLimit;
                    }
                    uploadData.PeakValue = _WSWaveInfo.WaveDescInfo.PK;
                    uploadData.PeakPeakValue = _WSWaveInfo.WaveDescInfo.PPK;
                    uploadData.EffValue = _WSWaveInfo.WaveDescInfo.RMS;
                    uploadData.CarpetValue = _WSWaveInfo.WaveDescInfo.GPKC;
                   

                    cacheData =uploadData.ToClientString();
                    ComFunction.CreateRequest(EnumRequestType.UpLoadData, "UploadVibrationWave", cacheData);



                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithServer.UploadVibrationWave执行失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
                    if (iCMS.WG.Agent.ComFunction.GetAppConfig("isCache").Trim() == "1")
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Cache.ToString(), "UploadVibrationWave # \r\n" + cacheData);
                }
            });
        }
        #endregion

        #region 计算波形数据
        /// <summary>
        /// add by masu 2016年3月7日15:55:29 计算波形数据
        /// </summary>
        /// <param name="waveData"></param>
        /// <param name="tranceformCofe"></param>
        /// <returns></returns>
        public static List<float> CalculateWaveData(byte[] waveData, float tranceformCofe, int length)
        {
            float[] result = new float[length / 2];
            List<float> resultList = new List<float>();

            for (int i = 0; i < length; i += 2)
            {
                if (i + 1 == length)
                {
                    break;
                }                
                /*
                Int16[] nTemp = new Int16[1];
                Buffer.BlockCopy(waveData, i, nTemp, 0, 2);
                result[i / 2] = (nTemp[0] / tranceformCofe);
                 */
                Int16 Temp = (Int16)(waveData[i] << 8 | waveData[i + 1]);
                float Wavepoint = Temp / tranceformCofe;
                resultList.Add(Wavepoint);
            }
            return resultList;
        }
        #endregion

        #region 上传特征值
        public void UploadVibrationValue(tEigenValueParam param)
        {
            string cacheData = string.Empty;
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {

                    if (iCMS.WG.Agent.ComFunction.IsExisted(iCMS.WG.Agent.ComFunction.CreateCacheData(param, EnumCacheType.CharacterValue)))
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.InvalidData.ToString(), "重复特征值，MAC: " + param.mac.ToHexString() + "采集时间：" + iCMS.WG.Agent.ComFunction.GetSamplingTime(param.SampleTime));
                        return;
                    }

                    VibrationValueParameter vibrationValue = new VibrationValueParameter();
                    vibrationValue.SamplingTime = iCMS.WG.Agent.ComFunction.GetSamplingTime(param.SampleTime);
                    vibrationValue.DAQStyle = ((int)param.DaqMode).ToString();
                    vibrationValue.WSMAC = param.mac.ToHexString().ToUpper();


                    if ((int)param.AccWaveType == (int)enMeasDefType.eAccWaveform)
                    {
                        if (param.bAccRMSValid != 0)
                        {
                            vibrationValue.ACCEffValue = param.f32AccRMS;
                        }
                        else
                        {
                            vibrationValue.ACCEffValue = null;
                        }
                        if (param.bAccPKValid != 0)
                        {
                            vibrationValue.ACCPeakValue = param.f32AccPK;
                        }
                        else
                        {
                            vibrationValue.ACCPeakValue = null;
                        }
                        if (param.bAccPKPKValid != 0)
                        {
                            vibrationValue.ACCPeakPeakValue = param.f32AccPKPK;
                        }
                        else
                        {
                            vibrationValue.ACCPeakPeakValue = null;
                        }
                        vibrationValue.ACCSignalType = (int)enMeasDefType.eAccWaveform;
                    }
                    if ((int)param.VelWaveType == (int)enMeasDefType.eVelWaveform)
                    {
                        if (param.bVelRMSValid != 0)
                        {
                            vibrationValue.VelEffValue = param.f32VelRMS;
                        }
                        else
                        {
                            vibrationValue.VelEffValue = null;
                        }
                        if (param.bVelPKPKValid != 0)
                        {
                            vibrationValue.VelPeakPeakValue = param.f32VelPKPK;
                        }
                        else
                        {
                            vibrationValue.VelPeakPeakValue = null;
                        }
                        if (param.bVelPKValid != 0)
                        {
                            vibrationValue.VelPeakValue = param.f32VelPK;
                        }
                        else
                        {
                            vibrationValue.VelPeakValue = null;
                        }
                        vibrationValue.VelSignalType = (int)enMeasDefType.eVelWaveform;
                    }


                    if ((int)param.DspWaveType == (int)enMeasDefType.eDspWaveform)
                    {
                        if (param.bDspRMSValid != 0)
                        {
                            vibrationValue.DispEffValue = param.f32DspRMS;
                        }
                        else
                        {
                            vibrationValue.DispEffValue = null;
                        }
                        if (param.bDspPKPKValid != 0)
                        {
                            vibrationValue.DispPeakPeakValue = param.f32DspPKPK;
                        }
                        else
                        {
                            vibrationValue.DispPeakPeakValue = null;
                        }

                        if (param.bDspPKValid != 0)
                        {
                            vibrationValue.DispPeakValue = param.f32DspPK;
                        }
                        else
                        {
                            vibrationValue.DispPeakValue = null;
                        }


                        vibrationValue.DispSignalType = (int)enMeasDefType.eDspWaveform;
                    }


                    if ((int)param.AccEnvWaveType == (int)enMeasDefType.eAccEnvelope)
                    {
                        if (param.bAccEnvPKCValid != 0)
                        {
                            vibrationValue.EnvlCarpetValue = param.f32AccEnvPKC;
                        }
                        else
                        {
                            vibrationValue.EnvlCarpetValue = null;
                        }

                        if (param.bAccEnvPKValid != 0)
                        {
                            vibrationValue.EnvlPeakValue = param.f32AccEnvPK;
                        }
                        else
                        {
                            vibrationValue.EnvlPeakValue = null;
                        }
                        vibrationValue.EnvlSignalType = (int)enMeasDefType.eAccEnvelope;
                    }

                    cacheData = vibrationValue.ToClientString();
                    ComFunction.CreateRequest(EnumRequestType.UpLoadData, "UploadVibrationValue", cacheData);
                   
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithServer.UploadVibrationValue执行失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
                    if (iCMS.WG.Agent.ComFunction.GetAppConfig("isCache").Trim() == "1")
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Cache.ToString(), "UploadVibrationValue # \r\n" + cacheData);
                }
            });
        }
        #endregion

        #region 上传启停机特征值
        public void UploadStatusCritical(tRevStopParam param)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                string cacheData=string.Empty;
                try
                {

                    if (iCMS.WG.Agent.ComFunction.IsExisted(iCMS.WG.Agent.ComFunction.CreateCacheData(param, EnumCacheType.CriticalValue)))
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.InvalidData.ToString(), "重复启停机数据，MAC: " + param.mac.ToHexString() + "采集时间：" + iCMS.WG.Agent.ComFunction.GetSamplingTime(param.SampleTime));
                        return;
                    }

                    CriticalValueParameter statusOfCritical = new CriticalValueParameter();
                    statusOfCritical.DAQStyle = ((int)param.DaqMode).ToString();
                    statusOfCritical.WSMAC = param.mac.ToHexString();
                    statusOfCritical.SamplingTime = iCMS.WG.Agent.ComFunction.GetSamplingTime(param.SampleTime);
                    statusOfCritical.SignalType = (int)param.WaveType;
                    statusOfCritical.EigenType = (int)param.EigenType;
                    statusOfCritical.EigenValue = param.f32EigenPK;


                    cacheData = statusOfCritical.ToClientString();
                    ComFunction.CreateRequest(EnumRequestType.UpLoadData, "UploadStatusCritical", cacheData);




                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithServer.UploadStatusCritical执行失败，异常" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());

                    if (iCMS.WG.Agent.ComFunction.GetAppConfig("isCache").Trim() == "1")
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Cache.ToString(), "UploadStatusCritical # \r\n" + cacheData);

                }
            });
        }
        #endregion

        #region 上传温度、电压
        public void UploadTempAndVol(tTmpVoltageParam _WSTemperature)
        {
            string cacheData = string.Empty;
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    if (iCMS.WG.Agent.ComFunction.IsExisted(iCMS.WG.Agent.ComFunction.CreateCacheData(_WSTemperature, EnumCacheType.TmpVoltage)))
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.InvalidData.ToString(), "重复温度电压，MAC: " + _WSTemperature.mac.ToHexString() + "采集时间：" + iCMS.WG.Agent.ComFunction.GetSamplingTime(_WSTemperature.SampleTime));
                        return;
                    }



                    VolAndTempParameter volAndTemp  = new VolAndTempParameter()
                       {
                           SamplingTime = iCMS.WG.Agent.ComFunction.GetSamplingTime(_WSTemperature.SampleTime),
                           Temperature = _WSTemperature.f32Temperature,
                           Volatage = _WSTemperature.f32Voltage,
                           WSMAC = _WSTemperature.mac.ToHexString().ToUpper()
                       };

                    cacheData = volAndTemp.ToClientString();
                    ComFunction.CreateRequest(EnumRequestType.UpLoadData, "UploadVolAndTemp", cacheData);

                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithServer.UploadTempAndVol执行失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
                    if (iCMS.WG.Agent.ComFunction.GetAppConfig("isCache").Trim() == "1")
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Cache.ToString(), "UploadTempAndVol # \r\n" + cacheData);

                }
            });
        }
        #endregion

        #region 上传LQ
        public void UploadLQValue(tLQParam param)
        {
            string cacheData = string.Empty;
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    if (iCMS.WG.Agent.ComFunction.IsExisted(iCMS.WG.Agent.ComFunction.CreateCacheData(param, EnumCacheType.LQValue)))
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.InvalidData.ToString(), "重复LQ，MAC: " + param.mac.ToHexString() + "采集时间：" + iCMS.WG.Agent.ComFunction.GetSamplingTime(param.SampleTime));
                        return;
                    }

                    VibrationValueParameter LQData = new VibrationValueParameter();
                    LQData.SamplingTime = iCMS.WG.Agent.ComFunction.GetSamplingTime(param.SampleTime);
                    LQData.DAQStyle = ((int)param.DaqMode).ToString();
                    LQData.WSMAC = param.mac.ToHexString().ToUpper();
                    LQData.LQSignalType = (int)param.WaveType;
                    LQData.LQValue = param.EigenValuePara;
                    
                    cacheData = LQData.ToClientString();

                    ComFunction.CreateRequest(EnumRequestType.UpLoadData, "UploadVibrationValue", cacheData);
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithServer.UploadLQValue执行失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
                    if (iCMS.WG.Agent.ComFunction.GetAppConfig("isCache").Trim() == "1")
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Cache.ToString(), "UploadLQValue # \r\n" + cacheData);

                }
            });
        }
        #endregion
        #endregion
    }
}
