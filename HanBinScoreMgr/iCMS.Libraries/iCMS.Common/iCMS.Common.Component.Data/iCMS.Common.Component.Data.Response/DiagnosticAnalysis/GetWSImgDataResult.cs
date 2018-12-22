/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.DiagnosticAnalysis
 *文件名：  GetWSImgDataResult 
 *创建人：  王颖辉
 *创建时间：2017/10/14 18:28:08 
 *描述：请求基类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    /// <summary>
    /// 获取形貌图数据展示（传感器)
    /// </summary>
    public class GetWSImgDataResult
    {

        /// <summary>
        /// 网关ID
        /// </summary>
        public int WGID
        {
            get;
            set;
        }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string WGName
        {
            get;
            set;
        }

        /// <summary>
        /// 网关连接状态0:断开，1：连接
        /// </summary>
        public int WGLinkStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 网关编号
        /// </summary>
        public int WGNO
        {
            get;
            set;
        }

        /// <summary>
        /// 可挂靠传感器个数
        /// </summary>
        public int WSNum
        {
            get;
            set;
        }


        /// <summary>
        /// NetworkID
        /// </summary>
        public int? NetworkID
        {
            get;
            set;
        }

        /// <summary>
        /// Agent
        /// </summary>
        public string Agent
        {
            get;
            set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            get;
            set;
        }

        /// <summary>
        /// 集合
        /// </summary>
        public List<WSStatusInfoForWSImg> MSStatusInfo
        {
            get;
            set;
        }

    }

    public class WSStatusInfoForWSImg
    {
        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int? MSiteID
        {
            get;
            set;
        }

      

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public string MeasureSiteName
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器ID
        /// </summary>
        public int WSID
        {
            get;
            set;
        }


        /// <summary>
        /// 传感器名称
        /// </summary>
        public string WSName
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器状态
        /// </summary>
        public int WSStatus
        {
            get;
            set;
        }


        /// <summary>
        /// 传感器连接状态
        /// </summary>
        public int WSLinkStatus
        {
            get;
            set;
        }


        /// <summary>
        /// 无线传感器最新的温度的状态
        /// </summary>
        public int? WSTemperatureStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器温度单位
        /// </summary>
        public string WSTemperatureUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器最新的温度值
        /// </summary>
        public double? WSTemperatureValue
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器最新的温度值的采集时间
        /// </summary>
        public DateTime? WSTemperatureTime
        {
            get;
            set;
        }

        #region 添加设传感器温度阈值
        /// <summary>
        /// 无线传感器最新的温度值危险
        /// </summary>
        public double? WSTemperatureAlarmValue
        {
            get;
            set;
        }
        /// <summary>
        /// 无线传感器最新的温度值报警
        /// </summary>
        public double? WSTemperatureDangerValue
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 无线传感器最新的电池电压值
        /// </summary>
        public double? WSBatteryVolatageValue
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器最新的电池电压状态
        /// </summary>
        public int? WSBatteryVolatageStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器电池电压采集时间
        /// </summary>
        public DateTime? WSBatteryVolatageTime
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器电源电压单位
        /// </summary>
        public string WSBatteryVolatageUnit
        {
            get;
            set;
        }
        #region 添加设传感器温度阈值
        /// <summary>
        /// 无线传感器最新的电池电压值报警
        /// </summary>
        public double? WSBatteryVolatageAlarmValue
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器最新的电池电压值危险
        /// </summary>
        public double? WSBatteryVolatageDangerValue
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 测量位置类型Code
        /// </summary>
        public string MeasureSiteTypeCode
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 获取最后一次采集时间分类统计
    /// </summary>
    public class GetLastWSSamplingInfoResult
    {
        /// <summary>
        /// 最后一次采集时间
        /// </summary>
        public DateTime LatestStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 报警类型
        /// </summary>
        public int MSAlmID
        {
            get;
            set;
        }


        /// <summary>
        /// 无线传感器Id
        /// </summary>
        public int WSID
        {
            get;
            set;
        }

        /// <summary>
        /// 无线传感器Id
        /// </summary>
        public int MSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 采集值
        /// </summary>
        public float? SamplingValue
        {
            get;
            set;
        }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int AlmStatus
        {
            get;
            set;
        }
    }
}
