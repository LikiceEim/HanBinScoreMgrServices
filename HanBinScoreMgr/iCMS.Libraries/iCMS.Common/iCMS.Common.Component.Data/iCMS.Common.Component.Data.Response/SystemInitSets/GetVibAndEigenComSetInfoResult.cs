/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.SystemInitSets
 * 文件名：  GetVibAndEigenComSetInfoResult 
 * 创建人：  王颖辉
 * 创建时间：2017/9/29 10:06:29 
 * 描述：获取通用数据振动及特征值信息
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    /// <summary>
    /// 获取通用数据振动及特征值信息
    /// </summary>
    public class GetVibAndEigenComSetInfoResult : BaseRequest
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-10
        /// 创建记录：无线振动信号类型集合
        /// </summary>
        public List<VibTypeinfo> WireLessVibTypeinfo
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-10
        /// 创建记录：有线振动信号类型集合
        /// </summary>
        public List<VibTypeinfo> WiredVibTypeinfo
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-10
        /// 创建记录：三轴振动信号类型集合
        /// </summary>
        public List<VibTypeinfo> TriaxialVibTypeinfo
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置检测类型集合
        /// </summary>
        public List<MSMonitorType> MSMonitorType
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 通用数据振动及特征值信息
    /// </summary>
    public class VibAndMonitorTypeInfo
    {
        /// <summary>
        /// 振动信号类型集合
        /// </summary>
        public List<VibTypeinfo> VibTypeinfo
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置检测类型集合
        /// </summary>
        public List<MSMonitorType> MSMonitorType
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 振动信号类型
    /// </summary>
    public class VibTypeinfo
    {
        /// <summary>
        /// 振动信号ID
        /// </summary>
        public int VibrationSignalTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号
        /// </summary>
        public String VibrationSignalName
        {
            get;
            set;
        }

        /// <summary>
        /// 简写
        /// </summary>
        public String Code
        {
            get;
            set;
        }

        /// <summary>
        /// 上限ID
        /// </summary>
        public List<SelectItemResult> UpperLimitList
        {
            get;
            set;
        }

        /// <summary>
        /// 下限ID
        /// </summary>
        public List<SelectItemResult> LowLimitList
        {
            get;
            set;
        }

        /// <summary>
        /// 波长ID
        /// </summary>
        public List<SelectItemResult> WaveLengthList
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-10
        /// 创建记录：特征值波长集合
        /// </summary>
        public List<SelectItemResult> EigenWaveLengthList
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-10
        /// 创建记录：包络滤波器上限频率
        /// </summary>
        public List<SelectItemResult> EnvlFilterUpperLimitList
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-10
        /// 创建记录：包络滤波器下限频率
        /// </summary>
        public List<SelectItemResult> EnvlFilterLowLimitList
        {
            get;
            set;
        }

        /// <summary>
        /// 描述信息
        /// </summary>
        public String Description
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值信息集合
        /// </summary>
        public List<EigenValueInfo> EigenValueInfo
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 特征值
    /// </summary>
    public class EigenValueInfo
    {
        /// <summary>
        /// 特征值ID    
        /// </summary>
        public int EigenValueTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值
        /// </summary>
        public string EigenValueName
        {
            get;
            set;
        }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 测量位置监测类型
    /// </summary>
    public class MSMonitorType
    {
        /// <summary>
        /// 测量位置监测类型ID
        /// </summary>
        public int MSMonitorTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置监测类型编码
        /// </summary>
        public string MSMonitorTypeCode
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置监测类型名称
        /// </summary>
        public string MSMonitorTypeName
        {
            get;
            set;
        }
    }
}