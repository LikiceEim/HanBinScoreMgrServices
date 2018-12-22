/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  EnumSamplingType
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：采集方式  1：定时采集；2：临时采集；3：特征值采集 测量定义类型枚举
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Common.Enum
{
    /// <summary>
    /// 采集方式  1：定时采集；2：临时采集；3：特征值采集
    /// </summary>
    public enum EnumSamplingType
    {
        /// <summary>
        /// 定时
        /// </summary>
        EnumSamplingType_Time = 1,
        /// <summary>
        /// 临时采集
        /// </summary>
        EnumSamplingType_Temporary,
        /// <summary>
        /// 特征值采集
        /// </summary>
        EnumSamplingType_CharacterValue,
    }
}
