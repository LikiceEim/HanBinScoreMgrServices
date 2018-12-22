/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent
 *文件名：  CommunicationWithMesh
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：iCMS.WG.Agent与iCMS.Mesh通讯主类
 *=====================================================================**/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

using iMesh;

using iCMS.WG.Agent.Model.Send;
using iCMS.WG.Agent.Model.Receive;
using iCMS.WG.Agent.Common.Enum;
using iCMS.Common.Component.Tool;
using iCMS.WG.Agent.Common;
using iCMS.WG.Agent.Model;

namespace iCMS.WG.Agent
{
    /// <summary>
    /// 通讯，包括接受底层Mesh网络的通讯及与iCMS.Server的通讯
    /// </summary>
    public class CommunicationWithMesh
    {
        //采集数据的集合列表
        public List<WSWaveInfo> listWaveInfo = new List<WSWaveInfo>();

        //与iCMS.Server通讯类
        private CommunicationWithServer communication2Server;

        System.Timers.Timer timerGetData;

        List<string> ListHasJoin = new List<string>();
        #region 启动Agent与Mesh网络的通讯
        /// <summary>
        /// 启动Agent
        /// </summary>
        public void Run()
        {
            try
            {
                if (communication2Server != null && iCMS.WG.Agent.ComFunction.meshAdapter != null)
                    throw new Exception("Agent 已经启动，不可重复启动！");
                communication2Server = new CommunicationWithServer();
                iCMS.WG.Agent.ComFunction.InitLogType();
                ///启动检查采集数据的完整性
                //System.Threading.Tasks.Task.Run(() => ThreadCheckWaveIsFull());

                iCMS.WG.Agent.ComFunction.meshAdapter = new MeshAdapter();
                iCMS.WG.Agent.ComFunction.meshAdapter.Initialize();

                ///初始化WS/WG状态列表
                try
                {
                    ///配置文件标识由Server获取初始数据

                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), " 开始从Server获取数据");
                    iCMS.WG.Agent.ComFunction.InitDeviceStatusFromServer();
                    iCMS.WG.Agent.ComFunction.getData = true;
                }
                catch
                {
                    iCMS.WG.Agent.Common.Interop.ShowMessageBox("iCMS.Server没有启动或发生错误，详细请查看日志处理", "iCMS.WG.Agent警告");
                    iCMS.WG.Agent.ComFunction.getData = false;
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "iCMS.Server沒有启动或发生错误，5秒后重试");
                    timerGetData = new System.Timers.Timer();
                    timerGetData.Enabled = true;
                    timerGetData.Interval = 5000;//执行间隔时间,单位为毫秒

