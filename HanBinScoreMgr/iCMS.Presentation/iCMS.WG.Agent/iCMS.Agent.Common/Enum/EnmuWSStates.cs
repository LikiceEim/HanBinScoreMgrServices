/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  EnmuWSStates
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：WS运行状态枚举
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
    /// WS状态
    /// </summary>
    public enum EnmuWSStates
    {
        /// <summary>
        /// 初始化态
        /// </summary>
        EnmuWSStates_Load = 0,
        /// <summary>
        /// 运行态
        /// </summary>
        EnmuWSStates_Run,
        /// <summary>
        /// 发送态
        /// </summary>
        EnmuWSStates_Send,
        /// <summary>
        /// 配置态
        /// </summary>
        EnmuWSStates_Config,
        /// <summary>
        /// 采集态
        /// </summary>
        EnmuWSStates_Sampling,
        /// <summary>
        /// 故障态
        /// </summary>
        EnmuWSStates_Error,
        /// <summary>
        /// 升级态
        /// </summary>
        EnmuWSStates_Update,
        /// <summary>
        /// 未知态
        /// </summary>
        EnmuWSStates_UnKnown,
    }
}
