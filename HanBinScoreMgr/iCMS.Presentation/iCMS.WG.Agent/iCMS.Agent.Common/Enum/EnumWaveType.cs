/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  EnumWaveType
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：波形类型枚举
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Common.Enum
{
    public enum EnumWaveType
    {

        /// <summary>
        /// 加速度波形
        /// </summary>

        EnumWaveType_AcceleratedSpeed = 1,
        /// <summary>
        /// 速度波形
        /// </summary>

        EnumWaveType_Speed = 2,
        /// <summary>
        /// 位移波形
        /// </summary>

        EnumWaveType_Displacement = 3,
        /// <summary>
        /// 包络波形
        /// </summary>

        EnumWaveType_Envelope = 4,
        /// <summary>
        /// 原始波形
        /// </summary>

        EnumWaveType_LQ=5,
    }



    public static class GetWaveTypeString
    {

        public static string GetString(int typeNum)
        {
            string strBack = "";
            switch (typeNum)
            {
                case 1:
                    strBack = "加速度波形";
                    break;
                case 2:
                    strBack = "速度波形";
                    break;
                case 3:
                    strBack = "位移波形";
                    break;
                case 4:
                    strBack = "包络波形";
                    break;
                case 5:
                    strBack = "原始波形";
                    break;
            };
            return strBack;
        }

    }
}
