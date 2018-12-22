/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 * 命名空间：iCMS.WG.Agent
 * 文件名：  WatchdogOperation
 * 创建人：  张辽阔
 * 创建时间：2017-06-05
 * 描述：看门狗管理类
 *=====================================================================**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iLine.WatchdogHelper;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.WirelessSensors;
using iCMS.Common.Component.Data.Response.WirelessSensors;
using iCMS.WG.Agent.Common;
using iCMS.WG.Agent.Common.Enum;

namespace iCMS.WG.Agent
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2017-06-05
    /// 创建记录：看门狗操作类
    /// </summary>
    public static class WatchdogOperation
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-05
        /// 创建记录：是否启动看门狗 1：启动看门狗，其他值为不启动看门狗，默认不启动
        /// </summary>
        private static bool IsStartWatchdog
        {
            get
            {
                try
                {
                    return ComFunction.GetAppConfig("IsStartWatchdog") == "1";
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-05
        /// 创建记录：最大温度采集时间间隔的倍数 不能大于1.5，不能小于1，默认1.5（保留小数位后一位小数）
        /// </summary>
        private static float MaxTemperatureTimeMulti
        {
            get
            {
                try
                {
                    float maxTemperatureTimeMulti;
                    if (float.TryParse(ComFunction.GetAppConfig("MaxTemperatureTimeMulti"), out maxTemperatureTimeMulti))
                    {
                        if (maxTemperatureTimeMulti < 1)
                            maxTemperatureTimeMulti = 1.5f;
                        else if (maxTemperatureTimeMulti > 1.5f)
                            maxTemperatureTimeMulti = 1.5f;
                        return Convert.ToSingle(Math.Round(maxTemperatureTimeMulti, 1));
                    }
                    else
                        return 1.5f;
                }
                catch
                {
                    return 1.5f;
                }
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-05
        /// 创建记录：看门狗是否启动
        /// </summary>
        private static bool IsStartedWatchdog { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-05
        /// 创建记录：操作看门狗时的锁定变量
        /// </summary>
        private static object WatchdogOperation_Lock = new object();

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-09
        /// 创建记录：是否已经提醒“设置不启动看门狗”
        /// </summary>
        private static bool IsWarnNotStartWatchdog { get; set; }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-19
        /// 创建记录：能设置的最大看门狗重启时间
        /// </summary>
        private const uint MaxinumResetTime = 3276700;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-19
        /// 创建记录：能设置的最小看门狗重启时间
        /// </summary>
        private const uint MininumResetTime = 100;

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-07
        /// 创建记录：启动看门狗
        /// </summary>
        /// <param name="wgID">网关ID</param>
        public static void StartWatchdog(int wgID)
        {
            if (wgID <= 0)
            {
                LogHelper.WriteWatchDogLog("WGID错误，未能启动看门狗！");
                return;
            }

            lock (WatchdogOperation_Lock)
            {
                if (IsStartWatchdog)
                {
                    if (IsStartedWatchdog)
                    {
                        LogHelper.WriteWatchDogLog("看门狗已经启动，无法再次启动！");
                        return;
                    }

                    try
                    {
                        int? maxTemperTime = GetMaxTemperatureTimeByWGID(wgID);
                        if (maxTemperTime.HasValue)
                        {
                            //最大温度采集时间间隔毫秒数
                            double resetTimeMillisecond = maxTemperTime.Value * 60 * 1000;
                            if (resetTimeMillisecond > MaxinumResetTime)
                            {
                                LogHelper.WriteWatchDogLog("最大温度采集时间间隔（" + resetTimeMillisecond
                                    + "毫秒）大于看门狗支持的最大重启时间（" + MaxinumResetTime + "毫秒），放弃启动看门狗！");
                                return;
                            }
                            //计算看门狗重启毫秒数
                            double tempResetTime = MaxTemperatureTimeMulti * resetTimeMillisecond;
                            if (tempResetTime < MininumResetTime)
                            {
                                LogHelper.WriteWatchDogLog("设置看门狗重启时间的值小于" + MininumResetTime + "毫秒，放弃启动看门狗！");
                                return;
                            }
                            if (tempResetTime > MaxinumResetTime)
                                tempResetTime = MaxinumResetTime;

                            uint resetTime = Convert.ToUInt32(Math.Floor(tempResetTime));

                            LogHelper.WriteWatchDogLog("设置看门狗重启时间为“" + resetTime + "”毫秒！");

                            string message;
                            if (Watchdog.GetInstance().Start(resetTime, out message))
                            {
                                IsStartedWatchdog = true;
                                LogHelper.WriteWatchDogLog("启动看门狗成功！");

                                //启动定时器
                                WatchdogTimerOperation.WatchdogTimerStart(wgID, resetTimeMillisecond);
                                LogHelper.WriteWatchDogLog("启动定时器成功！");
                            }
                            else
                                LogHelper.WriteWatchDogLog(message);
                        }
                        else
                            LogHelper.WriteWatchDogLog("最大温度采集时间为空！");
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog(e);
                    }
                }
                else
                {
                    if (!IsWarnNotStartWatchdog)
                    {
                        try
                        {
                            LogHelper.WriteWatchDogLog("设置不启动看门狗！");

                            IsWarnNotStartWatchdog = true;
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteLog(e);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-09
        /// 创建记录：重新启动看门狗
        /// </summary>
        /// <param name="wgID">网关ID</param>
        public static void ReStartWatchdog(int wgID)
        {
            if (wgID <= 0)
            {
                LogHelper.WriteWatchDogLog("WGID错误，未能重新启动看门狗！");
                return;
            }

            lock (WatchdogOperation_Lock)
            {
                if (IsStartWatchdog)
                {
                    try
                    {
                        int? maxTemperTime = GetMaxTemperatureTimeByWGID(wgID);
                        if (maxTemperTime.HasValue)
                        {
                            //最大温度采集时间间隔毫秒数
                            double resetTimeMillisecond = maxTemperTime.Value * 60 * 1000;
                            if (resetTimeMillisecond > MaxinumResetTime)
                            {
                                LogHelper.WriteWatchDogLog("最大温度采集时间间隔（" + resetTimeMillisecond
                                    + "毫秒）大于看门狗支持的最大重启时间（" + MaxinumResetTime + "毫秒），放弃重新设置看门狗！");
                                return;
                            }
                            //计算看门狗重启毫秒数
                            double tempResetTime = MaxTemperatureTimeMulti * resetTimeMillisecond;
                            if (tempResetTime < MininumResetTime)
                            {
                                LogHelper.WriteWatchDogLog("设置看门狗重启时间的值小于" + MininumResetTime + "毫秒，放弃重新设置看门狗！");
                                return;
                            }
                            if (tempResetTime > MaxinumResetTime)
                                tempResetTime = MaxinumResetTime;

                            uint resetTime = Convert.ToUInt32(Math.Floor(tempResetTime));

                            LogHelper.WriteWatchDogLog("设置看门狗重启时间为“" + resetTime + "”毫秒！");

                            string message;
                            if (Watchdog.GetInstance().Stop(out message))
                            {
                                if (Watchdog.GetInstance().Start(resetTime, out message))
                                {
                                    IsStartedWatchdog = true;
                                    LogHelper.WriteWatchDogLog("重新设置看门狗成功！");

                                    //启动定时器
                                    WatchdogTimerOperation.WatchdogTimerStart(wgID, resetTimeMillisecond);
                                    LogHelper.WriteWatchDogLog("重新设置定时器成功！");
                                }
                                else
                                    LogHelper.WriteWatchDogLog(message);
                            }
                            else
                                LogHelper.WriteWatchDogLog(message);
                        }
                        else
                            LogHelper.WriteWatchDogLog("最大温度采集时间为空！");
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog(e);
                    }
                }
                else
                {
                    if (!IsWarnNotStartWatchdog)
                    {
                        try
                        {
                            LogHelper.WriteWatchDogLog("设置不启动看门狗！");

                            StopWatchdog();

                            IsWarnNotStartWatchdog = true;
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteLog(e);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-07
        /// 创建记录：投喂看门狗
        /// </summary>
        public static void TriggerWatchdog()
        {
            lock (WatchdogOperation_Lock)
            {
                if (IsStartWatchdog)
                {
                    if (IsStartedWatchdog)
                    {
                        try
                        {
                            string message;
                            if (Watchdog.GetInstance().Trigger(out message))
                                LogHelper.WriteWatchDogLog("投喂看门狗成功！");
                            else
                                LogHelper.WriteWatchDogLog(message);
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteLog(e);
                        }
                    }
                    else
                        LogHelper.WriteWatchDogLog("看门狗已经停止，无法投喂！");
                }
                else
                {
                    if (!IsWarnNotStartWatchdog)
                    {
                        try
                        {
                            LogHelper.WriteWatchDogLog("设置不启动看门狗！");

                            StopWatchdog();

                            IsWarnNotStartWatchdog = true;
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteLog(e);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-07
        /// 创建记录：停止看门狗
        /// </summary>
        public static void StopWatchdog()
        {
            lock (WatchdogOperation_Lock)
            {
                if (IsStartedWatchdog)
                {
                    try
                    {
                        string message;
                        if (Watchdog.GetInstance().Stop(out message))
                        {
                            IsStartedWatchdog = false;
                            IsWarnNotStartWatchdog = false;
                            LogHelper.WriteWatchDogLog("停止看门狗成功！");

                            //停止定时器
                            WatchdogTimerOperation.WatchdogTimerStop();
                            LogHelper.WriteWatchDogLog("停止定时器成功！");
                        }
                        else
                            LogHelper.WriteWatchDogLog(message);
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog(e);
                    }
                }
                else
                    LogHelper.WriteWatchDogLog("看门狗已经停止，无法再次停止！");
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-09
        /// 创建记录：获取最大温度采集时间间隔
        /// </summary>
        /// <param name="wgID">网关ID</param>
        /// <returns></returns>
        private static int? GetMaxTemperatureTimeByWGID(int wgID)
        {
            if (wgID <= 0)
            {
                LogHelper.WriteWatchDogLog("WGID错误，未能读取最大温度采集时间间隔！");
                return null;
            }

            //请求地址
            string requestURL = ComFunction.GetAppConfig("iCMSServer") + "/"
                + ComFunction.GetAppConfig("GetSensorsInfo")
                + "/GetMaxTemperatureTimeByWGID";
            //请求结果
            string requestResult;
            //请求次数
            int requestNumber = 0;
            while (true)
            {
                requestResult =
                    ComFunction.CreateRequest(EnumRequestType.GetSensorsInfo, "GetMaxTemperatureTimeByWGID",
                        new GetMaxTemperatureTimeByWGIDParameter
                        {
                            WGID = wgID
                        }
                        .ToClientString()
                    );
                if (requestResult != requestURL)
                {
                    LogHelper.WriteWatchDogLog("请求最大温度采集时间间隔成功！");
                    break;
                }

                requestNumber++;
                LogHelper.WriteWatchDogLog("请求最大温度采集时间间隔已请求“" + requestNumber + "”次！");
                if (requestNumber == 10)
                    return null;
                System.Threading.Thread.Sleep(6000);
            }
            //转换请求的结果
            BaseResponse<GetMaxTemperatureTimeByWGIDResult> result =
                ComFunction.ParseFormJson<BaseResponse<GetMaxTemperatureTimeByWGIDResult>>(requestResult);
            if (result != null)
            {
                if (result.IsSuccessful && result.Result != null)
                {
                    if (result.Result.TemperatureTime.HasValue)
                        return result.Result.TemperatureTime.Value;
                    else
                        LogHelper.WriteWatchDogLog("最大温度采集时间为空！");
                }
                else
                    LogHelper.WriteWatchDogLog("错误编码：" + result.Code + "，错误信息：" + result.Reason);
            }
            return null;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2017-06-26
        /// 创建记录：看门狗定时器操作类
        /// </summary>
        private static class WatchdogTimerOperation
        {
            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2017-06-26
            /// 创建记录：看门狗定时器
            /// </summary>
            private static System.Timers.Timer WatchdogTimer;

            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2017-06-26
            /// 创建记录：启动看门狗定时器（定时器定时触发时间为最大温度采集时间间隔）
            /// </summary>
            /// <param name="wgID">网关ID</param>
            /// <param name="interval">看门狗启动的时间间隔</param>
            internal static void WatchdogTimerStart(int wgID, double interval)
            {
                if (wgID <= 0 || interval <= 0)
                    return;

                WatchdogTimerStop();

                WatchdogTimer = new System.Timers.Timer();

                WatchdogTimer.Interval = interval;
                WatchdogTimer.Elapsed += (s, e) =>
                {
                    try
                    {
                        bool deviceStatusAllOff;
                        lock (ComFunction.deviceStatus)
                        {
                            deviceStatusAllOff = ComFunction.deviceStatus.All(item => item.WGID == wgID.ToString() && item.WSLinkstatu == "0");
                        }
                        //如果全部的WS连接状态为断开
                        if (deviceStatusAllOff)
                        {
                            LogHelper.WriteWatchDogLog("所有的传感器都已经断开，开始停止看门狗！");
                            WatchdogOperation.StopWatchdog();

                            WatchdogTimerStart(wgID);
                        }
                    }
                    catch
                    {
                    }
                };
                WatchdogTimer.Start();
            }

            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2017-06-26
            /// 创建记录：停止看门狗定时器
            /// </summary>
            internal static void WatchdogTimerStop()
            {
                if (WatchdogTimer != null)
                {
                    WatchdogTimer.Stop();
                    WatchdogTimer.Close();
                    WatchdogTimer.Dispose();
                    WatchdogTimer = null;
                }
            }

            /// <summary>
            /// 创建人：张辽阔
            /// 创建时间：2017-06-26
            /// 创建记录：启动看门狗定时器（定时器定时触发时间为一分钟）
            /// </summary>
            /// <param name="wgID">网关ID</param>
            /// <param name="Interval">看门狗启动的时间间隔</param>
            private static void WatchdogTimerStart(int wgID)
            {
                if (wgID <= 0)
                    return;

                WatchdogTimerStop();

                WatchdogTimer = new System.Timers.Timer();

                WatchdogTimer.Interval = 60000;
                WatchdogTimer.Elapsed += (s, e) =>
                {
                    try
                    {
                        bool deviceStatusAnyOn;
                        lock (ComFunction.deviceStatus)
                        {
                            deviceStatusAnyOn = ComFunction.deviceStatus.Any(item => item.WGID == wgID.ToString() && item.WSLinkstatu == "1");
                        }
                        //如果有WS连接状态为连接
                        if (deviceStatusAnyOn)
                        {
                            WatchdogTimerStop();
                            LogHelper.WriteWatchDogLog("有传感器已经连接，停止定时器！");

                            LogHelper.WriteWatchDogLog("有传感器已经连接，开始启动看门狗！");
                            WatchdogOperation.StartWatchdog(wgID);
                        }
                    }
                    catch
                    {
                    }
                };
                WatchdogTimer.Start();
            }
        }
    }
}