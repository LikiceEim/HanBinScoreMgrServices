/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Enum
 *文件名：  EnumEigenType
 *创建人：  王颖辉  
 *创建时间：2016年09月06日
 *描述：特征值
/************************************************************************************/
using System.ComponentModel;

namespace iCMS.Common.Component.Data.Enum
{
    #region 特征值
    /// <summary>
    /// 特征值类型名称
    /// </summary>
    [Description("特征值类型名称")]
    public enum EnumEigenType
    {


        /// <summary>
        /// 峰值
        /// </summary>
        [Description("峰值")]
        PeakValue = 1,

        /// <summary>
        /// 峰峰值
        /// </summary>
        [Description("峰峰值")]
        PeakPeakValue = 2,

        /// <summary>
        /// 有效值
        /// </summary>
        [Description("有效值")]
        EffectivityValue = 3,

        /// <summary>
        /// 地毯值
        /// </summary>
        [Description("地毯值")]
        CarpetValue = 4,
        /// <summary>
        ///轴承状态
        /// </summary>
        [Description("轴承状态")]
        LQValue = 5,
    }
    #endregion
}
