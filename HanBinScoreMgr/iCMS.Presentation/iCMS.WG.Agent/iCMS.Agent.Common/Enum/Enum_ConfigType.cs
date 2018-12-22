/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common.Enum
 *文件名：  Enum_ConfigType
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：WS操作类型枚举
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Common.Enum
{
    public enum Enum_ConfigType
    {
        /// <summary>
        /// 测量定义
        /// </summary>
        ConfigType_MeasDefine = 1,
        /// <summary>
        /// 升级
        /// </summary>
        ConfigType_UpdateFirmware = 2,

        /// <summary>
        /// 触发式定义
        /// </summary>
        ConfigType_TriggerDefine = 3,

        /// <summary>
        /// 配置WS NetID
        /// </summary>
        ConfigType_WS_NetID = 4,
        /// <summary>
        /// 配置WS 编号
        /// </summary>
        ConfigType_WS_NO = 5,
        /// <summary>
        /// 配置WS SNCode
        /// </summary>
        ConfigType_WS_SNCode = 6,
        /// <summary>
        /// 校准WS
        /// </summary>
        ConfigType_WS_Calibration = 7,
        /// <summary>
        /// 取得SNCode
        /// </summary>
        ConfigType_WS_Get_SNCode = 8,

        /// <summary>
        /// 重启WG
        /// </summary>
        ConfigType_ReSetWG = 9,
        /// <summary>
        /// 重启WS
        /// </summary>
        ConfigType_ReSetWS = 10,
        /// <summary>
        /// WG恢复出厂设置
        /// </summary>
        ConfigType_RestoreWG=11,
        /// <summary>
        /// WS恢复出厂设置
        /// </summary>
        ConfigType_RestoreWS = 12,

        /// <summary>
        /// 配置WG NetID
        /// </summary>
        ConfigType_WG_NetID = 13,
    }
}
