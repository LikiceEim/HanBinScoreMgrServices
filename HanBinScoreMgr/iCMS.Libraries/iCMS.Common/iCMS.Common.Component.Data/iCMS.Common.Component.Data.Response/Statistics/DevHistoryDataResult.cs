/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Statistics
 *文件名：  ViewDevHistoryDataResult
 *创建人：  王颖辉
 *创建时间：2016-10-26
 *描述：统计服务
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    #region 统计服务

    #region 设备历史数据
    /// <summary>
    /// 设备历史数据
    /// </summary>

    public class DevHistoryDataResult
    {
        public List<DevHistoryCollectDataInfo2> DevHistoryCollectDataInfo { get; set; }

        public int Total { get; set; }

        public DevHistoryDataResult()
        {
            DevHistoryCollectDataInfo = new List<DevHistoryCollectDataInfo2>();
        }
    }
    #endregion

    #region 设备历史数据
    /// <summary>
    /// 设备历史数据
    /// </summary>

    public class DevHistoryCollectDataInfo2 : EntityBase
    {
        public int DAQStyle { get; set; }	//Int	采集方式
        public int MSiteID { get; set; }	//Int	测量位置ID
        public string MSiteName { get; set; }	//String	测量位置名称
        public int DevId { get; set; }	//Int	设备ID
        public string DevName { get; set; }	//String	设备名称
        public float? TempValue { get; set; }	//float	设备温度采集值
        public float? TempWarnSet { get; set; }	//float	设备温度高报阈值
        public float? TempAlarmSet { get; set; }	//float	设备温度高高报阈值
        public int? TempStat { get; set; }	//Int	设备温度状态
        public float? SpeedVirtualValue { get; set; }	//float	速度有效值
        public float? SpeedVirtualValueWarnSet { get; set; }//float	速度有效值高报阈值
        public float? SpeedVirtualValueAlarmSet { get; set; }	//float	速度有效值高高阈值
        public int? SpeedVirtualValueStat { get; set; }	//Int	速度有效值状态
        public float? ACCPEAKValue { get; set; }	//float	加速度峰值
        public float? ACCPEAKValueWarnSet { get; set; }	//float	加速度峰值高报阈值
        public float? ACCPEAKValueAlarmSet { get; set; }	//float	加速度峰值高高报阈值
        public int? ACCPEAKValueStat { get; set; }	//Int	加速度峰值状态
        public float? LQValue { get; set; }	//float	轴承状态采集值
        public float? LQWarnSet { get; set; }	//float	轴承状态采集值高报阈值
        public float? LQAlarmSet { get; set; }	//float	轴承状态采集值高高阈值
        public int? LQStat { get; set; }	//Int	轴承状态采集值状态
        public float? DisplacementDPEAKValue { get; set; }	//float	位移峰峰值
        public float? DisplacementDPEAKValueWarnSet { get; set; }	//float	位移峰峰值高报阈值
        public float? DisplacementDPEAKValueAlarmSet { get; set; }	//float	位移峰峰值高高报阈值
        public int? DisplacementDPEAKValueStat { get; set; }	//Int	位移峰峰峰状态

        public float? EnvelopPEAKValue { get; set; }

        public float? EnvelopPEAKValueWarnSet { get; set; }

        public float? EnvelopPEAKValueAlmSet { get; set; }

        public int? EnvelopPEAKValueStat { get; set; }

        public DateTime CollectitTime { get; set; }	//String	采集时间

        public int DataType { get; set; }

        //add by lwj---2018.05.03---添加转速值返回
        public float? SpeedData { get; set; }
        //add by lwj---2018.05.03---添加转速值返回 end

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted
        {
            get;
            set;
        }
    }

    #endregion

    #region 视图监测树
    /// <summary>
    /// 视图监测树
    /// </summary>

    public class View_MonitorTree
    {
        /// <summary>
        /// 监测树id
        /// </summary>
        public int MonitorTreeID
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树级别
        /// </summary>
        public int Lvl
        {
            get;
            set;
        }

        /// <summary>
        /// 父节点id
        /// </summary>
        public int PID
        {
            get;
            set;
        }

        /// <summary>
        /// 级别描述
        /// </summary>
        public string Describe
        {
            get;
            set;
        }

        /// <summary>
        /// 设备id
        /// </summary>
        public int? DevID
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
        /// 设备状态1,使用，0未使用
        /// </summary>
        public int? UseType
        {
            get;
            set;
        }


        /// <summary>
        /// 运行状态
        /// </summary>
        public int? RunStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 报警状态
        /// </summary>
        public int? AlmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 位置id
        /// </summary>
        public int? MSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 位置名称
        /// </summary>
        public string MeasureSiteName
        {
            get;
            set;
        }



        /// <summary>
        /// 位置状态
        /// </summary>
        public int? MSiteStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string WSName
        {
            get;
            set;
        }



        /// <summary>
        /// 温度报警id
        /// </summary>
        public int? MsiteAlmID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备温度状态
        /// </summary>
        public int? DeviceTemperatureStatus
        {
            get;
            set;
        }


        /// <summary>
        /// 信号id
        /// </summary>
        public int? SingalID
        {
            get;
            set;
        }

        /// <summary>
        /// 振动类型id
        /// </summary>
        public int? VibratingTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 振动类型名称
        /// </summary>
        public string VibratingTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 信号报警id
        /// </summary>
        public int? SingalAlmID
        {
            get;
            set;
        }
        /// <summary>
        /// 振动信号状态
        /// </summary>
        public int? SingalStatus
        {
            get;
            set;
        }



        /// <summary>
        /// 特征值id
        /// </summary>
        public int? EigenTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值名称
        /// </summary>
        public string EigenTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 特征值状态
        /// </summary>
        public int? EnginStatus
        {
            get;
            set;
        }

    }
    #endregion

    #region 设备历史数据
    /// <summary>
    /// 设备历史数据
    /// </summary>

    public class DevHistoryInfo
    {
        public int MSiteID { get; set; }

        public DateTime CollectitTime { get; set; }
    }
    #endregion

    #endregion
}
