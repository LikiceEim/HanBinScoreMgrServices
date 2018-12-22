/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Cloud

 *文件名：  CloudConfigDataParameter
 *创建人：  王颖辉
 *创建时间：2016-12-02
 *描述：配置请求参数
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
namespace iCMS.Common.Component.Data.Request.Cloud
{
    #region 配置请求参数
    /// <summary>
    /// 配置请求参数
    /// </summary>
    public class CloudConfigDataParameter: BaseRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 节点类型
        /// 1、基础数据，2、云平台，3、配置数据，4、日志是否记录，5、数据推送量；
        /// </summary>
        public EnumCloudConfigType Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Value值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 是否启用
        /// 0：未启用，1：启用
        /// </summary>
        public EnumCloudConfigIsUse IsUse { get; set; }
    }
    #endregion

    #region 配置请求参数
    /// <summary>
    /// 配置请求参数
    /// </summary>
    public class DeleteCloudConfigDataParameter: BaseRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
    }
    #endregion
}
