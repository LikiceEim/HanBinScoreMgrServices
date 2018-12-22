/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 *文件名：  WaveAttrTypeForDeviceTreeResult
 *创建人：  张辽阔
 *创建时间：2016-11-01
 *描述：返回波形属性(波长，上限频率，下限频率)类型数据信息的参数
/************************************************************************************/

using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    #region 返回波形属性(波长，上限频率，下限频率)类型数据信息的参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：返回波形属性(波长，上限频率，下限频率)类型数据信息的参数
    /// </summary>
    public class WaveAttrTypeForDeviceTreeResult
    {
        /// <summary>
        /// 波形长度类型信息集合
        /// </summary>
        public List<WaveLengthTypeInfo> WaveLengthTypeInfoList { set; get; }

        /// <summary>
        /// 波形上限频率类型信息集合
        /// </summary>
        public List<WaveUpperLimitTypeInfo> WaveUpperLimitTypeInfoList { set; get; }

        /// <summary>
        /// 波形下限频率类型信息集合
        /// </summary>
        public List<WaveLowerLimitTypeInfo> WaveLowerLimitTypeInfoList { set; get; }
    }
    #endregion

    #region 波形长度
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：波形长度
    /// </summary>
    public class WaveLengthTypeInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 波形长度
        /// </summary>
        public int WaveLengthValue { get; set; }

        /// <summary>
        /// 0不可用，1可用
        /// </summary>
        public int IsUsable { get; set; }
    }
    #endregion

    #region 波形上限
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：波形上限
    /// </summary>
    public class WaveUpperLimitTypeInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 波形上限频率值
        /// </summary>
        public int WaveUpperLimitValue { get; set; }

        /// <summary>
        /// 0不可用，1可用
        /// </summary>
        public int IsUsable { get; set; }
    }
    #endregion

    #region 波形下限
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-01
    /// 创建记录：波形下限
    /// </summary>
    public class WaveLowerLimitTypeInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 波形下限频率值
        /// </summary>
        public int WaveLowerLimitValue { get; set; }

        /// <summary>
        /// 0不可用，1可用
        /// </summary>
        public int IsUsable { get; set; }
    }
    #endregion

}