using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Service.Web.DAUService.DAUAlarmParameter
{
    public class DeviceVibtationAlarmParameter
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
        /// 振动信号
        /// </summary>
        public VibSingal Signal { get; set; }

        /// <summary>
        /// 振动信号特征值配置
        /// </summary>
        public SignalAlmSet AlmSet { get; set; }

        /// <summary>
        /// 振动历史数据
        /// </summary>
        public float? HisDataValue { get; set; }

        public DateTime SamplingTime { get; set; }
    }
}