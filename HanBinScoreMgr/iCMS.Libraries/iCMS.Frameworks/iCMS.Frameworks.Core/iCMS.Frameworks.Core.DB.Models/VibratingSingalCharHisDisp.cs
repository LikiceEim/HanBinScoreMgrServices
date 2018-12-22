/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  张辽阔
 *创建时间：2016.07.21
 *描述：位移振动信号历史数据表实体
 **=================================================================================
 *修改记录
 *修改时间：2016年7月25日11:18:37
 *修改人： LF
 *修改原因：添加构造函数，初始化数据,并修改数据类型
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 位移振动信号历史数据表（位移振动信号ID和采集时间创建索引）
    /// </summary>
    [Table("T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP")]
    public class VibratingSingalCharHisDisp : EntityBase
    {
        #region Model
        public VibratingSingalCharHisDisp()
        {
            Rotate = "-";
            TransformCofe = 0;
            RealSamplingFrequency = 0;
            SamplingPointData = 0;
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int HisDataID { get; set; }

        /// <summary>
        /// 信号ID
        /// </summary>
        public int SingalID { get; set; }

        ///// <summary>
        ///// 振动信号信息实体集合
        ///// </summary>
        //public IList<T_SYS_VIBSINGAL> MEASURESITE_List { get; set; }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MsiteID { get; set; }

        ///// <summary>
        ///// 测量位置实体集合
        ///// </summary>
        //public IList<T_SYS_MEASURESITE> MEASURESITE_List { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }

        ///// <summary>
        ///// 设备信息实体集合
        ///// </summary>
        //public IList<T_SYS_DEVICE> DEVICE_List { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingDate { get; set; }

        /// <summary>
        /// 波形数据路径
        /// </summary>
        public string WaveDataPath { get; set; }

        /// <summary>
        /// 转速
        /// </summary>
        public string Rotate { get; set; }

        /// <summary>
        /// 压缩因子
        /// </summary>
        public float TransformCofe { get; set; }

        /// <summary>
        /// 实际采样频率
        /// </summary>
        public float RealSamplingFrequency { get; set; }

        /// <summary>
        /// 波长
        /// </summary>
        public int SamplingPointData { get; set; }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlmStatus { get; set; }

        /// <summary>
        /// 采集方式
        /// </summary>
        public int? DAQStyle { get; set; }

        /// <summary>
        /// 峰值
        /// </summary>
        public float? PeakValue { get; set; }

        /// <summary>
        /// 峰峰值
        /// </summary>
        public float? PeakPeakValue { get; set; }

        /// <summary>
        /// 有效值
        /// </summary>
        public float? EffValue { get; set; }

        /// <summary>
        /// 峰值警告值
        /// </summary>
        public float? PeakWarnValue { get; set; }

        /// <summary>
        /// 峰值告警值
        /// </summary>
        public float? PeakAlmValue { get; set; }

        /// <summary>
        /// 峰峰值警告值
        /// </summary>
        public float? PeakPeakWarnValue { get; set; }

        /// <summary>
        /// 峰峰值告警值
        /// </summary>
        public float? PeakPeakAlmValue { get; set; }

        /// <summary>
        /// 有效值警告值
        /// </summary>
        public float? EffWarnValue { get; set; }

        /// <summary>
        /// 有效值告警值
        /// </summary>
        public float? EffAlmValue { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int MonthDate { get; set; }

        /// <summary>
        /// 采集类型 1.有线、2.无线、3.三轴   add by lwj---2018.05.02
        /// </summary>
        public int SamplingDataType { get; set; }

        #endregion Model
    }
}