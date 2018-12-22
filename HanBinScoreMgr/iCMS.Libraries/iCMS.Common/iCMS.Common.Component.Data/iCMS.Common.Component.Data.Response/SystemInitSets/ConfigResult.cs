/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.SystemInitSets
 *文件名：  ConfigResult
 *创建人：  QXM
 *创建时间：2016-11-3
 *描述：返回设备类型数据信息的参数
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    #region 通用配置返回值
    public class ConfigResult
    {
        public List<ConfigData> ConfigList { get; set; }
    }
    #region 配置信息
    /// <summary>
    /// 配置信息
    /// </summary>
    public class ConfigData
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 0不可用，1可用
        /// </summary>
        public int IsUsed { get; set; }

        /// <summary>
        /// 0系统初始状态，1其它状态
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool IsExistChild { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? OrderNo { get; set; }

        /// <summary>
        /// 关联通用数据表名：
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
        /// 通用数据Code
        /// </summary>
        public string CommonDataCode { get; set; }
    }
    #endregion

    #endregion
}
