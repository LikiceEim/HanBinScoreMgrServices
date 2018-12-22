/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common.Enum
 *文件名：  EnumRequestWSType
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：操作（请求、响应）WS类型枚举
 * 修改记录：
    R1：
     修改作者：李峰
     修改时间：2016/5/30 13:30:00
     修改原因：①  增加启停机枚举；
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Common.Enum
{
    public enum EnumRequestWSType
    {

        /// <summary>
        /// 校时
        /// </summary>
        CalibrateTime = 0,
        /// <summary>
        /// 设置WSID
        /// </summary>
        SetWsID = 1,
        /// <summary>
        /// 设置NetWorkID
        /// </summary>
        SetNetworkID = 2,
        /// <summary>
        /// 下发测量定义
        /// </summary>
        SetMeasDef = 3,
        /// <summary>
        /// 设置SN
        /// </summary>
        SetWsSn = 4,
        /// <summary>
        /// 传感器校准
        /// </summary>
        CalibrateWsSensor = 5,
        /// <summary>
        /// 获取WSSNCode
        /// </summary>
        GetWsSn = 6,
        /// <summary>
        /// 恢复出厂设置WS
        /// </summary>
        RestoreWS = 7,
        /// <summary>
        /// 恢复出厂设置WG
        /// </summary>
        RestoreWG = 8,
        /// <summary>
        /// 重启WS
        /// </summary>
        ResetWS = 9,
        /// <summary>
        /// 重启WG
        /// </summary>
        ResetWG = 10,
        /// <summary>
        /// 发送升级信息
        /// </summary>
        SetFwDescInfo = 11,
        /// <summary>
        /// 发送升级数据
        /// </summary>
        SetFwData = 12,
        /// <summary>
        /// 响应自报告
        /// </summary>
        ReplySelfReport=13,
        /// <summary>
        /// 响应波形描述
        /// </summary>
        ReplyWaveDesc=14,
        /// <summary>
        /// 响应波形数据
        /// </summary>
        ReplyWaveData=15,
        /// <summary>
        /// 响应特征值
        /// </summary>
        ReplyEigenValue=16,
        /// <summary>
        /// 响应温度电压
        /// </summary>
        ReplyTmpVolReport=17,
         /// <summary>
        /// 响应启停机特征值
        /// </summary>
        ReplyRevStop = 18,

        /// <summary>
        /// 响应LQ特征值
        /// </summary>
        ReplyLQ = 19,
        /// <summary>
        /// 设置触发式上传
        /// </summary>
        SetTrigger = 20

    }
}
