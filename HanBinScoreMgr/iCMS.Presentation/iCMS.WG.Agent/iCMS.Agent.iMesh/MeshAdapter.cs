using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    public partial class MeshAdapter : IAppServerRequest, IAppServerRequestReply, IAppWsRequest, IAppServerResponse, IAppServerRequestFail
    {
        /// <summary>
        /// [常量]应用层功能码偏移位
        /// </summary>
        private const byte APP_CMD_OFFS_IN_NOTF_DATA = 4;     
        /// <summary>
        /// 同步对象
        /// </summary>
        private object objectLock = new Object();
        /// <summary>
        /// 表示Manager压力缓解的信号量
        /// </summary>
        //private AutoResetEvent m_asigEased = new AutoResetEvent(false);

        /// <summary>
        /// 检查上位机侧是否正在等待此命令的WS响应
        /// </summary>
        /// <param name="rspMac">响应WS的地址</param>
        /// <param name="rspMainCMD">响应主功能码</param>
        /// <param name="rspSubCMD">响应子功能码</param>
        private void checkWsResponse(tMAC rspMac, Byte rspMainCMD, Byte rspSubCMD)
        {
            try
            {
                lock (m_dicWaitAppRespCmds)
                {
                    int cnt = m_dicWaitAppRespCmds.Count;
                    byte Main_Cmd = 0;
                    byte Sub_Cmd  = 0;
                    
                    for (int i = 0; i < cnt; i++)
                    {
                        UserRequestElement elem = m_dicWaitAppRespCmds.Keys.ElementAt(i);
                        if (elem.cmd != enCmd.CMDID_SENDDATA)
                            continue;
                        tSENDDATA sendData = (tSENDDATA)elem.param;
                        Main_Cmd = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] >> 5);
                        
                        Sub_Cmd  = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] & 0x1F);
                        
                        if (!rspMac.isEqual(sendData.mac))
                            continue;
                        else if (rspMainCMD != Main_Cmd)
                            continue;
                        else if (rspSubCMD != Sub_Cmd)
                            continue;
                        else
                        {
                            m_dicWaitAppRespCmds[elem] = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
            }
        }

        /// <summary>
        /// 1.5新版WS中未对网络时间进行北京时间对准
        /// 故，在WS上传的所有带有时间戳的报文进行时间补偿
        /// </summary>
        private void compensateTime(ref tDateTime origTime, UInt64 compTime)
        {
            UInt64 absSecs = (UInt64)(origTime.u8Year  << 40 | 
                                      origTime.u8Month << 32 | 
                                      origTime.u8Day   << 24 | 
                                      origTime.u8Hour  << 16 | 
                                      origTime.u8Min   << 8  | 
                                      origTime.u8Sec);

            absSecs += compTime;
            DateTime dt = UtcBaseTime.AddSeconds(absSecs);
            origTime.u8Year  = (byte)(dt.Year - 2000);
            origTime.u8Month = (byte)dt.Month;
            origTime.u8Day   = (byte)dt.Day;
            origTime.u8Hour  = (byte)dt.Hour;
            origTime.u8Min   = (byte)dt.Minute;
            origTime.u8Sec   = (byte)dt.Second;
        }

        /// <summary>
        /// 顶层对网络通知的处理函数
        /// </summary>
        /// <param name="notType">通知类型</param>
        /// <param name="eventType">通知事件类型定义</param>
        /// <param name="notObj">通知信息基类</param>
        internal void notifyHandler(enNotifyType notType, enEventType eventType, NotifBase notObj/*, byte payloadlen*/)
        {
            byte main_Cmd = 0;
            byte sub_Cmd = 0;
            /*payloadlen = (byte)(payloadlen - NOTIFY_HEAD_LEN);*/
            switch (notType)
            {
                #region NOTIFEVENT
                case enNotifyType.NOTIFID_NOTIFEVENT:
                {
                    tMAC theWs = new tMAC();
                    #region EVENTMOTELOST
                    if (eventType == enEventType.EVENTID_EVENTMOTELOST)
                    {
                        EventMoteLost tmpEvent = (EventMoteLost)notObj;
                        theWs.Assign(tmpEvent.Mac);
                        lock (m_dicOnLineWs)
                        {
                            if (m_dicOnLineWs.ContainsKey(tmpEvent.Mac.ToHexString()))
                                m_dicOnLineWs[tmpEvent.Mac.ToHexString()] = false;
                            else
                                m_dicOnLineWs.Add(tmpEvent.Mac.ToHexString(), false);
                        }

                        if (wsLost != null)
                            wsLost(theWs);
                        //正在升级的ws掉线的判断                      
                        if (curUpdatingWs != null&&curUpdatingWs.isEqual(theWs))
                        {
                            m_bExitUpdat = true;
                            asigFwDatSent.Set();
                        }                        
                    }
                    #endregion
                    #region EVENTNETWORKRESET
                    else if (eventType == enEventType.EVENTID_EVENTNETWORKRESET)
                    {
                        if (managerReset != null)
                        {
                            managerReset();
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "Notify App NetworkReset");
                        }

                        if (!m_bManualNetworkResetEvent)
                            m_asigAutoNwResetEvt.Set();
                        m_bManualNetworkResetEvent = false;
                        
                        // 获取当前网络时间
                        GetTime(true);
                    }
                    #endregion
                    #region EVENTMOTEOPERATIONAL
                    else if (eventType == enEventType.EVENTID_EVENTMOTEOPERATIONAL)
                    {
                        EventMoteOperational tmpEvent = (EventMoteOperational)notObj;
                        theWs.Assign(tmpEvent.Mac);
                        /*
                        lock (m_dicOnLineWs)
                        {
                            if (string.Compare(m_macManager, tmpEvent.Mac.ToHexString()) == 0)
                                return;
                            else if (m_dicOnLineWs.ContainsKey(tmpEvent.Mac.ToHexString()))
                                m_dicOnLineWs[tmpEvent.Mac.ToHexString()] = true;
                            else
                                m_dicOnLineWs.Add(tmpEvent.Mac.ToHexString(), true);
                        }
                        */

                        if (wsConnected != null)
                            wsConnected(theWs);
                    }
                    #endregion
                    break;
                }
                #endregion
                #region NOTIFDATA
                case enNotifyType.NOTIFID_NOTIFDATA:
                {
                    NotifData notData = (NotifData)notObj;
                    SENDDATA_SRCPORT_DEF = notData.u16SrcPort;
                    SENDDATA_DSTPORT_DEF = notData.u16DstPort;
                    main_Cmd = (byte)(notData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] >> 5);
                    sub_Cmd = (byte)(notData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] & 0x1F);
                    
                    switch ((enAppMainCMD)(main_Cmd))
                    {
                        #region eNotify
                        case enAppMainCMD.eNotify:
                        {
                            switch ((enAppNotifySubCMD)(sub_Cmd))
                            {
                                #region eSelfReport
                                case enAppNotifySubCMD.eSelfReport:
                                {
                                    tMeshSelfReportParam param = new tMeshSelfReportParam();
                                    param.Unserialize(notData.u8aData);
                                    param.mac.Assign(notData.Mac);
                                    lock (m_dicOnLineWs)
                                    {
                                        if (string.Compare(m_macManager, param.mac.ToHexString()) == 0)
                                            return;
                                        else if (m_dicOnLineWs.ContainsKey(param.mac.ToHexString()))
                                            m_dicOnLineWs[param.mac.ToHexString()] = true;
                                        else
                                            m_dicOnLineWs.Add(param.mac.ToHexString(), true);
                                    }
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SR of ws(" + param.mac.ToHexString() + ")|FwVer(" +
                                        param.verMcuFw.u8Main + "." + param.verMcuFw.u8Sub + "." + 
                                        param.verMcuFw.u8Rev + "." + param.verMcuFw.u8Build + ")|BlVer(" +
                                        param.verMcuBl.u8Main + "." + param.verMcuBl.u8Sub + "." +
                                        param.verMcuBl.u8Rev + "." + param.verMcuBl.u8Build + ")|StkVer(" +
                                        param.verMoteStack.u8Main + "." + param.verMoteStack.u8Sub + "." +
                                        param.verMoteStack.u8Rev + "." + param.verMoteStack.u8Build + ")|NetID(" + param.u16NetWorkID+")");
                                    if (GetNetworkId() == param.u16NetWorkID)
                                    {
                                        SelfReportAnalysis(param);
                                    }
                                    else
                                    {
                                        ResetMote(param.mac);
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "WS (" + param.mac.ToHexString() + ")JoinErr!" +
                                                                                    "WS_NetID = " + param.u16NetWorkID.ToString() +", "+
                                                                                    "WG_NetID = " + NetworkConfigCopy.u16NetworkId.ToString());
                                    }                                   
                                    break;
                                }
                                #endregion
                                #region eHealthReport
                                case enAppNotifySubCMD.eHealthReport:
                                {
                                    tHealthReportParam param = new tHealthReportParam();
                                    param.Unserialize(notData.u8aData);
                                    compensateTime(ref param.TmpTime, calibrationTime);
                                    param.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "HR of ws(" + param.mac.ToHexString() +
                                        ")|Time(" +
                                        param.TmpTime.u8Year + "-" +
                                        param.TmpTime.u8Month + "-" +
                                        param.TmpTime.u8Day + " " +
                                        param.TmpTime.u8Hour + ":" +
                                        param.TmpTime.u8Min + ":" +
                                        param.TmpTime.u8Sec +
                                        ")|State(" + param.u8State + ")|Wkpmode(" + param.u8WakeupMode + 
                                        ")|Factmp(" + param.u32FacilityTmp + ")|Devtmp(" + param.u32DeviceTmp + ")|BatState(" + param.u32BatState + ")");
                                    HealthReportAnalysis(param);
                                    break;
                                }
                                #endregion
                                #region eWaveDesc
                                case enAppNotifySubCMD.eWaveDesc:
                                {
                                    tMeshWaveDescParam param = new tMeshWaveDescParam();
                                    param.Unserialize(notData.u8aData);
                                    compensateTime(ref param.DaqTime, calibrationTime);
                                    param.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "WR of ws(" + param.mac.ToHexString() +
                                        ")|Time(" + 
                                        param.DaqTime.u8Year + "-" + 
                                        param.DaqTime.u8Month + "-" + 
                                        param.DaqTime.u8Day + " " + 
                                        param.DaqTime.u8Hour + ":" + 
                                        param.DaqTime.u8Min + ":" + 
                                        param.DaqTime.u8Sec +
                                        ")|DaqMode(" + param.DaqMode +
                                        ")|MeasDef(Type<" + param.WaveType + ">" +
                                                   "WaveLen<" + param.u16WaveLen + ">" + 
                                                   "UpFreq<" + param.u16UpFreqOrAevBw + ">" +
                                                   "DwFreq<" + param.u16DwFreqOrAevFt + ">" +
                                        ")|AmpScaler(" + param.f32AmpScaler +
                                        ")|TotalFrames(" + param.u16TotalFramesNum + ")");
                                    //响应波形描述
                                    tMeshWaveDescResult result = new tMeshWaveDescResult();
                                    result.mac.Assign(param.mac);
                                    result.u8RC = 0;
                                    //ReplyWaveDesc(result,true);
                                    //再次解析
                                    WsWaveDescAnalysis(param);                          
                                    break;
                                }
                                #endregion
                                #region eWaveData
                                case enAppNotifySubCMD.eWaveData:
                                {
                                    tWaveDataParam param = new tWaveDataParam();
                                    param.Unserialize(notData.u8aData);
                                    param.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "WD(" + param.u16CurrentFrameID + "/" + param.u16TotalFramesNum + ")" + " of ws(" + param.mac.ToHexString() + ")");
                                    WsWaveDataAnalysis(param);                                 
                                    break;
                                }
                                #endregion
                                #region eEigenVal
                                case enAppNotifySubCMD.eEigenVal:
                                {
                                    tMeshEigenValueParam param = new tMeshEigenValueParam();
                                    param.Unserialize(notData.u8aData);
                                    compensateTime(ref param.SampleTime, calibrationTime);
                                    param.mac.Assign(notData.Mac);
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("ER of ws(" + param.mac.ToHexString() + ")|Time(");
                                    sb.Append(param.SampleTime.u8Year + "-" + param.SampleTime.u8Month + "-" + param.SampleTime.u8Day + " " +
                                        param.SampleTime.u8Hour + ":" + param.SampleTime.u8Min + ":" + param.SampleTime.u8Sec + ")");
                                    if (param.AccDef != null)
                                    {
                                        if (param.AccDef.bAccWaveRMSValid)
                                            sb.Append("|Acc.RMS(" + param.AccDef.fAccRMSValue + ")");
                                        if (param.AccDef.bAccWavePKValid)
                                            sb.Append("|Acc.PK(" + param.AccDef.fAccPKValue + ")");
                                    }
                                    if (param.VelDef != null)
                                    {
                                        if (param.VelDef.bVelWaveRMSValid)
                                            sb.Append("|Vel.RMS(" + param.VelDef.fVelRMSValue + ")");
                                        if (param.VelDef.bVelWaveLPEValid)
                                            sb.Append("|Vel.LPE(" + param.VelDef.fVelLPEValue + ")");
                                        if (param.VelDef.bVelWaveMPEValid)
                                            sb.Append("|Vel.MPE(" + param.VelDef.fVelMPEValue + ")");
                                        if (param.VelDef.bVelWaveHPEValid)
                                            sb.Append("|Vel.HPE(" + param.VelDef.fVelHPEValue + ")");
                                    }
                                    if (param.DspDef != null)
                                    {
                                        if (param.DspDef.bDspWavePKPKValid)
                                            sb.Append("|Dsp.PPK(" + param.DspDef.fDspPKPKValue + ")");
                                    }
                                    if (param.AccEnvDef != null)
                                    {
                                        if (param.AccEnvDef.bAccEnvWavePKValid)
                                            sb.Append("|Aev.PK(" + param.AccEnvDef.fAccEnvPKValue + ")");
                                        if (param.AccEnvDef.bAccEnvWavePKCValid)
                                            sb.Append("|Aev.PKC(" + param.AccEnvDef.fAccEnvPKCValue + ")");
                                        if (param.AccEnvDef.bAccEnvWaveMEANValid)
                                            sb.Append("|Aev.MEAN(" + param.AccEnvDef.fAccEnvMEANValue + ")");
                                    }
                                    if (param.LQDef != null)
                                    {
                                        if (param.LQDef.bLQWaveRMSValid)
                                            sb.Append("|Lq.RMS(" + param.LQDef.fLQRMSValue + ")");
                                    }
                                    if (param.RevStopDef != null)
                                    {
                                        if (param.RevStopDef.bRevStopWavePKValid)
                                            sb.Append("|Ss.PK(" + param.RevStopDef.fRevStopPKValue + ")");
                                    }

                                    CommStackLog.RecordInf(enLogLayer.eAdapter, sb.ToString());
                                    WsEigenDataAnalysis(param);                                    
                                    break;
                                }
                                #endregion
                                default:
                                    break;
                            }
                            break;
                        }
                        #endregion
                        #region eSet
                        case enAppMainCMD.eSet:
                        {
                            switch ((enAppSetSubCMD)(sub_Cmd))
                            {
                                #region eTimeCali
                                case enAppSetSubCMD.eTimeCali:
                                {
                                    // 校时是广播形式，因为无应答信息
                                    break;
                                }
                                #endregion
                                #region eNetworkID
                                case enAppSetSubCMD.eNetworkID:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eSet, (Byte)enAppSetSubCMD.eNetworkID);
                                    tMeshSetWsNwIdResult result = new tMeshSetWsNwIdResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetNetworkIdResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                    SetNetworkIDReaultAnalysis(result);                                   
                                    break;
                                }
                                #endregion
                                #region eMeasDef
                                case enAppSetSubCMD.eMeasDef:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eSet, (Byte)enAppSetSubCMD.eMeasDef);
                                    tMeshSetMeasDefResult result = new tMeshSetMeasDefResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetMeasDefResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                    SetMeasDefReaultAnalysis(result);
                                    break;
                                }
                                #endregion
                                #region eSn
                                case enAppSetSubCMD.eSn:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eSet, (Byte)enAppSetSubCMD.eSn);
                                    tMeshSetWsSnResult result = new tMeshSetWsSnResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsSnResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                    SetWsSnReaultAnalysis(result);

                                    break;
                                }
                                #endregion
                                #region eCaliCoeff
                                case enAppSetSubCMD.eCaliCoeff:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eSet, (Byte)enAppSetSubCMD.eCaliCoeff);
                                    tMeshCaliSensorResult result = new tMeshCaliSensorResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "CaliSensorResult of ws(" + result.mac.ToHexString() + ") is RC("
                                        + result.u8RC + ")|Gain(" + result.f32Gain + ")|Offset(" + result.f32Offset + ")");
                                    SetCaliWsSensorReaultAnalysis(result);
                                    break;
                                }
                                #endregion
                                #region eADCloseVolt
                                case enAppSetSubCMD.eADCloseVolt:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eSet, (Byte)enAppSetSubCMD.eADCloseVolt);
                                    tMeshSetADCloseVoltResult result = new tMeshSetADCloseVoltResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetADCloseVolt of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                    SetADCloseVoltReaultAnalysis(result);
                                    break;
                                }
                                #endregion
                                #region eRevStop
                                case enAppSetSubCMD.eRevStop:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eSet, (Byte)enAppSetSubCMD.eRevStop);
                                    tMeshSetWsStartOrStopResult result = new tMeshSetWsStartOrStopResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsStartOrStop of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                    SetWsStartOrStopReaultAnalysis(result);                                              
                                    break;
                                }
                                #endregion
                                #region eTrigParam
                                case enAppSetSubCMD.eTrigParam:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eSet, (Byte)enAppSetSubCMD.eTrigParam);
                                    tMeshSetTrigParamResult result = new tMeshSetTrigParamResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetTrigParam of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                    SetTrigParamResultAnalysis(result);                                              
                                    break;
                                }
                                #endregion
                                #region eWsRouteMode
                                case enAppSetSubCMD.eWsRouteMode:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eSet, (Byte)enAppSetSubCMD.eWsRouteMode);
                                    tMeshSetWsRouteModeResult result = new tMeshSetWsRouteModeResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsRouteMode of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                    SetWsRouteModeResultAnalysis(result);                                               
                                    break;
                                }
                                #endregion
                                default:
                                    break;
                            }
                            break;
                        }
                        #endregion
                        #region eGet
                        case enAppMainCMD.eGet:
                        {
                            switch ((enAppGetSubCMD)(sub_Cmd))
                            {
                                #region eSelfReport
                                case enAppGetSubCMD.eSelfReport:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eGet, (Byte)enAppGetSubCMD.eSelfReport);
                                    tGetSelfReportResult result = new tGetSelfReportResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    lock (m_dicOnLineWs)
                                    {
                                        if (string.Compare(m_macManager, result.mac.ToHexString()) == 0)
                                            return;
                                        else if (m_dicOnLineWs.ContainsKey(result.mac.ToHexString()))
                                            m_dicOnLineWs[result.mac.ToHexString()] = true;
                                        else
                                            m_dicOnLineWs.Add(result.mac.ToHexString(), true);
                                    }
                                    if (result.u8RC == 0)
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetSelfReport of ws(" + result.mac.ToHexString() + ")|FwVer(" +
                                            result.verMcuFw.u8Main + "." + result.verMcuFw.u8Sub + "." +
                                            result.verMcuFw.u8Rev + "." + result.verMcuFw.u8Build + ")|BlVer(" +
                                            result.verMcuBl.u8Main + "." + result.verMcuBl.u8Sub + "." +
                                            result.verMcuBl.u8Rev + "." + result.verMcuBl.u8Build + ")|StkVer(" +
                                            result.verMoteStack.u8Main + "." + result.verMoteStack.u8Sub + "." +
                                            result.verMoteStack.u8Rev + "." + result.verMoteStack.u8Build + ")");
                                        if (GetSelfReportReaultNotify != null)
                                            GetSelfReportReaultNotify(result);
                                        GetSelfReportAnalysis(result);
                                    }
                                    else
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetSelfReport of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                        if (getSelfReportFailed != null)
                                            getSelfReportFailed(result.mac);
                                    }
                                                
                                    break;
                                }
                                #endregion
                                #region eMeasDef
                                case enAppGetSubCMD.eMeasDef:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eGet, (Byte)enAppGetSubCMD.eMeasDef);
                                    tGetMeasDefResult result = new tGetMeasDefResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    if (result.u8RC == 0)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        sb.Append("GetMeasDefResult of ws(" + result.mac.ToHexString() + ") is ");
                                        sb.Append("Mode(" + result.DaqMode + ")");
                                        sb.Append("|DaqPace(" + result.TmpDaqPeriod.u8Hour + ":" + result.TmpDaqPeriod.u8Min + ")");
                                        sb.Append("|EvMult(" + result.u16EigenDaqMult + ")|WvMult(" + result.u16WaveDaqMult + ")");
                                        sb.Append("|LFB(" + result.LFBSpcLwFreq + ", " + result.LFBSpcUpFreq + ")");
                                        sb.Append("|MFB(" + result.MFBSpcLwFreq + ", " + result.MFBSpcUpFreq + ")");
                                        sb.Append("|HFB(" + result.HFBSpcLwFreq + ", " + result.HFBSpcUpFreq + ")");
                                        if (result.AccMdf != null)
                                            sb.Append("|AccMdf(" + result.AccMdf.u16WaveLen + ", " + result.AccMdf.u16EigenLen + ", " + result.AccMdf.u16LowFreq + ", " + result.AccMdf.u16UpperFreq + ")");
                                        if (result.VelMdf != null)
                                            sb.Append("|VelMdf(" + result.VelMdf.u16WaveLen + ", " + result.VelMdf.u16EigenLen + ", " + result.VelMdf.u16LowFreq + ", " + result.VelMdf.u16UpperFreq + ")");
                                        if (result.DspMdf != null)
                                            sb.Append("|DspMdf(" + result.DspMdf.u16WaveLen + ", " + result.DspMdf.u16EigenLen + ", " + result.DspMdf.u16LowFreq + ", " + result.DspMdf.u16UpperFreq + ")");
                                        if (result.AccEnvMdf != null)
                                            sb.Append("|AevMdf(" + result.AccEnvMdf.u16WaveLen + ", " + result.AccEnvMdf.u16EigenLen + ", " + result.AccEnvMdf.u16LowFreq + ", " + result.AccEnvMdf.u16UpperFreq + ")");
                                        if (result.LQMdf != null)
                                            sb.Append("|LQMdf(" + result.LQMdf.u16WaveLen + ", " + result.LQMdf.u16EigenLen + ", " + result.LQMdf.u16LowFreq + ", " + result.LQMdf.u16UpperFreq + ")");
                                        if (result.RevStop != null)
                                            sb.Append("|RevStop(" + result.RevStop.u16WaveLen + ", " + result.RevStop.u16EigenLen + ", " + result.RevStop.u16LowFreq + ", " + result.RevStop.u16UpperFreq + ")");

                                        CommStackLog.RecordInf(enLogLayer.eAdapter, sb.ToString());
                                        if (GetMeasDefReaultNotify != null)
                                            GetMeasDefReaultNotify(result);
                                        if (!NetworkWSDaqSlot.ContainsKey(result.mac.ToHexString()))
                                        {
                                            tDaqTimeSlot slot = new tDaqTimeSlot();
                                            slot.TmpDaqPeriod.u8Hour = result.TmpDaqPeriod.u8Hour;
                                            slot.TmpDaqPeriod.u8Min = result.TmpDaqPeriod.u8Min;
                                            slot.u16EigenDaqMult = result.u16EigenDaqMult;
                                            slot.u16WaveDaqMult = result.u16WaveDaqMult;
                                            NetworkWSDaqSlot[result.mac.ToHexString()] = slot;
                                        }
                                        else
                                        {
                                            NetworkWSDaqSlot[result.mac.ToHexString()].TmpDaqPeriod.u8Hour = result.TmpDaqPeriod.u8Hour;
                                            NetworkWSDaqSlot[result.mac.ToHexString()].TmpDaqPeriod.u8Min  = result.TmpDaqPeriod.u8Min;
                                            NetworkWSDaqSlot[result.mac.ToHexString()].u16EigenDaqMult     = result.u16EigenDaqMult;
                                            NetworkWSDaqSlot[result.mac.ToHexString()].u16WaveDaqMult      = result.u16WaveDaqMult;
                                        }
                                    }
                                    else
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetMeasDefResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                        if (getMeasDefFailed != null)
                                            getMeasDefFailed(result.mac);
                                    }
                                                    
                                    break;
                                }
                                #endregion
                                #region eSn
                                case enAppGetSubCMD.eSn:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eGet, (Byte)enAppGetSubCMD.eSn);
                                    tGetWsSnResult result = new tGetWsSnResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    if (result.u8RC == 0)
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsSnResult of ws(" + result.mac.ToHexString() + ") is " + result.sn);
                                        if (GetWsSnReaultNotify != null)
                                            GetWsSnReaultNotify(result);
                                    }
                                    else
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsSnResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                        if (getWsSnFailed != null)
                                            getWsSnFailed(result.mac);
                                    }
                                    
                                    break;
                                }
                                #endregion
                                #region eCaliCoeff
                                case enAppGetSubCMD.eCaliCoeff:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eGet, (Byte)enAppGetSubCMD.eCaliCoeff);
                                    tGetSensorCaliResult result = new tGetSensorCaliResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    if (result.u8RC == 0)
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetSensorCaliCoeff of ws("
                                            + result.mac.ToHexString() + ") is Flag(" + result.Flag +
                                            ")|Gain(" + result.Gain + ")|Offset(" + result.Offset + ")");
                                        if (GetSensorCaliReaultNotify != null)
                                            GetSensorCaliReaultNotify(result);
                                    }
                                    else
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetSensorCaliCoeff of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                        if (getSensorCaliFailed != null)
                                            getSensorCaliFailed(result.mac);
                                    }
                                                
                                    break;
                                }
                                #endregion
                                #region eADCloseVolt
                                case enAppGetSubCMD.eADCloseVolt:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eGet, (Byte)enAppGetSubCMD.eADCloseVolt);
                                    tGetADCloseVoltResult result = new tGetADCloseVoltResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    if (result.u8RC == 0)
                                    {
                                         CommStackLog.RecordInf(enLogLayer.eAdapter, "GetADCloseVolt of ws(" + result.mac.ToHexString() +
                                            ") is VoltageLimit(" + result.VoltageLimit + ")|VoltageCount(" + +result.VoltageCount + ")");
                                        if (GetADCloseVoltNotify != null)
                                            GetADCloseVoltNotify(result);
                                    }
                                    else
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetADCloseVolt of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                        if (getADCloseVoltFailed != null)
                                            getADCloseVoltFailed(result.mac);
                                    }
                                   
                                    break;
                                }
                                #endregion
                                #region eRevStop
                                case enAppGetSubCMD.eRevStop:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eGet, (Byte)enAppGetSubCMD.eRevStop);
                                    tGetRevStopResult result = new tGetRevStopResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    if (result.u8RC == 0)
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetRevStop of ws(" + result.mac.ToHexString() + ") is " + result.bStart);
                                        if (getRevStopResultNotify != null)
                                            getRevStopResultNotify(result);
                                    }
                                    else
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetRevStop of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                        if (getRevStopFailed != null)
                                            getRevStopFailed(result.mac);
                                    }
                                    
                                    break;
                                }
                                #endregion
                                #region eTrigParam
                                case enAppGetSubCMD.eTrigParam:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eGet, (Byte)enAppGetSubCMD.eTrigParam);
                                    tGetTrigParamResult result = new tGetTrigParamResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    if (result.u8RC == 0)
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetTrigParam of ws(" + result.mac.ToHexString() + ") is " +
                                            "Enable(" + result.Enable + ")|" + 
                                            "Acc.RMS(" + result.AccMdf.bAccWaveRMSValid + ", " +result.AccMdf.fAccRMSValue + ")|" +
                                            "Acc.PK(" + result.AccMdf.bAccWavePKValid + ", " + result.AccMdf.fAccPKValue + ")|" +
                                            "Vel.RMS(" + result.VelMdf.bVelWaveRMSValid + ", " + result.VelMdf.fVelRMSValue + ")|" +
                                            "Vel.LPE(" + result.VelMdf.bVelWaveLPEValid + ", " + result.VelMdf.fVelLPEValue + ")|" +
                                            "Vel.MPE(" + result.VelMdf.bVelWaveMPEValid + ", " + result.VelMdf.fVelMPEValue + ")|" +
                                            "Vel.HPE(" + result.VelMdf.bVelWaveHPEValid + ", " + result.VelMdf.fVelHPEValue + ")|" +
                                            "Dsp.PPK(" + result.DspMdf.bDspWavePKPKValid + ", " + result.DspMdf.fDspPKPKValue + ")|" +
                                            "Aev.PK(" + result.AccEnvMdf.bAccEnvWavePKValid + ", " + result.AccEnvMdf.fAccEnvPKValue + ")|" +
                                            "Aev.PKC(" + result.AccEnvMdf.bAccEnvWavePKCValid + ", " + result.AccEnvMdf.fAccEnvPKCValue + ")|" +
                                            "Aev.MEAN(" + result.AccEnvMdf.bAccEnvWaveMEANValid + ", " + result.AccEnvMdf.fAccEnvMEANValue + ")");
                                        if (GetTrigParamResultNotify != null)
                                            GetTrigParamResultNotify(result);
                                    }
                                    else
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetTrigParam of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                        if (getTrigParamFailed != null)
                                            getTrigParamFailed(result.mac);
                                    }

                                    break;
                                }
                                #endregion
                                #region eWsRouteMode
                                case enAppGetSubCMD.eWsRouteMode:
                                {
                                    checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eGet, (Byte)enAppGetSubCMD.eWsRouteMode);
                                    tGetWsRouteResult result = new tGetWsRouteResult();
                                    result.Unserialize(notData.u8aData);
                                    result.mac.Assign(notData.Mac);
                                    if (result.u8RC == 0)
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsRouteModeResult of ws(" + result.mac.ToHexString() + ") is " + result.mode);
                                        if (GetWsRouteResultNotify != null)
                                            GetWsRouteResultNotify(result);
                                    }
                                    else
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsRouteModeResult of ws(" + result.mac.ToHexString() + ") is " + result.u8RC);
                                        if (getWsRouteFailed != null)
                                            getWsRouteFailed(result.mac);
                                    }
                                    
                                    break;
                                }
                                #endregion
                                default:
                                    break;
                                }
                                break;
                            }
                            #endregion
                        #region eRestore
                        case enAppMainCMD.eRestore:
                        {
                            if (sub_Cmd == (byte)enAppRestoreSubCMD.eWS)
                            {
                                checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eRestore, (Byte)enAppRestoreSubCMD.eWS);
                                tMeshRestoreWSResult result = new tMeshRestoreWSResult();
                                result.Unserialize(notData.u8aData);
                                result.mac.Assign(notData.Mac);
                                RestoreWSresultAnalysis(result);
                                
                            }
                            else
                                CommStackLog.RecordErr(enLogLayer.eAdapter, "Received restore result subcommand error!");

                            break;
                        }
                            #endregion
                        #region eReset
                        case enAppMainCMD.eReset:
                        {
                            if (sub_Cmd == (byte)enAppResetSubCMD.eWS)
                            {
                                checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eReset, (Byte)enAppResetSubCMD.eWS);
                                tMeshResetWSResult result = new tMeshResetWSResult();
                                result.Unserialize(notData.u8aData);
                                result.mac.Assign(notData.Mac);
                                ResetWSresultAnalysis(result);
                            }
                            else
                                CommStackLog.RecordErr(enLogLayer.eAdapter, "Received reset result subcommand error!");

                            break;
                        }
                        #endregion
                        #region eUpdate
                        case enAppMainCMD.eUpdate:
                        {
                            #region eFwDesc
                            if (sub_Cmd == (byte)enAppUpdateSubCMD.eFwDesc)
                            {
                                checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eUpdate, (Byte)enAppUpdateSubCMD.eFwDesc);
                                tSetFwDescInfoResult result = new tSetFwDescInfoResult();
                                result.Unserialize(notData.u8aData);
                                result.mac.Assign(notData.Mac);
                                if (result.u8RC == 0)
                                {
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "Reply FwDesc of ws(" + result.mac.ToHexString()+")");
                                    if (SetFwDescInfoReaultNotify != null)
                                        SetFwDescInfoReaultNotify(result);
                                }
                                else
                                {
                                    if (setFwDescFailed != null)
                                        setFwDescFailed(result.mac);
                                }
                            }
                            #endregion
                            #region eFwData
                            else if (sub_Cmd == (byte)enAppUpdateSubCMD.eFwData)
                            {
                                tSetFwDataResult result = new tSetFwDataResult();
                                result.Unserialize(notData.u8aData);
                                result.mac.Assign(notData.Mac);
                                /*if (result.u8RC == 0)
                                {
                                    if (SetFwDataReaultNotify != null)
                                        SetFwDataReaultNotify(result);
                                }
                                else
                                {
                                    if (setFwDataFailed != null)
                                        setFwDataFailed(result.mac);
                                }*/
                                CommStackLog.RecordInf(enLogLayer.eAdapter, "Reply FwData of ws(" + result.mac.ToHexString()+")");
                                if (SetFwDataReaultNotify != null)
                                    SetFwDataReaultNotify(result);
                            }
                            #endregion
                            #region eControl
                            else if (sub_Cmd == (byte)enAppUpdateSubCMD.eControl)
                            {
                                checkWsResponse(notData.Mac, (Byte)enAppMainCMD.eUpdate, (Byte)enAppUpdateSubCMD.eControl);
                                tCtlWsUpstrResult result = new tCtlWsUpstrResult();
                                result.Unserialize(notData.u8aData);
                                result.mac.Assign(notData.Mac);
                                // mac地址不匹配，则不处理
                                if (!m_bUpdateMoteMac.isEqual(notData.Mac))
                                {
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "CtlWsUpstr req ws mismatch rsp");
                                    return;
                                }
                                else
                                {
                                    if (m_bMuteOrDismute)
                                    {
                                        m_asigMute.Set();
                                        m_bMuteTimeout = false;
                                    }
                                    else
                                    {
                                        m_asigDismute.Set();
                                        m_bDismuteTimeout = false;
                                    }
                                }
                                //升级的ws的Mac地址匹配后，清除。
                                Array.Clear(m_bUpdateMoteMac.u8aData, 0, m_bUpdateMoteMac.u8aData.Length);

                                if (result.u8RC != 0x00)
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "UpdateControl RC(" + result.u8RC + ")");
                            }
                            #endregion
                            else
                                CommStackLog.RecordErr(enLogLayer.eAdapter, "Received update result subcommand error!");

                            break;
                        }
                        #endregion
                        default: break;
                    }

                    break;
                }
                #endregion
                default: break;
            }
        }

        /// <summary>
        /// 用户请求失败的处理函数
        /// </summary>
        /// <param name="element">原始请求元素</param>
        private void locateAppRequestFailed(UserRequestElement element)
        {
            byte Sub_Cmd  = 0;
            byte Main_Cmd = 0;
            if (element.cmd == enCmd.CMDID_SENDDATA)
            {
                tSENDDATA sendData = (tSENDDATA)element.param;
                Main_Cmd = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] >> 5);
                Sub_Cmd = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] & 0x1F);
                enAppMainCMD cmd = (enAppMainCMD)(Main_Cmd);
                
                tMAC wsMac = new tMAC();
                wsMac.Assign(sendData.mac);

                if (cmd == enAppMainCMD.eSet)
                {                   
                    if (Sub_Cmd == (byte)enAppSetSubCMD.eTimeCali && caliTimeFailed != null) caliTimeFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppSetSubCMD.eNetworkID && setNetworkIDFailed != null) setNetworkIDFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppSetSubCMD.eMeasDef && setMeasDefFailed != null) setMeasDefFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppSetSubCMD.eSn && setWsSnFailed != null) setWsSnFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppSetSubCMD.eCaliCoeff && caliWsSensorFailed != null) caliWsSensorFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppSetSubCMD.eADCloseVolt && setADCloseVoltFailed != null) setADCloseVoltFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppSetSubCMD.eRevStop && setWsStartOrStopFailed != null) setWsStartOrStopFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppSetSubCMD.eTrigParam && setTrigParamFailed != null) setTrigParamFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppSetSubCMD.eWsRouteMode && setWsRouteModeFailed != null) setWsRouteModeFailed(wsMac);
                }
                else if (cmd == enAppMainCMD.eGet)
                {                    
                    if (Sub_Cmd == (byte)enAppGetSubCMD.eSn && getWsSnFailed != null) getWsSnFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppGetSubCMD.eSelfReport && getSelfReportFailed != null) getSelfReportFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppGetSubCMD.eMeasDef && getMeasDefFailed != null) getMeasDefFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppGetSubCMD.eCaliCoeff && getSensorCaliFailed != null) getSensorCaliFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppGetSubCMD.eADCloseVolt && getADCloseVoltFailed != null) getADCloseVoltFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppGetSubCMD.eRevStop && getRevStopFailed != null) getRevStopFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppGetSubCMD.eTrigParam && getTrigParamFailed != null) getTrigParamFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppGetSubCMD.eWsRouteMode && getWsRouteFailed != null) getWsRouteFailed(wsMac);
                }
                else if (cmd == enAppMainCMD.eReset)
                {
                    if (Sub_Cmd == (byte)enAppResetSubCMD.eWS && resetWSFailed != null) resetWSFailed(wsMac);                    
                }
                else if (cmd == enAppMainCMD.eRestore)
                {
                    if (Sub_Cmd == (byte)enAppRestoreSubCMD.eWS && restoreWSFailed != null) restoreWSFailed(wsMac);                    
                }
                else if (cmd == enAppMainCMD.eUpdate)
                {
                    if (Sub_Cmd == (byte)enAppUpdateSubCMD.eFwDesc && setFwDescFailed != null) setFwDescFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppUpdateSubCMD.eFwData && setFwDataFailed != null) setFwDataFailed(wsMac);
                    else if (Sub_Cmd == (byte)enAppUpdateSubCMD.eControl)
                    {
                        if (m_bMuteOrDismute)
                        {
                            m_asigMute.Set();
                            m_bMuteTimeout = true;
                        }
                        else
                        {
                            m_asigDismute.Set();
                            m_bDismuteTimeout = true;
                        }
                    }
                }
            }

            if (element.cmd == enCmd.CMDID_SUBSCRIBE)
            {
                m_bSubsTimeout = true;
                m_asigSubs.Set();
            }
            else if (element.cmd == enCmd.CMDID_GETNETWORKCONFIG)
            {
                m_bGetNwCfgTimeout = true;
                m_asigGetNwCfg.Set();
            }
            else if (element.cmd == enCmd.CMDID_GETTIME)
            {
                m_bGetTimeTimeout = true;
                m_asigGetTime.Set();
            }
            else if (element.cmd == enCmd.CMDID_RESET)
            {
                if (m_bUpdating == true)
                {
                    m_bResetSysTimeout = true;
                    m_asigResetSys.Set();
                }
            }
            else if (element.cmd == enCmd.CMDID_SETNETWORKCONFIG)
            {
                if (m_bUpdating == true)
                {
                    m_bSetNwCfgTimeout = true;
                    m_asigSetNwCfg.Set();
                }
            }
            else if (element.cmd == enCmd.CMDID_GETMOTECONFIG)
            {
                m_bGetMtCfgTimeout = true;
                m_asigGetMtCfg.Set();
            }
            else if (element.cmd == enCmd.CMDID_EXCHANGENETWORKID)
            {
                if (exchNwIdFailed != null)
                    exchNwIdFailed();
            }
            else if (element.cmd == enCmd.CMDID_EXCHANGEMOTEJOINKEY)
            {
                if (exchMtJKFailed != null)
                {
                    tEXMOTEJOINKEY param = (tEXMOTEJOINKEY)element.param;
                    exchMtJKFailed(param.mac);
                }
            }
        }

        /// <summary>
        /// 顶层对网络响应的处理函数
        /// </summary>
        /// <param name="cmd">请求的命令</param>
        /// <param name="result">请求的结果</param>
        private void normalApiResponse(enCmd cmd, tEcho result)
        {
            switch (cmd)
            {
                case enCmd.CMDID_SUBSCRIBE:
                {
                    if (result.RC == eRC.RC_OK)
                    {
                        m_bSubsTimeout = false;
                        m_asigSubs.Set();
                    }

                    break;
                }
                case enCmd.CMDID_GETNETWORKCONFIG:
                {
                    if (result.RC == eRC.RC_OK)
                    {
                        m_bGetNwCfgTimeout = false;
                        m_asigGetNwCfg.Set();
                    }

                    tGetNetworkConfigEcho echo = (tGetNetworkConfigEcho)result;
                    NetworkConfigCopy.bInit = true;
                    NetworkConfigCopy.u16NetworkId = echo.u16NetworkId;
                    NetworkConfigCopy.s8ApTxPower = echo.s8ApTxPower;
                    NetworkConfigCopy.frmProfile = echo.frmProfile;
                    NetworkConfigCopy.u16MaxMotes = echo.u16MaxMotes;
                    NetworkConfigCopy.u16BaseBandwidth = echo.u16BaseBandwidth;
                    NetworkConfigCopy.u8DownFrameMultVal = echo.u8DownFrameMultVal;
                    NetworkConfigCopy.u8NumParents = echo.u8NumParents;
                    NetworkConfigCopy.ccaMode = echo.ccaMode;
                    NetworkConfigCopy.u16ChannelList = echo.u16ChannelList;
                    NetworkConfigCopy.bAutoStartNetwork = echo.bAutoStartNetwork;
                    NetworkConfigCopy.u8LocMode = echo.u8LocMode;
                    NetworkConfigCopy.bbMode = echo.bbMode;
                    NetworkConfigCopy.u8BBSize = echo.u8BbSize;
                    NetworkConfigCopy.isRadioTest = echo.isRadioTest;
                    NetworkConfigCopy.u16BwMult = echo.u16BwMult;
                    NetworkConfigCopy.u8OneChannel = echo.u8OneChannel;
                    break;
                }
                case enCmd.CMDID_GETTIME:
                {
                    if (result.RC == eRC.RC_OK)
                    {
                        m_bGetTimeTimeout = false;
                        m_asigGetTime.Set();
                    }

                    tGetTimeEcho echo = (tGetTimeEcho)result;
                    calibrationTime = (UInt64)(DateTime.Now - UtcBaseTime).TotalSeconds - echo.utcTime.u64Seconds;
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "New CaliTime(" + calibrationTime + ")");
                    break;
                }
                case enCmd.CMDID_GETMOTECONFIG:
                {
                    tGetMoteConfigEcho trueResult = (tGetMoteConfigEcho)result;
                    m_cacheGetMtCfgResult.RC = trueResult.RC;
                    m_cacheGetMtCfgResult.mac.Assign(trueResult.mac);
                    m_cacheGetMtCfgResult.u16MoteId = trueResult.u16MoteId;
                    m_cacheGetMtCfgResult.isAP = trueResult.isAP;
                    m_cacheGetMtCfgResult.u8State = trueResult.u8State;
                    m_cacheGetMtCfgResult.isRouting = trueResult.isRouting;

                    m_bGetMtCfgTimeout = false;
                    m_asigGetMtCfg.Set();

                    //if (QueryWsReaultNotify != null)
                    //    QueryWsReaultNotify(trueResult);

                    break;
                }
                case enCmd.CMDID_SETNETWORKCONFIG:
                {
                    if (m_bUpdating == true)
                    {
                        if (result.RC == eRC.RC_OK)
                        {
                            m_bSetNwCfgTimeout = false;
                            m_asigSetNwCfg.Set();
                        }
                    }
                    //张辽阔 2016-10-10 添加
                    else
                    {
                        if (result.RC == eRC.RC_OK)
                        {
                            if (m_SetNetworkCfgItem == enSNCItem.SNC_NWID && SetWgNwIdReplyNotify != null)
                                SetWgNwIdReplyNotify();
                        }
                        else
                        {
                            if (m_SetNetworkCfgItem == enSNCItem.SNC_NWID && setWgNwIdFailed != null)
                                setWgNwIdFailed();
                        }
                    }
						
                    m_SetNetworkCfgItem = enSNCItem.SNC_NULL;
                    break;
                }
                case enCmd.CMDID_GETNETWORKINFO:
                {
                    if (result.RC == eRC.RC_OK)
                    {
                        tGetNetworkInfoEcho nwInfo = (tGetNetworkInfoEcho)result;
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "NumMotes(" + nwInfo.u16NumMotes + ") now!");
                        if (m_bUpdating && nwInfo.u16NumMotes == 0)
                        {
                            // 人为构造NetworkReset事件，并通知上层
                            EventNetworkReset evtNetworkReset = new EventNetworkReset();
                            evtNetworkReset.NotifyType = enNotifyType.NOTIFID_NOTIFEVENT;
                            evtNetworkReset.EventType = enEventType.EVENTID_EVENTNETWORKRESET;
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "Artificial NetworkReset event!");
                            m_bManualNetworkResetEvent = true;
                            notifyHandler(evtNetworkReset.NotifyType, evtNetworkReset.EventType, evtNetworkReset);
                        }
                    }
                
                    break;
                }
                case enCmd.CMDID_RESET:
                {
                    tResetEcho ResetResult = (tResetEcho)result;
                    if (m_bUpdating == true)
                    {
                        if (ResetResult.RC == eRC.RC_OK)
                        {
                            m_bResetSysTimeout = false;
                            m_asigResetSys.Set();
                        }
                    }
                    else
                    {
                        if (ResetResult.RC == eRC.RC_OK)
                        {
                            if (ResetWGReaultNotify != null)
                                ResetWGReaultNotify(null);
                        }
                        else
                        {
                            if (resetWGFailed != null)
                                resetWGFailed();
                        }
                    }
                    break;
                }
                case enCmd.CMDID_RESTOREFACTORYDEFAULTS:
                {
                    tRestoreFactoryDefaultsEcho RestoreFactoryDefaultsResult = (tRestoreFactoryDefaultsEcho)result;
                    if (RestoreFactoryDefaultsResult.RC == eRC.RC_OK)
                    {
                        if (RestoreWGReaultNotify != null)
                            RestoreWGReaultNotify(null);
                    }
                    else
                    {
                        if (restoreWGFailed != null)
                            restoreWGFailed();
                    }

                    break;
                }
                default: break;
            }
        }

        /// <summary>
        /// 等待WS响应线程主函数
        /// </summary>
        private void appCmdResponseHandler()
        {
            CommStackLog.RecordInf(enLogLayer.eAdapter, "appCmdResponseHandler running");
            List<UserRequestElement> listDelElem = new List<UserRequestElement>();

            while (m_bKeepRunning)
            {
                m_msigMainThreadRun.WaitOne();

                try
                {
                    
                    lock (m_dicWaitAppRespCmds)
                    {
                        int cnt = m_dicWaitAppRespCmds.Count;
                        byte main_Cmd = 0;
                        byte sub_Cmd  = 0;
                        for (int i = 0; i < cnt; i++)
                        {
                            UserRequestElement elem = m_dicWaitAppRespCmds.Keys.ElementAt(i);
                            // 等待WS的响应成功
                            if (m_dicWaitAppRespCmds[elem])
                                listDelElem.Add(elem);
                            else
                            {
                                int iAgtReq2WsRepTimeout = cfgAgtReq2WsRepTimeout;

                                // 如果上层请求的命令是校准传感器，则超时时间为普通命令的3倍
                                tSENDDATA sendData = (tSENDDATA)elem.param;
                                main_Cmd = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] >> 5);                              
                                sub_Cmd  = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] & 0x1F);
                                if ((byte)enAppMainCMD.eSet == main_Cmd
                                    && (byte)enAppSetSubCMD.eCaliCoeff == sub_Cmd)
                                {
                                    iAgtReq2WsRepTimeout = 3 * iAgtReq2WsRepTimeout;
                                }

                                TimeSpan tsWaitAppRsp = DateTime.Now - elem.SuccessReqTime;
                                if (tsWaitAppRsp.TotalMilliseconds >= iAgtReq2WsRepTimeout)
                                {
                                    /*
                                    if (elem.retryTime < cfgUserRequestRetryTimes)
                                    {
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, describeCmd(elem) + " wait rsp timeout(" +
                                            tsWaitAppRsp.TotalMilliseconds + ")");
                                        elem.retryTime++;
                                        // 将重试命令入紧急队列
                                        ReqBuffer.EnQ(elem, true);
                                        listDelElem.Add(elem);
                                    }
                                    else
                                    {
                                        CommStackLog.RecordErr(enLogLayer.eAdapter, describeCmd(elem) + " failed");
                                        listDelElem.Add(elem);
                                        locateAppRequestFailed(elem);
                                    }
                                     */
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, describeCmd(elem) + " wait rsp timeout(" +
                                            tsWaitAppRsp.TotalMilliseconds + ")");
                                    listDelElem.Add(elem);
                                    locateAppRequestFailed(elem);
                                }
                            }
                        }

                        int aheadCnt = m_dicWaitAppRespCmds.Count;
                        foreach (UserRequestElement elem in listDelElem)
                        {
                            m_dicWaitAppRespCmds.Remove(elem);
                        }
                        int afterCnt = m_dicWaitAppRespCmds.Count;
                        if (aheadCnt != afterCnt)
                        {
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "WWSRah(" + aheadCnt + ")|WWSRaf(" + afterCnt + ")");
                            //CommStackLog.RecordInf(enLogLayer.eAdapter, "WWSRSPN(" + afterCnt + ")");
                        }
                    }

                    listDelElem.Clear();
                }
                catch (Exception ex)
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                }

                System.Threading.Thread.Sleep(TIMEOUT_PRECISION);
            }
        }

        /// <summary>
        /// 判定用户请求是否是需要响应的命令
        /// </summary>
        /// <param name="reqElem">用户请求</param>
        /// <returns>是或否</returns>
        private bool isWsRspCmd(UserRequestElement reqElem)
        {
            byte main_Cmd = 0;
            byte sub_Cmd  = 0;
            if (reqElem == null || reqElem.cmd != enCmd.CMDID_SENDDATA)
                return false;
            else
            {
                tSENDDATA sendData = (tSENDDATA)reqElem.param;
                main_Cmd = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] >> 5);
                sub_Cmd = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] & 0x1F);
                enAppMainCMD cmd = (enAppMainCMD)main_Cmd;

                if (cmd == enAppMainCMD.eSet || cmd == enAppMainCMD.eGet) return true;
                else if (cmd == enAppMainCMD.eRestore || cmd == enAppMainCMD.eReset)
                {
                    if (cmd == enAppMainCMD.eGet) return false;
                    else return true;
                }
                else if (cmd == enAppMainCMD.eUpdate && sub_Cmd == 0x01)
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// 检验下发命令是否为下发固件升级数据
        /// </summary>
        /// <param name="reqElem">用户请求</param>
        /// <returns>是或否</returns>
        private bool isSetFwDatCmd(UserRequestElement reqElem)
        {
            byte main_Cmd = 0;
            byte sub_Cmd = 0;
            if (reqElem == null || reqElem.cmd != enCmd.CMDID_SENDDATA)
                return false;
            else
            {
                tSENDDATA sendData = (tSENDDATA)reqElem.param;
                main_Cmd = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] >> 5);
                sub_Cmd = (byte)(sendData.u8aData[APP_CMD_OFFS_IN_NOTF_DATA] & 0x1F);
                enAppMainCMD cmd = (enAppMainCMD)main_Cmd;
                if (cmd == enAppMainCMD.eUpdate && sub_Cmd == (byte)enAppUpdateSubCMD.eFwData)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 异步命令响应处理线程主函数
        /// </summary>
        private void asynCmdResponseHandler()
        {
            CommStackLog.RecordInf(enLogLayer.eAdapter, "asynCmdResponseHandler running");

            List<uint> listDelCbid = new List<uint>();

            while (m_bKeepRunning)
            {
                m_msigMainThreadRun.WaitOne();

                try
                {
                    lock (m_Mesh.m_dicAsyncCmdRespRecords)
                    {
                        int cnt = m_Mesh.m_dicAsyncCmdRespRecords.Count;
                        // 便利等待列表中的每一个id
                        for (int i = 0; i < cnt; i++)
                        {
                            uint cbid = m_Mesh.m_dicAsyncCmdRespRecords.Keys.ElementAt(i);
                            AsyncCmdRespEntry target = m_Mesh.m_dicAsyncCmdRespRecords[cbid];
                            // 已收到Manger的ACK
                            if (target.ACK1ST)
                            {
                                // 已收到Manger的发送完成通知
                                if (target.NOT1ST)
                                {
                                    // 只要有新的异步命令完成则设置此信号量，表示Manager当前有能力处理新的请求
                                    //m_asigEased.Set();

                                    if (isSetFwDatCmd(target.SiblingRequest))
                                    {
                                        asigFwDatSent.Set();
                                    }

                                    // 准备删除此等待id
                                    listDelCbid.Add(cbid);
                                    if (isWsRspCmd(target.SiblingRequest))
                                    {
                                        // 如果当前命令是需要WS响应的命令
                                        //target.SiblingRequest.retryTime = 0;
                                        target.SiblingRequest.SuccessReqTime = DateTime.Now;
                                        lock (m_dicWaitAppRespCmds)
                                        {
                                            m_dicWaitAppRespCmds.Add(target.SiblingRequest, false);
                                        }
                                    }
                                    else
                                    {
                                        if (target.SiblingRequest.cmd == enCmd.CMDID_EXCHANGENETWORKID)
                                        {
                                            if (target.AsyncRC == 0 && ExchangeNetworkIdReplyNotify != null)
                                                ExchangeNetworkIdReplyNotify();
                                            else if (exchNwIdFailed != null)
                                                exchNwIdFailed();
                                        }
                                           
                                        if (target.SiblingRequest.cmd == enCmd.CMDID_EXCHANGEMOTEJOINKEY
                                            && ExchangeMoteJoinKeyReplyNotify != null)
                                        {
                                            tEXMOTEJOINKEY param = (tEXMOTEJOINKEY)target.SiblingRequest.param;
                                            ExchangeMoteJoinKeyReplyNotify(param.mac);
                                        }
                                    }
                                }
                                // ExchangeNetworkId的异步通知响应无cfgAgtReq2MshRepTimeout的限制，限制不住。
                                // ExchangeNetworkId的Manager执行完成通知消息的延迟时间根据网络规模及拓扑不同差异非常大
                                else if (target.SiblingRequest.cmd != enCmd.CMDID_EXCHANGENETWORKID)
                                {
                                    TimeSpan tsWaitNot = DateTime.Now - target.ACKTime;
                                    // 等待异步命令完成通知超时
                                    if (tsWaitNot.TotalMilliseconds >= cfgAgtReq2MshRepTimeout)
                                    {
                                        if (target.SiblingRequest.retryTime < cfgUserRequestRetryTimes)
                                        {
                                            CommStackLog.RecordInf(enLogLayer.eAdapter, "CBID(" + cbid +
                                                ") timeout(" + tsWaitNot.TotalMilliseconds + ")");
                                            target.SiblingRequest.retryTime++;
                                            // 将重试命令入紧急队列
                                            ReqBuffer.EnQ(target.SiblingRequest, true);
                                            listDelCbid.Add(cbid);
                                        }
                                        else
                                        {
                                            if (isSetFwDatCmd(target.SiblingRequest))
                                            {
                                                asigFwDatSent.Set();
                                            }

                                            CommStackLog.RecordErr(enLogLayer.eAdapter, describeCmd(target.SiblingRequest) +
                                                " failed");
                                            listDelCbid.Add(cbid);
                                            locateAppRequestFailed(target.SiblingRequest);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (target.NOT1ST)
                                {
                                    TimeSpan tsWaitNot = DateTime.Now - target.ACKTime;
                                    if (tsWaitNot.TotalMilliseconds >= cfgRetryBaseTime)
                                    {
                                        listDelCbid.Add(cbid);
                                    }
                                }
 
                            }
                        }

                        int aheadCnt = m_Mesh.m_dicAsyncCmdRespRecords.Count;
                        foreach (uint delcbid in listDelCbid)
                        {
                            m_Mesh.m_dicAsyncCmdRespRecords.Remove(delcbid);
                        }
                        int afterCnt = m_Mesh.m_dicAsyncCmdRespRecords.Count;
                        if (aheadCnt != afterCnt)
                        {
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "WACRNah(" + aheadCnt + ")|WACRNaf(" + afterCnt + ")");
                           // CommStackLog.RecordInf(enLogLayer.eAdapter, "WACRN(" + m_Mesh.m_dicAsyncCmdRespRecords.Count + ")");
                        }

                        listDelCbid.Clear();
                    }
                }
                catch (Exception ex)
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                }

                System.Threading.Thread.Sleep(TIMEOUT_PRECISION);
            }
        }

        /// <summary>
        /// 用户请求队列处理线程主函数
        /// </summary>
        private void userRequestQueueHandler()
        {
            CommStackLog.RecordInf(enLogLayer.eAdapter, "userRequestQueueHandler running");
            enErrCode EC = enErrCode.ERR_NONE;
            bool bRetry = false;
            byte u8RequestMgrCnt = 0;
            while (ReqBuffer.HaveRequest && m_bKeepRunning)
            {
                m_msigMainThreadRun.WaitOne();
                if (ReqBuffer.DeQ() == null)
                {
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

                m_bPassiveNewSession = false;
                u8RequestMgrCnt = 0;
                bRetry = false;

            GOON:
                m_Mesh.CancelTx();

                m_Mesh.Adapter2MeshBridge = ReqBuffer.Current;         
                m_Mesh.m_asigEased.Reset();
                EC = m_Mesh.MeshAPI(ReqBuffer.Current, bRetry);
                if (EC == enErrCode.ERR_NONE)
                {
                    if (m_bPassiveNewSession)
                        m_bPassiveNewSession = false;
                }
                else if (EC == enErrCode.ERR_MGR_RESPONSE_TIMEOUT)
                {
                    // 发出命令等待响应超时重试
                    if (u8RequestMgrCnt++ < cfgUserRequestRetryTimes)
                    {
                        CommStackLog.RecordInf(enLogLayer.eAdapter, describeCmd(ReqBuffer.Current) + " wait ack timeout");
                        bRetry = true;
                        goto GOON;
                    }
                    else
                    {
                        u8RequestMgrCnt = 0;
                        CommStackLog.RecordErr(enLogLayer.eAdapter, describeCmd(ReqBuffer.Current) + " wait ack failed");
                        locateAppRequestFailed(ReqBuffer.Current);
                    }
                }
                else if (EC == enErrCode.ERR_MGR_STRESSFUL)
                {
                    while (true)
                    {
                        if (m_Mesh.m_asigEased.WaitOne(cfgAgtReq2MshRepTimeout))
                        {
                            goto GOON;
                        }
                        // 等待超时后还未到Manager轻松，继续重试
                        else if (u8RequestMgrCnt < cfgUserRequestRetryTimes)
                        {
                            u8RequestMgrCnt++;
                            //System.Threading.Thread.Sleep(cfgRetryBaseTime + ReqBuffer.Current.retryTime * cfgRetryMultTime);
                        }
                        else
                        {
                            u8RequestMgrCnt = 0;
                            locateAppRequestFailed(ReqBuffer.Current);
                            CommStackLog.RecordErr(enLogLayer.eAdapter, describeCmd(ReqBuffer.Current) + " failed");
                            break;
                        }
                    }
                }
                else
                {
                    // 请求命令异常重试
                    if (u8RequestMgrCnt++ < cfgUserRequestRetryTimes)
                    {
                        CommStackLog.RecordInf(enLogLayer.eAdapter, describeCmd(ReqBuffer.Current) + " ErrCode(" + EC + ")");
                        bRetry = true;
                        goto GOON;
                    }
                    else
                    {
                        u8RequestMgrCnt = 0;
                        locateAppRequestFailed(ReqBuffer.Current);
                        CommStackLog.RecordErr(enLogLayer.eAdapter, describeCmd(ReqBuffer.Current) + " failed");
                    }
                }

                // 继续进行下一命令
                continue;
            }
        }

        #region 实现IAppWsRequest
        private WsSelfReportHandler WsSelfReportNotify = null;
        public event WsSelfReportHandler WsSelfReportArrived
        {
            add { lock (objectLock) { WsSelfReportNotify = value; } }
            remove { lock (objectLock) { WsSelfReportNotify = null; } }
        }
        private WsHealthReportHandler WsHealthReportNotify = null;
        public event WsHealthReportHandler WsHealthReportArrived
        {
            add { lock (objectLock) { WsHealthReportNotify = value; } }
            remove { lock (objectLock) { WsHealthReportNotify = null; } }
        }
        private WsWaveDescHandler WsWaveDescNotify = null;
        public event WsWaveDescHandler WsWaveDescArrived
        {
            add { lock (objectLock) { WsWaveDescNotify = value; } }
            remove { lock (objectLock) { WsWaveDescNotify = null; } }
        }
        private WsWaveDataHandler WsWaveDataNotify = null;
        public event WsWaveDataHandler WsWaveDataArrived
        {
            add { lock (objectLock) { WsWaveDataNotify = value; } }
            remove { lock (objectLock) { WsWaveDataNotify = null; } }
        }
        private WsEigenDataHandler WsEigenDataNotify = null;
        public event WsEigenDataHandler WsEigenDataArrived
        {
            add { lock (objectLock) { WsEigenDataNotify = value; } }
            remove { lock (objectLock) { WsEigenDataNotify = null; } }
        }
        private WsTmpVoltageDataHandler WsTmpVoltageDataNotify = null;
        public event WsTmpVoltageDataHandler WsTmpVoltageDataArrived
        {
            add { lock (objectLock) { WsTmpVoltageDataNotify = value; } }
            remove { lock (objectLock) { WsTmpVoltageDataNotify = null; } }
        }
        private WsRevStopHandler WsRevStopNotify = null;
        public event WsRevStopHandler WsRevStopArrived
        {
            add { lock (objectLock) { WsRevStopNotify = value; } }
            remove { lock (objectLock) { WsRevStopNotify = null; } }
        }
        private WsLQHandler WsLQNotify = null;
        public event WsLQHandler WsLQArrived
        {
            add { lock (objectLock) { WsLQNotify = value; } }
            remove { lock (objectLock) { WsLQNotify = null; } }
        }
        #endregion

        #region 实现IAppServerResponse
        public bool ReplySelfReport(tSelfReportResult resault, bool urgent = false)
        {
            return true;
            /*
            lock (objectLock)
            {
                byte[] appStream = new byte[resault.Len];
                resault.Serialize(appStream);
                if (SendData(resault.mac, appStream, urgent, enPktPriority.High) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplySelfReport mac(" + resault.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplySelfReport mac(" + resault.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
            */
        }
        public bool ReplyWaveDesc(tMeshWaveDescResult resault, bool urgent = false)
        {
            lock (objectLock)
            {
                //构造通信层的波形描述                
                byte[] appStream = new byte[resault.Len];
                resault.Serialize(appStream);
                if (SendData(resault.mac, appStream, urgent, enPktPriority.High) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyWaveDesc mac(" + resault.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyWaveDesc mac(" + resault.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool ReplyWaveData(tWaveDataResult resault, bool urgent = false)
        {
            lock (objectLock)
            {
                //构造通信层的波形描述
                tMeshWaveDataResult param = new tMeshWaveDataResult();
                param.mac.Assign(resault.mac);
                if (resault.u16aRC != null)
                {
                    if (resault.u16aRC[0] == 0xFFFF)
                    {
                        param.u8RC = 0;
                    }
                    else if (resault.u16aRC[0] == 0x0000)
                    {
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyWaveData mac(" + resault.mac.ToHexString() + ")" + "Lost WaveDesc!");
                        param.u8RC = 1;
                        param.u16aLostNum = new ushort[1];
                        param.u16aLostNum[0] = 0xFFFF;
                    }
                    else
                    {
                        param.u8RC = 1;
                        param.u16aLostNum = new ushort[resault.u16aRC.Length];
                        for (int i = 0; i < resault.u16aRC.Length; i++)
                        {
                            param.u16aLostNum[i] = (ushort)(resault.u16aRC[i] - 1);
                        }
                            //Array.Copy(resault.u16aRC, param.u16aLostNum, param.u16aLostNum.Length);
                    }      
                }       
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent, enPktPriority.High) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyWaveData mac(" + resault.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyWaveData mac(" + resault.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool ReplyEigenValue(tEigenValueResult resault, bool urgent = false)
        {
            return true;
            /*
            lock (objectLock)
            {
                tMeshEigenValueResult param = new tMeshEigenValueResult();
                param.mac.Assign(resault.mac);
                param.u8RC = (byte)resault.u16RC;

                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent, enPktPriority.High) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyEigenValue mac(" + resault.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyEigenValue mac(" + resault.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
             */
        }
        public bool ReplyTmpVolReport(tTmpVolResult resault, bool urgent = false)
        {
            return true;
            /*
            lock (objectLock)
            {
                //增加温度电压的响应作为健康报告的响应回复给通信层
                tHealthReportResult param = new tHealthReportResult();
                param.mac.Assign(resault.mac);
                param.u8RC = (byte)resault.u16RC;

                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent, enPktPriority.High) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyHealthReport mac(" + resault.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyHealthReport mac(" + resault.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
             */
        }
        public bool ReplyRevStop(tRevStopResult resault, bool urgent = false)
        {
            return true;
            /*
            bool result = true;
            bool bDaqEigen = false;
            lock (objectLock)
            {
                lock (m_dicReceiveEignWs)
                {
                    if (m_dicReceiveEignWs.ContainsKey(resault.mac.ToHexString()))
                    {
                        if (m_dicReceiveEignWs[resault.mac.ToHexString()])
                        {
                            bDaqEigen = true;
                        }
                    }
                    else
                    {
                        return result;
                    }
                }
                if (!bDaqEigen)
                {
                    tMeshEigenValueResult param = new tMeshEigenValueResult();
                    param.mac.Assign(resault.mac);
                    param.u8RC = (byte)resault.u16RC;

                    byte[] appStream = new byte[param.Len];
                    param.Serialize(appStream);
                    if (SendData(resault.mac, appStream, urgent, enPktPriority.High) == enURErrCode.ERR_NONE)
                    {
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyEigen_RevStop mac(" + resault.mac.ToHexString() + ")");
                    }
                    else
                    {
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyEigen_RevStop mac(" + resault.mac.ToHexString() + ")" + " unadmissible");
                        result = false;
                    }
                }              
            }
            //为了使特征值的通知与启停机的通知间隔开，在启停机通知时，等待1s再上报通知。 
            Thread.Sleep(1000);
            return result;
             * */
        }
        public bool ReplyLQReport(tLQResult resault, bool urgent = false)
        {
            return true;
            /*
            lock (objectLock)
            {               
                byte[] appStream = new byte[resault.Len];
                resault.Serialize(appStream);
                if (SendData(resault.mac, appStream, urgent, enPktPriority.High) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyLQ_EigenValue mac(" + resault.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ReplyLQ_EigenValue mac(" + resault.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
            */
        }
        #endregion

        #region 实现IAppServerRequest
        #region 实现IAppServerRequest.设置类命令
        public bool CalibrateTime(tCaliTimeParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "CalibrateTime mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "CalibrateTime mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetWsID(tSetWsIdParam param, bool urgent = false)
        {           
            lock (objectLock)
            {          
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsID mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsID mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetNetworkID(tSetNetworkIdParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetNetworkID mac(" + param.mac.ToHexString() + ") to " + param.u16ID);
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetNetworkID mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetMeasDef(tSetMeasDefParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                tMeshSetMeasDefParam Def = new tMeshSetMeasDefParam();
                Def.mac.Assign(param.mac);
               
                if (param.DaqMode == enWsDaqMode.eTiming)
                {
                    Def.DaqMode = enMeshWsDaqMode.eTiming;
                    Def.u8DevNum = param.u8DevNum;
                    Def.u8DevTotal = param.u8DevTotal;
                }
                else if (param.DaqMode == enWsDaqMode.eImmediate)
                {
                    Def.DaqMode = enMeshWsDaqMode.eImmediate;
                }
                Def.TmpDaqPeriod.u8Hour = param.TmpDaqPeriod.u8Hour;
                Def.TmpDaqPeriod.u8Min  = param.TmpDaqPeriod.u8Min;
                Def.u16EigenDaqMult     = param.u16EigenDaqMult;
                Def.u16WaveDaqMult      = (UInt16)(param.u16WaveDaqMult / param.u16EigenDaqMult);
                if (param.AccMdf.bSubscribed == true)
                {
                    Def.AccMdf = new tMeshMeasDef();
                    Def.AccMdf.MeasDefType      = (enMeasDefType)param.AccMdf.MeasDefType;
                    if ((byte)(param.AccMdf.u8EigenValueType & (byte)enEigenValueType.eRMS) != 0)
                    {
                        Def.AccMdf.u8EigenValueType |= (byte)AccWvEvDef.eRMS;
                    }
                    if ((byte)(param.AccMdf.u8EigenValueType & (byte)enEigenValueType.ePK) != 0)
                    {
                        Def.AccMdf.u8EigenValueType |= (byte)AccWvEvDef.ePK;
                    }                 
                    Def.AccMdf.u16WaveLen       = param.AccMdf.u16WaveLen;
                    Def.AccMdf.u16EigenLen      = param.AccMdf.u16WaveLen;
                    Def.AccMdf.u16LowFreq       = param.AccMdf.u16LowFreq;
                    Def.AccMdf.u16UpperFreq     = param.AccMdf.u16UpperFreq;
                    Def.u8MeasDefCnt++;
                }
                if (param.VelMdf.bSubscribed == true)
                {
                    Def.VelMdf = new tMeshMeasDef();
                    Def.VelMdf.MeasDefType      = (enMeasDefType)param.VelMdf.MeasDefType;
                    if ((byte)(param.VelMdf.u8EigenValueType & (byte)enEigenValueType.eRMS) != 0)
                    {
                        Def.VelMdf.u8EigenValueType |= (byte)VelWvEvDef.eRMS;
                    }                     
                    Def.VelMdf.u16WaveLen       = param.VelMdf.u16WaveLen;
                    Def.VelMdf.u16EigenLen      = param.VelMdf.u16WaveLen;
                    Def.VelMdf.u16LowFreq       = param.VelMdf.u16LowFreq;
                    Def.VelMdf.u16UpperFreq     = param.VelMdf.u16UpperFreq;
                    Def.u8MeasDefCnt++;
                }
                if (param.DspMdf.bSubscribed == true && param.DspMdf.u16WaveLen != 16384)
                {
                    Def.DspMdf = new tMeshMeasDef();
                    Def.DspMdf.MeasDefType      = (enMeasDefType)param.DspMdf.MeasDefType;
                    if ((byte)(param.DspMdf.u8EigenValueType & (byte)enEigenValueType.ePKPK) != 0)
                    {
                        Def.DspMdf.u8EigenValueType |= (byte)DspWvEvDef.ePPK;
                    }
                    Def.DspMdf.u16WaveLen       = param.DspMdf.u16WaveLen;
                    Def.DspMdf.u16EigenLen      = param.DspMdf.u16WaveLen;
                    Def.DspMdf.u16LowFreq       = param.DspMdf.u16LowFreq;
                    Def.DspMdf.u16UpperFreq     = param.DspMdf.u16UpperFreq;
                    Def.u8MeasDefCnt++;
                }
                if (param.AccEnvMdf.bSubscribed == true)
                {
                    Def.AccEnvMdf = new tMeshMeasDef();
                    Def.AccEnvMdf.MeasDefType      = (enMeasDefType)param.AccEnvMdf.MeasDefType;
                    if ((byte)(param.AccEnvMdf.u8EigenValueType & (byte)enEigenValueType.ePK) != 0)
                    {
                        Def.AccEnvMdf.u8EigenValueType |= (byte)AevWvEvDef.ePK;
                    }
                    if ((byte)(param.AccEnvMdf.u8EigenValueType & (byte)enEigenValueType.ePKC) != 0)
                    {
                        Def.AccEnvMdf.u8EigenValueType |= (byte)AevWvEvDef.ePKC;
                    }
                    Def.AccEnvMdf.u16WaveLen       = param.AccEnvMdf.u16WaveLen;
                    Def.AccEnvMdf.u16EigenLen      = param.AccEnvMdf.u16WaveLen;
                    Def.AccEnvMdf.u16LowFreq       = param.AccEnvMdf.u16LowFreq;
                    Def.AccEnvMdf.u16UpperFreq     = param.AccEnvMdf.u16UpperFreq;
                    Def.u8MeasDefCnt++;
                }
                if (param.LQMdf.bSubscribed == true && param.DaqMode == enWsDaqMode.eTiming)
                {
                    Def.LQMdf = new tMeshMeasDef();
                    Def.LQMdf.MeasDefType      = (enMeasDefType)param.LQMdf.MeasDefType;
                    if ((byte)(param.LQMdf.u8EigenValueType & (byte)enEigenValueType.eRMS) != 0)
                    {
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "SetLQ");
                        Def.LQMdf.u8EigenValueType |= (byte)LqWvEvDef.eRMS;
                    }
                    Def.LQMdf.u16WaveLen       = param.LQMdf.u16WaveLen;
                    Def.LQMdf.u16EigenLen      = param.LQMdf.u16WaveLen;
                    Def.LQMdf.u16LowFreq       = param.LQMdf.u16LowFreq;
                    Def.LQMdf.u16UpperFreq     = param.LQMdf.u16UpperFreq;
                    Def.u8MeasDefCnt++;
                }
                if (param.RevStop.bSubscribed == true && param.DaqMode == enWsDaqMode.eTiming)
                {
                    Def.RevStop = new tMeshMeasDef();
                    Def.RevStop.MeasDefType      = (enMeasDefType)param.RevStop.MeasDefType;
                    if ((byte)(param.RevStop.u8EigenValueType & (byte)enEigenValueType.ePK) != 0)
                    {
                        Def.RevStop.u8EigenValueType |= (byte)SsWvEvDef.ePK;
                    }
                    Def.RevStop.u16WaveLen       = param.RevStop.u16WaveLen;
                    Def.RevStop.u16EigenLen      = param.RevStop.u16WaveLen;
                    Def.RevStop.u16LowFreq       = param.RevStop.u16LowFreq;
                    Def.RevStop.u16UpperFreq     = param.RevStop.u16UpperFreq;
                    Def.u8MeasDefCnt++;
                }

                byte[] appStream = new byte[Def.Len];
                Def.Serialize(appStream);
                if (SendData(Def.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetMeasDef mac(" + param.mac.ToHexString() + ")");
                    if (!NetworkWSDaqSlot.ContainsKey(param.mac.ToHexString()))
                    {
                        tDaqTimeSlot slot = new tDaqTimeSlot();
                        slot.TmpDaqPeriod.u8Hour = param.TmpDaqPeriod.u8Hour;
                        slot.TmpDaqPeriod.u8Min  = param.TmpDaqPeriod.u8Min;
                        slot.u16EigenDaqMult     = param.u16EigenDaqMult;
                        slot.u16WaveDaqMult      = param.u16WaveDaqMult;
                        NetworkWSDaqSlot[param.mac.ToHexString()] = slot;
                    }
                    else
                    {
                        NetworkWSDaqSlot[param.mac.ToHexString()].TmpDaqPeriod.u8Hour = param.TmpDaqPeriod.u8Hour;
                        NetworkWSDaqSlot[param.mac.ToHexString()].TmpDaqPeriod.u8Min  = param.TmpDaqPeriod.u8Min;
                        NetworkWSDaqSlot[param.mac.ToHexString()].u16EigenDaqMult     = param.u16EigenDaqMult;
                        NetworkWSDaqSlot[param.mac.ToHexString()].u16WaveDaqMult      = param.u16WaveDaqMult;
                    }                   
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetMeasDef mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetWsSn(tSetWsSnParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                tMeshSetWsSnParam WsSn = new tMeshSetWsSnParam();
                WsSn.u8SnLen = (byte)param.sn.Length;
                WsSn.sn = new byte[param.sn.Length];
                Array.Copy(param.sn,WsSn.sn,param.sn.Length);

                byte[] appStream = new byte[WsSn.Len];
                WsSn.Serialize(appStream);
                if (SendData(WsSn.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsSn mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetMeasDef mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool CalibrateWsSensor(tCaliSensorParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                tMeshCaliSensorParam CaliSensor = new tMeshCaliSensorParam();
                CaliSensor.u8Flag = 0x02;
                CaliSensor.mac.Assign(param.mac);

                byte[] appStream = new byte[CaliSensor.Len];
                CaliSensor.Serialize(appStream);
                if (SendData(CaliSensor.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "CalibrateWsSensor mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "CalibrateWsSensor mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetADCloseVolt(tSetADCloseVoltParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetADCloseVolt mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetADCloseVolt mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetWsStartOrStop(tSetWsStateParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                if (param.WsState == 0)
                {
                    param.WsState = 1;
                }
                else if (param.WsState == 1)
                {
                    param.WsState = 0;
                }
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);               
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsStartOrStop mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsStartOrStop mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetTrigParam(tSetTrigParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                tMeshSetTrigParam Trig = new tMeshSetTrigParam();
                Trig.mac.Assign(param.mac);
                Trig.Enable = (enMeshEnableFun)param.Enable;
                //加速度波形的触发式参数
                Trig.AccMdf.bAccWaveRMSValid = param.AccMdf.bEnable;
                Trig.AccMdf.bAccWavePKValid  = param.AccMdf.bEnable;
                Trig.AccMdf.fAccRMSValue     = param.AccMdf.fCharRmsvalue;
                Trig.AccMdf.fAccPKValue      = param.AccMdf.fCharPKvalue;
                //速度波形的触发式参数
                Trig.VelMdf.bVelWaveRMSValid = param.VelMdf.bEnable;
                Trig.VelMdf.fVelRMSValue     = param.VelMdf.fCharRmsvalue;
                //位移波形的触发式参数
                Trig.DspMdf.bDspWavePKPKValid = param.DspMdf.bEnable;
                Trig.DspMdf.fDspPKPKValue     = param.DspMdf.fCharPKPKvalue;
                //加速度包络的触发式参数
                Trig.AccEnvMdf.bAccEnvWavePKValid  = param.AccEnvMdf.bEnable;
                Trig.AccEnvMdf.bAccEnvWavePKCValid = param.AccEnvMdf.bEnable;
                Trig.AccEnvMdf.fAccEnvPKValue      = param.AccEnvMdf.fCharPKvalue;
                Trig.AccEnvMdf.fAccEnvPKCValue     = param.AccEnvMdf.fCharPKCvalue;

                byte[] appStream = new byte[Trig.Len];
                Trig.Serialize(appStream);
                if (SendData(Trig.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetTrigParam mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetTrigParam mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetWsRouteMode(tSetWsRouteMode param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsRouteMode mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsRouteMode mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetWsDebugMode(tSetWsDebugMode param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsDebugMode mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetWsDebugMode mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
 
        #endregion
        #region 实现IAppServerRequest.获取类命令
        public bool GetSelfReport(tGetSelfReportParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetSelfReport mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetSelfReport mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool GetMeasDef(tGetMeasDefParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetMeasDef mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetMeasDef mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool GetWsSn(tGetWsSnParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsSn mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsSn mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool GetSensorCaliCoeff(tGetCaliCoeffParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetSensorCaliCoeff mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetSensorCaliCoeff mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool GetADCloseVolt(tGetADCloseVoltParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetADCloseVolt mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetADCloseVolt mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool GetWsStartOrStop(tGetWsStartOrStopParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsStartOrStopParam mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsStartOrStopParam mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool GetTrigParam(tGetTrigParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetTrigParam mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetTrigParam mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool GetWsRouteMode(tGetWsRouteMode param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsRouteMode mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "GetWsRouteMode mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        #endregion
        #region 实现IAppServerRequest.恢复类命令
        public bool RestoreWS(tRestoreWSParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                tMeshRestoreWSParam data = new tMeshRestoreWSParam();
                data.mac.Assign(param.mac);
                if (param.u8Mote == 1 && param.u8MCU == 1)
                {
                    data.u8Target = 0;
                }
                else if (param.u8MCU == 1)
                {
                    data.u8Target = 1;
                }
                else if (param.u8Mote == 1)
                {
                    data.u8Target = 2;
                }
                byte[] appStream = new byte[data.Len];
                data.Serialize(appStream);
                if (SendData(data.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "RestoreWS mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "RestoreWS mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool RestoreWG(bool urgent = false)
        {
            lock (objectLock)
            {
                if (RestoreFactoryDefaults(urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "RestoreWG");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "RestoreWG unadmissible");
                    return false;
                }
            }
        }
        public bool ResetWS(tResetWSParam param, bool urgent = false)
        {
            lock (objectLock)
            {

                tMeshResetWSParam data = new tMeshResetWSParam();
                data.mac.Assign(param.mac);
                if (param.u8Mote == 1 && param.u8MCU == 1)
                {
                    data.u8Target = 0;
                }
                else if (param.u8MCU == 1)
                {
                    data.u8Target = 1;
                }
                else if (param.u8Mote == 1)
                {
                    data.u8Target = 2;
                }
                byte[] appStream = new byte[data.Len];
                data.Serialize(appStream);
                if (SendData(data.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ResetWS mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ResetWS mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool ResetWG(bool urgent = false)
        {
            lock (objectLock)
            {
                if (ResetSystem(urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ResetWG");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ResetWG unadmissible");
                    return false;
                }
            }
        }
        #endregion
        #region 实现IAppServerRequest.升级类命令
        public bool SetFwDescInfo(tSetFwDescInfoParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetFwDescInfo mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetFwDescInfo mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool SetFwData(tSetFwDataParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetFwData " + param.u16BlkIdx + " of mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "SetFwData " + param.u16BlkIdx + " of mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        public bool CtlWsUpstr(tCtlWsUpstrParam param, bool urgent = false)
        {
            lock (objectLock)
            {
                byte[] appStream = new byte[param.Len];
                param.Serialize(appStream);
                m_bUpdateMoteMac = param.mac;
                urgent = true;
                if (SendData(param.mac, appStream, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ControlWsUpstream mac(" + param.mac.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "ControlWsUpstream mac(" + param.mac.ToHexString() + ")" + " unadmissible");
                    return false;
                }
            }
        }
        #endregion
        #region 实现IAppServerRequest查询类命令
        public bool QueryWs(tMAC param, bool urgent = false)
        {
            lock (objectLock)
            {
                if (GetNextMoteConfig(param, urgent) == enURErrCode.ERR_NONE)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "QueryWs mac(" + param.ToHexString() + ")");
                    return true;
                }
                else
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "QueryWs mac(" + param.ToHexString() + ") unadmissible");
                    return false;
                }
            }
        }
        public bool QueryAllWs(bool urgent = false)
        {
            lock (objectLock)
            {
                m_Mesh.IsQueryingAllWs = true;
                CommStackLog.RecordInf(enLogLayer.eAdapter, "Trigger QueryAllWs");
                return true;
            }
        }
        #endregion
        #endregion

        #region 实现IAppServerRequestReply
        #region 实现IAppServerRequestReply.设置类命令正常响应
        private CaliTimeReaultHandler CaliTimeReaultNotify = null;
        public event CaliTimeReaultHandler CaliTimeReaultArrived
        {
            add { lock (objectLock) { CaliTimeReaultNotify = value; } }
            remove { lock (objectLock) { CaliTimeReaultNotify = null; } }
        }
        private SetWsIDReaultHandler SetWsIDReaultNotify = null;
        public event SetWsIDReaultHandler SetWsIDReaultArrived
        {
            add { lock (objectLock) { SetWsIDReaultNotify = value; } }
            remove { lock (objectLock) { SetWsIDReaultNotify = null; } }
        }
        private SetWsNwIDReaultHandler SetWsNwIDReaultNotify = null;
        public event SetWsNwIDReaultHandler SetNetworkIDReaultArrived
        {
            add { lock (objectLock) { SetWsNwIDReaultNotify = value; } }
            remove { lock (objectLock) { SetWsNwIDReaultNotify = null; } }
        }
        private SetMeasDefReaultHandler SetMeasDefReaultNotify = null;
        public event SetMeasDefReaultHandler SetMeasDefReaultArrived
        {
            add { lock (objectLock) { SetMeasDefReaultNotify = value; } }
            remove { lock (objectLock) { SetMeasDefReaultNotify = null; } }
        }
        private SetWsSnReaultHandler SetWsSnReaultNotify = null;
        public event SetWsSnReaultHandler SetWsSnReaultArrived
        {
            add { lock (objectLock) { SetWsSnReaultNotify = value; } }
            remove { lock (objectLock) { SetWsSnReaultNotify = null; } }
        }
        private CaliWsSensorReaultHandler CaliWsSensorReaultNotify = null;
        public event CaliWsSensorReaultHandler CaliWsSensorReaultArrived
        {
            add { lock (objectLock) { CaliWsSensorReaultNotify = value; } }
            remove { lock (objectLock) { CaliWsSensorReaultNotify = null; } }
        }
        private SetADCloseVoltReaultHandler SetADCloseVoltReaultNotify = null;
        public event SetADCloseVoltReaultHandler SetADCloseVoltReaultArrived
        {
            add { lock (objectLock) { SetADCloseVoltReaultNotify = value; } }
            remove { lock (objectLock) { SetADCloseVoltReaultNotify = null; } }
        }
        private SetWsStartOrStopReaultHandler SetWsStartOrStopReaultNotify = null;
        public event SetWsStartOrStopReaultHandler SetWsStartOrStopReaultArrived
        {
            add { lock (objectLock) { SetWsStartOrStopReaultNotify = value; } }
            remove { lock (objectLock) { SetWsStartOrStopReaultNotify = null; } }
        }
        private SetTrigParamResultHandler SetTrigParamResultNotify = null;
        public event SetTrigParamResultHandler SetTrigParamResultArrived
        {
            add { lock (objectLock) { SetTrigParamResultNotify = value; } }
            remove { lock (objectLock) { SetTrigParamResultNotify = null; } }
        }
        private SetWsRouteModeResultHandler SetWsRouteModeResultNotify = null;
        public event SetWsRouteModeResultHandler SetWsRouteModeResultArrived
        {
            add { lock (objectLock) { SetWsRouteModeResultNotify = value; } }
            remove { lock (objectLock) { SetWsRouteModeResultNotify = null; } }
        }
        #endregion
        #region 实现IAppServerRequestReply.获取类命令正常响应

        private GetSelfReportReaultHandler GetSelfReportReaultNotify = null;
        public event GetSelfReportReaultHandler GetSelfReportReaultArrived
        {
            add { lock (objectLock) { GetSelfReportReaultNotify = value; } }
            remove { lock (objectLock) { GetSelfReportReaultNotify = null; } }
        }
        private GetMeasDefReaultHandler GetMeasDefReaultNotify = null;
        public event GetMeasDefReaultHandler GetMeasDefReaultArrived
        {
            add { lock (objectLock) { GetMeasDefReaultNotify = value; } }
            remove { lock (objectLock) { GetMeasDefReaultNotify = null; } }
        }
        private GetWsSnReaultHandler GetWsSnReaultNotify = null;
        public event GetWsSnReaultHandler GetWsSnReaultArrived
        {
            add { lock (objectLock) { GetWsSnReaultNotify = value; } }
            remove { lock (objectLock) { GetWsSnReaultNotify = null; } }
        }
        private GetSensorCaliReaultHandler GetSensorCaliReaultNotify = null;
        public event GetSensorCaliReaultHandler GetSensorCaliReaultArrived
        {
            add { lock (objectLock) { GetSensorCaliReaultNotify = value; } }
            remove { lock (objectLock) { GetSensorCaliReaultNotify = null; } }
        }
        private GetADCloseVoltResultHandler GetADCloseVoltNotify = null;
        public event GetADCloseVoltResultHandler GetADCloseVoltResultArrived
        {
            add { lock (objectLock) { GetADCloseVoltNotify = value; } }
            remove { lock (objectLock) { GetADCloseVoltNotify = null; } }
        }
        private GetRevStopResultHandler getRevStopResultNotify = null;
        public event GetRevStopResultHandler GetRevStopResultArrived
        {
            add { lock (objectLock) { getRevStopResultNotify = value; } }
            remove { lock (objectLock) { getRevStopResultNotify = null; } }
        }
        private GetTrigParamResultHandler GetTrigParamResultNotify = null;
        public event GetTrigParamResultHandler GetTrigParamResultArrived
        {
            add { lock (objectLock) { GetTrigParamResultNotify = value; } }
            remove { lock (objectLock) { GetTrigParamResultNotify = null; } }
        }
        private GetWsRouteResultHandler GetWsRouteResultNotify = null;
        public event GetWsRouteResultHandler GetWsRouteResultArrived
        {
            add { lock (objectLock) { GetWsRouteResultNotify = value; } }
            remove { lock (objectLock) { GetWsRouteResultNotify = null; } }
        }
        #endregion
        #region 实现IAppServerRequestReply.恢复类命令正常响应
        private RestoreWSReaultHandler RestoreWSReaultNotify = null;
        public event RestoreWSReaultHandler RestoreWSReaultArrived
        {
            add { lock (objectLock) { RestoreWSReaultNotify = value; } }
            remove { lock (objectLock) { RestoreWSReaultNotify = null; } }
        }
        private RestoreWGReaultHandler RestoreWGReaultNotify = null;
        public event RestoreWGReaultHandler RestoreWGReaultArrived
        {
            add { lock (objectLock) { RestoreWGReaultNotify = value; } }
            remove { lock (objectLock) { RestoreWGReaultNotify = null; } }
        }
        private ResetWSReaultHandler ResetWSReaultNotify = null;
        public event ResetWSReaultHandler ResetWSReaultArrived
        {
            add { lock (objectLock) { ResetWSReaultNotify = value; } }
            remove { lock (objectLock) { ResetWSReaultNotify = null; } }
        }
        private ResetWGReaultHandler ResetWGReaultNotify = null;
        public event ResetWGReaultHandler ResetWGReaultArrived
        {
            add { lock (objectLock) { ResetWGReaultNotify = value; } }
            remove { lock (objectLock) { ResetWGReaultNotify = null; } }
        }
        #endregion
        #region 实现IAppServerRequestReply.升级类命令正常响应
        private SetFwDescReaultHandler SetFwDescInfoReaultNotify = null;
        public event SetFwDescReaultHandler SetFwDescReaultArrived
        {
            add { lock (objectLock) { SetFwDescInfoReaultNotify = value; } }
            remove { lock (objectLock) { SetFwDescInfoReaultNotify = null; } }
        }
        private SetFwDataReaultHandler SetFwDataReaultNotify = null;
        public event SetFwDataReaultHandler SetFwDataReaultArrived
        {
            add { lock (objectLock) { SetFwDataReaultNotify = value; } }
            remove { lock (objectLock) { SetFwDataReaultNotify = null; } }
        }
        private CtlWsUpstrReaultHandler CtlWsUpstrReaultNotify = null;
        public event CtlWsUpstrReaultHandler CtlWsUpstrReaultArrived
        {
            add { lock (objectLock) { CtlWsUpstrReaultNotify = value; } }
            remove { lock (objectLock) { CtlWsUpstrReaultNotify = null; } }
        }
        #endregion
        #region 实现IAppServerRequestReply.查询类命令正常响应
        private QueryWsReaultHandler QueryWsReaultNotify = null;
        public event QueryWsReaultHandler QueryWsReaultArrived
        {
            add
            {
                lock (objectLock)
                {
                    QueryWsReaultNotify = value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    QueryWsReaultNotify = null;
                }
            }
        }
        private QueryAllWsReaultHandler QueryAllWsReaultNotify = null;
        public event QueryAllWsReaultHandler QueryAllWsReaultArrived
        {
            add { lock (objectLock) { QueryAllWsReaultNotify = value; } }
            remove { lock (objectLock) { QueryAllWsReaultNotify = null; } }
        }
        #endregion
        #region 实现IAppServerRequestReply.原生类命令正常响应
        private ExchNwIdReaultHandler ExchangeNetworkIdReplyNotify = null;
        public event ExchNwIdReaultHandler ExchangeNetworkIdReplyArrived
        {
            add { lock (objectLock) { ExchangeNetworkIdReplyNotify = value; } }
            remove { lock (objectLock) { ExchangeNetworkIdReplyNotify = null; } }
        }
		private SetWgNwIdReaultHandler SetWgNwIdReplyNotify = null;
        public event SetWgNwIdReaultHandler SetWgNwIdReaultArrived
        {
            add { lock (objectLock) { SetWgNwIdReplyNotify = value; } }
            remove { lock (objectLock) { SetWgNwIdReplyNotify = null; } }
        }
        private ExchMtJKReaultHandler ExchangeMoteJoinKeyReplyNotify = null;
        public event ExchMtJKReaultHandler ExchangeMoteJoinKeyReplyArrived
        {
            add { lock (objectLock) { ExchangeMoteJoinKeyReplyNotify = value; } }
            remove { lock (objectLock) { ExchangeMoteJoinKeyReplyNotify = null; } }
        }
        #endregion
        #endregion

        #region 实现IAppServerFailResponse
        #region 实现IAppServerFailResponse.设置类命令异常响应
        private CaliTimeFailedHandler caliTimeFailed = null;
        public event CaliTimeFailedHandler CaliTimeFailed
        {
            add { lock (objectLock) { caliTimeFailed = value; } }
            remove { lock (objectLock) { caliTimeFailed = null; } }
        }       
        private SetWsNwIDFailedHandler setNetworkIDFailed = null;
        public event SetWsNwIDFailedHandler SetWsNwIDFailed
        {
            add { lock (objectLock) { setNetworkIDFailed = value; } }
            remove { lock (objectLock) { setNetworkIDFailed = null; } }
        }
        private SetWsIDFailedHandler setWsIDFailed = null;
        public event SetWsIDFailedHandler SetWsIDFailed
        {
            add { lock (objectLock) { setWsIDFailed = value; } }
            remove { lock (objectLock) { setWsIDFailed = null; } }
        }
        private SetMeasDefFailedHandler setMeasDefFailed = null;
        public event SetMeasDefFailedHandler SetMeasDefFailed
        {
            add { lock (objectLock) { setMeasDefFailed = value; } }
            remove { lock (objectLock) { setMeasDefFailed = null; } }
        }
        private SetWsSnFailedHandler setWsSnFailed = null;
        public event SetWsSnFailedHandler SetWsSnFailed
        {
            add { lock (objectLock) { setWsSnFailed = value; } }
            remove { lock (objectLock) { setWsSnFailed = null; } }
        }
        private CaliWsSensorFailedHandler caliWsSensorFailed = null;
        public event CaliWsSensorFailedHandler CaliWsSensorFailed
        {
            add { lock (objectLock) { caliWsSensorFailed = value; } }
            remove { lock (objectLock) { caliWsSensorFailed = null; } }
        }
        private SetADCloseVoltFailedHandler setADCloseVoltFailed = null;
        public event SetADCloseVoltFailedHandler SetADCloseVoltFailed
        {
            add { lock (objectLock) { setADCloseVoltFailed = value; } }
            remove { lock (objectLock) { setADCloseVoltFailed = null; } }
        }
        private SetWsStartOrStopFailedHandler setWsStartOrStopFailed = null;
        public event SetWsStartOrStopFailedHandler SetWsStartOrStopFailed
        {
            add { lock (objectLock) { setWsStartOrStopFailed = value; } }
            remove { lock (objectLock) { setWsStartOrStopFailed = null; } }
        }
        private SetTrigParamFailedHandler setTrigParamFailed = null;
        public event SetTrigParamFailedHandler SetTrigParamFailed
        {
            add { lock (objectLock) { setTrigParamFailed = value; } }
            remove { lock (objectLock) { setTrigParamFailed = null; } }
        }
        private SetWsRouteModeFailedHandler setWsRouteModeFailed = null;
        public event SetWsRouteModeFailedHandler SetWsRouteModeFailed
        {
            add { lock (objectLock) { setWsRouteModeFailed = value; } }
            remove { lock (objectLock) { setWsRouteModeFailed = null; } }
        }
        #endregion
        #region 实现IAppServerFailResponse.获取类命令异常响应
        private GetSelfReportFailedHandler getSelfReportFailed = null;
        public event GetSelfReportFailedHandler GetSelfReportFailed
        {
            add { lock (objectLock) { getSelfReportFailed = value; } }
            remove { lock (objectLock) { getSelfReportFailed = null; } }
        }
        private GetMeasDefFailedHandler getMeasDefFailed = null;
        public event GetMeasDefFailedHandler GetMeasDefFailed
        {
            add { lock (objectLock) { getMeasDefFailed = value; } }
            remove { lock (objectLock) { getMeasDefFailed = null; } }
        }
        private GetWsSnFailedHandler getWsSnFailed = null;
        public event GetWsSnFailedHandler GetWsSnFailed
        {
            add { lock (objectLock) { getWsSnFailed = value; } }
            remove { lock (objectLock) { getWsSnFailed = null; } }
        }
        private GetSensorCaliFailedHandler getSensorCaliFailed = null;
        public event GetSensorCaliFailedHandler GetSensorCaliFailed
        {
            add { lock (objectLock) { getSensorCaliFailed = value; } }
            remove { lock (objectLock) { getSensorCaliFailed = null; } }
        }
        private GetADCloseVoltFailedHandler getADCloseVoltFailed = null;
        public event GetADCloseVoltFailedHandler GetADCloseVoltFailed
        {
            add { lock (objectLock) { getADCloseVoltFailed = value; } }
            remove { lock (objectLock) { getADCloseVoltFailed = null; } }
        }
        private GetRevStopFailedHandler getRevStopFailed = null;
        public event GetRevStopFailedHandler GetRevStopFailed
        {
            add { lock (objectLock) { getRevStopFailed = value; } }
            remove { lock (objectLock) { getRevStopFailed = null; } }
        }
        private GetTrigParamFailedHandler getTrigParamFailed = null;
        public event GetTrigParamFailedHandler GetTrigParamFailed
        {
            add { lock (objectLock) { getTrigParamFailed = value; } }
            remove { lock (objectLock) { getTrigParamFailed = null; } }
        }
        private GetWsRouteFailedHandler getWsRouteFailed = null;
        public event GetWsRouteFailedHandler GetWsRouteFailed
        {
            add { lock (objectLock) { getWsRouteFailed = value; } }
            remove { lock (objectLock) { getWsRouteFailed = null; } }
        }
        #endregion
        #region 实现IAppServerFailResponse.恢复类命令异常响应
        private RestoreWSFailedHandler restoreWSFailed = null;
        public event RestoreWSFailedHandler RestoreWSFailed
        {
            add { lock (objectLock) { restoreWSFailed = value; } }
            remove { lock (objectLock) { restoreWSFailed = null; } }
        }
        private RestoreWGFailedHandler restoreWGFailed = null;
        public event RestoreWGFailedHandler RestoreWGFailed
        {
            add { lock (objectLock) { restoreWGFailed = value; } }
            remove { lock (objectLock) { restoreWGFailed = null; } }
        }
        private ResetWSFailedHandler resetWSFailed = null;
        public event ResetWSFailedHandler ResetWSFailed
        {
            add { lock (objectLock) { resetWSFailed = value; } }
            remove { lock (objectLock) { resetWSFailed = null; } }
        }
        private ResetWGFailedHandler resetWGFailed = null;
        public event ResetWGFailedHandler ResetWGFailed
        {
            add { lock (objectLock) { resetWGFailed = value; } }
            remove { lock (objectLock) { resetWGFailed = null; } }
        }
        #endregion
        #region 实现IAppServerFailResponse.升级类命令异常响应
        private SetFwDescFailedHandler setFwDescFailed = null;
        public event SetFwDescFailedHandler SetFwDescFailed
        {
            add { lock (objectLock) { setFwDescFailed = value; } }
            remove { lock (objectLock) { setFwDescFailed = null; } }
        }
        private SetFwDataFailedHandler setFwDataFailed = null;
        public event SetFwDataFailedHandler SetFwDataFailed
        {
            add { lock (objectLock) { setFwDataFailed = value; } }
            remove { lock (objectLock) { setFwDataFailed = null; } }
        }
        private CtlWsUpstrFailedHandler ctlWsUpstrFailed = null;
        public event CtlWsUpstrFailedHandler CtlWsUpstrFailed
        {
            add { lock (objectLock) { ctlWsUpstrFailed = value; } }
            remove { lock (objectLock) { ctlWsUpstrFailed = null; } }
        }/*
        private UpdateFailedHandler updateFailed = null;
        public event UpdateFailedHandler UpdateFailed
        {
            add { lock (objectLock) { updateFailed = value; } }
            remove { lock (objectLock) { updateFailed = null; } }
        }*/
        #endregion
        #region 实现IAppServerFailResponse.查询类命令异常响应
        private QueryWsFailedHandler queryWsFailed = null;
        public event QueryWsFailedHandler QueryWsFailed
        {
            add { lock (objectLock) { queryWsFailed = value; } }
            remove { lock (objectLock) { queryWsFailed = null; } }
        }
        private QueryAllWsFailedHandler queryAllWsFailed = null;
        public event QueryAllWsFailedHandler QueryAllWsFailed
        {
            add { lock (objectLock) { queryAllWsFailed = value; } }
            remove { lock (objectLock) { queryAllWsFailed = null; } }
        }
        #endregion
        #region 实现IAppServerRequestReply.原生类命令异常响应
        private ExchNwIdFailedHandler exchNwIdFailed = null;
        public event ExchNwIdFailedHandler ExchNwIdFailed
        {
            add { lock (objectLock) { exchNwIdFailed = value; } }
            remove { lock (objectLock) { exchNwIdFailed = null; } }
        }
		private SetWgNwIdFailedHandler setWgNwIdFailed = null;
        public event SetWgNwIdFailedHandler SetWgNwIdFailed
        {
            add { lock (objectLock) { setWgNwIdFailed = value; } }
            remove { lock (objectLock) { setWgNwIdFailed = null; } }
        }
        private ExchMtJKFailedHandler exchMtJKFailed = null;
        public event ExchMtJKFailedHandler ExchMtJKFailed
        {
            add { lock (objectLock) { exchMtJKFailed = value; } }
            remove { lock (objectLock) { exchMtJKFailed = null; } }
        }
        #endregion
        #endregion
    }
}