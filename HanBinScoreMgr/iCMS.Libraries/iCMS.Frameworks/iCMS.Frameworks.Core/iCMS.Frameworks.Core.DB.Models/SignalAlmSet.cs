/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 * 命名空间：iCMS.Frameworks.Core.DB.Models
 * 文件名：  SingalAlmSet
 * 创建人：  LF  
 * 创建时间：2016年7月23日14:19:35
 * 描述：振动信号报警阈值设置实体类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    [Table("T_SYS_VIBRATING_SET_SIGNALALM")]
    public class SignalAlmSet : EntityBase
    {
        [Key]
        public int SingalAlmID { get; set; }

        /// <summary>
        /// 外键，振动信号ID
        /// </summary>
        public int SingalID { get; set; }

        /// <summary>
        /// 特征值类型
        /// </summary>
        public int ValueType { get; set; }

        /// <summary>
        /// 警告值
        /// </summary>
        public float WarnValue { get; set; }

        /// <summary>
        /// 报警值
        /// </summary>
        public float AlmValue { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 触发式上传阈值设定
        /// </summary>
        public float? UploadTrigger { get; set; }

        /// <summary>
        /// 趋势报警预值
        /// </summary>
        public float? ThrendAlarmPrvalue { get; set; }

        /// <summary>
        /// 能量值上限频率
        /// </summary>
        public int? EnergyUpLimit { get; set; }

        /// <summary>
        /// 能量值下限频率
        /// </summary>
        public int? EnergyLowLimit { get; set; }

        #region 导航属性

        // public VibSingal NVibSignal { get; set; }

        #endregion
    }
}