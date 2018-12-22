using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Statistics
{
    /// <summary>
    /// 数据单元
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        /// 测点ID
        /// </summary>
        public int MeasureSiteID { get; set; }
        /// <summary>
        /// 测点类型ID
        /// </summary>
        public int MeasureSiteTypeID { get; set; }
        /// <summary>
        /// 测点类型名称
        /// </summary>
        public string MeasureSiteTypeName { get; set; }
        /// <summary>
        /// 真实测点类型名称
        /// </summary>
        public string RealMeasureSiteTypeName { get; set; }
        /// <summary>
        /// 报警类型
        /// </summary>
        public int AlarmType { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime? SamplingDate { get; set; }
        /// <summary>
        /// 采集值
        /// </summary>
        public float? SamplingValue { get; set; }
        /// <summary>
        /// 高报
        /// </summary>
        public float? Warn { get; set; }
        /// <summary>
        /// 高高报 
        /// </summary>
        public float? Alarm { get; set; }
    }

    public class EigenValueData
    {
        /// <summary>
        /// 特征值ID
        /// </summary>
        public int EigenValueID { get; set; }
        /// <summary>
        /// 特征值名称
        /// </summary>
        public string EigenValueName { get; set; }
        /// <summary>
        /// 振动信号类型ID
        /// </summary>
        public int SignalTypeID { get; set; }
        /// <summary>
        /// 振动信号类型名称
        /// </summary>
        public string SignalName { get; set; }
        /// <summary>
        /// 振动信号单位
        /// </summary>
        public string SignalUnit { get; set; }
        /// <summary>
        /// 测点数据
        /// </summary>
        public List<DataPoint> DataPointList { get; set; }

        public EigenValueData()
        {
            this.DataPointList = new List<DataPoint>();
        }
    }

    public class DeviceRealTimeData
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 设备类型ID
        /// </summary>
        public int DeviceTypeID { get; set; }

        /// <summary>
        /// 是否停机
        /// </summary>
        public bool IsStopCrital { get; set; }

        /// <summary>
        /// 特征值分类数据
        /// </summary>
        public List<EigenValueData> EigenValueDataList { get; set; }

        public DeviceRealTimeData()
        {
            this.EigenValueDataList = new List<EigenValueData>();
        }
    }

    /// <summary>
    /// 获取设备实时数据返回结果类
    /// </summary>
    public class GetDeviceRealTimeDataResult
    {
        public List<DeviceRealTimeData> DeviceRealTimeDataList { get; set; }

        public int Total { get; set; }

        public GetDeviceRealTimeDataResult()
        {
            this.DeviceRealTimeDataList = new List<DeviceRealTimeData>();
        }
    }
}
