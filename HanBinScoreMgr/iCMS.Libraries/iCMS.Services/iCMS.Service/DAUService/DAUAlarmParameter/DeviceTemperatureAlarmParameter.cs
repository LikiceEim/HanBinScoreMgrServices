using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Service.Web.DAUService.DAUAlarmParameter
{
    #region 设备温度报警参数
    /// <summary>
    /// 设备温度报警参数
    /// </summary>
    public class DeviceTemperatureAlarmParameter
    {
        /// <summary>
        /// 监测树列表
        /// </summary>
        public List<MonitorTree> MonitorTrees { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public Device Dev { get; set; }
        /// <summary>
        /// 测量位置
        /// </summary>
        public MeasureSite MSite { get; set; }
        /// <summary>
        /// 测量位置温度阈值配置
        /// </summary>
        public TempeDeviceSetMSiteAlm MSiteAlmSet { get; set; }
        /// <summary>
        /// 温度数据
        /// </summary>
        public float? HisDataValue { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime SamplingTime { get; set; }
    }
    #endregion
}