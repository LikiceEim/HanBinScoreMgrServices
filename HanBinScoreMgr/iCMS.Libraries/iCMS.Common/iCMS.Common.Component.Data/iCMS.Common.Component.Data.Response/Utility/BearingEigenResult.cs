/************************************************************************************
* Copyright (c) @ILine All Rights Reserved.
*命名空间：iCMS.Common.Component.Data.Response.Utility
*文件名：  BearingEigenResult
*创建人：  QXM
*创建时间：2016-11-3
*描述：轴承库特征值返回结果
*====================================================================================
*修改人：张辽阔
*修改时间：2016-11-08
*修改记录：添加1.4.1 新增字段
/************************************************************************************/

using System.Collections.Generic;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Common.Component.Data.Response.Utility
{
    #region 轴承库特征值返回结果
    /// <summary>
    /// 轴承库特征值返回结果
    /// </summary>
    public class BearingEigenResult
    {
        /// <summary>
        /// 轴承特征频率列表
        /// </summary>
        public List<BearingInfo> BearingList { get; set; }
        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }
        /// <summary>
        /// 符合条件的总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 当前页码（从1开始）
        /// </summary>
        public int PageNum { get; set; }
        ///// <summary>
        ///// 原因
        ///// </summary>
        //public string Reason { get; set; }

        public BearingEigenResult()
        {
            BearingList = new List<BearingInfo>();
        }
    }
    #endregion

    #region 轴承信息
    /// <summary>
    /// 轴承信息
    /// </summary>
    public class BearingInfo : EntityBase
    {
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        public string FactoryID { get; set; }
        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum { get; set; }

        /// <summary>
        /// 轴承库Id
        /// </summary>
        public int BearingID { get; set; }
        /// <summary>
        /// 轴承描述
        /// </summary>
        public string BearingDescribe { get; set; }
        /// <summary>
        /// 外圈故障特征频率
        /// </summary>
        //public Double BFFO { get; set; }
        public float BPFO { get; set; }
        /// <summary>
        /// 内圈故障特征频率
        /// </summary>
        //public Double BFFI { get; set; }
        public float BPFI { get; set; }
        /// <summary>
        /// 滚动体故障特征频率
        /// </summary>
        public float BSF { get; set; }
        /// <summary>
        /// 保持架故障特征频率
        /// </summary>
        public float FTF { get; set; }
        /// <summary>
        /// 轴承特征频率表示ID
        /// </summary>
        public int EigenvalueID { get; set; }

        #region 1.4.1 新增字段 张辽阔 2016-11-08 添加

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
    }
    #endregion
}