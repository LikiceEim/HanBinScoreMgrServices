/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  EnumCacheType
 *创建人：  LF
 *创建时间：2016/6/23 10:10:19
 *描述：采集数据类型，主要用于Agent处理判断重复数据
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Common.Enum
{
    public enum EnumCacheType
    {
        /// <summary>
        /// 温度电压
        /// </summary>
        TmpVoltage = 6,

        /// <summary>
        /// 特征值
        /// </summary>
        CharacterValue = 7,

        /// <summary>
        /// 加速度波形
        /// </summary>

        Wave_AcceleratedSpeed = 1,
        /// <summary>
        /// 速度波形
        /// </summary>

        Wave_Speed = 2,
        /// <summary>
        /// 位移波形
        /// </summary>

        Wave_Displacement = 3,
        /// <summary>
        /// 包络波形
        /// </summary>
        Wave_Envelope = 4,
        /// <summary>
        /// LQ
        /// </summary>
        LQValue = 5,
        /// <summary>
        /// 启停机
        /// </summary>
        CriticalValue=8,

    }
}
