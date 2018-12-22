/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent
 *文件名：  ComFunction
 *创建人：  LF
 *创建时间：2016/2/15 10:10:19
 *描述：iCMS.WG.Agent公共方法类
 * 修改记录：
    R1：
     修改作者：李峰
     修改时间：2016/5/30 13:30:00
     修改原因：① 增加响应WS上传启停机报文实现；
 *=====================================================================**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.ServiceModel.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Collections;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

using iMesh;

using iCMS.WG.Agent.Model.Send;
using iCMS.WG.Agent.Common.Enum;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Data.Request.WirelessSensors;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.WirelessSensors;
using iCMS.WG.Agent.Common;
using iCMS.WG.Agent.Model;

namespace iCMS.WG.Agent
{
    public static class ComFunction
    {
        #region 公用变量

        /// <summary>
        /// 是否记录所有数据的交互操作
        /// </summary>
        public static bool detailLog = false;

        /// <summary>
        /// 记录当前正在获取的MAC地址 getmoteconfig
        /// </summary>
        public static string getConfigMacCurr = null;

        /// <summary>
        /// 与server通讯是否成功标识
        /// </summary>
        public static bool getData = false;

        /// <summary>
        /// 底層網絡對象
        /// </summary>
        public static MeshAdapter meshAdapter;

        /// <summary>
        /// WS与WG的对应关系以及他们各自的连接状态
        /// </summary>
        public static List<DeviceStatus> deviceStatus = new List<Model.DeviceStatus>();



        /// <summary>
        /// 测量定义列表
        /// </summary>
        public static List<SendMeasureDefine> sendMeasureDefineList = new List<SendMeasureDefine>();
        /// <summary>
        /// WSID列表
        /// </summary>
        public static List<SetNewWSID> setNewWSIDList = new List<SetNewWSID>();
        /// <summary>
        /// NetworkID列表
        /// </summary>
        public static List<SetNetworkID> setNetworkIDList = new List<SetNetworkID>();
        /// <summary>
        /// SN串码列表
        /// </summary>
        public static List<SetSNCode> setSNCodeList = new List<SetSNCode>();
        /// <summary>
        /// 传感器校准列表
        /// </summary>
        public static List<CheckMonitor> checkMonitorList = new List<CheckMonitor>();
        /// <summary>
        /// 创建人：wst
        /// 创建时间：2017-03-07
        /// 创建记录：正在升级的WS
        /// </summary>
        public static WSUpdateInfo WSUpdatingInfo = new WSUpdateInfo();
        /// <summary>
        /// 需要升级的文件版本号
        /// </summary>
        public static tVer UpdateVer = new tVer();
        /// <summary>
        /// 需要升级的总的包数
        /// </summary>
        public static Int16 UpdateTotalNum = 0;
        /// <summary>
        /// 升级，重启成功
        /// </summary>
        public static bool reSetSuccesfulForUpdate = false;

        /// <summary>
        /// 缓存列表，用以缓存每个侧点每一种数据最后一条数据
        /// </summary>
        public static List<CacheOfSamplingData> cacheDate = new List<CacheOfSamplingData>();

        /// <summary>
        /// 触发式上传列表
        /// </summary>
        public static List<SendTriggerDefine> sendTriggerDefineList = new List<SendTriggerDefine>();

        #endregion

        #region 通用方法

        #region 获取配置文件中配置节的值
        /// <summary>
        /// 获取配置文件中配置节的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppConfig(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
        }
        #endregion

        #region 获取请求iCMS.Server的服务地址
        /// <summary>
        /// 获取请求iCMS.Server的服务地址
        /// </summary>
        /// <param name="ServicesKey"></param>
        /// <returns></returns>
        public static string GetiCMSServicesURL(string ServicesKey)
        {
            return GetAppConfig("iCMSServer") + "/" + GetAppConfig(ServicesKey);
        }
        #endregion

