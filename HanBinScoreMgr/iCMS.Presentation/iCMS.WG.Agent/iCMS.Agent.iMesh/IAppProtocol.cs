using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    public delegate void NewMoteJionedHandler(tMAC mac, UInt16 MoteID);
    public delegate void MoteJionedHandler(tMAC mac);
    public delegate void MoteDeletedHandler(tMAC mac, UInt16 MoteID);
    public delegate void MoteLostHandler(tMAC mac);
    public delegate void MoteOperHandler(tMAC mac);
    public delegate void MoteResetHandler(tMAC mac);

    #region WS上报通知定义
    public delegate void WsSelfReportHandler(tSelfReportParam data);
    public delegate void WsHealthReportHandler(tHealthReportParam data);
    public delegate void WsWaveDescHandler(tWaveDescParam data);
    public delegate void WsWaveDataHandler(tWaveDataParam data);
    public delegate void WsEigenDataHandler(tEigenValueParam data);
    public delegate void WsTmpVoltageDataHandler(tTmpVoltageParam data);
    public delegate void WsReqFwDataHandler(tSetFwDataResult data);
    public delegate void WsRevStopHandler(tRevStopParam data);
    public delegate void WsLQHandler(tLQParam data);
    #endregion
    #region 设置类命令结果通知定义
    public delegate void CaliTimeReaultHandler(tCaliTimeResult result);
    public delegate void CaliTimeFailedHandler(tMAC wsMac);
    public delegate void SetWsIDReaultHandler(tSetWsIdResult result);
    public delegate void SetWsIDFailedHandler(tMAC wsMac);
    public delegate void SetWsNwIDReaultHandler(tSetNetworkIdResult result);
    public delegate void SetWsNwIDFailedHandler(tMAC wsMac);
    public delegate void SetMeasDefReaultHandler(tSetMeasDefResult result);
    public delegate void SetMeasDefFailedHandler(tMAC wsMac);
    public delegate void SetWsSnReaultHandler(tSetWsSnResult result);
    public delegate void SetWsSnFailedHandler(tMAC wsMac);
    public delegate void CaliWsSensorReaultHandler(tCaliSensorResult result);
    public delegate void CaliWsSensorFailedHandler(tMAC wsMac);
    public delegate void SetADCloseVoltReaultHandler(tSetADCloseVoltResult result);
    public delegate void SetADCloseVoltFailedHandler(tMAC wsMac);
    public delegate void SetWsStartOrStopReaultHandler(tSetWsStartOrStopResult result);
    public delegate void SetWsStartOrStopFailedHandler(tMAC wsMac);
    public delegate void SetTrigParamResultHandler(tSetTrigParamResult result);
    public delegate void SetTrigParamFailedHandler(tMAC wsMac);
    public delegate void SetWsRouteModeResultHandler(tSetWsRouteModeResult result);
    public delegate void SetWsRouteModeFailedHandler(tMAC wsMac);
    public delegate void SetWsDebugResultHandler(tSetWsDebugModeResult result);
    public delegate void SetWsDebugModeFailedHandler(tMAC wsMac);
    #endregion
    #region 获取类命令结果通知定义
    public delegate void GetSelfReportReaultHandler(tGetSelfReportResult result);
    public delegate void GetSelfReportFailedHandler(tMAC wsMac);
    public delegate void GetMeasDefReaultHandler(tGetMeasDefResult result);
    public delegate void GetMeasDefFailedHandler(tMAC wsMac);
    public delegate void GetWsSnReaultHandler(tGetWsSnResult result);
    public delegate void GetWsSnFailedHandler(tMAC wsMac);
    public delegate void GetSensorCaliReaultHandler(tGetSensorCaliResult result);
    public delegate void GetSensorCaliFailedHandler(tMAC wsMac);
    public delegate void GetADCloseVoltResultHandler(tGetADCloseVoltResult reault);
    public delegate void GetADCloseVoltFailedHandler(tMAC wsMac);
    public delegate void GetRevStopResultHandler(tGetRevStopResult reault);
    public delegate void GetRevStopFailedHandler(tMAC wsMac);
    public delegate void GetTrigParamResultHandler(tGetTrigParamResult reault);
    public delegate void GetTrigParamFailedHandler(tMAC wsMac);
    public delegate void GetWsRouteResultHandler(tGetWsRouteResult reault);
    public delegate void GetWsRouteFailedHandler(tMAC wsMac);
    #endregion
    #region 恢复类命令结果通知定义
    public delegate void RestoreWSReaultHandler(tRestoreWSResult result);
    public delegate void RestoreWSFailedHandler(tMAC wsMac);
    public delegate void RestoreWGReaultHandler(tRestoreWGResult result);
    public delegate void RestoreWGFailedHandler();
    public delegate void ResetWSReaultHandler(tResetWSResult result);
    public delegate void ResetWSFailedHandler(tMAC wsMac);
    public delegate void ResetWGReaultHandler(tResetWGResult result);
    public delegate void ResetWGFailedHandler();
    #endregion
    #region 升级类命令结果通知定义
    public delegate void SetFwDescReaultHandler(tSetFwDescInfoResult result);
    public delegate void SetFwDescFailedHandler(tMAC wsMac);
    public delegate void SetFwDataReaultHandler(tSetFwDataResult result);
    public delegate void SetFwDataFailedHandler(tMAC wsMac);
    public delegate void CtlWsUpstrReaultHandler(tCtlWsUpstrResult result);
    public delegate void CtlWsUpstrFailedHandler(tMAC wsMac);
    public delegate void UpdateFailedHandler(tMAC wsMac);
    public delegate void UpdateSucceedHandler(tMAC wsMac);
    public delegate void UpdateCompleteHandler();
    #endregion
    #region 查询类命令结果通知定义
    public delegate void QueryWsReaultHandler(tGetMoteConfigEcho result);
    public delegate void QueryWsFailedHandler(tMAC wsMac);
    public delegate void QueryAllWsReaultHandler(Dictionary<string, bool> InNetworkWS);
    public delegate void QueryAllWsFailedHandler();
    #endregion
    #region 原生类命令结果通知定义
    public delegate void ExchNwIdReaultHandler();
    public delegate void ExchNwIdFailedHandler();
    public delegate void ExchMtJKReaultHandler(tMAC wsMac);
    public delegate void ExchMtJKFailedHandler(tMAC wsMac);
    public delegate void SetWgNwIdReaultHandler();
    public delegate void SetWgNwIdFailedHandler();
    #endregion

    public interface IAppWsRequest
    {
        event WsSelfReportHandler     WsSelfReportArrived;
        event WsWaveDescHandler       WsWaveDescArrived;
        event WsWaveDataHandler       WsWaveDataArrived;
        event WsEigenDataHandler      WsEigenDataArrived;
        event WsTmpVoltageDataHandler WsTmpVoltageDataArrived;
        event WsRevStopHandler        WsRevStopArrived;
        event WsLQHandler             WsLQArrived;
    }

    public interface IAppServerResponse
    {
        bool ReplySelfReport(tSelfReportResult resault, bool urgent = false);
        bool ReplyWaveDesc(tMeshWaveDescResult resault, bool urgent = false);
        bool ReplyWaveData(tWaveDataResult resault, bool urgent = false);
        bool ReplyEigenValue(tEigenValueResult resault, bool urgent = false);
        bool ReplyTmpVolReport(tTmpVolResult resault, bool urgent = false);
        bool ReplyRevStop(tRevStopResult resault, bool urgent = false);
        bool ReplyLQReport(tLQResult resault, bool urgent = false);
    }

    public interface IAppServerRequest
    {
        #region 设置类命令
        // 网络校时，目前定义暂不需要响应
        bool CalibrateTime(tCaliTimeParam param, bool urgent = false);
        // 设置WSid
        bool SetWsID(tSetWsIdParam param, bool urgent = false);
        // 设置网络ID
        bool SetNetworkID(tSetNetworkIdParam param, bool urgent = false);
        // 更新网络测量定义
        bool SetMeasDef(tSetMeasDefParam param, bool urgent = false);
        // 更新WS序列码
        bool SetWsSn(tSetWsSnParam param, bool urgent = false);
        // 校正振动传感器
        bool CalibrateWsSensor(tCaliSensorParam param, bool urgent = false);
        // 配置AD采集截止电压
        bool SetADCloseVolt(tSetADCloseVoltParam param, bool urgent = false);
        // 配置WS进入启停机状态
        bool SetWsStartOrStop(tSetWsStateParam param, bool urgent = false);
        // 配置触发式上传参数
        bool SetTrigParam(tSetTrigParam param, bool urgent = false);
        // 配置路由节点模式
        bool SetWsRouteMode(tSetWsRouteMode param, bool urgent = false);
        // 配置DEBUG模式
        bool SetWsDebugMode(tSetWsDebugMode param, bool urgent = false);
        #endregion
        #region 获取类命令
        // 获取WS自描述报告
        bool GetSelfReport(tGetSelfReportParam param, bool urgent = false);
        // 获取WS测量定义
        bool GetMeasDef(tGetMeasDefParam param, bool urgent = false);
        // 获取WS序列码
        bool GetWsSn(tGetWsSnParam param, bool urgent = false);
        // 获取校准系数，为出现此函数
        bool GetSensorCaliCoeff(tGetCaliCoeffParam param, bool urgent = false);
        // 获取AD采集截止电压
        bool GetADCloseVolt(tGetADCloseVoltParam param, bool urgent = false);
        // 获取WS进入启停机状态
        bool GetWsStartOrStop(tGetWsStartOrStopParam param, bool urgent = false);
        // 获取触发式上传参数
        bool GetTrigParam(tGetTrigParam param, bool urgent = false);
        // 获取路由节点模式
        bool GetWsRouteMode(tGetWsRouteMode param, bool urgent = false);
        #endregion
        #region 恢复类命令
        // 恢复WS默认出厂设置
        bool RestoreWS(tRestoreWSParam param, bool urgent = false);
        // 恢复WG默认出厂设置
        bool RestoreWG(bool urgent = false);
        // 重启WS
        bool ResetWS(tResetWSParam param, bool urgent = false);
        // 重启WG
        bool ResetWG(bool urgent = false);
        #endregion
        #region 升级类命令
        // 更新所有WS固件描述信息
        bool SetFwDescInfo(tSetFwDescInfoParam param, bool urgent = false);
        // 更新所有WS固件数据
        bool SetFwData(tSetFwDataParam param, bool urgent = false);
        // 控制ws进入/退出升级周期，此方法应该对上层隐藏
        bool CtlWsUpstr(tCtlWsUpstrParam param, bool urgent = false);
        #endregion
        #region 查询类命令
        // 查询WS
        bool QueryWs(tMAC param, bool urgent = false);
        // 查询所有WS
        bool QueryAllWs(bool urgent = false);
        #endregion
    }

    public interface IAppServerRequestReply
    {
        #region 设置类命令正常响应
        event CaliTimeReaultHandler CaliTimeReaultArrived;
        event SetWsIDReaultHandler SetWsIDReaultArrived;
        event SetWsNwIDReaultHandler SetNetworkIDReaultArrived;
        event SetMeasDefReaultHandler SetMeasDefReaultArrived;
        event SetWsSnReaultHandler SetWsSnReaultArrived;
        event CaliWsSensorReaultHandler CaliWsSensorReaultArrived;
        event SetADCloseVoltReaultHandler SetADCloseVoltReaultArrived;
        event SetWsStartOrStopReaultHandler SetWsStartOrStopReaultArrived;
        event SetTrigParamResultHandler SetTrigParamResultArrived;
        event SetWsRouteModeResultHandler SetWsRouteModeResultArrived;
        #endregion
        #region 获取类命令正常响应
        event GetSelfReportReaultHandler GetSelfReportReaultArrived;
        event GetMeasDefReaultHandler GetMeasDefReaultArrived;
        event GetWsSnReaultHandler GetWsSnReaultArrived;
        event GetSensorCaliReaultHandler GetSensorCaliReaultArrived;
        event GetADCloseVoltResultHandler GetADCloseVoltResultArrived;
        event GetRevStopResultHandler GetRevStopResultArrived;
        event GetTrigParamResultHandler GetTrigParamResultArrived;
        event GetWsRouteResultHandler GetWsRouteResultArrived;
        #endregion
        #region 恢复类命令正常响应
        event RestoreWSReaultHandler RestoreWSReaultArrived;
        event RestoreWGReaultHandler RestoreWGReaultArrived;
        event ResetWSReaultHandler ResetWSReaultArrived;
        event ResetWGReaultHandler ResetWGReaultArrived;
        #endregion
        #region 升级类命令正常响应
        event SetFwDescReaultHandler SetFwDescReaultArrived;
        event SetFwDataReaultHandler SetFwDataReaultArrived;
        #endregion
        #region 查询类命令正常响应
        event QueryWsReaultHandler QueryWsReaultArrived;
        event QueryAllWsReaultHandler QueryAllWsReaultArrived;
        #endregion
        #region 原生类命令正常响应
        event ExchNwIdReaultHandler ExchangeNetworkIdReplyArrived;
        event SetWgNwIdReaultHandler SetWgNwIdReaultArrived;
        event ExchMtJKReaultHandler ExchangeMoteJoinKeyReplyArrived;
        #endregion
    }

    public interface IAppServerRequestFail
    {
        #region 设置类命令异常响应
        event CaliTimeFailedHandler         CaliTimeFailed;
        event SetWsIDFailedHandler          SetWsIDFailed;
        event SetWsNwIDFailedHandler        SetWsNwIDFailed;
        event SetMeasDefFailedHandler       SetMeasDefFailed;
        event SetWsSnFailedHandler          SetWsSnFailed;
        event CaliWsSensorFailedHandler     CaliWsSensorFailed;
        event SetADCloseVoltFailedHandler   SetADCloseVoltFailed;
        event SetWsStartOrStopFailedHandler SetWsStartOrStopFailed;
        event SetTrigParamFailedHandler     SetTrigParamFailed;
        event SetWsRouteModeFailedHandler   SetWsRouteModeFailed;
        #endregion
        #region 获取类命令异常响应
        event GetSelfReportFailedHandler    GetSelfReportFailed;
        event GetMeasDefFailedHandler       GetMeasDefFailed;
        event GetWsSnFailedHandler          GetWsSnFailed;
        event GetSensorCaliFailedHandler    GetSensorCaliFailed;
        event GetADCloseVoltFailedHandler   GetADCloseVoltFailed;
        event GetRevStopFailedHandler       GetRevStopFailed;
        event GetTrigParamFailedHandler     GetTrigParamFailed;
        event GetWsRouteFailedHandler       GetWsRouteFailed;
        #endregion
        #region 恢复类命令异常响应
        event RestoreWSFailedHandler        RestoreWSFailed;
        event RestoreWGFailedHandler        RestoreWGFailed;
        event ResetWSFailedHandler          ResetWSFailed;
        event ResetWGFailedHandler          ResetWGFailed;
        #endregion
        #region 升级类命令异常响应
        event SetFwDescFailedHandler        SetFwDescFailed;
        event SetFwDataFailedHandler        SetFwDataFailed;
        #endregion
        #region 查询类命令异常响应
        event QueryWsFailedHandler QueryWsFailed;
        event QueryAllWsFailedHandler QueryAllWsFailed;
        #endregion
        #region 原生类命令正常响应
        event ExchNwIdFailedHandler ExchNwIdFailed;
        event SetWgNwIdFailedHandler SetWgNwIdFailed;
        event ExchMtJKFailedHandler ExchMtJKFailed;
        #endregion
    }
}
