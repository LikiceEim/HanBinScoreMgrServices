using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    public partial class MeshAdapter
    {
        /// <summary>
        /// 缓存网络中目前在线的WS，是否正在上传WS特征值。
        /// </summary>
        private volatile Dictionary<string, bool> m_dicReceiveEignWs = new Dictionary<string, bool>();
        /// <summary>
        /// 采集间隔
        /// </summary>
        /// <param name="wsMac"></param>
        private class tDaqTimeSlot
        {
            // 定时温度采集周期时&分
            public tTimingTime TmpDaqPeriod = new tTimingTime();
            // 特征值采集周期（该值为温度采集周期的整倍数值）
            public UInt16 u16EigenDaqMult = 1;
            // 波形采集周期（该值为特征值采集周期的整倍数值）
            public UInt16 u16WaveDaqMult = 1;
        }
        /// <summary>
        /// 网络中WS的采集间隔
        /// </summary>
        private ConcurrentDictionary<string, tDaqTimeSlot> NetworkWSDaqSlot = new ConcurrentDictionary<string, tDaqTimeSlot>();      
        /// <summary>
        /// 特征值上传(是否有除了LQ及启停机外的其他特征值)
        /// </summary>
        //private bool bEigentoDaq = false;
        /// <summary>
        /// 网络中WS的版本号及地址
        /// </summary>
        private ConcurrentDictionary<string, tVer> NetworkWSInfo = new ConcurrentDictionary<string, tVer>();
        #region Get结果的再次解析
        /// <summary>
        /// mesh的获取自描述报告的处理
        /// </summary>       
        private void GetSelfReportAnalysis(tGetSelfReportResult result)
        {
            if (!NetworkWSInfo.ContainsKey(result.mac.ToHexString()))
            {
                tVer GetVer = new tVer();
                GetVer.u8Main  = result.verMcuFw.u8Main;
                GetVer.u8Sub   = result.verMcuFw.u8Sub;
                GetVer.u8Rev   = result.verMcuFw.u8Rev;
                GetVer.u8Build = result.verMcuFw.u8Build;
                NetworkWSInfo[result.mac.ToHexString()] = GetVer;
            }
            else
            {
                NetworkWSInfo[result.mac.ToHexString()].u8Main  = result.verMcuFw.u8Main;
                NetworkWSInfo[result.mac.ToHexString()].u8Sub   = result.verMcuFw.u8Sub;
                NetworkWSInfo[result.mac.ToHexString()].u8Rev   = result.verMcuFw.u8Rev;
                NetworkWSInfo[result.mac.ToHexString()].u8Build = result.verMcuFw.u8Build;
            }
 
        }
        
        #endregion

        #region Mesh侧的Notify再次解析适配1.4
        /// <summary>
        /// mesh的自描述报告的处理
        /// </summary>       
        private void SelfReportAnalysis(tMeshSelfReportParam param)
        {
            tVer NewVer = new tVer();
            NewVer.u8Main = param.verMcuFw.u8Main;
            NewVer.u8Sub = param.verMcuFw.u8Sub;
            NewVer.u8Rev = param.verMcuFw.u8Rev;
            NewVer.u8Build = param.verMcuFw.u8Build;
            NetworkWSInfo[param.mac.ToHexString()] = NewVer;            
            tMeshSelfReportResult date = new tMeshSelfReportResult();
            date.mac.Assign(param.mac);
            date.u8RC = 0;
            if (param.u8RC == 0)
            {
                //ReplyWSDescribe(date);
            }
            CommStackLog.RecordInf(enLogLayer.eAdapter, "WS(" + param.mac.ToHexString() + "):V" + NetworkWSInfo[param.mac.ToHexString()].u8Main.ToString() + "."
                                                                                                    + NetworkWSInfo[param.mac.ToHexString()].u8Sub.ToString() + "."
                                                                                                    + NetworkWSInfo[param.mac.ToHexString()].u8Rev.ToString() + "."
                                                                                                    + NetworkWSInfo[param.mac.ToHexString()].u8Build.ToString());            
        }      
        /// <summary>
        /// 自描述报告的响应
        /// </summary> 
        private bool ReplyWSDescribe(tMeshSelfReportResult resault, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[resault.Len];
                resault.Serialize(appStream);
                if (SendData(resault.mac, appStream, urgent, enPktPriority.High) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyDescribe mac(" + resault.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyDescribe mac(" + resault.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        /// <summary>
        /// 通信层的健康报告再处理
        /// </summary> 
        private void HealthReportAnalysis(tHealthReportParam param)
        {
            //拆分出温度电压的报告
            tTmpVoltageParam AppTV = new tTmpVoltageParam();
            AppTV.mac.Assign(param.mac);
            AppTV.f32Temperature     = param.u32FacilityTmp;
            AppTV.f32Voltage         = param.u32BatState;
            AppTV.SampleTime.u8Year  = param.TmpTime.u8Year;
            AppTV.SampleTime.u8Month = param.TmpTime.u8Month;
            AppTV.SampleTime.u8Day   = param.TmpTime.u8Day;
            AppTV.SampleTime.u8Hour  = param.TmpTime.u8Hour;
            AppTV.SampleTime.u8Min   = param.TmpTime.u8Min;
            AppTV.SampleTime.u8Sec   = param.TmpTime.u8Sec;           
            if (WsTmpVoltageDataNotify != null)
                WsTmpVoltageDataNotify(AppTV);
            //拆分出自报告
            tSelfReportParam AppSR = new tSelfReportParam();                      
            AppSR.State = (enWsRunState)param.u8State;
            AppSR.mac.Assign(param.mac);
            if (NetworkWSInfo.ContainsKey(param.mac.ToHexString()))
            {
                AppSR.Version.u8Main  = NetworkWSInfo[param.mac.ToHexString()].u8Main;
                AppSR.Version.u8Sub   = NetworkWSInfo[param.mac.ToHexString()].u8Sub;
                AppSR.Version.u8Rev   = NetworkWSInfo[param.mac.ToHexString()].u8Rev;
                AppSR.Version.u8Build = NetworkWSInfo[param.mac.ToHexString()].u8Build;
                
            }
            else
            {
                //没有版本号时，需要自己获取
                tGetSelfReportParam getSelf = new tGetSelfReportParam();
                getSelf.mac.Assign(param.mac);
                GetSelfReport(getSelf);
            }
            AppSR.u16ErrCode      = param.u16ErrorCode;
            AppSR.f32Temperature  = param.u32DeviceTmp;
            AppSR.f32Voltage      = param.u32BatState;
            AppSR.WakeupMode      = (enWsWkupMode)param.u8WakeupMode;
            AppSR.MoteBoot        = (enMoteBoot)param.u8MoteBoot;
            if (!NetworkWSDaqSlot.ContainsKey(param.mac.ToHexString()))
            {
                tGetMeasDefParam getDef = new tGetMeasDefParam();
                getDef.mac.Assign(param.mac);
                GetMeasDef(getDef);
            }
            else
            {
                AppSR.u8TempDaqPeriod = (byte)(NetworkWSDaqSlot[param.mac.ToHexString()].TmpDaqPeriod.u8Hour * 60 + NetworkWSDaqSlot[param.mac.ToHexString()].TmpDaqPeriod.u8Min);
                AppSR.u16CharCnt      = NetworkWSDaqSlot[param.mac.ToHexString()].u16EigenDaqMult;
                AppSR.u16WaveCnt      = NetworkWSDaqSlot[param.mac.ToHexString()].u16WaveDaqMult;
            }
            if (NetworkWSInfo.ContainsKey(param.mac.ToHexString()))
            {
                if (WsSelfReportNotify != null)
                    WsSelfReportNotify(AppSR);
            }
         
        }     
        /// <summary>
        /// 通信层的波形描述信息的再处理
        /// </summary>
        private void WsWaveDescAnalysis(tMeshWaveDescParam param)
        {                        
            //解析波形描述
            tWaveDescParam WaveDes  = new tWaveDescParam();
            WaveDes.mac.Assign(param.mac);
            WaveDes.DaqTime.u8Year           = param.DaqTime.u8Year;
            WaveDes.DaqTime.u8Month          = param.DaqTime.u8Month;
            WaveDes.DaqTime.u8Day            = param.DaqTime.u8Day;
            WaveDes.DaqTime.u8Hour           = param.DaqTime.u8Hour;
            WaveDes.DaqTime.u8Min            = param.DaqTime.u8Min;
            WaveDes.DaqTime.u8Sec            = param.DaqTime.u8Sec;

            if (param.DaqMode == enMeshWsDaqMode.eImmediate)
            {
                WaveDes.DaqMode = enWsDaqMode.eImmediate;
            }
            else
            {
                WaveDes.DaqMode = enWsDaqMode.eTiming;
            }
            WaveDes.MeasDef.MeasDefType      = (enMeasDefType)param.WaveType;          
            WaveDes.MeasDef.u16WaveLen       = param.u16WaveLen;
            WaveDes.f32AmpScaler             = param.f32AmpScaler;
            WaveDes.u16CurrentFrameID        = 0;
            WaveDes.u16TotalFramesNum        = param.u16TotalFramesNum;            
            WaveDes.MeasDef.u8EigenValueType = param.u8EigenValueType;
            WaveDes.MeasDef.u16LowFreq       = param.u16DwFreqOrAevFt;
            WaveDes.MeasDef.u16UpperFreq     = param.u16UpFreqOrAevBw;
            WaveDes.MeasDef.bSubscribed      = true;
            if(WaveDes.MeasDef.MeasDefType == enMeasDefType.eAccWaveform)
            {
                WaveDes.MeasDef.bWaveRMSValid = (param.AccDef.bAccWaveRMSValid == true) ? 1 : 0;
                WaveDes.f32RMS                = param.AccDef.fAccRMSValue;                  
                WaveDes.MeasDef.bWavePKValid  = (param.AccDef.bAccWavePKValid == true) ? 1 : 0;
                WaveDes.f32PK                 = param.AccDef.fAccPKValue;            
            }
            if (WaveDes.MeasDef.MeasDefType == enMeasDefType.eVelWaveform)
            {
                WaveDes.MeasDef.bWaveRMSValid = (param.VelDef.bVelWaveRMSValid == true)?1:0;
                WaveDes.f32RMS                = param.VelDef.fVelRMSValue;                  
            }
            if (WaveDes.MeasDef.MeasDefType == enMeasDefType.eDspWaveform)
            {
                WaveDes.MeasDef.bWavePKPKValid = (param.DspDef.bDspWavePKPKValid == true)?1:0;
                WaveDes.f32PKPK                = param.DspDef.fDspPKPKValue;
            }
            if (WaveDes.MeasDef.MeasDefType == enMeasDefType.eAccEnvelope)
            {              
                WaveDes.MeasDef.bWavePKValid   = (param.AccEnvDef.bAccEnvWavePKValid == true)?1:0;
                WaveDes.f32PK                  = param.AccEnvDef.fAccEnvPKValue;              
                WaveDes.MeasDef.bWavePKCValid  = (param.AccEnvDef.bAccEnvWavePKCValid == true)?1:0;
                WaveDes.f32PKC                 = param.AccEnvDef.fAccEnvPKCValue;              
            }
            if (WsWaveDescNotify != null)
                WsWaveDescNotify(WaveDes);           
        }
        /// <summary>
        /// 通信层的波形数据的再处理
        /// </summary>

        private void WsWaveDataAnalysis(tWaveDataParam param)
        {
            param.u16CurrentFrameID += 1;
            if (WsWaveDataNotify != null)
                WsWaveDataNotify(param);
        }
        
        /// <summary>
        /// 通信层的特征值的再次解析
        /// </summary>
        private void WsEigenDataAnalysis(tMeshEigenValueParam param)
        {                       
            lock (m_dicReceiveEignWs)
            {
                if (!m_dicReceiveEignWs.ContainsKey(param.mac.ToHexString()))
                {
                    m_dicReceiveEignWs.Add(param.mac.ToHexString(),false);
                }
                if (param.AccDef != null || param.VelDef != null || param.DspDef != null || param.AccEnvDef != null)
                {
                    m_dicReceiveEignWs[param.mac.ToHexString()] = true;
                }
                else
                {
                    m_dicReceiveEignWs[param.mac.ToHexString()] = false;
                }
            }
            if (param.AccDef != null || param.VelDef != null || param.DspDef != null || param.AccEnvDef != null)
            {
                tEigenValueParam Eigen = new tEigenValueParam();
                Eigen.mac.Assign(param.mac);
                Eigen.DaqMode = enWsDaqMode.eTiming;
                Eigen.SampleTime.u8Year = param.SampleTime.u8Year;
                Eigen.SampleTime.u8Month = param.SampleTime.u8Month;
                Eigen.SampleTime.u8Day = param.SampleTime.u8Day;
                Eigen.SampleTime.u8Hour = param.SampleTime.u8Hour;
                Eigen.SampleTime.u8Min = param.SampleTime.u8Min;
                Eigen.SampleTime.u8Sec = param.SampleTime.u8Sec;

                Eigen.AccWaveType = enMeasDefType.eAccWaveform;
                if (param.AccDef != null)
                {
                    Eigen.bAccRMSValid = (param.AccDef.bAccWaveRMSValid == true) ? 1 : 0;
                    Eigen.f32AccRMS = param.AccDef.fAccRMSValue;
                    Eigen.bAccPKValid = (param.AccDef.bAccWavePKValid == true) ? 1 : 0;
                    Eigen.f32AccPK = param.AccDef.fAccPKValue;
                }
                Eigen.VelWaveType = enMeasDefType.eVelWaveform;
                if (param.VelDef != null)
                {
                    Eigen.bVelRMSValid = (param.VelDef.bVelWaveRMSValid == true) ? 1 : 0;
                    Eigen.f32VelRMS = param.VelDef.fVelRMSValue;
                }
                Eigen.DspWaveType = enMeasDefType.eDspWaveform;
                if (param.DspDef != null)
                {
                    Eigen.bDspPKPKValid = (param.DspDef.bDspWavePKPKValid == true) ? 1 : 0;
                    Eigen.f32DspPKPK = param.DspDef.fDspPKPKValue;
                }
                Eigen.AccEnvWaveType = enMeasDefType.eAccEnvelope;
                if (param.AccEnvDef != null)
                {
                    Eigen.bAccEnvPKValid = (param.AccEnvDef.bAccEnvWavePKValid == true) ? 1 : 0;
                    Eigen.f32AccEnvPK = param.AccEnvDef.fAccEnvPKValue;
                    Eigen.bAccEnvPKCValid = (param.AccEnvDef.bAccEnvWavePKCValid == true) ? 1 : 0;
                    Eigen.f32AccEnvPKC = param.AccEnvDef.fAccEnvPKCValue;
                }
                    //bEigentoDaq = true;
                if (WsEigenDataNotify != null)
                    WsEigenDataNotify(Eigen);
            }
            //解析LQ的特征值
            if (param.LQDef != null)
            {
                tLQParam LQ = new tLQParam();
                LQ.mac.Assign(param.mac);
                LQ.DaqMode            = enWsDaqMode.eTiming;
                LQ.SampleTime.u8Year  = param.SampleTime.u8Year;
                LQ.SampleTime.u8Month = param.SampleTime.u8Month;
                LQ.SampleTime.u8Day   = param.SampleTime.u8Day;
                LQ.SampleTime.u8Hour  = param.SampleTime.u8Hour;
                LQ.SampleTime.u8Min   = param.SampleTime.u8Min;
                LQ.SampleTime.u8Sec   = param.SampleTime.u8Sec;
                LQ.EigenValuePara     = param.LQDef.fLQRMSValue;
                LQ.WaveType           = enMeasDefType.eLQform;
                LQ.EigenType          = enEigenValueType.eLQ;
                if (WsLQNotify != null)
                    WsLQNotify(LQ);
            }
            //解析启停机的特征值
            if (param.RevStopDef != null)
            {              
                tRevStopParam RevStop = new tRevStopParam();
                RevStop.mac.Assign(param.mac);
                RevStop.DaqMode            = enWsDaqMode.eTiming;
                RevStop.SampleTime.u8Year  = param.SampleTime.u8Year;
                RevStop.SampleTime.u8Month = param.SampleTime.u8Month;
                RevStop.SampleTime.u8Day   = param.SampleTime.u8Day;
                RevStop.SampleTime.u8Hour  = param.SampleTime.u8Hour;
                RevStop.SampleTime.u8Min   = param.SampleTime.u8Min;
                RevStop.SampleTime.u8Sec   = param.SampleTime.u8Sec;
                RevStop.f32EigenPK         = param.RevStopDef.fRevStopPKValue;
                RevStop.WaveType           = enMeasDefType.eAppRevStopform;
                RevStop.EigenType          = enEigenValueType.ePK;               
                if (WsRevStopNotify != null)
                    WsRevStopNotify(RevStop);
            }
        }
        #endregion 

        #region 设置命令的结果处理适配1.4
        /// <summary>
        /// 设置测量定义的结果
        /// </summary>
        private void SetMeasDefReaultAnalysis(tMeshSetMeasDefResult result)
        {
            tSetMeasDefResult Def = new tSetMeasDefResult();
            Def.mac.Assign(result.mac);
            Def.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                if (SetMeasDefReaultNotify != null)
                    SetMeasDefReaultNotify(Def);
            }
            else
            {
                if (setMeasDefFailed != null)
                    setMeasDefFailed(result.mac);
            }
        }
        /// <summary>
        /// 设置网络netid的结果
        /// </summary>
        private void SetNetworkIDReaultAnalysis(tMeshSetWsNwIdResult result)
        {
            tSetNetworkIdResult NwId = new tSetNetworkIdResult();
            NwId.mac.Assign(result.mac);
            NwId.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                if (SetWsNwIDReaultNotify != null)
                    SetWsNwIDReaultNotify(NwId);
            }
            else
            {
                if (setNetworkIDFailed != null)
                    setNetworkIDFailed(NwId.mac);
            }
        }
        /// <summary>
        /// 设置WSsn的结果
        /// </summary>
        private void SetWsSnReaultAnalysis(tMeshSetWsSnResult result)
        {
            tSetWsSnResult WsSn = new tSetWsSnResult();
            WsSn.mac.Assign(WsSn.mac);
            WsSn.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                if (SetWsSnReaultNotify != null)
                    SetWsSnReaultNotify(WsSn);
            }
            else
            {
                if (setWsSnFailed != null)
                    setWsSnFailed(WsSn.mac);
            }
        }
        /// <summary>
        /// 设置校准命令的结果
        /// </summary>
        private void SetCaliWsSensorReaultAnalysis(tMeshCaliSensorResult result)
        {
            tCaliSensorResult Cail = new tCaliSensorResult();
            Cail.mac.Assign(result.mac);
            Cail.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                if (CaliWsSensorReaultNotify != null)
                    CaliWsSensorReaultNotify(Cail);
            }
            else
            {
                if (caliWsSensorFailed != null)
                    caliWsSensorFailed(Cail.mac);
            }
        }
        ///  <summary>
        /// 设置AD截止电压的结果
        ///  <summary>
        private void SetADCloseVoltReaultAnalysis(tMeshSetADCloseVoltResult result)
        {
            tSetADCloseVoltResult Volt = new tSetADCloseVoltResult();
            Volt.mac.Assign(result.mac);
            Volt.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                if (SetADCloseVoltReaultNotify != null)
                    SetADCloseVoltReaultNotify(Volt);
            }
            else
            {
                if (setADCloseVoltFailed != null)
                    setADCloseVoltFailed(Volt.mac);
            }
        }
        ///  <summary>
        /// 设置启停机的结果
        ///  <summary>
        private void SetWsStartOrStopReaultAnalysis(tMeshSetWsStartOrStopResult result)
        {
            tSetWsStartOrStopResult RevStop = new tSetWsStartOrStopResult();
            RevStop.mac.Assign(result.mac);
            RevStop.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                if (SetWsStartOrStopReaultNotify != null)
                    SetWsStartOrStopReaultNotify(RevStop);
            }
            else
            {
                if (setWsStartOrStopFailed != null)
                    setWsStartOrStopFailed(RevStop.mac);
            }
        }
        ///  <summary>
        /// 设置触发式的结果
        ///  <summary>
        private void SetTrigParamResultAnalysis(tMeshSetTrigParamResult result)
        {
            tSetTrigParamResult Trig = new tSetTrigParamResult();
            Trig.mac.Assign(result.mac);
            Trig.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                if (SetTrigParamResultNotify != null)
                    SetTrigParamResultNotify(Trig);
            }
            else
            {
                if (setTrigParamFailed != null)
                    setTrigParamFailed(Trig.mac);
            }
        }
        ///  <summary>
        /// 设置WS路由模式的结果
        ///  <summary>
        private void SetWsRouteModeResultAnalysis(tMeshSetWsRouteModeResult result)
        {
            tSetWsRouteModeResult Route = new tSetWsRouteModeResult();
            Route.mac.Assign(result.mac);
            Route.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                if (SetWsRouteModeResultNotify != null)
                    SetWsRouteModeResultNotify(Route);
            }
            else
            {
                if (setWsRouteModeFailed != null)
                    setWsRouteModeFailed(Route.mac);
            }
        }
        #endregion

        #region Mesh侧恢复及重启命令的结果
        /// <summary>
        /// 恢复WS的出厂设置的结果
        /// </summary>
        private void RestoreWSresultAnalysis(tMeshRestoreWSResult result)
        {
            tRestoreWSResult Restore = new tRestoreWSResult();
            Restore.mac.Assign(result.mac);
            Restore.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "RestoreWSResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                if (RestoreWSReaultNotify != null)
                    RestoreWSReaultNotify(Restore);
            }
            else
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "RestoreWSResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                if (restoreWSFailed != null)
                    restoreWSFailed(Restore.mac);
            }
        }
        /// <summary>
        /// 重启WS的结果
        /// </summary>
        private void ResetWSresultAnalysis(tMeshResetWSResult result)
        {
            tResetWSResult Reset = new tResetWSResult();
            Reset.mac.Assign(result.mac);
            Reset.u16RC = result.u8RC;
            if (result.u8RC == 0)
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "ResetWSResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                if (ResetWSReaultNotify != null)
                    ResetWSReaultNotify(Reset);
            }
            else
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "ResetWSResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                if (resetWSFailed != null)
                    resetWSFailed(result.mac);
            }
        }
        #endregion
    }
}
