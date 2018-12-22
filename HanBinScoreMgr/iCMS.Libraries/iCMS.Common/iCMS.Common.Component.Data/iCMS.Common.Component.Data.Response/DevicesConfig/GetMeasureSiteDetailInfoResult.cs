/************************************************************************************
 * Copyright (c) 2017 iLine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Data.Response.DevicesConfig
 * 文件名：  GetMeasureSiteDetailInfoResult 
 * 创建人：  王颖辉
 * 创建时间：2017/9/29 17:34:34 
 * 描述：获取测量位置详细信息
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Common.Component.Data.Response.DevicesConfig
{
    /// <summary>
    /// 获取测量位置详细信息
    /// </summary>
    public class GetMeasureSiteDetailInfoResult
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置ID
        /// </summary>
        public int MeasureSiteID
        {
            get;
            set;
        }

        /// <summary>
        /// 测量位置
        /// </summary>
        public string MeasureSite
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
        /// 轴承名称
        /// </summary>
        public string BearingName
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承厂商ID
        /// </summary>
        public string FactoryID
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承厂商
        /// </summary>
        public string FactoryName
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
        /// 润滑形式
        /// </summary>
        public string LubricatingForm
        {
            get;
            set;
        }

        /// <summary>
        /// 添加用户ID
        /// </summary>
        public int AddUserID
        {
            get;
            set;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public string AddUser
        {
            get;
            set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate
        {
            get;
            set;
        }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public int UpdateUserID
        {
            get;
            set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUser
        {
            get;
            set;
        }

        /// <summary>
        /// 设备温度报警ID
        /// </summary>
        public int? DeviceTemperatureMsiteAlmID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备温度中级报警
        /// </summary>
        public float? DeviceTemperatureAlarmValue
        {
            get;
            set;
        }

        /// <summary>
        /// 设备温度高级报警
        /// </summary>
        public float? DeviceTemperatureDangerValue
        {
            get;
            set;
        }

        /// <summary>
        /// 设备温度显示名称
        /// </summary>
        public string DeviceTemperatureName
        {
            get;
            set;
        }

        /// <summary>
        /// 设备温度阈值添加时间
        /// </summary>
        public DateTime? DeviceTemperatureAddDate
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器报警ID
        /// </summary>
        public int? WSTemperatureMsiteAlmID
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器中级报警
        /// </summary>
        public float? WSTemperatureAlarmValue
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器高级报警
        /// </summary>
        public float? WSTemperatureDangerValue
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器温度添加时间
        /// </summary>
        public DateTime? WSTemperatureAddDate
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器温度显示名称
        /// </summary>
        public string WSTemperatureName
        {
            get;
            set;
        }

        /// <summary>
        /// 电池电压报警ID
        /// </summary>
        public int? VoltageMsiteAlmID
        {
            get;
            set;
        }

        /// <summary>
        /// 电池电压中级报警
        /// </summary>
        public float? VoltageAlarmValue
        {
            get;
            set;
        }

        /// <summary>
        /// 电池电压高级报警
        /// </summary>
        public float? VoltageDangerValue
        {
            get;
            set;
        }

        /// <summary>
        /// 传感器温度添加时间
        /// </summary>
        public DateTime? VoltageAddDate
        {
            get;
            set;
        }

        /// <summary>
        /// 电池电压显示名称
        /// </summary>
        public string VoltageName
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
        /// 特征值采集时间间隔
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
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：温度采集单元ID
        /// </summary>
        public int? TemeratureDAUID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：温度采集单元名称
        /// </summary>
        public string TemeratureDAUName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：温度采集通道ID
        /// </summary>
        public int? TemeratureWSID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：温度采集通道名称
        /// </summary>
        public string TemeratureWSName
        {
            get
            {
                switch (TemeratureWSID)
                {
                    case 0:
                        return "T/0";

                    case 1:
                        return "T/1";

                    case 2:
                        return "T/2";

                    case 3:
                        return "T/3";

                    case 4:
                        return "T/4";

                    case 5:
                        return "T/5";

                    case 6:
                        return "T/6";

                    case 7:
                        return "T/7";

                    case 8:
                        return "T/8";

                    case 9:
                        return "T/9";

                    case 10:
                        return "T/10";

                    case 11:
                        return "T/11";

                    case 12:
                        return "T/12";

                    case 13:
                        return "T/13";

                    case 14:
                        return "T/14";

                    case 15:
                        return "T/15";

                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-05
        /// 创建记录：传感器灵敏度系数
        /// </summary>
        public float? SensorCoefficient
        {
            get;
            set;
        }

        /// <summary>
        /// 振动和特征值
        /// </summary>
        public List<VibAndEigenInfo> VibAndEigenInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承列有
        /// </summary>
        public List<BearingInfoResult> BearingInfoList
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 振动和特征值
    /// </summary>
    public class VibAndEigenInfo
    {
        /// <summary>
        /// 振动信号ID
        /// </summary>
        public int SingalID
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号类型ID
        /// </summary>
        public int VibrationSignalTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号类型名称
        /// </summary>
        public string VibrationSignalName
        {
            get;
            set;
        }

        /// <summary>
        /// 上限ID
        /// </summary>
        public int UpperLimitID
        {
            get;
            set;
        }

        /// <summary>
        /// 上限值
        /// </summary>
        public int UpperLimitValue
        {
            get;
            set;
        }

        /// <summary>
        /// 下限ID
        /// </summary>
        public int? LowLimitID
        {
            get;
            set;
        }

        /// <summary>
        /// 下限值
        /// </summary>
        public int? LowLimitValue
        {
            get;
            set;
        }

        /// <summary>
        /// 波长ID
        /// </summary>
        public int? WaveLengthID
        {
            get;
            set;
        }

        /// <summary>
        /// 波长值
        /// </summary>
        public int? WaveLengthValue
        {
            get;
            set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate
        {
            get;
            set;
        }
        /// <summary>
        /// 特征值列表
        /// </summary>
        public List<EigenValueInfo> EigenValueInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-15
        /// 创建记录：特征值波长ID
        /// </summary>
        public int? EigenValueWaveLengthID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-15
        /// 创建记录：特征值波长值
        /// </summary>
        public int? EigenValueWaveLengthValue
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-15
        /// 创建记录：采集时间周期
        /// </summary>
        public int? SamplingTimePeriod
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-15
        /// 创建记录：包络滤波器上限ID
        /// </summary>
        public int? EnvelopeFilterUpLimitFreqID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-15
        /// 创建记录：包络滤波器上限值
        /// </summary>
        public int? EnvelopeFilterUpLimitFreqValue
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-15
        /// 创建记录：包络滤波器下限ID
        /// </summary>
        public int? EnvelopeFilterLowLimitFreqID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-15
        /// 创建记录：包络滤波器下限值
        /// </summary>
        public int? EnvelopeFilterLowLimitFreqValue
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 轴承信息
    /// </summary>
    public class BearingInfoResult : EntityBase
    {
        /// <summary>
        /// 轴承ID
        /// </summary>
        public int BearingID
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承厂商ID
        /// </summary>
        public string FactoryID
        {
            get;
            set;
        }

        /// <summary>
        /// 轴承厂商
        /// </summary>
        public string FactoryName
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
        /// 润滑形式
        /// </summary>
        public string LubricatingForm
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 特征值
    /// </summary>
    public class EigenValueInfo
    {
        /// <summary>
        /// 报警值ID
        /// </summary>
        public int SingalAlmID
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值ID
        /// </summary>
        public int EigenValueTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值名称
        /// </summary>
        public string EigenValueName
        {
            get;
            set;
        }

        /// <summary>
        /// 上传触发值
        /// </summary>
        public float? UploadTrigger
        {
            get;
            set;
        }

        /// <summary>
        /// 报警阈值
        /// </summary>
        public float AlarmValue
        {
            get;
            set;
        }

        /// <summary>
        /// 危险阈值
        /// </summary>
        public float DangerValue
        {
            get;
            set;
        }

        /// <summary>
        /// 趋势报警预值
        /// </summary>
        public float? ThrendAlarmPrvalue
        {
            get;
            set;
        }

        /// <summary>
        /// 振动信号Id
        /// </summary>
        public int SingalID
        {
            get;
            set;
        }

        /// <summary>
        /// 特征值添加时间
        /// </summary>
        public DateTime AddDate
        {
            get;
            set;
        }
    }
}