                    timerGetData.Elapsed += new System.Timers.ElapsedEventHandler(GetDataFromServerReTry);
                    timerGetData.Start();
                }
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "调用底层（GetMoteConfig），获取WS真实状态");
                QueryAllWSFromManager();
                //GetWSNStatusFromManager(iCMS.WG.Agent.Common.CommonConst.RequestGetMoteConfig);
                //iCMS.WG.Agent.ComFunction.getConfigMacCurr = iCMS.WG.Agent.Common.CommonConst.RequestGetMoteConfig;
                GetNetWorkIDFromManager();

                //System.Timers.Timer timerQueryWs = new System.Timers.Timer();
                //timerQueryWs.Enabled = true;
                //timerQueryWs.Interval = 1000*60*60;//执行间隔时间,单位为毫秒

                //timerQueryWs.Elapsed += new System.Timers.ElapsedEventHandler(QueryWSFromManager);
                //timerQueryWs.Start();
                #region 注册底层数据到达事件

                //波形描述信息
                iCMS.WG.Agent.ComFunction.meshAdapter.WsWaveDescArrived += WaveDescInfoReceived;
                //波形数据
                iCMS.WG.Agent.ComFunction.meshAdapter.WsWaveDataArrived += WaveDataInfoReceived;
                //温度、电压信息
                iCMS.WG.Agent.ComFunction.meshAdapter.WsTmpVoltageDataArrived += TempAndVolReceived;
                //WS自报告
                iCMS.WG.Agent.ComFunction.meshAdapter.WsSelfReportArrived += WSSelfReportReceived;
                //特征值
                iCMS.WG.Agent.ComFunction.meshAdapter.WsEigenDataArrived += CharacterValueReceived;

                //APP操作WS后的响应
                //下发测量定义
                iCMS.WG.Agent.ComFunction.meshAdapter.SetMeasDefReaultArrived += ConfigMeasDefineResultReceived;
                //下发测量定义超时
                iCMS.WG.Agent.ComFunction.meshAdapter.SetMeasDefFailed += MeasDefineFailedReceived;
               
                //通知上层某一个WS开始升级
                iCMS.WG.Agent.ComFunction.meshAdapter.UpdatingWs += meshAdapter_UpdatingWs;
                //通知上层某一个WS升级成功
                iCMS.WG.Agent.ComFunction.meshAdapter.UpdatedWsSucess += meshAdapter_UpdatedWsSucess;
                //通知上层某一个WS升级失败
                iCMS.WG.Agent.ComFunction.meshAdapter.UpdatedWsFailed += meshAdapter_UpdatedWsFailed;
                //通知上层所有的WS升级完成
                iCMS.WG.Agent.ComFunction.meshAdapter.UpdateFinished += meshAdapter_UpdateFinished;
                
                //配置WSID
                iCMS.WG.Agent.ComFunction.meshAdapter.SetWsIDReaultArrived += SetNewWSIDResultReceived;

                //配置参数NetworkID
                iCMS.WG.Agent.ComFunction.meshAdapter.SetNetworkIDReaultArrived += SetWS_NetWorkIDResultReceived;

                //配置参数SN串码
                iCMS.WG.Agent.ComFunction.meshAdapter.SetWsSnReaultArrived += SetSNCodeResultReceived;

                //配置参数传感器校准
                iCMS.WG.Agent.ComFunction.meshAdapter.CaliWsSensorReaultArrived += CheckSensorResultReceived;

                //获取SN串码
                iCMS.WG.Agent.ComFunction.meshAdapter.GetWsSnReaultArrived += GetSNCodeResultReceived;

                //恢复WG出厂设置
                iCMS.WG.Agent.ComFunction.meshAdapter.RestoreWGReaultArrived += RestoreWGResultReceived;

                //恢复WS出厂设置
                iCMS.WG.Agent.ComFunction.meshAdapter.RestoreWSReaultArrived += RestoreWSResultReceived;

                //重启WG
                iCMS.WG.Agent.ComFunction.meshAdapter.ResetWGReaultArrived += ReSetWGResultReceived;

                //重启WS
                iCMS.WG.Agent.ComFunction.meshAdapter.ResetWSReaultArrived += ReSetWSResultReceived;

                //WG断开
                iCMS.WG.Agent.ComFunction.meshAdapter.ManagerLost += WGLostReceived;

                //Manager Rest成功
                iCMS.WG.Agent.ComFunction.meshAdapter.ManagerReset += meshAdapter_ManagerReset;

                //WG连接
                iCMS.WG.Agent.ComFunction.meshAdapter.ManagerConnected += WGConnectedReceived;

                //WS断开
                iCMS.WG.Agent.ComFunction.meshAdapter.WsLost += WSLostReceived;

                //WS连接
                iCMS.WG.Agent.ComFunction.meshAdapter.WsConnected += WsConnectReceived;

                //获取WS真实状态
                // iCMS.WG.Agent.ComFunction.meshAdapter.QueryWsReaultArrived += WsStatusReaultArrived;             

                //获取所有WS的状态
                iCMS.WG.Agent.ComFunction.meshAdapter.QueryAllWsReaultArrived += AllWsStatusReaultArrived;

                //WS上传启停机状态
                iCMS.WG.Agent.ComFunction.meshAdapter.WsRevStopArrived += WsRevStopReaultArrived;

                //WS上传LQ
                iCMS.WG.Agent.ComFunction.meshAdapter.WsLQArrived += WsLQReaultArrived;

                //触发式上传的响应
                iCMS.WG.Agent.ComFunction.meshAdapter.SetTrigParamResultArrived += SetWS_TrigParamResultArrived;

                //触发式上传的失败回复
                iCMS.WG.Agent.ComFunction.meshAdapter.SetTrigParamFailed += SetTrigParamFailedReceive;

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("构建网络通讯失败，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
            }
        }

        public void Stop()
        {
            iCMS.WG.Agent.Common.Interop.ShowMessageBox("停止iCMS.WG.Agent，所有正在进行的操作将全部失效/失败", "iCMS.WG.Agent警告");
            if (iCMS.WG.Agent.ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Count > 0)
            {
                lock (ComFunction.WSUpdatingInfo)
                {
                    //停止正在升级的WS定时器
                    foreach (var item in ComFunction.WSUpdatingInfo.UpdatingAllWSInfo)
                    {
                        if (item.Value != null)
                        {
                            item.Value.Stop();
                            item.Value.Close();
                            item.Value.Dispose();
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "(" + item.Key + ")" + " 升级已超时，清除升级信息");
                            //通知WS升级结果
                            communication2Server.UploadConfigResponse(item.Key, Enum_ConfigType.ConfigType_UpdateFirmware, Convert.ToInt32(EnmuReceiveStatus.Unaccept), "iCMS.WG.Agent被终止，退出对该WS的升级");
                        }
                    }
                    //清除正在升级的WS信息
                    ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Clear();
                    //清除总的定时器
                    if (ComFunction.WSUpdatingInfo.UpdateAllWSTimer != null)
                    {
                        ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Stop();
                        ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Close();
                        ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Dispose();
                        ComFunction.WSUpdatingInfo.UpdateAllWSTimer = null;
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "清除设置的总的超时定时器！");
                    }
                } 
                iCMS.WG.Agent.ComFunction.meshAdapter.PostludeUpdate();
            }
            communication2Server = null;
            iCMS.WG.Agent.ComFunction.sendMeasureDefineList.Clear();
            iCMS.WG.Agent.ComFunction.setNetworkIDList.Clear();
            iCMS.WG.Agent.ComFunction.setNewWSIDList.Clear();
            iCMS.WG.Agent.ComFunction.setSNCodeList.Clear();
            iCMS.WG.Agent.ComFunction.meshAdapter.Dispose();
            iCMS.WG.Agent.ComFunction.meshAdapter = null;
        }
        #endregion

        #region iCMS.Server 通讯异常再次尝试
        private void GetDataFromServerReTry(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Threading.Tasks.Task.Run(() =>
             {
                 try
                 {
                     iCMS.WG.Agent.ComFunction.InitDeviceStatusFromServer();
                     iCMS.WG.Agent.ComFunction.getData = true;
                     timerGetData.Stop();
                 }
                 catch
                 {
                     iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "iCMS.Server沒有启动或发生错误，5秒后重试");
                 }
             });
        }
        #endregion

        #region 向Manager 请求所有网络中的WS真是状态
        private void QueryAllWSFromManager()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    ListHasJoin.Clear();
                    iCMS.WG.Agent.ComFunction.meshAdapter.QueryAllWs();
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), ex.Message + "\r\n" + ex.StackTrace);
                }
            });
        }
        #endregion

        #region WS主动发起的请求

        #region  WS自报告
        /// <summary>
        /// WS自报告
        /// </summary>
        /// <param name="_WSSelfReport"></param>
        private void WSSelfReportReceived(tSelfReportParam _WSSelfReport)
        {
            //WS自报告
            System.Threading.Tasks.Task.Run(() =>
            {
                //iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(_WSSelfReport.mac.ToHexString().ToUpper());

                ResponseWSSelfReport(_WSSelfReport);
            });
        }
        #endregion

        #region  WS断开
        /// <summary>
        /// WS自报告
        /// </summary>
        /// <param name="_WSSelfReport"></param>
        private void WSLostReceived(tMAC _tMAC)
        {
            //WS断开
            System.Threading.Tasks.Task.Run(() =>
            {
                iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(_tMAC.ToHexString().ToUpper(), EnumWSStatus.WSOff);

                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " WS 断开 " + _tMAC.ToHexString());
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "上传 WS 状态 " + _tMAC.ToHexString());
                communication2Server.UploadWSStatus(_tMAC.ToHexString().ToUpper(), "", EnumWSStatus.WSOff);
            });
        }
        #endregion

        #region  WS连接
        /// <summary>
        /// WS连接的通知
        /// </summary>
        private void WsConnectReceived(tMAC _tMAC)
        {
            //WS连接
            System.Threading.Tasks.Task.Run(() =>
            {
                iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(_tMAC.ToHexString().ToUpper(), EnumWSStatus.WSOn);

                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " WS 连接 " + _tMAC.ToHexString());
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "上传 WS 状态 " + _tMAC.ToHexString());
                communication2Server.UploadWSStatus(_tMAC.ToHexString().ToUpper(), "", EnumWSStatus.WSOn);
            });
        }
        #endregion

        #region  WG断开
        /// <summary>
        /// WG 断开
        /// </summary>
        /// <param name="_WSSelfReport"></param>
        private void WGLostReceived()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " WG 断开 ID " + iCMS.WG.Agent.ComFunction.GetAppConfig("WGID").ToString());

                iCMS.WG.Agent.ComFunction.UpdateConnectionWGStatu(iCMS.WG.Agent.ComFunction.GetAppConfig("WGID").ToString(), ((int)iCMS.WG.Agent.Common.Enum.WGStatus.WGOff).ToString());
                communication2Server.UploadWGStatus(iCMS.WG.Agent.ComFunction.GetAppConfig("WGID").ToString(), WGStatus.WGOff);
            });
        }
        #endregion

        #region  WG连接
        /// <summary>
        /// WG 连接
        /// </summary>
        /// <param name="_WSSelfReport"></param>
        private void WGConnectedReceived()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " WG 连接 ID " + iCMS.WG.Agent.ComFunction.GetAppConfig("WGID").ToString());

                iCMS.WG.Agent.ComFunction.UpdateConnectionWGStatu(iCMS.WG.Agent.ComFunction.GetAppConfig("WGID").ToString(), ((int)iCMS.WG.Agent.Common.Enum.WGStatus.WGOn).ToString());
                communication2Server.UploadWGStatus(iCMS.WG.Agent.ComFunction.GetAppConfig("WGID").ToString(), WGStatus.WGOn);
            });
        }
        #endregion

        #region 波形信息
        /// <summary>
        /// 接收到波形信息
        /// </summary>
        /// <param name="_WaveDescInfo"></param>
        private void WaveDescInfoReceived(tWaveDescParam _WaveDescInfo)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                //iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(_WaveDescInfo.mac.ToHexString().ToUpper());
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " WS 上传波形描述信息,长度： " + _WaveDescInfo.u16TotalFramesNum + " MAC: " + _WaveDescInfo.mac.ToHexString() + " 类型：" + GetWaveTypeString.GetString((int)_WaveDescInfo.MeasDef.MeasDefType));
                ResponseWaveDescInfo(_WaveDescInfo);
            });
        }
        #endregion

        #region 接收到波形数据
        /// <summary>
        /// 接收到波形数据
        /// </summary>
        /// <param name="_WaveDataInfo"></param>
        private void WaveDataInfoReceived(tWaveDataParam _WaveDataInfo)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                //iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(_WaveDataInfo.mac.ToHexString().ToUpper());

                ResponseWaveDataInfo(_WaveDataInfo);
            });
        }
        #endregion

        #region 特征值
        /// <summary>
        /// 接收到特征值
        /// </summary>
        /// <param name="_WSCharacterValue"></param>
        private void CharacterValueReceived(tEigenValueParam _WSCharacterValue)
        {
            System.Threading.Tasks.Task.Run(() =>
           {
               //iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(_WSCharacterValue.mac.ToHexString().ToUpper());

               //iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), iCMS.WG.Agent.ComFunction.GetJson(_WSCharacterValue));

               iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 接收特征值 " + _WSCharacterValue.mac.ToHexString());
               string macStr = _WSCharacterValue.mac.ToHexString().ToUpper();
               tEigenValueResult result = new tEigenValueResult();
               result.mac = new tMAC(macStr);
               result.u16RC = 0X00;

               iCMS.WG.Agent.ComFunction.Send2WS(result, EnumRequestWSType.ReplyEigenValue);
               iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 响应特征值 " + _WSCharacterValue.mac.ToHexString());

               iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 上传特征值 " + _WSCharacterValue.mac.ToHexString());

               communication2Server.UploadVibrationValue(_WSCharacterValue);              
           });
        }
        #endregion

        #region WS 上传启停机状态
        private void WsRevStopReaultArrived(tRevStopParam param)
        {
            System.Threading.Tasks.Task.Run(() =>
           {
               //iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(param.mac.ToHexString().ToUpper());
               iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 接收启停机特征值 " + param.mac.ToHexString());

               string macStr = param.mac.ToHexString().ToUpper();
               tRevStopResult result = new tRevStopResult();
               result.mac = new tMAC(macStr);
               result.u16RC = 0X00;

               iCMS.WG.Agent.ComFunction.Send2WS(result, EnumRequestWSType.ReplyRevStop);
               iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 响应启停机特征值 " + param.mac.ToHexString());

               communication2Server.UploadStatusCritical(param);
               iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 上传启停机特征值 " + param.mac.ToHexString());
           });
        }

        #endregion

        #region WS 上传LQ
        private void WsLQReaultArrived(tLQParam param)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                //iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(param.mac.ToHexString().ToUpper());
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 接收LQ特征值 " + param.mac.ToHexString());

                string macStr = param.mac.ToHexString().ToUpper();
                tLQResult result = new tLQResult();
                result.mac = new tMAC(macStr);
                result.u16RC = 0X00;

                iCMS.WG.Agent.ComFunction.Send2WS(result, EnumRequestWSType.ReplyLQ);
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 响应LQ特征值 " + param.mac.ToHexString());

                communication2Server.UploadLQValue(param);
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 上传LQ特征值 " + param.mac.ToHexString());
            });
        }
        #endregion

        #region 接收到温度、电压信息
        /// <summary>
        /// 接收到温度、电压信息
        /// </summary>
        /// <param name="_WSTemperature"></param>
        private void TempAndVolReceived(tTmpVoltageParam _WSTemperature)
        {
            System.Threading.Tasks.Task.Run(() =>
          {
              //iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(_WSTemperature.mac.ToHexString().ToUpper());

              iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 接收温度电压 " + _WSTemperature.mac.ToHexString());
              string macStr = _WSTemperature.mac.ToHexString().ToUpper();

              tTmpVolResult result = new tTmpVolResult();
              result.mac = new tMAC(macStr);
              result.u16RC = 0X00;

              iCMS.WG.Agent.ComFunction.Send2WS(result, EnumRequestWSType.ReplyTmpVolReport);
              iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 响应接收温度电压 " + _WSTemperature.mac.ToHexString());

              iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 上传温度电压 " + _WSTemperature.mac.ToHexString());
              communication2Server.UploadTempAndVol(_WSTemperature);
          });
        }
        #endregion

        #region

        private void meshAdapter_ManagerReset()
        {
            if (iCMS.WG.Agent.ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Count > 0 && !iCMS.WG.Agent.ComFunction.reSetSuccesfulForUpdate)
            {
                iCMS.WG.Agent.ComFunction.reSetSuccesfulForUpdate = true;

                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "网络重启成功");
            }
        }
        #endregion

        #region 响应WG报告所属WS状况
        /// <summary>
        /// 响应WG报告
        /// </summary>
        private void ResponseWGReport()//(ReceiveMessage_WGReport report)
        {
            try
            {
                //iCMS.WG.Agent.ComFunction.UpdateConnectionWGStatu(iCMS.WG.Agent.ComFunction.GetWGID(report.WSNO.ToString()).ToString(), report.WSStates.ToString());
                //iCMS.WG.Agent.ComFunction.UpdateConnectionWSStatu(report.WSNO.ToString(), report.WSStates.ToString());
                ////communication2Server.UploadWSStatus(receiveData, "WGReport");
                ////取得下一个WS状态
                //iCMS.WG.Agent.ComFunction.GetBytesMACFromString(report.MAC);
            }
            catch
            {

            }
        }
        #endregion

        #region WS  2  Agent 时 Agent的处理函数

        #region 波形处理
        /// <summary>
        /// 波形描述信息
        /// </summary>
        /// <param name="messageData"></param>
        /// <param name="receiveData"></param>
        private void ResponseWaveDescInfo(tWaveDescParam waveDescinfo)
        {
            try
            {
                WSWaveInfo waveInfo = null;
                //DeviceStatus obj = iCMS.WG.Agent.ComFunction.GetDeviceStatusByMac(waveDescinfo.mac.ToHexString());
                lock (listWaveInfo)
                {
                    int index = -1;
                    bool isHas = iCMS.WG.Agent.ComFunction.CheckObjectInArray(listWaveInfo, waveDescinfo.mac.ToHexString().ToUpper(), (EnumWaveType)waveDescinfo.MeasDef.MeasDefType, ref index);
                    if (!isHas)
                    {
                        waveInfo = new WSWaveInfo()
                        {
                            WGNO = int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("WGID")),//int.Parse(obj.WGID),
                            // WSNO = int.Parse(obj.WSID),
                            IsFull = false,
                            WaveData = new byte[waveDescinfo.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage],
                            WaveType = (EnumWaveType)waveDescinfo.MeasDef.MeasDefType,
                            LastReceiveTime = DateTime.Now,
                            ReceiveDataNumber = new int[waveDescinfo.u16TotalFramesNum + 1],
                            CurrentNoDataNumber = -1,
                            RepeatNumber = 0,
                            IsRepeat = false,

                            WaveDescInfo = null,

                            MAC = waveDescinfo.mac.ToHexString().ToUpper(),
                            checkFullTimer = null
                        };

                        //标记波形信息接收状态
                        waveInfo.ReceiveDataNumber[0] = 1;
                        listWaveInfo.Add(waveInfo);
                    }
                    else
                    {
                        if (index < 0)
                            return;
                        try
                        {
                            listWaveInfo[index].checkFullTimer.Dispose();
                            listWaveInfo[index].checkFullTimer = null;
                        }
                        catch
                        {
                        }
                        if (listWaveInfo[index].IsFull == true)
                        {
                            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("isInvalidData"))
                                if ((System.DateTime.Now - listWaveInfo[index].UpLoadDataTime).TotalMilliseconds <= int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("isInvalidData")))
                                {
                                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.InvalidData.ToString(), "无效波形描述信息，MAC " + waveDescinfo.mac.ToHexString());
                                    return;
                                }

                            listWaveInfo[index].IsFull = false;
                            listWaveInfo[index].WaveData = new byte[waveDescinfo.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage];
                            listWaveInfo[index].ReceiveDataNumber = new int[waveDescinfo.u16TotalFramesNum + 1];
                        }
                        listWaveInfo[index].LastReceiveTime = DateTime.Now;
                        listWaveInfo[index].RepeatNumber = 0;
                        listWaveInfo[index].CurrentNoDataNumber = -1;

                        //标记波形信息接收状态
                        listWaveInfo[index].ReceiveDataNumber[0] = 1;

                        waveInfo = listWaveInfo[index];
                    }
                }
                if (waveInfo.WaveDescInfo == null)
                {
                    waveInfo.WaveDescInfo = new WaveInformation()
                    {
                        //WSID = int.Parse(obj.WSID),
                        SamplingTime = iCMS.WG.Agent.ComFunction.GetSamplingTime(waveDescinfo.DaqTime),
                        DAQStyle = (int)waveDescinfo.DaqMode,
                        WaveLength = waveDescinfo.u16TotalFramesNum,
                        UpperLimit = (int)waveDescinfo.MeasDef.u16UpperFreq,
                        LowerLimit = (int)waveDescinfo.MeasDef.u16LowFreq,
                        AmplitueScaler = waveDescinfo.f32AmpScaler,

                        TotalWaveNum = waveDescinfo.u16TotalFramesNum,
                    };
                }
                else
                {
                    if (waveInfo.WaveDescInfo.SamplingTime != iCMS.WG.Agent.ComFunction.GetSamplingTime(waveDescinfo.DaqTime))
                    {
                        waveInfo.WaveDescInfo.SamplingTime = iCMS.WG.Agent.ComFunction.GetSamplingTime(waveDescinfo.DaqTime);
                    }
                }

                if (waveDescinfo.MeasDef.bWaveRMSValid != 0)
                {
                    waveInfo.WaveDescInfo.RMS = waveDescinfo.f32RMS;
                }
                else
                {
                    waveInfo.WaveDescInfo.RMS = null;
                }
                if (waveDescinfo.MeasDef.bWavePKValid != 0)
                {
                    waveInfo.WaveDescInfo.PK = waveDescinfo.f32PK;
                }
                else
                {
                    waveInfo.WaveDescInfo.PK = null;
                }

                if (waveDescinfo.MeasDef.bWavePKPKValid != 0)
                {
                    waveInfo.WaveDescInfo.PPK = waveDescinfo.f32PKPK;
                }
                else
                {
                    waveInfo.WaveDescInfo.PPK = null;
                }

                if (waveDescinfo.MeasDef.bWavePKCValid != 0)
                {
                    waveInfo.WaveDescInfo.GPKC = waveDescinfo.f32PKC;
                }
                else
                {
                    waveInfo.WaveDescInfo.GPKC = null;
                }
                waveInfo.checkFullTimer = new Timer(new TimerCallback(TimerCheckWaveIsFull), waveDescinfo.mac.ToHexString() + "|" + (int)waveInfo.WaveType, int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("CheckFullTime")), -1);

                //判断波形完整，并保存

                if (iCMS.WG.Agent.ComFunction.CheckDataIsFull(waveInfo))
                {
                    waveInfo.UpLoadDataTime = System.DateTime.Now;
                    try
                    {
                        waveInfo.checkFullTimer.Dispose();
                        waveInfo.checkFullTimer = null;
                    }
                    catch
                    {
                    }
                    tWaveDataResult result = new tWaveDataResult();
                    result.mac = waveDescinfo.mac;
                    result.u16aRC = new UInt16[] { 65535 };

                    iCMS.WG.Agent.ComFunction.Send2WS(result, EnumRequestWSType.ReplyWaveData);
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " WS 波形数据接收完整 " + waveDescinfo.mac.ToHexString() + " 类型： " + GetWaveTypeString.GetString((int)waveInfo.WaveType));

                    int length = 0;
                    if ((waveDescinfo.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage) % 1024 == 0)
                    {
                        length = (waveDescinfo.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage);
                    }
                    else
                    {
                        length = (waveDescinfo.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage) - Common.CommonConst.Wave_Length_InMessage + waveInfo.LastWaveLength;
                    }
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 上传波形数据 " + waveDescinfo.mac.ToHexString() + " 类型： " + GetWaveTypeString.GetString((int)waveInfo.WaveType));

                    //调用上层，上传数据
                    communication2Server.UploadVibrationWave(waveInfo.Clone() as WSWaveInfo, length);
                    lock (waveInfo)
                    {
                        ResetWaveInfo(waveInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithMesh.ResponseWaveDescInfo ，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
            }
        }

        /// <summary>
        /// 波形数据
        /// </summary>
        /// <param name="messageData"></param>
        /// <param name="receiveData"></param>
        private void ResponseWaveDataInfo(tWaveDataParam recWave)
        {
            //最后一条报文中波形数据的长度
            try
            {
                //DeviceStatus obj = iCMS.WG.Agent.ComFunction.GetDeviceStatusByMac(recWave.mac.ToString());
                if ((int)recWave.WaveType < (int)EnumWaveType.EnumWaveType_AcceleratedSpeed
                    || (int)recWave.WaveType > (int)EnumWaveType.EnumWaveType_LQ || recWave.u16CurrentFrameID <= 0)
                {
                    //波形数据类型不满足条件，不是指定的波形
                    return;
                }
                int index = -1;
                WSWaveInfo waveInfo = null;

                lock (listWaveInfo)
                {
                    bool isHas = iCMS.WG.Agent.ComFunction.CheckObjectInArray(listWaveInfo, recWave.mac.ToHexString().ToUpper(), (EnumWaveType)recWave.WaveType, ref index);
                    if (!isHas)
                    {
                        waveInfo = new WSWaveInfo()
                        {
                            WGNO = int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("WGID")),
                            // WSNO = int.Parse(obj.WSID),
                            IsFull = false,
                            WaveData = new byte[recWave.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage],
                            WaveType = (EnumWaveType)recWave.WaveType,
                            LastReceiveTime = DateTime.Now,
                            ReceiveDataNumber = new int[recWave.u16TotalFramesNum + 1],
                            CurrentNoDataNumber = -1,
                            RepeatNumber = 0,
                            IsRepeat = false,

                            WaveDescInfo = null,
                            retry = false,
                            MAC = recWave.mac.ToHexString().ToUpper(),
                            checkFullTimer = null
                        };
                        listWaveInfo.Add(waveInfo);
                    }
                    else
                    {
                        try
                        {
                            listWaveInfo[index].checkFullTimer.Dispose();
                            listWaveInfo[index].checkFullTimer = null;
                        }
                        catch
                        {
                            listWaveInfo[index].checkFullTimer = null;
                        }
                        if (listWaveInfo[index].IsFull == true)
                        {
                            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("isInvalidData"))
                                if ((System.DateTime.Now - listWaveInfo[index].UpLoadDataTime).TotalMilliseconds <= int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("isInvalidData")))
                                {
                                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.InvalidData.ToString(), " 波形数据已经接收完成， 类型： " + GetWaveTypeString.GetString((int)recWave.WaveType) + " MAC " + recWave.mac.ToHexString() + " 重复发送了：" + recWave.u16CurrentFrameID);
                                    return;
                                }
                            listWaveInfo[index].retry = false;
                            listWaveInfo[index].IsFull = false;
                            listWaveInfo[index].WaveData = new byte[recWave.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage];
                            listWaveInfo[index].ReceiveDataNumber = new int[recWave.u16TotalFramesNum + 1];
                        }
                        listWaveInfo[index].LastReceiveTime = DateTime.Now;
                        listWaveInfo[index].RepeatNumber = 0;
                        listWaveInfo[index].CurrentNoDataNumber = -1;

                        waveInfo = listWaveInfo[index];
                    }
                }

                lock (waveInfo)
                {
                    //if (recWave.u16CurrentFrameID == 20 && !waveInfo.retry)//
                    //{
                    //    waveInfo.retry = true;
                    //    return;
                    //}

                    //标记序号为current - 1的数据已收到
                    waveInfo.ReceiveDataNumber[recWave.u16CurrentFrameID] = 1;
                    //标记下一组报文没有收到
                    waveInfo.CurrentNoDataNumber = recWave.u16CurrentFrameID + 1;
                    //更新最后收到波形数据的时间
                    waveInfo.LastReceiveTime = DateTime.Now;

                    if (recWave.u16CurrentFrameID == recWave.u16TotalFramesNum)
                    {
                        //取得最后一条报文中波形数据的长度
                        waveInfo.LastWaveLength = recWave.u8PacketSize;
                    }

                    //向波形数据中添加数据
                    for (int i = 0; i < recWave.u8PacketSize; i++)
                    {
                        waveInfo.WaveData[Common.CommonConst.Wave_Length_InMessage * (recWave.u16CurrentFrameID - 1) + i] =
                            recWave.u8aData[i];
                    }

                    //响应接收该帧波形数据完成
                    if (iCMS.WG.Agent.ComFunction.detailLog)
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " WS 上传波形数据信息,接收第" + recWave.u16CurrentFrameID + "/" + (waveInfo.ReceiveDataNumber.Length - 1) + "帧波形数据完成 " + recWave.mac.ToHexString() + "  类型： " + GetWaveTypeString.GetString((int)recWave.WaveType));
                    else if (recWave.u16CurrentFrameID == 1)
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " WS 上传波形数据信息, " + recWave.mac.ToHexString() + "  类型： " + GetWaveTypeString.GetString((int)recWave.WaveType));

                    waveInfo.checkFullTimer = new Timer(new TimerCallback(TimerCheckWaveIsFull), recWave.mac.ToHexString() + "|" + (int)waveInfo.WaveType, int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("CheckFullTime")), -1);
                    //判断波形完整，并保存

                    if (iCMS.WG.Agent.ComFunction.CheckDataIsFull(waveInfo))
                    {
                        waveInfo.UpLoadDataTime = System.DateTime.Now;
                        try
                        {
                            waveInfo.checkFullTimer.Dispose();
                            waveInfo.checkFullTimer = null;
                        }
                        catch
                        {
                        }
                        tWaveDataResult result = new tWaveDataResult();
                        result.mac = recWave.mac;
                        result.u16aRC = new UInt16[] { 65535 };

                        iCMS.WG.Agent.ComFunction.Send2WS(result, EnumRequestWSType.ReplyWaveData);
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " WS 波形数据接收完整 " + recWave.mac.ToHexString() + " 类型： " + GetWaveTypeString.GetString((int)recWave.WaveType));

                        int length = 0;
                        if ((recWave.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage) % 1024 == 0)
                        {
                            length = (recWave.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage);
                        }
                        else
                        {
                            length = (recWave.u16TotalFramesNum * Common.CommonConst.Wave_Length_InMessage) - Common.CommonConst.Wave_Length_InMessage + waveInfo.LastWaveLength;
                        }
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), " 上传波形数据 " + recWave.mac.ToHexString() + " 类型： " + GetWaveTypeString.GetString((int)recWave.WaveType));
                        //调用上层，上传数据
                        communication2Server.UploadVibrationWave(waveInfo.Clone() as WSWaveInfo, length);
                        lock (waveInfo)
                        {
                            ResetWaveInfo(waveInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithMesh.ResponseWaveDataInfo ，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
            }
        }
        /// <summary>
        /// 重置某个采集数据的状态
        /// </summary>
        /// <param name="_WSWaveInfo"></param>
        private static void ResetWaveInfo(WSWaveInfo _WSWaveInfo)
        {
            //重置波形接收状态
            try
            {
                _WSWaveInfo.ReceiveDataNumber = null;
                _WSWaveInfo.IsFull = true;
                _WSWaveInfo.RepeatNumber = 0;
                _WSWaveInfo.CurrentNoDataNumber = -1;
                _WSWaveInfo.IsRepeat = false;
                _WSWaveInfo.WaveDescInfo = null;
                _WSWaveInfo.WaveData = null;
                _WSWaveInfo.retry = false;
                if (_WSWaveInfo.checkFullTimer != null)
                {
                    _WSWaveInfo.checkFullTimer.Dispose();
                    _WSWaveInfo.checkFullTimer = null;
                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithMesh.ResetWaveInfo ，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
            }
        }

        /// <summary>
        /// 检查采集数据的完整性  作废
        /// </summary>
        /*
        private void ThreadCheckWaveIsFull()
        {
            try
            {
                while (true)
                {

                    try
                    {
                        if (listWaveInfo.Count == 0)
                        {
                            //没有接收数据波形的ws
                            Thread.Sleep(1);
                            continue;
                        }

                        DateTime sTime = DateTime.Now;
                        for (int num = 0; num < listWaveInfo.Count; num++)
                        {
                            try
                            {
                                WSWaveInfo _WSWaveInfo = listWaveInfo[num];
                                if (_WSWaveInfo.IsFull == false)
                                {

                                    if (_WSWaveInfo.CurrentNoDataNumber < 0 || !((System.DateTime.Now - _WSWaveInfo.LastReceiveTime).TotalMilliseconds > 2500 * (_WSWaveInfo.RepeatNumber + 1)))
                                    {
                                        continue;
                                    }

                                    //当前时间比最后接收时间超过2.5秒
                                    if ((System.DateTime.Now - _WSWaveInfo.LastReceiveTime).TotalMilliseconds > 2500 * (_WSWaveInfo.RepeatNumber + 1))
                                    {


                                        bool isReceiveInfo = true;
                                        IList<UInt16> numbers = new List<UInt16>();
                                        lock (numbers)
                                        {
                                            if (_WSWaveInfo.ReceiveDataNumber[0] == 0)
                                            {
                                                isReceiveInfo = false;
                                                numbers.Add(0X00);
                                            }
                                            int countNum = 30;
                                            if (!isReceiveInfo)
                                            {
                                                countNum = 29;
                                            }
                                            for (int i = 1; i < _WSWaveInfo.ReceiveDataNumber.Length; i++)
                                            {
                                                if (_WSWaveInfo.ReceiveDataNumber[i] == 0)
                                                {
                                                    numbers.Add((UInt16)i);

                                                    if (numbers.Count > countNum)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }

                                            StringBuilder strMessage = new StringBuilder();
                                            UInt16[] aRC = null;
                                            if (numbers.Count == 1 && numbers[0] == 0)//仅仅缺少波形描述信息
                                            {
                                                aRC = new UInt16[2];
                                            }
                                            else
                                            {
                                                aRC = new UInt16[numbers.Count];
                                            }

                                            for (int n = 0; n < numbers.Count; n++)
                                            {

                                                if (aRC.Length > 30)
                                                    break;
                                                aRC[n] = numbers[n];
                                                strMessage.Append((int)numbers[n] + "  ");
                                            }




                                            if (_WSWaveInfo.RepeatNumber > (int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("TryAgainNum")) - 1))
                                            {
                                                _WSWaveInfo.IsFull = true;
                                                //同一个报文询问3次，数据接收结束
                                                if (_WSWaveInfo.WaveDescInfo == null)
                                                {
                                                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "由于iMesh没有及时响应（没有收到波形描述信息，已重试3次），丢弃该组波形,MAC：[ " + _WSWaveInfo.MAC + " ] 类型： " + GetWaveTypeString.GetString((int)_WSWaveInfo.WaveType));

                                                }
                                                else
                                                {

                                                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "由于iMesh没有及时响应（波形数据接收不完整，已重试3次），丢弃该组波形,采集时间[" + _WSWaveInfo.WaveDescInfo.SamplingTime + "],MAC：[ " + _WSWaveInfo.MAC + " ],[" + _WSWaveInfo.CurrentNoDataNumber + "]，类型：" + GetWaveTypeString.GetString((int)_WSWaveInfo.WaveType));
                                                }
                                                continue;
                                            }
                                            else
                                            {

                                                try
                                                {
                                                    if (iCMS.WG.Agent.ComFunction.GetConnectionStatu(_WSWaveInfo.MAC.ToString().ToUpper()))
                                                    {
                                                        _WSWaveInfo.RepeatNumber++;
                                                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "最后一次接收数据时间：" + _WSWaveInfo.LastReceiveTime.ToString()+"第" + _WSWaveInfo.RepeatNumber + "次，重新发送波形：" + strMessage.ToString() + "MAC: " + _WSWaveInfo.MAC.ToString() + " 类型： " + GetWaveTypeString.GetString((int)_WSWaveInfo.WaveType));

                                                        if (numbers.Count > 0 && aRC.Length>0)
                                                        {
                                                            tWaveDataResult result = new tWaveDataResult();
                                                            result.mac = new tMAC(_WSWaveInfo.MAC);
                                                            if (aRC.Length == 1 && aRC[0] == 0)
                                                            {
                                                                aRC[1] = 1;
                                                            }

                                                            result.u16aRC = aRC;

                                                            iCMS.WG.Agent.ComFunction.Send2WS(result, EnumRequestWSType.ReplyWaveData);


                                                        }
                                                    }
                                                    else
                                                    {
                                                        _WSWaveInfo.RepeatNumber++;
                                                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), _WSWaveInfo.MAC.ToString() + " 重发波形：" + strMessage.ToString() + ",但该WS已经断开连接。");
                                                        continue;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    _WSWaveInfo.RepeatNumber++;
                                                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "重发波形发生错误，\r\n" + ex.Message + "\r\n" + ex.TargetSite);
                                                    continue;
                                                }

                                            }

                                        }
                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }

                        }


                    }
                    catch (Exception ex)
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.Communication.ThreadCheckWaveIsFull()错误：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.Communication.ThreadCheckWaveIsFull()错误：" + ex.Message);
                throw ex;
            }
        }
        */

        /// <summary>
        /// 检查采集数据的完整性 
        /// </summary>
        public void TimerCheckWaveIsFull(Object status)
        {
            string[] info = status.ToString().Split('|');
            string mac = info[0].ToString();
            int type = int.Parse(info[1].ToString());
            EnumWaveType waveType = (EnumWaveType)type;

            List<WSWaveInfo> tempList = listWaveInfo.Where(p => p.MAC.ToString().ToUpper() == mac.ToString().ToUpper() && p.WaveType == waveType).ToList();
            if (tempList != null && tempList.Count > 0)
            {
                try
                {
                    WSWaveInfo _WSWaveInfo = tempList[0];
                    if ((System.DateTime.Now - _WSWaveInfo.LastReceiveTime).TotalMilliseconds < int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("CheckFullTime")))
                    {
                        return;
                    }
                    if (_WSWaveInfo.IsFull == false)
                    {
                        if (_WSWaveInfo.RepeatNumber > (int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("TryAgainNum")) - 1))
                        {
                            try
                            {
                                _WSWaveInfo.checkFullTimer.Dispose();
                                _WSWaveInfo.checkFullTimer = null;
                            }
                            catch
                            { }
                            _WSWaveInfo.IsFull = true;
                            //同一个报文询问3次，数据接收结束
                            if (_WSWaveInfo.WaveDescInfo == null)
                            {
                                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "由于iMesh没有及时响应（没有收到波形描述信息，已重试3次），丢弃该组波形,MAC：[ " + _WSWaveInfo.MAC + " ]，类型： " + GetWaveTypeString.GetString((int)_WSWaveInfo.WaveType));
                            }
                            else
                            {
                                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "由于iMesh没有及时响应（波形数据接收不完整，已重试3次），丢弃该组波形,采集时间[" + _WSWaveInfo.WaveDescInfo.SamplingTime + "],MAC：[ " + _WSWaveInfo.MAC + " ]，类型：" + GetWaveTypeString.GetString((int)_WSWaveInfo.WaveType));
                            }
                        }
                        else
                        {
                            try
                            {
                                StringBuilder strMessage = new StringBuilder();
                                if (iCMS.WG.Agent.ComFunction.GetConnectionStatu(_WSWaveInfo.MAC.ToString().ToUpper()))
                                {
                                    bool isReceiveInfo = true;
                                    IList<UInt16> numbers = new List<UInt16>();

                                    if (_WSWaveInfo.ReceiveDataNumber[0] == 0)
                                    {
                                        isReceiveInfo = false;
                                        numbers.Add(0X00);
                                    }
                                    int countNum = 20;
                                    if (!isReceiveInfo)
                                    {
                                        countNum = 19;
                                    }
                                    for (int i = 1; i < _WSWaveInfo.ReceiveDataNumber.Length; i++)
                                    {
                                        if (_WSWaveInfo.ReceiveDataNumber[i] == 0)
                                        {
                                            numbers.Add((UInt16)i);

                                            if (numbers.Count > countNum)
                                            {
                                                break;
                                            }
                                        }
                                    }

                                    UInt16[] aRC = new UInt16[numbers.Count];
                                    for (int n = 0; n < numbers.Count; n++)
                                    {
                                        if (n > 19)
                                            break;
                                        aRC[n] = numbers[n];
                                        strMessage.Append((int)numbers[n] + "  ");
                                    }

                                    _WSWaveInfo.RepeatNumber++;

                                    if (numbers.Count > 0 && aRC.Length > 0 && strMessage.ToString().Trim() != "")
                                    {
                                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "最后一次接收数据时间：" + _WSWaveInfo.LastReceiveTime.ToString() + "第" + _WSWaveInfo.RepeatNumber + "次，重新发送波形：" + strMessage.ToString() + "MAC: " + _WSWaveInfo.MAC.ToString() + " 类型： " + GetWaveTypeString.GetString((int)_WSWaveInfo.WaveType));
                                        tWaveDataResult result = new tWaveDataResult();
                                        result.mac = new tMAC(_WSWaveInfo.MAC);
                                        result.u16aRC = aRC;

                                        iCMS.WG.Agent.ComFunction.Send2WS(result, EnumRequestWSType.ReplyWaveData);
                                        _WSWaveInfo.checkFullTimer = new Timer(new TimerCallback(TimerCheckWaveIsFull), _WSWaveInfo.MAC + "|" + (int)_WSWaveInfo.WaveType, int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("CheckFullTime")), -1);
                                    }
                                }
                                else
                                {
                                    _WSWaveInfo.RepeatNumber++;
                                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), _WSWaveInfo.MAC.ToString() + " 重发波形：" + strMessage.ToString() + ",但该WS已经断开连接。");
                                }
                            }
                            catch (Exception ex)
                            {
                                _WSWaveInfo.RepeatNumber++;
                                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "重发波形发生错误，\r\n" + ex.Message + "\r\n" + ex.TargetSite);
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }
        #endregion

        #region WS自报告处理
        private void ResponseWSSelfReport(tSelfReportParam report)
        {
            try
            {
                string macStr = report.mac.ToHexString().ToUpper();
                tSelfReportResult result = new tSelfReportResult();
                result.mac = new tMAC(macStr);
                result.u16RC = 0x00;
                result.u64Seconds = iCMS.WG.Agent.ComFunction.meshAdapter.CalibrationTime;
                result.u32Microseconds = 0;
                iCMS.WG.Agent.ComFunction.Send2WS(result, EnumRequestWSType.ReplySelfReport);
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "响应 WS 自报告 " + report.mac.ToHexString());

                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "上传 WS 状态 " + report.mac.ToHexString());
                iCMS.WG.Agent.ComFunction.UpdateConnectionWSStatu(macStr.ToUpper(), ((int)report.State).ToString());
                communication2Server.UploadWSStatus(macStr.ToUpper(), iCMS.WG.Agent.ComFunction.GetVersion(report.Version), EnumWSStatus.WSOn);
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), iCMS.WG.Agent.ComFunction.GetVersion(report.Version));
                WGConnectedReceived();
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.CommunicationWithMesh.ResponseWSSelfReport ，异常：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());
            }
        }
        #endregion

        #endregion

        #endregion

        #region Agent下发操作后 WS的响应

        #region 校时
        /// <summary>
        /// 校时
        /// </summary>
        private void TimeCalibration()
        { }
        #endregion

        #region 设置新的WSID
        /// <summary>
        /// add by masu 2016年2月19日16:44:52 设置新的WSID
        /// </summary>
        private void SetNewWSIDResultReceived(tSetWsIdResult _WsIDResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    //int index = iCMS.WG.Agent.ComFunction.GetIndexOfSetNewWSIDList(_WsIDResult.mac.ToHexString());
                    //发送成功
                    if (_WsIDResult.u16RC == 0)
                    {
                        communication2Server.UploadConfigResponse(_WsIDResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_NO, 0, "");
                    }
                    else  //发送失败
                    {
                        communication2Server.UploadConfigResponse(_WsIDResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_NO, 1, "更新WSID失败！");
                    }
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
                }
            });
        }
        #endregion

        #region 设置WSNetWorkID
        /// <summary>
        /// add by masu 2016年2月22日13:49:01 设置WSNetWorkID 
        /// </summary>
        private void SetWS_NetWorkIDResultReceived(tSetNetworkIdResult _NetworkIDResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    //int index = iCMS.WG.Agent.ComFunction.GetIndexOfSetNetworkIDList(_NetworkIDResult.mac.ToHexString());
                    //发送成功
                    if (_NetworkIDResult.u16RC == 0)
                    {
                        //if (index != null && index >= 0)
                        //{
                        //    lock (iCMS.WG.Agent.ComFunction.setNetworkIDList)
                        //    {
                        //        iCMS.WG.Agent.ComFunction.setNetworkIDList[index].TryAgainnum = -1;
                        //    }

                        //}
                        communication2Server.UploadConfigResponse(_NetworkIDResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_NetID, 0, "");
                    }
                    else  //发送失败
                    {
                        communication2Server.UploadConfigResponse(_NetworkIDResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_NetID, 1, "更新WSID失败！");
                        //if (iCMS.WG.Agent.ComFunction.setNetworkIDList[index].TryAgainnum <= int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("TryAgainNum")))
                        //{
                        //    lock (iCMS.WG.Agent.ComFunction.setNetworkIDList)
                        //    {
                        //        iCMS.WG.Agent.ComFunction.setNetworkIDList[index].TryAgainnum++;
                        //    }
                        //    //重发
                        //    tSetNetworkIdParam networkIDParam = new tSetNetworkIdParam();
                        //    networkIDParam.u16ID = iCMS.WG.Agent.ComFunction.setNetworkIDList[index].NewNetWorkID;

                        //    //向底层发送信息
                        //    if (!iCMS.WG.Agent.ComFunction.Send2WS(networkIDParam, Common.Enum.EnumRequestWSType.SetWsID))
                        //    {
                        //        communication2Server.UploadConfigResponse(_NetworkIDResult.mac.ToHexString(), Enum_ConfigType.ConfigType_WS_NetID, 1, "网络正忙不能更新WSID！");
                        //    }
                        //    else
                        //    {
                        //        communication2Server.UploadConfigResponse(_NetworkIDResult.mac.ToHexString(), Enum_ConfigType.ConfigType_WS_NetID, 0, "");
                        //    }
                        //}
                        //else
                        //{
                        //    lock (iCMS.WG.Agent.ComFunction.setNetworkIDList)
                        //    {
                        //        iCMS.WG.Agent.ComFunction.setNetworkIDList.RemoveAt(index);
                        //        communication2Server.UploadConfigResponse(_NetworkIDResult.mac.ToHexString(), Enum_ConfigType.ConfigType_WS_NetID, 1, "网络正忙更新WSID失败！");
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
                }
            });
        }
        #endregion

        #region 下发测量定义
        /// <summary>
        /// 下发测量定义add by masu 2016年2月18日10:03:07
        /// </summary>
        private void ConfigMeasDefineResultReceived(tSetMeasDefResult _MeasureDefResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    //发送测量定义成功
                    if (_MeasureDefResult.u16RC == 0)
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), _MeasureDefResult.mac.ToHexString() + "下发测量定义成功WS返回结果： " + _MeasureDefResult.u16RC);
                        communication2Server.UploadConfigResponse(_MeasureDefResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_MeasDefine, Convert.ToInt32(EnmuReceiveStatus.Succeed), "");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), _MeasureDefResult.mac.ToHexString() + "上传测量定义下发成功");
                    }
                    else  //发送测量定义失败
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), _MeasureDefResult.mac.ToHexString() + "下发测量定义失败WS返回结果： " + _MeasureDefResult.u16RC);
                        communication2Server.UploadConfigResponse(_MeasureDefResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_MeasDefine, Convert.ToInt32(EnmuReceiveStatus.Faild), "下发测量定义失败！");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), _MeasureDefResult.mac.ToHexString() + "上传测量定义下发失败");
                    }

                    lock (iCMS.WG.Agent.ComFunction.sendMeasureDefineList)
                    {
                        int index = iCMS.WG.Agent.ComFunction.GetIndexOfSendMeasDefineList(_MeasureDefResult.mac.ToHexString().ToUpper());
                        //张辽阔 2017-01-13 增加
                        if (index > -1)
                        {
                            if (iCMS.WG.Agent.ComFunction.sendMeasureDefineList[index].time != null)
                                iCMS.WG.Agent.ComFunction.sendMeasureDefineList[index].time.Dispose();
                            iCMS.WG.Agent.ComFunction.sendMeasureDefineList.RemoveAt(index);
                        }
                    }
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
                }
            });
        }
        #endregion

        #region 下发测量定义超时
        /// <summary>
        /// 下发测量定义超时 add by masu 2016年3月4日11:40:37
        /// </summary>
        private void MeasDefineFailedReceived(tMAC mac)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                lock (iCMS.WG.Agent.ComFunction.sendMeasureDefineList)
                {
                    int index = iCMS.WG.Agent.ComFunction.GetIndexOfSendMeasDefineList(mac.ToHexString().ToUpper());
                    //张辽阔 2017-01-13 增加
                    if (index > -1)
                    {
                        if (iCMS.WG.Agent.ComFunction.sendMeasureDefineList[index].time != null)
                            iCMS.WG.Agent.ComFunction.sendMeasureDefineList[index].time.Dispose();
                        iCMS.WG.Agent.ComFunction.sendMeasureDefineList.RemoveAt(index);
                    }
                }
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "下发测量定义超时： " + mac.ToHexString());
                communication2Server.UploadConfigResponse(mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_MeasDefine, Convert.ToInt32(EnmuReceiveStatus.TimeOut), "下发测量定义超时！");
            });
        }
        #endregion

        #region 配置SN Code
        /// <summary>
        /// add by masu 2016年2月22日14:00:34 配置SN Code
        /// </summary>
        private void SetSNCodeResultReceived(tSetWsSnResult _WsSnResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    //int index = iCMS.WG.Agent.ComFunction.GetIndexOfSetSNCodeList(_WsSnResult.mac.ToHexString());
                    //发送成功
                    if (_WsSnResult.u16RC == 0)
                    {
                        //if (index != null && index >= 0)
                        //{
                        //    lock (iCMS.WG.Agent.ComFunction.setSNCodeList)
                        //    {
                        //        iCMS.WG.Agent.ComFunction.setSNCodeList[index].TryAgainnum = -1;
                        //    }

                        //}
                        communication2Server.UploadConfigResponse(_WsSnResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_Get_SNCode, 0, "");
                    }
                    else  //发送失败
                    {
                        communication2Server.UploadConfigResponse(_WsSnResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_Get_SNCode, 1, "更新SN串码失败！");
                        //if (iCMS.WG.Agent.ComFunction.setSNCodeList[index].TryAgainnum <= int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("TryAgainNum")))
                        //{
                        //    lock (iCMS.WG.Agent.ComFunction.setSNCodeList)
                        //    {
                        //        iCMS.WG.Agent.ComFunction.setSNCodeList[index].TryAgainnum++;
                        //    }
                        //    //重发
                        //    tSetWsSnParam wsSNParam = new tSetWsSnParam();
                        //    wsSNParam.sn = iCMS.WG.Agent.ComFunction.setSNCodeList[index].sn;

                        //    //向底层发送信息
                        //    if (!iCMS.WG.Agent.ComFunction.Send2WS(wsSNParam, Common.Enum.EnumRequestWSType.SetWsSn))
                        //    {
                        //        communication2Server.UploadConfigResponse(_WsSnResult.mac.ToHexString(), Enum_ConfigType.ConfigType_WS_Get_SNCode, 1, "网络正忙不能更新SN串码！");
                        //    }
                        //    else
                        //    {
                        //        communication2Server.UploadConfigResponse(_WsSnResult.mac.ToHexString(), Enum_ConfigType.ConfigType_WS_Get_SNCode, 0, "");
                        //    }
                        //}
                        //else
                        //{
                        //    lock (iCMS.WG.Agent.ComFunction.setNewWSIDList)
                        //    {
                        //        iCMS.WG.Agent.ComFunction.setNewWSIDList.RemoveAt(index);
                        //        communication2Server.UploadConfigResponse(_WsSnResult.mac.ToHexString(), Enum_ConfigType.ConfigType_WS_Get_SNCode, 1, "更新SN串码失败！");
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
                }
            });
        }
        #endregion

        #region WS校准
        /// <summary>
        /// add by masu 2016年2月22日14:44:05 WS校准
        /// </summary>
        private void CheckSensorResultReceived(tCaliSensorResult _CaliSensorResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    //int index = iCMS.WG.Agent.ComFunction.GetIndexOfCheckMonitorList(_CaliSensorResult.mac.ToHexString());
                    //发送成功
                    if (_CaliSensorResult.u16RC == 0)
                    {
                        //if (index != null && index >= 0)
                        //{
                        //    lock (iCMS.WG.Agent.ComFunction.checkMonitorList)
                        //    {
                        //        iCMS.WG.Agent.ComFunction.checkMonitorList[index].TryAgainnum = -1;
                        //    }

                        //}
                        communication2Server.UploadConfigResponse(_CaliSensorResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_Calibration, 0, "");
                    }
                    else  //发送失败
                    {
                        communication2Server.UploadConfigResponse(_CaliSensorResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_Calibration, 1, "传感器校准失败！");
                        //if (iCMS.WG.Agent.ComFunction.checkMonitorList[index].TryAgainnum <= int.Parse(iCMS.WG.Agent.ComFunction.GetAppConfig("TryAgainNum")))
                        //{
                        //    lock (iCMS.WG.Agent.ComFunction.checkMonitorList)
                        //    {
                        //        iCMS.WG.Agent.ComFunction.checkMonitorList[index].TryAgainnum++;
                        //    }
                        //    //重发
                        //    tCaliSensorParam caliSensorParam = new tCaliSensorParam();

                        //    //向底层发送信息
                        //    if (!iCMS.WG.Agent.ComFunction.Send2WS(caliSensorParam, Common.Enum.EnumRequestWSType.CalibrateWsSensor))
                        //    {
                        //        communication2Server.UploadConfigResponse(_CaliSensorResult.mac.ToHexString(), Enum_ConfigType.ConfigType_WS_Calibration, 1, "网络正忙不能传感器校准！");
                        //    }
                        //    else
                        //    {
                        //        communication2Server.UploadConfigResponse(_CaliSensorResult.mac.ToHexString(), Enum_ConfigType.ConfigType_WS_Calibration, 0, "");
                        //    }
                        //}
                        //else
                        //{
                        //    lock (iCMS.WG.Agent.ComFunction.checkMonitorList)
                        //    {
                        //        iCMS.WG.Agent.ComFunction.checkMonitorList.RemoveAt(index);
                        //        communication2Server.UploadConfigResponse(_CaliSensorResult.mac.ToHexString(), Enum_ConfigType.ConfigType_WS_Calibration, 1, "传感器校准失败！");
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
                }
            });
        }

        #endregion

        #region 取得WS SNCode
        /// <summary>
        /// 响应取得WS SNCode add by masu 2016年2月23日14:22:45
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rm"></param>
        private void GetSNCodeResultReceived(tGetWsSnResult _tGetWsSnResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                //根据获取SN串码是否为空判断是否成功 SN串码不为空返回成功
                if (!string.IsNullOrEmpty(_tGetWsSnResult.sn))
                {
                    communication2Server.UploadConfigResponse(_tGetWsSnResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_Get_SNCode, 0, "");
                }
                else //获取不成功
                {
                    communication2Server.UploadConfigResponse(_tGetWsSnResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_WS_Get_SNCode, 1, "获取SN串码不成功！");
                }
            });

        }
        #endregion

        #region 恢复出厂设置
        /// <summary>
        /// 恢复WG出厂设置 add by masu 2016年2月23日14:33:26
        /// </summary>
        private void RestoreWGResultReceived(tRestoreWGResult _tRestoreWGResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                //恢复WG出厂设置成功
                if (_tRestoreWGResult.u16RC == 0)
                {
                    communication2Server.UploadConfigResponse(_tRestoreWGResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_RestoreWG, 0, "");
                }
                else //恢复WG出厂设置失败
                {
                    communication2Server.UploadConfigResponse(_tRestoreWGResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_RestoreWG, 1, "恢复WG出厂设置失败！");
                }
            });
        }

        /// <summary>
        /// 恢复WS出厂设置 add by masu 2016年2月23日14:38:32
        /// </summary>
        private void RestoreWSResultReceived(tRestoreWSResult _tRestoreWSResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                //恢复WS出厂设置成功
                if (_tRestoreWSResult.u16RC == 0)
                {
                    communication2Server.UploadConfigResponse(_tRestoreWSResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_RestoreWG, 0, "");
                }
                else //恢复WS出厂设置失败
                {
                    communication2Server.UploadConfigResponse(_tRestoreWSResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_RestoreWG, 1, "恢复WS出厂设置失败！");
                }
            });
        }

        #endregion

        #region 重启
        /// <summary>
        /// 重启WG add by masu 2016年2月23日14:41:56
        /// </summary>
        private void ReSetWGResultReceived(tResetWGResult _tResetWGResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                //重启WG成功
                if (_tResetWGResult.u16RC == 0)
                {
                    communication2Server.UploadConfigResponse(_tResetWGResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_RestoreWG, 0, "");
                }
                else //重启WG失败
                {
                    communication2Server.UploadConfigResponse(_tResetWGResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_RestoreWG, 1, "重启WG失败！");
                }
            });
        }

        /// <summary>
        /// 重启WS add by masu 2016年2月23日14:44:02
        /// </summary>
        private void ReSetWSResultReceived(tResetWSResult _tResetWSResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                //重启WS成功
                if (_tResetWSResult.u16RC == 0)
                {
                    communication2Server.UploadConfigResponse(_tResetWSResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_RestoreWG, 0, "");
                }
                else //重启WS失败
                {
                    communication2Server.UploadConfigResponse(_tResetWSResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_RestoreWG, 1, "重启WS失败！");
                }
            });
        }
        #endregion
 		
     	#region 升级WS相关操作

        /// <summary>
        /// 创建人：wst
        /// 创建时间：2017-03-07
        /// 创建记录：清除总的超时时间定时器
        /// </summary>
        private void ClearAllWSTimer()
        { 
            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), " 设置了总的超时时间已到，清除总的超时定时器和针对单个传感器的定时器！");

            lock (ComFunction.WSUpdatingInfo)
            {
                //停止正在升级的WS定时器
                foreach (var item in ComFunction.WSUpdatingInfo.UpdatingAllWSInfo)
                {                    
                    if (item.Value != null)
                    {
                        item.Value.Stop();
                        item.Value.Close();
                        item.Value.Dispose();                  
                    }
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "(" + item.Key + ")" + " 升级已超时，清除升级信息");
                    //通知WS升级结果
                    communication2Server.UploadConfigResponse(item.Key, Enum_ConfigType.ConfigType_UpdateFirmware, Convert.ToInt32(EnmuReceiveStatus.Faild), "升级失败！");
                }
                //清除正在升级的WS信息
                ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Clear();

                //清除总的定时器
                if (ComFunction.WSUpdatingInfo.UpdateAllWSTimer != null)
                {
                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Stop();
                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Close();
                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Dispose();
                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer = null;
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "清除设置的总的超时定时器！");
                }
            }
            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "所有的WS已升级完成，PostludeUpdate！");
            if (ComFunction.meshAdapter.PostludeUpdate())
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "所有的WS已升级完成，PostludeUpdate成功！");
            }
            else
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "所有的WS已升级完成，PostludeUpdate失败！");
            }       
        }

        /// <summary>
        /// 创建人：wst
        /// 创建时间：2017-03-07
        /// 创建记录：通知上层某一个WS开始升级
        /// </summary>
        /// <param name="wsMac"></param>
        private void meshAdapter_UpdatingWs(tMAC wsMac)
        {
            Task.Run(() =>
            {
                try
                {
                    //传感器MAC地址
                    string currentMAC = wsMac.ToHexString().ToUpper();
                    //该传感器对应的定时器
                    System.Timers.Timer timer;
                    if (ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.TryGetValue(currentMAC, out timer))
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), currentMAC + "正在升级中...");
                        //单包的超时时间
                        int updateSinglePacketTimeOut = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AgtReq2MshRepTimeout"].ToString());
                        //单个WS升级的超时时间
                        int updateTimeOut = ComFunction.UpdateTotalNum * updateSinglePacketTimeOut;

                        lock (ComFunction.WSUpdatingInfo)
                        {
                            //是否启动了总的定时器
                            if (ComFunction.WSUpdatingInfo.UpdateAllWSTimer == null)
                            {
                                ComFunction.WSUpdatingInfo.UpdateAllWSTimer = new System.Timers.Timer();
                                ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Interval =
                                    updateTimeOut * ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Count;
                                ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Elapsed += (s, e) => { ClearAllWSTimer(); };
                                ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Start();
                                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "("+currentMAC+")" + " 启动了总的超时定时器");
                            }

                            if (timer == null)
                            {
                                //启动单个WS定时器
                                timer = new System.Timers.Timer();
                                timer.Interval = updateTimeOut;
                                timer.Elapsed += (s, e) =>
                                {
                                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "(" + currentMAC + ")" + " 升级已超时，清除升级信息");
                                    communication2Server.UploadConfigResponse(currentMAC, Enum_ConfigType.ConfigType_UpdateFirmware, Convert.ToInt32(EnmuReceiveStatus.TimeOut), "升级超时！");
                                    System.Timers.Timer tempTimer;
                                    if (ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.TryGetValue(currentMAC, out tempTimer))
                                    {
                                        lock (ComFunction.WSUpdatingInfo)
                                        {
                                            if (tempTimer != null)
                                            {
                                                tempTimer.Stop();
                                                tempTimer.Close();
                                                tempTimer.Dispose();
                                                tempTimer = null;
                                            }
                                            //清除该传感器的升级信息
                                            ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Remove(currentMAC);
                                            if (ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Any())
                                            {
                                                //是否启动了总的定时器
                                                if (ComFunction.WSUpdatingInfo.UpdateAllWSTimer != null)
                                                {
                                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Stop();
                                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Close();
                                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Dispose();
                                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer = null;

                                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer = new System.Timers.Timer();
                                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Interval =
                                                        updateTimeOut * ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Count;
                                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Elapsed += (s1, e1) => { ClearAllWSTimer(); };
                                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Start();
                                                }
                                            }
                                        }
                                    }
                                };
                                timer.Start();
                                ComFunction.WSUpdatingInfo.UpdatingAllWSInfo[currentMAC] = timer;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), ex.ToString());
                }
            });
        }

        /// <summary>
        /// 创建人：wst
        /// 创建时间：2017-03-07
        /// 创建记录：升级结果
        /// </summary>
        /// <param name="wsMac">传感器MAC地址</param>
        /// <param name="updateResult">0：表示升级成功，1：表示升级失败</param>
        private void UpdateWSResult(tMAC wsMac, EnmuReceiveStatus updateResult)
        {
            Task.Run(() =>
            {
                try
                {
                    string version = ComFunction.GetVersion(ComFunction.UpdateVer);
                    string currentMAC = wsMac.ToHexString().ToUpper();

                    if (updateResult == EnmuReceiveStatus.Succeed)
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "(" + currentMAC + ")" + "升级成功，清除升级信息");
                        communication2Server.UploadConfigResponse(currentMAC, Enum_ConfigType.ConfigType_UpdateFirmware, Convert.ToInt32(EnmuReceiveStatus.Succeed), version);
                    }
                    else if (updateResult == EnmuReceiveStatus.Faild)
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "(" + currentMAC + ")" + "升级失败，清除升级信息");
                        communication2Server.UploadConfigResponse(currentMAC, Enum_ConfigType.ConfigType_UpdateFirmware, Convert.ToInt32(EnmuReceiveStatus.Faild), "升级失败！");
                    }
                    System.Timers.Timer timer;
                    if (ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.TryGetValue(currentMAC, out timer))
                    {
                        //单包的超时时间
                        int updateSinglePacketTimeOut = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AgtReq2MshRepTimeout"].ToString());
                        //单个WS升级的超时时间
                        int updateTimeOut = ComFunction.UpdateTotalNum * updateSinglePacketTimeOut;
                        lock (ComFunction.WSUpdatingInfo)
                        {
                            if (timer != null)
                            {
                                timer.Stop();
                                timer.Close();
                                timer.Dispose();
                                timer = null;
                            }
                            //清除该传感器的升级信息
                            ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Remove(currentMAC);

                            if (ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Any())
                            {
                                //是否启动了总的定时器
                                if (ComFunction.WSUpdatingInfo.UpdateAllWSTimer != null)
                                {
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Stop();
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Close();
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Dispose();
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer = null;

                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer = new System.Timers.Timer();
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Interval =
                                        updateTimeOut * ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Count;
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Elapsed += (s1, e1) => { ClearAllWSTimer(); };
                                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Start();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), ex.ToString());
                }
            });
        }

        /// <summary>
        /// 创建人：wst
        /// 创建时间：2017-03-07
        /// 创建记录：通知上层某一个WS升级成功
        /// </summary>
        /// <param name="wsMac"></param>
        private void meshAdapter_UpdatedWsSucess(tMAC wsMac)
        {
            UpdateWSResult(wsMac, EnmuReceiveStatus.Succeed);
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-02-22
        /// 创建记录：通知上层某一个WS升级失败
        /// </summary>
        /// <param name="wsMac"></param>
        private void meshAdapter_UpdatedWsFailed(tMAC wsMac)
        {
            UpdateWSResult(wsMac, EnmuReceiveStatus.Faild);
        }

        /// <summary>
        /// 创建人：wst
        /// 创建时间：2017-03-07
        /// 创建记录：通知上层所有的WS升级完成
        /// </summary>
        private void meshAdapter_UpdateFinished()
        {
            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "升级结束！");
            lock (ComFunction.WSUpdatingInfo)
            {
                if (ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Count != 0)
                {
                    //停止正在升级的WS定时器
                    foreach (var item in ComFunction.WSUpdatingInfo.UpdatingAllWSInfo)
                    {
                        if (item.Value != null)
                        {
                            item.Value.Stop();
                            item.Value.Close();
                            item.Value.Dispose();                           
                        }
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "(" + item.Key + ")" + " 升级结束，清除升级信息");
                        //通知WS升级结果
                        communication2Server.UploadConfigResponse(item.Key, Enum_ConfigType.ConfigType_UpdateFirmware, Convert.ToInt32(EnmuReceiveStatus.Faild), "升级失败！");
                    }
                }
                //清除正在升级的WS信息
                ComFunction.WSUpdatingInfo.UpdatingAllWSInfo.Clear();
                //清除总的定时器
                if (ComFunction.WSUpdatingInfo.UpdateAllWSTimer != null)
                {
                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Stop();
                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Close();
                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer.Dispose();
                    ComFunction.WSUpdatingInfo.UpdateAllWSTimer = null;
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "清除设置的总的超时定时器！");
                }
                                     
            }
            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "所有的WS已升级完成，PostludeUpdate！");
            if (ComFunction.meshAdapter.PostludeUpdate())
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "所有的WS已升级完成，PostludeUpdate 成功！");
            }
            else
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "所有的WS已升级完成，PostludeUpdate 失败！");
            }
        }

        #endregion   
       
        #region 获取WS状态

        private void WsStatusReaultArrived(tGetMoteConfigEcho result)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    string macStrNext = result.mac.ToHexString();

                    if (macStrNext != iCMS.WG.Agent.Common.CommonConst.RequestGetMoteConfig)
                    {
                        ListHasJoin.Add(macStrNext);
                        int state = ConvertUtility.ConvertBCDToInt32(result.u8State);
                        if (state == 0)
                        {
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "上传 WS " + macStrNext + " 状态 断开 值：" + result.u8State.ToString());
                            communication2Server.UploadWSStatus(macStrNext.ToUpper(), "", EnumWSStatus.WSOff);
                        }
                        else
                        {
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "上传 WS " + macStrNext + " 状态 连接 值：" + result.u8State.ToString());
                            communication2Server.UploadWSStatus(macStrNext.ToUpper(), "", EnumWSStatus.WSOn);
                        }
                    }
                    if (macStrNext != iCMS.WG.Agent.Common.CommonConst.RequestGetMoteConfig)
                    {
                        iCMS.WG.Agent.ComFunction.getConfigMacCurr = macStrNext;
                        GetWSNStatusFromManager(macStrNext);
                    }
                    else
                    {
                        foreach (var obj in iCMS.WG.Agent.ComFunction.deviceStatus)
                        {
                            if (!ListHasJoin.Contains(obj.WSMAC.ToString().ToUpper()))
                            {
                                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "上传 WS " + obj.WSMAC.ToString().ToUpper() + " 状态 断开 值：" + 0);
                                communication2Server.UploadWSStatus(obj.WSMAC.ToString().ToUpper(), "", EnumWSStatus.WSOff);
                            }
                        }
                        iCMS.WG.Agent.ComFunction.getConfigMacCurr = iCMS.WG.Agent.Common.CommonConst.RequestGetMoteConfig;
                    }
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), ex.Message + "\r\n" + ex.StackTrace);
                }
            });
        }

        #endregion

        #region 网络中所有WS状态
        private void AllWsStatusReaultArrived(Dictionary<string, bool> InNetworkWS)
        {
            foreach (string mac in InNetworkWS.Keys)
            {
                tMAC AccMac = new tMAC(mac);
                ListHasJoin.Add(AccMac.ToHexString().ToUpper());

                if (InNetworkWS[mac])
                {
                    iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(AccMac.ToHexString().ToUpper(), EnumWSStatus.WSOn);
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "上传 WS " + mac + " 状态 连接 ");
                    communication2Server.UploadWSStatus(AccMac.ToHexString().ToUpper(), "", EnumWSStatus.WSOn);
                }
                else
                {
                    iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(AccMac.ToHexString().ToUpper(), EnumWSStatus.WSOff);
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "上传 WS " + mac + " 状态 断开");
                    communication2Server.UploadWSStatus(AccMac.ToHexString().ToUpper(), "", EnumWSStatus.WSOff);
                }
            }

            foreach (var obj in iCMS.WG.Agent.ComFunction.deviceStatus)
            {
                if (!ListHasJoin.Contains(obj.WSMAC.ToString().ToUpper()))
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "上传 WS " + obj.WSMAC.ToString().ToUpper() + " 状态 断开 值：" + 0);
                    communication2Server.UploadWSStatus(obj.WSMAC.ToString().ToUpper(), "", EnumWSStatus.WSOff);
                }
            }
            ListHasJoin.Clear();
        }
        #endregion

        #region 调用底层GetMoteConfig获取Manager真实WS状态

        private void GetWSNStatusFromManager(string mac)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    ListHasJoin.Clear();
                    tMAC tMac = new tMAC(mac);
                    iCMS.WG.Agent.ComFunction.meshAdapter.QueryWs(tMac);
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), ex.Message + "\r\n" + ex.StackTrace);
                }
            });
        }

        #endregion

        #region 获取WG NetWorkID
        private void GetNetWorkIDFromManager()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    string netID = ((int)iCMS.WG.Agent.ComFunction.meshAdapter.GetNetworkId()).ToString();
                    communication2Server.UploadWGNetWorkIDStatus(iCMS.WG.Agent.ComFunction.GetAppConfig("WGID").ToString(), netID);
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "WG NetWorkID " + netID);
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), ex.Message + "\r\n" + ex.StackTrace);
                }
            });
        }

        #endregion

        #region 触发式测量定义响应
        /// <summary>
        /// 触发式测量定义响应add by WST 2016年8月15日
        /// </summary>
        private void SetWS_TrigParamResultArrived(tSetTrigParamResult _MeasureDefResult)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    //发送测量定义成功
                    if (_MeasureDefResult.u16RC == 0)
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), _MeasureDefResult.mac.ToHexString() + "下发触发式定义成功WS返回结果： " + _MeasureDefResult.u16RC);
                        communication2Server.UploadConfigResponse(_MeasureDefResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_TriggerDefine, Convert.ToInt32(EnmuReceiveStatus.Succeed), "");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), _MeasureDefResult.mac.ToHexString() + "上传触发式定义下发成功");
                    }
                    else  //发送测量定义失败
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), _MeasureDefResult.mac.ToHexString() + "下发触发式定义失败WS返回结果： " + _MeasureDefResult.u16RC);
                        communication2Server.UploadConfigResponse(_MeasureDefResult.mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_TriggerDefine, Convert.ToInt32(EnmuReceiveStatus.Faild), "下发测量定义失败！");
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), _MeasureDefResult.mac.ToHexString() + "上传触发式定义下发失败");
                    }

                    lock (iCMS.WG.Agent.ComFunction.sendTriggerDefineList)
                    {
                        int index = iCMS.WG.Agent.ComFunction.GetIndexOfSendTriggerDefineList(_MeasureDefResult.mac.ToHexString().ToUpper());
                        if (iCMS.WG.Agent.ComFunction.sendTriggerDefineList[index].time != null)
                            iCMS.WG.Agent.ComFunction.sendTriggerDefineList[index].time.Dispose();
                        iCMS.WG.Agent.ComFunction.sendTriggerDefineList.RemoveAt(index);
                    }
                }
                catch (Exception ex)
                {
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
                }
            });
        }
        #endregion

        #region 下发触发式上传超时
        /// <summary>
        /// 下发触发式定义超时 
        /// </summary>
        private void SetTrigParamFailedReceive(tMAC mac)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                lock (iCMS.WG.Agent.ComFunction.sendTriggerDefineList)
                {
                    int index = iCMS.WG.Agent.ComFunction.GetIndexOfSendTriggerDefineList(mac.ToHexString().ToUpper());
                    if (iCMS.WG.Agent.ComFunction.sendTriggerDefineList[index].time != null)
                        iCMS.WG.Agent.ComFunction.sendTriggerDefineList[index].time.Dispose();
                    iCMS.WG.Agent.ComFunction.sendTriggerDefineList.RemoveAt(index);
                }
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Agent2WS.ToString(), "下发触发式超时： " + mac.ToHexString());
                communication2Server.UploadConfigResponse(mac.ToHexString().ToUpper(), Enum_ConfigType.ConfigType_TriggerDefine, Convert.ToInt32(EnmuReceiveStatus.TimeOut), "下发触发式超时！");
            });
        }
        #endregion

        #endregion
    }
}