/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.AgentServer
 *文件名：  ConfigService
 *创建人：  LF
 *创建时间：2016/10/14 10:10:19
 *描述：iCMS.WG.Agent 服务
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

using iCMS.WG.Agent.Model;

namespace iCMS.WG.AgentServer
{
   
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "jsonpcallback")]

    public class ConfigService : IConfigService
    {
        /// <summary>
        /// 下发测量定义
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string ConfigMeasureDefine(Stream stream)
        {
            ConfigMeasureDefineTaskModel param = new ConfigMeasureDefineTaskModel();
            UploadResult result = new UploadResult();
            try
            {
                StreamReader sr = new StreamReader(stream);

                string content = sr.ReadToEnd();
                sr.Dispose();
                NameValueCollection nvc = HttpUtility.ParseQueryString(content);
                param = Json.Parse<ConfigMeasureDefineTaskModel>(nvc["content"]);

               
                if (iCMS.WG.AgentServer.Common.syncTools == null)
                {
                    Common.Init();
                }
                Common.asyncTools.AddCmd(param);
                result.Result = 0;
               
            }
            catch
            {
                result.Result = 1;
                result.Reason = "发生错误无法受理请求";
            }

            return Json.Stringify(result);
        }
        /// <summary>
        /// 升级传感器
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string UpdateFirmware(Stream stream)
        {
            UpdateFirmwareTaskModel param = new UpdateFirmwareTaskModel();
            UploadResult result = new UploadResult();
            try
            {

                StreamReader sr = new StreamReader(stream);

                string content = sr.ReadToEnd();
                sr.Dispose();
                NameValueCollection nvc = HttpUtility.ParseQueryString(content);
                param = Json.Parse<UpdateFirmwareTaskModel>(nvc["content"]);


         
                if (iCMS.WG.AgentServer.Common.syncTools == null)
                {
                    Common.Init();
                }
                Common.asyncTools.AddCmd(param);
                result.Result = 0;
               
            }
            catch
            {
                result.Result = 1;
                result.Reason = "发生错误无法受理请求";
            }

            return Json.Stringify(result);
        }
        /// <summary>
        /// 获取触感器信息
        /// </summary>
        public void GetWSInfo()
        {
            TaskModelBase param = new TaskModelBase();
            UploadResult result = new UploadResult();
            try
            {
                param.operatorName = "RefreshWSStatesOper";
                if (iCMS.WG.AgentServer.Common.syncTools == null)
                {
                    Common.Init();
                }
                Common.asyncTools.AddCmd(param);
            }
            catch
            {

            }
        }
         
		
        /// <summary>
        /// //获取网络中所有WS的状态
        /// </summary>
        public void GetAllWSInfo()
        {
            TaskModelBase param = new TaskModelBase();
            UploadResult result = new UploadResult();
            try
            {
                param.operatorName = "RefreshAllWSStatesOper";
                if (iCMS.WG.AgentServer.Common.syncTools == null)
                {
                    Common.Init();
                }
                Common.asyncTools.AddCmd(param);
                result.Result = 0;
               
            }
            catch
            {
                result.Result = 1;
                result.Reason = "发生错误无法受理请求";
            }
        }
        /// <summary>
        /// 启停机
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string SetStatusOfCritical(Stream stream)
        {
            SetWSnStatesModel param = new SetWSnStatesModel();
            UploadResult result = new UploadResult();
            try
            {
                StreamReader sr = new StreamReader(stream);

                string content = sr.ReadToEnd();
                sr.Dispose();
                NameValueCollection nvc = HttpUtility.ParseQueryString(content);
                param = Json.Parse<SetWSnStatesModel>(nvc["content"]);
               
                if (iCMS.WG.AgentServer.Common.syncTools == null)
                {
                    Common.Init();
                }
                Common.asyncTools.AddCmd(param);
                result.Result = 0;
               
            }
            catch
            {
                result.Result = 1;
                result.Reason = "发生错误无法受理请求";
            }

            return Json.Stringify(result);

        }
        /// <summary>
        /// c触发式上传
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string ConfigTriggerDefine(Stream  stream)
        {
            ConfigTriggerDefineTaskModel param = new ConfigTriggerDefineTaskModel();
            UploadResult result = new UploadResult();
            try
            {
                StreamReader sr = new StreamReader(stream);

                string content = sr.ReadToEnd();
                sr.Dispose();
                NameValueCollection nvc = HttpUtility.ParseQueryString(content);
                param = Json.Parse<ConfigTriggerDefineTaskModel>(nvc["content"]);


                if (iCMS.WG.AgentServer.Common.syncTools == null)
                {
                    Common.Init();
                }
                Common.asyncTools.AddCmd(param);
                result.Result = 0;
               
            }
            catch
            {
                result.Result = 1;
                result.Reason = "发生错误无法受理请求";
            }

            return Json.Stringify(result);
        }

        #region 验证是否可以访问
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-09-07
        /// 验证是否可以访问,成功1
        /// </summary>
        /// <returns>是否可以访问</returns>
        public string IsAccess()
        {
            return "1";
        }
        #endregion
        /// <summary>
        ///重启Agent
        /// </summary>
        public void ReSetAgent()
        {
            TaskModelBase param = new TaskModelBase();
            UploadResult result = new UploadResult();
            try
            {
                param.operatorName = "ReSetAgentOper";
                if (iCMS.WG.AgentServer.Common.syncTools == null)
                {
                    Common.Init();
                }
                Common.asyncTools.AddCmd(param);
                result.Result = 0;
               
            }
            catch
            {
                result.Result = 1;
                result.Reason = "发生错误无法受理请求";
            }
        }
    }
}
