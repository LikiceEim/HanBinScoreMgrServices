/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  QXM
 *创建时间：2016/7/21 10:10:13
 *描述：
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 模块权限表
    /// </summary>
    [Table("T_SYS_MODULE")]
    public class Module : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ModuleID { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 父模块ID
        /// </summary>
        public int ParID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IsUsed { get; set; }

        /// <summary>
        /// 是否是必选功能0否，1是
        /// </summary>
        public int IsDeault { get; set; }

        /// <summary>
        /// 祖ID
        /// </summary>
        public int OID { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 管理通用数据表：
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

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        #endregion Model
    }
}