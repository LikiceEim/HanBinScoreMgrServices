/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.WirelessSensors
 *文件名：  QueryBaseInfoParameter
 *创建人：  LF
 *创建时间：2016/10/19 10:10:19
 *描述：获取网关与传感器对应关系及其他信息的查询参数实体类
 *=====================================================================**/

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 获取网关与传感器对应关系及其他信息的查询参数实体类
    /// <summary>
    /// 获取网关与传感器对应关系及其他信息的查询参数实体类
    /// </summary>
    public class QueryBaseInfoParameter : BaseRequest
    {
        private int wgid = -1;
        private string agentAddress = string.Empty;

        /// <summary>
        /// 网关编号
        /// </summary>
        public int WGID
        {
            get { return wgid; }
            set { wgid = value; /*agentAddress = string.Empty;*/ }
        }


        /// <summary>
        /// Agent对外暴露地址
        /// </summary>
        public string AgentAddress
        {
            get { return agentAddress; }
            set { agentAddress = value; /*wgid = -1;*/ }
        }
    }
    #endregion
}
