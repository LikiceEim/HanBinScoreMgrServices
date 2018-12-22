/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：
 * 文件名：  
 * 创建人：  QXM
 * 创建时间：2016/7/21 10:10:13
 * 描述：
/************************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 记录振动信号信息表
    /// </summary>
    [Table("T_SYS_VIBSINGAL")]
    public class VibSingal : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int SingalID { get; set; }

        /// <summary>
        /// 1或空为定时；2为临时。数据库有默认值不为空，王颖辉修改 2016-08-03
        /// </summary>
        public int DAQStyle { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 上限频率
        /// </summary>
        public int? UpLimitFrequency { get; set; }

        /// <summary>
        /// 下限频率
        /// </summary>
        public int? LowLimitFrequency { get; set; }

        /// <summary>
        /// 存储阈值
        /// </summary>
        public float? StorageTrighters { get; set; }

        /// <summary>
        /// 包络带宽
        /// </summary>
        public int? EnlvpBandW { get; set; }

        /// <summary>
        /// 包络滤波器
        /// </summary>
        public int? EnlvpFilter { get; set; }

        /// <summary>
        /// 信号类型
        /// </summary>
        public int SingalType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int SingalStatus { get; set; }

        /// <summary>
        /// 状态更新时间
        /// </summary>
        public DateTime SingalSDate { get; set; }

        /// <summary>
        /// 波长
        /// </summary>
        public int WaveDataLength { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 采集周期 add by lwj---2018.05.02
        /// </summary>
        public int? SamplingTimePeriod { get; set; }

        /// <summary>
        /// 包络滤波器上限 add by lwj---2018.05.02
        /// </summary>
        public int? EnvelopeFilterUpLimitFreq { get; set; }

        /// <summary>
        /// 包络滤波器下限 add by lwj---2018.05.02
        /// </summary>
        public int? EnvelopeFilterLowLimitFreq { get; set; }

        /// <summary>
        /// 特征值波长 add by lwj---2018.05.02
        /// </summary>
        public int? EigenValueWaveLength { get; set; }

        #endregion Model
    }
}