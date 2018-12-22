/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticAnalysis
 *文件名：  GetDevImgDataResult 
 *创建人：  王颖辉
 *创建时间：2017/10/13 16:44:49 
 *描述：向调用者暴露对形貌图数据展示
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    /// <summary>
    /// 向调用者暴露对形貌图数据展示
    /// </summary>
    public class GetDevImgDataResult
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevId
        {
            get;
            set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName
        {
            get;
            set;
        }

        /// <summary>
        /// 设备转速
        /// </summary>
        public double DevRoatate
        {
            get;
            set;
        }

        /// <summary>
        /// 设备当前状态
        /// 1：正常，2：高报，3：高高报
        /// </summary>
        public int DevStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 运行状态
        ///3停机，1运行，2检修
        /// </summary>
        public int DevRunningStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 设备类型ID
        /// </summary>
        public int DeviceTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DeviceTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 投运时间
        /// </summary>
        public string OperationDate
        {
            get;
            set;
        }

        /// <summary>
        /// 统计信息
        /// </summary>
        public List<MSStatusInfoForDevImgData> MSStatusInfo
        {
            get;
            set;
        }

    }

    public class MSStatusInfoForDevImgData
    {

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MSName
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置状态
        /// 1：正常，2：警告，3：报警
        /// </summary>
        public int MSStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置描述
        /// 说明：该值为测量位置字典表中对应描述信息
        /// </summary>
        public string MSDesInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 测量最新的速度有效值
        /// </summary>
        public double MSSpeedVirtualValue
        {
            get;
            set;
        }


        /// <summary>
        /// 测量最新的速度有效值的状态
        /// 1：正常，2：警告，3：报警
        /// </summary>
        public int MSSpeedVirtualSatus
        {
            get;
            set;
        }

        /// <summary>
        /// 测量最新的速度有效值的采集时间
        /// </summary>
        public string MSSpeedVirtualTime
        {
            get;
            set;
        }


        /// <summary>
        /// 测量位置最新的包络峰值
        /// </summary>
        public double MSEnvelopingPEAKValue
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置最新的包络峰值的状态
        /// 1：正常，2：警告，3：报警
        /// </summary>
        public int MSEnvelopingVirtualStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置最新的包络峰值采集时间.
        /// </summary>
        public string MSEnvelopingVirtualTime
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置最新的温度的状态
        /// 1：正常，2：警告，3：报警
        /// </summary>
        public int MSTemperatureStatus
        {
            get;
            set;
        }


        /// <summary>
        /// 测量位置最新的温度值
        /// </summary>
        public double MSTemperatureValue
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置最新的温度值的采集时间
        /// </summary>
        public string MSTemperatureTime
        {
            get;
            set;
        }

        /// <summary>
        /// 测量最新的LQ值
        /// </summary>
        public double MSLQValue
        {
            get;
            set;
        }

        /// <summary>
        /// 测量最新的Lq值的状态
        /// 1：正常，2：警告，3：报警
        /// </summary>
        public int MSLQValueSatus
        {
            get;
            set;
        }

        /// <summary>
        /// 测量最新的LQ值的采集时间
        /// </summary>
        public string MSLQValueTime
        {
            get;
            set;
        }

        /// <summary>
        /// 测量最新的位移峰峰值
        /// </summary>
        public double MSDispDPeakValue
        {
            get;
            set;
        }

        /// <summary>
        /// 测量最新的位移峰峰值的状态
        /// 1：正常，2：警告，3：报警
        /// </summary>
        public int MSDispDPeakSatus
        {
            get;
            set;
        }

        /// <summary>
        /// 测量最新的位移峰峰值的采集时间
        /// </summary>
        public string MSDispDPeakTime
        {
            get;
            set;
        }
    }
}
