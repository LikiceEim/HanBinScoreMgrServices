/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  QXM
 *创建时间：2016/7/21 10:10:13
 *描述：
/************************************************************************************/

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    /// <summary>
    /// 轴承库信息表
    /// </summary>
    [Table("T_SYS_BEARING")]
    public class Bearing : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int BearingID { get; set; }

        /// <summary>
        /// 厂商名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂商ID
        /// </summary>
        public string FactoryID { get; set; }

        /// <summary>
        /// 轴承编号
        /// </summary>
        public string BearingNum { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string BearingDescribe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float BPFO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float BPFI { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float BSF { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float FTF { get; set; }

        #region 1.4.1 新增字段

        /// <summary>
        /// 滚球/柱数
        /// </summary>
        public int? BallsNumber { get; set; }
        /// <summary>
        /// 滚球/柱直径 
        /// </summary>
        public float? BallDiameter { get; set; }
        /// <summary>
        /// 节圆直径
        /// </summary>
        public float? PitchDiameter { get; set; }
        /// <summary>
        /// 接触角
        /// </summary>
        public float? ContactAngle { get; set; }

        #endregion

        #endregion Model
    }
}