
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 通用配置信息表
    /// </summary>
    [Table("T_DICT_CONFIG")]
    public class Config : EntityBase
    {
        #region Model

        /// <summary>
        /// 类型ID
        /// </summary>
        [Key]
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

        #endregion Model
    }
}