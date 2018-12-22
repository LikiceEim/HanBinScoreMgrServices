/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.SystemInitSets
 *文件名：  AddConfigParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：添加模块数据
/************************************************************************************/
using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.SystemInitSets
{
    #region 配置信息

    #region Config表添加参数
    /// <summary>
    /// Config表添加参数
    /// </summary>
    public class AddConfigParameter : BaseRequest
    {
        public string Name { get; set; }

        public string Describe { get; set; }

        public string Value { get; set; }

        public int IsUsed { get; set; }

        public int IsDefault { get; set; }

        public int ParentID { get; set; }

        /// <summary>
        /// 关联通用数据表名
        /// 1 监测树类型
        /// 2 设备类型
        /// 3 测量位置类型
        /// 4 测量位置监测类型
        /// 5 振动信号类型
        /// 6 特征值
        /// 7 波长
        /// 8 上限频率
        /// 9 下限频率
        /// 10 传感器类型
        /// 11 传感器挂靠个数
        /// </summary>
        public int? CommonDataType { get; set; }

        /// <summary>
        /// 关联通用数据Code
        /// </summary>
        public string CommonDataCode { get; set; }
    }
    #endregion

    #region 编辑配置
    /// <summary>
    /// 编辑配置
    /// </summary>
    public class EditConfigParameter : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public string Value { get; set; }

        public int IsUsed { get; set; }

        public int IsDefault { get; set; }

        public int ParentID { get; set; }

        /// <summary>
        /// 关联通用数据表名
        /// 1 监测树类型
        /// 2 设备类型
        /// 3 测量位置类型
        /// 4 测量位置监测类型
        /// 5 振动信号类型
        /// 6 特征值
        /// 7 波长
        /// 8 上限频率
        /// 9 下限频率
        /// 10 传感器类型
        /// 11 传感器挂靠个数
        /// </summary>
        public int? CommonDataType { get; set; }

        /// <summary>
        /// 关联通用数据Code
        /// </summary>
        public string CommonDataCode { get; set; }
    }
    #endregion

    #region 删除配置
    /// <summary>
    /// 删除配置
    /// </summary>
    public class DeleteConfigParameter : BaseRequest
    {
        public int ID { get; set; }
    }
    #endregion

    #region 配置信息名称是否存在
    /// <summary>
    /// 配置信息名称是否存在
    /// </summary>
    public class ExistConfigNameByNameAndParnetIdParameter : BaseRequest
    {
        public string Name { get; set; }

        public int ParentID { get; set; }
    }
    #endregion

    #region 名称和id是否存在
    /// <summary>
    /// 名称和id是否存在
    /// </summary>
    public class ExistConfigNameByIDAndNameParameter : BaseRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
    #endregion

    #region 通过名称获取配置信息
    /// <summary>
    /// 通过名称获取配置信息
    /// </summary>
    public class ConfigByNameParameter : BaseRequest
    {
        public string RootName { get; set; }

        public string Name { get; set; }
    }
    #endregion

    #region 通过名称获取配置信息
    /// <summary>
    /// 通过Code获取配置信息
    /// </summary>
    public class ConfigByCodeParameter : BaseRequest
    {
        public string RootCode { get; set; }

        public string Code { get; set; }
    }
    #endregion

    public class GetConfigByIDParam : BaseRequest
    {
        public int ParentID { get; set; }

        public int? ID { get; set; }
    }

    #endregion
}
