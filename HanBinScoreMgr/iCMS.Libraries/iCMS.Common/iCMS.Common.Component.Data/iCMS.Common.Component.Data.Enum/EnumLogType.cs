/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Agent.Common.Enum
 *文件名：  EnumLogType
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：日志类型枚举
 *
 *=====================================================================**/

namespace iCMS.Common.Component.Data.Enum
{
    #region 日志类型枚举
    /// <summary>
    /// 日志类型枚举
    /// </summary>
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
        /// 测试日志
        /// </summary>
        [StringValue("Test")]
        Test,

        /// <summary>
        /// 缓存日志
        /// </summary>
        [StringValue("Cache")]
        Cache,
        /// <summary>
        /// 非法数据
        /// </summary>
        [StringValue("InvalidData")]
        InvalidData

    }
    #endregion

    #region 字符串值
    /// <summary>
    ///字符串值
    /// </summary>
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
    #endregion
}
