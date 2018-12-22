/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Request.DevicesConfig
 * 文件名：  EditMSiteRecordForDeviceTreeParameter
 * 创建人：  张辽阔
 * 创建时间：2016-11-02
 * 描述：编辑测量位置信息参数
/************************************************************************************/

using System.Collections.Generic;

using iCMS.Common.Component.Data.Base;

namespace iCMS.Common.Component.Data.Request.DevicesConfig
{
    #region 编辑测量位置信息参数
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-11-02
    /// 创建记录：编辑测量位置信息参数
    /// </summary>
    public class EditMSiteRecordForDeviceTreeParameter : BaseRequest
    {
        /// <summary>
        /// 测量位置集合
        /// </summary>
        public List<MeasureSiteForDeviceTree> MeasureSite
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 测量位置
    /// </summary>
    public class MeasureSiteForDeviceTree
    {
        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MeasureSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置名称
        /// </summary>
        public int MSiteTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器灵敏度系数A
        /// </summary>
        public int SensorCosA
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器灵敏度系数B
        /// </summary>
        public int SensorCosB
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承ID
        /// </summary>
        public int BearingID
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承厂商ID
        /// </summary>
        public int FactoryID
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承形式
        /// </summary>
        public string BearingType
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingModel
        {
            get;
            set;
        }

        /// <summary>
        /// 润滑形式
        /// </summary>
        public string LubricatingForm
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
        /// 设备温度报警ID
        /// </summary>
        public int DeviceTemperatureMsiteAlmID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备温度中级报警
        /// </summary>
        public float DeviceTemperatureAlarmValue
        {
            get;
            set;
        }

        /// <summary>
        /// 设备温度高级报警
        /// </summary>
        public float DeviceTemperatureDangerValue
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器报警ID
        /// </summary>
        public int WSTemperatureMsiteAlmID
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器温度中级报警
        /// </summary>
        public float WSTemperatureAlarmValue
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器温度高级报警
        /// </summary>
        public float WSTemperatureDangerValue
        {
            get;
            set;
        }

        /// <summary>
        /// 电池电压报警ID
        /// </summary>
        public int VoltageMsiteAlmID
        {
            get;
            set;
        }

        /// <summary>
        /// 电池电压中级报警
        /// </summary>
        public float VoltageAlarmValue
        {
            get;
            set;
        }

        /// <summary>
        /// 电池电压高级报警
        /// </summary>
        public float VoltageDangerValue
        {
            get;
            set;
        }

        /// <summary>
        /// 波形采集时间间隔
        /// </summary>
        public string WaveTime
        {
            get;
            set;
        }

        /// <summary>
        /// 波形采集时间间隔
        /// </summary>
        public string FlagTime
        {
            get;
            set;
        }

        /// <summary>
        /// 温度、电池电压采集时间间隔
        /// </summary>
        public string TemperatureTime
        {
            get;
            set;
        }

        /// <summary>
        /// 位置
        /// </summary>
        public string Position
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        ///创建人
        /// </summary>
        public int UserID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：采集温度的采集单元ID
        /// </summary>
        public int TemperatureWGID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：温度采集通道
        /// </summary>
        public int TemperatureWSID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-07
        /// 创建记录：传感器灵敏度系数
        /// </summary>
        public float SensorCoefficient
        {
            get;
            set;
        }

        /// <summary>
        /// 振动集合
        /// </summary>
        public List<VibSignalInfoForMeasureSite> VibSignalInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承信息
        /// </summary>
        public List<BearingInfoForMeasureSite> BearingInfoList
        {
            get;
            set;
        }
    }
    #endregion
}