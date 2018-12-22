/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request
 *文件名：  ConfigResponseParameter
 *创建人：  LF
 *创建时间：2016/10/19 10:10:19
 *描述：包含配置无线传感器后的结果信息实体类
 *=====================================================================**/
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.WirelessSensors
{
    #region 包含配置无线传感器后的结果信息实体类
    /// <summary>
    /// 包含配置无线传感器后的结果信息实体类
    /// </summary>
    public class ConfigResponseParameter : BaseRequest
    {
        /// <summary>
        /// 配置/操作类型 1：测量定义；2：升级,3:触发式上传
        /// </summary>
        public int ConfigType { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        public string Reson { get; set; }
        /// <summary>
        /// 配置结果状态码 0：成功；1：失败
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 传感器MAC
        /// </summary>
        public string WSMAC { get; set; }
    }
    #endregion
}
