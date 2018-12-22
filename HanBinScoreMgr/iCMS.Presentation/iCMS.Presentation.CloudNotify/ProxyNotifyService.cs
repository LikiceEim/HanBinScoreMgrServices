/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Presentation.CloudNotify
 *文件名：  ProxyNotifyService
 *创建人：  张辽阔
 *创建时间：2016-12-08
 *描述：云代理通知服务类
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;

using iCMS.Presentation.Common;
using iCMS.Presentation.CloudNotify.Common;
using iCMS.Common.Component.Data.Request.CloudProxy;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Tool.Extensions;
using iCMS.Common.Component.Data.Response.Cloud;
using iCMS.Common.Component.Data.Request.Cloud;

namespace iCMS.Presentation.CloudProxy
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-12-08
    /// 创建记录：云代理通知服务类
    /// </summary>
    public class ProxyNotifyService : BaseService, IProxyNotifyService
    {
        /// <summary>
        /// 请求的路径
        /// </summary>
        private readonly string requestURL = ConfigurationManager.AppSettings["CloudCommunicationURL"] + "/CloudCommunication/CloudcommunicationService";

        /// <summary>
        /// 请求的方法
        /// </summary>
        private const string requestMethod = "ReceiveCloudProxyNotify";

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-08
        /// 创建记录：接收触发源的请求和给云通讯发送数据到达通知
        /// </summary>
        /// <param name="parameter">接收触发源的请求和给云通讯发送数据到达通知的参数</param>
        public void ReceiveTriggerAndRequestCloudCommunication(ReceiveTriggerAndRequestCloudCommunicationParameter parameter)
        {
            Task.Run(() =>
            {
                try
                {
                    if (ValidateData<ReceiveTriggerAndRequestCloudCommunicationParameter>(parameter) && parameter.PlatformId > 0)
                    {
                        lock (PlatformCacheData.MuiltPlatformCacheData)
                        {
                            PlatformDataStatus platformDataStatus;
                            //如果已存在该平台的信息
                            if (PlatformCacheData.MuiltPlatformCacheData.TryGetValue(parameter.PlatformId, out platformDataStatus))
                                platformDataStatus.DataCount += 1;
                            //如果未存在该平台的信息
                            else
                            {
                                platformDataStatus = new PlatformDataStatus();
                                PlatformCacheData.MuiltPlatformCacheData.Add(parameter.PlatformId, platformDataStatus);

                                //给云通讯发送通知
                                RestClient client = new RestClient(requestURL);
                                ReceiveCloudProxyNotifyParameter receiveCloudProxyNotifyPara = new ReceiveCloudProxyNotifyParameter();
                                receiveCloudProxyNotifyPara.PlatformId = parameter.PlatformId;
                                string result = client.Post(receiveCloudProxyNotifyPara.ToClientString(), requestMethod);
                                //定义委托启动定时器
                                Action startTimer = () =>
                                {
                                    platformDataStatus.PlatformTimer.Elapsed += (s, e) => PlatformTimer_Elapsed(s, e, parameter.PlatformId);
                                    platformDataStatus.PlatformTimer.Interval = 60000;
                                    platformDataStatus.PlatformTimer.Start();
                                };

                                //如果该路径请求失败，则启动定时器
                                if (result.Equals(requestURL + "/" + requestMethod))
                                    startTimer();
                                else
                                {
                                    BaseResponse<ReceiveCloudProxyNotifyResult> resultObj = Json.JsonDeserialize<BaseResponse<ReceiveCloudProxyNotifyResult>>(result);
                                    //如果该路径请求失败，则启动定时器
                                    if (!(resultObj != null && resultObj.IsSuccessful && resultObj.Result.PlatformId == parameter.PlatformId))
                                        startTimer();
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(e);
                }
            });
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-12
        /// 创建记录：云代理定时器方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlatformTimer_Elapsed(object sender, ElapsedEventArgs e, int platformId)
        {
            Action stopTimer = () =>
            {
                lock (PlatformCacheData.MuiltPlatformCacheData)
                {
                    PlatformDataStatus tempDataStatus;
                    if (PlatformCacheData.MuiltPlatformCacheData.TryGetValue(platformId, out tempDataStatus))
                    {
                        tempDataStatus.PlatformTimer.Stop();
                        tempDataStatus.PlatformTimer.Close();
                        tempDataStatus.PlatformTimer.Dispose();
                        PlatformCacheData.MuiltPlatformCacheData.Remove(platformId);
                    }
                }
            };

            try
            {
                //给云通讯发送通知
                RestClient client = new RestClient(requestURL);
                ReceiveTriggerAndRequestCloudCommunicationParameter parameter = new ReceiveTriggerAndRequestCloudCommunicationParameter
                {
                    PlatformId = platformId
                };
                string result = client.Post(parameter.ToClientString(), requestMethod, 30000);
                //如果该路径请求成功，则停止定时器
                if (!result.Equals(requestURL + "/" + requestMethod))
                {
                    BaseResponse<ReceiveCloudProxyNotifyResult> resultObj = Json.JsonDeserialize<BaseResponse<ReceiveCloudProxyNotifyResult>>(result);
                    if (resultObj != null && resultObj.IsSuccessful && resultObj.Result.PlatformId == platformId)
                        stopTimer();
                }
            }
            catch (Exception ex)
            {
                stopTimer();
                LogHelper.WriteLog(ex);
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-12-08
        /// 创建记录：云通讯请求该接口获知是否有数据
        /// </summary>
        /// <param name="parameter">云通讯请求该接口获知是否有数据的参数</param>
        /// <returns></returns>
        public BaseResponse<bool> ReceiveCloudCommunicationNotify(ReceiveCloudCommunicationNotifyParameter parameter)
        {
            return Task.Run<BaseResponse<bool>>(() =>
            {
                BaseResponse<bool> response = new BaseResponse<bool>();
                Func<bool, bool> clearPlatformInfo = (IsError) =>
                {
                    lock (PlatformCacheData.MuiltPlatformCacheData)
                    {
                        bool IsData = false;
                        PlatformDataStatus tempDataStatus;
                        if (IsError)
                        {
                            PlatformCacheData.MuiltPlatformCacheData.TryGetValue(parameter.PlatformId, out tempDataStatus);
                        }
                        else
                        {
                            //如果该平台还有数据，则为true否则为false
                            IsData = PlatformCacheData.MuiltPlatformCacheData.TryGetValue(parameter.PlatformId, out tempDataStatus)
                                && (tempDataStatus.DataCount - parameter.ProcessCount) > 0;
                        }

                        //停止定时器并清除该平台的信息
                        if (tempDataStatus != null)
                        {
                            tempDataStatus.PlatformTimer.Close();
                            tempDataStatus.PlatformTimer.Dispose();
                            PlatformCacheData.MuiltPlatformCacheData.Remove(parameter.PlatformId);
                        }
                        return IsData;
                    }
                };

                try
                {
                    if (ValidateData<ReceiveCloudCommunicationNotifyParameter>(parameter) && parameter.PlatformId > 0)
                    {
                        response.IsSuccessful = true;
                        response.Result = clearPlatformInfo(false);
                        return response;
                    }
                    else
                    {
                        clearPlatformInfo(true);
                        response.IsSuccessful = false;
                        response.Result = false;
                        return response;
                    }
                }
                catch (Exception e)
                {
                    clearPlatformInfo(true);
                    LogHelper.WriteLog(e);
                    response.IsSuccessful = false;
                    response.Result = false;
                    return response;
                }
            })
            .Result;
        }
    }
}