        #region  通用发送信息到WS
        /// <summary>
        /// 通用发送信息到WS
        /// </summary>
        /// <param name="obj">下发数据对象</param>
        /// <param name="requestType">请求类型</param>
        /// <param name="urgent">是否需要立即执行</param>
        /// <returns></returns>
        public static bool Send2WS(object obj, EnumRequestWSType requestType, bool urgent = false)
        {


            if (meshAdapter == null)
                return false;

            bool backObj = false;


            using (System.Threading.Tasks.Task<bool> result = System.Threading.Tasks.Task.Run<bool>(() =>
            {

                switch (requestType)
                {
                    case EnumRequestWSType.CalibrateTime:
                        backObj = meshAdapter.CalibrateTime((tCaliTimeParam)obj, urgent);
                        break;
                    case EnumRequestWSType.SetWsID:
                        backObj = meshAdapter.SetWsID((tSetWsIdParam)obj, urgent);
                        break;
                    case EnumRequestWSType.SetNetworkID:
                        backObj = meshAdapter.SetNetworkID((tSetNetworkIdParam)obj, urgent);
                        break;
                    case EnumRequestWSType.SetMeasDef:
                        backObj = meshAdapter.SetMeasDef((tSetMeasDefParam)obj, urgent);
                        break;
                    case EnumRequestWSType.SetWsSn:
                        backObj = meshAdapter.SetWsSn((tSetWsSnParam)obj, urgent);
                        break;
                    case EnumRequestWSType.CalibrateWsSensor:
                        backObj = meshAdapter.CalibrateWsSensor((tCaliSensorParam)obj, urgent);
                        break;
                    case EnumRequestWSType.GetWsSn:
                        backObj = meshAdapter.GetWsSn((tGetWsSnParam)obj, urgent);
                        break;
                    case EnumRequestWSType.RestoreWS:
                        backObj = meshAdapter.RestoreWS((tRestoreWSParam)obj, urgent);
                        break;
                    case EnumRequestWSType.RestoreWG:
                        backObj = meshAdapter.RestoreWG(urgent);
                        break;
                    case EnumRequestWSType.ResetWS:
                        backObj = meshAdapter.ResetWS((tResetWSParam)obj, urgent);
                        break;
                    case EnumRequestWSType.ResetWG:
                        backObj = meshAdapter.ResetWG(urgent);
                        break;
/*
                    case EnumRequestWSType.SetFwDescInfo:
                        backObj = meshAdapter.SetFwDescInfo((tSetFwDescInfoParam)obj, urgent);
                        break;
                    case EnumRequestWSType.SetFwData:
                        backObj = meshAdapter.SetFwData((tSetFwDataParam)obj, urgent);
                        break;
*/
                    case EnumRequestWSType.ReplySelfReport:
                        backObj = meshAdapter.ReplySelfReport((tSelfReportResult)obj, urgent);
                        break;
                    case EnumRequestWSType.ReplyWaveDesc:
                        //backObj = meshAdapter.ReplyWaveDesc((tWaveDescResult)obj, urgent);
                        break;
                    case EnumRequestWSType.ReplyWaveData:
                        backObj = meshAdapter.ReplyWaveData((tWaveDataResult)obj, urgent);
                        break;
                    case EnumRequestWSType.ReplyEigenValue:
                        backObj = meshAdapter.ReplyEigenValue((tEigenValueResult)obj, urgent);
                        break;
                    case EnumRequestWSType.ReplyTmpVolReport:
                        backObj = meshAdapter.ReplyTmpVolReport((tTmpVolResult)obj, urgent);
                        break;
                    case EnumRequestWSType.ReplyRevStop:
                        backObj = meshAdapter.ReplyRevStop((tRevStopResult)obj, urgent);
                        break;
                    case EnumRequestWSType.ReplyLQ:
                        backObj = meshAdapter.ReplyLQReport((tLQResult)obj, urgent);
                        break;
                    case EnumRequestWSType.SetTrigger:
                        backObj = meshAdapter.SetTrigParam((tSetTrigParam)obj, urgent);
                        break;

                };

                return backObj;
            }))
            {
                return result.Result;
            };


        }
        #endregion

        #endregion

        #region JSON与实体对象互转
        /// <summary>
        /// 将实体对象转化为流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static MemoryStream GetJsonMemoryStream<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            json.WriteObject(ms, obj);

