/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Frameworks.Core.Repository
 *文件名：  CommunicationHelper
 *创建人：  QXM
 *创建时间：2017-01-16
 *描述：   云平台推送类
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

using iCMS.Cloud.JiaXun.Commons;
using iCMS.Cloud.JiaXun.Interface;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Tool.Extensions;

namespace iCMS.Communication.PushManually
{
    public class CommunicationHelper
    {
        public static string SendData(string msg, string url, string method = null)
        {
            string result = string.Empty;

            string baseURL = ConfigurationManager.AppSettings["CloudAddress"];
            if (baseURL == null || baseURL.Length == 0)
            {
                return "URL路径无效";
            }

            string finalURL = CreateCloudURL(baseURL, url, method);
            Uri address = new Uri(finalURL);

            // Create the web request
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            // Set type to POST
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Create the data we want to send
            StringBuilder sendData = new StringBuilder();
            sendData.Append(CommonVariate.HTTP_Request_Para_SessionID_Name + HttpUtility.UrlEncode("0"));
            sendData.Append(CommonVariate.HTTP_Request_Para_Content_Name + HttpUtility.UrlEncode(msg));

            // Create a byte array of the data we want to send
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(sendData.ToString());

            // Set the content length in the request headers
            request.ContentLength = byteData.Length;

            // Write data
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }
            string uri = string.Empty;
            // Get response
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream
                StreamReader reader = new StreamReader(response.GetResponseStream());

                //解析返回结果
                result = reader.ReadToEnd();
                uri = response.ResponseUri.ToString();
            }

            #region 添加记录日志
            string IsOpenLog = ConfigurationManager.AppSettings["IsOpenLog"];
            //如果为1,则开启日志
            if (IsOpenLog == "1")
            {
                LogHelper.WriteLog("--------云推送日志开始-----------");
                string logger = string.Format("URL:{0}  SessionID:{1}", address, "0");
                LogHelper.WriteLog(logger);
                string content = string.Format("Content:{0}", msg);
                LogHelper.WriteLog(content);
                LogHelper.WriteLog("日志记录时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                string logResult = string.Format("Result:{0}", result);
                LogHelper.WriteLog(logResult);
                LogHelper.WriteLog("--------云推送日志结束-----------");

            }

            #endregion
            return result;
        }

        public static string SendWaveData(string waveMsg, string url)
        {
            string result = string.Empty;

            string URL = ConfigurationManager.AppSettings["CloudAddress"];
            if (URL == null || URL.Length == 0)
            {
                return "URL路径无效";
            }

            Uri address = new Uri(URL + url);

            // Create the web request
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            // Set type to POST
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Create a byte array of the data we want to send
            byte[] byteData = GetSendedWaveData(waveMsg);
            // Set the content length in the request headers
            request.ContentLength = byteData.Length;

            // Write data
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            // Get response
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream
                StreamReader reader = new StreamReader(response.GetResponseStream());

                //解析返回结果
                result = reader.ReadToEnd();
            }

            #region 添加记录日志
            string IsOpenLog = ConfigurationManager.AppSettings["IsOpenLog"];
            //如果为1,则开启日志
            if (IsOpenLog == "1")
            {
                LogHelper.WriteLog("--------云推送日志开始-----------");
                string logger = string.Format("URL:{0}  SessionID:{1}", address, "0");
                LogHelper.WriteLog(logger);
                string content = string.Format("Content:{0}", waveMsg);
                LogHelper.WriteLog(content);
                LogHelper.WriteLog("日志记录时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                string logResult = string.Format("Result:{0}", result);
                LogHelper.WriteLog(logResult);
                LogHelper.WriteLog("--------云推送日志结束-----------");
            }

            #endregion
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        static byte[] GetSendedWaveData(string msg)
        {
            List<WaveData> waves = Json.Parse<List<WaveData>>(msg);
            WaveData wave = waves.FirstOrDefault();

            WaveDataParam param = new WaveDataParam()
            {
                colletTime = wave.colletTime,
                frequency = wave.frequency,
                sampleSize = wave.sampleSize,
                signalId = wave.signalId,
                frequencyDomainX = CalculateWaveXYValue.GetWaveValueByteString(CalculateWaveXYValue.GetWaveValueBytes(GetFloatValues(wave.frequencyDomainX))),
                frequencyDomainY = CalculateWaveXYValue.GetWaveValueByteString(CalculateWaveXYValue.GetWaveValueBytes(GetFloatValues(wave.frequencyDomainY))),
                timeDomainX = CalculateWaveXYValue.GetWaveValueByteString(CalculateWaveXYValue.GetWaveValueBytes(GetFloatValues(wave.timeDomainX))),
                timeDomainY = CalculateWaveXYValue.GetWaveValueByteString(CalculateWaveXYValue.GetWaveValueBytes(GetFloatValues(wave.timeDomainY))),
            };

            StringBuilder sendData = new StringBuilder();
            sendData.Append(CommonVariate.HTTP_Request_Para_SessionID_Name + HttpUtility.UrlEncode("0"));
            sendData.Append(CommonVariate.HTTP_Request_Para_Content_Name + HttpUtility.UrlEncode(Json.Stringify(param)));

            byte[] data = UTF8Encoding.UTF8.GetBytes(sendData.ToString());

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        static List<float> GetFloatValues(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return null;
            }

            List<float> fValues = new List<float>();
            string[] values = strValue.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                fValues.Add(Convert.ToSingle(values[i]));
            }
            return fValues;
        }


        public static string CreateCloudURL(string cloudPushBaseURL, string type, string method)
        {
            string url = cloudPushBaseURL + "/";

            url += CommonVariate.Cloud_URL + type;
            if (!string.IsNullOrEmpty(method))
            {
                url = url + CommonVariate.Cloud_URL_Method + method;
            }
            return url;
        }
    }
}
