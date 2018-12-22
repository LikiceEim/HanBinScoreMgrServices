/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common.Enum
 *文件名：  EnumLogType
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：日志类型枚举
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Common.Enum
{
    public enum EnumLogType
    {
        /// <summary>
        /// iCMS.WG.Agent运行日志
        /// </summary>
        [StringValue("Log")]
        Log = 1,
        /// <summary>
        /// 错误日志
        /// </summary>
        [StringValue("Error")]
        Error,
        /// <summary>
        /// 记录有上层Web->Server->Agent的操作日志
        /// </summary>
        [StringValue("CommunicationAgent2WS")]
        Agent2WS,
        /// <summary>
        /// 记录底层上传给Agent的操作日志
        /// </summary>
        [StringValue("CommunicationWS2Agent")]
        WS2Agent,
        /// <summary>
        /// 测试日志
        /// </summary>
       [StringValue("Test")]
        Test,

        /// <summary>
        /// 测试日志
        /// </summary>
       [StringValue("Cache")]
       Cache,
        /// <summary>
        /// 非法数据日志
        /// </summary>
       [StringValue("InvalidData")]
       InvalidData

    }

    public class StringValue : System.Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }
}