            return ms;
        }
        /// <summary>
        /// 将实体对象转化为Json串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJson<T>(T obj)
        {

            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(obj, iso);


            //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    json.WriteObject(ms, obj);
            //    string szJson = Encoding.UTF8.GetString(ms.ToArray());
            //    return szJson;
            //}
        }
        /// <summary>
        /// 将Json串转化为实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="szJson"></param>
        /// <returns></returns>
        public static T ParseFormJson<T>(string szJson)
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            return JsonConvert.DeserializeObject<T>(szJson, iso);

            //T obj = Activator.CreateInstance<T>();
            //using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            //{
            //    DataContractJsonSerializer dcj = new DataContractJsonSerializer(typeof(T));
            //    return (T)dcj.ReadObject(ms);
            //}
        }

        #endregion

        #region  WG 与WS关系、状态列表 的维护操作

        #region WG 与WS关系、状态列表 根据应用程序报告添加
        /// <summary>
        /// WG 与WS关系、状态列表 根据应用程序报告添加
        /// </summary>
        /// <param name="mac"></param>
        public static void InitDeviceStatusFromApp(string mac, EnumWSStatus status = EnumWSStatus.WSOn)
        {

            mac = mac.ToUpper();
            System.Threading.Tasks.Task.Run(() =>
            {
                // bool isUpdate = false;
                if (deviceStatus.Count > 0)
                {
                    if (deviceStatus.Where(obj => obj.WSMAC.ToUpper() == mac.ToUpper()).ToList().Count <= 0)
                    {
                        lock (deviceStatus)
                        {
                            deviceStatus.Add(new DeviceStatus() { WGID = GetAppConfig("WGID").ToString(), WGLinkstatu = "1", WSMAC = mac.ToUpper(), WSLinkstatu = ((int)status).ToString(), WSID = "" });
                        }
                        // isUpdate = true;
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), " 添加WG与WS的状态信息：WS MAC：" + mac + "  WGID:" + GetAppConfig("WGID").ToString());
                    }
                }
                else
                {
                    lock (deviceStatus)
                    {
                        deviceStatus.Add(new DeviceStatus() { WGID = GetAppConfig("WGID").ToString(), WGLinkstatu = "1", WSMAC = mac.ToUpper(), WSLinkstatu = ((int)status).ToString(), WSID = "" });
                    }
                    //  isUpdate = true;
                    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), " 添加WG与WS的状态信息：WS MAC：" + mac + "  WGID:" + GetAppConfig("WGID").ToString());

                }
                return;
                #region 重复上传
                //if (isUpdate)
                //{
                //    iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "上传 WS 状态 " + mac.ToUpper());
                //    iCMS.WG.Agent.CommunicationWithServer communication2Server = new CommunicationWithServer();
                //    communication2Server.UploadWSStatus(mac.ToUpper(), "", status);
                //}
                //else
                //{
                //    var list = deviceStatus.Where(obj => obj.WSMAC.ToUpper() == mac.ToUpper()).ToList();
                //    if (list != null && list.Count > 0)
                //    {
                //        if (list[0].WSLinkstatu.ToString().Trim() == "0")
                //        {
                //            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.WS2Agent.ToString(), "上传 WS 状态 " + mac.ToUpper());
                //            iCMS.WG.Agent.CommunicationWithServer communication2Server = new CommunicationWithServer();
                //            communication2Server.UploadWSStatus(mac.ToUpper(), "", status);
                //        }
                //    }
                //}
                #endregion
            });

        }
        #endregion

        #region 初始化WS与WG的对应关系以及他们各自的连接状态 由Server获取
        /// <summary>
        /// 初始化WS与WG的对应关系以及他们各自的连接状态
        /// </summary>
        public static void InitDeviceStatusFromServer()
        {
            try
            {


                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string address = GetAppConfig("HostIP").ToString() + "/ConfigService.svc";

                BaseResponse<WirelessSensorsBaseInfoResult> baseResponse = ParseFormJson<BaseResponse<WirelessSensorsBaseInfoResult>>(ComFunction.CreateRequest(EnumRequestType.GetSensorsInfo, "GetBaseInfoByAgentAddress",
                        new QueryBaseInfoParameter()
                        {
                            AgentAddress = address

                        }.ToClientString()));
                var list = baseResponse.Result as WirelessSensorsBaseInfoResult;

                if (list.DevicesInfo == null || list.DevicesInfo.Count <= 0)
                {
                    if (list.WGID == null)
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "服务器端未配置网关");
                        return;
                    }
                    else
                    {
                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "设置WGID： " + list.WGID.ToString());

                        cfa.AppSettings.Settings["WGID"].Value = list.WGID.ToString();
                        cfa.Save();
                        ConfigurationManager.RefreshSection("appSettings");


                        iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "网关下未配置任何WS，从Server获取WG、WS对应关系成功 ");

                        return;
                    }
                }

                //向iCMS.Server发送请求得到数据
                lock (deviceStatus)
                {
                    int num = 0;
                    foreach (StatuInfo statuInfo in list.DevicesInfo)
                    {

                        if (num == 0)
                        {
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "设置WGID： " + list.WGID.ToString());

                            cfa.AppSettings.Settings["WGID"].Value = statuInfo.WGID.ToString();
                            cfa.Save();
                            ConfigurationManager.RefreshSection("appSettings");
                            num++;
                        }
                        if (deviceStatus.Where(obj => obj.WSMAC.ToUpper() == statuInfo.WSMAC.ToUpper()).ToList().Count <= 0)
                        {
                            deviceStatus.Add(new DeviceStatus() { WGID = statuInfo.WGID, WSID = statuInfo.WSID, WGLinkstatu = statuInfo.WGLinkstatu, WSLinkstatu = statuInfo.WSLinkstatu, WSMAC = statuInfo.WSMAC.ToUpper() });
                            iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), " 添加WG与WS的状态信息：WS MAC：" + statuInfo.WSMAC.ToUpper() + "  WGID:" + GetAppConfig("WGID").ToString());
                        }
                    }
                }
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Log.ToString(), "从Server获取WG、WS对应关系成功");
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.WG.Agent.ComFunction.InitDS()错误：" + ex.Message + "\r\n详细：" + ex.StackTrace.ToString());

                throw ex;
            }

        }
        #endregion

        #region 判断连接状态，WG、WS任意一个处理非连接状态则返回false，反之true
        /// <summary>
        /// 判断连接状态，WG、WS任意一个处理非连接状态则返回false，反之true
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static bool GetConnectionStatu(string mac)
        {


            bool back = false;
            DeviceStatus ds = null;

            if (deviceStatus.Count == 0)
            {

                // iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.Server没有启动");

                return back = true;

            }

            ds = deviceStatus.Where(obj => obj.WSMAC.ToUpper() == mac.ToUpper()).ToList().First();

            //没有信息或在现有的信息中没有发现关联信息
            if (deviceStatus.Count == 0 || ds == null)
            {
                iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(mac.ToUpper());
                ds = deviceStatus.Where(obj => obj.WSMAC.ToUpper() == mac.ToUpper()).ToList().First();
            }

            if (ds != null)
                back = (ds.WGLinkstatu != "0" && ds.WSLinkstatu != "0");//相关定义参考WSStatus类


            return back;
        }

        #endregion

        #region  检查WS连接状态
        public static bool checkWSLinkstatus(string mac, string linkstatus)
        {
            try
            {
                if (deviceStatus.Count == 0)
                {



                    iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(mac);

                    //iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.Server没有启动");
                    return true;

                }
                return deviceStatus.Where(obj => obj.WSMAC.ToString().ToUpper() == mac.ToUpper()).ToList().First().WSLinkstatu != linkstatus;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region  检查WG连接状态
        public static bool checkWGLinkstatus(string WGID, string linkstatus)
        {
            try
            {
                if (deviceStatus.Count == 0)
                {

                    //iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.Server没有启动");

                    return true;
                }
                return deviceStatus.Where(obj => obj.WGID == WGID).ToList().First().WSLinkstatu != linkstatus;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 更新WS的连接状态
        /// <summary>
        /// 更新WS的连接状态
        /// </summary>
        /// <param name="wsID"></param>
        /// <param name="linkStatu"></param>
        public static void UpdateConnectionWSStatu(string mac, string linkStatu)
        {

            DeviceStatus ds = null;
            if (deviceStatus.Count == 0)
            {

                iCMS.WG.Agent.ComFunction.InitDeviceStatusFromApp(mac.ToUpper());


                //iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.Server没有启动");

            }
            var listTemp = deviceStatus.Where(obj => obj.WSMAC.ToString().ToUpper() == mac.ToString().ToUpper()).ToList();
            if (listTemp.Count <= 0)
                return;
            ds = listTemp.First();


            if (ds != null)
            {

                deviceStatus[deviceStatus.IndexOf(ds)].WSLinkstatu = linkStatu;
            }
        }
        #endregion

        #region 更新WG的连接状态
        /// <summary>
        /// 更新WG的连接状态
        /// </summary>
        /// <param name="wgID"></param>
        /// <param name="linkStatu"></param>
        public static void UpdateConnectionWGStatu(string wgID, string linkStatu)
        {

            List<DeviceStatus> ds = null;
            if (deviceStatus.Count == 0)
            {
                //iCMS.WG.Agent.Common.LogHelper.WriteLog(iCMS.WG.Agent.Common.Enum.EnumLogType.Error.ToString(), "iCMS.Server没有启动");
                return;
            }

            ds = deviceStatus.Where(obj => obj.WGID == wgID).ToList();

            //没有信息或在现有的信息中没有发现关联信息
            if (deviceStatus.Count == 0 || ds == null)
            {
                InitDeviceStatusFromServer();
                ds = deviceStatus.Where(obj => obj.WGID == wgID).ToList();
            }



            if (ds != null)
            {

                foreach (var status in ds)
                {

                    if (linkStatu == iCMS.WG.Agent.Common.Enum.WGStatus.WGOff.ToString())
                    {
                        deviceStatus[deviceStatus.IndexOf(status)].WGLinkstatu = linkStatu;
                        deviceStatus[deviceStatus.IndexOf(status)].WSLinkstatu = iCMS.WG.Agent.Common.Enum.WGStatus.WGOff.ToString();
                    }
                    else
                    {
                        deviceStatus[deviceStatus.IndexOf(status)].WGLinkstatu = linkStatu;
                    }
                }


            }
        }
        #endregion

        #endregion

        #region 检查波形数据集合中是否存在指定值的对象
        /// <summary>
        /// 检查波形数据集合中是否存在指定值的对象
        /// </summary>
        /// <param name="list"></param>
        /// <param name="MAC"></param>
        /// <param name="WaveType"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool CheckObjectInArray(List<WSWaveInfo> list, string MAC, EnumWaveType WaveType, ref int index)
        {
            List<WSWaveInfo> tempList = list.Where(p => p.MAC.ToString().ToUpper() == MAC.ToString().ToUpper() && p.WaveType == WaveType).ToList();
            if (tempList != null && tempList.Count > 0)
            {
                WSWaveInfo waveInfo = tempList.FirstOrDefault();
                index = list.IndexOf(waveInfo);
            }

            return tempList.Count > 0;
        }
        #endregion

        #region 取得采集时间
        /// <summary>
        /// 取得采集时间(年月日)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static DateTime GetSamplingTime(string fileName)
        {
            string time = fileName.Split('_')[1];
            return new DateTime(Convert.ToInt32(time.Substring(0, 4)),
                Convert.ToInt32(time.Substring(4, 2)),
                Convert.ToInt32(time.Substring(6, 2)));
        }

        public static DateTime GetSamplingTime(tDateTime time)
        {
            try
            {

                return new DateTime(2000 + Convert.ToInt32(time.u8Year),
                    Convert.ToInt32(time.u8Month),
                    Convert.ToInt32(time.u8Day),
                    Convert.ToInt32(time.u8Hour),
                    Convert.ToInt32(time.u8Min),
                    Convert.ToInt32(time.u8Sec)

                    );
            }
            catch
            {
                return System.DateTime.Now;
            }
        }

        #endregion

        #region 将字符型mac转换成byte型
        /// <summary>
        /// 将字符型mac转换成byte型
        /// </summary>
        /// <param name="macStr"></param>
        /// <returns></returns>
        public static byte[] GetBytesMACFromString(string macStr)
        {
            byte[] MAC = new byte[8];
            for (int i = 0; i < macStr.Length; i += 2)
            {
                MAC[i / 2] = Convert.ToByte(ConvertUtility.Convert16To10(macStr.Substring(i, 2)));
            }
            return MAC;
        }
        #endregion

        #region 获取版本信息
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <param name="ver"></param>
        /// <returns></returns>
        public static string GetVersion(tVer ver)
        {
            return ((int)ver.u8Main).ToString() +
                   "." + ((int)ver.u8Sub).ToString() +
                   "." + ((int)ver.u8Rev).ToString() +
                   "." + ((int)ver.u8Build).ToString();


        }
        #endregion

        #region  根据MAC地址获取下发测量定义列表索引
        /// <summary>
        /// add by masu 2016年2月19日08:59:03 根据MAC地址获取下发测量定义列表索引
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static int GetIndexOfSendMeasDefineList(string mac)
        {
            SendMeasureDefine measureDefine = sendMeasureDefineList.Where(p => p.MAC.ToString().ToUpper() == mac.ToString().ToUpper()).ToList().FirstOrDefault();
            return sendMeasureDefineList.IndexOf(measureDefine);
        }
        #endregion

        #region  根据MAC地址获取下发触发式定义列表索引
        /// <summary>
        /// 根据MAC地址获取下发触发式定义列表索引
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static int GetIndexOfSendTriggerDefineList(string mac)
        {
            SendTriggerDefine triggerDefine = sendTriggerDefineList.Where(p => p.MAC.ToString().ToUpper() == mac.ToString().ToUpper()).ToList().FirstOrDefault();
            return sendTriggerDefineList.IndexOf(triggerDefine);
        }
        #endregion

        #region Judge the legitimacy of the upgrade file
        /// <summary>
        /// 判断升级文件的合法性
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static bool JudgeLegitimacyOfUpgradeFile(byte[] buffer)
        {
            bool result = false;
            if (buffer != null && buffer.Length > 0)
            {
                //add by masu 2016年3月2日15:33:10 判断下发固件升级文件是否正确
                //获取升级文件中魔术数字
                int magicWord = ConvertUtility.ConvertToInt32(new byte[] { buffer[3], buffer[2], buffer[1], buffer[0] }, 0);
                //获取升级的版本号
                UpdateVer.u8Main  = buffer[4];
                UpdateVer.u8Sub   = buffer[5];
                UpdateVer.u8Rev   = buffer[6];
                UpdateVer.u8Build = buffer[7];
                //获取升级文件中包数大小
                UpdateTotalNum = ConvertUtility.ConvertToInt16(new byte[] { buffer[13], buffer[12] }, 0);
                 
                //获取固件大小
                int fwSize = ConvertUtility.ConvertToInt32(new byte[] { buffer[11], buffer[10], buffer[9], buffer[8] }, 0);
                //获取升级文件中单包数据大小
                int singlePacketSize = Convert.ToInt32(buffer[14]);
                //获取总包个数
                int dataTotalCount = (buffer.Length - 19) / singlePacketSize;
                //获取真实总包数目的余数
                int dataTotalCountRemainder = (buffer.Length - 19) % singlePacketSize;
                if (dataTotalCountRemainder > 0)
                {
                    dataTotalCount += 1;
                }
                if (dataTotalCount == UpdateTotalNum && (buffer.Length - 19) == fwSize)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        #endregion

        #region 根据MAC地址获取更新WSID索引
        /// <summary>
        /// add by masu 2016年2月22日11:40:30 据MAC地址获取更新WSID索引
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static int GetIndexOfSetNewWSIDList(string mac)
        {
            SetNewWSID setNewWSID = setNewWSIDList.Where(p => p.MAC.ToString().ToUpper() == mac.ToString().ToUpper()).ToList().First();
            return setNewWSIDList.IndexOf(setNewWSID);
        }
        #endregion

        #region 根据MAC地址获取更新NetworkID索引
        /// <summary>
        /// add by masu 2016年2月22日13:20:59 据MAC地址获取更新NetworkID索引
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static int GetIndexOfSetNetworkIDList(string mac)
        {
            SetNetworkID setNetworkID = setNetworkIDList.Where(p => p.MAC.ToString().ToUpper() == mac.ToString().ToUpper()).ToList().First();
            return setNetworkIDList.IndexOf(setNetworkID);
        }

        #endregion

        #region 根据MAC地址获取更新SN串码索引
        /// <summary>
        /// add by masu 2016年2月22日13:56:56 根据MAC地址获取更新SN串码索引
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static int GetIndexOfSetSNCodeList(string mac)
        {
            SetSNCode setSNCode = setSNCodeList.Where(p => p.MAC.ToString().ToUpper() == mac.ToString().ToUpper()).ToList().FirstOrDefault();
            return setSNCodeList.IndexOf(setSNCode);
        }
        #endregion

        #region 根据MAC地址获取传感器校准索引
        /// <summary>
        /// add by masu 2016年2月22日14:22:58 根据MAC地址获取传感器校准索引
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static int GetIndexOfCheckMonitorList(string mac)
        {
            CheckMonitor checkMonitor = checkMonitorList.Where(p => p.MAC.ToString().ToUpper() == mac.ToString().ToUpper()).ToList().FirstOrDefault();
            return checkMonitorList.IndexOf(checkMonitor);
        }
        #endregion

        #region 判断接收波形数据是否完整
        /// <summary>
        /// 判断接收波形数据是否完整
        /// </summary>
        /// <param name="_WSWaveInfo"></param>
        /// <returns></returns>
        public static bool CheckDataIsFull(WSWaveInfo _WSWaveInfo)
        {
            bool isfull = true;
            lock (_WSWaveInfo)
            {
                //var list = _WSWaveInfo.ReceiveDataNumber.Where(obj => obj.ToString().Trim() == "0").ToList();
                //if (list != null && list.Count > 0)
                //{
                //    isfull = false;
                //}
                if (_WSWaveInfo.WaveDescInfo == null)
                {
                    return false;
                }
                else if (_WSWaveInfo.WaveDescInfo.SamplingTime == null)
                {
                    return false;
                }
                for (int i = 1; i < _WSWaveInfo.ReceiveDataNumber.Length; i++)
                {
                    if (_WSWaveInfo.ReceiveDataNumber[i] == 0)
                    {
                        isfull = false;
                        break;
                    }
                }

            }
            return isfull;
        }
        #endregion

        

        #region 根据配置确定日志记录详细情况
        public static void InitLogType()
        {
            try
            {
                string strType = GetAppConfig("DetailLog");
                detailLog = (strType.Trim() == "1" ? true : false);
            }
            catch
            {
                detailLog = false;
            }
        }
        #endregion

        #region 创建各类采集数据的 缓存对象
        public static CacheOfSamplingData CreateCacheData<T>(T Data, EnumCacheType type)
        {
            CacheOfSamplingData backData = new CacheOfSamplingData();
            backData.CacheType = type;
            switch (type)
            {
                case EnumCacheType.CharacterValue:
                    backData.Mac = (Data as tEigenValueParam).mac.ToHexString();
                    backData.SamplingTime = GetSamplingTime((Data as tEigenValueParam).SampleTime);
                    break;
                case EnumCacheType.TmpVoltage:
                    backData.Mac = (Data as tTmpVoltageParam).mac.ToHexString();
                    backData.SamplingTime = GetSamplingTime((Data as tTmpVoltageParam).SampleTime);
                    break;
                case EnumCacheType.CriticalValue:
                    backData.Mac = (Data as tRevStopParam).mac.ToHexString();
                    backData.SamplingTime = GetSamplingTime((Data as tRevStopParam).SampleTime);
                    break;
                case EnumCacheType.LQValue:
                    backData.Mac = (Data as tLQParam).mac.ToHexString();
                    backData.SamplingTime = GetSamplingTime((Data as tLQParam).SampleTime);
                    break;
                default:
                    backData.Mac = (Data as WSWaveInfo).MAC;
                    backData.SamplingTime = (Data as WSWaveInfo).WaveDescInfo.SamplingTime;
                    break;

            }


            return backData;
        }
        #endregion

        #region  判断采集数据是否重复上传
        public static bool IsExisted(CacheOfSamplingData checkData)
        {
            lock (cacheDate)
            {
                CacheOfSamplingData samplingData = cacheDate.Where(obj => obj.Mac == checkData.Mac && obj.CacheType == checkData.CacheType && obj.SamplingTime == checkData.SamplingTime).ToList().FirstOrDefault();
                if (samplingData != null)
                {

                    return true;
                }
                else
                {

                    samplingData = null;
                    samplingData = cacheDate.Where(obj => obj.Mac == checkData.Mac && obj.CacheType == checkData.CacheType).ToList().FirstOrDefault();
                    try
                    {

                        if (samplingData != null)
                        {
                            cacheDate.Remove(samplingData);
                            cacheDate.Add(checkData);
                        }
                        else
                        {

                            cacheDate.Add(checkData);
                        }

                    }
                    catch
                    {
                        return false;

                    }

                    return false;
                }
            }

        }
        #endregion

        public static string CreateRequest(EnumRequestType requestType, string methodName, string parameter)
        {
            string serviceName = string.Empty;
            switch (requestType)
            {
                case EnumRequestType.GetSensorsInfo:
                    serviceName = GetAppConfig("GetSensorsInfo");
                    break;
                case EnumRequestType.UpLoadData:
                    serviceName = GetAppConfig("UpLoadData");
                    break;
            }
            RestClient client = new RestClient(GetAppConfig("iCMSServer") + "/" + serviceName);
            return client.Post(parameter, methodName);

        }
    }
}